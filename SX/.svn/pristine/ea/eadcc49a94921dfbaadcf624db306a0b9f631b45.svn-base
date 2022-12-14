<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysProblemRecord.aspx.cs" Inherits="Manager_System_SysProblemRecord_SysProblemRecord" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>问题记录表</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell
        {
        	background-color: #B0FFBA !important;
        }
        .x-grid-row-important .x-grid-cell
        {
        	background-color: #E3170D !important;
        }
    </style>
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

        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("DeleteFlag") == "1") {
                return "x-grid-row-collapsed";
            } else {
                var myDate = new Date();
                var now = myDate.getTime();
                var dealDate = Date.parse(record.get("DealDate"));
                if ((now - dealDate) > 172800000) {
                    return "x-grid-row-important";
                }
            }
        }

        //区分删除操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
            }
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定需要关闭此条信息？', function (btn) { commandcolumn_direct_delete(btn, record) });
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
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        var cellDblClick = function (grid, td, tdindex, record, tr, trindex, e, fn) {
            App.direct.commandcolumn_direct_detail(record.get("ObjID"), {
                success: function (result) {
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }
        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
            if (record.get("CreateUser") != App.hidden_txt_create_usename.getValue()) {
                toolbar.items.getAt(2).hide();
                toolbar.items.getAt(3).hide();
            }
        }
    </script>
      <script type="text/javascript">
          //-------人员绑定-----查询带回弹出框--BEGIN
          var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {//人员绑定
              var userType = App.hidden_user_type.value;
              if (!App.winModify.hidden) {
                  switch (userType) {
                      case "1":
                          App.modify_create_user.setValue(record.data.UserName);
                          App.hidden_modify_create_user.setValue(record.data.WorkBarcode);
                          break;
                      case "2":
                          App.modify_deal_user.setValue(record.data.UserName);
                          App.hidden_modify_deal_user.setValue(record.data.WorkBarcode);
                          break;
                      case "3":
                          App.modify_validate_user.setValue(record.data.UserName);
                          App.hidden_modify_validate_user.setValue(record.data.WorkBarcode);
                          break;
                      default:
                  }
              }
              if (!App.winAdd.hidden) {
                  switch (userType) {
                      case "1":
                          App.add_create_user.setValue(record.data.UserName);
                          App.hidden_add_create_user.setValue(record.data.WorkBarcode);
                          break;
                      case "2":
                          App.add_deal_user.setValue(record.data.UserName);
                          App.hidden_add_deal_user.setValue(record.data.WorkBarcode);
                          break;
                      case "3":
                          App.add_validate_user.setValue(record.data.UserName);
                          App.hidden_add_validate_user.setValue(record.data.WorkBarcode);
                          break;
                      default:
                  }
              }
              if (App.winModify.hidden && App.winAdd.hidden) {
                  switch (userType) {
                      case "1":
                          App.txt_create_user.setValue(record.data.UserName);
                          App.hidden_txt_create_user.setValue(record.data.WorkBarcode);
                          break;
                      case "2":
                          App.txt_deal_user.setValue(record.data.UserName);
                          App.hidden_txt_deal_user.setValue(record.data.WorkBarcode);
                          break;
                      case "3":
                          App.txt_validate_user.setValue(record.data.UserName);
                          App.hidden_txt_validate_user.setValue(record.data.WorkBarcode);
                          break;
                      default:

                  }
              }
          }

          var SelectUserCode = function (field, trigger, index, userType, hiddenId) {//人员绑定查询
              switch (index) {
                  case 0:
                      field.setValue('');
                      document.getElementById(hiddenId).value = '';
                      break;
                  case 1:
                      App.hidden_user_type.setValue(userType);
                      App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();
                      break;
              }
          }

          Ext.create("Ext.window.Window", {//人员绑定查询带回窗体
              id: "Manager_BasicInfo_CommonPage_QueryBasUser_Window",
              height: 460,
              hidden: true,
              width: 360,
              html: "<iframe src='../../BasicInfo/CommonPage/QueryBasUser.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
              bodyStyle: "background-color: #fff;",
              closable: true,
              title: "请选择人员",
              modal: true
          })
          //------------查询带回弹出框--END 
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
                                                <ext:TriggerField ID="txt_create_user" runat="server" FieldLabel="发起人" LabelAlign="Right" Editable="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="SelectUserCode(this, trigger, index , 1 , 'hidden_txt_create_user')" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:ComboBox ID="txt_create_dept" runat="server" FieldLabel="发起部门" LabelAlign="Right" Editable="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <Select Handler="this.getTrigger(0).show();" />
                                                        <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:DateField ID="txt_start_create_date" runat="server" FieldLabel="开始发起时间" LabelAlign="Right" Editable="false" />
                                                <ext:DateField ID="txt_end_create_date" runat="server" FieldLabel="结束发起时间" LabelAlign="Right" Editable="false"  />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TriggerField ID="txt_deal_user"  runat="server" FieldLabel="处理人" LabelAlign="Right" Editable="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="SelectUserCode(this, trigger, index , 2 , 'hidden_txt_deal_user')" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:ComboBox ID="txt_deal_dept"  runat="server" FieldLabel="处理部门" LabelAlign="Right" Editable="false" >
                                                     <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <Select Handler="this.getTrigger(0).show();" />
                                                        <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:DateField ID="txt_start_deal_date" runat="server" FieldLabel="开始处理时间" LabelAlign="Right" Editable="false"  />
                                                <ext:DateField ID="txt_end_deal_date" runat="server" FieldLabel="结束处理时间" LabelAlign="Right" Editable="false"  />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TriggerField ID="txt_validate_user"  runat="server" FieldLabel="验证人" LabelAlign="Right" Editable="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="SelectUserCode(this, trigger, index , 3 , 'hidden_txt_validate_user')" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:ComboBox ID="txt_validate_dept"  runat="server" FieldLabel="验证部门" LabelAlign="Right" Editable="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <Select Handler="this.getTrigger(0).show();" />
                                                        <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:DateField ID="txt_start_validate_date" runat="server" FieldLabel="开始验证时间" LabelAlign="Right" Editable="false"  />
                                                <ext:DateField ID="txt_end_validate_date" runat="server" FieldLabel="结束验证时间" LabelAlign="Right" Editable="false"  />
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
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" AutoDataBind="false" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="CreateDate" />
                                        <ext:ModelField Name="CreateDept"  />
                                        <ext:ModelField Name="CreateUser"  />
                                        <ext:ModelField Name="ProblemDesc"  />
                                        <ext:ModelField Name="ProblemType"  />
                                        <ext:ModelField Name="ErrorLevel"  />
                                        <ext:ModelField Name="ProblemReason"  />
                                        <ext:ModelField Name="Solution"  />
                                        <ext:ModelField Name="DealResult"  />
                                        <ext:ModelField Name="DealDept"  />
                                        <ext:ModelField Name="DealUser"  />
                                        <ext:ModelField Name="DealDate"  />
                                        <ext:ModelField Name="ValidateUser"  />
                                        <ext:ModelField Name="ValidateDept"  />
                                        <ext:ModelField Name="ValidateDate"  />
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
                            <ext:DateColumn ID="create_date" Format="yyyy-MM-dd" runat="server" Text="发起时间" DataIndex="CreateDate" Width="80"  />
                            <ext:Column ID="create_dept" runat="server" Text="发起部门" DataIndex="CreateDept" Width="80"  />
                            <ext:Column ID="create_user" runat="server" Text="发起人" DataIndex="CreateUser" Width="80"  />
                            <ext:Column ID="problem_desc" runat="server" Text="问题描述" DataIndex="ProblemDesc" Width="80"  />
                            <ext:Column ID="problem_type" runat="server" Text="问题类别" DataIndex="ProblemType" Width="80"  />
                            <ext:Column ID="error_level" runat="server" Text="严重等级" DataIndex="ErrorLevel" Width="80"  />
                            <ext:Column ID="problem_reason" runat="server" Text="问题原因" DataIndex="ProblemReason" Width="80"  />
                            <ext:Column ID="solution" runat="server" Text="解决方案" DataIndex="Solution" Width="80"  />
                            <ext:Column ID="deal_result" runat="server" Text="处理结果" DataIndex="DealResult" Width="80" />
                            <ext:Column ID="deal_dept" runat="server" Text="处理部门" DataIndex="DealDept" Width="80"  />
                            <ext:Column ID="deal_user" runat="server" Text="处理人" DataIndex="DealUser" Width="80" />
                            <ext:DateColumn ID="deal_date" Format="yyyy-MM-dd" runat="server" Text="处理时间" DataIndex="DealDate" Width="80"  />
                            <ext:Column ID="validate_user" runat="server" Text="验证人" DataIndex="ValidateUser" Width="80" />
                            <ext:Column ID="validate_dept" runat="server" Text="验证部门" DataIndex="ValidateDept" Width="80"  />
                            <ext:DateColumn ID="validate_date" Format="yyyy-MM-dd" runat="server" Text="验证时间" DataIndex="ValidateDate" Width="80"  />
                            <ext:Column ID="delete_flag" runat="server" Text="关闭标志" DataIndex="DeleteFlag" Width="80"  Hidden="true"   />
                            <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Width="80"  />
                            <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                        <ToolTip Text="修改本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Delete" CommandName="Delete" Text="关闭">
                                        <ToolTip Text="关闭本条数据" />
                                    </ext:GridCommand> 
                                    <ext:CommandSeparator />
                                </Commands>
                                <PrepareToolbar Fn="prepareToolbar" />
                                <Listeners>
                                    <Command Handler="return commandcolumn_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <Listeners>
                        <CellDblClick Fn="cellDblClick" />
                    </Listeners>
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
                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改问题记录信息"
                    Width="900" Height="320" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                        <ext:Container ID="Container2"  runat="server" Layout="FormLayout" ColumnWidth=".3"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField  ID="modify_obj_id" runat="server" Hidden="true" />
                                                <ext:DateField      ID="modify_create_date"    runat="server" FieldLabel="发起时间"   LabelAlign="Right"   AllowBlank="false" Editable="false" />
                                                <ext:ComboBox       ID="modify_create_dept"    runat="server" FieldLabel="发起部门"   LabelAlign="Right"   AllowBlank="false" Editable="false"  />
                                                <ext:TriggerField   ID="modify_create_user"    runat="server" FieldLabel="发起人"     LabelAlign="Right"   AllowBlank="false" Editable="false"  >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="SelectUserCode(this, trigger, index , 1 , 'hidden_modify_validate_user')" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:ComboBox       ID="modify_problem_type"   runat="server"   FieldLabel="问题类型"   LabelAlign="Right"  AllowBlank="false" Editable="false" />
                                                <ext:ComboBox       ID="modify_error_level"    runat="server"   FieldLabel="问题等级"   LabelAlign="Right"  AllowBlank="false" Editable="false" />
                                                <ext:TextArea       ID="modify_remark"         runat="server"   FieldLabel="备注"   LabelAlign="Right" Editable="false" Height="70" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container3"  runat="server" Layout="FormLayout" ColumnWidth=".3"
                                            Padding="5">
                                            <Items>
                                                 <ext:DateField     ID="modify_deal_date"  runat="server"   FieldLabel="处理时间"   LabelAlign="Right" Editable="false" />
                                                <ext:ComboBox       ID="modify_deal_dept"  runat="server"   FieldLabel="处理部门"   LabelAlign="Right" Editable="false" />
                                                <ext:TriggerField   ID="modify_deal_user"  runat="server"   FieldLabel="处理人"     LabelAlign="Right" Editable="false"   >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="SelectUserCode(this, trigger, index , 2 , 'hidden_modify_deal_user')" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextArea       ID="modify_problem_desc"  runat="server"   FieldLabel="问题描述"   LabelAlign="Right"  AllowBlank="false" Editable="false" />
                                                <ext:TextArea       ID="modify_problem_reason" runat="server"   FieldLabel="问题原因"   LabelAlign="Right" Editable="false" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container8"  runat="server" Layout="FormLayout" ColumnWidth=".3"
                                            Padding="5">
                                            <Items>
                                                 <ext:DateField     ID="modify_validate_date"      runat="server"   FieldLabel="验证时间"   LabelAlign="Right" Editable="false" />
                                                <ext:ComboBox       ID="modify_validate_dept"      runat="server"   FieldLabel="验证部门"   LabelAlign="Right" Editable="false" />
                                                <ext:TriggerField   ID="modify_validate_user"      runat="server"   FieldLabel="验证人"     LabelAlign="Right" Editable="false"   >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="SelectUserCode(this, trigger, index , 3 , 'hidden_modify_validate_user')" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextArea       ID="modify_solution"       runat="server"   FieldLabel="解决方案"   LabelAlign="Right" Editable="false" />
                                                <ext:TextArea       ID="modify_deal_result"    runat="server"   FieldLabel="处理结果"   LabelAlign="Right" Editable="false" />
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
                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加问题记录信息"
                    Width="900" Height="320" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                        <ext:Container ID="Container5"  runat="server" Layout="FormLayout" ColumnWidth=".3"
                                            Padding="5">
                                            <Items>
                                                <ext:DateField      ID="add_create_date"    runat="server" FieldLabel="发起时间"   LabelAlign="Right"   AllowBlank="false" Editable="false" />
                                                <ext:ComboBox       ID="add_create_dept"    runat="server" FieldLabel="发起部门"   LabelAlign="Right"   AllowBlank="false" Editable="false"  />
                                                <ext:TriggerField   ID="add_create_user"    runat="server" FieldLabel="发起人"     LabelAlign="Right"   AllowBlank="false" Editable="false"  >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="SelectUserCode(this, trigger, index , 1 , 'hidden_add_validate_user')" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:ComboBox       ID="add_problem_type"   runat="server"   FieldLabel="问题类型"   LabelAlign="Right"  AllowBlank="false" Editable="false" />
                                                <ext:ComboBox       ID="add_error_level"    runat="server"   FieldLabel="问题等级"   LabelAlign="Right"  AllowBlank="false" Editable="false" />
                                                <ext:TextArea       ID="add_remark"         runat="server"   FieldLabel="备注"   LabelAlign="Right"  Editable="false" Height="70" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container6"  runat="server" Layout="FormLayout" ColumnWidth=".3"
                                            Padding="5">
                                            <Items>
                                                 <ext:DateField     ID="add_deal_date"  runat="server"   FieldLabel="处理时间"   LabelAlign="Right" Editable="false" />
                                                <ext:ComboBox       ID="add_deal_dept"  runat="server"   FieldLabel="处理部门"   LabelAlign="Right" Editable="false" />
                                                <ext:TriggerField   ID="add_deal_user"  runat="server"   FieldLabel="处理人"     LabelAlign="Right" Editable="false"   >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="SelectUserCode(this, trigger, index , 2 , 'hidden_add_deal_user')" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextArea       ID="add_problem_desc"  runat="server"   FieldLabel="问题描述"   LabelAlign="Right"  AllowBlank="false" Editable="false" />
                                                <ext:TextArea       ID="add_problem_reason" runat="server"   FieldLabel="问题原因"   LabelAlign="Right" Editable="false" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container7"  runat="server" Layout="FormLayout" ColumnWidth=".3"
                                            Padding="5">
                                            <Items>
                                                 <ext:DateField     ID="add_validate_date"      runat="server"   FieldLabel="验证时间"   LabelAlign="Right"  Editable="false" />
                                                <ext:ComboBox       ID="add_validate_dept"      runat="server"   FieldLabel="验证部门"   LabelAlign="Right"  Editable="false" />
                                                <ext:TriggerField   ID="add_validate_user"      runat="server"   FieldLabel="验证人"     LabelAlign="Right"  Editable="false"   >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="SelectUserCode(this, trigger, index , 3 , 'hidden_add_validate_user')" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextArea       ID="add_solution"       runat="server"   FieldLabel="解决方案"   LabelAlign="Right" Editable="false" />
                                                <ext:TextArea       ID="add_deal_result"    runat="server"   FieldLabel="处理结果"   LabelAlign="Right" Editable="false" />
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
                <ext:Window ID="winDetail" runat="server" Icon="MonitorEdit" Title="问题记录信息详细"
                    Width="900" Height="320" Resizable="false" Closable="true" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items> 
                        <ext:FormPanel ID="FormPanel1" runat="server" Flex="1" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Container ID="Container9" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="Container10"  runat="server" Layout="FormLayout" ColumnWidth=".3"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField      ID="detail_create_date"    runat="server" FieldLabel="发起时间"   LabelAlign="Right" />
                                                <ext:TextField      ID="detail_create_dept"    runat="server" FieldLabel="发起部门"   LabelAlign="Right" />
                                                <ext:TextField      ID="detail_create_user"    runat="server" FieldLabel="发起人"     LabelAlign="Right" />
                                                <ext:TextField      ID="detail_problem_type"   runat="server"   FieldLabel="问题类型"   LabelAlign="Right"      />
                                                <ext:TextField      ID="detail_problem_level"    runat="server"   FieldLabel="问题等级"   LabelAlign="Right"    />
                                                <ext:TextArea       ID="detail_remark"         runat="server"   FieldLabel="备注"   LabelAlign="Right" Height="70" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container11"  runat="server" Layout="FormLayout" ColumnWidth=".3"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField  ID="detail_deal_date"  runat="server"   FieldLabel="处理时间"   LabelAlign="Right"    />
                                                <ext:TextField  ID="detail_deal_dept"  runat="server"   FieldLabel="处理部门"   LabelAlign="Right"  />
                                                <ext:TextField  ID="detail_deal_user"  runat="server"   FieldLabel="处理人"     LabelAlign="Right"  />
                                                <ext:TextArea   ID="detail_problem_desc"  runat="server"   FieldLabel="问题描述"   LabelAlign="Right"   />
                                                <ext:TextArea   ID="detail_problem_reason" runat="server"   FieldLabel="问题原因"   LabelAlign="Right"    />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container12"  runat="server" Layout="FormLayout" ColumnWidth=".3"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField  ID="detail_validate_date"      runat="server"   FieldLabel="验证时间"   LabelAlign="Right"   />
                                                <ext:TextField  ID="detail_validate_dept"      runat="server"   FieldLabel="验证部门"   LabelAlign="Right"   />
                                                <ext:TextField  ID="detail_validate_user"      runat="server"   FieldLabel="验证人"     LabelAlign="Right"  />
                                                <ext:TextArea   ID="detail_solution"       runat="server"   FieldLabel="解决方案"   LabelAlign="Right"  />
                                                <ext:TextArea   ID="detail_deal_result"    runat="server"   FieldLabel="处理结果"   LabelAlign="Right"  />
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                </ext:Window>
                <ext:Hidden ID="hidden_user_type" runat="server" />
                <ext:Hidden ID="hidden_txt_create_user" runat="server" />
                <ext:Hidden ID="hidden_txt_create_usename" runat="server" />
                <ext:Hidden ID="hidden_txt_deal_user" runat="server" />
                <ext:Hidden ID="hidden_txt_validate_user" runat="server" />
                
                <ext:Hidden ID="hidden_add_create_user" runat="server" />
                <ext:Hidden ID="hidden_add_deal_user" runat="server" />
                <ext:Hidden ID="hidden_add_validate_user" runat="server" />
                
                <ext:Hidden ID="hidden_modify_create_user" runat="server" />
                <ext:Hidden ID="hidden_modify_deal_user" runat="server" />
                <ext:Hidden ID="hidden_modify_validate_user" runat="server" />
            </Items>
        </ext:Viewport>
        </form>
</body>
</html>
