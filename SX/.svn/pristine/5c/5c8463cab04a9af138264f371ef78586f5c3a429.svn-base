<%@ page language="C#" autoeventwireup="true" inherits="Manager_RubberQuality_Manage_QrigProductionStatistics, App_Web_bsjgrvuf" %>

<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<script src="QrigProductionStatistics.js" type="text/javascript"></script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
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
    <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="PanelNorth" runat="server" Layout="ColumnLayout" Height="55" Region="North">
                <TopBar>
                    <ext:Toolbar ID="ToolbarNorth" runat="server">
                        <Items>
                            <ext:Button ID="ButtonNorthQuery" runat="server" Text="查询" Icon="Magnifier">
                                <%--<Listeners>
                                    <Click Fn="ButtonNorthQuery_Click" />
                                </Listeners>--%>
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
                                            <ext:Parameter Name="RowsValues" Mode="Raw" Value="#{GridPanelMain}.getRowsValues()" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:DateField ID="DateFieldCheckSDate" runat="server" FieldLabel="检验起始日期" LabelAlign="Right"
                        LabelWidth="100" Width="200" AllowBlank="false" Format="yyyy-MM-dd" Editable="false">
                    </ext:DateField>
                    <ext:DateField ID="DateFieldCheckEDate" runat="server" FieldLabel="检验截止日期" LabelAlign="Right"
                        LabelWidth="100" Width="200" AllowBlank="false" Format="yyyy-MM-dd" Editable="false">
                    </ext:DateField>
                    <ext:ComboBox runat="server" ID="ComboBoxCheckPlanClass" FieldLabel="检验班组" LabelAlign="Right"
                        LabelWidth="100" Width="200" Editable="false" EmptyText="全部">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.setValue('');" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="ComboBoxWorkShopId" FieldLabel="车间" LabelAlign="Right"
                        LabelWidth="100" Width="200" Editable="false" EmptyText="全部">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.setValue('');" />
                        </Listeners>
                    </ext:ComboBox>
                </Items>
            </ext:Panel>
            <ext:Panel ID="PanelCenter" runat="server" Layout="BorderLayout" Region="Center">
                <Items>
                    <ext:GridPanel ID="GridPanelMain" runat="server" Region="Center" Hidden="true">
                        <Store>
                            <ext:Store ID="StoreMain" runat="server" GroupField="检验班组">
                                <Model>
                                    <ext:Model ID="ModelMain" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="检验班组" />
                                            <ext:ModelField Name="车间" />
                                            <ext:ModelField Name="检验机台" />
                                            <ext:ModelField Name="检验类型" />
                                            <ext:ModelField Name="车数" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                                <Listeners>
                                    <GroupChange Fn="StoreMain_GroupChange">
                                    </GroupChange>
                                    <Refresh Fn="StoreMain_Refresh" />
                                </Listeners>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:Column runat="server" DataIndex="检验班组" Text="检验班组" Width="80" Hidden="true" />
                                <ext:Column runat="server" DataIndex="车间" Text="车间" Width="150" />
                                <ext:Column runat="server" DataIndex="检验类型" Text="检验类型" Width="150" />
                                <ext:Column runat="server" DataIndex="检验机台" Text="检验机台" Width="150" />
                                <ext:SummaryColumn runat="server" ID="SummaryColumnMainSerialNum" DataIndex="车数"
                                    Text="车数" Width="200" Groupable="false" Hideable="false" SummaryType="Sum">
                                    <%--<SummaryRenderer Fn="SummaryColumnMainSerialNum_SummaryRenderer" />--%>
                                </ext:SummaryColumn>
                            </Columns>
                        </ColumnModel>
                        <View>
                            <ext:GridView runat="server" ID="GridViewMain" AllowDeselect="true">
                                <Listeners>
                                    <Refresh Fn="setGroupStyle" />
                                </Listeners>
                            </ext:GridView>
                        </View>
                        <FooterBar>
                            <ext:StatusBar runat="server" ID="StatusBarMain" />
                        </FooterBar>
                        <Features>
                            <ext:Grouping runat="server" ID="GroupingMain" ShowSummaryRow="true" ShowGroupsText="是否分组"
                                GroupByText="按当前列分组" GroupHeaderTplString="{columnName}: {name} (SUM={[GroupingMain_Sum(values)]})" />
                            <ext:GridFilters runat="server" ID="GridFiltersMain" Local="true" UpdateBuffer="3000"
                                MenuFilterText="筛选">
                                <Filters>
                                    <ext:StringFilter DataIndex="检验班组" />
                                    <ext:StringFilter DataIndex="车间" />
                                    <ext:StringFilter DataIndex="检验机台" />
                                    <ext:StringFilter DataIndex="检验类型" />
                                    <ext:NumericFilter DataIndex="车数" EmptyText="输入数字..." />
                                </Filters>
                            </ext:GridFilters>
                        </Features>
                    </ext:GridPanel>
                </Items>
                <Content>
                    <%--<cc1:WebReport ID="WebReport1" runat="server" BackColor="White" Font-Bold="False"
                        Width="100%" Zoom="1" Padding="3, 3, 3, 3" ToolbarColor="Lavender" PrintInPdf="True" 
                        Layers="False" PdfEmbeddingFonts="true" ShowExports="true" ShowPrint="true"
                        ShowRefreshButton="false" />--%>
                    <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" Font-Bold="False"
                        Width="100%" Zoom="1" Padding="3, 3, 3, 3" ToolbarColor="Lavender" PrintInPdf="True"
                        Layers="False" PdfEmbeddingFonts="false" ShowExports="false" ShowRefreshButton="false" />
                </Content>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
