//--编辑开炼信息明细Start
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
    App.btnSave.setDisabled(true);
    try {
        var store = getStore();
        App.direct.SaveJsonInfo(storeToJson(store.mix0), storeToJson(store.mix1), storeToJson(store.mix2), storeToJson(store.mix3),
            storeToJson(store.mix4), storeToJson(store.mix5), storeToJson(store.mix6), {
            success: function (result) {
                after();
                App.btnSave.setDisabled(true);
                App.btnCanSave.setText("编辑");
                if (result == "") {
                    Ext.Msg.alert('成功', "开炼信息明细保存成功！", function (btn) { refreshOpenModelDetail(); });
                } else {
                    Ext.Msg.alert('失败', result);
                }
            },
            failure: function (errorMsg) {
                App.btnSave.setDisabled(false);
                after();
                Ext.Msg.alert('错误', errorMsg);
            }
        });
    }
    catch (ex) {
        App.btnSave.setDisabled(false);
        after();
        alert(ex);
    }
}
var Save = function () {
    Ext.Msg.confirm("提示", '您确定需要保存此模板的开炼信息明细?', function (btn) { doSave(btn); });
}


var isCanClose = function () {
    return App.btnSave.disabled
}

var onTabClose = function () {
    if (!isCanClose()) {
        return "正在编辑配方明细信息不能关闭当前页面";
    }
    return "";
}

//取消开炼信息明细编辑
var CanCancelSave = function (btn) {
    if (btn != "yes") {
        return;
    }
    App.btnSave.setDisabled(!App.btnSave.disabled);
    App.btnCanSave.setText("编辑");
    refreshOpenModelDetail();
}

//刷新开炼动作模板明细列表
var refreshOpenModelDetail = function () {
    App.storeMinxing0.reload();
    App.storeMinxing1.reload();
    App.storeMinxing2.reload();
    App.storeMinxing3.reload();
    App.storeMinxing4.reload();
    App.storeMinxing5.reload();
    App.storeMinxing6.reload();
}

//点击开炼信息明细编辑按钮触发事件
var SetCanSave = function () {
    if (App.hidden_main_id.value == "" || App.hidden_main_id.value == null)
    {
        Ext.Msg.alert('提示', '请先选择模板信息!');
        return;
    }
    if (!App.btnSave.disabled) {
        Ext.Msg.confirm("提示", '您确定取消此开炼模板的详细信息编辑状态？', function (btn) { CanCancelSave(btn); });
    }
    else {
        App.btnSave.setDisabled(false);
        App.btnCanSave.setText("取消");
    }
}

//获取Store值
var getStore = function () {
    var store = getOpenMixingStore();
    return store;
}
//Store值转化为JSON
var storeToJson = function (store) {
    if (!store) {
        return '';
    }
    if (store.type == "store") {
        arr = new Array();
        Ext.each(store.data.items, function (record) {
            arr.push(record.data);
        });
        return Ext.encode(arr);
    }
    else {
        return Ext.encode(store)
    }
}

//创建开炼信息的store存储
var newOpenActionModelStore = function () {
    return {
        mix0: undefined,
        mix1: undefined,
        mix2: undefined,
        mix3: undefined,
        mix4: undefined,
        mix5: undefined,
        mix6: undefined

    }
}

var getOpenMixingStore = function () {
    var store = newOpenActionModelStore();
    try {
        store.mix0 = window.App.gridPanelMinxing0.store;
        store.mix1 = window.App.gridPanelMinxing1.store;
        store.mix2 = window.App.gridPanelMinxing2.store;
        store.mix3 = window.App.gridPanelMinxing3.store;
        store.mix4 = window.App.gridPanelMinxing4.store;
        store.mix5 = window.App.gridPanelMinxing5.store;
        store.mix6 = window.App.gridPanelMinxing6.store;
    }
    catch (e) {
        alert(e);
    }
    return store;
}
//--编辑开炼信息明细End

var enableEdit = function () {
    var can = true;
    try {
        can = !App.btnSave.disabled;
    }
    catch (ex) {
    }
    return can;
}
var refreshRowNum = function (store) {
    for (var i = 0; i < store.data.items.length; i++) {
        store.data.items[i].index = i;
    }
    store.loadRecords(store.data.items, true);
}
//点击插入按钮
var commandcolumn_insert = function (command, store, index) {
    if (command.toLowerCase() != "insert") {
        return;
    }
    var modelName = store.model.getName();
    var r = Ext.create(modelName, {
    })
    store.data.insert(index, r);
    while (store.data.items.length > store.pageSize) {
        store.data.removeAt(store.pageSize);
    }
    refreshRowNum(store);
}

