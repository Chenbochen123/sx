<%@ page language="C#" autoeventwireup="true" inherits="Manager_RawMaterialQuality_CheckDataAnalysis, App_Web_pghqvuve" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>合格率统计分析</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript" language="javascript">
        var gridPanelRefresh = function () {
            App.direct.LoadReport();
            return false;
        };
    </script>
    <script type="text/javascript">
            //-------厂商绑定-----查询带回弹出框--BEGIN
            var Manager_RawMaterialQuality_QueryFactoryMapping_Request = function (record) {//厂商绑定
                App.trfSupplierName.setValue(record.data.SupplierName);
                App.txtSupplierId.setValue(record.data.SupplierId);
                App.trfManufacturerName.setValue(record.data.ManufacturerName);
                App.txtManufacturerId.setValue(record.data.ManufacturerId);
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
    <asp:Button ID="btnExportSPCSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSPCSubmit_Click" />
    <ext:TextField ID="txtSupplierId" runat="server" Hidden="true"/>
    <ext:TextField ID="txtManufacturerId" runat="server" Hidden="true"/>
    <ext:ResourceManager ID="rmAnalysis" runat="server" />
    <ext:Viewport ID="vwUnit" runat="server" Layout="border">
        <Items>
             <ext:Panel ID="pnlNorth" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="barUnit1">
                        <Items>
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
                            <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                <ToolTips>
                                    <ext:ToolTip ID="ttExport" runat="server" Html="点击导出Excel文件下载" />
                                </ToolTips>
                                <Listeners>
                                    <Click Handler="$('#btnExportSubmit').click();"></Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsMiddle2" />
                            <ext:Button runat="server" Icon="ScriptGo" Text="导出SPC报表" ID="btnExportSPC">
                                <ToolTips>
                                    <ext:ToolTip ID="ttExportSPC" runat="server" Html="点击导出SPC报表Excel文件下载" />
                                </ToolTips>
                                <Listeners>
                                    <Click Handler="$('#btnExportSPCSubmit').click();"></Click>
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
                                    <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="统计起始日期" LabelAlign="Right" Format="yyyy-MM-dd" Flex="1" LabelWidth="90" Editable="false">
                                    </ext:DateField>
                                    <ext:DateField ID="txtEndTime" runat="server" FieldLabel="统计结束日期" LabelAlign="Right" Format="yyyy-MM-dd" Flex="1" LabelWidth="90" Editable="false">
                                    </ext:DateField>
                                    <ext:TriggerField ID="trfSupplierName" runat="server" FieldLabel="供应商" LabelAlign="Right" Flex="1" LabelWidth="90"
                                        Visible="true" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="SelectSupplier" />
                                        </Listeners>
                                    </ext:TriggerField>                               
                                    <ext:TriggerField ID="trfManufacturerName" runat="server" FieldLabel="生产商" LabelAlign="Right" Flex="1" LabelWidth="70"
                                        Visible="true" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="SelectSupplier" />
                                        </Listeners>
                                    </ext:TriggerField>                                
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="HBoxLayout" Padding="5" >
                                <Items>  
                                     <ext:ComboBox runat="server" ID="cbxStandard" FieldLabel="执行标准" LabelAlign="Right" Visible="true" Editable="false" Flex="1" LabelWidth="90">
                                        <DirectEvents>
                                            <Change OnEvent="cbxSeries_Change">
                                                 <EventMask ShowMask="true" />
                                            </Change>
                                        </DirectEvents>
                                    </ext:ComboBox>      
                                    <ext:ComboBox runat="server" ID="cbxSeries" FieldLabel="原材料系列" LabelAlign="Right" Visible="true" Editable="false" Flex="1" LabelWidth="90">
                                        <DirectEvents>
                                            <Change OnEvent="cbxSeries_Change">
                                                 <EventMask ShowMask="false" />
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
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="pnlCenter" runat="server" Region="Center" Frame="true" Layout="Fit"
                MarginsSummary="0 5 0 5">
                <Items>
                    <ext:GridPanel ID="pnlAnalysis" runat="server" MarginsSummary="0 5 5 5">
                      <Store>
                           <ext:Store ID="store" runat="server">
                                <Model>
                                    <ext:Model ID="model" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="MaterialCode" />
                                            <ext:ModelField Name="SpecId" />
                                            <ext:ModelField Name="SupplierId" />
                                            <ext:ModelField Name="ManufacturerId" />
                                            <ext:ModelField Name="原材料名称" />
                                            <ext:ModelField Name="规格" />
                                            <ext:ModelField Name="供应商" />
                                            <ext:ModelField Name="生产商" />
                                            <ext:ModelField Name="送检批次" />
                                            <ext:ModelField Name="合格批次" />
                                            <ext:ModelField Name="送检重量" />
                                            <ext:ModelField Name="合格重量" />
                                            <ext:ModelField Name="批次合格率" />
                                            <ext:ModelField Name="重量合格率" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                           </ext:Store>
                      </Store>
                      <ColumnModel ID="colModel1" runat="server">
                          <Columns>
                              <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />
                              <ext:Column ID="columnMaterialCode" runat="server" Text="MaterialCode" DataIndex="MaterialCode" Visible="false"/>
                              <ext:Column ID="columnSpecId" runat="server" Text="SpecId" DataIndex="SpecId" Visible="false"/>
                              <ext:Column ID="columnSupplierId" runat="server" Text="SupplierId" DataIndex="SupplierId" Visible="false"/>
                              <ext:Column ID="columnManufacturerId" runat="server" Text="ManufacturerId" DataIndex="ManufacturerId" Visible="false"/>
                              <ext:Column ID="columnMaterialName" runat="server" Text="原材料名称" DataIndex="原材料名称" />
                              <ext:Column ID="columnSpecName" runat="server" Text="规格" DataIndex="规格" />
                              <ext:Column ID="columnSupplierName" runat="server" Text="供应商" DataIndex="供应商" />
                              <ext:Column ID="columnManufacturerName" runat="server" Text="生产商" DataIndex="生产商" />
                              <ext:Column ID="columnSendBatch" runat="server" Text="送检批次" DataIndex="送检批次" />
                              <ext:Column ID="columnPassBatch" runat="server" Text="合格批次" DataIndex="合格批次" />
                              <ext:Column ID="columnSendWeight" runat="server" Text="送检重量" DataIndex="送检重量" />
                              <ext:Column ID="columnPassWeight" runat="server" Text="合格重量" DataIndex="合格重量" />
                              <ext:Column ID="columnBatchRate" runat="server" Text="批次合格率" DataIndex="批次合格率" />
                              <ext:Column ID="columnWeightRate" runat="server" Text="重量合格率" DataIndex="重量合格率" />
                          </Columns>
                      </ColumnModel>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
