<%@ page language="C#" autoeventwireup="true" inherits="Manager_Rubber_RubberStoreOutToSAP, App_Web_dobmrtlx" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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

        var pnlListFresh = function () {
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
            commandcolumn_click_confirm(command, record);
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
            //alert(record.data.EquipName);
            App.txtEquip.setValue(record.data.EquipName);
            App.txtEquip.getTrigger(0).show();
            App.hiddenEquipCode.setValue(record.data.EquipCode);
        }

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            App.txtMaterName.getTrigger(0).show();
            App.txtMaterName.setValue(record.data.MaterialName);
            App.hiddenMaterCode.setValue(record.data.MaterialCode);
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

        var stockInChange = function (value) {
            return Ext.String.format(value == "1" ? "已返回" : "未返回");
        };

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager ID="rmReturnRubber" runat="server" />
        <ext:Viewport ID="vpReturnRubber" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnReturnRubber" runat="server" Region="North" Height="120">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="tbReturnRubber">
                            <Items>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <Listeners>
                                        <Click Fn="pnlListFresh" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsBegin" />
                                <ext:Button runat="server" Icon="LockEdit" Text="上传" ID="Button1">
                                    <DirectEvents>
                                        <Click OnEvent="btnUpload_Click">
                                            <EventMask ShowMask="true" Msg="上传中..." MinDelay="50"></EventMask>
                                        </Click>
                                    </DirectEvents>
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
                                <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="cbxChejian" runat="server" FieldLabel="车间" LabelAlign="Right" Editable="false">
                                            <Items>
                                                <%--<ext:ListItem Text="全部" Value="all"></ext:ListItem>--%>
                                                <ext:ListItem Text="M2车间" Value="2"></ext:ListItem>
                                                <ext:ListItem Text="M3车间" Value="3"></ext:ListItem>
                                                <ext:ListItem Text="M4车间" Value="4"></ext:ListItem>
                                                <ext:ListItem Text="M5车间" Value="5"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cbxStockType" runat="server" FieldLabel="上传类型" LabelAlign="Right" Editable="false">
                                            <Items>
                                                <ext:ListItem Text="报产" Value="1"></ext:ListItem>
                                                <ext:ListItem Text="冲产" Value="-1"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:TextField ID="txtObjID" runat="server" FieldLabel="序号" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                        <ext:DateField ID="txtPlanDate" runat="server" FieldLabel="生产日期" LabelAlign="Right" Editable="false" />
                                        <ext:TriggerField ID="txtEquip" runat="server" FieldLabel="机台" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryEquipInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".34"
                                    Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="cbxShiftID" runat="server" FieldLabel="班次" LabelAlign="Right" Editable="false">
                                            <Items>
                                                <%--<ext:ListItem Text="全部" Value="all"></ext:ListItem>--%>
                                                <ext:ListItem Text="中班" Value="1"></ext:ListItem>
                                                <ext:ListItem Text="夜班" Value="2"></ext:ListItem>
                                                <ext:ListItem Text="早班" Value="3"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
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
                <ext:Panel ID="Panel2" runat="server" Region="Center" Frame="true" Layout="Fit" MarginsSummary="0 5 0 5">
                <Items>
                    <ext:GridPanel ID="pnlList" runat="server">
                        <Store>
                            <ext:Store ID="store" runat="server" PageSize="1000">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model" runat="server" IDProperty="Barcode">
                                        <Fields>
                                            <ext:ModelField Name="StorageID" />
                                            <ext:ModelField Name="StorageName" />
                                            <ext:ModelField Name="ERPCode" />
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="PlanDate" />
                                            <ext:ModelField Name="ShiftID" />
                                            <ext:ModelField Name="ShiftName" />
                                            <ext:ModelField Name="EquipCode" />
                                            <ext:ModelField Name="EquipName" />
                                            <ext:ModelField Name="MaterCode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="ShiftNum" />
                                            <ext:ModelField Name="TotalWeight" />
                                            <ext:ModelField Name="OperPerson" />
                                            <ext:ModelField Name="UserName" />
                                            <ext:ModelField Name="SAPVersionID" />
                                            <ext:ModelField Name="SAPMaterialShortCode" />
                                            <ext:ModelField Name="StockType" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                <%--<ext:Column ID="StorageName" runat="server" Text="库房" DataIndex="StorageName" Flex="1" />--%>
                                <ext:Column ID="Barcode" runat="server" Text="条码号" DataIndex="Barcode" Flex="1" />
                                <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                <ext:Column ID="ShiftNum" runat="server" Text="车数" DataIndex="ShiftNum" Flex="1" />
                                <ext:Column ID="TotalWeight" runat="server" Text="重量" DataIndex="TotalWeight" Flex="1" />
                                <ext:Column ID="PlanDate" runat="server" Text="生产日期" DataIndex="PlanDate" Flex="1" />
                                <ext:Column ID="ShiftName" runat="server" Text="班次" DataIndex="ShiftName" Flex="1" />
                                <ext:Column ID="UserName" runat="server" Text="主机手" DataIndex="UserName" Flex="1" />
                                <ext:Column ID="SAPMaterialShortCode" runat="server" Text="SAP物料编号" DataIndex="SAPMaterialShortCode" Flex="1" />
                                <ext:Column ID="SAPVersionID" runat="server" Text="物料版本" DataIndex="SAPVersionID" Flex="1" />
                                <ext:Column ID="EquipName" runat="server" Text="机台" DataIndex="EquipName" Flex="1" />
                                <%--<ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
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
                                </ext:CommandColumn>--%>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                                <DirectEvents>
                                    <SelectionChange OnEvent="rowSelectMuti_SelectionChange">
                                        <ExtraParams>
                                            <ext:Parameter Name="StockType" Value="selected[0].get('StockType')" Mode="Raw" />
                                            <ext:Parameter Name="Barcode" Value="selected[0].get('Barcode')" Mode="Raw" />
                                        </ExtraParams>
                                    </SelectionChange>
                                </DirectEvents>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="pageToolBar" runat="server">
                                <%--<Items>
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
                                </Items>--%>
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
        <ext:Hidden ID="hiddenStockType" runat="server" />
        <ext:Hidden ID="hiddenBarcode" runat="server" />
    </form>
</body>
</html>
