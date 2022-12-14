<%@ page language="C#" autoeventwireup="true" inherits="Manager_RawMaterialQuality_QueryRawMaterialChkBillDetail, App_Web_drvpsf3a" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var response = function (command, record) {
            parent.Manager_RawMaterialQuality_QueryRawMaterialChkBillDetail_Request(record);
            parent.App.Manager_RawMaterialQuality_QueryRawMaterialChkBillDetail_Window.close();
            return false;
        }
        var CommandColumnConfirm_Click = function (command, record) {
            return response(command, record);
        };
        var GridPanelCenter_CellDblClick = function (grid, td, tdindex, record, tr, trindex, e, fn) {
            return response('dblclick', record);
        }
    </script>
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
                    <ext:TextField runat="server" ID="TextFieldNorthBillNo" FieldLabel="送检单号" LabelAlign="Right" ColumnWidth="0.5" InputWidth="180" MaxLength="50">
                    </ext:TextField>
                    <ext:TextField runat="server" ID="TextFieldNorthNoticeNo" FieldLabel="通知单号" LabelAlign="Right" ColumnWidth="0.5" InputWidth="180" MaxLength="50">
                    </ext:TextField>
                    <ext:DateField runat="server" ID="DateFieldNorthBeginChkDate" FieldLabel="开始日期" LabelAlign="Right"
                        Format="yyyy-MM-dd" Editable="false" InputWidth="180" ColumnWidth="0.5">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.setValue('');" />
                        </Listeners>
                    </ext:DateField>
                    <ext:DateField runat="server" ID="DateFieldNorthEndChkDate" FieldLabel="结束日期" LabelAlign="Right"
                        Format="yyyy-MM-dd" Editable="false" InputWidth="180" ColumnWidth="0.5">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.setValue('');" />
                        </Listeners>
                    </ext:DateField>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="BorderLayout">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenter" Region="Center">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenter" PageSize="10">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenter">
                                        <Fields>
                                            <ext:ModelField Name="BillNo" />
                                            <ext:ModelField Name="NoticeNo" />
                                            <ext:ModelField Name="FactoryID" />
                                            <ext:ModelField Name="FacName" />
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="MaterName" />
                                            <ext:ModelField Name="SendChkDate" />
                                            <ext:ModelField Name="SendNum" />
                                            <ext:ModelField Name="SendWeight" />
                                            <ext:ModelField Name="OrderID" />
                                            <ext:ModelField Name="MaterCode" />
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
                                <ext:Column runat="server" ID="ColumnCenterBillNo" DataIndex="BillNo" Text="单据号" />
                                <ext:Column runat="server" ID="ColumnCenterNoticeNo" DataIndex="NoticeNo" Text="通知单号" />
                                <ext:Column runat="server" ID="ColumnCenterFactoryName" DataIndex="FacName" Text="厂家" />
                                <ext:Column runat="server" ID="ColumnCenterBarcode" DataIndex="Barcode" Text="批次号" />
                                <ext:Column runat="server" ID="ColumnCenterMaterName" DataIndex="MaterName" Text="原材料名称" />
                                <ext:Column runat="server" ID="ColumnCenterSendChkDate" DataIndex="SendChkDate" Text="送检日期" />
                                <ext:Column runat="server" ID="ColumnCenterSendNum" DataIndex="SendNum" Text="送检数量" />
                                <ext:Column runat="server" ID="ColumnCenterSendWeight" DataIndex="SendWeight" Text="送检重量" />
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
