﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_ProducingPlan_PlanExecMonitoring_PlanMonitoring, App_Web_g25qpua0" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>计划执行监控</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script src="<%= Page.ResolveUrl("./") %>PlanMon.js" type="text/javascript"></script>
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
    <ext:Store ID="ZaoPlanStore" runat="server" AutoSync="true">
        <Sorters>
        </Sorters>
        <Model>
            <ext:Model ID="Model1" runat="server" Name="gridPanelZaoStoreModel" IDProperty="PlanID">
                <Fields>
                    <ext:ModelField Name="RecipeMaterialName" Type="String" Mapping="RecipeMaterialName" />
                    <ext:ModelField Name="PlanDate" Type="Date" Mapping="PlanDate" />
                    <ext:ModelField Name="RecipeEquipCode" Type="String" Mapping="RecipeEquipCode" />
                    <ext:ModelField Name="RealPlanNum" Type="String" Mapping="RealPlanNum" />
                    <ext:ModelField Name="Per" Type="String" Mapping="Per" />
                    <ext:ModelField Name="Num" Type="Float" Mapping="Num" />
                    <ext:ModelField Name="RealEndtime" Type="Date" Mapping="RealEndtime" />
                    <ext:ModelField Name="PlanID" Type="String" Mapping="PlanID" />
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
                    <ext:ModelField Name="PlanDate" Type="Date" Mapping="PlanDate" />
                    <ext:ModelField Name="RecipeEquipCode" Type="String" Mapping="RecipeEquipCode" />
                    <ext:ModelField Name="RealPlanNum" Type="String" Mapping="RealPlanNum" />
                    <ext:ModelField Name="Per" Type="String" Mapping="Per" />
                    <ext:ModelField Name="Num" Type="Float" Mapping="Num" />
                    <ext:ModelField Name="RealEndtime" Type="Date" Mapping="RealEndtime" />
                    <ext:ModelField Name="PlanID" Type="String" Mapping="PlanID" />
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
                    <ext:ModelField Name="PlanDate" Type="Date" Mapping="PlanDate" />
                    <ext:ModelField Name="RecipeEquipCode" Type="String" Mapping="RecipeEquipCode" />
                    <ext:ModelField Name="RealPlanNum" Type="String" Mapping="RealPlanNum" />
                    <ext:ModelField Name="Per" Type="String" Mapping="Per" />
                    <ext:ModelField Name="Num" Type="Float" Mapping="Num" />
                    <ext:ModelField Name="RealEndtime" Type="Date" Mapping="RealEndtime" />
                    <ext:ModelField Name="PlanID" Type="String" Mapping="PlanID" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Menu ID="Menu1" runat="server">
        <Items>
            <ext:MenuItem ID="MenuDel" runat="server" Icon="Delete" Text="修改">
                <Listeners>
                    <Click Fn="zaoPlan" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="MenuEdit" runat="server" Icon="Add" Text="执行查询">
                <Listeners>
                    <Click Fn="zaoPlanInfo" />
                </Listeners>
            </ext:MenuItem>
            <%--<ext:MenuItem ID="zaoPlan" runat="server" Icon="PackageGo" Text="配方查询">
                <Listeners>
                    <Click Fn="zaoPlan" />
                </Listeners>
            </ext:MenuItem>--%>
        </Items>
    </ext:Menu>
    <ext:Menu ID="Menu2" runat="server">
        <Items>
            <ext:MenuItem ID="MenuItem2" runat="server" Icon="Delete" Text="修改">
                <Listeners>
                    <Click Fn="zhongPlan" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="MenuItem3" runat="server" Icon="Add" Text="执行查询">
                <Listeners>
                    <Click Fn="zhongPlanInfo" />
                </Listeners>
            </ext:MenuItem>
            <%--<ext:MenuItem ID="MenuItem4" runat="server" Icon="PackageGo" Text="配方查询">
                <Listeners>
                    <Click Fn="zhongPlan" />
                </Listeners>
            </ext:MenuItem>--%>
        </Items>
    </ext:Menu>
    <ext:Menu ID="Menu3" runat="server">
        <Items>
            <ext:MenuItem ID="MenuItem8" runat="server" Icon="Delete" Text="修改">
                <Listeners>
                    <Click Fn="yePlan" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="MenuItem9" runat="server" Icon="Add" Text="执行查询">
                <Listeners>
                    <Click Fn="yePlanInfo" />
                </Listeners>
            </ext:MenuItem>
            <%-- <ext:MenuItem ID="MenuItem10" runat="server" Icon="PackageGo" Text="配方查询">
                <Listeners>
                    <Click Fn="yePlan" />
                </Listeners>
            </ext:MenuItem>--%>
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
                                                <ext:Column ID="SummaryColumn2" runat="server" Width="180" Text="物料名称" DataIndex="RecipeMaterialName">
                                                </ext:Column>
                                                <ext:Column ID="SummaryColumn4" runat="server" Width="90" Text="完成数/计划数" DataIndex="RealPlanNum">
                                                </ext:Column>
                                                <ext:Column ID="SummaryColumn6" runat="server" Text="完成日期" DataIndex="RealEndtime"
                                                    Width="180">
                                                    <Renderer Format="Date" FormatArgs="'Y-m-d H:m:s'" />
                                                </ext:Column>
                                                <ext:ComponentColumn ID="ComponentColumn1" DataIndex="Num" runat="server" Text="计划监控">
                                                    <Component>
                                                        <ext:ProgressBar ID="ProgressBar1" runat="server" Text="Progress">
                                                        </ext:ProgressBar>
                                                    </Component>
                                                    <Listeners>
                                                        <Bind Handler="cmp.updateProgress(record.get('Num'),record.get('Per'));" />
                                                    </Listeners>
                                                </ext:ComponentColumn>
                                            </Columns>
                                        </ColumnModel>
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
                                        <ColumnModel ID="ColumnModel4" runat="server">
                                            <Columns>
                                                <ext:RowNumbererColumn ID="RowNumbererColumn4" runat="server" Width="35" />
                                                <ext:Column ID="Column2" runat="server" Width="180" Text="物料名称" DataIndex="RecipeMaterialName">
                                                </ext:Column>
                                                <ext:Column ID="Column3" runat="server" DataIndex="RealPlanNum" Width="90" Text="完成数/计划数">
                                                </ext:Column>
                                                <ext:Column ID="Column6" runat="server" Text="完成日期" DataIndex="RealEndtime" Width="180">
                                                    <Renderer Format="Date" FormatArgs="'Y-m-d H:m:s'" />
                                                </ext:Column>
                                                <ext:ComponentColumn ID="ComponentColumn2" runat="server" Text="计划监控">
                                                    <Component>
                                                        <ext:ProgressBar ID="ProgressBar2" runat="server" Text="Progress">
                                                        </ext:ProgressBar>
                                                    </Component>
                                                    <Listeners>
                                                        <Bind Handler="cmp.updateProgress(record.get('Num'),record.get('Per'))" />
                                                    </Listeners>
                                                </ext:ComponentColumn>
                                            </Columns>
                                        </ColumnModel>
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
                                        <ColumnModel ID="ColumnModel2" runat="server">
                                            <Columns>
                                                <ext:RowNumbererColumn ID="RowNumbererColumn2" runat="server" Width="35" />
                                                <ext:Column ID="Column1" runat="server" Width="180" Text="物料名称" DataIndex="RecipeMaterialName">
                                                </ext:Column>
                                                <ext:Column ID="Column4" runat="server" DataIndex="RealPlanNum" Width="90" Text="完成数/计划数">
                                                </ext:Column>
                                                <ext:Column ID="Column5" runat="server" Text="完成日期" DataIndex="RealEndtime" Width="180">
                                                    <Renderer Format="Date" FormatArgs="'Y-m-d H:m:s'" />
                                                </ext:Column>
                                                <ext:ComponentColumn ID="ComponentColumn3" runat="server" Text="计划监控">
                                                    <Component>
                                                        <ext:ProgressBar ID="ProgressBar3" runat="server" Text="Progress">
                                                        </ext:ProgressBar>
                                                    </Component>
                                                    <Listeners>
                                                        <Bind Handler="cmp.updateProgress(record.get('Num'),record.get('Per'))" />
                                                    </Listeners>
                                                </ext:ComponentColumn>
                                            </Columns>
                                        </ColumnModel>
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
    <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="计划执行详情信息"
        Width="300" Height="300" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
        BodyPadding="5" Layout="Form">
        <Items>
            <ext:FormPanel ID="pnlEdit" runat="server" Flex="1" BodyPadding="5">
                <Items>
                    <ext:TextField ID="txtPlanID" Hidden="true" runat="server" Disabled="true" FieldLabel="计划号"
                        LabelAlign="Left" />
                    <ext:TextField ID="txtRecipeMaterialName" runat="server" Disabled="true" FieldLabel="物料名称"
                        LabelAlign="Left" />
                    <ext:TextField ID="txtRecipeName" runat="server" FieldLabel="配方编号" LabelAlign="Left"
                        Disabled="true" />
                    <ext:NumberField ID="txtPlanNum" MinValue="1" MinText="1" runat="server" FieldLabel="计划完成车数"
                        LabelAlign="Left" Disabled="true">
                        <Listeners>
                            <Blur Handler="NumChange();" />
                        </Listeners>
                    </ext:NumberField>
                    <ext:TextField ID="txtPlanWeight" runat="server" MaskRe="/[0-9\$\.]/" FieldLabel="计划完成重量"
                        LabelAlign="Left" AllowBlank="false" Disabled="true" />
                    <ext:NumberField ID="txtRealNum" MinValue="0" MinText="0" runat="server" FieldLabel="实际完成车数"
                        LabelAlign="Left" Disabled="true">
                        <Listeners>
                            <Change Handler="NumChange();">
                            </Change>
                        </Listeners>
                    </ext:NumberField>
                    <ext:TextField ID="txtRealWeight" runat="server" FieldLabel="实际完成重量" LabelAlign="Left"
                        AllowBlank="false" Disabled="true" MaskRe="/[0-9\$\.]/" />
                    <ext:DateField ID="txtRealStartTime" runat="server" Editable="false" AllowBlank="false"
                        Vtype="daterange" FieldLabel="开始执行时间" Disabled="true" EnableKeyEvents="true"
                        Format="Y-m-d H:m:s">
                    </ext:DateField>
                    <ext:DateField ID="txtRealEndtime" runat="server" Disabled="true" Editable="false"
                        AllowBlank="false" Vtype="daterange" FieldLabel="完成时间" EnableKeyEvents="true"
                        Format="Y-m-d H:m:s">
                    </ext:DateField>
                </Items>
                <Listeners>
                    <ValidityChange Handler="#{btnModifySave}.setDisabled(!valid);" />
                </Listeners>
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
    <ext:TaskManager ID="TaskManager1" runat="server">
        <Tasks>
            <ext:Task TaskID="servertime" Interval="60000">
                <DirectEvents>
                    <Update OnEvent="RefreshStore">
                    </Update>
                </DirectEvents>
            </ext:Task>
        </Tasks>
    </ext:TaskManager>
    </form>
</body>
</html>
