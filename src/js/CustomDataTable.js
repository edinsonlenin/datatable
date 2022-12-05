$.fn.dataTable.render.luxon = function (from, to, locale) {
    let DateTime = luxon.DateTime;
    // Argument shifting
    if (arguments.length === 1) {
        locale = 'es';
        to = from;
        from = null;
    }
    else if (arguments.length === 2) {
        locale = 'es';
    }

    return function (d, type, row) {
        if (!d) {
            return type === 'sort' || type === 'type' ? 0 : d;
        }

        return (from ? DateTime.fromFormat(from) : DateTime.fromISO(d))
            .setLocale(locale)
            .toFormat(to);
    };
};

class BaseDataTable {
    #_tableId;
    #datatableObject;
    #_options;
    #_nestedDatatables = {};
    #_parentTable;

    #defaults = {
        dom: 't',
        ordering: false,
        searching: false,
        paging: false,
        editMode: 'dblclick',
        shareSelectionWithChildren: true,
        initialExpanded: true,
        colResize: {
            isEnabled: true,
            saveState: false,
            hasBoundCheck: true
        },
        language: {
            emptyTable: "",
            zeroRecords: ""
        }
    };

    get parentTable() {
        return this.#_parentTable;
    }

    set parentTable(value) {
        this.#_parentTable = value;
    }

    get tableId() {
        return this.#_tableId;
    }

    set tableId(value) {
        this.#_tableId = value;
    }

    get options() {
        return this.#_options;
    }

    set options(value) {
        this.#_options = value;
    }

    get datatableObject() {
        return this.#datatableObject;
    }

    get nestedDatatables() {
        return this.#_nestedDatatables;
    }

    constructor(tableId, options) {
        this.tableId = tableId;
        this.options = { ...this.#defaults, ...options };
    }

    init() {
        let that = this;
        this.#_parentTable = this.options.parentTable;
        this.#datatableObject = $(this.tableId).DataTable(this.#_options);

        if (this.options.clickHandler instanceof Function) {
            $(this.tableId).on('click', 'tbody td', function (e) {
                let closestTable = $(this).closest("table")[0];
                if (closestTable !== that.datatableObject.table().node()) return false;

                let clickedRow = $($(this).closest('td')).closest('tr');
                let rowData = that.datatableObject.row(clickedRow).data();

                that.options.clickHandler(this, rowData);
            });
        }

        if (this.options.dblClickHandler instanceof Function) {
            $(this.tableId).on('dblclick', 'tbody tr td', function (e) {
                let closestTable = $(this).closest("table")[0];
                if (closestTable !== that.datatableObject.table().node()) return false;

                let clickedRow = $($(this).closest('td')).closest('tr');
                let rowData = that.datatableObject.row(clickedRow).data();
                that.options.dblClickHandler(this, rowData);
            });
        }

        if (this.options.rowClickHandler instanceof Function) {
            $(this.tableId).on('click', 'tbody tr', function (e) {
                let closestTable = $(this).closest("table")[0];
                if (closestTable !== that.datatableObject.table().node()) return false;

                let clickedRow = $(this);
                let rowData = that.datatableObject.row(clickedRow).data();

                that.options.rowClickHandler(this, rowData);
            });
        }

        if (this.options.rowDblClickHandler instanceof Function) {
            $(this.tableId).on('dblclick', 'tbody tr', function (e) {
                let closestTable = $(this).closest("table")[0];
                if (closestTable !== that.datatableObject.table().node()) return false;

                let clickedRow = $(this);
                let rowData = that.datatableObject.row(clickedRow).data();

                that.options.rowDblClickHandler(this, rowData);
            });
        }

        if (!this.options.select) {
            $(this.tableId).on('click', 'tbody tr', function () {
                if (!$(this).hasClass('selected')) {
                    that.datatableObject.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }
            });
        }

        $(this.tableId).on('click', 'tbody td.dt-control', async function () {
            let closestTable = $(this).closest("table")[0];
            if (closestTable !== that.datatableObject.table().node()) return false;

            let tr = $(this).closest('tr');
            that.expandRow(tr);
        });


        if (that.options.shareSelectionWithChildren) {
            this.#datatableObject.on('select', (e, dt, type, indexes) => {
                if (type === 'row') {
                    if (e.target === e.currentTarget) {
                        $("body").trigger("onSelected", [dt, indexes]);
                    }
                }
            });

            $("body").on("onSelected", (event, datatable, indexes) => {
                if (datatable.table().node().id !== that.datatableObject.table().node().id) {
                    this.deselectAllItems();
                }
            });
        }

        if (this.options.initialExpanded) {
            let expanders = $(this.tableId).find('tbody td.dt-control').toArray();

            if (expanders.length > 0) {
                expanders.forEach(p => {
                    let currentTr = p.closest("tr");
                    that.expandRow($(currentTr));
                });
            }
        }
    }

    async expandRow($tr) {
        let tr = $tr;
        let row = this.datatableObject.row(tr);

        if (row.child.isShown()) {
            row.child.hide();
            tr.removeClass('shown');

            let nestedDatatable = this.nestedDatatables[row.id()];
            if (nestedDatatable) {
                nestedDatatable.destroy();
            }
        } else {
            if (this.options.renderRowExpanded instanceof Function) {
                let expandedContent = await this.options.renderRowExpanded(row.data());
                row.child(expandedContent).show();

                if (this.options.renderRowExpanded instanceof Function) {
                    let response = await this.options.onRowExpand(row);
                    if (response instanceof BaseDataTable) {
                        this.nestedDatatables[row.id()] = response;
                    }
                }

                tr.addClass('shown');
            }
        }
    }

    deselectAllItems() {
        this.#datatableObject.rows({ selected: true }).deselect();
    }

    deselectParentItems() {
        if (!this.parentTable) return;

        this.parentTable.deselectAllItems();
        this.parentTable.deselectParentItems();
    }

    deselectChildItems() {
        let keys = Object.keys(this.#_nestedDatatables);
        if (keys.length == 0) return;

        keys.forEach(p => {
            this.#_nestedDatatables[p].deselectAllItems();
            this.#_nestedDatatables[p].deselectChildItems();
        });
    }

    destroy() {
        $(this.tableId).off('dblclick');
        $(this.tableId).off('blur');
        $(this.tableId).off('click');
        $(this.tableId).off('keyup');
        $(this.tableId).off('input');

        this.datatableObject.destroy();
    }

    getSelectedData() {
        let that = this;
        let mainTableSelection = this.datatableObject.rows({ selected: true }).data().toArray();

        if (this.options.shareSelectionWithChildren) {
            if (mainTableSelection.length == 0) {
                let keys = Object.keys(this.nestedDatatables);
                if (keys.length == 0) return [];

                let currentSelectedChild = [];
                keys.forEach(p => {
                    currentSelectedChild = that.nestedDatatables[p].getSelectedData();
                    if (currentSelectedChild.length > 0) {
                        return currentSelectedChild;
                    }
                });

                return currentSelectedChild;
            }
        }

        return mainTableSelection;
    }

    getRowData(rowId) {
        return this.datatableObject.row(`#${rowId}`).data();
    }

    getAllData() {
        return this.datatableObject.rows().data();
    }

    updateRowData(rowId, newRowData) {
        this.datatableObject.row(`#${rowId}`).data(newRowData);
    }

    refresh() {
        this.datatableObject.draw();
        this.datatableObject.rows().invalidate();
    }

    reload(data) {
        this.datatableObject.rows().deselect();

        if (data) {
            this.datatableObject.clear();
            this.datatableObject.rows.add(data).draw();
        }
    }

    fnResetControls() {
        let that = this;
        $(this.tableId).find("td.editing").removeClass('editing');

        let rowsModified = $(this.tableId).find('tr').has("input, select");

        $.each(rowsModified, function (k, $row) {
            let rowData = that.datatableObject.row($row).data();
            that.datatableObject.row($row).data(rowData);
        });
    }
}

