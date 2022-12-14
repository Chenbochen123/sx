<%@ page language="C#" autoeventwireup="true" inherits="Manager_System_SetRoleAction_SetOneRoleAction, App_Web_5whikgut" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>设置单角色权限</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>

    <script type="text/javascript">        //树节点选中
        var setParentNode = function (node, checked) {
            var pNode = node;
            while (checked) {
                pNode = pNode.parentNode;
                if (!pNode) {
                    break;
                }
                pNode.set("checked", checked);
            }
        }
        var setChildNode = function (node, checked) {
            var nodes = node.childNodes;
            for (var i = 0; i < nodes.length; i++) {
                var node = nodes[i];
                node.set("checked", checked);
                if (node.data.leaf) {
                    continue;
                }
                setChildNode(node, node.data.checked);
            }
        }
        var onTreeCheckChange = function (node, checked, fn) {
            setParentNode(node, checked);
            setChildNode(node, checked);
        }
    </script>
    <script type="text/javascript">
        var after = function () {
            App.waitProgressWindow.close();
        }
        var before = function () {
            App.waitProgressWindow.show();
        }
        var treePanelDept = function (store, operation, options) {
            var node = operation.node;
            var nodeid = node.getId() || "";
            App.direct.IniDeptTree(nodeid, {
                success: function (result) {
                    node.set('loading', false);
                    node.set('loaded', true);
                    for (var i = 0; i < result.children.length; i++) {
                        var data = result.children[i];
                        node.appendChild(data, undefined, true);
                    }
                    node.expand();
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
            return false;
        };

        var getTreeSelectionData = function (node) {
            if (node.data.leaf) {
                return node.data.ObjID;
            }
            return "";
        }
        var getTreeSelectionModel = function (nodes) {
            var Result = "|";
            for (var i = 0; i < nodes.length; i++) {
                var node = nodes[i];
                if (node.data.checked) {
                    Result = Result + getTreeSelectionData(node) + "|";
                }
                try {
                    if (node.childNodes) {
                        Result = Result + getTreeSelectionModel(node.childNodes) + "|";
                    }
                } catch (e) { }
            }
            return Result;
        }
        var doSetRole = function (btn, roles, actions) {
            if (btn != "yes") {
                return;
            }
            before();
            App.direct.ResetRoleAction(roles, actions, {
                success: function (result) {
                    if (result.length == 0) {
                        Ext.Msg.alert('成功', "角色权限设置成功！");
                    }
                    else {
                        Ext.Msg.alert('提示', result);
                    }
                    after();
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('错误', errorMsg);
                    after();
                }
            });
            return false;
        }
        var stringReplace = function (sss, ss, s) {
            var str = sss;
            while (true) {
                if (str.indexOf(ss) < 0) {
                    return str;
                }
                str = str.replace(ss, s);
            }
        }
        var setRole = function () {
            var actions = getTreeSelectionModel(App.TreePanel1.getRootNode().childNodes);
            var items = App.GridPanel1.getSelectionModel().selected.items;
            var roles = "|";
            for (var i = 0; i < items.length; i++) {
                roles = roles + items[i].data.ObjID + "|";
            }
            if ((!actions) || stringReplace(actions, "|", "").length == 0) {
                Ext.Msg.alert('提示', "请选择权限！");
                return false;
            }
            if ((!roles) || stringReplace(roles, "|", "").length == 0) {
                Ext.Msg.alert('提示', "请选择角色！");
                return false;
            }
            Ext.Msg.confirm("提示", '您确定设置选择角色的权限吗？', function (btn) { doSetRole(btn, roles, actions) });
            return false;
        }


        var setParentNode = function (node, checked) {
            var pNode = node;
            while (checked) {
                pNode = pNode.parentNode;
                if (!pNode) {
                    break;
                }
                pNode.set("checked", checked);
            }
        }
        var setNode = function (result, node) {
            if (!node.data.leaf) {
                node.set("checked", false);
                for (var i = 0; i < node.childNodes.length; i++) {
                    setNode(result, node.childNodes[i]);
                }
                return;
            }
            if (result.indexOf("|" + node.data.ObjID + "|") >= 0) {
                node.set("checked", true);
                setParentNode(node, true);
            }
            else {
                node.set("checked", false);
            }
        }
        var roleItemClick = function (view, record, node, index, event, fn) {
            var role = record.data.ObjID || "";
            App.direct.GetActionInfo(role, {
                success: function (result) {
                    App.TreePanel1.setTitle("权限信息[" + record.data.RoleName + "]");
                    setNode(result, App.TreePanel1.getRootNode());
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
            return false;
        }
    </script>

    <!--使用角色拷贝权限-->
    <script type="text/javascript">
        var Manager_BasicInfo_CommonPage_QuerySysRole_Request = function (role) {
            var items = App.GridPanel1.getSelectionModel().selected.items;
            var roleid = items[0].data.ObjID;
            before();
            App.direct.SetRoleActionByOther(roleid, role.data.ObjID, {
                success: function (result) {
                    after();
                    Ext.Msg.alert('成功', "权限设置成功！");
                },
                failure: function (errorMsg) {
                    after();
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
        }
        var showWindow = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.Manager_BasicInfo_CommonPage_QuerySysRole_Window.show();
        }
        var setRoleActionByOther = function () {
            var items = App.GridPanel1.getSelectionModel().selected.items;
            if (items.length <= 0) {
                Ext.Msg.alert('提示', "请选择角色！");
                return false;
            }
            var roleid = items[0].data.ObjID;
            if (roleid == undefined) {
                Ext.Msg.alert('提示', "请选择角色！");
                return false;
            }
            if (roleid.length == 0) {
                Ext.Msg.alert('提示', "请选择角色！");
                return false;
            }
            Ext.Msg.confirm("提示", '您确定复制当前角色的权限吗？', function (btn) { showWindow(btn) });
            return false;
        }
        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QuerySysRole_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QuerySysRole.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            bodyPadding: 5,
            closable: true,
            title: "请选择角色",
            modal: true
        })
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel4" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="toolbar1">
                            <Items>
                                <ext:ToolbarFill ID="toolbarFill_begin" />
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="FolderWrench" Text="设置角色权限" ID="btnSetRole">
                                    <Listeners>
                                        <Click Fn="setRole"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator1" />
                                <ext:Button runat="server" Icon="UserBrown" Text="当前角色权限复制到" ID="btnRoleCopy">
                                    <Listeners>
                                        <Click Fn="setRoleActionByOther" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" Flex="1" Region="Center" Collapsible="false" MultiSelect="false" FolderSort="true"
                            Title="角色信息" TitleAlign="Center">
                            <Store>
                                <ext:Store ID="store" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model1" runat="server" IDProperty="EquipCode">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="RoleName" />
                                                <ext:ModelField Name="Remark" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel" runat="server">
                                <Columns>
                                    <ext:Column ID="ObjID" DataIndex="ObjID" runat="server" Text="角色编码" Width="80" />
                                    <ext:Column ID="RoleName" DataIndex="RoleName" runat="server" Text="角色名称" Flex="1" />
                                    <ext:Column ID="Column1" DataIndex="Remark" runat="server" Text="备注" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <ItemClick Fn="roleItemClick"></ItemClick>
                            </Listeners>
                        </ext:GridPanel>
                        <ext:TreePanel ID="TreePanel1" runat="server" Flex="1" Region="East" Collapsible="false" RootVisible="false" MultiSelect="true"
                            Title="权限信息" TitleAlign="Center">
                            <Store>
                                <ext:TreeStore ID="TreeStore2" runat="server">
                                    <Model>
                                        <ext:Model ID="model2" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="ShowName" />
                                                <ext:ModelField Name="Remark" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Root>
                                        <ext:Node NodeID="Root" />
                                    </Root>
                                </ext:TreeStore>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:TreeColumn ID="ShowName" DataIndex="ShowName" runat="server" Sortable="false" Hideable="false" Text="权限名称" Width="200" />
                                    <ext:Column ID="Remark2" DataIndex="Remark" runat="server" Sortable="false" Hideable="false" Text="权限备注" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <CheckChange Fn="onTreeCheckChange" />
                            </Listeners>
                        </ext:TreePanel>
                    </Items>
                </ext:Panel>

            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
