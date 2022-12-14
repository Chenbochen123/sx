<%@ page language="C#" autoeventwireup="true" inherits="Manager_RawMaterialQuality_CheckData, App_Web_drvpsf3a" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>检测数据维护</title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="rmCheckData" />
    <ext:Viewport runat="server" ID="vwUnit" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" ID="PanelNorth" Region="North" Layout="ColumnLayout" Padding="2">
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
                            <ext:Button runat="server" ID="ButtonNorthAdd" Icon="PageAdd" Text="添加">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthAdd_Click">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthEdit" Icon="PageEdit" Text="修改">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthEdit_Click">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthDelete" Icon="PageDelete" Text="删除">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthDelete_Click">
                                        <EventMask ShowMask="true" />
                                        <Confirmation Message="确定要删除选中的记录吗" ConfirmRequest="true" Title="提示" />
                                    </Click>
                                </DirectEvents>
                                <Listeners>
                                    <Click Handler="if (#{CheckboxSelectionModelCenterMaster}.selected.length == 0) { Ext.Msg.alert('提示','请选中要删除的记录'); return false; }" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthApprove" Icon="Tick" Text="审核">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthApprove_Click">
                                        <EventMask ShowMask="true" />
                                        <%--<Confirmation Message="确定要审核选中的记录吗" ConfirmRequest="true" Title="提示" />--%>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthSpecEdit" Icon="PageEdit" Text="确认后修改">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthSpecEdit_Click">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthSpecDelete" Icon="PageDelete" Text="确认后删除">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthSpecDelete_Click">
                                        <EventMask ShowMask="true" />
                                        <Confirmation Message="确定要删除选中的记录吗" ConfirmRequest="true" Title="提示" />
                                    </Click>
                                </DirectEvents>
                                <Listeners>
                                    <Click Handler="if (#{CheckboxSelectionModelCenterMaster}.selected.length == 0) { Ext.Msg.alert('提示','请选中要删除的记录'); return false; }" />
                                </Listeners>
                            </ext:Button>
                             <ext:Button runat="server" ID="ButtonFX" Icon="PageEdit" Text="放行">
                                <DirectEvents>
                                    <Click OnEvent="ButtonFX_Click">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                   
                                </DirectEvents>
                                  <Listeners>
                                    <Click Handler="if (#{CheckboxSelectionModelCenterMaster}.selected.length == 0) { Ext.Msg.alert('提示','请选中要放行的记录'); return false; }" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonQXFX" Icon="PageDelete" Text="取消放行">
                                <DirectEvents>
                                    <Click OnEvent="ButtonQXFX_Click">
                                        <EventMask ShowMask="true" />
                                     </Click>
                                </DirectEvents>
                                <Listeners>
                                    <Click Handler="if (#{CheckboxSelectionModelCenterMaster}.selected.length == 0) { Ext.Msg.alert('提示','请选中要取消删除的记录'); return false; }" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Hidden runat="server" ID="HiddenUserId" />
                    <ext:Hidden runat="server" ID="HiddenSpecEditFlag" />
                    <ext:ComboBox runat="server" ID="ComboBoxNorthMaterMinorType" FieldLabel="原材料分类"
                        LabelAlign="Right" LabelWidth="80" Editable="false" InputWidth="250" MatchFieldWidth="false"
                        ColumnWidth="0.3" Padding="2">
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
                        LabelWidth="80" Editable="false" InputWidth="250" MatchFieldWidth="false" ColumnWidth="0.3"
                        Padding="2">
                        <ListConfig Width="250" />
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.setValue('');" />
                        </Listeners>
                    </ext:ComboBox>
                     <ext:TextField runat="server" ID="TextField2" FieldLabel="条码" LabelAlign="Right"
                        LabelWidth="80" ColumnWidth="0.3" InputWidth="150" Padding="2">
                    </ext:TextField>
                    <ext:ComboBox runat="server" ID="ComboBoxNorthSupplyFac" FieldLabel="供应商" LabelAlign="Right"
                        LabelWidth="80" InputWidth="250" MatchFieldWidth="false" Editable="false" ColumnWidth="0.3"
                        Padding="2">
                        <ListConfig Width="250" />
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                            <ext:FieldTrigger Icon="Search" Qtip="查找" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Fn="ComboBoxNorthSupplyFac_TriggerClick" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:TextField runat="server" ID="TextFieldNorthBarcode" FieldLabel="批次" LabelAlign="Right"
                        LabelWidth="80" ColumnWidth="0.3" InputWidth="110" Padding="2">
                    </ext:TextField>
                    <ext:ComboBox runat="server" ID="ComboBoxNorthRecorder" FieldLabel="录入人" LabelAlign="Right"
                        LabelWidth="80" InputWidth="110" MatchFieldWidth="false" Editable="false" ColumnWidth="0.3"
                        Padding="2">
                        <ListConfig Width="110" />
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.setValue('');" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:DateField runat="server" ID="DateFieldNorthCheckDate" FieldLabel="检验日期" LabelAlign="Right"
                        LabelWidth="80" Format="yyyy-MM-dd" Editable="false" ColumnWidth="0.3" InputWidth="120"
                        Padding="2">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.setValue('');" />
                        </Listeners>
                    </ext:DateField>
                    <ext:ComboBox runat="server" ID="ComboBoxNorthCheckResult" FieldLabel="是否合格" LabelAlign="Right"
                        LabelWidth="80" Editable="false" ColumnWidth="0.3" InputWidth="110" MatchFieldWidth="false"
                        Padding="2">
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
                    <ext:ComboBox runat="server" ID="ComboBoxNorthRecordStat" FieldLabel="状态" LabelAlign="Right"
                        LabelWidth="80" Editable="false" ColumnWidth="0.3" InputWidth="110" MatchFieldWidth="false"
                        Padding="2">
                        <ListConfig Width="110" />
                        <Items>
                            <ext:ListItem Value="0" Text="未提交" />
                            <ext:ListItem Value="1" Text="已提交" />
                        </Items>
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.setValue('');" />
                        </Listeners>
                    </ext:ComboBox>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="BorderLayout">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenterMaster" Region="Center" Padding="2">
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
                                            <ext:ModelField Name="ApproveFlag" />
                                            <ext:ModelField Name="ApproveStatDes" />
                                            <ext:ModelField Name="SpecID" />
                                            <ext:ModelField Name="SpecName" />
                                            <ext:ModelField Name="StandardId" />
                                            <ext:ModelField Name="StandardName" />
                                            <ext:ModelField Name="FXFlag2" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:CommandColumn runat="server" ID="CommandColumnCenterMaster" Text="操作">
                                    <Commands>
                                        <ext:GridCommand Icon="Accept" Text="提交" CommandName="Submit">
                                        </ext:GridCommand>
                                    </Commands>
                                    <DirectEvents>
                                        <Command OnEvent="CommandColumnCenterMaster_Command">
                                            <Confirmation ConfirmRequest="true" Title="提示" Message="提交后则不允许修改或删除，确认要提交吗" />
                                            <EventMask ShowMask="true" />
                                            <ExtraParams>
                                                <ext:Parameter Mode="Raw" Name="CommandName" Value="command" />
                                                <ext:Parameter Mode="Raw" Name="CheckId" Value="record.data.CheckId" />
                                            </ExtraParams>
                                        </Command>
                                    </DirectEvents>
                                    <PrepareToolbar Fn="CommandColumnCenterMaster_PrepareToolbar" />
                                </ext:CommandColumn>
                                <ext:Column runat="server" ID="ColumnCenterMasterRecorderName" DataIndex="RecorderName"
                                    Text="录入人" />
                                <ext:Column runat="server" ID="ColumnCenterMasterMaterName" DataIndex="MaterName"
                                    Text="原材料型号" Width="150" />
                                <ext:Column runat="server" ID="ColumnCenterMasterBarcode" DataIndex="Barcode" Text="条码号" />
                                <ext:Column runat="server" ID="ColumnCenterMasterBatchCode" DataIndex="BatchCode"
                                    Text="批次号" />
                                <ext:Column runat="server" ID="ColumnCenterMasterSpecName" DataIndex="SpecName" Text="规格" />
                                <ext:Column runat="server" ID="ColumnCenterMasterStandardName" DataIndex="StandardName" Text="执行标准" />
                                <ext:DateColumn runat="server" ID="DateColumnCenterMasterCheckDate" DataIndex="CheckDate"
                                    Text="检验日期" Format="yyyy-MM-dd" />
                                <ext:Column runat="server" ID="ColumnCenterMasterCheckResultDes" DataIndex="CheckResultDes"
                                    Text="检验结果">
                                    <Renderer Fn="ColumnCenterMasterCheckResultDes_Renderer" />
                                </ext:Column>
                                <ext:Column runat="server" ID="ColumnCenterMasterRecordStatDes" DataIndex="RecordStatDes"
                                    Text="提交状态">
                                    <Renderer Fn="ColumnCenterMasterRecordStatDes_Renderer" />
                                </ext:Column>
                                <ext:Column runat="server" ID="ColumnCenterMasterApproveStatDes" DataIndex="ApproveStatDes"
                                    Text="审核状态">
                                    <Renderer Fn="ColumnCenterMasterApproveStatDes_Renderer" />
                                </ext:Column>
                                <ext:Column runat="server" ID="ColumnCenterMasterSupplyFacName" DataIndex="SupplyFacName"
                                    Text="供应商" Width="150" />
                                <ext:Column runat="server" ID="ColumnCenterMasterProductFacName" DataIndex="ProductFacName"
                                    Text="生产商" Width="150" />
                                <ext:DateColumn runat="server" ID="DateColumnCenterMasterRecordTime" DataIndex="RecordTime"
                                    Text="录入时间" Width="150" Format="yyyy-MM-dd HH:mm:ss" />
                                     <ext:Column runat="server" ID="Column1" DataIndex="FXFlag2" Text="是否放行" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:CheckboxSelectionModel runat="server" ID="CheckboxSelectionModelCenterMaster"
                                Mode="Single" ShowHeaderCheckbox="false">
                                <DirectEvents>
                                    <SelectionChange OnEvent="CheckboxSelectionModelCenterMaster_SelectionChange">
                                        <EventMask ShowMask="true" />
                                        <ExtraParams>
                                            <ext:Parameter Mode="Raw" Name="CheckId" Value="selected[0].get('CheckId')" />
                                        </ExtraParams>
                                    </SelectionChange>
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
                                                 <ext:ModelField Name="MinValue" />
                                              <ext:ModelField Name="MaxValue" />
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
                                   <ext:Column runat="server" ID="Column2" DataIndex="MinValue"
                                    Text="最小值" />
                                                                <ext:Column runat="server" ID="Column3" DataIndex="MaxValue"
                                    Text="最大值" />
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
            <ext:Panel runat="server" ID="PanelEast" Region="East" Layout="BorderLayout" Width="300"
                Padding="2" Hidden="true">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenterProperty" Region="Center" AutoScroll="true">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenterProperty">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenterProperty">
                                        <Fields>
                                            <ext:ModelField Name="PropertyName" />
                                            <ext:ModelField Name="ValueType" />
                                            <ext:ModelField Name="ShowValue" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:Column runat="server" ID="ColumnCenterPropertyPropertyName" DataIndex="PropertyName"
                                    Text="属性名称" Width="140" />
                                <ext:Column runat="server" ID="ColumnCenterPropertyValueType" DataIndex="ValueType"
                                    Text="赋值类型" Width="60" />
                                <ext:Column runat="server" ID="ColumnCenterPropertyShowValue" DataIndex="ShowValue"
                                    Text="属性值" Width="100" />
                            </Columns>
                        </ColumnModel>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    <ext:Window runat="server" ID="WindowApprove" Width="800" Hidden="true" BodyPadding="5" Resizable="false" Title="审批检测数据">
        <Items>
            <ext:Panel runat="server" ID="PanelApproveMain" Layout="FormLayout" BodyPadding="5">
                <Items>
                    <ext:FieldSet runat="server" ID="FieldApproveBase" Layout="ColumnLayout" Title="送检原材料" Collapsible="true" Collapsed="false">
                        <Items>
                            <ext:TextField runat="server" ID="txtSampleNameApprove" FieldLabel="样品名称"
                                AllowBlank="false" Editable="false" LabelWidth="100" LabelAlign="Right" ReadOnly="true"
                                ColumnWidth="0.36" Padding="2">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="txtFrequencyApprove" ColumnWidth="0.121" Padding="2"
                                InputWidth="38" ReadOnly="true">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="txtSpecApprove" FieldLabel="规格" LabelWidth="115"
                                Editable="false" LabelAlign="Right" InputWidth="200" ColumnWidth="0.5" Padding="2"
                                MatchFieldWidth="false" ReadOnly="true">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="txtSupplierApprove" FieldLabel="生产商"
                                LabelWidth="100" LabelAlign="Right" ReadOnly="true" InputWidth="200" MatchFieldWidth="false"
                                ColumnWidth="0.5" Padding="2">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="txtManufacturerApprove" FieldLabel="供应商" LabelWidth="100"
                                LabelAlign="Right" ReadOnly="true" InputWidth="200" MatchFieldWidth="false" ColumnWidth="0.5"
                                Padding="2">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="txtBarcodeApprove" FieldLabel="条码号" LabelWidth="100"
                                LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5" Padding="2">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="txtBatchCodeApprove" FieldLabel="批次号" LabelWidth="100"
                                LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5" Padding="2">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="txtSampleCodeApprove" FieldLabel="样品编号" LabelWidth="100"
                                LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5" Padding="2">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="txtSampleNumApprove" FieldLabel="进货数量" LabelWidth="100"
                                LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5" Padding="2">
                            </ext:TextField>
                            <ext:FieldSet runat="server" ID="FieldApproveSlave" Title="点击查看台账详细信息" Layout="ColumnLayout"
                                ColumnWidth="1" Collapsible="true" Collapsed="true">
                                <Items>
                                    <ext:TextField runat="server" ID="txtSampleStatusApprove" FieldLabel="样品状态"
                                        LabelWidth="90" LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5"
                                        Padding="2">
                                    </ext:TextField>
                                    <ext:TextField runat="server" ID="txtFactoryNumApprove" FieldLabel="厂家编号" LabelWidth="100"
                                        LabelAlign="Right" ReadOnly="true" InputWidth="200" MatchFieldWidth="false" ColumnWidth="0.5"
                                        Padding="2">
                                    </ext:TextField>
                                    <ext:TextField runat="server" ID="txtExtractorApporve" FieldLabel="取样人"
                                        LabelWidth="90" LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5"
                                        Padding="2">
                                    </ext:TextField>
                                    <ext:TextField runat="server" ID="txtReceiverApprove" FieldLabel="接收人" LabelWidth="100"
                                        LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5" Padding="2">
                                    </ext:TextField>
                                    <ext:TextField runat="server" ID="txtReceiveDateApprove" FieldLabel="接收时间" LabelWidth="90"
                                        LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5" Padding="2">
                                    </ext:TextField>
                                    <ext:TextField runat="server" ID="txtSendDateApprove" FieldLabel="发放时间" LabelWidth="100"
                                        LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5" Padding="2">
                                    </ext:TextField>
                                    <ext:TextField runat="server" ID="txtFetcherApprove" FieldLabel="领取人" LabelWidth="90"
                                        LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5" Padding="2">
                                    </ext:TextField>
                                </Items>
                            </ext:FieldSet>
                            <ext:TextField runat="server" ID="txtSampleRemarkApprove" FieldLabel="样品备注"
                                LabelWidth="100" LabelAlign="Right" ReadOnly="true" InputWidth="450" ColumnWidth="1"
                                Padding="2">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="txtCheckDateApprove" FieldLabel="检验日期" LabelWidth="100"
                                LabelAlign="Right" Editable="false" InputWidth="110" ColumnWidth="0.325" Padding="2" ReadOnly="true">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="txtCheckResultApprove" FieldLabel="检验结果" LabelWidth="100"
                                LabelAlign="Right" Editable="false" InputWidth="85" MatchFieldWidth="false" ReadOnly="true"
                                 ColumnWidth="0.4" Padding="2">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="txtRemarkApprove" FieldLabel="备注" LabelWidth="100"
                                LabelAlign="Right" InputWidth="450" ColumnWidth="1" Padding="2" Hidden="true"
                                MaxLength="25">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="txtStandardApprove" FieldLabel="执行标准" LabelWidth="100"
                                Editable="false" LabelAlign="Right" InputWidth="450" ColumnWidth="1" Padding="2" ReadOnly="true"
                                MatchFieldWidth="false">
                            </ext:TextField>
                        </Items>
                    </ext:FieldSet>
                    <ext:FieldSet runat="server" ID="FieldApproveMain" Layout="ColumnLayout" Title="检测数据" Collapsible="true" Collapsed="false" AutoScroll="true" MaxHeight="200" Height="100">
                    </ext:FieldSet>
                </Items>
            </ext:Panel>
        </Items>
        <Buttons>
            <ext:Button runat="server" ID="btnConfirmApprove" Text="审核" Icon="Accept">
                <DirectEvents>
                    <Click OnEvent="BtnConfirmApprove_Click" />
                </DirectEvents>
            </ext:Button>
            <ext:Button runat="server" ID="btnCancelApprove" Text="取消" Icon="Cancel">
                <DirectEvents>
                    <Click OnEvent="BtnCancelApprove_Click">
                    </Click>
                </DirectEvents>
            </ext:Button>
        </Buttons>
        <Listeners>
            <Show Handler="Ext.fly('form1').mask();" />
            <Hide Handler="Ext.fly('form1').unmask();" />
        </Listeners>
    </ext:Window>
    <ext:Window runat="server" ID="WindowMaster"  Width="800" Hidden="true" BodyPadding="5" Resizable="false">
        <Items>
            <ext:Panel runat="server" ID="PanelMasterMain" Layout="FormLayout" BodyPadding="5">
                <Items>
                    <ext:FieldSet runat="server" ID="FieldSetMasterMain" Layout="ColumnLayout" Title="送检原材料" Collapsible="true" Collapsed="false">
                        <Items>
                            <ext:Hidden runat="server" ID="HiddenMasterCommandName" />
                            <ext:Hidden runat="server" ID="HiddenMasterCheckId" />
                            <ext:Hidden runat="server" ID="HiddenMasterLedgerId" />
                            <ext:Hidden runat="server" ID="HiddenMasterBillNo" />
                            <ext:Hidden runat="server" ID="HiddenMasterBarcode" />
                            <ext:Hidden runat="server" ID="HiddenMasterBatchCode" />
                            <ext:Hidden runat="server" ID="HiddenMasterSpecId" />
                            <ext:Hidden runat="server" ID="HiddenMasterOrderID" />
                            <ext:Hidden runat="server" ID="HiddenMasterMaterCode" />
                            <ext:Hidden runat="server" ID="HiddenMasterSeriesId" />
                            <ext:Hidden runat="server" ID="HiddenMasterSupplyFacId" />
                            <ext:Hidden runat="server" ID="HiddenMasterProductFacId" />
                            <ext:TriggerField runat="server" ID="TriggerFieldMasterSampleName" FieldLabel="样品名称"
                                AllowBlank="false" Editable="false" LabelWidth="100" LabelAlign="Right" EmptyText="请选择样品台账"
                                ColumnWidth="0.36" Padding="2">
                                <AfterLabelTextTpl runat="server">
                                    <Html>
                                        <label class="red-text">*</label>
                                    </Html>
                                </AfterLabelTextTpl>
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" Qtip="查找" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="App.Manager_RawMaterialQuality_QueryQmcSampleLedger_Window.show();" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField runat="server" ID="TextFieldMasterFrequency" ColumnWidth="0.121" Padding="2"
                                InputWidth="38" ReadOnly="true">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="TextFieldMasterSpec" FieldLabel="规格" LabelWidth="115"
                                Editable="false" LabelAlign="Right" InputWidth="200" ColumnWidth="0.5" Padding="2"
                                MatchFieldWidth="false" ReadOnly="true">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="TextFieldMasterManufacturerName" FieldLabel="生产商"
                                LabelWidth="100" LabelAlign="Right" ReadOnly="true" InputWidth="200" MatchFieldWidth="false"
                                ColumnWidth="0.5" Padding="2">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="TextFieldMasterSupplierName" FieldLabel="供应商" LabelWidth="100"
                                LabelAlign="Right" ReadOnly="true" InputWidth="200" MatchFieldWidth="false" ColumnWidth="0.5"
                                Padding="2">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="TextFieldMasterBarcode" FieldLabel="条码号" LabelWidth="100"
                                LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5" Padding="2">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="TextFieldMasterBatchCode" FieldLabel="批次号" LabelWidth="100"
                                LabelAlign="Right" InputWidth="200" ColumnWidth="0.5" Padding="2">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="TextFieldMasterSampleCode" FieldLabel="样品编号" LabelWidth="100"
                                LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5" Padding="2">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="TextFieldMasterSampleNum" FieldLabel="进货数量" LabelWidth="100"
                                LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5" Padding="2">
                            </ext:TextField>
                            <ext:FieldSet runat="server" ID="FieldSetMasterSampleInfo" Title="点击查看台账详细信息" Layout="ColumnLayout"
                                ColumnWidth="1" Collapsible="true" Collapsed="true">
                                <Items>
                                    <ext:TextField runat="server" ID="TextFieldMasterSampleStatus" FieldLabel="样品状态"
                                        LabelWidth="90" LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5"
                                        Padding="2">
                                    </ext:TextField>
                                    <ext:TextField runat="server" ID="TextFieldMasterFactoryCode" FieldLabel="厂家编号" LabelWidth="100"
                                        LabelAlign="Right" ReadOnly="true" InputWidth="200" MatchFieldWidth="false" ColumnWidth="0.5"
                                        Padding="2">
                                    </ext:TextField>
                                    <ext:TextField runat="server" ID="TextFieldMasterExtractorName" FieldLabel="取样人"
                                        LabelWidth="90" LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5"
                                        Padding="2">
                                    </ext:TextField>
                                    <ext:TextField runat="server" ID="TextFieldMasterReceiverName" FieldLabel="接收人" LabelWidth="100"
                                        LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5" Padding="2">
                                    </ext:TextField>
                                    <ext:TextField runat="server" ID="TextFieldMasterReceiveDate" FieldLabel="接收时间" LabelWidth="90"
                                        LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5" Padding="2">
                                    </ext:TextField>
                                    <ext:TextField runat="server" ID="TextFieldMasterSendDate" FieldLabel="发放时间" LabelWidth="100"
                                        LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5" Padding="2">
                                    </ext:TextField>
                                    <ext:TextField runat="server" ID="TextFieldMasterFetcherName" FieldLabel="领取人" LabelWidth="90"
                                        LabelAlign="Right" ReadOnly="true" InputWidth="200" ColumnWidth="0.5" Padding="2">
                                    </ext:TextField>
                                </Items>
                            </ext:FieldSet>
                            <ext:TextField runat="server" ID="TextFieldMasterSampleRemark" FieldLabel="样品备注"
                                LabelWidth="100" LabelAlign="Right" ReadOnly="true" InputWidth="450" ColumnWidth="1"
                                Padding="2">
                            </ext:TextField>
                            <ext:Checkbox runat="server" ID="CheckboxMasterRecordStat" FieldLabel="确认提交" ColumnWidth="0.1"
                                LabelWidth="100" LabelAlign="Right" TagString="是" Padding="2">
                            </ext:Checkbox>
                            <ext:DateField runat="server" ID="DateFieldMasterCheckDate" FieldLabel="检验日期" LabelWidth="100"
                                LabelAlign="Right" Editable="false" Format="yyyy-MM-dd" AllowBlank="false" EmptyText="请选择..."
                                InputWidth="110" ColumnWidth="0.325" Padding="2">
                                <AfterLabelTextTpl runat="server">
                                    <Html>
                                        <label class="red-text">*</label>
                                    </Html>
                                </AfterLabelTextTpl>
                            </ext:DateField>
                            <ext:ComboBox runat="server" ID="ComboBoxMasterCheckResult" FieldLabel="检验结果" LabelWidth="100"
                                LabelAlign="Right" Editable="false" AllowBlank="false" EmptyText="请选择..." InputWidth="85"
                                MatchFieldWidth="false" ColumnWidth="0.4" Padding="2">
                                <ListConfig Width="90" />
                                <AfterLabelTextTpl runat="server">
                                    <Html>
                                        <label class="red-text">*</label>
                                    </Html>
                                </AfterLabelTextTpl>
                                <Items>
                                    <ext:ListItem Mode="Value" Value="1" Text="合格" />
                                    <ext:ListItem Mode="Value" Value="0" Text="不合格" />
                                </Items>
                            </ext:ComboBox>
                            <ext:TextField runat="server" ID="TextFieldMasterRemark" FieldLabel="备注" LabelWidth="100"
                                LabelAlign="Right" InputWidth="450" ColumnWidth="1" Padding="2" Hidden="true"
                                MaxLength="25">
                            </ext:TextField>
                            <ext:ComboBox runat="server" ID="ComboBoxMasterStandard" FieldLabel="执行标准" LabelWidth="100"
                                Editable="false" LabelAlign="Right" InputWidth="450" ColumnWidth="1" Padding="2"
                                MatchFieldWidth="false">
                                <ListConfig Width="450" />
                                <AfterLabelTextTpl runat="server">
                                    <Html>
                                        <label class="red-text">*</label>
                                    </Html>
                                </AfterLabelTextTpl>
                                <DirectEvents>
                                    <Select OnEvent="ComboBoxMasterStandard_Click">
                                        <EventMask ShowMask="true" />
                                    </Select>
                                </DirectEvents>
                            </ext:ComboBox>
                        </Items>
                    </ext:FieldSet>
                    <ext:FieldSet Layout="ColumnLayout" runat="server" ID="FieldSetMasterProperty" Title="基本属性"
                        Height="100" Hidden="true">
                    </ext:FieldSet>
                    <ext:FieldSet Layout="ColumnLayout" runat="server" ID="FieldSetMasterDetail" Title="测试项" AutoScroll="true" MaxHeight="200"
                        Height="100">
                    </ext:FieldSet>
                </Items>
            </ext:Panel>
        </Items>
        <Buttons>
            <ext:Button runat="server" ID="ButtonMasterAccept" Text="保存" Icon="Accept">
                <Listeners>
                    <Click Fn="ButtonMasterAccept_Click" />
                </Listeners>
            </ext:Button>
            <ext:Button runat="server" ID="ButtonMasterCancel" Text="取消" Icon="Cancel">
                <DirectEvents>
                    <Click OnEvent="BtnCancel_Click">
                    </Click>
                </DirectEvents>
            </ext:Button>
        </Buttons>
        <Listeners>
            <Show Handler="Ext.fly('form1').mask();" />
            <Hide Handler="Ext.fly('form1').unmask();" />
        </Listeners>
    </ext:Window>
    </form>
</body>
</html>
