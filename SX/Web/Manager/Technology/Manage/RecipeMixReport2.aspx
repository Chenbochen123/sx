<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecipeMixReport2.aspx.cs" Inherits="Manager_Technology_Manage_RecipeMixReport2" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工艺步骤报表</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
     <style type="text/css">
        .x-grid-body .x-grid-cell-Cost
        {
            background-color: #f1f2f4;
        }
        
        .x-grid-row-summary .x-grid-cell-Cost .x-grid-cell-inner
        {
            background-color: #e1e2e4;
        }
        
        .task .x-grid-cell-inner
        {
            padding-left: 15px;
        }
        
        .x-grid-row-summary .x-grid-cell-inner
        {
            font-weight: bold;
            font-size: 11px;
            background-color: #f1f2f4;
        }
    </style>
    <script type="text/javascript">


    </script>
    
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
      <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
        <Items>
            
                <ext:Panel ID="pnlStopType" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barStopType">
                            <Items>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips><ext:ToolTip ID="ToolTip2" runat="server" Html="点击进行查询" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page"></EventMask>
                                    </Click></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                               <%-- <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip3" runat="server" Html="点击进行结果导出" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />--%>
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
                                <ext:Container ID="Container1"  runat="server" Layout="FormLayout" ColumnWidth=".25">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer9"  runat="server" Layout="HBoxLayout" FieldLabel="开始时间" >
                                            <Items>
                                                <ext:DateField ID="dStartDate" runat="server" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" />
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer3"  runat="server" Layout="HBoxLayout" FieldLabel="物料名称" >
                                            <Items>
                                                <ext:ComboBox ID="cbxMater" runat="server"  Editable="true" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container2"  runat="server" Layout="FormLayout" ColumnWidth=".25">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer7"  runat="server" Layout="HBoxLayout" FieldLabel="结束时间" >
                                            <Items>
                                                <ext:DateField ID="dEndDate" runat="server" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer2"  runat="server" Layout="HBoxLayout" FieldLabel="配方类型" >
                                            <Items>
                                                <ext:ComboBox ID="cbxPeifang" runat="server" Editable="false" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container3"  runat="server" Layout="FormLayout" ColumnWidth=".25">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer1"  runat="server" Layout="HBoxLayout" FieldLabel="班组">
                                            <Items>
                                                <ext:ComboBox ID="cbxClass" runat="server"  Editable="false" />
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer4"  runat="server" Layout="HBoxLayout" FieldLabel="机台">
                                            <Items>
                                                <ext:ComboBox ID="cbxEquip" runat="server"  Editable="false" />
                                            </Items>
                                        </ext:FieldContainer>
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
                                <ext:Model ID="model" runat="server" >
                                    <Fields>
                                        <ext:ModelField Name="mixid" />
                                        <ext:ModelField Name="act_name" />
                                        <ext:ModelField Name="num" />
                                        <ext:ModelField Name="time_max" />
                                        <ext:ModelField Name="time_min" />
                                        <ext:ModelField Name="time_avg" />
                                        <ext:ModelField Name="time_std" />
                                        <ext:ModelField Name="temp_max" />
                                        <ext:ModelField Name="temp_min" />
                                        <ext:ModelField Name="temp_avg" />
                                        <ext:ModelField Name="temp_std" />
                                        <ext:ModelField Name="ener_max" />
                                        <ext:ModelField Name="ener_min" />
                                        <ext:ModelField Name="ener_avg" />
                                        <ext:ModelField Name="ener_std" />
                                        <ext:ModelField Name="power_max" />
                                        <ext:ModelField Name="power_min" />
                                        <ext:ModelField Name="power_avg" />
                                        <ext:ModelField Name="power_std" />
                                        <ext:ModelField Name="press_max" />
                                        <ext:ModelField Name="press_min" />
                                        <ext:ModelField Name="press_avg" />
                                        <ext:ModelField Name="press_std" />
                                        <ext:ModelField Name="speed_max" />
                                        <ext:ModelField Name="speed_min" />
                                        <ext:ModelField Name="speed_avg" />
                                        <ext:ModelField Name="speed_std" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:Column runat="server" Text="">
                                <Columns>
                            <ext:Column ID="mater_name" runat="server" Text="序号" DataIndex="mixid" Width="35"/>
                            <ext:Column ID="Column1" runat="server" Text="动作名称" DataIndex="act_name" Width="100"/>
                            <ext:Column ID="Column2" runat="server" Text="统计车数" DataIndex="num" Width="60"/>
                                </Columns>
                            </ext:Column>
                            <ext:Column runat="server" Text="分步骤时间">
                                <Columns>
                            <ext:Column ID="Column3" runat="server" Text="最大值" DataIndex="time_max" Width="60"/>
                            <ext:Column ID="Column4" runat="server" Text="最小值" DataIndex="time_min" Width="60"/>
                            <ext:Column ID="Column5" runat="server" Text="均值" DataIndex="time_avg" Width="60"/>
                            <ext:Column ID="Column6" runat="server" Text="标准差" DataIndex="time_std" Width="60"/>
                                </Columns>
                            </ext:Column>
                            <ext:Column runat="server" Text="温度">
                                <Columns>
                            <ext:Column ID="Column7" runat="server" Text="最大值" DataIndex="temp_max" Width="60"/>
                            <ext:Column ID="Column8" runat="server" Text="最小值" DataIndex="temp_min" Width="60"/>
                            <ext:Column ID="Column9" runat="server" Text="均值" DataIndex="temp_avg" Width="60"/>
                            <ext:Column ID="Column10" runat="server" Text="标准差" DataIndex="temp_std" Width="60"/>
                                </Columns>
                            </ext:Column>
                            <ext:Column runat="server" Text="分步骤能量">
                                <Columns>
                            <ext:Column ID="Column11" runat="server" Text="最大值" DataIndex="ener_max" Width="60"/>
                            <ext:Column ID="Column12" runat="server" Text="最小值" DataIndex="ener_min" Width="60"/>
                            <ext:Column ID="Column13" runat="server" Text="均值" DataIndex="ener_avg" Width="60"/>
                            <ext:Column ID="Column14" runat="server" Text="标准差" DataIndex="ener_std" Width="60"/>
                                </Columns>
                            </ext:Column>
                            <ext:Column runat="server" Text="功率">
                                <Columns>
                            <ext:Column ID="Column15" runat="server" Text="最大值" DataIndex="power_max" Width="60"/>
                            <ext:Column ID="Column16" runat="server" Text="最小值" DataIndex="power_min" Width="60"/>
                            <ext:Column ID="Column17" runat="server" Text="均值" DataIndex="power_avg" Width="60"/>
                            <ext:Column ID="Column18" runat="server" Text="标准差" DataIndex="power_std" Width="60"/>
                                </Columns>
                            </ext:Column>
                            <ext:Column runat="server" Text="压力">
                                <Columns>
                            <ext:Column ID="Column19" runat="server" Text="最大值" DataIndex="press_max" Width="60"/>
                            <ext:Column ID="Column20" runat="server" Text="最小值" DataIndex="press_min" Width="60"/>
                            <ext:Column ID="Column21" runat="server" Text="均值" DataIndex="press_avg" Width="60"/>
                            <ext:Column ID="Column22" runat="server" Text="标准差" DataIndex="press_std" Width="60"/>
                                </Columns>
                            </ext:Column>
                            <ext:Column runat="server" Text="转速">
                                <Columns>
                            <ext:Column ID="Column23" runat="server" Text="最大值" DataIndex="speed_max" Width="60"/>
                            <ext:Column ID="Column24" runat="server" Text="最小值" DataIndex="speed_min" Width="60"/>
                            <ext:Column ID="Column25" runat="server" Text="均值" DataIndex="speed_avg" Width="60"/>
                            <ext:Column ID="Column26" runat="server" Text="标准差" DataIndex="speed_std" Width="60"/>
                                </Columns>
                            </ext:Column>

                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                            <Listeners>
                                <Select Handler="#{detailStore}.reload();" Buffer="250" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolBar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server"/>
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel> 
            
                <ext:Hidden ID="hidden_equip_code" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hidden_type" runat="server">
                </ext:Hidden>
         </Items>
    </ext:Viewport>
    </form>
</body>
</html>
