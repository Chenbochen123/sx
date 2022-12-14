<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckRubberQualityReport.aspx.cs"
    Inherits="Manager_RubberQuality_Manage_CheckRubberQualityReport" %>

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
            <ext:Hidden runat="server" ID="HiddenMonth" />
            <ext:Hidden runat="server" ID="HiddenWorkShopCode" />
            <ext:Hidden runat="server" ID="HiddenWorkShopName" />
            <ext:Hidden runat="server" ID="HiddenCheckTypeCode" />
            <ext:Hidden runat="server" ID="HiddenRubTypeCode" />
            <ext:Hidden runat="server" ID="HiddenCheckPlanDate" />
            <ext:Hidden runat="server" ID="HiddenShiftCheckId" />
            <ext:Panel runat="server" ID="PanelNorth" Region="North" Layout="ColumnLayout">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ToolbarNorth">
                        <Items>
                            <ext:Button runat="server" ID="ButtonNorthQuery" Text="查询">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthQuery_Click">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthExport" Text="导出">
                                <Listeners>
                                    <Click Fn="ButtonNorthExport_Click" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:DateField runat="server" ID="DateFieldNorthMonth" Type="Month" FieldLabel="月份"
                        LabelWidth="80" Width="200" LabelAlign="Right" Format="yyyy-MM" Editable="false" />
                    <ext:ComboBox runat="server" ID="ComboBoxNorthWorkShop" FieldLabel="生产车间" LabelAlign="Right"
                        LabelWidth="80" Width="200" EmptyText="全部" Editable="false">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="this.setValue('');" />
                        </Listeners>
                    </ext:ComboBox>
                    <ext:ComboBox runat="server" ID="ComboBoxNorthCheckTypeCode" FieldLabel="类别" LabelWidth="80" Width="200" LabelAlign="Right" Editable="false">
                        <Items>
                            <ext:ListItem Mode="Value" Text="检验" Value="2" />
                            <ext:ListItem Mode="Value" Text="考核" Value="1" />
                        </Items>
                    </ext:ComboBox>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="FitLayout">
                <TopBar>
                    <ext:StatusBar runat="server" ID="StatusBarCenter" Text="双击查询结果行，可以查看详细信息">
                    </ext:StatusBar>
                </TopBar>
                <Loader ID="LoaderCenter" runat="server" AutoLoad="false" DirectMethod="#{DirectMethods}.GetTabPanelCenterContent"
                    Mode="Component" RemoveAll="true">
                    <LoadMask ShowMask="true" />
                </Loader>
            </ext:Panel>
            <ext:Window runat="server" ID="WindowView" Hidden="true" Width="750" Height="500"
                Layout="BorderLayout">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ToolbarView">
                        <Items>
                            <ext:Button runat="server" ID="ButtonViewShowAll" Icon="ApplicationViewTile" Text="显示全部">
                                <DirectEvents>
                                    <Click OnEvent="ButtonViewShowAll_Click">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonViewShowUnQuality" Icon="ApplicationViewGallery" Text="显示不合格">
                                <DirectEvents>
                                    <Click OnEvent="ButtonViewShowUnQuality_Click">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonViewClose" Icon="Delete" Text="关闭窗口">
                                <Listeners>
                                    <Click Handler="#{WindowView}.close();" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel runat="server" ID="PanelViewNorth" Region="North" Layout="ColumnLayout">
                        <Items>
                            <ext:TextField runat="server" ID="TextFieldViewNorthCheckPlanDate" FieldLabel="检验日期" LabelAlign="Right" ReadOnly="true" />
                            <ext:TextField runat="server" ID="TextFieldViewNorthShiftCheckId" FieldLabel="检验班次" LabelAlign="Right" ReadOnly="true" />
                            <ext:Checkbox runat="server" ID="CheckboxViewNorthJudgeResult" FieldLabel="不合格" LabelAlign="Right" ReadOnly="true" />
                        </Items>
                    </ext:Panel>
                    <ext:GridPanel runat="server" ID="GridPanelViewLot" Region="Center" AutoScroll="true">
                        <Store>
                            <ext:Store runat="server" ID="StoreViewLot">
                                <Model>
                                    <ext:Model runat="server" ID="ModelViewLot">
                                        <Fields>
                                            <ext:ModelField Name="CheckCode" />
                                            <ext:ModelField Name="SerialId" />
                                            <ext:ModelField Name="LLSerialID" />
                                            <ext:ModelField Name="IfCheckNum" />
                                            <ext:ModelField Name="CheckPlanDate" />
                                            <ext:ModelField Name="MaterName" />
                                            <ext:ModelField Name="EquipName" />
                                            <ext:ModelField Name="ZJSID" />
                                            <ext:ModelField Name="DetailAmount" />
                                            <ext:ModelField Name="Amount_LB" />
                                            <ext:ModelField Name="Amount_BZ" />
                                            <ext:ModelField Name="Amount_YD" />
                                            <ext:ModelField Name="Amount_CC" />
                                            <ext:ModelField Name="Amount_MN" />
                                            <ext:ModelField Name="Amount_JS" />
                                            <ext:ModelField Name="NotQuaCompute" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:RowNumbererColumn runat="server" Width="40" />
                                <ext:CheckColumn runat="server" DataIndex="NotQuaCompute" Text="默认合格" Width="60" Hidden="true" />
                                <ext:Column runat="server" DataIndex="ZJSID" Text="主机手" Width="60" />
                                <ext:Column runat="server" DataIndex="LLSerialID" Text="车次" Width="50" />
                                <ext:Column runat="server" DataIndex="SerialId" Text="生产车次" Width="70" Hidden="true" />
                                <ext:Column runat="server" DataIndex="MaterName" Text="胶料名称" Width="120" />
                                <ext:Column runat="server" DataIndex="EquipName" Text="生产机台" Width="160" />
                                <ext:Column runat="server" DataIndex="Amount_LB" Text="硫变仪" Width="60">
                                    <Renderer Fn="pctChange" />
                                </ext:Column>
                                <ext:Column runat="server" DataIndex="Amount_YD" Text="硬度" Width="50">
                                    <Renderer Fn="pctChange" />
                                </ext:Column>
                                <ext:Column runat="server" DataIndex="Amount_BZ" Text="比重" Width="50">
                                    <Renderer Fn="pctChange" />
                                </ext:Column>
                                <ext:Column runat="server" DataIndex="Amount_CC" Text="抽出" Width="50">
                                    <Renderer Fn="pctChange" />
                                </ext:Column>
                                <ext:Column runat="server" DataIndex="Amount_MN" Text="粘度" Width="50">
                                    <Renderer Fn="pctChange" />
                                </ext:Column>
                                <ext:Column runat="server" DataIndex="Amount_JS" Text="焦烧" Width="50">
                                    <Renderer Fn="pctChange" />
                                </ext:Column>
                                <ext:Column runat="server" DataIndex="DetailAmount" Text="合计" Width="60" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel runat="server" ID="RowSelectionModelViewLot">
                                <DirectEvents>
                                    <SelectionChange OnEvent="RowSelectionModelViewLot_SelectionChange">
                                        <EventMask ShowMask="true" />
                                        <ExtraParams>
                                            <ext:Parameter Name="CheckCode" Value="selected[0].data.CheckCode" Mode="Raw" />
                                            <ext:Parameter Name="SerialId" Value="selected[0].data.SerialId" Mode="Raw" />
                                            <ext:Parameter Name="LLSerialID" Value="selected[0].data.LLSerialID" Mode="Raw" />
                                            <ext:Parameter Name="IfCheckNum" Value="selected[0].data.IfCheckNum" Mode="Raw" />
                                        </ExtraParams>
                                    </SelectionChange>
                                </DirectEvents>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                    </ext:GridPanel>
                    <ext:GridPanel runat="server" ID="GridPanelViewDetail" Region="South" AutoScroll="true" Height="200">
                        <Store>
                            <ext:Store runat="server" ID="StoreViewDetail">
                                <Model>
                                    <ext:Model runat="server" ID="ModelViewDetail">
                                        <Fields>
                                            <ext:ModelField Name="StandId" />
                                            <ext:ModelField Name="ItemCd" />
                                            <ext:ModelField Name="ItemName" />
                                            <ext:ModelField Name="ItemCheck" />
                                            <ext:ModelField Name="PermMax" />
                                            <ext:ModelField Name="PermMin" />
                                            <ext:ModelField Name="JudgeMemo" />
                                            <ext:ModelField Name="JudgeResultDes" />
                                            <ext:ModelField Name="CheckDateTime" />
                                            <ext:ModelField Name="StandCode" />
                                            <ext:ModelField Name="StandTypeName" />
                                            <ext:ModelField Name="Grade" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:Column runat="server" DataIndex="ItemName" Text="检验项目" />
                                <ext:Column runat="server" DataIndex="JudgeResultDes" Text="判级结果">
                                    <Renderer Fn="change" />
                                </ext:Column>
                                <ext:Column runat="server" DataIndex="ItemCheck" Text="检验值" />
                                <ext:Column runat="server" DataIndex="PermMax" Text="标准上限" />
                                <ext:Column runat="server" DataIndex="PermMin" Text="标准下限" />
                                <ext:Column runat="server" DataIndex="StandTypeName" Text="标准类型" />
                                <ext:Column runat="server" DataIndex="JudgeMemo" Text="备注" Hidden="true" />
                                <ext:Column runat="server" DataIndex="CheckDateTime" Text="检验时间" />
                                <ext:Column runat="server" DataIndex="StandId" Text="标准编号" Hidden="true" />
                                <ext:Column runat="server" DataIndex="StandCode" Text="标准类型编号" Hidden="true" />
                            </Columns>
                        </ColumnModel>
                    </ext:GridPanel>
                </Items>
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
