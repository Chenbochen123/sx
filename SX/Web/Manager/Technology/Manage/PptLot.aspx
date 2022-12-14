<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PptLot.aspx.cs" Inherits="Manager_Technology_Manage_PptLot" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工艺参数分析</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />

    <script src="<%= Page.ResolveUrl("~/") %>resources/js/default.js"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery.PrintArea.js"></script>

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>LotReport.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>

</head>
<body>
    <form id="form1" runat="server">

        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:ChartTheme ID="ChartTheme1" runat="server" ThemeName="Browser" Colors="<%#COLORS %>" AutoDataBind="true" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel20" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUser">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <DirectEvents>
                                        <Click OnEvent="btnSearchFirstClick">
                                            <EventMask ShowMask="true"></EventMask>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Container ID="container1" runat="server" Layout="AnchorLayout" AutoHeight="true" Padding="2">
                            <Items>
                                <ext:Container ID="container4" runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>

                                        <ext:ComboBox ID="cbxequip" runat="server" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="机台" MinWidth="150" Flex="1">
                                        </ext:ComboBox>
                                        <ext:DateField ID="Date" runat="server" Disabled="false" Width="300" AnchorHorizontal="100%" FieldLabel="生产日期" />
                                        <ext:ComboBox ID="cbxclass" runat="server" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="班组" MinWidth="150" Flex="1">
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cbxmater" runat="server" SelectOnTab="true" Editable="true" LabelAlign="Right" FieldLabel="物料" MinWidth="150" Flex="1">
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cbxtype" runat="server" FieldLabel="类型" LabelAlign="left" Width="230" Flex="1"
                                            Editable="false">
                                            <Items>
                                                <ext:ListItem Text="能量分析" Value="1">
                                                </ext:ListItem>
                                                <ext:ListItem Text="投入重量分析" Value="2">
                                                </ext:ListItem>
                                                <ext:ListItem Text="混炼时间分析" Value="3">
                                                </ext:ListItem>
                                                <ext:ListItem Text="产出重量分析" Value="4">
                                                </ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:NumberField ID="txtLotCount" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" DecimalPrecision="0" FieldLabel="总步骤" ReadOnly="true" Hidden="true" MinWidth="100">
                                        </ext:NumberField>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                        <ext:Hidden ID="hiddenPmtRecipeID" runat="server"></ext:Hidden>
                        <ext:Hidden ID="hiddenEquipCode" runat="server"></ext:Hidden>
                    </Items>
                </ext:Panel>

                <ext:Panel ID="Panel1" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <Items>
                        <ext:Panel ID="gridPanelCenter" runat="server" Region="Center" Layout="FitLayout" Frame="true" Title="能量曲线图" TitleAlign="Center">
                            <Items>

                                <ext:Chart ID="Chart1" runat="server" StyleSpec="background:#fff;font-size:9pt" Shadow="true" Theme="Browser:gradients" Animate="true">
                                    <LegendConfig Position="Right" />
                                    <Store>
                                        <ext:Store ID="Store1" runat="server">
                                            <Model>
                                                <ext:Model ID="Model1" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="Pj_Ener" />
                                                        <ext:ModelField Name="Serial_BatchId" />
                                                        <ext:ModelField Name="Real_Weight" />
                                                        <ext:ModelField Name="Done_Rtime" />
                                                        <ext:ModelField Name="MixingSpeed" />
                                                        <ext:ModelField Name="MixingPosition" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Axes>
                                        <ext:NumericAxis Position="Left" Grid="true" MinorTickSteps="1" Title="能量" />
                                        <ext:NumericAxis Position="Bottom" Grid="true" MajorTickSteps="49" Maximum="50" Minimum="0" Title="步骤" />
                                    </Axes>
                                    <Series>
                                        <ext:LineSeries Axis="Left" ShowMarkers="true" XField="Serial_BatchId" YField="Pj_Ener" Title="能量">
                                            <Tips ID="Tips1" runat="server" TrackMouse="true" Width="140" Height="100">
                                                <Renderer Handler="TipsRenderer(this,storeItem); "></Renderer>
                                            </Tips>
                                        </ext:LineSeries>
                                    </Series>
                                </ext:Chart>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>


                <ext:Panel ID="Panel2" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <Items>
                        <ext:Panel ID="Panel3" runat="server" Region="Center" Layout="FitLayout" Frame="true" Title="投入重量曲线图" TitleAlign="Center">
                            <Items>

                                <ext:Chart ID="Chart2" runat="server" StyleSpec="background:#fff;font-size:9pt" Shadow="true" Theme="Browser:gradients" Animate="true">
                                    <LegendConfig Position="Right" />
                                    <Store>
                                        <ext:Store ID="Store2" runat="server">
                                            <Model>
                                                <ext:Model ID="Model2" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="Pj_Ener" />
                                                        <ext:ModelField Name="Serial_BatchId" />
                                                        <ext:ModelField Name="Real_Weight" />
                                                        <ext:ModelField Name="Done_Rtime" />
                                                        <ext:ModelField Name="MixingSpeed" />
                                                        <ext:ModelField Name="MixingPosition" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Axes>
                                        <ext:NumericAxis Position="Left" Grid="true" MinorTickSteps="1" Title="投入重量" />
                                        <ext:NumericAxis Position="Bottom" Grid="true" MajorTickSteps="49" Maximum="50" Minimum="0" Title="步骤" />
                                    </Axes>
                                    <Series>
                                        <ext:LineSeries Axis="Left" ShowMarkers="false" XField="Serial_BatchId" YField="Real_Weight" Title="投入重量">
                                            <Tips ID="Tips2" runat="server" TrackMouse="true" Width="140" Height="100">
                                                <Renderer Handler="TipsRenderer(this,storeItem); "></Renderer>
                                            </Tips>
                                        </ext:LineSeries>
                                    </Series>
                                </ext:Chart>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel4" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <Items>
                        <ext:Panel ID="Panel5" runat="server" Region="Center" Layout="FitLayout" Frame="true" Title="混炼时间曲线图" TitleAlign="Center">
                            <Items>

                                <ext:Chart ID="Chart3" runat="server" StyleSpec="background:#fff;font-size:9pt" Shadow="true" Theme="Browser:gradients" Animate="true">
                                    <LegendConfig Position="Right" />
                                    <Store>
                                        <ext:Store ID="Store3" runat="server">
                                            <Model>
                                                <ext:Model ID="Model3" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="Pj_Ener" />
                                                        <ext:ModelField Name="Serial_BatchId" />
                                                        <ext:ModelField Name="Real_Weight" />
                                                        <ext:ModelField Name="Done_Rtime" />
                                                        <ext:ModelField Name="MixingSpeed" />
                                                        <ext:ModelField Name="MixingPosition" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Axes>
                                        <ext:NumericAxis Position="Left" Grid="true" MinorTickSteps="1" Title="混炼时间" />
                                        <ext:NumericAxis Position="Bottom" Grid="true" MajorTickSteps="49" Maximum="50" Minimum="0" Title="步骤" />
                                    </Axes>
                                    <Series>
                                        <ext:LineSeries Axis="Left" ShowMarkers="false" XField="Serial_BatchId" YField="Done_Rtime" Title="混炼时间">
                                            <Tips ID="Tips3" runat="server" TrackMouse="true" Width="140" Height="100">
                                                <Renderer Handler="TipsRenderer(this,storeItem); "></Renderer>
                                            </Tips>
                                        </ext:LineSeries>
                                    </Series>
                                </ext:Chart>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel6" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <Items>
                        <ext:Panel ID="Panel7" runat="server" Region="Center" Layout="FitLayout" Frame="true" Title="产出重量曲线图" TitleAlign="Center">
                            <Items>

                                <ext:Chart ID="Chart4" runat="server" StyleSpec="background:#fff;font-size:9pt" Shadow="true" Theme="Browser:gradients" Animate="true">
                                    <LegendConfig Position="Right" />
                                    <Store>
                                        <ext:Store ID="Store4" runat="server">
                                            <Model>
                                                <ext:Model ID="Model4" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="row_number" />
                                                        <ext:ModelField Name="Real_weight" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Axes>
                                        <ext:NumericAxis Position="Left" Grid="true" MinorTickSteps="1" Title="产出重量" />
                                        <ext:NumericAxis Position="Bottom" Grid="true" MajorTickSteps="49" Maximum="50" Minimum="0" Title="步骤" />
                                    </Axes>
                                    <Series>
                                        <ext:LineSeries Axis="Left" ShowMarkers="false" XField="row_number" YField="Real_weight" Title="产出重量">
                                            <Tips ID="Tips4" runat="server" TrackMouse="true" Width="140" Height="100">
                                                <Renderer Handler="TipsRenderer(this,storeItem); "></Renderer>
                                            </Tips>
                                        </ext:LineSeries>
                                    </Series>
                                </ext:Chart>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
