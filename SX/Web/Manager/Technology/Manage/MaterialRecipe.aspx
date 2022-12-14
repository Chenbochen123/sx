﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialRecipe.aspx.cs" Inherits="Manager_Technology_Manage_MaterialRecipe" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工艺配方管理</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />

    <script src="<%= Page.ResolveUrl("~/") %>resources/js/default.js"></script>
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.store.currentPage = 1;
            App.store.reload();
            return false;
        }
 
    </script>
     <%--表格加竖线--%>
    <style type="text/css">
        .x-grid-cell-inner {
            border-right:1px solid
                #d6d3d3;
        }
        .x-grid-row td, .x-grid-summary-row td {
            padding-right: 0px;
        }
        .x-grid-row {
            border-top-width:0px;
            border-bottom-width:0px;
        }
        </style>
    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>MaterialRecipe.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
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
                                <ext:Button runat="server" Icon="DiskUpload" Text="向上新增" ID="btnAddUpload" Hidden="true">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttAdd" runat="server" Html="选中的物料向上添加配方" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="btnAddClick"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" Icon="DiskDownload" Text="新增工艺配方" ID="btnAddDownload">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="新增选中信息的工艺配方" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="btnAddClick"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator3" />
                                <ext:Button runat="server" ID="btnDeletePmtRecipe" Icon="Delete" Text="配方删除">
                                    <Listeners>
                                        <Click Fn="btnDeletePmtRecipeClick"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator1" />
                                <ext:Button runat="server" ID="btnCopyPmtRecipe" Icon="PageCopy" Text="配方另存">
                                    <Listeners>
                                        <Click Fn="btnCopyPmtRecipeClick"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator2" />
                                 <ext:Button runat="server" Icon="DiskDownload" Text="导入配方" ID="Button1" Hidden="True">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip2" runat="server" Html="导入新配方" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="RecipeInfolead"></Click>
                                    </Listeners>
                                </ext:Button>
                                 
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Container ID="container1" runat="server" Layout="ColumnLayout"  Height="36" >
                            <Items>
                                <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".33" Padding="5">
                                    <Items>
                                        <ext:TriggerField ID="txtRubberName" runat="server" Flex="1" FieldLabel="胶料名称" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryRubberInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                    </Items>
                                </ext:Container>
                              
                         
                                <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"  Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="txtPmtRecipeType" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="配方类型">
                                            <Listeners>
                                                <Select Fn="QueryRubberTree"></Select>
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"  Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="txtPmtRecipeState" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="配方状态">
                                            <Listeners>
                                                <Select Fn="QueryRubberTree"></Select>
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container> 
                            </Items>
                        </ext:Container>
                        <ext:Hidden ID="hiddenRubberCode" runat="server"></ext:Hidden>
                        <ext:Hidden ID="hiddenMaterialID" runat="server"></ext:Hidden>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel1" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <Items>
                        <ext:TreePanel ID="treePanelUser" runat="server" Icon="FolderGo" Region="West" Width="320" AutoHeight="true" RootVisible="false">
                            <Store>
                                <ext:TreeStore ID="treeStoreUser" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.TreePanelBindData">
                                        </ext:PageProxy>
                                    </Proxy>
                                    <Root>
                                        <ext:Node NodeID="Root" Expanded="true" />
                                    </Root>
                                </ext:TreeStore>
                            </Store>
                            <DirectEvents>
                                <Select OnEvent="RefreshGridInfo" />
                            </DirectEvents>
                        </ext:TreePanel>
                        <ext:Panel ID="Panel2" runat="server" Region="Center"  Layout="BorderLayout">
                            <Items>
                                <ext:GridPanel ID="gridPanelCenter" runat="server" Region="Center" Frame="true" Title="机台配方基本信息"  Height="115" Split="true">
                                    <Store>
                                        <ext:Store ID="store" runat="server" PageSize="0">
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
                                                        <ext:ModelField Name="UseredtCode" />
                                                        <ext:ModelField Name="opername" />
                                                        <ext:ModelField Name="auditname" />
                                                        <ext:ModelField Name="RecipeModifyTime" Type="Date" />
                                                      
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Listeners>
                                                <BeforeLoad Fn="onGridPanelRefresh"></BeforeLoad>
                                            </Listeners>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="columnModel" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="30" />
                                            <ext:Column ID="EquipName" DataIndex="EquipName" runat="server" Text="机台" Width="80" />
                                            <ext:Column ID="MaterialName" DataIndex="MaterialName" runat="server" Text="物料名称" Align="Center" Width="100" />
                                            <ext:Column ID="RecipeTypeName" DataIndex="RecipeTypeName" runat="server" Text="配方类型" Align="Center" Width="60" />
                                            <ext:Column ID="AuditFlagName" DataIndex="AuditFlagName" runat="server" Text="审核标志" Align="Center" Width="60" />
                                            <ext:Column ID="RecipeStateName" DataIndex="RecipeStateName" runat="server" Text="配方状态" Align="Center" Width="60" />
                                            <ext:Column ID="OverTempMinTime" DataIndex="OverTempMinTime" runat="server" Text="超温排胶最短时间" Align="Center" Width="120" />
                                            <ext:Column ID="RecipeVersionID" DataIndex="RecipeVersionID" runat="server" Text="版本号" Align="Center" Width="50" />
                                            <ext:DateColumn ID="RecipeDefineDate" DataIndex="RecipeDefineDate" runat="server" Text="配方创建时间" Align="Center" Width="140" Format="yyyy-MM-dd HH:mm:ss" />
                                            <ext:Column ID="RecipeMaterialCode" DataIndex="RecipeMaterialCode" runat="server" Text="物料编号" Align="Center" Width="120" />
                                            <ext:Column ID="Column1" DataIndex="UseredtCode" runat="server" Text="用户版本号" Align="Center" Width="120" />
                                            <ext:Column ID="Column2" DataIndex="opername" runat="server" Text="操作人" Align="Center" Width="55" />
                                            <ext:Column ID="Column3" DataIndex="auditname" runat="server" Text="审核人" Align="Center" Width="55" />
                                            <ext:DateColumn ID="Column4" DataIndex="RecipeModifyTime" runat="server" Text="修改时间" Align="Center" Width="140"  Format="yyyy-MM-dd HH:mm:ss" />
                                        </Columns>
                                    </ColumnModel>
                                    <TopBar>
                                        <ext:Toolbar runat="server" ID="Toolbar1">
                                            <Items>
                                                <ext:Button runat="server" Icon="FolderStar" Text="显示正用版本" ID="btnShowEnablePmt" Pressed="true" EnableToggle="true" ToggleGroup="Group1">
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip4" runat="server" Html="显示当前正用的配方" />
                                                    </ToolTips>
                                                    <Listeners>
                                                        <Click Fn="ShowAllPmtRefresh"></Click>
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button runat="server" Icon="FolderMagnify" Text="显示全部版本" ID="btnShowAllPmt" Pressed="false" EnableToggle="true" ToggleGroup="Group1">
                                                    <ToolTips>
                                                        <ext:ToolTip ID="ToolTip3" runat="server" Html="显示当前所有配方" />
                                                    </ToolTips>
                                                    <Listeners>
                                                        <Click Fn="ShowAllPmtRefresh"></Click>
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Hidden ID="isShowAllPmt" runat="server" Text="1"></ext:Hidden>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
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
                                        <CellDblClick Fn="gridPanelCellDblClick" />
                                    </Listeners>
                                </ext:GridPanel>

                                <ext:FormPanel ID="DetailPanel" runat="server" Region="South" Layout="AnchorLayout" Title="配方基本信息" Height="290" AutoScroll="true" Split="true">
                                    <Items>
                                        <ext:Container ID="container5" runat="server" Layout="HBoxLayout">
                                            <Items>
                                                <ext:CheckboxGroup ID="CheckboxGroup4" runat="server" ColumnsNumber="3" Flex="1" AnchorHorizontal="true">
                                                    <Items>
                                                        <ext:TextField ID="TextField0" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="机台" Name="EquipName" />
                                                        <ext:TextField ID="TextField1" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="物料名称" Name="MaterialName" />
                                                        <ext:TextField ID="TextField2" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="配方类型" Name="RecipeTypeName" />

                                                        <ext:TextField ID="TextField3" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="审核标志" Name="AuditFlagName" />
                                                        <ext:TextField ID="TextField4" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="配方状态" Name="RecipeStateName" />
                                                        <ext:TextField ID="TextField19" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="配方编码" Name="RecipeName" />

                                                        <ext:TextField ID="TextField6" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="版本号" Name="RecipeVersionID" />
                                                        <ext:DateField ID="TextField7" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="配方创建时间" Name="RecipeDefineDate" Format="yyyy-MM-dd HH:mm:ss" />
                                                        <ext:TextField ID="TextField8" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="物料编号" Name="RecipeMaterialCode" />

                                                        <ext:DateField ID="TextField9" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="最后一次修改时间" Name="RecipeModifyTime" Format="yyyy-MM-dd HH:mm:ss" />
                                                        <ext:TextField ID="TextField10" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="每车时间" Name="LotDoneTime" />
                                                        <ext:TextField ID="TextField11" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="每架车数" Name="ShelfLotCount" />

                                                        <ext:TextField ID="TextField12" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="配方总重" Name="LotTotalWeight" />
                                                        <ext:TextField ID="TextField5" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="超温排胶最短时间" Name="OverTempMinTime" />

                                                        <ext:TextField ID="TextField15" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="超时排胶时间" Name="OverTimeSetTime" />
                                                        <ext:TextField ID="TextField16" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="超温排胶温度" Name="OverTempSetTemp" />
                                                        <ext:TextField ID="TextField17" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="最高进料温度" Name="InPolyMaxTemp" />
                                                        <ext:TextField ID="TextField13" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="工艺版本号" Name="RearchCode" />

                                                        <%--  <ext:TextField ID="TextField20" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="开始使用时间" Name="StartDatetime" />
                                                        <ext:TextField ID="TextField21" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="结束使用时间" Name="EndDatetime" />--%>
                                                    </Items>
                                                </ext:CheckboxGroup>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container3" runat="server" Layout="HBoxLayout"  >
                                            <Items>
                                                <ext:CheckboxGroup ID="CheckboxGroup1" runat="server" ColumnsNumber="3" Flex="1" AnchorHorizontal="true">
                                                    <Items>
                                                        <ext:ComboBox ID="TextField18" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="使用三区温度" Name="IsUseAreaTemp">
                                                            <Items>
                                                                <ext:ListItem Value="0" Text="不使用"></ext:ListItem>
                                                                <ext:ListItem Value="1" Text="使用"></ext:ListItem>
                                                            </Items>
                                                        </ext:ComboBox>
                                                        <ext:TextField ID="TextField20" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="侧壁温度" Name="SideTemp" />
                                                        <ext:TextField ID="TextField21" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="转子温度" Name="RollTemp" />
                                                        <ext:TextField ID="TextField22" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="卸料门温度" Name="DdoorTemp" />
                                                        <ext:ComboBox ID="ComboBox1" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="炭黑回收方式" Name="CarbonRecycleType">
                                                            <Items>
                                                                <ext:ListItem Value="0" Text="不回收"></ext:ListItem>
                                                                <ext:ListItem Value="1" Text="回收"></ext:ListItem>
                                                            </Items>
                                                        </ext:ComboBox>
                                                        <ext:TextField ID="TextField14" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="炭黑回收时间" Name="CarbonRecycleTime" />
                                                    </Items>
                                                </ext:CheckboxGroup>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container6" runat="server" Layout="HBoxLayout" >
                                            <Items> 
                                                <ext:CheckboxGroup ID="CheckboxGroup2" runat="server" ColumnsNumber="3" Flex="1" AnchorHorizontal="true">
                                                    <Items>
                                                        <ext:TextField ID="txtRecipeObjID" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="配方唯一标示" Name="ObjID" />
                                                        <ext:TextField ID="txtYonghu" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="用户版本号" Name="UseredtCode" />
                                                   
                                               
                                                        <ext:TextField ID="TextField25" runat="server" ReadOnly="true" Flex="1" LabelAlign="Right" FieldLabel="备注" Name="Remark" Visible="false" />
                                                  </Items>
                                                </ext:CheckboxGroup>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:FormPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
