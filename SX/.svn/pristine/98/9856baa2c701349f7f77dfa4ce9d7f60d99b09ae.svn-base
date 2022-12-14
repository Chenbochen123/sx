var ButtonNorthQuery_Click = function () {
    App.direct.SearchQrigMasterLog({
        success: function (result) {
        },
        failure: function (errorMessage) {
            Ext.Msg.alert('错误', errorMessage);
        },
        eventMask: {
            showMask: true
        }
    });
};

var ButtonNorthExcel_Click = function () {
    if (App.CheckboxSelectionModelCenter.selected.length == 0) {
        Ext.Msg.alert('提示', '请选中要导出的记录');
        return false;
    }
    
    Ext.get('btnExportSubmit').dom.click();
};

var DateFieldNorthImportDate_TriggerClick = function (item, trigger, index, tag, e) {
    if (index == 0) {
        item.clear();
    }
};

var ColumnCenterFlag_Render = function (value) {
    if (value == "1") {
        return "已导入";
    }
    else {
        return "未导入";
    }
};

var CheckboxSelectionModelCenter_SelectionChange = function (item, selected) {
    var guid = "";
    if (selected.length > 0) {
        guid = selected[0].get("GUID");
    }
    App.direct.GetQrigDetailInfo(guid, {
        success: function (result) {
        },
        failure: function (errorMessage) {
            Ext.Msg.alert('错误', errorMessage);
        },
        eventMask: {
            showMask: true
        }
    });
};