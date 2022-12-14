<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RubberStoreDaySum.aspx.cs" Inherits="Manager_Rubber_Report_RubberStoreDaySum" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>产量信息报表</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell {
            background-color: #99FF44 !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="rmRubberStorage" runat="server" />
        <ext:Viewport ID="vpRubberStorage" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnRubberStorage" runat="server" Region="North" Height="70">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="tbRubberStorage">
                            <Items>
                                <ext:Button runat="server" ID="btnSearch" Text="查询" Icon="Magnifier">
                                    <DirectEvents>
                                        <Click OnEvent="btnSearch_Click">
                                            <EventMask ShowMask="true" Target="Page" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsBegin" />
                                <ext:Button runat="server" ID="btnExport" Text="导出" Icon="PageExcel">
                                    <DirectEvents>
                                        <Click IsUpload="true" OnEvent="btnExport_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsEnd" />
                                <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                                <ext:ToolbarFill ID="tfEnd" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                            <Items>
                                <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".3"
                                    Padding="5">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer1" runat="server" FieldLabel="日期" Layout="HBoxLayout" LabelAlign="Right" Flex="1">
                                            <Items>
                                                <ext:DateField ID="txtBeginTime" runat="server" Format="yyyy-MM-dd" Flex="6" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnSearch}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                </ext:Panel>
                <ext:Panel runat="server" ID="Panel3" Region="Center" AutoScroll="true">
                    <Content>
                        <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" Font-Bold="False"
                            Width="112cm" Height="87cm" Zoom="1" Padding="3, 3, 3, 3" ToolbarColor="Lavender"
                            PrintInPdf="True" Layers="False" PdfEmbeddingFonts="false" ShowExports="false"
                            ShowRefreshButton="false" ShowToolbar="false" BorderColor="White" />
                    </Content>
                </ext:Panel>
            </Items>
        </ext:Viewport>
        <ext:Hidden ID="hiddenEquipCode" runat="server" />
    </form>
</body>
</html>
