<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LedgerItem.aspx.cs" Inherits="Manager_RawMaterialQuality_LedgerItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>台账项目维护</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript" language="javascript">
         //点击修改按钮
         var commandcolumn_direct_edit = function (record) {
             var KeyId = record.data.KeyId;
             App.direct.commandcolumn_direct_edit(KeyId, {
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
             var KeyId = record.data.KeyId;
             App.direct.commandcolumn_direct_delete(KeyId, {
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
             return false;
         };

         //根据按钮类别进行删除和编辑操作
         var commandcolumn_click = function (command, record) {
             commandcolumn_click_confirm(command, record);
             return false;
         };

         //转换DeleteFlag显示方式
         var deleteConvert = function (value) {
             return Ext.String.format(value == "1" ? "是" : "否");
         };

         //刷新中央列表
         var pnlCenterRefresh = function () {
             App.store.currentPage = 1;
             App.pageToolBar.doRefresh();
             return false;
         }
    </script>
    <script type="text/javascript" language="javascript">
        //点击修改值按钮
        var commandcolumndetail_direct_edit = function (record) {
            var ValueId = record.data.ValueId;
            App.direct.commandcolumndetail_direct_edit(ValueId, {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //点击删除值按钮
        var commandcolumndetail_direct_delete = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ValueId = record.data.ValueId;
            App.direct.commandcolumndetail_direct_delete(ValueId, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //区分删除操作，并进行二次确认操作
        var commandcolumndetail_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumndetail_direct_edit(record);
            }
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此条信息？', function (btn) { commandcolumndetail_direct_delete(btn, record) });
            }
            return false;
        };

        //根据按钮类别进行删除和编辑操作
        var commandcolumndetail_click = function (command, record) {
            commandcolumndetail_click_confirm(command, record);
            return false;
        };

        //刷新下方列表
        var pnlSouthRefresh = function () {
            App.storeValue.currentPage = 1;
            App.pageToolBar2.doRefresh();
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
     <ext:ResourceManager ID="rmLedgerItem" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
              <ext:Panel ID="pnlNorth" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUnit1">
                            <Items>
                                <ext:Button runat="server" Icon="Add" Text="添加" ID="btnAdd">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttAdd" runat="server" Html="点击进行添加" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btn_add_Click">
                                            <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="pnlCenterRefresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsMiddle" />
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
                      <ext:FormPanel runat="server" ID="pnlForm" Layout="ColumnLayout" AutoHeight="true">
                         <Items>
                            <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".2" Padding="5">
                                <Items>
                                    <ext:ComboBox ID="cbxValueType" runat="server" FieldLabel="赋值类型" LabelAlign="Right" Visible="true" Editable="false">
                                        <SelectedItems>
                                            <ext:ListItem Value="all">
                                            </ext:ListItem>
                                        </SelectedItems>
                                        <Items>
                                            <ext:ListItem Text="全部" Value="all" AutoDataBind="true">
                                            </ext:ListItem>
                                            <ext:ListItem Text="文字" Value="文字">
                                            </ext:ListItem>
                                            <ext:ListItem Text="数字" Value="数字">
                                            </ext:ListItem>
                                            <ext:ListItem Text="日期" Value="日期">
                                            </ext:ListItem>
                                        </Items>
                                        <Listeners>
                                            <Select Fn="pnlCenterRefresh"></Select>
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".2" Padding="5">
                                <Items>
                                 <ext:ComboBox ID="cbxCanDropDown" runat="server" FieldLabel="是否可下拉" LabelAlign="Right" Visible="true" Editable="false">
                                    <SelectedItems>
                                        <ext:ListItem Value="all">
                                        </ext:ListItem>
                                    </SelectedItems>
                                    <Items>
                                        <ext:ListItem Text="全部" Value="all" AutoDataBind="true">
                                        </ext:ListItem>
                                        <ext:ListItem Text="否" Value="0">
                                        </ext:ListItem>
                                        <ext:ListItem Text="是" Value="1">
                                        </ext:ListItem>
                                    </Items>
                                    <Listeners>
                                        <Select Fn="pnlCenterRefresh"></Select>
                                    </Listeners>
                                 </ext:ComboBox>
                                </Items>
                              </ext:Container>
                         </Items>
                      </ext:FormPanel>
                    </Items>
              </ext:Panel>

              <ext:Panel ID="pnlCenter" runat="server" Region="Center" Frame="true" Layout="Fit" MarginsSummary="0 5 0 5">
                <Items>
                    <ext:GridPanel ID="pnlItem" runat="server" MarginsSummary="0 5 5 5">
                      <Store>
                           <ext:Store ID="store" runat="server" PageSize="10">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model" runat="server" IDProperty="KeyId">
                                        <Fields>
                                            <ext:ModelField Name="KeyId" />
                                            <ext:ModelField Name="KeyName" />
                                            <ext:ModelField Name="KeyCode" />
                                            <ext:ModelField Name="ValueType" />
                                            <ext:ModelField Name="HasSelection" Type="Boolean" />
                                            <ext:ModelField Name="Remark" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                           </ext:Store>
                      </Store>
                      <ColumnModel ID="colModel1" runat="server">
                          <Columns>
                              <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />
                              <ext:Column ID="keyName" runat="server" Text="项目名称" DataIndex="KeyName" Flex="1" />
                              <ext:Column ID="keyCode" runat="server" Text="项目英文名" DataIndex="KeyCode" Flex="1" />
                              <ext:Column ID="valueType" runat="server" Text="赋值类型" DataIndex="ValueType" Flex="1" />
                              <ext:Column ID="hasSelection" runat="server" Text="是否可下拉" DataIndex="HasSelection" Flex="1" >
                                    <Renderer Fn="deleteConvert" />
                              </ext:Column>
                              <ext:Column ID="remark1" runat="server" Text="备注" DataIndex="Remark" Flex="1" />
                              <ext:CommandColumn ID="commandCol1" runat="server" Width="150" Text="操作" Align="Center">
                                    <Commands>
                                        <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改" >
                                            <ToolTip Text="修改本条数据" />
                                        </ext:GridCommand>
                                        <ext:CommandSeparator />
                                        <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除" >
                                            <ToolTip Text="删除本条数据" />
                                        </ext:GridCommand>
                                    </Commands>
                                    <Listeners>
                                        <Command Handler="return commandcolumn_click(command, record);" />
                                    </Listeners>
                              </ext:CommandColumn>
                          </Columns>
                      </ColumnModel>
                      <SelectionModel>
                            <ext:RowSelectionModel ID="rowSelect" runat="server" Mode="Single">
                                <Listeners>
                                    <Select Handler="#{storeValue}.reload();" Buffer="250" />
                                </Listeners>
                            </ext:RowSelectionModel>
                      </SelectionModel>
                      <BottomBar>
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
                                            <Select Handler="#{pnlItem}.store.pageSize = parseInt(this.getValue(), 10); #{pageToolBar}.doRefresh(); return false;" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager1" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                      </BottomBar>
                    </ext:GridPanel>
                </Items>
              </ext:Panel>

              <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="项目可选值" Height="200" Icon="Basket" Layout="Fit" Collapsible="true" Split="true" MarginsSummary="0 5 5 5">
                <TopBar>
                    <ext:Toolbar runat="server" ID="barUnit2">
                        <Items>
                            <ext:Button runat="server" Icon="Add" Text="添加" ID="btnAddValue">
                                <ToolTips>
                                    <ext:ToolTip ID="ttAddValue" runat="server" Html="点击进行添加" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="btn_adddetail_Click">
                                        <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_end2" />
                            <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end2" />
                            <ext:ToolbarFill ID="toolbarFill_end2" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:GridPanel ID="pnlValue" runat="server" MarginsSummary="0 5 5 5">
                      <Store>
                        <ext:Store ID="storeValue" runat="server" PageSize="10" OnReadData="RowSelect">
                                    <Model>
                                        <ext:Model ID="modelValue" runat="server" IDProperty="ValueId">
                                            <Fields>
                                                <ext:ModelField Name="ValueId" />
                                                <ext:ModelField Name="KeyId" />
                                                <ext:ModelField Name="KeyValue" />
                                                <ext:ModelField Name="Remark" />
                                                <ext:ModelField Name="DeleteFlag" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Parameters>
                                        <ext:StoreParameter Name="KeyId" Mode="Raw" Value="#{pnlItem}.getSelectionModel().hasSelection() ? #{pnlItem}.getSelectionModel().getSelection()[0].data.KeyId : -1" />
                                    </Parameters>
                                </ext:Store>
                            </Store>
                              <ColumnModel ID="colModel2" runat="server">
                                  <Columns>
                                      <ext:RowNumbererColumn ID="rowNumCol2" runat="server" Width="35" />
                                      <ext:Column ID="keyValue" runat="server" Text="字段可选值" DataIndex="KeyValue" Flex="1" />
                                      <ext:Column ID="valueRemark" runat="server" Text="备注" DataIndex="Remark" Flex="1" />
                                      <ext:CommandColumn ID="commandCol2" runat="server" Width="150" Text="操作" Align="Center">
                                            <Commands>
                                                <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改" >
                                                    <ToolTip Text="修改本条数据" />
                                                </ext:GridCommand>
                                                <ext:CommandSeparator />
                                                <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除" >
                                                    <ToolTip Text="删除本条数据" />
                                                </ext:GridCommand>
                                            </Commands>
                                            <Listeners>
                                                <Command Handler="return commandcolumndetail_click(command, record);" />
                                            </Listeners>
                                      </ext:CommandColumn>
                                  </Columns>
                              </ColumnModel>
                              <BottomBar>
                                    <ext:PagingToolbar ID="pageToolBar2" runat="server">
                                        <Items>
                                            <ext:Label ID="Label2" runat="server" Text="每页条数:" />
                                            <ext:ToolbarSpacer ID="ToolbarSpacer2" runat="server" Width="10" />
                                            <ext:ComboBox ID="ComboBox2" runat="server" Width="80" Editable="false">
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
                                                    <Select Handler="#{pnlValue}.store.pageSize = parseInt(this.getValue(), 10); #{pageToolBar2}.doRefresh(); return false;" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                        <Plugins>
                                            <ext:ProgressBarPager ID="ProgressBarPager2" runat="server" />
                                        </Plugins>
                                    </ext:PagingToolbar>
                              </BottomBar>
                    </ext:GridPanel>
                </Items>
              </ext:Panel>
              <ext:Window ID="windowModifyKey" runat="server" Icon="MonitorEdit" Closable="false" Title="修改台账项目"
                    Width="320" Height="220" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items> 
                       <ext:FormPanel ID="pnlEditKey" runat="server" Flex="1" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="txtModifyKeyId" runat="server" FieldLabel="项目编号"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                                <ext:TextField ID="txtModifyKeyName" runat="server" FieldLabel="项目名称"  LabelAlign="Right" AllowBlank="False" MaxLength="50" IndicatorText="*" IndicatorCls="red-text"/>
                                <ext:TextField ID="txtModifyKeyCode" runat="server" FieldLabel="英文名称" LabelAlign="Right" MaxLength="50"/>
                                    <ext:ComboBox ID="cbxModifyValueType" runat="server" FieldLabel="赋值类型" LabelAlign="Right" AllowBlank="False" OnDirectChange="cbxModifyValueType_change" Editable="false" IndicatorText="*" IndicatorCls="red-text">
                                        <Items>
                                            <ext:ListItem Text="数字" Value="数字">
                                            </ext:ListItem>
                                            <ext:ListItem Text="文字" Value="文字">
                                            </ext:ListItem>
                                            <ext:ListItem Text="日期" Value="日期">
                                            </ext:ListItem>
                                        </Items>
                                    </ext:ComboBox>
                                    <ext:ComboBox ID="cbxModifyCanDropDown" runat="server" FieldLabel="是否可下拉" LabelAlign="Right" AllowBlank="False" Editable="false" IndicatorText="*" IndicatorCls="red-text">
                                        <Items>
                                            <ext:ListItem Text="否" Value="0">
                                            </ext:ListItem>
                                            <ext:ListItem Text="是" Value="1">
                                            </ext:ListItem>
                                        </Items>
                                    </ext:ComboBox>
                                <ext:TextField ID="txtModifyKeyRemark" runat="server" FieldLabel="备注" LabelAlign="Right" MaxLength="50"/>
                            </Items>
                             <Listeners>
                                <ValidityChange Handler="#{btnModifyKeySave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnModifyKeySave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="BtnModifyKeySave_Click">
                                    <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnModifyKeyCancel" runat="server" Text="取消" Icon="Cancel">
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
                <ext:Window ID="windowAddKey" runat="server" Icon="MonitorEdit" Closable="false" Title="添加台账项目"
                    Width="320" Height="220" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items> 
                       <ext:FormPanel ID="pnlAddKey" runat="server" Flex="1" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="txtAddKeyId" runat="server" FieldLabel="项目编号"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                                <ext:TextField ID="txtAddKeyName" runat="server" FieldLabel="项目名称"  LabelAlign="Right" AllowBlank="False" MaxLength="50" IndicatorText="*" IndicatorCls="red-text"/>
                                <ext:TextField ID="txtAddKeyCode" runat="server" FieldLabel="英文名称" LabelAlign="Right" MaxLength="50" />
                                    <ext:ComboBox ID="cbxAddValueType" runat="server" FieldLabel="赋值类型" LabelAlign="Right" AllowBlank="False"  OnDirectChange="cbxAddValueType_change" Editable="false" IndicatorText="*" IndicatorCls="red-text">
                                        <Items>
                                            <ext:ListItem Text="数字" Value="数字">
                                            </ext:ListItem>
                                            <ext:ListItem Text="文字" Value="文字">
                                            </ext:ListItem>
                                            <ext:ListItem Text="日期" Value="日期">
                                            </ext:ListItem>
                                        </Items>
                                    </ext:ComboBox>
                                    <ext:ComboBox ID="cbxAddCanDropDown" runat="server" FieldLabel="是否可下拉" LabelAlign="Right" AllowBlank="False" Editable="false" IndicatorText="*" IndicatorCls="red-text">
                                        <Items>
                                            <ext:ListItem Text="否" Value="0">
                                            </ext:ListItem>
                                            <ext:ListItem Text="是" Value="1">
                                            </ext:ListItem>
                                        </Items>
                                    </ext:ComboBox>
                                <ext:TextField ID="txtAddKeyRemark" runat="server" FieldLabel="备注" LabelAlign="Right" MaxLength="50"/>
                            </Items>
                             <Listeners>
                                <ValidityChange Handler="#{btnAddKeySave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnAddKeySave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="BtnAddKeySave_Click">
                                    <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnAddKeyCancel" runat="server" Text="取消" Icon="Cancel">
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
                <ext:Window ID="windowAddValue" runat="server" Icon="MonitorEdit" Closable="false" Title="添加项目可选值"
                    Width="320" Height="195" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items> 
                       <ext:FormPanel ID="pnlAddValue" runat="server" Flex="1" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="txtAddValueId" runat="server" FieldLabel="值编号"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                                <ext:TextField ID="txtAddValueKeyName" runat="server" FieldLabel="项目名称"  LabelAlign="Right" ReadOnly="true"/>
                                <ext:TextField ID="txtAddValueKeyValueType" runat="server" FieldLabel="赋值类型"  LabelAlign="Right" ReadOnly="true"/>
                                <ext:TextField ID="txtAddValueContent" runat="server" FieldLabel="值内容"  LabelAlign="Right" AllowBlank="False" MaxLength="50" IndicatorText="*" IndicatorCls="red-text"/>
                                <ext:TextField ID="txtAddValueRemark" runat="server" FieldLabel="备注" LabelAlign="Right" MaxLength="50"/>
                            </Items>
                             <Listeners>
                                <ValidityChange Handler="#{btnAddValueSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnAddValueSave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="BtnAddValueSave_Click">
                                    <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnAddValueCancel" runat="server" Text="取消" Icon="Cancel">
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
                <ext:Window ID="windowModifyValue" runat="server" Icon="MonitorEdit" Closable="false" Title="修改项目可选值"
                    Width="320" Height="195" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items> 
                       <ext:FormPanel ID="pnlModifyValue" runat="server" Flex="1" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="txtModifyValueId" runat="server" FieldLabel="值编号"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                                <ext:TextField ID="txtModifyValueKeyName" runat="server" FieldLabel="项目名称"  LabelAlign="Right" ReadOnly="true"/>
                                <ext:TextField ID="txtModifyValueKeyValueType" runat="server" FieldLabel="赋值类型"  LabelAlign="Right" ReadOnly="true"/>
                                <ext:TextField ID="txtModifyValueContent" runat="server" FieldLabel="值内容"  LabelAlign="Right" AllowBlank="False" MaxLength="50" IndicatorText="*" IndicatorCls="red-text"/>
                                <ext:TextField ID="txtModifyValueRemark" runat="server" FieldLabel="备注" LabelAlign="Right" MaxLength="50"/>
                            </Items>
                             <Listeners>
                                <ValidityChange Handler="#{btnModifyValueSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnModifyValueSave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="BtnModifyValueSave_Click">
                                    <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnModifyValueCancel" runat="server" Text="取消" Icon="Cancel">
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
                <ext:Hidden ID="txtHiddenKeyId" runat="server"></ext:Hidden>
                <ext:Hidden ID="txtHiddenKeyName" runat="server"></ext:Hidden>
                <ext:Hidden ID="txtHiddenKeyCode" runat="server"></ext:Hidden>
                <ext:Hidden ID="txtHiddenKeyValue" runat="server"></ext:Hidden>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
