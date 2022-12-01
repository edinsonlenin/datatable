$.fn.select2.amd.define('select2/data/customResultsAdapter', [
    'select2/results',
    'select2/utils',
    'select2/defaults',
    'select2/dropdown/selectOnClose'
],
    function (ResultsList, Utils, Defaults, selectOnClose) {
        let dataTable;

        customResultsAdapter = Utils.Decorate(ResultsList, selectOnClose);

        customResultsAdapter.prototype.render = function () {
            
            var $results = $(
                `<table class="cell-border compact tabla-general stripe" role="listbox"></table>`
            );

            if (this.options.get('multiple')) {
                $results.attr('aria-multiselectable', 'true');
            }

            this.$results = $results;

            return $results;
        };

        customResultsAdapter.prototype.option = function (data) {
            var row = document.createElement('tr');
            row.classList.add('select2-results__option');
            row.classList.add('select2-results__option--selectable');     

            var attrs = {
                'role': 'option'
            };

            for (var attr in attrs) {
                var val = attrs[attr];

                row.setAttribute(attr, val);
            }

            this.template(data, row);

            Utils.StoreData(row, 'data', data);

            return row;
        };

        customResultsAdapter.prototype.append = function (data) {
            this.hideLoading();
            var $options = [];

            if (data.results == null || data.results.length === 0) {
                if (this.$results.children().length === 0) {
                    this.trigger('results:message', {
                        message: 'noResults'
                    });
                }

                return;
            }

            data.results = this.sort(data.results);

            for (var d = 0; d < data.results.length; d++) {
                var item = data.results[d];

                var $option = this.option(item);

                $options.push($option);
            }

            let $thead = $("<thead></thead>");
            let $trHead = $("<tr></tr>");
            let $tbody = $("<tbody></tbody>");
            let datatableOptions = this.options.get('datatableOptions');
            let columns = datatableOptions.columns;

            for (let i = 0; i < columns.length; i++) {
                let $th = $("<th></th>");
                $th.text(columns[i].name);

                $trHead.append($th);
            }           

            if (dataTable) {
                dataTable.destroy();
                console.log('destroy');
            }

            $thead.append($trHead);
            $tbody.append($options);

            if (this.$results.find("thead").length == 0) {
                this.$results.append($thead);
            }
            
            this.$results.append($tbody);

            dataTable = new BaseDataTable(this.$results[0], datatableOptions);
            dataTable.init();          
        };

        customResultsAdapter.prototype.bind = function (container, $container) {
            var self = this;

            var id = container.id + '-results';

            this.$results.attr('id', id);

            container.on('results:all', function (params) {
                self.clear();
                self.append(params.data);

                if (container.isOpen()) {
                    self.setClasses();
                    self.highlightFirstItem();
                }
            });

            container.on('results:append', function (params) {
                self.append(params.data);

                if (container.isOpen()) {
                    self.setClasses();
                }
            });

            container.on('query', function (params) {
                self.hideMessages();
                self.showLoading(params);
            });

            container.on('select', function () {
                if (!container.isOpen()) {
                    return;
                }

                self.setClasses();

                if (self.options.get('scrollAfterSelect')) {
                    self.highlightFirstItem();
                }
            });

            container.on('unselect', function () {
                if (!container.isOpen()) {
                    return;
                }

                self.setClasses();

                if (self.options.get('scrollAfterSelect')) {
                    self.highlightFirstItem();
                }
            });

            container.on('open', function () {
                // When the dropdown is open, aria-expended="true"
                self.$results.attr('aria-expanded', 'true');
                self.$results.attr('aria-hidden', 'false');

                self.setClasses();
                self.ensureHighlightVisible();
            });

            container.on('close', function () {
                // When the dropdown is closed, aria-expended="false"
                self.$results.attr('aria-expanded', 'false');
                self.$results.attr('aria-hidden', 'true');
                self.$results.removeAttr('aria-activedescendant');
            });

            container.on('results:toggle', function () {
                var $highlighted = self.getHighlightedResults();

                if ($highlighted.length === 0) {
                    return;
                }

                $highlighted.trigger('mouseup');
            });

            container.on('results:select', function () {
                var $highlighted = self.getHighlightedResults();

                if ($highlighted.length === 0) {
                    return;
                }

                var data = Utils.GetData($highlighted[0], 'data');

                if ($highlighted.hasClass('select2-results__option--selected')) {
                    self.trigger('close', {});
                } else {
                    self.trigger('select', {
                        data: data
                    });
                }
            });

            container.on('results:previous', function () {
                var $highlighted = self.getHighlightedResults();

                var $options = self.$results.find('.select2-results__option--selectable');

                var currentIndex = $options.index($highlighted);

                // If we are already at the top, don't move further
                // If no options, currentIndex will be -1
                if (currentIndex <= 0) {
                    return;
                }

                var nextIndex = currentIndex - 1;

                // If none are highlighted, highlight the first
                if ($highlighted.length === 0) {
                    nextIndex = 0;
                }

                var $next = $options.eq(nextIndex);

                $next.trigger('mouseenter');

                var currentOffset = self.$results.offset().top;
                var nextTop = $next.offset().top;
                var nextOffset = self.$results.scrollTop() + (nextTop - currentOffset);

                if (nextIndex === 0) {
                    self.$results.scrollTop(0);
                } else if (nextTop - currentOffset < 0) {
                    self.$results.scrollTop(nextOffset);
                }
            });

            container.on('results:next', function () {
                var $highlighted = self.getHighlightedResults();

                var $options = self.$results.find('.select2-results__option--selectable');

                var currentIndex = $options.index($highlighted);

                var nextIndex = currentIndex + 1;

                // If we are at the last option, stay there
                if (nextIndex >= $options.length) {
                    return;
                }

                var $next = $options.eq(nextIndex);

                $next.trigger('mouseenter');

                var currentOffset = self.$results.offset().top +
                    self.$results.outerHeight(false);
                var nextBottom = $next.offset().top + $next.outerHeight(false);
                var nextOffset = self.$results.scrollTop() + nextBottom - currentOffset;

                if (nextIndex === 0) {
                    self.$results.scrollTop(0);
                } else if (nextBottom > currentOffset) {
                    self.$results.scrollTop(nextOffset);
                }
            });

            container.on('results:focus', function (params) {
                params.element[0].classList.add('select2-results__option--highlighted');
                params.element[0].setAttribute('aria-selected', 'true');
            });

            container.on('results:message', function (params) {
                self.displayMessage(params);
            });

            if ($.fn.mousewheel) {
                this.$results.on('mousewheel', function (e) {
                    var top = self.$results.scrollTop();

                    var bottom = self.$results.get(0).scrollHeight - top + e.deltaY;

                    var isAtTop = e.deltaY > 0 && top - e.deltaY <= 0;
                    var isAtBottom = e.deltaY < 0 && bottom <= self.$results.height();

                    if (isAtTop) {
                        self.$results.scrollTop(0);

                        e.preventDefault();
                        e.stopPropagation();
                    } else if (isAtBottom) {
                        self.$results.scrollTop(
                            self.$results.get(0).scrollHeight - self.$results.height()
                        );

                        e.preventDefault();
                        e.stopPropagation();
                    }
                });
            }

            this.$results.on('mouseup', '.select2-results__option--selectable',
                function (evt) {
                    var $this = $(this);

                    var data = Utils.GetData(this, 'data');

                    if ($this.hasClass('select2-results__option--selected')) {
                        if (self.options.get('multiple')) {
                            self.trigger('unselect', {
                                originalEvent: evt,
                                data: data
                            });
                        } else {
                            self.trigger('close', {});
                        }

                        return;
                    }

                    self.trigger('select', {
                        originalEvent: evt,
                        data: data
                    });
                });

            this.$results.on('mouseenter', '.select2-results__option--selectable',
                function (evt) {
                    var data = Utils.GetData(this, 'data');

                    self.getHighlightedResults()
                        .removeClass('select2-results__option--highlighted')
                        .attr('aria-selected', 'false');

                    self.trigger('results:focus', {
                        data: data,
                        element: $(this)
                    });
                });
        };

        customResultsAdapter.prototype.template = function (result, container) {
            //var template = this.options.get('templateResult');
            let template = (result, container) => {
                let datatableOptions = this.options.get('datatableOptions');
                let columns = datatableOptions.columns;

                let rowTemplateString = columns.map(p => `<td>${result[p.data]}</td>`).join();
                return $(rowTemplateString);
            };

            let escapeMarkup = this.options.get('escapeMarkup');
            let content = template(result, container);

            if (content == null) {
                container.style.display = 'none';
            } else if (typeof content === 'string') {
                container.innerHTML = escapeMarkup(content);
            } else {
                $(container).append(content);
            }
        };

        return customResultsAdapter;
    }
);
