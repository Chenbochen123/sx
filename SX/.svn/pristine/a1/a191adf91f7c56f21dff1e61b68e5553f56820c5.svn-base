<%@ page language="C#" autoeventwireup="true" inherits="Manager_BasicInfo_MaterialInfo_RecipeIntoInfo, App_Web_mbidu024" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>胶料质检等同</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
     <script type="text/javascript">
       

      

         Ext.apply(Ext.form.VTypes, {
             integer: function (val, field) {
                 if (!val) {
                     return;
                 }
                 try {
                     if (/^[\d]+$/.test(val)) {
                         return true;
                     }
                     else {
                         return false;
                     }
                 }
                 catch (e) {
                     return false;
                 }
             },
             integerText: "此填入项格式为正整数"
         });

         //列表刷新数据重载方法
         var pnlListFresh = function () {
             App.hidden_delete_flag.setValue("0");
             App.store.currentPage = 1;
             App.pageToolBar.doRefresh();
             return false;
         }

         //历史查询按钮点击列表刷新数据重载方法
         var pnlHistoryListFresh = function () {
             App.hidden_delete_flag.setValue("");
             App.store.currentPage = 1;
             App.pageToolBar.doRefresh();
             return false;
         }

         //历史查询根据DeleteFlag的值进行样式绑定
         var SetRowClass = function (record, rowIndex, rowParams, store) {
             if (record.get("DeleteFlag") == "1") {
                 return "x-grid-row-deleted";
             }
         }
         //历史查询的每行按钮准备加载
         var prepareToolbar = function (grid, toolbar, rowIndex, record) {
             if (record.get("DeleteFlag") == "1") {
                 toolbar.items.getAt(0).hide();
                 toolbar.items.getAt(1).hide();
                 toolbar.items.getAt(2).hide();
                 toolbar.items.getAt(3).hide();
             } else {
                 toolbar.items.getAt(4).hide();
             }
         };
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
                                <ext:Button runat="server" Icon="Add" Text="添加" ID="btn_add">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttAdd" runat="server" Html="点击进行添加" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btn_add_Click">
                                          <ExtraParams>
                                            <ext:Parameter Name="Values" Value="Ext.encode(#{pnlList}.getRowsValues({selectedOnly:true}))" Mode="Raw" />
                                        </ExtraParams>
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
                                 <ext:ToolbarSeparator ID="toolbarSeparator_middle" />
                              
                       
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
                                           <ext:ComboBox ID="material_group"  runat="server" FieldLabel="物料分组" 
                                LabelAlign="Left" 
                                Width="300" >
                              </ext:ComboBox>  </Items> <Items>
                                  </Items>
                                        </ext:Container>
                                         <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                             <Items>
                              <ext:ComboBox ID="pmttype"  runat="server" FieldLabel="配方类型" 
                                LabelAlign="Left" 
                                Width="300">
                              </ext:ComboBox>   </Items>
                                        </ext:Container>
                                     </Items>
                                </ext:FormPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
              
                </ext:Panel>
                <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="999"> 
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="RecipeMaterialCode" />
                                        <ext:ModelField Name="RecipeType" />
                                        <ext:ModelField Name="ItemName"  />
                                        <ext:ModelField Name="dtName" />
                                           <ext:ModelField Name="MaterialName" />
                                              <ext:ModelField Name="Name" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                            <ext:Column ID="obj_id" runat="server" Text="物料编号" DataIndex="RecipeMaterialCode" Width="150"  />
                            <ext:Column ID="Column1" runat="server" Text="物料名称" DataIndex="MaterialName" Width="150"  />
                            <ext:Column ID="marjor_type_name" runat="server" Text="大类编号" DataIndex="RecipeType" Width="150"  />
                            <ext:Column ID="delete_flag" runat="server" Text="配方类型" DataIndex="ItemName" Width="150"  />
                            <ext:Column ID="remark" runat="server" Text="分组编码" DataIndex="dtName" Width="150"  />
                           <ext:Column ID="Column2" runat="server" Text="分组名称" DataIndex="Name" Width="150"  />
                        </Columns>
                    </ColumnModel>
                       <SelectionModel>
                    <ext:CheckboxSelectionModel ID="RowSelectionModel1" runat="server" Mode="Simple" />
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
           
                <ext:Hidden ID="hidden_major_type_name" runat="server" />
                <ext:Hidden ID="hidden_delete_flag"  runat="server" Text="0"></ext:Hidden>
            </Items>
        </ext:Viewport>
        </form>
</body>
</html>