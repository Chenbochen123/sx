<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryEqmFaultReason.aspx.cs" Inherits="Manager_BasicInfo_CommonPage_QueryEqmFaultReason" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>故障原因</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.store.currentPage = 1;
            App.pageToolbar.doRefresh();
            return false;
        }
        //点击弹出窗口，height=100%设置，google浏览器无法获取其高度方法
        window.onload = function () {
            if (parent && parent.document) {
                var iframes = parent.document.getElementsByTagName("iframe");
                if (iframes) {
                    for (var i = 0; i < iframes.length; i++) {
                        if (iframes[i].contentWindow == window) {
                            iframes[i].height = iframes[i].parentElement.style.height;
                        }
                    }
                }
            }
        };
    </script>

    <!--特殊-->
    <script type="text/javascript">
        var btnOK_Click = function () {
            App.direct.getFaultReason({
                success: function (result) {
                    parent.Manager_BasicInfo_CommonPage_QueryEqmFaultReason_Request(result);
                    parent.App.Manager_BasicInfo_CommonPage_QueryEqmFaultReason_Window.close();
                },
                failture: function () {
                    parent.App.Manager_BasicInfo_CommonPage_QueryEqmFaultReason_Window.close();
                }
            });
            return false;
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <ext:ResourceManager ID="resourceManager" runat="server" />
        <ext:Viewport ID="viewport" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="northPanel" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="northToolbar">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="gridPanelRefresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                                <ext:ToolbarSeparator ID="toolbarSeparator1" />
                                <ext:Button runat="server" Icon="Accept" Text="确定" ID="btnOK">
                                    <Listeners>
                                        <Click Fn="btnOK_Click"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator2" />
                                <ext:Button runat="server" Icon="Cancel" Text="取消" ID="btnCancel">
                                    <Listeners>
                                        <Click Handler="parent.App.Manager_BasicInfo_CommonPage_QueryEqmFaultReason_Window.close();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator3" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel ID="panelNorthQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:Container ID="container_Query" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container1" runat="server" Layout="FormLayout" Padding="5" ColumnWidth="1">
                                            <Items>
                                                <ext:TextField ID="txtReasonName" runat="server" FieldLabel="故障原因" LabelAlign="Right" >
                                                    <Listeners>
                                                        <Change Fn="gridPanelRefresh"></Change>
                                                    </Listeners>
                                                </ext:TextField>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="gridPanelCenter" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="10">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server" IDProperty="ObjID">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="ReasonName" />
                                        <ext:ModelField Name="DealDesc" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="columnModel" runat="server">
                        <Columns>
                            <ext:Column ID="obj_id" runat="server" Text="编号" DataIndex="ObjID" Width="40"  />
                            <ext:Column ID="reason_name" runat="server" Text="故障原因名称" DataIndex="ReasonName" Width="150"  />
                            <ext:Column ID="deal_desc" runat="server" Text="处理措施" DataIndex="DealDesc" Width="150"  />
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" />
                    </SelectionModel>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolbar" runat="server">
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>

