﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialShopStoragePrint.aspx.cs" Inherits="Manager_ShopStorage_MaterialShopStoragePrint" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell
        {
        	background-color: #FF8C69 !important;
        }
    </style>
    <script type="text/javascript">
        var BatchPrint = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            var barcodes = "";
            var storages = "";
            var storageplaces = "";
            var matercodes = "";
            var orderids = "";

            if (section && section.length == 0) {
                Ext.Msg.alert("提示", '您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要批量打印吗？', function (btn) {
                    if (btn != "yes") {
                        return;
                    }

                    for (var i = 0; i < section.length; i++) {
                        var bc = App.store.data.get(section[i].index).data.Barcode;
                        barcodes = barcodes == "" ? bc : barcodes + "," + bc;
                    }
                    for (var i = 0; i < section.length; i++) {
                        var st = App.store.data.get(section[i].index).data.StorageID;
                        storages = storages == "" ? st : storages + "," + st;
                    }
                    for (var i = 0; i < section.length; i++) {
                        var sp = App.store.data.get(section[i].index).data.StoragePlaceID;
                        storageplaces = storageplaces == "" ? sp : storageplaces + "," + sp;
                    }
                    for (var i = 0; i < section.length; i++) {
                        var mc = App.store.data.get(section[i].index).data.MaterialCode;
                        matercodes = matercodes == "" ? mc : matercodes + "," + mc;
                    }
                    for (var i = 0; i < section.length; i++) {
                        var od = App.store.data.get(section[i].index).data.OrderID;
                        orderids = orderids == "" ? od : orderids + "," + od;
                    }

                    App.direct.BatchSaveInStorageTime(barcodes, storages, storageplaces, matercodes, {
                        success: function (result) { },
                        failure: function (err) { Ext.Msg.alert("错误", err); }
                    });

                    var url = "ShopStorage/BarcodePrint.aspx?Strs=" + barcodes + ";" + storages + ";" + storageplaces + ";" + orderids + ";" + returnStr(App.txtHouseNo2.getValue()) + ";" + returnStr(App.txtMark2.getValue()) + ";" + App.txtNum2.getValue() + ";" + returnStr(App.txtProductNo2.getValue());
                    var tabid = "Manager_Storage_BarcodePrint";
                    var tabp = parent.App.mainTabPanel;
                    var tab = tabp.getComponent(tabid);
                    if (tab) {
                        tab.close();
                    }
                    var title = "条码批量打印";
                    parent.addTab(tabid, title, url, true);
                    parent.refresh("");
                });
            }
        }

        var returnStr = function (str) {
            return escape(str).replace(/\+/g, '%2B').replace(/\"/g, '%22').replace(/\'/g, '%27').replace(/\//g, '%2F').replace(/\ /g, '%20').replace(/\?/g, '%3F').replace(/\#/g, '%23').replace(/\&/g, '%26').replace(/\=/g, '%3D');
        }

        var btnBatchPrint_Click = function () {
            //            var strValue = App.txtBarcode.getValue();
            //            var result = escape(strValue).replace(/\+/g, '%2B').replace(/\"/g, '%22').replace(/\'/g, '%27').replace(/\//g, '%2F').replace(/\ /g, '%20').replace(/\?/g, '%3F').replace(/\#/g, '%23').replace(/\&/g, '%26').replace(/\=/g, '%3D');
            //            Ext.Msg.alert("提示", result);
            var section = App.pnlList.getView().getSelectionModel().getSelection();
            if (section && section.length == 0) {
                Ext.Msg.alert("提示", '您没有选择任何项，请选择！');
                return;
            }
            App.direct.BatchScreenWinBatchPrint();
        }

        var btnPrint_Click = function () {
            var message = "";
            if (App.hiddenIsPrint.getValue() == "0") {
                message = "确定要打印此条码信息吗？";
            }
            else {
                message = "确定补打条码？";
            }
            Ext.Msg.confirm("提示", message, function (btn) {
                if (btn != "yes") {
                    return;
                }
                App.direct.SaveInStorageTime(App.txtBarcode1.getValue(), App.hiddenStorageID.getValue(), App.hiddenStoragePlaceID.getValue(), App.hiddenMaterCode.getValue(), {
                    success: function (result) {
                    },
                    failure: function (errorMsg) {
                        Ext.Msg.alert('操作', errorMsg);
                    }
                });

                var url = "ShopStorage/BarcodePrint.aspx?Str=" + App.hiddenStorageID.getValue() + "," + App.hiddenStoragePlaceID.getValue() + "," + App.txtBarcode1.getValue() + "," + App.hiddenOrderID.getValue() + "," + App.txtNum1.getValue() + "," + returnStr(App.txtHouseNo1.getValue()) + "," + returnStr(App.txtMark1.getValue()) + "," + returnStr(App.txtProductNo1.getValue());
                var tabid = "Manager_Storage_BarcodePrint";
                var tabp = parent.App.mainTabPanel;
                var tab = tabp.getComponent(tabid);
                if (tab) {
                    tab.close();
                }
                var title = "条码打印";
                parent.addTab(tabid, title, url, true);
                parent.refresh("");
            });

        }

        //根据按钮类别进行删除和编辑操作
        var commandcolumn_click = function (command, record) {
            var Barcode = record.data.Barcode;
            var StorageID = record.data.StorageID;
            var StoragePlaceID = record.data.StoragePlaceID;
            var OrderID = record.data.OrderID;
            App.direct.commandcolumn_direct_print(Barcode, StorageID, StoragePlaceID, OrderID, {
                success: function (result) {
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
            return false;
        };

        Ext.apply(Ext.form.VTypes, {
            integer: function (val, field) {
                if (!val) {
                    return true;
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
            integerText: "此填入项格式为正整数",
            decimal: function (val, field) {
                if (!val) {
                    return true;
                }
                try {
                    if (/^[\d.\d]+$/.test(val)) {
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
            decimalText: "此填入项格式为浮点数"
        });

        var pnlListFresh = function () {
            if (App.txtBeginTime.getValue() > App.txtEndTime.getValue()) {
                Ext.Msg.alert('操作', '开始时间不能大于结束时间！');
                return false;
            }
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        var startTrack = function () {
            this.checkboxes = [];
            var cb;

            Ext.select(".x-form-item", false).each(function (checkEl) {
                cb = Ext.getCmp(checkEl.dom.id.selected);
                cb.setValue(false);
                this.rowselect.push(cb);
            }, this);
        };

        //--查询带弹出框--BEGIN   Manager_BasicInfo_CommonPage_QueryFactoryType_Request
        var Manager_BasicInfo_CommonPage_QueryFactory_Request = function (record) {//生产厂家信息返回值处理
            if (!App.winAdd.hidden) {
                App.txtFactoryID2.setValue(record.data.FacName);
                App.hiddenFactoryID.setValue(record.data.ObjID);
            }
            else if (!App.winModify.hidden) {
                App.hiddenBillNo.setValue(record.data.BillNo);
                App.hiddenFactoryID.setValue(record.data.FactoryID);
            }
            else {
                App.txtFactoryID.setValue(record.data.FacName);
                App.hiddenFactoryID.setValue(record.data.ObjID);
            }
        }

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            App.txtMaterName.getTrigger(0).show();
            App.txtMaterName.setValue(record.data.MaterialName);
            App.hiddenMaterCode.setValue(record.data.MaterialCode);
            App.pageToolBar.doRefresh();
        }

        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            App.txtStorageName.getTrigger(0).show();
            App.txtStorageName.setValue(record.data.StorageName);
            App.hiddenStorageID.setValue(record.data.StorageID);
            App.txtStoragePlaceName.setValue(record.data.StoragePlaceName);
            App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            App.pageToolBar.doRefresh();
        }

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Request = function (record) {//库位信息返回值处理
            App.txtStoragePlaceName.getTrigger(0).show();
            App.txtStorageName.setValue(record.data.StorageName);
            App.hiddenStorageID.setValue(record.data.StorageID);
            App.txtStoragePlaceName.setValue(record.data.StoragePlaceName);
            App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            App.pageToolBar.doRefresh();
        }

        var AddMaterial = function (field, trigger, index) {//物料添加
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenMaterCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
                    break;
            }
        }
        var AddStorage = function (field, trigger, index) {//库房添加
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenStorageID.setValue("");
                    App.txtStoragePlaceName.setValue("");
                    App.hiddenStoragePlaceID.setValue("");
                    App.txtStoragePlaceName.getTrigger(0).hide();
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
                    break;
            }
        }
        var AddStoragePlace = function (field, trigger, index) {//库位添加
            var url = "../BasicInfo/CommonPage/QueryBasStoragePlace.aspx?StorageID=" + App.hiddenStorageID.getValue();
            var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody()) {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody().update(html);
            } else {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.html = html;
            }

            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenStoragePlaceID.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.show();
                    break;
            }
        }

        Ext.create("Ext.window.Window", {//库房带窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasStorage_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryBasStorage.aspx?StorageType=1&&LastStorageFlag=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择库房名称",
            modal: true
        })

        Ext.create("Ext.window.Window", {//库位带窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window",
            height: 460,
            hidden: true,
            width: 360,
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择库位",
            modal: true
        })

        Ext.create("Ext.window.Window", {//物料带窗体
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料",
            modal: true
        })

        var isPrintChange = function (value) {
            return Ext.String.format(value == "1" ? "已打印" : "未打印");
        };

        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("IsPrint") == "0") {
                return "x-grid-row-collapsed";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmPrint" runat="server" />
    <ext:Viewport ID="vpPrint" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnPrint" runat="server" Region="North" Height="90">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbPrint">
                        <Items>
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <Listeners>
                                    <Click Fn="pnlListFresh" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsBegin" />
                                <ext:Button runat="server" Icon="PrinterGo" Text="批量打印" ID="Button1">
                                    <Listeners>
                                        <Click Handler="btnBatchPrint_Click();" />
                                    </Listeners>
                                </ext:Button>
                            <ext:ToolbarSeparator ID="tsEnd" />
                            <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                            <ext:ToolbarFill ID="tfEnd" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                        <Items>
                            <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                Padding="5">
                                <Items>
                                    <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="库房名称" LabelAlign="Left"
                                        Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="AddStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:DateField ID="txtBeginTime" runat="server" OnDirectChange="txtBeginTime_change" FieldLabel="开始时间" LabelAlign="Left" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                Padding="5">
                                <Items>
                                    <ext:TriggerField ID="txtStoragePlaceName" runat="server" FieldLabel="库位名称" LabelAlign="Left"
                                        Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="AddStoragePlace" />
                                        </Listeners>
                                    </ext:TriggerField>                                    
                                    <ext:DateField ID="txtEndTime" runat="server" OnDirectChange="txtEndTime_change" FieldLabel="结束时间" LabelAlign="Left" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                Padding="5">
                                <Items>
                                     <ext:TriggerField ID="txtMaterName" runat="server" FieldLabel="物料名称" LabelAlign="Left"
                                        Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="AddMaterial" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:Checkbox ID="cbxIsPrint" runat="server" BoxLabelAlign="After" OnDirectCheck="cbxIsPrint_Change" BoxLabel="显示已打印的数据" Checked="false" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container4" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                Padding="5">
                                <Items>
                                    <ext:TextField ID="txtBarcode" runat="server" FieldLabel="条码号" LabelAlign="Left" />
                                </Items>
                            </ext:Container>
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{btnSearch}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>

            <ext:Panel ID="Panel2" runat="server" Region="Center" Frame="true" Layout="Fit" MarginsSummary="0 5 0 5">
                <Items>
                    <ext:GridPanel ID="pnlList" runat="server">
                        <Store>
                            <ext:Store ID="store" runat="server" PageSize="50">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model" runat="server" IDProperty="Barcode, StorageID, StoragePlaceID, OrderID">
                                        <Fields>
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="MaterialCode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="Num" />
                                            <ext:ModelField Name="PieceWeight" />
                                            <ext:ModelField Name="Weight" />
                                            <ext:ModelField Name="InaccountDate" Type="Date" />
                                            <ext:ModelField Name="StorageID" />
                                            <ext:ModelField Name="StorageName" />
                                            <ext:ModelField Name="StoragePlaceID" />
                                            <ext:ModelField Name="StoragePlaceName" />
                                            <ext:ModelField Name="ShiftClassID" />
                                            <ext:ModelField Name="ClassName" />
                                            <ext:ModelField Name="ObjID" />
                                            <ext:ModelField Name="FacName" />
                                            <ext:ModelField Name="OrderID" />
                                            <ext:ModelField Name="IsPrint" />
                                            <ext:ModelField Name="InTime" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                    <Commands>
                                        <ext:GridCommand Icon="PrinterStart" CommandName="Print" Text="生成流转卡片设置" />
                                    </Commands>
                                    <Listeners>
                                        <Command Handler="return commandcolumn_click(command, record);" />
                                    </Listeners>
                                </ext:CommandColumn>
                                <ext:Column ID="Barcode" runat="server" Text="条码号" DataIndex="Barcode" Width="190" />
                                <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Width="110" />
                                <ext:Column ID="Num" runat="server" Text="数量" DataIndex="Num" Width="60" />
                                <ext:Column ID="Weight" runat="server" Text="重量" DataIndex="Weight" Width="80" />
                                <ext:DateColumn ID="InaccountDate" runat="server" Text="日期" DataIndex="InaccountDate" Width="90" Format="yyyy-MM-dd" />
                                <ext:Column ID="ClassName" runat="server" Text="班组" DataIndex="ClassName" Width="40" />
                                <ext:Column ID="StorageName" runat="server" Text="库房" DataIndex="StorageName" Width="90" />
                                <ext:Column ID="StoragePlaceName" runat="server" Text="库位" DataIndex="StoragePlaceName" Width="110" />
                                <ext:Column ID="FacName" runat="server" Text="生产厂家" DataIndex="FacName" Width="150" />
                                <ext:Column ID="IsPrint" runat="server" Text="是否打印" DataIndex="IsPrint" Width="60" >
                                    <Renderer Fn="isPrintChange" />
                                </ext:Column>
                                <ext:DateColumn ID="InTime" runat="server" Text="打印时间" DataIndex="InTime" Width="140" Format="yyyy-MM-dd HH:mm:ss" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:CheckboxSelectionModel  ID="rowSelectMuti" runat="server" Mode="Multi" />
                        </SelectionModel>
                        <View>
                            <ext:GridView ID="gvRows" runat="server">
                                <GetRowClass Fn="SetRowClass" />
                            </ext:GridView>
                        </View>
                        <BottomBar>
                            <ext:PagingToolbar ID="pageToolBar" runat="server">
                                <Items>
                                    <ext:Label ID="Label2" runat="server" Text="每页条数:" />
                                    <ext:ToolbarSpacer ID="ToolbarSpacer2" runat="server" Width="10" />
                                    <ext:ComboBox ID="ComboBox2" runat="server" Width="80">
                                        <Items>
                                            <ext:ListItem Text="10" />
                                            <ext:ListItem Text="50" />
                                            <ext:ListItem Text="100" />
                                            <ext:ListItem Text="200" />
                                        </Items>
                                        <SelectedItems>
                                            <ext:ListItem Value="50" />
                                        </SelectedItems>
                                        <Listeners>
                                            <Select Handler="#{pnlList}.store.pageSize = parseInt(this.getValue(), 10); #{pageToolBar}.doRefresh(); return false;" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>

            <ext:Window ID="winSet" runat="server" Icon="MonitorEdit" Closable="true" Title="生成流转卡设置"
                Width="320" Height="315" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="Form">
                <Items>
                    <ext:FormPanel ID="FormPanel1" runat="server" BodyPadding="5">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:TextField ID="txtStorageID1" runat="server" FieldLabel="所在库房" LabelAlign="Right" Width="280px" ReadOnly="true" />
                            <ext:TextField ID="txtStoragePlaceID1" runat="server" FieldLabel="所在库位" LabelAlign="Right" Width="280px" ReadOnly="true" />
                            <ext:TextField ID="txtBarcode1" runat="server" FieldLabel="条码" LabelAlign="Right" Width="280px" ReadOnly="true" />
                            <ext:TextField ID="txtMaterCode1" runat="server" FieldLabel="物料名称" LabelAlign="Right" Width="280px" ReadOnly="true" />
                            <ext:TextField ID="txtWeight1" runat="server" FieldLabel="重量" LabelAlign="Right" Width="280px" ReadOnly="true" />
                            <ext:TextField ID="txtProductNo1" runat="server" FieldLabel="批次号" LabelAlign="Right" Width="280px" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                            <ext:TextField ID="txtHouseNo1" runat="server" FieldLabel="烘房号" LabelAlign="Right" Width="280px" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                            <ext:TextField ID="txtMark1" runat="server" FieldLabel="其它" LabelAlign="Right" Width="280px" />
                            <ext:TextField ID="txtNum1" runat="server" FieldLabel="打印条数" Vtype="integer" LabelAlign="Right" Width="280px" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{btnPrint}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnPrint" runat="server" Text="生成流转卡" Icon="Accept" Disabled="false">
                        <Listeners>
                            <Click Fn="btnPrint_Click" />
                        </Listeners>
                    </ext:Button>
                    <ext:Button ID="btnCancel" runat="server" Text="取消" Icon="Cancel">
                        <DirectEvents>
                            <Click OnEvent="btnCancel_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="for(var i=0;i<#{vpPrint}.items.length;i++){#{vpPrint}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpPrint}.items.length;i++){#{vpPrint}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>

            <ext:Window ID="winBatch" runat="server" Icon="MonitorEdit" Closable="true" Title="批量生成流转卡设置"
                Width="320" Height="315" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="Form">
                <Items>
                    <ext:FormPanel ID="FormPanel2" runat="server" BodyPadding="5">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:TextField ID="txtProductNo2" runat="server" FieldLabel="批次号" LabelAlign="Right" Width="280px" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                            <ext:TextField ID="txtHouseNo2" runat="server" FieldLabel="烘房号" LabelAlign="Right" Width="280px" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                            <ext:TextField ID="txtMark2" runat="server" FieldLabel="其它" LabelAlign="Right" Width="280px" />
                            <ext:TextField ID="txtNum2" runat="server" FieldLabel="打印条数" Vtype="integer" LabelAlign="Right" Width="280px" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{btnPrint1}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnPrint1" runat="server" Text="生成流转卡" Icon="Accept" Disabled="true">
                        <Listeners>
                            <Click Fn="BatchPrint" />
                        </Listeners>
                    </ext:Button>
                    <ext:Button ID="btnCancel1" runat="server" Text="取消" Icon="Cancel">
                        <DirectEvents>
                            <Click OnEvent="btnCancel_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="for(var i=0;i<#{vpPrint}.items.length;i++){#{vpPrint}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpPrint}.items.length;i++){#{vpPrint}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>

            <ext:Hidden ID="hiddenStorageID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenStoragePlaceID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenOrderID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenFactoryID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenMaterCode" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenIsPrint" runat="server"></ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
