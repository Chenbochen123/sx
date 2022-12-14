<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_Manage_MaterialRecipeDetail_OpenMixing, App_Web_a5rwyiqa" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>开炼信息模板</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />
    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>OpenMixing.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:TabPanel ID="pnlMixing" runat="server" Region="Center" Header="false" Layout="Fit">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="Toolbar2">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="PackageIn" Text="调用开炼模板" ID="Button7" Hidden = "true">
                                    <Listeners>
                                        <Click Handler="QueryPmtOpenActionModelMainInfo()"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" Icon="Reload" Text="刷新开炼信息" ID="Button8">
                                    <Listeners>
                                        <Click Handler="gridPanelRefresh(true)"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator1" />
                           
                                  <ext:Button runat="server" Icon="Disk" Text="复制开炼信息" ID="Button2">
                                    <Listeners>
                                        <Click Handler="getOpenMixingStore()"></Click>
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:GridPanel ID="gridPanelMinxing0" Region="Center" runat="server" Frame="true" Title="0#开炼机">
                                    <Store>
                                        <ext:Store ID="storeMinxing0" runat="server" PageSize="0" >
                                            <AutoLoadParams>
                                                <ext:Parameter Name="MixingNo" Value="0" Mode="Raw" />
                                            </AutoLoadParams>
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.MixingGridPanelBindData" />
                                            </Proxy>
                                            <Model>
                                                <ext:Model ID="model0" runat="server" Name="gridPanelOpenActionModel">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="RecipeObjID" />
                                                        <ext:ModelField Name="RecipeEquipCode" />
                                                        <ext:ModelField Name="RecipeMaterialCode" />
                                                        <ext:ModelField Name="RecipeVersionID" />
                                                        <ext:ModelField Name="MainModelID" />
                                                        <ext:ModelField Name="OpenMixingNo" />
                                                        <ext:ModelField Name="MixingStep" />
                                                        <ext:ModelField Name="OpenActionCode" />
                                                        <ext:ModelField Name="MixTime" />
                                                        <ext:ModelField Name="CoolMixSpeed" />
                                                        <ext:ModelField Name="OpenMixSpeed" />
                                                        <ext:ModelField Name="MixRollor" />
                                                        <ext:ModelField Name="WaterTemp" />
                                                        <ext:ModelField Name="RubberTemp" />
                                                        <ext:ModelField Name="CarSpeed" />
                                                          <ext:ModelField Name="SpeedDiff" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Listeners>
                                            </Listeners>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="columnModel" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="rowNum1" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="OpenActionCode0" DataIndex="OpenActionCode" runat="server" Text="动作" Align="Center" Width="150" Sortable="false" FocusOnToFront="true">
                                                <Editor>
                                                    <ext:ComboBox ID="setOpenActionCode0" runat="server" SelectOnTab="true" Editable="false">
                                                    </ext:ComboBox>
                                                </Editor>
                                                <Renderer Handler="return RendererGridColumnComboBox(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                            </ext:Column>
                                            <ext:Column ID="MixTime0" DataIndex="MixTime" runat="server" DecimalPrecision="0" Text="时间" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField2" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CoolMixSpeed0" DataIndex="CoolMixSpeed" runat="server" DecimalPrecision="0" Text="冷却鼓速度" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField4" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="OpenMixSpeed0" DataIndex="OpenMixSpeed" runat="server" DecimalPrecision="0" Text="开炼机速度" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField5" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="MixRollor0" DataIndex="MixRollor" runat="server" DecimalPrecision="0" Text="辊距" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField6" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="WaterTemp0" DataIndex="WaterTemp" runat="server" Text="水温" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField7" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="RubberTemp0" DataIndex="RubberTemp" runat="server" Text="胶温" Visible="false"  DecimalPrecision="0" Align="Center" Width="80" Sortable="false" ToolTip="单机表头可批量修改">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField8" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CarSpeed0" DataIndex="CarSpeed" runat="server" Text="小车速度" DecimalPrecision="0" Align="Center" Width="80" Sortable="false" ToolTip="单机表头可批量修改" Hidden="true">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField9" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                              <ext:Column ID="SpeedDiff0" DataIndex="SpeedDiff" runat="server" Text="速差比" DecimalPrecision="0" Align="Center" Width="80" Sortable="false" ToolTip="单机表头可批量修改">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField50" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:CommandColumn ID="CommandColumn1" runat="server" Width="120" Text="操作" Align="Center" Sortable="false">
                                                <Commands>
                                                    <ext:GridCommand Icon="TableEdit" CommandName="Insert" Text="插入">
                                                        <ToolTip Text="本条之前插入数据" />
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
                                    <SelectionModel>
                                        <ext:CellSelectionModel ID="CellSelectionModel1" runat="server" />
                                    </SelectionModel>
                                    <Plugins>
                                        <ext:CellEditing ID="cellEditing1" runat="server" ClicksToEdit="1">
                                            <Listeners>
                                                <BeforeEdit Fn="enableEdit"></BeforeEdit>
                                            </Listeners>
                                        </ext:CellEditing>
                                    </Plugins>
                                </ext:GridPanel>
                        <ext:GridPanel ID="gridPanelMinxing1" Region="Center" runat="server" Frame="true" Title="1#开炼机">
                                    <Store>
                                        <ext:Store ID="storeMinxing1" runat="server" PageSize="0" >
                                            <AutoLoadParams>
                                                <ext:Parameter Name="MixingNo" Value="1" Mode="Raw" />
                                            </AutoLoadParams>
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.MixingGridPanelBindData" />
                                            </Proxy>
                                            <Model>
                                                <ext:Model ID="model1" runat="server" Name="gridPanelOpenActionModel">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="RecipeObjID" />
                                                        <ext:ModelField Name="RecipeEquipCode" />
                                                        <ext:ModelField Name="RecipeMaterialCode" />
                                                        <ext:ModelField Name="RecipeVersionID" />
                                                        <ext:ModelField Name="MainModelID" />
                                                        <ext:ModelField Name="OpenMixingNo" />
                                                        <ext:ModelField Name="MixingStep" />
                                                        <ext:ModelField Name="OpenActionCode" />
                                                        <ext:ModelField Name="MixTime" />
                                                        <ext:ModelField Name="CoolMixSpeed" />
                                                        <ext:ModelField Name="OpenMixSpeed" />
                                                        <ext:ModelField Name="MixRollor" />
                                                        <ext:ModelField Name="WaterTemp" />
                                                        <ext:ModelField Name="RubberTemp" />
                                                        <ext:ModelField Name="CarSpeed" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Listeners>
                                            </Listeners>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="columnModel1" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="OpenActionCode1" DataIndex="OpenActionCode" runat="server" Text="动作" Align="Center" Width="150" Sortable="false" FocusOnToFront="true">
                                                <Editor>
                                                    <ext:ComboBox ID="setOpenActionCode1" runat="server" SelectOnTab="true" Editable="false">
                                                    </ext:ComboBox>
                                                </Editor>
                                                <Renderer Handler="return RendererGridColumnComboBox(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                            </ext:Column>
                                            <ext:Column ID="MixTime1" DataIndex="MixTime" runat="server" DecimalPrecision="0" Text="时间" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField1" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CoolMixSpeed1" DataIndex="CoolMixSpeed" runat="server" DecimalPrecision="0" Text="冷却鼓速度" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField3" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="OpenMixSpeed1" DataIndex="OpenMixSpeed" runat="server" DecimalPrecision="0" Text="开炼机速度" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField10" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="MixRollor1" DataIndex="MixRollor" runat="server" DecimalPrecision="0" Text="辊距" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField11" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="WaterTemp1" DataIndex="WaterTemp" runat="server" Text="水温" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField12" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="RubberTemp1" DataIndex="RubberTemp" runat="server" Text="胶温" Visible="false"  DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField13" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CarSpeed1" DataIndex="CarSpeed" runat="server" Text="小车速度" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改" Hidden="true">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField14" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:CommandColumn ID="CommandColumn2" runat="server" Width="120" Text="操作" Align="Center" Sortable="false">
                                                <Commands>
                                                    <ext:GridCommand Icon="TableEdit" CommandName="Insert" Text="插入">
                                                        <ToolTip Text="本条之前插入数据" />
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
                                    <SelectionModel>
                                        <ext:CellSelectionModel ID="CellSelectionModel2" runat="server" />
                                    </SelectionModel>
                                    <Plugins>
                                        <ext:CellEditing ID="cellEditing2" runat="server" ClicksToEdit="1">
                                            <Listeners>
                                                <BeforeEdit Fn="enableEdit"></BeforeEdit>
                                            </Listeners>
                                        </ext:CellEditing>
                                    </Plugins>
                                </ext:GridPanel>
                        <ext:GridPanel ID="gridPanelMinxing2" Region="Center" runat="server" Frame="true" Title="2#开炼机">
                                    <Store>
                                        <ext:Store ID="storeMinxing2" runat="server" PageSize="0" >
                                            <AutoLoadParams>
                                                <ext:Parameter Name="MixingNo" Value="2" Mode="Raw" />
                                            </AutoLoadParams>
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.MixingGridPanelBindData" />
                                            </Proxy>
                                            <Model>
                                                <ext:Model ID="model2" runat="server" Name="gridPanelOpenActionModel">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="RecipeObjID" />
                                                        <ext:ModelField Name="RecipeEquipCode" />
                                                        <ext:ModelField Name="RecipeMaterialCode" />
                                                        <ext:ModelField Name="RecipeVersionID" />
                                                        <ext:ModelField Name="MainModelID" />
                                                        <ext:ModelField Name="OpenMixingNo" />
                                                        <ext:ModelField Name="MixingStep" />
                                                        <ext:ModelField Name="OpenActionCode" />
                                                        <ext:ModelField Name="MixTime" />
                                                        <ext:ModelField Name="CoolMixSpeed" />
                                                        <ext:ModelField Name="OpenMixSpeed" />
                                                        <ext:ModelField Name="MixRollor" />
                                                        <ext:ModelField Name="WaterTemp" />
                                                        <ext:ModelField Name="RubberTemp" />
                                                        <ext:ModelField Name="CarSpeed" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Listeners>
                                            </Listeners>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="columnModel2" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn2" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="OpenActionCode2" DataIndex="OpenActionCode" runat="server" Text="动作" Align="Center" Width="150" Sortable="false" FocusOnToFront="true">
                                                <Editor>
                                                    <ext:ComboBox ID="setOpenActionCode2" runat="server" SelectOnTab="true" Editable="false">
                                                    </ext:ComboBox>
                                                </Editor>
                                                <Renderer Handler="return RendererGridColumnComboBox(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                            </ext:Column>
                                            <ext:Column ID="MixTime2" DataIndex="MixTime" runat="server" DecimalPrecision="0" Text="时间" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField15" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CoolMixSpeed2" DataIndex="CoolMixSpeed" runat="server" DecimalPrecision="0" Text="冷却鼓速度" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField16" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="OpenMixSpeed2" DataIndex="OpenMixSpeed" runat="server" DecimalPrecision="0" Text="开炼机速度" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField17" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="MixRollor2" DataIndex="MixRollor" runat="server" DecimalPrecision="0" Text="辊距" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField18" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="WaterTemp2" DataIndex="WaterTemp" runat="server" Text="水温" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField19" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="RubberTemp2" DataIndex="RubberTemp" runat="server" Text="胶温" Visible="false"  DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField20" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                          
                                            <ext:CommandColumn ID="CommandColumn3" runat="server" Width="120" Text="操作" Align="Center" Sortable="false">
                                                <Commands>
                                                    <ext:GridCommand Icon="TableEdit" CommandName="Insert" Text="插入">
                                                        <ToolTip Text="本条之前插入数据" />
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
                                              <ext:Column ID="CarSpeed2" DataIndex="CarSpeed" runat="server" Text="小车速度" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改" Hidden="true">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField21" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                        </Columns>
                                    </ColumnModel>
                                    <SelectionModel>
                                        <ext:CellSelectionModel ID="CellSelectionModel3" runat="server" />
                                    </SelectionModel>
                                    <Plugins>
                                        <ext:CellEditing ID="cellEditing3" runat="server" ClicksToEdit="1">
                                            <Listeners>
                                                <BeforeEdit Fn="enableEdit"></BeforeEdit>
                                            </Listeners>
                                        </ext:CellEditing>
                                    </Plugins>
                                </ext:GridPanel>
                        <ext:GridPanel ID="gridPanelMinxing3" Region="Center" runat="server" Frame="true" Title="3#开炼机">
                                    <Store>
                                        <ext:Store ID="storeMinxing3" runat="server" PageSize="0" >
                                            <AutoLoadParams>
                                                <ext:Parameter Name="MixingNo" Value="3" Mode="Raw" />
                                            </AutoLoadParams>
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.MixingGridPanelBindData" />
                                            </Proxy>
                                            <Model>
                                                <ext:Model ID="model3" runat="server" Name="gridPanelOpenActionModel">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="RecipeObjID" />
                                                        <ext:ModelField Name="RecipeEquipCode" />
                                                        <ext:ModelField Name="RecipeMaterialCode" />
                                                        <ext:ModelField Name="RecipeVersionID" />
                                                        <ext:ModelField Name="MainModelID" />
                                                        <ext:ModelField Name="OpenMixingNo" />
                                                        <ext:ModelField Name="MixingStep" />
                                                        <ext:ModelField Name="OpenActionCode" />
                                                        <ext:ModelField Name="MixTime" />
                                                        <ext:ModelField Name="CoolMixSpeed" />
                                                        <ext:ModelField Name="OpenMixSpeed" />
                                                        <ext:ModelField Name="MixRollor" />
                                                        <ext:ModelField Name="WaterTemp" />
                                                        <ext:ModelField Name="RubberTemp" />
                                                        <ext:ModelField Name="CarSpeed" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Listeners>
                                            </Listeners>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="columnModel3" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn3" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="OpenActionCode3" DataIndex="OpenActionCode" runat="server" Text="动作" Align="Center" Width="150" Sortable="false" FocusOnToFront="true">
                                                <Editor>
                                                    <ext:ComboBox ID="setOpenActionCode3" runat="server" SelectOnTab="true" Editable="false">
                                                    </ext:ComboBox>
                                                </Editor>
                                                <Renderer Handler="return RendererGridColumnComboBox(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                            </ext:Column>
                                            <ext:Column ID="MixTime3" DataIndex="MixTime" runat="server" DecimalPrecision="0" Text="时间" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField22" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CoolMixSpeed3" DataIndex="CoolMixSpeed" runat="server" DecimalPrecision="0" Text="冷却鼓速度" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField23" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="OpenMixSpeed3" DataIndex="OpenMixSpeed" runat="server" DecimalPrecision="0" Text="开炼机速度" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField24" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="MixRollor3" DataIndex="MixRollor" runat="server" DecimalPrecision="0" Text="辊距" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField25" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="WaterTemp3" DataIndex="WaterTemp" runat="server" Text="水温" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField26" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="RubberTemp3" DataIndex="RubberTemp" runat="server" Text="胶温" Visible="false"  DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField27" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CarSpeed3" DataIndex="CarSpeed" runat="server" Text="小车速度" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改" Hidden="true">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField28" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:CommandColumn ID="CommandColumn4" runat="server" Width="120" Text="操作" Align="Center" Sortable="false">
                                                <Commands>
                                                    <ext:GridCommand Icon="TableEdit" CommandName="Insert" Text="插入">
                                                        <ToolTip Text="本条之前插入数据" />
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
                                    <SelectionModel>
                                        <ext:CellSelectionModel ID="CellSelectionModel4" runat="server" />
                                    </SelectionModel>
                                    <Plugins>
                                        <ext:CellEditing ID="cellEditing4" runat="server" ClicksToEdit="1">
                                            <Listeners>
                                                <BeforeEdit Fn="enableEdit"></BeforeEdit>
                                            </Listeners>
                                        </ext:CellEditing>
                                    </Plugins>
                                </ext:GridPanel>
                        <ext:GridPanel ID="gridPanelMinxing4" Region="Center" runat="server" Frame="true" Title="4#开炼机">
                                    <Store>
                                        <ext:Store ID="storeMinxing4" runat="server" PageSize="0" >
                                            <AutoLoadParams>
                                                <ext:Parameter Name="MixingNo" Value="4" Mode="Raw" />
                                            </AutoLoadParams>
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.MixingGridPanelBindData" />
                                            </Proxy>
                                            <Model>
                                                <ext:Model ID="model4" runat="server" Name="gridPanelOpenActionModel">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="RecipeObjID" />
                                                        <ext:ModelField Name="RecipeEquipCode" />
                                                        <ext:ModelField Name="RecipeMaterialCode" />
                                                        <ext:ModelField Name="RecipeVersionID" />
                                                        <ext:ModelField Name="MainModelID" />
                                                        <ext:ModelField Name="OpenMixingNo" />
                                                        <ext:ModelField Name="MixingStep" />
                                                        <ext:ModelField Name="OpenActionCode" />
                                                        <ext:ModelField Name="MixTime" />
                                                        <ext:ModelField Name="CoolMixSpeed" />
                                                        <ext:ModelField Name="OpenMixSpeed" />
                                                        <ext:ModelField Name="MixRollor" />
                                                        <ext:ModelField Name="WaterTemp" />
                                                        <ext:ModelField Name="RubberTemp" />
                                                        <ext:ModelField Name="CarSpeed" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Listeners>
                                            </Listeners>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="columnModel4" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn4" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="OpenActionCode4" DataIndex="OpenActionCode" runat="server" Text="动作" Align="Center" Width="150" Sortable="false" FocusOnToFront="true">
                                                <Editor>
                                                    <ext:ComboBox ID="setOpenActionCode4" runat="server" SelectOnTab="true" Editable="false">
                                                    </ext:ComboBox>
                                                </Editor>
                                                <Renderer Handler="return RendererGridColumnComboBox(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                            </ext:Column>
                                            <ext:Column ID="MixTime4" DataIndex="MixTime" runat="server" DecimalPrecision="0" Text="时间" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField29" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CoolMixSpeed4" DataIndex="CoolMixSpeed" runat="server" DecimalPrecision="0" Text="冷却鼓速度" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField30" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="OpenMixSpeed4" DataIndex="OpenMixSpeed" runat="server" DecimalPrecision="0" Text="开炼机速度" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField31" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="MixRollor4" DataIndex="MixRollor" runat="server" DecimalPrecision="0" Text="辊距" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField32" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="WaterTemp4" DataIndex="WaterTemp" runat="server" Text="水温" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField33" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="RubberTemp4" DataIndex="RubberTemp" runat="server" Text="胶温" Visible="false"  DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField34" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CarSpeed4" DataIndex="CarSpeed" runat="server" Text="小车速度" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改" Hidden="true">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField35" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:CommandColumn ID="CommandColumn5" runat="server" Width="120" Text="操作" Align="Center" Sortable="false">
                                                <Commands>
                                                    <ext:GridCommand Icon="TableEdit" CommandName="Insert" Text="插入">
                                                        <ToolTip Text="本条之前插入数据" />
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
                                    <SelectionModel>
                                        <ext:CellSelectionModel ID="CellSelectionModel5" runat="server" />
                                    </SelectionModel>
                                    <Plugins>
                                        <ext:CellEditing ID="cellEditing5" runat="server" ClicksToEdit="1">
                                            <Listeners>
                                                <BeforeEdit Fn="enableEdit"></BeforeEdit>
                                            </Listeners>
                                        </ext:CellEditing>
                                    </Plugins>
                                </ext:GridPanel>
                        <ext:GridPanel ID="gridPanelMinxing5" Region="Center" runat="server" Frame="true" Title="5#开炼机">
                                    <Store>
                                        <ext:Store ID="storeMinxing5" runat="server" PageSize="0" >
                                            <AutoLoadParams>
                                                <ext:Parameter Name="MixingNo" Value="5" Mode="Raw" />
                                            </AutoLoadParams>
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.MixingGridPanelBindData" />
                                            </Proxy>
                                            <Model>
                                                <ext:Model ID="model5" runat="server" Name="gridPanelOpenActionModel">
                                                    <Fields>
                                                        <ext:ModelField Name="ObjID" />
                                                        <ext:ModelField Name="RecipeObjID" />
                                                        <ext:ModelField Name="RecipeEquipCode" />
                                                        <ext:ModelField Name="RecipeMaterialCode" />
                                                        <ext:ModelField Name="RecipeVersionID" />
                                                        <ext:ModelField Name="MainModelID" />
                                                        <ext:ModelField Name="OpenMixingNo" />
                                                        <ext:ModelField Name="MixingStep" />
                                                        <ext:ModelField Name="OpenActionCode" />
                                                        <ext:ModelField Name="MixTime" />
                                                        <ext:ModelField Name="CoolMixSpeed" />
                                                        <ext:ModelField Name="OpenMixSpeed" />
                                                        <ext:ModelField Name="MixRollor" />
                                                        <ext:ModelField Name="WaterTemp" />
                                                        <ext:ModelField Name="RubberTemp" />
                                                        <ext:ModelField Name="CarSpeed" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Listeners>
                                            </Listeners>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="columnModel5" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn5" runat="server" Text="步骤" Width="50" Align="Center" />
                                            <ext:Column ID="OpenActionCode5" DataIndex="OpenActionCode" runat="server" Text="动作" Align="Center" Width="150" Sortable="false" FocusOnToFront="true">
                                                <Editor>
                                                    <ext:ComboBox ID="setOpenActionCode5" runat="server" SelectOnTab="true" Editable="false">
                                                    </ext:ComboBox>
                                                </Editor>
                                                <Renderer Handler="return RendererGridColumnComboBox(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                            </ext:Column>
                                            <ext:Column ID="MixTime5" DataIndex="MixTime" runat="server" DecimalPrecision="0" Text="时间" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField36" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CoolMixSpeed5" DataIndex="CoolMixSpeed" runat="server" DecimalPrecision="0" Text="冷却鼓速度" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField37" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="OpenMixSpeed5" DataIndex="OpenMixSpeed" runat="server" DecimalPrecision="0" Text="开炼机速度" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField38" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="MixRollor5" DataIndex="MixRollor" runat="server" DecimalPrecision="0" Text="辊距" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField39" runat="server" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="WaterTemp5" DataIndex="WaterTemp" runat="server" Text="水温" Align="Center" Width="120" Sortable="false">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField40" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="RubberTemp5" DataIndex="RubberTemp" runat="server" Text="胶温" Visible="false"  DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField41" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CarSpeed5" DataIndex="CarSpeed" runat="server" Text="小车速度" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改" Hidden="true">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField42" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:CommandColumn ID="CommandColumn6" runat="server" Width="120" Text="操作" Align="Center" Sortable="false">
                                                <Commands>
                                                    <ext:GridCommand Icon="TableEdit" CommandName="Insert" Text="插入">
                                                        <ToolTip Text="本条之前插入数据" />
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
                                    <SelectionModel>
                                        <ext:CellSelectionModel ID="CellSelectionModel6" runat="server" />
                                    </SelectionModel>
                                    <Plugins>
                                        <ext:CellEditing ID="cellEditing6" runat="server" ClicksToEdit="1">
                                            <Listeners>
                                                <BeforeEdit Fn="enableEdit"></BeforeEdit>
                                            </Listeners>
                                        </ext:CellEditing>
                                    </Plugins>
                                </ext:GridPanel>
                        <ext:GridPanel ID="gridPanelMinxing6" Region="Center" runat="server" Frame="true" Title="6#开炼机">
                                            <Store>
                                                <ext:Store ID="storeMinxing6" runat="server" PageSize="0" >
                                                    <AutoLoadParams>
                                                        <ext:Parameter Name="MixingNo" Value="6" Mode="Raw" />
                                                    </AutoLoadParams>
                                                    <Proxy>
                                                        <ext:PageProxy DirectFn="App.direct.MixingGridPanelBindData" />
                                                    </Proxy>
                                                    <Model>
                                                        <ext:Model ID="model6" runat="server" Name="gridPanelOpenActionModel">
                                                            <Fields>
                                                                <ext:ModelField Name="ObjID" />
                                                                <ext:ModelField Name="RecipeObjID" />
                                                                <ext:ModelField Name="RecipeEquipCode" />
                                                                <ext:ModelField Name="RecipeMaterialCode" />
                                                                <ext:ModelField Name="RecipeVersionID" />
                                                                <ext:ModelField Name="MainModelID" />
                                                                <ext:ModelField Name="OpenMixingNo" />
                                                                <ext:ModelField Name="MixingStep" />
                                                                <ext:ModelField Name="OpenActionCode" />
                                                                <ext:ModelField Name="MixTime" />
                                                                <ext:ModelField Name="CoolMixSpeed" />
                                                                <ext:ModelField Name="OpenMixSpeed" />
                                                                <ext:ModelField Name="MixRollor" />
                                                                <ext:ModelField Name="WaterTemp" />
                                                                <ext:ModelField Name="RubberTemp" />
                                                                <ext:ModelField Name="CarSpeed" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                    <Listeners>
                                                    </Listeners>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="columnModel6" runat="server">
                                                <Columns>
                                                    <ext:RowNumbererColumn ID="RowNumbererColumn6" runat="server" Text="步骤" Width="50" Align="Center" />
                                                    <ext:Column ID="OpenActionCode6" DataIndex="OpenActionCode" runat="server" Text="动作" Align="Center" Width="150" Sortable="false" FocusOnToFront="true">
                                                        <Editor>
                                                            <ext:ComboBox ID="setOpenActionCode6" runat="server" SelectOnTab="true" Editable="false">
                                                            </ext:ComboBox>
                                                        </Editor>
                                                        <Renderer Handler="return RendererGridColumnComboBox(value,metadata,record,rowIndex,colIndex,store,view)"></Renderer>
                                                    </ext:Column>
                                                    <ext:Column ID="MixTime6" DataIndex="MixTime" runat="server" DecimalPrecision="0" Text="时间" Align="Center" Width="120" Sortable="false">
                                                        <Editor>
                                                            <ext:NumberField ID="NumberField43" runat="server" MinValue="0" />
                                                        </Editor>
                                                        <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                                    </ext:Column>
                                                    <ext:Column ID="CoolMixSpeed6" DataIndex="CoolMixSpeed" runat="server" DecimalPrecision="0" Text="冷却鼓速度" Align="Center" Width="120" Sortable="false">
                                                        <Editor>
                                                            <ext:NumberField ID="NumberField44" runat="server" />
                                                        </Editor>
                                                        <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                                    </ext:Column>
                                                    <ext:Column ID="OpenMixSpeed6" DataIndex="OpenMixSpeed" runat="server" DecimalPrecision="0" Text="开炼机速度" Align="Center" Width="120" Sortable="false">
                                                        <Editor>
                                                            <ext:NumberField ID="NumberField45" runat="server" />
                                                        </Editor>
                                                        <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                                    </ext:Column>
                                                    <ext:Column ID="MixRollor6" DataIndex="MixRollor" runat="server" DecimalPrecision="0" Text="辊距" Align="Center" Width="120" Sortable="false">
                                                        <Editor>
                                                            <ext:NumberField ID="NumberField46" runat="server" />
                                                        </Editor>
                                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                                    </ext:Column>
                                                    <ext:Column ID="WaterTemp6" DataIndex="WaterTemp" runat="server" Text="水温" Align="Center" Width="120" Sortable="false">
                                                        <Editor>
                                                            <ext:NumberField ID="NumberField47" runat="server" MinValue="0" />
                                                        </Editor>
                                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                                    </ext:Column>
                                                    <ext:Column ID="RubberTemp6" DataIndex="RubberTemp" runat="server" Text="胶温" Visible="false"  DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
                                                        <Editor>
                                                            <ext:NumberField ID="NumberField48" runat="server" MinValue="0" />
                                                        </Editor>
                                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                                    </ext:Column>
                                                    <ext:Column ID="CarSpeed6" DataIndex="CarSpeed" runat="server" Text="小车速度" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改" Hidden="true">
                                                        <Editor>
                                                            <ext:NumberField ID="NumberField49" runat="server" MinValue="0" />
                                                        </Editor>
                                                        <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                                    </ext:Column>
                                                    <ext:CommandColumn ID="CommandColumn7" runat="server" Width="120" Text="操作" Align="Center" Sortable="false">
                                                        <Commands>
                                                            <ext:GridCommand Icon="TableEdit" CommandName="Insert" Text="插入">
                                                                <ToolTip Text="本条之前插入数据" />
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
                                            <SelectionModel>
                                                <ext:CellSelectionModel ID="CellSelectionModel7" runat="server" />
                                            </SelectionModel>
                                            <Plugins>
                                                <ext:CellEditing ID="cellEditing7" runat="server" ClicksToEdit="1">
                                                    <Listeners>
                                                        <BeforeEdit Fn="enableEdit"></BeforeEdit>
                                                    </Listeners>
                                                </ext:CellEditing>
                                            </Plugins>
                                        </ext:GridPanel>
                    </Items>
                </ext:TabPanel>
            </Items>
        </ext:Viewport>
        <ext:Hidden ID="txtRecipeObjID" runat="server"></ext:Hidden>
        <ext:Hidden ID="hidden_main_id" runat="server"></ext:Hidden>
        <ext:Hidden ID="txtOpenMainModelId" runat="server"></ext:Hidden>
    </form>
</body>
</html>
