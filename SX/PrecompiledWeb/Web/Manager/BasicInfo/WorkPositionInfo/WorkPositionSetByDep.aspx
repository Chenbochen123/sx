<%@ page language="C#" autoeventwireup="true" inherits="Manager_BasicInfo_WorkPositionInfo_WorkPositionSetByDep, App_Web_wumzmog0" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>设置用户岗位</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />

    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>

    <script type="text/javascript">
        var after = function () {
            App.waitProgressWindow.close();
        }
        var before = function () {
            App.waitProgressWindow.show();
        }
        var treePanelDept = function (store, operation, options) {
            var node = operation.node;
            var nodeid = node.getId() || "";
            App.direct.IniDeptTree(nodeid, {
                success: function (result) {
                    node.set('loading', false);
                    node.set('loaded', true);
                    for (var i = 0; i < result.children.length; i++) {
                        var data = result.children[i];
                        node.appendChild(data, undefined, true);
                    }
                    node.expand();
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
            return false;
        };
        var deptItemClick = function (view, record, node, index, event, fn) {
            App.hiddenDeptCode.setValue(record.data.DepCode);
            App.userPanel.setTitle("用户信息[" + record.data.DepName + "]");
            App.userPanel.store.currentPage = 1;
            App.userPanel.store.reload();
        }
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Hidden ID="hiddenDeptCode" runat="server"></ext:Hidden>
                <ext:Panel ID="Panel4" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="toolbar1">
                            <Items>
                                <ext:ToolbarFill ID="toolbarFill_begin" />
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="FolderWrench" Text="设置人员岗位" ID="btnSetWork">
                                    <DirectEvents>
                                        <Click OnEvent="btnSetWork_Click">
                                            <ExtraParams>
                                                <ext:Parameter Name="ValuesWork" Value="Ext.encode(#{workPanel}.getRowsValues({selectedOnly:true}))" Mode="Raw" />
                                                <ext:Parameter Name="ValuesUser" Value="Ext.encode(#{userPanel}.getRowsValues({selectedOnly:true}))" Mode="Raw" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:GridPanel ID="workPanel" runat="server" Flex="3" Region="West" Title="岗位信息" TitleAlign="Center">
                            <Store>
                                <ext:Store ID="workPanel_store" runat="server">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindDataWorkPosition" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="workModel" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="WorkName" />
                                                <ext:ModelField Name="Remark" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <SelectionModel>
                                <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Single" />
                            </SelectionModel>        
                            <ColumnModel ID="workColumnModel" runat="server">
                                <Columns>
                                    <ext:Column ID="Column2" DataIndex="ObjID" runat="server" Text="岗位编码" Flex="2" />
                                    <ext:Column ID="Column3" DataIndex="WorkName" runat="server" Text="岗位名称" Flex="5" />
                                    <ext:Column ID="Column4" DataIndex="Remark" runat="server" Text="备注" Flex="2" />
                                </Columns>
                            </ColumnModel>
                        </ext:GridPanel>
                        <ext:TreePanel ID="TreePanel1" runat="server" Flex="5" Region="Center" Collapsible="false" RootVisible="false" MultiSelect="false"
                            Title="部门信息" TitleAlign="Center">
                            <Store>
                                <ext:TreeStore ID="TreeStore1" runat="server">
                                    <Model>
                                        <ext:Model ID="model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="DepCode" />
                                                <ext:ModelField Name="DepName" />
                                                <ext:ModelField Name="HRCode" />
                                                <ext:ModelField Name="ParentNum" />
                                                <ext:ModelField Name="DeleteFlag" />
                                                <ext:ModelField Name="Remark" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Listeners>
                                        <BeforeLoad Fn="treePanelDept" />
                                    </Listeners>
                                </ext:TreeStore>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:TreeColumn ID="DepName" DataIndex="DepName" runat="server" Sortable="false" Hideable="false" Text="部门名称"  Flex="8" />
                                    <ext:Column ID="DepCode" DataIndex="DepCode" runat="server" Sortable="false" Hideable="false" Text="部门编号" Align="Center"  Flex="2" />
                                    <ext:Column ID="DeleteFlag" DataIndex="DeleteFlag" runat="server" Sortable="false" Hideable="false" Text="部门状态"  Flex="2" />
                                    <ext:Column ID="Remark" DataIndex="Remark" runat="server" Sortable="false" Hideable="false" Text="部门备注" Flex="2" />
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <ItemClick Fn="deptItemClick"></ItemClick>
                            </Listeners>
                        </ext:TreePanel>
                        <ext:GridPanel ID="userPanel" runat="server" Flex="5" Region="East" Collapsible="false" MultiSelect="false" FolderSort="true"
                            Title="用户信息" TitleAlign="Center">
                            <Store>
                                <ext:Store ID="user_store" runat="server" PageSize="15">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindDataUserByDep" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model3" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ObjID" />
                                                <ext:ModelField Name="HRCode" />
                                                <ext:ModelField Name="UserName" />
                                                <ext:ModelField Name="WorkBarcode" />
                                                <ext:ModelField Name="WorkID" />
                                                <ext:ModelField Name="Remark" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <SelectionModel>
                                <ext:CheckboxSelectionModel ID="CheckboxSelectionModel2" runat="server" Mode="Simple" />
                            </SelectionModel> 
                            <ColumnModel ID="columnModel" runat="server">
                                <Columns>
                                    <ext:Column ID="work_barcode" DataIndex="WorkBarcode" runat="server" Text="用户工号"  Flex="2" />
                                    <ext:Column ID="user_name" DataIndex="UserName" runat="server" Text="用户名称" Flex="4" />
                                    <ext:Column ID="work_id" DataIndex="WorkID" runat="server" Text="所属岗位" Flex="3" />
                                    <ext:Column ID="user_remark" DataIndex="Remark" runat="server" Text="备注" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="pageToolBar" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
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
