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
//////////////////////////////////20130417配方导入
Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_RecipeInfolead_Window",
    hidden: true,
    width: 600,
    height: 470,
    html: "<iframe src='../../Technology/Manage/RecipeInfolead.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择胶料",
    modal: true
})

var RecipeInfolead = function () {
    ClearRubber();

    App.Manager_BasicInfo_CommonPage_RecipeInfolead_Window.show();
        

}

///////////////////////////////////////////20130417配方导入

var Manager_BasicInfo_CommonPage_QueryRubberInfo_Request = function (record) {
    App.txtRubberName.getTrigger(0).show();
    App.hiddenRubberCode.setValue(record.data.RubCode);
    App.txtRubberName.setValue(record.data.RubName);
    App.treePanelUser.store.reload();
}

var QueryRubberTree = function (sender, selected, fn) {
    App.treePanelUser.store.reload();
}
var ClearRubber = function () {
    App.gridPanelCenter.setTitle("机台配方基本信息");
    App.hiddenMaterialID.setValue('');
    App.hiddenRubberCode.setValue('');
    var form = App.DetailPanel.getForm();
    var r = Ext.create("gridPanelCenterStoreModel", {
    })
    form.loadRecord(r);
    App.gridPanelCenter.store.removeAll();
}
var QueryRubberInfo = function (field, trigger, index) {
    ClearRubber();
    switch (index) {
        case 0:
            field.getTrigger(0).hide();
            field.setValue('');
            App.hiddenRubberCode.setValue('');
            App.treePanelUser.store.reload();
            field.getEl().down('input.x-form-text').setStyle('background', "white");
            break;
        case 1:
            App.Manager_BasicInfo_CommonPage_QueryRubberInfo_Window.show();
            break;
    }
}

var showDetailWindow = function (recipe, command, material) {
    App.direct.IsHaveWorkShopPower(recipe, {
        success: function (result) {
            if (result == "SUCCESS") {
                var url = "Technology/Manage/MaterialRecipeDetail/Default.aspx?Recipe=" + recipe + "&Command=" + command + "&Material=" + material;
                var tabid = "Manager_Technology_Manage_MaterialRecipeDetail_Default";
                var tabp = parent.App.mainTabPanel;
                var tab = tabp.getComponent("id=" + tabid);
                if (tab) {
                    tab.close();
                }
                parent.addTab(tabid, "配方明细信息", url, true);
            } else {
                Ext.Msg.alert('提示', result);
                return;
            }
        },
        failure: function (errorMsg) {
            Ext.Msg.alert('错误', errorMsg);
        }
    });
    
}

var gridPanelCellDblClick = function (grid, td, tdindex, record, tr, trindex, e, fn) {
    showDetailWindow(record.data.ObjID, "Show", record.data.RecipeMaterialCode);
}


var btnAddClick = function (sender, e, fn) {
    var recipe = "";
    var command = "";
    var material = "";
    material = App.hiddenRubberCode.getValue();
    if (App.hiddenMaterialID.getValue().length > 0) {
        material = App.hiddenMaterialID.getValue();
    }
    if (material.length == 0) {
        Ext.Msg.alert('提示', "请选择胶料或物料");
        return;
    }
    if (sender.id.toLowerCase().indexOf("upload") > 0) {
        command = "AddUpload";
        var Level1 = false;
        try {
            var treeRoot = App.treePanelUser.store.tree.root.childNodes[0];
            for (var i = 0; i < treeRoot.childNodes.length; i++) {
                var treeNodeID = treeRoot.childNodes[i].data.id;
                if (endWith(treeNodeID, "=" + material)) {
                    Level1 = true;
                    break;
                }
            }
        }
        catch (ex) { }
        if (!Level1) {
            Ext.Msg.alert('提示', "只有一级物料可以进行向上新增！");
            return;
        }
    }
    if (sender.id.toLowerCase().indexOf("download") > 0) {
        command = "AddDownload";
    }
    showDetailWindow(recipe, command, material);
}

var refreshLoaction = function () {
    gridPanelRefresh();
    App.treePanelUser.store.reload();
}

