Ext.create("Ext.window.Window", {//胶料查询带回窗体
    id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
    height: 460,
    hidden: true,
    width: 360,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx?MajorTypeID=5' width=100% height=100% scrolling=no  frameborder=0></iframe>",
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

var ButtonNorthPrintView_BeforeClick = function () {
    var StoreCenterMain = App.StoreCenterMain;
    if (StoreCenterMain.getCount() == 0) {
        Ext.Msg.alert('提示', '请先查询出结果');
        return false;
    }
    var RowSelectionModelCenterMain = App.RowSelectionModelCenterMain;
    if (RowSelectionModelCenterMain.getCount() == 0) {
        Ext.Msg.alert('提示', '请选择要预览的记录');
        return false;
    }
    var printFlag = RowSelectionModelCenterMain.getSelection()[0].data["是否可打印"];
    if (printFlag == null || printFlag == '0') {
        Ext.Msg.alert('提示', '该记录不可以打印');
        return false;
    }
    var barcode = RowSelectionModelCenterMain.getSelection()[0].data["架子条码"];
    if (barcode == null || barcode == '') {
        Ext.Msg.alert('提示', '该记录没有架子条码');
        return false;
    }
    return true;
};

var CheckResult_Renderer = function (value, metadata, record) {
    if (value == "不合格") {
        return "<span style='background-color: red;font-weight:bold'>" + value + "</span>";
    }
    return value;
};

var ItemCheck_Renderer = function (value, flag) {
    if (flag == "1") {
        return "<span style='background-color: yellow;font-weight:bold'>" + value + "↑</span>";
    }
    else if (flag == "-1") {
        return "<span style='background-color: pink;font-weight:bold'>" + value + "↓</span>";
    }
    else if (flag == "") {
        return "<span style='background-color: whitesmoke;font-weight:bold'>" + value + "</span>";
    }
    return value;
};