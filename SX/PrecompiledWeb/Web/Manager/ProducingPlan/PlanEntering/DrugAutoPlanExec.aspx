﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_ProducingPlan_PlanEntering_DrugAutoPlanExec, App_Web_qlhoypu3" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>药品自动排产</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script src="<%= Page.ResolveUrl("./") %>PlanExec.js" type="text/javascript"></script>
    <style type="text/css">
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
        function ChangeXLDate() {
            var flag = 1;
            App.direct.XlPlanDateChage(flag, {
                success: function (result) {
                    if (result) {

                    }
                    else {
                        //Ext.Msg.alert('错误！', '请对应机台信息！');
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
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Store ID="ShiftStore" runat="server">
        <Model>
            <ext:Model ID="Model4" runat="server">
                <Fields>
                    <ext:ModelField Name="ShiftName" Type="String" Mapping="ShiftName" />
                    <ext:ModelField Name="ObjID" Type="Int" Mapping="ObjID" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Store ID="ZaoPlanStore" runat="server" AutoSync="true">
        <Sorters>
            <ext:DataSorter Property="mater_Code" Direction="ASC" />
        </Sorters>
        <Model>
            <ext:Model ID="Model1" runat="server" Name="gridPanelZaoStoreModel">
                <Fields>
                    <ext:ModelField Name="mater_Code" Type="String" Mapping="mater_Code" />
                    <ext:ModelField Name="equip_Code" Type="String" Mapping="equip_Code" />
                    <ext:ModelField Name="edt_Code" Type="Int" Mapping="edt_Code" />
                    <ext:ModelField Name="total_weight" Type="Float" Mapping="total_weight" />
                    <ext:ModelField Name="recipe_code" Type="String" Mapping="recipe_code" />
                    <ext:ModelField Name="recipe_type" Type="Int" Mapping="recipe_type" />
                    <ext:ModelField Name="plan_num" Type="Int" Mapping="plan_num" />
                    <ext:ModelField Name="mater_name" Type="String" Mapping="mater_name" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Store ID="ZhongPlanStore" runat="server" AutoSync="true">
        <Sorters>
            <ext:DataSorter Property="mater_Code" Direction="ASC" />
        </Sorters>
        <Model>
            <ext:Model ID="Model2" runat="server" Name="gridPanelZaoStoreModel">
                <Fields>
                    <ext:ModelField Name="mater_Code" Type="String" Mapping="mater_Code" />
                    <ext:ModelField Name="equip_Code" Type="String" Mapping="equip_Code" />
                    <ext:ModelField Name="edt_Code" Type="Int" Mapping="edt_Code" />
                    <ext:ModelField Name="total_weight" Type="Float" Mapping="total_weight" />
                    <ext:ModelField Name="recipe_code" Type="String" Mapping="recipe_code" />
                    <ext:ModelField Name="recipe_type" Type="Int" Mapping="recipe_type" />
                    <ext:ModelField Name="plan_num" Type="Int" Mapping="plan_num" />
                    <ext:ModelField Name="mater_name" Type="String" Mapping="mater_name" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Store ID="YePlanStore" runat="server" AutoSync="true">
        <Sorters>
            <ext:DataSorter Property="mater_Code" Direction="ASC" />
        </Sorters>
        <Model>
            <ext:Model ID="Model3" runat="server" Name="gridPanelZaoStoreModel">
                <Fields>
                    <ext:ModelField Name="mater_Code" Type="String" Mapping="mater_Code" />
                    <ext:ModelField Name="equip_Code" Type="String" Mapping="equip_Code" />
                    <ext:ModelField Name="edt_Code" Type="Int" Mapping="edt_Code" />
                    <ext:ModelField Name="total_weight" Type="Float" Mapping="total_weight" />
                    <ext:ModelField Name="recipe_code" Type="String" Mapping="recipe_code" />
                    <ext:ModelField Name="recipe_type" Type="Int" Mapping="recipe_type" />
                    <ext:ModelField Name="plan_num" Type="Int" Mapping="plan_num" />
                    <ext:ModelField Name="mater_name" Type="String" Mapping="mater_name" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Menu ID="Menu1" runat="server">
        <Items>
            <ext:MenuItem ID="MenuDel" runat="server" Icon="CartAdd" Text="计划生成">
                <Listeners>
                    <Click Fn="zaoplanExec" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="MenuEdit" runat="server" Icon="Add" Text="全部生成">
                <Listeners>
                    <Click Fn="zaoallPlanExec" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuSeparator ID="MenuSeparator1" runat="server" />
        </Items>
    </ext:Menu>
    <ext:Menu ID="Menu2" runat="server">
        <Items>
            <ext:MenuItem ID="MenuItem2" runat="server" Icon="CartAdd" Text="计划生成">
                <Listeners>
                    <Click Fn="zhongplanExec" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="MenuItem3" runat="server" Icon="Add" Text="全部生成">
                <Listeners>
                    <Click Fn="zhongallPlanExec" />
                </Listeners>
            </ext:MenuItem>
        </Items>
    </ext:Menu>
    <ext:Menu ID="Menu3" runat="server">
        <Items>
            <ext:MenuItem ID="MenuItem8" runat="server" Icon="CartAdd" Text="计划生成">
                <Listeners>
                    <Click Fn="yeplanExec" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="MenuItem9" runat="server" Icon="Add" Text="全部生成">
                <Listeners>
                    <Click Fn="yeallPlanExec" />
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
                    <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true">
                        <Items>
                            <ext:Panel ID="pnlUnitQuery" runat="server" AutoHeight="true">
                                <Items>
                                    <ext:DateField ID="txtStratPlanDate" runat="server" Editable="false" AllowBlank="false"
                                        Vtype="daterange" FieldLabel="计划日期" EnableKeyEvents="true" Format="yyyy-MM-dd">
                                        <Listeners>
                                            <Change Fn="ChangeDate" />
                                        </Listeners>
                                    </ext:DateField>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="Panel3" runat="server" Region="Center" AutoScroll="true">
                        <Items>
                            <ext:Panel ID="Zao" runat="server" Region="Center" AutoScroll="true">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Stretch" Padding="5" />
                                </LayoutConfig>
                                <Items>
                                    <ext:Panel ID="zaoPanel" Region="Center" Width="100" Html="<table width='100%' height='100%'><tr align='center'><td align='center'>早</td></tr></table> "
                                        runat="server" AutoScroll="true">
                                    </ext:Panel>
                                    <ext:GridPanel ID="ZaoPlanGridPanel" Height="180" runat="server" Frame="true" StoreID="ZaoPlanStore"
                                        ContextMenuID="Menu1" Region="Center" Scroll="Vertical">
                                        <ColumnModel ID="ColumnModel1" runat="server">
                                            <Columns>
                                                <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Width="35" />
                                                <ext:Column ID="SummaryColumn2" runat="server" Width="180" Text="物料名称" DataIndex="mater_name">
                                                </ext:Column>
                                                <ext:Column ID="Column1" runat="server" DataIndex="recipe_code" Width="150" Text="配方编号">
                                                </ext:Column>
                                                <ext:Column ID="SummaryColumn4" runat="server" Text="生产车数" Sortable="true" DataIndex="plan_num">
                                                </ext:Column>
                                                <ext:CommandColumn ID="CommandColumn1" runat="server" Hidden="true" Text="操作">
                                                    <Commands>
                                                        <ext:GridCommand Text="操作" StandOut="true">
                                                            <Menu>
                                                                <Items>
                                                                    <ext:MenuCommand CommandName="ItemCommand" Text="Item" />
                                                                    <ext:MenuCommand CommandName="ItemCommand" Text="Item" />
                                                                </Items>
                                                            </Menu>
                                                        </ext:GridCommand>
                                                    </Commands>
                                                </ext:CommandColumn>
                                            </Columns>
                                        </ColumnModel>
                                        <View>
                                            <ext:GridView ID="GridView1" runat="server" StripeRows="true">
                                                <GetRowClass Fn="getRowClass" />
                                            </ext:GridView>
                                        </View>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="Zhong" runat="server" AutoScroll="true" Region="Center">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Stretch" Padding="5" />
                                </LayoutConfig>
                                <Items>
                                    <ext:Panel ID="ZhongPanel" Region="Center" Width="100" Html="<table width='100%' height='100%'><tr align='center'><td align='center'>中</td></tr></table> "
                                        runat="server" AutoScroll="true">
                                    </ext:Panel>
                                    <ext:GridPanel ID="ZhongGridPanel" Height="180" runat="server" Frame="true" StoreID="ZhongPlanStore"
                                        ContextMenuID="Menu2" Region="Center" Scroll="Vertical">
                                        <Plugins>
                                        </Plugins>
                                        <ColumnModel ID="ColumnModel4" runat="server">
                                            <Columns>
                                                <ext:RowNumbererColumn ID="RowNumbererColumn4" runat="server" Width="35" />
                                                <ext:Column ID="Column2" runat="server" Width="180" Text="物料名称" DataIndex="mater_name">
                                                </ext:Column>
                                                <ext:Column ID="Column3" runat="server" DataIndex="recipe_code" Width="150" Text="配方编号">
                                                </ext:Column>
                                                <ext:Column ID="Column4" runat="server" Text="生产车数" Sortable="true" DataIndex="plan_num">
                                                </ext:Column>
                                                <ext:CommandColumn ID="CommandColumn4" Hidden="true" runat="server" Text="操作">
                                                    <Commands>
                                                        <ext:GridCommand Text="操作" StandOut="true">
                                                            <Menu>
                                                                <Items>
                                                                    <ext:MenuCommand CommandName="ItemCommand" Text="Item" />
                                                                    <ext:MenuCommand CommandName="ItemCommand" Text="Item" />
                                                                </Items>
                                                            </Menu>
                                                        </ext:GridCommand>
                                                    </Commands>
                                                </ext:CommandColumn>
                                            </Columns>
                                        </ColumnModel>
                                        <View>
                                            <ext:GridView ID="GridView4" runat="server" StripeRows="true">
                                                <GetRowClass Fn="getRowClass" />
                                            </ext:GridView>
                                        </View>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="Ye" runat="server" AutoScroll="true" Region="Center">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Stretch" Padding="5" />
                                </LayoutConfig>
                                <Items>
                                    <ext:Panel ID="YePanel" Region="Center" Width="100" Html="<table width='100%' height='100%'><tr align='center'><td align='center'>夜</td></tr></table> "
                                        runat="server" AutoScroll="true">
                                    </ext:Panel>
                                    <ext:GridPanel ID="YeGridPanel" Height="180" runat="server" Frame="true" StoreID="YePlanStore"
                                        ContextMenuID="Menu3" Region="Center" Scroll="Vertical">
                                        <Plugins>
                                        </Plugins>
                                        <ColumnModel ID="ColumnModel2" runat="server">
                                            <Columns>
                                                <ext:RowNumbererColumn ID="RowNumbererColumn2" runat="server" Width="35" />
                                                <ext:Column ID="Column7" runat="server" Width="180" Text="物料名称" DataIndex="mater_name">
                                                </ext:Column>
                                                <ext:Column ID="Column8" runat="server" DataIndex="recipe_code" Width="150" Text="配方编号">
                                                </ext:Column>
                                                <ext:Column ID="Column9" runat="server" Text="生产车数" Sortable="true" DataIndex="plan_num">
                                                </ext:Column>
                                                <ext:CommandColumn ID="CommandColumn2" Hidden="true" runat="server" Text="操作">
                                                    <Commands>
                                                        <ext:GridCommand Text="操作" StandOut="true">
                                                            <Menu>
                                                                <Items>
                                                                    <ext:MenuCommand CommandName="ItemCommand" Text="Item" />
                                                                    <ext:MenuCommand CommandName="ItemCommand" Text="Item" />
                                                                </Items>
                                                            </Menu>
                                                        </ext:GridCommand>
                                                    </Commands>
                                                </ext:CommandColumn>
                                            </Columns>
                                        </ColumnModel>
                                        <View>
                                            <ext:GridView ID="GridView2" runat="server" StripeRows="true">
                                                <GetRowClass Fn="getRowClass" />
                                            </ext:GridView>
                                        </View>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Hidden ID="hidden_parent_num" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hidden_type" runat="server">
            </ext:Hidden>
        </Items>
    </ext:Viewport>
    <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="药品计划日期、班次选择"
        Width="300" Height="350" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
        BodyPadding="5" Layout="Form">
        <Items>
            <ext:FormPanel ID="pnlEdit" runat="server" Flex="1" BodyPadding="5">
                <Items>
                    <ext:TextField ID="txtMater_name" runat="server" ReadOnly="true" FieldLabel="物料名称"
                        LabelAlign="Left" />
                    <ext:TextField ID="txtRecipe_code" runat="server" FieldLabel="配方编号" LabelAlign="Left"
                        ReadOnly="true" />
                    <ext:ComboBox ID="txtWorkShopCode" Editable="false" runat="server" FieldLabel="设备车间"
                        LabelAlign="Left">
                        <Listeners>
                            <Select Handler="#{cbo_equip_code}.clearValue(); #{equipCodeStore}.reload();" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox ID="cbo_equip_code" Editable="false" runat="server" FieldLabel="机台名称"
                        LabelAlign="Left" TypeAhead="true" QueryMode="Local" ForceSelection="true" TriggerAction="All"
                        DisplayField="name" ValueField="id">
                        <Store>
                            <ext:Store runat="server" ID="equipCodeStore" AutoLoad="false" OnReadData="EquipCodeRefresh">
                                <Model>
                                    <ext:Model ID="Model5" runat="server" IDProperty="Id">
                                        <Fields>
                                            <ext:ModelField Name="id" Type="String" ServerMapping="Id" />
                                            <ext:ModelField Name="name" Type="String" ServerMapping="Name" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                                <Listeners>
                                    <Load Handler="#{cbo_equip_code}.setValue(#{cbo_equip_code}.store.getAt(0).get('id'));" />
                                </Listeners>
                            </ext:Store>
                        </Store>
                    </ext:ComboBox>
                    <ext:TextField ID="txtPlanNum" runat="server" FieldLabel="计划车数" LabelAlign="Left"
                        IsRemoteValidation="true" />
                    <ext:DateField ID="txtPlanDate" runat="server" Editable="false" Vtype="daterange"
                        FieldLabel="计划日期" EnableKeyEvents="true" Format="yyyy-MM-dd">
                        <Listeners>
                            <Change Fn="ChangeXLDate" />
                        </Listeners>
                    </ext:DateField>
                    <ext:ComboBox ID="cboPlanShift" runat="server" Editable="false" StoreID="ShiftStore"
                        FieldLabel="计划班次" LabelAlign="Left" ValueField="ObjID" DisplayField="ShiftName">
                        <Listeners>
                            <Select Handler="#{txtClassName}.clear();ChangeXLDate();" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:TextField ID="txtClassName" runat="server" FieldLabel="计划班组" AllowBlank="false" LabelAlign="Left"
                        IsRemoteValidation="true" ReadOnly="true" />
                    <ext:TextField ID="txtMater_Code" runat="server" FieldLabel="物料编码" Hidden="true"
                        LabelAlign="Left" />
                    <ext:TextField ID="txtRecipe_type" runat="server" FieldLabel="接受状态" Hidden="true"
                        LabelAlign="Left" />
                    <ext:TextField ID="txtTotal_weight" runat="server" FieldLabel="每车重量" Hidden="true"
                        LabelAlign="Left" />
                    <ext:TextField ID="txtEdt_Code" runat="server" FieldLabel="配方版本" Hidden="true" LabelAlign="Left" />
                </Items>
            </ext:FormPanel>
        </Items>
        <Buttons>
            <ext:Button ID="btnModifySave" runat="server" Text="确定" Icon="Accept">
                <DirectEvents>
                    <Click OnEvent="BtnModifySave_Click">
                    </Click>
                </DirectEvents>
            </ext:Button>
            <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                <DirectEvents>
                    <Click OnEvent="BtnCancel_Click">
                    </Click>
                </DirectEvents>
            </ext:Button>
        </Buttons>
        <Listeners>
            <Show Handler="for(var i=0;i<#{Viewport1}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
            <Hide Handler="for(var i=0;i<#{Viewport1}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
        </Listeners>
    </ext:Window>
    <ext:Window ID="AllWin" runat="server" Icon="MonitorEdit" Closable="false" Title="药品计划日期、班次选择"
        Width="300" Height="250" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
        BodyPadding="5" Layout="Form">
        <Items>
            <ext:FormPanel ID="FormPanel2" runat="server" Flex="1" BodyPadding="5">
                <Items>
                    <ext:DateField ID="txtAllPlanDate" runat="server" Editable="false" AllowBlank="false"
                        Vtype="daterange" FieldLabel="计划日期" EnableKeyEvents="true" Format="yyyy-MM-dd">
                        <Listeners>
                            <Change Fn="ChangeXLDate" />
                        </Listeners>
                    </ext:DateField>
                    <ext:ComboBox ID="cboAllPlanShift" runat="server" Editable="false" IsRemoteValidation="true"
                        StoreID="ShiftStore" FieldLabel="计划班次" LabelAlign="Left" ValueField="ObjID" DisplayField="ShiftName">
                        <Listeners>
                            <Select Handler="#{txtAllClassName}.clear();ChangeXLDate();" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:TextField ID="txtAllClassName" runat="server" FieldLabel="计划班组" LabelAlign="Left"
                        IsRemoteValidation="true" Disabled="true" />
                </Items>
                <Listeners>
                    <ValidityChange Handler="#{btnModifySave}.setDisabled(!valid);" />
                </Listeners>
            </ext:FormPanel>
        </Items>
        <Buttons>
            <ext:Button ID="Button1" runat="server" Text="确定" Icon="Accept">
                <DirectEvents>
                    <Click OnEvent="BtnModifyAllSave_Click">
                    </Click>
                </DirectEvents>
            </ext:Button>
            <ext:Button ID="Button2" runat="server" Text="取消" Icon="Cancel">
                <DirectEvents>
                    <Click OnEvent="BtnCancel_Click">
                    </Click>
                </DirectEvents>
            </ext:Button>
        </Buttons>
        <Listeners>
            <Show Handler="for(var i=0;i<#{Viewport1}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
            <Hide Handler="for(var i=0;i<#{Viewport1}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
        </Listeners>
    </ext:Window>
    </form>
</body>
</html>
