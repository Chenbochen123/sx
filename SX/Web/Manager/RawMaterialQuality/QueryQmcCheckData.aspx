<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryQmcCheckData.aspx.cs"
    Inherits="Manager_RawMaterialQuality_QueryQmcCheckData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查询检测记录</title>
        <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript" language="javascript">
        //点击弹出窗口，height=100%设置，google浏览器无法获取其高度方法
        window.onload = function () {
            if (parent && parent.document) {
                var iframes = parent.document.getElementsByTagName("iframe");
                if (iframes) {
                    for (var i = 0; i < iframes.length; i++) {
                        if (iframes[i].contentWindow == window) {
                            iframes[i].height = iframes[i].parentElement.style.height;
                        }
                    }
                }
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
        var response = function (command, record) {
            parent.Manager_RawMaterialQuality_QueryQmcCheckData_Request(record);
            parent.App.Manager_RawMaterialQuality_QueryQmcCheckData_Window.close();
            return false;
        }
        var CommandColumnConfirm_Click = function (command, record) {
            return response(command, record);
        };
        var GridPanelCenter_CellDblClick = function (grid, td, tdindex, record) {
            return response('dblclick', record);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="rmUnit" />
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
                                                <EventMask ShowMask="true" />
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
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="HBoxLayout" Padding="5" >
                                <Items>
                                    <ext:TextField runat="server" ID="TextFieldNorthBarcode" FieldLabel="批次" LabelAlign="Right" InputWidth="166" >
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
<%--                                <ext:DateColumn runat="server" ID="DateColumnCenterMasterLastModifyTime" DataIndex="LastModifyTime"
                                    Text="修改时间" Width="150" Format="yyyy-MM-dd HH:mm:ss" />--%>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel runat="server" ID="CheckboxSelectionModelCenterMaster"
                                Mode="Single" ShowHeaderCheckbox="false">
                                <DirectEvents>
                                    <Select OnEvent="CheckboxSelectionModelCenterMaster_SelectionChange">
                                        <ExtraParams>
                                            <ext:Parameter Mode="Raw" Name="CheckId" Value="record.get('CheckId')" />
                                        </ExtraParams>
                                    </Select>
                                </DirectEvents>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        <Listeners>
                            <CellDblClick Fn="GridPanelCenter_CellDblClick" />
                        </Listeners>
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
            <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="质检数据" Height="150" Icon="Basket" Layout="Fit" Collapsible="true" Split="true" MarginsSummary="0 5 5 5">
                <TopBar><ext:Toolbar ID="Toolbar1" runat="server" Height="1"></ext:Toolbar></TopBar>
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenterDeail" Region="South" AutoScroll="true"
                        Height="150">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenterDetail">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenterDetail">
                                        <Fields>
                                            <ext:ModelField Name="ItemName" />
                                            <ext:ModelField Name="MinValue" />
                                            <ext:ModelField Name="Operator" />
                                            <ext:ModelField Name="MaxValue" />
                                            <ext:ModelField Name="Grade" />
                                            <ext:ModelField Name="CheckMethod" />
                                            <ext:ModelField Name="TextValue" />
                                            <ext:ModelField Name="ActivateDate" Type="Date" />
                                            <ext:ModelField Name="Remark" />
                                            <ext:ModelField Name="CheckValue" />
                                            <ext:ModelField Name="CheckRange" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:Column runat="server" ID="ColumnCenterDetailItemName" DataIndex="ItemName" Text="检验项目"
                                    Width="200" />
                                <ext:Column runat="server" ID="ColumnCenterDetailCheckRange" DataIndex="CheckRange"
                                    Text="检验指标值" />
                                <ext:Column runat="server" ID="ColumnCenterDetailCheckValue" DataIndex="CheckValue"
                                    Text="检验值" />
                                <ext:Column runat="server" ID="ColumnCenterDetailGrade" DataIndex="Grade" Text="级别" />
                                <ext:Column runat="server" ID="ColumnCenterDetailCheckMethod" DataIndex="CheckMethod"
                                    Text="检验方法" Width="200" />
                                <ext:DateColumn runat="server" ID="DateColumnCenterDetailActivateDate" DataIndex="ActivateDate"
                                    Text="生效日期" Format="yyyy-MM-dd" />
                            </Columns>
                        </ColumnModel>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
