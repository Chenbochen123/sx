﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_Rubber_Report_ReturnRubberDayReport, App_Web_wycfotos" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="frmReturnDayReport" runat="server">
    <ext:ResourceManager ID="rmReturnDayReport" runat="server" />
        <ext:Panel ID="pnReturnDayReport" runat="server" Region="North" Height="30">
            <TopBar>
                <ext:Toolbar runat="server" ID="tbReturnDayReport">
                    <Items>
                        <ext:ToolbarSeparator ID="tsBegin" />
                        <ext:DateField ID="txtPlanDate" runat="server" FieldLabel="请选择时间" LabelAlign="Left" />
                        <ext:ToolbarSeparator ID="tsMiddle" />
                        <ext:ComboBox ID="cbxChejian" runat="server" FieldLabel="分厂名称" LabelAlign="Right">
                            <Items>
                                <ext:ListItem Text="二厂" Value="6"></ext:ListItem>
                                <ext:ListItem Text="三厂" Value="7"></ext:ListItem>
                                <ext:ListItem Text="四厂" Value="8"></ext:ListItem>
                                <ext:ListItem Text="五厂" Value="9"></ext:ListItem>
                                <ext:ListItem Text="六厂" Value="10"></ext:ListItem>
                            </Items>
                        </ext:ComboBox>
                        <ext:ToolbarSeparator ID="ToolbarSeparator1" />
                        <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch" AutoPostBack="true" OnClick="btnSearch_Click"></ext:Button>
                        <ext:ToolbarSeparator ID="tsMiddle2" />
                        <ext:Button runat="server" Icon="Printer" Text="打印" ID="btnPrint">
                            <DirectEvents>
                                <Click OnEvent="btnPrint_Click" />
                            </DirectEvents>
                        </ext:Button>
                        <ext:ToolbarSeparator ID="tsEnd" />
                        <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                        <ext:ToolbarFill ID="tfEnd" />
                    </Items>
                </ext:Toolbar>
            </TopBar>
        </ext:Panel>
        <ext:Panel ID="panel" runat="server">
            <Content>
                <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" Font-Bold="False" 
                            Width="112cm" Height="87cm" Zoom="1" Padding="3, 3, 3, 3" ToolbarColor="Lavender"
                            PrintInPdf="True" Layers="False" PdfEmbeddingFonts="false" ShowExports="false"
                            ShowRefreshButton="false" ShowToolbar="false" BorderColor="White" />
            </Content>
        </ext:Panel>
    </form>
</body>
</html>
