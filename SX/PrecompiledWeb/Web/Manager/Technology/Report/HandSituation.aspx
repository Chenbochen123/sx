﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_Report_ConReport, App_Web_0yyi2bmr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript">
        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
        }

        //点击修改按钮

        var QueryUser = function (field, trigger, index) {
            App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();
        }

        var QueryEquipmentInfo = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenEquipCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
            }
        }

        var AddEquipmentInfo = function (field, trigger, index) {
            App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
        }

      

      
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmMmShopout" runat="server" />
    <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
    <ext:Viewport ID="vpMmShopout" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnMmShopoutTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbMmShopout">
                        <Items>
                            <ext:ToolbarSeparator ID="tsBegin" />
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <Listeners>
                                    <Click Fn="pnlListFresh" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                <Listeners>
                                    <Click Handler="$('#btnExportSubmit').click();"></Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsEnd" />
                            <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                            <ext:ToolbarFill ID="tfEnd" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel ID="pnlStorageQuery" runat="server" AutoHeight="true">
                        <Items>
                            <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                  
                                    <ext:Container ID="Container4" runat="server" Layout="FormLayout" ColumnWidth=".30" 
                                        Padding="2">
                                        <Items>
                                            <ext:ComboBox ID="cbxjitai" runat="server" FieldLabel="机台" LabelAlign="Right" Editable="false">
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container5" runat="server" Layout="FormLayout" ColumnWidth=".15" 
                                        Padding="2">
                                          <Items>
                                     <ext:ComboBox ID="cbxChejian" runat="server" FieldLabel="车间" LabelAlign="Right" Editable="false">
                                                <Items>
                                                     <ext:ListItem Text="全部" Value="">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M2车间" Value="2">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M3车间" Value="3">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M4车间" Value="4">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M5车间" Value="5">
                                                    </ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                                </Items>
                                            </ext:Container>
                                     <ext:Container ID="Container1" runat="server" Layout="FormLayout"  ColumnWidth=".15" 
                                        Padding="2">
                                        <Items>
                                           
                                        <ext:TextField ID="txt_name" runat="server" FieldLabel="配方名称" LabelAlign="Right" />
                                        
                                        </Items>
                                    </ext:Container>
                                         <ext:Container ID="Container2" runat="server" Layout="FormLayout" ColumnWidth=".20" 
                                        Padding="2">
                                        <Items>
                                          <ext:DateField ID="txtBeginDate" runat="server" Flex="1" FieldLabel="生产时间"  LabelAlign="Left" LabelPad="-15" Editable="false" AllowBlank="false">
                                           
                                        </ext:DateField>  
                                        
                                        </Items>
                                    </ext:Container>

                                </Items>
                                <Listeners>
                                    <ValidityChange Handler="#{btnSearch}.setDisabled(!valid);" />
                                </Listeners>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>

            <ext:GridPanel ID="pnlList" runat="server" Cls="x-grid-custom" Region="Center">
                <Store>
                    <ext:Store ID="store" runat="server" PageSize="15"> 
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="model" runat="server">
                                <Fields>
                                      <ext:ModelField Name="type" />
                                      <ext:ModelField Name="barcode" />
                                      <ext:ModelField Name="WorkShop" />
                                      <ext:ModelField Name="equip" />
                                      <ext:ModelField Name="date" />
                                      <ext:ModelField Name="mater" />

                                      <ext:ModelField Name="materY" />
                                      <ext:ModelField Name="hand" />
  
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="colModel" runat="server">
                    <Columns>
                        <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                         <ext:Column ID="Column5" runat="server" Text="手动情况分类" DataIndex="type" Width="135"/>
                         <ext:Column ID="RecipeMaterialName" runat="server" Text="条码" DataIndex="barcode" Width="135"/>
                          <ext:Column ID="Column3" runat="server" Text="厂区" DataIndex="WorkShop" Width="135"/>
                          <ext:Column ID="Column2" runat="server" Text="机台" DataIndex="equip" Width="135"/>
                          <ext:Column ID="EquipName" runat="server" Text="计划时间" DataIndex="date" Width="135"/>
                          <ext:Column ID="LotTotalWeight" runat="server" Text="配方"  DataIndex="mater" Width="135" />
                          <ext:Column ID="Column1" runat="server" Text="物料名称"  DataIndex="materY" Width="135" />
                          <ext:Column ID="Column4" runat="server" Text="状态"  DataIndex="hand" Width="135" />
                    </Columns> 
                </ColumnModel>
                <SelectionModel>
                    <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single" />
                </SelectionModel>
                <BottomBar>
                    <ext:PagingToolbar ID="pageToolBar" runat="server">
                        <Items>
                         <%--   <ext:Label ID="Label2" runat="server" Text="每页条数:" />
                            <ext:ToolbarSpacer ID="ToolbarSpacer2" runat="server" Width="10" />
                            <ext:ComboBox ID="ComboBox2" runat="server" Width="80">
                                <Items>
                                    <ext:ListItem Text="10" />
                                    <ext:ListItem Text="50" />
                                    <ext:ListItem Text="100" />
                                    <ext:ListItem Text="200" />
                                </Items>
                                <SelectedItems>
                                    <ext:ListItem Value="10" />
                                </SelectedItems>
                                <Listeners>
                                    <Select Handler="#{pnlList}.store.pageSize = parseInt(this.getValue(), 10); #{pageToolBar}.doRefresh(); return false;" />
                                </Listeners>
                            </ext:ComboBox>--%>
                        </Items>
                        <Plugins>
                            <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                        </Plugins>
                    </ext:PagingToolbar>
                </BottomBar>
            </ext:GridPanel>

          
           
            <ext:Hidden ID="hiddenUserID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenEquipCode" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenMaterCode" runat="server"></ext:Hidden>
       
        
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
