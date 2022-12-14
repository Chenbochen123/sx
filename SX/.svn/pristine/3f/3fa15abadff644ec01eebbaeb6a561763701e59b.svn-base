
var onGridPanelRefresh = function () {
    var form = App.DetailPanel.getForm();
    var r = Ext.create("gridPanelCenterStoreModel", {
    })
    form.loadRecord(r)
}

var SetRowClass = function (record, rowIndex, rowParams, store) {
    if (record.get("AuditFlagName") == "未审核") {
        return "x-grid-row-deleted";
    }
}



Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryRubberInfo_Window",
    hidden: true,
    width: 360,
    height: 470,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryRubberInfo.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择胶料",
    modal: true
})

var Manager_BasicInfo_CommonPage_QueryRubberInfo_Request = function (record) {
    App.txtRubberName.getTrigger(0).show();
    App.hiddenRubberCode.setValue(record.data.RubCode);
    App.txtRubberName.setValue(record.data.RubName);
}
var QueryRubberInfo = function (field, trigger, index) {
    switch (index) {
        case 0:
            field.getTrigger(0).hide();
            field.setValue('');
            App.hiddenRubberCode.setValue('');
            field.getEl().down('input.x-form-text').setStyle('background', "white");
            break;
        case 1:
            App.Manager_BasicInfo_CommonPage_QueryRubberInfo_Window.show();
            break;
    }
}



Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
    hidden: true,
    width: 370,
    height: 470,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择物料",
    modal: true
})

var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {
    App.txtMaterialName.getTrigger(0).show();
    App.hiddenMaterialCode.setValue(record.data.MaterialCode);
    App.txtMaterialName.setValue(record.data.MaterialName);
}
var QueryMaterialInfo = function (field, trigger, index) {
    switch (index) {
        case 0:
            field.getTrigger(0).hide();
            field.setValue('');
            App.hiddenMaterialCode.setValue('');
            field.getEl().down('input.x-form-text').setStyle('background', "white");
            break;
        case 1:
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
            break;
    }
}




Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
    hidden: true,
    width: 370,
    height: 470,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择机台",
    modal: true
})

var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {
    App.txtRecipeEquipName.getTrigger(0).show();
    App.hiddenRecipeEquipCode.setValue(record.data.EquipCode);
    App.txtRecipeEquipName.setValue(record.data.EquipName);
}
var QueryEquipInfo = function (field, trigger, index) {
    switch (index) {
        case 0:
            field.getTrigger(0).hide();
            field.setValue('');
            App.hiddenRecipeEquipCode.setValue('');
            field.getEl().down('input.x-form-text').setStyle('background', "white");
            break;
        case 1:
            App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
            break;
    }
}


var doSave = function (btn) {
    if (btn != "yes") {
        return;
    }
    var after = function () {
        App.waitProgressWindow.close();
    }
    var before = function () {
        App.waitProgressWindow.show();
    }
    before();
    try {
        var record = App.DetailPanel.getRecord();
        var recipe = record.data.ObjID;
        var state = App.txtRecipeStateEdit.getValue();
        App.direct.SaveRecipeInfo(recipe, state, {
            success: function (result) {
                after();
                if (result == "") {
                    Ext.Msg.alert('成功', "工艺配方修改成功！", function (btn) { gridPanelRefresh() });
                } else {
                    Ext.Msg.alert('失败', result);
                }
            },
            failure: function (errorMsg) {
                after();
                Ext.Msg.alert('错误', errorMsg);
            }
        });
    }
    catch (ex) {
        after();
    }
}
var Save = function () {
    var record = App.DetailPanel.getRecord();
    if ((record) && (record.data.ObjID > 0)) {
        if (record.data.RecipeState == App.txtRecipeStateEdit.getValue()) {
            Ext.Msg.alert('提示', "没有修改工艺配方状态，不进行保存");
            return;
        }
        Ext.Msg.confirm("提示", '您确定需要修改此工艺配方信息？', function (btn) { doSave(btn); });
    }
}