var onGridPanelRefresh = function () {
    var form = App.DetailPanel.getForm();
    var r = Ext.create("gridPanelCenterStoreModel", {
    })
    form.loadRecord(r)
}

var onTabClose = function () {
    var tabid = "Manager_Technology_Manage_MaterialRecipeDetail_Default";
    var tabp = parent.App.mainTabPanel;
    var tab = tabp.getComponent("id=" + tabid);
    if (tab) {
        var window = tab.body.dom.firstChild.contentWindow;
        if (window.isCanClose) {
            if (!window.isCanClose()) {
                return "正在编辑配方明细信息不能关闭当前页面";
            }
        }
    }
    return "";
}

var SetRowClass = function (record, rowIndex, rowParams, store) {
    if (record.get("AuditFlagName") == "未审核") {
        return "x-grid-row-deleted";
    }
}

var dobtnDeletePmtRecipeClick = function (btn) {
    if (btn != "yes") {
        return;
    }
    var after = function () {
        App.waitProgressWindow.close();
    }
    var before = function () {
        App.waitProgressWindow.show();
    }
    var recipe = App.txtRecipeObjID.getValue();
    try {
        if (recipe.length == 0) {
            Ext.Msg.alert('提示', "请选择需要删除的工艺配方");
            return;
        }
        before();
        App.direct.DeletePmtRecipe(recipe, {
            success: function (result) {
                after();
                if (result == "") {
                    Ext.Msg.alert('成功', "配方删除成功！", function (btn) { refreshLoaction() });
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


var btnDeletePmtRecipeClick = function (sender, e, fn) {
    var tabid = "Manager_Technology_Manage_MaterialRecipeDetail_Default";
    var tabp = parent.App.mainTabPanel;
    var tab = tabp.getComponent("id=" + tabid);
    if (tab) {
        try {
            var recipe = App.txtRecipeObjID.getValue();
            if (recipe.length == 0) {
                Ext.Msg.alert('提示', "请选择需要删除的工艺配方");
                return;
            }
            var window = tab.body.dom.firstChild.contentWindow;
            if ((recipe == window.App.hiddenRecipeObjID.getValue()) && (!window.App.btnSave.disabled)) {
                Ext.Msg.alert('提示', "当前工艺配方正在编辑，不能进行删除");
                return;
            }
        }
        catch (ex) {
        }
    }
    Ext.Msg.confirm("提示", '您确定删除此工艺配方信息？', function (btn) { dobtnDeletePmtRecipeClick(btn); });
}

var ShowAllPmtRefresh = function (sender, e, fn) {
    if (sender.id.toLowerCase().indexOf("all") > 0) {
        App.isShowAllPmt.setValue("0")
    }
    else {
        App.isShowAllPmt.setValue("1")
    }
    gridPanelRefresh();
}



Ext.create("Ext.window.Window", {
    id: "Manager_Technology_Manage_RecipeCopyToEquip_Window",
    hidden: true,
    width: 600,
    height: 550,
    html: "<iframe src='RecipeCopyToEquip.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择机台进行工艺配方另存",
    modal: true
})

var RecipeCopyToEquip = function (btn) {
    if (btn != "yes") {
        return;
    }
    var recipe = App.txtRecipeObjID.getValue();
    var html = "<iframe src='RecipeCopyToEquip.aspx?recipe=" + recipe + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
    if (App.Manager_Technology_Manage_RecipeCopyToEquip_Window.getBody()) {
        App.Manager_Technology_Manage_RecipeCopyToEquip_Window.getBody().update(html);
    } else {
        App.Manager_Technology_Manage_RecipeCopyToEquip_Window.html = html;
    }
    App.Manager_Technology_Manage_RecipeCopyToEquip_Window.show();
}

var btnCopyPmtRecipeClick = function (sender, e, fn) {
    var recipe = App.txtRecipeObjID.getValue();
    if (recipe.length == 0) {
        Ext.Msg.alert('提示', "请选择复制的工艺配方");
        return;
    }
    Ext.Msg.confirm("提示", '您确定需要拷贝选中的工艺配方信息？', function (btn) { RecipeCopyToEquip(btn); });
}