<%@ page language="C#" autoeventwireup="true" inherits="Manager_ProducingPlan_PlanExecMonitoring_PlanSetMonitoring, App_Web_g25qpua0" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>监控运行设置</title>
        <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />

    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        //根据按钮类别进行删除和编辑操作
        var commandcolumn_click = function (command, record) {
            commandcolumn_click_confirm(command, record);
            return false;
        };

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


        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var EquipCode = record.data.EquipCode;
            App.direct.commandcolumn_direct_edit(EquipCode, {
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
            var EquipCode = record.data.EquipCode;
            App.direct.commandcolumn_direct_delete(EquipCode, {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }



        var SelectEquipID = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.hidden_select_equip.setValue("Equip");
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
            }
        }
        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {  //机台返回值处理
            var type = App.hidden_select_equip.getValue();
            App.txtEquipCode.getTrigger(0).show();
            if (type == "Equip") {
                App.txtEquipCode.setValue(record.data.EquipName);
                App.hidden_select_equip_code.setValue(record.data.EquipCode);
            }
        }
        Ext.create("Ext.window.Window", {//机台信息带回查询信息
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            height: 480,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择机台",
            modal: true
        })


    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Hidden ID="hidden_select_equip" runat="server">
    </ext:Hidden>
    <ext:Hidden ID="hidden_select_equip_code" runat="server">
    </ext:Hidden>
    <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="Panel1" runat="server" Region="Center" Layout="FitLayout">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ctl320">
                        <Items>
                            <ext:ToolbarSeparator ID="ctl347" />
                            <ext:Button runat="server" Icon="Find" Text="刷新" ID="btnSearch">
                                <DirectEvents>
                                    <Click OnEvent="BtnSeacherShift_Click">
                                    </Click>
                                </DirectEvents>
                                <ToolTips>
                                    <ext:ToolTip runat="server" Html="刷新" ID="ToolTip2" />
                                </ToolTips>
                            </ext:Button>
                            <ext:Button runat="server" Icon="Add" Text="新增" ID="btnAdd">
                                <DirectEvents>
                                    <Click OnEvent="btnAdd_Click">
                                    </Click>
                                </DirectEvents>
                                <ToolTips>
                                    <ext:ToolTip runat="server" Html="新增" ID="ctl350" />
                                </ToolTips>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="ctl361" />
                            <ext:ToolbarSpacer runat="server" ID="ctl363" />
                            <ext:ToolbarFill ID="ctl381" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:GridPanel ID="GridPanel1" runat="server" Layout="FitLayout">
                        <Store>
                            <ext:Store ID="Store2" runat="server" OnReadData="MyData_Refresh" PageSize="15">
                                <Model>
                                    <ext:Model ID="Model1" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="ObjID" Type="Int" />
                                            <ext:ModelField Name="EquipName" Type="String" />
                                            <ext:ModelField Name="EquipCode" Type="String" />
                                            <ext:ModelField Name="EquipIP" Type="String" />
                                            <ext:ModelField Name="EquipPort" Type="Int" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModel2" runat="server">
                            <Columns>
                                <ext:Column ID="Column1" runat="server" Text="机台" Width="150" DataIndex="EquipName">
                                </ext:Column>
                                <ext:Column ID="Column2" runat="server" Text="监控IP" Width="150" DataIndex="EquipIP">
                                </ext:Column>
                                <ext:Column ID="Column4" runat="server" Text="监控端口" Width="60" DataIndex="EquipPort">
                                </ext:Column>
                                <ext:CommandColumn ID="CommandColumn1" runat="server" Width="120" Text="操作" Align="Center">
                                    <Commands>
                                        <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                            <ToolTip Text="修改本条数据" />
                                        </ext:GridCommand>
                                        <ext:CommandSeparator />
                                        <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                            <ToolTip Text="删除本条数据" />
                                        </ext:GridCommand>
                                        <ext:CommandSeparator />
                                    </Commands>
                                    <Listeners>
                                        <Command Handler="return commandcolumn_click(command, record);" />
                                    </Listeners>
                                </ext:CommandColumn>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" Mode="Multi" />
                        </SelectionModel>
                        <View>
                            <ext:GridView ID="GridView2" runat="server" StripeRows="true">
                            </ext:GridView>
                        </View>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar1" runat="server">
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager1" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                        <Listeners>
                            <%-- <CellClick Handler="#{Window1}.show();" />--%>
                        </Listeners>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
            <ext:Hidden ID="hidden_update_equipCode" runat="server" />
            <ext:Hidden ID="hidden_delete_flag" runat="server" Text="0">
            </ext:Hidden>
        </Items>
    </ext:Viewport>
    <ext:Window ID="AddConfigWin" runat="server" Icon="MonitorAdd" Closable="false" Title="机台监控信息"
        Width="280" Height="180" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
        BodyPadding="5" Layout="Form">
        <Items>
            <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5">
                <Items>
                    <ext:TriggerField ID="txtEquipCode" runat="server" Flex="1" AllowBlank="false" Editable="false" FieldLabel="机台"
                        LabelAlign="Right">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            <ext:FieldTrigger Icon="Search" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Fn="SelectEquipID" />
                        </Listeners>
                    </ext:TriggerField>
                    <ext:TextField ID="txtIP" runat="server" FieldLabel="IP" AllowBlank="false" LabelAlign="Right" />
                    <ext:TextField ID="txtPort" runat="server" FieldLabel="端口号" AllowBlank="false" LabelAlign="Right" />
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
            <Show Handler="for(var i=0;i<#{Viewport1}.items.length;i++){#{Viewport1}.getComponent(i).disable(true);}" />
            <Hide Handler="for(var i=0;i<#{Viewport1}.items.length;i++){#{Viewport1}.getComponent(i).enable(true);}" />
        </Listeners>
    </ext:Window>
    <ext:Window ID="ModifyConfigWin" runat="server" Closable="false" Icon="Add" Title="修改监控信息"
        Width="350" Height="160" Hidden="true" Modal="false">
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" BodyPadding="5">
                <Items>
                    <ext:TextField ID="txtUpEuipName" runat="server" FieldLabel="机台名称" ReadOnly="true" AllowBlank="false"
                        LabelAlign="Right" />
                    <ext:TextField ID="txtUpIP" runat="server" FieldLabel="IP" AllowBlank="false"
                        LabelAlign="Right" />
                    <ext:TextField ID="txtUpPort" runat="server" FieldLabel="端口号" AllowBlank="false"
                        LabelAlign="Right" />
                </Items>
                <Listeners>
                    <ValidityChange Handler="#{btnModifySave}.setDisabled(!valid);" />
                </Listeners>
            </ext:FormPanel>
        </Items>
        <Buttons>
            <ext:Button ID="btnModifySave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                <DirectEvents>
                    <Click OnEvent="BtnModifySave_Click">
                        <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                    </Click>
                </DirectEvents>
            </ext:Button>
            <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                <DirectEvents>
                    <Click OnEvent="BtnModifyCancel_Click">
                    </Click>
                </DirectEvents>
            </ext:Button>
        </Buttons>
        <Listeners>
            <Show Handler="for(var i=0;i<#{Viewport1}.items.length;i++){#{Viewport1}.getComponent(i).disable(true);}" />
            <Hide Handler="for(var i=0;i<#{Viewport1}.items.length;i++){#{Viewport1}.getComponent(i).enable(true);}" />
        </Listeners>
    </ext:Window>
    </form>
</body>
</html>
