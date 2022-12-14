<%@ page language="C#" autoeventwireup="true" inherits="Manager_ProducingPlan_PlanExecMonitoring_PlanExecMonitoring, App_Web_g25qpua0" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>计划执行监控</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
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
        var prepareGroupToolbar = function (grid, toolbar, groupId, records) {
            // you can prepare ready toolbar
        };

        var onGroupCommand = function (column, command, group) {
            if (command === 'SelectGroup') {
                column.grid.getSelectionModel().select(group.children, true);
                return;
            }

            Ext.Msg.alert(command, 'Group name: ' + group.name + '<br/>Count - ' + group.children.length);
        };

        var getAdditionalData = function (data, idx, record, orig) {
            var o = Ext.grid.feature.RowBody.prototype.getAdditionalData.apply(this, arguments),
                d = data;

            Ext.apply(o, {
                rowBodyColspan: record.fields.getCount(),
                rowBody: Ext.String.format('<div style=\'padding:0 5px 5px 5px;\'>The {0} [{1}] requires light conditions of <i>{2}</i>.<br /><b>Price: {3}</b></div>', d.Common, d.Botanical, d.Light, Ext.util.Format.usMoney(d.Price)),
                rowBodyCls: ""
            });

            return o;
        };

    </script>
    <script type="text/javascript">
        function deleteuser() {
            //            var grid = #{GridPanel1};
            //            sm = grid.getSelectionModel().getSelected();
            //            alert("删除物料：" + sm.data.Name.toString());
            //Ext.net.DirectMethods.Deletestudent(record.data.stuid.toString());  
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
    <ext:Store ID="Store1" runat="server" GroupField="Name" PageSize="20">
        <Sorters>
            <ext:DataSorter Property="Due" Direction="ASC" />
        </Sorters>
        <Model>
            <ext:Model ID="Model1" runat="server" IDProperty="TaskID">
                <Fields>
                    <ext:ModelField Name="ProjectID" />
                    <ext:ModelField Name="Name" />
                    <ext:ModelField Name="TaskID" />
                    <ext:ModelField Name="Description" />
                    <ext:ModelField Name="Estimate" Type="Int" />
                    <ext:ModelField Name="Rate" Type="Float" />
                    <ext:ModelField Name="Cost" Type="Float" />
                    <ext:ModelField Name="Due" Type="Date" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Menu ID="Menu1" runat="server">
        <Items>
            <ext:MenuItem ID="MenuDel" runat="server" Icon="Delete" Text="删除用户">
                <Listeners>
                    <Click Fn="deleteuser" />
                </Listeners>
            </ext:MenuItem>
            <ext:MenuItem ID="MenuEdit" runat="server" Icon="PackageGo" Text="修改用户">
                <Listeners>
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
                                                    <ext:DateField ID="txtStratShiftDate" runat="server" Editable="false" AllowBlank="false"
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
                    <ext:GridPanel ID="GridPanel1" runat="server" Frame="true" StoreID="Store1" ContextMenuID="Menu1"
                        Region="Center">
                        <Plugins>
                            <ext:CellEditing ID="CellEditing1" runat="server" ClicksToEdit="1" />
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
                                            DataIndex="Due">
                                            <%--<Renderer Format="Date" FormatArgs="'m/d/Y'" />
                                            <SummaryRenderer Fn="Ext.util.Format.dateRenderer('m/d/Y')" />--%>
                                            <Editor>
                                                <ext:DateField ID="DateField1" runat="server" Format="MM/dd/yyyy" />
                                            </Editor>
                                        </ext:SummaryColumn>
                                        <ext:SummaryColumn ID="SummaryColumn3" runat="server" Text="配方编号" Sortable="true"
                                            DataIndex="Estimate">
                                            <%-- <Renderer Handler="return value +' hours';" />
                                            <SummaryRenderer Handler="return value +' hours';" />--%>
                                            <Editor>
                                                <ext:NumberField ID="NumberField1" runat="server" AllowBlank="false" MinValue="0"
                                                    StyleSpec="text-align:left" />
                                            </Editor>
                                        </ext:SummaryColumn>
                                        <ext:SummaryColumn ID="SummaryColumn4" runat="server" Text="生产车数" Sortable="true"
                                            DataIndex="Rate" SummaryType="Sum">
                                            <Editor>
                                                <ext:NumberField ID="NumberField2" runat="server" AllowBlank="false" MinValue="0"
                                                    StyleSpec="text-align:left" />
                                            </Editor>
                                        </ext:SummaryColumn>
                                    </Columns>
                                </ext:Column>
                                <ext:Column ID="clZhong" runat="server" Text="中">
                                    <Columns>
                                        <ext:SummaryColumn ID="SummaryColumn5" runat="server" Text="物料名称" Sortable="true"
                                            DataIndex="Due">
                                            <Editor>
                                                <ext:DateField ID="DateField2" runat="server" Format="MM/dd/yyyy" />
                                            </Editor>
                                        </ext:SummaryColumn>
                                        <ext:SummaryColumn ID="SummaryColumn6" runat="server" Text="配方编号" Sortable="true"
                                            DataIndex="Estimate">
                                            <Editor>
                                                <ext:NumberField ID="NumberField3" runat="server" AllowBlank="false" MinValue="0"
                                                    StyleSpec="text-align:left" />
                                            </Editor>
                                        </ext:SummaryColumn>
                                        <ext:SummaryColumn ID="SummaryColumn7" runat="server" Text="生产车数" Sortable="true"
                                            DataIndex="Rate" SummaryType="Sum">
                                            <Editor>
                                                <ext:NumberField ID="NumberField4" runat="server" AllowBlank="false" MinValue="0"
                                                    StyleSpec="text-align:left" />
                                            </Editor>
                                        </ext:SummaryColumn>
                                    </Columns>
                                </ext:Column>
                                <ext:Column ID="clYe" runat="server" Text="夜">
                                    <Columns>
                                        <ext:SummaryColumn ID="SummaryColumn8" runat="server" Text="物料名称" Sortable="true"
                                            DataIndex="Due">
                                            <Editor>
                                                <ext:DateField ID="DateField3" runat="server" Format="MM/dd/yyyy" />
                                            </Editor>
                                        </ext:SummaryColumn>
                                        <ext:SummaryColumn ID="SummaryColumn9" runat="server" Text="配方编号" Sortable="true"
                                            DataIndex="Estimate">
                                            <Editor>
                                                <ext:NumberField ID="NumberField5" runat="server" AllowBlank="false" MinValue="0"
                                                    StyleSpec="text-align:left" />
                                            </Editor>
                                        </ext:SummaryColumn>
                                        <ext:SummaryColumn ID="SummaryColumn10" runat="server" Text="生产车数" Sortable="true"
                                            DataIndex="Rate" SummaryType="Sum">
                                            <Editor>
                                                <ext:NumberField ID="NumberField6" runat="server" AllowBlank="false" MinValue="0"
                                                    StyleSpec="text-align:left" />
                                            </Editor>
                                        </ext:SummaryColumn>
                                    </Columns>
                                </ext:Column>
                                <ext:CommandColumn ID="CommandColumn1" runat="server" Hidden="true" Text="操作">
                                    <Commands>
                                        <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                            <ToolTip Text="修改本条数据" />
                                        </ext:GridCommand>
                                    </Commands>
                                    <%--<GroupCommands>
                                        <ext:GridCommand Icon="TableRow" CommandName="SelectGroup">
                                            <ToolTip Title="Select" Text="Select all rows of the group" />
                                        </ext:GridCommand>
                                        <ext:CommandFill />
                                        <ext:GridCommand Text="Menu" StandOut="true">
                                            <Menu>
                                                <Items>
                                                    <ext:MenuCommand CommandName="ItemCommand" Text="Item" />
                                                    <ext:MenuCommand CommandName="ItemCommand" Text="Item" />
                                                </Items>
                                            </Menu>
                                        </ext:GridCommand>
                                    </GroupCommands>--%>
                                    <PrepareGroupToolbar Fn="prepareGroupToolbar" />
                                    <Listeners>
                                        <GroupCommand Fn="onGroupCommand" />
                                    </Listeners>
                                </ext:CommandColumn>
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
                                HideGroupedHeader="true" EnableGroupingMenu="false" />
                        </Features>
                        <TopBar>
                            <ext:Toolbar ID="Toolbar1" runat="server">
                                <Items>
                                    <ext:Button ID="Button1" runat="server" Text="Toggle" ToolTip="Toggle the visibility of summary row"
                                        EnableToggle="true" Pressed="true">
                                        <Listeners>
                                            <Click Handler="#{GroupingSummary1}.toggleSummaryRow(!#{GroupingSummary1}.showSummaryRow);#{GroupingSummary1}.view.refresh();" />
                                        </Listeners>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                    </ext:GridPanel>
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
