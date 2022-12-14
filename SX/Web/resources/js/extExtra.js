Ext.define("override.net.GroupPaging", {
    override: "Ext.net.GroupPaging",
    applyPaging: function (notify, native) {
        var store = this.store,
            allData = store.allData.items,
            groups = this.getGroups(allData),
            items;

        if (allData.length != 0) {
            store.copyAllData(groups[store.currentPage - 1].children, native);
        }

        store.totalCount = groups.length;

        if (notify === true) {
            store.fireEvent("refresh", store);
        }

        store.fireEvent("paging", store);
    }
});

Ext.define("override.net.GridPrinter", {
    override: "Ext.net.GridPrinter",
    statics: {
        getBody: function (grid, columns, data, rowBody) {

            var groups,
                fields = grid.store.getModel().getFields(),
                groupField,
                header,
                feature,
                body;

            if (grid.store.isGrouped()) {
                groups = grid.store.getGroups();
                feature = grid.groupingFeature;
                header = feature.refreshData.header;
                groupField = feature.getGroupField();

                if (!fields || !groupField) {
                    return;
                }

                //修复组分页打印BUG
                var groupcount = 0, tmpGroups = groups;
                for (let i in tmpGroups.emptyGroups) {
                    groupcount++;
                }

                if (groupcount > tmpGroups.length) {
                    groups.items = [];
                    for (let i in tmpGroups.emptyGroups) {
                        groups.items.push(tmpGroups.emptyGroups[i]);
                    }
                    groups.length = tmpGroups.length;
                }


                var bodyTpl = [
                    '<tpl for=".">',
                        '<tr class="group-header">',
                            '<td colspan="{[this.colSpan]}">',
                              (feature.groupHeaderTpl.html || ''),
                            '</td>',
                        '</tr>',
                        '<tpl for="items">',
                            '<tr class="{[xindex % 2 === 0 ? "even" : "odd"]}">',
                                '<tpl for="this.columns">',
                                    '<td>',
                                      '{[ this.getValue(parent, Ext.String.createVarName(values.dataIndex || values.id)) ]}',
                                    '</td>',
                                '</tpl>',
                            '</tr>',
                            '<tr class="{[xindex % 2 === 0 ? "even" : "odd"]}">',
                                 (rowBody || ""),
                            '</tr>',
                        '</tpl>',
                    '</tpl>',
                    {
                        getBodyContent: this.getBodyContent,
                        view: grid.view,
                        columns: columns,
                        fields: fields,
                        colSpan: columns.length,
                        getValue: function (record, name) {
                            return record.convertedData[name];
                        }
                    }
                ];

                groups.each(function (group) {
                    group.groupField = groupField;
                    group.groupValue = group.getGroupKey();
                    group.name = group.getGroupKey();
                    group.columnName = header ? header.text : groupField;
                    group.rows = group.items;

                    Ext.each(group.items, function (record) {
                        var i, len;

                        for (i = 0, len = data.length; i < len; i++) {
                            if (data[i].__internalId == record.internalId) {
                                record.convertedData = data[i];
                                return;
                            }
                        }
                    });
                });

                body = Ext.create('Ext.XTemplate', bodyTpl).apply(groups.items);

                groups.each(function (group) {
                    delete group.groupField;
                    delete group.groupValue;
                    delete group.columnName;
                    delete group.name;
                    delete group.rows;

                    Ext.each(group.items, function (record) {
                        delete record.convertedData;
                    });
                });
            }
            else {
                body = Ext.create('Ext.XTemplate', this.bodyTpl).apply(columns);
            }

            return body;
        }
    }
});