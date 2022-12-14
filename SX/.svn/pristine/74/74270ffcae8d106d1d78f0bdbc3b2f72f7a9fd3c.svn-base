<%@ page language="C#" autoeventwireup="true" inherits="Manager_RubberQuality_Manage_CheckRubberQualitiedRateReport, App_Web_bsjgrvuf" %>

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
            <ext:Panel runat="server" ID="PanelNorth" Region="North" Layout="ColumnLayout">
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
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Hidden runat="server" ID="HiddenNorthCheckType" />
                    <ext:DateField runat="server" ID="txtBeginPlanDate" Type="Date" FieldLabel="开始生产日期"
                                LabelAlign="Right" Format="yyyy-MM-dd" Editable="false" AllowBlank="false" EmptyText="请选择..."
                                InputWidth="120" LabelWidth="80" />
                            <ext:DateField runat="server" ID="txtEndPlanDate" Type="Date" FieldLabel="结束生产日期"
                                LabelAlign="Right" Format="yyyy-MM-dd" Editable="false" AllowBlank="false" EmptyText="请选择..."
                                InputWidth="120" LabelWidth="80" />
                                <ext:ComboBox ID="cbxChejian" LabelWidth="60" runat="server" FieldLabel="车间" LabelAlign="Right">
                                                <Items>
                                                     <ext:ListItem Text="全部" Value="0">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M2车间" Value="2">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M3车间" Value="3">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M4车间" Value="4">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M5车间" Value="5">
                                                    </ext:ListItem>
                                                </Items>

                                            </ext:ComboBox>
                                                      <ext:ComboBox ID="cbxShift" LabelWidth="50" runat="server" FieldLabel="班次" LabelAlign="Right">
                                                <Items>
                                                     <ext:ListItem Text="全部" Value="0">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="中" Value="1">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="夜" Value="2">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="早" Value="3">
                                                    </ext:ListItem>
                                                 
                                                </Items>

                                            </ext:ComboBox>
                    <ext:ComboBox runat="server" LabelWidth="100" ID="ComboBoxNorthCheckType" FieldLabel="分类" LabelAlign="Right"
                        AllowBlank="false" Editable="false" EmptyText="请选择..." InputWidth="80" MatchFieldWidth="false"
                        >
                        <Items>
                            <ext:ListItem Mode="Value" Value="2" Text="检验标准" />
                            <ext:ListItem Mode="Value" Value="1" Text="考核标准" />
                        </Items>
                        <ListConfig Width="90" />
                    </ext:ComboBox>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" AutoScroll="true">
                <Content>
                    <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" Font-Bold="False"
                        Width="132cm" Height="107cm" Zoom="1" Padding="3, 3, 3, 3" ToolbarColor="Lavender"
                        PrintInPdf="True" Layers="False" PdfEmbeddingFonts="false" ShowExports="false"
                        ShowRefreshButton="false" ShowToolbar="false" BorderColor="White" />
                </Content>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
