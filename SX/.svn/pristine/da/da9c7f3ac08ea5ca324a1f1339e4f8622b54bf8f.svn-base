<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryPlanEntering.aspx.cs"
    Inherits="Manager_ProducingPlan_PlanEntering_QueryPlanEntering" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>计划录入查询</title>
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
    <script>
        var totalCost = function (records) {
            var i = 0,
                length = records.length,
                total = 0,
                record;

            for (; i < length; ++i) {
                record = records[i];
                total += record.get('Estimate') * record.get('Rate');
            }
            return total;
        };
    </script>
    <script>
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
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Store ID="Store1" runat="server" GroupField="Name">
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
    <ext:GridPanel ID="GridPanel1" runat="server" Frame="true" StoreID="Store1" Title="Sponsored Projects" ContextMenuID="Menu1"
        Collapsible="true" AnimCollapse="false" Icon="ApplicationViewColumns" Width="800"
        Height="450">
        <Plugins>
            <ext:CellEditing ID="CellEditing1" runat="server" ClicksToEdit="1" />
        </Plugins>
        <ColumnModel ID="ColumnModel1" runat="server">
            <Columns>
                <ext:SummaryColumn ID="SummaryColumn1" runat="server" TdCls="task" Text="Task" Sortable="true"
                    DataIndex="Description" Hideable="false" SummaryType="Count" Flex="1">
                    <SummaryRenderer Handler="return ((value === 0 || value > 1) ? '(' + value +' Tasks)' : '(1 Task)');" />
                </ext:SummaryColumn>
                <ext:Column ID="Column1" runat="server" Text="Project" DataIndex="Name" Width="20" />
                <ext:Column runat="server" Text="Stock Price">
                    <Columns>
                        <ext:SummaryColumn ID="SummaryColumn2" runat="server" Width="85" Text="Due Date"
                            Sortable="true" DataIndex="Due" SummaryType="Max">
                            <Renderer Format="Date" FormatArgs="'m/d/Y'" />
                            <SummaryRenderer Fn="Ext.util.Format.dateRenderer('m/d/Y')" />
                            <Editor>
                                <ext:DateField ID="DateField1" runat="server" Format="MM/dd/yyyy" />
                            </Editor>
                        </ext:SummaryColumn>
                        <ext:SummaryColumn ID="SummaryColumn3" runat="server" Width="75" Text="Estimate"
                            Sortable="true" DataIndex="Estimate" SummaryType="Sum">
                            <Renderer Handler="return value +' hours';" />
                            <SummaryRenderer Handler="return value +' hours';" />
                            <Editor>
                                <ext:NumberField ID="NumberField1" runat="server" AllowBlank="false" MinValue="0"
                                    StyleSpec="text-align:left" />
                            </Editor>
                        </ext:SummaryColumn>
                        <ext:SummaryColumn ID="SummaryColumn4" runat="server" Width="75" Text="Rate" Sortable="true"
                            DataIndex="Rate" SummaryType="Average">
                            <Renderer Format="UsMoney" />
                            <SummaryRenderer Fn="Ext.util.Format.usMoney" />
                            <Editor>
                                <ext:NumberField ID="NumberField2" runat="server" AllowBlank="false" MinValue="0"
                                    StyleSpec="text-align:left" />
                            </Editor>
                        </ext:SummaryColumn>
                    </Columns>
                </ext:Column>
                <ext:SummaryColumn runat="server" Width="75" ID="Cost" Text="Cost" Sortable="false"
                    Groupable="false" DataIndex="Cost" CustomSummaryType="totalCost">
                    <Renderer Handler="return Ext.util.Format.usMoney(record.data.Estimate * record.data.Rate);" />
                    <SummaryRenderer Fn="Ext.util.Format.usMoney" />
                </ext:SummaryColumn>
                <ext:CommandColumn ID="CommandColumn1" runat="server" Hidden="true">
                    <GroupCommands>
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
                    </GroupCommands>
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
            <ext:RowSelectionModel runat="server" ID="r1">
            </ext:RowSelectionModel>
            <%--<ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" RowSpan="1" />--%>
            <%-- <ext:CheckboxSelectionModel ID="CheckboxSelectionModel2" runat="server">
                <CustomConfig>
                    <ext:ConfigItem Name="aa" Value="true" Mode="Raw">
                    </ext:ConfigItem>
                </CustomConfig>
            </ext:CheckboxSelectionModel>--%>
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
    </form>
</body>
</html>
