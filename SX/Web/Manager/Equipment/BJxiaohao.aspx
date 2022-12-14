<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BJxiaohao.aspx.cs" Inherits="Manager_Equipment_BJxiaohao" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>机台备件消耗</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell {
            background-color: #CCFF66 !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnExportSubmit" runat="server" Text="Button" OnClick="btnExportSubmit_Click" Style="display: none" />
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Hidden ID="hidden_stop_type" runat="server" />
        <ext:Hidden ID="hidden_stop_fault" runat="server" />
        <ext:Hidden ID="hidden_fault_reason" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlStopType" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barStopType">
                            <Items>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btnSearch_Click">
                                            <EventMask ShowMask="true" Target="Page"></EventMask>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行结果导出" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".3"
                                    Padding="5">
                                    <Items>
                                       <ext:DateField ID="daDateTime" runat="server" FieldLabel="开始日期" Width="300" Editable="false" AllowBlank="false" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".3"
                                    Padding="5">
                                    <Items>
                                       <ext:DateField ID="daDateTimeEnd" runat="server" FieldLabel="结束日期" Width="300" Editable="false" AllowBlank="false" />
                                    </Items>
                                </ext:Container>

                            </Items>
                        </ext:FormPanel>
                    </Items>
                </ext:Panel>
                <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="FitLayout" Flex="25" Title="检验数据">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenter" ColumnLines="true" AnchorHeight="100">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenter">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenter">
                                           <%-- <Fields>
                                                <ext:ModelField Name="shiftClassName" />
                                                <ext:ModelField Name="MaterName" />
                                                <ext:ModelField Name="UsedNum" />
                                                <ext:ModelField Name="bagprice" />
                                            </Fields>--%>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                    <%--<ext:Column ID="Column1" DataIndex="shiftClassName" runat="server" Text="班组" Align="Center" Width="100" />
                                    <ext:Column ID="Column2" DataIndex="MaterName" runat="server" Text="规格型号" Align="Center" Width="150"/>
                                    <ext:Column ID="Column3" DataIndex="UsedNum" runat="server" Text="使用数量" Align="Center" Width="100" />
                                    <ext:Column ID="Column4" DataIndex="bagprice" runat="server" Text="金额" Align="Center" Width="150"/>
                                    --%>
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
