<%@ page language="C#" autoeventwireup="true" inherits="Manager_Rubber_Report_RubberAdjustReport, App_Web_wycfotos" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
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



        var pnlListFresh = function () {
            if (App.txtBeginTime.getValue() > App.txtEndTime.getValue()) {
                Ext.Msg.alert('操作', '开始时间不能大于结束时间！');
                return false;
            }
            App.store.currentPage = 1;
           
            return false;
        }

        var QueryEquipInfo = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenEquipCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                   
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
                   
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
                    break;
            }
        }
        var QueryStoragePlace = function (field, trigger, index) {//库位
            var url = "../../BasicInfo/CommonPage/QueryBasStoragePlace.aspx?StorageID=" + App.hiddenToStorageID.getValue();
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
           
        }

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            App.txtMaterName.getTrigger(0).show();
            App.txtMaterName.setValue(record.data.MaterialName);
            App.hiddenMaterCode.setValue(record.data.MaterialCode);
           
        }

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
           
                App.txtStorageName.getTrigger(0).show();
                App.txtStorageName.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
                App.txtStoragePlaceName.getTrigger(0).hide();
                App.txtStoragePlaceName.setValue("");
                App.hiddenStoragePlaceID.setValue("");
               
            
        }

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Request = function (record) {//库位信息返回值处理
            
                App.txtStorageName.getTrigger(0).show();
                App.txtStoragePlaceName.getTrigger(0).show();
                App.txtStorageName.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
                App.txtStoragePlaceName.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
               
            
        }

        Ext.create("Ext.window.Window", {//机台代码查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
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
            html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
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
            html: "<iframe src='../../BasicInfo/CommonPage/QueryBasStorage.aspx?LastStorageFlag=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
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
                             <ext:Button runat="server" ID="btnSearch" Text="查询" Icon="Magnifier">
                                <DirectEvents>
                                    <Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsBegin" />
                            <ext:Button runat="server" ID="btnExport" Text="导出" Icon="PageExcel">
                                <DirectEvents>
                                    <Click IsUpload="true" OnEvent="btnExport_Click">
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="ToolbarSeparator1" />
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
                                    <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" 
                                        LabelAlign="Right" />
                                    <ext:ComboBox ID="cbxShift" runat="server" FieldLabel="班次"
                                        LabelAlign="Right">
                                        <Items>
                                            <ext:ListItem Text="全部" Value="all">
                                            </ext:ListItem>
                                            <ext:ListItem Text="早班" Value="3">
                                            </ext:ListItem>
                                            <ext:ListItem Text="中班" Value="1">
                                            </ext:ListItem>
                                            <ext:ListItem Text="夜班" Value="2">
                                            </ext:ListItem>
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                Padding="5">
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
                                    <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" 
                                        LabelAlign="Right" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                Padding="5">
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
                        <Listeners>
                            <ValidityChange Handler="#{btnSearch}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="Panel3" Region="Center" AutoScroll="true">
                <Content>
                    <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" Font-Bold="False"
                        Width="112cm" Height="87cm" Zoom="1" Padding="3, 3, 3, 3" ToolbarColor="Lavender"
                        PrintInPdf="True" Layers="False" PdfEmbeddingFonts="false" ShowExports="false"
                        ShowRefreshButton="false" ShowToolbar="false" BorderColor="White" />
                </Content>
            </ext:Panel>
        </Items>
    </ext:Viewport>
  
    <ext:Hidden ID="hiddenEquipCode" runat="server" />
    <ext:Hidden ID="hiddenMaterCode" runat="server" />
    <ext:Hidden ID="hiddenStorageID" runat="server" />
    <ext:Hidden ID="hiddenStoragePlaceID" runat="server" />
    <ext:Hidden ID="hiddenToStorageID" runat="server" />
    <ext:Hidden ID="hiddenToStorageCheckID" runat="server" />
    <ext:Hidden ID="hiddenToStoragePlaceID" runat="server" />
    <ext:Hidden ID="hiddenStockFlag" runat="server" />
    <ext:Hidden ID="hiddenCheckBarcode" runat="server" />
    <ext:Hidden ID="hiddenCheckStorageID" runat="server" />
    <ext:Hidden ID="hiddenCheckStoragePlaceID" runat="server" />
    <ext:Hidden ID="hldname" runat="server">
    </ext:Hidden>
    <ext:Hidden ID="hiddenMakerPerson" runat="server">
    </ext:Hidden>
    </form>
</body>
</html>