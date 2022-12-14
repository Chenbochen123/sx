<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WLTH.aspx.cs" Inherits="Manager_Technology_Manage_WLTH" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>物料替换</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
     <script type="text/javascript">





         //列表刷新数据重载方法
         var pnlListFresh = function () {
             App.hidden_delete_flag.setValue("0");
             App.store.currentPage = 1;
             App.pageToolBar.doRefresh();
             return false;
         }
         var SetRowClass = function (record, rowIndex, rowParams, store) {
             if (record.get("DeleteFlag") == "1") {
                 return "x-grid-row-deleted";
             }
         }
       
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
                                <ext:Button runat="server" Icon="Add" Text="替换" ID="btn_edit">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttAdd" runat="server" Html="点击进行替换" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btn_add_Click">
                                        <Confirmation ConfirmRequest="true" Title="提示" Message="确定要替换选择的记录吗" />
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
                                 <ext:Button runat="server" Icon="Delete" Text="删除" ID="Button1" Hidden="true">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行删除" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btn_del_Click">
                                         <Confirmation ConfirmRequest="true" Title="提示" Message="确定要删除选择的记录吗" />
                                          <ExtraParams>
                                            <ext:Parameter Name="Values" Value="Ext.encode(#{pnlList}.getRowsValues({selectedOnly:true}))" Mode="Raw" />
                                        </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                       
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
                                           <ext:ComboBox ID="Fmaterial"  runat="server" FieldLabel="源物料" 
                                LabelAlign="Left" 
                                Width="380" >
                              </ext:ComboBox> 
                             
                               </Items> <Items>
                                  </Items>
                                        </ext:Container>
                                           <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                             <Items>
                            <ext:ComboBox ID="CRetype"  runat="server" FieldLabel="配方类型" 
                                LabelAlign="Left" 
                                Width="300" >
                              </ext:ComboBox>  </Items>
                                        </ext:Container>
                                         <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                             <Items>
                              <ext:ComboBox ID="Smaterial"  runat="server" FieldLabel="目标物料" 
                                LabelAlign="Left" 
                                Width="380">
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
                                        <ext:ModelField Name="objid" />
                                        <ext:ModelField Name="mater_name" />
                                        <ext:ModelField Name="edt_code"  />
                                        <ext:ModelField Name="equipname" />
                                           <ext:ModelField Name="recipe_typename" />
                                              <ext:ModelField Name="Set_weight" />
                                                 <ext:ModelField Name="Error_allow" />
                                         <ext:ModelField Name="Wname" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                            <ext:Column ID="obj_id" runat="server" Text="称量ID" DataIndex="objid" Width="100"   Hidden="true"/>
                            <ext:Column ID="Column1" runat="server" Text="配方名称" DataIndex="mater_name" Width="150"  />
                             <ext:Column ID="delete_flag" runat="server" Text="机台名称" DataIndex="equipname" Width="150"  />
                            <ext:Column ID="marjor_type_name" runat="server" Text="版本号" DataIndex="edt_code" Width="150"  />
                             <ext:Column ID="Column2" runat="server" Text="配方类型" DataIndex="recipe_typename" Width="150"  />
                              <ext:Column ID="Column5" runat="server" Text="称量物料" DataIndex="Wname" Width="250"  />
                                 <ext:Column ID="Column3" runat="server" Text="设定重量" DataIndex="Set_weight" Width="100"  />
                                     <ext:Column ID="Column4" runat="server" Text="允许误差" DataIndex="Error_allow" Width="100"  />
                           
                        
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