<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmallMaterialWeigh-2.aspx.cs" Inherits="Manager_ProducingPlan_SmallMaterialWeigh_SmallMaterialWeigh_2" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    
        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {//机台返回信息处理
            App.txtEquip.getTrigger(0).show();
            App.txtEquip.setValue(record.data.EquipName);
            App.hiddenEquipCode.setValue(record.data.EquipCode);
//            App.pageToolBar.doRefresh();
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
                                        <ext:Container ID="ctnBeginTime" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="2">
                                            <Items>
                                                <ext:ComboBox ID="cbxTotalType" runat="server" FieldLabel="统计分类" LabelAlign="Right" Editable="false">
                                                    <SelectedItems>
                                                        <ext:ListItem Value="2"></ext:ListItem>
                                                    </SelectedItems>
                                                    <Items>
                                                        <ext:ListItem Text="人员胶号" Value="11" AutoDataBind="true"></ext:ListItem>
                                                        <ext:ListItem Text="人员" Value="01"></ext:ListItem>
                                                        <ext:ListItem Text="胶号" Value="10"></ext:ListItem>
                                                        <ext:ListItem Text="总计" Value="00"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="ctnEndTime" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="2">
                                            <Items>
                                                <ext:DateField runat="server" ID="txtstart"  FieldLabel="开始时间" LabelAlign="Right" Format="yyyy-MM-dd" Editable="false" AllowBlank="false" />
                                            </Items>
                                         <%--   Type="Month"--%>
                                        </ext:Container>
                                        <ext:Container ID="ctnShift" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                            Padding="2">
                                         <Items>
                                                <ext:DateField runat="server" ID="txtend" FieldLabel="结束时间" LabelAlign="Right" Format="yyyy-MM-dd" Editable="false" AllowBlank="false" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="ctnShiftClass" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="2">
                                            <Items>
                                                <ext:TriggerField ID="txtEquip" runat="server" FieldLabel="机台" LabelAlign="Right"
                                                    Editable="false">
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
                                        <ext:Container ID="ctnEquipName" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="2">
                                            <Items>
                                                <ext:ComboBox ID="ComboBox1" runat="server" FieldLabel="数量重量" LabelAlign="Right" Editable="false">
                                                    <SelectedItems>
                                                        <ext:ListItem Value="2"></ext:ListItem>
                                                    </SelectedItems>
                                                    <Items>
                                                        <ext:ListItem Text="按数量统计" Value="1" ></ext:ListItem>
                                                        <ext:ListItem Text="按重量统计" Value="0"></ext:ListItem>
                                                      
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
                <ext:Panel runat="server" ID="PanelCenter" Region="Center" AutoScroll="true">
                    <Content>
                      <%--  <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" Font-Bold="False"
                           Width="100%" Zoom="1" Padding="1, 1,1, 1" ToolbarColor="Lavender"
                            PrintInPdf="True" Layers="True" PdfEmbeddingFonts="false" ShowExports="false"
                            ShowRefreshButton="false" ShowToolbar="false" BorderColor="White"  />--%>

                             <cc1:WebReport ID="WebReport1" runat="server" 
         Width="100%" Zoom="1"
            Padding="3, 3, 3, 3" ToolbarColor="Lavender" PrintInPdf="False" Layers="True" />
                    </Content>
                </ext:Panel>
            </Items>
        </ext:Viewport>
        <ext:Hidden ID="hiddenEquipCode" runat="server" />
        <ext:Hidden ID="hiddenUserID" runat="server" />
    </form>
</body>
</html>

