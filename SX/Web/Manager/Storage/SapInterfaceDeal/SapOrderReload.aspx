<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SapOrderReload.aspx.cs" Inherits="Manager_Storage_SapInterfaceDeal_SapOrderReload" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell {
            background-color: #B0FFBA !important;
        }
        .x-grid-row-error .x-grid-cell {
            background-color: #FF4D4D !important;
        }
        
    </style>
    <script type="text/javascript">
        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("IsUpload") == "0") {
                return "x-grid-row-collapsed";
            }
        }
        var SetDealStateRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("DealState") == "S") {
                return "x-grid-row-collapsed";
            }
        }

        var SetDealStateMainRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("DealState").indexOf("E") > -1) {
                return "x-grid-row-error";
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


        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        var errorStateJfun = function (value) {
            if (value == "E") {
                return "错误";
            } else {
                return "正确";
            }
        };

        var sapOrderTypeJfun = function (value) {
            if (value == "R") {
                return "返回消息单";
            } 
            if (value == "P") {
                return "生产计划单";
            }
            if (value == "V") {
                return "生产版本单";
            }
            if (value == "G") {
                return "物料收货单";
            }
            if (value == "S") {
                return "预留单";
            }
            if (value == "D") {
                return "内向交货单";
            }
            if (value == "O") {
                return "出货单";
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
            return false;
        };

        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var mesOrderCode = record.data.MesOrderCode;
            App.direct.commandcolumn_direct_edit(mesOrderCode, {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var gridPanelCellDblClick = function (grid, td, tdindex, record, tr, trindex, e, fn) {
            //alert(record.data.BillType);
            if (record.data.Ext_1 != "" && record.data.Ext_1 != null) {
                var url = "Rubber/RubberStoreOutToSAP.aspx?ObjID=" + record.data.Ext_1;
                var tabid = "Manager_Rubber_RubberStoreOutToSAP";
                var tabp = parent.App.mainTabPanel;
                var tab = tabp.getComponent("id=" + tabid);
                if (tab) {
                    tab.close();
                }
                parent.addTab(tabid, "SAP数据上传", url, true);
            }

        }
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
                                <ext:ToolbarSeparator ID="tsBegin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <Listeners>
                                        <Click Fn="pnlListFresh" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsEnd" />
                                <ext:Button runat="server" Icon="FolderUp" Text="错误单据重传" ID="btnReUpload">
                                    <DirectEvents>
                                        <Click OnEvent="ReUpload_Btn_Click" >
                                            <ExtraParams>
                                                <ext:Parameter Name="Values" Value="Ext.encode(#{pnlList}.getRowsValues({selectedOnly:true}))" Mode="Raw" />
                                            </ExtraParams>
                                            <EventMask ShowMask="true" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="ToolbarSeparator1" />
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
                                        <ext:TextField ID="txtMesOrderCode" runat="server" FieldLabel="MES单据号" LabelAlign="Right" />
                                        <ext:TextField ID="txtSAPOrderCode" runat="server" FieldLabel="SAP单据号" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                                 <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                        <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="发送开始时间" LabelAlign="Right" Editable="false" />
                                        <ext:DateField ID="txtEndTime" runat="server" FieldLabel="发送结束时间" LabelAlign="Right" Editable="false" />
                                    </Items>
                                </ext:Container>
                                 <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="txtIsUpload" runat="server" FieldLabel="是否重传" LabelAlign="Right" Editable="false"  >
                                            <Items>
                                                <ext:ListItem Text="是" Value="1"></ext:ListItem>
                                                <ext:ListItem Text="否" Value="0"></ext:ListItem>
                                            </Items>
                                             <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            </Triggers>
                                            <Listeners>
                                                <Select Handler="this.getTrigger(0).show();" />
                                                <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                            </Listeners>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="txtIsError" runat="server" FieldLabel="是否成功" LabelAlign="Right" Editable="false"  >
                                            <Items>
                                                <ext:ListItem Text="成功" Value="E"></ext:ListItem>
                                                <ext:ListItem Text="失败" Value="S"></ext:ListItem>
                                            </Items>
                                             <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            </Triggers>
                                            <Listeners>
                                                <Select Handler="this.getTrigger(0).show();" />
                                                <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container4" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="cbxOperype" runat="server" FieldLabel="上传类型" LabelAlign="Right" Editable="false">
                                            <Items>
                                                <ext:ListItem Text="MM库存数据" Value="10" />
                                                <ext:ListItem Text="终炼胶数据" Value="00" />
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
                                <ext:Store ID="store" runat="server" PageSize="10">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData" AutoDataBind="false" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model" runat="server" IDProperty="MesOrderCode,MesOrderType,Ext_1">
                                            <Fields>
                                                <ext:ModelField Name="MesOrderCode" />
                                                <ext:ModelField Name="MesOrderType" />
                                                <ext:ModelField Name="SendDate" />
                                                <ext:ModelField Name="SendSystem" />
                                                <ext:ModelField Name="UploadDate" />
                                                <ext:ModelField Name="DealState" />
                                                <ext:ModelField Name="IsUpload" />
                                                <ext:ModelField Name="Ext_2" />
                                                <ext:ModelField Name="Ext_1" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="colModel" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                    <ext:Column ID="mesOrderCode" runat="server" Text="MES单据号" DataIndex="MesOrderCode" Flex="1" />
                                    <ext:Column ID="mesOrderType" runat="server" Text="MES单据类型" DataIndex="MesOrderType" Flex="1" />
                                    <ext:Column ID="Ext_2" runat="server" Text="上/下行" DataIndex="Ext_2" Flex="1" />
                                    <ext:Column ID="sendDate" runat="server" Text="发送时间" DataIndex="SendDate" Flex="1" />
                                    <ext:Column ID="isUpload" runat="server" Text="是否重传" DataIndex="IsUpload" Flex="1" />
                                    <ext:Column ID="uploadDate" runat="server" Text="重传时间" DataIndex="UploadDate" Flex="1" />
                                     <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                        <Commands>
                                            <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改SAP订单号">
                                                <ToolTip Text="修改SAP订单号" />
                                            </ext:GridCommand>
                                        </Commands>
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
                            <View>
                                <ext:GridView ID="gvRows" runat="server">
                                    <GetRowClass Fn="SetDealStateMainRowClass" />
                                </ext:GridView>
                            </View>
                            <BottomBar>
                                <ext:PagingToolbar ID="pageToolBar" runat="server">
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
                <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="返回消息明细" Height="250" Icon="Basket" Layout="Fit" Collapsible="true"
                    Split="true" MarginsSummary="0 5 5 5">
                    <Items>
                        <ext:GridPanel ID="pnlDetailList" runat="server" MarginsSummary="0 5 5 5">
                            <Store>
                                <ext:Store ID="storeDetail" runat="server" PageSize="10" OnReadData="RowSelect">
                                    <Model>
                                        <ext:Model ID="modelDetail" runat="server" IDProperty="ErrorDesc, SAPOrderCode">
                                            <Fields>
                                                <ext:ModelField Name="DealState" />
                                                <ext:ModelField Name="ErrorDesc" />
                                                <ext:ModelField Name="SAPOrderCode" />
                                                <ext:ModelField Name="SAPOrderType" />
                                                <ext:ModelField Name="Ext_1" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Parameters>
                                        <ext:StoreParameter Name="MesOrderCode" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.MesOrderCode : -1" />
                                        <ext:StoreParameter Name="SendDate" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.SendDate : -1" />
                                    </Parameters>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModelDetail" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />
                                    <ext:Column ID="Ext_1" runat="server" Text="上传序号" DataIndex="Ext_1" Width="80" />
                                    <ext:Column ID="DealState" runat="server" Text="错误状态" DataIndex="DealState" Width="80" >
                                        <Renderer Fn="errorStateJfun" />
                                    </ext:Column>
                                    <ext:Column ID="ErrorDesc" runat="server" Text="错误描述" DataIndex="ErrorDesc" Width="950" />
                                    <ext:Column ID="SAPOrderCode" runat="server" Text="SAP订单号" DataIndex="SAPOrderCode" Width="100" Format="yyyy-MM-dd HH:mm:ss" />
                                    <ext:Column ID="SAPOrderType" runat="server" Text="SAP订单类型" DataIndex="SAPOrderType" Width="100" >
                                        <Renderer Fn="sapOrderTypeJfun" />
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <CellDblClick Fn="gridPanelCellDblClick" />
                            </Listeners>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectMutiDetail" runat="server" Mode="Multi" />
                            </SelectionModel>
                            <View>
                                <ext:GridView ID="GridView1" runat="server">
                                    <GetRowClass Fn="SetDealStateRowClass" />
                                </ext:GridView>
                            </View>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server">
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
                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改SAP订单号信息"
                    Width="320" Height="240" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                <ext:TextField ID="modify_mes_order_code" runat="server" FieldLabel="MES单据号"   LabelAlign="Left" ReadOnly="true" FieldStyle="color:#C0C0C0;"/>
                                <ext:TextField ID="modify_sap_order_code" runat="server" FieldLabel="SAP订单号"   LabelAlign="Left" />
                            </Items>
                             <Listeners>
                                <ValidityChange Handler="#{btnModifySave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnModifySave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="BtnModifySave_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="BtnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
