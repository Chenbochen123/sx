var ButtonNorthExport_Click = function () {
    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: "Loading..." });
    myMask.show();

    App.direct.GetRubberQualityReport({
        success: function (result) {

        },
        failure: function (errorMessage) {
            Ext.Msg.alert('提示', errorMessage);
        },
        eventMask: {
            showMask: false
        },
        isUpload: true
    });

    var myTask = new Ext.util.DelayedTask(function () { myMask.hide(); });
    myTask.delay(5000);

};

var GridPanelCenter_ItemDbClick = function (item, record, node, index, e) {
    var rubTypeCode = record.get("RubTypeCode");
    var checkPlanDate = record.get("CheckPlanDate");
    var shiftCheckId = record.get("ShiftCheckId");
    var workShopCode = App.HiddenWorkShopCode.getValue();

    App.HiddenRubTypeCode.setValue(rubTypeCode);
    App.HiddenCheckPlanDate.setValue(checkPlanDate);
    App.HiddenShiftCheckId.setValue(shiftCheckId);

    App.TextFieldViewNorthCheckPlanDate.setValue(checkPlanDate);
    App.TextFieldViewNorthShiftCheckId.setValue(shiftCheckId);

    App.direct.CheckBoxViewJudgeResult_Change({
        success: function (result) {
            
        },
        failure: function (errorMessage) {
            Ext.Msg.alert('提示', errorMessage);
        },
        eventMask: {
            showMask: true
        }
    });

};

var pctChange = function (value) {
    if (value == null || value == "" || value == "0") {
        return '';
    }
    else {
        return '<span style="color: red;font-weight:bold">' + value + '</span>';
    }
    return value;
};

var change = function (value) {
    if (value == "不合格") {
        return '<span style="background-color: red;font-weight:bold">' + value + '</span>';
    }
    return value;
};

