<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RubberBack.aspx.cs" Inherits="Manager_Rubber_RubberBack" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell
        {
        	background-color: #FF8C69 !important;
        }
    </style>
    <script type="text/javascript">
        var pnlListFresh = function () {
            if (App.txtBeginTime.getValue() > App.txtEndTime.getValue()) {
                Ext.Msg.alert('操作', '开始时间不能大于结束时间！');
                return false;
            }
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        var SetRubberBack = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要审核退回吗？', function (btn) { commandcolumn_direct_setbackflag(btn) });
            }
        }

        var commandcolumn_direct_setbackflag = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnBatchBack_Click({
                success: function (result) {
                    if (result == "false")
                        Ext.Msg.alert('操作', "操作失败！");
                    else if (result == "OK")
                        Ext.Msg.alert('操作', "退回成功！");
                    else {
                        Ext.Msg.alert('操作', result);
                    }
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var CancelRubberBack = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要撤销退回吗？', function (btn) { commandcolumn_direct_cancelrubberback(btn) });
            }
        }

        var commandcolumn_direct_cancelrubberback = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnCancelRubberBack_Click({
                success: function (result) {
                    if (result == "false")
                        Ext.Msg.alert('操作', "操作失败！");
                    else if (result == "OK")
                        Ext.Msg.alert('操作', "撤销成功！");
                    else
                        Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var QueryEquipInfo = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenEquipCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
            }
        }
        var QueryMaterial = function (field, trigger, index) {//物料查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenMaterCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
                    break;
            }
        }

        Ext.create("Ext.window.Window", {//机台代码查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择机台名称",
            modal: true
        })
        Ext.create("Ext.window.Window", {//物料带窗体
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料",
            modal: true
        })

        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {//机台代码退回值处理
            App.txtEquipCode.setValue(record.data.EquipName);
            App.txtEquipCode.getTrigger(0).show();
            App.hiddenEquipCode.setValue(record.data.EquipCode);
            App.pageToolBar.doRefresh();
        }

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息退回值处理
            App.txtMaterName.getTrigger(0).show();
            App.txtMaterName.setValue(record.data.MaterialName);
            App.hiddenMaterCode.setValue(record.data.MaterialCode);
            App.pageToolBar.doRefresh();
        }

        var backFlagChange = function (value) {
            return Ext.String.format(value == "1" ? "已退回" : "未退");
        };

        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("BackFlag") == "1") {
                return "x-grid-row-collapsed";
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="rmRubberBack" runat="server" />
        <ext:Viewport ID="vpRubberBack" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnRubberBack" runat="server" Region="North" Height="90">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="tbRubberBack">
                            <Items>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <Listeners>
                                        <Click Fn="pnlListFresh" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsBegin" />
                                <ext:Button runat="server" Icon="LockEdit" Text="审核退回" ID="Button1">
                                    <%--<Listeners>
                                        <Click Handler="SetRubberBack();" />
                                    </Listeners>--%>
                                    <DirectEvents>
                                        <Click OnEvent="btnLockEdit_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsMiddle" />
                                <ext:Button runat="server" Icon="LockEdit" Text="撤销退回" ID="Button2">
                                    <Listeners>
                                        <Click Handler="CancelRubberBack();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsEnd" />
                                <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                                <ext:ToolbarFill ID="tfEnd" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                            <Items>
                                <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                        <ext:TextField ID="txtBarcode" runat="server" FieldLabel="条码号" LabelAlign="Right" />
                                        <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" OnDirectChange="Query_Change" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                        <ext:TriggerField ID="txtEquipCode" runat="server" FieldLabel="机台" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryEquipInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" OnDirectChange="Query_Change" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                        <ext:TriggerField ID="txtMaterName" runat="server" FieldLabel="物料名称" LabelAlign="Right"
                                            Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryMaterial" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:ComboBox ID="cbxShiftClass" runat="server" OnDirectChange="Query_Change" FieldLabel="班组" LabelAlign="Right">
                                            <Items>
                                                <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                                <ext:ListItem Text="甲组" Value="1"></ext:ListItem>
                                                <ext:ListItem Text="乙组" Value="2"></ext:ListItem>
                                                <ext:ListItem Text="丙组" Value="3"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container4" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="cbxBackFlag" OnDirectChange="Query_Change" runat="server" FieldLabel="是否退回" LabelAlign="Right">
                                            <SelectedItems>
                                                <ext:ListItem Value="0">
                                                </ext:ListItem>
                                            </SelectedItems>
                                            <Items>
                                                <ext:ListItem Text="全部" Value="all" AutoDataBind="true">
                                                </ext:ListItem>
                                                <ext:ListItem Text="是" Value="1">
                                                </ext:ListItem>
                                                <ext:ListItem Text="否" Value="0">
                                                </ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnSearch}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel2" runat="server" Region="Center" Frame="true" Layout="Fit" MarginsSummary="0 5 0 5">
                    <Items>
                        <ext:GridPanel ID="pnlList" runat="server">
                            <Store>
                                <ext:Store ID="store" runat="server" PageSize="15">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model" runat="server" IDProperty="Barcode">
                                            <Fields>
                                                <ext:ModelField Name="Barcode" />
                                                <ext:ModelField Name="MaterCode" />
                                                <ext:ModelField Name="MaterialName" />
                                                <ext:ModelField Name="ValidDate" />
                                                <ext:ModelField Name="ShiftID" />
                                                <ext:ModelField Name="ShiftName" />
                                                <ext:ModelField Name="ShiftClassID" />
                                                <ext:ModelField Name="ClassName" />
                                                <ext:ModelField Name="EquipCode" />
                                                <ext:ModelField Name="EquipName" />
                                                <ext:ModelField Name="BarcodeStart" />
                                                <ext:ModelField Name="BarcodeEnd" />
                                                <ext:ModelField Name="RealWeight" Type="Float" />
                                                <ext:ModelField Name="BackFlag" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="colModel" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                    <ext:Column ID="Barcode" runat="server" Text="条码号" DataIndex="Barcode" Width="140" />
                                    <ext:Column ID="BarcodeStart" runat="server" Text="开始车次" DataIndex="BarcodeStart" Flex="1" />
                                    <ext:Column ID="BarcodeEnd" runat="server" Text="截止车次" DataIndex="BarcodeEnd" Flex="1" />
                                    <ext:Column ID="EquipName" runat="server" Text="机台" DataIndex="EquipName" Flex="1" />
                                    <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                    <ext:Column ID="ClassName" runat="server" Text="班组" DataIndex="ClassName" Flex="1" />
                                    <ext:DateColumn ID="ValidDate" runat="server" Text="保质期" DataIndex="ValidDate" Flex="1" Format="yyyy-MM-dd" />
                                    <ext:Column ID="RealWeight" runat="server" Text="实际重量" DataIndex="RealWeight" Flex="1" />
                                    <ext:Column ID="BackFlag" runat="server" Text="退回状态" DataIndex="BackFlag" Flex="1">
                                        <Renderer Fn="backFlagChange" />
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                                    <DirectEvents>
                                        <SelectionChange OnEvent="rowSelectMuti_SelectionChange">
                                            <ExtraParams>
                                                <ext:Parameter Name="Barcode" Value="selected[0].get('Barcode')" Mode="Raw" />
                                                <ext:Parameter Name="BackFlag" Value="selected[0].get('BackFlag')" Mode="Raw" />
                                                <ext:Parameter Name="RealWeight" Value="selected[0].get('RealWeight')" Mode="Raw" />
                                            </ExtraParams>
                                        </SelectionChange>
                                    </DirectEvents>
                                </ext:RowSelectionModel>
                            </SelectionModel>
                            <View>
                                <ext:GridView ID="gvRows" runat="server">
                                    <GetRowClass Fn="SetRowClass" />
                                </ext:GridView>
                            </View>
                            <BottomBar>
                                <ext:PagingToolbar ID="pageToolBar" runat="server">
                                    <Items>
                                        <ext:Label ID="Label1" runat="server" Text="每页条数:" />
                                        <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                                        <ext:ComboBox ID="ComboBox1" runat="server" Width="80">
                                            <Items>
                                                <ext:ListItem Text="15" />
                                                <ext:ListItem Text="50" />
                                                <ext:ListItem Text="100" />
                                                <ext:ListItem Text="200" />
                                            </Items>
                                            <SelectedItems>
                                                <ext:ListItem Value="15" />
                                            </SelectedItems>
                                            <Listeners>
                                                <Select Handler="#{pnlList}.store.pageSize = parseInt(this.getValue(), 10); #{pageToolBar}.doRefresh(); return false;" />
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                    <Plugins>
                                        <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>

                <ext:Window ID="winSet" runat="server" Icon="MonitorEdit" Closable="false" Title="设置退回信息"
                    Width="320" Height="220" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                <ext:ComboBox ID="cbxBackType" runat="server" FieldLabel="退库类型" OnDirectChange="cbxBackType_Change" LabelAlign="Right">
                                    <Items>
                                        <ext:ListItem Text="正常退库" Value="1"></ext:ListItem>
                                        <ext:ListItem Text="异常退库" Value="0"></ext:ListItem>
                                    </Items>
                                </ext:ComboBox>
                                <ext:ComboBox ID="cbxBackReason" runat="server" FieldLabel="退库原因" Editable="false" AllowBlank="false" LabelAlign="Right" Disabled="true" DisplayField="ReasonName" ValueField="ReasonID">
                                    <Store>
                                        <ext:Store runat="server" ID="storeBackReason">
                                            <Model>
                                                <ext:Model runat="server" ID="mBackReason">
                                                    <Fields>
                                                        <ext:ModelField Name="ReasonID" />
                                                        <ext:ModelField Name="ReasonName" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                </ext:ComboBox>
                                <ext:NumberField ID="txtBackWeight" runat="server" FieldLabel="退回重量" LabelAlign="Right" MinValue="0" AllowDecimals="true" DecimalPrecision="3" AllowBlank="false" Disabled="true" /> 
                                <ext:ComboBox ID="cbxStorage" runat="server" FieldLabel="退至库房" AllowBlank="false" Editable="false" LabelAlign="Right" Disabled="true">
                                    <Items>
                                        <ext:ListItem Text="M2不合格胶库" Value="008005"></ext:ListItem>
                                        <ext:ListItem Text="M3不合格胶库" Value="009005"></ext:ListItem>
                                        <ext:ListItem Text="M4不合格胶库" Value="010005"></ext:ListItem>
                                        <ext:ListItem Text="M5不合格胶库" Value="011005"></ext:ListItem>
                                    </Items>
                                </ext:ComboBox>
                            </Items>
                             <Listeners>
                                <ValidityChange Handler="#{btnSubmitBack}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnSubmitBack" runat="server" Text="确定" Icon="Accept">
                            <Listeners>
                                <Click Handler="SetRubberBack();" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vpRubberBack}.items.length;i++){#{vpRubberBack}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vpRubberBack}.items.length;i++){#{vpRubberBack}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>

            </Items>
        </ext:Viewport>
        <ext:Hidden ID="hiddenEquipCode" runat="server" />
        <ext:Hidden ID="hiddenMaterCode" runat="server" />
        <ext:Hidden ID="hiddenBarcode" runat="server" />
        <ext:Hidden ID="hiddenBackFlag" runat="server" />
    </form>
</body>
</html>
