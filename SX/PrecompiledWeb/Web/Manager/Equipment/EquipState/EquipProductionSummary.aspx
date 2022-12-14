<%@ page language="C#" autoeventwireup="true" inherits="Manager_Equipment_EquipState_EquipProductionSummary, App_Web_5kdmhlds" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>机台产量统计</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="css/data-view.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript">
        //树形结构点击刷新右侧方法
        var loadPage = function (record) {
            App.txtHiddenWorkShopCode.setValue(record.getId());
            App.store.currentPage = 1;
            App.direct.LoadList();
            return false;
        };
        //列表刷新数据重载方法
        var pnlCenterRefresh = function () {
            App.store.currentPage = 1;
            App.direct.LoadList();
            return false;
        }
        var setParentNode = function (node, checked) {
            var pNode = node;
            while (checked) {
                pNode = pNode.parentNode;
                if (!pNode) {
                    break;
                }
                pNode.set("checked", checked);
            }
        }
        var setChildNode = function (node, checked) {
            var nodes = node.childNodes;
            for (var i = 0; i < nodes.length; i++) {
                var node = nodes[i];
                node.set("checked", checked);
                if (node.data.leaf) {
                    continue;
                }
                setChildNode(node, node.data.checked);
            }
        }
        var onTreeCheckChange = function (node, checked, fn) {
            setParentNode(node, checked);
            setChildNode(node, checked);
            var actions = getTreeSelectionModel(App.equipTree.getRootNode().childNodes);
            App.treeStr.setValue(actions);
        }
        var getTreeSelectionData = function (node) {
            if (node.data.NodeId) {
                return node.data.NodeId;
            }
            return "";
        }
        var getTreeSelectionModel = function (nodes) {
            var Result = ",";
            for (var i = 0; i < nodes.length; i++) {
                var node = nodes[i];
                if (node.data.checked) {
                    Result = Result + getTreeSelectionData(node) + ",";
                }
                try {
                    if (node.childNodes) {
                        Result = Result + getTreeSelectionModel(node.childNodes) + ",";
                    }
                } catch (e) { }
            }
            return Result;
        }
    </script>
