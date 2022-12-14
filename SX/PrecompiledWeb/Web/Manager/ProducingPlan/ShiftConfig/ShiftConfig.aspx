﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_ProducingPlan_ShiftConfig_ShiftConfig, App_Web_pobph4a1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>架子信息查询</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <style type="text/css">
        .cbStates-list
        {
            width: 298px;
            font: 11px tahoma,arial,helvetica,sans-serif;
        }
        
        .cbStates-list th
        {
            font-weight: bold;
        }
        
        .cbStates-list td, .cbStates-list th
        {
            padding: 10px;
        }
        
        .list-item
        {
            cursor: pointer;
        }
        
        .indoor-b
        {
            color: Blue;
        }
        .indoor-r
        {
            color: Red;
        }
        .indoor-p
        {
            color: Purple;
        }
        .indoor-g
        {
            color: Green;
        }
    </style>
    <script type="text/javascript">
        var getRowClass = function (r) {
            var d = r.data;
            if (d.UpdateFlag == '3') {
                return "indoor-r";
            }
            if (d.UpdateFlag == '1') {
                return "indoor-p";
            }
            if (d.OrgOrNot == '1') {
                return "indoor-g";
            }
            if (d.BarcodeUse == '0') {
                return "indoor-b";
            }
        };

        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.hidden_delete_flag.setValue("0");
            App.Store1.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }
        var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {//单位返回值处理
            var type = $("#hidden_select_user").val();
            App.operUser.getTrigger(0).show();
            if (type == "User") {
                if (!App.AddConfigWin.hidden) {
                    App.operUser.setValue(record.data.UserName);
                    App.hidden_select_userCode.setValue(record.data.WorkBarcode)
                }
                //                else if (!App.winModify.hidden) {
                //                    App.modify_static_unit_id.setValue(record.data.UnitName);
                //                    App.hidden_static_unit_id.setValue(record.data.ObjID);
                //                }
            }
        }
        var SelectUserID = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.hidden_select_user.setValue("User");
                    App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();
                    break;
            }

        }

        Ext.create("Ext.window.Window", {//人员信息带回查询信息
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
        ///机台信息

        var SelectEquipID = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.cboAddMaterName.store.reload();
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.hidden_select_equip.setValue("Equip");
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
            }
        }
        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {  //机台返回值处理
            var type = $("#hidden_select_equip").val();
            App.txtAddEquipCode.getTrigger(0).show();
            if (type == "Equip") {
                if (!App.AddConfigWin.hidden) {
                    App.txtAddEquipCode.setValue(record.data.EquipName);
                    App.hidden_select_equip_code.setValue(record.data.EquipCode);
                    App.cboAddMaterName.setValue('');
                    App.cboAddMaterName.store.reload();
                }
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
        var GetPlanInfo = function () {
            App.direct.GetPlanNo({
                success: function (result) {
                    //                    Ext.Msg.alert('提示', result);
                },
                failure: function (errorMsg) {
                    //                    Ext.Msg.alert('提示', errorMsg);
                }
            });
        }


        var ChangeNum = function () {
            App.direct.ChangeNumChe({
                success: function (result) {
                    //                    Ext.Msg.alert('提示', result);
                },
                failure: function (errorMsg) {
                    //                    Ext.Msg.alert('提示', errorMsg);
                }
            })
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

        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var Barcode = record.data.Barcode;
            App.direct.commandcolumn_direct_edit(Barcode, {
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
            var Barcode = record.data.Barcode;
            App.direct.commandcolumn_direct_delete(Barcode, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Hidden ID="hidden_select_user" runat="server">
    </ext:Hidden>
    <ext:Hidden ID="hidden_select_userCode" runat="server">
    </ext:Hidden>
    <ext:Hidden ID="hidden_select_equip" runat="server">
    </ext:Hidden>
    <ext:Hidden ID="hidden_select_equip_code" runat="server">
    </ext:Hidden>
    <ext:Hidden ID="hidden_update_barcode" runat="server">
    </ext:Hidden>
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
                            <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击将查询结果导出到Excel中" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
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
                                            <ext:DateField ID="txtStratShiftDate" runat="server" Editable="false" Vtype="daterange"
                                                FieldLabel="生产日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd">
                                                <Listeners>
                                                    <Change Handler="App.cboMaterName.setValue('');App.cboMaterName.store.reload();">
                                                    </Change>
                                                </Listeners>
                                            </ext:DateField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container8" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cboShift" runat="server" Editable="false" FieldLabel="班次" LabelAlign="Right">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.getTrigger(0).show();App.cboMaterName.setValue('');App.cboMaterName.store.reload();" />
                                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { 
                                                                           this.clearValue(); 
                                                                           this.getTrigger(0).hide();
                                                                           App.cboMaterName.setValue('');App.cboMaterName.store.reload();
                                                                       }" />
                                                </Listeners>
                                            </ext:ComboBox>
                                         
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cboEquipCode" runat="server" Editable="false" FieldLabel="机台" LabelAlign="Right">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.getTrigger(0).show();" />
                                                    <Select Handler="#{cboMaterName}.clearValue(); #{MaterNameStore}.reload();" />
                                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { 
                                                                           this.clearValue(); 
                                                                           this.getTrigger(0).hide();
                                                                       }" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cboMaterName" runat="server" DisplayField="Name" Editable="false"
                                                FieldLabel="物料" LabelAlign="Right">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.getTrigger(0).show();App.cboMaterName.setValue('');App.cboMaterName.store.reload();" />
                                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { 
                                                                           this.clearValue(); 
                                                                           this.getTrigger(0).hide();
                                                                           App.cboMaterName.setValue('');App.cboMaterName.store.reload();
                                                                       }" />
                                                </Listeners>
                                                <Store>
                                                    <ext:Store runat="server" ID="MaterNameStore" AutoLoad="true" OnReadData="MaterNameRefresh">
                                                        <Model>
                                                            <ext:Model ID="Model1" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="Id" Type="Int" Mapping="Id" />
                                                                    <ext:ModelField Name="Name" Type="String" Mapping="Name" />
                                                                    <ext:ModelField Name="PlanID" Type="String" Mapping="PlanID" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                        <Listeners>
                                                            <%--<Load Handler="#{cboMaterName}.setValue(#{cboMaterName}.store.getAt(0).get('Id'));" />--%>
                                                        </Listeners>
                                                    </ext:Store>
                                                </Store>
                                                <ListConfig Width="320" Height="300" ItemSelector=".x-boundlist-item">
                                                    <Tpl ID="Tpl2" runat="server">
                                                        <Html>
                                                            <tpl for=".">
						                                        <tpl if="[xindex] == 1">
							                                        <table class="cbStates-list">
								                                        <tr>
									                                         <th>物料名称</th> 
									                                         <th>计划号</th>
								                                        </tr>
						                                        </tpl>
						                                        <tr class="x-boundlist-item">
							                                        <td>{Name}</td>
							                                        <td>{PlanID}</td>
						                                        </tr>
						                                        <tpl if="[xcount-xindex]==0">
							                                        </table>
						                                        </tpl>
					                                        </tpl>
                                                        </Html>
                                                    </Tpl>
                                                </ListConfig>
                                                <Listeners>
                                                    <Select Handler="this.getTrigger(0).show();" />
                                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { 
                                                                           this.clearValue(); 
                                                                           this.getTrigger(0).hide();
                                                                       }" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container13" runat="server" Layout="FormLayout" ColumnWidth=".66"
                                        Padding="5">
                                        <Items>
                                            <ext:Label ID="planState" Html="状态说明:<span style='color:Red;padding-right:20px;padding-left:20px;font-weight: bold;'>手工增加条码</span><span style='color:Blue;padding-right:20px;font-weight: bold;'>条码未打印</span><span style='color:Green;padding-right:20px;font-weight: bold;'>手工拆分条码</span><span style='color:Purple;padding-right:20px;font-weight: bold;'>手工修改条码</span>"
                                                runat="server" ColumnWidth=".7" Padding="2">
                                            </ext:Label>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:GridPanel ID="gpShiftQuery" runat="server" Region="Center" Layout="FitLayout">
                <Store>
                    <ext:Store ID="Store1" runat="server" PageSize="15">
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="Model2" runat="server">
                                <Fields>
                                    <ext:ModelField Name="PlanDate" Type="Date" />
                                    <ext:ModelField Name="EquipName" Type="String" />
                                    <ext:ModelField Name="ShiftID" Type="String" />
                                    <ext:ModelField Name="ClassID" Type="String" />
                                    <ext:ModelField Name="MaterialName" Type="String" />
                                    <ext:ModelField Name="Barcode" Type="String" />
                                    <ext:ModelField Name="BarcodeSE" Type="String" />
                                    <ext:ModelField Name="SetTotalWeight" Type="Float" />
                                    <ext:ModelField Name="RealWeight" Type="Float" />
                                    <ext:ModelField Name="ConfigDValue" />
                                    <ext:ModelField Name="OperCode" Type="String" />
                                    <ext:ModelField Name="ReceiveDate" Type="Date" />
                                    <ext:ModelField Name="DiffTime" Type="Float" />
                                    <ext:ModelField Name="MemNote" Type="String" />
                                     <ext:ModelField Name="LLMemNote" Type="String" />
                                    <ext:ModelField Name="UsedFlag" Type="String" />
                                    <ext:ModelField Name="PrintDate" Type="Date" />
                                    <ext:ModelField Name="ShelfNum" Type="Int" />
                                    <ext:ModelField Name="UsedNum" Type="Int" />
                                    <ext:ModelField Name="UsedWeigh" Type="Float" />
                                    <ext:ModelField Name="UpdateFlag" Type="String" />
                                    <ext:ModelField Name="BarcodeUse" Type="Int" />
                                    <ext:ModelField Name="OrgOrNot" Type="String" />
                                     <ext:ModelField Name="MainHanderCode" Type="String" />
                                     <ext:ModelField Name="UserName" Type="String" />
                                      <ext:ModelField Name="RecordName" Type="String" />
                                 
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="ColumnModel1" runat="server">
                    <Columns>
                        <ext:DateColumn ID="DateColumn1" runat="server" Text="生产日期" Width="80" DataIndex="PlanDate"
                            Format="yyyy-MM-dd" />
                        <ext:Column ID="Column3" runat="server" Text="机台" Width="80" DataIndex="EquipName">
                        </ext:Column>
                        <ext:Column ID="Column4" runat="server" Text="班次" Width="40" DataIndex="ShiftID">
                        </ext:Column>
                        <ext:Column ID="Column5" runat="server" Text="班组" Width="40" DataIndex="ClassID">
                        </ext:Column>
                        <ext:Column ID="Column2" runat="server" Text="物料名称" Width="150" DataIndex="MaterialName">
                        </ext:Column>
                        <ext:SummaryColumn ID="Column8" runat="server" Text="条码" Width="150" DataIndex="Barcode"
                            SummaryType="Count">
                            <SummaryRenderer Handler="return ((value === 0 || value > 1) ? '(' + value +' 条记录)' : '(1 条记录)');" />
                        </ext:SummaryColumn>
                        <ext:Column ID="Column9" runat="server" Text="起止车次" Width="60" DataIndex="BarcodeSE">
                        </ext:Column>
                         <ext:Column ID="Column15" runat="server" Text="玲珑车次" Width="60" DataIndex="LLMemNote">
                        </ext:Column>
                        <ext:SummaryColumn ID="Column10" runat="server" Text="设定总重" Width="60" DataIndex="SetTotalWeight"
                            SummaryType="Sum">
                        </ext:SummaryColumn>
                        <ext:SummaryColumn ID="Column11" runat="server" Text="实际重量" Width="60" DataIndex="RealWeight"
                            SummaryType="Sum">
                        </ext:SummaryColumn>
                        <ext:Column ID="Column12" runat="server" Text="架子差值" Width="60" DataIndex="ConfigDValue">
                        </ext:Column>
                        <ext:Column ID="Column13" runat="server" Text="接片人" Width="50" DataIndex="OperCode">
                        </ext:Column>
                        <ext:DateColumn ID="Column6" runat="server" Width="130" Text="接片时间" DataIndex="ReceiveDate"
                            Format="yyyy-MM-dd HH:mm:ss">
                            <%--<Renderer Fn="changePriceStyle" />--%>
                        </ext:DateColumn>
                        <%--<ext:Column ID="Column14" runat="server" Text="间隔时间" Width="80" DataIndex="DiffTime">
                        </ext:Column>
                        <ext:Column ID="Column15" runat="server" Text="备注" Width="80" DataIndex="MemNote">
                        </ext:Column>
                        <ext:Column ID="Column16" runat="server" Text="使用标志" Width="60" DataIndex="UsedFlag">
                        </ext:Column>--%>
                        <ext:DateColumn ID="Column7" runat="server" Text="打印时间" Width="130" DataIndex="PrintDate"
                            Format="yyyy-MM-dd HH:mm:ss">
                        </ext:DateColumn>
                        <ext:Column ID="Column18" runat="server" Text="数量" Width="40" DataIndex="ShelfNum">
                        </ext:Column>
                        <%--<ext:Column ID="Column19" runat="server" Text="使用数量" Width="60" DataIndex="UsedNum">
                        </ext:Column>
                        <ext:Column ID="Column17" runat="server" Text="使用重量" Width="60" DataIndex="UsedWeigh">
                        </ext:Column>--%>
                          <ext:Column ID="Column100" runat="server" Text="修改人" Width="40" DataIndex="RecordName">
                        </ext:Column>
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
                            </Commands>
                            <Listeners>
                                <Command Handler="return commandcolumn_click(command, record);" />
                            </Listeners>
                        </ext:CommandColumn>
                          <ext:Column ID="Column1" runat="server" Text="主机手代码" Width="40" DataIndex="MainHanderCode">
                        </ext:Column>
                          <ext:Column ID="Column14" runat="server" Text="主机手" Width="40" DataIndex="UserName">
                        </ext:Column>
                    </Columns>
                </ColumnModel>
                <SelectionModel>
                    <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Multi" />
                </SelectionModel>
                <View>
                    <ext:GridView ID="GridView1" runat="server" StripeRows="true">
                        <GetRowClass Fn="getRowClass" />
                    </ext:GridView>
                </View>
                <Features>
                    <ext:GroupingSummary ID="GroupingSummary1" runat="server" GroupHeaderTplString="{plandate}"
                        HideGroupedHeader="true" EnableGroupingMenu="false" />
                </Features>
                <BottomBar>
                    <ext:PagingToolbar ID="pageToolBar" runat="server">
                        <Plugins>
                            <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                        </Plugins>
                    </ext:PagingToolbar>
                </BottomBar>
                <Listeners>
                    <%-- <CellClick Handler="#{Window1}.show();" />--%>
                </Listeners>
            </ext:GridPanel>
            <ext:Hidden ID="hidden_equip_type_name" runat="server" />
            <ext:Hidden ID="hidden_delete_flag" runat="server" Text="0">
            </ext:Hidden>
        </Items>
    </ext:Viewport>
    <ext:Window ID="AddConfigWin" runat="server" Icon="MonitorAdd" Closable="false" Title="添加架子信息"
        Width="580" Height="480" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                    <ext:Container ID="Container4" runat="server" AutoHeight="true">
                        <Items>
                            <ext:FieldSet ID="FieldSet1" runat="server" Title="基本信息" Layout="AnchorLayout" DefaultAnchor="100%"
                                Padding="5">
                                <Items>
                                    <ext:Container ID="Container3" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:DateField ID="txtAddConfigDate" runat="server" Flex="1" Editable="false" Vtype="daterange"
                                                FieldLabel="生产日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd">
                                                <Listeners>
                                                    <Change Handler="App.cboAddMaterName.setValue('');App.cboAddMaterName.store.reload();">
                                                    </Change>
                                                </Listeners>
                                            </ext:DateField>
                                            <ext:ComboBox ID="cboAddShif" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"
                                                runat="server" Flex="1" Editable="false" FieldLabel="班次" LabelAlign="Right">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.getTrigger(0).show();App.cboAddMaterName.setValue('');App.cboAddMaterName.store.reload();" />
                                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { 
                                                                           this.clearValue(); 
                                                                           this.getTrigger(0).hide();App.cboAddMaterName.setValue('');App.cboAddMaterName.store.reload();
                                                                       }" />
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container114" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:TriggerField ID="txtAddEquipCode" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"
                                                runat="server" Flex="1" Editable="false" FieldLabel="机台" LabelAlign="Right">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="SelectEquipID" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:ComboBox ID="cboAddMaterName" runat="server" Flex="1" DisplayField="Name" ValueField="PlanID"
                                                Editable="false" FieldLabel="物料" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"
                                                LabelAlign="Right" TypeAhead="true" MinChars="1" MatchFieldWidth="false" ForceSelection="true">
                                                <Store>
                                                    <ext:Store runat="server" ID="AddMaterNameStore" AutoLoad="true" OnReadData="AddMaterNameRefresh">
                                                        <Model>
                                                            <ext:Model ID="Model4" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="Id" Type="Int" Mapping="Id" />
                                                                    <ext:ModelField Name="Name" Type="String" Mapping="Name" />
                                                                    <ext:ModelField Name="PlanID" Type="String" Mapping="PlanID" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <ListConfig Width="250" Height="300" ItemSelector=".x-boundlist-item">
                                                    <Tpl ID="Tpl1" runat="server">
                                                        <Html>
                                                            <tpl for=".">
						                                        <tpl if="[xindex] == 1">
							                                        <table class="cbStates-list">
								                                        <tr>
									                                         <th>物料名称</th> 
									                                         <th>计划号</th>
								                                        </tr>
						                                        </tpl>
						                                        <tr class="x-boundlist-item">
							                                        <td>{Name}</td>
							                                        <td>{PlanID}</td>
						                                        </tr>
						                                        <tpl if="[xcount-xindex]==0">
							                                        </table>
						                                        </tpl>
					                                        </tpl>
                                                        </Html>
                                                    </Tpl>
                                                </ListConfig>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.getTrigger(0).show();App.direct.GetPlanNo();" />
                                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { 
                                                                           this.clearValue(); 
                                                                           this.getTrigger(0).hide();App.direct.GetPlanNo();
                                                                       }" />
                                                                       
                                                </Listeners>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container6" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                                  <ext:ComboBox ID="CombZJ" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"
                                                runat="server" Flex="1" Editable="false" FieldLabel="主机手" LabelAlign="Right">
                                             
                                            </ext:ComboBox>
<%--                                            <ext:Label ID="Label2" Flex="1" runat="server">
                                            </ext:Label>--%>
                                            <ext:TextField ID="lblPlanNo" Flex="1" FieldLabel="计划编号" LabelAlign="Right" Disabled="true"
                                                runat="server">
                                            </ext:TextField>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FieldSet>
                            <ext:FieldSet ID="FieldSet2" runat="server" Title="其他信息" Layout="AnchorLayout" DefaultAnchor="100%"
                                Padding="5">
                                <Items>
                                    <ext:Container ID="Container10" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:NumberField ID="startNum" Flex="1" runat="server" FieldLabel="起始车次号" Text="1"
                                                MinValue="1" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text">
                                                <Listeners>
                                                    <Change Fn="ChangeNum" />
                                                </Listeners>
                                            </ext:NumberField>
                                            <ext:NumberField ID="endNum" Flex="1" runat="server" FieldLabel="终止车次号" Text="1"
                                                MinValue="1" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text">
                                                <Listeners>
                                                    <Change Fn="ChangeNum" />
                                                </Listeners>
                                            </ext:NumberField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container11" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:TextField ID="totalWeight" runat="server" Flex="1" Disabled="true" FieldLabel="单车设定重量"
                                                Vtype="integer" LabelAlign="Right" Enabled="true" />
                                            <ext:NumberField ID="setNum" Flex="1" Disabled="true" runat="server" FieldLabel="设定车数"
                                                LabelAlign="Right" IndicatorCls="red-text" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container12" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:NumberField ID="realWeight" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"
                                                runat="server" Flex="1" FieldLabel="实际称量重量" LabelAlign="Right" DecimalPrecision="3"
                                                Enabled="true">
                                            </ext:NumberField>
                                            <ext:TriggerField ID="operUser" runat="server" Flex="1" AllowBlank="false" IndicatorText="*"
                                                IndicatorCls="red-text" FieldLabel="接片人" LabelAlign="Right" Enabled="true" Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="SelectUserID" />
                                                </Listeners>
                                            </ext:TriggerField>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FieldSet>
                            <ext:FieldSet ID="FieldSet3" runat="server" Title="备注" Layout="AnchorLayout" DefaultAnchor="100%"
                                Padding="5">
                                <Items>
                                    <ext:Container ID="Container9" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:TextArea ID="txtMemNote" runat="server" Flex="1">
                                            </ext:TextArea>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FieldSet>
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
            <Show Handler="for(var i=0;i<#{Viewport1}.items.length;i++){#{Viewport1}.getComponent(i).disable(true);}" />
            <Hide Handler="for(var i=0;i<#{Viewport1}.items.length;i++){#{Viewport1}.getComponent(i).enable(true);}" />
        </Listeners>
    </ext:Window>
    <ext:Window ID="ModifyConfigWin" runat="server" Closable="false" Icon="Add" Title="修改架子信息"
        Width="350" Height="240" Hidden="true" Modal="false">
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" BodyPadding="5">
                <FieldDefaults>
                    <CustomConfig>
                        <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                        <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                    </CustomConfig>
                </FieldDefaults>
                <Items>
                    <ext:Container ID="Container14" runat="server" AutoHeight="true">
                        <Items>
                            <ext:FieldSet ID="FieldSet5" runat="server" Title="其他信息" Layout="AnchorLayout" DefaultAnchor="100%"
                                Padding="5">
                                <Items>
                                    <ext:Container ID="Container18" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:NumberField ID="modStartNum" Flex="1" runat="server" FieldLabel="起始车次号" Text="1"
                                                MinValue="1" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text">
                                            </ext:NumberField>
                                            <ext:NumberField ID="modEndNum" Flex="1" runat="server" FieldLabel="终止车次号" Text="1"
                                                MinValue="1" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text">
                                            </ext:NumberField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container20" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>
                                            <ext:NumberField ID="modRealWeight" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"
                                                runat="server" Flex="1" FieldLabel="实际称量重量" LabelAlign="Right" DecimalPrecision="3"
                                                Enabled="true">
                                            </ext:NumberField>
                                        </Items>
                                    </ext:Container>

                                      <ext:Container ID="Container15" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>

                                     <ext:ComboBox ID="EditZJ" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"
                                                runat="server" Flex="1" Editable="false" FieldLabel="主机手" LabelAlign="Right">
                                             
                                            </ext:ComboBox>
                                             </Items>
                                    </ext:Container>
                                     <ext:Container ID="Container16" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                        <Items>

                                  <ext:TextField ID="TextLLMEM" Flex="1" FieldLabel="玲珑车次" LabelAlign="Right" 
                                                runat="server">
                                            </ext:TextField>
                                             </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FieldSet>
                        </Items>
                    </ext:Container>
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
