<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RubPlan.aspx.cs" Inherits="Manager_ProducingPlan_MonthlyEntering_RubPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>月度胶料计划录入</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script src="My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript">


        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.Store2.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }
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
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var SelectMaterialID = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.txtMaterialMinorID.setValue('');
                    App.hiddenRubberCode.setValue('');
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryRubberInfo_Window.show();
                    break;
            }
        }
        var Manager_BasicInfo_CommonPage_QueryRubberInfo_Request = function (record) {
            var queryWindow = App.Manager_BasicInfo_CommonPage_QueryRubberInfo_Window;
            var thisIsAddWindow = function (record) {
                if ((!App.AddRubPlanWin) || (App.AddRubPlanWin.hidden)) {
                    return;
                }
                App.txtRubberName.getTrigger(0).show();
                App.hiddenRubberCode.setValue(record.data.RubCode);
                App.txtRubberName.setValue(record.data.RubName);
            }
            var thisIsDefaultWindow = function (record) {
                if ((!App.AddRubPlanWin) || (App.AddRubPlanWin.hidden)) {
                    App.txtMateral.getTrigger(0).show();
                    App.txtMaterialMinorID.setValue(record.data.RubCode);
                    App.txtMateral.setValue(record.data.RubName);
                }
            }
            thisIsAddWindow(record);
            thisIsDefaultWindow(record);
            queryWindow.close();
        }
        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryRubberInfo_Window",
            hidden: true,
            width: 360,
            height: 470,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryRubberInfo.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择胶料",
            modal: true
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Hidden ID="hiddenRubberCode" runat="server">
    </ext:Hidden>
    <ext:Hidden ID="txtMaterialMinorID" runat="server">
    </ext:Hidden>
    <ext:Window ID="AddRubPlanWin" runat="server" Icon="MonitorAdd" Closable="false"
        Title="添加胶料计划信息" Width="280" Height="180" Resizable="false" Hidden="true" Modal="false"
        BodyStyle="background-color:#fff;" BodyPadding="5" Layout="Form">
        <Items>
            <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5">
                <Items>
                    <ext:TriggerField ID="txtRubberName" runat="server" Flex="1" AllowBlank="false" Editable="false"
                        FieldLabel="胶料" LabelAlign="Right">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            <ext:FieldTrigger Icon="Search" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Fn="SelectMaterialID" />
                        </Listeners>
                    </ext:TriggerField>
                    <ext:TextField ID="txtPlanNum" runat="server" FieldLabel="计划量" AllowBlank="false"
                        LabelAlign="Right">
                    </ext:TextField>
                    <ext:Container runat="server" ID="sss">
                        <Content>
                            <asp:Label runat="server" ID="lbltext" Text="&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp月份:"></asp:Label>
                            <asp:TextBox runat="server" class="Wdate" Width="150" ID="txtDate" onclick="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM'})"></asp:TextBox>
                        </Content>
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
    <ext:Window ID="ModifyRubPlanWin" runat="server" Closable="false" Icon="Add" Title="修改胶料计划信息"
        Width="350" Height="160" Hidden="true" Modal="false">
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" BodyPadding="5">
                <Items>
                    <ext:TextField ID="txtUpMaterialName" runat="server" FieldLabel="胶料名称" ReadOnly="true"
                        AllowBlank="false" LabelAlign="Right" />
                    <ext:TextField ID="txtUpDate" runat="server" FieldLabel="月份" AllowBlank="false"
                        LabelAlign="Right" ReadOnly="true" />
                    <ext:TextField ID="txtUpPlanNum" runat="server" FieldLabel="计划量" AllowBlank="false"
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
    <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="Panel1" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ctl320">
                        <Items>
                            <ext:ToolbarSeparator ID="ctl347" />
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <Listeners>
                                    <Click Fn="pnlListFresh">
                                    </Click>
                                </Listeners>
                                <ToolTips>
                                    <ext:ToolTip runat="server" Html="查询" ID="ToolTip2" />
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
                    <ext:Panel ID="Panel2" runat="server" AutoHeight="true">
                        <Items>
                            <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="Container7" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:Container runat="server" ID="Container2">
                                                <Content>
                                                    <asp:Label runat="server" ID="Label1" Text="&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp月份:"></asp:Label>
                                                    <asp:TextBox runat="server" class="Wdate" Width="150" ID="txtQueryDate" onclick="WdatePicker({isShowClear:false,readOnly:true,dateFmt:'yyyy-MM'})"></asp:TextBox>
                                                </Content>
                                            </ext:Container>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:TriggerField ID="txtMateral" runat="server" Flex="1" Editable="false" FieldLabel="胶料"
                                                LabelAlign="Right">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="SelectMaterialID" />
                                                </Listeners>
                                            </ext:TriggerField>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:GridPanel ID="GridPanel1" runat="server" Region="Center">
                <Store>
                    <ext:Store ID="Store2" runat="server" PageSize="15">
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="Model1" runat="server">
                                <Fields>
                                    <ext:ModelField Name="ObjID" Type="Int" />
                                    <ext:ModelField Name="MaterialCode" Type="String" />
                                    <ext:ModelField Name="MaterialName" Type="String" />
                                    <ext:ModelField Name="PlanNum" Type="Float" />
                                    <ext:ModelField Name="TypeID" Type="Int" />
                                    <ext:ModelField Name="RealWeight" Type="Float" />
                                    <ext:ModelField Name="YearMonth" Type="String" />
                                    <ext:ModelField Name="Per" Type="String" Mapping="Per" />
                                    <ext:ModelField Name="Num" Type="Float" Mapping="Num" />
                                    <ext:ModelField Name="Remain" Type="Float">
                                    </ext:ModelField>
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="ColumnModel2" runat="server">
                    <Columns>
                        <ext:Column ID="Column1" runat="server" Text="胶料名称" Width="150" DataIndex="MaterialName">
                        </ext:Column>
                        <ext:Column ID="Column2" runat="server" Text="计划量" Width="150" DataIndex="PlanNum">
                        </ext:Column>
                        <ext:Column ID="Column4" runat="server" Text="累计完成量" Width="100" DataIndex="RealWeight">
                        </ext:Column>
                        <ext:ComponentColumn ID="ComponentColumn1" DataIndex="Num" runat="server" Text="完成率">
                            <Component>
                                <ext:ProgressBar ID="ProgressBar1" runat="server" Text="Progress">
                                </ext:ProgressBar>
                            </Component>
                            <Listeners>
                                <Bind Handler="cmp.updateProgress(record.get('Num'),record.get('Per'));" />
                            </Listeners>
                        </ext:ComponentColumn>
                        <ext:Column ID="Column5" runat="server" Text="剩余量" Width="60" DataIndex="Remain">
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
                    <ext:PagingToolbar ID="pageToolBar" runat="server">
                        <Plugins>
                            <ext:ProgressBarPager ID="ProgressBarPager1" runat="server" />
                        </Plugins>
                    </ext:PagingToolbar>
                </BottomBar>
            </ext:GridPanel>
            <ext:Hidden ID="hidden_update_equipCode" runat="server" />
            <ext:Hidden ID="hidden_delete_flag" runat="server" Text="0">
            </ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
