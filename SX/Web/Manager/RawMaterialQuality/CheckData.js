// 供应商查询带回窗体
Ext.create("Ext.window.Window", {
    id: "Manager_BasicInfo_CommonPage_QueryFactory_Window",
    height: 460,
    hidden: true,
    width: 760,
    html: "<iframe src='../BasicInfo/CommonPage/QueryFactory.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择供应商",
    modal: true
});

// 供应商返回值处理
var Manager_BasicInfo_CommonPage_QueryFactory_Request = function (record) {
    var ComboBoxSupplyFac;
    if (App.WindowMaster.hidden == true) {
        ComboBoxSupplyFac = App.ComboBoxNorthSupplyFac;
    }
    else {
        ComboBoxSupplyFac = App.ComboBoxMasterSupplyFac;
    }

    var facId = record.data.ObjID.toString();
    var facName = Ext.util.Format.trim(record.data.FacName);
    if (ComboBoxSupplyFac.findRecordByValue(facId) == false) {
        ComboBoxSupplyFac.insertItem(0, facName, facId);
    }
    ComboBoxSupplyFac.setValue(facId);

};

// 查询时清空或选择供应商
var ComboBoxNorthSupplyFac_TriggerClick = function (item, trigger, index) {
    if (index == 0) {
        // 清空
        App.ComboBoxNorthSupplyFac.setValue("");
    }
    else if (index == 1) {
        App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
    }
};

// 样品台账查询带回窗体
Ext.create("Ext.window.Window", {
    id: "Manager_RawMaterialQuality_QueryQmcSampleLedger_Window",
    height: 500,
    hidden: true,
    width: 800,
    html: "<iframe src='QueryQmcSampleLedger.aspx' width=100% height=460 scrolling=no  frameborder=0></iframe>",
    bodyStyle: "background-color: #fff;",
    closable: true,
    title: "请选择样品台账",
    modal: true
});

