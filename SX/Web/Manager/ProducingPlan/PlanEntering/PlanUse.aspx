﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanUse.aspx.cs" Inherits="Manager_ProducingPlan_PlanEntering_PlanUse" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>机台在用配方管理</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell
        {
        	background-color: #CCFF66 !important;
        }
    </style>
    <script type="text/javascript">
        //点击修改按钮
        var cmdcol_click = function (command, record) {
            if (command == "Edit") {
                App.direct.pnlList_Edit(record.data.RecipeEquipCode, record.data.RecipeVersionID, record.data.RecipeMaterialName, {
                    success: function () { },
                    failure: function () { }
                });
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
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
                                 <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items> 
                                        <ext:ComboBox ID="cbxequip" runat="server" FieldLabel="机台" LabelAlign="Right"  Editable="true" />
                                       
                                    </Items>
                                </ext:Container>
                                 <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items> 
                                        <ext:ComboBox ID="cbxpeifang" runat="server" FieldLabel="配方名称" LabelAlign="Right"  Editable="true" />
                                       
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="1000">
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="Equip_name" />
                                        <ext:ModelField Name="RecipeVersionID" />
                                        <ext:ModelField Name="RecipeMaterialName" />
                                        <ext:ModelField Name="Type" />
                                        <ext:ModelField Name="State" />
                                        <ext:ModelField Name="Plan_Use" />
                                        <ext:ModelField Name="RecipeEquipCode" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:Column ID="Column6" runat="server" Text="jitai" DataIndex="RecipeEquipCode" Width="80" Hidden="true"/>
                            <ext:Column ID="Oil_name" runat="server" Text="机台" DataIndex="Equip_name" Width="80"/>
                            <ext:Column ID="Column1" runat="server" Text="版本号" DataIndex="RecipeVersionID" Width="80"/>
                            <ext:Column ID="Column2" runat="server" Text="配方名称" DataIndex="RecipeMaterialName" Width="80"/>
                            <ext:Column ID="Column3" runat="server" Text="配方类型" DataIndex="Type" Width="80"/>
                            <ext:Column ID="Column4" runat="server" Text="配方状态" DataIndex="State" Width="80"/>
                            <ext:Column ID="Column5" runat="server" Text="计划使用" DataIndex="Plan_Use" Width="80"/>
                            <ext:CommandColumn ID="cmdCol" runat="server" Align="Center" Text="操作" Width="185">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改"/>
                                </Commands>
                                <Listeners>
                                    <Command Handler="cmdcol_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
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
                <ext:Window ID="winSave" runat="server" Icon="MonitorAdd" Closable="false" Title="计划维护" Width="550" Height="500" Resizable="false" Modal="true" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="FormLayout">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5" Layout="FormLayout">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="70" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Hidden runat="server" ID="hideObjID" />
                                <ext:FieldSet ID="FieldSet1" runat="server" Title="计划维护">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer12"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                         <ext:ComboBox ID="cbxshiyong" runat="server" FieldLabel="计划使用" LabelAlign="Right" Editable="false">
                                                <Items>
                                                    <ext:ListItem Text="否"></ext:ListItem>
                                                    <ext:ListItem Text="是"></ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:Hidden runat="server" ID="hideMode" Text="Add" />
                                        <ext:Hidden runat="server" ID="hideMode1" Text="Add" />
                                        <ext:Hidden runat="server" ID="hideMode2" Text="Add" />
                                    </Items>
                                </ext:FieldSet>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                     <Buttons>
                        <ext:Button ID="btnSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <DirectEvents>
                                <Click OnEvent="btnSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click"/>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:Window>

            </Items>
        </ext:Viewport>
    </form>
</body>
</html>