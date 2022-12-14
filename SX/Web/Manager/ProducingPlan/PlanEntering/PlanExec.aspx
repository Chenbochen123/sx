﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanExec.aspx.cs" Inherits="Manager_ProducingPlan_PlanEntering_PlanExec" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>计划录入</title>
        <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />

    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>PlanExec.js" type="text/javascript"></script>
    <style>
        .x-grid-body .x-grid-cell-Cost
        {
            background-color: #f1f2f4;
        }
        
        .x-grid-row-summary .x-grid-cell-Cost .x-grid-cell-inner
        {
            background-color: #e1e2e4;
        }
        
        .task .x-grid-cell-inner
        {
            padding-left: 15px;
        }
        
        .x-grid-row-summary .x-grid-cell-inner
        {
            font-weight: bold;
            font-size: 11px;
            background-color: #f1f2f4;
        }
    </style>
    <style>
        /* style rows on mouseover */
        .x-grid-row-over .x-grid-cell-inner
        {
            font-weight: bold;
        }
    </style>
    <style type="text/css">
        .indoor-b
        {
            color: Blue;
        }
        .indoor-r
        {
            color: Red;
        }
        .indoor-g
        {
            color: Green;
        }
        .indoor-y
        {
            color: Purple;
        }
    </style>
    <script type="text/javascript">
        function ChangeDate() {
            App.direct.DateChage({
                success: function (result) {
                    if (result) {

                    }
                    else {
                        Ext.Msg.alert('错误！', '请对应机台信息！');
                    }
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('Failure', errorMsg);
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Store ID="RecipeMaterialNameStore" runat="server" OnReadData="RecipeMaterialNameStoreRefresh">
        <Model>
            <ext:Model ID="Model4" runat="server">
                <Fields>
                    <ext:ModelField Name="RecipeMaterialCode" Type="String" Mapping="RecipeMaterialCode" />
                    <ext:ModelField Name="RecipeMaterialName" Type="String" Mapping="RecipeMaterialName" />
                </Fields>
            </ext:Model>
        </Model>
        <Reader>
            <ext:ArrayReader />
        </Reader>
    </ext:Store>
    <ext:Store ID="RecipeNameStore" runat="server" OnReadData="RecipeNameStoreRefresh">
        <Proxy>
            <ext:PageProxy />
        </Proxy>
        <Model>
            <ext:Model ID="Model6" runat="server">
                <Fields>
                    <ext:ModelField Name="RecipeName" Type="String" Mapping="RecipeName" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Store ID="AddRecipeNameStore" runat="server">
        <Model>
            <ext:Model ID="Model7" runat="server">
                <Fields>
                    <ext:ModelField Name="RecipeName" Type="String" Mapping="RecipeName" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Store ID="ZaoPlanStore" runat="server" AutoSync="true">
        <Sorters>
            <ext:DataSorter Property="PriLevel" Direction="ASC" />
        </Sorters>
        <Model>
            <ext:Model ID="Model1" runat="server" Name="gridPanelZaoStoreModel" IDProperty="ID">
                <Fields>
                    <ext:ModelField Name="RecipeMaterialName" Type="String" Mapping="RecipeMaterialName" />
                    <ext:ModelField Name="RecipeMaterialCode" Type="String" Mapping="RecipeMaterialCode" />
                    <ext:ModelField Name="RecipeEquipCode" Type="String" Mapping="RecipeEquipCode" />
                    <ext:ModelField Name="RecipeName" Type="String" Mapping="RecipeName" />
                    <ext:ModelField Name="PlanNum" Type="Int" Mapping="PlanNum" />
                    <ext:ModelField Name="PriLevel" Type="Int" Mapping="PriLevel" />
                    <ext:ModelField Name="ShiftName" Type="String" Mapping="ShiftName" />
                    <ext:ModelField Name="ShiftID" Type="Int" Mapping="ShiftID" />
                    <ext:ModelField Name="OperDatetime" Type="String" Mapping="OperDatetime" />
                    <ext:ModelField Name="UserName" Type="String" Mapping="UserName" />
                    <ext:ModelField Name="PlanID" Type="String" Mapping="PlanID" />
                    <ext:ModelField Name="PlanState" Type="String" Mapping="PlanState" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Store ID="ZhongPlanStore" runat="server" AutoSync="true">
        <Sorters>
            <ext:DataSorter Property="PriLevel" Direction="ASC" />
        </Sorters>
        <Model>
            <ext:Model ID="Model2" runat="server" Name="gridPanelZaoStoreModel" IDProperty="ID">
                <Fields>
                    <ext:ModelField Name="RecipeMaterialName" Type="String" Mapping="RecipeMaterialName" />
                    <ext:ModelField Name="RecipeMaterialCode" Type="String" Mapping="RecipeMaterialCode" />
                    <ext:ModelField Name="RecipeEquipCode" Type="String" Mapping="RecipeEquipCode" />
                    <ext:ModelField Name="RecipeName" Type="String" Mapping="RecipeName" />
                    <ext:ModelField Name="PlanNum" Type="Int" Mapping="PlanNum" />
                    <ext:ModelField Name="PriLevel" Type="Int" Mapping="PriLevel" />
                    <ext:ModelField Name="ShiftName" Type="String" Mapping="ShiftName" />
                    <ext:ModelField Name="ShiftID" Type="Int" Mapping="ShiftID" />
                    <ext:ModelField Name="OperDatetime" Type="String" Mapping="OperDatetime" />
                    <ext:ModelField Name="UserName" Type="String" Mapping="UserName" />
                    <ext:ModelField Name="PlanID" Type="String" Mapping="PlanID" />
                    <ext:ModelField Name="PlanState" Type="String" Mapping="PlanState" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Store ID="YePlanStore" runat="server" AutoSync="true">
        <Sorters>
            <ext:DataSorter Property="PriLevel" Direction="ASC" />
        </Sorters>
        <Model>
            <ext:Model ID="Model3" runat="server" Name="gridPanelZaoStoreModel" IDProperty="ID">
                <Fields>
                    <ext:ModelField Name="RecipeMaterialName" Type="String" Mapping="RecipeMaterialName" />
                    <ext:ModelField Name="RecipeMaterialCode" Type="String" Mapping="RecipeMaterialCode" />
                    <ext:ModelField Name="RecipeEquipCode" Type="String" Mapping="RecipeEquipCode" />
                    <ext:ModelField Name="RecipeName" Type="String" Mapping="RecipeName" />
                    <ext:ModelField Name="PlanNum" Type="Int" Mapping="PlanNum" />
                    <ext:ModelField Name="PriLevel" Type="Int" Mapping="PriLevel" />
                    <ext:ModelField Name="ShiftName" Type="String" Mapping="ShiftName" />
                    <ext:ModelField Name="ShiftID" Type="Int" Mapping="ShiftID" />
                    <ext:ModelField Name="OperDatetime" Type="String" Mapping="OperDatetime" />
                    <ext:ModelField Name="UserName" Type="String" Mapping="UserName" />
                    <ext:ModelField Name="PlanID" Type="String" Mapping="PlanID" />
                    <ext:ModelField Name="PlanState" Type="String" Mapping="PlanState" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Menu ID="Menu1" runat="server">
        <Items>
            <ext:MenuItem ID="MenuDel" runat="server" Icon="Delete" Text="删除">
                <Listeners>
                    <Click Fn="zaodeleteplan" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="MenuAdd" runat="server" Icon="PackageGo" Text="插入">
                <Listeners>
                    <Click Fn="zaoinsertPlan" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuSeparator ID="MenuSeparator1" runat="server" />
            <ext:MenuItem ID="MenuUp" runat="server" Icon="ArrowUp" Text="上移">
                <Listeners>
                    <Click Fn="zaomoveUpPlan" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="MenuDn" runat="server" Icon="ArrowDown" Text="下移">
                <Listeners>
                    <Click Fn="zaomoveDnPlan" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuSeparator ID="MenuSeparator2" runat="server" />
            <ext:MenuItem ID="MenuItem1" runat="server" Icon="Accept" Text="下达">
                <Listeners>
                    <Click Fn="zaoupPlanState" />
                </Listeners>
            </ext:MenuItem>
        </Items>
    </ext:Menu>
    <ext:Menu ID="Menu2" runat="server">
        <Items>
            <ext:MenuItem ID="MenuItem2" runat="server" Icon="Delete" Text="删除">
                <Listeners>
                    <Click Fn="zhongdeleteplan" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="MenuItem4" runat="server" Icon="PackageGo" Text="插入">
                <Listeners>
                    <Click Fn="zhonginsertPlan" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuSeparator ID="MenuSeparator3" runat="server" />
            <ext:MenuItem ID="MenuItem5" runat="server" Icon="ArrowUp" Text="上移">
                <Listeners>
                    <Click Fn="zhongmoveUpPlan" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="MenuItem6" runat="server" Icon="ArrowDown" Text="下移">
                <Listeners>
                    <Click Fn="zhongmoveDnPlan" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuSeparator ID="MenuSeparator4" runat="server" />
            <ext:MenuItem ID="MenuItem7" runat="server" Icon="Accept" Text="下达">
                <Listeners>
                    <Click Fn="zhongupPlanState" />
                </Listeners>
            </ext:MenuItem>
        </Items>
    </ext:Menu>
    <ext:Menu ID="Menu3" runat="server">
        <Items>
            <ext:MenuItem ID="MenuItem8" runat="server" Icon="Delete" Text="删除">
                <Listeners>
                    <Click Fn="yedeleteplan" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="MenuItem10" runat="server" Icon="PackageGo" Text="插入">
                <Listeners>
                    <Click Fn="yeinsertPlan" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuSeparator ID="MenuSeparator5" runat="server" />
            <ext:MenuItem ID="MenuItem11" runat="server" Icon="ArrowUp" Text="上移">
                <Listeners>
                    <Click Fn="yemoveUpPlan" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="MenuItem12" runat="server" Icon="ArrowDown" Text="下移">
                <Listeners>
                    <Click Fn="yemoveDnPlan" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuSeparator ID="MenuSeparator6" runat="server" />
            <ext:MenuItem ID="MenuItem13" runat="server" Icon="Accept" Text="下达">
                <Listeners>
                    <Click Fn="yeupPlanState" />
                </Listeners>
            </ext:MenuItem>
        </Items>
    </ext:Menu>
    <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="Panel1" runat="server" Region="West" Width="180" Split="true" Layout="BorderLayout">
                <Items>
                    <ext:TreePanel ID="treeEquip" runat="server" Title="机台分组信息" Region="Center" Icon="FolderGo"
                        AutoHeight="true" RootVisible="false">
                        <Store>
                            <ext:TreeStore ID="treeDeptStore" runat="server">
                                <Proxy>
                                    <ext:PageProxy>
                                        <RequestConfig Method="GET" Type="Load" />
                                    </ext:PageProxy>
                                </Proxy>
                                <Root>
                                    <ext:Node NodeID="root" Expanded="true" />
                                </Root>
                            </ext:TreeStore>
                        </Store>
                        <Listeners>
                            <ItemClick Fn="menuItemClick">
                            </ItemClick>
                        </Listeners>
                    </ext:TreePanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="Panel2" runat="server" Region="Center" Layout="BorderLayout">
                <Items>
                    <ext:Panel ID="pnlUnitTitle" runat="server" Region="North"  AutoHeight="true" Layout="TableLayOut">
                        <Items>
                            <ext:DateField ID="txtStratPlanDate" runat="server" LabelAlign="Right" Editable="false"
                                AllowBlank="false" Vtype="daterange" FieldLabel="计划日期" EnableKeyEvents="true"
                                Format="yyyy-MM-dd" ColumnWidth=".25">
                                <Listeners>
                                    <Change Fn="ChangeDate" />
                                </Listeners>
                            </ext:DateField>
                           
                            <ext:Label ID="lblBlank" Html="" runat="server" Height="20" ColumnWidth=".05">
                            </ext:Label>
                            <ext:Label ID="planState" Html="状态说明:<span style='padding-left:10px;font-weight: bold;'>新计划</span><span style='color:Red;padding-right:10px;padding-left:10px;font-weight: bold;'>下达计划</span><span style='color:Blue;padding-right:10px;font-weight: bold;'>已接受计划</span><span style='color:Green;padding-right:10px;font-weight: bold;'>运行计划</span><span style='color:Purple;padding-right:10px;pading-left:10px;font-weight: bold;'>完成计划</span>"
                                runat="server" ColumnWidth=".7" Padding="2">
                            </ext:Label>
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="Panel3" runat="server" Region="Center" AutoScroll="true">
                        <Items>
                            <ext:Panel ID="Zao" runat="server" Region="Center" AutoScroll="true">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Stretch" Padding="5" />
                                </LayoutConfig>
                                <Items>
                                    <ext:Panel ID="zaoPanel" Region="Center" Width="98" Html="<table width='100%' height='100%'><tr align='center'><td align='center'>白</td></tr></table> "
                                        runat="server" AutoScroll="true">
                                    </ext:Panel>
                                    <ext:GridPanel ID="ZaoPlanGridPanel" Height="400" Width="900" runat="server" Frame="true"
                                        StoreID="ZaoPlanStore" ContextMenuID="Menu1" Region="Center" Scroll="Vertical"
                                        SortableColumns="false">
                                        <Plugins>
                                            <ext:CellEditing ClicksToEdit="1">
                                                <Listeners>
                                                    <BeforeEdit Fn="beforeEdit" />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Edit OnEvent="Edit">
                                                        <EventMask ShowMask="true" CustomTarget="#{ZaoPlanGridPanel}" />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="field" Value="e.field" Mode="Raw" />
                                                            <ext:Parameter Name="index" Value="e.rowIdx" Mode="Raw" />
                                                            <ext:Parameter Name="record" Value="e.record.data" Mode="Raw" Encode="true" />
                                                            <ext:Parameter Name="flag" Value="1" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Edit>
                                                </DirectEvents>
                                            </ext:CellEditing>
                                        </Plugins>
                                        <ColumnModel ID="ColumnModel1" runat="server">
                                            <Columns>
                                                <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Width="35" />
                                                <ext:Column ID="SummaryColumn2" runat="server" Width="180" Text="物料名称" DataIndex="RecipeMaterialName">
                                                    <Editor>
                                                        <ext:ComboBox ID="cboZaoRecipeMaterialName" ValueField="RecipeMaterialName" MinChars="1"
                                                            DisplayField="RecipeMaterialName" runat="server" TypeAhead="false" ReadOnly="true">
                                                            <Store>
                                                                <ext:Store ID="RecipeMaterialNameStore_zao" runat="server" OnReadData="RecipeMaterialNameStoreRefresh">
                                                                    <Model>
                                                                        <ext:Model ID="Model10" runat="server">
                                                                            <Fields>
                                                                                <ext:ModelField Name="RecipeMaterialCode" Type="String" Mapping="RecipeMaterialCode" />
                                                                                <ext:ModelField Name="RecipeMaterialName" Type="String" Mapping="RecipeMaterialName" />
                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>
                                                                    <Proxy>
                                                                        <ext:PageProxy>
                                                                            <Reader>
                                                                                <ext:ArrayReader />
                                                                            </Reader>
                                                                        </ext:PageProxy>
                                                                    </Proxy>
                                                                </ext:Store>
                                                            </Store>
                                                        </ext:ComboBox>
                                                    </Editor>
                                                </ext:Column>
                                                <ext:Column ID="Column1" runat="server" DataIndex="RecipeName" Width="180" Text="配方编号">
                                                    <Editor>
                                                        <ext:ComboBox ID="StateCombo" runat="server" StoreID="RecipeNameStore" ValueField="RecipeName"
                                                            Editable="false" DisplayField="RecipeName" ReadOnly="true">
                                                            <CustomConfig>
                                                                <ext:ConfigItem Name="initQuery" Value="Ext.emptyFn" Mode="Raw" />
                                                            </CustomConfig>
                                                        </ext:ComboBox>
                                                    </Editor>
                                                </ext:Column>
                                                <ext:Column ID="SummaryColumn4" runat="server" Text="生产车数" Sortable="false" DataIndex="PlanNum"
                                                    SummaryType="Sum">
                                                    <Editor>
                                                        <ext:NumberField ID="NumberField2" runat="server" AllowBlank="false" MinValue="1"
                                                            StyleSpec="text-align:left" />
                                                    </Editor>
                                                </ext:Column>
                                                <ext:Column ID="SummaryColumn5" runat="server" Text="制定者" DataIndex="UserName">
                                                </ext:Column>
                                                <ext:Column ID="SummaryColumn6" runat="server" Text="制定日期" DataIndex="OperDatetime"
                                                    Width="140">
                                                  <%--  <Renderer Format="Date" FormatArgs="'yy-M-d H:m:s'" />--%>
                                                </ext:Column>
                                            </Columns>
                                        </ColumnModel>
                                        <View>
                                            <ext:GridView ID="GridView1" runat="server" StripeRows="true">
                                                <GetRowClass Fn="getRowClass" />
                                            </ext:GridView>
                                        </View>
                                        <TopBar>
                                            <ext:Toolbar ID="Toolbar1" runat="server" Layout="ColumnLayout">
                                                <Items>
                                                    <ext:Button ID="btnPlan1" runat="server" Text="批量下达" ToolTip="批量下达计划" Icon="Accept" Width="80" >
                                                        <DirectEvents>
                                                            <Click OnEvent="AllUpdatePlanState">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="flage" Value="1">
                                                                    </ext:Parameter>
                                                                </ExtraParams>
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                    <ext:ComboBox ID="add_recipe_material_code_zao" runat="server" Width="230" 
                                                        MinChars="1" FieldLabel="物料名称" LabelAlign="Right" IndicatorText="*" IndicatorCls="red-text"
                                                        ValueField="RecipeMaterialCode" DisplayField="RecipeMaterialName" TypeAhead="false">
                                                        <ListConfig LoadingText="Searching..." />
                                                        <Store>
                                                            <ext:Store ID="AddRecipeMaterialNameStore_zao" runat="server" OnReadData="AddRecipeMaterialNameStoreRefresh">
                                                                <Proxy>
                                                                    <ext:PageProxy>
                                                                        <Reader>
                                                                            <ext:ArrayReader />
                                                                        </Reader>
                                                                    </ext:PageProxy>
                                                                </Proxy>
                                                                <Model>
                                                                    <ext:Model ID="Model8" runat="server">
                                                                        <Fields>
                                                                            <ext:ModelField Name="RecipeMaterialCode" Type="String" Mapping="RecipeMaterialCode" />
                                                                            <ext:ModelField Name="RecipeMaterialName" Type="String" Mapping="RecipeMaterialName" />
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                            </ext:Store>
                                                        </Store>
                                                        <DirectEvents>
                                                            <Change OnEvent="FillAddRecipeComboBox">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="shiftFlag" Value="1">
                                                                    </ext:Parameter>
                                                                </ExtraParams>
                                                            </Change>
                                                        </DirectEvents>
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <Select Handler="this.getTrigger(0).show();" />
                                                            <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); App.add_recipe_name_zao.setValue('');this.getTrigger(0).hide();}" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                    <ext:ComboBox ID="add_recipe_name_zao" Width="220" runat="server" FieldLabel="配方编号"
                                                        Editable="false" LabelAlign="Right" IndicatorText="*" IndicatorCls="red-text"
                                                        StoreID="AddRecipeNameStore" ValueField="RecipeName" DisplayField="RecipeName">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear"  HideTrigger="true" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <Select Handler="this.getTrigger(0).show();" />
                                                            <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                    <ext:NumberField ID="add_plan_num_zao" Width="180" runat="server" FieldLabel="生产车数"
                                                        MinValue="1" LabelAlign="Right" IndicatorText="*" IndicatorCls="red-text" />
                                                    <ext:Button ID="btnAdd1" Icon="Add" Text="添加" runat="server" ToolTip="添加计划">
                                                        <DirectEvents>
                                                            <Click OnEvent="AddRecipePlan">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="shiftFlag" Value="1">
                                                                    </ext:Parameter>
                                                                </ExtraParams>
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                    <ext:Button ID="query1" Icon="Find" Text="查询" runat="server" ToolTip="查询">
                                                        <DirectEvents>
                                                            <Click OnEvent="FindRecipePlan">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="shiftFlag" Value="1">
                                                                    </ext:Parameter>
                                                                </ExtraParams>
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                    <ext:Button ID="btnCopy1" Icon="Add" Text="复制" runat="server" ToolTip="复制计划">
                                                        <DirectEvents>
                                                            <Click OnEvent="CopyRecipePlan">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="shiftFlag" Value="1">
                                                                    </ext:Parameter>
                                                                </ExtraParams>
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>
                                        <BottomBar>
                                            <ext:PagingToolbar ID="pageToolbar" runat="server">
                                                <Items>
                                                    <ext:TextField ID="txtcheshuzao" Width="180" runat="server" FieldLabel="车数汇总" LabelAlign="Right" ReadOnly="true"/>
                                                    <ext:TextField ID="txtzhouqizao" Width="180" runat="server" FieldLabel="周期汇总" LabelAlign="Right" ReadOnly="true"/>
                                                </Items>
                                            </ext:PagingToolbar>
                                        </BottomBar>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="Zhong" runat="server" AutoScroll="true" Region="Center" Hidden="true">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Stretch" Padding="5" />
                                </LayoutConfig>
                                <Items>
                                    <ext:Panel ID="ZhongPanel" Region="Center" Width="98" Html="<table width='100%' height='100%'><tr align='center'><td align='center'>中</td></tr></table> "
                                        runat="server" AutoScroll="true">
                                    </ext:Panel>
                                    <ext:GridPanel ID="ZhongGridPanel" Height="180" Width="850" runat="server" Frame="true"
                                        StoreID="ZhongPlanStore" ContextMenuID="Menu2" Region="Center" Scroll="Vertical"
                                        SortableColumns="false">
                                        <Plugins>
                                            <ext:CellEditing ClicksToEdit="1">
                                                <Listeners>
                                                    <BeforeEdit Fn="beforeEdit" />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Edit OnEvent="Edit">
                                                        <EventMask ShowMask="true" CustomTarget="#{ZhongGridPanel}" />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="field" Value="e.field" Mode="Raw" />
                                                            <ext:Parameter Name="index" Value="e.rowIdx" Mode="Raw" />
                                                            <ext:Parameter Name="record" Value="e.record.data" Mode="Raw" Encode="true" />
                                                            <ext:Parameter Name="flag" Value="2" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Edit>
                                                </DirectEvents>
                                            </ext:CellEditing>
                                        </Plugins>
                                        <ColumnModel ID="ColumnModel4" runat="server">
                                            <Columns>
                                                <ext:RowNumbererColumn ID="RowNumbererColumn4" runat="server" Width="35" />
                                                <ext:Column ID="Column2" runat="server" Width="180" Text="物料名称" DataIndex="RecipeMaterialName">
                                                    <Editor>
                                                        <ext:ComboBox ID="ComboBox1" ValueField="RecipeMaterialName" MinChars="1" DisplayField="RecipeMaterialName"
                                                            runat="server" TypeAhead="false" ReadOnly="true">
                                                            <Store>
                                                                <ext:Store ID="RecipeMaterialNameStore_zhong" runat="server" OnReadData="RecipeMaterialNameStoreRefresh">
                                                                    <Model>
                                                                        <ext:Model ID="Model11" runat="server">
                                                                            <Fields>
                                                                                <ext:ModelField Name="RecipeMaterialCode" Type="String" Mapping="RecipeMaterialCode" />
                                                                                <ext:ModelField Name="RecipeMaterialName" Type="String" Mapping="RecipeMaterialName" />
                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>
                                                                    <Proxy>
                                                                        <ext:PageProxy>
                                                                            <Reader>
                                                                                <ext:ArrayReader />
                                                                            </Reader>
                                                                        </ext:PageProxy>
                                                                    </Proxy>
                                                                </ext:Store>
                                                            </Store>
                                                        </ext:ComboBox>
                                                    </Editor>
                                                </ext:Column>
                                                <ext:Column ID="Column3" runat="server" DataIndex="RecipeName" Width="180" Text="配方编号">
                                                    <Editor>
                                                        <ext:ComboBox ID="ComboBox2" runat="server" StoreID="RecipeNameStore" ValueField="RecipeName"
                                                            Editable="false" DisplayField="RecipeName" ReadOnly="true">
                                                            <CustomConfig>
                                                                <ext:ConfigItem Name="initQuery" Value="Ext.emptyFn" Mode="Raw" />
                                                            </CustomConfig>
                                                        </ext:ComboBox>
                                                    </Editor>
                                                </ext:Column>
                                                <ext:Column ID="Column4" runat="server" Text="生产车数" Sortable="false" DataIndex="PlanNum"
                                                    SummaryType="Sum">
                                                    <Editor>
                                                        <ext:NumberField ID="NumberField1" runat="server" AllowBlank="false" MinValue="1"
                                                            StyleSpec="text-align:left" />
                                                    </Editor>
                                                </ext:Column>
                                                <ext:Column ID="Column5" runat="server" Text="制定者" DataIndex="UserName">
                                                </ext:Column>
                                                <ext:Column ID="Column6" runat="server" Text="制定日期" DataIndex="OperDatetime" Width="140">
                                                   <%-- <Renderer Format="Date" FormatArgs="'Y-m-d H:M:s'" />--%>
                                                </ext:Column>
                                            </Columns>
                                        </ColumnModel>
                                        <View>
                                            <ext:GridView ID="GridView4" runat="server" StripeRows="true">
                                                <GetRowClass Fn="getRowClass" />
                                            </ext:GridView>
                                        </View>
                                        <TopBar>
                                            <ext:Toolbar ID="Toolbar4" runat="server" Layout="ColumnLayout">
                                                <Items>
                                                    <ext:Button ID="btnPlan2" runat="server" Text="批量下达" ToolTip="批量下达计划" Icon="Accept" Width="80">
                                                        <DirectEvents>
                                                            <Click OnEvent="AllUpdatePlanState">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="flage" Value="2">
                                                                    </ext:Parameter>
                                                                </ExtraParams>
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                    <ext:ComboBox ID="add_recipe_material_code_zhong" Width="250" runat="server"
                                                        MinChars="1" FieldLabel="物料名称" LabelAlign="Right" IndicatorText="*" IndicatorCls="red-text"
                                                        ValueField="RecipeMaterialCode" DisplayField="RecipeMaterialName" TypeAhead="false">
                                                        <ListConfig LoadingText="Searching..." />
                                                        <Store>
                                                            <ext:Store ID="AddRecipeMaterialNameStore_zhong" runat="server" OnReadData="AddRecipeMaterialNameStoreRefresh">
                                                                <Proxy>
                                                                    <ext:PageProxy>
                                                                        <Reader>
                                                                            <ext:ArrayReader />
                                                                        </Reader>
                                                                    </ext:PageProxy>
                                                                </Proxy>
                                                                <Model>
                                                                    <ext:Model ID="Model5" runat="server">
                                                                        <Fields>
                                                                            <ext:ModelField Name="RecipeMaterialCode" Type="String" Mapping="RecipeMaterialCode" />
                                                                            <ext:ModelField Name="RecipeMaterialName" Type="String" Mapping="RecipeMaterialName" />
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                            </ext:Store>
                                                        </Store>
                                                        <DirectEvents>
                                                            <Change OnEvent="FillAddRecipeComboBox">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="shiftFlag" Value="2">
                                                                    </ext:Parameter>
                                                                </ExtraParams>
                                                            </Change>
                                                        </DirectEvents>
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <Select Handler="this.getTrigger(0).show();" />
                                                            <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue();App.add_recipe_name_zhong.setValue(''); this.getTrigger(0).hide();}" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                    <ext:ComboBox ID="add_recipe_name_zhong" Width="220" runat="server" FieldLabel="配方编号"
                                                        Editable="false" LabelAlign="Right" IndicatorText="*" IndicatorCls="red-text"
                                                        StoreID="AddRecipeNameStore" ValueField="RecipeName" DisplayField="RecipeName">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <Select Handler="this.getTrigger(0).show();" />
                                                            <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                    <ext:NumberField ID="add_plan_num_zhong" Width="150" runat="server" FieldLabel="生产车数"
                                                        MinValue="1" LabelAlign="Right" IndicatorText="*" IndicatorCls="red-text" />
                                                    <ext:Button ID="btnAdd2" Icon="Add" Text="添加" runat="server" ToolTip="添加计划">
                                                        <DirectEvents>
                                                            <Click OnEvent="AddRecipePlan">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="shiftFlag" Value="2">
                                                                    </ext:Parameter>
                                                                </ExtraParams>
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                    <ext:Button ID="query2" Icon="Find" Text="查询" runat="server" ToolTip="查询">
                                                        <DirectEvents>
                                                            <Click OnEvent="FindRecipePlan">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="shiftFlag" Value="2">
                                                                    </ext:Parameter>
                                                                </ExtraParams>
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                    <ext:Button ID="btnCopy2" Icon="Add" Text="复制" runat="server" ToolTip="复制计划">
                                                        <DirectEvents>
                                                            <Click OnEvent="CopyRecipePlan">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="shiftFlag" Value="2">
                                                                    </ext:Parameter>
                                                                </ExtraParams>
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="Ye" runat="server" AutoScroll="true" Region="Center">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Stretch" Padding="5" />
                                </LayoutConfig>
                                <Items>
                                    <ext:Panel ID="YePanel" Region="Center" Width="98" Html="<table width='100%' height='100%'><tr align='center'><td align='center'>夜</td></tr></table> "
                                        runat="server" AutoScroll="true">
                                    </ext:Panel>
                                    <ext:GridPanel ID="YeGridPanel" Height="400" Width="900" runat="server" Frame="true"
                                        StoreID="YePlanStore" ContextMenuID="Menu3" Region="Center" Scroll="Vertical"
                                        SortableColumns="false">
                                        <Plugins>
                                            <ext:CellEditing ClicksToEdit="1">
                                                <Listeners>
                                                    <BeforeEdit Fn="beforeEdit" />
                                                </Listeners>
                                                <DirectEvents>
                                                    <Edit OnEvent="Edit">
                                                        <EventMask ShowMask="true" CustomTarget="#{YeGridPanel}" />
                                                        <ExtraParams>
                                                            <ext:Parameter Name="field" Value="e.field" Mode="Raw" />
                                                            <ext:Parameter Name="index" Value="e.rowIdx" Mode="Raw" />
                                                            <ext:Parameter Name="record" Value="e.record.data" Mode="Raw" Encode="true" />
                                                            <ext:Parameter Name="flag" Value="3" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Edit>
                                                </DirectEvents>
                                            </ext:CellEditing>
                                        </Plugins>
                                        <ColumnModel ID="ColumnModel2" runat="server">
                                            <Columns>
                                                <ext:RowNumbererColumn ID="RowNumbererColumn2" runat="server" Width="35" />
                                                <ext:Column ID="Column7" runat="server" Width="180" Text="物料名称" DataIndex="RecipeMaterialName">
                                                    <Editor>
                                                        <ext:ComboBox ID="ComboBox3" ValueField="RecipeMaterialName" MinChars="1" DisplayField="RecipeMaterialName"
                                                            runat="server" TypeAhead="false" ReadOnly="true">
                                                            <Store>
                                                                <ext:Store ID="RecipeMaterialNameStore_ye" runat="server" OnReadData="RecipeMaterialNameStoreRefresh">
                                                                    <Model>
                                                                        <ext:Model ID="Model12" runat="server">
                                                                            <Fields>
                                                                                <ext:ModelField Name="RecipeMaterialCode" Type="String" Mapping="RecipeMaterialCode" />
                                                                                <ext:ModelField Name="RecipeMaterialName" Type="String" Mapping="RecipeMaterialName" />
                                                                            </Fields>
                                                                        </ext:Model>
                                                                    </Model>
                                                                    <Proxy>
                                                                        <ext:PageProxy>
                                                                            <Reader>
                                                                                <ext:ArrayReader />
                                                                            </Reader>
                                                                        </ext:PageProxy>
                                                                    </Proxy>
                                                                </ext:Store>
                                                            </Store>
                                                        </ext:ComboBox>
                                                    </Editor>
                                                </ext:Column>
                                                <ext:Column ID="Column8" runat="server" DataIndex="RecipeName" Width="180" Text="配方编号">
                                                    <Editor>
                                                        <ext:ComboBox ID="ComboBox4" runat="server" StoreID="RecipeNameStore" ValueField="RecipeName"
                                                            Editable="false" DisplayField="RecipeName" ReadOnly="true">
                                                            <CustomConfig>
                                                                <ext:ConfigItem Name="initQuery" Value="Ext.emptyFn" Mode="Raw" />
                                                            </CustomConfig>
                                                        </ext:ComboBox>
                                                    </Editor>
                                                </ext:Column>
                                                <ext:Column ID="Column9" runat="server" Text="生产车数" Sortable="false" DataIndex="PlanNum"
                                                    SummaryType="Sum">
                                                    <Editor>
                                                        <ext:NumberField ID="NumberField3" runat="server" AllowBlank="false" MinValue="1"
                                                            StyleSpec="text-align:left" />
                                                    </Editor>
                                                </ext:Column>
                                                <ext:Column ID="Column10" runat="server" Text="制定者" DataIndex="UserName">
                                                </ext:Column>
                                                <ext:Column ID="Column11" runat="server" Text="制定日期" DataIndex="OperDatetime" Width="140">
                                                    <%--<Renderer Format="Date" FormatArgs="'Y-m-d H:M:s'" />--%>
                                                </ext:Column>
                                            </Columns>
                                        </ColumnModel>
                                        <View>
                                            <ext:GridView ID="GridView2" runat="server" StripeRows="true">
                                                <GetRowClass Fn="getRowClass" />
                                            </ext:GridView>
                                        </View>
                                        <TopBar>
                                            <ext:Toolbar ID="Toolbar2" runat="server" Layout="ColumnLayout">
                                                <Items>
                                                    <ext:Button ID="btnPlan3" runat="server" Text="批量下达" ToolTip="批量下达计划" Icon="Accept" Width="80">
                                                        <DirectEvents>
                                                            <Click OnEvent="AllUpdatePlanState">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="flage" Value="3">
                                                                    </ext:Parameter>
                                                                </ExtraParams>
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                    <ext:ComboBox ID="add_recipe_material_code_ye" Width="230" runat="server" MinChars="1"
                                                        FieldLabel="物料名称" LabelAlign="Right" IndicatorText="*" IndicatorCls="red-text"
                                                        ValueField="RecipeMaterialCode" DisplayField="RecipeMaterialName" TypeAhead="false">
                                                        <Store>
                                                            <ext:Store ID="AddRecipeMaterialNameStore_ye" runat="server" OnReadData="AddRecipeMaterialNameStoreRefresh">
                                                                <Proxy>
                                                                    <ext:PageProxy>
                                                                        <Reader>
                                                                            <ext:ArrayReader />
                                                                        </Reader>
                                                                    </ext:PageProxy>
                                                                </Proxy>
                                                                <Model>
                                                                    <ext:Model ID="Model9" runat="server">
                                                                        <Fields>
                                                                            <ext:ModelField Name="RecipeMaterialCode" Type="String" Mapping="RecipeMaterialCode" />
                                                                            <ext:ModelField Name="RecipeMaterialName" Type="String" Mapping="RecipeMaterialName" />
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                            </ext:Store>
                                                        </Store>
                                                        <DirectEvents>
                                                            <Change OnEvent="FillAddRecipeComboBox">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="shiftFlag" Value="3">
                                                                    </ext:Parameter>
                                                                </ExtraParams>
                                                            </Change>
                                                        </DirectEvents>
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <Select Handler="this.getTrigger(0).show();" />
                                                            <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); App.add_recipe_name_ye.setValue('');this.getTrigger(0).hide();}" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                    <ext:ComboBox ID="add_recipe_name_ye" Width="220" runat="server" FieldLabel="配方编号"
                                                        Editable="false" LabelAlign="Right" IndicatorText="*" IndicatorCls="red-text"
                                                        StoreID="AddRecipeNameStore" ValueField="RecipeName" DisplayField="RecipeName">
                                                        <Triggers>
                                                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        </Triggers>
                                                        <Listeners>
                                                            <Select Handler="this.getTrigger(0).show();" />
                                                            <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                            <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                                        </Listeners>
                                                    </ext:ComboBox>
                                                    <ext:NumberField ID="add_plan_num_ye" Width="180" runat="server" FieldLabel="生产车数"
                                                        MinValue="1" LabelAlign="Right" IndicatorText="*" IndicatorCls="red-text" />
                                                    <ext:Button ID="btnAdd3" Icon="Add" Text="添加" runat="server" ToolTip="添加计划">
                                                        <DirectEvents>
                                                            <Click OnEvent="AddRecipePlan">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="shiftFlag" Value="3">
                                                                    </ext:Parameter>
                                                                </ExtraParams>
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                    <ext:Button ID="query3" Icon="Find" Text="查询" runat="server" ToolTip="查询">
                                                        <DirectEvents>
                                                            <Click OnEvent="FindRecipePlan">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="shiftFlag" Value="3">
                                                                    </ext:Parameter>
                                                                </ExtraParams>
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                    <ext:Button ID="btnCopy3" Icon="Add" Text="复制" runat="server" ToolTip="复制计划">
                                                        <DirectEvents>
                                                            <Click OnEvent="CopyRecipePlan">
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="shiftFlag" Value="3">
                                                                    </ext:Parameter>
                                                                </ExtraParams>
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>
                                        <BottomBar>
                                            <ext:PagingToolbar ID="PagingToolbar1" runat="server">
                                                <Items>
                                                    <ext:TextField ID="txtchechuwan" Width="180" runat="server" FieldLabel="车数汇总" LabelAlign="Right" ReadOnly="true"/>
                                                    <ext:TextField ID="txtzhouqiwan" Width="180" runat="server" FieldLabel="周期汇总" LabelAlign="Right" ReadOnly="true"/>
                                                </Items>
                                            </ext:PagingToolbar>
                                        </BottomBar>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Window ID="winCopy" runat="server" Icon="MonitorAdd" Closable="false" Title="复制计划信息" Width="320" Height="380" 
                    Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:DateField ID="DateField1" runat="server" LabelAlign="Right" Editable="false"
                                    AllowBlank="false" Vtype="daterange" FieldLabel="复制计划日期" EnableKeyEvents="true"
                                    Format="yyyy-MM-dd" ColumnWidth=".25" ReadOnly="true">
                                </ext:DateField>
                                <ext:DateField ID="DateField2" runat="server" LabelAlign="Right" Editable="false"
                                    AllowBlank="false" Vtype="daterange" FieldLabel="目的计划日期" EnableKeyEvents="true"
                                    Format="yyyy-MM-dd" ColumnWidth=".25">
                                </ext:DateField>
                                <ext:ComboBox ID="cbxStorageType2" runat="server" FieldLabel="目的班次" LabelAlign="Left">
                                    <Items>
                                        <ext:ListItem Text="白" Value="1"></ext:ListItem>
                                        <ext:ListItem Text="夜" Value="3"></ext:ListItem>
                                    </Items>
                                </ext:ComboBox>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                     <Buttons>
                        <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept" >
                            <DirectEvents>
                                <Click OnEvent="btnAddSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:Window>
            <ext:Hidden ID="hidden_parent_num" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hidden_type" runat="server">
            </ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