</head>
<body>
     <form id="form1" runat="server">
       <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
       <ext:ResourceManager ID="rmProductionSummary" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlWest" runat="server" Region="West" Width="200" Layout="BorderLayout">
                    <Items>
                        <ext:TreePanel ID="equipTree" runat="server" Region="West" Width="305" RootVisible="false"
                            MultiSelect="false" TitleAlign="Center" Collapsible="true">
                            <Store>
                                <ext:TreeStore ID="equipTreeStore" runat="server">
                                    <Model>
                                        <ext:Model ID="model2" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="NodeId" />
                                                <ext:ModelField Name="ShowName" />
                                                <ext:ModelField Name="Remark" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Root>
                                        <ext:Node NodeID="Root" />
                                    </Root>
                                </ext:TreeStore>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                    <ext:TreeColumn ID="ShowName" DataIndex="ShowName" runat="server" Sortable="false"
                                        Hideable="false" Text="设备信息树" Width="300" />
                                    <ext:Column ID="NodeId" DataIndex="NodeId" runat="server" Sortable="false" Hideable="false"
                                        Text="设备编号" Width="100" Hidden="true" />
                                </Columns>
                            </ColumnModel>
                            <Listeners>
                                <CheckChange Fn="onTreeCheckChange" />
                            </Listeners>
                        </ext:TreePanel>
                    </Items>
                </ext:Panel>

              <ext:Panel ID="pnlNorth" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUnit1">
                            <Items>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="pnlCenterRefresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsMiddle2" />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end1" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end1" />
                                <ext:ToolbarFill ID="toolbarFill_end1" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                      <ext:FormPanel runat="server" ID="pnlForm" Layout="AnchorLayout" AutoHeight="true">
                         <Items>
                            <ext:Container ID="container1" runat="server" Layout="HBoxLayout"  Padding="5">
                                <Items>
                                    <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="起始时间" LabelAlign="Right" Format="yyyy-MM-dd" LabelWidth="100" Editable="false" Flex="1">
                                    </ext:DateField>
                                    <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" LabelAlign="Right" Format="yyyy-MM-dd" LabelWidth="100" Editable="false" Flex="1">
                                    </ext:DateField>
                                    <ext:ComboBox ID="cbxShift" runat="server" FieldLabel ="班次" LabelAlign="Right" LabelWidth="100" Editable="false" Flex="1">
                                        <SelectedItems>
                                            <ext:ListItem Text="全部" Value="0" />
                                        </SelectedItems>
                                        <Items>
                                            <ext:ListItem Text="全部" Value="0" />
                                            <ext:ListItem Text="早" Value="3" />
                                            <ext:ListItem Text="中" Value="1" />
                                            <ext:ListItem Text="夜" Value="2" />
                                        </Items>
                                        <Listeners>
                                            <Select Fn="pnlCenterRefresh"></Select>
                                        </Listeners>
                                    </ext:ComboBox>
                                   <%-- <ext:ComboBox ID="cbxEquip" runat="server" FieldLabel="设备" LabelAlign="Right" LabelWidth="100" Editable="false" Flex="1">
                                        <Items>
                                        </Items>
                                        <Listeners>
                                            <Select Fn="pnlCenterRefresh"></Select>
                                        </Listeners>
                                    </ext:ComboBox>--%>
                                </Items>
                            </ext:Container>
                         </Items>
                      </ext:FormPanel>
                    </Items>
              </ext:Panel>

                <ext:Panel ID="pnlCenter" runat="server" Region="Center" Frame="true" Layout="Fit" MarginsSummary="0 5 0 5">
                    <Items>
                        <ext:GridPanel ID="pnlSummary" runat="server" MarginsSummary="0 5 5 5">
                            <Store>
                                <ext:Store ID="store" runat="server">
                                    <Model>
                                        <ext:Model ID="model" runat="server" IDProperty="SpecId">
                                            <Fields>
                                                <ext:ModelField Name="PlanDate" />
                                                <ext:ModelField Name="EquipName" />
                                                <ext:ModelField Name="ShiftID" />
                                                <ext:ModelField Name="Num" />
                                                <ext:ModelField Name="RealWeight" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="colModel1" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />
                                    <ext:Column ID="planDate" runat="server" Text="计划日期" DataIndex="PlanDate" Flex="1" />
                                    <ext:Column ID="equipName" runat="server" Text="设备名称" DataIndex="EquipName" Flex="1" />
                                    <ext:Column ID="shiftId" runat="server" Text="班次" DataIndex="ShiftID" Flex="1" />
                                    <ext:Column ID="num" runat="server" Text="产量" DataIndex="Num" Flex="1" />
                                    <ext:Column ID="realWeight" runat="server" Text="实际重量" DataIndex="RealWeight" Flex="1" />
                                </Columns>
                            </ColumnModel>
                           <%-- <BottomBar>
                                <ext:PagingToolbar ID="pageToolBar" runat="server">
                                    <Items>
                                        <ext:Label ID="Label1" runat="server" Text="每页条数:" />
                                        <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                                        <ext:ComboBox ID="ComboBox1" runat="server" Width="80" Editable="false">
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
                                                <Select Handler="#{pnlSummary}.store.pageSize = parseInt(this.getValue(), 10); #{pageToolBar}.doRefresh(); return false;" />
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                    <Plugins>
                                        <ext:ProgressBarPager ID="ProgressBarPager1" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>--%>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
                <ext:Hidden ID="txtHiddenWorkShopCode" runat="server" Text="0"></ext:Hidden>
            </Items>
        </ext:Viewport>
         <ext:Hidden ID="treeStr" runat="server" />
    </form>
</body>
</html>
