<%@ Page Language="C#" AutoEventWireup="true" CodeFile="XCSet.aspx.cs" Inherits="Manager_Technology_XCSet" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>配方洗车设置</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var ixc = 0;
    
        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var ObjID = record.data.ObjID;
            App.direct.commandcolumn_direct_edit(ObjID, {
                success: function (result) {
                   
                },

                failure: function (errorMsg) {
                
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


        //列表刷新数据重载方法
        var pnlListFresh = function () {
//            App.hidden_delete_flag.setValue("0");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        //点击修改按钮
        var direct_save = function (record) {


            App.direct.BtnModifySave_Click({
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }





        var add_save = function (record) {


            App.direct.BtnAddSave_Click({
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }




        var QueryMaterial = function (field, trigger, index) {//物料查询

            ixc = 1;
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();


        }

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            //            App.txtMaterName.getTrigger(0).show();
            //            App.txtMaterName.setValue(record.data.MaterialName);
            //            App.hiddenMaterCode.setValue(record.data.MaterialCode);
            //            App.hiddenMaterName.setValue(record.data.MaterialName);

            if (ixc == 1) {
                App.direct.AddMaterial(record.data.MaterialCode, record.data.MaterialName, {
                    success: function (result) {
                    },

                    failure: function (errorMsg) {

                    }
                });
            }
            if (ixc == 2) {
                App.direct.AddXCRecipe(record.data.MaterialCode, record.data.MaterialName, {
                    success: function (result) {
                    },

                    failure: function (errorMsg) {

                    }
                });
            }
        }

        Ext.create("Ext.window.Window", {//物料带窗体
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryMaterial.aspx?MajorTypeID=4,5' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料",
            modal: true
        })


        var QueryXC = function (field, trigger, index) {//物料查询

            ixc = 2;
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();


        }
       

      
    </script>
</head>
<body>
    <form id="fmUnit" runat="server">
 
        <ext:ResourceManager ID="rmUnit" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
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
                                        <Click Fn="pnlListFresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                 <ext:ToolbarSeparator ID="toolbarSeparator_middle" />
                                
                           
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>

                   
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
                                        <ext:ModelField Name="MaterialCode" />
                                         <ext:ModelField Name="MaterialName" />
                                <ext:ModelField Name="XCCode" />
                                         <ext:ModelField Name="XCName" />
                                           <ext:ModelField Name="XCNum" />
                                                
                 
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                   <ext:Column ID="Column4" runat="server" Text="编号" DataIndex="ObjID" Width="150" />
                        
                        <%--    <ext:Column ID="ZiAddress" runat="server" Text="工艺编号" DataIndex="MaterialCode" Width="150"  />--%>
                            <ext:Column ID="WeiAddress" runat="server" Text="工艺名称" DataIndex="MaterialName" Width="150"  />
                              <ext:Column ID="Column1" runat="server" Text="洗车名称" DataIndex="XCName" Width="150"  />
                             <ext:Column ID="AlarmNo" runat="server" Text="洗车数量" DataIndex="XCNum" Width="150"  />
               
                        
                             <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                <Commands>
                                  <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="编辑">
                                        <ToolTip Text="编辑本条数据" />
                                    </ext:GridCommand>
                                      <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                        <ToolTip Text="删除本条数据" />
                                    </ext:GridCommand>
                                   
                                    
                                </Commands>
                              <%--  <PrepareToolbar Fn="prepareToolbar"  />--%>
                                <Listeners>
                                    <Command Handler="return commandcolumn_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <View>
                   <%--     <ext:GridView ID="gvRows" runat="server">
                            <GetRowClass Fn="SetRowClass" />
                        </ext:GridView>--%>
                    </View>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolBar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改报警信息"
                    Width="400" Height="300" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                    <ext:TextField ID="modify_ID" runat="server" FieldLabel="id" LabelAlign="Left" readonly ="true" />
                                <ext:NumberField ID="NumberField1" runat="server" FieldLabel="字地址" LabelAlign="Left" />
                                <ext:NumberField ID="NumberField2" runat="server" FieldLabel="位地址" LabelAlign="Left" />
                                <ext:TextField ID="TextField6" runat="server" FieldLabel="报警编号" LabelAlign="Left" />
                                <ext:TextField ID="TextField7" runat="server" FieldLabel="报警名称" LabelAlign="Left" />
                                <ext:TextField ID="TextField8" runat="server" FieldLabel="报警地址" LabelAlign="Left" />
                                <ext:TextField ID="TextField9" runat="server" FieldLabel="备注" LabelAlign="Left" />
                           </Items>
                           <%--  <Listeners>
                                <ValidityChange Handler="#{btnModifySave}.setDisabled(!valid);" />
                            </Listeners>--%>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnModifySave" runat="server" Text="确定" Icon="Accept">
                           
                                  <Listeners>
                                        <Click Fn="direct_save"></Click>
                                    </Listeners>
                          
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
                    Width="320" Height="640" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                               <ext:TriggerField ID="txtAddRecipe" runat="server" FieldLabel="工艺配方" LabelAlign="Left" Editable="false">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Fn="QueryMaterial" />
                                    </Listeners>
                                </ext:TriggerField>
                                 <ext:TextArea ID="AddRecipe" runat="server" FieldLabel="已有配方" LabelAlign="Left" ReadOnly="true" Height="200"/>
                             
                                  <ext:TextField ID="AddRecipe2" runat="server" FieldLabel="已有物料" LabelAlign="Left" Hidden="true" />
                            
                                       
                       <ext:TriggerField ID="TriggerField1" runat="server" FieldLabel="洗车配方" LabelAlign="Left" Editable="false">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Fn="QueryXC" />
                                    </Listeners>
                                </ext:TriggerField>
                                 <ext:TextArea ID="AddXC" runat="server" FieldLabel="已有配方" LabelAlign="Left" ReadOnly="true" Height="200"/>
                             
                                  <ext:TextField ID="AddXC2" runat="server" FieldLabel="已有物料" LabelAlign="Left" Hidden="true" />
                                     <ext:NumberField ID="Addnum" runat="server" FieldLabel="洗车车次" LabelAlign="Left"  AllowBlank="false"/>
                                                          </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnAddSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                     <Buttons>
                      <ext:Button ID="Button2" runat="server" Text="清空工艺" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="ClearMaterial">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                         <ext:Button ID="Button1" runat="server" Text="清空洗车" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="ClearXC">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                           
                              <Listeners>
                                        <Click Fn="add_save"></Click>
                                    </Listeners>
                       
                         
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
           
                      <ext:Hidden ID="hiddenEquipCode" runat="server">
            </ext:Hidden>
          
               <ext:Hidden ID="TextID" runat="server"></ext:Hidden>
            </Items>
        </ext:Viewport>
        </form>
</body>
</html>