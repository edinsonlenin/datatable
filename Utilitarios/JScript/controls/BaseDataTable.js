class BaseDataTable {
    #_tableId;
    #datatableObject;
    #_options;
    #_nestedDatatables = {};

    #defaults = {
        dom: 't',
        ordering: false,
        searching: false,
        paging: false,
        editMode: 'dblclick',
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
    getDataTableObject() {
        return this.#datatableObject;
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
        this.#datatableObject = $(this.tableId).DataTable(this.#_options);

        if (this.options.clickHandler instanceof Function) {
            $(this.tableId).on('click', 'tbody td', function (e) {
                e.preventDefault();

                let clickedRow = $($(this).closest('td')).closest('tr');
                let rowData = that.datatableObject.row(clickedRow).data();

                that.options.clickHandler(this, rowData);
            });
        }

        if (this.options.dblClickHandler instanceof Function) {
            $(this.tableId).on('dblclick', 'tbody td', function (e) {
                let clickedRow = $($(this).closest('td')).closest('tr');
                let rowData = that.datatableObject.row(clickedRow).data();
                e.preventDefault();
                that.options.dblClickHandler(this, rowData);
            });
        }

        if (this.options.rowClickHandler instanceof Function) {
            $(this.tableId).on('click', 'tbody tr', function (e) {
                e.preventDefault();

                let clickedRow = $(this);
                let rowData = that.datatableObject.row(clickedRow).data();

                that.options.rowClickHandler(this, rowData);
            });
        }

        if (this.options.rowDblClickHandler instanceof Function) {
            $(this.tableId).on('dblclick', 'tbody tr', function (e) {
                e.preventDefault();

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
            let tr = $(this).closest('tr');
            let row = that.datatableObject.row(tr);

            if (row.child.isShown()) {
                row.child.hide();
                tr.removeClass('shown');

                let nestedDatatable = that.nestedDatatables[row.id()];
                if (nestedDatatable) {
                    nestedDatatable.destroy();
                }
            } else {
                let expandedContent = await that.options.renderRowExpanded(row.data());
                row.child(expandedContent).show();

                let response = await that.options.onRowExpand(row);
                if (response instanceof EditableDataTable) {
                    that.nestedDatatables[row.id()] = response;
                }

                tr.addClass('shown');
            }
        });
    }

    destroy() {
        $(this.tableId).off('dblclick');
        $(this.tableId).off('blur');
        $(this.tableId).off('click');

        this.datatableObject.destroy();
    }

    getSelectedData() {
        return this.datatableObject.rows({ selected: true }).data().toArray();
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

    reload(data) {
        this.datatableObject.rows().deselect();
        this.datatableObject.ajax.reload();

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
    #_parentTable;

    constructor(tableId, options) {
        super(tableId, options);
    }

    init() {
        super.init();
        let that = this;
        this.#_parentTable = this.options.parentTable;

        $(this.tableId).on(this.options.editMode, 'tbody td.editable:not(.editing)', function (e) {
            that.#editCell(this);
        });

        $(this.tableId).on('blur', 'tbody tr td > *[data-field]', function (evt) {
            let $cell = $(this).closest("td");
            let $row = $(this).closest("tr");
            let newValue = $(this).val();
            let currentValue = that.datatableObject.cell($cell).data();

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

                    if (that.#_parentTable) {
                        that.#_parentTable.childValuechanged(
                            currentValue,
                            that.datatableObject.cell($cell),
                            that.datatableObject.row($row));
                    }
                }
            }

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
            let dataName = this.options.columns[cellIndexData.column].data;

            $($cell).addClass('editing');

            let html;
            if ($($cell).hasClass('text')) {
                html = this.#fnCreateTextBox(dataValue, dataName);
                $($cell).html($(html));

                $($cell).find("input").focus();
            }

            if ($($cell).hasClass('dropdown')) {
                let arraylist = this.options.columns[cellIndexData.column].list;
                html = this.#fnCreateDropDown(dataValue, dataName, arraylist);

                $($cell).html($(html));
                $($cell).find("select").focus();
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

    #fnCreateTextBox(value, fieldprop) {
        let $input = `<input class="form-control form-control-sm" data-field="${fieldprop}" type="text" value="${value}" ></input>`;
        return $input;
    }

    #fnCreateDropDown(value, fieldprop, arrayList) {
        return `<select class="form-select form-select-sm" data-field="${fieldprop}">${arrayList.map(p => `<option value='${p.GenderId}' ${p.GenderId === parseInt(value.toString()) ? 'selected' : ''}>${p.Name}</option>`).join()}</select>`;
    }
}