//点击删除按钮
var commandcolumn_delete = function (command, store, index) {
    if (command.toLowerCase() != "delete") {
        return;
    }
    for (i = 0; i < store.data.items.length ; i++) {
        if (store.data.items[i].index == index) {
            store.data.removeAt(i);
            break;
        }
    }
    var modelName = store.model.getName();
    var r = Ext.create(modelName, {
    })
    store.data.add(r);
    refreshRowNum(store);
}
//根据按钮类别进行删除和编辑操作
var commandcolumn_click = function (column, command, record) {
    var index = record.index;
    var store = column.grid.store;
    if (App.btnSave.disabled == true)
    {
        Ext.Msg.alert("提示", "请先点击编辑按钮，进入编辑状态!");
        return;
    }
    commandcolumn_insert(command, store, index);
    commandcolumn_delete(command, store, index);
    return false;
};

var showInfo = function (value, items) {
    if (!value) {
        return value;
    }
    for (var i = 0; i < items.length; i++) {
        if (items[i].data.field1.toString().trim() == value.toString().trim()) {
            return items[i].data.field2;
        }
    }
    return value;
};
var RendererGridColumnComboBox = function (value, metadata, record, rowIndex, colIndex, store, view) {
    if (!view) {
        return value;
    }
    var cs = view.panel.headerCt.getGridColumns();
    var items = cs[colIndex].getEditor().items.items[0].store.data.items;
    return showInfo(value, items);
};


//--开炼动作模板Main增删改查方法Start

//根据按钮类别进行删除和编辑操作
var commandcolumn_click_main = function (command, record) {
    commandcolumn_click_main_confirm(command, record);
    return false;
};

//区分删除操作，并进行二次确认操作
var commandcolumn_click_main_confirm = function (command, record) {
    if (command.toLowerCase() == "edit") {
        commandcolumn_direct_main_edit(record);
    }
    if (command.toLowerCase() == "delete") {
        Ext.Msg.confirm("提示", '您确定需要删除此条信息？', function (btn) { commandcolumn_direct_main_delete(btn, record) });
    }
    if (command.toLowerCase() == "recover") {
        Ext.Msg.confirm("提示", '您确定需要恢复此条信息？', function (btn) { commandcolumn_direct_main_recover(btn, record) });
    }
    return false;
};

//点击修改按钮
var commandcolumn_direct_main_edit = function (record) {
    var ObjID = record.data.ObjID;
    App.direct.commandcolumn_direct_edit(ObjID, {
        success: function (result) {
        },

        failure: function (errorMsg) {
            Ext.Msg.alert('操作', errorMsg);
        }
    });
}

//点击删除按钮
var commandcolumn_direct_main_delete = function (btn, record) {
    if (btn != "yes") {
        return;
    }
    var ObjID = record.data.ObjID;
    App.direct.commandcolumn_direct_delete(ObjID, {
        success: function (result) {
            Ext.Msg.alert('操作', result);
        },

        failure: function (errorMsg) {
            Ext.Msg.alert('操作', errorMsg);
        }
    });
}

//点击恢复按钮
var commandcolumn_direct_main_recover = function (btn, record) {
    if (btn != "yes") {
        return;
    }
    var ObjID = record.data.ObjID;
    App.direct.commandcolumn_direct_recover(ObjID, {
        success: function (result) {
        },

        failure: function (errorMsg) {
            Ext.Msg.alert('操作', errorMsg);
        }
    });
}

//列表刷新数据重载方法
var pnlListFresh = function () {
    App.hidden_delete_flag.setValue("0");
    App.store.currentPage = 1;
    App.pageToolBar.doRefresh();
    return false;
}

//历史查询按钮点击列表刷新数据重载方法
var pnlHistoryListFresh = function () {
    App.hidden_delete_flag.setValue("");
    App.store.currentPage = 1;
    App.pageToolBar.doRefresh();
    return false;
}

//历史查询根据DeleteFlag的值进行样式绑定
var SetRowClass = function (record, rowIndex, rowParams, store) {
    if (record.get("DeleteFlag") == "1") {
        return "x-grid-row-deleted";
    }
}
//历史查询的每行按钮准备加载
var prepareToolbar = function (grid, toolbar, rowIndex, record) {
    if (record.get("DeleteFlag") == "1") {
        toolbar.items.getAt(0).hide();
        toolbar.items.getAt(1).hide();
        toolbar.items.getAt(2).hide();
        toolbar.items.getAt(3).hide();
    } else {
        toolbar.items.getAt(4).hide();
    }
};

//点击开炼动作某条信息触发事件
var selectMainData = function (data) {
    App.hidden_main_id.setValue(data);
    refreshOpenModelDetail();
}
//--开炼动作模板Main增删改查方法End