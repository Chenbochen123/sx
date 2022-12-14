<%@ page language="C#" autoeventwireup="true" inherits="Manager_ShopStorage_ShopDayStore, App_Web_ampjtxsw" %>
<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>车间原料日常结存</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript">
        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var FID = record.data.FID;
            var diffWeight = record.data.DiffWeight;
            App.direct.commandcolumn_direct_edit(FID, diffWeight, {
                success: function (result) {
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "reject") {
                record.reject();
            }
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
            }
            return false;
        };

        var auditFlagChange = function (value) {
            return Ext.String.format(value=="1" ? "已审核" : "未审核");
        };

//        var colorChange = function (value) {
//            if (value.toString().indexOf("-") >= 0)
//                return Ext.String.format('<div style="color:red;font-weight:bolder;">{0}</div>', value);
//            else if (value > "0")
//                return Ext.String.format('<div style="color:green;font-weight:bolder;">{0}</div>', value);
//            else
//                return Ext.String.format('<div style="color:black;font-weight:normal;">{0}</div>', value);
//        };

        var commandcolumn_direct_lockflag = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnAudit_Click({
                success: function (result) {
                    if (result == "false") {
                        Ext.Msg.alert('操作', "没有可审核的数据！");
                        return;
                    }
                    else
                        Ext.Msg.alert('操作', "审核成功！");
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var commandcolumn_direct_unlockflag = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnCancelAudit_Click({
                success: function (result) {
                    if (result == "false") {
                        Ext.Msg.alert('操作', "没有可撤销审核的数据！");
                        return;
                    }
                    else
                        Ext.Msg.alert('操作', "撤销成功！");
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var LockFlag = function () {
            Ext.Msg.confirm("提示", '确定要审核' + App.txtPlanDate.getValue().toLocaleString().substring(0, 11) + '的结存吗？', function (btn) { commandcolumn_direct_lockflag(btn) });
        }

        var UnLockFlag = function () {
            Ext.Msg.confirm("提示", '确定要撤销审核' + App.txtPlanDate.getValue().toLocaleString().substring(0, 11) + '的结存吗？', function (btn) { commandcolumn_direct_unlockflag(btn) });
        }

        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
            if (record.get("AuditFlag") == "1") {
                toolbar.items.getAt(0).hide();
            }
        };

        var edit2 = function (editor, e) {
            if (e.record.data.LastWeight + e.record.data.InWeight - e.record.data.OutWeight + e.value < 0) {
                Ext.Msg.alert("提示", "您的调整数使当前结存变为负数了，请重新设置！");
                e.record.reject();
            }
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
            integerText: "此填入项格式为正整数",
            decimal: function (val, field) {
                if (!val) {
                    return true;
                }
                try {
                    if (/^[\d.\d]+$/.test(val)) {
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
            decimalText: "此填入项格式为浮点数"
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
    <ext:ResourceManager ID="rmShopStore" runat="server" />
    <ext:Viewport ID="vpShopStore" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnShopStoreTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbShopStore">
                        <Items>
                            <ext:ToolbarSeparator ID="tsBegin" />
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
                            <%--<ext:Button runat="server" Icon="LockEdit" Text="审核" ID="btnLock">
                                <Listeners>
                                    <Click Handler="LockFlag();" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="ToolbarSeparator1" />
                            <ext:Button runat="server" Icon="LockEdit" Text="撤销审核" ID="Button1">
                                <Listeners>
                                    <Click Handler="UnLockFlag();" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsEnd" />--%>
                            <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                            <ext:ToolbarFill ID="tfEnd" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel ID="pnlStorageQuery" runat="server" AutoHeight="true">
                        <Items>
                            <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                        Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cbxWorkShopCode" runat="server" FieldLabel="车间" LabelAlign="Right" Editable="false">
                                                <SelectedItems>
                                                    <ext:ListItem Value="2"></ext:ListItem>
                                                </SelectedItems>
                                                <Items>
                                                    <ext:ListItem Text="M2车间" Value="2" AutoDataBind="true"></ext:ListItem>
                                                    <ext:ListItem Text="M3车间" Value="3"></ext:ListItem>
                                                    <ext:ListItem Text="M4车间" Value="4"></ext:ListItem>
                                                    <ext:ListItem Text="M5车间" Value="5"></ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                        <Items>
                                            <ext:DateField ID="txtPlanDate" runat="server" Editable="false" FieldLabel="结存日期" LabelAlign="Right" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cbxShiftID" runat="server" FieldLabel="班次" LabelAlign="Right" Editable="false">
                                                <SelectedItems>
                                                    <ext:ListItem Value="3"></ext:ListItem>
                                                </SelectedItems>
                                                <Items>
                                                    <ext:ListItem Text="早" Value="3"></ext:ListItem>
                                                    <ext:ListItem Text="中" Value="1"></ext:ListItem>
                                                    <ext:ListItem Text="夜" Value="2"></ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container4" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cbxMaterialype" runat="server" Editable="false" FieldLabel="统计分类" LabelAlign="Right">
                                                <SelectedItems>
                                                    <ext:ListItem Value="all">
                                                    </ext:ListItem>
                                                </SelectedItems>
                                                <Items>
                                                    <ext:ListItem Text="全部" Value="all" AutoDataBind="true"></ext:ListItem>
                                                    <ext:ListItem Text="生胶类" Value="1"></ext:ListItem>
                                                    <ext:ListItem Text="粉料类" Value="2"></ext:ListItem>
                                                    <ext:ListItem Text="炭黑类" Value="3"></ext:ListItem>
                                                    <ext:ListItem Text="油类" Value="4"></ext:ListItem>
                                                    <ext:ListItem Text="其它类" Value="5"></ext:ListItem>
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
                </Items>
            </ext:Panel>

            <ext:GridPanel ID="pnlList" runat="server" Cls="x-grid-custom" Region="Center">
                <Store>
                    <ext:Store ID="store" runat="server" PageSize="15"> 
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="model" runat="server" IDProperty="FID">
                                <Fields>
                                    <ext:ModelField Name="FID" />
                                    <ext:ModelField Name="PlanDate" Type="Date" />
                                    <ext:ModelField Name="MaterCode" />
                                    <ext:ModelField Name="MaterialName" />
                                    <ext:ModelField Name="LastWeight" Type="Float" />
                                    <ext:ModelField Name="InWeight" Type="Float" />
                                    <ext:ModelField Name="JieBaoWeight" Type="Float" />
                                    <ext:ModelField Name="OutWeight" Type="Float" />
                                    <ext:ModelField Name="StoreWeight" Type="Float" />
                                    <ext:ModelField Name="CheckWeight" Type="Float" />
                                    <ext:ModelField Name="DiffWeight" />
                                    <ext:ModelField Name="AuditFlag" />
                                    <ext:ModelField Name="WorkShopName" />
                                    <ext:ModelField Name="ShiftName" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="colModel" runat="server">
                    <Columns>
                        <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                        <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                        <ext:Column ID="LastWeight" runat="server" Text="前存重量" DataIndex="LastWeight" Flex="1" />
                        <ext:Column ID="InWeight" runat="server" Text="领入重量" DataIndex="InWeight" Flex="1" />
                        <ext:Column ID="OutWeight" runat="server" Text="消耗重量" DataIndex="OutWeight" Flex="1" />
                        <%--<ext:Column ID="StoreWeight" runat="server" Text="结存重量" DataIndex="StoreWeight" Flex="1" />--%>
                        <ext:Column ID="DiffWeight" runat="server" Text="调整重量" DataIndex="DiffWeight" Flex="1">
                            <%--<Renderer Fn="colorChange" />--%>
                            <Editor>
                                <ext:NumberField ID="NumberField" runat="server" />
                            </Editor>
                        </ext:Column>
                        <ext:CommandColumn ID="CommandColumn2" runat="server" Width="50">
                            <Commands>
                                <ext:GridCommand Text="取消" ToolTip-Text="取消修改" CommandName="reject" Icon="ArrowUndo" />
                            </Commands>
                            <PrepareToolbar Handler="toolbar.items.get(0).setVisible(record.dirty);" />
                            <Listeners>
                                <Command Handler="return commandcolumn_click_confirm(command, record);" />
                            </Listeners>
                        </ext:CommandColumn>
                        <%--<ext:Column ID="AuditFlag" runat="server" Text="审核标志" DataIndex="AuditFlag" Flex="1" >
                            <Renderer Fn="auditFlagChange" />
                        </ext:Column>--%>
                        <ext:Column ID="CheckWeight" runat="server" Text="实际结存" DataIndex="CheckWeight" Flex="1" />
                        <ext:DateColumn ID="PlanDate" runat="server" Text="结存日期" DataIndex="PlanDate" Format="yyyy-MM-dd" Flex="1" />
                        <ext:Column ID="WorkShopName" runat="server" Text="车间" DataIndex="WorkShopName" Flex="1" />
                        <ext:Column ID="ShiftName" runat="server" Text="班次" DataIndex="ShiftName" Flex="1" />
                        <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                            <Commands>
                                <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="保存调整">
                                    <ToolTip Text="调整本条数据" />
                                </ext:GridCommand>
                            </Commands>
                            <PrepareToolbar Fn="prepareToolbar" />
                            <Listeners>
                                <Command Handler="return commandcolumn_click_confirm(command, record);" />
                            </Listeners>
                        </ext:CommandColumn>
                    </Columns>
                </ColumnModel>
                <SelectionModel>
                    <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single" />
                </SelectionModel>
                <Plugins>
                    <ext:CellEditing ID="CellEditing2" runat="server">
                        <Listeners>
                            <Edit Fn="edit2" />
                        </Listeners>
                    </ext:CellEditing>
                </Plugins>
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

            <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改实际结存重量"
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
                            <ext:DateField ID="txtPlanDate2" runat="server" FieldLabel="统计日期" LabelAlign="Left" ReadOnly="true" />
                            <ext:TextField ID="txtMaterName2" runat="server" FieldLabel="物料名称" LabelAlign="Left" ReadOnly="true" />
                            <ext:TextField ID="txtStoreWeight2" runat="server" FieldLabel="结存重量" LabelAlign="Left" ReadOnly="true" />                         
                            <ext:TextField ID="txtCheckWeight2" runat="server" Vtype="decimal" FieldLabel="投入重量" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                        </Items>
                            <Listeners>
                            <ValidityChange Handler="#{btnModify}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnModify" runat="server" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="btnModify_Click">
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
                    <Show Handler="for(var i=0;i<#{vpShopStore}.items.length;i++){#{vpShopStore}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpShopStore}.items.length;i++){#{vpShopStore}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>
        </Items>
    </ext:Viewport>
    <ext:Hidden ID="hiddenMaterCode" runat="server" />
    </form>
</body>
</html>
