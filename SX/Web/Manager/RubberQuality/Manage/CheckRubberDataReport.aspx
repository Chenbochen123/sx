<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckRubberDataReport.aspx.cs" Inherits="Manager_RubberQuality_Manage_CheckRubberDataReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>检验数据查询</title>
    <script type="text/javascript">
        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            hidden: true,
            width: 370,
            height: 470,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx?MajorTypeID=5,5' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料",
            modal: true
        })

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {
            App.txMaterialName.getTrigger(0).show();
            //App.hiddenMaterialCode.setValue(record.data.MaterialCode);
            App.txMaterialName.setValue(record.data.MaterialName);
        }
        var QueryMaterialInfo = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    //App.hiddenMaterialCode.setValue('');
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
                    break;
            }
        }

        var change = function (value, metadata, record, rowIndex, columnIndex, stroe) {
            debugger;
            if (value == null) { return ""; }
            value = value.toString();
            if (value == "0") {
                return "";
            }
            return value;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="ResourceManager1" />
    <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
        <Items>
            <ext:Hidden runat="server" ID="HiddenBeginDate" />
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
                    <ext:DateField runat="server" ID="DateFieldNorthBeginDate" FieldLabel="开始日期" LabelWidth="70"
                        LabelAlign="Right" Editable="false" Format="yyyy-MM-dd" InputWidth="110">
                    </ext:DateField>
                     <ext:DateField runat="server" ID="DateFieldNorthEndDate" FieldLabel="结束日期" LabelWidth="70"
                        LabelAlign="Right" Editable="false" Format="yyyy-MM-dd" InputWidth="110">
                    </ext:DateField>
                    <ext:ComboBox runat="server" ID="ComboBoxNorthItemType" FieldLabel="检验类型" LabelAlign="Right"
                        Editable="false">
                    </ext:ComboBox>
                    <ext:TriggerField runat="server" ID="txMaterialName" FieldLabel="物料名称" LabelAlign="Right" Editable="false">
                        <Triggers>
                            <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                            <ext:FieldTrigger Icon="Search" />
                        </Triggers>
                        <Listeners>
                            <TriggerClick Fn="QueryMaterialInfo" />
                        </Listeners>
                    </ext:TriggerField>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="FitLayout">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenter" ColumnLines="true" AnchorHeight="100">
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
