var RendererGridColumnComboBox = function (value, metadata, record, rowIndex, colIndex, store, view) {
    if (!view) {
        return value;
    }
    var cs = view.panel.headerCt.getGridColumns();
    var items = cs[colIndex].getEditor().items.items[0].store.data.items;
    return showInfo(value, items);
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
var showInfoSearch = function (value, items, record) {
    if (!value) {
        return value;
    }
    for (var i = 0; i < items.length; i++) {
        if (items[i].data.field1.toString().trim() == value.toString().trim()) {
            record.set("MaterialCode", items[i].data.field1);
            record.set("MaterialName", items[i].data.field2);
            break;
        }
    }
};
var RendererGridColumnComboBoxSearch = function (value, metadata, record, rowIndex, colIndex, store, view) {
    if (!view) {
        return value;
    }
    var level = record.data.MaterialCode;
    var cs = view.panel.headerCt.getGridColumns();
    var items = cs[colIndex].getEditor().items.items[0].store.data.items;
    showInfoSearch(value, items, record);
    if (record.data.MaterialName != record.data.MaterialName) {
        record.set("MaterialCode", "");
        record.set("MaterialName", "");
    }
    if (level != record.data.RecipeEquipCode) {
        //try { refreshRowNum(store); } catch (ex) { }
    }
    return record.data.MaterialName;

};

var refreshRowNum = function (store) {
    for (var i = 0; i < store.data.items.length; i++) {
        store.data.items[i].index = i;
    }
    store.loadRecords(store.data.items, true);
}



//var showInfo = function (value, items) {
//    if (!value) {
//        return value;
//    }
//    for (var i = 0; i < items.length; i++) {
//        if (items[i].data.field1.toString().trim() == value.toString().trim()) {
//            return items[i].data.field2;
//        }
//    }
//    return value;
//};
var RendererGridColumnComboBox = function (value, metadata, record, rowIndex, colIndex, store, view) {
    if (!view) {
        return value;
    }
    var cs = view.panel.headerCt.getGridColumns();
    var items = cs[colIndex].getEditor().items.items[0].store.data.items;
    return showInfo(value, items);
};


//Ext.create("Ext.window.Window", {
//    id: "Manager_BasicInfo_CommonPage_QueryPmtRecipe_Window",
//    height: 450,
//    hidden: true,
//    width: 370,
//    html: "<iframe src='../../../BasicInfo/CommonPage/QueryPmtRecipe.aspx?EquipType=01' width=100% height=100% scrolling=no  frameborder=0></iframe>",
//    bodyStyle: "background-color: #fff;",
//    closable: true,
//    title: "请选择工艺配方",
//    modal: true
//})
//var Manager_BasicInfo_CommonPage_QueryPmtRecipe_Request = function (record) {
//    var queryWindow = App.Manager_BasicInfo_CommonPage_QueryPmtRecipe_Window;
//    var thisIsAddWindow = function (record) {
//    }
//    var thisIsEditWindow = function (record) {

//    }
//    var thisIsDefaultWindow = function (record) {
//        App.txtRecipeObjID.setValue(record.data.ObjID);
//    }
//    thisIsAddWindow(record);
//    thisIsEditWindow(record);
//    thisIsDefaultWindow(record);
//    queryWindow.close();
//    gridPanelRefresh();
//}
//var QueryPmtRecipeInfo = function () {
//    App.Manager_BasicInfo_CommonPage_QueryPmtRecipe_Window.show();
//}


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
var SetEditable = function () {
    
    var can = true;
    try {
        can = !parent.App.btnSave.disabled;
    }
    catch (ex) {

    }
    App.ErroeAllow.setReadOnly(!can);
    App.ErroeAllowMove.setReadOnly(!can);
    App.CTabletSpeed.setReadOnly(!can);
    App.CTabletThick.setReadOnly(!can);
    App.CTabletTemp.setReadOnly(!can);
    App.CTabletWeigh.setReadOnly(!can);
    App.CIsUseCode.setReadOnly(!can);
    App.CIsUseCutter.setReadOnly(!can);
    App.CCutterNum.setReadOnly(!can);
    App.RubErroeAllow.setReadOnly(!can);
    App.BatchWeigh.setReadOnly(!can);
    App.DrugNum.setReadOnly(!can);
    App.DrugTime.setReadOnly(!can);
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
  
}
