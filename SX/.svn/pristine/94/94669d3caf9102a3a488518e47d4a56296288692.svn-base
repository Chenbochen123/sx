<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckDataImportLog.aspx.cs"
    Inherits="Manager_RubberQuality_BasicInfo_CheckDataImportLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button"
        OnClick="btnExportSubmit_Click" />
    <ext:ResourceManager runat="server" ID="ResourceManager1" />
    <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" ID="PanelNorth" Region="North">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ToolbarNorth">
                        <Items>
                            <ext:Button runat="server" ID="ButtonNorthQuery" Icon="Magnifier" Text="查询">
                                <Listeners>
                                    <Click Fn="ButtonNorthQuery_Click" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthExcel" Icon="PageExcel" Text="导出">
                                <Listeners>
                                    <Click Fn="ButtonNorthExcel_Click">
                                    </Click>
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel runat="server" ID="PanelNorthQuery" Width="750" Layout="ColumnLayout"
                        Height="30">
                        <Items>
                            <ext:TextField runat="server" ID="TextFieldNorthFileName" FieldLabel="文件名称" ColumnWidth=".4"
                                LabelAlign="Right" />
                            <ext:DateField runat="server" ID="DateFieldNorthImportDate" FieldLabel="导入日期" Editable="false"
                                Format="yyyy-MM-dd" ColumnWidth=".3" LabelAlign="Right">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="DateFieldNorthImportDate_TriggerClick" />
                                </Listeners>
                            </ext:DateField>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthImportFlag" FieldLabel="导入状态" Editable="false"
                                LabelAlign="Right" EmptyText="全部" ColumnWidth=".3">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                </Triggers>
                                <Items>
                                    <ext:ListItem Mode="Value" Value="1" Text="已导入" />
                                    <ext:ListItem Mode="Value" Value="0" Text="未导入" />
                                </Items>
                                <Listeners>
                                    <TriggerClick Handler="this.setValue('');" />
                                </Listeners>
                            </ext:ComboBox>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="BorderLayout">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenter" Region="Center">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenter">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenter" IDProperty="ObjId">
                                        <Fields>
                                            <ext:ModelField Name="ObjId" />
                                            <ext:ModelField Name="FileName" />
                                            <ext:ModelField Name="SheetName" />
                                            <ext:ModelField Name="OperInfo" />
                                            <ext:ModelField Name="PlanDate" />
                                            <ext:ModelField Name="ClassName" />
                                            <ext:ModelField Name="ShiftName" />
                                            <ext:ModelField Name="TypeName" />
                                            <ext:ModelField Name="RecordTime" Type="Date" />
                                            <ext:ModelField Name="RecorderName" />
                                            <ext:ModelField Name="Flag" />
                                            <ext:ModelField Name="GUID" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:RowNumbererColumn runat="server" ID="RowNumbererColumnCenter" Width="40" Align="Left" />
                                <ext:Column runat="server" ID="ColumnCenterFileName" DataIndex="FileName" Text="文件名" />
                                <ext:Column runat="server" ID="ColumnCenterSheetName" DataIndex="SheetName" Text="Sheet页" />
                                <ext:Column runat="server" ID="ColumnCenterOperInfo" DataIndex="OperInfo" Text="操作员"
                                    Width="150" />
                                <ext:Column runat="server" ID="ColumnCenterPlanDate" DataIndex="PlanDate" Text="生产日期" />
                                <ext:Column runat="server" ID="ColumnCenterClassName" DataIndex="ClassName" Text="生产班组" />
                                <ext:Column runat="server" ID="ColumnCenterShiftName" DataIndex="ShiftName" Text="生产班次" />
                                <ext:Column runat="server" ID="ColumnCenterTypeName" DataIndex="TypeName" Text="标准类型" />
                                <ext:DateColumn runat="server" ID="DateColumnCenterRecordTime" DataIndex="RecordTime"
                                    Text="上传时间" Format="yyyy-MM-dd HH:mm:ss" Width="150" />
                                <ext:Column runat="server" ID="ColumnCenterRecorderName" DataIndex="RecorderName"
                                    Text="上传人" />
                                <ext:Column runat="server" ID="ColumnCenterFlag" DataIndex="Flag" Text="状态">
                                    <Renderer Fn="ColumnCenterFlag_Render" />
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:CheckboxSelectionModel runat="server" ID="CheckboxSelectionModelCenter" Mode="Single"
                                AllowDeselect="true">
                                <Listeners>
                                    <SelectionChange Fn="CheckboxSelectionModelCenter_SelectionChange" />
                                </Listeners>
                            </ext:CheckboxSelectionModel>
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar runat="server" ID="PagingToolbarCenter" HideRefresh="true">
                                <Plugins>
                                    <ext:ProgressBarPager runat="server" ID="ProgressBarPager" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                    <ext:GridPanel runat="server" ID="GridPanelCenterDetail" Region="South" Height="200">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenterDetail">
                                <Model>
                                    <ext:Model runat="server">
                                        <Fields>
                                            <ext:ModelField Name="EquipName" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="CheckShiftName" />
                                            <ext:ModelField Name="CheckClassName" />
                                            <ext:ModelField Name="CheckEquipName" />
                                            <ext:ModelField Name="ItemName" />
                                            <ext:ModelField Name="CheckTime" />
                                            <ext:ModelField Name="CheckPlanDate" />
                                            <ext:ModelField Name="SerialId" />
                                            <ext:ModelField Name="LLSerialID" />
                                            <ext:ModelField Name="ZJSID" />
                                            <ext:ModelField Name="ItemCheck" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel runat="server">
                            <Columns>
                                <ext:Column runat="server" DataIndex="EquipName" Text="生产机台" />
                                <ext:Column runat="server" DataIndex="MaterialName" Text="胶料名称" />
                                <ext:Column runat="server" DataIndex="CheckShiftName" Text="检验班次" />
                                <ext:Column runat="server" DataIndex="CheckClassName" Text="检验班组" />
                                <ext:Column runat="server" DataIndex="CheckEquipName" Text="检验机台" />
                                <ext:Column runat="server" DataIndex="ZJSID" Text="主机手" />
                                <ext:Column runat="server" DataIndex="SerialId" Text="车次" />
                                <ext:Column runat="server" DataIndex="LLSerialID" Text="玲珑车次" />
                                <ext:Column runat="server" DataIndex="ItemName" Text="检验项目" />
                                <ext:Column runat="server" DataIndex="ItemCheck" Text="检验值" />
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
