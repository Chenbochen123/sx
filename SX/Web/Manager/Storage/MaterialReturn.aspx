<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialReturn.aspx.cs" Inherits="Manager_Storage_MaterialReturn" %>
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
            var url = "Storage/MaterialReturnInsert.aspx?BillNo=" + BillNo;
            var tabid = "Manager_Storage_MaterialReturnInsert";
            var tabp = parent.App.mainTabPanel;
            var tab = tabp.getComponent("id=" + tabid);
            if (tab) {
                tab.close();
            }
            var title;
            if (record != null)
                title = "退货单编辑";
            else
                title = "退货单录入";
            parent.addTab(tabid, title, url, true);
            parent.refresh("");
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

        //        //点击取消作废按钮
        //        var commandcolumn_direct_returncancel = function (btn, record) {
        //            if (btn != "yes") {
        //                return;
        //            }
        //            var BillNo = record.data.BillNo;
        //            App.direct.commandcolumn_direct_returncancel(BillNo, {
        //                success: function (result) {
        //                    Ext.Msg.alert('操作', result);
        //                },

        //                failure: function (errorMsg) {
        //                    Ext.Msg.alert('操作', errorMsg);
        //                }
        //            });
        //        }

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

        var commandcolumndetail_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumndetail_direct_edit(record);
            }
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定要删除此条信息吗？', function (btn) { commandcolumndetail_direct_delete(btn, record) });
            }
        }

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
            if (record.get("FiledFlag") == "1") {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
                toolbar.items.getAt(3).hide();
            }
            else if (record.get("ChkResultFlag") == "1") {
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

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            if (!App.winAdd.hidden) {
                App.txtStorageName2.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
            }
            else if (!App.winModify.hidden) {
                App.txtStorageName1.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
            }
            else {
                App.txtStorageName.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
            }
        }

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Request = function (record) {//库位信息返回值处理
            if (!App.winAdd.hidden) {
                App.txtStoragePlaceName2.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            }
            else if (!App.winModify.hidden) {
                App.txtStoragePlaceName1.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            }
            else {
                App.txtStoragePlaceName2.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            }
        }

        var Manager_BasicInfo_CommonPage_QueryFactory_Request = function (record) {
            App.txtFactoryID.getTrigger(0).show();
            App.txtFactoryID.setValue(record.data.FacName);
            App.hiddenFactoryID.setValue(record.data.ObjID);
            App.pageToolBar.doRefresh();
            App.pageToolBar.DoRefresh();
        }

        var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {//用户返回值处理
            App.txtMakerPerson.getTrigger(0).show();
            App.txtMakerPerson.setValue(record.data.UserName);
            App.hiddenMakerPerson.setValue(record.data.HRCode);
            App.pageToolBar.doRefresh();
        }

        var QueryFactory = function (field, trigger, index) {//厂家查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenFactoryID.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    App.pageToolBar.DoRefresh();
                    break;
                case 1:
                    var url = "../BasicInfo/CommonPage/QueryFactory.aspx?QueryFlag=1";
                    var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
                    if (App.Manager_BasicInfo_CommonPage_QueryFactory_Window.getBody()) {
                        App.Manager_BasicInfo_CommonPage_QueryFactory_Window.getBody().update(html);
                    } else {
                        App.Manager_BasicInfo_CommonPage_QueryFactory_Window.html = html;
                    }
                    App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
                    break;
            }
        }
        var AddStorage = function () {//库房添加
            App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
        }
        var AddStoragePlace = function () {//库位添加
            var url = "../BasicInfo/CommonPage/QueryBasStoragePlace.aspx?StorageID=" + App.hiddenStorageID.getValue();
            var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody()) {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody().update(html);
            } else {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.html = html;
            }
            App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.show();
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

        Ext.create("Ext.window.Window", {//库房带窗体
            id: "Manager_BasicInfo_CommonPage_QueryFactory_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryFactory.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择厂家名称",
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

        var commandcolumn_direct_chkresultflag = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnBatchChk_Click({
                success: function (result) {
                    if (result == "false1") {
                        Ext.Msg.alert('操作', "退货日期已经超出库房的期间，请检查！");
                        return;
                    }
                    else if (result == "false") {
                        Ext.Msg.alert('操作', "库存数量不足，请检查！");
                        return;
                    }
                    else
                        Ext.Msg.alert('操作', result);
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var SendChkResultFlag = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要审核退货吗？', function (btn) { commandcolumn_direct_chkresultflag(btn) });
            }
        }

        var commandcolumn_direct_cancelchkresult = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnCancelChk_Click({
                success: function (result) {
                    if (result == "false1") {
                        Ext.Msg.alert('操作', "退货日期已经超出库房的期间，请检查！");
                        return;
                    }
                    else if (result == "false") {
                        Ext.Msg.alert('操作', "库存数量不足，请检查！");
                        return;
                    }
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
                Ext.Msg.confirm("提示", '确定要撤销审核吗？', function (btn) { commandcolumn_direct_cancelchkresult(btn) });
            }
        }

        var resultChange = function (value) {
            return Ext.String.format(value=="1" ? "已退货" : "");
        };

        var SourcePlaceChange = function (value) {
            return Ext.String.format(value == null ? "手工输入" : value);
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmChkBill" runat="server" />
    <ext:Viewport ID="vpChkBill" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnChkBill" runat="server" Region="North" Height="90">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbChkBill">
                        <Items>
                            <ext:Button runat="server" Icon="Add" Text="添加" ID="btnAdd">
                                <Listeners>
                                    <Click Handler="commandcolumn_direct_edit(null);" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsBegin" />
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <Listeners>
                                    <Click Fn="pnlListFresh" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsMiddle" />
                            <ext:Button runat="server" Icon="LockEdit" Text="审核退货" ID="Button1">
                                <Listeners>
                                    <Click Handler="SendChkResultFlag();" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsMiddle2" />
                            <ext:Button runat="server" Icon="LockEdit" Text="撤销审核" ID="Button2">
                                <Listeners>
                                    <Click Handler="CancelChkResult();" />
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
                                    <ext:TextField ID="txtBillNo" runat="server" FieldLabel="退货单号" LabelAlign="Right" />
                                    <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" OnDirectChange="txtBeginTime_Change" LabelAlign="Right" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:TriggerField ID="txtFactoryID" runat="server" FieldLabel="返回厂家" LabelAlign="Right"
                                        Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <RemoteValidation OnValidation="CheckField" />
                                        <Listeners>
                                            <TriggerClick Fn="QueryFactory" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" OnDirectChange="txtEndTime_change" LabelAlign="Right" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".34"
                                Padding="5">
                                <Items>
                                    <ext:TriggerField ID="txtMakerPerson" runat="server" FieldLabel="制单人" LabelAlign="Right" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryUser" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <%--<ext:ComboBox ID="cbxFiledFlag" runat="server" FieldLabel="是否归档" LabelAlign="Right">
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
                            <ext:Store ID="store" runat="server" PageSize="10">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model" runat="server" IDProperty="BillNo">
                                        <Fields>
                                            <ext:ModelField Name="BillNo" />
                                            <ext:ModelField Name="ReturnDate" Type="Date" />
                                            <ext:ModelField Name="ReturnReason" />
                                            <ext:ModelField Name="ReturnFactory" />
                                            <ext:ModelField Name="FacName" />
                                            <ext:ModelField Name="UserName" />
                                            <ext:ModelField Name="StorageID" />
                                            <ext:ModelField Name="StorageName" />
                                            <ext:ModelField Name="InaccountDuration" />
                                            <ext:ModelField Name="ChkResultFlag" />
                                            <ext:ModelField Name="Remark" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                <ext:Column ID="billNo" runat="server" Text="退货单号" DataIndex="BillNo" Flex="1" />
                                <ext:Column ID="facName" runat="server" Text="返回厂家" DataIndex="FacName" Flex="1" />                                    
                                <ext:DateColumn ID="returnDate" runat="server" Text="退货日期" DataIndex="ReturnDate"
                                    Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:Column ID="returnReason" runat="server" Text="退货原因" DataIndex="ReturnReason" Flex="1" />
                                <ext:Column ID="chkResultFlag" runat="server" Text="是否退货" DataIndex="ChkResultFlag" Flex="1">
                                    <Renderer Fn="resultChange" />
                                </ext:Column>
                                <ext:Column ID="storageName" runat="server" Text="源库房" DataIndex="StorageName" Flex="1" />
                                <ext:Column ID="userName" runat="server" Text="制单人" DataIndex="UserName" Flex="1" />
                                <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                    <Commands>
                                        <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改" />
                                        <ext:CommandSeparator />
                                        <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除" />
                                        <ext:GridCommand Icon="TableEdit" CommandName="Filed" Text="归档" />
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
                                <%--<DirectEvents>
                                    <Select OnEvent="RowSelect" Buffer="250">
                                        <ExtraParams>
                                            <ext:Parameter Name="BillNo" Value="record.getId()" Mode="Raw" />
                                        </ExtraParams>
                                    </Select>
                                </DirectEvents>--%>
                                <Listeners>
                                    <Select Handler="#{storeDetail}.reload();" Buffer="250" />
                                </Listeners>
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
            <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="入库单明细数据" Height="200" Icon="Basket" Layout="Fit" Collapsible="true" 
            Split="true" MarginsSummary="0 5 5 5">
                <Items>
                    <ext:GridPanel ID="pnlDetailList" runat="server" MarginsSummary="0 5 5 5">
                        <Store>
                            <ext:Store ID="storeDetail" runat="server" PageSize="10" OnReadData="RowSelect">
                                <%-- <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindDetail" />
                                </Proxy>--%>
                                <Model>
                                    <ext:Model ID="modelDetail" runat="server" IDProperty="BillNo, Barcode">
                                        <Fields>
                                            <ext:ModelField Name="BillNo" />
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="OrderID" />
                                            <ext:ModelField Name="ProductNo" />
                                            <ext:ModelField Name="StoragePlaceID" />
                                            <ext:ModelField Name="StoragePlaceName" />
                                            <ext:ModelField Name="MaterCode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="ProcDate" Type="Date" />
                                            <ext:ModelField Name="ReturnNum" Type="Int" />
                                            <ext:ModelField Name="ReturnWeight" Type="Float" />
                                            <ext:ModelField Name="Remark" />
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
                                <ext:Column ID="OrderID" runat="server" Text="序号" DataIndex="OrderID" Flex="1" />
                                <ext:Column ID="ProductNo" runat="server" Text="批次号" DataIndex="ProductNo" Flex="1" />
                                <ext:Column ID="StoragePlaceName" runat="server" Text="源库位" DataIndex="StoragePlaceName" Flex="1" />
                                <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                <ext:DateColumn ID="ProcDate" runat="server" Text="生产日期" DataIndex="ProcDate" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:Column ID="ReturnNum" runat="server" Text="退货数量" DataIndex="ReturnNum" Flex="1" />
                                <ext:Column ID="ReturnWeight" runat="server" Text="退货重量" DataIndex="ReturnWeight" Flex="1" />
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

            <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改入库单信息"
                Width="320" Height="380" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="Form">
                <Items>
                    <ext:FormPanel ID="pnlEdit" runat="server" Flex="1" BodyPadding="5">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:TextField ID="txtBillNo1" runat="server" FieldLabel="入库单号" LabelAlign="Left"
                                ReadOnly="true" />
                            <ext:TextField ID="txtSendChkNo11" runat="server" FieldLabel="送检单号" LabelAlign="Left" ReadOnly="true">
                                <RemoteValidation OnValidation="CheckField" />
                            </ext:TextField>
                            <ext:TriggerField ID="txtStorageName1" runat="server" FieldLabel="库房名称" LabelAlign="Left"
                                Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <RemoteValidation OnValidation="CheckField" />
                                <Listeners>
                                    <TriggerClick Fn="AddStorage" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TriggerField ID="txtStoragePlaceName1" runat="server" FieldLabel="库位名称" LabelAlign="Left"
                                Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <RemoteValidation OnValidation="CheckField" />
                                <Listeners>
                                    <TriggerClick Fn="AddStoragePlace" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:DateField ID="txtInputDate1" runat="server" Format="yyyy-MM-dd" FieldLabel="入库日期" LabelAlign="Left" />
                            <ext:TextField ID="txtRemark1" runat="server" FieldLabel="备注" LabelAlign="Left" />
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{btnModify}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnModify" runat="server" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="btnModify_Click" />
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                        <DirectEvents>
                            <Click OnEvent="btnCancel_Click" />
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="for(var i=0;i<#{vpChkBill}.items.length;i++){#{vpChkBill}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpChkBill}.items.length;i++){#{vpChkBill}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>
            <%--<ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加入库单信息"
                Width="550" Height="450" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="10" Layout="Form">
                <Items>
                    <ext:FormPanel ID="pnlAdd" runat="server" Layout="ColumnLayout" AutoHeight="true" BodyPadding="10">
                        <Items>
                            <ext:Container ID="container4" runat="server" Layout="FormLayout" ColumnWidth=".5"
                                Padding="5">
                                <Items>
                                    <ext:TriggerField ID="txtStorageName2" runat="server" FieldLabel="库房名称" LabelAlign="Left"
                                        Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <RemoteValidation OnValidation="CheckField" />
                                        <Listeners>
                                            <TriggerClick Fn="AddStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:DateField ID="txtInputDate2" runat="server" Format="yyyy-MM-dd" FieldLabel="入库日期" LabelAlign="Left" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container5" runat="server" Layout="FormLayout" ColumnWidth=".5"
                                Padding="5">
                                <Items>
                                    <ext:TriggerField ID="txtStoragePlaceName2" runat="server" FieldLabel="库位名称" LabelAlign="Left"
                                        Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <RemoteValidation OnValidation="CheckField" />
                                        <Listeners>
                                            <TriggerClick Fn="AddStoragePlace" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TextField ID="txtRemark2" runat="server" FieldLabel="备注" LabelAlign="Left" />
                                </Items>
                            </ext:Container>
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{btnAddSave}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                    <ext:GridPanel ID="pnlChkBillList" runat="server" Height="280">
                        <Store>
                            <ext:Store ID="store1" runat="server" PageSize="10">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindData1" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model1" runat="server" IDProperty="BillNo">
                                        <Fields>
                                            <ext:ModelField Name="BillNo" />
                                            <ext:ModelField Name="NoticeNo" />
                                            <ext:ModelField Name="FactoryID" />
                                            <ext:ModelField Name="FacName" />
                                            <ext:ModelField Name="SendChkFlag" />
                                            <ext:ModelField Name="InStockDate" Type="Date" />
                                            <ext:ModelField Name="MakerPerson" />
                                            <ext:ModelField Name="UserName" />
                                            <ext:ModelField Name="LockedFlag" />
                                            <ext:ModelField Name="FiledFlag" />
                                            <ext:ModelField Name="Remark" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Width="35" />
                                <ext:Column ID="Column1" runat="server" Text="单据号" DataIndex="BillNo" Flex="1" />
                                <ext:Column ID="Column2" runat="server" Text="通知单号" DataIndex="NoticeNo" Flex="1" />
                                <ext:Column ID="Column3" runat="server" Text="生产厂家" DataIndex="FacName" Flex="1" />
                                <ext:Column ID="Column4" runat="server" Text="制单人" DataIndex="UserName" Flex="1" />
                                <ext:DateColumn ID="inStockDate" runat="server" Text="接收日期" DataIndex="InStockDate"
                                    Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionMulti1" runat="server" Mode="Multi">
                                <DirectEvents>
                                    <Select OnEvent="RowSelect" Buffer="250">
                                        <ExtraParams>
                                            <ext:Parameter Name="BillNo" Value="record.getId()" Mode="Raw" />
                                        </ExtraParams>
                                    </Select>
                                </DirectEvents>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar2" runat="server">
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager2" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                        <Listeners>
                            <Click Handler="AddStoreIn();" />
                        </Listeners>
                    </ext:Button>
                    <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
                        <DirectEvents>
                            <Click OnEvent="btnCancel_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="for(var i=0;i<#{vpChkBill}.items.length;i++){#{vpChkBill}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpChkBill}.items.length;i++){#{vpChkBill}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>--%>

            <ext:Hidden ID="hiddenBillNo" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenStorageID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenStoragePlaceID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenFactoryID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenMakerPerson" runat="server"></ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
