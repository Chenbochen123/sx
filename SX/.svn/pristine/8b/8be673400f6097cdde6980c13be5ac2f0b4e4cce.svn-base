<%@ page language="C#" autoeventwireup="true" inherits="Manager_System_UserAction_ShowUserAction, App_Web_maba3i5c" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>权限对用户查看</title>
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
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script type="text/javascript">
        var treePanelActionNodeLoad = function (store, operation, options) {
            var node = operation.node;
            var nodeid = node.getId() || "";

            App.direct.treePanelActionNodeLoad(nodeid, {
                success: function (result) {
                    node.set('loading', false);
                    node.set('loaded', true);
                    var data = Ext.decode(result);
                    node.appendChild(data, undefined, true);
                    node.expand();
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
            return false;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Region="West" Title="系统中的权限列表" Width="400" Layout="BorderLayout">
                    <BottomBar>
                        <ext:StatusBar ID="StatusBar1" runat="server" Height="24" Text=""></ext:StatusBar>
                    </BottomBar>
                    <Items>
                        <ext:Panel ID="Panel3" runat="server" Region="Center" Layout="AccordionLayout">
                            <Items>
                                <ext:TreePanel ID="treePanelUser" runat="server" Title="系统中的权限列表" Icon="FolderGo" AutoHeight="true" RootVisible="false">
                                    <Store>
                                        <ext:TreeStore ID="treeStoreUser" runat="server">
                                            <Proxy>
                                                <ext:PageProxy>
                                                    <RequestConfig Method="GET" Type="Load" />
                                                </ext:PageProxy>
                                            </Proxy>
                                            <Root>
                                                <ext:Node NodeID="Root" Expanded="true" />
                                            </Root>
                                        </ext:TreeStore>
                                    </Store>
                                    <Listeners>
                                        <BeforeLoad Fn="treePanelActionNodeLoad" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Select OnEvent="GetActinUserGrid" />
                                    </DirectEvents>
                                </ext:TreePanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="GridPanel1" runat="server" Region="Center" Title="权限对应的用户列表" Layout="BorderLayout">
                    <Items>
                        <ext:Panel ID="Panel20" runat="server" Region="North" AutoHeight="true">
                            <Items>
                                <ext:Container ID="container1" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container2" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".45">
                                            <Items>
                                                <ext:TextField ID="txtActionID" runat="server" FieldLabel="ActionID" LabelAlign="Right" ReadOnly="true" Hidden="true" />
                                                <ext:TextField ID="txtPageParName" runat="server" FieldLabel="功能类型" LabelAlign="Right" ReadOnly="true" />
                                                <ext:TextField ID="txtActionName" runat="server" FieldLabel="操作简述" LabelAlign="Right" ReadOnly="true" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container3" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".45">
                                            <Items>
                                                <ext:TextField ID="txtPageName" runat="server" FieldLabel="功能页面" LabelAlign="Right" ReadOnly="true" />
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
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
                                                <ext:ModelField Name="WorkBarcode" />
                                                <ext:ModelField Name="DeptID" />
                                                <ext:ModelField Name="WorkID" />
                                                <ext:ModelField Name="ShiftID" />
                                                <ext:ModelField Name="WorkShopID" />
                                                <ext:ModelField Name="DeleteFlag" />
                                                <ext:ModelField Name="Remark" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="30" />
                                    <ext:Column ID="UserName" DataIndex="UserName" runat="server" Text="用户名" Width="120" />
                                    <ext:Column ID="RealName" DataIndex="RealName" runat="server" Text="用户姓名" Flex="1" />
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
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
