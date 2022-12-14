<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterBatchReport.aspx.cs" Inherits="Manager_ShopStorage_MaterBatchReport" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>原材料更换记录</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell
        {
        	background-color: #CCFF66 !important;
        }
    </style>
    <script type="text/javascript">
    </script>
</head>
<body>
    <form id="form1" runat="server">
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
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips><ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page"></EventMask>
                                    </Click></DirectEvents>
                                </ext:Button>
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
                                 <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                    Padding="5">
                                    <Items>
                                          <ext:DateField ID="StartDate" Vtype="integer" runat="server" FieldLabel="开始日期" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                                 <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                    Padding="5">
                                    <Items>
                                          <ext:DateField ID="EndDate" Vtype="integer" runat="server" FieldLabel="结束日期" LabelAlign="Right" />
                                       
                                    </Items>
                                </ext:Container>
                                 <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                    Padding="5">
                                    <Items>
                                          <ext:ComboBox ID="Fac" Vtype="integer" runat="server" FieldLabel="部门" LabelAlign="Right" />
                                       
                                    </Items>
                                </ext:Container>
                                 <ext:Container ID="container4" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                    Padding="5">
                                    <Items>
                                          <ext:ComboBox ID="Mater" Vtype="integer" runat="server" FieldLabel="物料" LabelAlign="Right" />
                                       
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="FitLayout">
                <Items>
                <ext:GridPanel ID="GridPanelCenter" runat="server" Region="Center"  AnchorHeight="100"  ColumnLines="true">
                    <Store>
                        <ext:Store ID="store" runat="server">
                            <Model>
                                <ext:Model ID="ModelCenter" runat="server">
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel>
                        <Columns>
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