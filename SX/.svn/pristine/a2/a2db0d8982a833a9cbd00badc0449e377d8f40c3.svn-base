<%@ page language="C#" autoeventwireup="true" inherits="Manager_Rubber_Report_PpmScanCalcRate, App_Web_wycfotos" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript">
        var QueryEquipmentInfo = function (field, trigger, index) { //机台添加
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenEquipCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
            }
        }
        var QueryUser = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenUserID.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();
                    break;
            }
        }

        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            height: 450,
            hidden: true,
            width: 370,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择机台",
            modal: true
        })
        Ext.create("Ext.window.Window", {//人员信息带回查询信息
            id: "Manager_BasicInfo_CommonPage_QueryBasUser_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryBasUser.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择人员",
            modal: true
        })

        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {//机台返回信息处理
            App.txtEquip.getTrigger(0).show();
            App.txtEquip.setValue(record.data.EquipName);
            App.hiddenEquipCode.setValue(record.data.EquipCode);
            App.pageToolBar.doRefresh();
        }
        var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {//用户返回值处理
            App.txtUserName.getTrigger(0).show();
            App.txtUserName.setValue(record.data.UserName);
            App.hiddenUserID.setValue(record.data.HRCode);
        }
    </script>
</head>
<body>
    <form id="form2" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExport_Click" />
        <ext:ResourceManager ID="rmReport" runat="server" />
        <ext:Viewport ID="vpReport" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnReportTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="tbReport">
                            <Items>
                                <ext:Button runat="server" ID="btnSearch" Text="查询" Icon="Magnifier">
                                    <DirectEvents>
                                        <Click OnEvent="btnSearch_Click">
                                            <EventMask ShowMask="true" Target="Page" />
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsBegin" />
                                <ext:Button runat="server" ID="btnExport" Text="导出" Icon="PageExcel">
                                    <DirectEvents>
                                        <Click IsUpload="true" OnEvent="btnExport_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsEnd" />
                                <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                                <ext:ToolbarFill ID="tfEnd" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel ID="Panel1" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="FormPanel2" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="ctnBeginTime" runat="server" Layout="FormLayout" ColumnWidth=".33" Padding="2">
                                            <Items>
                                                <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" Editable="false" LabelAlign="Right" />
                                                <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" Editable="false" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="ctnShift" runat="server" Layout="FormLayout" ColumnWidth=".33" Padding="2">
                                            <Items>
                                                <ext:ComboBox ID="cbxWorkShopCode" runat="server" FieldLabel="车间" LabelAlign="Right" Editable="false">
                                                    <SelectedItems>
                                                        <ext:ListItem Value="2"></ext:ListItem>
                                                    </SelectedItems>
                                                    <Items>
                                                        <ext:ListItem Text="M2车间" Value="2" AutoDataBind="true"></ext:ListItem>
                                                        <ext:ListItem Text="M3车间" Value="3"></ext:ListItem>
                                                        <ext:ListItem Text="M4车间" Value="4"></ext:ListItem>
                                                        <ext:ListItem Text="M5车间" Value="5"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                                <ext:TriggerField ID="txtUserName" runat="server" FieldLabel="主机手" LabelAlign="Right" Editable="false">
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="QueryUser" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="ctnShiftClass" runat="server" Layout="FormLayout" ColumnWidth=".34" Padding="2">
                                            <Items>
                                                <ext:ComboBox ID="cbxTotalType" runat="server" FieldLabel="统计类型" LabelAlign="Right" Editable="false">
                                                    <SelectedItems>
                                                        <ext:ListItem Value="1"></ext:ListItem>
                                                    </SelectedItems>
                                                    <Items>
                                                        <ext:ListItem Text="车间对比" Value="1" AutoDataBind="true"></ext:ListItem>
                                                        <ext:ListItem Text="机台对比" Value="2"></ext:ListItem>
                                                        <ext:ListItem Text="主机手对比" Value="3"></ext:ListItem>
                                                        <ext:ListItem Text="明细查询" Value="4"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:FormPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:Panel runat="server" ID="PanelCenter1" Region="Center" AutoScroll="true">
                    <Content>
                        <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" Font-Bold="False" 
                            Width="112cm" Height="87cm" Zoom="1" Padding="3, 3, 3, 3" ToolbarColor="Lavender"
                            PrintInPdf="True" Layers="False" PdfEmbeddingFonts="false" ShowExports="false"
                            ShowRefreshButton="false" ShowToolbar="false" BorderColor="White" />
                    </Content>
                </ext:Panel>
            </Items>
        </ext:Viewport>
        <ext:Hidden ID="hiddenEquipCode" runat="server" />
        <ext:Hidden ID="hiddenUserID" runat="server" />
    </form>
</body>
</html>
