<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_Manage_MaterialRecipeDetail_QDrug, App_Web_a5rwyiqa" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Q药品示方</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />
    <script type="text/javascript">
        var gridPanelRefresh = function () {
           App.gridPanelQDrug.store.currentPage = 1;
           App.gridPanelQDrug.store.reload();
            return false;
        }
    </script>

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>QDrug.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
      <Items>
        <ext:Panel ID="Panel2" runat="server"  > <Items>
                <ext:Panel ID="pnlQrug" runat="server"  Header="false" Layout="BorderLayout" Height="200">
     <Items>
                        <ext:GridPanel ID="gridPanelQDrug" Region="Center" runat="server" Frame="true"  >
                             <Store>
                                <ext:Store ID="storeMinxing" runat="server" PageSize="9">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.MinxingGridPanelBindData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model" runat="server" Name="gridPanelMinxingModel">
                                            <Fields>
                                               
                                                <ext:ModelField Name="RecipeObjID" />
                                                <ext:ModelField Name="MainMaterCode" />
                                                <ext:ModelField Name="EquipCode" />
                                                <ext:ModelField Name="EdtCode" />
                                                <ext:ModelField Name="WeightID" />
                                                <ext:ModelField Name="WeightType" />
                                                <ext:ModelField Name="MaterialCode" />
                                                <ext:ModelField Name="MaterialName" />
                                                <ext:ModelField Name="SetWeight" />
                                                <ext:ModelField Name="ErrorAllow" />
                                                <ext:ModelField Name="ErrorAllowMove" />
                                                <ext:ModelField Name="CreateDate" />
                                                <ext:ModelField Name="UpdateDate" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Listeners>
                                        <Load Handler="App.txtRecipeObjID.setValue('');"></Load>
                                    </Listeners>
                                </ext:Store>
                            </Store>
                             <ColumnModel ID="columnModel" runat="server">
                                <Listeners>
                                    <HeaderClick Fn="HeaderClick"></HeaderClick>
                                </Listeners>
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNum1" runat="server" Text="编号" Width="50" Align="Center" />
                                 <ext:Column ID="MaterialCodePowderPackage" DataIndex="MaterialName" runat="server" Text="小料名称" Hideable="false" Sortable="false" Flex="1" SummaryType="Count">
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
                                    <ext:Column ID="SetWeight" DataIndex="SetWeight" runat="server" DecimalPrecision="0" Text="配药重量" Align="Center" Width="160" Sortable="false">
                                        <Editor>
                                            <ext:NumberField ID="NumberField2" runat="server" MinValue="0" />
                                        </Editor>
                                        <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                    </ext:Column>
                                    <ext:Column ID="ErrorAllow" DataIndex="ErrorAllow" runat="server" DecimalPrecision="0" Text="允许误差" Align="Center" Width="160" Sortable="false">
                                        <Editor>
                                            <ext:NumberField ID="NumberField4" runat="server" />
                                        </Editor>
                                        <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                    </ext:Column>
                                        <ext:Column ID="ErrorAllowMove" DataIndex="ErrorAllowMove" runat="server" DecimalPrecision="0" Text="移送误差" Align="Center" Width="160" Sortable="false"  Visible="false">
                                        <Editor>
                                            <ext:NumberField ID="NumberField1" runat="server" />
                                        </Editor>
                                        <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                    </ext:Column>
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
                             <BottomBar>
                                <ext:PagingToolbar ID="pageToolbar" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="progressBarPager" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                         </ext:GridPanel>
                        <ext:Hidden ID="txtRecipeObjID" runat="server"></ext:Hidden>
                    </Items>
                    
                    
                      </ext:Panel>
                         <ext:Panel ID="RERUBPanel" runat="server"  Header="True" Layout="BorderLayout" Height="150"   Title="返回胶信息">
     <Items>
                        <ext:GridPanel ID="gridPanelRERUB" Region="Center" runat="server" Frame="true"  >
                             <Store>
                                <ext:Store ID="RERUBStore" runat="server" PageSize="3">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.RERUBPanelBindData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model1" runat="server" Name="gridPanelRERUBModel">
                                            <Fields>
                                               
                                                <ext:ModelField Name="RecipeObjID" />
                                                <ext:ModelField Name="MainMaterCode" />
                                                <ext:ModelField Name="EquipCode" />
                                                <ext:ModelField Name="EdtCode" />
                                                <ext:ModelField Name="WeightID" />
                                                <ext:ModelField Name="WeightType" />
                                                <ext:ModelField Name="MaterialCode" />
                                                <ext:ModelField Name="MaterialName" />
                                                <ext:ModelField Name="SetWeight" />
                                                <ext:ModelField Name="ErrorAllow" />
                                                <ext:ModelField Name="ErrorAllowMove" />
                                                <ext:ModelField Name="CreateDate" />
                                                <ext:ModelField Name="UpdateDate" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Listeners>
                                        <Load Handler="App.txtRecipeObjID.setValue('');"></Load>
                                    </Listeners>
                                </ext:Store>
                            </Store>
                             <ColumnModel ID="columnModel1" runat="server">
                                <Listeners>
                                    <HeaderClick Fn="HeaderClick"></HeaderClick>
                                </Listeners>
                                <Columns>
                                    <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Text="编号" Width="50" Align="Center" />
                                 <ext:Column ID="Column1" DataIndex="MaterialName" runat="server" Text="返回胶名称" Hideable="false" Sortable="false" Flex="1" SummaryType="Count">
                                        <Editor>
                                            <ext:ComboBox ID="ComboBox1" runat="server" MinChars="1" ValueField="field1" DisplayField="field2"
                                                EmptyText="输入关键词" TypeAhead="false" HideBaseTrigger="true" AllowBlank="false">
                                                <Store>
                                                    <ext:Store ID="Store2" runat="server" OnReadData="ComboBoxSearchStore_ReadData2">
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
                                    <ext:Column ID="Column2" DataIndex="SetWeight" runat="server" DecimalPrecision="0" Text="配药重量" Align="Center" Width="160" Sortable="false">
                                        <Editor>
                                            <ext:NumberField ID="NumberField3" runat="server" MinValue="0" />
                                        </Editor>
                                        <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                    </ext:Column>
                                    <ext:Column ID="Column3" DataIndex="ErrorAllow" runat="server" DecimalPrecision="0" Text="允许误差" Align="Center" Width="160" Sortable="false">
                                        <Editor>
                                            <ext:NumberField ID="NumberField5" runat="server" />
                                        </Editor>
                                        <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                    </ext:Column>
                                        <ext:Column ID="Column4" DataIndex="ErrorAllowMove" runat="server" DecimalPrecision="0" Text="移送误差" Align="Center" Width="160" Sortable="false"  Visible="false">
                                        <Editor>
                                            <ext:NumberField ID="NumberField6" runat="server" />
                                        </Editor>
                                        <Renderer Handler="if (!value){return ''};if (value==0) {return '';} else {return value;}" />
                                    </ext:Column>
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
                          <%--   <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="progressBarPager1" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>--%>
                         </ext:GridPanel>
                   
                    </Items>
                    
                    
                      </ext:Panel>
                           <ext:FormPanel ID="Panel1" runat="server" Header="true" Layout="AnchorLayout"  Height="160" AutoScroll="true">
                            <Items>
                                <ext:Container ID="container5" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                    <Items>  <ext:CheckboxGroup ID="CheckboxGroup1" runat="server" ColumnsNumber="4" Flex="1" AnchorHorizontal="true">
                                            <Items>
                                         <ext:NumberField ID="TotalWeigh" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="3" FieldLabel="药品总重（kg）" ReadOnly="true"  />
                                         <ext:NumberField ID="ErroeAllow" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="3" FieldLabel="复检允许误差（kg）"  />
                                         <ext:NumberField ID="ErroeAllowMove" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="3" FieldLabel="移送允许误差（kg）"  />
                                          <ext:NumberField ID="CTabletSpeed" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="1" FieldLabel="压片速度（mpm）"  />
                                          <ext:NumberField ID="CTabletThick" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="1" FieldLabel="压片厚度（mpm）" />
                                          <ext:NumberField ID="CTabletTemp" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="1" FieldLabel="压片温度（℃）" />
                                          <ext:NumberField ID="CTabletWeigh" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="3" FieldLabel="装载重量（kg）" />
                                          <ext:ComboBox ID="CIsUseCode" runat="server" LabelAlign="Right" Flex="1" FieldLabel="字码使用与否"  SelectOnTab="true" Editable="false" />
                                          <ext:ComboBox ID="CIsUseCutter" runat="server" LabelAlign="Right" Flex="1" FieldLabel="裁刀使用与否"        SelectOnTab="true" Editable="false" />
                                          <ext:NumberField ID="CCutterNum" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="0" FieldLabel="裁刀使用数量"  />
                                              <ext:NumberField ID="BatchWeigh" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="3" FieldLabel="Batch重量（kg）" />
                                          <ext:NumberField ID="DrugNum" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="0" FieldLabel="药品投入次数" />
                                          <ext:NumberField ID="DrugTime" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="1" FieldLabel="药品投入间隔（s）" />
                                           <ext:NumberField ID="RubErroeAllow" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="3" FieldLabel="返回胶允许误差"  />
                                         </Items>
                                        </ext:CheckboxGroup>
                                     </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
            </Items>
               </ext:Panel>
        </Items>
   </ext:Viewport>
    </form>
</body>
</html>
