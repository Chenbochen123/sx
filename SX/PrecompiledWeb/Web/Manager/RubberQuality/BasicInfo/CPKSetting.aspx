<%@ page language="C#" autoeventwireup="true" inherits="Manager_RubberQuality_BasicInfo_CPKSetting, App_Web_1dvad4ic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CPK指标设置</title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="ResourceManager1" />
    <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" ID="PanelNorth" Region="North">
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
                            <ext:Button runat="server" ID="ButtonNorthAdd" Icon="PageAdd" Text="新增">
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
                            <ext:Button runat="server" ID="ButtonNorthDelete" Icon="PageDelete" Text="删除" Hidden="true">
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="FitLayout">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenter">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenter">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenter" IDProperty="ObjId">
                                        <Fields>
                                            <ext:ModelField Name="ObjId" />
                                            <ext:ModelField Name="CAMin" />
                                            <ext:ModelField Name="CAMax" />
                                            <ext:ModelField Name="CPMin" />
                                            <ext:ModelField Name="CPMax" />
                                            <ext:ModelField Name="CPKMin" />
                                            <ext:ModelField Name="CPKMax" />
                                            <ext:ModelField Name="NDDeviation" />
                                            <ext:ModelField Name="JSDeviation" />
                                            <ext:ModelField Name="YDDeviation" />
                                            <ext:ModelField Name="BZDeviation" />
                                            <ext:ModelField Name="MLDeviation" />
                                            <ext:ModelField Name="MHDeviation" />
                                            <ext:ModelField Name="Ts1Deviation" />
                                            <ext:ModelField Name="T25Deviation" />
                                            <ext:ModelField Name="T30Deviation" />
                                            <ext:ModelField Name="T60Deviation" />
                                            <ext:ModelField Name="T90Deviation" />
                                            <ext:ModelField Name="CCDeviation" />
                                            <ext:ModelField Name="RecorderId" />
                                            <ext:ModelField Name="RecorderName" />
                                            <ext:ModelField Name="RecordTime" />
                                            <ext:ModelField Name="ModifierId" />
                                            <ext:ModelField Name="ModifierName" />
                                            <ext:ModelField Name="ModifyTime" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:RowNumbererColumn Width="20" />
                                <ext:Column DataIndex="CAMin" Text="CA指标下限" />
                                <ext:Column DataIndex="CAMax" Text="CA指标上限" />
                                <ext:Column DataIndex="CPMin" Text="CP指标下限" />
                                <ext:Column DataIndex="CPMax" Text="CP指标上限" Hidden="true" />
                                <ext:Column DataIndex="CPKMin" Text="CPK指标下限" />
                                <ext:Column DataIndex="CPKMax" Text="CPK指标上限" Hidden="true" />
                                <ext:Column DataIndex="NDDeviation" Text="门尼粘度偏差" />
                                <ext:Column DataIndex="JSDeviation" Text="门尼焦烧偏差" />
                                <ext:Column DataIndex="YDDeviation" Text="硬度偏差" />
                                <ext:Column DataIndex="BZDeviation" Text="比重偏差" />
                                <ext:Column DataIndex="MLDeviation" Text="ML偏差" />
                                <ext:Column DataIndex="MHDeviation" Text="MH偏差" />
                                <ext:Column DataIndex="Ts1Deviation" Text="Ts1偏差" Hidden="true" />
                                <ext:Column DataIndex="T25Deviation" Text="T25偏差" Hidden="true" />
                                <ext:Column DataIndex="T30Deviation" Text="T30偏差" />
                                <ext:Column DataIndex="T60Deviation" Text="T60偏差" />
                                <ext:Column DataIndex="T90Deviation" Text="T90偏差" Hidden="true" />
                                <ext:Column DataIndex="CCDeviation" Text="抽出偏差" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:CheckboxSelectionModel runat="server" ID="CheckboxSelectionModelCenter" ShowHeaderCheckbox="false" Mode="Single">
                            </ext:CheckboxSelectionModel>
                        </SelectionModel>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
            <ext:Window runat="server" ID="WindowEdit" Modal="false" Height="350" Width="450"
                Hidden="true" Layout="FitLayout">
                <Items>
                    <ext:FormPanel runat="server" ID="FormPanelEdit" AutoScroll="true">
                        <Items>
                            <ext:Hidden runat="server" ID="HiddenEditObjId" />
                            <ext:NumberField runat="server" ID="NumberFieldEditCAMin" LabelAlign="Right" FieldLabel="CA指标下限"
                                AllowDecimals="true" DecimalPrecision="3" InputWidth="120" AllowBlank="false"
                                EmptyText="必填" HideTrigger="true" />
                            <ext:NumberField runat="server" ID="NumberFieldEditCAMax" LabelAlign="Right" FieldLabel="CA指标上限"
                                AllowDecimals="true" DecimalPrecision="3" InputWidth="120" AllowBlank="false"
                                EmptyText="必填" HideTrigger="true" />
                            <ext:NumberField runat="server" ID="NumberFieldEditCPMin" LabelAlign="Right" FieldLabel="CP指标下限"
                                AllowDecimals="true" DecimalPrecision="3" InputWidth="120" AllowBlank="false"
                                EmptyText="必填" HideTrigger="true" />
                            <ext:NumberField runat="server" ID="NumberFieldEditCPMax" LabelAlign="Right" FieldLabel="CP指标上限"
                                AllowDecimals="true" DecimalPrecision="3" InputWidth="120" AllowBlank="false"
                                EmptyText="必填" HideTrigger="true" Hidden="true" />
                            <ext:NumberField runat="server" ID="NumberFieldEditCPKMin" LabelAlign="Right" FieldLabel="CPK指标下限"
                                AllowDecimals="true" DecimalPrecision="3" InputWidth="120" AllowBlank="false"
                                EmptyText="必填" HideTrigger="true" />
                            <ext:NumberField runat="server" ID="NumberFieldEditCPKMax" LabelAlign="Right" FieldLabel="CPK指标上限"
                                AllowDecimals="true" DecimalPrecision="3" InputWidth="120" AllowBlank="false"
                                EmptyText="必填" HideTrigger="true" Hidden="true" />
                            <ext:NumberField runat="server" ID="NumberFieldEditNDDeviation" LabelAlign="Right"
                                FieldLabel="粘度偏差" AllowDecimals="true" DecimalPrecision="3" InputWidth="120"
                                AllowBlank="false" EmptyText="必填" HideTrigger="true" />
                            <ext:NumberField runat="server" ID="NumberFieldEditJSDeviation" LabelAlign="Right"
                                FieldLabel="焦烧偏差" AllowDecimals="true" DecimalPrecision="3" InputWidth="120"
                                AllowBlank="false" EmptyText="必填" HideTrigger="true" />
                            <ext:NumberField runat="server" ID="NumberFieldEditYDDeviation" LabelAlign="Right"
                                FieldLabel="硬度偏差" AllowDecimals="true" DecimalPrecision="3" InputWidth="120"
                                AllowBlank="false" EmptyText="必填" HideTrigger="true" />
                            <ext:NumberField runat="server" ID="NumberFieldEditBZDeviation" LabelAlign="Right"
                                FieldLabel="比重偏差" AllowDecimals="true" DecimalPrecision="3" InputWidth="120"
                                AllowBlank="false" EmptyText="必填" HideTrigger="true" />
                            <ext:NumberField runat="server" ID="NumberFieldEditMLDeviation" LabelAlign="Right"
                                FieldLabel="ML偏差" AllowDecimals="true" DecimalPrecision="3" InputWidth="120"
                                AllowBlank="false" EmptyText="必填" HideTrigger="true" />
                            <ext:NumberField runat="server" ID="NumberFieldEditMHDeviation" LabelAlign="Right"
                                FieldLabel="MH偏差" AllowDecimals="true" DecimalPrecision="3" InputWidth="120"
                                AllowBlank="false" EmptyText="必填" HideTrigger="true" />
                            <ext:NumberField runat="server" ID="NumberFieldEditTs1Deviation" LabelAlign="Right"
                                FieldLabel="Ts1偏差" AllowDecimals="true" DecimalPrecision="3" InputWidth="120"
                                AllowBlank="false" EmptyText="必填" HideTrigger="true" Hidden="true" />
                            <ext:NumberField runat="server" ID="NumberFieldEditT25Deviation" LabelAlign="Right"
                                FieldLabel="T25偏差" AllowDecimals="true" DecimalPrecision="3" InputWidth="120"
                                AllowBlank="false" EmptyText="必填" HideTrigger="true" Hidden="true" />
                            <ext:NumberField runat="server" ID="NumberFieldEditT30Deviation" LabelAlign="Right"
                                FieldLabel="T30偏差" AllowDecimals="true" DecimalPrecision="3" InputWidth="120"
                                AllowBlank="false" EmptyText="必填" HideTrigger="true" />
                            <ext:NumberField runat="server" ID="NumberFieldEditT60Deviation" LabelAlign="Right"
                                FieldLabel="T60偏差" AllowDecimals="true" DecimalPrecision="3" InputWidth="120"
                                AllowBlank="false" EmptyText="必填" HideTrigger="true" />
                            <ext:NumberField runat="server" ID="NumberFieldEditT90Deviation" LabelAlign="Right"
                                FieldLabel="T90偏差" AllowDecimals="true" DecimalPrecision="3" InputWidth="120"
                                AllowBlank="false" EmptyText="必填" HideTrigger="true" Hidden="true" />
                            <ext:NumberField runat="server" ID="NumberFieldEditCCDeviation" LabelAlign="Right"
                                FieldLabel="抽出偏差" AllowDecimals="true" DecimalPrecision="3" InputWidth="120"
                                AllowBlank="false" EmptyText="必填" HideTrigger="true" />
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{ButtonEditAccept}.setDisabled(!valid)" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button runat="server" ID="ButtonEditAccept" Icon="Accept" Text="保存">
                        <DirectEvents>
                            <Click OnEvent="ButtonEditAccept_Click">
                                <EventMask ShowMask="true" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button runat="server" ID="ButtonEditCancel" Icon="Cancel" Text="取消">
                        <Listeners>
                            <Click Handler="#{WindowEdit}.close();" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="Ext.fly('form1').mask();" />
                    <Hide Handler="Ext.fly('form1').unmask();" />
                </Listeners>
            </ext:Window>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
