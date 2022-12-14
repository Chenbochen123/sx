<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckRubberCardPrint.aspx.cs"
    Inherits="Manager_RubberQuality_Manage_CheckRubberCardPrint" %>

<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="ResourceManager1" />
    <ext:TaskManager runat="server" ID="TaskManager">
        <Tasks>
            <ext:Task TaskID="servertime" Interval="60000">
                <DirectEvents>
                    <Update OnEvent="RefreshTime">
                    </Update>
                </DirectEvents>
            </ext:Task>
        </Tasks>
    </ext:TaskManager>
    <ext:Hidden runat="server" ID="HiddenServerTime" />
    <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" ID="PanelNorth" Region="North">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ToolbarNorth">
                        <Items>
                            <ext:Button runat="server" ID="ButtonNorthQuery" Text="查询" Icon="Magnifier">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthQuery_Click">
                                        <EventMask ShowMask="true" Target="Page" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthExport" Text="导出" Icon="PageExcel">
                                <DirectEvents>
                                    <Click IsUpload="true" OnEvent="ButtonNorthExport_Click">
                                        <ExtraParams>
                                            <ext:Parameter Name="fields" Value="#{ModelCenterCopy}.getFields()" Mode="Raw" />
                                            <ext:Parameter Name="records" Value="#{StoreCenterCopy}.getRecordsValues({ excludeId: true })"
                                                Mode="Raw" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthPrintView" Text="打印预览" Icon="Printer">
                                <DirectEvents>
                                    <Click Before="return ButtonNorthPrintView_BeforeClick();" OnEvent="ButtonNorthPrintView_Click">
                                        <ExtraParams>
                                            <ext:Parameter Name="fields" Value="#{ModelCenterCopy}.getFields()" Mode="Raw" />
                                            <ext:Parameter Name="records" Value="#{StoreCenterCopy}.getRecordsValues({ excludeId: false })"
                                                Mode="Raw" />
                                        </ExtraParams>
                                        <EventMask ShowMask="true" Target="Page" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel runat="server" ID="PanelNorthQuery" Layout="ColumnLayout">
                        <Items>
                            <ext:DateField runat="server" ID="DateFieldNorthPlanDate" Format="yyyy-MM-dd" FieldLabel="生产日期"
                                LabelAlign="Right" ColumnWidth="0.3" AllowBlank="false" Editable="false" EmptyText="请选择..."
                                InputWidth="110" LabelWidth="70">
                                <DirectEvents>
                                    <Change OnEvent="DateFieldNorthPlanDate_Change">
                                        <EventMask ShowMask="true" Target="Page" />
                                    </Change>
                                </DirectEvents>
                            </ext:DateField>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthEquipCode" FieldLabel="生产机台" ColumnWidth="0.4"
                                LabelAlign="Right" AllowBlank="false" Editable="false" EmptyText="请选择..." InputWidth="150"
                                MatchFieldWidth="false" LabelWidth="70">
                                <ListConfig Width="150" />
                                <DirectEvents>
                                    <Change OnEvent="ComboBoxNorthEquipCode_Change">
                                        <EventMask ShowMask="true" Target="Page" />
                                    </Change>
                                </DirectEvents>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthShiftId" FieldLabel="生产班次" ColumnWidth="0.3"
                                LabelAlign="Right" AllowBlank="false" Editable="false" EmptyText="请选择..." InputWidth="70"
                                MatchFieldWidth="false" LabelWidth="70">
                                <ListConfig Width="70" />
                                <DirectEvents>
                                    <Change OnEvent="ComboBoxNorthShiftId_Change">
                                        <EventMask ShowMask="true" Target="Page" />
                                    </Change>
                                </DirectEvents>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthZJSId" FieldLabel="主机手" ColumnWidth="0.3"
                                LabelAlign="Right" AllowBlank="false" Editable="false" EmptyText="请选择..." InputWidth="120"
                                MatchFieldWidth="false" LabelWidth="70">
                                <ListConfig Width="120" />
                                <DirectEvents>
                                    <Change OnEvent="ComboBoxNorthZJSId_Change">
                                        <EventMask ShowMask="true" Target="Page" />
                                    </Change>
                                </DirectEvents>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthMaterCode" FieldLabel="物料名称" ColumnWidth="0.4"
                                LabelAlign="Right" AllowBlank="false" EmptyText="请选择..." Editable="false" InputWidth="200"
                                LabelWidth="70" MatchFieldWidth="false">
                                <ListConfig Width="200" />
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" Qtip="查询" />
                                    <ext:FieldTrigger Icon="Clear" Qtip="清除" />
                                </Triggers>
                                <DirectEvents>
                                    <TriggerClick OnEvent="ComboBoxNorthMaterCode_TriggerClick">
                                        <ExtraParams>
                                            <ext:Parameter Name="index" Value="index" Mode="Raw" />
                                        </ExtraParams>
                                    </TriggerClick>
                                </DirectEvents>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthStandCode" FieldLabel="标准分类" ColumnWidth="0.3"
                                LabelAlign="Right" AllowBlank="false" EmptyText="请选择..." Editable="false" InputWidth="150"
                                LabelWidth="70" MatchFieldWidth="false">
                                <ListConfig Width="150" />
                            </ext:ComboBox>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="FitLayout">
                <Bin>
                    <ext:GridPanel runat="server" ID="GridPanelCenterCopy" Hidden="true">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenterCopy" AutoLoad="false">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenterCopy" />
                                </Model>
                            </ext:Store>
                        </Store>
                    </ext:GridPanel>
                </Bin>
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenterMain">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenterMain" AutoLoad="false">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenterMain">
                                        <Fields>
                                        </Fields>
                                    </ext:Model>
                                </Model>
                                <DirectEvents>
                                    <DataChanged OnEvent="StoreCenterMain_DataChanged">
                                    </DataChanged>
                                </DirectEvents>
                            </ext:Store>
                        </Store>
                        <ColumnModel runat="server" ID="ColumnModelCenterMain">
                            <Columns>
                                <ext:RowNumbererColumn runat="server" Width="40" />
                                <ext:Column runat="server" DataIndex="核定结果" Text="核定结果">
                                    <Renderer Fn="CheckResult_Renderer" />
                                </ext:Column>
                                <ext:Column runat="server" DataIndex="检验时间" Text="检验时间" Width="150" />
                                <ext:Column ID="Column5" runat="server" DataIndex="玲珑车次" Text="车次" />

                                <ext:Column ID="Column6" runat="server" DataIndex="门尼粘度" Text="门尼粘度">
                                    <Renderer Handler="return ItemCheck_Renderer(value, record.get('粘度偏差'));" />
                                </ext:Column>
                                <ext:Column ID="Column7" runat="server" DataIndex="门尼焦烧" Text="门尼焦烧">
                                    <Renderer Handler="return ItemCheck_Renderer(value, record.get('焦烧偏差'));" />
                                </ext:Column>
                                <ext:Column ID="Column8" runat="server" DataIndex="硬度" Text="硬度">
                                    <Renderer Handler="return ItemCheck_Renderer(value, record.get('硬度偏差'));" />
                                </ext:Column>
                                <ext:Column ID="Column9" runat="server" DataIndex="比重" Text="比重">
                                    <Renderer Handler="return ItemCheck_Renderer(value, record.get('比重偏差'));" />
                                </ext:Column>
                                <ext:Column ID="Column10" runat="server" DataIndex="ML" Text="ML">
                                    <Renderer Handler="return ItemCheck_Renderer(value, record.get('ML偏差'));" />
                                </ext:Column>
                                <ext:Column ID="Column11" runat="server" DataIndex="MH" Text="MH">
                                    <Renderer Handler="return ItemCheck_Renderer(value, record.get('MH偏差'));" />
                                </ext:Column>
                                <ext:Column ID="Column12" runat="server" DataIndex="Ts1" Text="Ts1">
                                    <Renderer Handler="return ItemCheck_Renderer(value, record.get('Ts1偏差'));" />
                                </ext:Column>
                                <ext:Column ID="Column13" runat="server" DataIndex="T25" Text="T25">
                                    <Renderer Handler="return ItemCheck_Renderer(value, record.get('T25偏差'));" />
                                </ext:Column>
                                <ext:Column ID="Column14" runat="server" DataIndex="T30" Text="T30">
                                    <Renderer Handler="return ItemCheck_Renderer(value, record.get('T30偏差'));" />
                                </ext:Column>
                                <ext:Column ID="Column15" runat="server" DataIndex="T60" Text="T60">
                                    <Renderer Handler="return ItemCheck_Renderer(value, record.get('T60偏差'));" />
                                </ext:Column>
                                <ext:Column ID="Column16" runat="server" DataIndex="T90" Text="T90">
                                    <Renderer Handler="return ItemCheck_Renderer(value, record.get('T90偏差'));" />
                                </ext:Column>
                                <ext:Column ID="Column17" runat="server" DataIndex="抽出" Text="抽出">
                                    <Renderer Handler="return ItemCheck_Renderer(value, record.get('抽出偏差'));" />
                                </ext:Column>

                                <ext:Column ID="Column1" runat="server" DataIndex="胶名" Text="胶名" Width="150" />
                                <ext:Column ID="Column2" runat="server" DataIndex="生产班次" Text="生产班次" />
                                <ext:Column ID="Column3" runat="server" DataIndex="主机手" Text="主机手" />
                                <ext:Column ID="Column4" runat="server" DataIndex="生产机台" Text="生产机台" Width="150" />

                                <ext:Column ID="Column19" runat="server" DataIndex="车次" Text="生产车次" />
                                <ext:Column ID="Column20" runat="server" DataIndex="架子条码" Text="架子条码" />
                                <ext:Column ID="Column21" runat="server" DataIndex="玲珑架子条码" Text="玲珑架子条码" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel runat="server" ID="RowSelectionModelCenterMain">
                            </ext:RowSelectionModel>
                        </SelectionModel>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    <ext:Window runat="server" ID="WindowPrint" Modal="false" Height="350" Width="680"
        AutoScroll="true" Hidden="true">
        <Content>
            <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" Font-Bold="False"
                Width="100%" Height="100%" Zoom="1" Padding="3, 3, 3, 3" ToolbarColor="Lavender"
                PrintInPdf="True" Layers="False" PdfEmbeddingFonts="false" ShowExports="false"
                ShowRefreshButton="false" BorderColor="White" />
        </Content>
        <Buttons>
            <ext:Button runat="server" ID="ButtonPrintClose" Icon="Cancel" Text="关闭">
                <DirectEvents>
                    <Click OnEvent="ButtonPrintClose_Click">
                        <EventMask ShowMask="true" Target="Page" />
                    </Click>
                </DirectEvents>
            </ext:Button>
        </Buttons>
        <Listeners>
            <Show Handler="Ext.fly('form1').mask();" />
            <Hide Handler="Ext.fly('form1').unmask();" />
        </Listeners>
    </ext:Window>
    </form>
</body>
</html>
