var refreshRowNum = function (store) {
    for (var i = 0; i < store.data.items.length; i++) {
        store.data.items[i].index = i;
    }
    store.loadRecords(store.data.items, true);
}
//点击插入按钮
var commandcolumn_insert = function (command, store, index) {
    if (command.toLowerCase() != "insert") {
        return;
    }
    var modelName = store.model.getName();
    var r = Ext.create(modelName, {
    })
    store.data.insert(index, r);
    while (store.data.items.length > store.pageSize) {
        store.data.removeAt(store.pageSize);
    }
    refreshRowNum(store);
}
var getOpenMixingStore = function () {


    App.gridPanelMinxing1.submitData();

    var mix1 = App.gridPanelMinxing1.store;

//    App.gridPanelMinxing2.store = App.gridPanelMinxing1.store;
//    alert(storeToJson(mix1));
       App.direct.SaveJsonInfo(storeToJson(mix1),{
                success: function (result) {
                
                    if (result == "") {
                        App.gridPanelMinxing2.store.currentPage = 1;
                        App.gridPanelMinxing2.store.reload();
                        App.gridPanelMinxing3.store.currentPage = 1;
                        App.gridPanelMinxing3.store.reload();
                        App.gridPanelMinxing4.store.currentPage = 1;
                        App.gridPanelMinxing4.store.reload();
                        App.gridPanelMinxing5.store.currentPage = 1;
                        App.gridPanelMinxing5.store.reload();
                        App.gridPanelMinxing6.store.currentPage = 1;
                        App.gridPanelMinxing6.store.reload();
                        setTimeout(function () {   App.direct.DeleteJsonInfo() }, 10000);
                    } else {
                        Ext.Msg.alert('失败', result);
                    }
                },
                failure: function (errorMsg) {
                
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
    

}

var storeToJson = function (store) {
    if (!store) {
        return '';
    }
    if (store.type == "store") {
        arr = new Array();
        Ext.each(store.data.items, function (record) {
            arr.push(record.data);
        });
        return Ext.encode(arr);
    }
    else {
        return Ext.encode(store)
    }
}


//点击删除按钮
var commandcolumn_delete = function (command, store, index) {
    if (command.toLowerCase() != "delete") {
        return;
    }
    for (i = 0; i < store.data.items.length ; i++) {
        if (store.data.items[i].index == index) {
            store.data.removeAt(i);
            break;
        }
    }
    var modelName = store.model.getName();
    var r = Ext.create(modelName, {
    })
    store.data.add(r);
    refreshRowNum(store);
}
//根据按钮类别进行删除和编辑操作
var commandcolumn_click = function (column, command, record) {
    var index = record.index;
    var store = column.grid.store;
    commandcolumn_insert(command, store, index);
    commandcolumn_delete(command, store, index);
    return false;
};

//下拉框展示数据方法
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
//下拉框方法
var RendererGridColumnComboBox = function (value, metadata, record, rowIndex, colIndex, store, view) {
    if (!view) {
        return value;
    }
    var cs = view.panel.headerCt.getGridColumns();
    var items = cs[colIndex].getEditor().items.items[0].store.data.items;
    return showInfo(value, items);
};


Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryPmtOpenActionModelMain_Window",
    height: 450,
    hidden: true,
    width: 370,
    html: "<iframe src='../../../BasicInfo/CommonPage/QueryPmtOpenActionModelMain.aspx?EquipType=01' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择工艺配方",
    modal: true
})
var Manager_BasicInfo_CommonPage_QueryPmtOpenActionModelMain_Request = function (record) {
    var queryWindow = App.Manager_BasicInfo_CommonPage_QueryPmtOpenActionModelMain_Window;
    var thisIsAddWindow = function (record) {
    }
    var thisIsEditWindow = function (record) {

    }
    var thisIsDefaultWindow = function (record) {
        App.txtOpenMainModelId.setValue(record.data.ObjID);
    }
    thisIsAddWindow(record);
    thisIsEditWindow(record);
    thisIsDefaultWindow(record);
    queryWindow.close();
    gridPanelRefresh(false);
}
var QueryPmtOpenActionModelMainInfo = function () {
    App.Manager_BasicInfo_CommonPage_QueryPmtOpenActionModelMain_Window.show();
}

var HeaderClick = function (item, column, e, t) {
    var findItemCount = function (item) {
        var count = 0;
        for (var i = 0; i < App.storeMinxing.data.items.length; i++) {
            var item = App.storeMinxing.data.items[i];
            if (item.data.ActionCode && item.data.ActionCode.length > 0) {
                count = i;
            }
        }
        return count + 1;
    }
    if (column.text == "转速" || column.text == "压力") {
        column.sortable = false;
        App.txtStepStart.setValue("1");
        App.txtStepEnd.setValue(findItemCount(item));
        App.txtSetValue.setValue("0");
        App.txtSetValue.setFieldLabel("批量设置[" + column.text + "]的值为");
        App.winHeaderClick.setTitle("批量设置[" + column.text + "]的值");
        App.hiddenDataIndex.setValue(column.dataIndex);
        App.winHeaderClick.show();
        e.stopEvent();
    }
}
var SetValueByHeader = function () {
    var value = App.txtSetValue.getValue();
    var istart = App.txtStepStart.getValue();
    if (istart - 1 < 0) {
        istart = 1;
    }
    var iend = App.txtStepEnd.getValue();
    if (iend >= App.storeMinxing.data.items.length) {
        iend = App.storeMinxing.data.items.length - 1;
    }
    for (var i = istart - 1; i < iend + 1; i++) {
        var item = App.storeMinxing.data.items[i];
        if (item.data.ActionCode && item.data.ActionCode.length > 0) {
            if (App.hiddenDataIndex.getValue() == "MixingSpeed") {
                item.data.MixingSpeed = (value);
            }
            if (App.hiddenDataIndex.getValue() == "MixingPress") {
                item.data.MixingPress = (value);
            }
        }
    }
    refreshRowNum(App.storeMinxing);
}

var enableEdit = function () {
    var can = true;
    try {
        can = !parent.App.btnSave.disabled;
    }
    catch (ex) {
    }
    return can;
}

//刷新列表方法
var gridPanelRefresh = function (flag) {
    if (flag) {
        App.txtOpenMainModelId.setValue("");
    }
    App.gridPanelMinxing0.store.currentPage = 1;
    App.gridPanelMinxing0.store.reload();
    App.gridPanelMinxing1.store.currentPage = 1;
    App.gridPanelMinxing1.store.reload();
    App.gridPanelMinxing2.store.currentPage = 1;
    App.gridPanelMinxing2.store.reload();
    App.gridPanelMinxing3.store.currentPage = 1;
    App.gridPanelMinxing3.store.reload();
    App.gridPanelMinxing4.store.currentPage = 1;
    App.gridPanelMinxing4.store.reload();
    App.gridPanelMinxing5.store.currentPage = 1;
    App.gridPanelMinxing5.store.reload();
    App.gridPanelMinxing6.store.currentPage = 1;
    App.gridPanelMinxing6.store.reload();
    return false;
}

