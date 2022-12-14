<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowDeptAction.aspx.cs" Inherits="Manager_System_ShowRoleAction_ShowDeptAction" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>查看部门权限</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>

    <script type="text/javascript">
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
        var deptItemClick = function (view, record, node, index, event, fn) {
            var depcode = record.data.DepCode || "";
            App.direct.GetActionInfo(depcode, {
                success: function (result) {
                    App.TreePanel2.setTitle("权限信息[" + record.data.DepName + "]");
                    setNode(result, App.TreePanel2.getRootNode());
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
                        <ext:TreePanel ID="TreePanel1" runat="server" Flex="1" Region="West" Collapsible="false" RootVisible="false" MultiSelect="false"
                            Title="部门信息" TitleAlign="Center">
                            <Store>
                                <ext:TreeStore ID="TreeStore1" runat="server">
                                    <Model>
                                        <ext:Model ID="model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="DepCode" />
                                                <ext:ModelField Name="DepName" />
                                                <ext:ModelField Name="HRCode" />
                                                <ext:ModelField Name="ParentNum" />
                                                <ext:ModelField Name="DeleteFlag" />
                                                <ext:ModelField Name="Remark" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Listeners>
                                        <BeforeLoad Fn="treePanelDept" />
                                    </Listeners>
                                </ext:TreeStore>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:TreeColumn ID="DepName" DataIndex="DepName" runat="server" Sortable="false" Hideable="false" Text="部门名称" Width="200" />
                                    <ext:Column ID="HRCode" DataIndex="HRCode" runat="server" Sortable="false" Hideable="false" Text="HR编号" Align="Center" Width="80" />
                                    <ext:Column ID="DepCode" DataIndex="DepCode" runat="server" Sortable="false" Hideable="false" Text="部门编号" Align="Center" Width="60" />
                                    <ext:Column ID="DeleteFlag" DataIndex="DeleteFlag" runat="server" Sortable="false" Hideable="false" Text="部门状态" Width="60" />
                                    <ext:Column ID="Remark" DataIndex="Remark" runat="server" Sortable="false" Hideable="false" Text="部门备注" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <ItemClick Fn="deptItemClick"></ItemClick>
                            </Listeners>
                        </ext:TreePanel>
                        <ext:TreePanel ID="TreePanel2" runat="server" Flex="1" Region="Center" Collapsible="false" RootVisible="false" MultiSelect="true"
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
