<%@ page language="C#" autoeventwireup="true" inherits="Manager_Equipment_StopManage_MixerFault_MixerGroupAnalysis, App_Web_44oxblsy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Viewport ID="vwUnit" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="barUnit">
                        <Items>
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                <ToolTips>
                                    <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="btn_search_click"></Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                            <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                            <ext:ToolbarFill ID="toolbarFill_end" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel ID="pnlUnitQuery" runat="server" AutoHeight="true">
                        <Items>
                            <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="txt_fault_type" Editable="false"  runat="server" FieldLabel="统计方式" LabelAlign="Right" >
                                                <Items>
                                                    <ext:ListItem Text="故障类型" Value="故障类型"></ext:ListItem>
                                                    <ext:ListItem Text="所属设备" Value="所属设备"></ext:ListItem>
                                                    <ext:ListItem Text="所属车间" Value="所属车间"></ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                            <ext:ComboBox ID="txt_count" Editable="false"  runat="server" FieldLabel="显示个数" LabelAlign="Right" >
                                                <Items>
                                                    <ext:ListItem Text="10" Value="10"></ext:ListItem>
                                                    <ext:ListItem Text="15" Value="15"></ext:ListItem>
                                                    <ext:ListItem Text="20" Value="20"></ext:ListItem>
                                                    <ext:ListItem Text="25" Value="25"></ext:ListItem>
                                                    <ext:ListItem Text="30" Value="30"></ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:DateField ID="txt_fault_begin_date" Editable="false"  runat="server" FieldLabel="开始时间" LabelAlign="Right" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container_3" runat="server"  Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:DateField ID="txt_fault_end_date" Editable="false"  runat="server" FieldLabel="结束时间" LabelAlign="Right" />
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
         <ext:Panel ID="Panel1" Width="500" runat="server" Title="密炼机故障统计分析" Layout="BorderLayout" Region="Center" >
                <Items>
                    <ext:Chart ID="Chart1" runat="server" Shadow="true" StyleSpec="background:#fff">
                        <Store>
                            <ext:Store ID="Store1" runat="server" OnReadData="MyData_Refresh" AutoDataBind="true">
                                <Model>
                                    <ext:Model ID="Model1" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="AnalysisName" />
                                            <ext:ModelField Name="Count" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <Axes>
                            <ext:NumericAxis Fields="Count" Grid="true" Title="故障数量" Minimum="0">
                                <Label>
                                    <Renderer Handler="return Ext.util.Format.number(value, '0,0');" />
                                </Label>
                            </ext:NumericAxis>
                            <ext:CategoryAxis Position="Bottom" Fields="AnalysisName" Title="分析类别">
                                <Label>
                                    <Rotate Degrees="60" />
                                </Label>
                            </ext:CategoryAxis>
                        </Axes>
                        <Series>
                            <ext:ColumnSeries Axis="Left" Highlight="true" XField="AnalysisName" YField="Count">
                                <Tips ID="Tips1" runat="server" TrackMouse="true" Width="200" Height="40">
                                    <Renderer Handler="this.setTitle(storeItem.get('AnalysisName') + ': ' + storeItem.get('Count'));" />
                                </Tips>
                                <Label Display="InsideEnd" Field="Count" Orientation="Horizontal" Color="#333" TextAnchor="middle">
                                    <Renderer Handler="return Ext.util.Format.number(value, '0');" />
                                </Label>
                            </ext:ColumnSeries>
                        </Series>
                    </ext:Chart>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
