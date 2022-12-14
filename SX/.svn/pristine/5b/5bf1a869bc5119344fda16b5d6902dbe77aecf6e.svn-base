<%@ page language="C#" autoeventwireup="true" inherits="Manager_BasicInfo_StorageInfo_StorageBaseInfo, App_Web_jkrsgo21" %>
<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
        <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
  
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell
        {
        	background-color: #FF8C69 !important;
        }
    </style>
    <script type="text/javascript">
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
            App.hiddenHigherLevel.setValue(record.getId());
            App.hiddenNodeID.setValue(record.getId());
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
        }

        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var ObjID = record.data.ObjID;
            App.hiddenNoStorageID.setValue(record.data.StorageID);
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
            var StorageID = record.data.StorageID;
            App.direct.commandcolumn_direct_delete(ObjID, StorageID, {
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
                    //Ext.Msg.prompt('Notice','请输入你的姓名：',function callback(id,msg){alert('单击的按钮ID：'+id+'\n您输入的姓名是：'+msg);},this,false); 
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
                Ext.Msg.confirm("提示", '您确定要删除此条信息吗？', function (btn) { commandcolumn_direct_delete(btn, record) });
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
            App.hiddenHigherLevel.setValue("");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
            if (record.get("StorageID").length < 3) {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
                toolbar.items.getAt(3).hide();
                toolbar.items.getAt(4).hide();
            }
            if (record.get("UsedFlag") == "1" && record.get("CancelFlag") == "0") {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
                toolbar.items.getAt(4).hide();
            } else if (record.get("UsedFlag") == "1" && record.get("CancelFlag") == "1") {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
                toolbar.items.getAt(3).hide();
            }
            else {
                toolbar.items.getAt(3).hide();
                toolbar.items.getAt(4).hide();
            }
        };

        var startTrack = function () {
            this.checkboxes = [];
            var cb;

            Ext.select(".x-form-item", false).each(function (checkEl) {
                cb = Ext.getCmp(checkEl.dom.id.selected);
                cb.setValue(false);
                this.rowselect.push(cb);
            }, this);
        };

        dragTrack = function () {
            var tracker = this,
            grid = App.pnlList,
            view = grid.getView(),
            columns = grid.columns,
            row,
            sel,
            value;

            grid.getStore().each(function (record, i) {
                    row = Ext.fly(view.getNode(i)); 
                    sel = tracker.dragRegion.intersect(row.getRegion());

                    if (sel) {
                        grid.getSelectionModel().select(record, true, true);
                    }
                    else {
                        grid.getSelectionModel().deselect(record, true);
                    }
                });
            };

            //--查询带弹出框--BEGIN
            var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
                if (!App.winAdd.hidden) {
                    App.txtStorageHigherLevel2.setValue(record.data.StorageName);
                    App.hiddenStorageID.setValue(record.data.StorageID);
                    App.hiddenObjID.setValue(record.data.ObjID);
                }
                else if (!App.winModify.hidden) {
                    App.txtStorageHigherLevel1.getTrigger(0).show();
                    App.txtStorageHigherLevel1.setValue(record.data.StorageName);
                    App.hiddenStorageID.setValue(record.data.StorageID);
                    App.hiddenObjID.setValue(record.data.ObjID);
                }
            }

            var AddStorage = function () {//库房添加
                var queryWindow = App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window;
                var html = "<iframe src='../../BasicInfo/CommonPage/QueryBasStorage.aspx?UsedFlag=1&&StorageHigherID=" + App.hiddenNodeID.getValue() + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
                if (queryWindow.getBody()) {
                    queryWindow.getBody().update(html);
                } else {
                    queryWindow.html = html;
                }
                queryWindow.show();
            }

            var EditStorage = function (field, trigger, index) {//库房修改
                var url = "../../BasicInfo/CommonPage/QueryBasStorage.aspx?UsedFlag=0&&NoStorageID=" + App.hiddenNoStorageID.getValue();
                var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
                if (App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.getBody()) {
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.getBody().update(html);
                } else {
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.html = html;
                }
                switch (index) {
                    case 0:
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.hiddenStorageID.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                        break;
                    case 1:
                        App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
                        break;
                }
            }
