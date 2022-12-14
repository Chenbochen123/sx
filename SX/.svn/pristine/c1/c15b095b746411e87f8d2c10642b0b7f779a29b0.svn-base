<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RubberOverValidQuery.aspx.cs"
    Inherits="Manager_Rubber_RubberOverValidQuery" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <style type="text/css">
        .b
        {
            color: Blue;
        }
        .r
        {
            color: Red;
        }
    </style>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        function TimeCom(dateValue) {
            var newCom = new Date(dateValue);
            this.year = newCom.getYear();
            this.month = newCom.getMonth() + 1;
            this.day = newCom.getDate();
            this.hour = newCom.getHours();
            this.minute = newCom.getMinutes();
            this.second = newCom.getSeconds();
            this.msecond = newCom.getMilliseconds();
            this.week = newCom.getDay();
        }
        var getRowClass = function () {

        }

        var pnlListFresh = function () {
            
            //判断日期之间间隔是否大于30天
            App.store.currentPage = 1;
            App.store.reload();
            return false;
        }
        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            var name = App.hldname.value;
            if (name == "txtStorageName") {
                App.txtStorageName.getTrigger(0).show();
                App.txtStorageName.setValue(record.data.StorageName);
                App.txtStoragePlaceName.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
                App.hiddenStorageID.setValue(record.data.StorageID);
            } else if (name == "txtToStorageName") {
                App.txtToStorageName.getTrigger(0).show();
                App.txtToStorageName.setValue(record.data.StorageName);
                App.txtToStoragePlaceName.setValue(record.data.StoragePlaceName);
                App.hiddenToStoragePlaceID.setValue(record.data.StoragePlaceID);
                App.hiddenToStorageID.setValue(record.data.StorageID);
            }

        }
        var Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Request = function (record) {//库位信息返回值处理
            var name = App.hldname.value;

            if (name == "txtStoragePlaceName") {
                App.txtStoragePlaceName.getTrigger(0).show();
                App.txtStorageName.setValue(record.data.StorageName);
                App.txtStoragePlaceName.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
                App.hiddenStorageID.setValue(record.data.StorageID);
            } else if (name == "txtToStoragePlaceName") {
                App.txtToStoragePlaceName.getTrigger(0).show();
                App.txtToStorageName.setValue(record.data.StorageName);
                App.txtToStoragePlaceName.setValue(record.data.StoragePlaceName);
                App.hiddenToStoragePlaceID.setValue(record.data.StoragePlaceID);
                App.hiddenToStorageID.setValue(record.data.StorageID);
            }
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
                    App.store.reload();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
                    break;
            }
        }
        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {//机台代码返回值处理
            App.txtEquipName.setValue(record.data.EquipName);
            App.txtEquipName.getTrigger(0).show();
            App.hiddenEquipCode.setValue(record.data.EquipCode);
            //App.pageToolBar.doRefresh();
        }
        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            App.txtMaterName.getTrigger(0).show();
            App.txtMaterName.setValue(record.data.MaterialName);
            App.hiddenMaterCode.setValue(record.data.MaterialCode);
            //App.pageToolBar.doRefresh();
        }
        var QueryStorage = function (field, trigger, index) {//库房添加
            App.hldname.setValue(field.name);
            switch (index) {
                case 0:
                    var name = App.hldname.value;
                    if (name == "txtStorageName") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.txtStoragePlaceName.setValue("");
                        App.txtStoragePlaceName.getTrigger(0).hide();
                        App.hiddenStorageID.setValue("");
                        App.hiddenStoragePlaceID.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    else if (name == "txtToStorageName") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.txtToStorageName.setValue("");
                        App.hiddenToStorageID.setValue("");
                        App.hiddenToStoragePlaceID.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
                    break;
            }
        }

        var OperTypeChange = function (value) {
            if (value == "001")
                return Ext.String.format("生产");
            else if (value == "002")
                return Ext.String.format("调拨");
            else if (value == "003")
                return Ext.String.format("外发");
            else if (value == "004")
                return Ext.String.format("外供");
            else if (value == "005")
                return Ext.String.format("外送");
            else if (value == "006")
                return Ext.String.format("消耗");
            else if (value == "007")
                return Ext.String.format("退回");
            else if (value == "008")
                return Ext.String.format("返回");
            else if (value == "009")
                return Ext.String.format("废品");
            else if (value == "010")
                return Ext.String.format("架子拆分");
            else if (value == "011")
                return Ext.String.format("盘点");
        }
        var QueryStoragePlace = function (field, trigger, index) {//库位添加
            App.hldname.setValue(field.name);
            var url = "../BasicInfo/CommonPage/QueryBasStoragePlace.aspx?StorageID=" + App.hiddenStorageID.getValue();
            var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody()) {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody().update(html);
            } else {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.html = html;
            }
            switch (index) {
                case 0:

                    if (field.name == "txtStoragePlaceName") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.hiddenStoragePlaceID.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");

                    } else if (field.name == "txtToStoragePlaceName") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.hiddenToStoragePlaceID.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    break;
                case 1:

                    App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.show();
                    break;
            }
        }
        Ext.create("Ext.window.Window", {//机台带窗体
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
            html: "<iframe src='../BasicInfo/CommonPage/QueryMaterial.aspx?minMajorTypeID=2' width=100% height=100% scrolling=no  frameborder=0></iframe>",
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
            html: "<iframe src='../BasicInfo/CommonPage/QueryBasStorage.aspx?StorageType=all&&LastStorageFlag=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
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
        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("状态") == "已超期") {
                return "r";
            }
            if (record.get("状态") == "即将超期") {
                return "b";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button"
        OnClick="btnExportSubmit_Click" />
    <ext:ResourceManager ID="rmReport" runat="server" />
    <ext:Viewport ID="vpReport" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnReportTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbReport">
                        <Items>
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <Listeners>
                                    <Click Fn="pnlListFresh" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsBegin" />
                            <ext:Button runat="server" Icon="ApplicationEdit" Text="批量处理" ID="btn_add">
                                <ToolTips>
                                    <ext:ToolTip ID="ttAdd" runat="server" Html="点击进行处理" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="btn_deal_Click">
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                <Listeners>
                                    <Click Handler="$('#btnExportSubmit').click();">
                                    </Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsEnd" />
                            <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                            <ext:ToolbarFill ID="tfEnd" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel ID="pnlReportByClass" runat="server" AutoHeight="true">
                        <Items>
                            <ext:FormPanel ID="FormPanel1" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth=".2">
                                        <Items>
                                            <ext:DateField ID="txtBeginTime" runat="server"  FieldLabel="开始时间" LabelAlign="Right" />
                                            <ext:DateField ID="txtEndTime" runat="server"  FieldLabel="结束时间" LabelAlign="Right"/>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="ctnBeginTime" runat="server" Layout="FormLayout" ColumnWidth=".2">
                                        <Items>
                                            <ext:ComboBox ID="cbxChejian" runat="server" FieldLabel="车间" LabelAlign="Right">
                                                <Items>
                                                    <ext:ListItem Text="M2车间" Value="2">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M3车间" Value="3">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M4车间" Value="4">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M5车间" Value="5">
                                                    </ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                           <ext:TextField ID="txtBarcode" runat="server" FieldLabel="条码号" LabelAlign="Right" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="ctnEndTime" runat="server" Layout="FormLayout" ColumnWidth=".2">
                                        <Items>
                                            <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="库房名称" LabelAlign="Right"
                                                Flex="1" Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryStorage" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:TextField ID="txtShlefBarCode" runat="server" FieldLabel="车条码号" LabelAlign="Right" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="ctnEquipName" runat="server" Layout="FormLayout" ColumnWidth=".2">
                                        <Items>
                                            <ext:TriggerField ID="txtStoragePlaceName" runat="server" FieldLabel="库位名称" LabelAlign="Right"
                                                Flex="1" Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryStoragePlace" />
                                                </Listeners>
                                            </ext:TriggerField>
                                           <ext:NumberField ID="txtLimit" runat="server" EmptyText="24" FieldLabel="预警设置(小时)"
                                                LabelAlign="Right" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container2" runat="server" Layout="FormLayout" ColumnWidth=".2">
                                         <Items>
                                             <ext:TriggerField ID="txtMaterName" runat="server" FieldLabel="物料名称" LabelAlign="Right"
                                                 Editable="false">
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
                                </Items>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:GridPanel ID="pnlList" runat="server" Cls="x-grid-custom" Region="Center">
                <Store>
                    <ext:Store ID="store" runat="server" AutoLoad="false">
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="model" runat="server">
                                <Fields>
                                    <ext:ModelField Name="状态" />
                                    <ext:ModelField Name="条码" />
                                    <ext:ModelField Name="StorageID" />
                                    <ext:ModelField Name="StoragePlaceID" />
                                    <ext:ModelField Name="车条码" />
                                    <ext:ModelField Name="仓库" />
                                    <ext:ModelField Name="库位" />
                                    <ext:ModelField Name="车间" />
                                    <ext:ModelField Name="车次" />
                                    <ext:ModelField Name="物料" />
                                    <ext:ModelField Name="数量" />
                                    <ext:ModelField Name="重量" />
                                    <ext:ModelField Name="记录时间" Type="Date" />
                                    <ext:ModelField Name="过期时间" Type="Date" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="colModel" runat="server">
                    <Columns>
                        <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                        <ext:Column ID="col1" runat="server" Text="状态" DataIndex="状态" Flex="1" />
                        <ext:Column ID="col2" runat="server" Text="条码" DataIndex="条码" Flex="1" />
                         <ext:Column ID="Column7" runat="server" Text="StorageID" Hidden="true" DataIndex="StorageID" Flex="1" />
                          <ext:Column ID="Column8" runat="server" Text="StoragePlaceID" Hidden="true" DataIndex="StoragePlaceID" Flex="1" />
                        
                        <ext:DateColumn ID="Column6" runat="server" Text="生产时间" DataIndex="记录时间" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                        <ext:Column ID="col4" runat="server" Text="仓库" DataIndex="仓库" Flex="1" />
                        <ext:Column ID="col5" runat="server" Text="库位" DataIndex="库位" Flex="1" />
                        <ext:DateColumn ID="Column5" runat="server" Text="过期时间" DataIndex="过期时间" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                        <ext:Column ID="col6" runat="server" Text="车次" DataIndex="车次" Flex="1" />
                        <ext:Column ID="Column1" runat="server" Text="物料" DataIndex="物料" Flex="1" />
                        <ext:Column ID="Column3" runat="server" Text="重量" DataIndex="重量" Flex="1" />
                        <ext:Column ID="Column4" runat="server" Text="数量" DataIndex="数量" Flex="1" />
                    </Columns>
                </ColumnModel>
                <View>
                    <ext:GridView ID="GridView1" runat="server">
                        <GetRowClass Fn="SetRowClass" />
                    </ext:GridView>
                </View>
                <SelectionModel>
                    <ext:CheckboxSelectionModel ID="rowSelectMuti" runat="server" Mode="Multi">
                        <Listeners>
                            <Select Handler="#{storeDetail}.reload();" Buffer="250" />
                        </Listeners>
                    </ext:CheckboxSelectionModel>
                </SelectionModel>
                <BottomBar>
                    <ext:PagingToolbar ID="pageToolBar" runat="server">
                        <Items>
                            <ext:Label ID="Label1" runat="server" Text="每页条数:" />
                            <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                            <ext:ComboBox ID="ComboBox1" runat="server" Width="80">
                                <Items>
                                    <ext:ListItem Text="10" />
                                    <ext:ListItem Text="50" />
                                    <ext:ListItem Text="100" />
                                    <ext:ListItem Text="200" />
                                </Items>
                                <SelectedItems>
                                    <ext:ListItem Value="10" />
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
            <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="库存明细数据" Height="200" Icon="Basket" Layout="Fit" Collapsible="true" 
            Split="true" MarginsSummary="0 5 5 5">
                <Items>
                    <ext:GridPanel ID="pnlDetailList" runat="server" MarginsSummary="0 5 5 5">
                        <Store>
                            <ext:Store ID="storeDetail" runat="server" PageSize="10" OnReadData="RowSelect">
                                <Model>
                                    <ext:Model ID="modelDetail" runat="server" IDProperty="Barcode, OrderID">
                                        <Fields>
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="Banci" />
                                            <ext:ModelField Name="OrderID" />
                                            <ext:ModelField Name="RecordDate" Type="Date" />
                                            <ext:ModelField Name="ShiftName" />
                                            <ext:ModelField Name="ClassName" />
                                            <ext:ModelField Name="Num" />
                                            <ext:ModelField Name="Weight" Type="Float" />
                                            <ext:ModelField Name="OperType" />
                                            <ext:ModelField Name="EquipName" />
                                            <ext:ModelField Name="StorageName" />
                                            <ext:ModelField Name="StoragePlaceName" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                                <Parameters>
                                    <ext:StoreParameter Name="Barcode" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.条码 : -1" />
                                    <ext:StoreParameter Name="StorageID" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.StorageID : -1" />
                                    <ext:StoreParameter Name="StoragePlaceID" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.StoragePlaceID : -1" />
                                </Parameters>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModelDetail" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />
                                <ext:DateColumn ID="RecordDate1" runat="server" Text="执行日期" DataIndex="RecordDate" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:Column ID="ShiftName1" runat="server" Text="班次班组" DataIndex="Banci" Flex="1" />
                                <ext:Column ID="Column2" runat="server" Text="机台" DataIndex="EquipName" Flex="1" />
                                <ext:Column ID="Num" runat="server" Text="数量" DataIndex="Num" Flex="1" />
                                <ext:Column ID="Weight1" runat="server" Text="重量" DataIndex="Weight" Flex="1" />
                                <ext:Column ID="StorageName1" runat="server" Text="发往库房" DataIndex="StorageName" Flex="1" />
                                <ext:Column ID="StoragePlaceName1" runat="server" Text="发往名称" DataIndex="StoragePlaceName" Flex="1" />
                                <ext:Column ID="OperType1" runat="server" Text="操作类型" DataIndex="OperType" Flex="1">
                                    <Renderer Fn="OperTypeChange" />
                                </ext:Column>
                             </Columns>
                        </ColumnModel>
                        <%--<Listeners>
                            <CellDblClick Fn="gridPanelCellDblClick" />
                        </Listeners>--%>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar1" runat="server">
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
                                            <ext:ListItem Value="10" />
                                        </SelectedItems>
                                        <Listeners>
                                            <Select Handler="#{pnlDetailList}.store.pageSize = parseInt(this.getValue(), 10); #{PagingToolbar1}.doRefresh(); return false;" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager1" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    <ext:Window ID="winAdd" runat="server" Icon="MonitorEdit" Closable="false" Title="超期物料处理"
        Width="300" Height="350" Resizable="false" Hidden="true" Modal="true" BodyStyle="background-color:#fff;"
        BodyPadding="5" Layout="Form">
        <Items>
            <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5">
                <FieldDefaults>
                    <CustomConfig>
                        <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                        <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                    </CustomConfig>
                </FieldDefaults>
                <Items>
                    <ext:Container ID="Container3" runat="server" Layout="ColumnLayout" AutoHeight="true">
                        <Items>
                            <ext:TextField ID="dealno" runat="server" FieldLabel="条码数量" LabelAlign="right"
                                Enabled="true" MaxLength="50" AllowBlank="false" Editable="false" IndicatorText="*" Padding="5"
                                IndicatorCls="red-text" />
                            <ext:DateField ID="dealdate" runat="server" FieldLabel="延期时间" FormatControlForValue="yyyy-MM-dd" Format="yyyy-MM-dd" Editable="false" AllowBlank="false"
                                IndicatorText="*" IndicatorCls="red-text" LabelAlign="Right" Padding="5">
                            </ext:DateField>
                            <ext:TextArea ID="dealremark" LabelAlign="Right" runat="server" FieldLabel="备注" Padding="5" />
                        </Items>
                    </ext:Container>
                </Items>
                <Listeners>
                    <ValidityChange Handler="#{btnAddSave}.setDisabled(!valid);" />
                </Listeners>
            </ext:FormPanel>
        </Items>
        <Buttons>
            <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                <DirectEvents>
                    <Click OnEvent="BtnAddSave_Click">
                        <EventMask ShowMask="true" Msg="处理中..." MinDelay="50" />
                        <ExtraParams>
                            <ext:Parameter Name="Values" Value="Ext.encode(#{pnlList}.getRowsValues({selectedOnly:true}))"
                                Mode="Raw" />
                        </ExtraParams>
                    </Click>
                </DirectEvents>
            </ext:Button>
            <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
                <DirectEvents>
                    <Click OnEvent="BtnCancel_Click">
                    </Click>
                </DirectEvents>
            </ext:Button>
        </Buttons>
        <Listeners>
            <Show Handler="for(var i=0;i<#{vpReport}.items.length;i++){#{vpReport}.getComponent(i).disable(true);}" />
            <Hide Handler="for(var i=0;i<#{vpReport}.items.length;i++){#{vpReport}.getComponent(i).enable(true);}" />
        </Listeners>
    </ext:Window>
    <ext:Hidden ID="hiddenEquipCode" runat="server" />
    <ext:Hidden ID="hiddenMaterCode" runat="server" />
    <ext:Hidden ID="hiddenStorageID" runat="server">
    </ext:Hidden>
    <ext:Hidden ID="hiddenStoragePlaceID" runat="server">
    </ext:Hidden>
    <ext:Hidden ID="hiddenToStorageID" runat="server">
    </ext:Hidden>
    <ext:Hidden ID="hiddenToStoragePlaceID" runat="server">
    </ext:Hidden>
    <ext:Hidden ID="hldname" runat="server">
    </ext:Hidden>
    </form>
</body>
</html>
