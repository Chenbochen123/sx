<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RubberInventory.aspx.cs" Inherits="Manager_Rubber_RubberInventory" %>
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
        var SetReturnFlag = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要审核返回吗？', function (btn) { commandcolumn_direct_setreturnflag(btn) });
            }
        }

        var commandcolumn_direct_setreturnflag = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnBatchReturn_Click({
                success: function (result) {
                    if (result == "false")
                        Ext.Msg.alert('操作', "操作失败！");
                    else if (result == "OK")
                        Ext.Msg.alert('操作', "返回成功！");
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var CancelReturn = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要撤销返回吗？', function (btn) { commandcolumn_direct_cancelreturn(btn) });
            }
        }

        var commandcolumn_direct_cancelreturn = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnCancelReturn_Click({
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

        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
            if (record.get("StockInSign") == "1") {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
                toolbar.items.getAt(3).hide();
            }
        }

        //根据按钮类别进行删除和编辑操作
        var commandcolumn_click = function (command, record) {
            //commandcolumn_click_confirm(command, record);
            return false;
        };

        //区分删除操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
            }
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定要删除此条信息吗？', function (btn) { commandcolumn_direct_delete(btn, record) });
            }
            return false;
        };

        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var barcode = record.data.Barcode;
            App.direct.commandcolumn_direct_edit(barcode, {
                success: function (result) {
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //点击删除按钮
        var commandcolumn_direct_delete = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var barcode = record.data.Barcode;
            App.direct.commandcolumn_direct_delete(barcode, {
                success: function (result) {
                    if (result == "false") {
                        Ext.Msg.alert('操作', "删除失败，存在多个条码，请检查！");
                    }
                    else
                        Ext.Msg.alert('操作', result);
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
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

        var AddEquipInfo = function () {
            App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
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

        var AddMaterial = function () {
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
        }

        var AddStorage = function () {//库房添加
            App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
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
            if (!App.winAdd.hidden) {
                App.txtEquipCode1.setValue(record.data.EquipName);
                App.hiddenEquipCode.setValue(record.data.EquipCode);
            }
            else if (!App.winModify.hidden) {
                App.txtEquipCode2.setValue(record.data.EquipName);
                App.hiddenEquipCode.setValue(record.data.EquipCode);
            }
            else {
                App.txtEquipCode.setValue(record.data.EquipName);
                App.txtEquipCode.getTrigger(0).show();
                App.hiddenEquipCode.setValue(record.data.EquipCode);
                App.pageToolBar.doRefresh();
            }
        }

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            if (!App.winAdd.hidden) {
                App.txtMaterName1.setValue(record.data.MaterialName);
                App.hiddenMaterCode.setValue(record.data.MaterialCode);
            }
            else if (!App.winModify.hidden) {
                App.txtMaterName2.setValue(record.data.MaterialName);
                App.hiddenMaterCode.setValue(record.data.MaterialCode);
            }
            else {
                App.txtMaterName.getTrigger(0).show();
                App.txtMaterName.setValue(record.data.MaterialName);
                App.hiddenMaterCode.setValue(record.data.MaterialCode);
                App.pageToolBar.doRefresh();
            }
        }

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            if (!App.winAdd.hidden) {
                App.txtStorageName1.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
            }
            else if (!App.winModify.hidden) {
                App.txtStorageName2.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
            }
            else if (!App.winStorage.hidden) {
                App.txtToStorageName.setValue(record.data.StorageName);
                App.hiddenToStorageID.setValue(record.data.StorageID);
            }
            else {
                App.txtStorageName1.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
                App.txtStoragePlaceName1.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            }
        }

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Request = function (record) {//库位信息返回值处理
            if (!App.winAdd.hidden) {
                App.txtStoragePlaceName1.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            }
            else if (!App.winModify.hidden) {
                App.txtStoragePlaceName2.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            }
            else if (!App.winStorage.hidden) {
                App.txtToStoragePlaceName.setValue(record.data.StoragePlaceName);
                App.hiddenToStoragePlaceID.setValue(record.data.StoragePlaceID);
            }
            else {
                App.txtStorageName1.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
                App.txtStoragePlaceName1.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
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

        var ProfitLossChange = function (value) {
            if (value == "0")
                return Ext.String.format("正常");
            if (value == "1")
                return Ext.String.format("盘盈");
            if (value == "2")
                return Ext.String.format("盘亏");
        };

        var PanCunTypeChange = function (value) {
            if (value == "0")
                return Ext.String.format("全部盘点");
            if (value == "1")
                return Ext.String.format("加硫盘点");
            if (value == "2")
                return Ext.String.format("无硫盘点");
        };

        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("ProfitLossFlag") == "1") {
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
                                <ext:Button runat="server" Icon="Add" Text="添加" ID="btnAdd" Hidden="true">
                                    <DirectEvents>
                                        <Click OnEvent="btnAdd_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsBegin" Hidden="true" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <Listeners>
                                        <Click Fn="pnlListFresh" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsMiddle" />
                                <ext:Button runat="server" Icon="LockEdit" Text="数据调整" ID="Button1">
                                    <%--<Listeners>
                                        <Click Handler="SetReturnFlag();" />
                                    </Listeners>--%>
                                    <DirectEvents>
                                        <%--<Click OnEvent="btnSetToStorage_Click"></Click>--%>
                                    </DirectEvents>
                                </ext:Button>
                                <%--<ext:ToolbarSeparator ID="tsMiddle2" />
                                <ext:Button runat="server" Icon="LockEdit" Text="撤销返回" ID="Button2">
                                    <Listeners>
                                        <Click Handler="CancelReturn();" />
                                    </Listeners>
                                </ext:Button>--%>
                                <ext:ToolbarSeparator ID="tsMiddle3" />
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
                                <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="cbxChejian" runat="server" OnDirectChange="cbxChejian_change" FieldLabel="车间名称" LabelAlign="Right" Editable="false">
                                            <Items>
                                                <ext:ListItem Text="M2车间" Value="008"></ext:ListItem>
                                                <ext:ListItem Text="M3车间" Value="009"></ext:ListItem>
                                                <ext:ListItem Text="M4车间" Value="010"></ext:ListItem>
                                                <ext:ListItem Text="M5车间" Value="011"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" LabelAlign="Right" Editable="false"/>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="cbxEndDate" Editable="false" runat="server" OnDirectChange="cbxEndDate_change" FieldLabel="盘点截止时间" LabelAlign="Right" />
                                        <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".34"
                                    Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="cbxIsHaveSulf" runat="server" FieldLabel="加硫/无硫" LabelAlign="Right" Editable="false">
                                            <Items>
                                                <ext:ListItem Text="全部" Value="0"></ext:ListItem>
                                                <ext:ListItem Text="加硫" Value="1"></ext:ListItem>
                                                <ext:ListItem Text="无硫" Value="2"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cbxProfitLoss" runat="server" FieldLabel="盘盈/亏" LabelAlign="Right" Editable="false">
                                            <Items>
                                                <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                                <ext:ListItem Text="正常" Value="0"></ext:ListItem>
                                                <ext:ListItem Text="盘盈" Value="1"></ext:ListItem>
                                                <ext:ListItem Text="盘亏" Value="2"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
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
                            <ext:Store ID="store" runat="server" PageSize="15" AutoLoad="false">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model" runat="server" IDProperty="Barcode">
                                        <Fields>
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="LLBarCode" />
                                            <ext:ModelField Name="MaterCode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="RealWeight" />
                                            <ext:ModelField Name="OldWeight" />
                                            <ext:ModelField Name="ProfitLossFlag" />
                                            <ext:ModelField Name="OperTime" />
                                            <ext:ModelField Name="UserName" />
                                            <ext:ModelField Name="EndDate" />
                                            <ext:ModelField Name="PanCunType" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                <ext:Column ID="Barcode" runat="server" Text="条码号" DataIndex="Barcode" Width="140" />
                                <ext:Column ID="LLBarCode" runat="server" Text="玲珑条码号" DataIndex="LLBarCode" Width="180" />
                                <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Width="60" />
                                <ext:Column ID="RealWeight" runat="server" Text="盘点重量" DataIndex="RealWeight" Flex="1" />
                                <ext:Column ID="OldWeight" runat="server" Text="生产重量" DataIndex="OldWeight" Flex="1" />
                                <ext:Column ID="ProfitLossFlag" runat="server" Text="盘盈/亏" DataIndex="ProfitLossFlag" Flex="1">
                                    <Renderer Fn="ProfitLossChange" />
                                </ext:Column>
                                <ext:Column ID="OperTime" runat="server" Text="扫描时间" DataIndex="OperTime" Width="140" />
                                <ext:Column ID="UserName" runat="server" Text="扫描人" DataIndex="UserName" Flex="1" />
                                <ext:Column ID="EndDate" runat="server" Text="盘点截止时间" DataIndex="EndDate" Width="140" />
                                <ext:Column ID="PanCunType" runat="server" Text="盘点类型" DataIndex="PanCunType" Flex="1">
                                    <Renderer Fn="PanCunTypeChange" />
                                </ext:Column>
                                <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                    <Commands>
                                        <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改" />
                                        <ext:CommandSeparator />
                                        <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除" />
                                        <ext:CommandSeparator />
                                    </Commands>
                                    <PrepareToolbar Fn="prepareToolbar" />
                                    <Listeners>
                                        <Command Handler="return commandcolumn_click(command, record);" />
                                    </Listeners>
                                </ext:CommandColumn>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                                <DirectEvents>
                                    <SelectionChange OnEvent="rowSelectMuti_SelectionChange">
                                        <ExtraParams>
                                            <ext:Parameter Name="Barcode" Value="selected[0].get('Barcode')" Mode="Raw" />
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
            <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加返回胶信息" Width="520" Height="330" 
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
                                <ext:Container ID="Container5" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:TextField ID="txtBarcode1" runat="server" FieldLabel="条码号" LabelAlign="Right" Flex="1" AllowBlank="false" Width="240" />
                                        <ext:TriggerField ID="txtEquipCode1" runat="server" FieldLabel="机台" LabelAlign="Right" Flex="1" Editable="false" AllowBlank="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="AddEquipInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container6" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:NumberField ID="txtBarcodeStart1" runat="server" FieldLabel="起始车次" LabelAlign="Right" Flex="1" AllowBlank="false" AllowDecimals="false" MaxValue="10" />
                                        <ext:NumberField ID="txtBarcodeEnd1" runat="server" FieldLabel="截止车次" LabelAlign="Right" Flex="1" AllowBlank="false" AllowDecimals="false" MaxValue="10" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container7" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:ComboBox ID="cbxShiftID1" runat="server" FieldLabel="班次" LabelAlign="Right" Editable="false" AllowBlank="false" Flex="1">
                                            <SelectedItems>
                                                <ext:ListItem Value="3"></ext:ListItem>
                                            </SelectedItems>
                                            <Items>
                                                <ext:ListItem Text="早" Value="3"></ext:ListItem>
                                                <ext:ListItem Text="中" Value="1"></ext:ListItem>
                                                <ext:ListItem Text="夜" Value="2"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cbxShiftClassID1" runat="server" FieldLabel="班组" LabelAlign="Right" Editable="false" AllowBlank="false" Flex="1">
                                            <SelectedItems>
                                                <ext:ListItem Value="1"></ext:ListItem>
                                            </SelectedItems>
                                            <Items>
                                                <ext:ListItem Text="甲" Value="1"></ext:ListItem>
                                                <ext:ListItem Text="乙" Value="2"></ext:ListItem>
                                                <ext:ListItem Text="丙" Value="3"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container8" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:TriggerField ID="txtMaterName1" runat="server" FieldLabel="物料名称" LabelAlign="Right" AllowBlank="false" Flex="1"
                                            Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="AddMaterial" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:DateField ID="txtPlanDate1" runat="server" FieldLabel="生产日期" LabelAlign="Right" AllowBlank="false" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container9" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:NumberField ID="txtRealWeight1" runat="server" FieldLabel="实际重量" LabelAlign="Right" AllowBlank="false" DecimalPrecision="3" Flex="1" />
                                        <ext:NumberField ID="txtReturnWeight1" runat="server" FieldLabel="返回重量" LabelAlign="Right" AllowBlank="false" DecimalPrecision="3" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container10" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:TriggerField ID="txtStorageName1" runat="server" FieldLabel="库房名称" LabelAlign="Right" Flex="1" AllowBlank="false"
                                            Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="AddStorage" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:TriggerField ID="txtStoragePlaceName1" runat="server" FieldLabel="库位名称" LabelAlign="Right" Flex="1" AllowBlank="false"
                                            Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="AddStoragePlace" />
                                            </Listeners>
                                        </ext:TriggerField>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container11" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:TextField ID="txtReturnReason1" runat="server" FieldLabel="返回原因" LabelAlign="Right" Flex="1" AllowBlank="false" />
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
                                <Click OnEvent="btnAddSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vpReturnRubber}.items.length;i++){#{vpReturnRubber}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vpReturnRubber}.items.length;i++){#{vpReturnRubber}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>

                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改返回胶信息" Width="520" Height="330" 
                    Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:FormPanel ID="FormPanel1" runat="server" BodyPadding="5">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Container ID="Container12" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:TextField ID="txtBarcode2" runat="server" FieldLabel="条码号" LabelAlign="Right" Flex="1" ReadOnly="true" Width="240" />
                                        <ext:TriggerField ID="txtEquipCode2" runat="server" FieldLabel="机台" LabelAlign="Right" ReadOnly="true" Flex="1" Editable="false" AllowBlank="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="AddEquipInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container13" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:NumberField ID="txtBarcodeStart2" runat="server" FieldLabel="起始车次" LabelAlign="Right" Flex="1" AllowBlank="false" AllowDecimals="false" MaxValue="10" />
                                        <ext:NumberField ID="txtBarcodeEnd2" runat="server" FieldLabel="截止车次" LabelAlign="Right" Flex="1" AllowBlank="false" AllowDecimals="false" MaxValue="10" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container14" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:ComboBox ID="cbxShiftID2" runat="server" FieldLabel="班次" LabelAlign="Right" ReadOnly="true" Editable="false" AllowBlank="false" Flex="1">
                                            <Items>
                                                <ext:ListItem Text="早" Value="3"></ext:ListItem>
                                                <ext:ListItem Text="中" Value="1"></ext:ListItem>
                                                <ext:ListItem Text="夜" Value="2"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cbxShiftClassID2" runat="server" FieldLabel="班组" LabelAlign="Right" ReadOnly="true" Editable="false" AllowBlank="false" Flex="1">
                                            <Items>
                                                <ext:ListItem Text="甲" Value="1"></ext:ListItem>
                                                <ext:ListItem Text="乙" Value="2"></ext:ListItem>
                                                <ext:ListItem Text="丙" Value="3"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container15" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:TriggerField ID="txtMaterName2" runat="server" FieldLabel="物料名称" LabelAlign="Right" ReadOnly="true" AllowBlank="false" Flex="1"
                                            Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="AddMaterial" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:DateField ID="txtPlanDate2" runat="server" FieldLabel="生产日期" LabelAlign="Right" ReadOnly="true" AllowBlank="false" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container16" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:NumberField ID="txtRealWeight2" runat="server" FieldLabel="实际重量" LabelAlign="Right" AllowBlank="false" DecimalPrecision="3" Flex="1" />
                                        <ext:NumberField ID="txtReturnWeight2" runat="server" FieldLabel="返回重量" LabelAlign="Right" AllowBlank="false" DecimalPrecision="3" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container17" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:TriggerField ID="txtStorageName2" runat="server" FieldLabel="库房名称" LabelAlign="Right" Flex="1" AllowBlank="false"
                                            Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="AddStorage" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:TriggerField ID="txtStoragePlaceName2" runat="server" FieldLabel="库位名称" LabelAlign="Right" Flex="1" AllowBlank="false"
                                            Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="AddStoragePlace" />
                                            </Listeners>
                                        </ext:TriggerField>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container18" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>
                                        <ext:TextField ID="txtReturnReason2" runat="server" FieldLabel="返回原因" LabelAlign="Right" Flex="1" AllowBlank="false" />
                                    </Items>
                                </ext:Container>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnModify}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                     <Buttons>
                        <ext:Button ID="btnModify" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <DirectEvents>
                                <Click OnEvent="btnModify_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vpReturnRubber}.items.length;i++){#{vpReturnRubber}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vpReturnRubber}.items.length;i++){#{vpReturnRubber}.getComponent(i).enable(true);}" />
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
        <ext:Hidden ID="hiddenStockFlag" runat="server" />
        <ext:Hidden ID="hiddenCheckBarcode" runat="server" />
    </form>
</body>
</html>
