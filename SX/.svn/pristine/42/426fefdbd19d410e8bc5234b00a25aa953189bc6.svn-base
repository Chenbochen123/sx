<%@ Page Language="C#" AutoEventWireup="true" CodeFile="THSC.aspx.cs" Inherits="Manager_ReportCenter_THSC_THSC" %>
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
        var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {//用户返回值处理
            App.cbxUser.getTrigger(0).show();
            App.cbxUser.setValue(record.data.UserName);
               
        }
        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
        
                App.txtMaterName.getTrigger(0).show();
                App.txtMaterName.setValue(record.data.MaterialName);
                App.hiddenMaterCode.setValue(record.data.MaterialCode);
        
        }

        var QueryMaterial = function (field, trigger, index) {//物料查询
        
//                    App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
    switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.txtMaterName.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                   App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
                    break;
            }

                }
                var QueryUser = function (field, trigger, index) {//物料查询
                    switch (index) {
                        case 0:
                            field.getTrigger(0).hide();
                            field.setValue('');
                         
                            field.getEl().down('input.x-form-text').setStyle('background', "white");
                            break;
                        case 1:
                            App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();
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
                           <%--   <ext:ComboBox ID="PIsInject" runat="server" LabelAlign="Right" Flex="1" FieldLabel="喷射使用与否"
                                        SelectOnTab="true" Editable="false" />--%>
                             <%--         <ext:ComboBox ID="cbxUser" runat="server" FieldLabel="人员" LabelAlign="Right">        </ext:ComboBox>--%>
                                      <ext:TriggerField ID="cbxUser" runat="server" FieldLabel="人员" LabelAlign="Right" Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear"  HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryUser" />
                                                </Listeners>
                                            </ext:TriggerField>
                         
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container5" runat="server" Layout="FormLayout" ColumnWidth=".2"
                            Padding="5">
                            <Items>
                            <ext:TriggerField ID="txtMaterName" runat="server" FieldLabel="物料" LabelAlign="Right" Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear"  HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryMaterial" />
                                                </Listeners>
                                            </ext:TriggerField>
                              
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
          <ext:Hidden ID="hiddenMaterCode" runat="server"></ext:Hidden>
    </form>
</body>
</html>
