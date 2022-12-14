<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainHanderInfo.aspx.cs" Inherits="Manager_BasicInfo_MainHanderInfo_MainHanderInfo" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>主机手信息</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
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

        //区分删除操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
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
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }
    </script>

     <script type="text/javascript">
         //-------人员绑定-----查询带回弹出框--BEGIN
         var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {//人员绑定
             if (!App.winModify.hidden) {
                 App.modify_user_code.setValue(record.data.UserName);
                 App.hidden_user_code.setValue(record.data.HRCode);
             }
             if (!App.winAdd.hidden) {
                 App.add_user_code.setValue(record.data.UserName);
                 App.hidden_user_code.setValue(record.data.HRCode);
             }
         }

         var SelectUserCode = function () {//人员绑定查询
             App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();
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
     <script type="text/javascript">
         //-------车间-----查询带回弹出框--BEGIN
         var Manager_BasicInfo_CommonPage_QueryWorkShop_Request = function (record) {//部门返回值处理
             if (!App.winAdd.hidden) {
                 App.add_workshop_code.setValue(record.data.WorkShopName);
             }
             else if (!App.winModify.hidden) {
                 App.modify_workshop_code.setValue(record.data.WorkShopName);
             }
             App.hidden_workshop_code.setValue(record.data.ObjID);
         }

         var SelectWorkShopCode = function () {//车间查询
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
             title: "请选择车间",
             modal: true
         })
         //------------查询带回弹出框--END 
    </script>
</head>
<body>
    <form id="fmMainHander" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager ID="rmMainHander" runat="server" />
        <ext:Viewport ID="vwMainHander" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlMainHanderTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barMainHander">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="pnlListFresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                 <ext:ToolbarSeparator ID="toolbarSeparator_middle_1" />
                                 <ext:Button ID="btnAdd" runat="server" Icon="Add" Text="添加">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行添加" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btnAdd_Click" />
                                    </DirectEvents>
                                 </ext:Button>
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
                        <ext:Panel ID="pnlMainHanderQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_main_hander_code" Vtype="integer" runat="server" FieldLabel="主机手编号" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_user_name" runat="server" FieldLabel="人员姓名" LabelAlign="Right" />
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
                        <ext:Store ID="store" runat="server" PageSize="15" RemoteSort="true"> 
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="MainHanderCode" />
                                        <ext:ModelField Name="WorkShopCode" />
                                        <ext:ModelField Name="ClassID" />
                                        <ext:ModelField Name="UserCode"  />
                                        <ext:ModelField Name="UserName"  />
                                        <ext:ModelField Name="Remark" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="ObjID" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                            <ext:Column ID="obj_id" runat="server" Text="自增编号" DataIndex="ObjID" Width="150" Hidden="true"  />
                            <ext:Column ID="main_hander_code" runat="server" Text="主机手编号" DataIndex="MainHanderCode" Width="150"  />
                            <ext:Column ID="work_shop_code" runat="server" Text="所属车间" DataIndex="WorkShopCode" Width="150"  />
                            <ext:Column ID="class_id" runat="server" Text="所属班组" DataIndex="ClassID" Width="150"  />
                            <ext:Column ID="user_code" runat="server" Text="人员编号" DataIndex="UserCode" Width="150"  />
                            <ext:Column ID="user_name" runat="server" Text="人员名称" DataIndex="UserName" Width="150"  />
                            <ext:Column ID="delete_flag" runat="server" Text="删除标志" DataIndex="DeleteFlag" Width="150" Hidden="true"  />
                            <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Width="150"  />
                            <ext:CommandColumn ID="commandCol" runat="server" Width="80" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                        <ToolTip Text="修改本条数据" />
                                    </ext:GridCommand>
                                </Commands>
                                <Listeners>
                                    <Command Handler="return commandcolumn_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolBar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改主机手信息"
                    Width="320" Height="240" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                <ext:TextField ID="modify_obj_id" runat="server" FieldLabel="自增编号"   LabelAlign="Left" ReadOnly=true Hidden=true Enabled="true" />
                                <ext:TextField ID="modify_main_hander" runat="server" FieldLabel="主机手编号"   LabelAlign="Left"   Enabled="true" />
                                <ext:TriggerField ID="modify_user_code" runat="server" FieldLabel="人员姓名" LabelAlign="Left" Editable="false"  >
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Fn="SelectUserCode" />
                                    </Listeners>
                                </ext:TriggerField>
                                  <ext:TriggerField ID="modify_workshop_code" runat="server" FieldLabel="所属车间" LabelAlign="Left" Editable="false" AllowBlank="false"  >
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Fn="SelectWorkShopCode" />
                                    </Listeners>
                                </ext:TriggerField>
                                <ext:ComboBox ID="modify_class_id" runat="server" FieldLabel="所属班组" LabelAlign="Left"  MaxLength="50" Editable="false" AllowBlank="false"  />
                                <ext:TextField ID="modify_remark" runat="server" FieldLabel="备注" LabelAlign="Left"  MaxLength="50" />
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
                        <Show Handler="for(var i=0;i<#{vwMainHander}.items.length;i++){#{vwMainHander}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwMainHander}.items.length;i++){#{vwMainHander}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Window ID="winAdd" runat="server" Icon="MonitorEdit" Closable="false" Title="添加主机手信息"
                    Width="320" Height="240" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                <ext:TextField ID="add_obj_id" runat="server" FieldLabel="自增编号"   LabelAlign="Left" ReadOnly="true" Hidden="true" Enabled="true" />
                                <ext:TextField ID="add_main_hander" runat="server" FieldLabel="主机手编号" LabelAlign="Left" AllowBlank="false" />
                                <ext:TriggerField ID="add_user_code" runat="server" FieldLabel="人员姓名" LabelAlign="Left" Editable="false" AllowBlank="false"  >
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Fn="SelectUserCode" />
                                    </Listeners>
                                </ext:TriggerField>
                                 <ext:TriggerField ID="add_workshop_code" runat="server" FieldLabel="所属车间" LabelAlign="Left" Editable="false" AllowBlank="false"  >
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Fn="SelectWorkShopCode" />
                                    </Listeners>
                                </ext:TriggerField>
                                <ext:ComboBox ID="add_class_id" runat="server" FieldLabel="所属班组" LabelAlign="Left"  MaxLength="50" Editable="false" AllowBlank="false"  />
                                <ext:TextField ID="txtRemark1" runat="server" FieldLabel="备注" LabelAlign="Left"  MaxLength="50" />
                            </Items>
                             <Listeners>
                                <ValidityChange Handler="#{btnAddSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <DirectEvents>
                                <Click OnEvent="btnAddSave_Click"></Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="Button2" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="BtnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwMainHander}.items.length;i++){#{vwMainHander}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwMainHander}.items.length;i++){#{vwMainHander}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Hidden ID="hidden_user_code"  runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_workshop_code"  runat="server"></ext:Hidden>
            </Items>
        </ext:Viewport>
        </form>
</body>
</html>