<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckRubberQualityWorkshopCPKReport.aspx.cs"
    Inherits="Manager_RubberQuality_Manage_CheckRubberQualityWorkshopCPKReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>车间CPK</title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="ResourceManager1" />
    <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
        <Items>
            <ext:Hidden runat="server" ID="HiddenBeginDate" />
            <ext:Hidden runat="server" ID="HiddenEndDate" />
            <ext:Panel runat="server" ID="PanelNorth" Region="North" Layout="ColumnLayout">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ToolbarNorth">
                        <Items>
                            <ext:Button runat="server" ID="ButtonNorthQuery" Icon="Magnifier" Text="查询">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthQuery_Click" Timeout="600000">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthExport" Icon="PageExcel" Text="导出">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthExport_Click" IsUpload="true">
                                        <ExtraParams>
                                            <ext:Parameter Mode="Raw" Name="records" Value="#{StoreCenter}.getRecordsValues()" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:DateField runat="server" ID="DateFieldNorthBeginDate" FieldLabel="生产日期" LabelWidth="70"
                        LabelAlign="Right" Editable="false" Format="yyyy-MM-dd" InputWidth="110">
                    </ext:DateField>
                    <ext:DateField runat="server" ID="DateFieldNorthEndDate" FieldLabel="至" LabelWidth="20"
                        LabelAlign="Right" Editable="false" Format="yyyy-MM-dd" InputWidth="110">
                    </ext:DateField>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="FitLayout">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenter">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenter">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenter">
                                        <Fields>
                                            <ext:ModelField Name="生产日期" />
                                            <ext:ModelField Name="M2_粘度_CA" />
                                            <ext:ModelField Name="M2_粘度_CP" />
                                            <ext:ModelField Name="M2_粘度_CPK" />
                                            <ext:ModelField Name="M2_焦烧_CA" />
                                            <ext:ModelField Name="M2_焦烧_CP" />
                                            <ext:ModelField Name="M2_焦烧_CPK" />
                                            <ext:ModelField Name="M2_ML_CA" />
                                            <ext:ModelField Name="M2_ML_CP" />
                                            <ext:ModelField Name="M2_ML_CPK" />
                                            <ext:ModelField Name="M2_MH_CA" />
                                            <ext:ModelField Name="M2_MH_CP" />
                                            <ext:ModelField Name="M2_MH_CPK" />
                                            <ext:ModelField Name="M2_硬度_CA" />
                                            <ext:ModelField Name="M2_硬度_CP" />
                                            <ext:ModelField Name="M2_硬度_CPK" />
                                            <ext:ModelField Name="M2_比重_CA" />
                                            <ext:ModelField Name="M2_比重_CP" />
                                            <ext:ModelField Name="M2_比重_CPK" />
                                            <ext:ModelField Name="M2_Ts1_CA" />
                                            <ext:ModelField Name="M2_Ts1_CP" />
                                            <ext:ModelField Name="M2_Ts1_CPK" />
                                            <ext:ModelField Name="M2_T25_CA" />
                                            <ext:ModelField Name="M2_T25_CP" />
                                            <ext:ModelField Name="M2_T25_CPK" />
                                            <ext:ModelField Name="M2_T30_CA" />
                                            <ext:ModelField Name="M2_T30_CP" />
                                            <ext:ModelField Name="M2_T30_CPK" />
                                            <ext:ModelField Name="M2_T60_CA" />
                                            <ext:ModelField Name="M2_T60_CP" />
                                            <ext:ModelField Name="M2_T60_CPK" />
                                            <ext:ModelField Name="M2_T90_CA" />
                                            <ext:ModelField Name="M2_T90_CP" />
                                            <ext:ModelField Name="M2_T90_CPK" />
                                            <ext:ModelField Name="M2_抽出_CA" />
                                            <ext:ModelField Name="M2_抽出_CP" />
                                            <ext:ModelField Name="M2_抽出_CPK" />
                                            <ext:ModelField Name="M3_粘度_CA" />
                                            <ext:ModelField Name="M3_粘度_CP" />
                                            <ext:ModelField Name="M3_粘度_CPK" />
                                            <ext:ModelField Name="M3_焦烧_CA" />
                                            <ext:ModelField Name="M3_焦烧_CP" />
                                            <ext:ModelField Name="M3_焦烧_CPK" />
                                            <ext:ModelField Name="M3_ML_CA" />
                                            <ext:ModelField Name="M3_ML_CP" />
                                            <ext:ModelField Name="M3_ML_CPK" />
                                            <ext:ModelField Name="M3_MH_CA" />
                                            <ext:ModelField Name="M3_MH_CP" />
                                            <ext:ModelField Name="M3_MH_CPK" />
                                            <ext:ModelField Name="M3_硬度_CA" />
                                            <ext:ModelField Name="M3_硬度_CP" />
                                            <ext:ModelField Name="M3_硬度_CPK" />
                                            <ext:ModelField Name="M3_比重_CA" />
                                            <ext:ModelField Name="M3_比重_CP" />
                                            <ext:ModelField Name="M3_比重_CPK" />
                                            <ext:ModelField Name="M3_Ts1_CA" />
                                            <ext:ModelField Name="M3_Ts1_CP" />
                                            <ext:ModelField Name="M3_Ts1_CPK" />
                                            <ext:ModelField Name="M3_T25_CA" />
                                            <ext:ModelField Name="M3_T25_CP" />
                                            <ext:ModelField Name="M3_T25_CPK" />
                                            <ext:ModelField Name="M3_T30_CA" />
                                            <ext:ModelField Name="M3_T30_CP" />
                                            <ext:ModelField Name="M3_T30_CPK" />
                                            <ext:ModelField Name="M3_T60_CA" />
                                            <ext:ModelField Name="M3_T60_CP" />
                                            <ext:ModelField Name="M3_T60_CPK" />
                                            <ext:ModelField Name="M3_T90_CA" />
                                            <ext:ModelField Name="M3_T90_CP" />
                                            <ext:ModelField Name="M3_T90_CPK" />
                                            <ext:ModelField Name="M3_抽出_CA" />
                                            <ext:ModelField Name="M3_抽出_CP" />
                                            <ext:ModelField Name="M3_抽出_CPK" />
                                            <ext:ModelField Name="M4_粘度_CA" />
                                            <ext:ModelField Name="M4_粘度_CP" />
                                            <ext:ModelField Name="M4_粘度_CPK" />
                                            <ext:ModelField Name="M4_焦烧_CA" />
                                            <ext:ModelField Name="M4_焦烧_CP" />
                                            <ext:ModelField Name="M4_焦烧_CPK" />
                                            <ext:ModelField Name="M4_ML_CA" />
                                            <ext:ModelField Name="M4_ML_CP" />
                                            <ext:ModelField Name="M4_ML_CPK" />
                                            <ext:ModelField Name="M4_MH_CA" />
                                            <ext:ModelField Name="M4_MH_CP" />
                                            <ext:ModelField Name="M4_MH_CPK" />
                                            <ext:ModelField Name="M4_硬度_CA" />
                                            <ext:ModelField Name="M4_硬度_CP" />
                                            <ext:ModelField Name="M4_硬度_CPK" />
                                            <ext:ModelField Name="M4_比重_CA" />
                                            <ext:ModelField Name="M4_比重_CP" />
                                            <ext:ModelField Name="M4_比重_CPK" />
                                            <ext:ModelField Name="M4_Ts1_CA" />
                                            <ext:ModelField Name="M4_Ts1_CP" />
                                            <ext:ModelField Name="M4_Ts1_CPK" />
                                            <ext:ModelField Name="M4_T25_CA" />
                                            <ext:ModelField Name="M4_T25_CP" />
                                            <ext:ModelField Name="M4_T25_CPK" />
                                            <ext:ModelField Name="M4_T30_CA" />
                                            <ext:ModelField Name="M4_T30_CP" />
                                            <ext:ModelField Name="M4_T30_CPK" />
                                            <ext:ModelField Name="M4_T60_CA" />
                                            <ext:ModelField Name="M4_T60_CP" />
                                            <ext:ModelField Name="M4_T60_CPK" />
                                            <ext:ModelField Name="M4_T90_CA" />
                                            <ext:ModelField Name="M4_T90_CP" />
                                            <ext:ModelField Name="M4_T90_CPK" />
                                            <ext:ModelField Name="M4_抽出_CA" />
                                            <ext:ModelField Name="M4_抽出_CP" />
                                            <ext:ModelField Name="M4_抽出_CPK" />
                                            <ext:ModelField Name="M5_粘度_CA" />
                                            <ext:ModelField Name="M5_粘度_CP" />
                                            <ext:ModelField Name="M5_粘度_CPK" />
                                            <ext:ModelField Name="M5_焦烧_CA" />
                                            <ext:ModelField Name="M5_焦烧_CP" />
                                            <ext:ModelField Name="M5_焦烧_CPK" />
                                            <ext:ModelField Name="M5_ML_CA" />
                                            <ext:ModelField Name="M5_ML_CP" />
                                            <ext:ModelField Name="M5_ML_CPK" />
                                            <ext:ModelField Name="M5_MH_CA" />
                                            <ext:ModelField Name="M5_MH_CP" />
                                            <ext:ModelField Name="M5_MH_CPK" />
                                            <ext:ModelField Name="M5_硬度_CA" />
                                            <ext:ModelField Name="M5_硬度_CP" />
                                            <ext:ModelField Name="M5_硬度_CPK" />
                                            <ext:ModelField Name="M5_比重_CA" />
                                            <ext:ModelField Name="M5_比重_CP" />
                                            <ext:ModelField Name="M5_比重_CPK" />
                                            <ext:ModelField Name="M5_Ts1_CA" />
                                            <ext:ModelField Name="M5_Ts1_CP" />
                                            <ext:ModelField Name="M5_Ts1_CPK" />
                                            <ext:ModelField Name="M5_T25_CA" />
                                            <ext:ModelField Name="M5_T25_CP" />
                                            <ext:ModelField Name="M5_T25_CPK" />
                                            <ext:ModelField Name="M5_T30_CA" />
                                            <ext:ModelField Name="M5_T30_CP" />
                                            <ext:ModelField Name="M5_T30_CPK" />
                                            <ext:ModelField Name="M5_T60_CA" />
                                            <ext:ModelField Name="M5_T60_CP" />
                                            <ext:ModelField Name="M5_T60_CPK" />
                                            <ext:ModelField Name="M5_T90_CA" />
                                            <ext:ModelField Name="M5_T90_CP" />
                                            <ext:ModelField Name="M5_T90_CPK" />
                                            <ext:ModelField Name="M5_抽出_CA" />
                                            <ext:ModelField Name="M5_抽出_CP" />
                                            <ext:ModelField Name="M5_抽出_CPK" />
                                            <ext:ModelField Name="综合_粘度_CA" />
                                            <ext:ModelField Name="综合_粘度_CP" />
                                            <ext:ModelField Name="综合_粘度_CPK" />
                                            <ext:ModelField Name="综合_焦烧_CA" />
                                            <ext:ModelField Name="综合_焦烧_CP" />
                                            <ext:ModelField Name="综合_焦烧_CPK" />
                                            <ext:ModelField Name="综合_ML_CA" />
                                            <ext:ModelField Name="综合_ML_CP" />
                                            <ext:ModelField Name="综合_ML_CPK" />
                                            <ext:ModelField Name="综合_MH_CA" />
                                            <ext:ModelField Name="综合_MH_CP" />
                                            <ext:ModelField Name="综合_MH_CPK" />
                                            <ext:ModelField Name="综合_硬度_CA" />
                                            <ext:ModelField Name="综合_硬度_CP" />
                                            <ext:ModelField Name="综合_硬度_CPK" />
                                            <ext:ModelField Name="综合_比重_CA" />
                                            <ext:ModelField Name="综合_比重_CP" />
                                            <ext:ModelField Name="综合_比重_CPK" />
                                            <ext:ModelField Name="综合_T30_CA" />
                                            <ext:ModelField Name="综合_T30_CP" />
                                            <ext:ModelField Name="综合_T30_CPK" />
                                            <ext:ModelField Name="综合_T60_CA" />
                                            <ext:ModelField Name="综合_T60_CP" />
                                            <ext:ModelField Name="综合_T60_CPK" />
                                            <ext:ModelField Name="综合_抽出_CA" />
                                            <ext:ModelField Name="综合_抽出_CP" />
                                            <ext:ModelField Name="综合_抽出_CPK" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:RowNumbererColumn Width="40" />
                                <ext:Column DataIndex="生产日期" Text="生产日期" />
                                <ext:ComponentColumn Text="M2">
                                    <Columns>
                                        <ext:ComponentColumn Text="粘度">
                                            <Columns>
                                                <ext:Column DataIndex="M2_粘度_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M2_粘度_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M2_粘度_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="焦烧">
                                            <Columns>
                                                <ext:Column DataIndex="M2_焦烧_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M2_焦烧_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M2_焦烧_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="硬度">
                                            <Columns>
                                                <ext:Column DataIndex="M2_硬度_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M2_硬度_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M2_硬度_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="比重">
                                            <Columns>
                                                <ext:Column DataIndex="M2_比重_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M2_比重_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M2_比重_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="ML">
                                            <Columns>
                                                <ext:Column DataIndex="M2_ML_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M2_ML_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M2_ML_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="MH">
                                            <Columns>
                                                <ext:Column DataIndex="M2_MH_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M2_MH_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M2_MH_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="T30">
                                            <Columns>
                                                <ext:Column DataIndex="M2_T30_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M2_T30_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M2_T30_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="T60">
                                            <Columns>
                                                <ext:Column DataIndex="M2_T60_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M2_T60_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M2_T60_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="抽出">
                                            <Columns>
                                                <ext:Column DataIndex="M2_抽出_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M2_抽出_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M2_抽出_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="M3">
                                    <Columns>
                                        <ext:ComponentColumn Text="粘度">
                                            <Columns>
                                                <ext:Column DataIndex="M3_粘度_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M3_粘度_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M3_粘度_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="焦烧">
                                            <Columns>
                                                <ext:Column DataIndex="M3_焦烧_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M3_焦烧_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M3_焦烧_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="硬度">
                                            <Columns>
                                                <ext:Column DataIndex="M3_硬度_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M3_硬度_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M3_硬度_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="比重">
                                            <Columns>
                                                <ext:Column DataIndex="M3_比重_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M3_比重_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M3_比重_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="ML">
                                            <Columns>
                                                <ext:Column DataIndex="M3_ML_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M3_ML_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M3_ML_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="MH">
                                            <Columns>
                                                <ext:Column DataIndex="M3_MH_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M3_MH_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M3_MH_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="T30">
                                            <Columns>
                                                <ext:Column DataIndex="M3_T30_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M3_T30_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M3_T30_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="T60">
                                            <Columns>
                                                <ext:Column DataIndex="M3_T60_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M3_T60_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M3_T60_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="抽出">
                                            <Columns>
                                                <ext:Column DataIndex="M3_抽出_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M3_抽出_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M3_抽出_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="M4">
                                    <Columns>
                                        <ext:ComponentColumn Text="粘度">
                                            <Columns>
                                                <ext:Column DataIndex="M4_粘度_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M4_粘度_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M4_粘度_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="焦烧">
                                            <Columns>
                                                <ext:Column DataIndex="M4_焦烧_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M4_焦烧_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M4_焦烧_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="硬度">
                                            <Columns>
                                                <ext:Column DataIndex="M4_硬度_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M4_硬度_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M4_硬度_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="比重">
                                            <Columns>
                                                <ext:Column DataIndex="M4_比重_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M4_比重_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M4_比重_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="ML">
                                            <Columns>
                                                <ext:Column DataIndex="M4_ML_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M4_ML_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M4_ML_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="MH">
                                            <Columns>
                                                <ext:Column DataIndex="M4_MH_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M4_MH_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M4_MH_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="T30">
                                            <Columns>
                                                <ext:Column DataIndex="M4_T30_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M4_T30_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M4_T30_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="T60">
                                            <Columns>
                                                <ext:Column DataIndex="M4_T60_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M4_T60_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M4_T60_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="抽出">
                                            <Columns>
                                                <ext:Column DataIndex="M4_抽出_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M4_抽出_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M4_抽出_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="M5">
                                    <Columns>
                                        <ext:ComponentColumn Text="粘度">
                                            <Columns>
                                                <ext:Column DataIndex="M5_粘度_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M5_粘度_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M5_粘度_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="焦烧">
                                            <Columns>
                                                <ext:Column DataIndex="M5_焦烧_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M5_焦烧_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M5_焦烧_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="硬度">
                                            <Columns>
                                                <ext:Column DataIndex="M5_硬度_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M5_硬度_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M5_硬度_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="比重">
                                            <Columns>
                                                <ext:Column DataIndex="M5_比重_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M5_比重_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M5_比重_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="ML">
                                            <Columns>
                                                <ext:Column DataIndex="M5_ML_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M5_ML_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M5_ML_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="MH">
                                            <Columns>
                                                <ext:Column DataIndex="M5_MH_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M5_MH_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M5_MH_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="T30">
                                            <Columns>
                                                <ext:Column DataIndex="M5_T30_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M5_T30_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M5_T30_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="T60">
                                            <Columns>
                                                <ext:Column DataIndex="M5_T60_CA" Text="CA" Width="60" />
                                                <ext:Column DataIndex="M5_T60_CP" Text="CP" Width="60" />
                                                <ext:Column DataIndex="M5_T60_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                <ext:ComponentColumn Text="抽出">
                                    <Columns>
                                        <ext:Column DataIndex="M5_抽出_CA" Text="CA" Width="60" />
                                        <ext:Column DataIndex="M5_抽出_CP" Text="CP" Width="60" />
                                        <ext:Column DataIndex="M5_抽出_CPK" Text="CPK" Width="60" />
                                    </Columns>
                                </ext:ComponentColumn>
                            </Columns>
                            </ext:ComponentColumn>
                            <ext:ComponentColumn Text="综合">
                                <columns>
                                        <ext:ComponentColumn Text="粘度">
                                            <Columns>
                                            <ext:Column DataIndex="综合_粘度_CA" Text="CA" Width="60" />
                                            <ext:Column DataIndex="综合_粘度_CP" Text="CP" Width="60" />
                                            <ext:Column DataIndex="综合_粘度_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="焦烧">
                                            <Columns>
                                            <ext:Column DataIndex="综合_焦烧_CA" Text="CA" Width="60" />
                                            <ext:Column DataIndex="综合_焦烧_CP" Text="CP" Width="60" />
                                            <ext:Column DataIndex="综合_焦烧_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="硬度">
                                            <Columns>
                                            <ext:Column DataIndex="综合_硬度_CA" Text="CA" Width="60" />
                                            <ext:Column DataIndex="综合_硬度_CP" Text="CP" Width="60" />
                                            <ext:Column DataIndex="综合_硬度_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="比重">
                                            <Columns>
                                            <ext:Column DataIndex="综合_比重_CA" Text="CA" Width="60" />
                                            <ext:Column DataIndex="综合_比重_CP" Text="CP" Width="60" />
                                            <ext:Column DataIndex="综合_比重_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="ML">
                                            <Columns>
                                            <ext:Column DataIndex="综合_ML_CA" Text="CA" Width="60" />
                                            <ext:Column DataIndex="综合_ML_CP" Text="CP" Width="60" />
                                            <ext:Column DataIndex="综合_ML_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="MH">
                                            <Columns>
                                            <ext:Column DataIndex="综合_MH_CA" Text="CA" Width="60" />
                                            <ext:Column DataIndex="综合_MH_CP" Text="CP" Width="60" />
                                            <ext:Column DataIndex="综合_MH_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="T30">
                                            <Columns>
                                            <ext:Column DataIndex="综合_T30_CA" Text="CA" Width="60" />
                                            <ext:Column DataIndex="综合_T30_CP" Text="CP" Width="60" />
                                            <ext:Column DataIndex="综合_T30_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="T60">
                                            <Columns>
                                            <ext:Column DataIndex="综合_T60_CA" Text="CA" Width="60" />
                                            <ext:Column DataIndex="综合_T60_CP" Text="CP" Width="60" />
                                            <ext:Column DataIndex="综合_T60_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn Text="抽出">
                                            <Columns>
                                            <ext:Column DataIndex="综合_抽出_CA" Text="CA" Width="60" />
                                            <ext:Column DataIndex="综合_抽出_CP" Text="CP" Width="60" />
                                            <ext:Column DataIndex="综合_抽出_CPK" Text="CPK" Width="60" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                    </columns>
                            </ext:ComponentColumn>
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
