<%@ page language="C#" autoeventwireup="true" inherits="Manager_Equipment_SapSparePart_SparePartOut, App_Web_kk4fezpu" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>备件出库管理</title>
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
            App.hidden_delete_flag.setValue("0");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }
    </script>
    <script type="text/javascript">
        //-------领用人-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {//领用人返回值处理
            if (!App.winAdd.hidden) {
                App.add_send_user.setValue(record.data.UserName);
                App.hidden_send_user.setValue(record.data.WorkBarcode);
            }
            else if (!App.winModify.hidden) {
                App.modify_send_user.setValue(record.data.UserName);
                App.hidden_send_user.setValue(record.data.WorkBarcode);
            }
        }

        var SelectUserInfo = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hidden_send_user.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();
                    break;
            }
        }

        Ext.create("Ext.window.Window", {//领用人查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasUser_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryBasUser.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择领用人",
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
     <script type="text/javascript">
         //-------备件代码-----查询带回弹出框--BEGIN
         var Manager_BasicInfo_CommonPage_QueryEqmSparePart_Request = function (record) {//备件代码返回值处理
             if (!App.winAdd.hidden) {
                 App.add_sparepart_code.setValue(record.data.SparePartName);
                 App.add_sparepart_code.getTrigger(0).show();
                 App.hidden_sparepart_code.setValue(record.data.SparePartCode);
             }
             else if (!App.winModify.hidden) {
                 App.modify_sparepart_code.setValue(record.data.SparePartName);
                 App.modify_sparepart_code.getTrigger(0).show();
                 App.hidden_sparepart_code.setValue(record.data.SparePartCode);
             } else {
                 App.txt_sparepart_code.setValue(record.data.SparePartName);
                 App.txt_sparepart_code.getTrigger(0).show();
                 App.hidden_select_sparepart_code.setValue(record.data.SparePartCode);
             }
         }

         var SelectSparePartInfo = function (field, trigger, index) {
             switch (index) {
                 case 0:
                     field.getTrigger(0).hide();
                     field.setValue('');
                     App.hidden_sparepart_code.setValue("");
                     App.hidden_select_sparepart_code.setValue("");
                     field.getEl().down('input.x-form-text').setStyle('background', "white");
                     break;
                 case 1:
                     App.Manager_BasicInfo_CommonPage_QueryEqmSparePart_Window.show();
                     break;
             }
         }

         Ext.create("Ext.window.Window", {//备件代码查询带回窗体
             id: "Manager_BasicInfo_CommonPage_QueryEqmSparePart_Window",
             height: 460,
             hidden: true,
             width: 360,
             html: "<iframe src='../../BasicInfo/CommonPage/QueryEqmSparePart.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
             bodyStyle: "background-color: #fff;",
             closable: true,
             title: "请选择备件名称",
             modal: true
         })
         //------------查询带回弹出框--END 
    </script>
</head>
<body>
    <form id="fmUnit" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
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
                                 <ext:ToolbarSeparator ID="toolbarSeparator_middle_1" />
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
                        <ext:Panel ID="pnlUnitQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                            Padding="5">
                                            <Items>
                                                 <ext:TriggerField ID="txt_sparepart_code" runat="server" FieldLabel="备件名称" LabelAlign="Right" Editable="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectSparePartInfo" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                            Padding="5">
                                            <Items>
                                                <ext:DateField ID="txt_begin_send_date" runat="server" FieldLabel="开始出库时间" LabelAlign="Right" Editable="false" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_3" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                            Padding="5">
                                            <Items>
                                                <ext:DateField ID="txt_end_send_date" runat="server" FieldLabel="结束出库时间" LabelAlign="Right" Editable="false" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container7" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txtSendNo" runat="server" FieldLabel="出库单号" LabelAlign="Right" />
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
                                        <ext:ModelField Name="SendNo" />
                                        <ext:ModelField Name="SendDate" Type="Date" />
                                        <ext:ModelField Name="SparePartCode" />
                                        <ext:ModelField Name="SparePartModel" />
                                        <ext:ModelField Name="StoreOutNum" />
                                        <ext:ModelField Name="SendUser" />
                                        <ext:ModelField Name="RecordDate" />
                                        <ext:ModelField Name="Remark" />
                                        <ext:ModelField Name="DeleteFlag" />
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
                            <ext:Column ID="obj_id" runat="server" Text="编号" DataIndex="ObjID" Width="100"  Hidden="true" />
                            <ext:Column ID="send_no" runat="server" Text="出库单号" DataIndex="SendNo" Width="110"  />
                            <ext:DateColumn Format="yyyy-MM-dd" ID="send_date" runat="server" Text="出库日期" DataIndex="SendDate" Width="120"  />
                            <ext:Column ID="sparepart_code" runat="server" Text="备件名称" DataIndex="SparePartCode" Width="120"  />
                            <ext:Column ID="sparepart_model" runat="server" Text="型号" DataIndex="SparePartModel" Width="80"  />
                            <ext:Column ID="store_out_num" runat="server" Text="出库数量" DataIndex="StoreOutNum" Width="60"  />
                            <ext:Column ID="send_user" runat="server" Text="领取人" DataIndex="SendUser" Width="100"  />
                            <ext:DateColumn ID="DateColumn1" Format="yyyy-MM-dd" runat="server" Text="记录时间" DataIndex="RecordDate" Width="100"  />
                            <ext:Column ID="delete_flag" runat="server" Text="删除标志" DataIndex="DeleteFlag" Width="150" Hidden="true"  />
                            <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Width="150"  />
                            <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                        <ToolTip Text="修改本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                        <ToolTip Text="删除本条数据" />
                                    </ext:GridCommand>
                                </Commands>
                                <Listeners>
                                    <Command Handler="return commandcolumn_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <View>
                        <ext:GridView ID="gvRows" runat="server">
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
                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改备件出库信息"
                    Width="500" Height="250" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                        <ext:Container ID="Container3" runat="server" Layout="FormLayout" ColumnWidth=".5" Padding="5">
                                            <Items>
                                                <ext:TextField ID="modify_obj_id" runat="server" FieldLabel="编号" LabelAlign="Left" Hidden="true"/>
                                                <ext:TextField ID="modify_send_no" runat="server" FieldLabel="出库单号" LabelAlign="Left" ReadOnly="true"/>
                                                <ext:DateField ID="modify_send_date" runat="server" FieldLabel="出库日期" LabelAlign="Left" Editable="false"/>
                                                <ext:TriggerField ID="modify_sparepart_code" runat="server" FieldLabel="备件名称" LabelAlign="Left" Editable="false" AllowBlank="false" ReadOnly="true">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectSparePartInfo" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="modify_model" runat="server" FieldLabel="型号" LabelAlign="Left"/>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container4" runat="server" Layout="FormLayout" ColumnWidth=".5" Padding="5">
                                            <Items>
                                                <ext:NumberField ID="modify_number" runat="server" FieldLabel="出库数量" MinValue="1" LabelAlign="Left"/>
                                                <ext:TriggerField ID="modify_send_user" runat="server" FieldLabel="领取人" LabelAlign="Left" Editable="false"  AllowBlank="false">
                                                     <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectUserInfo" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="modify_remark" runat="server" FieldLabel="备注" LabelAlign="Left"/>
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
                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加备件出库信息"
                    Width="500" Height="250" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="Container5" runat="server" Layout="FormLayout" ColumnWidth=".5" Padding="5">
                                            <Items>
                                                <%--<ext:TextField ID="add_send_no" runat="server" FieldLabel="出库单号" LabelAlign="Left" />--%>
                                                <ext:DateField ID="add_send_date" runat="server" FieldLabel="出库日期" LabelAlign="Left" Editable="false"/>
                                                <ext:TriggerField ID="add_sparepart_code" runat="server" FieldLabel="备件名称" LabelAlign="Left" Editable="false" AllowBlank="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectSparePartInfo" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="add_model" runat="server" FieldLabel="型号" LabelAlign="Left"/>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container6" runat="server" Layout="FormLayout" ColumnWidth=".5" Padding="5">
                                            <Items>
                                                <ext:NumberField ID="add_number" runat="server" FieldLabel="出库数量" MinValue="1" LabelAlign="Left"/>
                                                <ext:TriggerField ID="add_send_user" runat="server" FieldLabel="领取人" LabelAlign="Left" Editable="false"  AllowBlank="false" >
                                                     <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectUserInfo" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="add_remark" runat="server" FieldLabel="备注" LabelAlign="Left"/>
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
                <ext:Hidden ID="hidden_send_user"  runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_sparepart_code"  runat="server">
                    <DirectEvents>
                        <Change OnEvent="Hidden_SparePart_Code_Change_Event"></Change>
                    </DirectEvents>
                </ext:Hidden>
                <ext:Hidden ID="hidden_select_sparepart_code"  runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_delete_flag"  runat="server" Text="0"></ext:Hidden>
            </Items>
        </ext:Viewport>
        </form>
</body>
</html>