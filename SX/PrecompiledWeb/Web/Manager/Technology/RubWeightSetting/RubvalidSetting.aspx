<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_RubWeightSetting_RubvalidSetting, App_Web_pxnhn0ln" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>胶料称锁定设置</title>
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

        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }
        //全部解锁和全部锁定的确定框方法
        var fn_lock_cofirm = function (ctrl) {
            var notice = "";
            switch (ctrl) {
                case 0: notice = "不锁全选";
                    break;
                case 2: notice = "全锁定全选";
                    break;
                default:

            }
            Ext.Msg.confirm("提示", '是否执行[' + notice + ']', function (btn) {
                if (btn != "yes") {
                    return;
                }
                fn_lock(ctrl)
            });
        }

        var fn_lock = function (ctrl) {
            App.direct.equip_lock_func(ctrl, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                    pnlListFresh();
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

    </script>
    <script type="text/javascript">
        //------所属车间------查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryWorkShop_Request = function (record) {//所属车间返回值处理
            App.txt_workshop_code.setValue(record.data.WorkShopName);
            App.hidden_workshop_code.setValue(record.data.ObjID);
        }

        var SelectWorkShop = function (field, trigger, index, hiddenId) {//人员绑定查询
            switch (index) {
                case 0:
                    field.setValue('');
                    document.getElementById(hiddenId).value = '';
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryWorkShop_Window.show();
                    break;
            }
        }

        Ext.create("Ext.window.Window", {//所属车间查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryWorkShop_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryWorkShop.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择所属车间",
            modal: true
        })
        //------------查询带回弹出框--END 
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
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="pnlListFresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator1" />  
                                <ext:Button runat="server" Icon="Lock" Text="全部锁定" ID="btn_all_lock">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="锁定所有设备胶料称" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="fn_lock_cofirm(2)"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator2" /> 
                                <ext:Button runat="server" Icon="LockDelete" Text="全部不锁" ID="btn_all_unlock">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip2" runat="server" Html="解锁所有设备胶料称" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="fn_lock_cofirm(0)"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator3" />
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
                                                <ext:TextField ID="txt_equip_code" Vtype="integer" runat="server" FieldLabel="设备编号" LabelAlign="Right" />
                                                 <ext:TriggerField ID="txt_workshop_code" runat="server" FieldLabel="所属车间" Editable="false" AllowBlank="false" LabelAlign="Right" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Handler="SelectWorkShop(this, trigger, index , 'hidden_workshop_code')" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:ComboBox ID="txt_state" runat="server" FieldLabel="设备状态" LabelAlign="Right"  Editable="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <Select Handler="this.getTrigger(0).show();" />
                                                        <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                                    </Listeners>
                                                </ext:ComboBox>
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
                        <ext:Store ID="store" runat="server" PageSize="100" RemoteSort="true"> 
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="EquipCode" />
                                        <ext:ModelField Name="EquipName" />
                                        <ext:ModelField Name="State"  />
                                        <ext:ModelField Name="EquipElectricCurrent"  />
                                        <ext:ModelField Name="WeightSettingCtrl"  />
                                        <ext:ModelField Name="LockType"  />
                                        <ext:ModelField Name="DeleteFlag"  />
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
                            <ext:Column ID="Column1" runat="server" Text="设备编号" DataIndex="EquipCode" Width="150"  />
                            <ext:Column ID="equip_code" runat="server" Text="设备名称" DataIndex="EquipName" Width="150"  />
                            <ext:Column ID="state" runat="server" Text="设备状态" DataIndex="State" Width="150"  />
                            <ext:Column ID="equip_electric_current" runat="server" Text="设备电流" DataIndex="EquipElectricCurrent" Width="150"  />
                            <ext:Column ID="Column3" runat="server" Text="胶料称控制" DataIndex="WeightSettingCtrl" Width="150"  />
                            <ext:Column ID="LockType" runat="server" Text="锁定说明" DataIndex="LockType" Width="150"  />
                            <ext:Column ID="delete_flag" runat="server" Text="删除标志" DataIndex="DeleteFlag" Width="150" Hidden="true"  />
                            <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Width="150"  />
                            <ext:CommandColumn ID="commandCol" runat="server" Width="60" Text="操作" Align="Center">
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
                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改设备胶料称锁定信息"
                    Width="402" Height="250" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="HBoxLayout">
                    <Items> 
                        <ext:FormPanel ID="pnlEdit" runat="server" Flex="1" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="modify_obj_id" runat="server" FieldLabel="自增编号"   LabelAlign="Left" ReadOnly="true" Hidden="true" Enabled="true" />
                                <ext:TextField ID="modify_equip_code" runat="server" FieldLabel="设备名称" LabelAlign="Left" ReadOnly="true"   />
                                <ext:ComboBox ID="modify_state" runat="server" FieldLabel="设备状态" LabelAlign="Left"  ReadOnly="true"  />
                                <ext:NumberField ID="modify_equip_electric_current" runat="server" FieldLabel="设备电流" LabelAlign="Left" AllowBlank="false" />
                                <ext:ComboBox ID="modify_rub_weight_ctrl" runat="server" FieldLabel="胶料称控制" LabelAlign="Left" Editable="false"  AllowBlank="false" />
                                <ext:TextField ID="modify_lock_type" runat="server" FieldLabel="锁定说明" LabelAlign="Left"  />
                                <ext:TextField ID="modify_lock_type_old" runat="server" FieldLabel="锁定说明" Hidden="true" LabelAlign="Left"  />
                                <ext:TextField ID="modify_remark" runat="server" FieldLabel="备注" LabelAlign="Left"  MaxLength="50" />
                            </Items>
                             <Listeners>
                                <ValidityChange Handler="#{btnModifySave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                        <ext:FormPanel ID="pnlNotice" runat="server" Width="125" BodyPadding="5" Margins="0 0 0 6">
                            <Items>
                                <ext:TextArea ID="TextArea1" runat="server" Height="157" ReadOnly="true"
Text="101 天然胶
102 合成胶
103 再生胶
104 其他原材料
201 自动小料
202 人工小料
301 母炼胶
501 终炼胶
601 反炼胶" ></ext:TextArea>
                            </Items>
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
                <ext:Hidden runat="server" ID="hidden_workshop_code" />
            </Items>
        </ext:Viewport>
        </form>
</body>
</html>