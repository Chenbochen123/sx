<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Manager_Technology_Comparison_Default" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>配方日志查询</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.store.currentPage = 1;
            App.store.reload();
            return false;
        }
    </script>

    <!--特殊-->

</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:TabPanel ID="Panel1" runat="server" Region="Center" ActiveIndex="0" DefaultBorder="false" AutoScroll="false" MinTabWidth="200">
                    <Items>
                        <ext:Panel ID="pnlMain" runat="server" Title="基本信息" Layout="BorderLayout">
                        </ext:Panel>
                        <ext:Panel ID="pnlWeight" runat="server" Title="称量信息" Layout="BorderLayout">
                        </ext:Panel>
                        <ext:Panel ID="pnlMixing" runat="server" Title="混炼信息" Layout="BorderLayout">
                        </ext:Panel>
                        <ext:Hidden ID="a" runat="server"></ext:Hidden>
                        <ext:Hidden ID="b" runat="server"></ext:Hidden>
                    </Items>
                </ext:TabPanel>
            </Items>
        </ext:Viewport>
    </form>
    <script type="text/javascript">
        var pnlMainOnShow = function () {
            var setHtml = function (pnl, html) {
                if (!pnl) {
                    return;
                }
                if (pnl.getBody()) {
                    pnl.getBody().update(html);
                } else {
                    pnl.html = html;
                }
            }
            var a = App.a.getValue();
            var b = App.b.getValue();
            var url = "?a=" + a + "&b=" + b;
            var html = "<iframe src='Main.aspx" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            setHtml(App.pnlMain, html);
            html = "<iframe src='Weight.aspx" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            setHtml(App.pnlWeight, html);
            html = "<iframe src='Mixing.aspx" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            setHtml(App.pnlMixing, html);
        }
        Ext.onReady(function () {
            pnlMainOnShow();
        });
    </script>
</body>
</html>
