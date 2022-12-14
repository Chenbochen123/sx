<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HostStatistics.aspx.cs" Inherits="Manager_ProducingPlan_Statistics_HostStatistics" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>主机手产量统计</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">

        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.Store2.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }
        //查询日期设置
        var onKeyUp = function () {
            var me = this,
                v = me.getValue(),
                field;

            if (me.startDateField) {
                field = Ext.getCmp(me.startDateField);
                field.setMaxValue(v);
                me.dateRangeMax = v;
            } else if (me.endDateField) {
                field = Ext.getCmp(me.endDateField);
                field.setMinValue(v);
                me.dateRangeMin = v;
            }
            field.validate();
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="Panel1" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ctl320">
                        <Items>
                            <ext:ToolbarSeparator ID="ctl347" />
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <Listeners>
                                    <Click Fn="pnlListFresh">
                                    </Click>
                                </Listeners>
                                <ToolTips>
                                    <ext:ToolTip runat="server" Html="查询" ID="ToolTip2" />
                                </ToolTips>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="ctl361" />
                            <ext:ToolbarSpacer runat="server" ID="ctl363" />
                            <ext:ToolbarFill ID="ctl381" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel ID="Panel2" runat="server" AutoHeight="true">
                        <Items>
                            <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="Container7" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:DateField ID="txtStratDate" runat="server" Editable="false" AllowBlank="false"
                                                Vtype="daterange" FieldLabel="开始日期" EnableKeyEvents="true" Format="yyyy-MM-dd">
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="endDateField" Value="txtStopDate" Mode="Value" />
                                                </CustomConfig>
                                                <Listeners>
                                                    <KeyUp Fn="onKeyUp" />
                                                </Listeners>
                                            </ext:DateField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:DateField ID="txtStopDate" runat="server" Editable="false" AllowBlank="false"
                                                Vtype="daterange" FieldLabel="结束日期" EnableKeyEvents="true" Format="yyyy-MM-dd">
                                                <CustomConfig>
                                                    <ext:ConfigItem Name="startDateField" Value="txtStratDate" Mode="Value" />
                                                </CustomConfig>
                                                <Listeners>
                                                    <KeyUp Fn="onKeyUp" />
                                                </Listeners>
                                            </ext:DateField>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:GridPanel ID="GridPanel1" runat="server" Region="Center">
                <Store>
                    <ext:Store ID="Store2" runat="server" PageSize="15">
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="Model1" runat="server">
                                <Fields>
                                    <ext:ModelField Name="MaterialName" Type="String" />
                                    <ext:ModelField Name="UserName" Type="String" />
                                    <ext:ModelField Name="ClassName" Type="String" />
                                    <ext:ModelField Name="RealWeight" Type="Float" />
                                    <ext:ModelField Name="EquipName" Type="String">
                                    </ext:ModelField>
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="ColumnModel2" runat="server">
                    <Columns>
                        <ext:Column ID="Column3" runat="server" Text="主手" Width="100" DataIndex="UserName">
                        </ext:Column>
                        <ext:Column ID="Column2" runat="server" Text="班组" Width="150" DataIndex="ClassName">
                        </ext:Column>
                        <ext:Column ID="Column1" runat="server" Text="胶料名称" Width="150" DataIndex="MaterialName">
                        </ext:Column>
                        <ext:Column ID="Column5" runat="server" Text="产量" Width="150" DataIndex="RealWeight">
                        </ext:Column>
                        <ext:Column ID="Column4" runat="server" Text="机台" Width="100" DataIndex="EquipName">
                        </ext:Column>
                    </Columns>
                </ColumnModel>
                <SelectionModel>
                    <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" Mode="Multi" />
                </SelectionModel>
                <View>
                    <ext:GridView ID="GridView2" runat="server" StripeRows="true">
                    </ext:GridView>
                </View>
                <BottomBar>
                    <ext:PagingToolbar ID="pageToolBar" runat="server">
                        <Plugins>
                            <ext:ProgressBarPager ID="ProgressBarPager1" runat="server" />
                        </Plugins>
                    </ext:PagingToolbar>
                </BottomBar>
            </ext:GridPanel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
