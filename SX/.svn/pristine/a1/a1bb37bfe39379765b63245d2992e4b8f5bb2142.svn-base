<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialCheckReport.aspx.cs"
    Inherits="Manager_RawMaterialQuality_MaterialCheckReport" %>

<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>原材料质检报告</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript" language="javascript">
        // 供应商查询带回窗体
        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryFactory_Window",
            height: 460,
            hidden: true,
            width: 760,
            html: "<iframe src='../BasicInfo/CommonPage/QueryFactory.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择供应商",
            modal: true
        });

        // 供应商返回值处理
        var Manager_BasicInfo_CommonPage_QueryFactory_Request = function (record) {
            var ComboBoxSupplyFac;
            ComboBoxSupplyFac = App.ComboBoxNorthSupplyFac;
            var facId = record.data.ObjID.toString();
            var facName = Ext.util.Format.trim(record.data.FacName);
            if (ComboBoxSupplyFac.findRecordByValue(facId) == false) {
                ComboBoxSupplyFac.insertItem(0, facName, facId);
            }
            ComboBoxSupplyFac.setValue(facId);

        };

        // 查询时清空或选择供应商
        var ComboBoxNorthSupplyFac_TriggerClick = function (item, trigger, index) {
            if (index == 0) {
                // 清空
                App.ComboBoxNorthSupplyFac.setValue("");
            }
            else if (index == 1) {
                App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
            }
        };

        var setRowClass = function (record, index, rowParams, store) {
            if (record.get('AutoCheckResult') == '0') {
                return 'x-grid-row-deleted';
            }
        };

        var GridViewCenterMaster_GetRowClass = function (record, index, rowParams, store) {
            if (record.get("RecordStat") == "1") {
                return "x-grid-row-summary";
            }
            return "";
        };

        var ColumnCenterMasterRecordStatDes_Renderer = function (value, metadata, record, rowIndex, colIndex, store, view) {
            if (record.get("RecordStat") == "1") {
                return "<span style='background-color: lightgreen'>" + value + "</span>";
            }
            else if (record.get("RecordStat") == "0") {
                return "<span style='background-color: yellow'>" + value + "</span>";
            }
            return value;
        };

        var ColumnCenterMasterCheckResultDes_Renderer = function (value, metadata, record, rowIndex, colIndex, store, view) {
            if (record.get("CheckResult") == "0") {
                return "<span style='background-color: red'>" + value + "</span>";
            }
            return value;
        };
    </script>
    <script type="text/javascript">
        //-------人员绑定-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryQmcUser_Request = function (record) {//人员绑定
            var command = App.txtHiddenSelectCommand.getValue();
            switch (command) {
                case "former":
                    {
                        if (!App.windowExportSetting.hidden) {
                            App.trfFormerName.setValue(record.data.UserName);
                        }
                    }
                    break;
                case "confirmer":
                    {
                        if (!App.windowExportSetting.hidden) {
                            App.trfConfirmerName.setValue(record.data.UserName);
                        }
                    }
                    break;
                case "handler":
                    {
                        if (!App.windowGenerateLedger.hidden) {
                            App.trfGenerateLedgerHandlerName.setValue(record.data.UserName);
                            App.txtGenerateLedgerHandlerId.setValue(record.data.HRCode);
                        }
                        if (!App.windowExportSetting.hidden) {
                            App.trfExportHandlerName.setValue(record.data.UserName);
                            App.txtExportHandlerId.setValue(record.data.HRCode);
                        }
                    }
                default:
                    break;
            }
        }
        var SelectFormer = function () {//制表人绑定查询
            App.txtHiddenSelectCommand.setValue("former");
            App.Manager_BasicInfo_CommonPage_QueryQmcUser_Window.show();
        }
        var SelectConfirmer = function () {//审核人绑定查询
            App.txtHiddenSelectCommand.setValue("confirmer");
            App.Manager_BasicInfo_CommonPage_QueryQmcUser_Window.show();
        }
        var SelectHandler = function () {//处置人绑定查询
            App.txtHiddenSelectCommand.setValue("handler");
            App.Manager_BasicInfo_CommonPage_QueryQmcUser_Window.show();
        }
        Ext.create("Ext.window.Window", {//人员绑定查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryQmcUser_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryQmcUser.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择人员",
            modal: true
        });
        //------------查询带回弹出框--END 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="rmMaterialCheckReport" />
    <ext:Viewport runat="server" ID="vwUnit" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" ID="pnlNorth" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="barUnit1">
                        <Items>
                            <ext:Button runat="server" ID="btnSearch" Icon="Find" Text="查询">
                                <ToolTips>
                                    <ext:ToolTip ID="ttSearch" runat="server" Html="点击查询可生成报告的质检记录" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator1" />
                            <ext:Button runat="server" ID="btnBatchReCheck" Text="批量重判级" Icon="DatabaseGear">
                                <DirectEvents>
                                    <Click OnEvent="btn_batchModify_Click" IsUpload="true">
                                        <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="btnGenerateLedger" Text="生成台账" Icon="NoteAdd">
                                <DirectEvents>
                                    <Click OnEvent="btn_generateLedger_Click" IsUpload="true">
                                        <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator2" />
                            <ext:Button runat="server" ID="btnExportPhysical" Text="导出" Icon="PageExcel">
                                <DirectEvents>
                                    <Click OnEvent="btnExport_Click" IsUpload="true">
                                        <ExtraParams>
                                            <ext:Parameter Name="reportType" Value="physical" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                   <%--         <ext:Button runat="server" ID="btnExportChemical" Text="导出（化学模板）" Icon="PageExcel">
                                <DirectEvents>
                                    <Click OnEvent="btnExport_Click" IsUpload="true">
                                        <ExtraParams>  
                                            <ext:Parameter Name="reportType" Value="chemical" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>--%>
                         <%--   <ext:ToolbarSeparator ID="toolbarSeparator_end" />--%>
                            <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                            <ext:ToolbarFill ID="toolbarFill_end" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:FormPanel runat="server" ID="pnlForm" Layout="AnchorLayout" AutoHeight="true">
                        <Items>
                           <ext:Container ID="container1" runat="server" Layout="HBoxLayout" Padding="5" >
                                <Items>
                                   <ext:ComboBox runat="server" ID="ComboBoxNorthMaterMinorType" FieldLabel="原材料分类"
                                        LabelAlign="Right" Editable="false" MatchFieldWidth="false">
                                        <ListConfig Width="250" />
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Handler="this.setValue('');" />
                                        </Listeners>
                                        <DirectEvents>
                                            <Change OnEvent="ComboBoxNorthMaterMinorType_Change">
                                                <EventMask ShowMask="false" />
                                            </Change>
                                        </DirectEvents>
                                    </ext:ComboBox>
                                    <ext:ComboBox runat="server" ID="ComboBoxNorthMater" FieldLabel="原材料型号" LabelAlign="Right"
                                        Editable="false" MatchFieldWidth="false">
                                        <ListConfig Width="250" />
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Handler="this.setValue('');" />
                                        </Listeners>
                                    </ext:ComboBox>
                                    <ext:ComboBox runat="server" ID="ComboBoxNorthSupplyFac" FieldLabel="供应商" LabelAlign="Right"
                                        MatchFieldWidth="false" Editable="false" >
                                        <ListConfig Width="250" />
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                            <ext:FieldTrigger Icon="Search" Qtip="查找" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Fn="ComboBoxNorthSupplyFac_TriggerClick" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="HBoxLayout" Padding="5" >
                                <Items>
                                    <ext:TextField runat="server" ID="TextFieldNorthBarcode" FieldLabel="条码号" LabelAlign="Right" InputWidth="181" >
                                    </ext:TextField>
                                    <ext:DateField runat="server" ID="DateFieldNorthCheckDate" FieldLabel="检验日期" LabelAlign="Right"
                                        Format="yyyy-MM-dd" Editable="false" >
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Handler="this.setValue('');" />
                                        </Listeners>
                                    </ext:DateField>
                                    <ext:ComboBox runat="server" ID="ComboBoxNorthCheckResult" FieldLabel="是否合格" LabelAlign="Right"
                                        Editable="false" MatchFieldWidth="false">
                                        <ListConfig Width="110" />
                                        <Items>
                                            <ext:ListItem Value="1" Text="合格" />
                                            <ext:ListItem Value="0" Text="不合格" />
                                        </Items>
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Handler="this.setValue('');" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="pnlCenter" Region="Center" Layout="BorderLayout">
                <Items>
                   <ext:GridPanel runat="server" ID="GridPanelCenterMaster" Region="Center">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenterMaster" PageSize="10">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenterMaster" IDProperty="CheckId">
                                        <Fields>
                                            <ext:ModelField Name="CheckId" />
                                            <ext:ModelField Name="BillNo" />
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="BatchCode" />
                                            <ext:ModelField Name="OrderID" />
                                            <ext:ModelField Name="MaterCode" />
                                            <ext:ModelField Name="SupplyFac" />
                                            <ext:ModelField Name="ProductFac" />
                                            <ext:ModelField Name="CheckDate" Type="Date" />
                                            <ext:ModelField Name="CheckResult" />
                                            <ext:ModelField Name="Remark" />
                                            <ext:ModelField Name="RecorderId" />
                                            <ext:ModelField Name="RecordTime" Type="Date" />
                                            <ext:ModelField Name="LastModifierId" />
                                            <ext:ModelField Name="LastModifyTime" Type="Date" />
                                            <ext:ModelField Name="CheckResultDes" />
                                            <ext:ModelField Name="MaterName" />
                                            <ext:ModelField Name="SupplyFacName" />
                                            <ext:ModelField Name="ProductFacName" />
                                            <ext:ModelField Name="RecorderName" />
                                            <ext:ModelField Name="LastModifierName" />
                                            <ext:ModelField Name="RecordStat" />
                                            <ext:ModelField Name="RecordStatDes" />
                                            <ext:ModelField Name="ExtractorName" />
                                            <ext:ModelField Name="ReceiverName" />
                                            <ext:ModelField Name="FetcherName" />
                                            <ext:ModelField Name="HandlerName" />
                                            <ext:ModelField Name="ReceiveDate" Type="Date" />
                                            <ext:ModelField Name="SendDate" Type="Date" />
                                            <ext:ModelField Name="ReturnDate" Type="Date" />
                                            <ext:ModelField Name="HandleDate" Type="Date" />
                                            <ext:ModelField Name="SpecName" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                                <Parameters>
                                    <ext:StoreParameter Name="CheckId" Mode="Raw" Value="#{GridPanelCenterMaster}.getSelectionModel().hasSelection() ? #{GridPanelCenterMaster}.getSelectionModel().getSelection()[0].data.CheckId : -1" />
                                </Parameters>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel1" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />
                                <ext:Column runat="server" ID="ColumnCenterMasterMaterName" DataIndex="MaterName"
                                    Text="原材料型号" Width="120" />
                                <ext:Column runat="server" ID="ColumnCenterMasterMaterSpecName" DataIndex="SpecName"
                                    Text="规格" Width="120" />
                                <ext:Column runat="server" ID="ColumnCenterMasterBarcode" DataIndex="Barcode" Text="条码号" />
                                <ext:Column runat="server" ID="ColumnCenterMasterBatchCode" DataIndex="BatchCode" Text="批次号" />
                                <ext:DateColumn runat="server" ID="DateColumnCenterMasterCheckDate" DataIndex="CheckDate"
                                    Text="检验日期" Format="yyyy-MM-dd" />
                                <ext:Column runat="server" ID="ColumnCenterMasterCheckResultDes" DataIndex="CheckResultDes"
                                    Text="检验结果">
                                    <Renderer Fn="ColumnCenterMasterCheckResultDes_Renderer" />
                                </ext:Column>
                                <ext:Column runat="server" ID="ColumnCenterMasterSupplyFacName" DataIndex="SupplyFacName"
                                    Text="供应商" Width="150" />
                                <ext:Column runat="server" ID="ColumnCenterMasterProductFacName" DataIndex="ProductFacName"
                                    Text="生产商" Width="150" />
                                <ext:Column runat="server" ID="ColumnCenterMasterExtractorName" DataIndex="ExtractorName"
                                    Text="取样人" Width="80" />
                                <ext:Column runat="server" ID="ColumnCenterMasterReceiverName" DataIndex="ReceiverName"
                                    Text="接收人" Width="80" />
                                <ext:Column runat="server" ID="ColumnCenterMasterFetcherName" DataIndex="FetcherName"
                                    Text="领取人" Width="80" />
                                <ext:Column runat="server" ID="ColumnCenterMasterHandlerName" DataIndex="HandlerName"
                                    Text="处置人" Width="80" />
                                <ext:DateColumn runat="server" ID="ColumnCenterMasterReceiveDate" DataIndex="ReceiveDate"
                                    Text="接收时间" Format="yyyy-MM-dd" />
                                <ext:DateColumn runat="server" ID="ColumnCenterMasterSendDate" DataIndex="SendDate"
                                    Text="发放时间" Format="yyyy-MM-dd" />
                                <ext:DateColumn runat="server" ID="ColumnCenterMasterReturnDate" DataIndex="ReturnDate"
                                    Text="返库时间" Format="yyyy-MM-dd" />
                                <ext:DateColumn runat="server" ID="ColumnCenterMasterHandleDate" DataIndex="HandleDate"
                                    Text="处置时间" Format="yyyy-MM-dd" />
                                <ext:Column runat="server" ID="ColumnCenterMasterRecorderName" DataIndex="RecorderName"
                                    Text="录入人" Width="80"/>
                                <ext:DateColumn runat="server" ID="DateColumnCenterMasterRecordTime" DataIndex="RecordTime"
                                    Text="录入时间" Width="150" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:Column runat="server" ID="ColumnCenterMasterLastModifierName" DataIndex="LastModifierName"
                                    Text="修改人" Width="80"/>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:CheckboxSelectionModel runat="server" ID="CheckboxSelectionModelCenterMaster"
                                Mode="Simple" ShowHeaderCheckbox="true">
                                <DirectEvents>
                                    <Select OnEvent="CheckboxSelectionModelCenterMaster_SelectionChange">
                                        <ExtraParams>
                                            <ext:Parameter Mode="Raw" Name="CheckId" Value="record.get('CheckId')" />
                                        </ExtraParams>
                                    </Select>
                                </DirectEvents>
                            </ext:CheckboxSelectionModel>
                        </SelectionModel>
                        <View>
                            <ext:GridView runat="server" ID="GridViewCenterMaster">
                                <GetRowClass Fn="GridViewCenterMaster_GetRowClass" />
                            </ext:GridView>
                        </View>
                        <BottomBar>
                            <ext:PagingToolbar runat="server" ID="PagingToolbarCenterMaster" HideRefresh="true" />
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="质检数据" Height="200" Icon="Basket" Layout="Fit" Collapsible="true" Split="true" MarginsSummary="0 5 5 5">
                <TopBar><ext:Toolbar ID="Toolbar1" runat="server" Height="1"></ext:Toolbar></TopBar>
                <Items>
                     <ext:GridPanel runat="server" ID="GridPanelCenterDeail" Region="South" AutoScroll="true"
                        Height="150" Padding="2">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenterDetail">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenterDetail">
                                        <Fields>
                                            <ext:ModelField Name="ItemName" />
                                            <ext:ModelField Name="GoodMinValue" />
                                            <ext:ModelField Name="GoodOperator" />
                                            <ext:ModelField Name="GoodMaxValue" />
                                            <ext:ModelField Name="CheckMethod" />
                                            <ext:ModelField Name="GoodTextValue" />
                                            <ext:ModelField Name="Remark" />
                                            <ext:ModelField Name="CheckValue" />
                                            <%--<ext:ModelField Name="GoodCheckRange" />--%>
                                            <ext:ModelField Name="GoodDisplayValue" />
                                            <ext:ModelField Name="Frequency" />
                                            <%--<ext:ModelField Name="PrimeCheckRange" />--%>
                                            <ext:ModelField Name="PrimeDisplayValue" />
                                            <ext:ModelField Name="AutoCheckResult" />
                                            <ext:ModelField Name="TextCheckResult" />
                                            <ext:ModelField Name="IsPrime" />
                                            <ext:ModelField Name="TextIsPrime" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:Column runat="server" ID="ColumnCenterDetailItemName" DataIndex="ItemName" Text="检验项目"
                                    Width="200" />
                                <ext:Column runat="server" ID="CheckColumnCenterDetailAutoCheckResult" DataIndex="AutoCheckResult"
                                    Text="是否合格" Hidden="true"/>
                                <ext:Column runat="server" ID="CheckColumnCenterDetailAutoIsPrime" DataIndex="IsPrime"
                                    Text="是否一级品" Hidden="true"/>
                                <ext:Column runat="server" ID="ColumnCenterDetailCheckRange" DataIndex="GoodDisplayValue"
                                    Text="合格品指标值" Width="150"/>
                                <ext:Column runat="server" ID="ColumnCenterDetailCheckValue" DataIndex="CheckValue"
                                    Text="检验值" />
                                <ext:Column runat="server" ID="CheckColumnCenterDetailTextCheckResult" DataIndex="TextCheckResult"
                                    Text="是否合格" />
                                <ext:Column runat="server" ID="CheckColumnCenterDetailTextIsPrime" DataIndex="TextIsPrime"
                                    Text="是否一级品" />
                                <ext:Column runat="server" ID="ColumnCenterDetailAutoPrimeCheckRange" DataIndex="PrimeDisplayValue"
                                    Text="一级品指标值" Width="150"/>
                                <ext:Column runat="server" ID="ColumnCenterDetailFrequency" DataIndex="Frequency"
                                    Text="频次" />
                                <ext:Column runat="server" ID="ColumnCenterDetailCheckMethod" DataIndex="CheckMethod"
                                    Text="检验方法" Width="200" />
                            </Columns>
                        </ColumnModel>
                         <View>
                            <ext:GridView runat="server" ID="GridViewCenterDetail" StripeRows="true" TrackOver="true">
                                <GetRowClass Fn="setRowClass" />
                            </ext:GridView>
                         </View>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
            <ext:Window ID="windowGenerateLedger" runat="server" Icon="MonitorEdit" Closable="false" Title="台账生成设置"
                Width="280" Height="215" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="Form">
                <Items> 
                    <ext:FormPanel ID="pnlGenerateLedger" runat="server" Flex="1" BodyPadding="5">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items> 
                            <ext:TextField ID="txtGenerateLedgerHandlerId" runat="server" FieldLabel="处置人Id" LabelAlign="Right"
                            ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TriggerField ID="trfGenerateLedgerHandlerName" runat="server" FieldLabel="处置人" LabelAlign="Right"
                                Editable="false" >
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectHandler" />
                                </Listeners>
                            </ext:TriggerField>                           
                            <ext:ComboBox ID="cbxGenerateLedgerHandleMethod" runat="server" FieldLabel="处置方式" LabelAlign="Right" Editable="false"  >
                                <Items>
                                    <ext:ListItem Text="退库（车间）" Value="退库（车间）">
                                    </ext:ListItem>
                                    <ext:ListItem Text="退回委托方" Value="退回委托方">
                                    </ext:ListItem>
                                    <ext:ListItem Text="废弃" Value="废弃">
                                    </ext:ListItem>
                                </Items>
                            </ext:ComboBox>     
                            <ext:DateField ID="dtfGenerateLedgerHandleDate" runat="server" FieldLabel="处置时间" LabelAlign="Right" MaxLength="50" Editable="false" />
                            <ext:DateField ID="dtfGenerateLedgerReturnDate" runat="server" FieldLabel="返库时间" LabelAlign="Right" MaxLength="50" Editable="false" />
                            <ext:Checkbox runat="server" ID="cbxChangeGenerateInfo" FieldLabel="修改以上信息" LabelAlign="Right" Checked="false" OnDirectCheck="cbxChangeGenerateInfo_checked"></ext:Checkbox>
                        </Items>
                            <Listeners>
                            <ValidityChange Handler="#{btnGenerateLedgerSave}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="Button1" runat="server" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="BtnGenerateLedgerSave_Click">
                                <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnBatchModifyCancel" runat="server" Text="取消" Icon="Cancel">
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
            <ext:Window ID="windowBatchReCheck" runat="server" Icon="MonitorEdit" Closable="false" Title="批量重判级"
                Width="325" Height="115" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="Form">
                <Items> 
                    <ext:FormPanel ID="pnlBatchReCheck" runat="server" Flex="1" BodyPadding="5">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>                              
                            <ext:ComboBox ID="cbxBatchModifyResult" runat="server" FieldLabel="检测结果" LabelAlign="Right" Editable="false" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text">
                                <SelectedItems>
                                    <ext:ListItem Text="合格" Value="1">
                                    </ext:ListItem>
                                </SelectedItems>
                                <Items>
                                    <ext:ListItem Text="合格" Value="1">
                                    </ext:ListItem>
                                    <ext:ListItem Text="不合格" Value="0">
                                    </ext:ListItem>
                                </Items>
                            </ext:ComboBox>     
                        </Items>
                            <Listeners>
                            <ValidityChange Handler="#{btnBatchModifySave}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnBatchModifySave" runat="server" Text="确定" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="BtnBatchModifySave_Click">
                                <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnModifyDetailCancel" runat="server" Text="取消" Icon="Cancel">
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
            <ext:Window ID="windowExportSetting" runat="server" Icon="MonitorEdit" Closable="false" Title="导出设置"
                Width="325" Height="335" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="Form">
                <Items> 
                    <ext:FormPanel ID="pnlExportSetting" runat="server" Flex="1" BodyPadding="5">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:TextField runat="server" ID="txtHiddenReportType" Hidden="true" FieldLabel="报告类型"></ext:TextField>
                            <ext:TriggerField runat="server" ID="trfFormerName" Editable="false" FieldLabel="制表人" LabelAlign="Right">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectFormer" />
                                </Listeners>
                            </ext:TriggerField>  
                            <ext:TriggerField runat="server" ID="trfConfirmerName" Editable="false" FieldLabel="审核人" LabelAlign="Right">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectConfirmer" />
                                </Listeners>
                            </ext:TriggerField> 
                            <ext:DateField ID="dtfReportDate" runat="server" FieldLabel="报告日期" LabelAlign="Right" MaxLength="50" Editable="false" />
                            <ext:TextField runat="server" ID="txtRemark" MaxLength="50" FieldLabel="备注" LabelAlign="Right"></ext:TextField>
                            <ext:Checkbox runat="server" ID="cbxGenerateLedger" FieldLabel="生成台账" LabelAlign="Right" Checked="true" OnDirectCheck="cbxGenerateLedger_checked"></ext:Checkbox>
                            <ext:Checkbox runat="server" ID="cbxChangeExportInfo" FieldLabel="修改以下信息" LabelAlign="Right" Checked="false" OnDirectCheck="cbxChangeExportInfo_checked"></ext:Checkbox>
                            <ext:TextField ID="txtExportHandlerId" runat="server" FieldLabel="处置人Id" LabelAlign="Right"
                            ReadOnly="true" Hidden="true" Enabled="true" Padding="5" />
                            <ext:TriggerField ID="trfExportHandlerName" runat="server" FieldLabel="处置人" LabelAlign="Right"
                                Editable="false" >
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="SelectHandler" />
                                </Listeners>
                            </ext:TriggerField>                           
                            <ext:ComboBox ID="cbxExportHandleMethod" runat="server" FieldLabel="处置方式" LabelAlign="Right" Editable="false"  >
                                <Items>
                                    <ext:ListItem Text="退库（车间）" Value="退库（车间）">
                                    </ext:ListItem>
                                    <ext:ListItem Text="退回委托方" Value="退回委托方">
                                    </ext:ListItem>
                                    <ext:ListItem Text="废弃" Value="废弃">
                                    </ext:ListItem>
                                </Items>
                            </ext:ComboBox>     
                            <ext:DateField ID="dtfExportHandleDate" runat="server" FieldLabel="处置时间" LabelAlign="Right" MaxLength="50" Editable="false" />
                            <ext:DateField ID="dtfExportReturnDate" runat="server" FieldLabel="返库时间" LabelAlign="Right" MaxLength="50" Editable="false" />
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{btnExportSave}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnExportSave" runat="server" Text="导出" Icon="Accept" >
                        <DirectEvents>
                            <Click OnEvent="BtnExportSave_Click" IsUpload="true">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnExportClose" runat="server" Text="取消" Icon="Cancel">
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
            <ext:Hidden ID="txtHiddenCheckId" runat="server" />
            <ext:Hidden ID="txtHiddenSelectCommand" runat="server" />
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
