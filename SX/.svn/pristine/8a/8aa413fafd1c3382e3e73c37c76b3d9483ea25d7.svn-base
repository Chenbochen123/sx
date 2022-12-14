<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_BasicInfo_ShowPmtAction, App_Web_xrwstxsv" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>动作代码</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.store.currentPage = 1;
            App.store.reload();
            return false;
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
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
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
                        <ext:Panel ID="panelNorthQuery" runat="server" Layout="AnchorLayout" AutoHeight="true">
                            <Items>
                                <ext:Container ID="container1" runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:TextField ID="txtCode" runat="server" Flex="1" FieldLabel="动作代码" LabelAlign="Right" />
                                        <ext:TextField ID="txtShowName" runat="server" Flex="1" FieldLabel="动作名称" LabelAlign="Right" />
                                        <ext:TextField ID="txAddress" runat="server" Flex="1" FieldLabel="动作地址" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="gridPanelCenter" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="30">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="ActionCode" />
                                        <ext:ModelField Name="ShowName" />
                                        <ext:ModelField Name="ActionAddress" />
                                        <ext:ModelField Name="Remark" />
                                        <ext:ModelField Name="RecordTime" />
                                        <ext:ModelField Name="SeqIdx" />
                                        <ext:ModelField Name="DeleteFlag" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="columnModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="30" />
                            <ext:Column ID="ActionCode" DataIndex="ActionCode" runat="server" Text="动作代码" Width="120" />
                            <ext:Column ID="ShowName" DataIndex="ShowName" runat="server" Text="动作名称" Width="160" />
                            <ext:Column ID="ActionAddress" DataIndex="ActionAddress" runat="server" Text="动作地址" Width="120" />
                            <ext:Column ID="Remark" DataIndex="Remark" runat="server" Text="动作备注" Flex="1" />
                        </Columns>
                    </ColumnModel>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolbar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="progressBarPager" runat="server" />
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
