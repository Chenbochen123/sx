<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_Manage_MaterialRecipeDetail_AdvanceSeparateWeight, App_Web_a5rwyiqa" %>

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
    </script>

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>AdvanceSeparateWeight.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="pnlWeightAdvanceSeparate" runat="server" Region="Center" Header="false" Layout="AccordionLayout">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="Toolbar3">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Calculator" Text="计算重量" ID="Button2">
                                    <Listeners>
                                        <Click Handler="advanceSeparateWeightTotal();"></Click>
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
                        <ext:GridPanel ID="gridPanelWeightAdvanceSeparate" runat="server" Flex="1" Frame="true" Title="预分散称量信息">
                            <Store>
                                <ext:Store ID="storeWeightAdvanceSeparate" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.WeightGridPanelBindData" PageParam="Page@6" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="ModelAdvanceSeparate" runat="server" Name="gridPanelWeightAdvanceSeparateModel">
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
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModelAdvanceSeparate" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumAdvanceSeparate" runat="server" Width="30" />
                                    <ext:Column ID="Column1" DataIndex="RecipeMaterialCode" runat="server" Text="物料名称" Hideable="false" Sortable="false" Flex="1" SummaryType="Count">
                                        <Editor>
                                            <ext:ComboBox ID="MaterialCodeAdvanceSeparate" runat="server" MinChars="1" ValueField="field1" DisplayField="field2"
                                                EmptyText="输入关键词" TypeAhead="false" HideBaseTrigger="true" AllowBlank="false">
                                                <Store>
                                                    <ext:Store ID="ComboBoxSearchStore6" runat="server" OnReadData="ComboBoxSearchStore_ReadData">
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
                                    <ext:Column ID="RecipeEquipAdvanceSeparate" DataIndex="RecipeEquipCode" runat="server" Width="160" Text="物料级别" Hideable="false" Sortable="false">
                                    </ext:Column>
                                    <ext:Column ID="ActCodeAdvanceSeparate" DataIndex="ActCode" runat="server" Text="称量动作" Hideable="false" Sortable="false" Width="100" Hidden="true">
                                        <Editor>
                                            <ext:ComboBox ID="setActCodeAdvanceSeparate" runat="server" SelectOnTab="true" Editable="false">
                                            </ext:ComboBox>
                                        </Editor>
                                        <Renderer Handler="return RendererGridColumnComboBox(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                    </ext:Column>
                                    <ext:Column ID="SetWeightAdvanceSeparate" DataIndex="SetWeight" runat="server" Text="设定重量" Hideable="false" Sortable="false" Width="80" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return  parseFloat(value).toFixed(2).toString() +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField3" runat="server" AllowBlank="false" AllowNegative="false" DecimalPrecision="3" StyleSpec="text-align:left" />
                                        </Editor>
                                    </ext:Column>
                                    
                                    <ext:Column ID="ErrorAllowAdvanceSeparate" DataIndex="ErrorAllow" runat="server" Width="80" Text="允许误差" Hideable="false" Sortable="false" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return  parseFloat(value).toFixed(3).toString() +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField1" runat="server" AllowBlank="false" AllowNegative="false" DecimalPrecision="3" StyleSpec="text-align:left" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CommandColumn ID="CommandColumnAdvanceSeparate" DataIndex="CommandColumnAdvanceSeparate" runat="server" Width="180" Text="操作" Align="Center" SummaryType="Count">
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
                                <ext:CellEditing ID="CellEditingAdvanceSeparate" runat="server" ClicksToEdit="1">
                                    <Listeners>
                                        <BeforeEdit Fn="enableEdit"></BeforeEdit>
                                    </Listeners>
                                </ext:CellEditing>
                            </Plugins>
                            <SelectionModel>
                                <ext:CellSelectionModel ID="CellSelectionModel1" runat="server" />
                            </SelectionModel>
                            <View>
                                <ext:GridView ID="gridViewWeigthAdvanceSeparate" runat="server">
                                    <Listeners>
                                        <Refresh Handler="updateTotal(this.panel, #{WeigthTotalAdvanceSeparate});" />
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
                                <ext:Container ID="WeigthTotalAdvanceSeparate" runat="server" Layout="HBoxLayout" Dock="Bottom" StyleSpec="margin-top:2px;">
                                    <Defaults>
                                        <ext:Parameter Name="height" Value="22" />
                                    </Defaults>
                                    <Items>
                                        <ext:DisplayField ID="dfMaterialCodeAdvanceSeparate" runat="server" Name="RecipeMaterialCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfRecipeEquipCodeAdvanceSeparate" runat="server" Name="RecipeEquipCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfActCodeCodeAdvanceSeparate" runat="server" Name="ActCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfSetWeightCodeAdvanceSeparate" runat="server" Name="SetWeight" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfErrorAllowCodeAdvanceSeparate" runat="server" Name="ErrorAllow" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfCommandColumnAdvanceSeparate" runat="server" Name="CommandColumnAdvanceSeparate" Cls="total-field" Text="-" />
                                    </Items>
                                </ext:Container>
                            </DockedItems>
                        </ext:GridPanel>
                                    <ext:GridPanel ID="gridPanelWeightRub" runat="server" Flex="1" Frame="true" Title="返回胶称量信息">
                            <Store>
                                <ext:Store ID="storeWeightRub" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.WeightGridPanelBindData" PageParam="Page@7" />
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
                                    <ext:Column ID="Column5" DataIndex="MaterialCode" runat="server" Width="160" Text="物料编码"  Sortable="false">
                                    </ext:Column>
                                    <ext:Column ID="MaterialLevelRub" DataIndex="RecipeEquipCode" runat="server" Width="160" Text="物料级别" Hideable="false" Sortable="false">
                                    </ext:Column>
                                    <ext:Column ID="ActCodeRub" DataIndex="ActCode" runat="server" Text="称量动作" Hideable="false" Sortable="false" Width="120">
                                        <Editor>
                                            <ext:ComboBox ID="setActCodeRub" runat="server" SelectOnTab="true" Editable="false">
                                            </ext:ComboBox>
                                        </Editor>
                                        <Renderer Handler="return RendererGridColumnComboBox(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                    </ext:Column>
                                    <ext:Column ID="SetWeightRub" DataIndex="SetWeight" runat="server" Text="设定重量" Hideable="false" Sortable="false" Width="80" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return  parseFloat(value).toFixed(2).toString() +' 千克';}" />
                                        <Editor>
                                            <ext:NumberField ID="NumberField7" runat="server" AllowBlank="false" AllowNegative="false" DecimalPrecision="3" StyleSpec="text-align:left" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="ErrorAllowRub" DataIndex="ErrorAllow" runat="server" Width="80" Text="允许误差" Hideable="false" Sortable="false" SummaryType="Sum">
                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return  parseFloat(value).toFixed(3).toString() +' 千克';}" />
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
                                        <ext:DisplayField ID="dfActCodeCodeRub" runat="server" Name="ActCode" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfSetWeightCodeRub" runat="server" Name="SetWeight" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfErrorAllowCodeRub" runat="server" Name="ErrorAllow" Cls="total-field" Text="-" />
                                        <ext:DisplayField ID="dfCommandColumnRub" runat="server" Name="CommandColumnRub" Cls="total-field" Text="-" />
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

