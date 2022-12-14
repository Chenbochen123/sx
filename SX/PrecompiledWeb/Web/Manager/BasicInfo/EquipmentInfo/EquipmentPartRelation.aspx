﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_BasicInfo_EquipmentInfo_EquipmentPartRelation, App_Web_0zpstymj" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>设备部件关联信息</title>
        <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <%--<link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/extExtra.css" />
    <link rel="stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources\css\font-awesome\css\font-awesome.css" />--%>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        //------部件------查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryEquipPartInfo_Request = function (record) {//部件返回值处理
            if (!App.winAdd.hidden) {
                App.add_part_code.setValue(record.data.PartName);
                App.hidden_part_code.setValue(record.data.ObjID);
            }
            else if (!App.winModify.hidden) {
                App.modify_part_code.setValue(record.data.PartName);
                App.hidden_part_code.setValue(record.data.ObjID);
                App.hidden_part_code.setValue(record.data.ObjID);
            }
        }

        var SelectEquipPart = function () {
            var url = "../../BasicInfo/CommonPage/QueryEquipmentPart.aspx?equipType=" + App.hidden_equip_type.getValue();
            var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (App.Manager_BasicInfo_CommonPage_QueryEquipPartInfo_Window.getBody()) {
                App.Manager_BasicInfo_CommonPage_QueryEquipPartInfo_Window.getBody().update(html);
            } else {
                App.Manager_BasicInfo_CommonPage_QueryEquipPartInfo_Window.html = html;
            }
            App.Manager_BasicInfo_CommonPage_QueryEquipPartInfo_Window.show();
        }
        Ext.create("Ext.window.Window", {//部件查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryEquipPartInfo_Window",
            height: 460,
            hidden: true,
            width: 360,
            bodyStyle: "background-color: #fff;",
            closable: true,
            layout: 'fit',
            title: "请选择部件",
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var ObjID = record.data.ObjID;
            var EquipCode = record.data.EquipCode;
            App.direct.commandcolumn_direct_edit(ObjID,EquipCode, {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //点击恢复按钮
        var commandcolumn_direct_recover = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ObjID = record.data.ObjID;
            App.direct.commandcolumn_direct_recover(ObjID, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
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
            var EquipCode = record.data.EquipCode;
            App.direct.commandcolumn_direct_delete(ObjID,EquipCode, {
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
            if (command.toLowerCase() == "recover") {
                Ext.Msg.confirm("提示", '您确定需要恢复此条信息？', function (btn) { commandcolumn_direct_recover(btn, record) });
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
                    return;
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

        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.hidden_equip_code.setValue("");
            App.hidden_equip_type.setValue("");
            App.hidden_delete_flag.setValue("0");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        //历史查询按钮点击列表刷新数据重载方法
        var pnlHistoryListFresh = function () {
            App.hidden_equip_code.setValue("");
            App.hidden_equip_type.setValue("");
            App.hidden_delete_flag.setValue("");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

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
    </script>
    <script>
        //树形结构点击刷新右侧方法
        var loadPage = function (record) {
            if (record.getDepth() == 2) {
                App.hidden_equip_code.setValue(record.getId());
                App.hidden_equip_type.setValue("");
            }
            if (record.getDepth() == 1) {

                App.hidden_equip_code.setValue("");
                App.hidden_equip_type.setValue(record.getId());
            }
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
        };

        var filterTree = function (tf, e) {
            var tree = App.treeDept,
                text = tf.getRawValue();

            tree.clearFilter();

            if (Ext.isEmpty(text, false)) {
                return;
            }

            if (e.getKey() === Ext.EventObject.ESC) {
                clearFilter();
            } else {
                var re = new RegExp(".*" + text + ".*", "i");

                tree.filterBy(function (node) {
                    return re.test(node.data.text);
                });
            }
        };

        var clearFilter = function () {
            var field = App.TextField,
                tree = App.treeDept;

            field.setValue("");
            tree.clearFilter(true);
            tree.getView().focus();
        };
    </script>
</head>
<body>
    <form id="fmUnit" runat="server">
        <asp:Button ID="Button2" Style="display: none" runat="server" Text="Button" OnClientClick="pnlListFresh" />
        <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button"
            OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager ID="rmUnit" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Region="West" Width="250" Layout="BorderLayout">
                    <Items>
                        <ext:TreePanel ID="treeDept" runat="server" Title="设备列表" Region="Center" Icon="FolderGo"
                            AutoHeight="true" RootVisible="false">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:ToolbarTextItem ID="ToolbarTextItem1" runat="server" Text="过滤:" />
                                        <ext:ToolbarSpacer />
                                        <ext:TriggerField ID="TextField" runat="server" EnableKeyEvents="true">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" />
                                            </Triggers>
                                            <Listeners>
                                                <KeyUp Fn="filterTree" Buffer="250" />
                                                <TriggerClick Handler="clearFilter();" />
                                            </Listeners>
                                        </ext:TriggerField>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
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
                <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUnit">
                            <Items>
                               <ext:Button runat="server" Icon="Add" Text="添加" ID="btn_add">
                                <ToolTips>
                                    <ext:ToolTip ID="ttAdd" runat="server" Html="点击进行添加" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="btn_add_Click">
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                <ToolTips>
                                    <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                </ToolTips>
                                <Listeners>
                                    <Click Fn="pnlListFresh">
                                    </Click>
                                </Listeners>
                            </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_middle" />
                                <ext:ToolbarSeparator ID="toolbarSeparator_middle_2" />
                            <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTip2" runat="server" Html="点击将查询结果导出到Excel中" />
                                </ToolTips>
                                <Listeners>
                                    <Click Handler="$('#btnExportSubmit').click();">
                                    </Click>
                                </Listeners>
                            </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel ID="pnlUnitQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_obj_id" Vtype="integer" runat="server" FieldLabel="关联编号" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_remark" runat="server" FieldLabel="备注" LabelAlign="Right" />
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
                                        <ext:ModelField Name="PartCode" />
                                        <ext:ModelField Name="EquipCode" />
                                        <ext:ModelField Name="EquipName" />
                                        <ext:ModelField Name="EquipType" />
                                        <ext:ModelField Name="EquipAddress" />
                                        <ext:ModelField Name="LastModifyDate" Type="Date" />
                                        <ext:ModelField Name="Remark" />
                                        <ext:ModelField Name="DeleteFlag" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                            <ext:Column ID="obj_id" runat="server" Text="关联编号" DataIndex="ObjID" Width="80" />
                            <ext:Column ID="part_code" runat="server" Text="部件名称" DataIndex="PartCode" Width="80" />
                            <ext:Column ID="equip_code" runat="server" Text="机台名称" DataIndex="EquipName" Width="80" />
                            <ext:Column ID="equip_type" runat="server" Text="设备类型" DataIndex="EquipType" Width="80" />
                            <ext:Column ID="equip_address" runat="server" Text="设备地址" DataIndex="EquipAddress"
                                Width="150" />
                      <%--      <ext:DateColumn ID="last_modify_date" runat="server" Text="修改时间" DataIndex="LastModifyDate"
                                Format="yyyy-MM-dd HH:mm:ss" Width="150" />--%>
                            <ext:Column ID="delete_flag" runat="server" Text="删除标志" DataIndex="DeleteFlag" Width="150"
                                Hidden="true" />
                            <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Width="80" />
                            <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                <Commands>
                                     <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                    <ToolTip Text="修改本条数据" />
                                </ext:GridCommand>
                                <ext:CommandSeparator />
                                <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                    <ToolTip Text="删除本条数据" />
                                </ext:GridCommand>
                                <ext:CommandSeparator />
                                <ext:GridCommand Icon="Accept" CommandName="Recover" Text="恢复">
                                    <ToolTip Text="恢复本条数据" />
                                </ext:GridCommand>
                                </Commands>
                                <PrepareToolbar Fn="prepareToolbar" />
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
                        <ext:PagingToolbar ID="pageToolBar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改部件信息"
                    Width="350" Height="240" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:FormPanel ID="pnlEdit" runat="server" Flex="1" BodyPadding="5" Layout="ColumnLayout">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Container ID="Container2" runat="server" Layout="FormLayout" ColumnWidth="1"
                                    Padding="5">
                                    <Items>
                                        <ext:TextField LabelAlign="left" ID="modify_obj_id" runat="server" FieldLabel="关联编号"
                                            Hidden="true" />
                                        <ext:TextField LabelAlign="left" ID="modify_part_code" runat="server" FieldLabel="部件名称"
                                            Editable="false" AllowBlank="false"  ReadOnly="true">
                                      
                                        </ext:TextField>
                                        <ext:TextField LabelAlign="left" ID="modify_equip_code" runat="server" FieldLabel="设备名称" />
                                        <ext:TextField LabelAlign="left" ID="modify_equip_type" runat="server" FieldLabel="设备类型" Visible="false" />
                                        <ext:TextField LabelAlign="left" ID="modify_equip_address" runat="server" FieldLabel="设备地址"
                                            MaxLength="40" IsRemoteValidation="true">
                                            <RemoteValidation OnValidation="CheckEquipIP" />
                                        </ext:TextField>
                                        <ext:TextField LabelAlign="left" ID="modify_remark" runat="server" FieldLabel="备注"
                                            MaxLength="150" />
                                    </Items>
                                </ext:Container>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnModifySave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnModifySave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="BtnModifySave_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="BtnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加部件信息"
                    Width="350" Height="340" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5" Layout="ColumnLayout">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth="1"
                                    Padding="5">
                                    <Items>
                                        <ext:TriggerField LabelAlign="left" ID="add_part_code" runat="server" FieldLabel="部件名称"
                                            Editable="false" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="SelectEquipPart" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:TextField LabelAlign="left" ID="add_code" runat="server" FieldLabel="设备编码" MaxLength="150" Visible="false"/>
                                        <ext:TextField LabelAlign="left" ID="add_equip_code" runat="server" FieldLabel="设备名称"
                                            ReadOnly="true" />
                                        <ext:TextField LabelAlign="left" ID="add_equip_type" runat="server" FieldLabel="设备分类" Visible="false"
                                            ReadOnly="true" />
                                        <ext:TextField LabelAlign="left" ID="add_equip_address" runat="server" FieldLabel="设备地址"
                                            MaxLength="40" IsRemoteValidation="true">
                                            <RemoteValidation OnValidation="CheckEquipIP" />
                                        </ext:TextField>
                                        <ext:TextField LabelAlign="left" ID="add_remark" runat="server" FieldLabel="备注" MaxLength="150" />
                                    </Items>
                                </ext:Container>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnAddSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <DirectEvents>
                                <Click OnEvent="BtnAddSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="BtnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Hidden ID="hidden_part_name" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hidden_equip_name" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hidden_part_code" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hidden_equip_code" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hidden_equip_type" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hidden_delete_flag" runat="server" Text="0">
                </ext:Hidden>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
