<%@ page language="C#" autoeventwireup="true" inherits="Manager_System_ShowRoleAction_ShowUserRole, App_Web_pzumhttw" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>设置用户角色</title>
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
        var deptItemClick = function (view, record, node, index, event, fn) {
            App.hiddenDeptCode.setValue(record.data.DepCode);
            App.GridPanel1.setTitle("用户信息[" + record.data.DepName + "]");
            App.GridPanel1.store.currentPage = 1;
            App.GridPanel1.store.reload();
        }

        var setNode = function (result, store) {
            for (var i = 0; i < store.data.items.length; i++) {
                var record = store.data.items[i];
                if (result.indexOf("|" + record.data.ObjID + "|") >= 0) {
                    record.set("CheckColumn", true);
                }
                else {
                    record.set("CheckColumn", false);
                }
            }
        }
        var userItemClick = function (view, record, node, index, event, fn) {
            var depcode = record.data.WorkBarcode || "";
            App.direct.GetActionInfo(depcode, {
                success: function (result) {
                    App.GridPanel2.setTitle("角色信息[" + record.data.HRCode + "|" + record.data.UserName + "]");
                    setNode(result, App.GridPanel2.store);
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
                <ext:Hidden ID="hiddenDeptCode" runat="server"></ext:Hidden>
                <ext:Panel ID="Panel4" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <Items>
                        <ext:TreePanel ID="TreePanel1" runat="server" Flex="3" Region="West" Collapsible="false" RootVisible="false" MultiSelect="false"
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
                                    <ext:Column ID="UserHRCode" DataIndex="HRCode" runat="server" Text="用户编码" Width="80" />
                                    <ext:Column ID="UserName" DataIndex="UserName" runat="server" Text="用户名称" Flex="1" />
                                    <ext:Column ID="Column1" DataIndex="Remark" runat="server" Text="备注" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <ItemClick Fn="userItemClick"></ItemClick>
                            </Listeners>
                        </ext:GridPanel>
                       <ext:GridPanel ID="GridPanel2" runat="server" Flex="2" Region="East" Collapsible="false" MultiSelect="false" FolderSort="true"
                            Title="角色信息" TitleAlign="Center">
                            <Store>
                                <ext:Store ID="store1" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindDataRole" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model2" runat="server" IDProperty="EquipCode">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="RoleName" />
                                                <ext:ModelField Name="Remark" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel1" runat="server">
                                <Columns>
                                    <ext:CheckColumn ID="CheckColumn" DataIndex="CheckColumn" runat="server" Width="30" />
                                    <ext:Column ID="ObjID" DataIndex="ObjID" runat="server" Text="角色编码" Width="80" />
                                    <ext:Column ID="RoleName" DataIndex="RoleName" runat="server" Text="角色名称" Flex="1" />
                                    <ext:Column ID="Remark2" DataIndex="Remark" runat="server" Text="备注" Flex="1" />
                                </Columns>
                            </ColumnModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
