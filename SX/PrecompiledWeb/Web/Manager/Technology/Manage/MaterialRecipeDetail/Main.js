

var refreshMain = function () {
    var tabid = "id=47";
    var tabp = parent.App.mainTabPanel;
    var tab = tabp.getComponent(tabid);
    if (tab) {
        //tab.reload(true);
        tabp.setActiveTab(tab);
        var window = tab.getBody().dom.firstChild.contentWindow;
        window.refreshLoaction();
    }
    return false;
}


var updateTotal = function (grid, container) {
    if (!grid.view.rendered) {
        return;
    }

    var simpleRenderer = function (v) {
        return v;
    };
    var field,
        value,
        width,
        c,
        cs = grid.headerCt.getVisibleGridColumns();

    container.suspendLayout = true;
    for (var i = 0; i < cs.length; i++) {
        c = cs[i];
        if (c.summaryType) {
            value = App.groupingSummaryWeigth.getSummary(grid.store, c.summaryType, c.dataIndex);
        }
        else {
            value = "-";
        }

        field = container.down('component[name="' + c.dataIndex + '"]');

        container.remove(field, false);
        container.insert(i, field);
        width = c.getWidth();
        field.setWidth(width - 1);
        var r = c.summaryRenderer || simpleRenderer;
        var fvalue = r(value, {}, c.dataIndex);
        field.setValue(fvalue);
    }

    container.items.each(function (field) {
        try {
            var column = grid.headerCt.down('component[dataIndex="' + field.name + '"]');
            field.setVisible(column.isVisible());
        }
        catch
            (ex) {
        }
    });

    container.suspendLayout = false;
    container.doLayout();
};

var pnlWeightIsFirstShow = true;
var pnlWeightOnShow = function (sender, fn) {
    if (pnlWeightIsFirstShow) {
        pnlWeightIsFirstShow = false;
        App.gridPanelWeight.view.refresh();
    }
}
var pnlMinxingIsFirstShow = true;
var pnlMinxingOnShow = function (sender, fn) {
    if (pnlMinxingIsFirstShow) {
        pnlMinxingIsFirstShow = false;
        App.gridPanelMinxing.view.refresh();
    }
}


var after = function () {
    App.waitProgressWindow.close();
}
var before = function () {
    App.waitProgressWindow.show();
}


var onAfterFloatLayout = function (sender, fn) {
    alert("onAfterFloatLayout");
}


Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
    height: 450,
    hidden: true,
    width: 370,
    html: "<iframe src='../../../BasicInfo/CommonPage/QueryMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择物料信息",
    modal: true
})
var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {
    var queryWindow = App.Manager_BasicInfo_CommonPage_QueryMaterial_Window;
    var thisIsAddWindow = function (record) {
    }
    var thisIsEditWindow = function (record) {

    }
    var thisIsDefaultWindow = function (record) {
        App.txtRecipeMaterialCode.setValue(record.data.MaterialCode);
        App.txtRecipeMaterialName.setValue(record.data.MaterialName);
    }
    thisIsAddWindow(record);
    thisIsEditWindow(record);
    thisIsDefaultWindow(record);
    queryWindow.close();
}
var QueryMaterialInfo = function (field, trigger, index) {
    App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
}


//点击插入按钮
var commandcolumn_insert = function (command, record) {
    if (command.toLowerCase() != "insert") {
        return;
    }
    var index = record.data.SeqIdx;
    grid = App.gridPanelCenter;
    store = grid.store;
    var r = Ext.create('gridPanelCenterStoreModel', {
        SeqIdx: index
    })

    for (i = 0; i < store.data.items.length ; i++) {
        if (i >= store.pageSize - 1) {
            store.data.removeAt(i);
            i--;
            continue;
        }
        if (store.data.items[i].data.SeqIdx > index) {
            store.data.items[i].data.SeqIdx++;
        }
    }
    record.data.SeqIdx++;
    store.data.add(r);
    grid.store.loadRecords(store.data.items, false);
}
//点击添加按钮
var commandcolumn_add = function (command, record) {
    if (command.toLowerCase() != "add") {
        return;
    }
    var index = record.data.SeqIdx;
    grid = App.gridPanelCenter;
    store = grid.store;
    var r = Ext.create('gridPanelCenterStoreModel', {
        SeqIdx: index + 1
    })

    for (i = 0; i < store.data.items.length ; i++) {
        if (i >= store.pageSize - 1) {
            store.data.removeAt(i);
            i--;
            continue;
        }
        if (store.data.items[i].data.SeqIdx > index) {
            store.data.items[i].data.SeqIdx++;
        }
    }
    store.data.add(r);
    grid.store.loadRecords(store.data.items, false);
}
//点击删除按钮
var commandcolumn_delete = function (command, record) {
    if (command.toLowerCase() != "delete") {
        return;
    }
    var index = record.data.SeqIdx;
    grid = App.gridPanelCenter;
    store = grid.store;

    for (i = 0; i < store.data.items.length ; i++) {
        if (i >= store.pageSize) {
            store.data.removeAt(i);
            i--;
            continue;
        }
        if (store.data.items[i].data.SeqIdx > index) {
            store.data.items[i].data.SeqIdx--;
        }
    }
    store.data.removeAt(record.index);
    var r = Ext.create('gridPanelCenterStoreModel', {
        SeqIdx: store.pageSize
    })
    store.data.add(r);
    grid.store.loadRecords(store.data.items, false);
}
//根据按钮类别进行删除和编辑操作
var commandcolumn_click = function (command, record) {
    commandcolumn_insert(command, record);
    commandcolumn_add(command, record);
    commandcolumn_delete(command, record);
    return false;
};




