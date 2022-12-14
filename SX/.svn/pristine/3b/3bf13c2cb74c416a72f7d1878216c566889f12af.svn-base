

Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryPmtRecipe_Window",
    hidden: true,
    width: 360,
    height: 470,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryPmtRecipe.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择工艺配方",
    modal: true
})

var Manager_BasicInfo_CommonPage_QueryPmtRecipe_Request = function (record) {
    App.txtPmtRecipe.getTrigger(0).show();
    App.hiddenPmtRecipeID.setValue(record.data.ObjID);
    App.txtPmtRecipe.setValue(record.data.MaterialName);
}

var QueryPmtRecipeWindowShow = function () {
    var equipName = App.txtEquipName.getValue();
    if (equipName.length == 0) {
        Ext.Msg.alert('提示', "请选择机台");
        return;
    }
    var window = App.Manager_BasicInfo_CommonPage_QueryPmtRecipe_Window;
    var html = "<iframe src='../../BasicInfo/CommonPage/QueryPmtRecipe.aspx?EquipName=" + equipName + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
    if (window.getBody()) {
        window.getBody().update(html);
    } else {
        window.html = html;
    }
    window.show();
}

var QueryPmtRecipeInfo = function (field, trigger, index) {
    switch (index) {
        case 0:
            field.getTrigger(0).hide();
            field.setValue('');
            App.hiddenPmtRecipeID.setValue('');
            field.getEl().down('input.x-form-text').setStyle('background', "white");
            break;
        case 1:
            QueryPmtRecipeWindowShow();
            break;
    }
}


Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
    hidden: true,
    width: 370,
    height: 470,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择机台",
    modal: true
})

var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {
    App.txtEquipName.getTrigger(0).show();
    App.hiddenEquipCode.setValue(record.data.EquipCode);
    App.txtEquipName.setValue(record.data.EquipName);
}
var QueryEquipInfo = function (field, trigger, index) {
    switch (index) {
        case 0:
            field.getTrigger(0).hide();
            field.setValue('');
            App.hiddenEquipCode.setValue('');
            field.getEl().down('input.x-form-text').setStyle('background', "white");
            break;
        case 1:
            App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
            break;
    }
}


Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
    hidden: true,
    width: 370,
    height: 470,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx?MajorTypeID=4,5' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择物料",
    modal: true
})

var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {
    App.txMaterialName.getTrigger(0).show();
    App.hiddenMaterialCode.setValue(record.data.MaterialCode);
    App.txMaterialName.setValue(record.data.MaterialName);
}
var QueryMaterialInfo = function (field, trigger, index) {
    switch (index) {
        case 0:
            field.getTrigger(0).hide();
            field.setValue('');
            App.hiddenMaterialCode.setValue('');
            field.getEl().down('input.x-form-text').setStyle('background', "white");
            break;
        case 1:
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
            break;
    }
}

var ShowRptPmtLotInfo = function () {

    var barcode = App.txtShowBarcode.getValue();
    if ((!barcode) || (barcode.length == 0)) {
        Ext.Msg.alert('提示', "没有需要显示的车条码");
        return;
    }
    parent.addTab("id=RptPmtLotInfo", "车报表详细信息[" + barcode + "]", "ReportCenter/RptPmtLotInfo/RptPmtLotInfo.aspx?BarCode=" + barcode, true);

}



var showDetailWindow = function (recipe) {
    var url = "Technology/ProductReview/Detail.aspx?Recipe=" + recipe;
    var tabid = "Manager_Technology_Manage_MaterialRecipeDetail_Default";
    var tabp = parent.App.mainTabPanel;
    var tab = tabp.getComponent("id=" + tabid);
    if (tab) {
        tab.close();
    }
    parent.addTab(tabid, "每车明细信息（" + recipe + ")", url, true);
}

var showDetailWindow2 = function (Barcode) {
    var url = "Technology/ProductReview/ForwardSearch.aspx?Barcode=" + Barcode;
    var tabid = "Manager_Technology_ProductReview_ForwardSearch_DefaultByCode";
    var tabp = parent.App.mainTabPanel;
    var tab = tabp.getComponent("id=" + tabid);
    if (tab) {
        tab.close();
    }
    parent.addTab(tabid, "正向追溯（" + Barcode + ")", url, true);
}

var gridPanelCellDblClick = function (grid, td, tdindex, record, tr, trindex, e, fn) {
    showDetailWindow(record.data.Barcode);
}
var forwordSearch = function () {
    var barcode = App.gridPanelCenter.getSelectionModel().hasSelection() ? App.gridPanelCenter.getSelectionModel().getSelection()[0].data.ShelfBarcode : "";
    if ((!barcode) || (barcode.length == 0)) {
        Ext.Msg.alert('提示', "请选择一个车条码！");
        return;
    }
    showDetailWindow2(barcode);
}