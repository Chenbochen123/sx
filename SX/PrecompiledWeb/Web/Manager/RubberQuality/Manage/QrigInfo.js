/// <reference path="../../../resources/vswd-ext_2.2.js" />

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
    var type = App.HiddenEquipType.getValue();
    if (type == "QueryEquip") {
        App.TriggerFieldQueryEquipName.setValue(record.data.EquipName);
        App.HiddenQueryEquipCode.setValue(record.data.EquipCode);
    }
    else if (type == "QueryCheck") {
        App.TriggerFieldQueryCheckEquipName.setValue(record.data.EquipName);
        App.HiddenQueryCheckEquipCode.setValue(record.data.EquipCode);
    }
    else if (type == "EditEquip") {
        App.TriggerFieldEditEquipName.setValue(record.data.EquipName);
        App.HiddenEditEquipCode.setValue(record.data.EquipCode);
    }
    else if (type == "EditCheckEquip") {
        App.TriggerFieldEditCheckEquipName.setValue(record.data.EquipName);
        App.HiddenEditCheckEquipCode.setValue(record.data.EquipCode);
    }
};

Ext.create("Ext.window.Window", {//人员信息带回查询信息
    id: "Manager_BasicInfo_CommonPage_QueryBasUser_Window",
    height: 480,
    hidden: true,
    width: 360,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryBasUser.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择人员",
    modal: true
});

var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {  //人员返回值处理
    var type = App.HiddenUserType.getValue();
    if (type == "QueryCheck") {
        App.TriggerFieldQueryCheckUserName.setValue(record.data.UserName);
        App.HiddenQueryCheckUserCode.setValue(record.data.WorkBarcode);
    }
    else if (type == "EditCheck") {
        App.TriggerFieldEditCheckUserName.setValue(record.data.UserName);
        App.HiddenEditCheckUserCode.setValue(record.data.WorkBarcode);
    }
};

Ext.create("Ext.window.Window", {//物料信息带回查询信息
    id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
    height: 480,
    hidden: true,
    width: 360,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx?MajorTypeID=4,5' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择物料",
    modal: true
});

var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {  //物料返回值处理
    var type = App.HiddenMaterType.getValue();
    if (type == "QueryMater") {
        App.TriggerFieldQueryMaterName.setValue(record.data.MaterialName);
        App.HiddenQueryMaterCode.setValue(record.data.MaterialCode);
    }
    else if (type == "EditMater") {
        App.TriggerFieldEditMaterName.setValue(record.data.MaterialName);
        App.HiddenEditMaterCode.setValue(record.data.MaterialCode);
    }
};

var GridViewDetail_GetRowClass = function (record, index, rowParams, store) {
    var standId = record.get("StandId");
    var itemCheck = record.get("ItemCheck");
    var permMax = record.get("PermMax");
    var permMin = record.get("PermMin");
    if (standId == null || standId == 0 || permMax == null) {
        return "GridViewRowClass_NoStand";
    }
    else if (itemCheck != null && permMax != null && itemCheck > permMax) {
        return "GridViewRowClass_MoreThanMax";
    }
    else if (itemCheck != null && permMin != null && itemCheck < permMin) {
        return "GridViewRowClass_LessThanMin";
    }

};

var ButtonDelete_BeforeClick = function () {
    if (App.RowSelectionModelMaster.getCount() == 0) {
        Ext.Msg.alert("提示", "请选择要删除的质检记录");
        return false;
    }
    return true;
};

var ButtonDetailDelete_BeforeClick = function () {
    if (App.RowSelectionModelDetail.getCount() == 0) {
        Ext.Msg.alert("提示", "请选择要删除的质检明细记录");
        return false;
    }

    return true;
};

var ButtonExcel_BeforeClick = function (el, type, action, extraParams, o) {
    var totalCount = App.StoreMaster.getTotalCount();
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