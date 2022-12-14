<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CuringCurveComparison.aspx.cs" Inherits="Manager_Technology_CuringCurveComparison_CuringCurveComparison" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>密炼曲线对比</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.gridPanel1.store.currentPage = 1;
            App.gridPanel1.store.reload();
            return false;
        }
        var commandcolumn_direct_add = function (command, record) {
            var Barcode = record.data.Barcode;
            App.direct.commandcolumnDirectAdd(Barcode, {
                success: function (result) {
                    Ext.Msg.notify('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.notify('操作', errorMsg);
                }
            });
        }
        var commandcolumn_direct_delete = function (command, record) {
            var Barcode = record.data.Barcode;
            App.direct.commandcolumnDirectDelete(Barcode, {
                success: function (result) {
                    Ext.Msg.notify('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.notify('操作', errorMsg);
                }
            });
        }
    </script>
    <script type="text/javascript">
        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            height: 450,
            hidden: true,
            width: 370,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料信息",
            modal: true
        })
        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {
            var queryWindow = App.Manager_BasicInfo_CommonPage_QueryMaterial_Window;
            var thisIsDefaultWindow = function (record) {
                App.txt_material_name.setValue(record.data.MaterialName);
                App.txt_material_code.setValue(record.data.MaterialCode);
                return;
            }
            App.txt_material_name.getTrigger(0).show();
            thisIsDefaultWindow(record);
            queryWindow.close();
        }
        var GetMaterialInfo = function () {
            var queryWindowShow = function (record) {
                var queryWindow = App.Manager_BasicInfo_CommonPage_QueryMaterial_Window;
                queryWindow.show();
            }
            queryWindowShow();
        }

        var txtMaterialName_click = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.txt_material_name.setValue("");
                    App.txt_material_code.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    GetMaterialInfo();
                    break;
            }
        }
    </script>
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
        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {
            var queryWindow = App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window;
            var thisIsAddWindow = function (record) {
            }
            var thisIsEditWindow = function (record) {

            }
            var thisIsDefaultWindow = function (record) {
                App.txt_equip_code.setValue(record.data.EquipCode);
                App.txt_equip_name.setValue(record.data.EquipName);
            }
            App.txt_equip_name.getTrigger(0).show();
            thisIsAddWindow(record);
            thisIsEditWindow(record);
            thisIsDefaultWindow(record);
            queryWindow.close();
        }
        var QueryEquipmentInfo = function (field, trigger, index) {
            App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
        }

        var txtEquipName_click = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.txt_equip_name.setValue("");
                    App.txt_equip_code.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    QueryEquipmentInfo();
                    break;
            }
        }
    </script>
