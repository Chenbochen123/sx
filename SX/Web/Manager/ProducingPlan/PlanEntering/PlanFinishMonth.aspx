<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanFinishMonth.aspx.cs" Inherits="Manager_ProducingPlan_PlanEntering_PlanFinishMonth" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>月计划完成查询</title>
    <!--通用-->
    <%--<link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />--%>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <%--<script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>--%>
    <style type="text/css">
        .x-grid-row-collapsedYellow .x-grid-cell {     
            background-color: #FFFF00 !important;
        } 
    </style>
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            debugger;
            App.gridPanel1.store.currentPage = 1;
            App.gridPanel1.store.reload();
            return false;
        }

        var SetRowClass = function (record, rowIndex, rowParams, store) {
            var rowClass = '';
            if (record.get("胶名") == "合计") {
                rowClass = 'x-grid-row-collapsedYellow';
            }

            return rowClass;
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel20" runat="server" Region="North" AutoHeight="true">
                    <BottomBar>
                        <ext:StatusBar ID="StatusBar1" runat="server" Height="20" Text=""></ext:StatusBar>
                    </BottomBar>
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUser">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <Listeners>
                                        <Click Fn="gridPanelRefresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_Middle" />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
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
                        <ext:Container ID="container1" runat="server" Layout="HBoxLayout" Padding="5">
                            <Items>
                                <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="生产开始日期" LabelAlign="Right">
                                </ext:DateField>
                                <ext:DateField ID="txtEndTime" runat="server" FieldLabel="生产结束日期" LabelAlign="Right">
                                </ext:DateField>
                                <ext:ComboBox ID="txtDep" runat="server" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="部门">
                                </ext:ComboBox>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel1" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout" Flex="1">
                    <Items>
                        <ext:GridPanel ID="gridPanel1" runat="server" Region="Center" Frame="true" Title="月计划完成信息">
                            <Store>
                                <ext:Store ID="gridPanel1Store" runat="server" PageSize="1000">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="机台" />
                                                <ext:ModelField Name="班组" />
                                                <ext:ModelField Name="序号" />
                                                <ext:ModelField Name="胶名" />
                                                <ext:ModelField Name="配方编号" />
                                                <ext:ModelField Name="计划" />
                                                <ext:ModelField Name="实际" />
                                                <%--<ext:ModelField Name="备注" />--%>
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel1" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="30" />
                                    <ext:Column ID="Column1" DataIndex="机台" runat="server" Text="机台" Align="Center" Width="100" />
                                    <ext:Column ID="Column2" DataIndex="班组" runat="server" Text="班组" Align="Center" Width="60" />
                                    <ext:Column ID="Column3" DataIndex="序号" runat="server" Text="序号" Align="Center" Width="60" />
                                    <ext:Column ID="Column5" DataIndex="胶名" runat="server" Text="胶名" Align="Center" Width="120" />
                                    <ext:Column ID="Column6" DataIndex="配方编号" runat="server" Text="配方编号" Align="Center" Width="150" />
                                    <ext:Column ID="Column7" DataIndex="计划" runat="server" Text="计划" Align="Center" Width="80" />
                                    <ext:Column ID="Column8" DataIndex="实际" runat="server" Text="实际" Align="Center" Width="80" />
                                    <%--<ext:Column ID="Column9" DataIndex="备注" runat="server" Text="备注" Align="Center" Width="200" />--%>
                                </Columns>
                            </ColumnModel>
                            <View>
                                <ext:GridView runat="server" ID="GridViewCenter">
                                    <GetRowClass Fn="SetRowClass" />
                                </ext:GridView>
                            </View>
                            <BottomBar>
                                <ext:PagingToolbar ID="pageToolbar1" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="progressBarPager1" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>
                        </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
