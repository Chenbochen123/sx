<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageAlarm.aspx.cs" Inherits="Manager_Storage_StorageAlarm" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell
        {
        	background-color: #FF8C69 !important;
        }
        .x-grid-row-collapsed1 .x-grid-cell
        {
        	background-color: #3377FF !important;
        }
    </style>
    <script type="text/javascript">
        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        var AddStorage = function (field, trigger, index) {//库房添加
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenStorageID.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
                    break;
            }
        }

        var QueryMaterial = function (field, trigger, index) {//物料查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenMaterCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
                    break;
            }
        }

        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            App.txtStorageName.getTrigger(0).show();
            App.txtStorageName.setValue(record.data.StorageName);
            App.hiddenStorageID.setValue(record.data.StorageID);
            App.pageToolBar.doRefresh();
        }

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            App.txtMaterName.getTrigger(0).show();
            App.txtMaterName.setValue(record.data.MaterialName);
            App.hiddenMaterCode.setValue(record.data.MaterialCode);
            App.pageToolBar.doRefresh();
        }

        Ext.create("Ext.window.Window", {//库房带窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasStorage_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryBasStorage.aspx?StorageType=0&&LastStorageFlag=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择库房名称",
            modal: true
        })

        Ext.create("Ext.window.Window", {//物料带窗体
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料",
            modal: true
        })

        var alarmChange = function (value) {
            if (value == "0")
                return Ext.String.format("正常");
            if (value == "1")
                return Ext.String.format("超出最大库存要求");
            if (value == "2")
                return Ext.String.format("低于最小库存要求");
        };

        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("AlarmFlag") == "1") {
                return "x-grid-row-collapsed";
            }
            if (record.get("AlarmFlag") == "2") {
                return "x-grid-row-collapsed1";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="rmStorage" runat="server" />
        <ext:Viewport ID="vpStorage" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnStorageTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="tbStorage">
                            <Items>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <Listeners>
                                        <Click Fn="pnlListFresh" />
                                    </Listeners>
                                </ext:Button>
                                 <ext:Button runat="server" Icon="LockEdit" Text="库存备注" ID="Button1">
                                
                                    <DirectEvents>
                                        <Click OnEvent="ButtonSH_Click"></Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsBegin" />
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
                                        <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33" Padding="5" Hidden="true">
                                            <Items>
                                                <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="库房名称" LabelAlign="Right"
                                                    Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="AddStorage" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33" Padding="5">
                                            <Items>
                                                <ext:TriggerField ID="txtMaterName" runat="server" FieldLabel="物料名称" LabelAlign="Right"
                                                    Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="QueryMaterial" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".33" Padding="5">
                                            <Items>
                                                <ext:ComboBox ID="cbxAlarmFlag" runat="server" OnDirectChange="cbxAlarmFlag_change" FieldLabel="预警标志" LabelAlign="Right">
                                                    <Items>
                                                        <ext:ListItem Text="全部库存" Value="all" AutoDataBind="true"></ext:ListItem>
                                                        <ext:ListItem Text="全部预警" Value="0"></ext:ListItem>
                                                        <ext:ListItem Text="超量预警" Value="1"></ext:ListItem>
                                                        <ext:ListItem Text="低量预警" Value="2"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
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
                                <ext:Model ID="model" runat="server" IDProperty="MaterCode">
                                    <Fields>
                                        <%--<ext:ModelField Name="StorageID" />
                                        <ext:ModelField Name="StorageName" />--%>
                                        <ext:ModelField Name="MaterCode" />
                                        <ext:ModelField Name="MaterialName" />
                                        <ext:ModelField Name="TotalWeight" Type="Float" />
                                        <ext:ModelField Name="MaxStock" />
                                        <ext:ModelField Name="MinStock" />
                                        <ext:ModelField Name="AlarmFlag" />
                                          <ext:ModelField Name="GFlag" />
                                        <ext:ModelField Name="BFlag" />
                                         <ext:ModelField Name="Reamrk" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                            <%--<ext:Column ID="storageID" runat="server" Text="库房编号" DataIndex="StorageID" Hidden="true" />
                            <ext:Column ID="storageName" runat="server" Text="库房名称" DataIndex="StorageName" Flex="1" />--%>
                            <ext:Column ID="MaterCode" runat="server" Text="物料编号" DataIndex="MaterCode" Flex="1" Hidden="true" />
                            <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                            <ext:Column ID="TotalWeight" runat="server" Text="重量" DataIndex="TotalWeight" Flex="1" />
                            <ext:Column ID="MaxStock" runat="server" Text="最大库存" DataIndex="MaxStock" Flex="1" />
                            <ext:Column ID="MinStock" runat="server" Text="最小库存" DataIndex="MinStock" Flex="1" />
                              <ext:Column ID="Column1" runat="server" Text="超期库存" DataIndex="GFlag" Flex="1" />
                                <ext:Column ID="Column2" runat="server" Text="报警库存" DataIndex="BFlag" Flex="1" />
                                <ext:Column ID="Reamrk" runat="server" Text="预警备注" DataIndex="Reamrk" Flex="1" />
                            <ext:Column ID="AlarmFlag" runat="server" Text="预警标志" DataIndex="AlarmFlag" Flex="1" >
                             
                                <Renderer Fn="alarmChange" />
                            </ext:Column>
                              
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single" />
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
            </Items>
        </ext:Viewport>

         <ext:Window runat="server" ID="WindowCY" Title="库存备注" Width="400" Height="150"
        Hidden="true" Modal="false">
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" ShadowMode="Frame">
                <Items>
                    <ext:TextField runat="server" ID="TxtMatercode" FieldLabel="物料号" LabelAlign="Right"
                        ReadOnly="true" />
                           <ext:TextField runat="server" ID="TxtMaterialName" FieldLabel="物料名称" LabelAlign="Right"
                        ReadOnly="true" />
                      
                    <ext:TextField runat="server" ID="TxtRemark" FieldLabel="库存备注" LabelAlign="Right"
                        Width="350" />
                </Items>
            </ext:FormPanel>
        </Items>
        <Buttons>
            <ext:Button runat="server" ID="Button4" Icon="Accept" Text="确定">
                <DirectEvents>
                    <Click OnEvent="ButtonEditCY_Click">
                       <%-- <Confirmation Title="提示" Message="确定要掺用吗" ConfirmRequest="true" />--%>
                    </Click>
                </DirectEvents>
            </ext:Button>
            <ext:Button runat="server" ID="Button5" Icon="Cancel" Text="取消">
                <Listeners>
                    <Click Handler="#{WindowCY}.close();" />
                </Listeners>
            </ext:Button>
        </Buttons>
        <Listeners>
            <Show Handler="Ext.fly('form1').mask();" />
            <Hide Handler="Ext.fly('form1').unmask();" />
        </Listeners>
    </ext:Window>



        <ext:Hidden ID="hiddenStorageID" runat="server" />
        <ext:Hidden ID="hiddenMaterCode" runat="server" />
    </form>
</body>
</html>
