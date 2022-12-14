﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_ProductReview_Detail, App_Web_1ixwfq50" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>条码追溯</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("./") %>Search.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.store.currentPage = 1;
            App.store.reload();
            return false;
        }
    </script>
    <script type="text/javascript">
        function saveChart(btn) {
            var url = '../../ReportCenter/SvgToImage.aspx';
            Ext.MessageBox.confirm('提示', '确定将当前的曲线图导出为图片？', function (choice) {
                if (choice == 'yes') {
                    App.Chart1.save({
                        type: 'image/png',
                        url: url
                    });
                }
            });
        }
    </script>
    <!--延迟加载本车生产耗用和耗用本车生产-->
    <script type="text/javascript">
        var after = function () {
            App.waitProgressWindow.close();
        }
        var before = function () {
            App.waitProgressWindow.show();
        }
        function refresh_panel() {
            before();
            App.direct.Lazy_Delay_Load({
                success: function (result) {
                    after();
                },
                failure: function (errorMsg) {
                    after();
                    Ext.Msg.alert('提示', "本车生产耗用追溯数据加载异常!");
                }
            });
        }
        var task = new Ext.util.DelayedTask(refresh_panel);
        task.delay(5000);
    </script>
     <!--点击节点触发的加载方法-->
     <script type="text/javascript">
         var nodeLoad = function (store, operation, options) {
             var node = operation.node;

             App.direct.NodeLoad(node.getId(), {
                 success: function (result) {
                     node.set('loading', false);
                     node.set('loaded', true);
                     var data = Ext.decode(result);
                     node.appendChild(data, undefined, true);
                     node.expand();
                 },

                 failure: function (errorMsg) {
                     Ext.Msg.alert('Failure', errorMsg);
                 }
             });

             return false;
         };
         var nodeJson;
         var nodeAllJson;
         var UnionNodeTree = function (node) {
             nodeJson = { nodeId: node.getId(), nodeText: node.data.text, nodeLevel: node.getDepth() }
             if (App.treeJson.getValue() != null && App.treeJson.getValue() != "") {
                 nodeAllJson = App.treeJson.getValue()+"," + JSON.stringify(nodeJson);
             } else {
                 nodeAllJson = JSON.stringify(nodeJson);
             }
             App.treeJson.setValue(nodeAllJson.toString());
             //alert(App.treeJson.getValue());
         }
    </script>
