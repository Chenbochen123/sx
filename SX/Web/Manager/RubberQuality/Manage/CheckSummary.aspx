<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckSummary.aspx.cs" Inherits="Manager_RubberQuality_Manage_CheckSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>胶料质检数据汇总</title>
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
                            <ext:Button runat="server" ID="ButtonQuery" Icon="Magnifier" Text="查询">
                                <DirectEvents>
                                    <Click OnEvent="ButtonQuery_Click">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonExcel" Icon="PageExcel" Text="导出">
                                <DirectEvents>
                                    <Click Before="ButtonExcel_BeforeClick" IsUpload="true" OnEvent="ButtonExcel_Click">
                                        <ExtraParams>
                                            <ext:Parameter Name="RecordCount" Value="#{StoreMain}.getTotalCount()" Mode="Raw" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel runat="server" Layout="ColumnLayout" Border="false">
                        <Items>
                            <ext:DateField runat="server" ID="DateFieldSDate" FieldLabel="起始生产日期" LabelAlign="Right"
                                LabelWidth="80" Width="200" EmptyText="请选择..." Format="yyyy-MM-dd" Editable="false" />
                            <ext:DateField runat="server" ID="DateFieldEDate" FieldLabel="截止生产日期" LabelAlign="Right"
                                LabelWidth="80" Width="200" EmptyText="请选择..." Format="yyyy-MM-dd" Editable="false" />
                            <ext:ComboBox runat="server" ID="ComboBoxWorkShopId" FieldLabel="生产车间" LabelAlign="Right"
                                LabelWidth="80" Width="200" EmptyText="全部" Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="this.setValue('');" />
                                </Listeners>
                            </ext:ComboBox>
                        </Items>
                    </ext:Panel>
                    <ext:Panel runat="server" Height="2" Border="false" />
                    <ext:Panel runat="server" Layout="ColumnLayout" Border="false">
                        <Items>
                            <ext:TriggerField runat="server" ID="TriggerFieldEquipName" FieldLabel="生产机台" LabelAlign="Right"
                                LabelWidth="80" Width="200" EmptyText="全部" Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                    <ext:FieldTrigger Icon="Search" Qtip="查找" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="
                                        if(index == 0) { 
                                            this.setValue('');
                                            App.HiddenEquipCode.setValue('');
                                        } else if (index == 1) {
                                            App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                                        }" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:Hidden runat="server" ID="HiddenEquipCode" />
                            <ext:ComboBox runat="server" ID="ComboBoxShiftId" FieldLabel="生产班次" LabelAlign="Right"
                                LabelWidth="80" Width="200" EmptyText="全部" Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="this.setValue('');" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxShiftClass" FieldLabel="生产班组" LabelAlign="Right"
                                LabelWidth="80" Width="200" EmptyText="全部" Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="this.setValue('');" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxZJSID" FieldLabel="主机手" LabelAlign="Right"
                                LabelWidth="80" Width="200" EmptyText="全部" Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="this.setValue('');" />
                                </Listeners>
                            </ext:ComboBox>
                        </Items>
                    </ext:Panel>
                    <ext:Panel runat="server" Height="2" Border="false" />
                    <ext:Panel runat="server" Layout="ColumnLayout" Border="false">
                        <Items>
                            <ext:TriggerField runat="server" ID="TriggerFieldMaterName" FieldLabel="胶料名称" LabelAlign="Right"
                                LabelWidth="80" Width="300" EmptyText="全部" Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                    <ext:FieldTrigger Icon="Search" Qtip="查找" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="TriggerFieldMaterName_TriggerClick" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:ComboBox runat="server" ID="ComboBoxStandCode" FieldLabel="标准分类" LabelAlign="Right"
                                LabelWidth="80" Width="300" EmptyText="全部" Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="this.setValue('');" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:Hidden runat="server" ID="HiddenMaterCode" />
                            <ext:ComboBox runat="server" ID="ComboBoxJudgeResult" FieldLabel="是否合格" LabelAlign="Right"
                                LabelWidth="80" Width="200" EmptyText="全部" Editable="false">
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

                            <ext:ComboBox ID="cboType" runat="server" SelectOnTab="true" Editable="false"  LabelAlign="Right" LabelWidth="70"  FieldLabel="配方类型">
                            </ext:ComboBox>
                        </Items>
                    </ext:Panel>
                    <ext:Panel runat="server" Height="2" Border="false" />
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="FitLayout">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelMain">
                        <TopBar>
                            <ext:PagingToolbar runat="server" ID="PagingToolbarMain" HideRefresh="true">
                                <Plugins>
                                    <ext:ProgressBarPager />
                                </Plugins>
                            </ext:PagingToolbar>
                        </TopBar>
                        <Store>
                            <ext:Store runat="server" ID="StoreMain" PageSize="10" AutoLoad="false" RemoteSort="true"
                                OnReadData="StoreMain_ReadData">
                                <Proxy>
                                    <ext:PageProxy />
                                </Proxy>
                                <Model>
                                    <ext:Model runat="server" ID="ModelMain">
                                        <Fields>
                                            <ext:ModelField Name="结果" />
                                            <ext:ModelField Name="生产日期" />
                                            <ext:ModelField Name="生产班次" />
                                            <ext:ModelField Name="生产班组" />
                                            <ext:ModelField Name="生产机台" />
                                            <ext:ModelField Name="胶料名称" />
                                            <ext:ModelField Name="主机手" />
                                            <ext:ModelField Name="车间" />
                                            <ext:ModelField Name="标准分类" />
                                            <ext:ModelField Name="车次" />
                                            <ext:ModelField Name="玲珑车次" />
                                            <ext:ModelField Name="检验次数" />
                                            <ext:ModelField Name="门尼粘度(MU)" />
                                            <ext:ModelField Name="门尼焦烧(mm:ss)" />
                                            <ext:ModelField Name="ML(dNm)" />
                                            <ext:ModelField Name="MH(dNm)" />
                                            <ext:ModelField Name="Ts1(s)" />
                                            <ext:ModelField Name="T25(s)" />
                                            <ext:ModelField Name="T30(s)" />
                                            <ext:ModelField Name="T60(s)" />
                                            <ext:ModelField Name="T90(s)" />
                                              <ext:ModelField Name="BarCode" />
                                            <ext:ModelField Name="硬度(°)" />
                                            <ext:ModelField Name="比重(g/cm3)" />
                                            <ext:ModelField Name="H抽出(N)" />
                                             <ext:ModelField Name="ΔG'" />
                                            <ext:ModelField Name="T25'" />
                                            <ext:ModelField Name="X" />
                                             <ext:ModelField Name="配方类型" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                  <ext:Column ID="Column25" runat="server" DataIndex="BarCode" Text="条码号" Width="90" />
                                <ext:Column ID="Column1" runat="server" DataIndex="结果" Text="结果" Width="60">
                                    <Renderer Fn="pctChange" />
                                </ext:Column>
                                <ext:Column ID="Column2" runat="server" DataIndex="生产日期" Text="生产日期" Width="90" />
                                <ext:Column ID="Column3" runat="server" DataIndex="生产班次" Text="生产班次" Width="70" />
                                <ext:Column ID="Column4" runat="server" DataIndex="生产班组" Text="生产班组" Width="70" />
                                <ext:Column ID="Column5" runat="server" DataIndex="生产机台" Text="生产机台" Width="120" />
                                <ext:Column ID="Column6" runat="server" DataIndex="胶料名称" Text="胶料名称" Width="90" />
                                <ext:Column ID="Column29" runat="server" DataIndex="配方类型" Text="配方类型" Width="90" />
                                <ext:Column ID="Column7" runat="server" DataIndex="主机手" Text="主机手" Width="60" />
                                <ext:Column ID="Column8" runat="server" DataIndex="车间" Text="车间" Width="60" />
                                <ext:Column ID="Column9" runat="server" DataIndex="标准分类" Text="标准分类" Width="160" />
                                <ext:Column ID="Column10" runat="server" DataIndex="车次" Text="生产车次" Width="70" />
                                <ext:Column ID="Column11" runat="server" DataIndex="玲珑车次" Text="车次" Width="70" />
                                <ext:Column ID="Column12" runat="server" DataIndex="检验次数" Text="检验次数" Width="70" />
                                <ext:Column ID="Column13" runat="server" DataIndex="门尼粘度(MU)" Text="门尼粘度(MU)" Width="90">
                                    <Renderer Fn="pctChange" />
                                </ext:Column>
                                <ext:Column ID="Column14" runat="server" DataIndex="门尼焦烧(mm:ss)" Text="门尼焦烧(mm:ss)"
                                    Width="90">
                                    <Renderer Fn="change" />
                                </ext:Column>
                                <ext:Column ID="Column15" runat="server" DataIndex="ML(dNm)" Text="ML(dNm)" Width="90">
                                    <Renderer Fn="change" />
                                </ext:Column>
                                <ext:Column ID="Column16" runat="server" DataIndex="MH(dNm)" Text="MH(dNm)" Width="90">
                                    <Renderer Fn="change" />
                                </ext:Column>
                                <ext:Column ID="Column17" runat="server" DataIndex="Ts1(s)" Text="Ts1(s)" Width="70">
                                    <Renderer Fn="change" />
                                </ext:Column>
                                <ext:Column ID="Column18" runat="server" DataIndex="T25(s)" Text="T25(s)" Width="60">
                                    <Renderer Fn="change" />
                                </ext:Column>
                                <ext:Column ID="Column19" runat="server" DataIndex="T30(s)" Text="T30(s)" Width="60">
                                    <Renderer Fn="change" />
                                </ext:Column>
                                <ext:Column ID="Column20" runat="server" DataIndex="T60(s)" Text="T60(s)" Width="60">
                                    <Renderer Fn="change" />
                                </ext:Column>
                                <ext:Column ID="Column21" runat="server" DataIndex="T90(s)" Text="T90(s)" Width="60">
                                    <Renderer Fn="change" />
                                </ext:Column>
                                <ext:Column ID="Column22" runat="server" DataIndex="硬度(°)" Text="硬度(°)" Width="90">
                                    <Renderer Fn="change" />
                                </ext:Column>
                                <ext:Column ID="Column23" runat="server" DataIndex="比重(g/cm3)" Text="比重(g/cm3)" Width="90">
                                    <Renderer Fn="change" />
                                </ext:Column>
                                <ext:Column ID="Column24" runat="server" DataIndex="H抽出(N)" Text="H抽出(N)" Width="90">
                                    <Renderer Fn="change" />
                                </ext:Column>
                                                                <ext:Column ID="Column26" runat="server" DataIndex="ΔG')" Text="ΔG'" Width="90">
                                    <Renderer Fn="change" />
                                </ext:Column>
                                                                <ext:Column ID="Column27" runat="server" DataIndex="T25'" Text="T25'" Width="90">
                                    <Renderer Fn="change" />
                                </ext:Column>
                                                                <ext:Column ID="Column28" runat="server" DataIndex="X" Text="X" Width="90">
                                    <Renderer Fn="change" />
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
                        <View>
                            <ext:GridView runat="server" ID="GridViewMain">
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
