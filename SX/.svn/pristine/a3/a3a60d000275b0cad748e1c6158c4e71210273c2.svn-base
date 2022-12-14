<%@ page language="C#" autoeventwireup="true" inherits="Manager_ShopStorage_MaterialStaticGroup, App_Web_ampjtxsw" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>物料分组统计信息</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        //树形结构点击刷新右侧方法
        var loadPage = function (record) {
            if (record.getDepth() == 2) {
                App.hiddenMajorTypeID.setValue(record.getId().split('|')[0]);
                App.hiddenMinorTypeID.setValue(record.getId().split('|')[1]);
            }
            if (record.getDepth() == 1) {
                App.hiddenMajorTypeID.setValue(record.getId());
                App.hiddenMinorTypeID.setValue("");
            }
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
        };

        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.hiddenMajorTypeID.setValue("");
            App.hiddenMinorTypeID.setValue("");
            App.hidden_delete_flag.setValue("0");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        Ext.apply(Ext.form.VTypes, {
            integer: function (val, field) {
                if (!val) {
                    return true;
                }
                try {
                    if (/^[\d]+\.?[\d]*$/.test(val)) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                catch (e) {
                    return false;
                }
            },
            integerText: "此填入项为数字格式！"

        });

        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }
    </script>
    <script type="text/javascript">
        //历史查询根据DeleteFlag的值进行样式绑定
        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("DeleteFlag") == "1") {
                return "x-grid-row-deleted";
            }
        }
        //历史查询的每行按钮准备加载
        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
            if (record.get("DeleteFlag") == "1") {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
                toolbar.items.getAt(3).hide();
            } else {
                toolbar.items.getAt(4).hide();
            }
        };

        var cellDblClick = function (grid, td, tdindex, record, tr, trindex, e, fn) {
            App.direct.MaterialGroupData_DbClick(record.data.MaterialName, record.data.MaterialCode, {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }
    </script>
</head>
<body>
    <form id="fmRubber" runat="server">
    <ext:ResourceManager ID="rmRubber" runat="server" />
    <ext:Viewport ID="vwMaterialStaticGroup" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="Panel1" runat="server" Region="West" Width="200" Layout="BorderLayout">
                <Items>
                    <ext:TreePanel ID="treeDept" runat="server" Title="物料分类" Region="Center" Icon="FolderGo"
                        AutoHeight="true" RootVisible="false">
                        <Store>
                            <ext:TreeStore ID="treeDeptStore" runat="server">
                                <Proxy>
                                    <ext:PageProxy>
                                        <RequestConfig Method="GET" Type="Load" />
                                    </ext:PageProxy>
                                </Proxy>
                                <Root>
                                    <ext:Node NodeID="root" Expanded="true" />
                                </Root>
                            </ext:TreeStore>
                        </Store>
                        <Listeners>
                            <ItemClick Handler="loadPage(record)" />
                        </Listeners>
                    </ext:TreePanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="pnlRubberTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="barRubber">
                        <Items>
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                <ToolTips>
                                    <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                </ToolTips>
                                <Listeners>
                                    <Click Fn="pnlListFresh">
                                    </Click>
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                            <ext:Button runat="server" Icon="Group" Text="设置物料分组" ID="btn_material_group">
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip3" runat="server" Html="点击设置物料分组" />
                                </ToolTips>
                               <DirectEvents>
                                    <Click OnEvent="btnGroupClick">
                                        <ExtraParams>
                                            <ext:Parameter Name="Values" Value="Ext.encode(#{pnlList}.getRowsValues({selectedOnly:true}))" Mode="Raw" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                            <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                            <ext:ToolbarFill ID="toolbarFill_end" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel ID="pnlRubberQuery" runat="server" AutoHeight="true">
                        <Items>
                            <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:TextField ID="txtMaterialCode" Vtype="integer" runat="server" FieldLabel="物料代码"
                                                LabelAlign="Right" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:TextField ID="txtMaterialName" runat="server" FieldLabel="物料名称" LabelAlign="Right" />
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                <Store>
                    <ext:Store ID="store" runat="server" PageSize="15">
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="model" runat="server">
                                <Fields>
                                    <ext:ModelField Name="ObjID" />
                                    <ext:ModelField Name="MajorTypeID" />
                                    <ext:ModelField Name="MajorTypeName" />
                                    <ext:ModelField Name="MinorTypeID" />
                                    <ext:ModelField Name="MinorTypeName" />
                                    <ext:ModelField Name="MaterialCode" />
                                    <ext:ModelField Name="MaterialName" />
                                    <ext:ModelField Name="MaterialGroup" />
                                    <ext:ModelField Name="MaterialGroupName" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="colModel" runat="server">
                    <Columns>
                        <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="45" />
                        <ext:Column ID="ObjID" runat="server" Text="物料编码" DataIndex="ObjID" Width="40" Visible="false" />
                        <ext:Column ID="MaterialCode1" runat="server" Text="物料代码" DataIndex="MaterialCode" Width="150" />
                        <ext:Column ID="MaterialName1" runat="server" Text="物料名称" DataIndex="MaterialName" Width="200" />
                        <ext:Column ID="MajorTypeName" runat="server" Text="物料大类" DataIndex="MajorTypeName" Width="120" />
                        <ext:Column ID="MinorTypeName" runat="server" Text="物料细类" DataIndex="MinorTypeName" Width="120" />
                        <ext:Column ID="MaterialGroupName" runat="server" Text="物料分组" DataIndex="MaterialGroupName" Width="200" />
                    </Columns>
                </ColumnModel>
                <SelectionModel>
                    <ext:CheckboxSelectionModel ID="RowSelectionModel1" runat="server" Mode="Simple" />
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
             <ext:Window ID="winMaterialGroup" runat="server" Icon="Group" Closable="false" Title="设定物料分组"
                Height="340" Width="500" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5"  Layout="BorderLayout">
                <Items>
                    <ext:FormPanel ID="pnlGroup" runat="server" BodyPadding="5" Region="North">
                        <Items>
                            <ext:ComboBox ID="cboGroup"  runat="server" FieldLabel="物料分组" 
                                LabelAlign="Left" DisplayField="MaterialName" TypeAhead="false"
                                ValueField="MaterialCode" IndicatorText="*" IndicatorCls="red-text"
                                Width="400" MinChars="1">
                                <Store>
                                    <ext:Store ID="materialgroupstore" runat="server" OnReadData="MaterialGroupStoreRefresh">
                                        <Model>
                                            <ext:Model ID="Model2" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="MaterialName" Type="String" Mapping="MaterialName" />
                                                    <ext:ModelField Name="MaterialCode" Type="String" Mapping="MaterialCode" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                        <Proxy>
                                            <ext:PageProxy>
                                                <Reader>
                                                    <ext:ArrayReader />
                                                </Reader>
                                            </ext:PageProxy>
                                        </Proxy>
                                    </ext:Store>
                                </Store>
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                </Triggers>
                                <Listeners>
                                    <Select Handler="this.getTrigger(0).show();" />
                                    <Change Handler="App.hidden_material_group_code.setValue(App.cboGroup.getValue());" />
                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                    <TriggerClick Handler="if (index == 0) { this.clearValue(); App.cboGroup.setValue('');App.hidden_material_group_code.setValue('');this.getTrigger(0).hide();}" />
                                </Listeners>
                            </ext:ComboBox>
                        </Items>
                    </ext:FormPanel>
                    <ext:GridPanel ID="groupPanel" Collapsible="true" runat="server" Region="Center" Title="所选物料">
                        <Store>
                            <ext:Store ID="groupStore" runat="server" PageSize="5">
                                <Model>
                                    <ext:Model ID="model3" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="MaterialCode" />
                                            <ext:ModelField Name="MaterialGroup" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Width="35" />
                                <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Width="150"  />
                                <ext:Column ID="MaterialCode" runat="server" Text="物料代码" DataIndex="MaterialCode" Width="150"  />
                                <ext:Column ID="MaterialGroup" runat="server" Text="物料分组" DataIndex="MaterialGroup" Width="150"  />
                            </Columns>
                        </ColumnModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar1" runat="server">
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager1" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                        <Listeners>
                            <CellClick Fn="cellDblClick" />
                        </Listeners>
                    </ext:GridPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnGroupSave" runat="server" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="BtnGroupSave_Click">
                                <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnGroupCancel" runat="server" Text="取消" Icon="Cancel">
                        <DirectEvents>
                            <Click OnEvent="BtnCancel_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnGroupClear" runat="server" Text="清除物料分组" Icon="Delete">
                        <DirectEvents>
                            <Click OnEvent="BtnGroupClear_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="for(var i=0;i<#{vwMaterialStaticGroup}.items.length;i++){#{vwMaterialStaticGroup}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vwMaterialStaticGroup}.items.length;i++){#{vwMaterialStaticGroup}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>
            <ext:Hidden Hidden="true" ID="hiddenMajorTypeID" runat="server" />
            <ext:Hidden Hidden="true" ID="hiddenMinorTypeID" runat="server" />
            <ext:Hidden ID="hidden_material_name" runat="server" />
            <ext:Hidden ID="hidden_delete_flag" runat="server" Text="0" />
            <ext:Hidden ID="hidden_material_group" runat="server" />
            <ext:Hidden ID="hidden_material_group_code" runat="server" />
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
