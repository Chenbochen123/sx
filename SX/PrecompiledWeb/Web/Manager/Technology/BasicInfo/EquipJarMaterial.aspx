﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_BasicInfo_EquipJarMaterial, App_Web_xrwstxsv" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>料仓物料对应</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            App.store.currentPage = 1;
            App.store.reload();
            return false;
        }
    </script>

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>EquipJarMaterial.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <script type="text/javascript">
        var treePanelBeforeLoad = function (store, operation, options) {
            var node = operation.node;
            var nodeid = node.getId() || "";
            App.direct.treePanelBeforeLoad(nodeid, {
                success: function (result) {
                    node.set('loading', false);
                    node.set('loaded', true);
                    var data = Ext.decode(result);
                    node.appendChild(data, undefined, true);
                    node.expand();
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
            return false;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Region="West" Title="机台设备" Width="200" Layout="BorderLayout">
                    <BottomBar>
                        <ext:StatusBar ID="StatusBar1" runat="server" Height="24" Text=""></ext:StatusBar>
                    </BottomBar>
                    <Items>
                        <ext:TreePanel ID="treePanel1" runat="server" Title="机台设备" Icon="FolderGo" AutoHeight="true" Region="Center" RootVisible="false">
                            <Store>
                                <ext:TreeStore ID="treeStore1" runat="server">
                                    <Proxy>
                                        <ext:PageProxy>
                                            <RequestConfig Method="GET" Type="Load" />
                                        </ext:PageProxy>
                                    </Proxy>
                                    <Root>
                                        <ext:Node NodeID="Root" Expanded="true" />
                                    </Root>
                                </ext:TreeStore>
                            </Store>
                            <Listeners>
                                <BeforeLoad Fn="treePanelBeforeLoad" />
                            </Listeners>
                            <DirectEvents>
                                <Select OnEvent="BindGridPanelData" />
                            </DirectEvents>
                        </ext:TreePanel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="GridPanel1" runat="server" Region="Center" Title="料仓物料对应" Layout="BorderLayout">
                    <Items>
                        <ext:Panel ID="Panel20" runat="server" Region="North" AutoHeight="true">
                            <Items>
                                <ext:Container ID="container1" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container2" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".45">
                                            <Items>
                                                <ext:TextField ID="txtEquipCode" runat="server" FieldLabel="设备编号" LabelAlign="Right" ReadOnly="true" />
                                                <ext:TextField ID="txtEquipName" runat="server" FieldLabel="设备名称" LabelAlign="Right" ReadOnly="true" />
                                                <ext:TriggerField ID="txtMaterialMinorName" runat="server" FieldLabel="物料细类" LabelAlign="Right" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="GetMaterialMinorInfo" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container3" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".45">
                                            <Items>
                                                <ext:TextField ID="txtEquipJarType" runat="server" FieldLabel="设备类型" LabelAlign="Right" ReadOnly="true" />
                                                <ext:TextField ID="txtJarName" runat="server" FieldLabel="料仓分类" LabelAlign="Right" ReadOnly="true" />
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                                <ext:Hidden ID="txtEquipID" runat="server"></ext:Hidden>
                                <ext:Hidden ID="txtJarTypeID" runat="server"></ext:Hidden>
                                <ext:Hidden ID="txtMaterialMinorID" runat="server"></ext:Hidden>
                            </Items>
                        </ext:Panel>
                        <ext:GridPanel ID="gridPanelCenter" runat="server" Region="Center">
                            <Store>
                                <ext:Store ID="store" runat="server" PageSize="30">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="JarNum" />
                                                <ext:ModelField Name="Priority" />
                                                <ext:ModelField Name="StorageID" />
                                                <ext:ModelField Name="StorageName" />
                                                <ext:ModelField Name="StoragePlaceID" />
                                                <ext:ModelField Name="StoragePlaceName" />
                                                <ext:ModelField Name="MinorTypeID" />
                                                <ext:ModelField Name="MinorTypeName" />
                                                <ext:ModelField Name="MaterialCode" />
                                                <ext:ModelField Name="MaterialName" />
                                                <ext:ModelField Name="WorkID" />
                                                <ext:ModelField Name="DeleteFlag" />
                                                 <ext:ModelField Name="SingleWeight" />
                                                  <ext:ModelField Name="Supply" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="columnModel" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="30" />
                                    <ext:Column ID="JarNum" DataIndex="JarNum" runat="server" Text="料仓序号" Width="80" />
                                    <ext:Column ID="MaterialName" DataIndex="MaterialName" runat="server" Text="物料名称" Flex="1" />
                                    <ext:Column ID="MinorTypeName" DataIndex="MinorTypeName" runat="server" Text="物料细类" Width="120" />
                                    <ext:Column ID="Priority" DataIndex="Priority" runat="server" Text="料仓优先级" Width="80" />
                                    <ext:Column ID="StorageName" DataIndex="StorageName" runat="server" Text="仓库名称" Width="120" />
                                    <ext:Column ID="StoragePlaceName" DataIndex="StoragePlaceName" runat="server" Text="库位名称" Width="120" />
                                    <ext:Column ID="WorkID" DataIndex="WorkID" runat="server" Text="工位" Width="80" />
                                     <ext:Column ID="Column2" DataIndex="SingleWeight" runat="server" Text="最大单包重量" Width="80" />
                                         <ext:Column ID="Column1" DataIndex="Supply" runat="server" Text="供应商" Width="80" />
                                    <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                        <Commands>
                                            <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                                <ToolTip Text="修改本条数据" />
                                            </ext:GridCommand>
                                            <ext:CommandSeparator />
                                            <ext:GridCommand Icon="TableDelete" CommandName="Delete" Text="清空">
                                                <ToolTip Text="清空本条数据" />
                                            </ext:GridCommand>
                                        </Commands>
                                        <PrepareToolbar />
                                        <Listeners>
                                            <Command Handler="return commandcolumn_click(command, record);" />
                                        </Listeners>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                            <View>
                                <ext:GridView ID="gvRows" runat="server">
                                    <GetRowClass Fn="SetRowClass" />
                                </ext:GridView>
                            </View>
                            <BottomBar>
                                <ext:PagingToolbar ID="pageToolbar" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="progressBarPager" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>

        <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="true" Title="修改料仓基础信息"
            Width="600" Height="350" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
            BodyPadding="5" Layout="Form">
            <Items>
                <ext:FormPanel ID="pnlModify" runat="server" BodyPadding="5" Title="设备信息">
                    <FieldDefaults>
                        <CustomConfig>
                            <ext:ConfigItem Name="LabelWidth" Value="60" Mode="Raw" />
                            <ext:ConfigItem Name="ReadOnly" Value="true" Mode="Value" />
                        </CustomConfig>
                    </FieldDefaults>
                    <Items>
                        <ext:Container ID="Container10" runat="server" Layout="ColumnLayout" AutoHeight="true">
                            <Items>
                                <ext:Container ID="container11" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".5">
                                    <Items>
                                        <ext:TextField ID="winModifytxtEquipCode" runat="server" LabelAlign="Right" FieldLabel="设备编号" />
                                        <ext:TextField ID="winModifytxtEquipName" runat="server" LabelAlign="Right" FieldLabel="设备名称" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container12" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".5">
                                    <Items>
                                        <ext:TextField ID="winModifytxtEquipJarType" runat="server" LabelAlign="Right" FieldLabel="设备类型" />
                                        <ext:TextField ID="winModifytxtJarName" runat="server" LabelAlign="Right" FieldLabel="料仓分类" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                        <ext:Hidden ID="winModifytxtJarObjID" runat="server"></ext:Hidden>
                    </Items>
                </ext:FormPanel>
                <ext:FormPanel ID="FormPanel2" runat="server" BodyPadding="5" Title="料仓信息">
                    <FieldDefaults>
                        <CustomConfig>
                            <ext:ConfigItem Name="LabelWidth" Value="60" Mode="Raw" />
                        </CustomConfig>
                    </FieldDefaults>
                    <Items>
                        <ext:Container ID="Container13" runat="server" Layout="ColumnLayout" AutoHeight="true">
                            <Items>
                                <ext:Container ID="container14" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".5">
                                    <Items>
                                        <ext:TriggerField ID="winModifytxtStorageName" runat="server" FieldLabel="仓库名称" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="GetStorageInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:TriggerField ID="winModifytxtStoragePlaceName" runat="server" FieldLabel="库位名称" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="GetStoragePlaceInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:TextField ID="winModifytxtWork" runat="server" LabelAlign="Right" FieldLabel="工位" ReadOnly="false" />
                                            <ext:NumberField ID="TextWeight" runat="server" LabelAlign="Right" FieldLabel="最大单包重量kg" ReadOnly="false" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container15" runat="server" Layout="FormLayout" Padding="5" ColumnWidth=".5">
                                    <Items>
                                        <ext:TriggerField ID="winModifytxtMaterialName" runat="server" FieldLabel="物料名称" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="GetMaterialInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:Checkbox ID="winMadifycbDeleteFlag" runat="server" LabelAlign="Right" FieldLabel="是否可用" Checked="true" />
                                     <ext:TextField ID="TextCD" runat="server" LabelAlign="Right" FieldLabel="产地（6位数字加|间隔）" ReadOnly="false" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                        <ext:Hidden ID="winModifytxtStorageID" runat="server"></ext:Hidden>
                        <ext:Hidden ID="winModifytxtStoragePlaceID" runat="server"></ext:Hidden>
                        <ext:Hidden ID="winModifytxtMaterialID" runat="server"></ext:Hidden>
                    </Items>
                </ext:FormPanel>
            </Items>
            <Buttons>
                <ext:Button ID="btnModifySave" runat="server" Text="确定" Icon="Accept">
                    <DirectEvents>
                        <Click OnEvent="btnModifySave_Click">
                            <EventMask ShowMask="true" Msg="修改..." MinDelay="50" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                    <DirectEvents>
                        <Click OnEvent="btnCancel_Click">
                        </Click>
                    </DirectEvents>
                </ext:Button>
            </Buttons>
            <Listeners>
                <Show Fn="windowOnShow"></Show>
                <Hide Fn="windowOnHide"></Hide>
            </Listeners>
        </ext:Window>
    </form>
</body>
</html>
