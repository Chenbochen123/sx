﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_ProducingPlan_EquipAbility_EquipAbilityt, App_Web_mtrmpb1q" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设备生产能力</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        //树形结构点击刷新右侧方法
        var menuItemClick = function (view, rcd, item, idx, event, eOpts) {
            var s = rcd.get('qtip');
            if (s) {
                App.direct.LoadGridData(s, {
                    success: function (result) {
                    },
                    failure: function (errorMsg) {
                        Ext.Msg.alert('Failure', errorMsg);
                    }
                });
            }
            else {
                Ext.Msg.alert('提示', '请选择机台！');
            }
        };
        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.Store1.currentPage = 1;
            //App.hidden_select_equip_code.setValue('');
            App.pageToolBar.doRefresh();
            return false;
        }
        //点击汇总
        var pnlSumFresh = function () {
            App.direct.SumEquipAbilityt({
                success: function (result) {
                    App.Store1.currentPage = 1;
                    //App.hidden_select_equip_code.setValue('');
                    App.pageToolBar.doRefresh();
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('Failure', errorMsg);
                }
            });
           
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Hidden ID="hidden_select_equip_code" runat="server">
    </ext:Hidden>
    <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button"
        OnClick="btnExportSubmit_Click" />
    <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="Panel1" runat="server" Region="West" Width="180" Split="true" Layout="BorderLayout">
                <Items>
                    <ext:TreePanel ID="treeEquip" runat="server" Title="机台分组信息" Region="Center" Icon="FolderGo"
                        AutoHeight="true" RootVisible="false">
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
                            <ItemClick Fn="menuItemClick">
                            </ItemClick>
                        </Listeners>
                    </ext:TreePanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="Panel2" runat="server" Region="Center" Layout="BorderLayout">
                <Items>
                    <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true">
                        <TopBar>
                            <ext:Toolbar runat="server" ID="ctl320">
                                <Items>
                                    <ext:ToolbarSeparator ID="ctl347" />
                                    <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSum">
                                        <Listeners>
                                            <Click Fn="pnlSumFresh">
                                            </Click>
                                        </Listeners>
                                        <ToolTips>
                                            <ext:ToolTip runat="server" Html="查询" ID="ToolTip1" />
                                        </ToolTips>
                                    </ext:Button>
                                    <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                        <Listeners>
                                            <Click Handler="$('#btnExportSubmit').click();">
                                            </Click>
                                        </Listeners>
                                        <ToolTips>
                                            <ext:ToolTip runat="server" Html="导出" ID="ctl350" />
                                        </ToolTips>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        <Items>
                            <ext:Panel ID="Panel4" runat="server" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                        <Items>
                                            <ext:Container ID="Container7" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                                Padding="5">
                                                <Items>
                                                    <ext:DateField ID="txtStratPlanDate" runat="server" Editable="false" Vtype="daterange"
                                                        FieldLabel="生产开始日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd">
                                                    </ext:DateField>
                                                </Items>
                                            </ext:Container>
                                            <ext:Container ID="Container13" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                                Padding="5">
                                                <Items>
                                                    <ext:DateField ID="txtEndPlanDate" runat="server" Editable="false" Vtype="daterange"
                                                        FieldLabel="生产结束日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd">
                                                    </ext:DateField>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
                    <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                        <Store>
                            <ext:Store ID="Store1" runat="server" PageSize="18">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="Model2" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="StatTime" Type="Date" />
                                            <ext:ModelField Name="EquipName" Type="String" />
                                            <ext:ModelField Name="MaterialName" Type="String" />
                                            <ext:ModelField Name="MaxAllrtime" Type="String" />
                                            <ext:ModelField Name="MinAllrtime" Type="String" />
                                            <ext:ModelField Name="AvgAllrtime" Type="String" />
                                            <ext:ModelField Name="MaxBwbtime" Type="String" />
                                            <ext:ModelField Name="MinBwbtime" Type="String" />
                                            <ext:ModelField Name="AvgBwbtime" Type="String" />
                                            <ext:ModelField Name="MaxPolyTime" Type="String" />
                                            <ext:ModelField Name="MinPolyTime" Type="String" />
                                            <ext:ModelField Name="AvgPolyTime" Type="String" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:Column ID="Column3" runat="server" Text="机台" Width="100" DataIndex="EquipName">
                                </ext:Column>
                                <ext:Column ID="Column2" runat="server" Text="物料名称" Width="150" DataIndex="MaterialName">
                                </ext:Column>
                                <ext:Column ID="Column1" runat="server" Text="最大混炼时间" Flex="1" DataIndex="MaxAllrtime">
                                </ext:Column>
                                <ext:Column ID="Column4" runat="server" Text="最小混炼时间" Flex="1" DataIndex="MinAllrtime">
                                </ext:Column>
                                <ext:Column ID="Column5" runat="server" Text="平均混炼时间" Flex="1" DataIndex="AvgAllrtime">
                                </ext:Column>
                                <ext:Column ID="Column6" runat="server" Text="最大间隔时间" Flex="1" DataIndex="MaxBwbtime">
                                </ext:Column>
                                <ext:Column ID="Column7" runat="server" Text="最小间隔时间" Flex="1" DataIndex="MinBwbtime">
                                </ext:Column>
                                <ext:Column ID="Column8" runat="server" Text="平均间隔时间" Flex="1" DataIndex="AvgBwbtime">
                                </ext:Column>
                                <ext:Column ID="Column9" runat="server" Text="最大加胶时间" Flex="1" DataIndex="MaxPolyTime">
                                </ext:Column>
                                <ext:Column ID="Column10" runat="server" Text="最小加胶时间" Flex="1" DataIndex="MinPolyTime">
                                </ext:Column>
                                <ext:Column ID="Column11" runat="server" Text="平均加胶时间" Flex="1" DataIndex="AvgPolyTime">
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Multi" />
                        </SelectionModel>
                        <View>
                            <ext:GridView ID="GridView1" runat="server" StripeRows="true">
                                <%--<GetRowClass Fn="getRowClass" />--%>
                            </ext:GridView>
                        </View>
                        <BottomBar>
                            <ext:PagingToolbar ID="pageToolBar" runat="server">
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
            <ext:Hidden ID="hidden_parent_num" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hidden_type" runat="server">
            </ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
