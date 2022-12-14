<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pstmmshopout.aspx.cs" Inherits="Manager_ShopStorage_Pstmmshopout" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>车间原料消耗</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" runat="server" Text="Button" OnClick="btnExportSubmit_Click" Style="display:none" />
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Hidden ID="hidden_stop_type" runat="server" />
    <ext:Hidden ID="hidden_stop_fault" runat="server" />
    <ext:Hidden ID="hidden_fault_reason" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlStopType" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barStopType">
                            <Items>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips><ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page"></EventMask>
                                    </Click></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                    <ToolTips><ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行结果导出" /></ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                        <ext:DateField ID="txtDate" runat="server" Editable="false" AllowBlank="false"
                                            Vtype="daterange" FieldLabel="生产日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd" />
                                    </Items>
                                </ext:Container>

                                <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                        <ext:ComboBox LabelAlign="left" ID="cbxshift" runat="server" FieldLabel="班次" />
                                    </Items>
                                </ext:Container>

                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                        <ext:ComboBox LabelAlign="left" ID="cbxshop" runat="server" FieldLabel="车间" />
                                    </Items>
                                </ext:Container>

                            </Items>
                        </ext:FormPanel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="50">
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="Plan_date" />
                                        <ext:ModelField Name="ShiftName" />
                                        <ext:ModelField Name="shift_ClassName" />
                                        <ext:ModelField Name="EquipName" />
                                        <ext:ModelField Name="SCMater" />
                                        <ext:ModelField Name="XHMater" />
                                        <ext:ModelField Name="consume_qty" />
                                        <ext:ModelField Name="Balance_Qty" />
                                        <ext:ModelField Name="Cons_qty" />
                                        <ext:ModelField Name="Sur_plus" />
                                        <ext:ModelField Name="sunhao" />
                                        <ext:ModelField Name="MinorTypeName" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:Column ID="clEquip" runat="server" Text="生产日期" DataIndex="Plan_date" Width="80"/>
                            <ext:Column ID="clShiftName" runat="server" Text="班次" DataIndex="ShiftName" Width="40"/>
                            <ext:Column ID="clClassName" runat="server" Text="班组" DataIndex="shift_ClassName" Width="40"/>
                            <ext:Column ID="clStartTime" runat="server" Text="生产机台" DataIndex="EquipName" Width="90"/>
                            <ext:Column ID="Column1" runat="server" Text="生产物料" DataIndex="SCMater" Width="220"/>
                            <ext:Column ID="Column2" runat="server" Text="消耗物料" DataIndex="XHMater" Width="160"/>
                            <ext:Column ID="Column3" runat="server" Text="定额用量" DataIndex="consume_qty" Width="60"/>
                            <ext:Column ID="Column4" runat="server" Text="称量用量" DataIndex="Balance_Qty" Width="60"/>
                            <ext:Column ID="Column5" runat="server" Text="暂记用量" DataIndex="Cons_qty" Width="60"/>
                            <ext:Column ID="Column6" runat="server" Text="损耗量" DataIndex="Sur_plus" Width="60"/>
                            <ext:Column ID="Column7" runat="server" Text="损耗率(%)" DataIndex="sunhao" Width="70"/>
                            <ext:Column ID="Column8" runat="server" Text="物料分类" DataIndex="MinorTypeName" Width="70"/>
                           
                        </Columns>
                    </ColumnModel>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolBar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server"/>
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>