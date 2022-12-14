//点击修改按钮
var commandcolumn_direct_edit = function (record) {
    var ObjID = record.data.ObjID;
    App.direct.commandcolumn_direct_edit(ObjID, {
        success: function (result) {
        },

        failure: function (errorMsg) {
            Ext.Msg.alert('操作', errorMsg);
        }
    });
}

//点击恢复按钮
var commandcolumn_direct_recover = function (btn, record) {
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

//点击删除按钮
var commandcolumn_direct_delete = function (btn, record) {
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

//区分删除操作，并进行二次确认操作
var commandcolumn_click_confirm = function (command, record) {
    if (command.toLowerCase() == "edit") {
        commandcolumn_direct_edit(record);
    }
    if (command.toLowerCase() == "delete") {
        Ext.Msg.confirm("提示", '您确定需要删除此条信息？', function (btn) { commandcolumn_direct_delete(btn, record) });
    }
    if (command.toLowerCase() == "recover") {
        Ext.Msg.confirm("提示", '您确定需要恢复此条信息？', function (btn) { commandcolumn_direct_recover(btn, record) });
    }
    return false;
};


//根据按钮类别进行删除和编辑操作
var commandcolumn_click = function (command, record) {
    commandcolumn_click_confirm(command, record);
    return false;
};

Ext.apply(Ext.form.VTypes, {
    integer: function (val, field) {
        if (!val) {
            return;
        }
        try {
            if (/^[\d]+$/.test(val)) {
                return true;
            }
            else {
                return false;
            }
        }
        catch (e) {
            return false;
        }
    },
    integerText: "此填入项格式为正整数"
});


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
    if (!App.winAdd.hidden) {
        App.add_material_name.getTrigger(0).show();
        App.hidden_material_code.setValue(record.data.MaterialCode);
        App.add_material_name.setValue(record.data.MaterialName);
    }
    else if (!App.winModify.hidden) {
        App.modify_material_name.getTrigger(0).show();
        App.hidden_material_code.setValue(record.data.MaterialCode);
        App.modify_material_name.setValue(record.data.MaterialName);
    }
    else {
        App.txt_material_name.getTrigger(0).show();
        App.hidden_material_code.setValue(record.data.MaterialCode);
        App.txt_material_name.setValue(record.data.MaterialName);
    }
}
var QueryMaterialInfo = function (field, trigger, index) {
    switch (index) {
        case 0:
            field.getTrigger(0).hide();
            field.setValue('');
            App.hidden_material_code.setValue('');
            field.getEl().down('input.x-form-text').setStyle('background', "white");
            break;
        case 1:
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
            break;
    }
}