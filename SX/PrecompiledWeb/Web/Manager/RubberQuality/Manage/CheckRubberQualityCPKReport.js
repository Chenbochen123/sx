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
    if (App.HiddenNorthMaterFlag.getValue() == '1') {
        App.TriggerFieldNorthMaterName.setValue(record.data.MaterialName);
        App.HiddenNorthMaterCode.setValue(record.data.MaterialCode);
    }
    else if (App.HiddenNorthMaterFlag.getValue() == '2') {
        App.TriggerFieldNorthOtherMaterName.setValue(record.data.MaterialName);
        App.HiddenNorthOtherMaterCode.setValue(record.data.MaterialCode);
    }
};

var TriggerFieldNorthMaterName_TriggerClick = function (item, trigger, index, tag, e) {
    if (index == 0) {
        App.TriggerFieldNorthMaterName.setValue('');
        App.HiddenNorthMaterCode.setValue('');
    }
    else if (index == 1) {
        App.HiddenNorthMaterFlag.setValue('1');
        App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
    } 
};

var TriggerFieldNorthOtherMaterName_TriggerClick = function (item, trigger, index, tag, e) {
    if (index == 0) {
        App.TriggerFieldNorthOtherMaterName.setValue('');
        App.HiddenNorthOtherMaterCode.setValue('');
    }
    else if (index == 1) {
        App.HiddenNorthMaterFlag.setValue('2');
        App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
    }
};

var ButtonNorthExport_Click = function () {

    var myMask = new Ext.LoadMask(Ext.getBody(), { msg: "Loading..." });
    myMask.show();
    var dateType = App.RadioGroupNorthDateType.getChecked()[0].inputValue;
    var month = App.DateFieldNorthMonth.rawValue;
    var beginDate = App.DateFieldNorthBeginDate.rawValue;
    var endDate = App.DateFieldNorthEndDate.rawValue;
    var zjsID = App.ComboBoxNorthZJSID.getValue();
    zjsID = zjsID == null ? "" : zjsID;
    var equipCode = App.ComboBoxNorthEquipCode.getValue();
    equipCode = equipCode == null ? "" : equipCode;

    var otherMaterCodes = "";
    var otherMaters = App.MultiSelectNorthMater.getValues();
    for (var index = 0; index < otherMaters.length; index++) {
        otherMaterCodes = otherMaterCodes + "," + otherMaters[index].value;
    }

    App.direct.GetRubberQualityCPKReport(otherMaterCodes, {
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
    myTask.delay(20000);

};

var ButtonNorthQuery_Click = function () {
};

var RadioGroupNorthDateType_Change = function (item, newValue, oldValue) {
    switch (item.getChecked()[0].inputValue) {
        case '1':
            App.DateFieldNorthMonth.show();
            App.DateFieldNorthBeginDate.hide();
            App.DateFieldNorthEndDate.hide();
            break;
        case '2':
            App.DateFieldNorthMonth.hide();
            App.DateFieldNorthBeginDate.show();
            App.DateFieldNorthEndDate.show();
            break;
        default:
            break;
    }
};

var ComboBoxNorthZJSID_TriggerClick = function (item, trigger, index, tag, e) {
    item.setValue('');
};

var ComboBoxNorthEquipCode_TriggerClick = function (item, trigger, index, tag, e) {
    item.setValue('');
};

var ButtonNorthAddToList_Click = function () {
    var materCode = App.HiddenNorthOtherMaterCode.getValue();
    if (materCode == null || materCode == "") {
        Ext.Msg.alert("提示", "请选择相关胶料");
        return;
    }
    if (App.MultiSelectNorthMater.findRecordByValue(materCode) != false) {
        Ext.Msg.alert("提示", "相关胶料已存在");
        return;
    }

    var materName = App.TriggerFieldNorthOtherMaterName.getValue();


    var ms = App.MultiSelectNorthMater;
    var rec = {};

    rec[ms.displayField] = materName;
    rec[ms.valueField] = materCode;
    ms.getStore().add(rec);
    App.HiddenNorthOtherMaterCode.setValue("");
    App.TriggerFieldNorthOtherMaterName.setValue("");
};

var ButtonNorthDeleteFromList_Click = function () {
    var ms = App.MultiSelectNorthMater;
    if (ms.getSelected().length == 0) {
        Ext.Msg.alert("提示", "请选择要删除的相关胶料");
        return;
    }

    ms.getStore().remove(ms.getSelected());
};

var ButtonNorthClearList_Click = function () {
    var ms = App.MultiSelectNorthMater;
    ms.getStore().removeAll();
};