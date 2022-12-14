<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RubberAdjust.aspx.cs" Inherits="Manager_Rubber_RubberAdjust" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        var SetStockFlag = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要审核调拨吗？', function (btn) { commandcolumn_direct_setstockflag(btn) });
            }
        }

        var commandcolumn_direct_setstockflag = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnBatchStock_Click({
                success: function (result) {
                    if (result == "false")
                        Ext.Msg.alert('操作', "操作失败！");
                    else if (result == "OK")
                        Ext.Msg.alert('操作', "调拨成功！");
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var CancelStock = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要撤销调拨吗？', function (btn) { commandcolumn_direct_cancelstock(btn) });
            }
        }

        var commandcolumn_direct_cancelstock = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnCancelStock_Click({
                success: function (result) {
                    if (result == "false")
                        Ext.Msg.alert('操作', "操作失败！");
                    else if (result == "OK")
                        Ext.Msg.alert('操作', "撤销成功！");
                    else
                        Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var pnlListFresh = function () {
            if (App.txtBeginTime.getValue() > App.txtEndTime.getValue()) {
                Ext.Msg.alert('操作', '开始时间不能大于结束时间！');
                return false;
            }
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        var QueryEquipInfo = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenEquipCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
            }
        }

        var QueryMaterial = function (field, trigger, index) {//物料查询
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

        var QueryStorage = function (field, trigger, index) {//库房
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenStorageID.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
                    break;
            }
        }
        var QueryStoragePlace = function (field, trigger, index) {//库位
            var url = "../BasicInfo/CommonPage/QueryBasStoragePlace.aspx?StorageType=2&&StorageID=" + App.hiddenToStorageID.getValue();
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

        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {//机台代码返回值处理
            App.txtEquipCode.setValue(record.data.EquipName);
            App.txtEquipCode.getTrigger(0).show();
            App.hiddenEquipCode.setValue(record.data.EquipCode);
            App.pageToolBar.doRefresh();
        }

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            App.txtMaterName.getTrigger(0).show();
            App.txtMaterName.setValue(record.data.MaterialName);
            App.hiddenMaterCode.setValue(record.data.MaterialCode);
            App.pageToolBar.doRefresh();
        }

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            if (!App.winStorage.hidden) {
                App.txtToStorageName.getTrigger(0).show();
                App.txtToStorageName.setValue(record.data.StorageName);
                App.hiddenToStorageID.setValue(record.data.StorageID);
                App.txtToStoragePlaceName.getTrigger(0).hide();
                App.txtToStoragePlaceName.setValue("");
                App.hiddenToStoragePlaceID.setValue("");
            }
            else {
                App.txtStorageName.getTrigger(0).show();
                App.txtStorageName.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
                App.txtStoragePlaceName.getTrigger(0).hide();
                App.txtStoragePlaceName.setValue("");
                App.hiddenStoragePlaceID.setValue("");
                App.pageToolBar.doRefresh();
            }
        }

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Request = function (record) {//库位信息返回值处理
            if (!App.winStorage.hidden) {
                App.txtToStorageName.getTrigger(0).show();
                App.txtToStoragePlaceName.getTrigger(0).show();
                App.txtToStorageName.setValue(record.data.StorageName);
                App.hiddenToStorageID.setValue(record.data.StorageID);
                App.txtToStoragePlaceName.setValue(record.data.StoragePlaceName);
                App.hiddenToStoragePlaceID.setValue(record.data.StoragePlaceID);
            }
            else {
                App.txtStorageName.getTrigger(0).show();
                App.txtStoragePlaceName.getTrigger(0).show();
                App.txtStorageName.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
                App.txtStoragePlaceName.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
                App.pageToolBar.doRefresh();
            }
        }

        Ext.create("Ext.window.Window", {//机台代码查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择机台名称",
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
        Ext.create("Ext.window.Window", {//库房带窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasStorage_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryBasStorage.aspx?StorageType=2&&LastStorageFlag=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
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

        var adjustFlagChange = function (value) {
            return Ext.String.format(value == "1" ? "已调拨" : "未调拨");
        };

        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("StockFlag") == "1") {
                return "x-grid-row-collapsed";
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="rmRubberStorage" runat="server" />
        <ext:Viewport ID="vpRubberStorage" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnRubberStorage" runat="server" Region="North" Height="120">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="tbRubberStorage">
                            <Items>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <Listeners>
                                        <Click Fn="pnlListFresh" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsBegin" />
                                <ext:Button runat="server" Icon="LockEdit" Text="审核调拨" ID="Button1" Visible="false">
                                    <%--<Listeners>
                                        <Click Handler="SetStockFlag();" />
                                    </Listeners>--%>
                                    <DirectEvents>
                                        <Click OnEvent="btnSetToStorage_Click"></Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsMiddle2" />
                                <ext:Button runat="server" Icon="LockEdit" Text="调拨撤销" ID="Button2" Visible="false">
                                    <Listeners>
                                        <Click Handler="CancelStock();" />
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
                                        <ext:TextField ID="txtBarcode" runat="server" FieldLabel="条码号" LabelAlign="Right" />
                                        <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" OnDirectChange="txtQueryTime_change" LabelAlign="Right" />
                                        <ext:ComboBox ID="cbxShift" runat="server" OnDirectChange="page_change" FieldLabel="班次" LabelAlign="Right">
                                            <Items>
                                                <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                                <ext:ListItem Text="早班" Value="3"></ext:ListItem>
                                                <ext:ListItem Text="中班" Value="1"></ext:ListItem>
                                                <ext:ListItem Text="夜班" Value="2"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                        <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="库房名称" LabelAlign="Right" Flex="1" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryStorage" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" OnDirectChange="txtQueryTime_change" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                        <ext:TriggerField ID="txtStoragePlaceName" runat="server" FieldLabel="库位名称" LabelAlign="Right" Flex="1" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryStoragePlace" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:TriggerField ID="txtMaterName" runat="server" FieldLabel="物料名称" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryMaterial" />
                                            </Listeners>
                                        </ext:TriggerField>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container4" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                    <Items>
                                        <ext:TriggerField ID="txtEquipCode" runat="server" FieldLabel="机台" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryEquipInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:ComboBox ID="cbxShiftClass" runat="server" OnDirectChange="page_change" FieldLabel="班组" LabelAlign="Right">
                                            <Items>
                                                <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                                <ext:ListItem Text="甲组" Value="1"></ext:ListItem>
                                                <ext:ListItem Text="乙组" Value="2"></ext:ListItem>
                                                <ext:ListItem Text="丙组" Value="3"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                        <%--<ext:ComboBox ID="cbxAdjust" OnDirectChange="page_change" runat="server" FieldLabel="是否调拨" LabelAlign="Right">
                                            <Items>
                                                <ext:ListItem Text="全部" Value="all" AutoDataBind="true">
                                                </ext:ListItem>
                                                <ext:ListItem Text="是" Value="1">
                                                </ext:ListItem>
                                                <ext:ListItem Text="否" Value="0">
                                                </ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>--%>
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
                            <ext:Store ID="store" runat="server" PageSize="15">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model" runat="server" IDProperty="Barcode,StorageID,StoragePlaceID">
                                        <Fields>
                                            <ext:ModelField Name="StorageID" />
                                            <ext:ModelField Name="StorageName" />
                                            <ext:ModelField Name="StoragePlaceID" />
                                            <ext:ModelField Name="StoragePlaceName" />
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="ShelfBarcode" />
                                            <ext:ModelField Name="BarcodeStart" />
                                            <ext:ModelField Name="BarcodeEnd" />
                                            <ext:ModelField Name="ShelfNum" />
                                            <ext:ModelField Name="Mem_Note" />
                                            <ext:ModelField Name="PlanDate" Type="Date" />
                                            <ext:ModelField Name="ShiftID" />
                                            <ext:ModelField Name="ShiftName" />
                                            <ext:ModelField Name="ShiftClassID" />
                                            <ext:ModelField Name="ClassName" />
                                            <ext:ModelField Name="EquipCode" />
                                            <ext:ModelField Name="EquipName" />
                                            <ext:ModelField Name="MaterCode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="ValidDate" Type="Date" />
                                           
                                            <ext:ModelField Name="RealWeight" Type="Float" />
                                            <ext:ModelField Name="AdjustWeight" Type="Float" />
                                            <ext:ModelField Name="RecordDate" Type="Date" />
                                            <ext:ModelField Name="ToStorageID" />
                                            <ext:ModelField Name="ToStorageName" />
                                            <ext:ModelField Name="ToStoragePlaceID" />
                                            <ext:ModelField Name="ToStoragePlaceName" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                <ext:Column ID="Barcode" runat="server" Text="条码号" DataIndex="Barcode" Width="140" />
                                <ext:Column ID="StorageName" runat="server" Text="库房" DataIndex="StorageName" Flex="1" />
                                <ext:Column ID="StoragePlaceName" runat="server" Text="库位" DataIndex="StoragePlaceName" Flex="1" />
                                <ext:Column ID="BarcodeStart" runat="server" Text="开始车次" DataIndex="BarcodeStart" Flex="1" />
                                <ext:Column ID="BarcodeEnd" runat="server" Text="截止车次" DataIndex="BarcodeEnd" Flex="1" />
                                <ext:Column ID="EquipName" runat="server" Text="机台" DataIndex="EquipName" Flex="1" />
                                <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                <%--                <ext:DateColumn ID="PlanDate" runat="server" Text="生产日期" DataIndex="PlanDate" Flex="1" Format="yyyy-MM-dd" />--%>
                                  <ext:DateColumn ID="ADate" runat="server" Text="调拨时间" DataIndex="RecordDate" Flex="1" Format="yyyy-MM-dd hh:mm:ss" />
                                <ext:Column ID="ClassName" runat="server" Text="班组" DataIndex="ClassName" Flex="1" />
                             <%--   <ext:DateColumn ID="ValidDate" runat="server" Text="保质期" DataIndex="ValidDate" Flex="1" Format="yyyy-MM-dd" />--%>
                                <ext:Column ID="RealWeight" runat="server" Text="实际重量" DataIndex="RealWeight" Flex="1" />
                                <%--<ext:Column ID="AdjustFlag" runat="server" Text="调拨状态" DataIndex="AdjustFlag" Flex="1">
                                    <Renderer Fn="adjustFlagChange" />
                                </ext:Column>--%>
                                <ext:Column ID="AdjustWeight" runat="server" Text="调拨重量" DataIndex="AdjustWeight" Flex="1" />
                                <ext:Column ID="ToStorageName" runat="server" Text="发往库房" DataIndex="ToStorageName" Flex="1" />
                                <ext:Column ID="ToStoragePlaceName" runat="server" Text="发往库位" DataIndex="ToStoragePlaceName" Flex="1" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                                <DirectEvents>
                                    <SelectionChange OnEvent="rowSelectMuti_SelectionChange">
                                        <ExtraParams>
                                            <ext:Parameter Name="AdjustFlag" Value="selected[0].get('AdjustFlag')" Mode="Raw" />
                                            <ext:Parameter Name="Barcode" Value="selected[0].get('Barcode')" Mode="Raw" />
                                            <ext:Parameter Name="StorageID" Value="selected[0].get('StorageID')" Mode="Raw" />
                                            <ext:Parameter Name="StoragePlaceID" Value="selected[0].get('StoragePlaceID')" Mode="Raw" />
                                            <ext:Parameter Name="RealWeight" Value="selected[0].get('RealWeight')" Mode="Raw" />
                                            <ext:Parameter Name="AdjustWeight" Value="selected[0].get('AdjustWeight')" Mode="Raw" />
                                            <ext:Parameter Name="ToStorageID" Value="selected[0].get('ToStorageID')" Mode="Raw" />
                                            <ext:Parameter Name="ToStoragePlaceID" Value="selected[0].get('ToStoragePlaceID')" Mode="Raw" />
                                        </ExtraParams>
                                    </SelectionChange>
                                </DirectEvents>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        <View>
                            <ext:GridView ID="gvRows" runat="server">
                                <GetRowClass Fn="SetRowClass" />
                            </ext:GridView>
                        </View>
                        <BottomBar>
                            <ext:PagingToolbar ID="pageToolBar" runat="server">
                                <Items>
                                    <ext:Label ID="Label1" runat="server" Text="每页条数:" />
                                    <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                                    <ext:ComboBox ID="ComboBox1" runat="server" Width="80">
                                        <Items>
                                            <ext:ListItem Text="15" />
                                            <ext:ListItem Text="50" />
                                            <ext:ListItem Text="100" />
                                            <ext:ListItem Text="200" />
                                        </Items>
                                        <SelectedItems>
                                            <ext:ListItem Value="15" />
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
            <ext:Window ID="winStorage" runat="server" Icon="MonitorAdd" Closable="false" Title="请选择发往库房库位" Width="300" Height="150" 
                    Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TriggerField ID="txtToStorageName" runat="server" FieldLabel="发往库房" LabelAlign="Right" Flex="1" AllowBlank="false" Editable="false">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Fn="QueryStorage" />
                                    </Listeners>
                                </ext:TriggerField>
                                <ext:TriggerField ID="txtToStoragePlaceName" runat="server" FieldLabel="发往库位" LabelAlign="Right" Flex="1" AllowBlank="false" Editable="false">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Fn="QueryStoragePlace" />
                                    </Listeners>
                                </ext:TriggerField>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnSelectStorage}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                     <Buttons>
                        <ext:Button ID="btnSelectStorage" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <%--<DirectEvents>
                                <Click OnEvent="btnSelectStorage_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>--%>
                            <Listeners>
                                <Click Handler="SetStockFlag();" />
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
                        <Show Handler="for(var i=0;i<#{vpRubberStorage}.items.length;i++){#{vpRubberStorage}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vpRubberStorage}.items.length;i++){#{vpRubberStorage}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
            </Items>
        </ext:Viewport>
        
        <ext:Hidden ID="hiddenEquipCode" runat="server" />
        <ext:Hidden ID="hiddenMaterCode" runat="server" />
        <ext:Hidden ID="hiddenStorageID" runat="server" />
        <ext:Hidden ID="hiddenStoragePlaceID" runat="server" />
        <ext:Hidden ID="hiddenToStorageID" runat="server" />
        <ext:Hidden ID="hiddenToStoragePlaceID" runat="server" />
        <ext:Hidden ID="hiddenCheckAdjustFlag" runat="server" />
        <ext:Hidden ID="hiddenCheckBarcode" runat="server" />
        <ext:Hidden ID="hiddenCheckRealWeight" runat="server" />
        <ext:Hidden ID="hiddenCheckAdjustWeight" runat="server" />
        <ext:Hidden ID="hiddenCheckStorageID" runat="server" />
        <ext:Hidden ID="hiddenCheckStoragePlaceID" runat="server" />
        <ext:Hidden ID="hiddenCheckToStorageID" runat="server" />
        <ext:Hidden ID="hiddenCheckToStoragePlaceID" runat="server" />
    </form>
</body>
</html>
