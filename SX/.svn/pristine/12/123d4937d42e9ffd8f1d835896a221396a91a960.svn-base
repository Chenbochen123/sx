var FileUploadFieldNorthExcel_Change = function () {
    App.direct.SelectExcel({
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


var setRowClass = function (record, index, rowParams, store) {
    if (record.get('flag') == '1') {
        return 'x-grid-row-deleted';
    }
};

var ToolTipCenterOrigin_Show = function (item) {
    item.update(App.GridPanelCenterOrigin.getView().getRecord(item.triggerElement).get('errmsg'));
};

var ToolTipCenterError_Show = function (item) {
    item.update(App.GridPanelCenterError.getView().getRecord(item.triggerElement).get('errmsg'));
};

var ButtonNorthSave_Click = function (item) {
    if (App.StoreCenterSave.data.length == 0) {
        Ext.Msg.show({
            title: '提示',
            msg: '没有保存的数据',
            minWidth: 200,
            icon: Ext.Msg.INFO,
            buttons: Ext.Msg.OK
        });
        //Ext.net.Mask.hide();
    }
    else {

        var o = Ext.Msg.confirm({
            title: '提示',
            msg: '确定要保存吗',
            icon: Ext.Msg.QUESTION,
            buttons: Ext.Msg.OKCANCEL,
            callback: function (btn) {
                if (btn == 'ok') {
                    App.StoreCenterSave.submitData(null, {
                        success: function (result) {
                            var serviceResponse = Ext.decode(result.responseText).serviceResponse;
                            if (serviceResponse.success == false) {
                                Ext.Msg.alert('错误', serviceResponse.message);
                            }
                        },
                        failure: function (result) {
                            Ext.Msg.alert('提示', '保存失败');
                        },
                        eventMask: {
                            showMask: true,
                            msg: '数据保存中...'
                        }
                    });
                }
            }
        });

    }
};
