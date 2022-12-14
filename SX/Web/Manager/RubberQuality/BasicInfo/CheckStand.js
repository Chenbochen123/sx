/// <reference path="../../../resources/vswd-ext_2.2.js" />

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
    if (!App.WindowMaster.hidden) {
        App.TriggerFieldMasterMaterName.setValue(record.data.MaterialName);
        App.HiddenMasterMaterCode.setValue(record.data.MaterialCode);
    }
    else if (!App.WindowMasterCopy.hidden) {
        App.TriggerFieldMasterCopyToMaterName.setValue(record.data.MaterialName);
        App.HiddenMasterCopyToMaterCode.setValue(record.data.MaterialCode);
    }
    else {
        App.TriggerFieldNorthMaterName.setValue(record.data.MaterialName);
        App.HiddenNorthMaterCode.setValue(record.data.MaterialCode)
    }
};

// 加载子节点
var TreePanelMater_BeforeLoad = function (item, operation, options) {
    var node = operation.node;
    App.TreePanelMater.mask();

    App.direct.NodeLoad(node.getId(), node.get('MajorTypeID'), node.get('MinorTypeID'), node.get('MaterCode'), {
        success: function (result) {
            var data = Ext.decode(result);
            if (data.length > 0) {
                node.appendChild(data, false, true);
            }
            node.set('loading', false);
            node.set('loaded', true);

            node.expand();

            App.TreePanelMater.unmask();
        },

        failure: function (errorMsg) {
            App.TreePanelMater.unmask();
            Ext.Msg.alert('Failure', errorMsg);
        }
    });

    return false;
};

// this "setGroupStyle" function is called when the GroupingView is refreshed.
var setGroupStyle = function (view) {
    // get an instance of the Groups
    var groups = view.el.query(App.GroupingMaster.eventSelector);

    for (var i = 0; i < groups.length; i++) {
        //                var groupId = Ext.fly(groups[i]).next().id.substr((view.id + '-gp-').length),
        //                    records = view.panel.store.getGroups(groupId).children,
        //                    color = "#" + records[0].data.ColorCode;
        //                color = "Gray";
        // Set the "background-color" of the original Group node.
        Ext.get(groups[i]).select('.x-grid-cell-inner').setStyle("background-color", "WhiteSmoke");
    }
    var firsts = view.el.query('.x-grid-cell-first');
    for (var i = 0; i < firsts.length; i++) {
        var first = Ext.get(firsts[i]);
        first.setStyle("background-color", "WhiteSmoke");
    }

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
    App.TriggerFieldEquipCheckEquipName.setValue(record.data.EquipName);
    App.HiddenEquipCheckEquipCode.setValue(record.data.EquipCode);
};
var SetRowClass = function (record, rowIndex, rowParams, store) {
    if ( record.get("DeleteFlag") == "1") {//确认为已删除
        return "x-grid-row-important";
    }
}
var ButtonMasterDelete_BeforeClick = function () {
    if (App.RowSelectionModelMaster.getCount() == 0) {
        Ext.Msg.alert("提示", "请选择要删除的质检标准");
        return false;
    }
    return true;
};

var ButtonDetailDelete_BeforeClick = function () {
    if (App.RowSelectionModelDetail.getCount() == 0) {
        Ext.Msg.alert("提示", "请选择要删除的质检明细标准");
        return false;
    }
    return true;
};