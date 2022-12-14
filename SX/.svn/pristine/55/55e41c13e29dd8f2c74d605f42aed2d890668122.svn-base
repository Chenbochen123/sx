<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckRubberQualityZJSCPKReport.aspx.cs" Inherits="Manager_RubberQuality_Manage_CheckRubberQualityZJSCPKReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>主机手CPK</title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="ResourceManager1" />
    <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
        <Items>
            <ext:Hidden runat="server" ID="HiddenBeginDate" />
            <ext:Hidden runat="server" ID="HiddenEndDate" />
            <ext:Panel runat="server" ID="PanelNorth" Region="North" Layout="ColumnLayout">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ToolbarNorth">
                        <Items>
                            <ext:Button runat="server" ID="ButtonNorthQuery" Icon="Magnifier" Text="查询">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthQuery_Click" Timeout="600000">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthExport" Icon="PageExcel" Text="导出">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthExport_Click" IsUpload="true">
                                        <ExtraParams>
                                            <ext:Parameter Name="fields" Value="#{ModelCenter}.getFields()" Mode="Raw" />
                                            <ext:Parameter Name="records" Value="#{StoreCenter}.getRecordsValues({ excludeId: true })"
                                                Mode="Raw" />
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
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="FitLayout">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenter">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenter">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenter">
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
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
