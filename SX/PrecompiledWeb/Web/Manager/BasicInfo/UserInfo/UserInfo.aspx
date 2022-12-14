﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_BasicInfo_UserInfo_UserInfo, App_Web_iatpywgy" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人员基础信息</title>
        <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
   
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
       //-------部门-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryDepartment_Request = function (record) {//部门返回值处理
            if (!App.winAdd.hidden) {
                App.add_dept_code.setValue(record.data.DepName);
                App.hidden_depart_code.setValue(record.data.DepCode);
            }
            else if (!App.winModify.hidden) {
                App.modify_dept_code.setValue(record.data.DepName);
                App.hidden_depart_code.setValue(record.data.DepCode);
            }
            else {
                App.txt_depart_code.getTrigger(0).show();
                App.txt_depart_code.setValue(record.data.DepName);
                App.hidden_select_depart_code.setValue(record.data.DepCode);
            }
        }

        var SelectDepartment = function (field, trigger, index) {//部门查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hidden_select_depart_code.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryDepartment_Window.show();
                    break;
            }
        }

        var UpdateDepartment = function (field, trigger, index) {//部门查询
            App.Manager_BasicInfo_CommonPage_QueryDepartment_Window.show();
        }

         Ext.create("Ext.window.Window", {//部门查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryDepartment_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryDepartment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            layout: 'fit',
            title: "请选择部门",
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">
        //-------岗位-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryWorkPosition_Request = function (record) {//部门返回值处理
            if (!App.winAdd.hidden) {
                App.add_work_id.setValue(record.data.WorkName);
            }
            else if (!App.winModify.hidden) {
                App.modify_work_id.setValue(record.data.WorkName);
            }
            else {
                App.txt_work_id.setValue(record.data.WorkName);
            }
            App.hidden_work_id.setValue(record.data.ObjID);
        }

        var SelectWorkPosition = function () {//岗位查询
            App.Manager_BasicInfo_CommonPage_QueryWorkPosition_Window.show();
        }

        Ext.create("Ext.window.Window", {//岗位查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryWorkPosition_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryWorkPosition.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            layout: 'fit',
            title: "请选择岗位",
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
     <script type="text/javascript">
         //-------班组-----查询带回弹出框--BEGIN
         var Manager_BasicInfo_CommonPage_QueryShiftClass_Request = function (record) {//返回值处理
             if (!App.winAdd.hidden) {
                 App.add_shift_id.setValue(record.data.ClassName);
             }
             else if (!App.winModify.hidden) {
                 App.modify_shift_id.setValue(record.data.ClassName);
             }
             else {
                 App.txt_shift_id.setValue(record.data.ClassName);
             }
             App.hidden_shift_id.setValue(record.data.ObjID);
         }

         var SelectShiftClass = function () {//班组查询
             App.Manager_BasicInfo_CommonPage_QueryShiftClass_Window.show();
         }

         Ext.create("Ext.window.Window", {//班组查询带回窗体
             id: "Manager_BasicInfo_CommonPage_QueryShiftClass_Window",
             height: 460,
             hidden: true,
             width: 360,
             html: "<iframe src='../../BasicInfo/CommonPage/QueryShiftClass.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
             bodyStyle: "background-color: #fff;",
             closable: true,
             layout: 'fit',
             title: "请选择班组",
             modal: true
         })
         //------------查询带回弹出框--END 
    </script>
     <script type="text/javascript">
         //-------车间-----查询带回弹出框--BEGIN
         var Manager_BasicInfo_CommonPage_QueryWorkShop_Request = function (record) {//部门返回值处理
             if (!App.winAdd.hidden) {
                 App.add_workshop_id.setValue(record.data.WorkShopName);
             }
             else if (!App.winModify.hidden) {
                 App.modify_workshop_id.setValue(record.data.WorkShopName);
             }
             else {
                 App.txt_workshop_id.setValue(record.data.WorkShopName);
             }
             App.hidden_workshop_id.setValue(record.data.ObjID);
         }

         var SelectWorkShop = function () {//车间查询
             App.Manager_BasicInfo_CommonPage_QueryWorkShop_Window.show();
         }
       
         Ext.create("Ext.window.Window", {//车间查询带回窗体
             id: "Manager_BasicInfo_CommonPage_QueryWorkShop_Window",
             height: 460,
             hidden: true,
             width: 360,
             html: "<iframe src='../../BasicInfo/CommonPage/QueryWorkShop.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
             bodyStyle: "background-color: #fff;",
             closable: true,
             layout: 'fit',
             title: "请选择车间",
             modal: true
         })
         //------------查询带回弹出框--END 
    </script>
    <script type="text/javascript">

        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
      
            var WorkBarcode = record.data.WorkBarcode;
            App.modify_dept_code.setValue(record.data.DeptCode);
            App.modify_work_id.setValue(record.data.WorkID);
            App.modify_shift_id.setValue(record.data.ShiftID);
            //            App.UploadImage.setValue('');


            App.direct.commandcolumn_direct_edit(WorkBarcode, {
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
            var WorkBarcode = record.data.WorkBarcode;
            App.direct.commandcolumn_direct_delete(WorkBarcode, {
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
                             if(/^[\d]+$/.test(val))
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
            integerText:"此填入项格式为正整数"
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
        <asp:Button ID="Button2" Style="display:none"  runat="server" Text="Button" OnClientClick="pnlListFresh" />
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
                                        <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_user_name" runat="server" FieldLabel="用户名称" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                          <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="Tgw" runat="server" FieldLabel="岗位名称" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_3" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                            Padding="5">
                                            <Items>
                                                <ext:TriggerField ID="txt_depart_code" runat="server" FieldLabel="所属部门" LabelAlign="Right" Editable="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search"/>
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectDepartment" />
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
                                        <ext:ModelField Name="UserName" />
                                        <ext:ModelField Name="RealName"  />
                                        <ext:ModelField Name="Sex"  />
                                        <ext:ModelField Name="Telephone"  />
                                        <ext:ModelField Name="WorkBarcode"  />
                                        <ext:ModelField Name="DeptCode"  />
                                        <ext:ModelField Name="WorkID"  />
                                        <ext:ModelField Name="ShiftID"  />
                                        <ext:ModelField Name="WorkShopID"  />
                                        <ext:ModelField Name="HRCode"  />
                                        <ext:ModelField Name="ERPCode"  />
                                        <ext:ModelField Name="DeleteFlag"  />
                                        <ext:ModelField Name="Remark"  />
                                        <ext:ModelField Name="WorkA"  />
                                        <ext:ModelField Name="WorkB"  />
                                        <ext:ModelField Name="WorkC"  />
                                        <ext:ModelField Name="WorkD"  />
                                        <ext:ModelField Name="WorkE"  />
                                        <ext:ModelField Name="WorkF"  />
                                        <ext:ModelField Name="A"  />
                                        <ext:ModelField Name="B"  />
                                        <ext:ModelField Name="C"  />
                                        <ext:ModelField Name="D"  />
                                        <ext:ModelField Name="E"  />
                                        <ext:ModelField Name="F"  />
                                        <ext:ModelField Name="A_fen"  />
                                        <ext:ModelField Name="B_fen"  />
                                        <ext:ModelField Name="C_fen"  />
                                        <ext:ModelField Name="D_fen"  />
                                        <ext:ModelField Name="E_fen"  />
                                        <ext:ModelField Name="F_fen"  />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" Width="45" runat="server" />
                            <ext:Column ID="work_barcode" runat="server" Text="用户工号" DataIndex="WorkBarcode" Width="80"  />
                            <ext:Column ID="obj_id" runat="server" Text="用户编号" DataIndex="ObjID" Visible="False" Width="80"  />
                            <ext:Column ID="user_name" runat="server" Text="用户名称" DataIndex="UserName" Width="80"  />
                            <ext:Column ID="real_name" runat="server" Text="真实姓名" DataIndex="RealName" Width="80"  />
<%--                            <ext:Column ID="sex" runat="server" Text="性别" DataIndex="Sex" Width="80"  />--%>
                            <ext:Column ID="telephone" runat="server" Text="手机号码" DataIndex="Telephone" Width="80"  />
                            <ext:Column ID="depart_code" runat="server" Text="所属部门" DataIndex="DeptCode" Width="80"  />
                            <ext:Column ID="work_id" runat="server" Text="所属岗位" DataIndex="WorkID" Width="80"  />
                            <ext:Column ID="shift_id" runat="server" Text="所属班组" DataIndex="ShiftID" Width="80"  />
<%--                            <ext:Column ID="hr_code" runat="server" Text="HR代码" DataIndex="HRCode" Width="80" />--%>
<%--                            <ext:Column ID="erp_code" runat="server" Text="ERP代码" DataIndex="ERPCode" Width="80"  />--%>
                            <ext:Column ID="delete_flag" runat="server" Text="删除标志" DataIndex="DeleteFlag" Width="80" Hidden="true"  />
                            <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Width="80"  />
                             <ext:Column ID="Column1" runat="server" Text="岗位A" DataIndex="WorkA" Width="80"  />
                            <ext:Column ID="Column3" runat="server" Text="A岗程度" DataIndex="A" Width="80"  />
                              <ext:Column ID="Column2" runat="server" Text="A岗分数" DataIndex="A_fen" Width="80"  />
                                            <ext:Column ID="Column4" runat="server" Text="岗位B" DataIndex="WorkB" Width="80"  />
                            <ext:Column ID="Column5" runat="server" Text="B岗程度" DataIndex="B" Width="80"  />
                              <ext:Column ID="Column6" runat="server" Text="B岗分数" DataIndex="B_fen" Width="80"  />
                                <ext:Column ID="Column7" runat="server" Text="岗位C" DataIndex="WorkC" Width="80"  />
                            <ext:Column ID="Column8" runat="server" Text="C岗程度" DataIndex="C" Width="80"  />
                              <ext:Column ID="Column9" runat="server" Text="C岗分数" DataIndex="C_fen" Width="80"  />
                                <ext:Column ID="Column10" runat="server" Text="岗位D" DataIndex="WorkD" Width="80"  />
                            <ext:Column ID="Column11" runat="server" Text="D岗程度" DataIndex="D" Width="80"  />
                              <ext:Column ID="Column12" runat="server" Text="D岗分数" DataIndex="D_fen" Width="80"  />
                                 <ext:Column ID="Column13" runat="server" Text="岗位E" DataIndex="WorkE" Width="80"  />
                            <ext:Column ID="Column14" runat="server" Text="E岗程度" DataIndex="E" Width="80"  />
                              <ext:Column ID="Column15" runat="server" Text="E岗分数" DataIndex="E_fen" Width="80"  />
                                 <ext:Column ID="Column16" runat="server" Text="岗位F" DataIndex="WorkF" Width="80"  />
                            <ext:Column ID="Column17" runat="server" Text="F岗程度" DataIndex="F" Width="80"  />
                              <ext:Column ID="Column18" runat="server" Text="F岗分数" DataIndex="F_fen" Width="80"  />
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
                          <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改用户基础信息"
                    Width="800" Height="550" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                <ext:Container ID="Container2" runat="server" Layout="HBoxLayout" AutoHeight="true">
                                    <Items>
                                       <ext:CheckboxGroup ID="CheckboxGroup2" runat="server" ColumnsNumber="3" Flex="1" AnchorHorizontal="true">
                                            <Items>
                                                <ext:TextField ID="modify_obj_id" runat="server" FieldLabel="用户编号"   LabelAlign="Left" ReadOnly=true Enabled="true" />
                                                <ext:TextField ID="modify_work_barcode" runat="server" FieldLabel="用户工号"   LabelAlign="Left" AllowBlank="false" ReadOnly=true Enabled="true">
                                                </ext:TextField>
                                                <ext:TextField ID="modify_user_name" runat="server" FieldLabel="用户名称"   LabelAlign="Left" Enabled="true" AllowBlank="false" Editable="false" MaxLength="20" IndicatorText="*" IndicatorCls="red-text" >
                                                   
                                                </ext:TextField>
                                                <ext:TextField ID="modify_real_name" runat="server" FieldLabel="真实姓名"   LabelAlign="Left" Enabled="true" />
                                                <ext:TextField ID="modify_telephone" runat="server" FieldLabel="手机号码" Vtype="integer"  LabelAlign="Left" Enabled="true" MaxLength="11" />
                                        
                                                
                                                <ext:TriggerField ID="modify_dept_code" runat="server" FieldLabel="所属部门" LabelAlign="Left" Editable="false"   >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="UpdateDepartment" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TriggerField ID="modify_work_id" runat="server" FieldLabel="所属岗位" LabelAlign="Left" Editable="false"  >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectWorkPosition" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TriggerField ID="modify_shift_id" runat="server" FieldLabel="所属班组" LabelAlign="Left"  Editable="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectShiftClass" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="modify_remark" runat="server" FieldLabel="备注"   LabelAlign="Left" Enabled="true" MaxLength="100"/>
                                
                                                  <ext:ComboBox LabelAlign="left" ID="workA" runat="server" FieldLabel="A岗位" >
                                            
                                        </ext:ComboBox> 
                                        <ext:ComboBox LabelAlign="left" ID="A" runat="server" FieldLabel="A岗程度" >
                                         <Items>
                                             <ext:ListItem Text="红"  Value ="0" />
                                               <ext:ListItem Text="绿"  Value ="1" />
                                                 <ext:ListItem Text="黄"  Value ="2" />
                                              </Items>
                                        </ext:ComboBox> 
                                                 <ext:TextField ID="A_fen" runat="server" Flex="1" FieldLabel="A岗分数" Vtype="integer"
                                                        LabelAlign="Right" Enabled="true"  />

                                                          <ext:ComboBox LabelAlign="left" ID="workB" runat="server" FieldLabel="B岗位" >
                                            
                                        </ext:ComboBox> 
                                        <ext:ComboBox LabelAlign="left" ID="B" runat="server" FieldLabel="B岗程度" >
                                         <Items>
                                             <ext:ListItem Text="红"  Value ="0" />
                                               <ext:ListItem Text="绿"  Value ="1" />
                                                 <ext:ListItem Text="黄"  Value ="2" />
                                              </Items>
                                        </ext:ComboBox> 
                                                 <ext:TextField ID="B_fen" runat="server" Flex="1" FieldLabel="B岗分数" Vtype="integer"
                                                        LabelAlign="Right" Enabled="true"  />
                                       
                                       
                                        <ext:ComboBox LabelAlign="left" ID="workC" runat="server" FieldLabel="C岗位" >
                                            
                                        </ext:ComboBox> 
                                        <ext:ComboBox LabelAlign="left" ID="C" runat="server" FieldLabel="C岗程度" >
                                         <Items>
                                             <ext:ListItem Text="红"  Value ="0" />
                                               <ext:ListItem Text="绿"  Value ="1" />
                                                 <ext:ListItem Text="黄"  Value ="2" />
                                              </Items>
                                        </ext:ComboBox> 
                                                 <ext:TextField ID="C_fen" runat="server" Flex="1" FieldLabel="C岗分数" Vtype="integer"
                                                        LabelAlign="Right" Enabled="true"  />
                                       
                                        <ext:ComboBox LabelAlign="left" ID="workD" runat="server" FieldLabel="D岗位" >
                                            
                                        </ext:ComboBox> 
                                        <ext:ComboBox LabelAlign="left" ID="D" runat="server" FieldLabel="D岗程度" >
                                         <Items>
                                             <ext:ListItem Text="红"  Value ="0" />
                                               <ext:ListItem Text="绿"  Value ="1" />
                                                 <ext:ListItem Text="黄"  Value ="2" />
                                              </Items>
                                        </ext:ComboBox> 
                                                 <ext:TextField ID="D_fen" runat="server" Flex="1" FieldLabel="D岗分数" Vtype="integer"
                                                        LabelAlign="Right" Enabled="true"  />

                                                        <ext:ComboBox LabelAlign="left" ID="workE" runat="server" FieldLabel="E岗位" >
                                            
                                        </ext:ComboBox> 
                                        <ext:ComboBox LabelAlign="left" ID="E" runat="server" FieldLabel="E岗程度" >
                                         <Items>
                                             <ext:ListItem Text="红"  Value ="0" />
                                               <ext:ListItem Text="绿"  Value ="1" />
                                                 <ext:ListItem Text="黄"  Value ="2" />
                                              </Items>
                                        </ext:ComboBox> 
                                                 <ext:TextField ID="E_fen" runat="server" Flex="1" FieldLabel="E岗分数" Vtype="integer"
                                                        LabelAlign="Right" Enabled="true"  />

                                                        <ext:ComboBox LabelAlign="left" ID="workF" runat="server" FieldLabel="F岗位" >
                                            
                                        </ext:ComboBox> 
                                        <ext:ComboBox LabelAlign="left" ID="F" runat="server" FieldLabel="F岗程度" >
                                         <Items>
                                             <ext:ListItem Text="红"  Value ="0" />
                                               <ext:ListItem Text="绿"  Value ="1" />
                                                 <ext:ListItem Text="黄"  Value ="2" />
                                              </Items>
                                        </ext:ComboBox> 
                                                 <ext:TextField ID="F_fen" runat="server" Flex="1" FieldLabel="F岗分数" Vtype="integer"
                                                        LabelAlign="Right" Enabled="true"  />
                                                         <ext:TextField ID="hidden_work_id" runat="server" FieldLabel="岗位编号"   LabelAlign="Left" Enabled="true"  ReadOnly="true" />      
                                                         <ext:TextField ID="hidden_shift_id" runat="server" FieldLabel="班组编号"   LabelAlign="Left" Enabled="true"  ReadOnly="true" />  
                                                         <ext:TextField ID="hidden_depart_code" runat="server" FieldLabel="部门编号"   LabelAlign="Left" Enabled="true"  ReadOnly="true" />  
                                                        <ext:Image runat="server" ID="Im"   Height ="150"  Width="150" />
                                    <ext:FileUploadField  runat="server" ID="UploadImage"   ButtonText="上传" Text="上传照片"  />
                                      
                                          </Items>
                                        </ext:CheckboxGroup>
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


                   






                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加用户基础信息"
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
                                <ext:Container runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items> 
                                        <ext:Container  runat="server" Layout="FormLayout" ColumnWidth=".5"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="add_user_name" runat="server" FieldLabel="用户名称"   LabelAlign="Left" Enabled="true" MaxLength="20" AllowBlank="false"  IndicatorText="*" IndicatorCls="red-text"  />
                                                <ext:ComboBox ID="add_sex" runat="server" FieldLabel="性别"   LabelAlign="Left" Enabled="true"   IndicatorText="*" IndicatorCls="red-text" hidden="true"/>
                                                <ext:TextField ID="add_telephone" runat="server" FieldLabel="手机号码"  Vtype="integer"  LabelAlign="Left" Enabled="true" MaxLength="11" /> 
                                                <ext:TriggerField ID="add_dept_code" runat="server" FieldLabel="所属部门" LabelAlign="Left" Editable="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="UpdateDepartment" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container  runat="server" Layout="FormLayout" ColumnWidth=".5"
                                            Padding="5">
                                            <Items>
                                               
                                                 <ext:TriggerField ID="add_work_id" runat="server" FieldLabel="所属岗位" LabelAlign="Left" Editable="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectWorkPosition" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TriggerField ID="add_shift_id" runat="server" FieldLabel="所属班组" LabelAlign="Left" Editable="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectShiftClass" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="add_remark" runat="server" FieldLabel="备注"   LabelAlign="Left" Enabled="true" MaxLength="100"/>
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
                <ext:Hidden ID="hidden_select_depart_code" runat="server"></ext:Hidden>
          <%--  <ext:Hidden ID="hidden_depart_code" runat="server"></ext:Hidden>
               <ext:Hidden ID="hidden_work_id" runat="server"></ext:Hidden>
               <ext:Hidden ID="hidden_shift_id" runat="server"></ext:Hidden>--%>
                <ext:Hidden ID="hidden_workshop_id" runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_type" runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_workcode" runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_username" runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_delete_flag"  runat="server" Text="0"></ext:Hidden>
                                                
            </Items>
        </ext:Viewport>
        </form>
</body>
</html>