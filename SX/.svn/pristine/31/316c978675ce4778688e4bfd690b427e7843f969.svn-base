<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialStoreinPrint.aspx.cs" Inherits="Manager_Storage_MaterialStoreinPrint" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var btnPrint_Click = function () {
            var url = "Storage/BarcodePrint.aspx?Str=" + App.hiddenBillNo.getValue() + "," + App.hiddenStorageID.getValue() + "," + App.hiddenStoragePlaceID.getValue() + "," + App.txtBarcode1.getValue() + "," + App.hiddenOrderID.getValue() + "," + App.txtShelfPieceWeight1.getValue() + "," + App.txtNum1.getValue() + "," + App.txtFacBarcode1.getValue() + "," + App.txtInBarcode1.getValue() + "," + App.txtBatch1.getValue() + "," + App.txtProductPlace1.getValue();
            var tabid = "Manager_Storage_BarcodePrint";
            var tabp = parent.App.mainTabPanel;
            var tab = tabp.getComponent(tabid);
            if (tab) {
                tab.close();
            }
            var title = "条码打印预览";
            parent.addTab(tabid, title, url, true);
            parent.refresh("");
        }

        //根据按钮类别进行删除和编辑操作
        var commandcolumn_click = function (command, record) {
//            var section = App.pnlList.getView().getSelectionModel().getSelection();
//                var barcodes = "";
//                for (var i = 0; i < section.length; i++) {
//                    var bc = App.store.data.get(section[i].index).data.Barcode;
//                    barcodes = barcodes == "" ? bc : barcodes + "," + bc;
//                }
//                App.direct.btnAddSave_Click(barcodes, {
//                    success: function (result) { },
//                    failure: function (err) { Ext.Msg.alert("错误", err); }
//                });
            //            }
            var BillNo = record.data.BillNo;
            var StorageID = record.data.TargetStorage;
            var StoragePlaceID = record.data.TargetStoragePlace;
            var Barcode = record.data.Barcode;
            var OrderID = record.data.OrderID;
            App.direct.commandcolumn_direct_print(BillNo, StorageID, StoragePlaceID, Barcode, OrderID, {
                success: function (result) {
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
            return false;
        };

        //区分删除操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "print") {
                commandcolumn_direct_print(record);
            }
            return false;
        };

        Ext.apply(Ext.form.VTypes, {
            integer: function (val, field) {
                if (!val) {
                    return true;
                }
                try {
                    if (/^[\d]+$/.test(val)) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                catch (e) {
                    return false;
                }
            },
            integerText: "此填入项格式为正整数",
            decimal: function (val, field) {
                if (!val) {
                    return true;
                }
                try {
                    if (/^[\d.\d]+$/.test(val)) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                catch (e) {
                    return false;
                }
            },
            decimalText: "此填入项格式为浮点数"
        });

        var pnlListFresh = function () {
            if (App.txtBeginTime.getValue() > App.txtEndTime.getValue()) {
                Ext.Msg.alert('操作', '开始时间不能大于结束时间！');
                return false;
            }
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        var startTrack = function () {
            this.checkboxes = [];
            var cb;

            Ext.select(".x-form-item", false).each(function (checkEl) {
                cb = Ext.getCmp(checkEl.dom.id.selected);
                cb.setValue(false);
                this.rowselect.push(cb);
            }, this);
        };

        //--查询带弹出框--BEGIN   Manager_BasicInfo_CommonPage_QueryFactoryType_Request
        var Manager_BasicInfo_CommonPage_QueryFactory_Request = function (record) {//生产厂家信息返回值处理
            if (!App.winAdd.hidden) {
                App.txtFactoryID2.setValue(record.data.FacName);
                App.hiddenFactoryID.setValue(record.data.ObjID);
            }
            else if (!App.winModify.hidden) {
                App.hiddenBillNo.setValue(record.data.BillNo);
                App.hiddenFactoryID.setValue(record.data.FactoryID);
            }
            else {
                App.txtFactoryID.setValue(record.data.FacName);
                App.hiddenFactoryID.setValue(record.data.ObjID);
            }
        }

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            if (!App.winAddDetail.hidden) {
                App.txtMaterialName3.setValue(record.data.MaterialName);
                App.hiddenMaterCode.setValue(record.data.MaterialCode);
            }
        }

        var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {//用户返回值处理
            App.txtMakerPerson.getTrigger(0).show();
            App.txtMakerPerson.setValue(record.data.UserName);
            App.hiddenMakerPerson.setValue(record.data.HRCode);
        }

        var QueryFactory = function () {//厂家查询
            App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
        }
        var AddFactory = function () {//厂家添加
            App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
        }
        var EditFactory = function () {//厂家修改
            App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
        }
        var AddMaterial = function () {//物料添加
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
        }

        var QueryUser = function (field, trigger, index) {//人员查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenMakerPerson.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();
                    break;
            }
        }

        Ext.create("Ext.window.Window", {//生产厂家带窗体
            id: "Manager_BasicInfo_CommonPage_QueryFactory_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryFactory.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择生产厂家",
            modal: true
        })

        Ext.create("Ext.window.Window", {//物料带窗体
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryMaterial.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料",
            modal: true
        })

        Ext.create("Ext.window.Window", {//人员信息带回查询信息
            id: "Manager_BasicInfo_CommonPage_QueryBasUser_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryBasUser.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择人员",
            modal: true
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmPrint" runat="server" />
    <ext:Viewport ID="vpPrint" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnPrint" runat="server" Region="North" Height="90">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbPrint">
                        <Items>
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <Listeners>
                                    <Click Fn="pnlListFresh" />
                                </Listeners>
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
                            <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:TextField ID="txtBillNo" runat="server" FieldLabel="入库单号" LabelAlign="Right" />
                                    <ext:TextField ID="txtBarcode" runat="server" FieldLabel="条码号" LabelAlign="Right" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" LabelAlign="Right" />
                                    <ext:TriggerField ID="txtMakerPerson" runat="server" FieldLabel="制单人" LabelAlign="Right" Editable="false">
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
                            <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".34"
                                Padding="5">
                                <Items>
                                    <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" LabelAlign="Right" />
                                </Items>
                            </ext:Container>
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{btnSearch}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>

            <ext:Panel ID="Panel2" runat="server" Region="Center" Frame="true" Layout="Fit" MarginsSummary="0 5 0 5">
                <Items>
                    <ext:GridPanel ID="pnlList" runat="server">
                        <Store>
                            <ext:Store ID="store" runat="server" PageSize="10">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model" runat="server" IDProperty="BillNo,Barcode">
                                        <Fields>
                                            <ext:ModelField Name="BillNo" />
                                            <ext:ModelField Name="UserName" />
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="InputDate" Type="Date" />
                                            <ext:ModelField Name="StorageName" />
                                            <ext:ModelField Name="InputNum" />
                                            <ext:ModelField Name="InputWeight" />
                                            <ext:ModelField Name="ProcDate" Type="Date" />
                                            <ext:ModelField Name="TargetStorage" />
                                            <ext:ModelField Name="TargetStoragePlace" />
                                            <ext:ModelField Name="OrderID" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel" runat="server">
                            <Columns>
                               <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                <ext:Column ID="billNo" runat="server" Text="调拨单号" DataIndex="BillNo" Flex="1" />
                                <ext:Column ID="Barcode" runat="server" Text="条码号" DataIndex="Barcode" Flex="1" />
                                <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                <ext:DateColumn ID="InputDate" runat="server" Text="调拨日期" DataIndex="InputDate" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:Column ID="StorageName" runat="server" Text="库房" DataIndex="StorageName" Flex="1" />
                                <ext:Column ID="InputNum" runat="server" Text="数量" DataIndex="InputNum" Flex="1" />
                                <ext:Column ID="InputWeight" runat="server" Text="重量" DataIndex="InputWeight" Flex="1" />
                                <ext:DateColumn ID="ProcDate" runat="server" Text="生产日期" DataIndex="ProcDate" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                    <Commands>
                                        <ext:GridCommand Icon="TableEdit" CommandName="Print" Text="生成流转卡片设置" />
                                    </Commands>
                                    <Listeners>
                                        <Command Handler="return commandcolumn_click(command, record);" />
                                    </Listeners>
                                </ext:CommandColumn>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single" />
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="pageToolBar" runat="server">
                                <Items>
                                    <ext:Label ID="Label2" runat="server" Text="每页条数:" />
                                    <ext:ToolbarSpacer ID="ToolbarSpacer2" runat="server" Width="10" />
                                    <ext:ComboBox ID="ComboBox2" runat="server" Width="80">
                                        <Items>
                                            <ext:ListItem Text="10" />
                                            <ext:ListItem Text="50" />
                                            <ext:ListItem Text="100" />
                                            <ext:ListItem Text="200" />
                                        </Items>
                                        <SelectedItems>
                                            <ext:ListItem Value="10" />
                                        </SelectedItems>
                                        <Listeners>
                                            <Select Handler="#{pnlList}.store.pageSize = parseInt(this.getValue(), 10); #{pageToolBar}.doRefresh(); return false;" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>

            <ext:Window ID="winSet" runat="server" Icon="MonitorEdit" Closable="true" Title="生成流转卡设置"
                Width="320" Height="400" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="Form">
                <Items>
                    <ext:FormPanel ID="FormPanel1" runat="server" BodyPadding="5">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <ext:TextField ID="txtBillNo1" runat="server" FieldLabel="单据号" ReadOnly="true" />
                            <ext:TextField ID="txtStorageID1" runat="server" FieldLabel="所在库房" ReadOnly="true" />
                            <ext:TextField ID="txtStoragePlaceID1" runat="server" FieldLabel="所在库位" ReadOnly="true" />
                            <ext:TextField ID="txtBarcode1" runat="server" FieldLabel="条码" ReadOnly="true" />
                            <ext:TextField ID="txtMaterCode1" runat="server" FieldLabel="物料名称" ReadOnly="true" />
                            <ext:TextField ID="txtFacBarcode1" runat="server" FieldLabel="供应商批号" />
                            <ext:TextField ID="txtInBarcode1" runat="server" FieldLabel="进厂批号" />
                            <ext:TextField ID="txtBatch1" runat="server" FieldLabel="批量" />
                            <ext:TextField ID="txtProductPlace1" runat="server" FieldLabel="产地" />
                            <ext:TextField ID="txtWeight1" runat="server" FieldLabel="重量" ReadOnly="true" />
                            <ext:TextField ID="txtShelfPieceWeight1" runat="server" FieldLabel="胶料每盘单重" Vtype="decimal" LabelAlign="Left" OnDirectChange="txtShelfPieceWeight1_Change" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                            <ext:TextField ID="txtNum1" runat="server" FieldLabel="打印条数" Vtype="integer" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{btnPrint}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnPrint" runat="server" Text="生成流转卡" Icon="Accept" Disabled="false">
                        <Listeners>
                            <Click Fn="btnPrint_Click" />
                        </Listeners>
                    </ext:Button>
                    <ext:Button ID="btnCancel" runat="server" Text="取消" Icon="Cancel">
                        <DirectEvents>
                            <Click OnEvent="btnCancel_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="for(var i=0;i<#{vpPrint}.items.length;i++){#{vpPrint}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpPrint}.items.length;i++){#{vpPrint}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>

            <ext:Hidden ID="hiddenBillNo" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenStorageID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenStoragePlaceID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenOrderID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenFactoryID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenMaterCode" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenMakerPerson" runat="server"></ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
