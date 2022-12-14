<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_BasicInfo_EquipJarMaterialLog, App_Web_xrwstxsv" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>料仓物料更改日志查询</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
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
                App.txtEquipCode.setValue(record.data.EquipCode);
                App.txtEquipName.setValue(record.data.EquipName);
            }
            App.txtEquipName.getTrigger(0).show();
            thisIsAddWindow(record);
            thisIsEditWindow(record);
            thisIsDefaultWindow(record);
            queryWindow.close();
        }
        var QueryEquipmentInfo = function (field, trigger, index) {
            App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
        }

        var txtEquipName_click = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.txtEquipName.setValue("");
                    App.txtEquipCode.setValue("");
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
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Container ID="container1" runat="server" Layout="AnchorLayout" AutoHeight="true" Padding="5">
                            <Items>
                                <ext:Container ID="container2" runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="txtJarType" runat="server" FieldLabel="料仓类型" Flex="1" LabelAlign="Right" Editable="false" />
                                        <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="操作开始时间" Flex="1" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container3" runat="server" Layout="HBoxLayout" Padding="5">
                                    <Items>
                                        <ext:TriggerField ID="txtEquipName" runat="server" Flex="1" SelectOnTab="true" Editable="false" LabelAlign="Right" FieldLabel="机台名称">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="txtEquipName_click" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:DateField ID="txtEndTime" runat="server" FieldLabel="操作结束时间" Flex="1" LabelAlign="Right" />
                                        <ext:Hidden ID="txtEquipCode" runat="server"></ext:Hidden>
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
                                                <ext:ModelField Name="Priority" />
                                                <ext:ModelField Name="EquipName" />
                                                <ext:ModelField Name="EquipTypeName" />
                                                <ext:ModelField Name="JarType" />
                                                <ext:ModelField Name="StoragePlaceCodeBefore" />
                                                <ext:ModelField Name="StoragePlaceCodeAfter" />
                                                <ext:ModelField Name="StoragePlaceNameBefore" />
                                                <ext:ModelField Name="StoragePlaceNameAfter" />
                                                <ext:ModelField Name="StorageIDBefore" />
                                                <ext:ModelField Name="StorageIDAfter" />
                                                <ext:ModelField Name="StorageNameBefore" />
                                                <ext:ModelField Name="StorageNameAfter" />
                                                <ext:ModelField Name="MaterialCodeBefore" />
                                                <ext:ModelField Name="MaterialCodeAfter" />
                                                <ext:ModelField Name="MaterialNameBefore" />
                                                <ext:ModelField Name="MaterialNameAfter" />
                                                <ext:ModelField Name="WorkIDBefore" />
                                                <ext:ModelField Name="WorkIDAfter" />
                                                <ext:ModelField Name="OperDate" />
                                                <ext:ModelField Name="OperCode" />
                                                <ext:ModelField Name="DeleteFlag" />
                                                <ext:ModelField Name="Remark" />
                                                <ext:ModelField Name="Ext_1" />
                                                <ext:ModelField Name="Ext_2" />
                                                <ext:ModelField Name="Ext_3" />
                                                <ext:ModelField Name="Ext_4" />
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
                                    <ext:Column ID="col_ObjID" DataIndex="ObjID" runat="server" Text="编号" Align="Center" Width="40"  Hidden="true"/>
                                    <ext:Column ID="col_EquipName" DataIndex="EquipName" runat="server" Text="设备名称" Align="Center" Width="120" />
                                    <ext:Column ID="col_EquipTypeName" DataIndex="EquipTypeName" runat="server" Text="设备类型" Align="Center" Width="80" />
                                    <ext:Column ID="col_JarType" DataIndex="JarType" runat="server" Text="料仓类型" Align="Center" Width="60" />
                                    <ext:Column ID="col_Priority" DataIndex="Priority" runat="server" Text="优先级" Align="Center" Width="60" />
                                    <ext:Column ID="col_StorageNameBefore" DataIndex="StorageNameBefore" runat="server" Text="改前仓库名称" Align="Center" Width="100" />
                                    <ext:Column ID="col_StorageNameAfter" DataIndex="StorageNameAfter" runat="server" Text="改后仓库名称" Align="Center" Width="100" />
                                    <ext:Column ID="col_StoragePlaceNameBefore" DataIndex="StoragePlaceNameBefore" runat="server" Text="改前仓位名称" Align="Center" Width="150" />
                                    <ext:Column ID="col_StoragePlaceNameAfter" DataIndex="StoragePlaceNameAfter" runat="server" Text="改后仓位名称" Align="Center" Width="150" />
                                    <ext:Column ID="col_MaterialNameBefore" DataIndex="MaterialNameBefore" runat="server" Text="改前物料名称" Align="Center" Width="150" />
                                    <ext:Column ID="col_MaterialNameAfter" DataIndex="MaterialNameAfter" runat="server" Text="改后物料名称" Align="Center" Width="150" />
                                    <ext:DateColumn Format="yyyy-MM-dd" ID="col_OperDate" DataIndex="OperDate" runat="server" Text="操作时间" Align="Center" Width="80" />
                                    <ext:Column ID="col_OperCode" DataIndex="OperCode" runat="server" Text="操作人" Align="Center" Width="80" />
                                    <ext:Column ID="col_DeleteFlag" DataIndex="DeleteFlag" runat="server" Text="是否删除" Align="Center" Width="60" Hidden="true" />
                                    <ext:Column ID="col_Remark" DataIndex="Remark" runat="server" Text="备注" Align="Center" Width="60" Hidden="true" />
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
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
