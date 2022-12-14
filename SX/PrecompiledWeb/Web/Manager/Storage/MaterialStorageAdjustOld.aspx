<%@ page language="C#" autoeventwireup="true" inherits="Manager_Storage_MaterialStorageAdjustOld, App_Web_p5ht2o2r" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript">
        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var BillNo = "";
            if (record != null)
                BillNo = record.data.BillNo;
            var url = "Storage/MaterialStorageAdjustInsert.aspx?BillNo=" + BillNo;
            var tabid = "Manager_Storage_MaterialStorageAdjustInsert";
            var tabp = parent.App.mainTabPanel;
            var tab = tabp.getComponent("id=" + tabid);
            if (tab) {
                tab.close();
            }
            var tabName = "";
            if (record == null)
                tabName = "领料单录入";
            else
                tabName = "领料单编辑"
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
                toolbar.items.getAt(3).hide();
            }
            else if (record.get("SendChkFlag") == "1") {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
            }
            else {
                toolbar.items.getAt(3).hide();
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
            if (!App.winAddDetail.hidden) {
                App.txtMaterialName3.setValue(record.data.MaterialName);
                App.hiddenMaterCode.setValue(record.data.MaterialCode);
            }
            else if (!App.winModifyDetail.hidden) {
                //                App.hiddenBillNo.setValue(record.data.BillNo);
                //                App.hiddenFactoryID.setValue(record.data.FactoryID);
            }
        }

        var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {//用户返回值处理
            App.txtMakerPerson.getTrigger(0).show();
            App.txtMakerPerson.setValue(record.data.UserName);
            App.hiddenMakerPerson.setValue(record.data.HRCode);
            App.pageToolBar.doRefresh();
        }

        var QueryFactory = function () {//厂家查询
            App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
        }
        var AddFactory = function () {//厂家添加
            App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
        }
        var EditFactory = function () {//厂家修改
            App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
        }
        var AddMaterial = function () {//物料添加
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
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

        Ext.create("Ext.window.Window", {//生产厂家带窗体
            id: "Manager_BasicInfo_CommonPage_QueryFactory_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryFactory.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择生产厂家",
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

        var commandcolumn_direct_sendlockedflag = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnBatchLocked_Click({
                success: function (result) {
                    if (result == "false")
                        Ext.Msg.alert('操作', "该单据已经审核领料！");
                    else if (result == "false1")
                        Ext.Msg.alert('操作', "您要求的领料数量大于库存数量，请检查！");
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
                Ext.Msg.confirm("提示", '确定要审核领料吗？', function (btn) { commandcolumn_direct_sendlockedflag(btn) });
            }
        }

        var commandcolumn_direct_cancellocked = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnCancelLocked_Click({
                success: function (result) {
                    if (result == "false")
                        Ext.Msg.alert('操作', "该单据还未审核，不能撤销！");
                    else if (result == "false1")
                        Ext.Msg.alert('操作', "目标库房库存数量不足，不能撤销！");
                    else
                        Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var CancelChkResult = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要撤销审核吗？', function (btn) { commandcolumn_direct_cancellocked(btn) });
            }
        }

        var chkResultChange = function (value) {
            return Ext.String.format(value == "1" ? "已审核" : "");
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmChkBill" runat="server" />
    <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
    <ext:Viewport ID="vpChkBill" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnChkBill" runat="server" Region="North" Height="120">
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
                            <ext:Button runat="server" Icon="LockEdit" Text="审核领料" ID="btnLock">
                                <Listeners>
                                    <Click Handler="SetChkResultFlag();" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsMiddle2" />
                            <ext:Button runat="server" Icon="LockEdit" Text="撤销领料" ID="btnUnLock">
                                <Listeners>
                                    <Click Handler="CancelChkResult();" />
                                </Listeners>
                            </ext:Button>
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
                                    <ext:TextField ID="txtBillNo" runat="server" FieldLabel="领料单号" LabelAlign="Right" />
                                    <ext:ComboBox ID="cbxChkResultFlag" runat="server" FieldLabel="是否领料" OnDirectChange="cbxChkResultFlag_change" LabelAlign="Right">
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
                                     <ext:TextField ID="TextBarcode" runat="server" FieldLabel="条码号" LabelAlign="Right" />
                                   
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" OnDirectChange="txtBeginTime_change" LabelAlign="Right" />
                                    <ext:TriggerField ID="txtMakerPerson" runat="server" FieldLabel="制单人" LabelAlign="Right" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryUser" />
                                        </Listeners>
                                    </ext:TriggerField>
                                       <ext:TextField ID="TextMaterial" runat="server" FieldLabel="物料" LabelAlign="Right" />
                               
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".34"
                                Padding="5">
                                <Items>
                                    <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" OnDirectChange="txtEndTime_change" LabelAlign="Right" />
                                    <ext:TextField ID="txtSAPBillNo" runat="server" FieldLabel="SAP单号" LabelAlign="Right" />
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
                                            <ext:ModelField Name="AdjustDate" Type="Date" />
                                            <ext:ModelField Name="SourceStorageName" />
                                            <ext:ModelField Name="TargetStorageName" />
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
                                <ext:Column ID="billNo" runat="server" Text="领料单号" DataIndex="BillNo" Flex="1" />
                                <ext:DateColumn ID="inStockDate" runat="server" Text="领料日期" DataIndex="AdjustDate"
                                    Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:Column ID="SourceStorageName" runat="server" Text="源库房" DataIndex="SourceStorageName" Flex="1" />
                                <ext:Column ID="TargetStorageName" runat="server" Text="目的库房" DataIndex="TargetStorageName" Flex="1" />
                                <ext:Column ID="ChkResultFlag" runat="server" Text="是否审核" DataIndex="ChkResultFlag" Flex="1">
                                    <Renderer Fn="chkResultChange" />
                                </ext:Column>
                                <%--<ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Flex="1" />--%>
                                <ext:Column ID="userName" runat="server" Text="制单人" DataIndex="UserName" Flex="1" />
                                <ext:CommandColumn ID="commandCol" runat="server" Width="220" Text="操作" Align="Center">
                                    <Commands>
                                        <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改" />
                                        <ext:CommandSeparator />
                                        <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除" />
                                        <ext:GridCommand Icon="TableEdit" CommandName="Filed" Text="归档" />
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
            <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="领料单明细数据" Height="200" Icon="Basket" Layout="Fit" Collapsible="true" 
            Split="true" MarginsSummary="0 5 5 5">
                <Items>
                    <ext:GridPanel ID="pnlDetailList" runat="server" MarginsSummary="0 5 5 5">
                        <Store>
                            <ext:Store ID="storeDetail" runat="server" PageSize="10" OnReadData="RowSelect">
                                <Model>
                                    <ext:Model ID="modelDetail" runat="server" IDProperty="BillNo, Barcode">
                                        <Fields>
                                            <ext:ModelField Name="BillNo" />
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="ProductNo" />
                                            <ext:ModelField Name="SourceStoragePlaceName" />
                                            <ext:ModelField Name="TargetStoragePlaceName" />
                                            <ext:ModelField Name="MaterCode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="ProcDate" Type="Date" />
                                            <ext:ModelField Name="AdjustNum" Type="Int" />
                                            <ext:ModelField Name="AdjustWeight" Type="Float" />
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
                                <ext:Column ID="Barcode" runat="server" Text="条码号" DataIndex="Barcode" Flex="1" />
                                <ext:Column ID="SourceStoragePlaceName" runat="server" Text="源库位" DataIndex="SourceStoragePlaceName" Flex="1" />
                                <ext:Column ID="TargetStoragePlaceName" runat="server" Text="目的库位" DataIndex="TargetStoragePlaceName" Flex="1" />
                                <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                <ext:DateColumn ID="ProcDate" runat="server" Text="生产日期" DataIndex="ProcDate" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:Column ID="AdjustNum" runat="server" Text="领料数量" DataIndex="AdjustNum" Flex="1" />
                                <ext:Column ID="AdjustWeight" runat="server" Text="领料重量" DataIndex="AdjustWeight" Flex="1" />
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
            <ext:Hidden ID="hiddenFactoryID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenMaterCode" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenMakerPerson" runat="server"></ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>