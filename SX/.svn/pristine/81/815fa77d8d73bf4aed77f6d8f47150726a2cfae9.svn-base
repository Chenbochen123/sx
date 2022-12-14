<%@ page language="C#" autoeventwireup="true" inherits="Manager_BasicInfo_CommonPage_QueryFactory, App_Web_rgel41dj" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>查询厂家</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell
        {
        	background-color: #FF8C69 !important;
        }
    </style>
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.store.currentPage = 1;
            App.pageToolbar.doRefresh();
            return false;
        }
    </script>

    <!--特殊-->
    <script type="text/javascript">
        var response = function (command, record) {
            parent.Manager_BasicInfo_CommonPage_QueryFactory_Request(record);
            parent.App.Manager_BasicInfo_CommonPage_QueryFactory_Window.close();
            return false;
        }
        var commandColumn_click = function (command, record) {
            return response(command, record);
        };
        var cellDblClick = function (grid, td, tdindex, record, tr, trindex, e, fn) {
            return response('dblclick', record);
        }

        var Manager_BasicInfo_CommonPage_QueryFactoryType_Request = function (record) {//厂商类别返回值处理
            App.txt_fac_type.getTrigger(0).show();
            App.txt_fac_type.setValue(record.data.FactoryTypeName);
            App.hiddenSelectFacType.setValue(record.data.ObjID);
        }

        var QueryFactoryType = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenSelectFacType.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryFactoryType_Window.show();
                    break;
            }
        }

        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryFactoryType_Window",
            height: 420,
            hidden: true,
            width: 320,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryFactoryType.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择厂商类别",
            modal: true
        })

        var deleteChange = function (value) {
            return Ext.String.format(value=="1" ? "是" : "否");
        };

        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("DeleteFlag") == "1") {
                return "x-grid-row-collapsed";
            }
        }
    </script>
</head>
<body>
    <form id="form" runat="server">
        <ext:ResourceManager ID="resourceManager" runat="server" />
        <ext:Viewport ID="viewport" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="northPanel" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="northToolbar">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="gridPanelRefresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel ID="panelNorthQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".34">
                                            <Items>
                                                <ext:TextField ID="txtFactoryName" runat="server" FieldLabel="厂家名称" LabelWidth="60" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33">
                                            <Items>
                                                <ext:TriggerField ID="txt_fac_type" runat="server" FieldLabel="所属类别" LabelWidth="60" LabelAlign="Right" Editable="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="QueryFactoryType" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".33">
                                            <Items>
                                                <ext:ComboBox ID="cbxDeleteFlag" runat="server" FieldLabel="是否删除" LabelWidth="60" LabelAlign="Right" Hidden="true">
                                                    <SelectedItems>
                                                        <ext:ListItem Value="all"></ext:ListItem>
                                                    </SelectedItems>
                                                    <Items>
                                                        <ext:ListItem Text="全部" Value="all" AutoDataBind="true"></ext:ListItem>
                                                        <ext:ListItem Text="是" Value="1"></ext:ListItem>
                                                        <ext:ListItem Text="否" Value="0"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container> 
                                    </Items>
                                </ext:FormPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="gridPanelCenter" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="10">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="FacName" />
                                        <ext:ModelField Name="FacSimpleName" />
                                        <ext:ModelField Name="FacType" />
                                        <ext:ModelField Name="DeleteFlag" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="columnModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="30" />
                            <ext:Column ID="FacName" DataIndex="FacName" runat="server" Text="厂家名称" Width="100" />
                            <ext:Column ID="FacSimpleName" DataIndex="FacSimpleName" runat="server" Text="厂家简称" Width="90" />
                            <ext:Column ID="FacType" DataIndex="FacType" runat="server" Text="厂家类别" Flex="1" />
                            <ext:Column ID="DeleteFlag" DataIndex="DeleteFlag" runat="server" Text="删除标志" Hidden="true" Flex="1">
                                <Renderer Fn="deleteChange" />
                            </ext:Column>
                            <ext:CommandColumn ID="commandColumn" runat="server" Width="60" Text="确认" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="accept" CommandName="Select" Text="确认">
                                        <ToolTip Text="确认使用本条数据" />
                                    </ext:GridCommand>
                                </Commands>
                                <PrepareToolbar />
                                <Listeners>
                                    <Command Handler="return commandColumn_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <Listeners>
                        <CellDblClick Fn="cellDblClick" />
                    </Listeners>
                    <View>
                        <ext:GridView ID="gvRows" runat="server">
                            <GetRowClass Fn="SetRowClass" />
                        </ext:GridView>
                    </View>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolbar" runat="server">
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
                <ext:Hidden ID="hiddenSelectFacType" runat="server" />
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
