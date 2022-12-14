<%@ page language="C#" autoeventwireup="true" inherits="Manager_RubberQuality_Manage_CheckRubberQualityCPKReport, App_Web_bsjgrvuf" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="ResourceManager" />
    <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" ID="PanelNorth" Region="North">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ToolbarNorth">
                        <Items>
                            <ext:Button runat="server" ID="ButtonNorthExport" Text="导出" Icon="PageExcel">
                                <Listeners>
                                    <Click Fn="ButtonNorthExport_Click" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel runat="server" ID="PanelNorthQuery" Width="500" Layout="ColumnLayout">
                        <Items>
                            <ext:Hidden runat="server" ID="HiddenNorthCheckType" />
                            <ext:ComboBox runat="server" ID="ComboBoxNorthCheckType" FieldLabel="分类" LabelAlign="Right"
                                AllowBlank="false" Editable="false" EmptyText="请选择..." InputWidth="90" ColumnWidth="1"
                                MatchFieldWidth="false">
                                <Items>
                                    <ext:ListItem Mode="Value" Value="2" Text="检验标准" />
                                    <ext:ListItem Mode="Value" Value="1" Text="考核标准" />
                                     <ext:ListItem Mode="Value" Value="3" Text="专检标准" />
                                </Items>
                                <ListConfig Width="90" />
                            </ext:ComboBox>
                            <ext:Hidden runat="server" ID="HiddenNorthMaterFlag" />
                            <ext:TriggerField runat="server" ID="TriggerFieldNorthMaterName" FieldLabel="主胶料"
                                LabelAlign="Right" AllowBlank="false" EmptyText="请选择胶料" Editable="false" ColumnWidth="1">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                    <ext:FieldTrigger Icon="Search" Qtip="查找" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="TriggerFieldNorthMaterName_TriggerClick" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:Hidden runat="server" ID="HiddenNorthMaterCode" />
                            <ext:Container runat="server" Height="2" ColumnWidth="1" />
                            <ext:TriggerField runat="server" ID="TriggerFieldNorthOtherMaterName" FieldLabel="相关胶料"
                                LabelAlign="Right" Editable="false" ColumnWidth="1">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                    <ext:FieldTrigger Icon="Search" Qtip="查找" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="TriggerFieldNorthOtherMaterName_TriggerClick" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:Hidden runat="server" ID="HiddenNorthOtherMaterCode" />
                            <ext:MultiSelect runat="server" ID="MultiSelectNorthMater" HideEmptyLabel="false"
                                ColumnWidth="0.75" Height="80" SingleSelect="true" />
                            <ext:Container runat="server" ColumnWidth="0.25" Layout="FormLayout">
                                <Items>
                                    <ext:Button runat="server" ID="ButtonNorthAddToList" Icon="Add" Text="添加到列表">
                                        <Listeners>
                                            <Click Fn="ButtonNorthAddToList_Click" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button runat="server" ID="ButtonNorthDeleteFromList" Icon="Delete" Text="从列表删除">
                                        <Listeners>
                                            <Click Fn="ButtonNorthDeleteFromList_Click" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button runat="server" ID="ButtonNorthClearList" Icon="Cancel" Text="清空列表">
                                        <Listeners>
                                            <Click Fn="ButtonNorthClearList_Click" />
                                        </Listeners>
                                    </ext:Button>
                                </Items>
                            </ext:Container>
                            <ext:FieldSet runat="server" ID="FieldSetNorthDateType" ColumnWidth="1" Layout="ColumnLayout">
                                <Items>
                                    <ext:RadioGroup runat="server" ID="RadioGroupNorthDateType" ColumnsNumber="1" ColumnWidth="0.3"
                                        HideEmptyLabel="false" LabelWidth="50" Height="60">
                                        <Items>
                                            <ext:Radio ID="RadioNorthDateType1" runat="server" BoxLabel="按月份" InputValue="1"
                                                Checked="true" />
                                            <ext:Radio ID="RadioNorthDateType2" runat="server" BoxLabel="按时间段" InputValue="2" />
                                        </Items>
                                        <Listeners>
                                            <Change Fn="RadioGroupNorthDateType_Change" />
                                        </Listeners>
                                    </ext:RadioGroup>
                                    <ext:Container runat="server" ColumnWidth="0.7" Layout="FormLayout" Height="60">
                                        <Items>
                                            <ext:DateField runat="server" ID="DateFieldNorthMonth" FieldLabel="月份" LabelAlign="Right"
                                                Type="Month" Format="yyyy-MM" AllowBlank="false" Editable="false" EmptyText="请选择月份"
                                                Width="200" />
                                            <ext:DateField runat="server" ID="DateFieldNorthBeginDate" FieldLabel="开始日期" LabelAlign="Right"
                                                Format="yyyy-MM-dd" AllowBlank="false" Editable="false" EmptyText="请选择日期" Hidden="true"
                                                Width="200" />
                                            <ext:DateField runat="server" ID="DateFieldNorthEndDate" FieldLabel="结束日期" LabelAlign="Right"
                                                Format="yyyy-MM-dd" AllowBlank="false" Editable="false" EmptyText="请选择日期" Hidden="true"
                                                Width="200" />
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FieldSet>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthZJSID" FieldLabel="主机手" LabelAlign="Right"
                                Editable="false" ColumnWidth="0.5">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清除" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="ComboBoxNorthZJSID_TriggerClick" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthEquipCode" FieldLabel="机台" LabelAlign="Right"
                                Editable="false" ColumnWidth="0.5">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清除" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="ComboBoxNorthEquipCode_TriggerClick" />
                                </Listeners>
                            </ext:ComboBox>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center">
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
