﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_Manage_MaterialRecipeDetail_Weight, App_Web_a5rwyiqa" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工艺配方明细-称量</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            for (var i = 0; i < App.pnlWeight.items.items.length; i++) {
                var item = App.pnlWeight.items.items[i];
                item.store.currentPage = 1;
                item.store.reload();
            }
            return false;
        }
    </script>

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>Weight.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="pnlWeight" runat="server" Region="Center" Header="false" Layout="AccordionLayout">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="Toolbar3">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Calculator" Text="计算重量" ID="Button2">
                                    <Listeners>
                                        <Click Handler="weightTotal();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" Icon="Reload" Text="刷新称量信息" ID="Button3">
                                    <Listeners>
                                        <Click Handler="gridPanelRefresh();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator1" />
                                <ext:Checkbox ID="cbPageCanEdit" runat="server" FieldLabel="编辑当前信息" LabelAlign="Right" 
                                    Disabled="true" Hidden="true"></ext:Checkbox>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <BottomBar>
                        <ext:StatusBar ID="StatusBar1" runat="server" Height="24" Text=""></ext:StatusBar>
                    </BottomBar>
                    <Items>
                        <ext:GridPanel ID="gridPanelWeightCarbon" runat="server" Flex="1" Frame="true" Title="炭黑称量信息">
                            <Store>
                                <ext:Store ID="storeWeightCarbon" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.WeightGridPanelBindData" PageParam="Page@0" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="ModelCarbon" runat="server" Name="gridPanelWeightCarbonModel">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="WeightType" />
                                                <ext:ModelField Name="MaterialCode" Type="String" />
                                                <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                <ext:ModelField Name="MaterialName" Type="String" />
                                                <ext:ModelField Name="ActCode" Type="String" />
                                                <ext:ModelField Name="SetWeight" />
                                                <ext:ModelField Name="OldSetWeight" Type="Float" />
                                                <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                <ext:ModelField Name="RecipeEquipCode" Type="String" />
                                                     <ext:ModelField Name="Supply_code" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModelCarbon" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumColCarbon" runat="server" Width="30" />
                                    <%--<ext:Column ID="Column2" DataIndex="OldSetWeight" runat="server" Hideable="true"/>--%>
                                    <ext:Column ID="Column1" DataIndex="RecipeMaterialCode" runat="server" Text="炭黑名称" Hideable="false" Sortable="false" Flex="1" SummaryType="Count">
                                        <Editor>
                                            <ext:ComboBox ID="MaterialCodeCarbo" runat="server" MinChars="1" ValueField="field1" DisplayField="field2"
                                                EmptyText="输入关键词" TypeAhead="false" HideBaseTrigger="true" AllowBlank="false">
                                                <Store>
                                                    <ext:Store ID="ComboBoxSearchStore0" runat="server" OnReadData="ComboBoxSearchStore_ReadData">
                                                        <Proxy>
                                                            <ext:PageProxy>
                                                                <Reader>
                                                                    <ext:ArrayReader />
                                                                </Reader>
                                                            </ext:PageProxy>
                                                        </Proxy>
                                                        <Model>
                                                            <ext:Model ID="Model1" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="field1" Mapping="MaterialCode" />
                                                                    <ext:ModelField Name="field2" Mapping="MaterialName" />
                                                                    <ext:ModelField Name="RecipeEquipCode" Mapping="MaterialLevel" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <ListConfig LoadingText="查询中...">
                                                </ListConfig>
                                            </ext:ComboBox>
                                        </Editor>
                                        <Renderer Handler="return RendererGridColumnComboBoxSearch(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                    </ext:Column>
                                    <ext:Column ID="RecipeMaterialCode" DataIndex="MaterialCode" runat="server" Width="160" Text="物料编码"  Sortable="false">
                                    </ext:Column>
                                    <ext:Column ID="RecipeEquipCodeCarbon" DataIndex="RecipeEquipCode" runat="server" Width="80" Text="物料级别" Hideable="false" Sortable="false">
                                    </ext:Column>
                                      <ext:Column ID="Column9" DataIndex="Supply_code" runat="server" Width="80" Text="原料厂商" Hideable="false" Sortable="false">
                                    </ext:Column>
                                    <ext:Column ID="ActCodeCarbon" DataIndex="ActCode" runat="server" Text="称量动作" Hideable="false" Sortable="false" Width="100">
                                        <Editor>
                                            <ext:ComboBox ID="setActCodeCarbon" runat="server" SelectOnTab="true" Editable="false">
                                            </ext:ComboBox>
                                        </Editor>
                                        <Renderer Handler="return RendererGridColumnComboBox(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                    </ext:Column>
                                    <ext:Column ID="SetWeightCarbon" DataIndex="SetWeight" runat="server" Text="设定重量" Hideable="false" Sortable="false" Width="80" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return  parseFloat(value).toFixed(1).toString() +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField3" runat="server" AllowBlank="false" AllowNegative="false" DecimalPrecision="3" StyleSpec="text-align:left" />
                                        </Editor>
                                    </ext:Column>
                                    
                                    <ext:Column ID="ErrorAllowCarbon" DataIndex="ErrorAllow" runat="server" Width="80" Text="允许误差" Hideable="false" Sortable="false" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return  parseFloat(value).toFixed(2).toString() +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField1" runat="server" AllowBlank="false" AllowNegative="false" DecimalPrecision="3" StyleSpec="text-align:left" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CommandColumn ID="CommandColumnCarbon" DataIndex="CommandColumnCarbon" runat="server" Width="180" Text="操作" Align="Center" SummaryType="Count">
                                        <Commands>
                                            <ext:GridCommand Icon="TableEdit" CommandName="Add" Text="添加">
                                                <ToolTip Text="最后插入一条数据" />
                                            </ext:GridCommand>
                                            <ext:CommandSeparator />
                                            <ext:GridCommand Icon="TableEdit" CommandName="Insert" Text="插入">
                                                <ToolTip Text="此条之前插入一条数据" />
                                            </ext:GridCommand>
                                            <ext:CommandSeparator />
                                            <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                                <ToolTip Text="删除本条数据" />
                                            </ext:GridCommand>
                                        </Commands>
                                        <PrepareToolbar />
                                        <Listeners>
                                            <Command Handler="return commandcolumn_click(this,command, record);" />
                                        </Listeners>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                            <Plugins>
                                <ext:CellEditing ID="CellEditingCarbon" runat="server" ClicksToEdit="1">
                                    <Listeners>
                                        <BeforeEdit Fn="enableEdit"></BeforeEdit>
                                    </Listeners>
                                </ext:CellEditing>
                            </Plugins>
                            <SelectionModel>
                                <ext:CellSelectionModel ID="CellSelectionModel1" runat="server" />
                            </SelectionModel>
                            <View>
                                <ext:GridView ID="gridViewWeigthCarbon" runat="server">
                                    <Listeners>
                                        <Refresh Handler="updateTotal(this.panel, #{WeigthTotalCarbon});" />
                                    </Listeners>
                                    <KeyMap ID="KeyMap2" EventName="itemkeydown" runat="server" ComponentEvent="true">
                                        <ProcessEvent Fn="processEvent" />
                                        <Binding>
                                            <ext:KeyBinding Handler="KeyMapClick">
                                                <Keys>
                                                    <ext:Key Code="TAB" />
                                                </Keys>
                                            </ext:KeyBinding>
                                        </Binding>
                                    </KeyMap>
                                </ext:GridView>
                            </View>
                            <DockedItems>
                                <ext:Container ID="WeigthTotalCarbon" runat="server" Layout="HBoxLayout" Dock="Bottom" StyleSpec="margin-top:2px;">
                                    <Defaults>
                                        <ext:Parameter Name="height" Value="22" />
                                    </Defaults>
                                    <Items>
                                        <ext:DisplayField ID="dfMaterialCodeCarbon" runat="server" Name="RecipeMaterialCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="DisplayField1" runat="server" Name="MaterialCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfRecipeEquipCodeCarbon" runat="server" Name="RecipeEquipCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfActCodeCodeCarbon" runat="server" Name="ActCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfSetWeightCodeCarbon" runat="server" Name="SetWeight" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfErrorAllowCodeCarbon" runat="server" Name="ErrorAllow" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfCommandColumnCarbon" runat="server" Name="CommandColumnCarbon" Cls="total-field" Text="-" />
                                                           <ext:DisplayField ID="DisplayField7" runat="server" Name="Supply_code" Cls="total-field" Text="-" />
                                           </Items>
                                </ext:Container>
                            </DockedItems>
                        </ext:GridPanel>

                        <ext:GridPanel ID="gridPanelWeightOil1" runat="server" Flex="1" Frame="true" Title="油称(1)称量信息">
                            <Store>
                                <ext:Store ID="storeWeightOil1" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.WeightGridPanelBindData" PageParam="Page@1" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="ModelOil1" runat="server" Name="gridPanelWeightOil1Model">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="WeightType" />
                                                <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                <ext:ModelField Name="MaterialCode" Type="String" />
                                                <ext:ModelField Name="MaterialName" Type="String" />
                                                <ext:ModelField Name="ActCode" Type="String" />
                                                <ext:ModelField Name="SetWeight" Type="Float" />
                                                <ext:ModelField Name="OldSetWeight" Type="Float" />
                                                <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                <ext:ModelField Name="RecipeEquipCode" Type="String" />
                                                     <ext:ModelField Name="Supply_code" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModelOil1" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumColOil1" runat="server" Width="30" />
                                    <ext:Column ID="MaterialCodeOil1" DataIndex="RecipeMaterialCode" runat="server" Text="油料名称" Hideable="false" Sortable="false" Flex="1" SummaryType="Count">
                                        <Editor>
                                            <ext:ComboBox ID="setMaterialCodeOil1" runat="server" MinChars="1" ValueField="field1" DisplayField="field2"
                                                EmptyText="输入关键词" TypeAhead="false" HideBaseTrigger="true" AllowBlank="false">
                                                <Store>
                                                    <ext:Store ID="ComboBoxSearchStore1" runat="server" OnReadData="ComboBoxSearchStore_ReadData">
                                                        <Proxy>
                                                            <ext:PageProxy>
                                                                <Reader>
                                                                    <ext:ArrayReader />
                                                                </Reader>
                                                            </ext:PageProxy>
                                                        </Proxy>
                                                        <Model>
                                                            <ext:Model ID="Model2" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="field1" Mapping="MaterialCode" />
                                                                    <ext:ModelField Name="field2" Mapping="MaterialName" />
                                                                    <ext:ModelField Name="RecipeEquipCode" Mapping="MaterialLevel" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <ListConfig LoadingText="查询中...">
                                                </ListConfig>
                                            </ext:ComboBox>
                                        </Editor>
                                        <Renderer Handler="return RendererGridColumnComboBoxSearch(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                    </ext:Column>
                                    <ext:Column ID="Column3" DataIndex="MaterialCode" runat="server" Width="160" Text="物料编码"  Sortable="false">
                                    </ext:Column>
                                    <ext:Column ID="MaterialLevelOil1" DataIndex="RecipeEquipCode" runat="server" Width="80" Text="物料级别" Hideable="false" Sortable="false">
                                    </ext:Column>
                                      <ext:Column ID="Column10" DataIndex="Supply_code" runat="server" Width="80" Text="原料厂商" Hideable="false" Sortable="false">
                                    </ext:Column>
                                    <ext:Column ID="ActCodeOil1" DataIndex="ActCode" runat="server" Text="称量动作" Hideable="false" Sortable="false" Width="120">
                                        <Editor>
                                            <ext:ComboBox ID="setActCodeOil1" runat="server" SelectOnTab="true" Editable="false">
                                            </ext:ComboBox>
                                        </Editor>
                                        <Renderer Handler="return RendererGridColumnComboBox(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                    </ext:Column>
                                    <ext:Column ID="SetWeightOil1" DataIndex="SetWeight" runat="server" Text="设定重量" Hideable="false" Sortable="false" Width="80" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return  parseFloat(value).toFixed(2).toString() +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField2" runat="server" AllowBlank="false" AllowNegative="false" DecimalPrecision="3" StyleSpec="text-align:left" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ErrorAllowOil1" DataIndex="ErrorAllow" runat="server" Width="80" Text="允许误差" Hideable="false" Sortable="false" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return  parseFloat(value).toFixed(3).toString() +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField4" runat="server" AllowBlank="false" AllowNegative="false" DecimalPrecision="3" StyleSpec="text-align:left" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CommandColumn ID="CommandColumnOil1" DataIndex="CommandColumnOil1" runat="server" Width="180" Text="操作" Align="Center" SummaryType="Count">
                                        <Commands>
                                            <ext:GridCommand Icon="TableEdit" CommandName="Add" Text="添加">
                                                <ToolTip Text="最后插入一条数据" />
                                            </ext:GridCommand>
                                            <ext:CommandSeparator />
                                            <ext:GridCommand Icon="TableEdit" CommandName="Insert" Text="插入">
                                                <ToolTip Text="此条之前插入一条数据" />
                                            </ext:GridCommand>
                                            <ext:CommandSeparator />
                                            <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                                <ToolTip Text="删除本条数据" />
                                            </ext:GridCommand>
                                        </Commands>
                                        <PrepareToolbar />
                                        <Listeners>
                                            <Command Handler="return commandcolumn_click(this,command, record);" />
                                        </Listeners>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                            <Plugins>
                                <ext:CellEditing ID="CellEditingOil1" runat="server" ClicksToEdit="1">
                                    <Listeners>
                                        <BeforeEdit Fn="enableEdit"></BeforeEdit>
                                    </Listeners>
                                </ext:CellEditing>
                            </Plugins>
                            <SelectionModel>
                                <ext:CellSelectionModel ID="CellSelectionModel2" runat="server" />
                            </SelectionModel>
                            <View>
                                <ext:GridView ID="gridViewWeigthOil1" runat="server">
                                    <Listeners>
                                        <Refresh Handler="updateTotal(this.panel, #{WeigthTotalOil1});" />
                                    </Listeners>
                                    <KeyMap ID="KeyMap1" EventName="itemkeydown" runat="server" ComponentEvent="true">
                                        <ProcessEvent Fn="processEvent" />
                                        <Binding>
                                            <ext:KeyBinding Handler="KeyMapClick">
                                                <Keys>
                                                    <ext:Key Code="TAB" />
                                                </Keys>
                                            </ext:KeyBinding>
                                        </Binding>
                                    </KeyMap>
                                </ext:GridView>
                            </View>
                            <DockedItems>
                                <ext:Container ID="WeigthTotalOil1" runat="server" Layout="HBoxLayout" Dock="Bottom" StyleSpec="margin-top:2px;">
                                    <Defaults>
                                        <ext:Parameter Name="height" Value="22" />
                                    </Defaults>
                                    <Items>
                                        <ext:DisplayField ID="dfMaterialCodeOil1" runat="server" Name="RecipeMaterialCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="DisplayField2" runat="server" Name="MaterialCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfRecipeEquipCodeOil1" runat="server" Name="RecipeEquipCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfActCodeCodeOil1" runat="server" Name="ActCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfSetWeightCodeOil1" runat="server" Name="SetWeight" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfErrorAllowCodeOil1" runat="server" Name="ErrorAllow" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfCommandColumnOil1" runat="server" Name="CommandColumnOil1" Cls="total-field" Text="-" />
                                   <ext:DisplayField ID="DisplayField8" runat="server" Name="Supply_code" Cls="total-field" Text="-" />

                                    </Items>
                                </ext:Container>
                            </DockedItems>
                        </ext:GridPanel>

                        <ext:GridPanel ID="gridPanelWeightOil2" runat="server" Flex="1" Frame="true" Title="油称(2)称量信息">
                            <Store>
                                <ext:Store ID="storeWeightOil2" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.WeightGridPanelBindData" PageParam="Page@5" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="ModelOil2" runat="server" Name="gridPanelWeightOil2Model">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="WeightType" />
                                                <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                <ext:ModelField Name="MaterialCode" Type="String" />
                                                <ext:ModelField Name="MaterialName" Type="String" />
                                                <ext:ModelField Name="ActCode" Type="String" />
                                                <ext:ModelField Name="SetWeight" Type="Float" />
                                                <ext:ModelField Name="OldSetWeight" Type="Float" />
                                                <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                <ext:ModelField Name="RecipeEquipCode" Type="String" />
                                                     <ext:ModelField Name="Supply_code" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModelOil2" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumColOil2" runat="server" Width="30" />
                                    <ext:Column ID="MaterialCodeOil2" DataIndex="RecipeMaterialCode" runat="server" Text="油料名称" Hideable="false" Sortable="false" Flex="1" SummaryType="Count">
                                        <Editor>
                                            <ext:ComboBox ID="setMaterialCodeOil2" runat="server" MinChars="1" ValueField="field1" DisplayField="field2"
                                                EmptyText="输入关键词" TypeAhead="false" HideBaseTrigger="true" AllowBlank="false">
                                                <Store>
                                                    <ext:Store ID="ComboBoxSearchStore5" runat="server" OnReadData="ComboBoxSearchStore_ReadData">
                                                        <Proxy>
                                                            <ext:PageProxy>
                                                                <Reader>
                                                                    <ext:ArrayReader />
                                                                </Reader>
                                                            </ext:PageProxy>
                                                        </Proxy>
                                                        <Model>
                                                            <ext:Model ID="Model3" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="field1" Mapping="MaterialCode" />
                                                                    <ext:ModelField Name="field2" Mapping="MaterialName" />
                                                                    <ext:ModelField Name="RecipeEquipCode" Mapping="MaterialLevel" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <ListConfig LoadingText="查询中...">
                                                </ListConfig>
                                            </ext:ComboBox>
                                        </Editor>
                                        <Renderer Handler="return RendererGridColumnComboBoxSearch(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                    </ext:Column>
                                    <ext:Column ID="Column4" DataIndex="MaterialCode" runat="server" Width="160" Text="物料编码"  Sortable="false">
                                    </ext:Column>
                                    <ext:Column ID="MaterialLevelOil2" DataIndex="RecipeEquipCode" runat="server" Width="80" Text="物料级别" Hideable="false" Sortable="false">
                                    </ext:Column>
                                      <ext:Column ID="Column11" DataIndex="Supply_code" runat="server" Width="80" Text="原料厂商" Hideable="false" Sortable="false">
                                    </ext:Column>
                                    <ext:Column ID="ActCodeOil2" DataIndex="ActCode" runat="server" Text="称量动作" Hideable="false" Sortable="false" Width="120">
                                        <Editor>
                                            <ext:ComboBox ID="setActCodeOil2" runat="server" SelectOnTab="true" Editable="false">
                                            </ext:ComboBox>
                                        </Editor>
                                        <Renderer Handler="return RendererGridColumnComboBox(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                    </ext:Column>
                                    <ext:Column ID="SetWeightOil2" DataIndex="SetWeight" runat="server" Text="设定重量" Hideable="false" Sortable="false" Width="80" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return  parseFloat(value).toFixed(2).toString() +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField5" runat="server" AllowBlank="false" AllowNegative="false" DecimalPrecision="3" StyleSpec="text-align:left" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ErrorAllowOil2" DataIndex="ErrorAllow" runat="server" Width="80" Text="允许误差" Hideable="false" Sortable="false" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return  parseFloat(value).toFixed(3).toString() +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField6" runat="server" AllowBlank="false" AllowNegative="false" DecimalPrecision="3" StyleSpec="text-align:left" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CommandColumn ID="CommandColumnOil2" DataIndex="CommandColumnOil2" runat="server" Width="180" Text="操作" Align="Center" SummaryType="Count">
                                        <Commands>
                                            <ext:GridCommand Icon="TableEdit" CommandName="Add" Text="添加">
                                                <ToolTip Text="最后插入一条数据" />
                                            </ext:GridCommand>
                                            <ext:CommandSeparator />
                                            <ext:GridCommand Icon="TableEdit" CommandName="Insert" Text="插入">
                                                <ToolTip Text="此条之前插入一条数据" />
                                            </ext:GridCommand>
                                            <ext:CommandSeparator />
                                            <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                                <ToolTip Text="删除本条数据" />
                                            </ext:GridCommand>
                                        </Commands>
                                        <PrepareToolbar />
                                        <Listeners>
                                            <Command Handler="return commandcolumn_click(this,command, record);" />
                                        </Listeners>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                            <Plugins>
                                <ext:CellEditing ID="CellEditingOil2" runat="server" ClicksToEdit="1">
                                    <Listeners>
                                        <BeforeEdit Fn="enableEdit"></BeforeEdit>
                                    </Listeners>
                                </ext:CellEditing>
                            </Plugins>
                            <SelectionModel>
                                <ext:CellSelectionModel ID="CellSelectionModel3" runat="server" />
                            </SelectionModel>
                            <View>
                                <ext:GridView ID="gridViewWeigthOil2" runat="server">
                                    <Listeners>
                                        <Refresh Handler="updateTotal(this.panel, #{WeigthTotalOil2});" />
                                    </Listeners>
                                    <KeyMap ID="KeyMap3" EventName="itemkeydown" runat="server" ComponentEvent="true">
                                        <ProcessEvent Fn="processEvent" />
                                        <Binding>
                                            <ext:KeyBinding Handler="KeyMapClick">
                                                <Keys>
                                                    <ext:Key Code="TAB" />
                                                </Keys>
                                            </ext:KeyBinding>
                                        </Binding>
                                    </KeyMap>
                                </ext:GridView>
                            </View>
                            <DockedItems>
                                <ext:Container ID="WeigthTotalOil2" runat="server" Layout="HBoxLayout" Dock="Bottom" StyleSpec="margin-top:2px;">
                                    <Defaults>
                                        <ext:Parameter Name="height" Value="22" />
                                    </Defaults>
                                    <Items>
                                        <ext:DisplayField ID="dfMaterialCodeOil2" runat="server" Name="RecipeMaterialCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="DisplayField3" runat="server" Name="MaterialCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfRecipeEquipCodeOil2" runat="server" Name="RecipeEquipCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfActCodeCodeOil2" runat="server" Name="ActCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfSetWeightCodeOil2" runat="server" Name="SetWeight" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfErrorAllowCodeOil2" runat="server" Name="ErrorAllow" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfCommandColumnOil2" runat="server" Name="CommandColumnOil2" Cls="total-field" Text="-" />
                                           <ext:DisplayField ID="DisplayField9" runat="server" Name="Supply_code" Cls="total-field" Text="-" />

                                    </Items>
                                </ext:Container>
                            </DockedItems>
                        </ext:GridPanel>

                        <ext:GridPanel ID="gridPanelWeightRub" runat="server" Flex="1" Frame="true" Title="胶料称量信息">
                            <Store>
                                <ext:Store ID="storeWeightRub" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.WeightGridPanelBindData" PageParam="Page@2" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="ModelRub" runat="server" Name="gridPanelWeightRubModel">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="WeightType" />
                                                <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                <ext:ModelField Name="MaterialCode" Type="String" />
                                                <ext:ModelField Name="MaterialName" Type="String" />
                                                <ext:ModelField Name="ActCode" Type="String" />
                                                <ext:ModelField Name="OldSetWeight" Type="Float" />
                                                <ext:ModelField Name="SetWeight" Type="Float" />
                                                <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                <ext:ModelField Name="RecipeEquipCode" Type="String" />
                                                  <ext:ModelField Name="Into_type" Type="String" />
                                                    <ext:ModelField Name="Supply_code" Type="String" />
                                            
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModelRub" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumColRub" runat="server" Width="30" />
                                    <ext:Column ID="MaterialCodeRub" DataIndex="RecipeMaterialCode" runat="server" Text="胶料名称" Hideable="false" Sortable="false" Flex="1" SummaryType="Count">
                                        <Editor>
                                            <ext:ComboBox ID="setMaterialCodeRub" runat="server" MinChars="1" ValueField="field1" DisplayField="field2"
                                                EmptyText="输入关键词" TypeAhead="false" HideBaseTrigger="true" AllowBlank="false">
                                                <Store>
                                                    <ext:Store ID="ComboBoxSearchStore2" runat="server" OnReadData="ComboBoxSearchStore_ReadData">
                                                        <Proxy>
                                                            <ext:PageProxy>
                                                                <Reader>
                                                                    <ext:ArrayReader />
                                                                </Reader>
                                                            </ext:PageProxy>
                                                        </Proxy>
                                                        <Model>
                                                            <ext:Model ID="Model5" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="field1" Mapping="MaterialCode" />
                                                                    <ext:ModelField Name="field2" Mapping="MaterialName" />
                                                                    <ext:ModelField Name="RecipeEquipCode" Mapping="MaterialLevel" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <ListConfig LoadingText="查询中...">
                                                </ListConfig>
                                            </ext:ComboBox>
                                        </Editor>
                                        <Renderer Handler="return RendererGridColumnComboBoxSearch(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                    </ext:Column>
                                       <ext:Column ID="Column7" DataIndex="Into_type" runat="server" Text="投入类型" Hideable="false" Sortable="false" Width="120">
                                        <Editor>
                                            <ext:ComboBox ID="ComboBox1" runat="server" SelectOnTab="true" Editable="false">
                                            </ext:ComboBox>
                                        </Editor>
                                   <Renderer Handler="return RendererGridColumnComboBox(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                            </ext:Column>
                                    <ext:Column ID="Column5" DataIndex="MaterialCode" runat="server" Width="160" Text="物料编码"  Sortable="false">
                                    </ext:Column>
                                    <ext:Column ID="MaterialLevelRub" DataIndex="RecipeEquipCode" runat="server" Width="80" Text="物料级别" Hideable="false" Sortable="false">
                                    </ext:Column>
                                     <ext:Column ID="Column8" DataIndex="Supply_code" runat="server" Width="80" Text="原料厂商" Hideable="false" Sortable="false">
                                    </ext:Column>
                                    <ext:Column ID="ActCodeRub" DataIndex="ActCode" runat="server" Text="称量动作" Hideable="false" Sortable="false" Width="100">
                                        <Editor>
                                            <ext:ComboBox ID="setActCodeRub" runat="server" SelectOnTab="true" Editable="false">
                                            </ext:ComboBox>
                                        </Editor>
                                        <Renderer Handler="return RendererGridColumnComboBox(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                    </ext:Column>
                                    <ext:Column ID="SetWeightRub" DataIndex="SetWeight" runat="server" Text="设定重量" Hideable="false" Sortable="false" Width="80" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return  parseFloat(value).toFixed(1).toString() +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField7" runat="server" AllowBlank="false" AllowNegative="false" DecimalPrecision="3" StyleSpec="text-align:left" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ErrorAllowRub" DataIndex="ErrorAllow" runat="server" Width="80" Text="允许误差" Hideable="false" Sortable="false" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return  parseFloat(value).toFixed(1).toString() +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField8" runat="server" AllowBlank="false" AllowNegative="false" DecimalPrecision="3" StyleSpec="text-align:left" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CommandColumn ID="CommandColumnRub" DataIndex="CommandColumnRub" runat="server" Width="180" Text="操作" Align="Center" SummaryType="Count">
                                        <Commands>
                                            <ext:GridCommand Icon="TableEdit" CommandName="Add" Text="添加">
                                                <ToolTip Text="最后插入一条数据" />
                                            </ext:GridCommand>
                                            <ext:CommandSeparator />
                                            <ext:GridCommand Icon="TableEdit" CommandName="Insert" Text="插入">
                                                <ToolTip Text="此条之前插入一条数据" />
                                            </ext:GridCommand>
                                            <ext:CommandSeparator />
                                            <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                                <ToolTip Text="删除本条数据" />
                                            </ext:GridCommand>
                                        </Commands>
                                        <PrepareToolbar />
                                        <Listeners>
                                            <Command Handler="return commandcolumn_click(this,command, record);" />
                                        </Listeners>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                            <Plugins>
                                <ext:CellEditing ID="CellEditingRub" runat="server" ClicksToEdit="1">
                                    <Listeners>
                                        <BeforeEdit Fn="enableEdit"></BeforeEdit>
                                    </Listeners>
                                </ext:CellEditing>
                            </Plugins>
                            <SelectionModel>
                                <ext:CellSelectionModel ID="CellSelectionModel4" runat="server" />
                            </SelectionModel>
                            <View>
                                <ext:GridView ID="gridViewWeigthRub" runat="server">
                                    <Listeners>
                                        <Refresh Handler="updateTotal(this.panel, #{WeigthTotalRub});" />
                                    </Listeners>
                                    <KeyMap ID="KeyMap4" EventName="itemkeydown" runat="server" ComponentEvent="true">
                                        <ProcessEvent Fn="processEvent" />
                                        <Binding>
                                            <ext:KeyBinding Handler="KeyMapClick">
                                                <Keys>
                                                    <ext:Key Code="TAB" />
                                                </Keys>
                                            </ext:KeyBinding>
                                        </Binding>
                                    </KeyMap>
                                </ext:GridView>
                            </View>
                            <DockedItems>
                                <ext:Container ID="WeigthTotalRub" runat="server" Layout="HBoxLayout" Dock="Bottom" StyleSpec="margin-top:2px;">
                                    <Defaults>
                                        <ext:Parameter Name="height" Value="22" />
                                    </Defaults>
                                    <Items>
                                        <ext:DisplayField ID="dfMaterialCodeRub" runat="server" Name="RecipeMaterialCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="DisplayField1MaterialCode" runat="server" Name="MaterialCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfRecipeEquipCodeRub" runat="server" Name="RecipeEquipCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="DisplayField6" runat="server" Name="Supply_code" Cls="total-field" Text="-" />
                                           <ext:DisplayField ID="dfInto_typeCodeRub" runat="server" Name="Into_type" Cls="total-field" Text="-" />
                                          <ext:DisplayField ID="dfActCodeCodeRub" runat="server" Name="ActCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfSetWeightCodeRub" runat="server" Name="SetWeight" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfErrorAllowCodeRub" runat="server" Name="ErrorAllow" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfCommandColumnRub" runat="server" Name="CommandColumnRub" Cls="total-field" Text="-" />
                                    </Items>
                                </ext:Container>
                            </DockedItems>
                        </ext:GridPanel>

                        <ext:GridPanel ID="gridPanelWeightPowderPackage" runat="server" Flex="1" Frame="true" Title="小料校核称量信息">
                            <Store>
                                <ext:Store ID="storeWeightPowderPackage" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.WeightGridPanelBindData" PageParam="Page@3" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="ModelPowderPackage" runat="server" Name="gridPanelWeightPowderPackageModel">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="WeightType" />
                                                <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                <ext:ModelField Name="MaterialCode" Type="String" />
                                                <ext:ModelField Name="MaterialName" Type="String" />
                                                <ext:ModelField Name="ActCode" Type="String" />
                                                <ext:ModelField Name="SetWeight" Type="Float" />
                                                <ext:ModelField Name="OldSetWeight" Type="Float" />
                                                <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                <ext:ModelField Name="RecipeEquipCode" Type="String" />
                                                     <ext:ModelField Name="Supply_code" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModelPowderPackage" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumColPowderPackage" runat="server" Width="30" />
                                    <ext:Column ID="MaterialCodePowderPackage" DataIndex="RecipeMaterialCode" runat="server" Text="小料名称" Hideable="false" Sortable="false" Flex="1" SummaryType="Count">
                                        <Editor>
                                            <ext:ComboBox ID="setMaterialCodePowderPackage" runat="server" MinChars="1" ValueField="field1" DisplayField="field2"
                                                EmptyText="输入关键词" TypeAhead="false" HideBaseTrigger="true" AllowBlank="false">
                                                <Store>
                                                    <ext:Store ID="ComboBoxSearchStore3" runat="server" OnReadData="ComboBoxSearchStore_ReadData">
                                                        <Proxy>
                                                            <ext:PageProxy>
                                                                <Reader>
                                                                    <ext:ArrayReader />
                                                                </Reader>
                                                            </ext:PageProxy>
                                                        </Proxy>
                                                        <Model>
                                                            <ext:Model ID="Model6" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="field1" Mapping="MaterialCode" />
                                                                    <ext:ModelField Name="field2" Mapping="MaterialName" />
                                                                    <ext:ModelField Name="RecipeEquipCode" Mapping="MaterialLevel" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <ListConfig LoadingText="查询中...">
                                                </ListConfig>
                                            </ext:ComboBox>
                                        </Editor>
                                        <Renderer Handler="return RendererGridColumnComboBoxSearch(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                    </ext:Column>
                                    <ext:Column ID="Column2" DataIndex="MaterialCode" runat="server" Width="160" Text="物料编码"  Sortable="false">
                                    </ext:Column>
                                    <ext:Column ID="MaterialLevelPowderPackage" DataIndex="RecipeEquipCode" runat="server" Width="80" Text="物料级别" Hideable="false" Sortable="false">
                                    </ext:Column>
                                      <ext:Column ID="Column12" DataIndex="Supply_code" runat="server" Width="80" Text="原料厂商" Hideable="false" Sortable="false">
                                    </ext:Column>
                                     <ext:Column ID="ActCodePowderPackage" DataIndex="ActCode" runat="server" Text="称量动作" Hideable="false" Sortable="false" Width="120">
                                        <Editor>
                                            <ext:ComboBox ID="setActCodePowderPackage" runat="server" SelectOnTab="true" Editable="false">
                                            </ext:ComboBox>
                                        </Editor>
                                        <Renderer Handler="return RendererGridColumnComboBox(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                    </ext:Column>

                                    <ext:Column ID="SetWeightPowderPackage" DataIndex="SetWeight" runat="server" Text="设定重量" Hideable="false" Sortable="false" Width="80" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return parseFloat(value).toFixed(2).toString() +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField11" runat="server" AllowBlank="false" AllowNegative="false" DecimalPrecision="3" StyleSpec="text-align:left" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ErrorAllowPowderPackage" DataIndex="ErrorAllow" runat="server" Width="80" Text="允许误差" Hideable="false" Sortable="false" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return  parseFloat(value).toFixed(3).toString() +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField12" runat="server" AllowBlank="false" AllowNegative="false" DecimalPrecision="3" StyleSpec="text-align:left" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CommandColumn ID="CommandColumnPowderPackage" DataIndex="CommandColumnPowderPackage" runat="server" Width="180" Text="操作" Align="Center" SummaryType="Count">
                                        <Commands>
                                            <ext:GridCommand Icon="TableEdit" CommandName="Add" Text="添加">
                                                <ToolTip Text="最后插入一条数据" />
                                            </ext:GridCommand>
                                            <ext:CommandSeparator />
                                            <ext:GridCommand Icon="TableEdit" CommandName="Insert" Text="插入">
                                                <ToolTip Text="此条之前插入一条数据" />
                                            </ext:GridCommand>
                                            <ext:CommandSeparator />
                                            <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                                <ToolTip Text="删除本条数据" />
                                            </ext:GridCommand>
                                        </Commands>
                                        <PrepareToolbar />
                                        <Listeners>
                                            <Command Handler="return commandcolumn_click(this,command, record);" />
                                        </Listeners>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                            <Plugins>
                                <ext:CellEditing ID="CellEditingPowderPackage" runat="server" ClicksToEdit="1">
                                    <Listeners>
                                        <BeforeEdit Fn="enableEdit"></BeforeEdit>
                                    </Listeners>
                                </ext:CellEditing>
                            </Plugins>
                            <SelectionModel>
                                <ext:CellSelectionModel ID="CellSelectionModel5" runat="server" />
                            </SelectionModel>
                            <View>
                                <ext:GridView ID="gridViewWeigthPowderPackage" runat="server">
                                    <Listeners>
                                        <Refresh Handler="updateTotal(this.panel, #{WeigthTotalPowderPackage});" />
                                    </Listeners>
                                    <KeyMap ID="KeyMap5" EventName="itemkeydown" runat="server" ComponentEvent="true">
                                        <ProcessEvent Fn="processEvent" />
                                        <Binding>
                                            <ext:KeyBinding Handler="KeyMapClick">
                                                <Keys>
                                                    <ext:Key Code="TAB" />
                                                </Keys>
                                            </ext:KeyBinding>
                                        </Binding>
                                    </KeyMap>
                                </ext:GridView>
                            </View>
                            <DockedItems>
                                <ext:Container ID="WeigthTotalPowderPackage" runat="server" Layout="HBoxLayout" Dock="Bottom" StyleSpec="margin-top:2px;">
                                    <Defaults>
                                        <ext:Parameter Name="height" Value="22" />
                                    </Defaults>
                                    <Items>
                                        <ext:DisplayField ID="dfMaterialCodePowderPackage" runat="server" Name="RecipeMaterialCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="DisplayField4" runat="server" Name="MaterialCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfRecipeEquipCodePowderPackage" runat="server" Name="RecipeEquipCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfActCodeCodePowderPackage" runat="server" Name="ActCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfSetWeightCodePowderPackage" runat="server" Name="SetWeight" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfErrorAllowCodePowderPackage" runat="server" Name="ErrorAllow" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfCommandColumnPowderPackage" runat="server" Name="CommandColumnPowderPackage" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="DisplayField10" runat="server" Name="Supply_code" Cls="total-field" Text="-" />

                                    </Items>
                                </ext:Container>
                            </DockedItems>
                        </ext:GridPanel>

                        <ext:GridPanel ID="gridPanelWeightPowder" runat="server" Flex="1" Frame="true" Title="小料称量信息">
                            <Store>
                                <ext:Store ID="storeWeightPowder" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.WeightGridPanelBindData" PageParam="Page@9" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="ModelPowder" runat="server" Name="gridPanelWeightPowderModel">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="WeightType" />
                                                <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                <ext:ModelField Name="MaterialCode" Type="String" />
                                                <ext:ModelField Name="MaterialName" Type="String" />
                                                <ext:ModelField Name="ActCode" Type="String" />
                                                <ext:ModelField Name="SetWeight" Type="Float" />
                                                <ext:ModelField Name="OldSetWeight" Type="Float" />
                                                <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                <ext:ModelField Name="RecipeEquipCode" Type="String" />
                                                  <ext:ModelField Name="Supply_code" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModelPowder" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumColPowder" runat="server" Width="30" />
                                    <ext:Column ID="MaterialCodePowder" DataIndex="RecipeMaterialCode" runat="server" Text="小料名称" Hideable="false" Sortable="false" Flex="1" SummaryType="Count">
                                        <Editor>
                                            <ext:ComboBox ID="setMaterialCodePowder" runat="server" MinChars="1" ValueField="field1" DisplayField="field2"
                                                EmptyText="输入关键词" TypeAhead="false" HideBaseTrigger="true" AllowBlank="false">
                                                <Store>
                                                    <ext:Store ID="ComboBoxSearchStore9" runat="server" OnReadData="ComboBoxSearchStore_ReadData">
                                                        <Proxy>
                                                            <ext:PageProxy>
                                                                <Reader>
                                                                    <ext:ArrayReader />
                                                                </Reader>
                                                            </ext:PageProxy>
                                                        </Proxy>
                                                        <Model>
                                                            <ext:Model ID="Model4" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="field1" Mapping="MaterialCode" />
                                                                    <ext:ModelField Name="field2" Mapping="MaterialName" />
                                                                    <ext:ModelField Name="RecipeEquipCode" Mapping="MaterialLevel" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <ListConfig LoadingText="查询中...">
                                                </ListConfig>
                                            </ext:ComboBox>
                                        </Editor>
                                        <Renderer Handler="return RendererGridColumnComboBoxSearch(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                    </ext:Column>
                                    <ext:Column ID="Column6" DataIndex="MaterialCode" runat="server" Width="160" Text="物料编码"  Sortable="false">
                                    </ext:Column>
                                    <ext:Column ID="MaterialLevelPowder" DataIndex="RecipeEquipCode" runat="server" Width="80" Text="物料级别" Hideable="false" Sortable="false">
                                    </ext:Column>
                                      <ext:Column ID="Column13" DataIndex="Supply_code" runat="server" Width="80" Text="原料厂商" Hideable="false" Sortable="false">
                                    </ext:Column>
                                    <ext:Column ID="ActCodePowder" DataIndex="ActCode" runat="server" Text="称量动作" Hideable="false" Sortable="false" Width="120">
                                        <Renderer Handler="return '称量'"></Renderer>
                                    </ext:Column>
                                    <ext:Column ID="SetWeightPowder" DataIndex="SetWeight" runat="server" Text="设定重量" Hideable="false" Sortable="false" Width="80" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return  parseFloat(value).toFixed(2).toString() +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField9" runat="server" AllowBlank="false" AllowNegative="false" DecimalPrecision="3" StyleSpec="text-align:left" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ErrorAllowPowder" DataIndex="ErrorAllow" runat="server" Width="80" Text="允许误差" Hideable="false" Sortable="false" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return  parseFloat(value).toFixed(3).toString() +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField10" runat="server" AllowBlank="false" AllowNegative="false" DecimalPrecision="3" StyleSpec="text-align:left" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CommandColumn ID="CommandColumnPowder" DataIndex="CommandColumnPowder" runat="server" Width="180" Text="操作" Align="Center" SummaryType="Count">
                                        <Commands>
                                            <ext:GridCommand Icon="TableEdit" CommandName="Add" Text="添加">
                                                <ToolTip Text="最后插入一条数据" />
                                            </ext:GridCommand>
                                            <ext:CommandSeparator />
                                            <ext:GridCommand Icon="TableEdit" CommandName="Insert" Text="插入">
                                                <ToolTip Text="此条之前插入一条数据" />
                                            </ext:GridCommand>
                                            <ext:CommandSeparator />
                                            <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                                <ToolTip Text="删除本条数据" />
                                            </ext:GridCommand>
                                        </Commands>
                                        <PrepareToolbar />
                                        <Listeners>
                                            <Command Handler="return commandcolumn_click(this,command, record);" />
                                        </Listeners>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                            <Plugins>
                                <ext:CellEditing ID="CellEditingPowder" runat="server" ClicksToEdit="1">
                                    <Listeners>
                                        <BeforeEdit Fn="enableEdit"></BeforeEdit>
                                    </Listeners>
                                </ext:CellEditing>
                            </Plugins>
                            <SelectionModel>
                                <ext:CellSelectionModel ID="CellSelectionModel6" runat="server" />
                            </SelectionModel>
                            <View>
                                <ext:GridView ID="gridViewWeigthPowder" runat="server">
                                    <Listeners>
                                        <Refresh Handler="updateTotal(this.panel, #{WeigthTotalPowder});" />
                                    </Listeners>
                                    <KeyMap ID="KeyMap6" EventName="itemkeydown" runat="server" ComponentEvent="true">
                                        <ProcessEvent Fn="processEvent" />
                                        <Binding>
                                            <ext:KeyBinding Handler="KeyMapClick">
                                                <Keys>
                                                    <ext:Key Code="TAB" />
                                                </Keys>
                                            </ext:KeyBinding>
                                        </Binding>
                                    </KeyMap>
                                </ext:GridView>
                            </View>
                            <DockedItems>
                                <ext:Container ID="WeigthTotalPowder" runat="server" Layout="HBoxLayout" Dock="Bottom" StyleSpec="margin-top:2px;">
                                    <Defaults>
                                        <ext:Parameter Name="height" Value="22" />
                                    </Defaults>
                                    <Items>
                                        <ext:DisplayField ID="dfMaterialCodePowder" runat="server" Name="RecipeMaterialCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="DisplayField5" runat="server" Name="MaterialCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfRecipeEquipCodePowder" runat="server" Name="RecipeEquipCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfActCodeCodePowder" runat="server" Name="ActCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfSetWeightCodePowder" runat="server" Name="SetWeight" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfErrorAllowCodePowder" runat="server" Name="ErrorAllow" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfCommandColumnPowder" runat="server" Name="CommandColumnPowder" Cls="total-field" Text="-" />
                                       <ext:DisplayField ID="DisplayField11" runat="server" Name="Supply_code" Cls="total-field" Text="-" />
                                        
                                         </Items>
                                </ext:Container>
                            </DockedItems>
                        </ext:GridPanel>




                        <ext:GridPanel ID="gridPanelWeightPowder2" runat="server" Flex="1" Frame="true" Title="小料检量称量信息">
                            <Store>
                                <ext:Store ID="storeWeightPowder2" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.WeightGridPanelBindData" PageParam="Page@22"/>
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="ModelPowder2" runat="server" Name="gridPanelWeightPowderModel">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="WeightType" />
                                                <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                <ext:ModelField Name="MaterialCode" Type="String" />
                                                <ext:ModelField Name="MaterialName" Type="String" />
                                                <ext:ModelField Name="ActCode" Type="String" />
                                                <ext:ModelField Name="SetWeight" Type="Float" />
                                                <ext:ModelField Name="OldSetWeight" Type="Float" />
                                                <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                <ext:ModelField Name="RecipeEquipCode" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModelPowder2" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumColPowder2" runat="server" Width="30" />
                                    <ext:Column ID="MaterialCodePowder2" DataIndex="RecipeMaterialCode" runat="server" Text="小料名称" Hideable="false" Sortable="false" Flex="1" SummaryType="Count">
                                    </ext:Column>
                                    <ext:Column ID="Column62" DataIndex="MaterialCode" runat="server" Width="160" Text="物料编码"  Sortable="false">
                                    </ext:Column>
                                    <ext:Column ID="MaterialLevelPowder2" DataIndex="RecipeEquipCode" runat="server" Width="160" Text="物料级别" Hideable="false" Sortable="false">
                                    </ext:Column>
                                    <ext:Column ID="ActCodePowder2" DataIndex="ActCode" runat="server" Text="称量动作" Hideable="false" Sortable="false" Width="120">
                                        <Renderer Handler="return '称量'"></Renderer>
                                    </ext:Column>
                                    <ext:Column ID="SetWeightPowder2" DataIndex="SetWeight" runat="server" Text="设定重量" Hideable="false" Sortable="false" Width="80" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return  parseFloat(value).toFixed(3).toString() +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField92" runat="server" AllowBlank="false" AllowNegative="false" DecimalPrecision="3" StyleSpec="text-align:left"  />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ErrorAllowPowder2" DataIndex="ErrorAllow" runat="server" Width="80" Text="允许误差" Hideable="false" Sortable="false" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return  parseFloat(value).toFixed(3).toString() +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField102" runat="server" AllowBlank="false" AllowNegative="false" DecimalPrecision="3" StyleSpec="text-align:left" />
                                        </Editor>
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <Plugins>
                                <ext:CellEditing ID="CellEditingPowder2" runat="server" ClicksToEdit="1">
                                    <Listeners>
                                        <BeforeEdit Fn="enableEdit"></BeforeEdit>
                                    </Listeners>
                                </ext:CellEditing>
                            </Plugins>
                            <SelectionModel>
                                <ext:CellSelectionModel runat="server" />
                            </SelectionModel>
                            <View>
                                <ext:GridView ID="gridViewWeigthPowder2" runat="server">
                                    <Listeners>
                                        <Refresh Handler="updateTotal(this.panel, #{WeigthTotalPowder2});" />
                                    </Listeners>
                                    <KeyMap ID="KeyMap62" EventName="itemkeydown" runat="server" ComponentEvent="true">
                                        <ProcessEvent Fn="processEvent" />
                                        <Binding>
                                            <ext:KeyBinding Handler="KeyMapClick">
                                                <Keys>
                                                    <ext:Key Code="TAB" />
                                                </Keys>
                                            </ext:KeyBinding>
                                        </Binding>
                                    </KeyMap>
                                </ext:GridView>
                            </View>
                            <DockedItems>
                                <ext:Container ID="WeigthTotalPowder2" runat="server" Layout="HBoxLayout" Dock="Bottom" StyleSpec="margin-top:2px;">
                                    <Defaults>
                                        <ext:Parameter Name="height" Value="22" />
                                    </Defaults>
                                    <Items>
                                        <ext:DisplayField ID="dfMaterialCodePowder2" runat="server" Name="RecipeMaterialCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="DisplayField52" runat="server" Name="MaterialCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfRecipeEquipCodePowder2" runat="server" Name="RecipeEquipCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfActCodeCodePowder2" runat="server" Name="ActCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfSetWeightCodePowder2" runat="server" Name="SetWeight" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfErrorAllowCodePowder2" runat="server" Name="ErrorAllow" Cls="total-field" Text="-" />
                                    </Items>
                                </ext:Container>
                            </DockedItems>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
