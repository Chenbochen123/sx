<%@ page language="C#" autoeventwireup="true" inherits="Manager_RubberQuality_Manage_CheckMasterBatchReport, App_Web_bsjgrvuf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>母胶测试报表</title>
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
                            <ext:Button runat="server" ID="ButtonNorthExcel" Text="导出" Icon="PageExcel" Hidden="false">
                                <DirectEvents>
                                    <Click IsUpload="true" OnEvent="ButtonNorthExcel_Click">
                                        <ExtraParams>
                                            <ext:Parameter Name="fields" Value="#{ModelCenterCopy}.getFields()" Mode="Raw" />
                                            <ext:Parameter Name="records" Value="#{StoreCenterCopy}.getRecordsValues({ excludeId: true })"
                                                Mode="Raw" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel runat="server" ID="PanelNorthQuery" Layout="ColumnLayout" Height="60">
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
                            <ext:ComboBox runat="server" ID="ComboBoxNorthShiftId" FieldLabel="生产班次" ColumnWidth="0.2"
                                LabelAlign="Right" AllowBlank="false" Editable="false" EmptyText="请选择..." InputWidth="70"
                                MatchFieldWidth="false" LabelWidth="70">
                                <ListConfig Width="70" />
                                <DirectEvents>
                                    <Change OnEvent="ComboBoxNorthShiftId_Change">
                                        <EventMask ShowMask="true" Target="Page" />
                                    </Change>
                                </DirectEvents>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthEquipCode" FieldLabel="生产机台" ColumnWidth="0.5"
                                LabelAlign="Right" AllowBlank="false" Editable="false" EmptyText="请选择..." InputWidth="150"
                                MatchFieldWidth="false" LabelWidth="70">
                                <ListConfig Width="150" />
                                <DirectEvents>
                                    <Change OnEvent="ComboBoxNorthEquipCode_Change">
                                        <EventMask ShowMask="true" Target="Page" />
                                    </Change>
                                </DirectEvents>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthMaterCode" FieldLabel="物料名称" ColumnWidth="0.3"
                                LabelAlign="Right" AllowBlank="false" EmptyText="请选择..." Editable="false" LabelWidth="70">
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
                            <ext:NumberField runat="server" ID="NumberFieldNorthSSerialId" FieldLabel="起始车次"
                                Text="1" ColumnWidth="0.2" LabelAlign="Right" MinValue="1" MaxValue="999" AllowBlank="false"
                                AllowDecimals="false" InputWidth="70" LabelWidth="70" DecimalPrecision="0">
                            </ext:NumberField>
                            <ext:NumberField runat="server" ID="NumberFieldNorthESerialId" FieldLabel="结束车次"
                                Text="999" ColumnWidth="0.2" LabelAlign="Right" MinValue="1" MaxValue="999" AllowBlank="false"
                                AllowDecimals="false" InputWidth="70" LabelWidth="70" DecimalPrecision="0">
                            </ext:NumberField>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthStandCode" FieldLabel="标准" ColumnWidth="0.3"
                                LabelAlign="Right" AllowBlank="false" Editable="false" EmptyText="请选择..." InputWidth="90"
                                MatchFieldWidth="false" LabelWidth="70" Hidden="true">
                                <ListConfig Width="90" />
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
                            <ext:Store runat="server" ID="StoreCenterMain" AutoLoad="false" Buffered="true" PageSize="200">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenterMain" />
                                </Model>
                                <Listeners>
                                    <Refresh Fn="StoreCenterMain_Refresh" />
                                </Listeners>
                            </ext:Store>
                        </Store>
                        <BottomBar>
                            <ext:StatusBar runat="server" ID="StatusBarCenterMain" />
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
