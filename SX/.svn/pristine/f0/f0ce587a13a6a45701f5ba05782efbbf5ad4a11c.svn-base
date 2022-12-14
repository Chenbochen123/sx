<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginLogQuery.aspx.cs" Inherits="Manager_System_SysLog_LoginLogQuery" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>日志信息</title>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
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

    <!--特殊-->
    <script type="text/javascript">
        var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {
            App.txtUserName.setValue(record.data.UserName);
            App.txtUserRealName.setValue(record.data.RealName);
        }
        var SelectUser = function () {
            App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.showOnParentShow = true;
            App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show(this);
        }

        var Manager_BasicInfo_CommonPage_QueryPageMenu_Request = function (record) {
            App.txtPageName.setValue(record.data.ShowName);
        }
        var SelectMenuPage = function () {
            App.Manager_BasicInfo_CommonPage_QueryPageMenu_Window.show(this);
        }

        //确认返回
        var Manager_BasicInfo_CommonPage_QueryPageMethod_Request = function (record) {
            App.txtMethodName.setValue(record.data.ShowName);
        }
        //弹出window
        var SelectPageMethod = function () {
            App.Manager_BasicInfo_CommonPage_QueryPageMethod_Window.show(this);
        }
        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryBasUser_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='<%= Page.ResolveUrl("~/") %>Manager/BasicInfo/CommonPage/QueryBasUser.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择用户",
            modal: true
        })
        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryPageMenu_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='<%= Page.ResolveUrl("~/") %>Manager/BasicInfo/CommonPage/QueryPageMenu.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择模块名称",
            modal: true
        })
        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryPageMethod_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='<%= Page.ResolveUrl("~/") %>Manager/BasicInfo/CommonPage/QueryPageMethod.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择操作简述",
            modal: true
        })
    </script>

</head>
<body>
    <form id="form" runat="server">
        <div style="display: none">
            <asp:Button ID="btnExportSubmit" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        </div>
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
                                <ext:ToolbarSeparator ID="toolbarSeparator2" />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击将查询结果导出到Excel中" />
                                    </ToolTips>
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
                        <ext:Panel ID="panelNorthQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:Container ID="container_Query" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container1" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".33">
                                            <Items>
                                                <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="登录开始时间" LabelAlign="Right" />
                                                <ext:ComboBox ID="txtLoginState" runat="server" LabelAlign="Right" Flex="1" FieldLabel="当前状态" SelectOnTab="true" Editable="false" />

                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container2" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".33">
                                            <Items>
                                                <ext:DateField ID="txtEndTime" runat="server" FieldLabel="登录结束时间" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container3" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".33">
                                            <Items>
                                                <ext:TriggerField ID="txtUserName" runat="server" FieldLabel="用户名称" LabelAlign="Right">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectUser" />
                                                    </Listeners>
                                                </ext:TriggerField>

                                                <ext:TriggerField ID="txtUserRealName" runat="server" FieldLabel="用户姓名" LabelAlign="Right">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectUser" />
                                                    </Listeners>
                                                </ext:TriggerField>
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
                                        <ext:ModelField Name="UserName" />
                                        <ext:ModelField Name="RealName" />
                                        <ext:ModelField Name="WorkBarcode" />
                                        <ext:ModelField Name="LoginIP" />
                                        <ext:ModelField Name="LoginTime" Type="Date" />
                                        <ext:ModelField Name="LogoutIP" />
                                        <ext:ModelField Name="LogoutTime" Type="Date" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="columnModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="30" />
                            <ext:Column ID="UserName" DataIndex="UserName" runat="server" Text="用户名称" Width="80" />
                            <ext:Column ID="RealName" DataIndex="RealName" runat="server" Text="用户姓名" Width="80" />
                            <ext:Column ID="PageName" DataIndex="PageName" runat="server" Text="功能名称" Width="100" />
                            <ext:Column ID="MethodName" DataIndex="LoginIP" runat="server" Text="登录IP" Width="100" />
                            <ext:DateColumn ID="DateColumn1" DataIndex="LoginTime" runat="server" Text="登录时间" Width="160" Align="Center" Format="yyyy-MM-dd HH:mm:ss" />
                            <ext:Column ID="Column1" DataIndex="LogoutIP" runat="server" Text="退出IP" Width="100" />
                            <ext:DateColumn ID="DateColumn2" DataIndex="LogoutTime" runat="server" Text="退出时间" Width="160" Align="Center" Format="yyyy-MM-dd HH:mm:ss" />
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
