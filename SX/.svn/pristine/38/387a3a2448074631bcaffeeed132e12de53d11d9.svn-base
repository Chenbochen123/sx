<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetUserRole.aspx.cs" Inherits="Manager_System_UserRole_SetUserRole" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>角色设置</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/parseURL.js"></script>
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.store.currentPage = 1;
            App.store.reload();
            return false;
        }
    </script>

    <!--特殊-->
    <script type="text/javascript">
        var setUserRoleNode = function (nodes, userid) {
            for (var i = 0; i < nodes.length; i++) {
                var node = nodes[i];
                App.direct.setUserRole(node.ObjID, userid, {
                    success: function (result) {

                    },
                    failure: function (errorMsg) {
                        Ext.Msg.alert('错误', errorMsg);
                    }
                });
            }
        }
        var deleteUserRoleNode = function (nodes, userid) {
            for (var i = 0; i < nodes.length; i++) {
                var node = nodes[i];
                App.direct.deleteUserRole(node.ObjID, userid, {
                    success: function (result) {

                    },
                    failure: function (errorMsg) {
                        Ext.Msg.alert('错误', errorMsg);
                    }
                });
            }
        }

        var setUserRoleNode_direct = function (btn) {
            if (btn != "yes") {
                return;
            }
            var url = parseURL(window.location);
            var userid = url.params.userid;
            var grid = App.GridPanel1;
            setUserRoleNode(grid.getRowsValues({ selectedOnly: true }), userid);
            App.direct.setUserRoleSuccess("", {
                success: function (result) {
                    Ext.Msg.alert('成功提示', "角色设置成功！");
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
        }

        var deleteUserRoleNode_direct = function (btn) {
            if (btn != "yes") {
                return;
            }
            var url = parseURL(window.location);
            var userid = url.params.userid;
            var grid = App.GridPanel2;
            deleteUserRoleNode(grid.getRowsValues({ selectedOnly: true }), userid);
            App.direct.setUserRoleSuccess("", {
                success: function (result) {
                    Ext.Msg.alert('成功提示', "角色设置成功！");
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
        }

        var setUserRoleGrid = function () {
            var url = parseURL(window.location);
            var userid = url.params.userid;
            if (userid == undefined) {
                Ext.Msg.alert('提示', "请选择用户！");
                return false;
            }
            if (userid.length == 0) {
                Ext.Msg.alert('提示', "请选择用户！");
                return false;
            }
            Ext.Msg.confirm("提示", '您确定添加当前用户的角色吗？', function (btn) { setUserRoleNode_direct(btn) });
            return false;
        }
        var deleteUserRoleGrid = function () {
            var url = parseURL(window.location);
            var userid = url.params.userid;
            if (userid == undefined) {
                Ext.Msg.alert('提示', "请选择用户！");
                return false;
            }
            if (userid.length == 0) {
                Ext.Msg.alert('提示', "请选择用户！");
                return false;
            }
            Ext.Msg.confirm("提示", '您确定删除当前用户的角色吗？', function (btn) { deleteUserRoleNode_direct(btn) });
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
                                <ext:Button runat="server" Icon="VcardEdit" Text="添加用户角色" ID="btn_userrole">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击添加用户角色" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="setUserRoleGrid" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                 <ext:Button runat="server" Icon="VcardEdit" Text="删除用户角色" ID="btn_deleteuserrole">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击删除用户角色" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="deleteUserRoleGrid" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel ID="panelNorthQuery" runat="server" AutoHeight="true">
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel1" runat="server" Region="Center">
                    <LayoutConfig>
                        <ext:HBoxLayoutConfig Align="Stretch" />
                    </LayoutConfig>
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" MultiSelect="true" Flex="1" TitleAlign="Center" Title="系统角色列表">
                            <Store>
                                <ext:Store ID="Store1" runat="server">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="RoleName" />
                                                <ext:ModelField Name="RecordTime" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:Column ID="RoleName1" DataIndex="RoleName" runat="server" Text="角色名称" Width="50" Flex="1" />
                                    <ext:Column ID="RecordTime1" DataIndex="RecordTime" runat="server" Text="角色添加时间" Width="150" Align="Center" />
                                </Columns>
                            </ColumnModel>
                             <SelectionModel>
                                <ext:CheckboxSelectionModel ID="RowSelectionModel1" runat="server" Mode="Simple" />
                            </SelectionModel>  
                            <View>
                                <ext:GridView ID="GridView1" runat="server">
                                    <Plugins>
                                        <ext:GridDragDrop ID="GridDragDrop1" runat="server" DragGroup="firstGridDDGroup" DropGroup="secondGridDDGroup" />
                                    </Plugins>
                                </ext:GridView>
                            </View>
                        </ext:GridPanel>
                        <ext:GridPanel ID="GridPanel2" runat="server" MultiSelect="true" Flex="1" TitleAlign="Center" Title="已有角色列表">
                            <Store>
                                <ext:Store ID="Store2" runat="server">
                                    <Model>
                                        <ext:Model ID="Model2" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="RoleName" />
                                                <ext:ModelField Name="RecordTime" />
                                                <ext:ModelField Name="SeqIdx" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:Column ID="RoleName2" DataIndex="RoleName" runat="server" Text="角色名称" Width="50" Flex="1" />
                                    <ext:Column ID="RecordTime2" DataIndex="RecordTime" runat="server" Text="角色添加时间" Width="150" Align="Center" />
                                </Columns>
                            </ColumnModel>
                             <SelectionModel>
                                <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Simple" />
                            </SelectionModel>  
                            <View>
                                <ext:GridView ID="GridView2" runat="server">
                                    <Plugins>
                                        <ext:GridDragDrop ID="GridDragDrop2" runat="server" DragGroup="secondGridDDGroup" DropGroup="firstGridDDGroup" />
                                    </Plugins>
                                </ext:GridView>
                            </View>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
