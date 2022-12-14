<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainFrame.aspx.cs" Inherits="Manager_MainFrame" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>密炼车间管控网络系统</title>
    <link rel="Shortcut Icon" type="image/x-icon" href="~/resources/images/logotiny.ico" media="screen" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <style type="text/css">
        *
        {
            margin: 0;
            padding: 0;
        }
        
        body
        {
            font-family: "微软雅黑" ,Verdana, Geneva, sans-serif;
            font-size: 12px;
            background: #fff;
        }
        
        .t
        {
            height: 50px;
            line-height: 50px;
            background: #f5f5f5;
            border-bottom: 1px solid #e5ecf0;
            overflow: hidden;
        }
        
        a
        {
            outline: none;
            color: #1a0202;
            text-decoration: none;
            cursor: auto;
        }
        
        a:-webkit-any-link
        {
            color: #1a0202;
            text-decoration: none;
            cursor: auto;
            outline: none;
        }
        
        .h
        {
            margin: 0 auto;
        }
        
        .logo
        {
            padding-left: 4px;
            float: left;
        }
        
        .logo img
        {
            position: relative;
            margin-top: -5px;
        }
        
        .head_menu
        {
            float: right;
            font-size: 12px;
            font-weight: 700;
            color: #787878;
            text-decoration: none;
            margin-right: 16px;
            font-family: "微软雅黑" ,Verdana, Geneva, sans-serif;
        }
        
        .head_menu:hover
        {
            text-decoration: underline;
        }
        
        .foot
        {
            background: #f5f5f5;
            line-height: 30px;
            text-align: center;
            font-family: "微软雅黑" ,Verdana, Geneva, sans-serif;
            font-weight: bold;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function nodeLoad(store, operation, options) {
            var node = operation.node;
            App.direct.NodeLoad(store.storeId, node.getId(), {
                success: function (result) {
                    node.set('loading', false);
                    node.set('loaded', true);
                    var data = Ext.decode(result);
                    node.appendChild(data, undefined, true);
                    node.expand();
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('加载错误', errorMsg);
                }
            });
            return false;
        };
        function onTabClose(item) {
            try {
                var window = item.body.dom.firstChild.contentWindow;
                if (window.onTabClose) {
                    var msg = window.onTabClose();
                    if (msg.length > 0) {
                        alert(msg);
                        return false;
                    }
                }
            } catch (ex) { }
            return true;
        }
        function onTabsRemove(tabs, item) {
            return onTabClose(item);
        }
        function addTab(id, title, url, closable) {
            if (!url) {
                return;
            }
            tabp = App.mainTabPanel;
            var tabid = "id=" + id;
            var tab = tabp.getComponent(tabid);
            if (!tab) {
                tab = tabp.add({
                    id: tabid,
                    title: title,
                    closable: closable,
                    /*html: "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>",*/
                    loader: {
                        url: url,
                        renderer: "frame",
                        loadMask: {
                            showMask: true,
                            msg: "正在加载 " + title + "..."
                        }
                    },
                    listeners: {
                        beforeclose: { fn: function (item) { return onTabClose(item) } }
                    },
                    autoScroll: false
                });
            }
            tabp.setActiveTab(tab);
        }
        function loadPage(record, e) {
            if (!record) {
                return;
            }
            if (!record.data.href) {
                return;
            }
            if (e) {
                e.stopEvent();
            }
            try {
                eval(record.data.href)
            } catch (e) {
                addTab(record.getId(), record.data.text, record.data.href, true);
            }
        };

    </script>
    <script language="javascript" type="text/javascript">
        var dologout = function (btn) {
            if (btn != "yes") {
                return;
            }
            var top = window;
            while (true) {
                if (top.parent) {
                    if (top == top.parent) {
                        break;
                    }
                    top = top.parent;
                } else {
                    break;
                }
            }
            top.top.location.href = "<%= Page.ResolveUrl("~/") %>Manager/Authentication/Logout.aspx";
        }
        function logout(sender) {
            Ext.Msg.confirm("提示", '您确定退出系统吗？', function (btn) { dologout(btn) });
            return false;
        }
        function refresh(sender) {
            var tab = App.mainTabPanel.getActiveTab();
            if (tab) {
                tab.reload(true);
                //var html = tab.getBody().dom.innerHTML;
                //tab.getBody().update(html);
            }
            return false;
        }

        function GetNode(nodes, nodeinfo) {
            for (var i = 0; i < nodes.length; i++) {
                var node = nodes[i];
                if (node.data.leaf) {
                    if ((node.data.text == nodeinfo) || (node.data.id == nodeinfo)) {
                        return node;
                    }
                } else {
                    var resultnode = GetNode(node.childNodes, nodeinfo);
                    if (resultnode) {
                        return resultnode;
                    }
                }

            }
        }

        function OpenMenu(menu) {
            menu = menu.replace("id=", "");
            loadPage(GetNode(App.mainTreePanel.getRootNode().childNodes, menu));
        }

        function help(sender) {
            var menu = sender.text;
            OpenMenu(menu)
            return false;
        }
        function user(sender) {
            App.Manager_System_SysUser_MyUser_Window.show();
            return false;
            var menu = sender.text;
            OpenMenu(menu)
            return false;
        }
        function clickMenu(sender) {
            var menu = sender.text;
            OpenMenu(menu)
            return false;
        }
    </script>
    <script language="javascript" type="text/javascript">
        Ext.create("Ext.window.Window", {
            id: "Manager_System_SysUser_MyUser_Window",
            hidden: true,
            width: 360,
            height: 300,
            html: "<iframe src='System/SysUser/MyUser.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "设置用户信息",
            modal: true
        })

        var OnTimer = function () {
            App.direct.OnTimer({
                success: function (result) {
                    App.StatusBar1.setText(result);
                },
                showFailureWarning: false
            });
        }
        var setTheme = function (color) {
            App.direct.GetThemeUrl(color, {
                success: function (result) {
                    //
                    Ext.net.ResourceMgr.setTheme(result);
                    App.mainTabPanel.items.each(function (el) {
                        if (!Ext.isEmpty(el.iframe)) {
                            if (el.getBody().Ext) {
                                el.getBody().Ext.net.ResourceMgr.setTheme(result, color.toLowerCase());
                            }
                        }
                    });
                },
                failture: function (result) { alert(result); }
            });
        }
        Ext.onReady(function () {
            var int = self.setInterval("OnTimer()", 50000)
            if (Ext.net.ResourceMgr.theme == "gray") document.getElementById('sp1').style.border = '2px solid'; else document.getElementById('sp2').style.border = '2px solid'
        })
    </script>
</head>
<body>
    
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="BorderPanelNorth" runat="server" Height="50" Region="North" Header="false"
                Border="false" ContentEl="head_div">
            </ext:Panel>
            <ext:Panel ID="BorderPanelWest" runat="server" Collapsible="true" Layout="accordion"
                Region="West" Split="true" Title="系统功能导航" Width="160" Icon="ApplicationLink">
                <BottomBar>
                    <ext:StatusBar ID="StatusBar1" runat="server" Height="24">
                    </ext:StatusBar>
                </BottomBar>
                <Items>
                    <ext:TreePanel ID="mainTreePanel" runat="server" Title="系统菜单" Icon="FolderGo" Width="240"
                        SingleExpand="false" RootVisible="false" Border="false" TagString="0">
                        <Store>
                            <ext:TreeStore ID="mainTreeStore" runat="server" OnReadData="GetUserPageGroup">
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
                            <BeforeLoad Fn="nodeLoad" />
                            <ItemClick Handler="loadPage(record,e)" />
                        </Listeners>
                    </ext:TreePanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="p_center" runat="server" Layout="Fit" Region="Center">
                <Items>
                    <ext:TabPanel ID="mainTabPanel" runat="server" Border="false" MinTabWidth="115">
                        <Plugins>
                            <ext:TabCloseMenu ID="TabCloseMenu1" runat="server" CloseTabText="关闭" CloseAllTabsText="关闭全部"
                                CloseOthersTabsText="关闭其他">
                            </ext:TabCloseMenu>
                        </Plugins>
                        <Listeners>
                            <BeforeRemove Fn="onTabsRemove">
                            </BeforeRemove>
                        </Listeners>
                    </ext:TabPanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="p_south" runat="server" Height="30" Region="South" Header="false"
                Border="false" ContentEl="foot_div">
            </ext:Panel>
        </Items>
    </ext:Viewport>
    <div id="head_div" class='t'>
        <div class='h'>
           <%-- <div class='logo'>
                <img alt="MESNAC.COM" src='<%= Page.ResolveUrl("~/") %>resources/images/login_logo.png' />
            </div>--%>
            <a style="float: right; cursor: default;"><em style="padding: 2px;"><span id="sp1"
                onclick="setTheme('Gray');this.style.border='2px solid';document.getElementById('sp2').style.border='1px solid'"
                style="background: #e5ecf0; width: 10px; height: 1px; border: 1px solid; font-size: 6px;">
                &nbsp;&nbsp;&nbsp; </span></em></a><a style="float: right;"><em style="padding: 2px;
                    cursor: default;"><span id="sp2" onclick="setTheme('Default');this.style.border='2px solid';document.getElementById('sp1').style.border='1px solid'"
                        style="background: #28ddf6; width: 10px; height: 1px; border: 1px solid; font-size: 6px;">
                        &nbsp;&nbsp;&nbsp; </span></em></a>
            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="head_menu" OnClientClick="logout(this);return false">退出系统</asp:LinkButton>
            <asp:LinkButton ID="LinkButton2" runat="server" CssClass="head_menu" OnClientClick="clickMenu(this);return false">系统帮助</asp:LinkButton>
            <asp:LinkButton ID="LinkButton3" runat="server" CssClass="head_menu" OnClientClick="user(this);return false">用户设置</asp:LinkButton>
            <asp:LinkButton ID="LinkButton4" runat="server" CssClass="head_menu" OnClientClick="refresh(this);return false">刷新选项卡</asp:LinkButton>
            <%-- <ext:ColorPicker ID="ColorPicker1" runat="server">
                   <Template ID="Template1" runat="server">
                         <Html>
				               <a class="head_menu">
                              
				                    <em style="padding:2px;">
				                        <span onclick="setTheme('Gray');" style="background:#00ff90; width:10px; height:10px; border:1px solid black;" unselectable="on">
				
				                        </span>
				                    </em>
				                </a>
                          <a class="head_menu" >
				                    <em style="padding:2px;">
				                        <span onclick="setTheme('Default');" style="background:#00ff90; width:10px; height:10px; border:1px solid black;" unselectable="on">
				
				                        </span>
				                    </em>
				                </a> 
				        </Html>
                     </Template>
                 </ext:ColorPicker>--%>
        </div>
    </div>
    <div id="foot_div" class='foot'>
      风神轮胎(太原)有限公司&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;V&nbsp;<%= Vision() %>&nbsp;&nbsp;&nbsp;&nbsp;在线人数：<%Response.Write(Application["count"]); %>
    </div>
    <%--<script type="text/javascript">
            Ext.onReady(function () {
                <%= AddTabDefault() %>
            })
    </script>--%>
    </form>
</body>
</html>