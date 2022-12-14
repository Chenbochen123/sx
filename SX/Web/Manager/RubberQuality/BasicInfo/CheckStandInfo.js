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
    App.TriggerFieldNorthMaterName.setValue(record.data.MaterialName);
    App.HiddenNorthMaterCode.setValue(record.data.MaterialCode);
};

var TriggerFieldNorthMaterName_TriggerClick = function (item, trigger, index, tag, e) {
    if (index == 0) {
        // 清空
        item.setValue("");
    }
    else if (index == 1) {
        //查找
        App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
    }
};

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
