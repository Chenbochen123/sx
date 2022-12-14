﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_Manage_Search, App_Web_zqfhdfip" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工艺配方浏览</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />

    <script src="<%= Page.ResolveUrl("~/") %>resources/js/default.js"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.store.currentPage = 1;
            App.store.reload();
            return false;
        }
    </script>

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>Search.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel20" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUser">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <Listeners>
                                        <Click Fn="gridPanelRefresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator3" />
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
                        <ext:Container ID="container1" runat="server" Layout="AnchorLayout" AutoHeight="true" Padding="5">
                            <Items>
                                <ext:Container ID="container4" runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="修改开始时间" Flex="1" LabelAlign="Right" />
                                        <ext:DateField ID="txtEndTime" runat="server" FieldLabel="修改结束时间" Flex="1" LabelAlign="Right" />

                                        <ext:TriggerField ID="txtRubberName" runat="server" Flex="1" FieldLabel="胶料名称" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryRubberInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:TriggerField ID="txtMaterialName" runat="server" Flex="1" FieldLabel="物料名称" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryMaterialInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container3" runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:TriggerField ID="txtRecipeEquipName" runat="server" Flex="1" FieldLabel="机台名称" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryEquipInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:NumberField ID="txtRecipeVersionID" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" DecimalPrecision="0" FieldLabel="配方版本">
                                        </ext:NumberField>
                                        <ext:ComboBox ID="txtPmtRecipeType" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="配方类型">
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="txtPmtRecipeState" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="配方状态">
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container2" runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="txtPmtRecipeAudit" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="审核状态">
                                        </ext:ComboBox>
                                        <ext:ToolbarFill ID="toolbarFill1" Flex="1" />
                                        <ext:ToolbarFill ID="toolbarFill2" Flex="1" />
                                        <ext:ToolbarFill ID="toolbarFill3" Flex="1" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                        <ext:Hidden ID="hiddenRubberCode" runat="server"></ext:Hidden>
                        <ext:Hidden ID="hiddenMaterialCode" runat="server"></ext:Hidden>
                        <ext:Hidden ID="hiddenRecipeEquipCode" runat="server"></ext:Hidden>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel1" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <Items>
                        <ext:GridPanel ID="gridPanelCenter" runat="server" Region="Center" Frame="true" Title="机台配方基本信息">
                            <Store>
                                <ext:Store ID="store" runat="server" PageSize="15">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model" runat="server" Name="gridPanelCenterStoreModel">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="RecipeEquipCode" />
                                                <ext:ModelField Name="RecipeMaterialCode" />
                                                <ext:ModelField Name="RecipeVersionID" />
                                                <ext:ModelField Name="RecipeName" />
                                                <ext:ModelField Name="RecipeType" />
                                                <ext:ModelField Name="RecipeMaterialName" />
                                                <ext:ModelField Name="RecipeUserVersion" />
                                                <ext:ModelField Name="RecipeState" />
                                                <ext:ModelField Name="RecipeDefineDate" Type="Date" />
                                                <ext:ModelField Name="RecipeModifyTime" Type="Date" />
                                                <ext:ModelField Name="LotDoneTime" />
                                                <ext:ModelField Name="ShelfLotCount" />
                                                <ext:ModelField Name="LotTotalWeight" />
                                                <ext:ModelField Name="CarbonRecycleType" />
                                                <ext:ModelField Name="CarbonRecycleTime" />
                                                <ext:ModelField Name="OverTempMinTime" />
                                                <ext:ModelField Name="OverTimeSetTime" />
                                                <ext:ModelField Name="OverTempSetTemp" />
                                                <ext:ModelField Name="InPolyMaxTemp" />
                                                <ext:ModelField Name="InPolyMinTemp" />
                                                <ext:ModelField Name="MakeUpTemp" />
                                                <ext:ModelField Name="InPolySetTime" />
                                                <ext:ModelField Name="InCarbonSetTime" />
                                                <ext:ModelField Name="InOilSetTime" />
                                                <ext:ModelField Name="InPowderSetTime" />
                                                <ext:ModelField Name="RollSpeedDiff" />
                                                <ext:ModelField Name="RamPressDiff" />
                                                <ext:ModelField Name="IsUseAreaTemp" />
                                                <ext:ModelField Name="SideTemp" />
                                                <ext:ModelField Name="SideTempDiff" />
                                                <ext:ModelField Name="RollTemp" />
                                                <ext:ModelField Name="RollTempDiff" />
                                                <ext:ModelField Name="DdoorTemp" />
                                                <ext:ModelField Name="DdoorTempDiff" />
                                                <ext:ModelField Name="OperCode" />
                                                <ext:ModelField Name="Remark" />
                                                <ext:ModelField Name="StartDatetime" Type="Date" />
                                                <ext:ModelField Name="EndDatetime" Type="Date" />
                                                <ext:ModelField Name="AuditFlag" />
                                                <ext:ModelField Name="AuditUser" />
                                                <ext:ModelField Name="AuditDateTime" Type="Date" />
                                                <ext:ModelField Name="RecipeDic" />
                                                <ext:ModelField Name="StayTimeSpand" />
                                                <ext:ModelField Name="RearchCode" />
                                                <ext:ModelField Name="CanAuditUser" />
                                                <ext:ModelField Name="EquipName" />
                                                <ext:ModelField Name="MaterialName" />
                                                <ext:ModelField Name="RecipeTypeName" />
                                                <ext:ModelField Name="RecipeStateName" />
                                                <ext:ModelField Name="AuditFlagName" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Sorters>
                                        <ext:DataSorter Property="SeqIdx" Direction="ASC" />
                                    </Sorters>
                                    <Listeners>
                                        <BeforeLoad Fn="onGridPanelRefresh"></BeforeLoad>
                                    </Listeners>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="30" />
                                    <ext:Column ID="EquipName" DataIndex="EquipName" runat="server" Text="机台" Width="100" />
                                    <ext:Column ID="MaterialName" DataIndex="MaterialName" runat="server" Text="物料名称" Align="Center" Width="160" />
                                    <ext:Column ID="RecipeTypeName" DataIndex="RecipeTypeName" runat="server" Text="配方类型" Align="Center" Width="60" />
                                    <ext:Column ID="AuditFlagName" DataIndex="AuditFlagName" runat="server" Text="审核标志" Align="Center" Width="60" />
                                    <ext:Column ID="RecipeStateName" DataIndex="RecipeStateName" runat="server" Text="配方状态" Align="Center" Width="60" />
                                    <ext:Column ID="OverTempMinTime" DataIndex="OverTempMinTime" runat="server" Text="超温排胶最短时间" Align="Center" Width="40" />
                                    <ext:Column ID="RecipeVersionID" DataIndex="RecipeVersionID" runat="server" Text="版本号" Align="Center" Width="40" />
                                    <ext:DateColumn ID="RecipeModifyTime" DataIndex="RecipeModifyTime" runat="server" Text="配方修改时间" Align="Center" Width="140" Format="yyyy-MM-dd HH:mm:ss" />
                                    <ext:DateColumn ID="RecipeDefineDate" DataIndex="RecipeDefineDate" runat="server" Text="配方创建时间" Align="Center" Width="140" Format="yyyy-MM-dd HH:mm:ss" />
                                    <ext:Column ID="RecipeMaterialCode" DataIndex="RecipeMaterialCode" runat="server" Text="物料编号" Align="Center" Width="120" />
                                </Columns>
                            </ColumnModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="pageToolbar" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="progressBarPager" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                            <View>
                                <ext:GridView ID="gvRows" runat="server">
                                    <GetRowClass Fn="SetRowClass" />
                                </ext:GridView>
                            </View>
                            <Listeners>
                                <SelectionChange Handler="if (selected[0]) { #{DetailPanel}.getForm().loadRecord(selected[0]); }" />
                            </Listeners>
                        </ext:GridPanel>

                        <ext:FormPanel ID="DetailPanel" runat="server" Region="East" Layout="AnchorLayout" Title="配方基本信息" Width="300" AutoScroll="true">
                            <TopBar>
                                <ext:Toolbar runat="server" ID="Toolbar1">
                                    <Items>
                                        <ext:ToolbarSeparator ID="toolbarSeparator1" />
                                        <ext:Button runat="server" Icon="BookEdit" Text="修改选定配方的状态" ID="btnChangeState" TextAlign="Left" Flex="1">
                                            <Listeners>
                                                <Click Fn="Save"></Click>
                                            </Listeners>
                                        </ext:Button>
                                        <ext:ToolbarSeparator ID="toolbarSeparator2" />
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Items>
                                <ext:Container ID="container5" runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:CheckboxGroup ID="CheckboxGroup4" runat="server" ColumnsNumber="1" Flex="1" AnchorHorizontal="true">
                                            <Items>
                                                <ext:ComboBox ID="txtRecipeStateEdit" Name="RecipeState" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="配方状态">
                                                </ext:ComboBox>
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="机台" Name="EquipName" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="物料名称" Name="MaterialName" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="配方类型" Name="RecipeTypeName" />

                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="审核标志" Name="AuditFlagName" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="配方编码" Name="RecipeName" />

                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="版本号" Name="RecipeVersionID" />
                                                <ext:DateField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="最后一次修改时间" Name="RecipeModifyTime" Format="yyyy-MM-dd HH:mm:ss" />
                                                <ext:DateField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="配方创建时间" Name="RecipeDefineDate" Format="yyyy-MM-dd HH:mm:ss" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="物料编号" Name="RecipeMaterialCode" />

                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="每车时间" Name="LotDoneTime" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="每架车数" Name="ShelfLotCount" />

                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="配方总重" Name="LotTotalWeight" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="超温排胶最短时间" Name="OverTempMinTime" />

                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="超时排胶时间" Name="OverTimeSetTime" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="超温排胶温度" Name="OverTempSetTemp" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="最高进料温度" Name="InPolyMaxTemp" />

                                                <ext:ComboBox runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="使用三区温度" Name="IsUseAreaTemp">
                                                    <Items>
                                                        <ext:ListItem Value="0" Text="不使用"></ext:ListItem>
                                                        <ext:ListItem Value="1" Text="使用"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="侧壁温度" Name="SideTemp" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="转子温度" Name="RollTemp" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="卸料门温度" Name="DdoorTemp" />
                                                <ext:ComboBox runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="炭黑回收方式" Name="CarbonRecycleType">
                                                    <Items>
                                                        <ext:ListItem Value="0" Text="不回收"></ext:ListItem>
                                                        <ext:ListItem Value="1" Text="回收"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="炭黑回收时间" Name="CarbonRecycleTime" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="配方唯一标示" Name="ObjID" />
                                                <ext:TextField runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="备注" Name="Remark" />
                                            </Items>
                                        </ext:CheckboxGroup>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