</head>
<body>
    <form id="fmUnit" runat="server">
        <ext:ResourceManager ID="rmUnit" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUnit">
                            <Items>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="gridPanelRefresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                 <ext:ToolbarSeparator ID="toolbarSeparator_middle_1" />
                                 <ext:Button runat="server" Icon="ArrowSwitchBluegreen" Text="对比" ID="btn_compare_curing">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip2" runat="server" Html="点击将查询结果导出到Excel中" />
                                    </ToolTips>
                                     <DirectEvents>
                                        <Click OnEvent="btn_compare_curing_click">
                                            <ExtraParams>
                                                <ext:Parameter Name="Values" Value="Ext.encode(#{gridPanel1}.getRowsValues({selectedOnly:true}))" Mode="Raw" />
                                            </ExtraParams>
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
                        <ext:Panel ID="pnlUnitQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true" >
                                    <Items>
                                        <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TriggerField ID="txt_material_name"  runat="server" FieldLabel="物料名称" LabelAlign="Right" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="txtMaterialName_click" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TriggerField ID="txt_equip_name"  runat="server" FieldLabel="设备名称" LabelAlign="Right" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="txtEquipName_click" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:FieldContainer ID="FieldContainer1" runat="server" FieldLabel="生产开始时间" Layout="HBoxLayout" LabelAlign="Right" Flex="1">
                                                    <Items>
                                                        <ext:DateField ID="txtBeginDate" runat="server" Format="yyyy-MM-dd" Flex="6" />
                                                        <ext:TextField ID="txtBeginTime" runat="server" Flex="4">
                                                            <Plugins>
                                                                <ext:InputMask ID="InputMask1" runat="server" Mask="ah:bm:cs">
                                                                    <MaskSymbols>
                                                                        <ext:MaskSymbol Name="a" Regex="[012]" Placeholder="h" />
                                                                        <ext:MaskSymbol Name="h" Regex="[0-9]" Placeholder="h" />
                                                                        <ext:MaskSymbol Name="b" Regex="[0-5]" Placeholder="i" />
                                                                        <ext:MaskSymbol Name="m" Regex="[0-9]" Placeholder="i" />
                                                                        <ext:MaskSymbol Name="c" Regex="[0-5]" Placeholder="s" />
                                                                        <ext:MaskSymbol Name="s" Regex="[0-9]" Placeholder="s" />
                                                                    </MaskSymbols>
                                                                </ext:InputMask>
                                                            </Plugins>
                                                        </ext:TextField>
                                                    </Items>
                                                </ext:FieldContainer>
                                                <ext:FieldContainer ID="FieldContainer2" runat="server" FieldLabel="生产结束时间" Layout="HBoxLayout" LabelAlign="Right" Flex="1">
                                                    <Items>
                                                        <ext:DateField ID="txtEndDate" runat="server" Format="yyyy-MM-dd" Flex="6" />
                                                        <ext:TextField ID="txtEndTime" runat="server" Flex="4">
                                                            <Plugins>
                                                                <ext:InputMask ID="InputMask3" runat="server" Mask="ah:bm:cs">
                                                                    <MaskSymbols>
                                                                        <ext:MaskSymbol Name="a" Regex="[012]" Placeholder="h" />
                                                                        <ext:MaskSymbol Name="h" Regex="[0-9]" Placeholder="h" />
                                                                        <ext:MaskSymbol Name="b" Regex="[0-5]" Placeholder="i" />
                                                                        <ext:MaskSymbol Name="m" Regex="[0-9]" Placeholder="i" />
                                                                        <ext:MaskSymbol Name="c" Regex="[0-5]" Placeholder="s" />
                                                                        <ext:MaskSymbol Name="s" Regex="[0-9]" Placeholder="s" />
                                                                    </MaskSymbols>
                                                                </ext:InputMask>
                                                            </Plugins>
                                                        </ext:TextField>
                                                    </Items>
                                                </ext:FieldContainer>
                                               
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                 <ext:ComboBox ID="txt_Curve_Type" runat="server" FieldLabel="曲线类型" LabelAlign="Right" Editable="false" >
                                                    <Items>
                                                        <ext:ListItem Value="温度" Text="温度"></ext:ListItem>
                                                        <ext:ListItem Value="功率" Text="功率"></ext:ListItem>
                                                        <ext:ListItem Value="压力" Text="压力"></ext:ListItem>
                                                        <ext:ListItem Value="能量" Text="能量"></ext:ListItem>
                                                        <ext:ListItem Value="转速" Text="转速"></ext:ListItem>
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
                <ext:Panel ID="Panel1" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <Items>
                        <ext:GridPanel ID="gridPanel1" runat="server" Title="查询数据" Region="Center" Frame="true" Flex="1" Collapsible="true" >
                            <Store>
                                <ext:Store ID="gridPanel1Store" runat="server" PageSize="15">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData"  AutoDataBind="false"/>
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="Barcode" />
                                                <ext:ModelField Name="MaterCode" />
                                                <ext:ModelField Name="MaterName" />
                                                <ext:ModelField Name="EquipCode" />
                                                <ext:ModelField Name="EquipName" />
                                                <ext:ModelField Name="ShiftName" />
                                                <ext:ModelField Name="ClassName" />
                                                <ext:ModelField Name="RecipeName" />
                                                <ext:ModelField Name="StartDatetime" />
                                                <ext:ModelField Name="DoneRtime" />
                                                <ext:ModelField Name="DoneAllRtime" />
                                                <ext:ModelField Name="SerialID" />
                                                <ext:ModelField Name="PlanID" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel1" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="30" />
                                    <ext:CommandColumn ID="commandCol" runat="server" Width="25" Align="Center">
                                        <Commands>
                                            <ext:GridCommand Icon="Add" CommandName="Add">
                                                <ToolTip Text="选择此数据进行对比" />
                                            </ext:GridCommand>
                                        </Commands>
                                        <Listeners>
                                            <Command Handler="return commandcolumn_direct_add(command, record);" />
                                        </Listeners>
                                    </ext:CommandColumn>
                                    <ext:Column ID="col_Barcode" DataIndex="Barcode" runat="server" Text="条码号" Align="Left" Width="130" />
                                    <ext:Column ID="col_SerialID" DataIndex="SerialID" runat="server" Text="车次号" Align="Left" Width="60" />
                                    <ext:Column ID="col_PlanID" DataIndex="PlanID" runat="server" Text="计划号" Align="Left" Width="100" />
                                    <ext:Column ID="col_MaterCode" DataIndex="MaterCode" runat="server" Text="物料编号" Align="Left" Width="120" />
                                    <ext:Column ID="col_MaterName" DataIndex="MaterName" runat="server" Text="物料名称" Align="Left" Width="120" />
                                    <ext:Column ID="col_EquipCode" DataIndex="EquipCode" runat="server" Text="设备编号" Align="Left" Width="100" />
                                    <ext:Column ID="col_EquipName" DataIndex="EquipName" runat="server" Text="设备名称" Align="Left" Width="120" />
                                    <ext:Column ID="col_ShiftName" DataIndex="ShiftName" runat="server" Text="班次" Align="Left" Width="60" />
                                    <ext:Column ID="col_ClassName" DataIndex="ClassName" runat="server" Text="班组" Align="Left" Width="60" />
                                    <ext:Column ID="col_RecipeName" DataIndex="RecipeName" runat="server" Text="配方名称" Align="Left" Width="120" Hidden="true" />
                                    <ext:Column ID="col_StartDateTime" DataIndex="StartDatetime" runat="server" Text="开始混炼时间" Align="Left" Width="130" />
                                    <ext:Column ID="col_DoneRTime" DataIndex="DoneRtime" runat="server" Text="炼胶时间" Align="Left" Width="60" />
                                    <ext:Column ID="col_DoneAllRTime" DataIndex="DoneAllRtime" runat="server" Text="消耗总时间" Align="Left" Width="80" />
                                </Columns>
                            </ColumnModel>         
                            <BottomBar>
                                <ext:PagingToolbar ID="pageToolbar1" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="progressBarPager1" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>
                        <ext:GridPanel ID="comparisonPnl" Title="对比数据" runat="server" Region="South" Frame="true" Flex="1"  Collapsible="true">
                            <Store>
                                <ext:Store ID="comparisonStore" runat="server" PageSize="15">
                                    <Model>
                                        <ext:Model ID="model2" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="Barcode" />
                                                <ext:ModelField Name="MaterCode" />
                                                <ext:ModelField Name="MaterName" />
                                                <ext:ModelField Name="EquipCode" />
                                                <ext:ModelField Name="EquipName" />
                                                <ext:ModelField Name="ShiftName" />
                                                <ext:ModelField Name="ClassName" />
                                                <ext:ModelField Name="RecipeName" />
                                                <ext:ModelField Name="StartDatetime" />
                                                <ext:ModelField Name="DoneRtime" />
                                                <ext:ModelField Name="DoneAllRtime" />
                                                <ext:ModelField Name="SerialID" />
                                                <ext:ModelField Name="PlanID" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel2" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Width="30" />
                                    <ext:Column ID="Column1" DataIndex="Barcode" runat="server" Text="条码号" Align="Left" Width="130" />
                                    <ext:Column ID="Column2" DataIndex="SerialID" runat="server" Text="车次号" Align="Left" Width="60" />
                                    <ext:Column ID="Column3" DataIndex="PlanID" runat="server" Text="计划号" Align="Left" Width="100" />
                                    <ext:Column ID="Column4" DataIndex="MaterCode" runat="server" Text="物料编号" Align="Left" Width="120" />
                                    <ext:Column ID="Column5" DataIndex="MaterName" runat="server" Text="物料名称" Align="Left" Width="120" />
                                    <ext:Column ID="Column6" DataIndex="EquipCode" runat="server" Text="设备编号" Align="Left" Width="100" />
                                    <ext:Column ID="Column7" DataIndex="EquipName" runat="server" Text="设备名称" Align="Left" Width="120" Hidden="true" />
                                    <ext:Column ID="Column8" DataIndex="ShiftName" runat="server" Text="班次" Align="Left" Width="60" Hidden="true" />
                                    <ext:Column ID="Column9" DataIndex="ClassName" runat="server" Text="班组" Align="Left" Width="60" Hidden="true" />
                                    <ext:Column ID="Column10" DataIndex="RecipeName" runat="server" Text="配方名称" Align="Left" Width="120" Hidden="true" />
                                    <ext:Column ID="Column11" DataIndex="StartDatetime" runat="server" Text="开始混炼时间" Align="Left" Width="130" />
                                    <ext:Column ID="Column12" DataIndex="DoneRtime" runat="server" Text="炼胶时间" Align="Left" Width="60" />
                                    <ext:Column ID="Column13" DataIndex="DoneAllRtime" runat="server" Text="消耗总时间" Align="Left" Width="80" />
                                    <ext:CommandColumn ID="CommandColumn1" runat="server" Width="25" Align="Center">
                                        <Commands>
                                            <ext:GridCommand Icon="Delete" CommandName="Delete">
                                                <ToolTip Text="删除此数据进行对比" />
                                            </ext:GridCommand>
                                        </Commands>
                                        <Listeners>
                                            <Command Handler="return commandcolumn_direct_delete(command, record);" />
                                        </Listeners>
                                    </ext:CommandColumn>
                                </Columns>

                            </ColumnModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
                <ext:Window ID="winCompareCuring" runat="server" Icon="ChartLine" Closable="true" Title="曲线比较"
                Maximizable="true" Maximized="true" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5"  Layout="BorderLayout">
                    <Items>
                        <ext:Panel ID="BorderPanelNorth" runat="server" Region="North">
                            <TopBar>
                                <ext:StatusBar ID="winStatus" runat="server" Height="40" AutoScroll="true" />
                            </TopBar>
                        </ext:Panel>
                        <ext:Chart ID="Chart1" runat="server" StyleSpec="background:#fff;font-size:9pt" Shadow="true" StandardTheme="Category1" Animate="true">
                            <LegendConfig Position="Right" />
                            <Store>
                                <ext:Store ID="Store1" runat="server">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="SecondSpan" />
                                                <ext:ModelField Name="MixingTemp" />
                                                <ext:ModelField Name="MixingPower" />
                                                <ext:ModelField Name="MixingEnergy" />
                                                <ext:ModelField Name="MixingPress" />
                                                <ext:ModelField Name="MixingSpeed" />
                                                   <ext:ModelField Name="MixingPosition" />
                                                  <ext:ModelField Name="PlanDate" />
                                                   <ext:ModelField Name="L1" />
                                                    <ext:ModelField Name="L2" />
                                                     <ext:ModelField Name="L3" />
                                                      <ext:ModelField Name="L4" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <Axes>
                                <ext:NumericAxis Position="Left" Grid="true" Minimum="0" Title="温度(℃)能量(KWH)压力(bar)转速(RPM)功率(KW)"    />
                                <ext:NumericAxis Position="Bottom" Grid="true" MajorTickSteps="20" Minimum="0" Title="时间(S)" />
                            </Axes>
                            <Series>
                                <ext:LineSeries SeriesID="SeriesMixingTemp" Axis="Left"  ShowMarkers="false" XField="SecondSpan" YField="MixingTemp" Title="曲线1">
                                </ext:LineSeries>
                                <ext:LineSeries SeriesID="SeriesMixingPower" Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="MixingPower" Title="曲线2">
                                </ext:LineSeries>
                                <ext:LineSeries SeriesID="SeriesMixingPress" Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="MixingPress" Title="曲线3">
                                </ext:LineSeries>
                                <ext:LineSeries SeriesID="SeriesMixingEnergy" Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="MixingEnergy" Title="曲线4">
                                </ext:LineSeries>
                                <ext:LineSeries SeriesID="SeriesMixingSpeed" Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="MixingSpeed" Title="曲线5">
                                </ext:LineSeries>
                                  <ext:LineSeries SeriesID="SeriesMixingSpeed2" Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="MixingPosition" Title="曲线6">
                                </ext:LineSeries>
                                <ext:LineSeries SeriesID="SeriesMixingSpeed3" Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="L1" Title="曲线7">
                                </ext:LineSeries>
                                <ext:LineSeries SeriesID="SeriesMixingSpeed4" Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="L2" Title="曲线8">
                                </ext:LineSeries>
                                  <ext:LineSeries SeriesID="SeriesMixingSpeed6" Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="L3" Title="曲线9">
                                </ext:LineSeries>
                                  <ext:LineSeries SeriesID="SeriesMixingSpeed5" Axis="Left" ShowMarkers="false" XField="SecondSpan" YField="L4" Title="曲线10">
                                </ext:LineSeries>
                            </Series>
                        </ext:Chart>
                    </Items>
                </ext:Window>
                <ext:Hidden ID="txt_material_code" runat="server"></ext:Hidden>
                <ext:Hidden ID="txt_equip_code" runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_comparison_barcode" runat="server"></ext:Hidden>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
