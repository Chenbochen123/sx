<%@ page language="C#" autoeventwireup="true" inherits="Manager_ShopStorage_RubberShopAdjust, App_Web_ampjtxsw" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>生胶原材料调拨管理</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        var CancelShopAdjust = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要撤销调拨吗？', function (btn) { commandcolumn_direct_cancelAdjust(btn) });
            }
        }
        var commandcolumn_direct_cancelAdjust = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnCancelShopAdjust_Click({
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

        var QueryStorage = function (field, trigger, index) {//库房
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenStorageID.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.hiddenCurrentStorage.setValue("txtStorageName");
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
                    break;
            }
        }
        var QueryToStorage = function (field, trigger, index) {//库房
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenToStorageID.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.hiddenCurrentStorage.setValue("txtToStorageName");
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
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
        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            if (App.hiddenCurrentStorage.getValue() == "txtStorageName") {
                App.txtStorageName.getTrigger(0).show();
                App.txtStorageName.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
            }
            else if (App.hiddenCurrentStorage.getValue() == "txtToStorageName") {
                App.txtToStorageName.getTrigger(0).show();
                App.txtToStorageName.setValue(record.data.StorageName);
                App.hiddenToStorageID.setValue(record.data.StorageID);
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
        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            App.txtMaterName.getTrigger(0).show();
            App.txtMaterName.setValue(record.data.MaterialName);
            App.hiddenMaterCode.setValue(record.data.MaterialCode);
        }
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

        var QueryUser = function (field, trigger, index) {//人员查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenOperPerson.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();
                    break;
            }
        }
        Ext.create("Ext.window.Window", {//人员信息带回查询信息
            id: "Manager_BasicInfo_CommonPage_QueryBasUser_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryBasUser.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择人员",
            modal: true
        })
        var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {//用户返回值处理
            App.txtOperPerson.getTrigger(0).show();
            App.txtOperPerson.setValue(record.data.UserName);
            App.hiddenOperPerson.setValue(record.data.WorkBarcode);
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager ID="rmShopAdjust" runat="server" />
         
        <ext:Viewport ID="vpShopAdjust" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnShopAdjust" runat="server" Region="North" Height="120">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="tbShopAdjust">
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
                                        <ext:ComboBox ID="cbxChejian" runat="server" FieldLabel="车间" LabelAlign="Right">
                                            <Items>
                                                <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                                <ext:ListItem Text="M2车间" Value="2"></ext:ListItem>
                                                <ext:ListItem Text="M3车间" Value="3"></ext:ListItem>
                                                <ext:ListItem Text="M4车间" Value="4"></ext:ListItem>
                                                <ext:ListItem Text="M5车间" Value="5"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="库房名称" LabelAlign="Right" Flex="1" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryStorage" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:TextField ID="txtBarcode" runat="server" FieldLabel="条码号" LabelAlign="Right" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                        <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" LabelAlign="Right" />
                                        <ext:TriggerField ID="txtToStorageName" runat="server" FieldLabel="发往库房" LabelAlign="Right" Flex="1" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryToStorage" />
                                            </Listeners>
                                        </ext:TriggerField>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                        <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" LabelAlign="Right" />
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
                                        <ext:ComboBox ID="cbxShift" runat="server" FieldLabel="班次" LabelAlign="Right">
                                            <Items>
                                                <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                                <ext:ListItem Text="早班" Value="3"></ext:ListItem>
                                                <ext:ListItem Text="中班" Value="1"></ext:ListItem>
                                                <ext:ListItem Text="夜班" Value="2"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:TriggerField ID="txtOperPerson" runat="server" FieldLabel="调拨人" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="false" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryUser" />
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
                                                <ext:ModelField Name="OrderID" />
                                                <ext:ModelField Name="MaterCode" />
                                                <ext:ModelField Name="MaterialName" />
                                                <ext:ModelField Name="ProcDate" Type="Date" />
                                                <ext:ModelField Name="AdjustNum" />
                                                <ext:ModelField Name="AdjustWeight" Type="Float" />
                                                <ext:ModelField Name="ToStorageID" />
                                                <ext:ModelField Name="ToStorageName" />
                                                <ext:ModelField Name="ToStoragePlaceID" />
                                                <ext:ModelField Name="ToStoragePlaceName" />
                                                <ext:ModelField Name="InaccountDate" Type="Date" />
                                                <ext:ModelField Name="ShiftID" />
                                                <ext:ModelField Name="ShiftName" />
                                                <ext:ModelField Name="ShiftClassID" />
                                                <ext:ModelField Name="ClassName" />
                                                <ext:ModelField Name="OperPerson" />
                                                <ext:ModelField Name="UserName" />
                                                <ext:ModelField Name="RecordDate" Type="Date" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="colModel" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                    <ext:Column ID="StorageID" runat="server" Text="库房编号" Hidden="true" DataIndex="StorageID" />
                                    <ext:Column ID="StoragePlaceID" runat="server" Text="库位编号" Hidden="true" DataIndex="StoragePlaceID" />
                                    <ext:Column ID="Barcode" runat="server" Text="条码号" DataIndex="Barcode" Width="160" />
                                    <ext:Column ID="OrderID" runat="server" Text="序号" DataIndex="OrderID" Width="60" />
                                    <ext:Column ID="StorageName" runat="server" Text="库房" DataIndex="StorageName" Flex="1" />
                                    <ext:Column ID="ToStorageName" runat="server" Text="发往库房" DataIndex="ToStorageName" Flex="1" />
                                    <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Width="180" />
                                    <ext:Column ID="AdjustNum" runat="server" Text="调拨数量" DataIndex="AdjustNum" Flex="1" />
                                    <ext:Column ID="AdjustWeight" runat="server" Text="调拨重量" DataIndex="AdjustWeight" Flex="1" />
                                    <ext:Column ID="ShiftName" runat="server" Text="班次" DataIndex="ShiftName" Flex="1" />
                                    <ext:Column ID="ClassName" runat="server" Text="班组" DataIndex="ClassName" Flex="1" />
                                    <ext:Column ID="UserName" runat="server" Text="调拨人" DataIndex="UserName" Flex="1" />
                                    <ext:DateColumn ID="RecordDate" runat="server" Text="调拨时间" DataIndex="RecordDate" Width="150" Format="yyyy-MM-dd HH:mm:ss" />
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
        <ext:Hidden ID="hiddenStorageID" runat="server" />
        <ext:Hidden ID="hiddenToStorageID" runat="server" />
        <ext:Hidden ID="hiddenMaterCode" runat="server" />
        <ext:Hidden ID="hiddenOperPerson" runat="server" />
        <ext:Hidden ID="hiddenCurrentStorage" runat="server" />

        <ext:Hidden ID="hiddenCheckStorageID" runat="server" />
        <ext:Hidden ID="hiddenCheckStoragePlaceID" runat="server" />
        <ext:Hidden ID="hiddenCheckBarcode" runat="server" />
        <ext:Hidden ID="hiddenCheckOrderID" runat="server" />
    </form>
</body>
</html>
