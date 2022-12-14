<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RubberConsume.aspx.cs" Inherits="Manager_Rubber_Report_RubberConsume" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
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
        var QueryMaterial = function (field, trigger, index) {//物料查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenMaterCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
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

        Ext.create("Ext.window.Window", {//物料带窗体
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料",
            modal: true
        })

       



        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {
     
                App.txtEquip.getTrigger(0).show();
                App.txtEquip.setValue(record.data.EquipName);
                App.hiddenEquipCode.setValue(record.data.EquipCode);
            
        }

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
           
                App.txtMaterName.getTrigger(0).show();
                App.txtMaterName.setValue(record.data.MaterialName);
                App.hiddenMaterCode.setValue(record.data.MaterialCode);
            
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmMmShopout" runat="server" />
    <ext:Viewport ID="vpMmShopout" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnMmShopoutTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                   <ext:Toolbar runat="server" ID="ToolbarNorth">
                        <Items>
                            <ext:Button runat="server" ID="btnSearch" Text="查询" Icon="Magnifier">
                                <DirectEvents>
                                    <Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="btnExport" Text="导出" Icon="PageExcel">
                                <DirectEvents>
                                    <Click IsUpload="true" OnEvent="btnExport_Click">
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel ID="pnlStorageQuery" runat="server" AutoHeight="true">
                        <Items>
                            <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="Container2" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                        Padding="2">
                                        <Items>
                                            <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" LabelAlign="Right" Editable="false" />
                                            <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" LabelAlign="Right" Editable="false" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                        Padding="2">
                                        <Items>
                                            <ext:ComboBox ID="cboMaterType" runat="server" FieldLabel="物料分类" LabelAlign="Right" Editable="false">
                                                <Items>
                                                    <ext:ListItem Text="母炼胶" Value="4">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="终炼胶" Value="5">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="返回胶" Value="6">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="其他" Value="05">
                                                    </ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container3" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                        Padding="2">
                                        <Items>
                                            <ext:TriggerField ID="txtMaterName" runat="server" FieldLabel="物料名称" LabelAlign="Right"
                                                Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryMaterial" />
                                                </Listeners>
                                            </ext:TriggerField>
                                        </Items>
                                    </ext:Container>
<%--                                    <ext:Container ID="Container4" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                        Padding="2">
                                        <Items>
                                            <ext:ComboBox ID="cbxChejian" runat="server" FieldLabel="车间" LabelAlign="Right" Editable="false" />
                                        </Items>
                                    </ext:Container>--%>
                                    <ext:Container ID="Container5" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                        Padding="2">
                                        <Items>
                                            <ext:TriggerField ID="txtEquip" runat="server" FieldLabel="机台" Padding="5" LabelAlign="Right"
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
                                </Items>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" AutoScroll="true">
                <Content>
                    <cc1:webreport id="WebReport1" runat="server" backcolor="White" font-bold="False"
                        width="1000cm" height="1000cm" zoom="1" padding="3, 3, 3, 3" toolbarcolor="Lavender"
                        printinpdf="True" layers="False" pdfembeddingfonts="false" showexports="false"
                        showrefreshbutton="false" showtoolbar="false" bordercolor="White" />
                </Content>
            </ext:Panel>
            
            <ext:Hidden ID="hiddenEquipCode" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hiddenMaterCode" runat="server">
            </ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
