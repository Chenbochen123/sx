<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PmtInValidQuery.aspx.cs" Inherits="Manager_RubberQuality_Report_PmtInValidQuery" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmMmShopout" runat="server" />
    <ext:Viewport ID="vpMmShopout" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnMmShopoutTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                   <ext:Toolbar runat="server" ID="ToolbarNorth">
                        <Items>
                            <ext:Button runat="server" ID="btnSearch" Text="查询" Icon="Magnifier">
                                <DirectEvents>
                                    <Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="btnExport" Text="导出" Icon="PageExcel">
                                <DirectEvents>
                                    <Click IsUpload="true" OnEvent="btnExport_Click">
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel ID="pnlStorageQuery" runat="server" AutoHeight="true">
                        <Items>
                            <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="Container2" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                        Padding="2">
                                        <Items>
                                           <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" Format="yyyy-MM-dd" FormatControlForValue="yyyy-MM-dd" LabelAlign="Right" Editable="false" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container4" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                        Padding="2">
                                        <Items>
                                           <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" Format="yyyy-MM-dd" FormatControlForValue="yyyy-MM-dd" LabelAlign="Right" Editable="false" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                        Padding="2">
                                        <Items>
                                           <ext:ComboBox ID="cbxworkbar" EmptyText="请选择车间" runat="server" FieldLabel="车间" LabelAlign="Right" Editable="false">
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
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container3" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                        Padding="2">
                                        <Items>
                                            <ext:ComboBox ID="cbxtype" EmptyText="请选择报表" runat="server" FieldLabel="分组" LabelAlign="Right" Editable="false">
                                                <Items>
                                                    <ext:ListItem Text="总计" Value="-1">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="按车间" Value="0">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="按主机手" Value="1">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="按班组" Value="2">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="按机台" Value="3">
                                                    </ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container5" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                        Padding="2">
                                        <Items>
                                            <ext:ComboBox ID="cbxStand" EmptyText="质检标准" runat="server" FieldLabel="标准" LabelAlign="Right" Editable="false">
                                                <Items>
                                                    <ext:ListItem Text="质检标准" Value="1">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="考核标准" Value="0">
                                                    </ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" AutoScroll="true">
                <Content>
                    <cc1:webreport id="WebReport1" runat="server" backcolor="White" font-bold="False"
                        width="1000cm" height="1000cm" zoom="1" padding="3, 3, 3, 3" toolbarcolor="Lavender"
                        printinpdf="True" layers="False" pdfembeddingfonts="false" showexports="false"
                        showrefreshbutton="false" showtoolbar="false" bordercolor="White" />
                </Content>
            </ext:Panel>
            
            <ext:Hidden ID="hiddenEquipCode" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hiddenMaterCode" runat="server">
            </ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
