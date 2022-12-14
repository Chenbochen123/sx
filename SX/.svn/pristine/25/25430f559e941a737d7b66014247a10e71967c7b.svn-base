<%@ page language="C#" autoeventwireup="true" inherits="Manager_RawMaterialQuality_SampleLedger, App_Web_drvpsf3a" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>样品台账维护</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript" language="javascript">
        var gridPanelRefresh = function () {
            App.store.currentPage = 1;
            App.direct.ReloadLedgerList();
            return false;
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
         var Manager_RawMaterialQuality_QueryMaterialChkDetail_Request = function (record) {//质检信息绑定
             if (!App.windowAddLedger.hidden) {
                 App.trfAddBillNo.setValue(record.data.BillNo);
                 App.txtAddBarcode.setValue(record.data.Barcode);
                 App.txtAddOrderId.setValue(record.data.OrderId);
                 App.txtAddMaterCode.setValue(record.data.MaterCode);
                 App.txtHiddenMaterialChkDetailId.setValue(record.data.ObjID);
                 App.txtAddUnit.setValue("千克");
                 App.direct.LoadAddCheckDetail();
             }
         }
         var SelectMaterial = function () {//质检信息查询
             Ext.create("Ext.window.Window", {//质检信息绑定查询带回窗体
                 id: "Manager_RawMaterialQuality_QueryMaterialChkDetail_Window",
                 height: 540,
                 hidden: true,
                 width: 600,
                 html: "<iframe src='QueryMaterialChkDetail.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
                 bodyStyle: "background-color: #fff;",
                 closable: true,
                 title: "请选择送检信息",
                 modal: true
             })
             App.Manager_RawMaterialQuality_QueryMaterialChkDetail_Window.show();
         }
         //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        var Manager_RawMaterialQuality_QueryRawMaterial_Request = function (record) {//物料信息返回值处理
            App.trfManualAddMaterialName.setValue(record.data.MaterialName);
            App.txtManualAddMaterialCode.setValue(record.data.MaterialCode);
            App.direct.LoadManualAddCheckDetail();
        }

        var ManualSelectMaterial = function () {
            Ext.create("Ext.window.Window", {//物料带窗体
                id: "Manager_RawMaterialQuality_QueryRawMaterial_Window",
                height: 460,
                hidden: true,
                width: 360,
                html: "<iframe src='QueryRawMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
                bodyStyle: "background-color: #fff;",
                closable: true,
                title: "请选择原材料",
                modal: true
            })
            App.Manager_RawMaterialQuality_QueryRawMaterial_Window.show();
        }
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //-------人员绑定-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryQmcUser_Request = function (record) {//人员绑定
            var command = App.txtHiddenSelectCommand.getValue();
            switch (command) {
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
                        if (!App.windowManualAddLedger.hidden) {
                            App.trfManualAddExtractorName.setValue(record.data.UserName);
                            App.txtManualAddExtractorId.setValue(record.data.HRCode);
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
                        if (!App.windowManualAddLedger.hidden) {
                            App.trfManualAddReceiverName.setValue(record.data.UserName);
                            App.txtManualAddReceiverId.setValue(record.data.HRCode);
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
                        if (!App.windowManualAddLedger.hidden) {
                            App.trfManualAddFetcherName.setValue(record.data.UserName);
                            App.txtManualAddFetcherId.setValue(record.data.HRCode);
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
                        if (!App.windowManualAddLedger.hidden) {
                            App.trfManualAddHandlerName.setValue(record.data.UserName);
                            App.txtManualAddHandlerId.setValue(record.data.HRCode);
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
        });
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //-------厂商绑定-----查询带回弹出框--BEGIN
        var Manager_RawMaterialQuality_QueryFactoryMapping_Request = function (record) {//厂商绑定
            if (!App.windowModifyLedger.hidden) {
                App.trfModifySupplierName.setValue(record.data.SupplierName);
                App.txtModifySampleSource.setValue(record.data.SupplierName);
                App.txtModifySupplierId.setValue(record.data.SupplierId);
                App.txtModifyFactoryCode.setValue(record.data.SupplierERPCode);
                App.txtModifyManufacturerName.setValue(record.data.ManufacturerName);
                App.txtModifyManufacturerId.setValue(record.data.ManufacturerId);
            }
            if (!App.windowAddLedger.hidden) {
                App.trfAddSupplierName.setValue(record.data.SupplierName);
                App.txtAddSampleSource.setValue(record.data.SupplierName);
                App.txtAddSupplierId.setValue(record.data.SupplierId);
                App.txtAddFactoryCode.setValue(record.data.SupplierERPCode);
                App.txtAddManufacturerName.setValue(record.data.ManufacturerName);
                App.txtAddManufacturerId.setValue(record.data.ManufacturerId);
            }
            if (!App.windowManualAddLedger.hidden) {
                App.trfManualAddSupplierName.setValue(record.data.SupplierName);
                App.txtManualAddSampleSource.setValue(record.data.SupplierName);
                App.txtManualAddSupplierId.setValue(record.data.SupplierId);
                App.txtManualAddFactoryCode.setValue(record.data.SupplierERPCode);
                App.txtManualAddManufacturerName.setValue(record.data.ManufacturerName);
                App.txtManualAddManufacturerId.setValue(record.data.ManufacturerId);
            }
        }
        var SelectSupplier = function () {//厂商绑定查询
            var supplierName = '';
            if (!App.windowModifyLedger.hidden) {
                supplierName = App.trfModifySupplierName.value;
            }
            if (!App.windowAddLedger.hidden) {
                supplierName = App.trfAddSupplierName.value;
            }
            if (!App.windowManualAddLedger.hidden) {
                supplierName = App.trfManualAddSupplierName.value;
            }
            if (supplierName == undefined)
            {
                supplierName = '';
            }
            Ext.create("Ext.window.Window", {//厂商绑定查询带回窗体
                id: "Manager_RawMaterialQuality_QueryFactoryMapping_Window",
                height: 460,
                hidden: true,
                width: 360,
                html: "<iframe src='QueryFactoryMapping.aspx" + "?SupplierName=" + supplierName + "' width=100% height=100% scrolling=no  frameborder=0></iframe>",
                bodyStyle: "background-color: #fff;",
                closable: true,
                title: "请选择厂商关系",
                modal: true
            });
            App.Manager_RawMaterialQuality_QueryFactoryMapping_Window.show();
        }
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //批量生成样品标签
        var btnGenerateLabel_Click = function () {
            var section = App.pnlLedger.getView().getSelectionModel().getSelection();
            if (section && section.length == 0) {
                Ext.Msg.alert("提示", '至少要选择一条台账！');
            }
            else {
                    Ext.Msg.confirm("提示", '确定要批量生成吗？', function (btn) {
                    if (btn != "yes") {
                        return;
                    }
                    App.direct.GetSampleIdSequence({
                        success: function (result) {
                            var url = "RawMaterialQuality/SampleLabelPrint.aspx?Strs=" + result;
                            var tabid = "Manager_RawMaterialQuality_SampleLabelPrint";
                            var tabp = parent.App.mainTabPanel;
                            var tab = tabp.getComponent(tabid);
                            if (tab) {
                                tab.close();
                            }
                            var title = "样品标签打印";
                            parent.addTab(tabid, title, url, true);
                            parent.refresh("");
                        },
                        failure: function (errorMsg) {
                            Ext.Msg.alert('操作', errorMsg);
                        }
                    });
                });
            }
        }
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
                                    <ext:ToolTip ID="ttAdd" runat="server" Html="点击从送检单录入新台账" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="btn_add_Click">
                                        <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_begin2" />
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
                            <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                            <ext:Button runat="server" Icon="ApplicationAdd" Text="手动添加" ID="btnManualAdd">
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip1" runat="server" Html="点击手动添加新台账" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="btn_manualAdd_Click">
                                        <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_begin1" />
                            <ext:Button runat="server" Icon="Script" Text="生成标签" ID="btnGenerateLabel">
                                <ToolTips>
                                    <ext:ToolTip ID="ttGenereateLabel" runat="server" Html="点击生成选中台账的电子标签" />
                                </ToolTips>
                                <Listeners>
                                    <Click Handler="btnGenerateLabel_Click();" />
                                </Listeners>
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
                                    <ext:ToolTip ID="ttExport" runat="server" Html="点击将查询结果导出为Excel文件下载" />
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
                                    <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="录入起始时间" LabelAlign="Right" Format="yyyy-MM-dd" Flex="1" LabelWidth="100">
                                    </ext:DateField>
                                    <ext:DateField ID="txtEndTime" runat="server" FieldLabel="录入结束时间" LabelAlign="Right" Format="yyyy-MM-dd"  Flex="1" LabelWidth="100">
                                    </ext:DateField>
                                    <ext:TextField ID="txtBillNo" runat="server" FieldLabel="送检单号" LabelAlign="Right"
                                        Visible="true"  Flex="1" LabelWidth="80">
                                    </ext:TextField>                               
                                    <ext:TextField ID="txtBarcode" runat="server" FieldLabel="条码号" LabelAlign="Right"
                                        Visible="true"  Flex="1" LabelWidth="80">
                                    </ext:TextField>                               
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
                                        <ext:ModelField Name="BillDetailId" />
                                        <ext:ModelField Name="OrderId" />
                                        <ext:ModelField Name="MaterialCode" />
                                        <ext:ModelField Name="样品名称" />
                                        <ext:ModelField Name="规格" />   
                                        <ext:ModelField Name="生产商" />
                                        <ext:ModelField Name="供应商" />
                                        <ext:ModelField Name="厂家编号" />
                                        <ext:ModelField Name="条码号" />
                                        <ext:ModelField Name="批次号" />
                                        <ext:ModelField Name="检测频次" />
                                        <ext:ModelField Name="样品数量" />
                                        <ext:ModelField Name="样品编号" />
                                        <ext:ModelField Name="样品状态" />  
                                        <ext:ModelField Name="取样人" />
                                        <ext:ModelField Name="接收人" />
                                        <ext:ModelField Name="接收时间" Type="Date"/>
                                        <ext:ModelField Name="发放时间" Type="Date"/>
                                        <ext:ModelField Name="领取人" />
                                        <ext:ModelField Name="检验结果" />
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
                              <ext:Column ID="ledgercolumn0" runat="server" Text="LedgerId" DataIndex="LedgerId" Visible="false"/>
                              <ext:Column ID="ledgercolumn1" runat="server" Text="BillDetailId" DataIndex="BillDetailId" Visible="false"/>
                              <ext:Column ID="ledgercolumn2" runat="server" Text="OrderId" DataIndex="OrderId" Visible="false"/>
                              <ext:Column ID="ledgercolumn3" runat="server" Text="MaterialCode" DataIndex="MaterialCode" Visible="false"/>
                              <ext:Column ID="ledgercolumn4" runat="server" Text="样品名称" DataIndex="样品名称" />
                              <ext:Column ID="ledgercolumn41" runat="server" Text="规格" DataIndex="规格" />
                              <ext:Column ID="ledgercolumn5" runat="server" Text="生产商" DataIndex="生产商" />
                              <ext:Column ID="ledgercolumn6" runat="server" Text="供应商" DataIndex="供应商" />
                              <ext:Column ID="ledgercolumn7" runat="server" Text="厂家编号" DataIndex="厂家编号" />
                              <ext:Column ID="ledgercolumn8" runat="server" Text="条码号" DataIndex="条码号" />
                              <ext:Column ID="ledgercolumn81" runat="server" Text="批次号" DataIndex="批次号" />
                              <ext:Column ID="ledgercolumn82" runat="server" Text="检测频次" DataIndex="检测频次" />
                              <ext:Column ID="ledgercolumn9" runat="server" Text="样品数量" DataIndex="样品数量" />
                              <ext:Column ID="ledgercolumn10" runat="server" Text="样品编号" DataIndex="样品编号" />
                              <ext:Column ID="ledgercolumn11" runat="server" Text="样品状态" DataIndex="样品状态" />
                              <ext:Column ID="ledgercolumn12" runat="server" Text="取样人" DataIndex="取样人" />
                              <ext:Column ID="ledgercolumn13" runat="server" Text="接收人" DataIndex="接收人" />
                              <ext:DateColumn ID="ledgercolumn14" runat="server" Text="接收时间" DataIndex="接收时间" Format="yyyy-MM-dd"/>
                              <ext:DateColumn ID="ledgercolumn15" runat="server" Text="发放时间" DataIndex="发放时间" Format="yyyy-MM-dd"/>
                              <ext:Column ID="ledgercolumn16" runat="server" Text="领取人" DataIndex="领取人" />
                              <ext:DateColumn ID="ledgercolumn18" runat="server" Text="返库时间" DataIndex="返库时间" Format="yyyy-MM-dd"/>
                              <ext:DateColumn ID="ledgercolumn19" runat="server" Text="处置时间" DataIndex="处置时间" Format="yyyy-MM-dd"/>
                              <ext:Column ID="ledgercolumn20" runat="server" Text="处置方式" DataIndex="处置方式" />
                              <ext:Column ID="ledgercolumn21" runat="server" Text="处置人" DataIndex="处置人" />
                              <ext:DateColumn ID="ledgercolumn22" runat="server" Text="记录时间" DataIndex="记录时间" Format="yyyy-MM-dd HH:mm:ss"/>
                              <ext:Column ID="ledgercolumn23" runat="server" Text="备注" DataIndex="备注" />
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
                            <ext:CheckboxSelectionModel ID="ledgerSelectionModel" runat="server" Mode="Simple"/>
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
            <ext:Window ID="windowAddLedger" runat="server" Icon="MonitorEdit" Closable="false"
                Title="录入台账" Width="680" Height="485" Resizable="false" Hidden="true" Modal="false"
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
                            <ext:TextField ID="txtAddDetailId" runat="server" FieldLabel="送检信息Id" LabelAlign="Right"
                                 Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtAddOrderId" runat="server" FieldLabel="显示序号" LabelAlign="Right"
                                 Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtAddMaterCode" runat="server" FieldLabel="物料编码" LabelAlign="Right"
                                 Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtAddFactoryCode" runat="server" FieldLabel="厂家编号" LabelAlign="Right"
                                Hidden="true" Enabled="true" Padding="5"/>
                            <ext:Triggerfield ID="trfAddBillNo" runat="server" FieldLabel="检测任务" LabelAlign="Right"
                                AllowBlank="False" Editable="false" Padding="5" IndicatorText="*" IndicatorCls="red-text">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectMaterial" />
                                </Listeners>
                            </ext:Triggerfield>
                            <ext:TextField ID="txtAddNoticeNo" runat="server" FieldLabel="通知单号" LabelAlign="Right"
                                ReadOnly="true" Padding="5" />
                            <ext:TextField ID="txtAddMaterialName" runat="server" FieldLabel="样品名称" LabelAlign="Right"
                                ReadOnly="true" Padding="5" />
                            <ext:TextField ID="txtAddBarcode" runat="server" FieldLabel="条码号" LabelAlign="Right"
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
                            <ext:TextField ID="txtAddSampleSource" runat="server" FieldLabel="样品来源" LabelAlign="Right" MaxLength="50"
                                Padding="5" /> 
                            <ext:TextField ID="txtAddBatchCode" runat="server" FieldLabel="批次号" LabelAlign="Right"
                                Padding="5" LabelPad="11" ReadOnly="true"/>
                            <ext:TextField ID="txtAddSampleCode" runat="server" FieldLabel="样品编号" LabelAlign="Right"
                                Padding="5" MaxLength="25"/>         
                            <ext:Checkbox ID="cbxAddIsFlow" runat="server" FieldLabel="自动流水" LabelAlign="Right" Padding="5" LabelPad="11" OnDirectCheck="cbxAddIsFlow_checked">
                            </ext:Checkbox>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnAddKeySave}.setDisabled(!valid);" />
                            </Listeners>
                            </ext:FormPanel>
                        <ext:FormPanel ID="pnlAddLedgerCustom" runat="server" Flex="1" Layout="Column" BodyPadding="5" Y="2">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="120" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:TextField ID="txtAddSampleNum" runat="server" FieldLabel="进货数量" LabelAlign="Right" MaxLength="10" ReadOnly="true"
                                Padding="5" LabelPad="5"/>
                            <ext:TextField ID="txtAddUnit" runat="server" FieldLabel="单位" LabelAlign="Right" Padding="5" MaxLength="10" LabelPad="11" ReadOnly="true" Text="千克"/>
                             <ext:ComboBox ID="cbxAddSpec" runat="server" Editable="false" FieldLabel="规格" LabelAlign="Right" Padding="5" MaxLength="10" LabelPad="5">
                                <Items>
                                </Items>
                            </ext:ComboBox>
                            <ext:ComboBox ID="cbxAddStatus" runat="server" FieldLabel="样品状态" LabelAlign="Right" Padding="5" MaxLength="10" LabelPad="11">
                                <SelectedItems>
                                    <ext:ListItem Text="正常" Value="正常">
                                    </ext:ListItem>
                                </SelectedItems>
                                <Items>
                                    <ext:ListItem Text="正常" Value="正常">
                                    </ext:ListItem>
                                    <ext:ListItem Text="异常" Value="异常">
                                    </ext:ListItem>
                                </Items>
                            </ext:ComboBox>
                            <ext:TriggerField ID="trfAddExtractorName" runat="server" FieldLabel="取样人" LabelAlign="Right"
                                Editable="false" Padding="5" LabelPad="5">
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
                                Editable="false" Padding="5"  LabelPad="11">
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
                                Editable="false" Padding="5" LabelPad="5">
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
                                Editable="false" Padding="5"  LabelPad="11">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectHandler" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtAddHandlerId" runat="server" FieldLabel="处置人Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:DateField ID="dtfAddSendDate" runat="server" FieldLabel="发放时间" LabelAlign="Right" Editable="false" Padding="5" LabelPad="5"/>         
                            <ext:DateField ID="dtfAddReceiveDate" runat="server" FieldLabel="接收时间" LabelAlign="Right" Editable="false" Padding="5"  LabelPad="11"/>                 
                            <ext:DateField ID="dtfAddReturnDate" runat="server" FieldLabel="返库时间" LabelAlign="Right" Editable="false" Padding="5" LabelPad="5"/>
                            <ext:DateField ID="dtfAddHandleDate" runat="server" FieldLabel="处置时间" LabelAlign="Right" Editable="false" Padding="5"  LabelPad="11"/>
                              <ext:ComboBox ID="cbxAddHandleMethod" runat="server" FieldLabel="处置方式" LabelAlign="Right" Padding="5" LabelPad="5">
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
                                MaxLength="50" Padding="5"  LabelPad="11"/>
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
                <ext:Window ID="windowManualAddLedger" runat="server" Icon="MonitorEdit" Closable="false"
                Title="手动录入台账" Width="680" Height="485" Resizable="false" Hidden="true" Modal="false"
                BodyStyle="background-color:#fff;" BodyPadding="5" Layout="Form" AutoScroll="true">
                <Items>
                    <ext:FormPanel ID="pnlManualAddLedgerNative" runat="server" Flex="1" BodyPadding="5" Layout="Column">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="120" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items> 
                            <ext:TextField ID="txtManualAddMaterialCode" runat="server" FieldLabel="物料编码" LabelAlign="Right"
                                 Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtManualAddBillNo" runat="server" FieldLabel="送检单号" LabelAlign="Right"
                                 Padding="5"  IndicatorText="*" IndicatorCls="red-text" AllowBlank="False" MaxLength="36"/>
                            <ext:TextField ID="txtManualAddBarcode" runat="server" FieldLabel="条码号" LabelAlign="Right"
                                 Padding="5" LabelPad="5" IndicatorText="*" IndicatorCls="red-text" AllowBlank="False" MaxLength="24"/>
                            <ext:TriggerField ID="trfManualAddMaterialName" runat="server" FieldLabel="送检原材料" LabelAlign="Right" 
                                Editable="false" Padding="5"  IndicatorText="*" IndicatorCls="red-text" AllowBlank="False">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="ManualSelectMaterial" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtManualAddBatchCode" runat="server" FieldLabel="批次号" LabelAlign="Right"
                                 Padding="5" LabelPad="5" IndicatorText="*" IndicatorCls="red-text" AllowBlank="False" MaxLength="20"/>
                            <ext:TriggerField ID="trfManualAddSupplierName" runat="server" FieldLabel="供应商" LabelAlign="Right"
                                Editable="false" Padding="5"  LabelPad="5">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectSupplier" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtManualAddManufacturerName" runat="server" FieldLabel="生产商" LabelAlign="Right"
                                ReadOnly="true" Padding="5" LabelPad="11"/>
                            <ext:TextField ID="txtManualAddSupplierId" runat="server" FieldLabel="供应商Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtManualAddManufacturerId" runat="server" FieldLabel="生产商Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtManualAddSampleSource" runat="server" FieldLabel="样品来源" LabelAlign="Right" MaxLength="50"
                                Padding="5" LabelPad="5"/> 
                            <ext:TextField ID="txtManualAddFactoryCode" runat="server" FieldLabel="厂家编号" LabelAlign="Right"
                                Padding="5" MaxLength="25" LabelPad="11"/>
                            <ext:TextField ID="txtManualAddSampleCode" runat="server" FieldLabel="样品编号" LabelAlign="Right"
                                Padding="5" MaxLength="25" LabelPad="5"/>         
                            <ext:Checkbox ID="cbxManualAddIsFlow" runat="server" FieldLabel="自动流水" LabelAlign="Right" Padding="5" LabelPad="11" OnDirectCheck="cbxManualAddIsFlow_checked">
                            </ext:Checkbox>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnManualAddKeySave}.setDisabled(!valid);" />
                            </Listeners>
                            </ext:FormPanel>
                        <ext:FormPanel ID="pnlManualAddLedgerCustom" runat="server" Flex="1" Layout="Column" BodyPadding="5" Y="3">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="120" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:TextField ID="txtManualAddSampleNum" runat="server" FieldLabel="进货数量" LabelAlign="Right" MaxLength="10"
                                Padding="5" LabelPad="5"/>
                             <ext:TextField ID="txtManualAddUnit" runat="server" FieldLabel="单位" LabelAlign="Right" Padding="5" MaxLength="10" LabelPad="11" ReadOnly="true" Text="千克"/>
                             <ext:ComboBox ID="cbxManualAddSpec" runat="server" Editable="false" FieldLabel="规格" LabelAlign="Right" Padding="5" MaxLength="10" LabelPad="5">
                                <Items>
                                </Items>
                            </ext:ComboBox>
                            <ext:ComboBox ID="cbxManualAddStatus" runat="server" FieldLabel="样品状态" LabelAlign="Right" Padding="5" MaxLength="10" LabelPad="11">
                                <SelectedItems>
                                    <ext:ListItem Text="正常" Value="正常">
                                    </ext:ListItem>
                                </SelectedItems>
                                <Items>
                                    <ext:ListItem Text="正常" Value="正常">
                                    </ext:ListItem>
                                    <ext:ListItem Text="异常" Value="异常">
                                    </ext:ListItem>
                                </Items>
                            </ext:ComboBox>
                            <ext:TriggerField ID="trfManualAddExtractorName" runat="server" FieldLabel="取样人" LabelAlign="Right"
                                Editable="false" Padding="5" LabelPad="5">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectExtractor" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtManualAddExtractorId" runat="server" FieldLabel="取样人Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TriggerField ID="trfManualAddReceiverName" runat="server" FieldLabel="接收人" LabelAlign="Right"
                                Editable="false" Padding="5"  LabelPad="11">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectReceiver" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtManualAddReceiverId" runat="server" FieldLabel="接收人Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                             <ext:TriggerField ID="trfManualAddFetcherName" runat="server" FieldLabel="领取人" LabelAlign="Right"
                                Editable="false" Padding="5" LabelPad="5">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectFetcher" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtManualAddFetcherId" runat="server" FieldLabel="领取人Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TriggerField ID="trfManualAddHandlerName" runat="server" FieldLabel="处置人" LabelAlign="Right"
                                Editable="false" Padding="5"  LabelPad="11">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectHandler" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtManualAddHandlerId" runat="server" FieldLabel="处置人Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:DateField ID="dtfManualAddSendDate" runat="server" FieldLabel="发放时间" LabelAlign="Right" Editable="false" Padding="5" LabelPad="5"/>         
                            <ext:DateField ID="dtfManualAddReceiveDate" runat="server" FieldLabel="接收时间" LabelAlign="Right" Editable="false" Padding="5"  LabelPad="11"/>                 
                            <ext:DateField ID="dtfManualAddReturnDate" runat="server" FieldLabel="返库时间" LabelAlign="Right" Editable="false" Padding="5" LabelPad="5"/>
                            <ext:DateField ID="dtfManualAddHandleDate" runat="server" FieldLabel="处置时间" LabelAlign="Right" Editable="false" Padding="5"  LabelPad="11"/>
                              <ext:ComboBox ID="cbxManualAddHandleMethod" runat="server" FieldLabel="处置方式" LabelAlign="Right" Padding="5" LabelPad="5">
                                <Items>
                                    <ext:ListItem Text="退库（车间）" Value="退库（车间）">
                                    </ext:ListItem>
                                    <ext:ListItem Text="退回委托方" Value="退回委托方">
                                    </ext:ListItem>
                                    <ext:ListItem Text="废弃" Value="废弃">
                                    </ext:ListItem>
                                </Items>
                            </ext:ComboBox>
                            <ext:TextField ID="txtManualAddRemark" runat="server" FieldLabel="备注" LabelAlign="Right"
                                MaxLength="50" Padding="5"  LabelPad="11"/>
                        </Items>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnManualAddKeySave" runat="server" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="BtnManualAddSave_Click">
                                <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnManualAddCancel" runat="server" Text="取消" Icon="Cancel">
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
                Title="修改台账" Width="680" Height="485" Resizable="false" Hidden="true" Modal="false"
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
                                Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtModifyOrderId" runat="server" FieldLabel="显示序号" LabelAlign="Right"
                                Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtModifyMaterCode" runat="server" FieldLabel="物料编码" LabelAlign="Right"
                                Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtModifyFactoryCode" runat="server" FieldLabel="厂家编号" LabelAlign="Right"
                                Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtModifyBillNo" runat="server" FieldLabel="送检单号" LabelAlign="Right"
                                AllowBlank="False" ReadOnly="true" Padding="5" />
                            <ext:TextField ID="txtModifyBarcode" runat="server" FieldLabel="条码号" LabelAlign="Right"
                                ReadOnly="true" Padding="5" />
                            <ext:TextField ID="txtModifyMaterialName" runat="server" FieldLabel="样品名称" LabelAlign="Right"
                                ReadOnly="true" Padding="5" />
                             <ext:TextField ID="txtModifyBatchCode" runat="server" FieldLabel="批次号" LabelAlign="Right"
                                Padding="5" ReadOnly="true"/>
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
                                ReadOnly="true" Padding="5" />
                            <ext:TextField ID="txtModifySupplierId" runat="server" FieldLabel="供应商Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtModifyManufacturerId" runat="server" FieldLabel="生产商Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TextField ID="txtModifySampleSource" runat="server" FieldLabel="样品来源" LabelAlign="Right" MaxLength="50"
                                Padding="5" />
                            <ext:TextField ID="txtModifyNoticeNo" runat="server" FieldLabel="通知单号" LabelAlign="Right"
                                ReadOnly="true" Padding="5" />
                            <ext:TextField ID="txtModifySampleCode" runat="server" FieldLabel="样品编号" LabelAlign="Right"
                                Padding="5" MaxLength="25"/>
                            <ext:Checkbox ID="cbxModifyIsFlow" runat="server" FieldLabel="自动流水" LabelAlign="Right" Padding="5" LabelPad="11" OnDirectCheck="cbxModifyIsFlow_checked">
                            </ext:Checkbox>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnModifyKeySave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                        <ext:FormPanel ID="pnlModifyLedgerCustom" runat="server" Flex="1"  Layout="Column" BodyPadding="5" Y="2">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="120" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:TextField ID="txtModifySampleNum" runat="server" FieldLabel="进货数量" LabelAlign="Right" MaxLength="10"
                                Padding="5" LabelPad="5"/>
                            <ext:TextField ID="txtModifyUnit" runat="server" FieldLabel="单位" LabelAlign="Right" Padding="5" MaxLength="10" LabelPad="11" ReadOnly="true" Text="千克"/>  
                            <ext:ComboBox ID="cbxModifySpec" runat="server" Editable="false" FieldLabel="规格" LabelAlign="Right" Padding="5" MaxLength="10" LabelPad="5">
                                <Items>
                                </Items>
                            </ext:ComboBox>                    
                            <ext:ComboBox ID="cbxModifyStatus" runat="server" FieldLabel="样品状态" LabelAlign="Right" Padding="5" MaxLength="10" LabelPad="5" >
                                <SelectedItems>
                                    <ext:ListItem Text="正常" Value="正常">
                                    </ext:ListItem>
                                </SelectedItems>
                                <Items>
                                    <ext:ListItem Text="正常" Value="正常">
                                    </ext:ListItem>
                                    <ext:ListItem Text="异常" Value="异常">
                                    </ext:ListItem>
                                </Items>
                            </ext:ComboBox>
                            <ext:TriggerField ID="trfModifyExtractorName" runat="server" FieldLabel="取样人" LabelAlign="Right"
                                Editable="false" Padding="5">
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
                                Editable="false" Padding="5">
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
                                Editable="false" Padding="5">
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
                                Editable="false" Padding="5" >
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectHandler" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtModifyHandlerId" runat="server" FieldLabel="处置人Id" LabelAlign="Right"
                                ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:DateField ID="dtfModifySendDate" runat="server" FieldLabel="发放时间" LabelAlign="Right" Editable="false" Padding="5" />
                            <ext:DateField ID="dtfModifyReceiveDate" runat="server" FieldLabel="接收时间" LabelAlign="Right" Editable="false" Padding="5" />
                            <ext:DateField ID="dtfModifyReturnDate" runat="server" FieldLabel="返库时间" LabelAlign="Right" Editable="false" Padding="5"/>
                            <ext:DateField ID="dtfModifyHandleDate" runat="server" FieldLabel="处置时间" LabelAlign="Right" Editable="false" Padding="5" /> 
                            <ext:ComboBox ID="cbxModifyHandleMethod" runat="server" FieldLabel="处置方式" LabelAlign="Right" Padding="5">
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
                                MaxLength="50" Padding="5" />
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
            <ext:Hidden ID="txtHiddenMaterialChkDetailId" runat="server" />
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
