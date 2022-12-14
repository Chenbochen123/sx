<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManager.aspx.cs" Inherits="Manager_System_SysUser_UserManager" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>系统用户设置</title>
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
        var commandcolumn_direct_ClearPassword = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var id = record.data.WorkBarcode;
            App.direct.commandcolumn_direct_ClearPassword(id, {
                success: function (result) {
                    Ext.Msg.alert('设置成功', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('设置失败', errorMsg);
                }
            });
        }
        var commandcolumn_direct_ResetPassword = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var id = record.data.WorkBarcode;
            App.direct.commandcolumn_direct_ResetPassword(id, {
                success: function (result) {
                    Ext.Msg.alert('设置成功', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('设置失败', errorMsg);
                }
            });
        }

        Ext.create("Ext.window.Window", {
            id: "SetUserRole_Window",
            height: 460,
            hidden: true,
            width: 600,
            html: "<iframe src='<%= Page.ResolveUrl("~/") %>Manager/System/UserRole/SetUserRole.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            closeAction: "close",
            title: "设置用户的角色",
            modal: true
        })
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "ClearPassword".toLowerCase()) {
                Ext.Msg.confirm("提示", '您确定清 [' + record.data.UserName + '] 的密码吗？<br/><br/>清空密码后，用户将不能再登录！',
                    function (btn) { commandcolumn_direct_ClearPassword(btn, record) });
            }
            if (command.toLowerCase() == "ResetPassword".toLowerCase()) {
                Ext.Msg.confirm("提示", '您确定将 [' + record.data.UserName + '] 密码初始化吗？', function (btn) { commandcolumn_direct_ResetPassword(btn, record) });
            }
            if (command.toLowerCase() == "SetUserRole".toLowerCase()) {
                App.SetUserRole_Window.setTitle("设置用户 [" + record.data.UserName + "] 的角色");
                var id = record.data.WorkBarcode;
                var html = "<iframe src='<%= Page.ResolveUrl("~/") %>Manager/System/UserRole/SetUserRole.aspx?userid=" + id + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
                if (App.SetUserRole_Window.getBody()) {
                    App.SetUserRole_Window.getBody().update(html);
                } else {
                    App.SetUserRole_Window.html = html;
                }
                App.SetUserRole_Window.show();
            }
            return false;
        };
        var commandcolumn_click = function (command, record) {
            commandcolumn_click_confirm(command, record);
            return false;
        };
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
                        <ext:Panel ID="panelNorthQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:Container ID="container_Query" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container2" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".3">
                                            <Items>
                                                <ext:TextField ID="txtUserName" runat="server" FieldLabel="用户编号" LabelAlign="Left" LabelPad="-30"/>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container3" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".3">
                                            <Items>
                                                <ext:TextField ID="txtUserRealName" runat="server" FieldLabel="用户姓名" LabelAlign="Left" LabelPad="-30"/>
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
                                        <ext:ModelField Name="UserPWD" />
                                        <ext:ModelField Name="RealName" />
                                        <ext:ModelField Name="Sex" />
                                        <ext:ModelField Name="Telephone" />
                                        <ext:ModelField Name="HRCode" />
                                        <ext:ModelField Name="WorkBarcode" />
                                        <ext:ModelField Name="DeptID" />
                                        <ext:ModelField Name="WorkID" />
                                        <ext:ModelField Name="ShiftID" />
                                        <ext:ModelField Name="WorkShopID" />
                                        <ext:ModelField Name="Remark" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="columnModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="30" />
                            <ext:Column ID="HRCode" DataIndex="HRCode" runat="server" Text="HR代码" Width="80" />
                            <ext:Column ID="UserName" DataIndex="UserName" runat="server" Text="用户名称" Width="60" />
                            <ext:Column ID="RealName" DataIndex="RealName" runat="server" Text="真实姓名" Width="60" />
                            <ext:Column ID="Sex" DataIndex="Sex" runat="server" Text="性别" Width="40" />
                            <ext:Column ID="Telephone" DataIndex="Telephone" runat="server" Text="手机号码" Width="100" />
                            <ext:Column ID="DepartID" DataIndex="DepartID" runat="server" Text="所属部门" Width="80" />
                            <ext:Column ID="WorkID" DataIndex="WorkID" runat="server" Text="所属岗位" Width="80" />
                            <ext:Column ID="ShiftID" DataIndex="ShiftID" runat="server" Text="所属班组" Width="80" />
                            <ext:Column ID="WorkShopID" DataIndex="WorkShopID" runat="server" Text="所属车间" Width="80" />
                            <ext:Column ID="remark" DataIndex="Remark" runat="server" Text="备注" Flex="1" />
                            <ext:CommandColumn ID="commandColumn" runat="server" Width="90" Text="设置" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="UserEdit" Text="用户设置">
                                        <Menu EnableScrolling="true">
                                            <Items>
                                                <ext:MenuCommand Icon="VcardEdit" Text="设置用户角色" CommandName="SetUserRole" />
                                                <ext:MenuCommand Icon="UserKey" Text="初始化密码" CommandName="ResetPassword" />
                                                <ext:MenuCommand Icon="UserKey" Text="清除密码" CommandName="ClearPassword" />
                                            </Items>
                                        </Menu>
                                    </ext:GridCommand>
                                </Commands>
                                <Listeners>
                                    <Command Handler="return commandcolumn_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolbar" runat="server">
                                 <Items>
                                    <ext:Label ID="Label2" runat="server" Text="每页条数:" />
                                    <ext:ToolbarSpacer ID="ToolbarSpacer2" runat="server" Width="10" />
                                    <ext:ComboBox ID="ComboBox2" runat="server" Width="80">
                                        <Items>
                                            <ext:ListItem Text="10" />
                                            <ext:ListItem Text="50" />
                                            <ext:ListItem Text="100" />
                                            <ext:ListItem Text="200" />
                                        </Items>
                                        <SelectedItems>
                                            <ext:ListItem Value="10" />
                                        </SelectedItems>
                                        <Listeners>
                                            <Select Handler="#{gridPanelCenter}.store.pageSize = parseInt(this.getValue(), 10); #{pageToolbar}.doRefresh(); return false;" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
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
