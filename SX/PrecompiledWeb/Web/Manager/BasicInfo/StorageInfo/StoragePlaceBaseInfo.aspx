<%@ page language="C#" autoeventwireup="true" inherits="Manager_BasicInfo_StoragePlaceBaseInfo_StoragePlaceBaseInfo, App_Web_jkrsgo21" %>
<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell
        {
        	background-color: #FF8C69 !important;
        }
    </style>
    <script type="text/javascript">
        var defaultStoragePlace = function (value) {
            debugger;
            return Ext.String.format(value == 1 ? "是" : " ");
        }
        //树形结构点击刷新右侧方法
        var treePanelStorage = function (store, operation, options) {
            var node = operation.node;
            var nodeid = node.getId() || "";
            App.direct.treePanelStorageLoad(nodeid, {
                success: function (result) {
                    node.set('loading', false);
                    node.set('loaded', true);
                    var data = Ext.decode(result);
                    if (data != "") {
                        node.appendChild(data, undefined, true);
                        node.expand();
                    }
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('错误', errorMsg);
                }
            });
            return false;
        };

        var treeStorageClick = function (record) {
            
            App.direct.SetSelectStorageID(record.getId(), {
                success: function (result) {
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
            //App.hiddenSelectStorageID.setValue(record.getId());
            //App.hiddenNodeID.setValue(record.getId());
            //App.store.currentPage = 1;
            //App.pageToolBar.doRefresh();
        }
        
        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("CancelFlag") == "1") {
                return "x-grid-row-collapsed";
            }
        }
        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var ObjID = record.data.ObjID;
            App.direct.commandcolumn_direct_edit(ObjID, {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //点击删除按钮
        var commandcolumn_direct_delete = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ObjID = record.data.ObjID;
            App.direct.commandcolumn_direct_delete(ObjID, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //点击作废按钮
        var commandcolumn_direct_cancel = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ObjID = record.data.ObjID;
            App.direct.commandcolumn_direct_cancel(ObjID, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //点击取消作废按钮
        var commandcolumn_direct_returncancel = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ObjID = record.data.ObjID;
            App.direct.commandcolumn_direct_returncancel(ObjID, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //区分删除操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
            }
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此条信息？', function (btn) { commandcolumn_direct_delete(btn, record) });
            }
            if (command.toLowerCase() == "cancel") {
                Ext.Msg.confirm("提示", '您确定要作废此条信息吗？', function (btn) { commandcolumn_direct_cancel(btn, record) });
            }
            if (command.toLowerCase() == "returncancel") {
                Ext.Msg.confirm("提示", '您确定要取消作废吗？', function (btn) { commandcolumn_direct_returncancel(btn, record) });
            }
            return false;
        };

        //根据按钮类别进行删除和编辑操作
        var commandcolumn_click = function (command, record) {
            commandcolumn_click_confirm(command, record);
            return false;
        };
        var commandcolumn_click1 = function (command, record) {
          
        };
        Ext.apply(Ext.form.VTypes, {
            integer: function (val, field) {
                if (!val) {
                    return true;
                }
                try {
                    if (/^[\d]+$/.test(val)) {
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
            integerText: "此填入项格式为正整数"
        });


        var pnlListFresh = function () {
            if (App.txtStorageName.getValue() == "")
                App.hiddenSelectStorageID.setValue("");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            if (!App.winAdd.hidden) {
                App.txtStorageName2.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
            }
            else if (!App.winModify.hidden) {
                App.txtStorageName1.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
            }
            else {
                App.txtStorageName.getTrigger(0).show();
                App.txtStorageName.setValue(record.data.StorageName);
                App.hiddenSelectStorageID.setValue(record.data.StorageID);
            }
        }

        var SelectStorage = function (field, trigger, index) {//库房查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenSelectStorageID.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    var url = "../../BasicInfo/CommonPage/QueryBasStorage.aspx?UsedFlag=0&&StorageType=" + App.cbxStorageType.getValue();
                    var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no frameborder=0></iframe>";
                    if (App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.getBody()) {
                        App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.getBody().update(html);
                    } else {
                        App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.html = html;
                    }
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
                    break;
            }
        }

        var commandcolumn_direct_lockedflag = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnBatchUsing_Click({
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var SetLockedFlag = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();
            var grid = App.pnlList;
            grid.store.model.getFields()
            //record = grid.store.getById(id);
            /*
            if (section.record.get("CancelFlag") == "1")
            {
                debugger;
                alert('已经作废的不能启用！！！');
            }*/
            if (section && section.length == 0) {

                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要启用该库位吗？', function (btn) { commandcolumn_direct_lockedflag(btn) });
            }
        }

        var AddStorage = function () {//库房添加
            var url = "../../BasicInfo/CommonPage/QueryBasStorage.aspx?UsedFlag=0&&StorageHigherID=" + App.hiddenNodeID.getValue();
            var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no frameborder=0></iframe>";
            if (App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.getBody()) {
                App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.getBody().update(html);
            } else {
                App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.html = html;
            }
            App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
        }
//        var AddStorage = function () {//库房添加
//            App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
//        }
        var EditStorage = function () {//库房修改
            App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
        }

        Ext.create("Ext.window.Window", {//库房查询带窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasStorage_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryBasStorage.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择库房",
            modal: true
        })
        //END


        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
            if (record.get("LockFlag") == "1" && record.get("CancelFlag") == "0") {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
                toolbar.items.getAt(4).hide();
            } else if (record.get("LockFlag") == "1" && record.get("CancelFlag") == "1") {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
                toolbar.items.getAt(3).hide();
            }
            else if (record.get("LockFlag") == "0" && record.get("CancelFlag") == "1")
            {
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
                toolbar.items.getAt(3).hide();
                
            }
            else {
                toolbar.items.getAt(3).hide();
                toolbar.items.getAt(4).hide();
            }
            if (record.get("AutoGenFlag") == "1") {
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
            }
        };

        var defaultChange = function (value) {
            return Ext.String.format(value ? "默认" : "");
        };

        var lockChange = function (value) {
            return Ext.String.format(value ? "已启用" : "");
        };

        var cancelChange = function (value) {
            return Ext.String.format(value=="1" ? "已作废" : "");
        };
    </script>

    <script type="text/javascript">
        var EditMaterial = function () {//物料选择
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
        }
        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {
            var queryWindow = App.Manager_BasicInfo_CommonPage_QueryMaterial_Window;
            var thisIsModifyWindow = function (record) {
                if ((!App.winAdd.hidden) || (App.winModify.hidden)) {
                    return;
                }
                App.txtModifyMaterial.getTrigger(0).show();
                App.hiddenModifyMaterial.setValue(record.data.MaterialCode);
                App.txtModifyMaterial.setValue(record.data.MaterialName);
            }
            var thisIsAddWindow = function (record) {
                if ((!App.winAdd.hidden) || (App.winModify.hidden)) {
                    return;
                }
                App.txtModifyMaterial.getTrigger(0).show();
                App.hiddenModifyMaterial.setValue(record.data.MaterialCode);
                App.txtModifyMaterial.setValue(record.data.MaterialName);
            }
            thisIsModifyWindow(record);
            thisIsAddWindow(record);
            queryWindow.close();
            App.direct.AddMaterial(record.data.MaterialCode, record.data.MaterialName,{
                success: function (result) {
                },

                failure: function (errorMsg) {
                 
                }
            });
        }
        Ext.create("Ext.window.Window", {//物料带窗体
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料",
            modal: true
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="rmStoragePlace" runat="server" />
        <ext:Viewport ID="vpStoragePlace" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Region="West" Width="235" Layout="BorderLayout">
                    <Items>
                         <ext:TreePanel ID="treePanel" runat="server" Title="库房列表" Region="Center"  Icon="FolderGo" AutoHeight="true" RootVisible="false">
                            <Store>
                                <ext:TreeStore ID="treeStorage" runat="server">
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
                                <BeforeLoad Fn="treePanelStorage" />
                                <ItemClick Handler="treeStorageClick(record)" />
                            </Listeners>
                        </ext:TreePanel>       
                    </Items>
                </ext:Panel>
                <ext:Panel ID="pnStoragePlaceTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="tbStoragePlace">
                            <Items>
                                <ext:Button runat="server" Icon="Add" Text="添加" ID="btnAdd">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttAdd" runat="server" Html="点击进行添加" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btnAdd_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsBegin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="pnlListFresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsMiddle" />
                                 <ext:Button runat="server" Icon="LockEdit" Text="库位启用" ID="Button1">
                                    <%--<DirectEvents>
                                        <Click OnEvent="btnBatchUsing_Click" />
                                    </DirectEvents>--%>
                                    <Listeners>
                                        <Click Handler="SetLockedFlag();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsEnd" />
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
                                        <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".2" Padding="5">
                                            <Items>
                                                <ext:ComboBox ID="cbxStorageType" runat="server" FieldLabel="库房类别" LabelAlign="Right">
                                                    <SelectedItems>
                                                        <ext:ListItem Value="all"></ext:ListItem>
                                                    </SelectedItems>
                                                    <Items>
                                                        <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                                        <ext:ListItem Text="原材料库" Value="0"></ext:ListItem>
                                                        <ext:ListItem Text="密炼原材料库" Value="1"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container> 
                                        <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".2" Padding="5">
                                            <Items>
                                                <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="库房名称" LabelAlign="Right" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectStorage" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".2" Padding="5">
                                            <Items>
                                                <ext:TextField ID="txtStoragePlaceName" runat="server" FieldLabel="库位名称" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                           <ext:Container ID="container4" runat="server" Layout="FormLayout" ColumnWidth=".2" Padding="5">
                                            <Items>
                                                <ext:TextField ID="TextField1" runat="server" FieldLabel="包含物料" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container5" runat="server" Layout="FormLayout" ColumnWidth=".2" Padding="5">
                                            <Items>
                                                <ext:ComboBox ID="cbxDefaultFlag" runat="server" FieldLabel="是否默认库位" LabelAlign="Right">
                                                    <SelectedItems>
                                                        <ext:ListItem Value="all"></ext:ListItem>
                                                    </SelectedItems>
                                                    <Items>
                                                        <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                                        <ext:ListItem Text="是" Value="1"></ext:ListItem>
                                                        <ext:ListItem Text="否" Value="0"></ext:ListItem>
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
                <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="15"> 
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server" IDProperty="ObjID">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="StorageID" />
                                        <ext:ModelField Name="StorageName" />
                                        <ext:ModelField Name="StoragePlaceID" />
                                        <ext:ModelField Name="StoragePlaceName" />
                                        <ext:ModelField Name="DefaultFlag" />
                                        <ext:ModelField Name="CancelFlag" />
                                        <ext:ModelField Name="LockFlag" />
                                        <ext:ModelField Name="AutoGenFlag" />
                                        <ext:ModelField Name="StorageCapacity" />
                                        <ext:ModelField Name="SpecialPlace" />
                                        <ext:ModelField Name="MaterialName" />
                                        <ext:ModelField Name="Remark" />
                                         <ext:ModelField Name="ShiYanFlag" />
                                        <ext:ModelField Name="isfull" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                            <ext:Column ID="objID" runat="server" Text="ID" Hidden="true" DataIndex="ObjID" Flex="1" />
                            <ext:Column ID="storageID" runat="server" Text="库房编号" Hidden="true" DataIndex="StorageID" Flex="1" />
                            <ext:Column ID="storageName" runat="server" Text="库房名称" DataIndex="StorageName" Flex="1" />
                            <ext:Column ID="storagePlaceID" runat="server" Text="库位编号" Hidden="true" DataIndex="StoragePlaceID" Flex="1" />
                            <ext:Column ID="storagePlaceName" runat="server" Text="库位名称" DataIndex="StoragePlaceName" Flex="1" />
                            <ext:Column ID="defaultFlag" runat="server" Text="是否默认库位" DataIndex="DefaultFlag" Flex="1" Hidden="true">
                                <Renderer Fn="defaultChange" />
                            </ext:Column>
                           
                            <ext:Column ID="materialName" runat="server" Text="物料名称" DataIndex="MaterialName" Width="135" />
                            <ext:CheckColumn ID="lockFlag" runat="server" Text="是否启用" DataIndex="LockFlag" Flex="1">
                                <Renderer Fn="lockChange" />
                            </ext:CheckColumn>
                            <ext:CheckColumn ID="CancelFlag" runat="server" Text="是否作废" DataIndex="CancelFlag" Flex="1">
                                <Renderer Fn="cancelChange" />
                            </ext:CheckColumn>
                            <ext:Column ID="storagePlaceSubAmount" runat="server" Text="子库位数量" DataIndex="StorageCapacity"></ext:Column>
                             <ext:Column ID="Column1" runat="server" Text="是否已满" DataIndex="isfull"></ext:Column>
                            <ext:Column ID="specialStorage" runat="server" Text="特殊库位" DataIndex="SpecialPlace">
                          <%--      <Renderer Fn="defaultStoragePlace"></Renderer>--%>
                            </ext:Column>
                              <ext:Column ID="Column2" runat="server" Text="实验库位" DataIndex="ShiYanFlag"  >
                              
                            </ext:Column>
                            <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Flex="1" />
                            <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                        <ToolTip Text="修改本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                        <ToolTip Text="删除本条数据" />
                                    </ext:GridCommand>
                                    <ext:GridCommand Icon="Delete" CommandName="Cancel" Text="作废">
                                        <ToolTip Text="作废本条数据" />
                                    </ext:GridCommand>
                                    <ext:GridCommand Icon="TableEdit" CommandName="ReturnCancel" Text="取消作废">
                                    </ext:GridCommand>
                                </Commands>
                                <PrepareToolbar Fn="prepareToolbar" />
                                <Listeners>
                                    <Command Handler="return commandcolumn_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single" />
                    </SelectionModel>
                    <View>
                        <ext:GridView ID="gvRows" runat="server" EnableTextSelection="true">
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
                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改库位信息"
                    Width="320" Height="520" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items> 
                        <ext:FormPanel ID="pnlEdit" runat="server" Flex="1" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="txtObjID1" runat="server" FieldLabel="库房编号" LabelAlign="Left" ReadOnly="true" Hidden="true" Enabled="true" />
                                <ext:TriggerField ID="txtStorageName1" runat="server" FieldLabel="库房名称" IndicatorText="*" IndicatorCls="red-text" LabelAlign="Left" Editable="false">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Fn="EditStorage" />
                                    </Listeners>
                                </ext:TriggerField>
                                <ext:TextField ID="txtStoragePlaceName1" runat="server" FieldLabel="库位名称" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" LabelAlign="Left" IsRemoteValidation="true" >
                                     <RemoteValidation OnValidation="CheckField" />
                                </ext:TextField>
                                <ext:TextField ID="txtStoragePlacaSubAmount" runat="server" FieldLabel="子库位数量" LabelAlign="Left">
                                   <%-- <RemoteValidation OnValidation="CheckFieldAmount" />--%>
                                </ext:TextField>
                                <ext:TriggerField ID="txtModifyMaterial" runat="server" FieldLabel="增加物料" LabelAlign="Left" Editable="false">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Fn="EditMaterial" />
                                    </Listeners>
                                </ext:TriggerField>
                                <ext:Checkbox ID="chkModifySpecialPlaceFlag" runat="server" FieldLabel="是否特殊库位" LabelAlign="Left"/>
                                     <ext:Checkbox ID="chkSY" runat="server" FieldLabel="是否实验库位" LabelAlign="Left"/>
                                <ext:TextField ID="txtRemark1" runat="server" FieldLabel="备注" LabelAlign="Left" />
                                  <ext:TextArea ID="TextArea1" runat="server" FieldLabel="已有物料" LabelAlign="Left" ReadOnly="true" Height="200"/>
                             
                                  <ext:TextField ID="TextField2" runat="server" FieldLabel="已有物料" LabelAlign="Left" Hidden="true" />
                            
                 
                            
                          
                            </Items>
                             <Listeners>
                                <ValidityChange Handler="#{btnModify}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                         <ext:Button ID="Button2" runat="server" Text="清空" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="ClearMaterial">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnModify" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="btnModify_Click">
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
                        <Show Handler="for(var i=0;i<#{vpStoragePlace}.items.length;i++){#{vpStoragePlace}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vpStoragePlace}.items.length;i++){#{vpStoragePlace}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加库位信息" Width="320" Height="380" 
                    Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TriggerField ID="txtStorageName2" runat="server" AllowBlank="false" FieldLabel="库房名称" IndicatorText="*" IndicatorCls="red-text" LabelAlign="Left" Editable="false">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Fn="AddStorage" />
                                    </Listeners>
                                </ext:TriggerField>
                                <ext:TextField ID="txtStoragePlaceName2" runat="server" FieldLabel="库位名称" LabelAlign="Left" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" IsRemoteValidation="true">
                                    <RemoteValidation OnValidation="CheckField" />
                                </ext:TextField>
                                <ext:TextField ID="txtRemark2" runat="server" FieldLabel="备注" LabelAlign="Left" />
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnAddSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                     <Buttons>
                        <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <DirectEvents>
                                <Click OnEvent="btnAddSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vpStoragePlace}.items.length;i++){#{vpStoragePlace}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vpStoragePlace}.items.length;i++){#{vpStoragePlace}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Hidden ID="hiddenSelectStorageID" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenStorageID" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenStoragePlaceName" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenNodeID" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenModifyMaterial" runat="server"></ext:Hidden>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
