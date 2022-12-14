Ext.create("Ext.window.Window", {//供应商、生产商带回窗体
    id: "Manager_BasicInfo_CommonPage_QueryFactory_Window",
    height: 460,
    hidden: true,
    width: 760,
    html: "<iframe src='../BasicInfo/CommonPage/QueryFactory.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择",
    modal: false
});

var Manager_BasicInfo_CommonPage_QueryFactory_Request = function (record) {//供应商、生产商信息返回值处理
    var facType = App.HiddenNorthFacType.getValue();
    if (facType == '2') { // 供应商
        App.TriggerFieldNorthSupplierName.setValue(record.data.FacName);
        App.HiddenNorthSupplierId.setValue(record.data.ObjID.toString());
    }
    else if (facType == '4') { // 生产商
        App.TriggerFieldNorthManufacturerName.setValue(record.data.FacName);
        App.HiddenNorthManufacturerId.setValue(record.data.ObjID.toString());
    }
};

var TriggerFieldNorthSupplierName_Click = function (item, trigger, index) {
    if (index == 0) {
        this.setValue('');
        App.HiddenNorthSupplierId.setValue('');
    }
    else if (index == 1) {
        App.HiddenNorthFacType.setValue('2');
        App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
    }
};

var TriggerFieldNorthManufacturerName_Click = function (item, trigger, index) {
    if (index == 0) {
        this.setValue('');
        App.HiddenNorthManufacturerId.setValue('');
    }
    else if (index == 1) {
        App.HiddenNorthFacType.setValue('4');
        App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
    }
};

var response = function (command, record) {
    parent.Manager_RawMaterialQuality_QueryQmcSampleLedger_Request(record);
    parent.App.Manager_RawMaterialQuality_QueryQmcSampleLedger_Window.close();
    return false;
}
var CommandColumnConfirm_Click = function (command, record) {
    return response(command, record);
};
var GridPanelCenter_CellDblClick = function (grid, td, tdindex, record) {
    return response('dblclick', record);
}
