<%@ page language="C#" autoeventwireup="true" inherits="Manager_RubberQuality_Demo_DemoMain, App_Web_ayksz03z" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>质量SPC分析</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript">
        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.hidden_delete_flag.setValue("0");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }
    </script>
</head>
<body>
    <form id="fmUnit" runat="server">
        <ext:ResourceManager ID="rmUnit" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUnit">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="pnlListFresh"></Click>
                                    </DirectEvents>
                                </ext:Button>
                                 <ext:Button runat="server" Icon="ChartLine" Text="运行图" ID="Button1">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="查看运行图" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="App.win1.show()"></Click>
                                    </Listeners>
                                </ext:Button>
                                 <ext:Button runat="server" Icon="ChartBar" Text="能力图" ID="Button2">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip3" runat="server" Html="查看能力图" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="App.win2.show()"></Click>
                                    </Listeners>
                                </ext:Button>
                                 <ext:Button runat="server" Icon="ChartCurve" Text="均指级差" ID="Button3">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip4" runat="server" Html="查看均指级差" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="App.win3.show()"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>

                    <Items>
                        <ext:Panel ID="pnlUnitQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:DateField ID="txt_obj_id"  runat="server" FieldLabel="起始日期" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:DateField ID="txt_unit_name"  runat="server" FieldLabel="截至日期" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:ComboBox ID="txt_remark" runat="server" FieldLabel="胶料名称" LabelAlign="Right" >
                                                    <Items>
                                                        <ext:ListItem Text="F003-1" Value="F003"></ext:ListItem>
                                                        <ext:ListItem Text="S460" Value="S460"></ext:ListItem>
                                                        <ext:ListItem Text="HF518" Value="HF518"></ext:ListItem>
                                                        <ext:ListItem Text="B361" Value="B361"></ext:ListItem>
                                                        <ext:ListItem Text="N839" Value="N839"></ext:ListItem>
                                                        <ext:ListItem Text="S401" Value="S401"></ext:ListItem>
                                                        <ext:ListItem Text="S430" Value="S430"></ext:ListItem>
                                                        <ext:ListItem Text="EMI" Value="EMI"></ext:ListItem>
                                                        <ext:ListItem Text="F561" Value="F561"></ext:ListItem>
                                                        <ext:ListItem Text="F549" Value="F549"></ext:ListItem>
                                                        <ext:ListItem Text="GNS" Value="GNS"></ext:ListItem>
                                                        <ext:ListItem Text="L711" Value="L711"></ext:ListItem>
                                                        <ext:ListItem Text="LPS" Value="LPS"></ext:ListItem>
                                                        <ext:ListItem Text="L732" Value="L732"></ext:ListItem>
                                                        <ext:ListItem Text="A692" Value="A692"></ext:ListItem>
                                                        <ext:ListItem Text="TIC" Value="TIC"></ext:ListItem>
                                                        <ext:ListItem Text="A643" Value="A643"></ext:ListItem>
                                                        <ext:ListItem Text="TRI-5" Value="TRI-5"></ext:ListItem>
                                                        <ext:ListItem Text="WAPL" Value="WAPL"></ext:ListItem>
                                                        <ext:ListItem Text="F528" Value="F528"></ext:ListItem>
                                                        <ext:ListItem Text="T219" Value="T219"></ext:ListItem>
                                                        <ext:ListItem Text="S418" Value="S418"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:ComboBox ID="TextField1" runat="server" FieldLabel="检验项目" LabelAlign="Right" >
                                                    <Items>
                                                        <ext:ListItem Text="MH" Value="MH"></ext:ListItem>
                                                        <ext:ListItem Text="ML" Value="ML"></ext:ListItem>
                                                        <ext:ListItem Text="T10" Value="T10"></ext:ListItem>
                                                        <ext:ListItem Text="T90" Value="T90"></ext:ListItem>
                                                        <ext:ListItem Text="密度" Value="密度"></ext:ListItem>
                                                        <ext:ListItem Text="硬度" Value="硬度"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:FormPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="15" RemoteSort="true">
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="CheckValue" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="ObjID" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                            <ext:Column ID="unit_name" runat="server" Text="检验值" DataIndex="CheckValue" Width="150"  />
                        </Columns>
                    </ColumnModel>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolBar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
                <ext:Window ID="win1" Maximized="true" AutoScroll="true" runat="server" Icon="MonitorAdd" Closable="true" Title="运行图"
                    Width="1100" Height="500" Resizable="true" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:Image runat="server" ImageUrl="11.png"></ext:Image>
                    </Items>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                 <ext:Window ID="win2" Maximized="true" AutoScroll="true" runat="server" Icon="MonitorAdd" Closable="true" Title="能力图"
                    Width="1100" Height="500" Resizable="true" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:Image ID="Image1" runat="server" ImageUrl="21.png"></ext:Image>
                    </Items>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                 <ext:Window ID="win3" Maximized="true" AutoScroll="true" runat="server" Icon="MonitorAdd" Closable="true" Title="均值级差图"
                    Width="1100" Height="500" Resizable="true" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:Image ID="Image2" runat="server" ImageUrl="31.png"></ext:Image>
                    </Items>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
            </Items>
        </ext:Viewport>
        </form>
</body>
</html>