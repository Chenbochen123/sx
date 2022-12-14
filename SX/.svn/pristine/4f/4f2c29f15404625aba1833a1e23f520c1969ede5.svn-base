<%@ page language="C#" autoeventwireup="true" inherits="Manager_Equipment_StopManage_StopRecord, App_Web_whc5u0u2" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>停机记录</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell
        {
        	background-color: #CCFF66 !important;
        }
    </style>
    <script type="text/javascript">
        //点击修改按钮
        var cmdcol_click = function (command, record) {
            if (command == "Edit") {
                App.direct.pnlList_Edit(record.data.ObjID, {
                    success: function () { },
                    failure: function () { }
                });
            }
            else if (command == "Delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此信息？删除后将不能恢复！', function (btn) { deleteStopRecord(btn, record) });
            }
            else if (command == "Add") {
                App.direct.pnlList_Add(record.data.ObjID, {
                    success: function () { },
                    failure: function () { }
                });
            }
        }

        var deleteStopRecord = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ObjID = record.data.ObjID;
            App.direct.pnlList_Delete(ObjID, {
                success: function () { },
                failure: function () { }
            });
        }

        var Manager_BasicInfo_CommonPage_QueryBasUsers_Request = function (result) {
            App.txtMaintainers.setValue(result);
        }
        var QueryBasUsers = function () {
            App.Manager_BasicInfo_CommonPage_QueryBasUsers_Window.show();
        }
        Ext.create("Ext.window.Window", {//人员查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasUsers_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryBasUsers.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择维修人",
            modal: true
        })
    </script>
    <script type="text/javascript">
        var Manager_BasicInfo_CommonPage_QueryEqmStopType_Request = function (result) {
            App.cbType.setValue(result.data.TypeName);
            App.hidden_stop_type.setValue(result.data.TypeCode);
        }
        var QueryEqmStopType = function () {
            App.Manager_BasicInfo_CommonPage_QueryEqmStopType_Window.show();
        }
        Ext.create("Ext.window.Window", {//停机类型查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryEqmStopType_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryEqmStopType.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择停机类型",
            modal: true
        })
    </script>
    <script type="text/javascript">
        var Manager_BasicInfo_CommonPage_QueryEqmStopFault_Request = function (result) {
            App.cbFault.setValue(result.data.FaultName);
            App.hidden_stop_fault.setValue(result.data.FaultCode);
        }
        var QueryEqmStopFault = function () {
            var url = "../../BasicInfo/CommonPage/QueryEqmStopFault.aspx?TypeID=" + App.hidden_stop_type.getValue();
            var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (App.Manager_BasicInfo_CommonPage_QueryEqmStopFault_Window.getBody()) {
                App.Manager_BasicInfo_CommonPage_QueryEqmStopFault_Window.getBody().update(html);
            } else {
                App.Manager_BasicInfo_CommonPage_QueryEqmStopFault_Window.html = html;
            }
            App.Manager_BasicInfo_CommonPage_QueryEqmStopFault_Window.show();
        }
        Ext.create("Ext.window.Window", {//故障类型查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryEqmStopFault_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryEqmStopFault.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择停机故障点",
            modal: true
        })
    </script>
    <script type="text/javascript">
         var Manager_BasicInfo_CommonPage_QueryEqmFaultReason_Request = function (result) {
             App.direct.setFaultReason(result,{
                 success: function (result) {
                 },
                 failture: function () {
                 }
             });

         }
         var QueryEqmFaultReason = function () {
             var url = "../../BasicInfo/CommonPage/QueryEqmFaultReason.aspx?FaultID=" + App.hidden_stop_fault.getValue();
             var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
             if (App.Manager_BasicInfo_CommonPage_QueryEqmFaultReason_Window.getBody()) {
                 App.Manager_BasicInfo_CommonPage_QueryEqmFaultReason_Window.getBody().update(html);
             } else {
                 App.Manager_BasicInfo_CommonPage_QueryEqmFaultReason_Window.html = html;
             }
             App.Manager_BasicInfo_CommonPage_QueryEqmFaultReason_Window.show();
         }
         Ext.create("Ext.window.Window", {//停机类型查询带回窗体
             id: "Manager_BasicInfo_CommonPage_QueryEqmFaultReason_Window",
             height: 460,
             hidden: true,
             width: 450,
             html: "<iframe src='../../BasicInfo/CommonPage/QueryEqmFaultReason.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
             bodyStyle: "background-color: #fff;",
             closable: true,
             title: "请选择故障点原因",
             modal: true
         })

         var SetRowClass = function (record, rowIndex, rowParams, store) {
             if (record.get("IsReportMaintain") == "1") {
                 return "x-grid-row-collapsed";
             }
         }
    </script>
    <script type="text/javascript">

        var stopReasonDeal = function (si, item) {
            if (si != null) {
                App.direct.stopReasonDeal(si, {
                    success: function (result) {
                        return result;
                    },
                    failture: function () {
                        return "";
                    }
                });
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" runat="server" Text="Button" OnClick="btnExportSubmit_Click" Style="display:none" />
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Hidden ID="hidden_stop_type" runat="server" />
    <ext:Hidden ID="hidden_stop_fault" runat="server" />
    <ext:Hidden ID="hidden_fault_reason" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlStopType" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barStopType">
                            <Items>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips><ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page"></EventMask>
                                    </Click></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                    <ToolTips><ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行结果导出" /></ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Container runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="Container1"  runat="server" Layout="FormLayout" ColumnWidth=".25">
                                            <Items>
                                                <ext:FieldContainer ID="FieldContainer9"  runat="server" Layout="HBoxLayout" FieldLabel="开始时间" LabelAlign="Right">
                                                    <Items>
                                                        <ext:DateField ID="dStartDate" runat="server" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="100"/>
                                                        <ext:TimeField ID="dStartTime" runat="server" Width="65"/>
                                                    </Items>
                                                </ext:FieldContainer>
                                                <ext:FieldContainer ID="FieldContainer5"  runat="server" Layout="HBoxLayout" FieldLabel="结束时间" LabelAlign="Right">
                                                    <Items>
                                                        <ext:DateField ID="dEndDate" runat="server" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="100"/>
                                                        <ext:TimeField ID="dEndTime" runat="server" Width="65"/>
                                                    </Items>
                                                </ext:FieldContainer>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container2"  runat="server" Layout="FormLayout" ColumnWidth=".23">
                                            <Items>
                                                <ext:ComboBox ID="cbWorkShop" runat="server" FieldLabel="车间"  LabelAlign="Right" LabelWidth="60" Width="180" DisplayField="WorkShopName" ValueField="ObjID" Editable="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storeWorkShop">
                                                            <Model>
                                                                <ext:Model runat="server" ID="mWorkShop">
                                                                    <Fields>
                                                                        <ext:ModelField Name="ObjID" />
                                                                        <ext:ModelField Name="WorkShopName" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <DirectEvents>
                                                        <Change OnEvent="cbWorkShop_SelectChanged" />
                                                    </DirectEvents>
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <Select Handler="this.getTrigger(0).show();" />
                                                        <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:ComboBox ID="cbStopEquip" runat="server" FieldLabel="机台"  LabelAlign="Right" LabelWidth="60" Width="180" DisplayField="EquipName" ValueField="EquipCode" Editable="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storeEquip">
                                                            <Model>
                                                                <ext:Model runat="server" ID="mEquip">
                                                                    <Fields>
                                                                        <ext:ModelField Name="EquipCode" />
                                                                        <ext:ModelField Name="EquipName" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
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
                                        <ext:Container ID="Container3"  runat="server" Layout="FormLayout" ColumnWidth=".25" Border="true">
                                            <Items>
                                                <ext:ComboBox ID="cbStopMainType" runat="server" FieldLabel="停机大类"  LabelAlign="Right" Width="280" LabelWidth="75" DisplayField="ItemName" ValueField="ItemCode" Editable="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storeStopMainType">
                                                            <Model>
                                                                <ext:Model runat="server" ID="Model1">
                                                                    <Fields>
                                                                        <ext:ModelField Name="ItemCode" />
                                                                        <ext:ModelField Name="ItemName" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <DirectEvents>
                                                        <Change OnEvent="cbStopMainType_SelectChanged" />
                                                    </DirectEvents>
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <Select Handler="this.getTrigger(0).show();" />
                                                        <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                        <TriggerClick Handler="if (index == 0) { this.clearValue(); this.getTrigger(0).hide();}" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                                <ext:ComboBox ID="cbStopType" runat="server" FieldLabel="停机类型" LabelAlign="Right" Width="280" LabelWidth="75" DisplayField="TypeName" ValueField="TypeCode" Editable="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storeStopType">
                                                            <Model>
                                                                <ext:Model runat="server" ID="Model3">
                                                                    <Fields>
                                                                        <ext:ModelField Name="TypeCode" />
                                                                        <ext:ModelField Name="TypeName" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <DirectEvents>
                                                        <Change OnEvent="cbStopType_SelectChanged" />
                                                    </DirectEvents>
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
                                        <ext:Container ID="Container7"  runat="server" Layout="FormLayout" ColumnWidth=".25" Border="true">
                                            <Items>
                                                <ext:ComboBox ID="cbStopFault" runat="server" FieldLabel="停机故障点"  LabelAlign="Right" Width="280" LabelWidth="75" DisplayField="FaultName" ValueField="FaultCode" Editable="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storeStopFault">
                                                            <Model>
                                                                <ext:Model runat="server" ID="Model4">
                                                                    <Fields>
                                                                        <ext:ModelField Name="FaultCode" />
                                                                        <ext:ModelField Name="FaultName" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
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
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="15">
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="EquipName" />
                                        <ext:ModelField Name="ShiftName" />
                                        <ext:ModelField Name="ClassName" />
                                        <ext:ModelField Name="StartTime" />
                                        <ext:ModelField Name="EndTime" />
                                        <ext:ModelField Name="BeginTime" />
                                        <ext:ModelField Name="OverTime" />
                                        <ext:ModelField Name="DurationMi" />
                                        <ext:ModelField Name="ReportTime" />
                                        <ext:ModelField Name="MaintainStartTime" />
                                        <ext:ModelField Name="MaintainEndTime" />
                                        <ext:ModelField Name="MainTypeName" />
                                        <ext:ModelField Name="TypeName" />
                                        <ext:ModelField Name="FaultName" />
                                        <ext:ModelField Name="StopReason" />
                                        <ext:ModelField Name="DealDesc" />
                                        <ext:ModelField Name="Maintainers" />
                                        <ext:ModelField Name="UserName" />
                                        <ext:ModelField Name="Remark" />
                                        <ext:ModelField Name="StopMainTypeID" />
                                        <ext:ModelField Name="StopTypeID" />
                                        <ext:ModelField Name="FaultID" />
                                        <ext:ModelField Name="ReasonID" />
                                        <ext:ModelField Name="IsReportMaintain" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:Column ID="ObjID" runat="server" Text="ObjID" DataIndex="ObjID" Hidden="true" />
                            <ext:Column ID="clEquip" runat="server" Text="机台" DataIndex="EquipName" Width="100"/>
                            <ext:Column ID="clShiftName" runat="server" Text="班次" DataIndex="ShiftName" Width="40"/>
                            <ext:Column ID="clClassName" runat="server" Text="班组" DataIndex="ClassName" Width="40"/>
                            <ext:Column ID="clStartTime" runat="server" Text="开始时间" DataIndex="BeginTime" Width="135"/>
                            <ext:Column ID="clEndTime" runat="server" Text="结束时间" DataIndex="OverTime" Width="135"/>
                            <ext:Column ID="Column1" runat="server" Text="间隔(分钟)" DataIndex="DurationMi" Width="65" />
                            <ext:Column ID="Column2" runat="server" Text="报修时间" DataIndex="ReportTime" Width="135" Hidden="true"/>
                            <ext:Column ID="Column3" runat="server" Text="维修开始时间" DataIndex="MaintainStartTime" Width="135" Hidden="true"/>
                            <ext:Column ID="Column4" runat="server" Text="维修结束时间" DataIndex="MaintainEndTime" Width="135" Hidden="true"/>
                            <ext:Column ID="Column5" runat="server" Text="停机大类" DataIndex="MainTypeName" Width="80"/>
                            <ext:Column ID="Column6" runat="server" Text="停机类型" DataIndex="TypeName" Width="80"/>
                            <ext:Column ID="Column7" runat="server" Text="故障点" DataIndex="FaultName" Width="120"/>
                            <ext:Column ID="Column10" runat="server" Text="维修人" DataIndex="Maintainers" Width="120"/>
                            <ext:Column ID="Column11" runat="server" Text="记录人" DataIndex="UserName" Width="60"/>
                            <ext:Column ID="Column12" runat="server" Text="备注" DataIndex="Remark" Width="120"/>
                            <ext:CommandColumn ID="cmdCol" runat="server" Align="Center" Text="操作" Width="185">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改"/>
                                    <ext:GridCommand Icon="TableDelete" CommandName="Delete" Text="删除"/>
                                    <ext:GridCommand Icon="Add" CommandName="Add" Text="报修"/>
                                </Commands>
                                <Listeners>
                                    <Command Handler="cmdcol_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                            <Listeners>
                                <Select Handler="#{detailStore}.reload();" Buffer="250" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <View>
                        <ext:GridView ID="gvRows" runat="server">
                            <GetRowClass Fn="SetRowClass" />
                        </ext:GridView>
                    </View>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolBar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server"/>
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
                <ext:GridPanel ID="detailPnl" runat="server" Region="South" AutoScroll="true" Height ="130">
                    <Store>
                        <ext:Store ID="detailStore" runat="server" PageSize="10" OnReadData="RowSelect">
                            <Model>
                                <ext:Model ID="model5" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="ReasonName" />
                                        <ext:ModelField Name="DealDesc" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Parameters>
                                <ext:StoreParameter Name="StopReason" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.StopReason : -1" />
                            </Parameters>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="columnModel1" runat="server">
                        <Columns>
                            <ext:Column ID="Column13" runat="server" Text="故障原因名称" DataIndex="ReasonName" Flex="1" />
                            <ext:Column ID="Column14" runat="server" Text="处理措施" DataIndex="DealDesc" Flex="1"  />
                        </Columns>
                    </ColumnModel>
                </ext:GridPanel>
                <ext:Window ID="winSave" runat="server" Icon="MonitorAdd" Closable="false" Title="维护停机记录" Width="550" Height="500" Resizable="false" Modal="true" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="FormLayout">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5" Layout="FormLayout">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="70" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Hidden runat="server" ID="hideObjID" />
                                <ext:FieldSet runat="server" Title="停机信息" Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:FieldContainer ID="Container5"  runat="server" Layout="HBoxLayout" Padding="5" FieldLabel="机台&班次" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtEquip" runat="server" Disabled="true" Width="100" Margins="0 3 0 0"/>
                                                <ext:TextField ID="txtShift" runat="server" Disabled="true" Width="100"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="Container4"  runat="server" Layout="HBoxLayout" Padding="5" FieldLabel="停机时间" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtStartTime" runat="server" Disabled="true" Width="160" Margins="0 3 0 0"/>
                                                <ext:DisplayField ID="DisplayField1" runat="server" Text="~" Margins="0 3 0 0"/>
                                                <ext:TextField ID="txtEndTime" runat="server" Disabled="true" Width="160"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:FieldSet>
                                <ext:FieldSet ID="FieldSet1" runat="server" Title="维修信息">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer4"  runat="server" Layout="HBoxLayout" AnchorHorizontal="100%" FieldLabel="故障类型" LabelWidth="70">
                                            <Items>
                                                <ext:ComboBox ID="cbFaultType" runat="server" Width="155" DisplayField="ItemName" ValueField="ItemCode">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storeFaultType">
                                                            <Model>
                                                                <ext:Model runat="server" ID="Model8">
                                                                    <Fields>
                                                                        <ext:ModelField Name="ItemCode" />
                                                                        <ext:ModelField Name="ItemName" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
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
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer2"  runat="server" Layout="HBoxLayout" FieldLabel="维修人" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TriggerField ID="txtMaintainers" runat="server" Editable="false" Width="400" Enabled="true">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="QueryBasUsers" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer1" runat="server" FieldLabel="备注">
                                            <Items>
                                                <ext:TextField ID="txtRemark" runat="server" Width="400"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer3"  runat="server" Layout="HBoxLayout" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TriggerField ID="cbType" runat="server" Width="230" FieldLabel="停机类型"  LabelWidth="70" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="QueryEqmStopType" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TriggerField ID="cbFault" runat="server" Width="225" FieldLabel="故障点"  LabelWidth="60" Margins="0 0 0 20" >
                                                     <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="QueryEqmStopFault" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer6" runat="server" FieldLabel="故障原因" >
                                            <Items>
                                               <ext:TriggerField ID="cbReason" runat="server" Width="400" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="QueryEqmFaultReason" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:Container ID="Container6"  runat="server" Layout="HBoxLayout" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:GridPanel ID="gridPanelCenter" runat="server" Region="Center" AutoScroll="true" Height ="130">
                                                    <Store>
                                                        <ext:Store ID="reasonStore" runat="server" PageSize="10">
                                                            <Model>
                                                                <ext:Model ID="model2" runat="server">
                                                                    <Fields>
                                                                        <ext:ModelField Name="ObjID" />
                                                                        <ext:ModelField Name="ReasonName" />
                                                                        <ext:ModelField Name="DealDesc" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <ColumnModel ID="columnModel" runat="server">
                                                        <Columns>
                                                            <ext:Column ID="reason_name" runat="server" Text="故障原因名称" DataIndex="ReasonName" Width="235"  />
                                                            <ext:Column ID="deal_desc" runat="server" Text="处理措施" DataIndex="DealDesc" Width="235"  />
                                                        </Columns>
                                                    </ColumnModel>
                                                </ext:GridPanel>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:FieldSet>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                     <Buttons>
                        <ext:Button ID="btnSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <DirectEvents>
                                <Click OnEvent="btnSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click"/>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:Window>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>