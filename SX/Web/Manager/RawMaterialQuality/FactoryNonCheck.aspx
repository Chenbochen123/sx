<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FactoryNonCheck.aspx.cs" Inherits="Manager_RawMaterialQuality_FactoryNonCheck" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>厂商免质检批次设置</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript">
         //-------供应商-----查询带回弹出框--BEGIN
         var Manager_BasicInfo_CommonPage_QueryFactory_Request = function (record) {//厂商查询返回值处理
             if (!App.winAdd.hidden) {
                 App.add_factory_code.setValue(record.data.FacName);
                 App.hidden_factory_code.setValue(record.data.ObjID.toString());
                 App.add_factory_code.getTrigger(0).show();
             }
             else if (!App.winModify.hidden) {
                 App.modify_factory_code.setValue(record.data.FacName);
                 App.hidden_factory_code.setValue(record.data.ObjID.toString());
                 App.modify_factory_code.getTrigger(0).show();
             }
         }

         var SelectFactory = function (field, trigger, index) {//部门查询

             switch (index) {
                 case 0:
                     field.getTrigger(0).hide();
                     field.setValue('');
                     App.hidden_factory_code.setValue("");
                    
                     break;
                 case 1:
                     App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
                     break;
             }


//             App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
         }

         // 供应商查询带回窗体
         Ext.create("Ext.window.Window", {
             id: "Manager_BasicInfo_CommonPage_QueryFactory_Window",
             height: 460,
             hidden: true,
             width: 400,
             html: "<iframe src='../BasicInfo/CommonPage/QueryFactory.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
             bodyStyle: "background-color: #fff;",
             closable: true,
             title: "请选择供应商",
             modal: true
         });
         //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //-------物料-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料查询返回值处理
            if (!App.winAdd.hidden) {
                App.add_material_code.setValue(record.data.MaterialName);
                App.hidden_material_code.setValue(record.data.MaterialCode);
            }
            else if (!App.winModify.hidden) {
                App.modify_material_code.setValue(record.data.MaterialName);
                App.hidden_material_code.setValue(record.data.MaterialCode);
            }
        }

        var SelectMaterial = function (field, trigger, index) {//部门查询
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
        }

        // 供应商查询带回窗体
        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            height: 460,
            hidden: true,
            width: 400,
            html: "<iframe src='../BasicInfo/CommonPage/QueryMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料",
            modal: true
        });
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
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

        //点击恢复按钮
        var commandcolumn_direct_recover = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ObjID = record.data.ObjID;
            App.direct.commandcolumn_direct_recover(ObjID, {
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


        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.hidden_delete_flag.setValue("0");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        //历史查询按钮点击列表刷新数据重载方法
        var pnlHistoryListFresh = function () {
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
        }
    </script>
</head>
<body>
    <form id="fmUser" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager ID="rmUser" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlUserTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUser">
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
                                        <Click Fn="pnlListFresh"></Click>
                                    </Listeners>
                                </ext:Button> 
                                <ext:ToolbarSeparator ID="toolbarSeparator_middle" />
                                 <ext:Button runat="server" Icon="Note" Text="历史查询" ID="btn_history_search">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行历史查询" />
                                    </ToolTips>
                                     <Listeners>
                                        <Click Fn="pnlHistoryListFresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_middle_2" />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip2" runat="server" Html="点击将查询结果导出到Excel中" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>

                    <Items>
                        <ext:Panel ID="pnlUserQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_material_name" runat="server" FieldLabel="物料名称" LabelAlign="Right"/>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_factory_name" runat="server" FieldLabel="厂商名称" LabelAlign="Right" />
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
                                        <ext:ModelField Name="FactoryCode" />
                                        <ext:ModelField Name="MaterialCode"  />
                                        <ext:ModelField Name="NonCheckNum"  />
                                        <ext:ModelField Name="NonCheckWeight"  />
                                        <ext:ModelField Name="DeleteFlag"  />
                                        <ext:ModelField Name="Remark"  />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" Width="45" runat="server" />
                            <ext:Column ID="obj_id" runat="server" Text="自增编号" DataIndex="ObjID" Visible="False" Width="150"  />
                            <ext:Column ID="factory_code" runat="server" Text="厂商名称" DataIndex="FactoryCode" Width="200"  />
                            <ext:Column ID="material_code" runat="server" Text="物料名称" DataIndex="MaterialCode" Width="100"  />
                            <ext:Column ID="non_check_num" runat="server" Text="免审核批次" DataIndex="NonCheckNum" Width="100"  />
                            <ext:Column ID="Column1" runat="server" Text="免审核重量(KG)" DataIndex="NonCheckWeight" Width="100"  />
                            <ext:Column ID="delete_flag" runat="server" Text="删除标志" DataIndex="DeleteFlag" Width="80" Hidden="true"  />
                            <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Width="80"  />
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
                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改信息"
                    Width="400" Height="350" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="Container2" runat="server" Layout="FormLayout" ColumnWidth="1"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField      ID="modify_obj_id" runat="server" FieldLabel="自增编号"   LabelAlign="Left" ReadOnly="true" Enabled="true" />
                                                <ext:TriggerField   ID="modify_factory_code" runat="server" FieldLabel="厂商名称" LabelAlign="Left" Editable="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search"/>
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectFactory" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TriggerField   ID="modify_material_code" runat="server" FieldLabel="物料名称" LabelAlign="Left" Editable="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search"/>
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectMaterial" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:NumberField    ID="modify_non_check_num" runat="server" FieldLabel="免审批次"   LabelAlign="Left" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" />
                                                <ext:NumberField    ID="modify_non_check_Weight" runat="server" FieldLabel="免审重量(KG)"   LabelAlign="Left" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"  DecimalPrecision="3"/>
                                                <ext:TextField      ID="modify_remark" runat="server" FieldLabel="备注" LabelAlign="Left" Enabled="true" />
                                            </Items>
                                        </ext:Container>
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
                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加信息"
                    Width="400" Height="350" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items> 
                                <ext:Container ID="Container4" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items> 
                                        <ext:Container ID="Container5"  runat="server" Layout="FormLayout" ColumnWidth="1"
                                            Padding="5">
                                            <Items>
                                                <ext:TriggerField   ID="add_factory_code" runat="server" FieldLabel="厂商名称" LabelAlign="Left" Editable="false"  IndicatorCls="red-text"  >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search"/>
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectFactory" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TriggerField   ID="add_material_code" runat="server" FieldLabel="物料名称" LabelAlign="Left" Editable="false" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"  >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search"/>
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectMaterial" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:NumberField   ID="add_non_check_num" runat="server" FieldLabel="免审批次"  LabelAlign="Left" Enabled="true"  AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" />
                                                 <ext:NumberField    ID="add_non_check_Weight" runat="server" FieldLabel="免审重量(KG)"   LabelAlign="Left" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"  DecimalPrecision="3"/>
                                              <ext:TextField  ID="add_remark" runat="server" FieldLabel="备注" LabelAlign="Left" Enabled="true"  /> 
                                            </Items>
                                        </ext:Container>
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
                <ext:Hidden ID="hidden_factory_code" runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_factory_code_origin" runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_material_code" runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_material_code_origin" runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_delete_flag"  runat="server" Text="0"></ext:Hidden>
            </Items>
        </ext:Viewport>
        </form>
</body>
</html>