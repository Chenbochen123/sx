/// <reference path="../../../resources/vswd-ext_2.2.js" />
var ButtonNorthQuery_BeforeClick = function (item, e) {

    App.StoreMain.loadPage(1);
    //return false;
    if (ValidateFormPanelNorth() == false) {
        return false;
    }
    
    return true;
};

var ButtonNorthQuery_Click = function (item, e) {
    if (ValidateFormPanelNorth() == false) {
        return false;
    }
    App.direct.SearchQrigMaster({
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

var ValidateFormPanelNorth = function () {
    if (Ext.isEmpty(App.DateFieldCheckSDate.getValue()) == true) {
        Ext.Msg.alert("提示", "请选择检验起始日期");
        App.DateFieldCheckSDate.focus();
        return false;
    }
    if (App.DateFieldCheckSDate.validate() == false) {
        Ext.Msg.alert("提示", "检验起始日期格式不正确");
        App.DateFieldCheckSDate.focus();
        return false;
    }
    if (Ext.isEmpty(App.TimeFieldCheckSTime.getValue()) == true) {
        Ext.Msg.alert("提示", "请选择检验起始时间");
        App.TimeFieldCheckSTime.focus();
        return false;
    }
    if (App.TimeFieldCheckSTime.validate() == false) {
        Ext.Msg.alert("提示", "检验起始时间格式不正确");
        App.TimeFieldCheckSTime.focus();
        return false;
    }
    if (Ext.isEmpty(App.DateFieldCheckEDate.getValue()) == true) {
        Ext.Msg.alert("提示", "请选择检验截止日期");
        App.DateFieldCheckEDate.focus();
        return false;
    }
    if (App.DateFieldCheckEDate.validate() == false) {
        Ext.Msg.alert("提示", "检验截止日期格式不正确");
        App.DateFieldCheckEDate.focus();
        return false;
    }
    if (Ext.isEmpty(App.TimeFieldCheckETime.getValue()) == true) {
        Ext.Msg.alert("提示", "请选择检验截止时间");
        App.TimeFieldCheckETime.focus();
        return false;
    }
    if (App.TimeFieldCheckETime.validate() == false) {
        Ext.Msg.alert("提示", "检验截止时间格式不正确");
        App.TimeFieldCheckETime.focus();
        return false;
    }
    return true;
};

var GroupingMain_Sum = function (values) {
    var sumValue = 0;
    for (var i = 0; i < values.rows.length; i++) {
        if (values.rows[i].data["车数"] != null && values.rows[i].data["车数"].toString() != "") {
            sumValue += parseInt(values.rows[i].data["车数"].toString());
        }
    }

    return sumValue.toString();
};

var StoreMain_GroupChange = function (store, groupers) {
    for (var i = 0; i < App.GridPanelMain.columns.length; i++) {
        if (groupers.items.length > 0 && App.GridPanelMain.columns[i].dataIndex == groupers.items[0].property) {
            App.GridPanelMain.columns[i].hide();
        }
        else {
            App.GridPanelMain.columns[i].show();
        }
    }
};

var SummaryColumnMainSerialNum_SummaryRenderer = function (value, summaryData, field) {
    return "小计：" + value;
};

var StoreMain_Refresh = function (store) {
    App.StatusBarMain.setText("合计车数：" + store.sum("车数").toString());
};

// this "setGroupStyle" function is called when the GroupingView is refreshed.
var setGroupStyle = function (view) {
    // get an instance of the Groups
    App.GroupingMain.collapseAll();
    var groups = view.el.query(App.GroupingMain.eventSelector);

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
