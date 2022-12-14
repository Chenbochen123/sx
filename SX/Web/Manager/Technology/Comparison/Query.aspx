<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Query.aspx.cs" Inherits="Manager_Technology_Comparison_Query" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>配方日志查询</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />

    <script src="<%= Page.ResolveUrl("~/") %>resources/js/default.js"></script>
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.gridPanel1.store.currentPage = 1;
            App.gridPanel1.store.reload();
            App.gridPanel2.store.currentPage = 1;
            App.gridPanel2.store.reload();
            return false;
        }
    </script>

    <!--特殊-->

    <script type="text/javascript">
        var btnComparisonClick = function () {
            var queryWindowShow = function (a, b) {
                var queryWindow = App.Manager_Technology_Comparison_Defaulte_Window;
                var html = "<iframe src='Default.aspx?a=" + a + "&b=" + b + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
                if (queryWindow.getBody()) {
                    queryWindow.getBody().update(html);
                } else {
                    queryWindow.html = html;
                }
                queryWindow.show();
            }
            var a = 0;
            try {
                a = App.gridPanel1.view.getSelectionModel().lastSelected.data.ObjID;
            }
            catch (ex) {
                Ext.Msg.alert('提示', "请在表（1）中选择信息！");
                return;
            }
            var b = 0;
            try {
                b = App.gridPanel2.view.getSelectionModel().lastSelected.data.ObjID;
            }
            catch (ex) {
                Ext.Msg.alert('提示', "请在表（2）中选择信息！");
                return;
            }
            if (a == b) {
                Ext.Msg.alert('提示', "选择的是相同信息，不需要对比！");
                return;
            }
            var data1 = App.gridPanel1.view.getSelectionModel().lastSelected.data;
            var data2 = App.gridPanel2.view.getSelectionModel().lastSelected.data;

            if (data1.RecipeEquipCode != data2.RecipeEquipCode) {
                Ext.Msg.alert('提示', "请选择相同的机台进行对比！");
                return;
            }
            if (data1.RecipeMaterialCode != data2.RecipeMaterialCode) {
                Ext.Msg.alert('提示', "请选择相同的物料进行对比！");
                return;
            }
            queryWindowShow(a, b);
            return false;
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
            title: "请选择物料信息",
            modal: true
        })
        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {
            var queryWindow = App.Manager_BasicInfo_CommonPage_QueryMaterial_Window;
            var thisIsDefaultWindow = function (record) {
                App.txtRecipeMaterialName.setValue(record.data.MaterialName);
                App.txtRecipeMaterialCode.setValue(record.data.MaterialCode);
                return;
            }
            App.txtRecipeMaterialName.getTrigger(0).show();
            thisIsDefaultWindow(record);
            queryWindow.close();
        }
        var GetMaterialInfo = function () {
            var queryWindowShow = function (record) {
                var queryWindow = App.Manager_BasicInfo_CommonPage_QueryMaterial_Window;
                queryWindow.show();
            }
            queryWindowShow();
        }

        var txtRecipeMaterialName_click = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.txtRecipeMaterialName.setValue("");
                    App.txtRecipeMaterialCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    GetMaterialInfo();
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
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel20" runat="server" Region="North" AutoHeight="true">
                    <BottomBar>
                        <ext:StatusBar ID="StatusBar1" runat="server" Height="24" Text=""></ext:StatusBar>
                    </BottomBar>
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
                                <ext:Button runat="server" Icon="ArrowSwitchBluegreen" Text="对比" ID="btnComparison">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="选择两个不同配方进行对比" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="btnComparisonClick"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Container ID="container1" runat="server" Layout="AnchorLayout" AutoHeight="true" Padding="5">
                            <Items>
                                <ext:Container ID="container2" runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" Flex="1" LabelAlign="Right" />
                                        <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" Flex="1" LabelAlign="Right" />
                                        <ext:ToolbarFill ID="ToolbarFill1" runat="server" Flex="1" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container3" runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:TriggerField ID="txtRecipeMaterialName" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="物料名称">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="txtRecipeMaterialName_click" />
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
                                        <ext:NumberField ID="txtRecipeVersionID" runat="server" LabelAlign="Right" Flex="1" MaxLength="4" DecimalPrecision="0" FieldLabel="配方版本">
                                        </ext:NumberField>
                                        <ext:Hidden ID="txtRecipeMaterialCode" runat="server"></ext:Hidden>
                                        <ext:Hidden ID="txtRecipeEquipCode" runat="server"></ext:Hidden>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel1" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <Items>
                        <ext:GridPanel ID="gridPanel1" runat="server" Region="West" Frame="true" Flex="1" Title="机台配方基本信息（1）">
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
                                                <ext:ModelField Name="ShowMaterialName" />
                                                <ext:ModelField Name="RecipeVersionID" />
                                                <ext:ModelField Name="RecipeName" />
                                                <ext:ModelField Name="RecipeType" />
                                                <ext:ModelField Name="RecipeTypeName" />
                                                <ext:ModelField Name="RecipeState" />
                                                <ext:ModelField Name="RecipeStateName" />
                                                <ext:ModelField Name="RecipeModifyUserName" />
                                                <ext:ModelField Name="LogRecordTime" Type="Date" />
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
                                    <ext:Column ID="ShowEquipName1" DataIndex="ShowEquipName" runat="server" Text="机台名称" Align="Center" Flex="1" />
                                    <ext:Column ID="ShowMaterialName1" DataIndex="ShowMaterialName" runat="server" Text="物料名称" Align="Center" Flex="1" />
                                    <ext:Column ID="RecipeVersionID1" DataIndex="RecipeVersionID" runat="server" Text="版本号" Align="Center" Width="60" />
                                    <ext:Column ID="RecipeModifyUserName1" DataIndex="RecipeModifyUserName" runat="server" Text="修改用户" Align="Center" Width="100" />
                                    <ext:DateColumn ID="LogRecordTime1" DataIndex="LogRecordTime" runat="server" Text="修改时间" Align="Center" Width="160" Format="yyyy-MM-dd HH:mm:ss" />
                                </Columns>
                            </ColumnModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="pageToolbar1" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="progressBarPager1" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>

                        <ext:GridPanel ID="gridPanel2" runat="server" Region="Center" Frame="true" Flex="1" Title="机台配方基本信息（2）">
                            <Store>
                                <ext:Store ID="gridPanel2Store" runat="server" PageSize="15">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="RecipeEquipCode" />
                                                <ext:ModelField Name="ShowEquipName" />
                                                <ext:ModelField Name="RecipeMaterialCode" />
                                                <ext:ModelField Name="ShowMaterialName" />
                                                <ext:ModelField Name="RecipeVersionID" />
                                                <ext:ModelField Name="RecipeName" />
                                                <ext:ModelField Name="RecipeType" />
                                                <ext:ModelField Name="RecipeTypeName" />
                                                <ext:ModelField Name="RecipeState" />
                                                <ext:ModelField Name="RecipeStateName" />
                                                <ext:ModelField Name="RecipeModifyUserName" />
                                                <ext:ModelField Name="LogRecordTime" Type="Date" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Sorters>
                                        <ext:DataSorter Property="SeqIdx" Direction="ASC" />
                                    </Sorters>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel2" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol2" runat="server" Width="30" />
                                    <ext:Column ID="ShowEquipName2" DataIndex="ShowEquipName" runat="server" Text="机台名称" Align="Center" Flex="1" />
                                    <ext:Column ID="ShowMaterialName2" DataIndex="ShowMaterialName" runat="server" Text="物料名称" Align="Center" Flex="1" />
                                    <ext:Column ID="RecipeVersionID2" DataIndex="RecipeVersionID" runat="server" Text="版本号" Align="Center" Width="60" />
                                    <ext:Column ID="RecipeModifyUserName2" DataIndex="RecipeModifyUserName" runat="server" Text="修改用户" Align="Center" Width="100" />
                                    <ext:DateColumn ID="LogRecordTime2" DataIndex="LogRecordTime" runat="server" Text="修改时间" Align="Center" Width="160" Format="yyyy-MM-dd HH:mm:ss" />
                                </Columns>
                            </ColumnModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar2" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="progressBarPager2" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
        <ext:Window ID="Manager_Technology_Comparison_Defaulte_Window" runat="server" Maximized="true" Title="工艺配方对比"  Modal="true" Closable="true" Hidden="true" Html="<iframe src='Default.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>">
            <TopBar>
                <ext:Toolbar runat="server" ID="Toolbar1">
                    <Items>
                        <ext:ToolbarSeparator ID="toolbarSeparator1" />
                        <ext:Button runat="server" Icon="Cancel" Text="关闭对比页面" ID="Button1">
                           <Listeners>
                               <Click Handler="#{Manager_Technology_Comparison_Defaulte_Window}.close()"></Click>
                           </Listeners>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
        </ext:Window>
    </form>
</body>
</html>
