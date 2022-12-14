<%@ page language="C#" autoeventwireup="true" inherits="Manager_System_SetRoleAction_SetUserActionEqual, App_Web_5whikgut" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>设置等同权限用户</title>
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
            if (node.data.DepCode) {
                return node.data.DepCode;
            }
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
        var doSetRole = function (btn, user1s, user2s) {
            if (btn != "yes") {
                return;
            }
            before();
            App.direct.ResetRoleAction(user1s, user2s, {
                success: function (result) {
                    if (result.length == 0) {
                        Ext.Msg.alert('成功', "等同权限用户设置成功！");
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
            var items = App.GridPanel2.getSelectionModel().selected.items;
            var user1s = "|";
            for (var i = 0; i < items.length; i++) {
                user1s = user1s + items[i].data.WorkBarcode + "|";
            }
            items = App.GridPanel1.getSelectionModel().selected.items;
            var user2s = "|";
            for (var i = 0; i < items.length; i++) {
                user2s = user2s + items[i].data.WorkBarcode + "|";
            }
            if ((!user1s) || stringReplace(user1s, "|", "").length == 0) {
                Ext.Msg.alert('提示', "请选择用户！");
                return false;
            }
            if ((!user2s) || stringReplace(user2s, "|", "").length == 0) {
                Ext.Msg.alert('提示', "请选择等同用户！");
                return false;
            }
            Ext.Msg.confirm("提示", '您确定设置用户的角色吗？', function (btn) { doSetRole(btn, user1s, user2s) });
            return false;
        }
        var deptItemClick = function (view, record, node, index, event, fn) {
            App.hiddenDeptCode.setValue(record.data.DepCode);
            if (App.btnShow1.pressed) {
                App.GridPanel1.setTitle("用户信息[" + record.data.DepName + "]");
                App.GridPanel1.store.currentPage = 1;
                App.GridPanel1.store.reload();
            }
            if (App.btnShow2.pressed) {
                App.GridPanel2.setTitle("等同用户信息[" + record.data.DepName + "]");
                App.GridPanel2.store.currentPage = 1;
                App.GridPanel2.store.reload();
            }
        }

    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>

                <ext:Hidden ID="hiddenDeptCode" runat="server"></ext:Hidden>
                <ext:Panel ID="Panel4" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="toolbar1">
                            <Items>
                                <ext:ToolbarFill ID="toolbarFill_begin" />
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="FolderWrench" Text="设置等同权限用户" ID="btnSetRole">
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
                        <ext:TreePanel ID="TreePanel1" runat="server" Flex="3" Region="West" Collapsible="false" RootVisible="false" MultiSelect="false"
                            Title="部门信息" TitleAlign="Center">
                            <TopBar>
                                <ext:Toolbar runat="server" ID="Toolbar2">
                                    <Items>
                                        <ext:Button runat="server" Icon="FolderStar" Text="选择用户信息" ID="btnShow1" Pressed="true" EnableToggle="true" ToggleGroup="Group1">
                                        </ext:Button>
                                        <ext:Button runat="server" Icon="FolderMagnify" Text="选择等同用户信息" ID="btnShow2" Pressed="false" EnableToggle="true" ToggleGroup="Group1">
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
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
                        <ext:GridPanel ID="GridPanel1" runat="server" Flex="2" Region="Center" Collapsible="false" MultiSelect="false" FolderSort="true"
                            Title="用户信息" TitleAlign="Center">
                            <Store>
                                <ext:Store ID="store" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model3" runat="server" IDProperty="EquipCode">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="HRCode" />
                                                <ext:ModelField Name="UserName" />
                                                <ext:ModelField Name="WorkBarcode" />
                                                <ext:ModelField Name="Remark" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="30" />
                                    <ext:Column ID="UserHRCode" DataIndex="HRCode" runat="server" Text="用户编码" Width="80" />
                                    <ext:Column ID="UserName" DataIndex="UserName" runat="server" Text="用户名称" Flex="1" />
                                    <ext:Column ID="Column1" DataIndex="Remark" runat="server" Text="备注" Flex="1" />
                                </Columns>
                            </ColumnModel>
                        </ext:GridPanel>
                        <ext:GridPanel ID="GridPanel2" runat="server" Flex="2" Region="East" Collapsible="false" MultiSelect="true" FolderSort="true"
                            Title="等同用户信息" TitleAlign="Center">
                            <Store>
                                <ext:Store ID="store1" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model2" runat="server" IDProperty="EquipCode">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="HRCode" />
                                                <ext:ModelField Name="UserName" />
                                                <ext:ModelField Name="WorkBarcode" />
                                                <ext:ModelField Name="Remark" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel1" runat="server">
                                <Columns>
                                    <ext:Column ID="Column2" DataIndex="HRCode" runat="server" Text="用户编码" Width="80" />
                                    <ext:Column ID="Column3" DataIndex="UserName" runat="server" Text="用户名称" Flex="1" />
                                    <ext:Column ID="Column4" DataIndex="Remark" runat="server" Text="备注" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:CheckboxSelectionModel ID="CheckboxSelectionModel2" runat="server" Mode="Multi" />
                            </SelectionModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
