<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ledger.aspx.cs" Inherits="Manager_RawMaterialQuality_Ledger" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>电子台账维护</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript" language="javascript">
        var gridPanelRefresh = function () {
            App.store.currentPage = 1;
            App.direct.ReloadLedgerList();
            return false;
        };

        var setRowClass = function (record, index, rowParams, store) {
            if (record.get('AutoCheckResult') == '0') {
                return 'x-grid-row-deleted';
            }
        };

        var ColumnCenterMasterCheckResultDes_Renderer = function (value, metadata, record, rowIndex, colIndex, store, view) {
            if (record.get("检测结果") == "0") {
                return "<span style='background-color: red'>" + value + "</span>";
            }
            return value;
        };

        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var LedgerId = record.data.LedgerId;
            App.direct.commandcolumn_direct_edit(LedgerId, {
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
            var LedgerId = record.data.LedgerId;
            App.direct.commandcolumn_direct_delete(LedgerId, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //区分删除操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
            }
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此条信息？', function (btn) { commandcolumn_direct_delete(btn, record) });
            }
            return false;
        };

        //根据按钮类别进行删除和编辑操作
        var commandcolumn_click = function (command, record) {
            commandcolumn_click_confirm(command, record);
            return false;
        };

        //转换DeleteFlag显示方式
        var deleteConvert = function (value) {
            return Ext.String.format(value == "1" ? "是" : "否");
        };
    </script>
     <script type="text/javascript">
         //-------质检信息绑定-----查询带回弹出框--BEGIN
         var Manager_RawMaterialQuality_QueryQmcCheckData_Request = function (record) {//质检信息绑定
             if (!App.windowAddLedger.hidden) {
                 App.txtAddCheckId.setValue(record.data.CheckId);
                 App.trfAddBillNo.setValue(record.data.BillNo);
                 App.txtAddBarcode.setValue(record.data.Barcode);
                 App.txtAddOrderId.setValue(record.data.OrderId);
                 App.txtAddMaterCode.setValue(record.data.MaterCode);
                 App.trfAddCheckerName.setValue(record.data.RecorderName);
                 App.trfAddExtractorName.setValue(record.data.ExtractorName);
                 App.trfAddFetcherName.setValue(record.data.FetcherName);
                 App.trfAddHandlerName.setValue(record.data.HandlerName);
                 App.trfAddReceiverName.setValue(record.data.ReceiverName);
                 App.trfAddSupplierName.setValue(record.data.SupplyFacName);
                 App.txtAddManufacturerName.setValue(record.data.ProductFacName);
                 App.txtAddCheckerId.setValue(record.data.RecorderId);
                 App.txtAddSpec.setValue(record.data.SpecName);
                 App.direct.LoadAddCheckDetail();
             }
         }

         var SelectCheck = function () {//质检信息查询
             Ext.create("Ext.window.Window", {//质检信息绑定查询带回窗体
                 id: "Manager_RawMaterialQuality_QueryQmcCheckData_Window",
                 height: 500,
                 hidden: true,
                 width: 900,
                 html: "<iframe src='QueryQmcCheckData.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
                 bodyStyle: "background-color: #fff;",
                 closable: true,
                 title: "请选择检测记录",
                 modal: true
             })
             App.Manager_RawMaterialQuality_QueryQmcCheckData_Window.show();
         }
         //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //-------人员绑定-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryQmcUser_Request = function (record) {//人员绑定
            var command = App.txtHiddenSelectCommand.getValue();
            switch (command) {
                case "checker":
                    {
                        if (!App.windowModifyLedger.hidden) {
                            App.trfModifyCheckerName.setValue(record.data.UserName);
                            App.txtModifyCheckerId.setValue(record.data.HRCode);
                        }
                        if (!App.windowAddLedger.hidden) {
                            App.trfAddCheckerName.setValue(record.data.UserName);
                            App.txtAddCheckerId.setValue(record.data.HRCode);
                        }
                    }
                    break;
                case "extractor":
                    {
                        if (!App.windowModifyLedger.hidden) {
                            App.trfModifyExtractorName.setValue(record.data.UserName);
                            App.txtModifyExtractorId.setValue(record.data.HRCode);
                        }
                        if (!App.windowAddLedger.hidden) {
                            App.trfAddExtractorName.setValue(record.data.UserName);
                            App.txtAddExtractorId.setValue(record.data.HRCode);
                        }
                    }
                    break;
                case "receiver":
                    {
                        if (!App.windowModifyLedger.hidden) {
                            App.trfModifyReceiverName.setValue(record.data.UserName);
                            App.txtModifyReceiverId.setValue(record.data.HRCode);
                        }
                        if (!App.windowAddLedger.hidden) {
                            App.trfAddReceiverName.setValue(record.data.UserName);
                            App.txtAddReceiverId.setValue(record.data.HRCode);
                        }
                    }
                    break;
                case "fetcher":
                    {
                        if (!App.windowModifyLedger.hidden) {
                            App.trfModifyFetcherName.setValue(record.data.UserName);
                            App.txtModifyFetcherId.setValue(record.data.HRCode);
                        }
                        if (!App.windowAddLedger.hidden) {
                            App.trfAddFetcherName.setValue(record.data.UserName);
                            App.txtAddFetcherId.setValue(record.data.HRCode);
                        }
                    }
                    break;
                case "handler":
                    {
                        if (!App.windowModifyLedger.hidden) {
                            App.trfModifyHandlerName.setValue(record.data.UserName);
                            App.txtModifyHandlerId.setValue(record.data.HRCode);
                        }
                        if (!App.windowAddLedger.hidden) {
                            App.trfAddHandlerName.setValue(record.data.UserName);
                            App.txtAddHandlerId.setValue(record.data.HRCode);
                        }
                        if (!App.windowBatchModify.hidden) {
                            App.trfBatchModifyHandlerName.setValue(record.data.UserName);
                            App.txtBatchModifyHandlerId.setValue(record.data.HRCode);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        var SelectChecker = function () {//检测人绑定查询
            App.txtHiddenSelectCommand.setValue("checker");
            App.Manager_BasicInfo_CommonPage_QueryQmcUser_Window.show();
        }
        var SelectFetcher = function () {//领取人绑定查询
            App.txtHiddenSelectCommand.setValue("fetcher");
            App.Manager_BasicInfo_CommonPage_QueryQmcUser_Window.show();
        }
        var SelectExtractor = function () {//取样人绑定查询
            App.txtHiddenSelectCommand.setValue("extractor");
            App.Manager_BasicInfo_CommonPage_QueryQmcUser_Window.show();
        }
        var SelectReceiver = function () {//接收人绑定查询
            App.txtHiddenSelectCommand.setValue("receiver");
            App.Manager_BasicInfo_CommonPage_QueryQmcUser_Window.show();
        }
        var SelectHandler = function () {//处置人绑定查询
            App.txtHiddenSelectCommand.setValue("handler");
            App.Manager_BasicInfo_CommonPage_QueryQmcUser_Window.show();
        }
        Ext.create("Ext.window.Window", {//人员绑定查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryQmcUser_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryQmcUser.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择人员",
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //-------厂商绑定-----查询带回弹出框--BEGIN
        var Manager_RawMaterialQuality_QueryFactoryMapping_Request = function (record) {//厂商绑定
            if (!App.windowModifyLedger.hidden) {
                App.trfModifySupplierName.setValue(record.data.SupplierName);
                App.txtModifySupplierId.setValue(record.data.SupplierId);
                App.txtModifyManufacturerName.setValue(record.data.ManufacturerName);
                App.txtModifyManufacturerId.setValue(record.data.ManufacturerId);
            }
            if (!App.windowAddLedger.hidden) {
                App.trfAddSupplierName.setValue(record.data.SupplierName);
                App.txtAddSupplierId.setValue(record.data.SupplierId);
                App.txtAddManufacturerName.setValue(record.data.ManufacturerName);
                App.txtAddManufacturerId.setValue(record.data.ManufacturerId);
            }
        }

        var SelectSupplier = function () {//厂商绑定查询
            App.Manager_RawMaterialQuality_QueryFactoryMapping_Window.show();
        }
            Ext.create("Ext.window.Window", {//厂商绑定查询带回窗体
            id: "Manager_RawMaterialQuality_QueryFactoryMapping_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='QueryFactoryMapping.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择厂商关系",
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
    <ext:ResourceManager ID="rmLedger" runat="server" />
    <ext:Viewport ID="vwUnit" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnlNorth" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="barUnit1">
                        <Items>
                            <ext:Button runat="server" Icon="Add" Text="台账录入" ID="btnAdd">
                                <ToolTips>
                                    <ext:ToolTip ID="ttAdd" runat="server" Html="点击添加新台账" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="btn_add_Click">
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <ToolTips>
                                    <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsMiddle" />
                            <ext:Button runat="server" Icon="DatabaseGear" Text="批量修改" ID="btnBatchModify">
                                <ToolTips>
                                    <ext:ToolTip ID="ttBatchModify" runat="server" Html="点击批量修改所选台账的处置时间、处置方式和返库时间" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="btn_batchModify_Click">
                                        <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsMiddle2" />
                            <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                <ToolTips>
                                    <ext:ToolTip ID="ttExport" runat="server" Html="点击导出Excel文件下载" />
                                </ToolTips>
                                <Listeners>
                                    <Click Handler="$('#btnExportSubmit').click();"></Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_end1" />
                            <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end1" />
                            <ext:ToolbarFill ID="toolbarFill_end1" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:FormPanel runat="server" ID="pnlForm" Layout="AnchorLayout" AutoHeight="true">
                        <Items>
                            <ext:Container ID="container1" runat="server" Layout="HBoxLayout" Padding="5" >
                                <Items>
                                    <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="录入起始时间" LabelAlign="Right" Format="yyyy-MM-dd" Flex="1" LabelWidth="90" Editable="false">
<%--                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Handler="this.setValue('');" />
                                        </Listeners>--%>
                                    </ext:DateField>
                                    <ext:DateField ID="txtEndTime" runat="server" FieldLabel="录入结束时间" LabelAlign="Right" Format="yyyy-MM-dd" Flex="1" LabelWidth="90" Editable="false">
<%--                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Handler="this.setValue('');" />
                                        </Listeners>--%>
                                    </ext:DateField>
                                    <ext:TextField ID="txtBillNo" runat="server" FieldLabel="送检单号" LabelAlign="Right" Flex="1" LabelWidth="70"
                                        Visible="true">
                                    </ext:TextField>                               
                                    <ext:TextField ID="txtBarcode" runat="server" FieldLabel="条码号" LabelAlign="Right" Flex="1" LabelWidth="70"
                                        Visible="true">
                                    </ext:TextField>                               
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="HBoxLayout" Padding="5" >
                                <Items>       
                                    <ext:ComboBox runat="server" ID="cbxSeries" FieldLabel="原材料系列" LabelAlign="Right" Visible="true" Editable="false" Flex="1" LabelWidth="90">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Handler="this.setValue('');" />
                                        </Listeners>
                                        <DirectEvents>
                                            <Change OnEvent="cbxSeries_Change">
                                                 <EventMask ShowMask="true" />
                                            </Change>
                                        </DirectEvents>
                                    </ext:ComboBox>       
                                    <ext:ComboBox runat="server" ID="cbxMaterial" FieldLabel="原材料型号" LabelAlign="Right" Visible="true" Editable="false" Flex="1" LabelWidth="90">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Handler="this.setValue('');" />
                                        </Listeners>
                                        <DirectEvents>
                                            <Change OnEvent="cbxMaterial_Change">
                                                 <EventMask ShowMask="true" />
                                            </Change>
                                        </DirectEvents>
                                    </ext:ComboBox>  
                                    <ext:ComboBox runat="server" ID="cbxSpec" FieldLabel="规格" LabelAlign="Right" Visible="true" Editable="false" Flex="1" LabelWidth="70">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Handler="this.setValue('');" />
                                        </Listeners>
                                        <DirectEvents>
                                            <Change OnEvent="cbxSpec_Change">
                                                 <EventMask ShowMask="true" />
                                            </Change>
                                        </DirectEvents>
                                    </ext:ComboBox>             
                                    <ext:ComboBox ID="cbxCheckResult" runat="server" FieldLabel="检测结果" LabelAlign="Right" LabelWidth="70"
                                        Visible="true" Editable="false"  Flex="1">
                                        <SelectedItems>
                                            <ext:ListItem Value="all">
                                            </ext:ListItem>
                                        </SelectedItems>
                                        <Items>
                                            <ext:ListItem Text="全部" Value="all" AutoDataBind="true">
                                            </ext:ListItem>
                                            <ext:ListItem Text="合格" Value="1">
                                            </ext:ListItem>
                                            <ext:ListItem Text="不合格" Value="2">
                                            </ext:ListItem>
                                        </Items>
                                        <Listeners>
                                            <Select Fn="gridPanelRefresh"></Select>
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="pnlCenter" runat="server" Region="Center" Frame="true" Layout="Fit"
                MarginsSummary="0 5 0 5">
                <Items>
                    <ext:GridPanel ID="pnlLedger" runat="server" MarginsSummary="0 5 5 5">
                      <Store>
                           <ext:Store ID="store" runat="server" PageSize="10">
                                <Model>
                                    <ext:Model ID="model" runat="server" IDProperty="LedgerId">
                                        <Fields>
                                        <ext:ModelField Name="LedgerId" />
                                        <ext:ModelField Name="CheckId" />
                                        <ext:ModelField Name="BillDetailId" />
                                        <ext:ModelField Name="OrderId" />
                                        <ext:ModelField Name="原材料名称" />
                                        <ext:ModelField Name="送检单号" />
                                        <ext:ModelField Name="条码号" />
                                        <ext:ModelField Name="批次号" />
                                        <ext:ModelField Name="规格" />
                                        <ext:ModelField Name="送检数量" />
                                        <ext:ModelField Name="单位" />
                                        <ext:ModelField Name="检测结果" />
                                        <ext:ModelField Name="检测人" />
                                        <ext:ModelField Name="取样人" />
                                        <ext:ModelField Name="接收人" />
                                        <ext:ModelField Name="接收时间" Type="Date"/>
                                        <ext:ModelField Name="发放时间" Type="Date"/>
                                        <ext:ModelField Name="领取人" />
                                        <ext:ModelField Name="返库时间" Type="Date"/>
                                        <ext:ModelField Name="处置时间" Type="Date"/>
                                        <ext:ModelField Name="处置方式" />
                                        <ext:ModelField Name="处置人" />
                                        <ext:ModelField Name="记录时间" Type="Date"/>
                                        <ext:ModelField Name="备注" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                                <Parameters>
                                    <ext:StoreParameter Name="LedgerId" Mode="Raw" Value="#{pnlLedger}.getSelectionModel().hasSelection() ? #{pnlLedger}.getSelectionModel().getSelection()[0].data.LedgerId : -1" />
                                </Parameters>
                           </ext:Store>
                      </Store>
                      <ColumnModel ID="colModel1" runat="server">
                          <Columns>
                              <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />
                              <ext:Column ID="ledgercolumn1" runat="server" Text="LedgerId" DataIndex="LedgerId" Visible="false"/>
                              <ext:Column ID="ledgercolumn2" runat="server" Text="CheckId" DataIndex="CheckId" Visible="false"/>
                              <ext:Column ID="ledgercolumn3" runat="server" Text="BillDetailId" DataIndex="BillDetailId" Visible="false"/>
                              <ext:Column ID="ledgercolumn4" runat="server" Text="OrderId" DataIndex="OrderId" Visible="false"/>
                              <ext:Column ID="ledgercolumn5" runat="server" Text="原材料名称" DataIndex="原材料名称" />
                              <ext:Column ID="ledgercolumn6" runat="server" Text="送检单号" DataIndex="送检单号" />
                              <ext:Column ID="ledgercolumn7" runat="server" Text="条码号" DataIndex="条码号" />
                              <ext:Column ID="ledgercolumn8" runat="server" Text="批次号" DataIndex="批次号" />
                              <ext:Column ID="ledgercolumn9" runat="server" Text="规格" DataIndex="规格" />
                              <ext:Column ID="ledgercolumn10" runat="server" Text="送检数量" DataIndex="送检数量" />
                              <ext:Column ID="ledgercolumn11" runat="server" Text="单位" DataIndex="单位" />
                              <ext:Column ID="ledgercolumn12" runat="server" Text="检测结果" DataIndex="检测结果" >
                                  <Renderer Fn="ColumnCenterMasterCheckResultDes_Renderer" />
                              </ext:Column>
                              <ext:Column ID="ledgercolumn13" runat="server" Text="检测人" DataIndex="检测人" />
                              <ext:Column ID="ledgercolumn14" runat="server" Text="取样人" DataIndex="取样人" />
                              <ext:Column ID="ledgercolumn15" runat="server" Text="接收人" DataIndex="接收人" />
                              <ext:DateColumn ID="ledgercolumn16" runat="server" Text="接收时间" DataIndex="接收时间" Format="yyyy-MM-dd"/>
                              <ext:DateColumn ID="ledgercolumn17" runat="server" Text="发放时间" DataIndex="发放时间" Format="yyyy-MM-dd"/>
                              <ext:Column ID="ledgercolumn18" runat="server" Text="领取人" DataIndex="领取人" />
                              <ext:DateColumn ID="ledgercolumn19" runat="server" Text="返库时间" DataIndex="返库时间" Format="yyyy-MM-dd"/>
                              <ext:DateColumn ID="ledgercolumn20" runat="server" Text="处置时间" DataIndex="处置时间" Format="yyyy-MM-dd"/>
                              <ext:Column ID="ledgercolumn21" runat="server" Text="处置方式" DataIndex="处置方式" />
                              <ext:Column ID="ledgercolumn22" runat="server" Text="处置人" DataIndex="处置人" />
                              <ext:DateColumn ID="ledgercolumn23" runat="server" Text="记录时间" DataIndex="记录时间" Format="yyyy-MM-dd HH:mm:ss"/>
                              <ext:Column ID="ledgercolumn24" runat="server" Text="备注" DataIndex="备注" />
                              <ext:CommandColumn ID="commandCol1" runat="server" Width="130" Text="操作" Align="Center">
                                    <Commands>
                                        <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改" >
                                            <ToolTip Text="修改本条数据" />
                                        </ext:GridCommand>
                                        <ext:CommandSeparator />
                                        <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除" >
                                            <ToolTip Text="删除本条数据" />
                                        </ext:GridCommand>
                                    </Commands>
                                    <Listeners>
                                        <Command Handler="return commandcolumn_click(command, record);" />
                                    </Listeners>
                              </ext:CommandColumn>
                          </Columns>
                      </ColumnModel>
                      <SelectionModel>
                            <ext:CheckboxSelectionModel runat="server" ID="CheckboxSelectionModelCenterMaster"
                                Mode="Simple" ShowHeaderCheckbox="true">
                                <DirectEvents>
                                    <Select OnEvent="CheckboxSelectionModelCenterMaster_SelectionChange">
                                        <ExtraParams>
                                            <ext:Parameter Mode="Raw" Name="CheckId" Value="record.get('CheckId')" />
                                        </ExtraParams>
                                    </Select>
                                </DirectEvents>
                            </ext:CheckboxSelectionModel>
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="pageToolBar" runat="server" RefreshHandler="gridPanelRefresh">
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager" runat="server"/>
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="质检数据" Height="200" Icon="Basket" Layout="Fit" Collapsible="true" Split="true" MarginsSummary="0 5 5 5">
                <TopBar><ext:Toolbar ID="Toolbar1" runat="server" Height="1"></ext:Toolbar></TopBar>
                <Items>
                     <ext:GridPanel runat="server" ID="GridPanelCenterDeail" Region="South" AutoScroll="true"
                        Height="150" Padding="2">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenterDetail">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenterDetail">
                                        <Fields>
                                            <ext:ModelField Name="ItemName" />
                                            <ext:ModelField Name="GoodMinValue" />
                                            <ext:ModelField Name="GoodOperator" />
                                            <ext:ModelField Name="GoodMaxValue" />
                                            <ext:ModelField Name="CheckMethod" />
                                            <ext:ModelField Name="GoodTextValue" />
                                            <ext:ModelField Name="Remark" />
                                            <ext:ModelField Name="CheckValue" />
                                            <%--<ext:ModelField Name="GoodCheckRange" />--%>
                                            <ext:ModelField Name="GoodDisplayValue" />
                                            <ext:ModelField Name="Frequency" />
                                            <%--<ext:ModelField Name="PrimeCheckRange" />--%>
                                            <ext:ModelField Name="PrimeDisplayValue" />
                                            <ext:ModelField Name="AutoCheckResult" />
                                            <ext:ModelField Name="TextCheckResult" />
                                            <ext:ModelField Name="IsPrime" />
                                            <ext:ModelField Name="TextIsPrime" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:Column runat="server" ID="ColumnCenterDetailItemName" DataIndex="ItemName" Text="检验项目"
                                    Width="200" />
                                <ext:Column runat="server" ID="CheckColumnCenterDetailAutoCheckResult" DataIndex="AutoCheckResult"
                                    Text="是否合格" Hidden="true"/>
                                <ext:Column runat="server" ID="CheckColumnCenterDetailAutoIsPrime" DataIndex="IsPrime"
                                    Text="是否一级品" Hidden="true"/>
                                <ext:Column runat="server" ID="ColumnCenterDetailCheckRange" DataIndex="GoodDisplayValue"
                                    Text="合格品指标值" Width="150"/>
                                <ext:Column runat="server" ID="ColumnCenterDetailCheckValue" DataIndex="CheckValue"
                                    Text="检验值" />
                                <ext:Column runat="server" ID="CheckColumnCenterDetailTextCheckResult" DataIndex="TextCheckResult"
                                    Text="是否合格" />
                                <ext:Column runat="server" ID="CheckColumnCenterDetailTextIsPrime" DataIndex="TextIsPrime"
                                    Text="是否一级品" />
                                <ext:Column runat="server" ID="ColumnCenterDetailAutoPrimeCheckRange" DataIndex="PrimeDisplayValue"
                                    Text="一级品指标值" Width="150"/>
                                <ext:Column runat="server" ID="ColumnCenterDetailFrequency" DataIndex="Frequency"
                                    Text="频次" />
                                <ext:Column runat="server" ID="ColumnCenterDetailCheckMethod" DataIndex="CheckMethod"
                                    Text="检验方法" Width="200" />
                            </Columns>
                        </ColumnModel>
                         <View>
                            <ext:GridView runat="server" ID="GridViewCenterDetail" StripeRows="true" TrackOver="true">
                                <GetRowClass Fn="setRowClass" />
                            </ext:GridView>
                         </View>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
            <ext:Window ID="windowAddLedger" runat="server" Icon="MonitorEdit" Closable="false"
                Title="录入台账" Width="720" ManageHeight="false" Resizable="false" Hidden="true" Modal="false"
                BodyStyle="background-color:#fff;" BodyPadding="5" Layout="Form" AutoScroll="true">
                <Items>
                    <ext:FormPanel ID="pnlAddLedgerNative" runat="server" Flex="1" BodyPadding="5" Layout="Column">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="120" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:TextField ID="txtAddCheckId" runat="server" FieldLabel="检测Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtAddOrderId" runat="server" FieldLabel="显示序号" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtAddMaterCode" runat="server" FieldLabel="物料编码" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtAddSpecId" runat="server" FieldLabel="规格ID" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtAddFrequency" runat="server" FieldLabel="检测频次" LabelAlign="Right"
                                MaxLength="10" Padding="5" ReadOnly="true" Hidden="true"/>
                            <ext:Triggerfield ID="trfAddBillNo" runat="server" FieldLabel="检测记录" LabelAlign="Right"
                                AllowBlank="False" Editable="false" Padding="5" IndicatorText="*" IndicatorCls="red-text">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectCheck" />
                                </Listeners>
                            </ext:Triggerfield>
                            <ext:TextField ID="txtAddSpec" runat="server" FieldLabel="规格" LabelAlign="Right"
                                ReadOnly="true" Padding="5" />
                            <ext:TextField ID="txtAddBarcode" runat="server" FieldLabel="条码号" LabelAlign="Right"
                                ReadOnly="true" Padding="5" />
                            <ext:TextField ID="txtAddMaterialName" runat="server" FieldLabel="物料名称" LabelAlign="Right"
                                ReadOnly="true" Padding="5" LabelPad="11"/>
                            <ext:TriggerField ID="trfAddSupplierName" runat="server" FieldLabel="供应商" LabelAlign="Right"
                                Editable="false" Padding="5"  >
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectSupplier" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtAddManufacturerName" runat="server" FieldLabel="生产商" LabelAlign="Right"
                                ReadOnly="true" Padding="5" LabelPad="11"/>
                            <ext:TextField ID="txtAddSupplierId" runat="server" FieldLabel="供应商Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtAddManufacturerId" runat="server" FieldLabel="生产商Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtAddSendNum" runat="server" FieldLabel="送检数量" LabelAlign="Right"
                                ReadOnly="true" Padding="5" />
                            <ext:TextField ID="txtAddUnit" runat="server" FieldLabel="单位" LabelAlign="Right"
                                MaxLength="10" Padding="5" LabelPad="11" ReadOnly="true"/>
                            <ext:TextField ID="txtAddBatchCode" runat="server" FieldLabel="批次号" LabelAlign="Right"
                                MaxLength="20" Padding="5" ReadOnly="true"/>
                            <ext:TextField ID="txtAddCheckResult" runat="server" FieldLabel="检测结果" LabelAlign="Right"
                                AllowBlank="False" ReadOnly="true" Padding="5" LabelPad="11">
                            </ext:TextField>
                            <ext:TriggerField ID="trfAddCheckerName" runat="server" FieldLabel="检测人" LabelAlign="Right"
                                Editable="false" AllowBlank="false" Padding="5" IndicatorText="*" IndicatorCls="red-text">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectChecker" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtAddCheckerId" runat="server" FieldLabel="检测人Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TriggerField ID="trfAddExtractorName" runat="server" FieldLabel="取样人" LabelAlign="Right"
                                Editable="false" Padding="5">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectExtractor" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtAddExtractorId" runat="server" FieldLabel="取样人Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TriggerField ID="trfAddReceiverName" runat="server" FieldLabel="接收人" LabelAlign="Right"
                                Editable="false" Padding="5" >
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectReceiver" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtAddReceiverId" runat="server" FieldLabel="接收人Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TriggerField ID="trfAddFetcherName" runat="server" FieldLabel="领取人" LabelAlign="Right"
                                Editable="false" Padding="5" LabelPad="11">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectFetcher" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtAddFetcherId" runat="server" FieldLabel="领取人Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TriggerField ID="trfAddHandlerName" runat="server" FieldLabel="处置人" LabelAlign="Right"
                                Editable="false" Padding="5" >
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectHandler" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtAddHandlerId" runat="server" FieldLabel="处置人Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:DateField ID="dtfAddSendDate" runat="server" FieldLabel="发放时间" LabelAlign="Right" Editable="false" Padding="5" LabelPad="11"/>         
                            <ext:DateField ID="dtfAddReceiveDate" runat="server" FieldLabel="接收时间" LabelAlign="Right" Editable="false" Padding="5"  LabelPad="5"/>                 
                            <ext:DateField ID="dtfAddReturnDate" runat="server" FieldLabel="返库时间" LabelAlign="Right" Editable="false" Padding="5" LabelPad="11"/>
                            <ext:DateField ID="dtfAddHandleDate" runat="server" FieldLabel="处置时间" LabelAlign="Right" Editable="false" Padding="5"  LabelPad="5"/>
                              <ext:ComboBox ID="cbxAddHandleMethod" runat="server" FieldLabel="处置方式" LabelAlign="Right" Padding="5" LabelPad="11">
                                <Items>
                                    <ext:ListItem Text="退库（车间）" Value="退库（车间）">
                                    </ext:ListItem>
                                    <ext:ListItem Text="退回委托方" Value="退回委托方">
                                    </ext:ListItem>
                                    <ext:ListItem Text="废弃" Value="废弃">
                                    </ext:ListItem>
                                </Items>
                            </ext:ComboBox>
                            <ext:TextField ID="txtAddRemark" runat="server" FieldLabel="备注" LabelAlign="Right"
                                MaxLength="50" Padding="5" LabelPad="5"/>
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{btnAddKeySave}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                    <ext:FormPanel ID="pnlAddLedgerCustom" runat="server" Flex="1" BodyPadding="5" Layout="Column">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="120" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                        </Items>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnAddKeySave" runat="server" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="BtnAddSave_Click">
                                <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnAddKeyCancel" runat="server" Text="取消" Icon="Cancel">
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
            <ext:Window ID="windowModifyLedger" runat="server" Icon="MonitorEdit" Closable="false"
                Title="修改台账" Width="720" ManageHeight="false" Resizable="false" Hidden="true" Modal="false"
                BodyStyle="background-color:#fff;" BodyPadding="5" Layout="Form" AutoScroll="true">
                <Items>
                    <ext:FormPanel ID="pnlModifyLedgerNative" runat="server" Flex="1" BodyPadding="5" Layout="Column">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="120" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:TextField ID="txtModifyDetailId" runat="server" FieldLabel="送检信息Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtModifyOrderId" runat="server" FieldLabel="显示序号" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtModifyMaterCode" runat="server" FieldLabel="物料编码" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtModifyFrequency" runat="server" FieldLabel="检测频次" LabelAlign="Right"
                                MaxLength="10" Padding="5" ReadOnly="true" Hidden="true"/>
                            <ext:TextField ID="txtModifyBillNo" runat="server" FieldLabel="送检单号" LabelAlign="Right"
                                AllowBlank="False" Editable="false" Padding="5" />
                            <ext:TextField ID="txtModifySpec" runat="server" FieldLabel="规格" LabelAlign="Right"
                                ReadOnly="true" Padding="5" LabelPad="11"/>
                            <ext:TextField ID="txtModifyBarcode" runat="server" FieldLabel="条码号" LabelAlign="Right"
                                ReadOnly="true" Padding="5" />
                            <ext:TextField ID="txtModifyMaterialName" runat="server" FieldLabel="物料名称" LabelAlign="Right"
                                ReadOnly="true" Padding="5" LabelPad="11"/>
                            <ext:TriggerField ID="trfModifySupplierName" runat="server" FieldLabel="供应商" LabelAlign="Right"
                                Editable="false" Padding="5"  >
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectSupplier" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtModifyManufacturerName" runat="server" FieldLabel="生产商" LabelAlign="Right"
                                ReadOnly="true" Padding="5" LabelPad="11"/>
                            <ext:TextField ID="txtModifySupplierId" runat="server" FieldLabel="供应商Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtModifyManufacturerId" runat="server" FieldLabel="生产商Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtModifySendNum" runat="server" FieldLabel="送检数量" LabelAlign="Right"
                                ReadOnly="true" Padding="5" />
                            <ext:TextField ID="txtModifyUnit" runat="server" FieldLabel="单位" LabelAlign="Right"
                                MaxLength="10" Padding="5"  ReadOnly="true" LabelPad="11"/>
                            <ext:TextField ID="txtModifyBatchCode" runat="server" FieldLabel="批次号" LabelAlign="Right"
                                MaxLength="20" Padding="5" ReadOnly="true" />
                            <ext:TextField ID="txtModifyCheckResult" runat="server" FieldLabel="检测结果" LabelAlign="Right"
                                AllowBlank="False" ReadOnly="true" Padding="5" LabelPad="11">
                            </ext:TextField>
                            <ext:TriggerField ID="trfModifyCheckerName" runat="server" FieldLabel="检测人" LabelAlign="Right"
                                Editable="false" AllowBlank="false" Padding="5" IndicatorText="*" IndicatorCls="red-text">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectChecker" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtModifyCheckerId" runat="server" FieldLabel="检测人Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TriggerField ID="trfModifyExtractorName" runat="server" FieldLabel="取样人" LabelAlign="Right"
                                Editable="false" Padding="5" >
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectExtractor" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtModifyExtractorId" runat="server" FieldLabel="取样人Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TriggerField ID="trfModifyReceiverName" runat="server" FieldLabel="接收人" LabelAlign="Right"
                                Editable="false" Padding="5" >
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectReceiver" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtModifyReceiverId" runat="server" FieldLabel="接收人Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TriggerField ID="trfModifyFetcherName" runat="server" FieldLabel="领取人" LabelAlign="Right"
                                Editable="false" Padding="5" LabelPad="11">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectFetcher" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtModifyFetcherId" runat="server" FieldLabel="领取人Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TriggerField ID="trfModifyHandlerName" runat="server" FieldLabel="处置人" LabelAlign="Right"
                                Editable="false" Padding="5">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectHandler" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtModifyHandlerId" runat="server" FieldLabel="处置人Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:DateField ID="dtfModifySendDate" runat="server" FieldLabel="发放时间" LabelAlign="Right" Editable="false" Padding="5" LabelPad="11"/>
                            <ext:DateField ID="dtfModifyReceiveDate" runat="server" FieldLabel="接收时间" LabelAlign="Right" Editable="false" Padding="5" LabelPad="5"/>
                            <ext:DateField ID="dtfModifyReturnDate" runat="server" FieldLabel="返库时间" LabelAlign="Right" Editable="false" Padding="5" LabelPad="11"/>
                            <ext:DateField ID="dtfModifyHandleDate" runat="server" FieldLabel="处置时间" LabelAlign="Right" Editable="false" Padding="5" LabelPad="5"/> 
                            <ext:ComboBox ID="cbxModifyHandleMethod" runat="server" FieldLabel="处置方式" LabelAlign="Right" Padding="5" LabelPad="11">
                                <Items>
                                    <ext:ListItem Text="退库（车间）" Value="退库（车间）">
                                    </ext:ListItem>
                                    <ext:ListItem Text="退回委托方" Value="退回委托方">
                                    </ext:ListItem>
                                    <ext:ListItem Text="废弃" Value="废弃">
                                    </ext:ListItem>
                                </Items>
                            </ext:ComboBox>
                            <ext:TextField ID="txtModifyRemark" runat="server" FieldLabel="备注" LabelAlign="Right"
                                MaxLength="50" Padding="5" LabelPad="5"/>
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{btnModifyKeySave}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                    <ext:FormPanel ID="pnlModifyLedgerCustom" runat="server" Flex="1" BodyPadding="5" Layout="Column">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="120" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                        </Items>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnModifyKeySave" runat="server" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="BtnModifySave_Click">
                                <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnModifyKeyCancel" runat="server" Text="取消" Icon="Cancel">
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
            <ext:Window ID="windowBatchModify" runat="server" Icon="MonitorEdit" Closable="false" Title="批量修改"
                Width="280" Height="195" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="Form">
                <Items> 
                    <ext:FormPanel ID="pnlEditItem" runat="server" Flex="1" BodyPadding="5">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items> 
                            <ext:TextField ID="txtBatchModifyHandlerId" runat="server" FieldLabel="处置人Id" LabelAlign="Right"
                            ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TriggerField ID="trfBatchModifyHandlerName" runat="server" FieldLabel="处置人" LabelAlign="Right"
                                Editable="false" >
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectHandler" />
                                </Listeners>
                            </ext:TriggerField>                           
                            <ext:ComboBox ID="cbxBatchModifyHandleMethod" runat="server" FieldLabel="处置方式" LabelAlign="Right" Editable="false"  >
                                <Items>
                                    <ext:ListItem Text="退库（车间）" Value="退库（车间）">
                                    </ext:ListItem>
                                    <ext:ListItem Text="退回委托方" Value="退回委托方">
                                    </ext:ListItem>
                                    <ext:ListItem Text="废弃" Value="废弃">
                                    </ext:ListItem>
                                </Items>
                            </ext:ComboBox>     
                            <ext:DateField ID="dtfBatchModifyHandleDate" runat="server" FieldLabel="处置时间" LabelAlign="Right" MaxLength="50" Editable="false" />
                            <ext:DateField ID="dtfBatchModifyReturnDate" runat="server" FieldLabel="返库时间" LabelAlign="Right" MaxLength="50" Editable="false" />
                        </Items>
                            <Listeners>
                            <ValidityChange Handler="#{btnBatchModifySave}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnBatchModifySave" runat="server" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="BtnBatchModifySave_Click">
                                <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnBatchModifyCancel" runat="server" Text="取消" Icon="Cancel">
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
            <ext:Hidden ID="txtHiddenSelectCommand" runat="server" />
            <ext:Hidden ID="txtHiddenLedgerId" runat="server" />
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
