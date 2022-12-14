<%@ page language="C#" autoeventwireup="true" inherits="Manager_Rubber_Report_RubberInvalidQuery, App_Web_wycfotos" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

        Ext.create("Ext.window.Window", {//库房带窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasStorage_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryBasStorage.aspx?StorageType=all&&LastStorageFlag=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择库房名称",
            modal: true
        })

        var QueryStorage = function (field, trigger, index) {//库房添加
            App.hldname.setValue(field.name);
            switch (index) {
                case 0:
                    var name = App.hldname.value;
                    if (name == "txtStorageName") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.hiddenStorageID.setValue("");
                        App.hiddenStoragePlaceID.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    else if (name == "txtToStorageName") {
                        field.getTrigger(0).hide();
                        field.setValue('');
                        App.txtToStorageName.setValue("");
                        App.hiddenToStorageID.setValue("");
                        App.hiddenToStoragePlaceID.setValue("");
                        field.getEl().down('input.x-form-text').setStyle('background', "white");
                    }
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
                    break;
            }
        }

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
        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            var name = App.hldname.value;
            if (name == "txtStorageName") {
                App.txtStorageName.getTrigger(0).show();
                App.txtStorageName.setValue(record.data.StorageName);
               
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
                App.hiddenStorageID.setValue(record.data.StorageID);
            } else if (name == "txtToStorageName") {
                App.txtToStorageName.getTrigger(0).show();
                App.txtToStorageName.setValue(record.data.StorageName);
              
                App.hiddenToStoragePlaceID.setValue(record.data.StoragePlaceID);
                App.hiddenToStorageID.setValue(record.data.StorageID);
            }

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
                                            <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" LabelAlign="Right" />
                                            <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" LabelAlign="Right" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                        Padding="2">
                                        <Items>
                                            <ext:ComboBox ID="cboMaterType" runat="server" FieldLabel="物料分类" LabelAlign="Right">
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
                                            <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="源库房" LabelAlign="Right"
                                                Flex="1" Editable="false" >
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryStorage" />
                                                </Listeners>
                                            </ext:TriggerField>
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
                                            <ext:TriggerField ID="txtToStorageName" runat="server" FieldLabel="目标库房" LabelAlign="Right"
                                                Flex="1" Editable="false" >
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryStorage" />
                                                </Listeners>
                                            </ext:TriggerField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container4" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                        Padding="2">
                                        <Items>
                                            <ext:ComboBox ID="cbxChejian" runat="server" FieldLabel="车间" LabelAlign="Right">
                                                <Items>
                                                    <ext:ListItem Text="M2车间" Value="2">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M3车间" Value="3">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M4车间" Value="4">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="M5车间" Value="5">
                                                    </ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                             <ext:ComboBox ID="cbxShift" runat="server" FieldLabel="出入库"
                                                LabelAlign="Right" >
                                                <Items>
                                                    <ext:ListItem Text="全部" Value="all">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="入库" Value="I">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="出库" Value="O">
                                                    </ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
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
