<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetRubBuhege2.aspx.cs" Inherits="Manager_RubberQuality_Manage_GetRubBuhege2" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>返炼处理胶料查询</title>
    <script type="text/javascript">
        var change = function (value, metadata, record, rowIndex, columnIndex, stroe) {
            debugger;
            if (value == null) { return ""; }
            value = value.toString();
            if (record.data[App.GridPanelCenter.columns[columnIndex].dataIndex] == "否") {
                //return '<span style="background-color: red;font-weight:bold">' + value + '</span>';
                return '<span style="background-color: red">' + value + '</span>';
            }
            else if (record.data[App.GridPanelCenter.columns[columnIndex].dataIndex] == "预警") {
                return '<span style="background-color: gold">' + value + '</span>';
            }
            else if (record.data[App.GridPanelCenter.columns[columnIndex].dataIndex] == "没有标准") {
                return '<span style="background-color: #33FFFF">' + value + '</span>';
            }
            //else if (record.data[App.GridPanelCenter.columns[columnIndex + 1].dataIndex] == "优等") {
            //    return '<span style="background-color: #00FF33">' + value + '</span>';
            //}
            return value;
        };
    </script>
   <%-- <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell {
            background-color: #CCFF66 !important;
        }
    </style>
    <script type="text/javascript">
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlStopType" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barStopType">
                            <Items>
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
                                <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".3"
                                    Padding="5">
                                    <Items>
                                        <ext:DateField ID="StartDate" Vtype="integer" runat="server" FieldLabel="开始日期" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".3"
                                    Padding="5">
                                    <Items>
                                        <ext:DateField ID="EndDate" Vtype="integer" runat="server" FieldLabel="结束日期" LabelAlign="Right" />

                                    </Items>
                                </ext:Container>
<%--                                <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".3"
                                    Padding="5">
                                    <Items>

                                        <ext:ComboBox ID="cbxType" runat="server" FieldLabel="检验类别" LabelAlign="Left">
                                            <Items>
                                                <ext:ListItem Value="1" Text="子午快检"></ext:ListItem>
                                                <ext:ListItem Value="2" Text="母炼胶"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>--%>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                </ext:Panel>
                <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="FitLayout">
                    <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenter" ColumnLines="true" AnchorHeight="100">
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
                        <TopBar>
                            <ext:Toolbar ID="Toolbar1" runat="server">
                                <Items>
                                    <ext:Label ID="planState" Html="(颜色状态说明:<span style='color:gold;padding-right:10px;font-weight: bold;'>预警</span><span style='color:red;padding-right:10px;padding-left:10px;font-weight: bold;'>不合格</span><span style='color:#33FFFF;padding-right:10px;font-weight: bold;'>无标准</span>)"
                                        runat="server" ColumnWidth=".7"><%--<span style='color:#00FF33;padding-right:10px;font-weight: bold;'>优等品</span>--%>
                                    </ext:Label>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
