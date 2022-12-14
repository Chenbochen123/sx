var SetRowClass = function (record, rowIndex, rowParams, store) {
    if (record.get("DeleteFlag") == "1") {
        return "x-grid-row-deleted";
    }
}

//点击修改按钮
var commandcolumn_direct_edit = function (record) {
    var ObjID = record.data.ObjID;
    App.direct.commandcolumn_direct_edit(ObjID, {
        success: function (result) {
        },

        failure: function (errorMsg) {
            waitProgressWindow.close();
            Ext.Msg.alert('操作异常', errorMsg);
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
        },

        failure: function (errorMsg) {
            Ext.Msg.alert('操作异常', errorMsg);
        }
    });
}


//根据按钮类别进行删除和编辑操作
var commandcolumn_click = function (command, record) {
    if (command.toLowerCase() == "edit") {
        commandcolumn_direct_edit(record);
    }
    if (command.toLowerCase() == "delete") {
        Ext.Msg.confirm("提示", '您确定需要清空此条信息？', function (btn) { commandcolumn_direct_delete(btn, record) });
    }
    return false;
};




Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryBasStorage_Window",
    height: 460,
    hidden: true,
    width: 360,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryBasStorage.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择仓库",
    modal: true
})
var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {
    var queryWindow = App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window;
    var thisIsAddWindow = function (record) {
        if ((!App.winAdd) || (App.winAdd.hidden)) {
            return;
        }
        App.winAddtxtStorageID.setValue(record.data.StorageID);
        App.winAddtxtStorageName.setValue(record.data.StorageName);
    }
    var thisIsEditWindow = function (record) {
        if ((!App.winModify) || (App.winModify.hidden)) {
            return;
        }
        var oldid = App.winModifytxtStorageID.getValue();
        if (oldid != record.data.StorageID) {
            App.winModifytxtStorageID.setValue(record.data.StorageID);
            App.winModifytxtStorageName.setValue(record.data.StorageName);
        }
    }
    var thisIsDefaultWindow = function (record) {
        return;
    }
    thisIsAddWindow(record);
    thisIsEditWindow(record);
    thisIsDefaultWindow(record);
    queryWindow.close();
}
var GetStorageInfo = function () {
    var queryWindowShow = function (record) {
        var queryWindow = App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window;
        queryWindow.show();
    }
    queryWindowShow();
}



Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window",
    height: 460,
    hidden: true,
    width: 360,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryBasStoragePlace.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择仓库库位",
    modal: true
})
var Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Request = function (record) {
    var queryWindow = App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window;
    var thisIsAddWindow = function (record) {
        if ((!App.winAdd) || (App.winAdd.hidden)) {
            return;
        }
        App.winAddtxtStoragePlaceID.setValue(record.data.StoragePlaceID);
        App.winAddtxtStoragePlaceName.setValue(record.data.StoragePlaceName);
    }
    var thisIsEditWindow = function (record) {
        if ((!App.winModify) || (App.winModify.hidden)) {
            return;
        }
        var oldid = App.winModifytxtStorageID.getValue();
        if (oldid != record.data.StoragePlaceID) {
            App.winModifytxtStoragePlaceID.setValue(record.data.StoragePlaceID);
            App.winModifytxtStoragePlaceName.setValue(record.data.StoragePlaceName);
        }
    }
    var thisIsDefaultWindow = function (record) {
        return;
    }
    thisIsAddWindow(record);
    thisIsEditWindow(record);
    thisIsDefaultWindow(record);
    queryWindow.close();
}
var GetStoragePlaceInfo = function () {
    var queryWindowShow = function (record) {
        var queryWindow = App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window;
        var id = "";
        if ((App.winModify) && (!App.winModify.hidden)) {
            id = App.winModifytxtStorageID.getValue();
        }
        var html = "<iframe src='../../BasicInfo/CommonPage/QueryBasStoragePlace.aspx?StorageID=" + id + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
        if (queryWindow.getBody()) {
            queryWindow.getBody().update(html);
        } else {
            queryWindow.html = html;
        }
        queryWindow.show();
    }
    queryWindowShow();
}



Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryMaterialMinorType_Window",
    height: 470,
    hidden: true,
    width: 360,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterialMinorType.aspx?MajorTypeID=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择物料的物料细类",
    modal: true
})
var Manager_BasicInfo_CommonPage_QueryMaterialMinorType_Request = function (record) {
    var queryWindow = App.Manager_BasicInfo_CommonPage_QueryMaterialMinorType_Window;
    var thisIsAddWindow = function (record) {
        if ((!App.winAdd) || (App.winAdd.hidden)) {
            return;
        }
    }
    var thisIsEditWindow = function (record) {
        if ((!App.winModify) || (App.winModify.hidden)) {
            return;
        }
    }
    var thisIsDefaultWindow = function (record) {
        var oldid = App.txtMaterialMinorID.getValue();
        if (oldid != record.data.ObjID) {
            App.txtMaterialMinorID.setValue(record.data.MinorTypeID);
            App.txtMaterialMinorName.setValue(record.data.MinorTypeName);
        }
    }
    thisIsAddWindow(record);
    thisIsEditWindow(record);
    thisIsDefaultWindow(record);
    queryWindow.close();
}
var GetMaterialMinorInfo = function () {
    var queryWindowShow = function (record) {
        var queryWindow = App.Manager_BasicInfo_CommonPage_QueryMaterialMinorType_Window;
        queryWindow.show();
    }
    queryWindowShow();
}





Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
    height: 450,
    hidden: true,
    width: 370,
    html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择物料信息",
    modal: true
})
var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {
    var queryWindow = App.Manager_BasicInfo_CommonPage_QueryMaterial_Window;
    var thisIsAddWindow = function (record) {
        if ((!App.winAdd) || (App.winAdd.hidden)) {
            return;
        }
        App.winModifytxtMaterialID.setValue(record.data.MaterialCode);
        App.winModifytxtMaterialName.setValue(record.data.MaterialName);
    }
    var thisIsEditWindow = function (record) {
        if ((!App.winModify) || (App.winModify.hidden)) {
            return;
        }
        var oldid = App.winModifytxtMaterialID.getValue();
        if (oldid != record.data.StoragePlaceID) {
            App.winModifytxtMaterialID.setValue(record.data.MaterialCode);
            App.winModifytxtMaterialName.setValue(record.data.MaterialName);
        }
    }
    var thisIsDefaultWindow = function (record) {
        return;
    }
    thisIsAddWindow(record);
    thisIsEditWindow(record);
    thisIsDefaultWindow(record);
    queryWindow.close();
}
var GetMaterialInfo = function (field, trigger, index) {
    debugger;
    switch (index) {
        case 0:
            //field.getTrigger(0).hide();
            field.setValue('');
            App.winModifytxtMaterialID.setValue('');
            field.getEl().down('input.x-form-text').setStyle('background', "white");
            break;
        case 1:
            var queryWindowShow = function (record) {
                var queryWindow = App.Manager_BasicInfo_CommonPage_QueryMaterial_Window;
                var id = "";

                id = App.txtMaterialMinorID.getValue();
                var html = "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx?MinorTypeID=" + id + "&MajorTypeID=1' width=100% height=100% scrolling=no  frameborder=0></iframe>";
                if (queryWindow.getBody()) {
                    queryWindow.getBody().update(html);
                } else {
                    queryWindow.html = html;
                }
                queryWindow.show();
            }
            queryWindowShow();
            break;
    }
}


var windowOnShow = function (sender, fn) {
    var p = App.Viewport1;
    if (p) {
        for (var i = 0; i < p.items.length; i++) {
            p.getComponent(i).disable(true);
        }
    }
}
var windowOnHide = function () {
    var p = App.Viewport1;
    if (p) {
        for (var i = 0; i < p.items.length; i++) {
            p.getComponent(i).enable(true);
        }
    }
}