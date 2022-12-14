<%@ page language="C#" autoeventwireup="true" inherits="Manager_RubberQuality_Manage_CheckRubberAssessQSReport, App_Web_bsjgrvuf" %>

<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>胶料快检考核报表</title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="ResourceManager1" />
    <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" ID="PanelNorth" Region="North">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ToolbarNorth">
                        <Items>
                            <ext:Button runat="server" ID="ButtonNorthQuery" Icon="Magnifier" Text="查询">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthQuery_Click">
                                        <EventMask ShowMask="true" Target="Page" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button ID="ButtonNorthExport" runat="server" Text="导出" Icon="PageExcel">
                                <DirectEvents>
                                    <Click IsUpload="true" OnEvent="ButtonNorthExport_Click">
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthExcel" Text="导出" Icon="PageExcel" Hidden="true">
                                <DirectEvents>
                                    <Click IsUpload="true" OnEvent="ButtonNorthExcel_Click">
                                        <ExtraParams>
                                            <ext:Parameter Name="fields" Value="#{ModelCenterDetail}.getFields()" Mode="Raw" />
                                            <ext:Parameter Name="records" Value="#{StoreCenterDetail}.getRecordsValues({ excludeId: true })"
                                                Mode="Raw" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel runat="server" ID="PanelNorthQuery" Layout="ColumnLayout" Width="700" Frame="false">
                        <Items>
                            <ext:DateField runat="server" ID="DateFieldNorthCheckPlanSDate" Format="yyyy-MM-dd"
                                FieldLabel="开始质检日期" LabelAlign="Right" AllowBlank="false" Editable="false" EmptyText="请选择..."
                                InputWidth="110" LabelWidth="100" ColumnWidth="0.5">
                            </ext:DateField>
                            <ext:DateField runat="server" ID="DateFieldNorthCheckPlanEDate" Format="yyyy-MM-dd"
                                FieldLabel="结束质检日期" LabelAlign="Right" AllowBlank="false" Editable="false" EmptyText="请选择..."
                                InputWidth="110" LabelWidth="100" ColumnWidth="0.5">
                            </ext:DateField>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthCheckShiftClass" FieldLabel="质检班组"
                                LabelAlign="Right" Editable="false" EmptyText="全部" InputWidth="70" MultiSelect="false"
                                MatchFieldWidth="false" LabelWidth="100" ColumnWidth="0.3">
                                <ListConfig Width="70" />
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="this.setValue('');" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthCheckShiftId" FieldLabel="质检班次" LabelAlign="Right"
                                Editable="false" EmptyText="全部" InputWidth="70" MultiSelect="false" MatchFieldWidth="false"
                                LabelWidth="100" ColumnWidth="0.3">
                                <ListConfig Width="70" />
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="this.setValue('');" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthWorkShop" FieldLabel="生产车间" LabelAlign="Right"
                                LabelWidth="80" InputWidth="120" EmptyText="全部" Editable="false" ColumnWidth="0.4"
                                MatchFieldWidth="false">
                                <ListConfig Width="120" />
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="this.setValue('');" />
                                </Listeners>
                            </ext:ComboBox>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="BorderLayout">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenterMain" Region="West" Width="280"
                        Collapsible="true" Title="质检班次班组">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenterMain" AutoLoad="false">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenterMain">
                                        <Fields>
                                            <ext:ModelField Name="CheckPlanDate" />
                                            <ext:ModelField Name="ShiftCheckId" />
                                            <ext:ModelField Name="ShiftCheckName" />
                                            <ext:ModelField Name="ShiftCheckGroupID" />
                                            <ext:ModelField Name="ShiftCheckGroupName" />
                                            <ext:ModelField Name="WorkShopCode" />
                                            <ext:ModelField Name="WorkShopName" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:RowNumbererColumn runat="server" ID="RowNumbererColumnCenterMain" Width="30" />
                                <ext:Column runat="server" ID="ColumnCenterMainCheckPlanDate" DataIndex="CheckPlanDate"
                                    Text="质检日期" Width="80" />
                                <ext:Column runat="server" ID="ColumnCenterMainShiftCheckGroupName" DataIndex="ShiftCheckGroupName"
                                    Text="班组" Width="40" />
                                <ext:Column runat="server" ID="ColumnCenterMainShiftCheckName" DataIndex="ShiftCheckName"
                                    Text="班次" Width="40" />
                                <ext:Column runat="server" ID="ColumnCenterMainWorkShopName" DataIndex="WorkShopName"
                                    Text="生产车间" Width="60" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel runat="server" ID="RowSelectionModelCenterMain">
                                <DirectEvents>
                                    <SelectionChange OnEvent="RowSelectionModel_SelectionChange">
                                        <ExtraParams>
                                            <ext:Parameter Name="CheckPlanDate" Value="selected[0].get('CheckPlanDate')" Mode="Raw" />
                                            <ext:Parameter Name="ShiftCheckId" Value="selected[0].get('ShiftCheckId')" Mode="Raw" />
                                            <ext:Parameter Name="ShiftCheckGroupID" Value="selected[0].get('ShiftCheckGroupID')"
                                                Mode="Raw" />
                                            <ext:Parameter Name="ShiftCheckName" Value="selected[0].get('ShiftCheckName')" Mode="Raw" />
                                            <ext:Parameter Name="ShiftCheckGroupName" Value="selected[0].get('ShiftCheckGroupName')"
                                                Mode="Raw" />
                                            <ext:Parameter Name="WorkShopCode" Value="selected[0].get('WorkShopCode')" Mode="Raw" />
                                            <ext:Parameter Name="WorkShopName" Value="selected[0].get('WorkShopName')" Mode="Raw" />
                                        </ExtraParams>
                                        <EventMask ShowMask="true" Target="Page" />
                                    </SelectionChange>
                                </DirectEvents>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                    </ext:GridPanel>
                    <ext:Panel runat="server" ID="PanelCenterReport" Region="Center" AutoScroll="true">
                        <Content>
                            <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" Font-Bold="False"
                                Width="700" Height="100%" Zoom="1" Padding="3, 3, 3, 3" ToolbarColor="Lavender"
                                PrintInPdf="True" Layers="False" PdfEmbeddingFonts="false" ShowExports="false"
                                ShowRefreshButton="false" />
                        </Content>
                    </ext:Panel>
                    <ext:Panel runat="server" ID="PanelCenterDetail" Region="East" Width="1">
                        <Items>
                            <ext:GridPanel runat="server" ID="GridPanelCenterDetail" Hidden="true">
                                <Store>
                                    <ext:Store runat="server" ID="StoreCenterDetail" AutoLoad="false">
                                        <Model>
                                            <ext:Model runat="server" ID="ModelCenterDetail">
                                                <Fields>
                                                    <ext:ModelField Name="炼胶批次" />
                                                    <ext:ModelField Name="胶料代号" />
                                                    <ext:ModelField Name="胶料车数" />
                                                    <ext:ModelField Name="合格车数" />
                                                    <ext:ModelField Name="不合格项数" />
                                                    <ext:ModelField Name="不合格原因" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:RowNumbererColumn runat="server" ID="RowNumbererColumnCenterDetail" />
                                        <ext:Column runat="server" ID="ColumnCenterDetailBatchNo" DataIndex="炼胶批次" Text="炼胶批次" />
                                        <ext:Column runat="server" ID="ColumnCenterDetailMaterName" DataIndex="胶料代号" Text="胶料代号" />
                                        <ext:Column runat="server" ID="ColumnCenterDetailSerialCount" DataIndex="胶料车数" Text="胶料车数" />
                                        <ext:Column runat="server" ID="ColumnCenterDetailQualifiedCount" DataIndex="合格车数"
                                            Text="合格车数" />
                                        <ext:Column runat="server" ID="ColumnCenterDetailUnqualifiedCount" DataIndex="不合格数"
                                            Text="不合格数" />
                                        <ext:Column runat="server" ID="ColumnCenterDetailUnqualifiedReason" DataIndex="不合格原因"
                                            Text="不合格原因" Width="200" />
                                    </Columns>
                                </ColumnModel>
                            </ext:GridPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