class EditableDataTable extends BaseDataTable {
    constructor(tableId, options) {
        super(tableId, options);
    }

    init() {
        super.init();
        let that = this;

        $(this.tableId).on(this.options.editMode, 'tbody td.editable:not(.editing)', function (e) {
            let closestTable = $(this).closest("table")[0];
            if (closestTable !== that.datatableObject.table().node()) return false;

            that.#editCell(this);
        });

        $(this.tableId).on('blur', 'tbody tr td > *[data-field]', function (evt) {
            let closestTable = $(this).closest("table")[0];
            if (closestTable !== that.datatableObject.table().node()) return false;

            let $cell = $(this).closest("td");
            let $row = $(this).closest("tr");
            let newValue = $(this).val();
            let cellIndexData = that.datatableObject.cell($cell).index();
            let columnOptions = that.options.columns[cellIndexData.column];
            let currentValue = that.datatableObject.cell($cell).data() ?? '';

            newValue = that.#formatValue(newValue, currentValue, columnOptions);

            if (newValue.toString() !== currentValue.toString()) {
                let isValid = true;
                if (that.options.validateValue instanceof Function) {
                    isValid = that.options.validateValue(newValue, that.datatableObject.cell($cell), that.datatableObject.row($row));
                }

                if (isValid) {
                    that.datatableObject.cell($cell).data(newValue);
                    $cell.addClass("modified");

                    if (that.options.valueChanged instanceof Function) {
                        that.options.valueChanged(
                            currentValue,
                            that.datatableObject.cell($cell),
                            that.datatableObject.row($row));
                    }

                    if (that.parentTable) {
                        that.parentTable.childValuechanged(
                            currentValue,
                            that.datatableObject.cell($cell),
                            that.datatableObject.row($row));
                    }
                }
            }

            $(that.tableId).off('keyup');
            $(that.tableId).off('input');

            that.fnResetControls();
        });
    }

