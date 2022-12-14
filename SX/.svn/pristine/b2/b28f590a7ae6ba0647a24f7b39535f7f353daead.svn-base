<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckRubberQualityCPKDailyReport.aspx.cs"
    Inherits="Manager_RubberQuality_Manage_CheckRubberQualityCPKDailyReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>胶料CPK统计日报表</title>
    <script type="text/javascript">
        Ext.create("Ext.window.Window", {//胶料查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx?MajorTypeID=5' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择胶料",
            modal: true
        });

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//胶料返回值处理
            var materCode = record.data.MaterialCode;
            var materName = record.data.MaterialName;
            App.HiddenNorthMaterCode.setValue(materCode);
            App.TriggerFieldNorthMaterName.setValue(materName);
        };

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="ResourceManager1" />
    <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
        <Items>
            <ext:Hidden runat="server" ID="HiddenMaterName" />
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
                    <ext:DateField runat="server" ID="DateFieldNorthBeginDate" FieldLabel="生产日期" LabelWidth="70"
                        LabelAlign="Right" Editable="false" Format="yyyy-MM-dd" InputWidth="110">
                    </ext:DateField>
                    <ext:DateField runat="server" ID="DateFieldNorthEndDate" FieldLabel="至" LabelWidth="20"
                        LabelAlign="Right" Editable="false" Format="yyyy-MM-dd" InputWidth="110">
                    </ext:DateField>
                    <ext:TriggerField runat="server" ID="TriggerFieldNorthMaterName" FieldLabel="胶料名称"
                        LabelAlign="Right" LabelWidth="70" InputWidth="200" Editable="false" AllowBlank="false"
                        IndicatorText="*" IndicatorCls="red-text">
                        <Triggers>
                            <ext:FieldTrigger Icon="Search" Qtip="查找" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Handler="App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();" />
                        </Listeners>
                    </ext:TriggerField>
                    <ext:Hidden runat="server" ID="HiddenNorthMaterCode" />
                    <ext:ComboBox runat="server" ID="ComboBoxNorthStandCode" FieldLabel="车间标准" Editable="false"
                        InputWidth="150" LabelWidth="70" LabelAlign="Right" MatchFieldWidth="false" AllowBlank="false"
                        IndicatorText="*" IndicatorCls="red-text">
                        <ListConfig Width="150" />
                    </ext:ComboBox>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="FitLayout">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenter">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenter">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenter">
                                        <Fields>
                                            <ext:ModelField Name="日期" />
                                            <ext:ModelField Name="总车数" />
                                            <ext:ModelField Name="合格数" />
                                            <ext:ModelField Name="不合格数" />
                                            <ext:ModelField Name="合格率" />
                                            <ext:ModelField Name="合格率2" />
                                            <ext:ModelField Name="粘度_CA" />
                                            <ext:ModelField Name="粘度_CP" />
                                            <ext:ModelField Name="粘度_CPK" />
                                            <ext:ModelField Name="焦烧_CA" />
                                            <ext:ModelField Name="焦烧_CP" />
                                            <ext:ModelField Name="焦烧_CPK" />
                                            <ext:ModelField Name="ML_CA" />
                                            <ext:ModelField Name="ML_CP" />
                                            <ext:ModelField Name="ML_CPK" />
                                            <ext:ModelField Name="MH_CA" />
                                            <ext:ModelField Name="MH_CP" />
                                            <ext:ModelField Name="MH_CPK" />
                                            <ext:ModelField Name="硬度_CA" />
                                            <ext:ModelField Name="硬度_CP" />
                                            <ext:ModelField Name="硬度_CPK" />
                                            <ext:ModelField Name="比重_CA" />
                                            <ext:ModelField Name="比重_CP" />
                                            <ext:ModelField Name="比重_CPK" />
                                            <ext:ModelField Name="T30_CA" />
                                            <ext:ModelField Name="T30_CP" />
                                            <ext:ModelField Name="T30_CPK" />
                                            <ext:ModelField Name="T60_CA" />
                                            <ext:ModelField Name="T60_CP" />
                                            <ext:ModelField Name="T60_CPK" />
                                            <ext:ModelField Name="抽出_CA" />
                                            <ext:ModelField Name="抽出_CP" />
                                            <ext:ModelField Name="抽出_CPK" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:RowNumbererColumn Width="40" />
                                <ext:Column DataIndex="日期" Text="日期" />
                                <ext:Column DataIndex="总车数" Text="总车数" />
                                <ext:Column DataIndex="合格数" Text="合格数" />
                                <ext:Column DataIndex="不合格数" Text="不合格数" />
                                <ext:Column DataIndex="合格率" Text="合格率" />
                                <ext:ComponentColumn Text="粘度">
                                    <Columns>
                                        <ext:Column DataIndex="粘度_CA" Text="CA" Width="60" />
                                        <ext:Column DataIndex="粘度_CP" Text="CP" Width="60" />
                                        <ext:Column DataIndex="粘度_CPK" Text="CPK" Width="60" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="焦烧">
                                    <Columns>
                                        <ext:Column DataIndex="焦烧_CA" Text="CA" Width="60" />
                                        <ext:Column DataIndex="焦烧_CP" Text="CP" Width="60" />
                                        <ext:Column DataIndex="焦烧_CPK" Text="CPK" Width="60" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="ML">
                                    <Columns>
                                        <ext:Column DataIndex="ML_CA" Text="CA" Width="60" />
                                        <ext:Column DataIndex="ML_CP" Text="CP" Width="60" />
                                        <ext:Column DataIndex="ML_CPK" Text="CPK" Width="60" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="MH">
                                    <Columns>
                                        <ext:Column DataIndex="MH_CA" Text="CA" Width="60" />
                                        <ext:Column DataIndex="MH_CP" Text="CP" Width="60" />
                                        <ext:Column DataIndex="MH_CPK" Text="CPK" Width="60" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="硬度">
                                    <Columns>
                                        <ext:Column DataIndex="硬度_CA" Text="CA" Width="60" />
                                        <ext:Column DataIndex="硬度_CP" Text="CP" Width="60" />
                                        <ext:Column DataIndex="硬度_CPK" Text="CPK" Width="60" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="比重">
                                    <Columns>
                                        <ext:Column DataIndex="比重_CA" Text="CA" Width="60" />
                                        <ext:Column DataIndex="比重_CP" Text="CP" Width="60" />
                                        <ext:Column DataIndex="比重_CPK" Text="CPK" Width="60" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="T30">
                                    <Columns>
                                        <ext:Column DataIndex="T30_CA" Text="CA" Width="60" />
                                        <ext:Column DataIndex="T30_CP" Text="CP" Width="60" />
                                        <ext:Column DataIndex="T30_CPK" Text="CPK" Width="60" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="T60">
                                    <Columns>
                                        <ext:Column DataIndex="T60_CA" Text="CA" Width="60" />
                                        <ext:Column DataIndex="T60_CP" Text="CP" Width="60" />
                                        <ext:Column DataIndex="T60_CPK" Text="CPK" Width="60" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn Text="抽出">
                                    <Columns>
                                        <ext:Column DataIndex="抽出_CA" Text="CA" Width="60" />
                                        <ext:Column DataIndex="抽出_CP" Text="CP" Width="60" />
                                        <ext:Column DataIndex="抽出_CPK" Text="CPK" Width="60" />
                                    </Columns>
                                </ext:ComponentColumn>
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
