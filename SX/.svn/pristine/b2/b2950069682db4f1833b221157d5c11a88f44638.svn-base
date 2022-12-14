<%@ page language="C#" autoeventwireup="true" inherits="Manager_System_ShowRoleAction_ShowRoleAction, App_Web_pzumhttw" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>查看角色权限</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    
    <script type="text/javascript">        //树节点选中
        var onTreeCheckChange = function (node, checked, fn) {
            node.set("checked", !checked);
            return false;
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
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel4" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
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
                                        <ext:Node NodeID="Root"/>
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