</head>
<body>
    <form id="form" runat="server">
    <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button"
        OnClick="btnExportSubmit_Click" />
    <asp:Button ID="btnExportDetailSubmit" Style="display: none" runat="server" Text="Button"
        OnClick="btnExportDetailSubmit_Click" />
    <ext:ResourceManager ID="resourceManager" runat="server" />
    <ext:Viewport ID="viewport" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="barUnit">
                        <Items>
                            <ext:Button runat="server" Icon="PageExcel" Text="导出" ID="btnExport">
                                <ToolTips>
                                    <ext:ToolTip ID="ttbtnExport" runat="server" Html="导出条码相关追溯信息" />
                                </ToolTips>
                                <Listeners>
                                    <Click Handler="$('#btnExportSubmit').click();">
                                    </Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarFill ID="toolbarFill2" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
            </ext:Panel>
            <ext:Panel ID="Panel2" runat="server" Region="West" Width="330" Title="耗用信息" Layout="BorderLayout"
                Collapsible="true">
                <Items>
                    <ext:TreePanel ID="treePanelSourceBarcode" runat="server" Title="本车生产耗用追溯" Region="Center">
                        <Store>
                            <ext:TreeStore ID="TreeStore1" runat="server">
                                <Root>
                                    <ext:Node NodeID="Root" Expanded="true" />
                                </Root>
                                <Listeners>
                                    <BeforeLoad Fn="nodeLoad" />
                                </Listeners>
                            </ext:TreeStore>
                        </Store>
                        <DirectEvents>
                            <ItemClick OnEvent="treeNodeClick">
                                <EventMask ShowMask="true">
                                </EventMask>
                                <ExtraParams>
                                    <ext:Parameter Name="value" Value="record.getId()" Mode="Raw" Encode="true" />
                                </ExtraParams>
                            </ItemClick>
                        </DirectEvents>
                    </ext:TreePanel>
                    <ext:TreePanel ID="treePanelTargetBarcode" runat="server" Title="耗用本车生产追溯" Region="South"
                        Height="250" RootVisible="false">
                        <Store>
                            <ext:TreeStore runat="server">
                                <Root>
                                    <ext:Node NodeID="Root" Expanded="true" />
                                </Root>
                            </ext:TreeStore>
                        </Store>
                        <DirectEvents>
                            <ItemClick OnEvent="treeNodeClick">
                                <EventMask ShowMask="true">
                                </EventMask>
                                <ExtraParams>
                                    <ext:Parameter Name="value" Value="record.getId()" Mode="Raw" Encode="true" />
                                </ExtraParams>
                            </ItemClick>
                        </DirectEvents>
                    </ext:TreePanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="Panel3" runat="server" Region="Center" Title="本车明细信息" Layout="BorderLayout">
                <Items>
                    <ext:Panel ID="DetailPanel" runat="server" Region="North" Layout="AnchorLayout" AutoHeight="true">
                        <Items>
                            <ext:Container ID="container4" runat="server" Layout="HBoxLayout" Padding="5">
                                <Items>
                                    <ext:CheckboxGroup ID="CheckboxGroup1" runat="server" ColumnsNumber="4" Flex="1"
                                        AnchorHorizontal="true">
                                        <Items>
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="架子条码"
                                                ID="txtShowDetailBarcode" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="物料名称"
                                                ID="txtShowDetailMaterName" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="机台"
                                                ID="txtShowDetailEquipName" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="班次"
                                                ID="txtShowDetailShiftName" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="班组"
                                                ID="txtShowDetailClassName" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="计划数量"
                                                ID="txtShowDetailPlanNum" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="设重"
                                                ID="txtShowDetailSetWeight" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="实重"
                                                ID="txtShowDetailRealWeight" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="托盘车数"
                                                ID="txtShowDetailShelfnum" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="密炼车次"
                                                ID="txtShowDetailSerialID" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="超时报警"
                                                ID="txtShowDetail2" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="超差报警"
                                                ID="txtShowDetail3" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="每车能量"
                                                ID="txtShowDetailLotEnergy" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="排胶温度"
                                                ID="txtShowDetailPjTemp" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="排胶功率"
                                                ID="txtShowDetailPjPower" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="排胶能量"
                                                ID="txtShowDetailPjEner" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="密炼状态"
                                                ID="txtShowDetailMixStatusName" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="累计车次"
                                                ID="txtShowDetailSerialBatchID" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="混炼时间"
                                                ID="txtShowDetailDoneRtime" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="消耗时间"
                                                ID="txtShowDetailDoneAllRtime" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="间隔时间"
                                                ID="txtShowDetailBwbTime" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="开始生产时间"
                                                ID="txtShowDetailStartDatetime" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="操作工"
                                                ID="txtShowDetailWorkerbarcode" />
                                            <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="备注"
                                                ID="txtShowDetailMemNote" />
                                        </Items>
                                    </ext:CheckboxGroup>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="Panel1" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                        <Items>
                            <ext:TabPanel ID="TabPanel1" runat="server" Region="Center" ActiveIndex="0" DefaultBorder="false"
                                AutoScroll="false" MinTabWidth="160">
                                <Items>
                                    <ext:GridPanel ID="pnlWeight" runat="server" Title="称量信息" Frame="true">
                                        <Store>
                                            <ext:Store ID="Store1" runat="server">
                                                <Model>
                                                    <ext:Model ID="Model3" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="Barcode" />
                                                            <ext:ModelField Name="WeightID" />
                                                            <ext:ModelField Name="MaterCode" />
                                                            <ext:ModelField Name="MaterName" />
                                                            <ext:ModelField Name="EquipCode" />
                                                            <ext:ModelField Name="SetWeight" />
                                                            <ext:ModelField Name="RealWeight" />
                                                            <ext:ModelField Name="ErrorAllow" />
                                                            <ext:ModelField Name="ErrorOut" />
                                                            <ext:ModelField Name="WarningSgn" />
                                                            <ext:ModelField Name="MaterQua" />
                                                            <ext:ModelField Name="MaterBarcode" />
                                                            <ext:ModelField Name="WeighTime" />
                                                            <ext:ModelField Name="WeighType" />
                                                            <ext:ModelField Name="WeighState" />
                                                            <ext:ModelField Name="UnitName" />
                                                            <ext:ModelField Name="PlanID" />
                                                            <ext:ModelField Name="PlanDate" />
                                                            <ext:ModelField Name="SerialID" />
                                                            <ext:ModelField Name="ScaleCode" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel3" runat="server">
                                            <Columns>
                                                <ext:RowNumbererColumn ID="RowNumbererColumn2" runat="server" Text="步骤" Width="50"
                                                    Align="Center" />
                                                <ext:Column ID="Column17" DataIndex="WeighType" runat="server" Text="物料类型" Width="120"
                                                    Sortable="true" />
                                                <ext:Column ID="Column2" DataIndex="MaterName" runat="server" Text="物料名称" Flex="1"
                                                    Sortable="false" />
                                                <ext:Column ID="Column4" DataIndex="SetWeight" runat="server" DecimalPrecision="0"
                                                    Text="设重" Align="Center" Width="80" Sortable="false">
                                                    <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                                </ext:Column>
                                                <ext:Column ID="Column18" DataIndex="RealWeight" runat="server" DecimalPrecision="0"
                                                    Text="实重" Align="Center" Width="80" Sortable="false">
                                                    <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                                </ext:Column>
                                                <ext:Column ID="Column5" DataIndex="ErrorAllow" runat="server" DecimalPrecision="0"
                                                    Text="公差" Align="Center" Width="80" Sortable="false">
                                                    <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                                </ext:Column>
                                                <ext:Column ID="Column27" DataIndex="WarningSgn" runat="server" Text="超差" Width="80"
                                                    Sortable="false">
                                                    <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return '<font color=red>是</font>';}" />
                                                </ext:Column>
                                                <ext:Column ID="Column19" DataIndex="WeighState" runat="server" Text="称量状态" Width="80"
                                                    Sortable="false">
                                                    <Renderer Handler="if (!value){return ''};if (value==0) {return '手动';}  else if (value==1) {return '自动';} else {return value;}" />
                                                </ext:Column>
                                            </Columns>
                                        </ColumnModel>
                                    </ext:GridPanel>
                                    <ext:GridPanel ID="pnlMixing" runat="server" Title="混炼信息" Frame="true">
                                        <TopBar>
                                            <ext:Toolbar runat="server" ID="barExport">
                                                <Items>
                                                    <ext:ToolbarSeparator ID="toolbarSeparator_start" />
                                                    <ext:Button runat="server" Icon="PageExcel" Text="导出-混炼信息" ID="btnExportDetail">
                                                        <Listeners>
                                                            <Click Handler="$('#btnExportDetailSubmit').click();">
                                                            </Click>
                                                        </Listeners>
                                                    </ext:Button>
                                                    <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                                    <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                                    <ext:ToolbarFill ID="toolbarFill_end" />
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>
                                        <Store>
                                            <ext:Store ID="Store2" runat="server">
                                                <Model>
                                                    <ext:Model ID="Model1" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="Barcode" />
                                                            <ext:ModelField Name="MixID" />
                                                            <ext:ModelField Name="TermCode" />
                                                            <ext:ModelField Name="SetTime" />
                                                            <ext:ModelField Name="SeTemp" />
                                                            <ext:ModelField Name="SetEner" />
                                                            <ext:ModelField Name="SetPower" />
                                                            <ext:ModelField Name="SetPres" />
                                                            <ext:ModelField Name="SetRota" />
                                                            <ext:ModelField Name="ActCode" />
                                                            <ext:ModelField Name="SaveTime" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel1" runat="server">
                                            <Columns>
                                                <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Text="步骤" Width="50"
                                                    Align="Center" />
                                                <ext:Column ID="Column1" DataIndex="TermCode" runat="server" Text="条件名称" Flex="1"
                                                    Sortable="false" />
                                                <ext:Column ID="Column6" DataIndex="SetTime" runat="server" Text="时间" Flex="1" Sortable="false" />
                                                <ext:Column ID="Column7" DataIndex="SeTemp" runat="server" Text="温度" Flex="1" Sortable="false" />
                                                <ext:Column ID="Column8" DataIndex="SetEner" runat="server" Text="能量" Flex="1" Sortable="false" />
                                                <ext:Column ID="Column20" DataIndex="SetPower" runat="server" Text="功率" Flex="1"
                                                    Sortable="false" />
                                                <ext:Column ID="Column21" DataIndex="ActCode" runat="server" Text="动作名称" Flex="1"
                                                    Sortable="false" />
                                                <ext:Column ID="Column22" DataIndex="SetPres" runat="server" Text="压力" Flex="1" Sortable="false" />
                                                <ext:Column ID="Column23" DataIndex="SetRota" runat="server" Text="转速" Flex="1" Sortable="false" />
                                            </Columns>
                                        </ColumnModel>
                                    </ext:GridPanel>
                                    <ext:Panel ID="gridPanelCenter" runat="server" Frame="true" Title="密炼机工作曲线图" Layout="FitLayout">
                                        <TopBar>
                                            <ext:Toolbar runat="server" ID="barImage">
                                                <Items>
                                                    <ext:ToolbarSeparator ID="toolbarSeparator_start1" />
                                                    <ext:Button runat="server" Icon="Image" Text="导出-密炼曲线" ID="btnExportImage">
                                                        <Listeners>
                                                            <Click Fn="saveChart">
                                                            </Click>
                                                        </Listeners>
                                                    </ext:Button>
                                                    <ext:ToolbarSeparator ID="toolbarSeparator_end1" />
                                                    <ext:ToolbarSpacer runat="server" ID="toolbarSpacer1" />
                                                    <ext:ToolbarFill ID="toolbarFill1" />
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>
                                        <Items>
                                            <ext:Chart ID="Chart1" runat="server" StyleSpec="background:#fff;" Shadow="true"
                                                StandardTheme="Category1" Animate="true">
                                                <LegendConfig Position="Right" />
                                                <Store>
                                                    <ext:Store ID="Store5" runat="server">
                                                        <Model>
                                                            <ext:Model ID="Model5" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="SecondSpan" />
                                                                    <ext:ModelField Name="MixingTemp" />
                                                                    <ext:ModelField Name="MixingPower" />
                                                                    <ext:ModelField Name="MixingEnergy" />
                                                                    <ext:ModelField Name="MixingPress" />
                                                                    <ext:ModelField Name="MixingSpeed" />
                                                                    <ext:ModelField Name="MixingPosition" />
                                                                      <ext:ModelField Name="L1" />
                                                        <ext:ModelField Name="L2" />
                                                        <ext:ModelField Name="L3" />
                                                          <ext:ModelField Name="L4" />
                                                            <ext:ModelField Name="L5" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <Axes>
                                                    <ext:NumericAxis Position="Left" Grid="true" MajorTickSteps="20" Maximum="210" Minimum="0"
                                                        Title="温度(℃)能量(KWH)压力(bar)转速(RPM)" />
                                                    <ext:NumericAxis Position="Right" Grid="true" MajorTickSteps="20" Maximum="2100"
                                                        Minimum="0" Title="功率(KW)" />
                                                    <ext:NumericAxis Position="Bottom" Grid="true" MajorTickSteps="20" Minimum="0" Title="时间(S)" />
                                                </Axes>
                                                <Series>
                                                    <ext:LineSeries Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="MixingTemp"
                                                        Title="温度">
                                                        <Tips ID="Tips1" runat="server" TrackMouse="true" Width="80" Height="100">
                                                            <Renderer Handler="TipsRenderer(this,storeItem); ">
                                                            </Renderer>
                                                        </Tips>
                                                    </ext:LineSeries>
                                                    <ext:LineSeries Axis="Right" ShowMarkers="false" XField="SecondSpan" YField="MixingPower"
                                                        Title="功率">
                                                        <Tips ID="Tips2" runat="server" TrackMouse="true" Width="80" Height="100">
                                                            <Renderer Handler="TipsRenderer(this,storeItem); ">
                                                            </Renderer>
                                                        </Tips>
                                                    </ext:LineSeries>
                                                    <ext:LineSeries Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="MixingPress"
                                                        Title="压力">
                                                        <Tips ID="Tips3" runat="server" TrackMouse="true" Width="80" Height="100">
                                                            <Renderer Handler="TipsRenderer(this,storeItem); ">
                                                            </Renderer>
                                                        </Tips>
                                                    </ext:LineSeries>
                                                    <ext:LineSeries Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="MixingEnergy"
                                                        Title="能量">
                                                        <Tips ID="Tips4" runat="server" TrackMouse="true" Width="80" Height="100">
                                                            <Renderer Handler="TipsRenderer(this,storeItem); ">
                                                            </Renderer>
                                                        </Tips>
                                                    </ext:LineSeries>
                                                    <ext:LineSeries Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="MixingSpeed"
                                                        Title="转速">
                                                        <Tips ID="Tips5" runat="server" TrackMouse="true" Width="80" Height="100">
                                                            <Renderer Handler="TipsRenderer(this,storeItem); ">
                                                            </Renderer>
                                                        </Tips>
                                                    </ext:LineSeries>
                                                    <ext:LineSeries Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="MixingPosition"
                                                        Title="上顶栓">
                                                        <Tips ID="Tips6" runat="server" TrackMouse="true" Width="140" Height="100">
                                                            <Renderer Handler="TipsRenderer(this,storeItem); ">
                                                            </Renderer>
                                                        </Tips>
                                                    </ext:LineSeries>

                                                      <ext:LineSeries Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="L1" Title="侧壁温度">
                                            <Tips ID="Tips7" runat="server" TrackMouse="true" Width="140" Height="100">
                                                <Renderer Handler="TipsRenderer(this,storeItem); "></Renderer>
                                            </Tips>
                                        </ext:LineSeries>

                                         <ext:LineSeries Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="L2" Title="转子温度">
                                            <Tips ID="Tips8" runat="server" TrackMouse="true" Width="140" Height="100">
                                                <Renderer Handler="TipsRenderer(this,storeItem); "></Renderer>
                                            </Tips>
                                        </ext:LineSeries>

                                         <ext:LineSeries Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="L3" Title="卸料门温度">
                                            <Tips ID="Tips9" runat="server" TrackMouse="true" Width="140" Height="100">
                                                <Renderer Handler="TipsRenderer(this,storeItem); "></Renderer>
                                            </Tips>
                                        </ext:LineSeries>
                                         <ext:LineSeries Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="L4" Title="捣胶1">
                                            <Tips ID="Tips10" runat="server" TrackMouse="true" Width="140" Height="100">
                                                <Renderer Handler="TipsRenderer(this,storeItem); "></Renderer>
                                            </Tips>
                                        </ext:LineSeries>
                                         <ext:LineSeries Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="L5" Title="捣胶2">
                                            <Tips ID="Tips11" runat="server" TrackMouse="true" Width="140" Height="100">
                                                <Renderer Handler="TipsRenderer(this,storeItem); "></Renderer>
                                            </Tips>
                                        </ext:LineSeries>
                                                </Series>
                                            </ext:Chart>
                                        </Items>
                                    </ext:Panel>
                                    <ext:GridPanel ID="pnlBatch" runat="server" Frame="true" Title="批次信息"  Visible="false">
                                        <Store>
                                            <ext:Store ID="storeBatch" runat="server">
                                                <Model>
                                                    <ext:Model ID="modelBatch" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="MaterialName" />
                                                            <ext:ModelField Name="BillNo" />
                                                            <ext:ModelField Name="LLBarcode" />
                                                            <ext:ModelField Name="ProductNo" />
                                                            <ext:ModelField Name="ProcDate" />
                                                            <ext:ModelField Name="RecordDate" />
                                                            <ext:ModelField Name="FacName" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="batchColumnModel" runat="server">
                                            <Columns>
                                                <ext:RowNumbererColumn ID="RowNumbererColumn3" runat="server" Text="条目" Width="50"
                                                    Align="Center" />
                                                <ext:Column ID="Column29" DataIndex="MaterialName" runat="server" Text="原材料名称" Flex="1"
                                                    Sortable="false" />
                                                <%--<ext:Column ID="Column13" DataIndex="BillNo" runat="server" Text="入库单号" Flex="1" Sortable="false" />--%>
                                                <ext:Column ID="Column14" DataIndex="ProductNo" runat="server" Text="批次号" Flex="1"
                                                    Sortable="false" />
                                                <ext:Column ID="Column3" DataIndex="LLBarcode" runat="server" Text="玲珑批次号" Flex="1" Sortable="false" Hidden="true" />
                                                <ext:Column ID="Column15" DataIndex="ProcDate" runat="server" Text="生产日期" Flex="1"
                                                    Sortable="false" />
                                                <ext:Column ID="Column16" DataIndex="RecordDate" runat="server" Text="入库日期" Flex="1"
                                                    Sortable="false" />
                                                <ext:Column ID="Column28" DataIndex="FacName" runat="server" Text="供应商/经销商" Flex="1"
                                                    Sortable="false" />
                                            </Columns>
                                        </ColumnModel>
                                    </ext:GridPanel>
                                    <ext:Panel runat="server" ID="checkPanel" Title="质检信息" Region="North" Layout="BorderLayout" Visible="false">
                                        <Items>
                                            <ext:TabPanel runat="server" Region="Center" Layout="BorderLayout">
                                                <Items>
                                                    <ext:Panel runat="server" Title="胶料质检" Region="Center" Layout="FitLayout">
                                                        <Items>
                                                            <ext:Container ID="container1" runat="server" Layout="HBoxLayout" Padding="5" AutoScroll="true">
                                                                <Items>
                                                                    <ext:CheckboxGroup ID="cgCheck" runat="server" ColumnsNumber="4" Flex="1" AnchorHorizontal="true">
                                                                        <Items>
                                                                        </Items>
                                                                    </ext:CheckboxGroup>
                                                                </Items>
                                                            </ext:Container>
                                                        </Items>
                                                    </ext:Panel>
                                                    <ext:Panel runat="server" Title="原材料质检" Region="Center" Layout="FitLayout">
                                                        <Items>
                                                            <ext:GridPanel runat="server" ID="gpMaterCheck" AutoScroll="true">
                                                                <Store>
                                                                    <ext:Store runat="server" ID="storeMaterCheck">
                                                                        <Model>
                                                                            <ext:Model runat="server" ID="modelMaterCheck">
                                                                                <Fields>
                                                                                    <ext:ModelField Name="MaterName" />
                                                                                    <ext:ModelField Name="Barcode" />
                                                                                    <ext:ModelField Name="ItemName" />
                                                                                    <ext:ModelField Name="CheckValue" />
                                                                                    <ext:ModelField Name="GoodCheckRange" />
                                                                                    <ext:ModelField Name="CheckDate" Type="Date" />
                                                                                    <ext:ModelField Name="CheckerName" />
                                                                                    <ext:ModelField Name="CheckResult" />
                                                                                </Fields>
                                                                            </ext:Model>
                                                                        </Model>
                                                                    </ext:Store>
                                                                </Store>
                                                                <ColumnModel>
                                                                    <Columns>
                                                                        <ext:Column DataIndex="MaterName" Text="原材料名称" />
                                                                        <ext:Column DataIndex="Barcode" Text="批次号" />
                                                                        <ext:Column DataIndex="CheckResult" Text="检验结果" />
                                                                        <ext:Column DataIndex="ItemName" Text="检验项目" />
                                                                        <ext:Column DataIndex="CheckValue" Text="检验数值" />
                                                                        <ext:Column DataIndex="GoodCheckRange" Text="合格范围" />
                                                                        <ext:DateColumn DataIndex="CheckDate" Text="检验时间" Format="yyyy-MM-dd HH:mm:ss" />
                                                                        <ext:Column DataIndex="CheckerName" Text="检验人" />
                                                                    </Columns>
                                                                </ColumnModel>
                                                            </ext:GridPanel>
                                                        </Items>
                                                    </ext:Panel>
                                                </Items>
                                            </ext:TabPanel>
                                        </Items>
                                    </ext:Panel>
                                </Items>
                            </ext:TabPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Hidden runat="server" ID="treeJson" />
            <ext:Hidden runat="server" ID="treeJsonTarget" />
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