//            var EditStorage = function () {//库房修改
//                App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
//            }

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

            var commandcolumn_direct_usedflag = function (btn) {
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

            var SetUsedFlag = function () {
                var section = App.pnlList.getView().getSelectionModel().getSelection();
                
                if (section && section.length == 0) {
                    alert('您没有选择任何项，请选择！');
                }
                else {
                    Ext.Msg.confirm("提示", '启用后下级库房同时启用，确定要启用吗？', function (btn) { commandcolumn_direct_usedflag(btn) });
                }
            }

            var SetRowClass = function (record, rowIndex, rowParams, store) {
                if (record.get("CancelFlag") == "1") {
                    return "x-grid-row-collapsed";
                }
            }

            var usedChange = function (value) {
                return Ext.String.format(value ? "已启用" : "否");
            };

            var cancelChange = function (value) {
                return Ext.String.format(value ? "已作废" : "否");
            };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="rmStorage" runat="server" />
        <ext:Viewport ID="vpStorage" runat="server" Layout="border">
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
                <ext:Panel ID="pnStorageTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="tbStorage">
                            <Items>
                               <ext:Button runat="server" Icon="Add" Text="添加" ID="btnAdd">
                                    <DirectEvents>
                                        <Click OnEvent="btnAdd_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsBegin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <Listeners>
                                        <Click Fn="pnlListFresh" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsMiddle" />
                                <ext:Button runat="server" Icon="LockEdit" Text="库房启用" ID="Button1" Hidden="true">
                                    <%--<DirectEvents>
                                        <Click OnEvent="btnBatchUsing_Click" />
                                    </DirectEvents>--%>
                                    <Listeners>
                                        <Click Handler="SetUsedFlag();" />
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
                                        <%--<ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                            <Items>
                                                <ext:TextField ID="txtObjID" Vtype="integer" runat="server" FieldLabel="库房编号" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>--%>
                                        <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                            <Items>
                                                <ext:TextField ID="txtStorageName" runat="server" FieldLabel="库房名称" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                            <Items>
                                                <ext:ComboBox ID="ckxStorageType" runat="server" FieldLabel="库房类型" LabelAlign="Right" Hidden="true">
                                                    <SelectedItems>
                                                        <ext:ListItem Value="all"></ext:ListItem>
                                                    </SelectedItems>
                                                    <Items>
                                                        <ext:ListItem Text="全部" Value="all" AutoDataBind="true"></ext:ListItem>
                                                        <ext:ListItem Text="原材料库" Value="0"></ext:ListItem>
                                                        <ext:ListItem Text="密炼原材料库" Value="1"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>

                                        <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                            <Items>
                                                <ext:ComboBox ID="cbxUsedFlag" runat="server" FieldLabel="是否启用" LabelAlign="Right" Hidden="true">
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
                                <ext:Model ID="model" runat="server" IDProperty="ObjID">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="StorageID" />
                                        <ext:ModelField Name="StorageName" />
                                        <ext:ModelField Name="StorageType" />
                                        <ext:ModelField Name="WorkShopCode" />
                                        <ext:ModelField Name="Remark" />
                                        <ext:ModelField Name="ERPCode" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />

                            <ext:Column ID="ObjID" runat="server" Text="ID" Hidden="true" DataIndex="ObjID" Flex="1" />
                            <ext:Column ID="StorageID" runat="server" Text="库房编号" DataIndex="StorageID"  />
                            <ext:Column ID="StorageName" runat="server" Text="库房名称" DataIndex="StorageName" Flex="1" />
                            <ext:Column ID="WorkShopCode" runat="server" Text="部门编号" DataIndex="WorkShopCode" Flex="1" />
                            <ext:Column ID="StorageType" runat="server" Text="仓库类别" DataIndex="StorageType" Flex="1" />
                            <ext:Column ID="Column1" runat="server" Text="ERP编码" DataIndex="ERPCode" Flex="1" />
                            <ext:Column ID="Remark" runat="server" Text="备注" DataIndex="Remark" Flex="1" />
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
                              <%--  <PrepareToolbar Fn="prepareToolbar" />--%>
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
                        <ext:GridView ID="gvRows" runat="server">
                         <%--   <GetRowClass Fn="SetRowClass" />--%>
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

                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改库房信息"
                    Width="320" Height="380" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                <ext:ComboBox ID="cbxStorageType3" runat="server" FieldLabel="库房类型" LabelAlign="Left">
                                    <SelectedItems>
                                        <ext:ListItem Value="0"></ext:ListItem>
                                    </SelectedItems>
                                    <Items>
                                      <ext:ListItem Text="" Value=""></ext:ListItem>
                                        <ext:ListItem Text="Y" Value="Y"></ext:ListItem>
                                    <%--    <ext:ListItem Text="密炼车间原材料库" Value="1"></ext:ListItem>
                                        <ext:ListItem Text="车间胶料库" Value="2"></ext:ListItem>
                                        <ext:ListItem Text="半制品车间原材料库" Value="3"></ext:ListItem>
                                        <ext:ListItem Text="半制品车间胶料库" Value="4"></ext:ListItem>--%>
                                    </Items>
                                </ext:ComboBox>
                                <ext:TextField ID="txtObjID1" runat="server" FieldLabel="库房编号" LabelAlign="Left" Hidden="true" Enabled="true" />
                                <ext:TextField ID="txtStorageName1" runat="server" FieldLabel="库房名称" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" LabelAlign="Left" IsRemoteValidation="true" >
                                     <RemoteValidation OnValidation="CheckField" />
                                </ext:TextField>
<%--                                <ext:TextField ID="txtStorageHigherLevel1" runat="server" ReadOnly="true" FieldLabel="上级库房" LabelAlign="Left" />
--%>                         <%--   <ext:TextField ID="txtStorageHigherLevel1" runat="server" FieldLabel="上级库房" ReadOnly="true" LabelAlign="Left" Editable="false">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" Hidden="false" />
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <RemoteValidation OnValidation="CheckField" />
                                    <Listeners>
                                        <TriggerClick Fn="EditStorage" />
                                    </Listeners>
                                </ext:TextField>--%>
<%--                                <ext:Checkbox ID="chkNatureMonth1" runat="server" FieldLabel="自然月" Checked="true" LabelAlign="Left" >
                                    <DirectEvents>
                                        <Change OnEvent="chkNatureMonth1_CheckedChanged" />
                                    </DirectEvents>
                                </ext:Checkbox>--%>
        <%--                        <ext:ComboBox ID="chkDurationBeginDate1" runat="server" Disabled="true" FieldLabel="期间开始日期" OnDirectChange="chkDurationBeginDate1_Change" EmptyText="选择上月某天" LabelAlign="Left" />
                                <ext:ComboBox ID="chkDurationEndDate1" runat="server" Disabled="true" FieldLabel="期间截止日期" ReadOnly="true" EmptyText="本月某天" LabelAlign="Left" />--%>
                                <ext:TextField ID="txtERPCode1" runat="server" FieldLabel="ERP编号" LabelAlign="Left" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" />
                                <ext:TextField ID="txtDept1" runat="server" FieldLabel="部门编号" LabelAlign="Left" />
                                <ext:TextField ID="txtRemark1" runat="server" FieldLabel="备注" LabelAlign="Left" />
                            </Items>
                             <Listeners>
                                <ValidityChange Handler="#{btnModify}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
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
                        <Show Handler="for(var i=0;i<#{vpStorage}.items.length;i++){#{vpStorage}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vpStorage}.items.length;i++){#{vpStorage}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加库房信息" Width="320" Height="380" 
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
                                <ext:ComboBox ID="cbxStorageType2" runat="server" FieldLabel="库房类型" LabelAlign="Left">
                                    <SelectedItems>
                                        <ext:ListItem Value="0"></ext:ListItem>
                                    </SelectedItems>
                                    <Items>
                                     <ext:ListItem Text="" Value=""></ext:ListItem>
                                        <ext:ListItem Text="Y" Value="Y"></ext:ListItem>
                                        <%--<ext:ListItem Text="密炼车间原材料库" Value="1"></ext:ListItem>
                                        <ext:ListItem Text="车间胶料库" Value="2"></ext:ListItem>
                                        <ext:ListItem Text="半制品车间原材料库" Value="3"></ext:ListItem>
                                        <ext:ListItem Text="半制品车间胶料库" Value="4"></ext:ListItem>--%>
                                    </Items>
                                </ext:ComboBox>
                                <ext:TextField ID="txtStorageName2" runat="server" FieldLabel="库房名称" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" LabelAlign="Left" IsRemoteValidation="true" >
                                     <RemoteValidation OnValidation="CheckField" />
                                </ext:TextField>
<%--                                <ext:TextField ID="txtStorageHigherLevel2" runat="server" FieldLabel="上级库房" LabelAlign="Left" Editable="false">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Fn="AddStorage" />
                                    </Listeners>
                                </ext:TextField>--%>
                                <ext:Checkbox ID="chkNatureMonth2" runat="server" FieldLabel="自然月" Checked="true" LabelAlign="Left" Hidden ="true">
                                    <DirectEvents>
                                        <Change OnEvent="chkNatureMonth2_CheckedChanged" />
                                    </DirectEvents>
                                </ext:Checkbox>
<%--                                <ext:ComboBox ID="chkDurationBeginDate2" runat="server" Disabled="true" FieldLabel="期间开始日期" EmptyText="选择上月某天" OnDirectChange="chkDurationBeginDate2_Change" LabelAlign="Left" />
                                <ext:ComboBox ID="chkDurationEndDate2" runat="server" Disabled="true" FieldLabel="期间截止日期" EmptyText="本月某天" ReadOnly="true" LabelAlign="Left" />--%>
                               <ext:TextField ID="txtERPCode2" runat="server" FieldLabel="ERP编号" LabelAlign="Left" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" />
                                <ext:TextField ID="txtDept2" runat="server" FieldLabel="部门编号" LabelAlign="Left" />
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
                        <Show Handler="for(var i=0;i<#{vpStorage}.items.length;i++){#{vpStorage}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vpStorage}.items.length;i++){#{vpStorage}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Hidden ID="hiddenStorageID" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenStorageName" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenObjID" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenNoStorageID" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenHigherLevel" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenNodeID" runat="server"></ext:Hidden>
            </Items>
        </ext:Viewport>
        <ext:DragTracker ID="DragTracker1" runat="server" ConstrainTo="={#{pnlList}.body}" Target="={#{pnlList}.body}" ProxyCls="black-view-selector">
            <Listeners>
                <Drag Fn="dragTrack" />
            </Listeners>
        </ext:DragTracker>

    </form>
</body>
</html>
