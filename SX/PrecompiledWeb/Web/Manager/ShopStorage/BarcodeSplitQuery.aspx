<%@ page language="C#" autoeventwireup="true" inherits="Manager_ShopStorage_BarcodeSplitQuery, App_Web_ampjtxsw" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>批次条码拆分情况查询</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
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

        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            App.txtStorageName.getTrigger(0).show();
            App.txtStorageName.setValue(record.data.StorageName);
            App.hiddenStorageID.setValue(record.data.StorageID);
            App.txtStoragePlaceName.setValue(record.data.StoragePlaceName);
            App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
        }

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Request = function (record) {//库位信息返回值处理
            App.txtStoragePlaceName.getTrigger(0).show();
            App.txtStorageName.setValue(record.data.StorageName);
            App.hiddenStorageID.setValue(record.data.StorageID);
            App.txtStoragePlaceName.setValue(record.data.StoragePlaceName);
            App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="rmBarcodeSplit" runat="server" />
        <ext:Viewport ID="vpPrint" runat="server" Layout="border">
            <Items>
                 <ext:Panel ID="pnPrint" runat="server" Region="North" Height="70">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="tbPrint">
                            <Items>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <DirectEvents>
                                        <Click OnEvent="btnSearch_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsBegin" />
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
                                        <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="库房名称" AllowBlank="false" LabelAlign="Right" Editable="false" IndicatorText="*" IndicatorCls="red-text">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="AddStorage" />
                                            </Listeners>
                                        </ext:TriggerField>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                        <ext:TriggerField ID="txtStoragePlaceName" runat="server" FieldLabel="库位名称" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="AddStoragePlace" />
                                            </Listeners>
                                        </ext:TriggerField>                                    
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                         <ext:TextField ID="txtBarcode" runat="server" FieldLabel="条码号" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" />
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
                        <ext:FormPanel ID="container_top1" runat="server" Layout="ColumnLayout" AutoHeight="true">
                            <Items>
                                <ext:Container ID="containerScreen" runat="server" Layout="FormLayout" ColumnWidth=".33" Padding="5">
                                    <Items>
                                        <ext:TextField ID="txtBarcode1" runat="server" FieldLabel="条码号" LabelAlign="Right" ReadOnly="true" />
                                        <ext:TextField ID="txtMaterName1" runat="server" FieldLabel="物料名称" LabelAlign="Right" ReadOnly="true" />
                                        <ext:TextField ID="txtStorageName1" runat="server" FieldLabel="库房名称" LabelAlign="Right" ReadOnly="true" />
                                        <ext:TextField ID="txtStoragePlaceName1" runat="server" FieldLabel="库位名称" LabelAlign="Right" ReadOnly="true" />
                                        <ext:TextField ID="txtStoreInWeight1" runat="server" FieldLabel="入库重量" LabelAlign="Right" ReadOnly="true" />
                                        <ext:TextField ID="txtStoreOutWeight1" runat="server" FieldLabel="消耗重量" LabelAlign="Right" ReadOnly="true" />
                                        <ext:TextField ID="txtRealWeight1" runat="server" FieldLabel="当前库存重量" LabelAlign="Right" ReadOnly="true" />
                                        <ext:TextField ID="txtChaiWeight1" runat="server" FieldLabel="已拆分重量" LabelAlign="Right" ReadOnly="true" />
                                        <ext:TextField ID="txtUnChaiWeight1" runat="server" FieldLabel="未拆分重量" LabelAlign="Right" ReadOnly="true" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                </ext:Panel>

                <ext:Hidden ID="hiddenStorageID" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenStoragePlaceID" runat="server"></ext:Hidden>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
