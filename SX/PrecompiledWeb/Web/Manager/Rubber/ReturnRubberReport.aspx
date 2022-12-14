<%@ page language="C#" autoeventwireup="true" inherits="Manager_Rubber_ReturnRubberReport, App_Web_dobmrtlx" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell
        {
        	background-color: #99FF44 !important;
        }
    </style>
    <script type="text/javascript">

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


        var AddStoragePlace = function (field, trigger, index) {//库位添加
            var url = "../BasicInfo/CommonPage/QueryBasStoragePlace.aspx?StorageID=" + App.hiddenStorageID.getValue();
            var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody()) {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody().update(html);
            } else {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.html = html;
            }

            App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.show();
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
            
                App.txtStorageName1.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
                App.txtStoragePlaceName1.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            
        }

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Request = function (record) {//库位信息返回值处理
           
                App.txtStorageName1.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
                App.txtStoragePlaceName1.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            
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

        var stockInChange = function (value) {
            return Ext.String.format(value == "1" ? "已返回" : "未返回");
        };

        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("StockInSign") == "1") {
                return "x-grid-row-collapsed";
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager ID="rmReturnRubber" runat="server" />
        <ext:Viewport ID="vpReturnRubber" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnReturnRubber" runat="server" Region="North" Height="90">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="tbReturnRubber">
                            <Items>
                               
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <Listeners>
                                        <Click Fn="pnlListFresh" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsMiddle" />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
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
                                        <ext:ComboBox ID="cbxChejian" runat="server" OnDirectChange="cbxChejian_change" FieldLabel="分厂名称" LabelAlign="Right">
                                            <Items>
                                                <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                                <ext:ListItem Text="二厂" Value="6"></ext:ListItem>
                                                <ext:ListItem Text="三厂" Value="7"></ext:ListItem>
                                                <ext:ListItem Text="四厂" Value="8"></ext:ListItem>
                                                <ext:ListItem Text="五厂" Value="9"></ext:ListItem>
                                                <ext:ListItem Text="六厂" Value="10"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="cbxEquip" Editable="false" runat="server" OnDirectChange="cbxChejian_change" FieldLabel="机台" LabelAlign="Right" />
                                        <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
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
                                        <ext:ComboBox ID="cbxShiftClass" runat="server" OnDirectChange="cbxShiftClass_change" FieldLabel="班组" LabelAlign="Right">
                                            <Items>
                                                <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                                <ext:ListItem Text="甲组" Value="1"></ext:ListItem>
                                                <ext:ListItem Text="乙组" Value="2"></ext:ListItem>
                                                <ext:ListItem Text="丙组" Value="3"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container4" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                    <Items>
                                        <ext:TextField ID="txtBarcode" runat="server" FieldLabel="条码号" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                            </Items>
                           
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
                                    <ext:Model ID="model" runat="server" IDProperty="Barcode">
                                        <Fields>
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="ShelfBarcode" />
                                            <ext:ModelField Name="BarcodeStart" />
                                            <ext:ModelField Name="BarcodeEnd" />
                                            <ext:ModelField Name="PlanDate" Type="Date" />
                                            <ext:ModelField Name="EquipCode" />
                                            <ext:ModelField Name="MadeLine" />
                                            <ext:ModelField Name="ShiftID" />
                                            <ext:ModelField Name="ShiftName" />
                                            <ext:ModelField Name="ShiftClassID" />
                                            <ext:ModelField Name="ClassName" />
                                            <ext:ModelField Name="MaterCode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="ReturnWeight" Type="Float" />
                                            <ext:ModelField Name="RealWeight" Type="Float" />
                                            <ext:ModelField Name="OperDate" Type="Date" />
                                            <ext:ModelField Name="BackFlag" />
                                            <ext:ModelField Name="CustCode" />
                                            <ext:ModelField Name="DepName" />
                                            <ext:ModelField Name="OperPerson" />
                                            <ext:ModelField Name="UserName" />
                                            <ext:ModelField Name="ReturnReason" />
                                            <ext:ModelField Name="prodDate" Type="Date" />
                                            <ext:ModelField Name="StockInSign" />
                                            <ext:ModelField Name="StockNo" />
                                            <ext:ModelField Name="Mem_Note" />
                                            <ext:ModelField Name="ValidDate" Type="Date" />
                                            <ext:ModelField Name="ScanPerson" />
                                            <ext:ModelField Name="StorageName" />
                                            <ext:ModelField Name="StoragePlaceName" />
                                            <ext:ModelField Name="WorkShopName" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                <ext:Column ID="WorkShopName" runat="server" Text="分厂" DataIndex="WorkShopName" Flex="1" />
                                <ext:Column ID="Barcode" runat="server" Text="条码号" DataIndex="Barcode" Width="140" />
                                <ext:Column ID="BarcodeStart" runat="server" Text="开始车次" DataIndex="BarcodeStart" Flex="1" />
                                <ext:Column ID="BarcodeEnd" runat="server" Text="截止车次" DataIndex="BarcodeEnd" Flex="1" />
                                <ext:Column ID="MadeLine" runat="server" Text="机台" DataIndex="MadeLine" Width="50" />
                                <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Width="60" />
                                <ext:DateColumn ID="PlanDate" runat="server" Text="生产日期" DataIndex="PlanDate" Width="80" Format="yyyy-MM-dd" />
                                <ext:Column ID="ClassName" runat="server" Text="班组" DataIndex="ClassName" Width="40" />
                                <ext:DateColumn ID="ValidDate" runat="server" Text="保质期" DataIndex="ValidDate" Flex="1" Format="yyyy-MM-dd" />
                                <ext:Column ID="ReturnWeight" runat="server" Text="返回重量" DataIndex="ReturnWeight" Flex="1" />
                                <ext:Column ID="UserName" runat="server" Text="主机手" DataIndex="UserName" Flex="1" />
                                <ext:Column ID="StockInSign" runat="server" Text="返回状态" DataIndex="StockInSign" Flex="1">
                                    <Renderer Fn="stockInChange" />
                                </ext:Column>
                                <ext:Column ID="ScanPerson" runat="server" Text="扫描人" DataIndex="ScanPerson" Flex="1" />
                                <ext:Column ID="StorageName" runat="server" Text="库房" DataIndex="StorageName" Flex="1" />
                                <ext:Column ID="StoragePlaceName" runat="server" Text="库位" DataIndex="StoragePlaceName" Flex="1" />
                               
                            </Columns>
                        </ColumnModel>
                        

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
          

            
            </Items>
        </ext:Viewport>
        
        <ext:Hidden ID="hiddenEquipCode" runat="server" />
        <ext:Hidden ID="hiddenMaterCode" runat="server" />
        <ext:Hidden ID="hiddenStorageID" runat="server" />
        <ext:Hidden ID="hiddenStoragePlaceID" runat="server" />
        <ext:Hidden ID="hiddenToStorageID" runat="server" />
        <ext:Hidden ID="hiddenToStoragePlaceID" runat="server" />
        <ext:Hidden ID="hiddenStockFlag" runat="server" />
        <ext:Hidden ID="hiddenCheckBarcode" runat="server" />
    </form>
</body>
</html>

