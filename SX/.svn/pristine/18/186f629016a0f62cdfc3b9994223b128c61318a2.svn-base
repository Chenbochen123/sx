<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryFactoryMapping.aspx.cs" Inherits="Manager_RawMaterialQuality_QueryFactoryMapping" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>页面名称</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
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
            parent.Manager_RawMaterialQuality_QueryFactoryMapping_Request(record);
            parent.App.Manager_RawMaterialQuality_QueryFactoryMapping_Window.close();
            return false;
        }
        var commandColumn_click = function (command, record) {
            return response(command, record);
        };
        var cellDblClick = function (grid, td, tdindex, record, tr, trindex, e, fn) {
            return response('dblclick', record);
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
                                <ext:Container ID="container_Query" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container1" runat="server" Layout="FormLayout" Padding="5" ColumnWidth="1">
                                            <Items>
                                        <ext:ComboBox ID="cbxSeriesName" runat="server" FieldLabel="原材料系列" LabelAlign="Right"
                                           Editable="false" Flex="1" LabelWidth="80">
                                        </ext:ComboBox>
                                        <ext:TextField ID="txtSupplierName" runat="server" FieldLabel="供应商名称" LabelAlign="Right"
                                            Visible="true"  LabelWidth="80" Flex="1">
                                        </ext:TextField>                              
                                        <ext:TextField ID="txtManufacturerName" runat="server" FieldLabel="生产厂名称" LabelAlign="Right"
                                            Visible="true"  LabelWidth="80" Flex="1">
                                        </ext:TextField>                                  
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
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
                                        <ext:ModelField Name="MappingId" />
                                        <ext:ModelField Name="SeriesId" />
                                        <ext:ModelField Name="SupplierId" />
                                        <ext:ModelField Name="SeriesName" />
                                        <ext:ModelField Name="SupplierName" />
                                        <ext:ModelField Name="SupplierERPCode" />
                                        <ext:ModelField Name="ManufacturerId" />
                                        <ext:ModelField Name="ManufacturerName" />
                                        <ext:ModelField Name="ManufacturerERPCode" />
                                        <ext:ModelField Name="Remark" />
                                        <ext:ModelField Name="DeleteFlag" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="columnModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="30" />
                            <ext:Column ID="mappingId" runat="server" Text="关系ID" DataIndex="MappingId" Visible="false"/>
                            <ext:Column ID="supplierId" runat="server" Text="供应商ID" DataIndex="SupplierId" Visible="false"/>
                            <ext:Column ID="supplierERPCode" runat="server" Text="供应商ERP代码" DataIndex="SupplierERPCode" Visible="false"/>
                            <ext:Column ID="manufacturerERPCode" runat="server" Text="生产商ERP代码" DataIndex="ManufacturerERPCode" Visible="false"/>
                            <ext:Column ID="manufacturerId" runat="server" Text="生产商ID" DataIndex="ManufacturerId" Visible="false"/>
                            <ext:Column ID="seriesName" runat="server" Text="原材料系列" DataIndex="SeriesName" Flex="1"/>
                            <ext:Column ID="supplierName" runat="server" Text="供应商名称" DataIndex="SupplierName" Flex="1"/>
                            <ext:Column ID="manufacturerName" runat="server" Text="生产商名称" DataIndex="ManufacturerName" Flex="1"/>
                            <ext:CommandColumn ID="commandColumn" runat="server" Width="50" Text="确认" Align="Center">
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
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolbar" runat="server">
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
