<%@ page language="C#" autoeventwireup="true" inherits="Manager_BasicInfo_CommonPage_QueryRubberTyrePart, App_Web_rgel41dj" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>轮胎部件信息</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.store.currentPage = 1;
            App.pageToolbar.doRefresh();
            return false;
        }
    </script>

    <!--特殊-->
    <script type="text/javascript">
        var response = function (command, record) {
            parent.Manager_BasicInfo_CommonPage_QueryRubberTyrePart_Request(record);
            parent.App.Manager_BasicInfo_CommonPage_QueryRubberTyrePart_Window.close();
            return false;
        }
        var commandColumn_click = function (command, record) {
            return response(command, record);
        };
        //grid:panelgrid   td
        var cellDblClick = function (grid, td, tdindex, record, tr, trindex, e, fn) {
            return response('dblclick', record);
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <ext:ResourceManager ID="resourceManager" runat="server" />
        <ext:Viewport ID="viewport" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="northPanel" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="northToolbar">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="gridPanelRefresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel ID="panelNorthQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:Container ID="container_Query" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container1" runat="server" Layout="FormLayout" Padding="5" ColumnWidth="1">
                                            <Items>
                                                <ext:TextField ID="txt_tyre_part_name" runat="server" FieldLabel="部件名称" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="gridPanelCenter" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="10">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="TyrePartName" />
                                        <ext:ModelField Name="Remark" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="columnModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="30" />
                            <ext:Column ID="obj_id" runat="server" Text="部件编号" DataIndex="ObjID" Width="70"  />
                            <ext:Column ID="tyre_part_name" runat="server" Text="部件名称" DataIndex="TyrePartName" Width="90"  />
                            <ext:CommandColumn ID="commandColumn" runat="server" Width="60" Text="确认" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="accept" CommandName="Select" Text="确认">
                                        <ToolTip Text="确认使用本条数据" />
                                    </ext:GridCommand>
                                </Commands>
                                <PrepareToolbar />
                                <Listeners>
                                    <Command Handler="return commandColumn_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <Listeners>
                        <CellDblClick Fn="cellDblClick" />
                    </Listeners>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolbar" runat="server">
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>