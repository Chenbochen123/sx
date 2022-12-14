<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_Analysis_Weigh, App_Web_g0iycshh" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>称量过程分析</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />

    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>Weigh.js"></script>

    <script type="text/javascript">
        var btnSearchOverWeighClick = function () {
            App.OverWeighGridPanel.store.currentPage = 1;
            App.OverWeighGridPanelPageToolbar.doRefresh();
            return false;
        }
    </script>
     <script type="text/javascript">
         var btnSearchOverWeighClick2 = function () {
             App.OverWeighGridPanel2.store.currentPage = 1;
             App.OverWeighGridPanelPageToolbar2.doRefresh();
             return false;
         }
    </script>
    <script type="text/javascript">
        var btnSearchWeighRateClick = function () {
            App.WeighRateGridPanel.store.currentPage = 1;
            App.WeighRateGridPanel.store.reload();
            return false;
        }
    </script>
    <script type="text/javascript">
        var PptMaterialRefresh = function () {
            App.txtPptMaterial.getStore().reload();
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnExportOverWeighSubmit" Style="display: none" runat="server" Text="Button" OnClick="btnExportOverWeighSubmit_Click" />
          <asp:Button ID="btnExportOverWeighSubmit2" Style="display: none" runat="server" Text="Button" OnClick="btnExportOverWeighSubmit_Click2" />
        <asp:Button ID="btnExportWeighRateSubmit" Style="display: none" runat="server" Text="Button" OnClick="btnExportWeighRateSubmit_Click" />
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel20" runat="server" Region="North" AutoHeight="true">
                    <Items>
                        <ext:Container ID="container1" runat="server" Layout="ColumnLayout" AutoHeight="true" Padding="5">
                            <Items>
                                <ext:Container ID="container4" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".3">
                                    <Items>
                                        <ext:TriggerField ID="txtEquipName" runat="server" Flex="1" FieldLabel="机台名称"  LabelAlign="Left" LabelPad="-15" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryEquipInfo" />
                                                <Change Fn="PptMaterialRefresh" />
                                            </Listeners>
                                        </ext:TriggerField>  
                                        <ext:DateField ID="txtBeginDate" runat="server" Flex="1" FieldLabel="开始生产时间"  LabelAlign="Left" LabelPad="-15" Editable="false" AllowBlank="false">
                                            <Listeners>
                                                <Change Fn="PptMaterialRefresh" />
                                            </Listeners>
                                        </ext:DateField>  
                                         <ext:ComboBox ID="ComboBox1" runat="server" Flex="1" SelectOnTab="true" Editable="false"  LabelAlign="Left" LabelPad="-15" FieldLabel="生产方式">
                                    </ext:ComboBox>
                              
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container5" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".3">
                                  <Items>
                                    <ext:ComboBox ID="txtPptMaterial" runat="server" Flex="1" SelectOnTab="true" Editable="false" FieldLabel="物料信息"
                                            ValueField="MaterialCode" DisplayField="MaterialName" QueryMode="Local" LabelAlign="Left" LabelPad="-15">
                                            <Store>
                                                <ext:Store ID="storeMaterial" runat="server" AutoLoad="false" OnReadData="storeMaterial_ReadData">
                                                    <Model>
                                                        <ext:Model ID="Model3" runat="server" IDProperty="MaterialCode">
                                                            <Fields>
                                                                <ext:ModelField Name="MaterialCode" Type="String" />
                                                                <ext:ModelField Name="MaterialName" Type="String" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                        </ext:ComboBox>
                                        <ext:DateField ID="txtEndDate" runat="server" Flex="1" FieldLabel="结束生产时间"  LabelAlign="Left" LabelPad="-15" Editable="false" AllowBlank="false">
                                            <Listeners>
                                                <Change Fn="PptMaterialRefresh" />
                                            </Listeners>
                                        </ext:DateField>                                     
                                  </Items>
                                </ext:Container>
                                <ext:Container ID="container6" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".3">
                                  <Items>
                                    <ext:ComboBox ID="txtWeightType" runat="server" Flex="1" SelectOnTab="true" Editable="false"  LabelAlign="Left" LabelPad="-15" FieldLabel="称量类型">
                                    </ext:ComboBox>

                                      <ext:ComboBox runat="server" ID="ComboBoxWorkShopId" FieldLabel="生产车间" LabelAlign="Left"  LabelPad="-15" Flex="1"
                            EmptyText="全部" Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="this.setValue('');" />
                                </Listeners>
                            </ext:ComboBox>
                                  </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                        <ext:Hidden ID="hiddenEquipCode" runat="server"></ext:Hidden>
                    </Items>
                </ext:Panel>
                <ext:TabPanel ID="tabPanel1" runat="server" Region="Center" ActiveIndex="0" DefaultBorder="false" AutoScroll="false" MinTabWidth="160">
                    <Items>
                        <ext:GridPanel ID="OverWeighGridPanel" runat="server" Frame="true" Region="West" Flex="1" Title="超差记录">
                            <TopBar>
                                <ext:Toolbar runat="server" ID="barUser">
                                    <Items>
                                        <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                        <ext:Button runat="server" Icon="Find" Text="查询-超差记录" ID="btnSearchOverWeigh">
                                            <Listeners>
                                                <Click Fn="btnSearchOverWeighClick"></Click>
                                            </Listeners>
                                        </ext:Button>
                                        <ext:ToolbarSeparator ID="toolbarSeparator1" />
                                        <ext:Button runat="server" Icon="PageExcel" Text="导出-超差记录" ID="btnExportOverWeigh">
                                            <Listeners>
                                                <Click Handler="$('#btnExportOverWeighSubmit').click();"></Click>
                                            </Listeners>
                                        </ext:Button>
                                        <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                        <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                        <ext:ToolbarFill ID="toolbarFill_end" />
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="gridPanel1Store" runat="server" PageSize="15">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.OverWeighGridPanelBindData" AutoDataBind="false" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="PlanDate" />
                                                <ext:ModelField Name="EquipName" />
                                                <ext:ModelField Name="RecipeMaterialName" />
                                                <ext:ModelField Name="MaterName" />
                                                <ext:ModelField Name="SetWeight" />
                                                <ext:ModelField Name="RealWeight" />
                                                <ext:ModelField Name="ErrorAllow" />
                                                <ext:ModelField Name="ErrorOut" />
                                                <ext:ModelField Name="weightype" />
                                                <ext:ModelField Name="realnum" />
                                                <ext:ModelField Name="num" />
                                                 <ext:ModelField Name="WeighState" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel1" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNum1" runat="server" Width="50" Align="Center" />
                                    <ext:Column ID="PlanDate" DataIndex="PlanDate" runat="server" Text="生产日期"  Width="200"/>
                                    <ext:Column ID="EquipName" DataIndex="EquipName" runat="server" Text="机台" Flex="1" Sortable="false" />
                                               <ext:Column ID="Column1" DataIndex="realnum" runat="server" Text="生产车数" Flex="1" Sortable="false" />
                                                          <ext:Column ID="Column2" DataIndex="num" runat="server" Text="车次" Flex="1" Sortable="false" />
                                    <ext:Column ID="RecipeMaterialName" DataIndex="RecipeMaterialName" runat="server" Text="配方名称" Flex="1" Sortable="false" />
                                    <ext:Column ID="MaterName" DataIndex="MaterName" runat="server" Text="物料名称" Flex="1" Sortable="false" />
                                    <ext:Column ID="SetWeight" DataIndex="SetWeight" runat="server" Text="设定重量" Flex="1" Sortable="false" />
                                    <ext:Column ID="RealWeight" DataIndex="RealWeight" runat="server" Text="实际称重" Flex="1" Sortable="false" />
                                    <ext:Column ID="ErrorAllow" DataIndex="ErrorAllow" runat="server" Text="设定误差" Flex="1" Sortable="false" />
                                    <ext:Column ID="ErrorOut" DataIndex="ErrorOut" runat="server" Text="实际误差" Flex="1" Sortable="false" />
                                      <ext:Column ID="Column3" DataIndex="weightype" runat="server" Text="称量类型" Flex="1" Sortable="false" />
                                          <ext:Column ID="Column15" DataIndex="WeighState" runat="server" Text="生产方式" Flex="1" Sortable="false" />
                                </Columns>
                            </ColumnModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="OverWeighGridPanelPageToolbar" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="progressBarPager2" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>
                        <ext:GridPanel ID="WeighRateGridPanel" runat="server" Frame="true" Region="West" Flex="1" Title="称量合格率">
                            <TopBar>
                                <ext:Toolbar runat="server" ID="Toolbar1">
                                    <Items>
                                        <ext:ToolbarSeparator ID="toolbarSeparator2" />
                                        <ext:Button runat="server" Icon="Find" Text="查询-称量合格率" ID="btnSearchWeighRate">
                                            <DirectEvents>
                                                <Click OnEvent="btnSearchWeighRateClick">
                                                    <EventMask  ShowMask="true" Target="Page"/>
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:ToolbarSeparator ID="toolbarSeparator3" />
                                        <ext:Button runat="server" Icon="PageExcel" Text="导出-称量合格率" ID="btnExportWeighRate">
                                            <Listeners>
                                                <Click Handler="$('#btnExportWeighRateSubmit').click();"></Click>
                                            </Listeners>
                                        </ext:Button>
                                        <ext:ToolbarSeparator ID="toolbarSeparator4" />
                                        <ext:ToolbarSpacer runat="server" ID="toolbarSpacer1" />
                                        <ext:ToolbarFill ID="toolbarFill1" />
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="Store1" runat="server">
                                    <Model>
                                        <ext:Model ID="model2" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel2" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Width="50" Align="Center" />
                                </Columns>
                            </ColumnModel>
                        </ext:GridPanel>

                           <ext:GridPanel ID="OverWeighGridPanel2" runat="server" Frame="true" Region="West" Flex="1" Title="称量记录">
                            <TopBar>
                                <ext:Toolbar runat="server" ID="Toolbar2">
                                    <Items>
                                        <ext:ToolbarSeparator ID="toolbarSeparator5" />
                                        <ext:Button runat="server" Icon="Find" Text="查询-称量记录" ID="Button1">
                                            <Listeners>
                                                <Click Fn="btnSearchOverWeighClick2"></Click>
                                            </Listeners>
                                        </ext:Button>
                                        <ext:ToolbarSeparator ID="toolbarSeparator6" />
                                        <ext:Button runat="server" Icon="PageExcel" Text="导出-称量记录" ID="Button2">
                                            <Listeners>
                                                <Click Handler="$('#btnExportOverWeighSubmit2').click();"></Click>
                                            </Listeners>
                                        </ext:Button>
                                        <ext:ToolbarSeparator ID="toolbarSeparator7" />
                                        <ext:ToolbarSpacer runat="server" ID="toolbarSpacer2" />
                                        <ext:ToolbarFill ID="toolbarFill2" />
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="Store2" runat="server" PageSize="15">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.OverWeighGridPanelBindData2" AutoDataBind="false" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model4" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="PlanDate" />
                                                <ext:ModelField Name="EquipName" />
                                                <ext:ModelField Name="RecipeMaterialName" />
                                                <ext:ModelField Name="MaterName" />
                                                <ext:ModelField Name="SetWeight" />
                                                <ext:ModelField Name="RealWeight" />
                                                <ext:ModelField Name="ErrorAllow" />
                                                <ext:ModelField Name="ErrorOut" />
                                                <ext:ModelField Name="weightype" />
                                                <ext:ModelField Name="realnum" />
                                                <ext:ModelField Name="num" />
                                                     <ext:ModelField Name="WeighState" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel3" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="RowNumbererColumn2" runat="server" Width="50" Align="Center" />
                                    <ext:Column ID="Column4" DataIndex="PlanDate" runat="server" Text="生产日期"  Width="200"  />
                                    <ext:Column ID="Column5" DataIndex="EquipName" runat="server" Text="机台" Flex="1" Sortable="false" />
                                               <ext:Column ID="Column6" DataIndex="realnum" runat="server" Text="生产车数" Flex="1" Sortable="false" />
                                                          <ext:Column ID="Column7" DataIndex="num" runat="server" Text="车次" Flex="1" Sortable="false" />
                                    <ext:Column ID="Column8" DataIndex="RecipeMaterialName" runat="server" Text="配方名称" Flex="1" Sortable="false" />
                                    <ext:Column ID="Column9" DataIndex="MaterName" runat="server" Text="物料名称" Flex="1" Sortable="false" />
                                    <ext:Column ID="Column10" DataIndex="SetWeight" runat="server" Text="设定重量" Flex="1" Sortable="false" />
                                    <ext:Column ID="Column11" DataIndex="RealWeight" runat="server" Text="实际称重" Flex="1" Sortable="false" />
                                    <ext:Column ID="Column12" DataIndex="ErrorAllow" runat="server" Text="设定误差" Flex="1" Sortable="false" />
                                    <ext:Column ID="Column13" DataIndex="ErrorOut" runat="server" Text="实际误差" Flex="1" Sortable="false" />
                                      <ext:Column ID="Column14" DataIndex="weightype" runat="server" Text="称量类型" Flex="1" Sortable="false" />
                                          <ext:Column ID="Column16" DataIndex="WeighState" runat="server" Text="生产方式" Flex="1" Sortable="false" />
                                </Columns>
                            </ColumnModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="OverWeighGridPanelPageToolbar2" runat="server">

                                  <Items>
                                   <ext:TextField ID="txtTotalWeight" runat="server" LabelAlign="Right" ReadOnly="true"  Weigh="300" Width="300"  />
                                    </Items>
                                    <Plugins>
                                        <ext:ProgressBarPager ID="progressBarPager1" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:TabPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