var showInfo = function (value, items) {
    if (!value) {
        return value;
    }
    for (var i = 0; i < items.length; i++) {
        if (items[i].data.field1.toString().trim() == value.toString().trim()) {
            return items[i].data.field2;
        }
    }
    return value;
};
var showTemCode = function (value) {
    var items = App.setTemCode.store.data.items;
    return showInfo(value, items);
};
var showActionCode = function (value) {
    var items = App.setActionCode.store.data.items;
    return showInfo(value, items);
};



var saveInfo = function () {

    var typejson = "[]";

    var grid = App.gridPanelCenter;
    var store = grid.store;
    arr = new Array();
    Ext.each(store.data.items, function (record) {
        arr.push(record.data);
    });
    var datajson = Ext.encode(arr);


    App.direct.SaveGridInfo(typejson, datajson, {
        success: function (result) {
            if (result == "") {
                Ext.Msg.alert('成功', '很好很强大');
                gridPanelRefresh();
            } else {
                Ext.Msg.alert('失败', result);
            }

        },

        failure: function (errorMsg) {
            Ext.Msg.alert('错误', errorMsg);
        }
    });
}



Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
    height: 450,
    hidden: true,
    width: 370,
    html: "<iframe src='../../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择机台",
    modal: true
})
var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {
    var queryWindow = App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window;
    var thisIsAddWindow = function (record) {
    }
    var thisIsEditWindow = function (record) {

    }
    var thisIsDefaultWindow = function (record) {
        App.txtRecipeEquipCode.setValue(record.data.EquipCode);
        App.txtRecipeEquipName.setValue(record.data.EquipName);
    }
    thisIsAddWindow(record);
    thisIsEditWindow(record);
    thisIsDefaultWindow(record);
    App.direct.OneMixDeal(record.data.EquipCode, {
        success: function (result) {

            if (result == "1") {
                parent.App.Panel1.addTab(parent.App.pnlWeight, false);
                parent.App.Panel1.addTab(parent.App.pnlMixing, false);
                parent.App.Panel1.addTab(parent.App.pnlAdvanceSeparateWeight, false);
                parent.App.Panel1.addTab(parent.App.pnlOpenMixing, false);
                parent.App.Panel1.closeTab(parent.App.pnlQDrug);
                parent.App.Panel1.closeTab(parent.App.pnlMILL);
            }
            else
                if (result == "2") {
//                    parent.App.Panel1.closeTab(parent.App.pnlWeight);
//                    parent.App.Panel1.closeTab(parent.App.pnlMixing);
                    parent.App.Panel1.addTab(parent.App.pnlWeight, false);
                    parent.App.Panel1.addTab(parent.App.pnlMixing, false);
                    parent.App.Panel1.closeTab(parent.App.pnlAdvanceSeparateWeight);
                    parent.App.Panel1.closeTab(parent.App.pnlOpenMixing);
                    parent.App.Panel1.addTab(parent.App.pnlQDrug, false);
                    parent.App.Panel1.addTab(parent.App.pnlMILL, false);
                }
                else {
                    parent.App.Panel1.addTab(parent.App.pnlWeight, false);
                    parent.App.Panel1.addTab(parent.App.pnlMixing, false);
                    parent.App.Panel1.closeTab(parent.App.pnlAdvanceSeparateWeight);
                    parent.App.Panel1.closeTab(parent.App.pnlOpenMixing);
                    parent.App.Panel1.closeTab(parent.App.pnlQDrug);
                    parent.App.Panel1.closeTab(parent.App.pnlMILL);
                }

        },

        failure: function (errorMsg) {
            Ext.Msg.alert('错误', errorMsg);
        }
    });

    queryWindow.close();
}
var QueryEquipmentInfo = function (field, trigger, index) {
    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
}

var SetEditable = function () {
    var can = true;
    try {
        can = !parent.App.btnSave.disabled;
    }
    catch (ex) {
    }
    App.txtRecipeMaterialCode.setReadOnly(!can);
    App.txtRecipeEquipName.setReadOnly(!can);
    App.txtRecipeType.setReadOnly(!can);
    App.txtRecipeState.setReadOnly(!can);
    App.txtLotTotalWeight.setReadOnly(!can);
    App.txtShelfLotCount.setReadOnly(!can);
    App.txtLotDoneTime.setReadOnly(!can);
    App.txtOverTimeSetTime.setReadOnly(!can);
    App.txtOverTempSetTemp.setReadOnly(!can);
    App.txtOverTempMinTime.setReadOnly(!can);
    App.txtInPolyMaxTemp.setReadOnly(!can);
    App.txtInPolyMinTemp.setReadOnly(!can);
    App.txtMakeUpTemp.setReadOnly(!can);
    App.txtCarbonRecycleType.setReadOnly(!can);
    App.txtFactory.setReadOnly(!can);
    App.ComboBoxCC.setReadOnly(!can);
    App.txJieDuan.setReadOnly(!can);
    App.txtCarbonRecycleTime.setReadOnly(!can);
    App.txtIsUseAreaTemp.setReadOnly(!can);
    App.txtSideTemp.setReadOnly(!can);
    App.txtRollTemp.setReadOnly(!can);
    App.txtDdoorTemp.setReadOnly(!can);
    App.txtRecipeEquipCode.setReadOnly(!can);

    for (var i = 0; i < App.CheckboxGroupAuditUser.items.items.length; i++) {
        App.CheckboxGroupAuditUser.items.items[i].setReadOnly(!can);
    }
}