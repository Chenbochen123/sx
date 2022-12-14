﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_ProductReview_Search, App_Web_1ixwfq50" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>条码追溯</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
     <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>Search.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.store.currentPage = 1;
            App.refreshHidden.setValue("1");
            App.store.reload();
            return false;
        }
    </script>
    <script type="text/javascript">
        var basicInfoLoad = function (barcode) {
            App.direct.BasicInfoLoad(barcode, {
                success: function (result) {
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('提示', errorMsg);
                }
            });
        }

    </script>
</head>
<body>
    <form id="form" runat="server">
         <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
     
        <ext:ResourceManager ID="resourceManager" runat="server" />
        <ext:Viewport ID="viewport" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="northPanel" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="northToolbar">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="gridPanelRefresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" Icon="Find" Text="正向追溯" ID="Button1">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行追溯" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="forwordSearch"></Click>
                                    </Listeners>
                                </ext:Button>
                                  <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip2" runat="server" Html="点击将查询结果导出到Excel中" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel ID="panelNorthQuery" runat="server" Layout="AnchorLayout" AutoHeight="true">
                            <Items>
                                <ext:Container ID="container1" runat="server" Layout="AnchorLayout" AutoHeight="true" Padding="5">
                                    <Items>
                                        <ext:Container ID="container4" runat="server" Layout="HBoxLayout" Padding="5">
                                            <Items>
                                                <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="生成开始时间" Flex="2" LabelAlign="Right" Format="yyyy-MM-dd">
                                                </ext:DateField>
                                                <ext:DateField ID="txtEndTime" runat="server" FieldLabel="生成结束时间" Flex="2" LabelAlign="Right" Format="yyyy-MM-dd">
                                                </ext:DateField>
                                                <ext:ComboBox ID="txtPptShift" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="班次信息">
                                                </ext:ComboBox>
                                                <ext:ComboBox ID="txtPptClass" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="班组信息">
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container3" runat="server" Layout="HBoxLayout" Padding="5">
                                            <Items>
                                                <ext:TextField ID="txtshiftbarcode" runat="server" FieldLabel="架子条码" LabelAlign="Right" Flex="2" />
                                                <ext:TextField ID="txtBarcode" runat="server" FieldLabel="车条码" LabelAlign="Right" Flex="2" />
                                                <ext:TriggerField ID="txMaterialName" runat="server" Flex="1" FieldLabel="物料名称" LabelAlign="Right" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="QueryMaterialInfo" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                
                                                <ext:TriggerField ID="txtEquipName" runat="server" Flex="1" FieldLabel="机台名称" LabelAlign="Right" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="QueryEquipInfo" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                                <ext:Hidden ID="hiddenPmtRecipeID" runat="server"></ext:Hidden>
                                <ext:Hidden ID="hiddenEquipCode" runat="server"></ext:Hidden>
                                <ext:Hidden ID="hiddenMaterialCode" runat="server"></ext:Hidden>
                                <ext:Hidden ID="hiddenSearchTimes" runat="server" Text="0"></ext:Hidden>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel1" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <Items>
                        <ext:GridPanel ID="gridPanelCenter" runat="server" Region="Center">
                            <Store>
                                <ext:Store ID="store" runat="server" PageSize="30">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData" AutoDataBind="false" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="Barcode" />
                                                <ext:ModelField Name="PlanDate" />
                                                <ext:ModelField Name="MaterCode" />
                                                <ext:ModelField Name="MaterName" />
                                                <ext:ModelField Name="EquipCode" />
                                                <ext:ModelField Name="EquipName" />
                                                <ext:ModelField Name="SerialID" />
                                                <ext:ModelField Name="ShiftID" />                 
                                                <ext:ModelField Name="ShiftName" />
                                                <ext:ModelField Name="ClassID" />
                                                <ext:ModelField Name="ClassName" />
                                                <ext:ModelField Name="EdtCode" />
                                                <ext:ModelField Name="PlanID" />
                                                <ext:ModelField Name="StartDatetime" />
                                                <ext:ModelField Name="DoneRtime" />
                                                <ext:ModelField Name="DoneAllRtime" />
                                                <ext:ModelField Name="BwbTime" />
                                                <ext:ModelField Name="SetWeight" />
                                                <ext:ModelField Name="RealWeight" />
                                                <ext:ModelField Name="ErrorSgn" />
                                                <ext:ModelField Name="ShelfBarcode" />
                                                <ext:ModelField Name="ShelfUpdate" />
                                                <ext:ModelField Name="TestResult" />
                                                <ext:ModelField Name="TestResultName" />
                                                <ext:ModelField Name="PjTemp" />
                                                <ext:ModelField Name="PjPower" />
                                                <ext:ModelField Name="PjEner" />
                                                <ext:ModelField Name="PjStatus" />
                                                <ext:ModelField Name="MixStatus" />
                                                <ext:ModelField Name="MixStatusName" />
                                                <ext:ModelField Name="PolyDisTime" />
                                                <ext:ModelField Name="CBDisTime" />
                                                <ext:ModelField Name="OilDisTime" />
                                                <ext:ModelField Name="PowderDisTime" />
                                                <ext:ModelField Name="SerialBatchID" />
                                                <ext:ModelField Name="CBBatch" />
                                                <ext:ModelField Name="OilBatch" />
                                                <ext:ModelField Name="PolyBatch" />
                                                <ext:ModelField Name="PowderBatch" />
                                                <ext:ModelField Name="SmallBatch" />
                                                <ext:ModelField Name="UsedFlag" />
                                                <ext:ModelField Name="UsedDatetime" />
                                                <ext:ModelField Name="UsedPlanid" />
                                                <ext:ModelField Name="Workerbarcode" />
                                                <ext:ModelField Name="UserName" />
                                                <ext:ModelField Name="MemNote" />
                                                <ext:ModelField Name="WarningSgn" />
                                                <ext:ModelField Name="Shelfnum" />
                                                <ext:ModelField Name="LimitTime" />
                                                <ext:ModelField Name="Maxtime" />
                                                <ext:ModelField Name="LotEnergy" />
                                                <ext:ModelField Name="SDSTime" />
                                                <ext:ModelField Name="ZJSID" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="30" />
                                    <ext:Column ID="ShelfBarCode" DataIndex="ShelfBarcode" runat="server" Text="架子条码" Width="120" />
                                    <ext:Column ID="Barcode" DataIndex="Barcode" runat="server" Text="车条码" Width="120" />
                                    <ext:Column ID="EquipName" DataIndex="EquipName" runat="server" Text="机台" Width="100" />
                                    <ext:Column ID="ShiftID" DataIndex="ShiftName" runat="server" Text="班次" Width="35" />
                                    <ext:Column ID="ClassID" DataIndex="ClassName" runat="server" Text="班组" Width="35" />
                                    <ext:Column ID="MaterName" DataIndex="MaterName" runat="server" Text="物料名称" Width="100" />
                                    <ext:Column ID="PlanID" DataIndex="PlanID" runat="server" Text="计划编号" Width="100" />
                                    <ext:Column ID="SerialID" DataIndex="SerialID" runat="server" Text="车次号" Width="45" />
                                    <ext:Column ID="StartDatetime" DataIndex="StartDatetime" runat="server" Text="开始生产时间" Width="120" />
                                    <ext:Column ID="SetWeight" DataIndex="SetWeight" runat="server" Text="设重" Width="50" />
                                    <ext:Column ID="RealWeight" DataIndex="RealWeight" runat="server" Text="实重" Width="50" />
                                    <ext:Column ID="TestResult" DataIndex="TestResultName" runat="server" Text="质检结果" Width="60" />
                                    <ext:Column ID="SerialBatchID" DataIndex="SerialBatchID" runat="server" Text="累计车次" Width="60" />
                                    <ext:Column ID="MixStatus" DataIndex="MixStatusName" runat="server" Text="生产状态" Width="60" />
                                    <ext:Column ID="ZJSID" DataIndex="ZJSID" runat="server" Text="主机手编号" Width="60" />
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single">
                                </ext:RowSelectionModel>
                            </SelectionModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="pageToolbar" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="progressBarPager" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                            <Listeners>
                                <SelectionChange Handler="basicInfoLoad(App.gridPanelCenter.getRowsValues({selectedOnly:true}));" />
                                <CellDblClick Fn="gridPanelCellDblClick" />
                            </Listeners>
                        </ext:GridPanel>

                        <ext:FormPanel ID="DetailPanel" runat="server" Region="East" Layout="AnchorLayout" Title="配方基本信息" Width="290" AutoScroll="true">
                            <Items>
                                <ext:Container ID="container5" runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:CheckboxGroup ID="CheckboxGroup4" runat="server" ColumnsNumber="1" Flex="1" AnchorHorizontal="true">
                                            <Items>
                                               
                                                <ext:TextField runat="server" ID="detailShelfBarcode" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="车架子条码" Name="ShelfBarcode" />
                                                <ext:TextField runat="server" ID="detailBarcode" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="车条码" Name="Barcode" />
                                                <ext:TextField runat="server" ID="detailEquipName" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="机台" Name="EquipName" />
                                                <ext:TextField runat="server" ID="detailShiftName" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="班次" Name="ShiftName" />
                                                <ext:TextField runat="server" ID="detailClassName" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="班组" Name="ClassName" />

                                                <ext:TextField runat="server" ID="detailMaterName" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="物料名称" Name="MaterName" />
                                                <ext:TextField runat="server" ID="detailPlanID" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="计划编号" Name="PlanID" />

                                                <ext:TextField runat="server" ID="detailSerialID" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="车次号" Name="SerialID" />
                                                <ext:DateField runat="server" ID="detailStartDatetime" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="开始生产时间" Name="StartDatetime" Format="yyyy-MM-dd HH:mm:ss" />
                                                <ext:TextField runat="server" ID="detailSetWeight" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="设重" Name="SetWeight" />

                                                <ext:TextField runat="server" ID="detailRealWeight" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="实重" Name="RealWeight" />
                                                <ext:TextField runat="server" ID="detailTestResultName" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="质检结果" Name="TestResultName" />

                                                <ext:TextField runat="server" ID="detailSerialBatchID" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="累计车次" Name="SerialBatchID" />
                                                <ext:TextField runat="server" ID="detailMixStatusName" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="生产状态" Name="MixStatusName" />

                                                <ext:TextField runat="server" ID="detailDoneAllRtime" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="混炼总时间" Name="DoneAllRtime" />
                                                <ext:TextField runat="server" ID="detailLotEnergy" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="每车能量" Name="LotEnergy" />
                                                <ext:TextField runat="server" ID="detailBwbTime" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="间隔时间" Name="BwbTime" />
                                                <ext:TextField runat="server" ID="detailPjTemp" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="排胶温度" Name="PjTemp" />
                                                <ext:TextField runat="server" ID="detailPolyDisTime" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="加胶时间" Name="PolyDisTime" />
                                                <ext:TextField runat="server" ID="detailCBDisTime" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="加炭黑时间" Name="CBDisTime" />
                                                <ext:TextField runat="server" ID="detailOilDisTime" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="加油时间" Name="OilDisTime" />
                                                <ext:TextField runat="server" ID="detailUserName" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="操作工" Name="UserName" />
                                                <ext:TextField runat="server" ID="detailMemNote" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="备注" Name="MemNote" />
                                            </Items>
                                        </ext:CheckboxGroup>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                </ext:Panel>
                <ext:Hidden runat="server" ID="refreshHidden" />
                 
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
