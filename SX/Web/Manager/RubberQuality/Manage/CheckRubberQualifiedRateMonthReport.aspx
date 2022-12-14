<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckRubberQualifiedRateMonthReport.aspx.cs"
    Inherits="Manager_RubberQuality_Manage_CheckRubberQualifiedRateMonthReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>胶料月合格率报表</title>
    <style type="text/css">
        .x-grid3-hd-category1 
        {
            text-align: center;
        }
        .x-grid-record-red {     
            color: #FF0000;    
        } 
        .x-grid-record-green {     
            color: #008000;    
        } 
        .x-grid-record-blue {     
            color: #0000FF;    
        } 
        .x-grid-record-total td {     
            background-color: #FFFF00!important;    
        } 
    </style>
    <script type="text/javascript">
        var SetRowClass = function (record, rowIndex, rowParams, store) {
            var rowClass = '';
            if (record.get("QuaRate") < 0.9) {
                rowClass = 'x-grid-record-red';
            }
            else if (record.get("QuaRate") < 1) {
                rowClass = 'x-grid-record-blue';
            }
            else {
                rowClass = 'x-grid-record-green';
            }

            if (store.getTotalCount() == rowIndex + 1) {
                rowClass = rowClass + ' x-grid-record-total'
            }

            return rowClass;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="ResourceManager1" />
    <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
        <Items>
            <ext:Hidden runat="server" ID="HiddenRubTypeName" />
            <ext:Hidden runat="server" ID="HiddenBeginDate" />
            <ext:Hidden runat="server" ID="HiddenEndDate" />
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
                            <ext:Button runat="server" ID="ButtonNorthExport" Icon="PageExcel" Text="导出">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthExport_Click" IsUpload="true">
                                        <ExtraParams>
                                            <ext:Parameter Mode="Raw" Name="records" Value="#{StoreCenter}.getRecordsValues()" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:DateField runat="server" ID="DateFieldNorthBeginDate" FieldLabel="起始日期" Editable="false"
                        Format="yyyy-MM-dd" InputWidth="110" LabelWidth="70" LabelAlign="Right" ColumnWidth="0.3"
                        Height="25">
                    </ext:DateField>
                    <ext:DateField runat="server" ID="DateFieldNorthEndDate" FieldLabel="截止日期" Editable="false"
                        Format="yyyy-MM-dd" InputWidth="110" LabelWidth="70" LabelAlign="Right" ColumnWidth="0.7"
                        Height="25">
                    </ext:DateField>
                    <ext:ComboBox runat="server" ID="ComboBoxNorthRubTypeCode" FieldLabel="胶料分类" Editable="false"
                        InputWidth="110" LabelWidth="70" LabelAlign="Right" MatchFieldWidth="false" ColumnWidth="0.3"
                        Height="25">
                        <ListConfig Width="110" />
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" HideTrigger="true" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.setValue('');" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="ComboBoxNorthZJSID" FieldLabel="主机手" Editable="false"
                        InputWidth="110" LabelWidth="70" LabelAlign="Right" MatchFieldWidth="false" ColumnWidth="0.3"
                        Height="25">
                        <ListConfig Width="110" />
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.setValue('');" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="ComboBoxNorthStandCode" FieldLabel="车间标准" Editable="false"
                        InputWidth="150" LabelWidth="70" LabelAlign="Right" MatchFieldWidth="false" ColumnWidth="0.4"
                        Height="25">
                        <ListConfig Width="150" />
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.setValue('');" />
                        </Listeners>
                    </ext:ComboBox>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="FitLayout">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenter" ColumnLines="true">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenter">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenter">
                                        <Fields>
                                            <ext:ModelField Name="MaterName" />
                                            <ext:ModelField Name="Amount" />
                                            <ext:ModelField Name="QuaAmount" />
                                            <ext:ModelField Name="UnquaAmount" />
                                            <ext:ModelField Name="QuaRate" />
                                            <ext:ModelField Name="Amount_MN_Up" />
                                            <ext:ModelField Name="Amount_MN_Down" />
                                            <ext:ModelField Name="Amount_JS_Up" />
                                            <ext:ModelField Name="Amount_JS_Down" />
                                            <ext:ModelField Name="Amount_YD_Up" />
                                            <ext:ModelField Name="Amount_YD_Down" />
                                            <ext:ModelField Name="Amount_BZ_Up" />
                                            <ext:ModelField Name="Amount_BZ_Down" />
                                            <ext:ModelField Name="Amount_ML_Up" />
                                            <ext:ModelField Name="Amount_ML_Down" />
                                            <ext:ModelField Name="Amount_MH_Up" />
                                            <ext:ModelField Name="Amount_MH_Down" />
                                            <ext:ModelField Name="Amount_T30_Up" />
                                            <ext:ModelField Name="Amount_T30_Down" />
                                            <ext:ModelField Name="Amount_T60_Up" />
                                            <ext:ModelField Name="Amount_T60_Down" />
                                            <ext:ModelField Name="Amount_CC_Up" />
                                            <ext:ModelField Name="Amount_CC_Down" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:RowNumbererColumn Width="40" />
                                <ext:Column DataIndex="MaterName" Text="胶名" />
                                <ext:Column DataIndex="Amount" Text="总车数" />
                                <ext:Column DataIndex="QuaAmount" Text="合格数" />
                                <ext:Column DataIndex="UnquaAmount" Text="不合格数" />
                                <ext:NumberColumn DataIndex="QuaRate" Text="合格率">
                                    <Renderer Handler="return Ext.util.Format.number(value * 100, '0.00%');" />
                                </ext:NumberColumn>
                                <ext:ComponentColumn Text="粘度">
                                    <Columns>
                                        <ext:Column DataIndex="Amount_MN_Up" Text="+" Width="40" />
                                        <ext:Column DataIndex="Amount_MN_Down" Text="-" Width="40" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="焦烧">
                                    <Columns>
                                        <ext:Column DataIndex="Amount_JS_Up" Text="+" Width="40" />
                                        <ext:Column DataIndex="Amount_JS_Down" Text="-" Width="40" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="硬度">
                                    <Columns>
                                        <ext:Column DataIndex="Amount_YD_Up" Text="+" Width="40" />
                                        <ext:Column DataIndex="Amount_YD_Down" Text="-" Width="40" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="比重">
                                    <Columns>
                                        <ext:Column DataIndex="Amount_BZ_Up" Text="+" Width="40" />
                                        <ext:Column DataIndex="Amount_BZ_Down" Text="-" Width="40" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="ML">
                                    <Columns>
                                        <ext:Column DataIndex="Amount_ML_Up" Text="+" Width="40" />
                                        <ext:Column DataIndex="Amount_ML_Down" Text="-" Width="40" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="MH">
                                    <Columns>
                                        <ext:Column DataIndex="Amount_MH_Up" Text="+" Width="40" />
                                        <ext:Column DataIndex="Amount_MH_Down" Text="-" Width="40" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="T30">
                                    <Columns>
                                        <ext:Column DataIndex="Amount_T30_Up" Text="+" Width="40" />
                                        <ext:Column DataIndex="Amount_T30_Down" Text="-" Width="40" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="T60">
                                    <Columns>
                                        <ext:Column DataIndex="Amount_T60_Up" Text="+" Width="40" />
                                        <ext:Column DataIndex="Amount_T60_Down" Text="-" Width="40" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="抽出">
                                    <Columns>
                                        <ext:Column DataIndex="Amount_CC_Up" Text="+" Width="40" />
                                        <ext:Column DataIndex="Amount_CC_Down" Text="-" Width="40" />
                                    </Columns>
                                </ext:ComponentColumn>
                            </Columns>
                        </ColumnModel>
                        <View>
                            <ext:GridView runat="server" ID="GridViewCenter">
                                <GetRowClass Fn="SetRowClass" />
                            </ext:GridView>
                        </View>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
