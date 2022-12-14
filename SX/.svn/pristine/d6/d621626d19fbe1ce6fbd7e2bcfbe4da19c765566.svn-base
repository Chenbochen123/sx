<%@ page language="C#" autoeventwireup="true" inherits="Manager_ProducingPlan_PlanEntering_PlanEntering, App_Web_qlhoypu3" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
    <script type="text/javascript">
        function deleteuser() {
            var grid = App.ZaoGridPanel;
            store = grid.store;
            sm = grid.getSelectionModel().getSelection()[0];
            if (sm) {
                alert("删除物料：" + sm.data.ShowName.toString());

                store.data.removeAt(store.indexOf(sm));
                grid.store.loadRecords(store.data.items, false);

            }
            //Ext.net.DirectMethods.Deletestudent(record.data.stuid.toString());  
        }
        function addPlan() {
            var grid = App.ZaoGridPanel;
            sm = grid.getSelectionModel().getSelection()[0];
            if (sm) {
                store = grid.store;
                var r = Ext.create('gridPanelCenterStoreModel', {
                    ShowName: sm.data.ShowName.toString(),
                    SerialNum: sm.data.SerialNum - 1
                })

                store.data.insert(store.indexOf(sm), r);
                grid.store.loadRecords(store.data.items, false);
            }
        }
    </script>
    <style>
        /* style rows on mouseover */
        .x-grid-row-over .x-grid-cell-inner
        {
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        //树形结构点击刷新右侧方法
        //点击tree产生配方相信信息 绑定到Gridpanel
        var menuItemClick = function (view, rcd, item, idx, event, eOpts) {
            var s = rcd.get('qtip');
            App.direct.LoadGridData(s, {
                success: function (result) {
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('Failure', errorMsg);
                }
            });
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Store ID="ZaoStore" runat="server" GroupField="ShowName">
        <Sorters>
            <ext:DataSorter Property="ID" Direction="ASC" />
        </Sorters>
        <Model>
            <ext:Model ID="Model1" Name="gridPanelCenterStoreModel" runat="server" IDProperty="ID">
                <Fields>
                     <ext:ModelField Name="EquipCode" />
                    <ext:ModelField Name="ShowName" />
                    <ext:ModelField Name="RecipeMaterialName" />
                    <ext:ModelField Name="RecipeMaterialCode" />
                    <ext:ModelField Name="RecipeName" />
                    <ext:ModelField Name="PlanNum" Type="Int" />
                    <ext:ModelField Name="SerialNum" Type="Int" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Store ID="ZhongStore" runat="server" GroupField="ShowName">
        <Sorters>
            <ext:DataSorter Property="ID" Direction="ASC" />
        </Sorters>
        <Model>
            <ext:Model ID="Model2" Name="gridPanelZhongStoreStoreModel" runat="server" IDProperty="ID">
                <Fields>
                    <ext:ModelField Name="EquipCode" Type="String" />
                    <ext:ModelField Name="ShowName" Type="String" />
                    <ext:ModelField Name="RecipeMaterialName" Type="String" />
                    <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                    <ext:ModelField Name="RecipeName" Type="String" />
                    <ext:ModelField Name="PlanNum" Type="Int" />
                    <ext:ModelField Name="SerialNum" Type="Int" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Store ID="YeStore" runat="server" GroupField="ShowName">
        <Sorters>
            <ext:DataSorter Property="ID" Direction="ASC" />
        </Sorters>
        <Model>
            <ext:Model ID="Model3" Name="gridPanelYeStoreModel" runat="server" IDProperty="ID">
                <Fields>
                    <ext:ModelField Name="EquipCode" />
                    <ext:ModelField Name="ShowName" />
                    <ext:ModelField Name="RecipeMaterialName" />
                    <ext:ModelField Name="RecipeMaterialCode" />
                    <ext:ModelField Name="RecipeName" />
                    <ext:ModelField Name="PlanNum" Type="Int" />
                    <ext:ModelField Name="SerialNum" Type="Int" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Menu ID="Menu1" runat="server">
        <Items>
            <ext:MenuItem ID="MenuDel" runat="server" Icon="Delete" Text="删除">
                <Listeners>
                    <Click Fn="deleteuser" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="MenuEdit" runat="server" Icon="PackageGo" Text="修改">
                <Listeners>
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="MenuItem1" runat="server" Icon="PackageGo" Text="插入">
                <Listeners>
                    <Click Fn="addPlan">
                    </Click>
                    <%--<Click Handler="#{ZaoStore}.insert(#{ZaoStore}.indexOf(#{ZaoGridPanel}.getSelectionModel().getSelection()[0]),new );" />--%>
                </Listeners>
            </ext:MenuItem>
        </Items>
    </ext:Menu>
    <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="Panel1" runat="server" Region="West" Width="120" Split="true" Layout="BorderLayout">
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
                        <TopBar>
                            <ext:Toolbar runat="server" ID="barUnit">
                                <Items>
                                    <ext:Button runat="server" Icon="Add" Text="添加" ID="btn_add">
                                        <ToolTips>
                                            <ext:ToolTip ID="ttAdd" runat="server" Html="点击进行添加" />
                                        </ToolTips>
                                        <DirectEvents>
                                            <Click OnEvent="btn_add_Click">
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                    <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                    <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                        <ToolTips>
                                            <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                        </ToolTips>
                                        <Listeners>
                                            <%--<Click Fn="pnlListFresh">
                                            </Click>--%>
                                        </Listeners>
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
                                    <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                        <Items>
                                            <ext:FormPanel ID="FormPanel1" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                                <Items>
                                                    <ext:DateField ID="txtStratPlanDate" runat="server" Editable="false" AllowBlank="false"
                                                        Vtype="daterange" FieldLabel="计划日期" EnableKeyEvents="true">
                                                    </ext:DateField>
                                                </Items>
                                                <Listeners>
                                                    <ValidityChange Handler="#{btn_search}.setDisabled(!valid);" />
                                                </Listeners>
                                            </ext:FormPanel>
                                        </Items>
                                        <Listeners>
                                            <ValidityChange Handler="#{btn_search}.setDisabled(!valid);" />
                                        </Listeners>
                                    </ext:FormPanel>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="Panel3" runat="server" Region="Center" AutoScroll="true">
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Stretch" Padding="5" />
                        </LayoutConfig>
                        <Items>
                            <ext:GridPanel ID="ZaoGridPanel" runat="server" StoreID="ZaoStore" Scroll="Vertical"
                                ContextMenuID="Menu1">
                                <Plugins>
                                    <ext:CellEditing ID="CellEditing1" runat="server" />
                                </Plugins>
                                <ColumnModel ID="ColumnModel1" runat="server">
                                    <Columns>
                                        <ext:SummaryColumn ID="SummaryColumn1" runat="server" Width="60" Hidden="true" TdCls="task"
                                            Text="机台名称" Sortable="true" Hideable="false" SummaryType="Count">
                                            <SummaryRenderer Handler="return ((value === 0 || value > 1) ? '(' + value +' 条计划)' : '(1 条计划)');" />
                                        </ext:SummaryColumn>
                                        <ext:Column ID="clZao" runat="server" Text="早">
                                            <Columns>
                                                <ext:SummaryColumn ID="SummaryColumn2" runat="server" Text="物料名称" Sortable="true"
                                                    DataIndex="RecipeMaterialName">
                                                    <Editor>
                                                        <ext:DateField ID="DateField1" runat="server" Format="MM/dd/yyyy" />
                                                    </Editor>
                                                </ext:SummaryColumn>
                                                <ext:SummaryColumn ID="SummaryColumn3" runat="server" Text="配方编号" Sortable="true"
                                                    DataIndex="RecipeName">
                                                    <Editor>
                                                        <ext:NumberField ID="NumberField1" runat="server" AllowBlank="false" MinValue="0"
                                                            StyleSpec="text-align:left" />
                                                    </Editor>
                                                </ext:SummaryColumn>
                                                <ext:SummaryColumn ID="SummaryColumn4" runat="server" Text="生产车数" Sortable="true"
                                                    DataIndex="PlanNum" SummaryType="Sum">
                                                    <Editor>
                                                        <ext:NumberField ID="NumberField2" runat="server" AllowBlank="false" MinValue="0"
                                                            StyleSpec="text-align:left" />
                                                    </Editor>
                                                </ext:SummaryColumn>
                                            </Columns>
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <View>
                                    <ext:GridView ID="GridView1" runat="server" StripeRows="true" MarkDirty="false" />
                                </View>
                                <SelectionModel>
                                    <%--<ext:RowSelectionModel runat="server" ID="r1">
                            </ext:RowSelectionModel>--%>
                                    <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" RowSpan="1" />
                                </SelectionModel>
                                <Features>
                                    <ext:GroupingSummary ID="GroupingSummary1" runat="server" GroupHeaderTplString="{name}"
                                        HideGroupedHeader="true" EnableGroupingMenu="true" />
                                </Features>
                            </ext:GridPanel>
                            <ext:GridPanel ID="ZhongGridPanel" runat="server" StoreID="ZhongStore" Scroll="Vertical"
                                ContextMenuID="Menu1">
                                <Plugins>
                                    <ext:CellEditing ID="CellEditing2" runat="server" />
                                </Plugins>
                                <ColumnModel ID="ColumnModel2" runat="server">
                                    <Columns>
                                        <ext:SummaryColumn ID="SummaryColumnzhong" runat="server" Width="60" Hidden="true"
                                            TdCls="task" Text="机台名称" Sortable="true" Hideable="false" SummaryType="Count">
                                            <SummaryRenderer Handler="return ((value === 0 || value > 1) ? '(' + value +' 条计划)' : '(1 条计划)');" />
                                        </ext:SummaryColumn>
                                        <ext:Column ID="clZhong" runat="server" Text="中">
                                            <Columns>
                                                <ext:SummaryColumn ID="SummaryColumn5" runat="server" Text="物料名称" Sortable="true"
                                                    DataIndex="RecipeMaterialName">
                                                    <Editor>
                                                        <%-- <ext:DateField ID="DateField2" runat="server" Format="MM/dd/yyyy" />--%>
                                                    </Editor>
                                                </ext:SummaryColumn>
                                                <ext:SummaryColumn ID="SummaryColumn6" runat="server" Text="配方编号" Sortable="true"
                                                    DataIndex="RecipeName">
                                                    <Editor>
                                                        <ext:NumberField ID="NumberField3" runat="server" AllowBlank="false" MinValue="0"
                                                            StyleSpec="text-align:left" />
                                                    </Editor>
                                                </ext:SummaryColumn>
                                                <ext:SummaryColumn ID="SummaryColumn7" runat="server" Text="生产车数" Sortable="true"
                                                    DataIndex="PlanNum" SummaryType="Sum">
                                                    <Editor>
                                                        <ext:NumberField ID="NumberField4" runat="server" AllowBlank="false" MinValue="0"
                                                            StyleSpec="text-align:left" />
                                                    </Editor>
                                                </ext:SummaryColumn>
                                            </Columns>
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <View>
                                    <ext:GridView ID="GridView2"  runat="server" StripeRows="true" MarkDirty="false" />
                                </View>
                                <SelectionModel>
                                    <ext:CheckboxSelectionModel ID="CheckboxSelectionModel2" runat="server" RowSpan="1" />
                                </SelectionModel>
                                <Features>
                                    <ext:GroupingSummary ID="GroupingSummary2" runat="server"
                                        GroupHeaderTplString="{name}" HideGroupedHeader="true" EnableGroupingMenu="false" />
                                </Features>
                            </ext:GridPanel>
                            <ext:GridPanel ID="YeGridPanel" runat="server" StoreID="YeStore" Scroll="Vertical"
                                >
                                <Plugins>
                                    <ext:CellEditing ID="CellEditing3" runat="server" />
                                </Plugins>
                                <ColumnModel ID="ColumnModel3" runat="server">
                                    <Columns>
                                        <ext:SummaryColumn ID="SummaryColumnye9" runat="server" Width="60" Hidden="true"
                                            TdCls="task" Text="机台名称" Sortable="true" Hideable="false" SummaryType="Count">
                                            <SummaryRenderer Handler="return ((value === 0 || value > 1) ? '(' + value +' 条计划)' : '(1 条计划)');" />
                                        </ext:SummaryColumn>
                                        <ext:Column ID="clYe" runat="server" Text="夜">
                                            <Columns>
                                                <ext:SummaryColumn ID="SummaryColumn8" runat="server" Text="物料名称" Sortable="true"
                                                    DataIndex="RecipeMaterialName">
                                                    <Editor>
                                                        <ext:DateField ID="DateField3" runat="server" Format="MM/dd/yyyy" />
                                                    </Editor>
                                                </ext:SummaryColumn>
                                                <ext:SummaryColumn ID="SummaryColumn9" runat="server" Text="配方编号" Sortable="true"
                                                    DataIndex="RecipeName">
                                                    <Editor>
                                                        <ext:NumberField ID="NumberField5" runat="server" AllowBlank="false" MinValue="0"
                                                            StyleSpec="text-align:left" />
                                                    </Editor>
                                                </ext:SummaryColumn>
                                                <ext:SummaryColumn ID="SummaryColumn10" runat="server" Text="生产车数" Sortable="true"
                                                    DataIndex="PlanNum" SummaryType="Sum">
                                                    <Editor>
                                                        <ext:NumberField ID="NumberField6" runat="server" AllowBlank="false" MinValue="0"
                                                            StyleSpec="text-align:left" />
                                                    </Editor>
                                                </ext:SummaryColumn>
                                            </Columns>
                                        </ext:Column>
                                    </Columns>
                                </ColumnModel>
                                <View>
                                    <ext:GridView ID="GridView3" runat="server" StripeRows="true" MarkDirty="false" />
                                </View>
                                <SelectionModel>
                                    <ext:CheckboxSelectionModel ID="CheckboxSelectionModel3" runat="server" RowSpan="1" />
                                </SelectionModel>
                                <Features>
                                    <ext:GroupingSummary ID="GroupingSummary3" runat="server" GroupHeaderTplString="{name}"
                                        HideGroupedHeader="true" EnableGroupingMenu="false" />
                                </Features>
                            </ext:GridPanel>
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
    </form>
</body>
</html>
