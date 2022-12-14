﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_Report_PlanLotReport, App_Web_0yyi2bmr" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>配方日志查询</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />

    <script src="<%= Page.ResolveUrl("~/") %>resources/js/default.js"></script>
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.gridPanel1.store.currentPage = 1;
            App.gridPanel1.store.reload();
            return false;
        }
        var gridPanelItemClick = function (view, record, tr, index, event, fn) {
            App.hiddenMaterialCode.setValue(record.data.RecipeMaterialCode);
            App.gridPanel2.store.currentPage = 1;
            App.gridPanel2.store.reload();
        }
    </script>


    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>PlanLotReport.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel20" runat="server" Region="North" AutoHeight="true">
                    <BottomBar>
                        <ext:StatusBar ID="StatusBar1" runat="server" Height="24" Text=""></ext:StatusBar>
                    </BottomBar>
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUser">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttAdd" runat="server" Html="查询需要对比的信息" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="gridPanelRefresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator2" />
                                <ext:Button runat="server" Icon="ArrowSwitchBluegreen" Text="更详细批记录" ID="btnSwitch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="查看更详细批记录报表" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="ShowPlanLotReport"></Click>
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
                                        <ext:TriggerField ID="txtEquipName" runat="server" Flex="1" FieldLabel="机台名称" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryEquipInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="计划开始时间" Flex="1" LabelAlign="Right">
                                        </ext:DateField>
                                        <ext:DateField ID="txtEndTime" runat="server" FieldLabel="计划结束时间" Flex="1" LabelAlign="Right">
                                        </ext:DateField>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container3" runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="txtPptClass" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="班组信息">
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="txtPptShift" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="班次信息">
                                        </ext:ComboBox>
                                        <ext:TriggerField ID="txtPmtRecipe" runat="server" Flex="1" FieldLabel="配方信息" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryPmtRecipeInfo" />
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
                <ext:Panel ID="Panel1" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <Items>
                        <ext:GridPanel ID="gridPanel1" runat="server" Region="West" Frame="true" Width="420" Title="生产计划执行统计信息">
                            <Store>
                                <ext:Store ID="gridPanel1Store" runat="server" PageSize="15">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData1" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="RecipeMaterialCode" />
                                                <ext:ModelField Name="RecipeMaterialName" />
                                                <ext:ModelField Name="PlanNum" />
                                                <ext:ModelField Name="RealNum" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Listeners>
                                        <BeforeLoad Handler="App.gridPanel2.store.removeAll(); App.hiddenMaterialCode.setValue('');"></BeforeLoad>
                                    </Listeners>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel1" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="30" />
                                    <ext:Column ID="ShowRecipeMaterialCode1" DataIndex="RecipeMaterialCode" runat="server" Text="胶料编号" Align="Center" Width="120" />
                                    <ext:Column ID="ShowMaterialName1" DataIndex="RecipeMaterialName" runat="server" Text="胶料名称" Align="Center" Flex="1" />
                                    <ext:Column ID="ShowPlanNum1" DataIndex="PlanNum" runat="server" Text="设定数量" Align="Center" Width="60" />
                                    <ext:Column ID="ShowRealNum1" DataIndex="RealNum" runat="server" Text="完成数量" Align="Center" Width="60" />
                                </Columns>
                            </ColumnModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="pageToolbar1" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="progressBarPager1" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                            <Listeners>
                                <ItemClick Fn="gridPanelItemClick"></ItemClick>
                            </Listeners>
                        </ext:GridPanel>

                        <ext:GridPanel ID="gridPanel2" runat="server" Region="Center" Frame="true" Flex="1" Title="生产计划执行明细信息">
                            <Store>
                                <ext:Store ID="gridPanel2Store" runat="server" PageSize="15">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData2" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="PlanID" />
                                                <ext:ModelField Name="ClassName" />
                                                <ext:ModelField Name="ShiftName" />
                                                <ext:ModelField Name="RecipeMaterialCode" />
                                                <ext:ModelField Name="RecipeMaterialName" />
                                                <ext:ModelField Name="PlanNum" />
                                                <ext:ModelField Name="RealNum" />
                                                <ext:ModelField Name="RealStartTime" Type="Date" />
                                                <ext:ModelField Name="RealEndtime" Type="Date" />
                                                <ext:ModelField Name="PlanStateName" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel2" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol2" runat="server" Width="30" />
                                    <ext:Column ID="ShowClassName2" DataIndex="ShiftName" runat="server" Text="班次" Align="Center" Width="36" />
                                    <ext:Column ID="ShowShiftName2" DataIndex="ClassName" runat="server" Text="班组" Align="Center" Width="36" />
                                    <ext:Column ID="ShowRecipeMaterialCode2" DataIndex="RecipeMaterialCode" runat="server" Text="胶料编号" Align="Center" Width="100" />
                                    <ext:Column ID="ShowMaterialName2" DataIndex="RecipeMaterialName" runat="server" Text="胶料名称" Align="Center" Flex="1" />
                                    <ext:Column ID="ShowPlanNum2" DataIndex="PlanNum" runat="server" Text="设定数量" Align="Center" Width="60" />
                                    <ext:Column ID="ShowRealNum2" DataIndex="RealNum" runat="server" Text="完成数量" Align="Center" Width="60" />
                                    <ext:DateColumn ID="ShowBeginTime2" DataIndex="RealStartTime" runat="server" Text="开始时间" Align="Center" Width="160" Format="yyyy-MM-dd HH:mm:ss" />
                                    <ext:DateColumn ID="ShowEndTime2" DataIndex="RealEndtime" runat="server" Text="结束时间" Align="Center" Width="160" Format="yyyy-MM-dd HH:mm:ss" />
                                    <ext:Column ID="ShowPlanState2" DataIndex="PlanStateName" runat="server" Text="完成标志" Align="Center" Width="60" />
                                </Columns>
                            </ColumnModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar2" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="progressBarPager2" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
         <ext:Window ID="Manager_ReportCenter_CommonReportView_CommonReportView_Window" runat="server" Maximized="true" Title="车报表详细信息"  Modal="true" Closable="true" Hidden="true" Html="<iframe src='Default.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>">
            <TopBar>
                <ext:Toolbar runat="server" ID="Toolbar1">
                    <Items>
                        <ext:ToolbarSeparator ID="toolbarSeparator4" />
                        <ext:Button runat="server" Icon="Cancel" Text="关闭" ID="Button1">
                           <Listeners>
                               <Click Handler="#{Manager_ReportCenter_CommonReportView_CommonReportView_Window}.close()"></Click>
                           </Listeners>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
        </ext:Window>
    </form>
</body>
</html>
