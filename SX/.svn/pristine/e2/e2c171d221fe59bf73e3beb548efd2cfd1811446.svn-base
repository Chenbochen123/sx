<%@ page language="C#" autoeventwireup="true" inherits="Manager_System_SetRoleAction_SetUserOneRole, App_Web_5whikgut" %>

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

        var closeParent = function () {
            parent.Manager_System_SetRoleAction_SetUserOneRole_Request();
            parent.App.Manager_System_SetRoleAction_SetUserOneRole_Window.close();
            return false;
        }
        var doSetRole = function (btn ,users) {
            if (btn != "yes") {
                return;
            }
            before();
            App.direct.ResetRoleAction(users, {
                success: function (result) {
                    if (result.length == 0) {
                        Ext.Msg.alert('成功', "用户角色设置成功！", function (btn) { closeParent() });
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
            var items = Ext.encode(App.SetUserPnl.getRowsValues());
            Ext.Msg.confirm("提示", '您确定设置选择用户的角色吗？', function (btn) { doSetRole(btn, items) });
            return false;
        }
        var onGridPanel1Render = function (store) {
            var records = store.data.items;
            var arr = [];
            for (var i = 0; i < records.length; i++) {
                var record = records[i];
                if (record.data.IsEmployee == "1") {
                    arr.push(record);
                }
            }
            App.CheckboxSelectionModel1.select(arr);
        }

        var allUserPnlListFresh = function () {
            App.AllUserStore.currentPage = 1;
            App.AllUserPageToolbar.doRefresh();
            return false;
        }

        var SetUserGridDrop = function (usercode) {
            var jsonStr = Ext.encode(App.SetUserPnl.getRowsValues());
            App.direct.set_user_power_drop(usercode, jsonStr, {
                success: function (result) {
                    Ext.Msg.notify('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.notify('操作', errorMsg);
                }
            });
        }
        //拖拽产生提示文字
        var getDragDropText = function () {
            var buf = [];

            buf.push("<ul>");

            Ext.each(this.view.panel.getSelectionModel().getSelection(), function (record) {
                buf.push("<li>" + record.data.Name + "</li>");
            });

            buf.push("</ul>");

            return buf.join("");
        };

    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FormLayout">
            <Items>
                <ext:Panel ID="northPanel" runat="server" Region="North"  Layout="BorderLayout" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="northToolbar">
                            <Items>
                                <ext:TextField ID="txtUserName" runat="server" FieldLabel="用户姓名" LabelAlign="Left" />
                                <ext:TextField ID="txtHRCode" runat="server" FieldLabel="HR代码" LabelAlign="Left" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip4" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="allUserPnlListFresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarFill ID="toolbarFill_begin" />
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="FolderWrench" Text="设置角色" ID="Button1">
                                    <Listeners>
                                        <Click Fn="setRole" />
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                </ext:Panel>
                <ext:Panel ID="Panel1" runat="server" Height="400">
                    <LayoutConfig>
                        <ext:HBoxLayoutConfig Align="Stretch" Padding="5" />
                    </LayoutConfig>
                    <Items>
                        <ext:GridPanel ID="AllUserPnl"  runat="server"  MultiSelect="true" Title="所有用户列表" TitleAlign="Center" Flex="1" Margins="0 2 0 0">
                            <Store>
                                <ext:Store ID="AllUserStore" runat="server" PageSize="10">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindUserData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="Model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="UserName" />
                                                <ext:ModelField Name="WorkBarcode" />
                                                <ext:ModelField Name="HRCode" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:Column ID="Column1" runat="server" Text="用户名称" Flex="1" Align="Center" DataIndex="UserName" />
                                    <ext:Column ID="Column2" runat="server" Text="用户工号" Flex="1" Align="Center" DataIndex="WorkBarcode" />
                                    <ext:Column ID="Column3" runat="server" Text="HR编号" Flex="1" Align="Center" DataIndex="HRCode" />
                                </Columns>
                            </ColumnModel>                    
                            <View>
                                <ext:GridView ID="GridView1" runat="server">
                                    <Plugins>
                                        <ext:GridDragDrop ID="GridDragDrop1" runat="server" DragGroup="firstGridDDGroup" DropGroup="secondGridDDGroup"/>
                                    </Plugins>
                                    <Listeners>
                                        <AfterRender Handler="this.plugins[0].dragZone.getDragText = getDragDropText;" />
                                        <Drop Handler="Ext.net.Notification.show({title:'提示', html:'删除用户： ' + data.records[0].get('UserName') + '成功'});" />
                                    </Listeners>
                                </ext:GridView>
                            </View>
                                <BottomBar>
                                <ext:PagingToolbar ID="AllUserPageToolbar" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="ProgressBarPager1" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>   
                        </ext:GridPanel>
                        <ext:GridPanel ID="SetUserPnl" runat="server" MultiSelect="true" Title="设置用户列表" TitleAlign="Center" Flex="1" Margins="0 0 0 3" >
                            <Store>
                                <ext:Store ID="SetUserStore" runat="server">
                                    <Model>
                                        <ext:Model ID="Model2" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="UserName" />
                                                <ext:ModelField Name="WorkBarcode" />
                                                <ext:ModelField Name="HRCode" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:Column ID="Column4" runat="server" Text="用户名称" Flex="1" Align="Center" DataIndex="UserName" />
                                    <ext:Column ID="Column5" runat="server" Text="用户工号" Flex="1" Align="Center" DataIndex="WorkBarcode" />
                                    <ext:Column ID="Column6" runat="server" Text="HR编码" Flex="1" Align="Center" DataIndex="HRCode" />
                                </Columns>
                            </ColumnModel>                   
                            <View>
                                <ext:GridView ID="GridView2" runat="server">
                                    <Plugins>
                                        <ext:GridDragDrop ID="GridDragDrop2" runat="server" DragGroup="secondGridDDGroup" DropGroup="firstGridDDGroup"/>
                                    </Plugins>
                                    <Listeners>
                                        <BeforeDrop Handler="SetUserGridDrop(data.records[0].get('WorkBarcode'))" />
                                        <AfterRender Handler="this.plugins[0].dragZone.getDragText = getDragDropText;" />
                                        <Drop Handler="var dropOn = overModel ? ' ' + dropPosition + ' ' + overModel.get('UserName') : ' on empty view';" />
                                    </Listeners>
                                </ext:GridView>
                            </View>   
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
                <ext:Hidden ID="hiddenHaveRole" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenDeptCode" runat="server"></ext:Hidden>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
