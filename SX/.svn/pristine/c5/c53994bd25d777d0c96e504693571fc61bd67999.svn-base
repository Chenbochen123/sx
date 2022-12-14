<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipRubTimeReport.aspx.cs" Inherits="Manager_Rubber_Report_EquipRubTimeReport" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="rmReport" runat="server" />
        <ext:Panel ID="pnReport" runat="server" Region="North" Height="30">
            <TopBar>
                <ext:Toolbar runat="server" ID="tbReport">
                    <Items>
                        <ext:ComboBox ID="cbxChejian" runat="server" FieldLabel="车间名称" LabelAlign="Right">
                            <Items>
                                <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                <ext:ListItem Text="M2" Value="2"></ext:ListItem>
                                <ext:ListItem Text="M3" Value="3"></ext:ListItem>
                                <ext:ListItem Text="M4" Value="4"></ext:ListItem>
                                <ext:ListItem Text="M5" Value="5"></ext:ListItem>
                            </Items>
                        </ext:ComboBox>
                        <ext:DateField ID="txtBeginDate" runat="server" Editable="false" FieldLabel="开始时间" LabelAlign="Right" />
                        <ext:DateField ID="txtEndDate" runat="server" Editable="false" FieldLabel="结束时间" LabelAlign="Right" />
                        <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                            <DirectEvents>
                                <Click OnEvent="btnSearch_Click">
                                    <EventMask ShowMask="true" Target="Page" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                        <ext:ToolbarFill ID="tfEnd" />
                    </Items>
                </ext:Toolbar>
            </TopBar>
        </ext:Panel>
        <ext:Panel ID="panel" runat="server">
            <Content>
                <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" Font-Bold="False" Width="100%" Height="87cm" Zoom="1"
                    Padding="3, 3, 3, 3" ToolbarColor="Lavender" PrintInPdf="True" Layers="False" PdfEmbeddingFonts="false" ShowExports="true"
                    ShowRefreshButton="false" ShowToolbar="true" BorderColor="White" />
            </Content>
        </ext:Panel>
    </form>
</body>
</html>
