<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Technology.aspx.cs" Inherits="Manager_Technology_Analysis_Technology" %>


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
    <script src="<%= Page.ResolveUrl("./") %>Technology.js"></script>
    
    <script type="text/javascript">
        var PptMaterialRefresh = function () {
            App.txtPptMaterial.clearValue();
            App.txtPptMaterial.getStore().reload();
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel20" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUser">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <DirectEvents>
                                        <Click OnEvent="btnSearchClick">
                                            <EventMask ShowMask="true"></EventMask>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Container ID="container1" runat="server" Layout="AnchorLayout" AutoHeight="true" Padding="5">
                            <Items>
                                <ext:Container ID="container4" runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:DateField ID="txtBeginDate" runat="server" Flex="1" FieldLabel="生产开始时间" LabelAlign="Right">
                                            <Listeners>
                                                <Change Fn="PptMaterialRefresh" />
                                            </Listeners>
                                        </ext:DateField>
                                        <ext:DateField ID="txtEndDate" runat="server" Flex="1" FieldLabel="生产结束时间" LabelAlign="Right">
                                            <Listeners>
                                                <Change Fn="PptMaterialRefresh" />
                                            </Listeners>
                                        </ext:DateField>
                                        <ext:TriggerField ID="txtEquipName" runat="server" Flex="1" FieldLabel="机台名称" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryEquipInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:ComboBox ID="txtPptShift" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="班次信息">
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="txtPptMaterial" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="物料信息"
                                            ValueField="MaterialCode" DisplayField="MaterialName"  QueryMode="Local">
                                            <Store>
                                                <ext:Store ID="storeMaterial" runat="server" AutoLoad="false" OnReadData="storeMaterial_ReadData">
                                                    <Model>
                                                        <ext:Model ID="Model5" runat="server" IDProperty="MaterialCode">
                                                            <Fields>
                                                                <ext:ModelField Name="MaterialCode" Type="String" />
                                                                <ext:ModelField Name="MaterialName" Type="String" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                        <ext:Hidden ID="hiddenEquipCode" runat="server"></ext:Hidden>
                    </Items>
                </ext:Panel>
                <ext:TabPanel ID="gridPanelCenter" runat="server" Region="Center" ActiveIndex="0" DefaultBorder="false" AutoScroll="false" MinTabWidth="160">
                    <Items>
                        <ext:Panel ID="Panel1" runat="server" Region="Center" Layout="FitLayout" Frame="true" Title="能量分析" TitleAlign="Center">
                            <Items>
                                <ext:Chart ID="ChartEner" runat="server" StyleSpec="background:#fff;" Shadow="true" StandardTheme="Category1" Animate="true">
                                    <LegendConfig Position="Right" />
                                    <Store>
                                        <ext:Store ID="Store1" runat="server">
                                            <Model>
                                                <ext:Model ID="Model1" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="LotIndex" />
                                                        <ext:ModelField Name="MixingEner" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Axes>
                                        <ext:NumericAxis Position="Left" Grid="true" MinorTickSteps="1" Minimum="-1" Title="能量(KWH)" />
                                        <ext:CategoryAxis Position="Bottom" Grid="true" Title="车次" Fields="LotIndex" />
                                    </Axes>
                                    <Series>
                                        <ext:LineSeries Axis="Left" ShowMarkers="true" XField="LotIndex" YField="MixingEner" Title="能量">
                                            <Tips ID="Tips1" runat="server" TrackMouse="true" Width="160" Height="42">
                                                <Renderer Handler="TipsEnerRenderer(this,storeItem); "></Renderer>
                                            </Tips>
                                        </ext:LineSeries>
                                    </Series>
                                </ext:Chart>
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="Panel2" runat="server" Region="Center" Layout="FitLayout" Frame="true" Title="投入重量分析" TitleAlign="Center">
                            <Items>
                                <ext:Chart ID="ChartWeight" runat="server" StyleSpec="background:#fff;" Shadow="true" StandardTheme="Category1" Animate="true">
                                    <LegendConfig Position="Right" />
                                    <Store>
                                        <ext:Store ID="Store2" runat="server">
                                            <Model>
                                                <ext:Model ID="Model2" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="LotIndex" />
                                                        <ext:ModelField Name="InSetWeight" />
                                                        <ext:ModelField Name="InRealWeight" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Axes>
                                        <ext:NumericAxis Position="Left" Grid="true" Title="投入重量(Kg)" />
                                        <ext:CategoryAxis Position="Bottom" Grid="true" Title="车次" Fields="LotIndex" />
                                    </Axes>
                                    <Series>
                                        <ext:LineSeries Axis="Left" ShowMarkers="true" XField="LotIndex" YField="InSetWeight" Title="设定重量">
                                            <Tips ID="Tips2" runat="server" TrackMouse="true" Width="160" Height="50" >
                                                <Renderer Handler="TipsWeightRenderer(this,storeItem); "></Renderer>
                                            </Tips>
                                        </ext:LineSeries>
                                        <ext:LineSeries Axis="Left" ShowMarkers="true" XField="LotIndex" YField="InRealWeight" Title="实际重量">
                                            <Tips ID="Tips3" runat="server" TrackMouse="true" Width="160" Height="50">
                                                <Renderer Handler="TipsWeightRenderer(this,storeItem); "></Renderer>
                                            </Tips>
                                        </ext:LineSeries>
                                    </Series>
                                </ext:Chart>
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="Panel3" runat="server" Region="Center" Layout="FitLayout" Frame="true" Title="混炼时间分析" TitleAlign="Center">
                            <Items>
                                <ext:Chart ID="ChartDoneTime" runat="server" StyleSpec="background:#fff;" Shadow="true" StandardTheme="Category1" Animate="true">
                                    <LegendConfig Position="Right" />
                                    <Store>
                                        <ext:Store ID="Store3" runat="server">
                                            <Model>
                                                <ext:Model ID="Model3" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="LotIndex" />
                                                        <ext:ModelField Name="DoneRtime" />
                                                        <ext:ModelField Name="StandTime" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Axes>
                                        <ext:NumericAxis Position="Left" Grid="true" MinorTickSteps="1" Minimum="-1" Title="混炼时间(S)" />
                                        <ext:CategoryAxis Position="Bottom" Grid="true" Title="车次" Fields="LotIndex" />
                                    </Axes>
                                    <Series>
                                        <ext:LineSeries Axis="Left" ShowMarkers="true" XField="LotIndex" YField="DoneRtime" Title="实际时间">
                                            <Tips ID="Tips4" runat="server" TrackMouse="true" Width="160" Height="50">
                                                <Renderer Handler="TipsDoneTimeRenderer(this,storeItem); "></Renderer>
                                            </Tips>
                                        </ext:LineSeries>
                                        <ext:LineSeries Axis="Left" ShowMarkers="true" XField="LotIndex" YField="StandTime" Title="标准时间">
                                            <Tips ID="Tips5" runat="server" TrackMouse="true" Width="160" Height="50">
                                                <Renderer Handler="TipsDoneTimeRenderer(this,storeItem); "></Renderer>
                                            </Tips>
                                        </ext:LineSeries>
                                    </Series>
                                </ext:Chart>
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="Panel4" runat="server" Region="Center" Layout="FitLayout" Frame="true" Title="产出重量分析" TitleAlign="Center">
                            <Items>
                                <ext:Chart ID="ChartShiftWeigh" runat="server" StyleSpec="background:#fff;" Shadow="true" StandardTheme="Category1" Animate="true">
                                    <LegendConfig Position="Right" />
                                    <Store>
                                        <ext:Store ID="Store4" runat="server">
                                            <Model>
                                                <ext:Model ID="Model4" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="LotIndex" />
                                                        <ext:ModelField Name="OutSetWeight" />
                                                        <ext:ModelField Name="OutRealWeight" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Axes>
                                        <ext:NumericAxis Position="Left" Grid="true" MinorTickSteps="1" Minimum="-1" Title="产出重量(Kg)" />
                                        <ext:CategoryAxis Position="Bottom" Grid="true" Title="架子" Fields="LotIndex" />
                                    </Axes>
                                    <Series>
                                        <ext:LineSeries Axis="Left" ShowMarkers="true" XField="LotIndex" YField="OutSetWeight" Title="设定重量">
                                            <Tips ID="Tips6" runat="server" TrackMouse="true" Width="160" Height="50">
                                                <Renderer Handler="TipsShiftWeighRender(this,storeItem); "></Renderer>
                                            </Tips>
                                        </ext:LineSeries>
                                        <ext:LineSeries Axis="Left" ShowMarkers="true" XField="LotIndex" YField="OutRealWeight" Title="实际重量">
                                            <Tips ID="Tips7" runat="server" TrackMouse="true" Width="160" Height="50">
                                                <Renderer Handler="TipsShiftWeighRender(this,storeItem); "></Renderer>
                                            </Tips>
                                        </ext:LineSeries>
                                    </Series>
                                </ext:Chart>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:TabPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
