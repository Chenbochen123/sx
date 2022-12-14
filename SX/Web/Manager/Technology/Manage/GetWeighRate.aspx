﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetWeighRate.aspx.cs" Inherits="Manager_Technology_Manage_GetWeighRate" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>称量合格率</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>GetWeightRate.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
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
        
        .x-grid-row-collapsedYellow .x-grid-cell {     
            background-color: #FFFF00 !important;
        } 
    </style>
    <script type="text/javascript">


        var SetRowClass = function (record, rowIndex, rowParams, store) {
            var rowClass = '';
            if (record.get("合格率") < "100") {
                rowClass = 'x-grid-row-collapsedYellow';
            }

            return rowClass;
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
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
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                    <ToolTips><ext:ToolTip ID="ToolTip3" runat="server" Html="点击进行结果导出" /></ToolTips>
                                    <DirectEvents>
                                    <Click OnEvent="btnExportSubmit_Click" IsUpload="true">
                                        <ExtraParams>
                                            <ext:Parameter Name="fields" Value="#{ModelCenter}.getFields()" Mode="Raw" />
                                            <ext:Parameter Name="records" Value="#{StoreCenter}.getRecordsValues({ excludeId: true })"
                                                Mode="Raw" />
                                        </ExtraParams>
                                    </Click>
                                    </DirectEvents>
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
                                <ext:Container ID="Container1"  runat="server" Layout="FormLayout" ColumnWidth=".2">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer9"  runat="server" Layout="HBoxLayout" FieldLabel="开始时间" >
                                            <Items>
                                                <ext:DateField ID="dStartDate" runat="server" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" />
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer3"  runat="server" Layout="HBoxLayout" FieldLabel="部门" >
                                            <Items>
                                                <ext:ComboBox ID="cbxDep" runat="server"  Editable="false" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container2"  runat="server" Layout="FormLayout" ColumnWidth=".2">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer7"  runat="server" Layout="HBoxLayout" FieldLabel="结束时间" >
                                            <Items>
                                                <ext:DateField ID="dEndDate" runat="server" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer2"  runat="server" Layout="HBoxLayout" FieldLabel="类型" >
                                            <Items>
                                              
                                                <ext:ComboBox ID="cbxType" runat="server" Editable="false" Text="炭黑">
                                                    <Items>
                                                        <ext:ListItem Text="炭黑" Value="0">
                                                        </ext:ListItem>
                                                        <ext:ListItem Text="油" Value="1">
                                                        </ext:ListItem>
                                                        <ext:ListItem Text="自动小料" Value="2">
                                                        </ext:ListItem>
                                                        <ext:ListItem Text="人工小料" Value="3">
                                                        </ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container3"  runat="server" Layout="FormLayout" ColumnWidth=".2">
                                    <Items>
                                        <ext:TriggerField ID="cbxEquip" runat="server" Flex="1" FieldLabel="机台名称" LabelAlign="left" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryEquipInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        
                                         <ext:ComboBox ID="cbxclass" runat="server" FieldLabel="班组" LabelAlign="left" 
                                                    Editable="false">
                                                    <Items>
                                                        <ext:ListItem Text="甲" Value="1">
                                                        </ext:ListItem>
                                                        <ext:ListItem Text="乙" Value="2">
                                                        </ext:ListItem>
                                                        <ext:ListItem Text="丙" Value="3">
                                                        </ext:ListItem>
                                                    </Items>
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" Qtip="清空">
                                                        </ext:FieldTrigger>
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="this.setValue('');" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                                
                                <ext:Container ID="Container4"  runat="server" Layout="FormLayout" ColumnWidth=".2">
                                    <Items>
                                         <ext:ComboBox ID="cbxuser" runat="server" FieldLabel="人员" LabelAlign="left" 
                                                    Editable="false" MultiSelect="true">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" Qtip="清空">
                                                        </ext:FieldTrigger>
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="this.setValue('');" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                        
                                        <ext:TriggerField ID="cbxmater" runat="server" Flex="1" FieldLabel="物料" LabelAlign="left" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryMaterialInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                        <ext:Hidden ID="hiddenRecipeEquipCode" runat="server"></ext:Hidden>
                        <ext:Hidden ID="hiddenmater" runat="server"></ext:Hidden>
                    </Items>
                </ext:Panel>
                        
                <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="FitLayout" Flex="80">
                    <Items>
                        <ext:GridPanel runat="server" ID="GridPanelCenter" ColumnLines="true" AnchorHeight="100">
                            <Store>
                                <ext:Store runat="server" ID="StoreCenter">
                                    <Model>
                                        <ext:Model runat="server" ID="ModelCenter">
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                </Columns>
                            </ColumnModel>
                            
                            <View>
                                <ext:GridView runat="server" ID="GridViewCenter">
                                    <GetRowClass Fn="SetRowClass" />
                                </ext:GridView>
                            </View>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>  
                <ext:Panel runat="server" ID="Panel1" Region="South" Layout="FitLayout" Flex="20">
                    <Items>
                        <ext:GridPanel runat="server" ID="GridPanel1" ColumnLines="true" AnchorHeight="100">
                            <Store>
                                <ext:Store runat="server" ID="Store1">
                                    <Model>
                                        <ext:Model runat="server" ID="Model1">
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                </Columns>
                            </ColumnModel>
                            
                            <View>
                                <ext:GridView runat="server" ID="GridView1">
                                    <GetRowClass Fn="SetRowClass" />
                                </ext:GridView>
                            </View>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
                <ext:Hidden ID="hidden_equip_code" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hidden_type" runat="server">
                </ext:Hidden>
         </Items>
    </ext:Viewport>
    </form>
</body>
</html>
