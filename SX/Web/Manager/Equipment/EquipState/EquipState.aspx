<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipState.aspx.cs" Inherits="Manager_Equipment_EquipState_EquipState" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>生产状况展示</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="css/data-view.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript">
        var treeClick = function (record) {
            App.store.currentPage = 1;
            App.direct.EquipStoreReload(record.getId(), {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }
        $(document).ready(function () {
            setInterval(function () {
                App.store.currentPage = 1;
                App.direct.EquipStoreReload('', {
                    success: function (result) {
                    },

                    failure: function (errorMsg) {
                        Ext.Msg.alert('操作', errorMsg);
                    }
                });
            }, 15000);
        });
    </script>
</head>
<body>
    <form id="fmUnit" runat="server">
        <ext:ResourceManager ID="rmUnit" runat="server" />
        <ext:Store runat="server" ID="store">
             <Proxy>
                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
            </Proxy>
            <Model>
                <ext:Model ID="Model1" runat="server" >
                    <Fields>
                        <ext:ModelField Name="name" />
                        <ext:ModelField Name="url" />      
                    </Fields>
                </ext:Model>
            </Model>
        </ext:Store>
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" Padding="5" Height="30">
                    <Items>
                        <ext:Label runat="server" ColumnWidth=".2"></ext:Label>
                        <ext:Label ID="planState" Html="<span style='padding:20px;font-weight: bold;' >密炼机状态说明:<span><span style='color:Red;padding:20px;font-weight: bold;'>停机</span><span style='color:green;padding:20px;font-weight: bold;'>运转</span><span style='color:#DD9900;padding:20px;font-weight: bold;'>空转</span>"
                            runat="server" ColumnWidth=".25" Padding="2">
                        </ext:Label>
                        <ext:Label runat="server" ColumnWidth=".25"></ext:Label>
                        <ext:Label runat="server" ColumnWidth=".25"></ext:Label>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel1" runat="server" Region="West" Width="235" Layout="BorderLayout">
                    <Items>
                         <ext:TreePanel ID="treeDept" runat="server" Title="车间列表" Region="Center"  Icon="FolderGo" AutoHeight="true" RootVisible="false">
                            <Store>
                                <ext:TreeStore ID="treeDeptStore" runat="server">
                                    <Proxy>
                                        <ext:PageProxy>
                                            <RequestConfig Method="GET" Type="Load" />
                                        </ext:PageProxy>
                                    </Proxy>
                                    <Root>
                                        <ext:Node NodeID="root" Expanded="true" />
                                    </Root>
                                </ext:TreeStore>
                            </Store>
                            <Listeners>
                                <ItemClick Handler="treeClick(record)" />
                            </Listeners>
                        </ext:TreePanel>       
                    </Items>
                </ext:Panel>
                <ext:Panel  ID="Panel2" runat="server" Region="Center" Cls="images-view" Title="所有机台" AutoScroll="true" Layout="Fit">
                    <Items>
                        <ext:DataView runat="server" StoreID="store" OverItemCls="x-item-over" ItemSelector="div.thumb">
                            <Tpl runat="server">
                                <Html>
                                    <tpl for=".">
                                        <div class="div_warp" id="{name}">
									        <div class="thumb">
                                                <img src="{url}" title="{name}" alt="{name}">
                                                <span>{name}</span>
                                            </div>
								        </div>
                                    </tpl>
                                </Html>
                            </Tpl>
                        </ext:DataView>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
        </form>
</body>
</html>
