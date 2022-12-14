<%@ page language="C#" autoeventwireup="true" inherits="Manager_ReportCenter_ShopStorage_ShopConsumeTotal, App_Web_pinlblzb" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {

            App.txtEquip.getTrigger(0).show();
            App.txtEquip.setValue(record.data.EquipName);
            App.hiddenEquipCode.setValue(record.data.EquipCode);

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
        var QueryEquipmentInfo = function (field, trigger, index) {
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmChkBill" runat="server" />
        <ext:Panel ID="pnStorage" runat="server" Region="North" Height="70">
            <TopBar>
                <ext:Toolbar runat="server" ID="tbStorage">
                    <Items>
                        <ext:ToolbarSeparator ID="tsBegin" />
                        <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch" AutoPostBack="true" OnClick="btnSearch_Click">
                            <%--<DirectEvents>
                                <Click OnEvent="btnSearch_Click" />
                            </DirectEvents>--%>
                        </ext:Button>
                        <ext:ToolbarSeparator ID="tsMiddle" />
                        <ext:Button runat="server" Icon="Printer" Text="打印" ID="btnPrint">
                            <DirectEvents>
                                <Click OnEvent="btnPrint_Click" />
                            </DirectEvents>
                        </ext:Button>
                        <ext:ToolbarSeparator ID="tsEnd" />
                        <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                        <ext:ToolbarFill ID="tfEnd" />
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Items>
                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                    <Items>
                        <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".2"
                            Padding="5">
                            <Items>
                                <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" LabelAlign="Right" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".2"
                            Padding="5">
                            <Items>
                                <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" LabelAlign="Right" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container4" runat="server" Layout="FormLayout" ColumnWidth=".2"
                            Padding="5">
                            <Items>
                                <ext:ComboBox ID="cbxShiftID" runat="server" FieldLabel="班次" LabelAlign="Right">
                                    <SelectedItems>
                                        <ext:ListItem Value="all">
                                        </ext:ListItem>
                                    </SelectedItems>
                                    <Items>
                                        <ext:ListItem Text="全部" Value="all" AutoDataBind="true">
                                        </ext:ListItem>
                                        <ext:ListItem Text="早" Value="1">
                                        </ext:ListItem>
                                        <ext:ListItem Text="中" Value="2">
                                        </ext:ListItem>
                                        <ext:ListItem Text="夜" Value="3">
                                        </ext:ListItem>
                                    </Items>
                                </ext:ComboBox>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container5" runat="server" Layout="FormLayout" ColumnWidth=".2"
                            Padding="5">
                            <Items>
                                <ext:TriggerField ID="txtEquip" runat="server" FieldLabel="机台" LabelAlign="Right"
                                    LabelWidth="80" Width="220" Editable="false">
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                        <ext:FieldTrigger Icon="Search" />
                                    </Triggers>
                                    <Listeners>
                                        <TriggerClick Fn="QueryEquipmentInfo" />
                                    </Listeners>
                                </ext:TriggerField>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".2"
                            Padding="5">
                            <Items>
                                <ext:ComboBox ID="cbxMaterialype" runat="server" FieldLabel="统计分类" LabelAlign="Right">
                                    <SelectedItems>
                                        <ext:ListItem Value="all">
                                        </ext:ListItem>
                                    </SelectedItems>
                                    <Items>
                                        <ext:ListItem Text="全部" Value="all" AutoDataBind="true"></ext:ListItem>
                                        <ext:ListItem Text="生胶类" Value="1"></ext:ListItem>
                                        <ext:ListItem Text="粉料类" Value="2"></ext:ListItem>
                                        <ext:ListItem Text="炭黑类" Value="3"></ext:ListItem>
                                        <ext:ListItem Text="油类" Value="4"></ext:ListItem>
                                        <ext:ListItem Text="其它类" Value="5"></ext:ListItem>
                                    </Items>
                                </ext:ComboBox>
                            </Items>
                        </ext:Container>
                    </Items>
                    <Listeners>
                        <ValidityChange Handler="#{btnSearch}.setDisabled(!valid);" />
                    </Listeners>
                </ext:FormPanel>
            </Items>
        </ext:Panel>

        <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" Font-Bold="False" Width="100%" Zoom="1"
                Padding="3, 3, 3, 3" ToolbarColor="Lavender" PrintInPdf="True" Layers="False" />
        <ext:Hidden ID="hiddenEquipCode" runat="server"></ext:Hidden>
    </form>
</body>
</html>
