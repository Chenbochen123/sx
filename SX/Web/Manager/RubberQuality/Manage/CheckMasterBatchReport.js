Ext.create("Ext.window.Window", {//胶料查询带回窗体
    id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
    height: 460,
    hidden: true,
    width: 360,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择胶料",
    modal: true
});

var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//胶料返回值处理
    var ComboBoxNorthMaterCode = App.ComboBoxNorthMaterCode;
    var materCode = record.data.MaterialCode;
    var materName = record.data.MaterialName;
    if (ComboBoxNorthMaterCode.findRecordByValue(materCode) == false) {
        ComboBoxNorthMaterCode.insertItem(0, materName, materCode);
    }
    ComboBoxNorthMaterCode.setValue(materCode);
};

var StoreCenterMain_Refresh = function (store) {
    App.StatusBarCenterMain.setText("合计：" + store.getTotalCount().toString());
};
