<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_ProductReview_ForwardSearch, App_Web_1ixwfq50" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>正向条码追溯</title>
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
        var after = function () {
            App.waitProgressWindow.close();
        }
        var before = function () {
            App.waitProgressWindow.show();
        }
        var SelectEquipID = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hidden_select_equip_code.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.hidden_select_equip.setValue("Equip");
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
            }
        }
        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {  //机台返回值处理
            var type = App.hidden_select_equip.getValue();
            App.txtEquipCode.getTrigger(0).show();
            if (type == "Equip") {
                App.txtEquipCode.setValue(record.data.EquipName);
                App.hidden_select_equip_code.setValue(record.data.EquipCode);
             
            }
        }
        Ext.create("Ext.window.Window", {//机台信息带回查询信息
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            height: 480,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择机台",
            modal: true
        })

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
    <script>
        var nodeLoad = function (store, operation, options) {
            var node = operation.node;
            before();
            App.direct.NodeLoad(node.getId(), {
                success: function (result) {
                    node.set('loading', false);
                    node.set('loaded', true);
                    var data = Ext.decode(result);
                    node.appendChild(data, undefined, true);
                    node.expand();
                    after();
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('Failure', errorMsg);
                    after();
                }
            });

            return false;
        };
    </script>
</head>
<body>
    <form id="form" runat="server">
            <ext:Hidden ID="hidden_select_equip" runat="server">
    </ext:Hidden>
          <ext:Hidden ID="hidden_select_equip_code" runat="server">
    </ext:Hidden>
        <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        <asp:Button ID="btnExportDetailSubmit" Style="display: none" runat="server" Text="Button" OnClick="btnExportDetailSubmit_Click" />
        <ext:ResourceManager ID="resourceManager" runat="server" />
        <ext:Viewport ID="viewport" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUnit">
                            <Items>
                              
                                <ext:TextField ID="txtCurrentBarcode" runat="server" FieldLabel="扫描条码" LabelAlign="Right"/>
                                
                             
                           
                               
                              
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnQuery">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttbtnQuery" runat="server" Html="查询条码对应的追溯信息" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btn_query_barcode" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button runat="server" Icon="PageExcel" Text="导出" Hidden="true" ID="btnExport">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttbtnExport" runat="server" Html="导出条码相关追溯信息" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarFill ID="toolbarFill2" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                       <Items>
                            <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                     <Items>
                                    <ext:Container ID="Container7" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                              <ext:TextField ID="TextField1" runat="server" FieldLabel="产生胶料名称" LabelAlign="Right" />
                               
                                        </Items>
                                    </ext:Container>
                                            <ext:Container ID="Container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                               <ext:TextField ID="TextField2" runat="server" FieldLabel="产生车条码" LabelAlign="Right" />
                                      </Items>
                                    </ext:Container>
                                            <ext:Container ID="Container3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                              <ext:TriggerField ID="txtEquipCode" runat="server" Editable="false" FieldLabel="机台"  
                                                LabelAlign="Right">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="SelectEquipID" />
                                                </Listeners>
                                            </ext:TriggerField>
                                        </Items>
                                    </ext:Container>
                              <%--              <ext:Container ID="Container6" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                              <ext:DateField ID="txtStratShiftDate" runat="server" Editable="false" Vtype="daterange"
                                                FieldLabel="开始日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd" Hidden="true">
                                              
                                            </ext:DateField>
                                        </Items>
                                    </ext:Container>
                                            <ext:Container ID="Container8" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:DateField ID="txtEndShiftDate" runat="server" Editable="false" Vtype="daterange"
                                                FieldLabel="结束日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd" Hidden="true">
                                              
                                            </ext:DateField>
                                        </Items>
                                    </ext:Container>
                                            <ext:Container ID="Container9" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                               <ext:ComboBox ID="cboShift" runat="server" Editable="false" FieldLabel="班次" LabelAlign="Right" Hidden="true" >
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.getTrigger(0).show();" />
                                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { 
                                                                           this.clearValue(); 
                                                                           this.getTrigger(0).hide();
                                                                         
                                                                       }" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>--%>
                                          </Items>
                                  </ext:Container>
                        </Items>
                </ext:Panel>
                     


                <ext:Panel ID="Panel2" runat="server" Region="West" Width="330" Title="追溯信息" Layout="BorderLayout" Collapsible="true">
                    <Items>
                        <ext:TreePanel ID="treePanelSourceBarcode" runat="server" Title="生产正向追溯" Region="Center">
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
                                    <EventMask ShowMask="true"></EventMask>
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
                                        <ext:CheckboxGroup ID="CheckboxGroup1" runat="server" ColumnsNumber="4" Flex="1" AnchorHorizontal="true">
                                            <Items>
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="架子条码" ID="txtShowDetailBarcode" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="物料名称" ID="txtShowDetailMaterName" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="机台" ID="txtShowDetailEquipName" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="班次" ID="txtShowDetailShiftName" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="班组" ID="txtShowDetailClassName" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="计划数量" ID="txtShowDetailPlanNum" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="设重" ID="txtShowDetailSetWeight" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="实重" ID="txtShowDetailRealWeight" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="托盘车数" ID="txtShowDetailShelfnum" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="密炼车次" ID="txtShowDetailSerialID" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="超时报警" ID="txtShowDetail2" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="超差报警" ID="txtShowDetail3" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="每车能量" ID="txtShowDetailLotEnergy" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="排胶温度" ID="txtShowDetailPjTemp" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="排胶功率" ID="txtShowDetailPjPower" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="排胶能量" ID="txtShowDetailPjEner" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="密炼状态" ID="txtShowDetailMixStatusName" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="累计车次" ID="txtShowDetailSerialBatchID" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="混炼时间" ID="txtShowDetailDoneRtime" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="消耗时间" ID="txtShowDetailDoneAllRtime" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="间隔时间" ID="txtShowDetailBwbTime" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="开始生产时间" ID="txtShowDetailStartDatetime" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="操作工" ID="txtShowDetailWorkerbarcode" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="备注" ID="txtShowDetailMemNote" />
                                            </Items>
                                        </ext:CheckboxGroup>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="Panel1" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                            <Items>
                                <ext:TabPanel ID="TabPanel1" runat="server" Region="Center" ActiveIndex="0" DefaultBorder="false" AutoScroll="false" MinTabWidth="160">
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
                                                    <ext:RowNumbererColumn ID="RowNumbererColumn2" runat="server" Text="步骤" Width="50" Align="Center" />
                                                    <ext:Column ID="Column17" DataIndex="WeighType" runat="server" Text="物料类型" Width="120" Sortable="true" />
                                                    <ext:Column ID="Column2" DataIndex="MaterName" runat="server" Text="物料名称" Flex="1" Sortable="false" />
                                                    <ext:Column ID="Column4" DataIndex="SetWeight" runat="server" DecimalPrecision="0" Text="设重" Align="Center" Width="80" Sortable="false">
                                                        <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column18" DataIndex="RealWeight" runat="server" DecimalPrecision="0" Text="实重" Align="Center" Width="80" Sortable="false">
                                                        <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column5" DataIndex="ErrorAllow" runat="server" DecimalPrecision="0" Text="公差" Align="Center" Width="80" Sortable="false">
                                                        <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column27" DataIndex="WarningSgn" runat="server" Text="超差" Width="80" Sortable="false">
                                                        <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return '<font color=red>是</font>';}" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column19" DataIndex="WeighState" runat="server" Text="称量状态" Width="80" Sortable="false">
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
                                                             <Click Handler="$('#btnExportDetailSubmit').click();"></Click>
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
                                                    <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Text="步骤" Width="50" Align="Center" />
                                                    <ext:Column ID="Column1" DataIndex="TermCode" runat="server" Text="条件名称" Flex="1" Sortable="false" />
                                                    <ext:Column ID="Column6" DataIndex="SetTime" runat="server" Text="时间" Flex="1" Sortable="false" />
                                                    <ext:Column ID="Column7" DataIndex="SeTemp" runat="server" Text="温度" Flex="1" Sortable="false" />
                                                    <ext:Column ID="Column8" DataIndex="SetEner" runat="server" Text="能量" Flex="1" Sortable="false" />
                                                    <ext:Column ID="Column20" DataIndex="SetPower" runat="server" Text="功率" Flex="1" Sortable="false" />
                                                    <ext:Column ID="Column21" DataIndex="ActCode" runat="server" Text="动作名称" Flex="1" Sortable="false" />
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
                                                        <Click Fn="saveChart"></Click>
                                                     </Listeners>
                                                  </ext:Button>
                                                  <ext:ToolbarSeparator ID="toolbarSeparator_end1" />
                                                  <ext:ToolbarSpacer runat="server" ID="toolbarSpacer1" />
                                                  <ext:ToolbarFill ID="toolbarFill1" />
                                              </Items>
                                           </ext:Toolbar>
                                          </TopBar>
                                            <Items>
                                                <ext:Chart ID="Chart1" runat="server" StyleSpec="background:#fff;" Shadow="true" StandardTheme="Category1" Animate="true">
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
                                                                        <ext:ModelField Name="MixingPostion" />
                                                                        <ext:ModelField Name="L1" />
                                                        <ext:ModelField Name="L2" />
                                                        <ext:ModelField Name="L3" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <Axes>
                                                        <ext:NumericAxis Position="Left" Grid="true" MajorTickSteps="20" Maximum="210" Minimum="0" Title="温度(℃)能量(KWH)压力(bar)转速(RPM)" />
                                                        <ext:NumericAxis Position="Right" Grid="true" MajorTickSteps="20" Maximum="2100" Minimum="0" Title="功率(KW)" />
                                                        <ext:NumericAxis Position="Bottom" Grid="true" MajorTickSteps="20" Minimum="0" Title="时间(S)" />
                                                    </Axes>
                                                    <Series>
                                                        <ext:LineSeries Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="MixingTemp" Title="温度">
                                                            <Tips ID="Tips1" runat="server" TrackMouse="true" Width="80" Height="100">
                                                                <Renderer Handler="TipsRenderer(this,storeItem); "></Renderer>
                                                            </Tips>
                                                        </ext:LineSeries>
                                                        <ext:LineSeries Axis="Right" ShowMarkers="false" XField="SecondSpan" YField="MixingPower" Title="功率">
                                                            <Tips ID="Tips2" runat="server" TrackMouse="true" Width="80" Height="100">
                                                                <Renderer Handler="TipsRenderer(this,storeItem); "></Renderer>
                                                            </Tips>
                                                        </ext:LineSeries>
                                                        <ext:LineSeries Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="MixingPress" Title="压力">
                                                            <Tips ID="Tips3" runat="server" TrackMouse="true" Width="80" Height="100">
                                                                <Renderer Handler="TipsRenderer(this,storeItem); "></Renderer>
                                                            </Tips>
                                                        </ext:LineSeries>
                                                        <ext:LineSeries Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="MixingEnergy" Title="能量">
                                                            <Tips ID="Tips4" runat="server" TrackMouse="true" Width="80" Height="100">
                                                                <Renderer Handler="TipsRenderer(this,storeItem); "></Renderer>
                                                            </Tips>
                                                        </ext:LineSeries>
                                                        <ext:LineSeries Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="MixingSpeed" Title="转速">
                                                            <Tips ID="Tips5" runat="server" TrackMouse="true" Width="80" Height="100">
                                                                <Renderer Handler="TipsRenderer(this,storeItem); "></Renderer>
                                                            </Tips>
                                                        </ext:LineSeries>
                                                        <ext:LineSeries Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="MixingPosition" Title="上顶栓">
                                                            <Tips ID="Tips6" runat="server" TrackMouse="true" Width="140" Height="100">
                                                                <Renderer Handler="TipsRenderer(this,storeItem); "></Renderer>
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
                                                                <ext:ModelField Name="BillNo"/>
                                                                <ext:ModelField Name="ProductNo" />
                                                                <ext:ModelField Name="ProcDate" />
                                                                <ext:ModelField Name="RecordDate"/>
                                                                <ext:ModelField Name="FacName"/>
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                              <ColumnModel ID="batchColumnModel" runat="server">
                                                <Columns>
                                                    <ext:RowNumbererColumn ID="RowNumbererColumn3" runat="server" Text="条目" Width="50" Align="Center" />
                                                    <ext:Column ID="Column29" DataIndex="MaterialName" runat="server" Text="原材料名称" Flex="1" Sortable="false" />
                                                    <ext:Column ID="Column13" DataIndex="BillNo" runat="server" Text="入库单号" Flex="1" Sortable="false" />
                                                    <ext:Column ID="Column14" DataIndex="ProductNo" runat="server" Text="批次号" Flex="1" Sortable="false" />
                                                    <ext:Column ID="Column15" DataIndex="ProcDate" runat="server" Text="生产日期" Flex="1" Sortable="false" />
                                                    <ext:Column ID="Column16" DataIndex="RecordDate" runat="server" Text="入库日期" Flex="1" Sortable="false" />
                                                    <ext:Column ID="Column28" DataIndex="FacName" runat="server" Text="供应商/经销商" Flex="1" Sortable="false" />
                                                </Columns>
                                            </ColumnModel>
                                         </ext:GridPanel>
                                         <ext:Panel runat="server" ID="checkPanel" Title="质检信息" Region="North" Layout="AnchorLayout" Visible="false">
                                            <Items>
                                                <ext:Container ID="container1" runat="server" Layout="HBoxLayout" Padding="5">
                                                    <Items>
                                                        <ext:CheckboxGroup ID="cgCheck" runat="server" ColumnsNumber="4" Flex="1" AnchorHorizontal="true">
                                                            <Items>
                                                            </Items>
                                                        </ext:CheckboxGroup>
                                                    </Items>
                                                </ext:Container>
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