    childValuechanged($childCell, $childRow, datatableObject) {
        this.options.childValuechanged($childCell, $childRow, datatableObject);
    }

    #editRow($cell) {
        this.#fnResetControls();
        let clickedRow = $($($cell).closest('td')).closest('tr');

        $(clickedRow).find('td').each(function (index) {
            this.#editCell(this);
        });

        return clickedRow;
    }

    #editCell($cell) {
        if ($($cell).hasClass('editable')) {
            let dataValue = this.datatableObject.cell($cell).data();
            let cellIndexData = this.datatableObject.cell($cell).index();
            let columnOptions = this.options.columns[cellIndexData.column];
            let dataName = columnOptions.data;

            $($cell).addClass('editing');

            let html;
            if ($($cell).hasClass('text')) {
                html = this.#fnCreateTextBox(dataValue, dataName, columnOptions);
                $($cell).html($(html));

                $($cell).find("input").focus();
            }

            if ($($cell).hasClass('dropdown')) {
                html = this.#fnCreateDropDown(dataValue, dataName, columnOptions);

                $($cell).html($(html));
                $($cell).find("select").focus();
            }

            if ($($cell).hasClass('date')) {
                html = this.#fnCreatedate(dataValue, dataName);
                $($cell).html($(html));

                $($cell).find("input").focus();
            }
        }
    }

    #fnResetControls() {
        let that = this;
        $(this.tableId).find("td.editing").removeClass('editing');

        let rowsModified = $(this.tableId).find('tr').has("input, select");

        $.each(rowsModified, function (k, $row) {
            let rowData = that.datatableObject.row($row).data();
            that.datatableObject.row($row).data(rowData);
        });
    }

    #formatValue(newValue, currentValue, columnOptions) {
        if (columnOptions.datatype === 'decimal') {
            let splitted = newValue.split('.');

            if (splitted.some(p => !isNaN(parseInt(p)))) {
                return `${splitted[0] !== '' ? splitted[0] : '0'}.${splitted[1] !== '' ? splitted[1] : '0'}`;
            }

            return '';
        }

        return newValue;
    }

    #fnCreateTextBox(value, fieldprop, columnOptions) {
        let that = this;
        let type = columnOptions.datatype ?? 'free';

        let $input = `<input class="form-control form-control-sm" data-field="${fieldprop}" type="text" value="${value ?? ''}" />`;

        $(this.tableId).on("keyup", $($input), this.#fnOnKeyUpHandler);
        $(this.tableId).on("input", $($input), function (event) { that.#fnOnTextInputHandler(event, type); });

        return $input;
    }

    #fnOnTextInputHandler(event, datatype) {
        let target = event.target;
        if (datatype === 'integer') {
            target.value = target.value.replace(/[^0-9]/g, '');
        } else if (datatype === 'decimal') {
            let regex = new RegExp(/^((\d+(\.\d*)?)|(\.\d*))$/);
            let currentValue = target.value;
            let isValid = regex.test(currentValue);

            if (!isValid) {
                let match = currentValue.match(/^((\d+(\.\d*)?)|(\.\d*))/g);
                target.value = match ? match[0] : '';
            }
        } else if (datatype === 'alphanumeric') {
            target.value = target.value.replace(/[^a-zA-Z0-9]/g, '');
        } else if (datatype === 'alphanumeric_space') {
            target.value = target.value.replace(/[^a-zA-Z0-9 ]/g, '');
        }
    }

    #fnOnKeyUpHandler(event) {
        if (event.originalEvent.key === 'Enter') { event.target.blur(); }
    }

    #fnCreateDropDown(value, fieldprop, columnOptions) {
        let idColumn = columnOptions.idColumn;
        let textColumn = columnOptions.textColumn;
        let arrayList = columnOptions.list;
        value = value ?? '';

        return `<select class="form-select form-select-sm" data-field="${fieldprop}">${arrayList.map(p => `<option value='${p[idColumn]}' ${p[idColumn]?.toString() === value.toString() ? 'selected' : ''}>${p[textColumn]}</option>`).join()}</select>`;
    }

    #fnCreatedate(value, fieldprop) {
        let date, month, year;
        let fecha;
        if (value.length == 10) {
            value = value + "T00:00:00";
        }
        var inputDate = new Date(value);
        date = inputDate.getDate();
        month = inputDate.getMonth() + 1;
        year = inputDate.getFullYear();
        date = date.toString().padStart(2, '0');
        month = month.toString().padStart(2, '0');
        fecha = year + '-' + month + '-' + date;
        //${ value }
        let $input = `<input class="form-control form-control-sm" data-field="${fieldprop}" type="date" value="${fecha}" ></input>`;
        return $input;
    }
}
function formatDefault(element){
    let s = 4;
    if (element.class == 'text-center')
        s = 4;
    else if (element.class == 'text-start')
        s = 5;
    else if (element.class == 'text-end')
        s = 6;
    return s;
}
function buttons(options){
    let {tabla, filtro, align_header, formatData} = options;
    if(align_header === undefined)
        align_header = 'start';
    if(formatData === undefined)
        formatData = formatDefault;
    return [
            {
                extend: 'excelHtml5',
                title: '',
                exportOptions: {
                    columns: tablaColumnasExportar
                },
                customizeData: function (data) {
                    let tcd = eval(`${tabla}ColumnasDetalle`);
                    let datos = eval(`${tabla}Data`);
                    let subchild = [...data.body];
                    let secondTable = ['', ...tcd.map(x => x.title)];
                    let columnas = tcd.map(x => x.data)
                    let numberRow = 0;
                    subchild.forEach((e, i) => {
                        let filas = datos.find(x => eval(filtro)).Detalle;
                        ++numberRow;
                        parentRowsIndex.push(numberRow);
                        filas.forEach((el, idx) => {
                            let detalle = [""];
                            Object.entries(el).forEach(([key, value]) => {
                                if (columnas.includes(key))
                                    detalle.push(value);
                                console.log(detalle);
                            })
                            data.body.splice(numberRow, null, secondTable);
                            ++numberRow;
                            secondRowsIndex.push(numberRow);
                            data.body.splice(numberRow, null, detalle)
                            ++numberRow;
                        })
                    })
                },
                customize: function (xlsx) {
                    let tc = eval(`${tabla}Columnas`);
                    let tce = eval(`${tabla}ColumnasExportar`);
                    let tcd = eval(`${tabla}ColumnasDetalle`);
                    let style = xlsx.xl['styles.xml'];
                    let s = '1';
                    $("styleSheet", style).replaceWith(styleUwg);

                    let sheet = xlsx.xl.worksheets['sheet1.xml'];
                    let sheetPr = `<sheetPr><outlinePr summaryBelow="0"/></sheetPr>`;
                    let indice = 1;
                    let position;
                    $("worksheet", sheet).prepend(sheetPr);
                    $("sheetFormatPr", sheet).attr("outlineLevelRow", "1");

                    $('row', sheet).each(function (i, element) {
                        if (!parentRowsIndex.includes(i)) {
                            $(element).attr('outlineLevel', '1');
                            $(element).attr('hidden', '1');
                        }
                        s = '1';
                        if (!secondRowsIndex.includes(i)) {
                            if (parentRowsIndex.includes(i))
                                $(element).find('c').each(function (j) {
                                    position = tce[j];
                                    $(this).removeAttr('s');
                                    s = formatData(tc[position]);
                                    $(this).attr('s', s);           
                                    
                                })
                            else
                                $(element).find('c').each(function (j) {
                                    $(this).removeAttr('s');
                                    s = formatData(tcd[j]);
                                    $(this).attr('s', s); 
                                })
                        }
                        else {
                            
                            if (align_header === 'center')
                                s = '1'
                            else if (align_header === 'start')
                                s = '2'
                            else if (align_header === 'end')
                                s = '3'

                            $(element).find('c').each(function (k) {
                                    $(this).attr('s', s);
                            })
                        }

                    })
                }
            }
        ];
}
