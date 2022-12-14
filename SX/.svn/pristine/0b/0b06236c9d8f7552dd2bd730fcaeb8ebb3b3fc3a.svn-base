<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_BasicInfo_OpenActionModel, App_Web_xrwstxsv" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>开炼信息模板</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>OpenActionModel.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell {
            background-color: #B0FFBA !important;
        }
        .x-grid-row-error .x-grid-cell {
            background-color: #FFC0CB !important;
        }
        
    </style>
</head>
<body>
    <form id="fmUnit" runat="server">
        <ext:ResourceManager ID="rmUnit" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true" Collapsible="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUnit">
                            <Items>
                                <ext:Button runat="server" Icon="Add" Text="添加" ID="btn_add">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttAdd" runat="server" Html="点击进行添加" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btn_add_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="pnlListFresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                 <ext:ToolbarSeparator ID="toolbarSeparator14" />
                                 <ext:Button runat="server" Icon="Note" Text="历史查询" ID="btn_history_search">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行历史查询" />
                                    </ToolTips>
                                     <Listeners>
                                        <Click Fn="pnlHistoryListFresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_middle_2"  />
                                <ext:Button runat="server" Icon="DiskEdit" Text="编辑" ID="btnCanSave">
                                    <Listeners>
                                        <Click Handler="SetCanSave();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" Icon="Disk" Text="保存" ID="btnSave" Disabled="true">
                                    <Listeners>
                                        <Click Handler="Save()"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarFill ID="toolbarFill_middle_1" />
                                <ext:TextField ID="txt_model_name" runat="server" FieldLabel="模板名称" LabelAlign="Right" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                </ext:Panel>
                <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="15"> 
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="ModelName" />
                                        <ext:ModelField Name="ModelCreateDate" />
                                        <ext:ModelField Name="UserName" />
                                        <ext:ModelField Name="ModelValidDate" />
                                        <ext:ModelField Name="ModelDetail" />
                                        <ext:ModelField Name="DeleteFlag" />
                                        <ext:ModelField Name="Remark" />
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
                            <ext:Column ID="obj_id" runat="server" Text="编号" DataIndex="ObjID" Width="100"  Hidden="true" Align="Center" />
                            <ext:Column ID="model_name" runat="server" Text="模板名称" DataIndex="ModelName" Width="150" Align="Center" />
                            <ext:DateColumn Format="yyyy-MM-dd hh:mm:ss" ID="model_create_date"  runat="server" Text="创建时间" DataIndex="ModelCreateDate" Width="150"  Align="Center" />
                            <ext:Column ID="user_name" runat="server" Text="创建用户" DataIndex="UserName" Width="150"  Align="Center" />
                            <ext:DateColumn ID="model_valid_date" Format="yyyy-MM-dd" runat="server" Text="模板有效时间" DataIndex="ModelValidDate" Width="150" Align="Center" />
                            <ext:Column ID="model_detail" runat="server" Text="模板描述" DataIndex="ModelDetail" Width="150" Align="Center" />
                            <ext:Column ID="delete_flag" runat="server" Text="删除标志" DataIndex="DeleteFlag" Width="150" Hidden="true" Align="Center" />
                            <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Width="200" Align="Center"  />
                            <ext:CommandColumn ID="commandCol" runat="server" Width="180" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                        <ToolTip Text="修改本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                        <ToolTip Text="删除本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Accept" CommandName="Recover" Text="恢复">
                                        <ToolTip Text="恢复本条数据" />
                                    </ext:GridCommand>
                                </Commands>
                                <PrepareToolbar Fn="prepareToolbar" />
                                <Listeners>
                                    <Command Handler="return commandcolumn_click_main(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                            <Listeners>
                                <Select Handler="selectMainData(record.data.ObjID)" Buffer="250" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <View>
                        <ext:GridView ID="gvRows" runat="server">
                            <GetRowClass Fn="SetRowClass" />
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
                 <ext:TabPanel ID="pnlSouth" runat="server" Region="South" Title="开炼动作信息明细" 
                     Icon="Basket" Layout="Fit" Collapsible="true" Split="true" MarginsSummary="0 5 5 5">
                    <Items>
                        <ext:Panel ID="pnlMix0" runat="server" Title="0#开炼机">
                            <Items>
                                 <ext:GridPanel ID="gridPanelMinxing0" Region="Center" runat="server" Height="250">
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
                                            <ext:Column ID="RubberTemp0" DataIndex="RubberTemp" runat="server" Text="胶温" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField8" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CarSpeed0" DataIndex="CarSpeed" runat="server" Text="小车速度" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField9" runat="server" MinValue="0" />
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
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="pnlMix1" runat="server" Title="1#开炼机">
                            <Items>
                                 <ext:GridPanel ID="gridPanelMinxing1" Region="Center" runat="server" Height="250">
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
                                            <ext:Column ID="RubberTemp1" DataIndex="RubberTemp" runat="server" Text="胶温" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField13" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CarSpeed1" DataIndex="CarSpeed" runat="server" Text="小车速度" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
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
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="pnlMix2" runat="server" Title="2#开炼机">
                            <Items>
                                 <ext:GridPanel ID="gridPanelMinxing2" Region="Center" runat="server" Height="250">
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
                                            <ext:Column ID="RubberTemp2" DataIndex="RubberTemp" runat="server" Text="胶温" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField20" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CarSpeed2" DataIndex="CarSpeed" runat="server" Text="小车速度" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField21" runat="server" MinValue="0" />
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
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="pnlMix3" runat="server" Title="3#开炼机">
                            <Items>
                                 <ext:GridPanel ID="gridPanelMinxing3" Region="Center" runat="server" Height="250">
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
                                            <ext:Column ID="RubberTemp3" DataIndex="RubberTemp" runat="server" Text="胶温" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField27" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CarSpeed3" DataIndex="CarSpeed" runat="server" Text="小车速度" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
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
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="pnlMix4" runat="server" Title="4#开炼机">
                            <Items>
                                 <ext:GridPanel ID="gridPanelMinxing4" Region="Center" runat="server" Height="250">
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
                                            <ext:Column ID="RubberTemp4" DataIndex="RubberTemp" runat="server" Text="胶温" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField34" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CarSpeed4" DataIndex="CarSpeed" runat="server" Text="小车速度" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
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
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="pnlMix5" runat="server" Title="5#开炼机">
                            <Items>
                                 <ext:GridPanel ID="gridPanelMinxing5" Region="Center" runat="server" Height="250">
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
                                            <ext:Column ID="RubberTemp5" DataIndex="RubberTemp" runat="server" Text="胶温" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField41" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CarSpeed5" DataIndex="CarSpeed" runat="server" Text="小车速度" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
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
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="pnlMix6" runat="server" Title="6#开炼机">
                            <Items>
                                 <ext:GridPanel ID="gridPanelMinxing6" Region="Center" runat="server" Height="250">
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
                                            <ext:Column ID="RubberTemp6" DataIndex="RubberTemp" runat="server" Text="胶温" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
                                                <Editor>
                                                    <ext:NumberField ID="NumberField48" runat="server" MinValue="0" />
                                                </Editor>
                                                <Renderer Handler="if (!value){return ''}; if (value==0) {return '';} else {return value;}" />
                                            </ext:Column>
                                            <ext:Column ID="CarSpeed6" DataIndex="CarSpeed" runat="server" Text="小车速度" DecimalPrecision="0" Align="Center" Width="120" Sortable="false" ToolTip="单机表头可批量修改">
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
                        </ext:Panel>
                    </Items>
                </ext:TabPanel>
                 <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改开炼动作模板"
                    Width="320" Height="300" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items> 
                        <ext:FormPanel ID="pnlEdit" runat="server" Flex="1" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="modify_obj_id" runat="server" FieldLabel="模板编号"   LabelAlign="Left" ReadOnly="true" Hidden="true" Enabled="true" />
                                <ext:TextField ID="modify_model_name" runat="server" FieldLabel="模板名称"  LabelAlign="Left" MaxLength="50" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" IsRemoteValidation=true  >
                                    <RemoteValidation  OnValidation = "CheckModelName" />
                                </ext:TextField>
                                <ext:DateField ID="modify_model_valid_date" runat="server" FieldLabel="有效时间" LabelAlign="Left" MaxLength="50" />
                                <ext:TextArea ID="modify_model_detail" runat="server" FieldLabel="模板描述" LabelAlign="Left" MaxLength="50" />
                                <ext:TextArea ID="modify_remark" runat="server" FieldLabel="备注" LabelAlign="Left" MaxLength="50" />
                            </Items>
                             <Listeners>
                                <ValidityChange Handler="#{btnModifySave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnModifySave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="BtnModifySave_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="BtnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加开炼动作模板"
                    Width="320" Height="300" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="add_model_name" runat="server" FieldLabel="模板名称"  LabelAlign="Left" MaxLength="50" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" IsRemoteValidation=true  >
                                    <RemoteValidation  OnValidation = "CheckModelName" />
                                </ext:TextField>
                                <ext:DateField ID="add_model_valid_date" runat="server" FieldLabel="有效时间" LabelAlign="Left" MaxLength="50" />
                                <ext:TextArea ID="add_model_detail" runat="server" FieldLabel="模板描述" LabelAlign="Left" MaxLength="50" />
                                <ext:TextArea ID="add_remark" runat="server" FieldLabel="备注" LabelAlign="Left" MaxLength="50" />
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnAddSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                     <Buttons>
                        <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <DirectEvents>
                                <Click OnEvent="BtnAddSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="BtnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Hidden runat="server" ID="hidden_main_id" Text="" />
                <ext:Hidden runat="server" ID="hidden_model_name" Text="" />
                <ext:Hidden runat="server" ID="hidden_delete_flag" Text="0" />
            </Items>
        </ext:Viewport>
        </form>
</body>
</html>