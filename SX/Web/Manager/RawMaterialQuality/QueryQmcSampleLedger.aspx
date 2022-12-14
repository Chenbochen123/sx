<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryQmcSampleLedger.aspx.cs"
    Inherits="Manager_RawMaterialQuality_QueryQmcSampleLedger" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="ResourceManager1" />
    <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" ID="PanelNorth" Region="North" Layout="ColumnLayout">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ToolbarNorth">
                        <Items>
                            <ext:Button runat="server" ID="ButtonNorthQuery" Icon="Magnifier" Text="查询">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthQuery_Click">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Hidden runat="server" ID="HiddenNorthManufacturerId" />
                    <ext:Hidden runat="server" ID="HiddenNorthSupplierId" />
                    <ext:Hidden runat="server" ID="HiddenNorthFacType" />
                    <ext:DateField runat="server" ID="DateFieldNorthBeginDate" FieldLabel="开始日期" LabelAlign="Right"
                        Format="yyyy-MM-dd" Editable="false" InputWidth="180" ColumnWidth="0.5">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.setValue('');" />
                        </Listeners>
                    </ext:DateField>
                    <ext:DateField runat="server" ID="DateFieldNorthEndDate" FieldLabel="结束日期" LabelAlign="Right"
                        Format="yyyy-MM-dd" Editable="false" InputWidth="180" ColumnWidth="0.5">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.setValue('');" />
                        </Listeners>
                    </ext:DateField>
                    <ext:TextField runat="server" ID="TextFieldNorthSampleName" FieldLabel="样品名称" LabelAlign="Right"
                        ColumnWidth="0.5" InputWidth="230" MaxLength="50" EmptyText="模糊查找">
                    </ext:TextField>
                    <ext:TextField runat="server" ID="TextFieldNorthSampleCode" FieldLabel="样品编号" LabelAlign="Right"
                        ColumnWidth="0.5" InputWidth="230" MaxLength="50" EmptyText="精确查找">
                    </ext:TextField>
                    <ext:TriggerField runat="server" ID="TriggerFieldNorthManufacturerName" FieldLabel="生产商" LabelAlign="Right"
                        ColumnWidth="0.5" InputWidth="230" MaxLength="50" Editable="false" EmptyText="精确查找">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                            <ext:FieldTrigger Icon="Search" Qtip="查找" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Fn="TriggerFieldNorthManufacturerName_Click" />
                        </Listeners>
                    </ext:TriggerField>
                    <ext:TriggerField runat="server" ID="TriggerFieldNorthSupplierName" FieldLabel="供应商" LabelAlign="Right"
                        ColumnWidth="0.5" InputWidth="230" MaxLength="50" Editable="false" EmptyText="精确查找">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                            <ext:FieldTrigger Icon="Search" Qtip="查找" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Fn="TriggerFieldNorthSupplierName_Click" />
                        </Listeners>
                    </ext:TriggerField>
                    <ext:TextField runat="server" ID="TextFieldNorthFactoryCode" FieldLabel="厂家编号" LabelAlign="Right"
                        ColumnWidth="0.5" InputWidth="230" MaxLength="50" EmptyText="精确查找">
                    </ext:TextField>
                    <ext:TextField runat="server" ID="TextFieldNorthBarcode" FieldLabel="批次号" LabelAlign="Right"
                        ColumnWidth="0.5" InputWidth="230" MaxLength="50" EmptyText="模糊查找">
                    </ext:TextField>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="BorderLayout">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenter" Region="Center">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenter" PageSize="8">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenter">
                                        <Fields>
                                            <ext:ModelField Name="LedgerId" />
                                            <ext:ModelField Name="BillDetailId" />
                                            <ext:ModelField Name="BillNo" />
                                            <ext:ModelField Name="OrderId" />
                                            <ext:ModelField Name="SpecId" />
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="BatchCode" />
                                            <ext:ModelField Name="Frequency" />
                                            <ext:ModelField Name="MaterialCode" />
                                            <ext:ModelField Name="SampleCode" />
                                            <ext:ModelField Name="SampleName" />
                                            <ext:ModelField Name="SampleNum" />
                                            <ext:ModelField Name="SampleUnit" />
                                            <ext:ModelField Name="SampleStatus" />
                                            <ext:ModelField Name="SupplierId" />
                                            <ext:ModelField Name="SupplierName" />
                                            <ext:ModelField Name="ManufacturerId" />
                                            <ext:ModelField Name="ManufacturerName" />
                                            <ext:ModelField Name="FactoryCode" />
                                            <ext:ModelField Name="ExtractorId" />
                                            <ext:ModelField Name="ExtractorName" />
                                            <ext:ModelField Name="ReceiverId" />
                                            <ext:ModelField Name="ReceiverName" />
                                            <ext:ModelField Name="FetcherId" />
                                            <ext:ModelField Name="FetcherName" />
                                            <ext:ModelField Name="HandlerId" />
                                            <ext:ModelField Name="HandlerName" />
                                            <ext:ModelField Name="CheckResult" />
                                            <ext:ModelField Name="ReceiveDate" Type="Date" />
                                            <ext:ModelField Name="SendDate" Type="Date" />
                                            <ext:ModelField Name="ReturnDate" Type="Date" />
                                            <ext:ModelField Name="HandleDate" Type="Date" />
                                            <ext:ModelField Name="RecordDate" Type="Date" />
                                            <ext:ModelField Name="HandleMethod" />
                                            <ext:ModelField Name="Remark" />
                                            <ext:ModelField Name="DeleteFlag" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:CommandColumn ID="CommandColumnConfirm" runat="server" Width="70" Text="确认"
                                    Align="Center">
                                    <Commands>
                                        <ext:GridCommand Icon="accept" CommandName="Select" Text="确认">
                                            <ToolTip Text="确认使用本条数据" />
                                        </ext:GridCommand>
                                    </Commands>
                                    <PrepareToolbar />
                                    <Listeners>
                                        <Command Handler="return CommandColumnConfirm_Click(command, record);" />
                                    </Listeners>
                                </ext:CommandColumn>
                                <ext:Column runat="server" ID="ColumnCenterSampleName" DataIndex="SampleName" Text="样品名称" Width="120" />
                                <ext:Column runat="server" ID="ColumnCenterBillNo" DataIndex="BillNo" Text="单据号"
                                    Hidden="true" />
                                <ext:Column runat="server" ID="ColumnCenterSpecId" DataIndex="BillNo" Text="规格ID"
                                    Hidden="true" />
                                <ext:Column runat="server" ID="ColumnCenterManufacturerName" DataIndex="ManufacturerName"
                                    Text="生产商" Width="150" />
                                <ext:Column runat="server" ID="ColumnCenterSupplierName" DataIndex="SupplierName"
                                    Text="供应商" Width="150" />
                                <ext:Column runat="server" ID="ColumnCenterFactoryCode" DataIndex="FactoryCode" Text="厂家编号" Width="100" />
                                <ext:Column runat="server" ID="ColumnCenterBarcode" DataIndex="Barcode" Text="条码号" Width="80" />
                                <ext:Column runat="server" ID="ColumnCenterBatchCode" DataIndex="BatchCode" Text="批次号" Width="80" />
                                <ext:Column runat="server" ID="ColumnCenterFrequency" DataIndex="Frequency" Text="检测频次" Width="80" />
                                <ext:Column runat="server" ID="ColumnCenterSampleNum" DataIndex="SampleNum" Text="样品数量" Width="80" />
                                <ext:Column runat="server" ID="ColumnCenterSampleUnit" DataIndex="SampleUnit" Text="样品单位" Width="60" />
                                <ext:Column runat="server" ID="ColumnCenterSampleCode" DataIndex="SampleCode" Text="样品编号" Width="120" />
                                <ext:Column runat="server" ID="ColumnCenterSampleStatus" DataIndex="SampleStatus"
                                    Text="样品状态" Width="60" />
                                <ext:Column runat="server" ID="ColumnCenterExtractorName" DataIndex="ExtractorName"
                                    Text="取样人" Width="60" />
                                <ext:Column runat="server" ID="ColumnCenterReceiverName" DataIndex="ReceiverName"
                                    Text="接收人" Width="60" />
                                <ext:DateColumn runat="server" ID="ColumnCenterReceiveDate" DataIndex="ReceiveDate" Text="接收时间" Width="120" Format="yyyy-MM-dd" />
                                <ext:DateColumn runat="server" ID="ColumnCenterSendDate" DataIndex="SendDate" Text="发放时间" Width="120" Format="yyyy-MM-dd" />
                                <ext:Column runat="server" ID="ColumnCenterFetcherName" DataIndex="FetcherName" Text="领取人" Width="120" />
                                <ext:Column runat="server" ID="ColumnCenterCheckResultDes" DataIndex="CheckResultDes"
                                    Text="检验结果" Width="60" />
                                <ext:DateColumn runat="server" ID="ColumnCenterReturnDate" DataIndex="ReturnDate" Text="返库时间" Width="120" Format="yyyy-MM-dd" />
                                <ext:DateColumn runat="server" ID="ColumnCenterHandleDate" DataIndex="HandleDate" Text="处置时间" Width="120" Format="yyyy-MM-dd" />
                                <ext:Column runat="server" ID="ColumnCenterHandleMethod" DataIndex="HandleMethod"
                                    Text="处置方式" Width="120" />
                                <ext:Column runat="server" ID="ColumnCenterHandlerName" DataIndex="HandlerName" Text="处置人" Width="60" />
                                <ext:Column runat="server" ID="ColumnCenterRemark" DataIndex="Remark" Text="备注" Width="250" />
                                <ext:Column runat="server" Width="200" />
                            </Columns>
                        </ColumnModel>
                        <Listeners>
                            <CellDblClick Fn="GridPanelCenter_CellDblClick" />
                        </Listeners>
                        <BottomBar>
                            <ext:PagingToolbar runat="server" ID="PagingToolbarCenter" HideRefresh="true" />
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
