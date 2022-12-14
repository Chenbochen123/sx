<%@ page language="C#" autoeventwireup="true" inherits="Manager_Storage_MaterialStorageAdjusting, App_Web_p5ht2o2r" %>
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
        	background-color: #B0FFBA !important;
        }
    </style>
    <script type="text/javascript">
        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("ChkResultFlag") == "1") {
                return "x-grid-row-collapsed";
            }
        }
        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var BillNo = "";
            if (record != null)
                BillNo = record.data.BillNo;
            var url = "Storage/MaterialStorageAdjustingInsert.aspx?BillNo=" + BillNo;
            var tabid = "Manager_Storage_MaterialStorageAdjustingInsert";
            var tabp = parent.App.mainTabPanel;
            var tab = tabp.getComponent(tabid);
            if (tab) {
                tab.close();
            }
            var tabName = "";
            if (record == null)
                tabName = "调整单录入";
            else
                tabName = "调整单编辑"
            parent.addTab(tabid, tabName, url, true);
        }

        //点击删除按钮
        var commandcolumn_direct_delete = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var BillNo = record.data.BillNo;
            App.direct.commandcolumn_direct_delete(BillNo, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //点击归档按钮
        var commandcolumn_direct_filed = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var BillNo = record.data.BillNo;
            App.direct.commandcolumn_direct_filed(BillNo, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
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
            if (command.toLowerCase() == "cancel") {
                Ext.Msg.confirm("提示", '您确定要作废此条信息吗？', function (btn) { commandcolumn_direct_cancel(btn, record) });
            }
            if (command.toLowerCase() == "filed") {
                Ext.Msg.confirm("提示", '您确定要归档吗？', function (btn) { commandcolumn_direct_filed(btn, record) });
            }
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
            integerText: "此填入项格式为正整数"
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

        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
            if (record.get("ChkResultFlag") == "1") {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
            }
        };

        var startTrack = function () {
            this.checkboxes = [];
            var cb;

            Ext.select(".x-form-item", false).each(function (checkEl) {
                cb = Ext.getCmp(checkEl.dom.id.selected);
                cb.setValue(false);
                this.rowselect.push(cb);
            }, this);
        };

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            App.txtStorageName.getTrigger(0).show();
            App.txtStorageName.setValue(record.data.StorageName);
            App.hiddenStorageID.setValue(record.data.StorageID);
            App.pageToolBar.doRefresh();
        }

        var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {//用户返回值处理
            App.txtMakerPerson.getTrigger(0).show();
            App.txtMakerPerson.setValue(record.data.UserName);
            App.hiddenMakerPerson.setValue(record.data.HRCode);
            App.pageToolBar.doRefresh();
        }

        var QueryUser = function (field, trigger, index) {//人员查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenMakerPerson.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();
                    break;
            }
        }
        
        var AddStorage = function (field, trigger, index) {//库房添加
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

        Ext.create("Ext.window.Window", {//库房带窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasStorage_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryBasStorage.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择库房名称",
            modal: true
        })

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

        //点击取消作废按钮
        var commandcolumn_direct_usedflag = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnBatchUsing_Click({
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var commandcolumn_direct_sendchkresultflag = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnBatchResult_Click({
                success: function (result) {
                    if (result == "false1")
                        Ext.Msg.alert('操作', "调整日期已经超出库房期间，请检查！");
                    else if (result == "false")
                        Ext.Msg.alert('操作', "您要求的调整盘亏重量大于库存重量，请检查！");
                    else
                        Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var SetChkResultFlag = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '提交后将锁定您选择的单据，确定要提交吗？', function (btn) { commandcolumn_direct_sendchkresultflag(btn) });
            }
        }

        var chkResultChange = function (value) {
            return Ext.String.format(value=="1" ? "已审核" : "");
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmChkBill" runat="server" />
    <ext:Viewport ID="vpChkBill" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnChkBill" runat="server" Region="North" Height="95">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbChkBill">
                        <Items>
                            <ext:Button runat="server" Icon="Add" Text="添加" ID="btnAdd">
                                <Listeners>
                                    <Click Handler="commandcolumn_direct_edit(null)" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsBegin" />
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <Listeners>
                                    <Click Fn="pnlListFresh" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsMiddle" />
                            <ext:Button runat="server" Icon="LockEdit" Text="审核提交" ID="Button1">
                                <Listeners>
                                    <Click Handler="SetChkResultFlag();" />
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
                                    <ext:TextField ID="txtBillNo" runat="server" FieldLabel="调整单号" LabelAlign="Right" />
                                    <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" OnDirectChange="txtBeginTime_change" LabelAlign="Right" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="库房名称" LabelAlign="Right" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="AddStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" OnDirectChange="txtEndTime_change" LabelAlign="Right" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:ComboBox ID="cbxChkResultFlag" runat="server" FieldLabel="是否审核" OnDirectChange="cbxChkResultFlag_change" LabelAlign="Right">
                                        <SelectedItems>
                                            <ext:ListItem Value="all">
                                            </ext:ListItem>
                                        </SelectedItems>
                                        <Items>
                                            <ext:ListItem Text="全部" Value="all" AutoDataBind="true">
                                            </ext:ListItem>
                                            <ext:ListItem Text="是" Value="1">
                                            </ext:ListItem>
                                            <ext:ListItem Text="否" Value="0">
                                            </ext:ListItem>
                                        </Items>
                                    </ext:ComboBox>
                                    <ext:TriggerField ID="txtMakerPerson" runat="server" FieldLabel="制单人" LabelAlign="Right" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
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
                            <ext:Store ID="store" runat="server" PageSize="10">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model" runat="server" IDProperty="BillNo">
                                        <Fields>
                                            <ext:ModelField Name="BillNo" />
                                            <ext:ModelField Name="InventoryNo" />
                                            <ext:ModelField Name="StorageName" />
                                            <ext:ModelField Name="AdjustingDate" Type="Date" />
                                            <ext:ModelField Name="ChkResultFlag" />
                                            <ext:ModelField Name="UserName" />
                                            <ext:ModelField Name="Remark" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                <ext:Column ID="billNo" runat="server" Text="调整单号" DataIndex="BillNo" Flex="1" />
                                <ext:Column ID="InventoryNo" runat="server" Text="盘点单号" DataIndex="InventoryNo" Flex="1" />
                                <ext:Column ID="StorageName" runat="server" Text="库房名称" DataIndex="StorageName" Flex="1" />
                                <ext:DateColumn ID="AdjustingDate" runat="server" Text="调整日期" DataIndex="AdjustingDate" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                                
                                <ext:Column ID="ChkResultFlag" runat="server" Text="是否审核" DataIndex="ChkResultFlag" Flex="1">
                                    <Renderer Fn="chkResultChange" />
                                </ext:Column>
                                <ext:Column ID="userName" runat="server" Text="制单人" DataIndex="UserName" Flex="1" />
                                <%--<ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Flex="1" />--%>
                                <ext:CommandColumn ID="commandCol" runat="server" Width="220" Text="操作" Align="Center">
                                    <Commands>
                                        <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改" />
                                        <ext:CommandSeparator />
                                        <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除" />
                                        <%--<ext:GridCommand Icon="TableEdit" CommandName="Filed" Text="归档" />--%>
                                    </Commands>
                                    <PrepareToolbar Fn="prepareToolbar" />
                                    <Listeners>
                                        <Command Handler="return commandcolumn_click(command, record);" />
                                    </Listeners>
                                </ext:CommandColumn>
                            </Columns>
                        </ColumnModel>
                        <View>
                            <ext:GridView ID="gvRows" runat="server">
                                <GetRowClass Fn="SetRowClass" />
                            </ext:GridView>
                        </View>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                                <Listeners>
                                    <Select Handler="#{storeDetail}.reload();" Buffer="250" />
                                </Listeners>
                            </ext:RowSelectionModel>
                        </SelectionModel>
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
                </Items>
            </ext:Panel>
            <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="调整单明细数据" Height="200" Icon="Basket" Layout="Fit" Collapsible="true" 
            Split="true" MarginsSummary="0 5 5 5">
                <Items>
                    <ext:GridPanel ID="pnlDetailList" runat="server" MarginsSummary="0 5 5 5">
                        <Store>
                            <ext:Store ID="storeDetail" runat="server" PageSize="10" OnReadData="RowSelect">
                                <Model>
                                    <ext:Model ID="modelDetail" runat="server" IDProperty="BillNo, Barcode,OrderID">
                                        <Fields>
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="OrderID" />
                                            <ext:ModelField Name="StoragePlaceID" />
                                            <ext:ModelField Name="StoragePlaceName" />
                                            <ext:ModelField Name="MaterCode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="DecreaseOrAddFlag" />
                                            <ext:ModelField Name="AdjustingNum" Type="Int" />
                                            <ext:ModelField Name="AdjustingWeight" Type="Float" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                                <Parameters>
                                    <ext:StoreParameter Name="BillNo" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.BillNo : -1" />
                                </Parameters>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModelDetail" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />
                                <ext:Column ID="Column1" runat="server" Text="条码号" DataIndex="Barcode" Flex="1" />
                                <ext:Column ID="Column2" runat="server" Text="库位名称" DataIndex="StoragePlaceName" Flex="1" />
                                <ext:Column ID="Column3" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                <ext:Column ID="Column4" runat="server" Text="增减标志" DataIndex="DecreaseOrAddFlag" Flex="1" />
                                <ext:Column ID="Column5" runat="server" Text="调整数量" DataIndex="AdjustingNum" Flex="1" />
                                <ext:Column ID="Column6" runat="server" Text="调整重量" DataIndex="AdjustingWeight" Flex="1" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="rowSelectMutiDetail" runat="server" Mode="Multi" />
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar1" runat="server">
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

            <ext:Hidden ID="hiddenBillNo" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenStorageID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenFactoryID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenMaterCode" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenMakerPerson" runat="server"></ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>