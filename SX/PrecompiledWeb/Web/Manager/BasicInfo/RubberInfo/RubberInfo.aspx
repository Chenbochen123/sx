﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_BasicInfo_RubberInfo_RubberInfo, App_Web_npi04ekm" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>胶料基础信息</title>
        <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
  <%--  <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/extExtra.css" />
    <link rel="stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources\css\font-awesome\css\font-awesome.css" />--%>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        //-------胶料类别-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryRubberType_Request = function (record) {//胶料类别返回值处理
            if (!App.winAdd.hidden) {
                App.add_rub_type_id.setValue(record.data.RubTypeName);
                App.hidden_rub_type_id.setValue(record.data.ObjID);
            }
            else if (!App.winModify.hidden) {
                App.modify_rub_type_id.setValue(record.data.RubTypeName);
                App.hidden_rub_type_id.setValue(record.data.ObjID);
            }
            else {
                App.txt_rub_type_id.getTrigger(0).show();
                App.txt_rub_type_id.setValue(record.data.RubTypeName);
                App.hidden_select_rub_type_id.setValue(record.data.ObjID);
            }
        }

        var SelectRubberType = function (field, trigger, index) {//胶料类别查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hidden_select_rub_type_id.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryRubberType_Window.show();
                    break;
            }
        }

        var UpdateRubberType = function () {//胶料类别查询
            App.Manager_BasicInfo_CommonPage_QueryRubberType_Window.show();
        }

        Ext.create("Ext.window.Window", {//胶料类别查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryRubberType_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryRubberType.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择胶料类别",
            layout: 'fit',
            resizable: false,
            closable: true,
            draggable: true,
            resizable: false,
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //-------轮胎部件-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryRubberTyrePart_Request = function (record) {//轮胎部件返回值处理
            if (!App.winAdd.hidden) {
                App.add_rub_purpose.setValue(record.data.TyrePartName);
            }
            else if (!App.winModify.hidden) {
                App.modify_rub_purpose.setValue(record.data.TyrePartName);
            }
            else {
                App.txt_rub_purpose.setValue(record.data.TyrePartName);
            }
            App.hidden_rub_purpose.setValue(record.data.ObjID);
        }

        var SelectRubPurpose = function () {//轮胎部件查询
            App.Manager_BasicInfo_CommonPage_QueryRubberTyrePart_Window.show();
        }

        Ext.create("Ext.window.Window", {//轮胎部件查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryRubberTyrePart_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryRubberTyrePart.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择胶料用途",
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //-------厂家-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryRubberFactory_Request = function (record) {//厂家返回值处理
            if (!App.winAdd.hidden) {
                App.add_factory_id.setValue(record.data.FacName);
            }
            else if (!App.winModify.hidden) {
                App.modify_factory_id.setValue(record.data.FacName);
            }
            else {
                App.txt_factory_id.setValue(record.data.FacName);
            }
            App.hidden_factory_id.setValue(record.data.ObjID);
        }

        var SelectFactory = function () {//厂家查询
            App.Manager_BasicInfo_CommonPage_QueryRubberFactory_Window.show();
        }

        Ext.create("Ext.window.Window", {//厂家查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryRubberFactory_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryRubberFactory.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择厂家",
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">

        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var ObjID = record.data.RubCode;
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
            integer: function(val,field)
                    {   
                        if(!val)
                        {
                            return true;
                        }
                        try     
                         {      
                             if(/^[\d]+\.?[\d]*$/.test(val))
                             { 
                                return true;      
                             }
                             else
                             {
                                return false;
                             }      
                         }      
                        catch(e)      
                        { 
                            return false;      
                        }      
                    },      
              integerText:"此填入项为数字格式！"
             
        });
         

        //列表刷新数据重载方法
         var pnlListFresh = function () {
            App.hidden_delete_flag.setValue("0");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }
        var allUserPnlListFresh = function () {
            App.AllUserStore.currentPage = 1;
            App.AllUserPageToolbar.doRefresh();
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
        };

        //拖拽产生提示文字
         var getDragDropText = function () {
             var buf = [];

             buf.push("<ul>");

             Ext.each(this.view.panel.getSelectionModel().getSelection(), function (record) {
                 buf.push("<li>" + record.data.Name + "</li>");
             });

             buf.push("</ul>");

             return buf.join("");
         };

         //
         var SetUserGridDrop = function (usercode) {
             var jsonStr = Ext.encode(App.SetUserPnl.getRowsValues());
             App.direct.set_user_power_drop(usercode,jsonStr, {
                 success: function (result) {
                     Ext.Msg.notify('操作', result);
                 },

                 failure: function (errorMsg) {
                     Ext.Msg.notify('操作', errorMsg);
                 }
             });
         }
    </script>
</head>
<body>
    <form id="fmRubber" runat="server">
        <asp:Button ID="Button2" Style="display:none"  runat="server" Text="Button" OnClientClick="pnlListFresh" />
        <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager ID="rmRubber" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlRubberTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barRubber">
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
                        <ext:Panel ID="pnlRubberQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout"  AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:TextField ID="txt_obj_id" Vtype="integer" runat="server" FieldLabel="胶料代码" LabelAlign="Right" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:TextField ID="txt_rub_name" runat="server" FieldLabel="胶料名称" LabelAlign="Right" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container_3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:TriggerField ID="txt_rub_type_id" runat="server" FieldLabel="胶料类别" LabelAlign="Right" Editable="false" >
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="SelectRubberType" />
                                                </Listeners>
                                            </ext:TriggerField>
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
                                        <ext:ModelField Name="RubCode" />
                                        <ext:ModelField Name="RubName" />
                                        <ext:ModelField Name="RubOtherName" />
                                        <ext:ModelField Name="RubTypeCode"  />
                                        <ext:ModelField Name="RubPurpose"  />
                                        <ext:ModelField Name="RubPowerUser"  />
                                        <ext:ModelField Name="RubRate"  />
                                        <ext:ModelField Name="FactoryID"  />
                                        <ext:ModelField Name="DeleteFlag"  />
                                        <ext:ModelField Name="Remark"  />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                            <ext:Column ID="obj_id" runat="server" Text="胶料编号" DataIndex="ObjID" Width="80"  />
                            <ext:Column ID="rub_code" runat="server" Text="胶料代码" DataIndex="RubCode" Width="80"  />
                            <ext:Column ID="rub_name" runat="server" Text="胶料名称" DataIndex="RubName" Width="80"  />
                            <ext:Column ID="rub_other_name" runat="server" Text="胶料别名" DataIndex="RubOtherName" Width="80"  />
                            <ext:Column ID="rub_type_id" runat="server" Text="胶料类别" DataIndex="RubTypeCode" Width="80"  />
                            <ext:Column ID="rub_purpose" runat="server" Text="胶料用途" DataIndex="RubPurpose" Width="80"  />
                            <ext:Column ID="rub_rate" runat="server" Text="含胶率" DataIndex="RubRate" Width="60"  />
                            <ext:Column ID="delete_flag" runat="server" Text="是否删除" DataIndex="DeleteFlag" Width="80" Hidden="true" />
                            <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Width="60"  />
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
                    <SelectionModel>
                        <ext:CheckboxSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single" />
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
                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改胶料基础信息"
                    Width="500" Height="400" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items> 
                        <ext:FormPanel ID="pnlEdit" runat="server" Flex="1" BodyPadding="1">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="Container2" runat="server" Layout="FormLayout" ColumnWidth=".5"
                                            Padding="1">
                                            <Items>
                                                <ext:TextField ID="modify_obj_id" runat="server" FieldLabel="胶料编号"   LabelAlign="Left" ReadOnly=true Enabled="true" />
                                                <ext:TextField ID="modify_rub_code" runat="server" FieldLabel="胶料代码"   LabelAlign="Left" ReadOnly=true Enabled="true" />
                                                <ext:TextField ID="modify_rub_name" runat="server" FieldLabel="胶料名称"  MaxLength="25"  LabelAlign="Left" Enabled="true" AllowBlank="false" Editable="false"  IndicatorText="*" IndicatorCls="red-text" >
                                                  
                                                </ext:TextField>
                                                <ext:TextField ID="modify_rub_other_name" runat="server" FieldLabel="胶料别名" MaxLength="25"   LabelAlign="Left" Enabled="true" />
                                                    <ext:TextField ID="modify_rub_rate" runat="server" Vtype="integer" FieldLabel="含胶率"   LabelAlign="Left" Enabled="true" />
                                                <ext:TextField ID="modify_remark" runat="server" FieldLabel="备注" MaxLength="50"   LabelAlign="Left" Enabled="true" />
                                          <ext:TextField ID="modify_rub_purpose" runat="server" FieldLabel="胶料用途" LabelAlign="Left" Editable="True" >
                                              
                                                </ext:TextField>
                                                <ext:TriggerField ID="modify_rub_type_id" runat="server" FieldLabel="胶料类别" LabelAlign="Left" AllowBlank="false" Editable="false" IndicatorText="*" IndicatorCls="red-text">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="UpdateRubberType" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container3" runat="server" Layout="FormLayout" ColumnWidth=".5"
                                            Padding="1">
                                            <Items>  
                                               
                                             <ext:TriggerField ID="modify_factory_id" runat="server" FieldLabel="厂家" LabelAlign="Left"  Editable="false" IndicatorText="*" IndicatorCls="red-text" Hidden="true">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectFactory" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                
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
                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加胶料基础信息"
                    Width="600" Height="350" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                        <ext:Container ID="Container5"  runat="server" Layout="FormLayout" ColumnWidth=".5"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="add_rub_name" runat="server" FieldLabel="胶料名称" MaxLength="25"   LabelAlign="Left" Enabled="true" AllowBlank="false"  IndicatorText="*" IndicatorCls="red-text" >
                                                   
                                                </ext:TextField>
                                                <ext:TextField ID="add_rub_other_name" runat="server" FieldLabel="胶料别名" MaxLength="25"   LabelAlign="Left" Enabled="true" />
                                                <ext:TriggerField ID="add_rub_type_id" runat="server" FieldLabel="胶料类别" LabelAlign="Left" AllowBlank="false" Editable="false" IndicatorText="*" IndicatorCls="red-text">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="UpdateRubberType" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TriggerField ID="add_factory_id" runat="server" FieldLabel="厂家" LabelAlign="Left" Editable="false" IndicatorText="*" IndicatorCls="red-text" Hidden="true">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectFactory" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container6"  runat="server" Layout="FormLayout" ColumnWidth=".5"
                                            Padding="5">
                                            <Items>  
                                                <ext:TextField ID="add_rub_purpose" runat="server"  FieldLabel="胶料用途"  LabelAlign="Left" Enabled="true" />
                                                <ext:TextField ID="add_rub_rate" runat="server" Vtype="integer" FieldLabel="含胶率"  LabelAlign="Left" Enabled="true" />
                                                <ext:TextField ID="add_remark" runat="server" FieldLabel="备注" MaxLength="25"   LabelAlign="Left" Enabled="true" />
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
                <ext:Window ID="winSetRubPower" runat="server" Icon="MonitorAdd" Closable="true" Title="设置胶料人员权限"
                    Height="550" Width="610" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:Panel ID="northPanel" runat="server" Region="North" AutoHeight="true">
                            <TopBar>
                                <ext:Toolbar runat="server" ID="northToolbar">
                                    <Items>
                                        <ext:ToolbarSeparator ID="toolbarSeparator2" />
                                        <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip4" runat="server" Html="点击进行查询" />
                                            </ToolTips>
                                            <Listeners>
                                                <Click Fn="allUserPnlListFresh"></Click>
                                            </Listeners>
                                        </ext:Button>
                                        <ext:ToolbarSeparator ID="toolbarSeparator3" />
                                        <ext:ToolbarSpacer runat="server" ID="toolbarSpacer1" />
                                        <ext:ToolbarFill ID="toolbarFill1" />
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Items>
                                <ext:Panel ID="panelNorthQuery" runat="server" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container_Query" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                            <Items>
                                                <ext:Container ID="container7" runat="server" Layout="FormLayout" ColumnWidth="0.4" Padding="5">
                                                    <Items>
                                                        <ext:TextField ID="txtUserName" runat="server" FieldLabel="用户姓名" LabelAlign="Right" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="container8" runat="server" Layout="FormLayout" ColumnWidth="0.4" Padding="5">
                                                    <Items>
                                                        <ext:TextField ID="txtHRCode" runat="server" FieldLabel="HR代码" LabelAlign="Right" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="Panel1" runat="server" Height="400">
                            <LayoutConfig>
                                <ext:HBoxLayoutConfig Align="Stretch" Padding="5" />
                            </LayoutConfig>
                            <Items>
                                <ext:GridPanel ID="AllUserPnl"  runat="server"  MultiSelect="true" Title="所有用户列表" TitleAlign="Center" Flex="1" Margins="0 2 0 0">
                                    <Store>
                                        <ext:Store ID="AllUserStore" runat="server" PageSize="10">
                                            <Proxy>
                                                <ext:PageProxy DirectFn="App.direct.GridPanelBindUserData" />
                                            </Proxy>
                                            <Model>
                                                <ext:Model ID="Model1" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="UserName" />
                                                        <ext:ModelField Name="WorkBarcode" />
                                                        <ext:ModelField Name="HRCode" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel>
                                        <Columns>
                                            <ext:Column ID="Column1" runat="server" Text="用户名称" Flex="1" Align="Center" DataIndex="UserName" />
                                            <ext:Column ID="Column2" runat="server" Text="用户工号" Flex="1" Align="Center" DataIndex="WorkBarcode" />
                                            <ext:Column ID="Column3" runat="server" Text="HR编号" Flex="1" Align="Center" DataIndex="HRCode" />
                                        </Columns>
                                    </ColumnModel>                    
                                    <View>
                                        <ext:GridView ID="GridView1" runat="server">
                                            <Plugins>
                                                <ext:GridDragDrop ID="GridDragDrop1" runat="server" DragGroup="firstGridDDGroup" DropGroup="secondGridDDGroup"/>
                                            </Plugins>
                                            <Listeners>
                                                <AfterRender Handler="this.plugins[0].dragZone.getDragText = getDragDropText;" />
                                                <Drop Handler="Ext.net.Notification.show({title:'提示', html:'删除用户： ' + data.records[0].get('UserName') + '成功'});" />
                                            </Listeners>
                                        </ext:GridView>
                                    </View>
                                     <BottomBar>
                                        <ext:PagingToolbar ID="AllUserPageToolbar" runat="server">
                                            <Plugins>
                                                <ext:ProgressBarPager ID="ProgressBarPager1" runat="server" />
                                            </Plugins>
                                        </ext:PagingToolbar>
                                    </BottomBar>   
                                </ext:GridPanel>
                                <ext:GridPanel ID="SetUserPnl" runat="server" MultiSelect="true" Title="设置用户列表" TitleAlign="Center" Flex="1" Margins="0 0 0 3" >
                                    <Store>
                                        <ext:Store ID="SetUserStore" runat="server">
                                            <Model>
                                                <ext:Model ID="Model2" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="UserName" />
                                                        <ext:ModelField Name="WorkBarcode" />
                                                        <ext:ModelField Name="HRCode" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel>
                                        <Columns>
                                            <ext:Column ID="Column4" runat="server" Text="用户名称" Flex="1" Align="Center" DataIndex="UserName" />
                                            <ext:Column ID="Column5" runat="server" Text="用户工号" Flex="1" Align="Center" DataIndex="WorkBarcode" />
                                            <ext:Column ID="Column6" runat="server" Text="HR编码" Flex="1" Align="Center" DataIndex="HRCode" />
                                        </Columns>
                                    </ColumnModel>                   
                                    <View>
                                        <ext:GridView ID="GridView2" runat="server">
                                            <Plugins>
                                                <ext:GridDragDrop ID="GridDragDrop2" runat="server" DragGroup="secondGridDDGroup" DropGroup="firstGridDDGroup"/>
                                            </Plugins>
                                            <Listeners>
                                                <BeforeDrop Handler="SetUserGridDrop(data.records[0].get('WorkBarcode'))" />
                                                <AfterRender Handler="this.plugins[0].dragZone.getDragText = getDragDropText;" />
                                                <Drop Handler="var dropOn = overModel ? ' ' + dropPosition + ' ' + overModel.get('UserName') : ' on empty view';" />
                                            </Listeners>
                                        </ext:GridView>
                                    </View>   
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnSetPowerSave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="BtnSetPowerSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                    <ExtraParams>
                                        <ext:Parameter Name="Values" Value="Ext.encode(#{SetUserPnl}.getRowsValues())" Mode="Raw" />
                                    </ExtraParams>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnSetPowerCanel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="BtnSetPowerCanel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Hidden ID="hidden_select_rub_type_id" runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_rub_type_id" runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_rub_purpose" runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_factory_id" runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_type" runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_rubname" runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_delete_flag" runat="server" Text="0"></ext:Hidden>
                <ext:Hidden ID="hidden_set_power_user_rub_code" runat="server"></ext:Hidden>
            </Items>
        </ext:Viewport>
        </form>
</body>
</html>