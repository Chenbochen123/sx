<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PmtEquipParamChart.aspx.cs"
    Inherits="Manager_RubberQuality_Report_PmtEquipParamChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>计划执行监控</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            height: 450,
            hidden: true,
            width: 370,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择机台",
            modal: true
        })
        var QueryEquipmentInfo = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenEquipCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
            }
        }
        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {

            App.txtEquip.getTrigger(0).show();
            App.txtEquip.setValue(record.data.EquipName);
            App.hiddenEquipCode.setValue(record.data.EquipCode);

        }
        function TipsRenderer(tip, record) {
            var ss = "时间：" + record.get('时间') + '<br />';
            ss = ss + "数值：" + record.get('数值') + '<br />';
            ss = ss + "胶料：" + record.get('物料') + '<br />';
            ss = ss + "排胶温度：" + record.get('排胶温度') + '<br />';
            ss = ss + "混炼时间：" + record.get('混炼时间') + '<br />';
            ss = ss + "最大压力：" + record.get('最大压力') + '<br />';
            ss = ss + "最大能量：" + record.get('最大能量') + '<br />';
            ss = ss + "最大功率：" + record.get('最大功率') + '<br />';
            tip.setTitle(ss);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:ChartTheme ID="ChartTheme1" runat="server" ThemeName="Browser" Colors="<%#COLORS %>"
        AutoDataBind="true" />
    <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="Panel2" runat="server" Region="Center" Layout="BorderLayout">
                <Items>
                    <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true">
                        <Items>
                            <ext:Toolbar runat="server" ID="barUser">
                                <Items>
                                    <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                    <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                        <DirectEvents>
                                            <Click OnEvent="btnSearchFirstClick">
                                                <EventMask ShowMask="true">
                                                </EventMask>
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                            <ext:Container ID="container2" runat="server" Layout="TableLayout" Padding="0" Margin="0"
                                ColumnWidth="1" Dock="Left">
                                <Items>
                                    <ext:DateField ID="txtBeginTime" Padding="0" Margin="0" Format="yyyy-MM-dd" Width="180"
                                        FormatControlForValue="yyyy-MM-dd" runat="server" FieldLabel="时间" LabelWidth="60"
                                        LabelAlign="Right" Editable="false" />
                                    <ext:DateField Hidden="true" ID="txtEndTime" Padding="0" Margin="0" Format="yyyy-MM-dd"
                                        Width="180" FormatControlForValue="yyyy-MM-dd" runat="server" FieldLabel="结束时间"
                                        LabelWidth="60" LabelAlign="Right" Editable="false" />
                                    <ext:TextField ID="txtBarCode" Hidden="true" Padding="0" LabelWidth="50" Margin="0"
                                        runat="server" Width="170" FieldLabel="条码号" LabelAlign="Right" Editable="false" />
                                    <ext:TriggerField ID="txtEquip" Padding="0" LabelWidth="40" Margin="0" runat="server"
                                        Width="170" FieldLabel="机台" LabelAlign="Right" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="QueryEquipmentInfo" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:ComboBox ID="cbxType" EmptyText="排胶温度" runat="server" FieldLabel="报表类型" LabelAlign="Right"
                                        Editable="false">
                                        <Items>
                                            <ext:ListItem Text="排胶温度" Value="排胶温度">
                                            </ext:ListItem>
                                            <ext:ListItem Text="混炼时间" Value="混炼时间">
                                            </ext:ListItem>
                                            <ext:ListItem Text="最大压力" Value="最大压力">
                                            </ext:ListItem>
                                            <ext:ListItem Text="最大能量" Value="最大能量">
                                            </ext:ListItem>
                                            <ext:ListItem Text="最大功率" Value="最大功率">
                                            </ext:ListItem>
                                        </Items>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="Panel3" runat="server" Region="Center" AutoScroll="true">
                        <Items>
                            <ext:Panel ID="Zao" runat="server" Region="Center" AutoScroll="true">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Stretch" Padding="5" />
                                </LayoutConfig>
                                <Items>
                                    <ext:Panel ID="zaoPanel" Region="Center" Width="100" Html="<table width='100%' height='100%'><tr align='center'><td align='center'>中</td></tr></table> "
                                        runat="server" AutoScroll="true">
                                    </ext:Panel>
                                    <ext:Chart ID="Chart1" Theme="Browser:gradients" Region="Center" Height="280" Width="900"
                                        Frame="true" Shadow="true" runat="server" StyleSpec="background:#fff;font-size:9pt"
                                        Animate="true">
                                        <LegendConfig Position="Right" />
                                        <Store>
                                            <ext:Store ID="Store1" runat="server">
                                                <Model>
                                                    <ext:Model ID="Model1" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="SecondSpan" />
                                                            <ext:ModelField Name="时间" />
                                                            <ext:ModelField Name="数值" />
                                                            <ext:ModelField Name="物料" />
                                                            <ext:ModelField Name="排胶温度" />
                                                            <ext:ModelField Name="混炼时间" />
                                                            <ext:ModelField Name="最大压力" />
                                                            <ext:ModelField Name="最大能量" />
                                                            <ext:ModelField Name="最大功率" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <Axes>
                                            <ext:NumericAxis Position="Left" Grid="true"/>
                                            <ext:NumericAxis Position="Bottom" Grid="true"  MinorTickSteps="1" MajorTickSteps="1" />
                                        </Axes>
                                        <Series>
                                            <ext:ColumnSeries Axis="Left" XField="SecondSpan" YField="数值">
                                                <Tips ID="Tips1" runat="server" TrackMouse="true" Width="140" Height="180">
                                                    <Renderer Handler="TipsRenderer(this,storeItem); ">
                                                    </Renderer>
                                                </Tips>
                                            </ext:ColumnSeries>
                                        </Series>
                                    </ext:Chart>
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="Zhong" runat="server" AutoScroll="true" Region="Center">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Stretch" Padding="5" />
                                </LayoutConfig>
                                <Items>
                                    <ext:Panel ID="ZhongPanel" Region="Center" Width="100" Html="<table width='100%' height='100%'><tr align='center'><td align='center'>夜</td></tr></table> "
                                        runat="server" AutoScroll="true">
                                    </ext:Panel>
                                    <ext:Chart ID="Chart2" Region="Center" Theme="Browser:gradients" Height="280" Width="900"
                                        Frame="true" Shadow="true" runat="server" StyleSpec="background:#fff;font-size:9pt"
                                        Animate="true">
                                        <LegendConfig Position="Right" />
                                        <Store>
                                            <ext:Store ID="Store2" runat="server">
                                                <Model>
                                                    <ext:Model ID="Model2" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="SecondSpan" />
                                                            <ext:ModelField Name="时间" />
                                                            <ext:ModelField Name="数值" />
                                                            <ext:ModelField Name="物料" />
                                                            <ext:ModelField Name="排胶温度" />
                                                            <ext:ModelField Name="混炼时间" />
                                                            <ext:ModelField Name="最大压力" />
                                                            <ext:ModelField Name="最大能量" />
                                                            <ext:ModelField Name="最大功率" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <Axes>
                                            <ext:NumericAxis Position="Left" Grid="true" />
                                            <ext:NumericAxis Position="Bottom" Grid="true" MinorTickSteps="1" MajorTickSteps="1" />
                                        </Axes>
                                        <Series>
                                            <ext:ColumnSeries Axis="Left" AutoDataBind="false" XField="SecondSpan" YField="数值"
                                                >
                                                <Tips ID="Tips6" runat="server" TrackMouse="true" Width="140" Height="180">
                                                    <Renderer Handler="TipsRenderer(this,storeItem); ">
                                                    </Renderer>
                                                </Tips>
                                            </ext:ColumnSeries>
                                        </Series>
                                    </ext:Chart>
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="Ye" runat="server" AutoScroll="true" Region="Center">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Stretch" Padding="5" />
                                </LayoutConfig>
                                <Items>
                                    <ext:Panel ID="YePanel" Region="Center" Width="100" Html="<table width='100%' height='100%'><tr align='center'><td align='center'>早</td></tr></table> "
                                        runat="server" AutoScroll="true">
                                    </ext:Panel>
                                    <ext:Chart ID="Chart3" runat="server" Theme="Browser:gradients" Region="Center" Width="900"
                                        Height="280" Frame="true" Shadow="true" StyleSpec="background:#fff;font-size:9pt"
                                        Animate="true">
                                        <LegendConfig Position="Right" />
                                        <Store>
                                            <ext:Store ID="Store3" runat="server">
                                                <Model>
                                                    <ext:Model ID="Model3" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="SecondSpan" />
                                                            <ext:ModelField Name="时间" />
                                                            <ext:ModelField Name="数值" />
                                                            <ext:ModelField Name="物料" />
                                                            <ext:ModelField Name="排胶温度" />
                                                            <ext:ModelField Name="混炼时间" />
                                                            <ext:ModelField Name="最大压力" />
                                                            <ext:ModelField Name="最大能量" />
                                                            <ext:ModelField Name="最大功率" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <Axes>
                                            <ext:NumericAxis Position="Left" Grid="true" />
                                            <ext:NumericAxis Position="Bottom" Grid="true" MinorTickSteps="1" MajorTickSteps="1" />
                                        </Axes>
                                        <Series>
                                            <ext:ColumnSeries Axis="Left" XField="SecondSpan" YField="数值" >
                                                <Tips ID="Tips11" runat="server" TrackMouse="true" Width="140" Height="180">
                                                    <Renderer Handler="TipsRenderer(this,storeItem); ">
                                                    </Renderer>
                                                </Tips>
                                            </ext:ColumnSeries>
                                        </Series>
                                    </ext:Chart>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Hidden ID="hiddenEquipCode" runat="server">
            </ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>