// 送检单返回值处理
var Manager_RawMaterialQuality_QueryQmcSampleLedger_Request = function (record) {
    var ledgerId = record.data.LedgerId;
    var billDetailId = record.data.BillDetailId;
    var billNo = record.data.BillNo;
    var barcode = record.data.Barcode;
    var batchCode = record.data.BatchCode;
    var frequency = record.data.Frequency;
    var orderId = record.data.OrderId;
    var specId = record.data.SpecId;

    var materCode = record.data.MaterialCode;
    var sampleCode = record.data.SampleCode;
    var sampleName = record.data.SampleName;
    var sampleNum = record.data.SampleNum;
    var sampleUnit = record.data.SampleUnit;
    var sampleStatus = record.data.SampleStatus;

    var supplierName = record.data.SupplierName;
    var supplierId = record.data.SupplierId;
    var manufacturerId = record.data.ManufacturerId;
    var manufacturerName = record.data.ManufacturerName;
    var factoryCode = record.data.FactoryCode;

    var extractorName = record.data.ExtractorName;
    var receiverName = record.data.ReceiverName;
    var fetcherName = record.data.FetcherName;

    var receiveDate = record.data.ReceiveDate == null ? "" : Ext.util.Format.date(record.data.ReceiveDate, 'Y-m-d');
    var sendDate = record.data.SendDate == null ? "" : Ext.util.Format.date(record.data.SendDate, 'Y-m-d');
    var sampleRemark = record.data.Remark;

    App.HiddenMasterLedgerId.setValue(ledgerId);
    App.HiddenMasterBillNo.setValue(billNo);
    App.HiddenMasterBarcode.setValue(barcode);
    App.HiddenMasterBatchCode.setValue(batchCode);
    App.HiddenMasterOrderID.setValue(orderId);
    App.HiddenMasterMaterCode.setValue(materCode);
    App.HiddenMasterSupplyFacId.setValue(supplierId);
    App.HiddenMasterProductFacId.setValue(manufacturerId);
    App.HiddenMasterSpecId.setValue(specId);

    App.TriggerFieldMasterSampleName.setValue(sampleName);
    App.TextFieldMasterSupplierName.setValue(supplierName);
    App.TextFieldMasterManufacturerName.setValue(manufacturerName);
    App.TextFieldMasterFactoryCode.setValue(factoryCode);
    App.TextFieldMasterBarcode.setValue(barcode);
    //App.TextFieldMasterBatchCode.setValue(batchCode);
    App.TextFieldMasterFrequency.setValue(frequency);
    App.TextFieldMasterSampleNum.setValue(sampleNum + sampleUnit);
    App.TextFieldMasterSampleCode.setValue(sampleCode);
    App.TextFieldMasterSampleStatus.setValue(sampleStatus);
    App.TextFieldMasterExtractorName.setValue(extractorName);
    App.TextFieldMasterReceiverName.setValue(receiverName);
    App.TextFieldMasterFetcherName.setValue(fetcherName);
    App.TextFieldMasterReceiveDate.setValue(receiveDate);
    App.TextFieldMasterSendDate.setValue(sendDate);
    App.TextFieldMasterSampleRemark.setValue(sampleRemark);

    App.direct.DMFillCheckDataSet(materCode, {
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

// 提交前保存
var ButtonMasterAccept_Click = function () {
    var alertMessage = '';
    var jsonDetail = {};
    var lenDetail = App.FieldSetMasterDetail.items.items.length;
    for (var index = 0; index < lenDetail; index++) {
        var item = App.FieldSetMasterDetail.items.items[index];
        if (item.rawValue == "") {
            jsonDetail[item.id] = "";
            continue;
        }
        switch (item.xtype) {
            case "textfield":
                jsonDetail[item.id] = item.rawValue;
                break;
            case "numberfield":
                jsonDetail[item.id] = item.rawValue;
                break;
            case "combobox":
                jsonDetail[item.id] = item.value;
                break;
            case "datefield":
                jsonDetail[item.id] = item.rawValue;
                break;
            default:
                jsonDetail[item.id] = item.rawValue;
                break;
        }
    }

    var commandName = App.HiddenMasterCommandName.getValue();
    if (App.CheckboxMasterRecordStat.checked == false
        || commandName == "SpecUpdate") {
        App.direct.DMValidFrequency(jsonDetail, {
            success: function (result) {
                alertMessage = result;
                if (alertMessage != '') {
                    Ext.Msg.confirm('提示', alertMessage + "您确定要提交吗？", function (btn) {
                        if (btn == "yes") {
                            SaveCheckData();
                        }
                    });
                }
                else {
                    SaveCheckData();
                }
            },
            failure: function (errorMessage) {
                Ext.Msg.alert('错误', errorMessage);
            },
            eventMask: {
                showMask: true
            }
        });
        return;
    }

    App.direct.DMValidateCheckData({
        success: function (result) {
            if (result == false) {
                return;
            }  
            App.direct.DMValidFrequency(jsonDetail, {
                success: function (result) {
                    alertMessage = result;
                    Ext.Msg.confirm('提示', '提交后不允许修改，' + alertMessage + "您确定要提交吗？", function (btn) {
                        if (btn == "yes") {
                            SaveCheckData();
                        }
                    });
                },
                failure: function (errorMessage) {
                    Ext.Msg.alert('错误', errorMessage);
                },
            });
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
    if (record.get('AutoCheckResult') == '0') {
        return 'x-grid-row-deleted';
    }
};

// 保存检验数据
var SaveCheckData = function () {
    var jsonProperty = {};
    var lenProperty = App.FieldSetMasterProperty.items.items.length;
    for (var index = 0; index < lenProperty; index++) {
        var item = App.FieldSetMasterProperty.items.items[index];
        if (item.rawValue == "") {
            jsonProperty[item.id] = "";
            continue;
        }
        switch (item.xtype) {
            case "textfield":
                jsonProperty[item.id] = item.rawValue;
                break;
            case "numberfield":
                jsonProperty[item.id] = item.rawValue;
                break;
            case "combobox":
                jsonProperty[item.id] = item.value;
                break;
            case "datefield":
                jsonProperty[item.id] = item.rawValue;
                break;
            default:
                jsonProperty[item.id] = item.rawValue;
                break;
        }
    }

    var jsonDetail = {};
    var lenDetail = App.FieldSetMasterDetail.items.items.length;
    for (var index = 0; index < lenDetail; index++) {
        var item = App.FieldSetMasterDetail.items.items[index];
        if (item.rawValue == "") {
            jsonDetail[item.id] = "";
            continue;
        }
        switch (item.xtype) {
            case "textfield":
                jsonDetail[item.id] = item.rawValue;
                break;
            case "numberfield":
                jsonDetail[item.id] = item.rawValue;
                break;
            case "combobox":
                jsonDetail[item.id] = item.value;
                break;
            case "datefield":
                jsonDetail[item.id] = item.rawValue;
                break;
            default:
                jsonDetail[item.id] = item.rawValue;
                break;
        }
    }

    App.direct.DMSaveCheckData(jsonProperty, jsonDetail, {
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

// 区分行
var GridViewCenterMaster_GetRowClass = function (record, index, rowParams, store) {
    if (record.get("RecordStat") == "1") {
        return "x-grid-row-summary";
    }
    return "";
};

// 区分提交状态
var ColumnCenterMasterRecordStatDes_Renderer = function (value, metadata, record, rowIndex, colIndex, store, view) {
    if (record.get("RecordStat") == "1") {
        return "<span style='background-color: lightgreen'>" + value + "</span>";
    }
    else if (record.get("RecordStat") == "0") {
        return "<span style='background-color: yellow'>" + value + "</span>";
    }
    return value;
};

// 区分审核状态
var ColumnCenterMasterApproveStatDes_Renderer = function (value, metadata, record, rowIndex, colIndex, store, view) {
    if (record.get("ApproveFlag") == "1") {
        return "<span style='background-color: lightgreen'>" + value + "</span>";
    }
    else if (record.get("ApproveFlag") == "0") {
        return "<span style='background-color: yellow'>" + value + "</span>";
    }
    return value;
};

// 区分检验结果
var ColumnCenterMasterCheckResultDes_Renderer = function (value, metadata, record, rowIndex, colIndex, store, view) {
    if (record.get("CheckResult") == "0") {
        return "<span style='background-color: red'>" + value + "</span>";
    }
    return value;
};

// 清空或查找供应商
var ComboBoxMasterSupplyFac_Click = function (item, trigger, index) {
    if (index == 0) {
        // 清空
        App.ComboBoxMasterSupplyFac.setValue("");
    }
    else if (index == 1) {
        // 查找
        App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
    }
};

// 显示或隐藏确认按钮
var CommandColumnCenterMaster_PrepareToolbar = function (grid, toolbar, rowIndex, record) {
    var specEditFlag = App.HiddenSpecEditFlag.getValue();
    var recordStat = record.data.RecordStat;
    if (specEditFlag == '1') {
        // 拥有提交后修改的权限，可以确认提交
        toolbar.items.get(0).setDisabled(recordStat);
        return;
    }
    var userId = App.HiddenUserId.getValue();
    var recorderId = record.data.RecorderId;
    if (recorderId != userId) {
        // 如果不是录入人，则不可以确认提交
        toolbar.items.get(0).setDisabled(1);
        return;
    }

    // 确认提交状态为0时，可以确认提交
    toolbar.items.get(0).setDisabled(recordStat);

};