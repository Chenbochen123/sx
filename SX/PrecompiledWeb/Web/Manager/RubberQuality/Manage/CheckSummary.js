
Ext.create("Ext.window.Window", {//胶料查询带回窗体
    id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
    height: 460,
    hidden: true,
    width: 360,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx?MajorTypeID=4,5' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择胶料",
    modal: true
});

var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//胶料返回值处理
    App.TriggerFieldMaterName.setValue(record.data.MaterialName);
    App.HiddenMaterCode.setValue(record.data.MaterialCode);
};

Ext.create("Ext.window.Window", {//机台信息带回查询信息
    id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
    height: 480,
    hidden: true,
    width: 360,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择机台",
    modal: true
});

var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {  //机台返回值处理
    App.TriggerFieldEquipName.setValue(record.data.EquipName);
    App.HiddenEquipCode.setValue(record.data.EquipCode);
};

var TriggerFieldMaterName_TriggerClick = function (item, trigger, index, tag, e) {
    if (index == 0) {
        //清空
        App.TriggerFieldMaterName.setValue("");
        App.HiddenMaterCode.setValue("");
    }
    else if (index == 1) {
        // 查询
        App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
    }
};

var StoreMain_Refresh = function (store) {
    App.StatusBarMain.setText("合计：" + store.getTotalCount().toString());
};


var SetRowClass = function (record, rowIndex, rowParams, store) {
    if (record.get("结果") == "不合格") {
        //return 'x-grid-row-deleted';
    }
}


var pctChange = function (value) {
    if (value == "不合格") {
        return '<span style="background-color: red;font-weight:bold">' + value + '</span>';
    }
    return value;
};

var change = function (value) {
    if (value != null && value.indexOf("↑") > 0) {
        return '<span style="background-color: yellow;font-weight:bold">' + value + '</span>';
    }
    else if (value != null && value.indexOf("↓") > 0) {
        return '<span style="background-color: pink;font-weight:bold">' + value + '</span>';
    }
    return value;
};

var ButtonExcel_BeforeClick = function (el, type, action, extraParams, o) {
    var totalCount = App.StoreMain.getTotalCount();
    if (totalCount > 0) {
        var delayCount = 3000;
        if (totalCount > 40000) {
            delayCount = 18000;
        }
        else if (totalCount > 30000) {
            delayCount = 15000;
        }
        else if (totalCount > 20000) {
            delayCount = 12000;
        }
        else if (totalCount > 10000) {
            delayCount = 10000;
        }
        var myMask = new Ext.LoadMask(Ext.getBody(), { msg: "Loading..." });
        myMask.show();
        var myTask = new Ext.util.DelayedTask(function () { myMask.hide(); });

        myTask.delay(delayCount);
    }
    return true;
};