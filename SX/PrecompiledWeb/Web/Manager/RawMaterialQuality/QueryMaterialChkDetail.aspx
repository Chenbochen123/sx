<%@ page language="C#" autoeventwireup="true" inherits="Manager_RawMaterialQuality_QueryMaterialChkDetail, App_Web_pghqvuve" %>

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
            parent.Manager_RawMaterialQuality_QueryMaterialChkDetail_Request(record);
            parent.App.Manager_RawMaterialQuality_QueryMaterialChkDetail_Window.close();
            return false;
        }
        var commandColumn_click = function (command, record) {
            return response(command, record);
        };
        var cellDblClick = function (grid, td, tdindex, record, tr, trindex, e, fn) {
            return response('dblclick', record);
        }
    </script>

    <script type="text/javascript">
            var Manager_RawMaterialQuality_QueryRawMaterial_Request = function (record) {//物料信息返回值处理
                App.trfManualAddMaterialName.setValue(record.data.MaterialName);
                App.txtManualAddMaterialCode.setValue(record.data.MaterialCode);
            }

            var ManualSelectMaterial = function (item, trigger, index) {
                if (index == 0) {
                    // 清空
                    App.trfManualAddMaterialName.setValue("");
                    App.txtManualAddMaterialCode.setValue("");
                }
                else if (index == 1) {
                    Ext.create("Ext.window.Window", {//物料带窗体
                        id: "Manager_RawMaterialQuality_QueryRawMaterial_Window",
                        height: 450,
                        hidden: true,
                        width: 360,
                        html: "<iframe src='QueryRawMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
                        bodyStyle: "background-color: #fff;",
                        closable: true,
                        title: "请选择原材料",
                        modal: true
                    })
                    App.Manager_RawMaterialQuality_QueryRawMaterial_Window.show();
                }  
            }
            //------------查询带回弹出框--END 
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
                                        <ext:Container ID="container1" runat="server" Layout="FormLayout" Padding="2" ColumnWidth="0.45">
                                            <Items>
                                                <ext:TextField ID="txtBillNo" runat="server" FieldLabel="送检单号" LabelAlign="Right" LabelWidth="80" LabelPad="10"/>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container2" runat="server" Layout="FormLayout" Padding="2" ColumnWidth="0.45">
                                            <Items>
                                                <ext:ComboBox runat="server" ID="cbxStatus" FieldLabel="检测状态" LabelAlign="Right"
                                                    Editable="false" MatchFieldWidth="false" LabelWidth="80"  LabelPad="10">
                                                    <ListConfig Width="122" />
                                                    <Items>
                                                        <ext:ListItem Value="0" Text="未检测" />
                                                        <ext:ListItem Value="9" Text="检测中" />
                                                        <ext:ListItem Value="1" Text="检测合格" />
                                                        <ext:ListItem Value="2" Text="检测不合格" />
                                                    </Items>
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="this.setValue('');" />
                                                        <Select Fn="gridPanelRefresh" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container_Param" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container4" runat="server" Layout="FormLayout" Padding="2" ColumnWidth="0.45">
                                            <Items>
                                                <ext:TriggerField ID="trfManualAddMaterialName" runat="server" FieldLabel="送检原材料" LabelAlign="Right" 
                                                    Editable="false" LabelWidth="80" LabelPad="10" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="ManualSelectMaterial" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                 <ext:TextField ID="txtManualAddMaterialCode" runat="server" FieldLabel="物料编码" LabelAlign="Right"
                                                 Hidden="true" Enabled="true" Padding="5" >
                                                     <Listeners>
                                                         <Change Fn="gridPanelRefresh" />
                                                     </Listeners>
                                                 </ext:TextField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container3" runat="server" Layout="FormLayout" Padding="2" ColumnWidth="0.45">
                                            <Items>
                                                <ext:DateField ID="dtfSendTime" runat="server" FieldLabel="送检日期" LabelAlign="Right" Format="yyyy-MM-dd" LabelWidth="80" Editable="false" LabelPad="10" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="this.setValue('');" />
                                                        <Change Fn="gridPanelRefresh" />
                                                    </Listeners>
                                                </ext:DateField>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                                   <ext:Container ID="container5" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container6" runat="server" Layout="FormLayout" Padding="2" ColumnWidth="0.45">
                                            <Items>
                                                <ext:TextField ID="TextBarcode" runat="server" FieldLabel="条码号" LabelAlign="Right" LabelWidth="80" LabelPad="10"/>
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
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="BillNo" />
                                        <ext:ModelField Name="Barcode" />
                                        <ext:ModelField Name="OrderId" />
                                        <ext:ModelField Name="MaterCode" />
                                        <ext:ModelField Name="NoticeNo" />
                                        <ext:ModelField Name="MaterialName" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="columnModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="30" />
                            <ext:Column ID="objID" DataIndex="ObjID" runat="server" Text="ID" Hidden="true"/>
                            <ext:Column ID="materCode" DataIndex="MaterCode" runat="server" Text="MaterCode" Hidden="true"/>
                            <ext:Column ID="orderId" DataIndex="OrderId" runat="server" Text="顺序ID" Hidden="true"/>
                            <ext:Column ID="billNo" DataIndex="BillNo" runat="server" Text="送检单号" Width="100" />
                            <ext:Column ID="noticeNo" DataIndex="NoticeNo" runat="server" Text="通知单号" Width="100" />
                            <ext:Column ID="barCode" DataIndex="Barcode" runat="server" Text="条码号" Width="170" />
                            <ext:Column ID="materialName" DataIndex="MaterialName" runat="server" Text="物料名称" Width="100" />
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
