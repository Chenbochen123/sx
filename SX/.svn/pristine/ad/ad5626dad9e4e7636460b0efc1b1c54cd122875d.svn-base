<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_QueryRecipe_QueryRecipeSimple, App_Web_bapt3n1v" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>工艺配方查询</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
       <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />

    <script src="<%= Page.ResolveUrl("~/") %>resources/js/default.js"></script>
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.gridPanel1.store.currentPage = 1;
            App.gridPanel1.store.reload();
            return false;
        }
    </script>
    <script type="text/javascript">
        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryWorkShop_Window",
            height: 450,
            hidden: true,
            width: 370,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryWorkShop.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择车间信息",
            modal: true
        })
        var Manager_BasicInfo_CommonPage_QueryWorkShop_Request = function (record) {
            var queryWindow = App.Manager_BasicInfo_CommonPage_QueryWorkShop_Window;
            var thisIsDefaultWindow = function (record) {
                App.txtRecipeWorkShopName.setValue(record.data.WorkShopName);
                App.txtRecipeWorkShopCode.setValue(record.data.ObjID);
                return;
            }
            App.txtRecipeWorkShopName.getTrigger(0).show();
            thisIsDefaultWindow(record);
            queryWindow.close();
        }
        var GetWorkShopInfo = function () {
            var queryWindowShow = function (record) {
                var queryWindow = App.Manager_BasicInfo_CommonPage_QueryWorkShop_Window;
                queryWindow.show();
            }
            queryWindowShow();
        }

        var txtRecipeWorkShopName_click = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.txtRecipeWorkShopName.setValue("");
                    App.txtRecipeWorkShopCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    GetWorkShopInfo();
                    break;
            }
        }
    </script>

    <script type="text/javascript">

        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            height: 450,
            hidden: true,
            width: 370,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择机台",
            modal: true
        })
        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {
            var queryWindow = App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window;
            var thisIsAddWindow = function (record) {
            }
            var thisIsEditWindow = function (record) {

            }
            var thisIsDefaultWindow = function (record) {
                App.txtRecipeEquipCode.setValue(record.data.EquipCode);
                App.txtRecipeEquipName.setValue(record.data.EquipName);
            }
            App.txtRecipeEquipName.getTrigger(0).show();
            thisIsAddWindow(record);
            thisIsEditWindow(record);
            thisIsDefaultWindow(record);
            queryWindow.close();
        }
        var QueryEquipmentInfo = function (field, trigger, index) {
            App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
        }

        var txtRecipeEquipName_click = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.txtRecipeEquipName.setValue("");
                    App.txtRecipeEquipCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    QueryEquipmentInfo();
                    break;
            }
        }
    </script>

    <script type="text/javascript">

        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            height: 450,
            hidden: true,
            width: 370,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料",
            modal: true
        })
        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {
            var queryWindow = App.Manager_BasicInfo_CommonPage_QueryMaterial_Window;
            var thisIsAddWindow = function (record) {
            }
            var thisIsEditWindow = function (record) {

            }
            var thisIsDefaultWindow = function (record) {
                App.txtRecipeMaterCode.setValue(record.data.MaterialCode);
                App.txtRecipeMaterName.setValue(record.data.MaterialName);
            }
            App.txtRecipeMaterName.getTrigger(0).show();
            thisIsAddWindow(record);
            thisIsEditWindow(record);
            thisIsDefaultWindow(record);
            queryWindow.close();
        }
        var QueryMaterInfo = function (field, trigger, index) {
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
        }

        var txtRecipeMaterName_click = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.txtRecipeMaterName.setValue("");
                    App.txtRecipeMaterCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    QueryMaterInfo();
                    break;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
       <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel20" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUser">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttAdd" runat="server" Html="查询需要对比的信息" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="gridPanelRefresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />    <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Container ID="container1" runat="server" Layout="AnchorLayout" AutoHeight="true" Padding="5">
                            <Items>
                                <ext:Container ID="container2" runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:TextField ID="txtRecipeName" runat="server" FieldLabel="配方名称" Flex="1" LabelAlign="Right" />
                                        <ext:TriggerField ID="txtRecipeMaterName" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="物料名称">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="txtRecipeMaterName_click" />
                                            </Listeners>
                                        </ext:TriggerField><ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" Flex="1" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container3" runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:TriggerField ID="txtRecipeWorkShopName" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="车间名称">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="txtRecipeWorkShopName_click" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:TriggerField ID="txtRecipeEquipName" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="机台名称">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="txtRecipeEquipName_click" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" Flex="1" LabelAlign="Right" />
                                        <ext:Hidden ID="txtRecipeWorkShopCode" runat="server"></ext:Hidden>
                                        <ext:Hidden ID="txtRecipeEquipCode" runat="server"></ext:Hidden>
                                        <ext:Hidden ID="txtRecipeMaterCode" runat="server"></ext:Hidden>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel1" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <Items>
                        <ext:GridPanel ID="gridPanel1" runat="server" Region="Center" Frame="true" Flex="1" >
                            <Store>
                                <ext:Store ID="gridPanel1Store" runat="server" PageSize="15">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="RecipeEquipCode" />
                                                <ext:ModelField Name="ShowEquipName" />
                                                <ext:ModelField Name="RecipeMaterialCode" />
                                                <ext:ModelField Name="WorkShopCode" />
                                                <ext:ModelField Name="ShowMaterialName" />
                                                <ext:ModelField Name="RecipeVersionID" />
                                                <ext:ModelField Name="RecipeName" />
                                                <ext:ModelField Name="RecipeTypeName" />
                                                <ext:ModelField Name="RecipeStateName" />
                                                <ext:ModelField Name="LotTotalWeight" />
                                                <ext:ModelField Name="ShelfLotCount" />
                                                <ext:ModelField Name="LotDoneTime" />
                                                <ext:ModelField Name="OverTimeSetTime" />
                                                <ext:ModelField Name="OverTempMinTime" />
                                                <ext:ModelField Name="InPolyMaxTemp" />
                                                <ext:ModelField Name="InPolyMinTemp" />
                                                <ext:ModelField Name="AuditFlagName" />
                                                <ext:ModelField Name="AuditUserName" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Sorters>
                                        <ext:DataSorter Property="SeqIdx" Direction="ASC" />
                                    </Sorters>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel1" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="30" />
                                    <ext:Column ID="col_ObjID" DataIndex="ObjID" runat="server" Text="配方编号" Align="Center" Width="80" />
                                    <ext:Column ID="col_RecipeName" DataIndex="RecipeName" runat="server" Text="配方名称" Align="Center" Width="100" />
                                    <ext:Column ID="col_ShowMaterialName" DataIndex="ShowMaterialName" runat="server" Text="物料名称" Align="Center" Width="100" />
                                    <ext:Column ID="col_WorkShopCode" DataIndex="WorkShopCode" runat="server" Text="车间名称" Align="Center" Width="100" />
                                    <ext:Column ID="col_ShowEquipName" DataIndex="ShowEquipName" runat="server" Text="机台名称" Align="Center" Width="100" />
                                    <ext:Column ID="col_RecipeTypeName" DataIndex="RecipeTypeName" runat="server" Text="配方类型" Align="Center" Width="60" />
                                    <ext:Column ID="col_RecipeStateName" DataIndex="RecipeStateName" runat="server" Text="配方状态" Align="Center" Width="60" />
                                    <ext:Column ID="col_RecipeVersionID" DataIndex="RecipeVersionID" runat="server" Text="版本号" Align="Center" Width="60" />
                                    <ext:Column ID="col_LotTotalWeight" DataIndex="LotTotalWeight" runat="server" Text="配方总重" Align="Center" Width="60" />
                                    <ext:Column ID="col_ShelfLotCount" DataIndex="ShelfLotCount" runat="server" Text="每架车数" Align="Center" Width="60" />
                                    <ext:Column ID="col_LotDoneTime" DataIndex="LotDoneTime" runat="server" Text="每车标准时间" Align="Center" Width="100" />
                                    <ext:Column ID="col_OverTimeSetTime" DataIndex="OverTimeSetTime" runat="server" Text="超温排胶时间" Align="Center" Width="100" />
                                    <ext:Column ID="col_OverTempMinTime" DataIndex="OverTempMinTime" runat="server" Text="超温排胶最短时间" Align="Center" Width="120" />
                                    <ext:Column ID="col_InPolyMaxTemp" DataIndex="InPolyMaxTemp" runat="server" Text="最高进胶温度" Align="Center" Width="100" />
                                    <ext:Column ID="col_InPolyMinTemp" DataIndex="InPolyMinTemp" runat="server" Text="最低进胶温度" Align="Center" Width="100" />
                                    <ext:Column ID="col_AuditUserName" DataIndex="AuditUserName" runat="server" Text="审核人" Align="Center" Width="80" />
                                    <ext:Column ID="col_AuditFlagName" DataIndex="AuditFlagName" runat="server" Text="审核状态" Align="Center" Width="80" />
                                </Columns>
                            </ColumnModel>
                                     <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                                   
                                    <Listeners>
                                        <Select Handler="#{storeDetail}.reload();" Buffer="250" />
                                    </Listeners>
                                </ext:RowSelectionModel>
                            </SelectionModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="pageToolbar1" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="progressBarPager1" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
                     <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="胶料称量信息" Height="200" Icon="Basket" Layout="Fit" Collapsible="true"
                    Split="true" MarginsSummary="0 5 5 5">
                    <Items>
                        <ext:GridPanel ID="pnlDetailList" runat="server" MarginsSummary="0 5 5 5">
                            <Store>
                                <ext:Store ID="storeDetail" runat="server" PageSize="10" OnReadData="RowSelect">
                                    <%-- <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindDetail" />
                                </Proxy>--%>
                                    <Model>
                                        <ext:Model ID="modelDetail" runat="server" IDProperty="BillNo, Barcode">
                                            <Fields>
                                      <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="WeightType" />
                                                <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                                <ext:ModelField Name="MaterialCode" Type="String" />
                                                <ext:ModelField Name="MaterialName" Type="String" />
                                                <ext:ModelField Name="showname" Type="String" />
                                                <ext:ModelField Name="OldSetWeight" Type="Float" />
                                                <ext:ModelField Name="SetWeight" Type="Float" />
                                                <ext:ModelField Name="ErrorAllow" Type="Float" />
                                                <ext:ModelField Name="materiallevel" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Parameters>
                                        <ext:StoreParameter Name="BillNo" Mode="Raw" Value="#{gridPanel1}.getSelectionModel().hasSelection() ? #{gridPanel1}.getSelectionModel().getSelection()[0].data.ObjID : -1" />
                                    </Parameters>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModelDetail" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Width="35" />

                                    <ext:Column ID="MaterialName" runat="server" DataIndex="RecipeMaterialCode"  Text="胶料名称" Flex="1" />
                                    <ext:Column ID="SendNum" runat="server" Text="物料编码" DataIndex="MaterialCode" Flex="1" />
                                    <ext:Column ID="SendWeight" runat="server" Text="物料级别" DataIndex="materiallevel" Flex="1" />
                                      <ext:Column ID="Column3" runat="server" Text="称量动作" DataIndex="showname" Flex="1" />
                                  <ext:Column ID="Column1" runat="server" Text="设定重量" DataIndex="SetWeight" Flex="1" />
                            
                                      <ext:Column ID="Column2" runat="server" Text="允许误差" DataIndex="ErrorAllow" Flex="1" />
                            
                            
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectMutiDetail" runat="server" Mode="Multi" />
                            </SelectionModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server">
                                    <Items>
                                        <ext:Label ID="Label2" runat="server" Text="每页条数:" />
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
                                                <Select Handler="#{pnlDetailList}.store.pageSize = parseInt(this.getValue(), 10); #{PagingToolbar1}.doRefresh(); return false;" />
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                    <Plugins>
                                        <ext:ProgressBarPager ID="ProgressBarPager2" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
