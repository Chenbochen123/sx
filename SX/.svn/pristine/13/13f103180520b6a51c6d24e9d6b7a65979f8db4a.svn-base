<%@ page language="C#" autoeventwireup="true" inherits="Manager_System_SetRoleAction_SetDeptRole, App_Web_5whikgut" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>设置部门角色</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>

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
            return node.data.DepCode;
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
        var doSetRole = function (btn, depts, roles) {
            if (btn != "yes") {
                return;
            }
            before();
            App.direct.ResetRoleAction(depts, roles, {
                success: function (result) {
                    if (result.length == 0) {
                        Ext.Msg.alert('成功', "部门角色设置成功！");
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
            var depts = getTreeSelectionModel(App.TreePanel1.getRootNode().childNodes);
            var items = App.GridPanel1.getSelectionModel().selected.items;
            var roles = "|";
            for (var i = 0; i < items.length; i++) {
                roles = roles + items[i].data.ObjID + "|";
            }
            if ((!depts) || stringReplace(depts, "|", "").length == 0) {
                Ext.Msg.alert('提示', "请选择部门！");
                return false;
            }
            if ((!roles) || stringReplace(roles, "|", "").length == 0) {
                Ext.Msg.alert('提示', "请选择角色！");
                return false;
            }
            Ext.Msg.confirm("提示", '您确定设置选择部门的角色吗？', function (btn) { doSetRole(btn, depts, roles) });
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
                    <TopBar>
                        <ext:Toolbar runat="server" ID="toolbar1">
                            <Items>
                                <ext:ToolbarFill ID="toolbarFill_begin" />
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="FolderWrench" Text="设置部门角色" ID="btnSetRole">
                                    <Listeners>
                                        <Click Fn="setRole"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:TreePanel ID="TreePanel1" runat="server" Flex="1" Region="West" Collapsible="false" RootVisible="false" MultiSelect="true"
                            Title="部门信息" TitleAlign="Center">
                            <Store>
                                <ext:TreeStore ID="treeDeptStore" runat="server">
                                    <Model>
                                        <ext:Model ID="model" runat="server">
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
                        </ext:TreePanel>
                        <ext:GridPanel ID="GridPanel1" runat="server" Flex="1" Region="Center" Collapsible="false" MultiSelect="true" FolderSort="true"
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
                                    <ext:Column ID="Remark2" DataIndex="Remark" runat="server" Text="备注" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" />
                            </SelectionModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>

            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
