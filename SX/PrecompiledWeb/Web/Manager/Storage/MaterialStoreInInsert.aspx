<%@ page language="C#" autoeventwireup="true" inherits="Manager_Storage_MaterialStoreInInsert, App_Web_p5ht2o2r" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var refreshMain = function () {
            App.direct.btnSave_Click({
                success: function (result) {
                    if (result == "false")
                        Ext.Msg.alert('操作', "您没有添加明细数据，不能保存！");
                    else if (result == "false1")
                        Ext.Msg.alert('操作', "明细数据必须添加库位，不能保存！");
                    else {

                        var tabid = "id=38";
                        var tabp = parent.App.mainTabPanel;
                        var tab = tabp.getComponent(tabid);
                        if (tab) {
                            tab.reload();
                            tabp.activeTab.close();

                            //tabp.setActiveTab(tab);
                            //parent.refresh("");
                        }
                        else {
                            parent.OpenMenu(tabid);
                        }
                    }
                    return false;
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var AddMaterial = function () {//物料添加
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
        }

        var EditMaterial = function () {//物料修改
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
        }

        var AddFactory = function () {//厂家添加
            App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
        }

        var QueryFactory = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenFactoryID2.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
                    break;
            }
        }

        var AddStorage = function () {//库房添加
            var url = "../BasicInfo/CommonPage/QueryBasStorage.aspx?LastStorageFlag=1&&UsedFlag=1";
            var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.getBody()) {
                App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.getBody().update(html);
            } else {
                App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.html = html;
            }
            App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
        }

        var AddStoragePlace = function () {//库位添加
            if (App.hiddenStorageID.getValue() == "") {
                Ext.Msg.alert('操作', "请在主信息中选择库房！");
                return;
            }
            var url = "../BasicInfo/CommonPage/QueryBasStoragePlace.aspx?StorageID=" + App.hiddenStorageID.getValue();
            var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody()) {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.getBody().update(html);
            } else {
                App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.html = html;
            }
            App.Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window.show();
        }

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

        Ext.create("Ext.window.Window", {//库房带窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasStorage_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryBasStorage.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择库房名称",
            modal: true
        })

        Ext.create("Ext.window.Window", {//库位带窗体
            id: "Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Window",
            height: 460,
            hidden: true,
            width: 360,
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择库位",
            modal: true
        })

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            if (!App.winAddDetail.hidden) {
                App.txtMaterialName3.setValue(record.data.MaterialName);
                App.hiddenMaterCode.setValue(record.data.MaterialCode);
            }
            else if (!App.winModifyDetail.hidden) {
                App.txtMaterialName2.setValue(record.data.MaterialName);
                App.hiddenMaterCode.setValue(record.data.MaterialCode);
            }
        }

        var Manager_BasicInfo_CommonPage_QueryFactory_Request = function (record) {//生产厂家信息返回值处理
            if (!App.winAdd.hidden) {
                App.txtFactory2.getTrigger(0).show();
                App.txtFactory2.setValue(record.data.FacName);
                App.hiddenFactoryID2.setValue(record.data.ObjID);
            }
            else {
                App.txtFactoryID.setValue(record.data.FacName);
                App.hiddenFactoryID.setValue(record.data.ObjID);
            }
        }

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            if (!App.winAdd.hidden) {
                App.txtStorageName4.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
            }
            else {
                App.txtStorageName.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
                App.txtStoragePlaceName3.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            }
        }

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStoragePlace_Request = function (record) {//库位信息返回值处理
            if (!App.winAdd.hidden) {
                App.txtStoragePlaceName4.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            }
            else if (!App.winAddDetail.hidden) {
                App.txtStoragePlaceName3.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            }
            else if (!App.winModifyDetail.hidden) {
                App.txtStoragePlaceName2.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            }
        }

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

        var commandcolumn_click = function (command, record) {
            commandcolumn_click_confirm(command, record);
            return false;
        };

        //区分删除操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumndetail_direct_edit(record);
            }
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定要删除此条信息吗？', function (btn) { commandcolumn_direct_delete(btn, record) });
            }
            return false;
        };

        var commandcolumndetail_direct_edit = function (record) {
            //var BillNo = record.data.BillNo;
            var Barcode = record.data.Barcode;
            App.direct.commandcolumndetail_direct_edit(Barcode, {
                success: function (result) {
                    //App.store.remove(record);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var commandcolumn_direct_delete = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            //var BillNo = record.data.BillNo;
            var Barcode = record.data.Barcode;
            App.direct.DeleteChkDetail(Barcode, {
                success: function (result) { App.store.remove(record); },
                failure: function (err) { Ext.Msg.alert("错误提示", err); }
            });
        }

        var AddStoreIn = function () {
            var section = App.pnlDetailList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                Ext.Msg.alert("提示", '您没有选择任何项，请选择！');
            }
            else {
                var billNo = App.storeDetail.data.get(0).data.BillNo;
                var barcodes = "";
                var orders = "";
                for (var i = 0; i < section.length; i++) {
                    var bc = App.storeDetail.data.get(section[i].index).data.Barcode;
                    var od = App.storeDetail.data.get(section[i].index).data.OrderID;
                    barcodes = barcodes == "" ? bc : barcodes + "," + bc;
                    orders = orders == "" ? od : orders + "," + od;
                }
                App.direct.btnAddSave_Click(billNo, barcodes, orders, {
                    success: function (result) { },
                    failure: function (err) { Ext.Msg.alert("错误", err); }
                });
            }
        }

        var pnlListFresh = function () {
            App.store1.currentPage = 1;
            App.PagingToolbar2.doRefresh();
            return false;
        }

        var SetFocusInputNum3 = function () {
            App.hiddenFocus.setValue("txtInputNum3");
        }

        var SetFocusPieceWeight3 = function () {
            App.hiddenFocus.setValue("txtPieceWeight3");
        }

        var SetFocusInputNum2 = function () {
            App.hiddenFocus.setValue("txtInputNum2");
        }

        var SetFocusPieceWeight2 = function () {
            App.hiddenFocus.setValue("txtPieceWeight2");
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmChkBill" runat="server" />
    <ext:Viewport ID="vpStoreinBill" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnStoreinBill" runat="server" Region="North" Height="90">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbStoreinBill">
                        <Items>
                            <ext:Button ID="btnSave" runat="server" Icon="Add" Text="保存入库单" Disabled="true">
                                <Listeners>
                                    <Click Fn="refreshMain" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsBegin" />
                            <ext:Button ID="btnInport" runat="server" Icon="Add" Text="送检单导入">
                                <DirectEvents>
                                    <Click OnEvent="btnInport_Click" />
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
                            <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:TextField ID="txtBillNo" runat="server" FieldLabel="入库单号" LabelAlign="Left" Text="(自动生成)" ReadOnly="true" />
                                    <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="库房名称" LabelAlign="Left" Editable="false" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <RemoteValidation OnValidation="CheckField" />
                                        <Listeners>
                                            <TriggerClick Fn="AddStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                   <%-- <ext:TextField ID="txtSendChkNo" runat="server" FieldLabel="通知单号" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" LabelAlign="Left">
                                        <RemoteValidation OnValidation="CheckField" />
                                    </ext:TextField>--%>
                                    <ext:DateField ID="txtInPutDate" runat="server" FieldLabel="入库日期" LabelAlign="Left" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"></ext:DateField>
                                    <ext:TextField ID="txtRemark" runat="server" FieldLabel="备注" LabelAlign="Left" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:TriggerField ID="txtFactoryID" runat="server" FieldLabel="生产厂家" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <RemoteValidation OnValidation="CheckField" />
                                        <Listeners>
                                            <TriggerClick Fn="AddFactory" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TextField ID="txtMakerPerson" runat="server" FieldLabel="制单人" LabelAlign="Left" ReadOnly="true" />
                                </Items>
                            </ext:Container>
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{btnSave}.setDisabled(!valid);" />
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
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindDetail" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model" Name="PstMaterialChkDetail" runat="server" IDProperty="BillNo,Barcode,OrderID">
                                        <Fields>
                                            <ext:ModelField Name="BillNo" />
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="OrderID" />
                                            <ext:ModelField Name="ProductNo" />
                                            <ext:ModelField Name="MaterCode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="StoragePlaceName" />
                                            <ext:ModelField Name="ProcDate" Type="Date" />
                                            <ext:ModelField Name="InputNum" Type="Int" />
                                            <ext:ModelField Name="PieceWeight" Type="Float" />
                                            <ext:ModelField Name="InputWeight" Type="Float" />
                                            <ext:ModelField Name="ChkDate" Type="Date" />
                                            <ext:ModelField Name="ProductPlace" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />
                                <%--<ext:Column ID="BillNo" runat="server" Text="单据号" DataIndex="BillNo" Flex="1" />--%>
                                <ext:Column ID="Barcode" runat="server" Text="条码号" DataIndex="Barcode" Flex="1" />
                                <ext:Column ID="ProductNo" runat="server" Text="批次号" DataIndex="ProductNo" Flex="1" />
                                <ext:Column ID="MaterialName1" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1"/>
                                <ext:Column ID="StoragePlaceName" runat="server" Text="库位名称" DataIndex="StoragePlaceName" Flex="1"/>
                                <ext:DateColumn ID="ProcDate" runat="server" Text="生产日期" DataIndex="ProcDate" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:Column ID="InputNum" runat="server" Text="数量" DataIndex="InputNum" Flex="1" />
                                <ext:Column ID="PieceWeight" runat="server" Text="单重" DataIndex="PieceWeight" Flex="1" />
                                <ext:Column ID="InputWeight" runat="server" Text="实际重量" DataIndex="InputWeight" Flex="1" />
                                <ext:Column ID="ProductPlace" runat="server" Text="产地" DataIndex="ProductPlace" Flex="1" />
                                <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                    <Commands>
                                        <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改" />
                                    </Commands>
                                    <Commands>
                                        <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除" />
                                    </Commands>
                                    <Listeners>
                                        <Command Handler="return commandcolumn_click(command, record);" />
                                    </Listeners>
                                </ext:CommandColumn>
                            </Columns>
                        </ColumnModel>
                        <TopBar>
                            <ext:Toolbar ID="Toolbar1" runat="server">
                                <Items>
                                    <ext:Button ID="btnAdd" runat="server" Text="添加明细信息" Icon="Add">
                                        <DirectEvents>
                                            <Click OnEvent="btnAdd_Click" />
                                        </DirectEvents>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        <BottomBar>
                            <ext:PagingToolbar ID="pageToolBar" runat="server" StoreID="store">
                                <Items>
                                    <ext:Label ID="Label1" runat="server" Text="每页条数:" />
                                    <ext:ToolbarSpacer ID="ToolbarSpacer2" runat="server" Width="10" />
                                    <ext:ComboBox ID="ComboBox1" runat="server" Width="80">
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
            <ext:Window ID="winAddDetail" runat="server" Icon="MonitorAdd" Closable="true" Title="添加入库单明细信息"
                Width="320" Height="380" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="5" Layout="Form">
                <Items>
                    <ext:FormPanel ID="pnlAddDetail" runat="server" BodyPadding="5">
                        <FieldDefaults>
                            <CustomConfig>
                                <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                            </CustomConfig>
                        </FieldDefaults>
                        <Items>
                            <%--<ext:TextField ID="txtBillNo3" runat="server" FieldLabel="单据号" ReadOnly="true" LabelAlign="Left" />--%>
                            <ext:TextField ID="txtBarcode3" runat="server" FieldLabel="条码号" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" OnDirectChange="txtBarcode3_Change" LabelAlign="Left" />
                            <ext:TextField ID="txtProductNo3" runat="server" FieldLabel="批次号" LabelAlign="Left" />
                            <ext:TriggerField ID="txtMaterialName3" runat="server" FieldLabel="物料名称" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false"
                                Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <RemoteValidation OnValidation="CheckField" />
                                <Listeners>
                                    <TriggerClick Fn="AddMaterial" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TriggerField ID="txtStoragePlaceName3" runat="server" FieldLabel="库位名称" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <RemoteValidation OnValidation="CheckField" />
                                <Listeners>
                                    <TriggerClick Fn="AddStoragePlace" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:DateField ID="txtProcDate3" runat="server" Format="yyyy-MM-dd" FieldLabel="生产日期" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                            <ext:TextField ID="txtInputWeight3" runat="server" Vtype="decimal" FieldLabel="实际重量" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                            <ext:TextField ID="txtInputNum3" runat="server" Vtype="integer" FieldLabel="数量" OnDirectChange="txtStorein1_Change" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" >
                                <Listeners>
                                    <Focus Fn="SetFocusInputNum3" />
                                </Listeners> 
                            </ext:TextField>
                            <ext:TextField ID="txtPieceWeight3" runat="server" FieldLabel="单重" Vtype="decimal" OnDirectChange="txtStorein2_Change" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" >
                                <Listeners>
                                    <Focus Fn="SetFocusPieceWeight3" />
                                </Listeners> 
                            </ext:TextField>
                            <ext:ComboBox ID="cbxProductPlace" runat="server" FieldLabel="产地" LabelAlign="Left" DisplayField="ProductPlace" ValueField="ObjID">
                                <Store>
                                    <ext:Store ID="storePlace" runat="server" AutoLoad="false" OnReadData="placeRefresh">
                                        <Model>
                                            <ext:Model ID="modelPlace" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="ObjID" Type="String" ServerMapping="ObjID" />
                                                    <ext:ModelField Name="ProductPlace" Type="String" ServerMapping="ProductPlace" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                            </ext:ComboBox>
                            <ext:TextField ID="txtRemark3" runat="server" FieldLabel="备注" LabelAlign="Left" />
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{btnAddDetailSave}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnAddDetailSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                        <DirectEvents>
                            <Click OnEvent="btnAddDetail_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnAddDetailCancel" runat="server" Text="取消" Icon="Cancel">
                        <DirectEvents>
                            <Click OnEvent="btnCancel_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="for(var i=0;i<#{vpStoreinBill}.items.length;i++){#{vpStoreinBill}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpStoreinBill}.items.length;i++){#{vpStoreinBill}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>

            <ext:Window ID="winModifyDetail" runat="server" Icon="MonitorEdit" Closable="true" Title="修改入库明细信息"
                Width="320" Height="380" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                            <%--<ext:TextField ID="txtBillNo2" runat="server" FieldLabel="单据号" ReadOnly="true" LabelAlign="Left" />--%>
                            <ext:TextField ID="txtBarcode2" runat="server" FieldLabel="条码号" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" LabelAlign="Left">
                                <RemoteValidation OnValidation="CheckField" />
                            </ext:TextField>
                            <ext:TextField ID="txtProductNo2" runat="server" FieldLabel="批次号" LabelAlign="Left" />                            
                            <ext:TriggerField ID="txtMaterialName2" runat="server" FieldLabel="物料名称" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" LabelAlign="Left"
                                Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <RemoteValidation OnValidation="CheckField" />
                                <Listeners>
                                    <TriggerClick Fn="EditMaterial" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TriggerField ID="txtStoragePlaceName2" runat="server" FieldLabel="库位名称" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <RemoteValidation OnValidation="CheckField" />
                                <Listeners>
                                    <TriggerClick Fn="AddStoragePlace" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:DateField ID="txtProcDate2" runat="server" Format="yyyy-MM-dd" FieldLabel="生产日期" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                            <ext:TextField ID="txtInputWeight2" runat="server" Vtype="decimal" FieldLabel="实际重量" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                            <ext:TextField ID="txtInputNum2" runat="server" Vtype="integer" FieldLabel="数量" LabelAlign="Left" OnDirectChange="txtStorein3_Change" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" >
                                <Listeners>
                                    <Focus Fn="SetFocusInputNum2" />
                                </Listeners> 
                            </ext:TextField>
                            <ext:TextField ID="txtPieceWeight2" runat="server" FieldLabel="单重" Vtype="decimal" LabelAlign="Left" OnDirectChange="txtStorein4_Change" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false">
                                <Listeners>
                                    <Focus Fn="SetFocusPieceWeight2" />
                                </Listeners> 
                            </ext:TextField>
                            <ext:ComboBox ID="cbxProductPlace2" runat="server" FieldLabel="产地" LabelAlign="Left" DisplayField="ProductPlace" ValueField="ObjID">
                                <Store>
                                    <ext:Store ID="store2" runat="server" AutoLoad="false" OnReadData="placeRefresh">
                                        <Model>
                                            <ext:Model ID="model2" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="ObjID" Type="String" ServerMapping="ObjID" />
                                                    <ext:ModelField Name="ProductPlace" Type="String" ServerMapping="ProductPlace" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                            </ext:ComboBox>
                            <ext:TextField ID="txtRemark2" runat="server" FieldLabel="备注" LabelAlign="Left" />
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{btnModifyDetailSave}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnModifyDetailSave" runat="server" Text="确定" Icon="Accept" Disabled="false">
                        <DirectEvents>
                            <Click OnEvent="btnModifyDetailSave_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnModifyDetailCancel" runat="server" Text="取消" Icon="Cancel">
                        <DirectEvents>
                            <Click OnEvent="btnCancel_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="for(var i=0;i<#{vpStoreinBill}.items.length;i++){#{vpStoreinBill}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpStoreinBill}.items.length;i++){#{vpStoreinBill}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>

            <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="true" Title="送检单信息"
                Width="550" Height="550" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="10" Layout="Form">
                <Items>
                    <ext:Panel ID="Panel1" runat="server" Region="North" AutoHeight="true">
                        <TopBar>
                            <ext:Toolbar runat="server" ID="Toolbar2">
                                <Items>
                                    <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                        <Listeners>
                                            <Click Fn="pnlListFresh" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:ToolbarSpacer runat="server" ID="ToolbarSpacer1" />
                                    <ext:ToolbarFill ID="ToolbarFill1" />
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        <Items>
                            <ext:FormPanel ID="FormPanel2" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="container4" runat="server" ColumnWidth=".5">
                                        <Items>
                                            <ext:TextField ID="txtBillNo1" runat="server" FieldLabel="送检单号" LabelAlign="Right" />
                                            <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" LabelAlign="Right" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container5" runat="server" ColumnWidth=".5">
                                        <Items>
                                            <ext:TriggerField ID="txtFactory2" runat="server" FieldLabel="生产厂家" LabelAlign="Right"
                                                Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryFactory" />
                                                </Listeners>
                                            </ext:TriggerField>
                                            <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" LabelAlign="Right" />
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                    <ext:GridPanel ID="pnlChkBillList" runat="server" Height="180">
                        <Store>
                            <ext:Store ID="store1" runat="server" PageSize="5">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model1" runat="server" IDProperty="BillNo">
                                        <Fields>
                                            <ext:ModelField Name="BillNo" />
                                            <ext:ModelField Name="NoticeNo" />
                                            <ext:ModelField Name="FactoryID" />
                                            <ext:ModelField Name="FacName" />
                                            <ext:ModelField Name="SendChkFlag" />
                                            <ext:ModelField Name="MakerPerson" />
                                            <ext:ModelField Name="UserName" />
                                            <ext:ModelField Name="LockedFlag" />
                                            <ext:ModelField Name="FiledFlag" />
                                            <ext:ModelField Name="Remark" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Width="35" />
                                <ext:Column ID="Column1" runat="server" Text="送检单号" DataIndex="BillNo" Flex="1" />
                                <ext:Column ID="Column2" runat="server" Text="通知单号" DataIndex="NoticeNo" Flex="1" />
                                <ext:Column ID="Column3" runat="server" Text="生产厂家" DataIndex="FacName" Flex="1" />
                                <ext:Column ID="Column4" runat="server" Text="制单人" DataIndex="UserName" Flex="1" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionMulti1" runat="server" Mode="Single">
                                <DirectEvents>
                                    <Select OnEvent="RowSelect" Buffer="250">
                                        <ExtraParams>
                                            <ext:Parameter Name="BillNo" Value="record.getId()" Mode="Raw" />
                                        </ExtraParams>
                                    </Select>
                                </DirectEvents>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar2" runat="server">
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager2" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                    <ext:GridPanel ID="pnlDetailList" runat="server" Height="200">
                        <Store>
                            <ext:Store ID="storeDetail" runat="server" PageSize="10">
                                <Model>
                                    <ext:Model ID="modelDetail" runat="server" IDProperty="BillNo,Barcode,OrderID">
                                        <Fields>
                                            <ext:ModelField Name="BillNo" />
                                            <ext:ModelField Name="ProductNo" />
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="OrderID" />
                                            <ext:ModelField Name="MaterCode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="ProcDate" Type="Date" />
                                            <ext:ModelField Name="SendNum" Type="Int" />
                                            <ext:ModelField Name="SendWeight" Type="Float" />
                                            <ext:ModelField Name="ChkDate" Type="Date" />
                                            <ext:ModelField Name="InStockDate" Type="Date" />
                                            <ext:ModelField Name="ChkResultFlag" />
                                            <ext:ModelField Name="LastWeight" Type="Float" />
                                            <ext:ModelField Name="SendChkFlag" />
                                            <ext:ModelField Name="LastNum" Type="Int" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModelDetail" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="RowNumbererColumn2" runat="server" Width="35" />
                                <ext:Column ID="Barcode1" runat="server" Text="条码号" DataIndex="Barcode" Flex="1" />
                                <ext:Column ID="ProductNo1" runat="server" Text="批次号" DataIndex="ProductNo" Flex="1" />
                                <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                <ext:DateColumn ID="DateColumn1" runat="server" Text="生产日期" DataIndex="ProcDate" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:DateColumn ID="DateColumn2" runat="server" Text="检验日期" DataIndex="ChkDate" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:Column ID="LastNum" runat="server" Text="数量" DataIndex="LastNum" Flex="1" />
                                <ext:Column ID="LastWeight" runat="server" Text="重量" DataIndex="LastWeight" Flex="1" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionMulti2" runat="server" Mode="Simple">
                            </ext:RowSelectionModel>
                        </SelectionModel>
                    </ext:GridPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept">
                        <Listeners>
                            <Click Handler="AddStoreIn();" />
                        </Listeners>
                        <%--<DirectEvents>
                            <Click OnEvent="btnAddSave_Click" />
                        </DirectEvents>--%>
                    </ext:Button>
                    <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
                        <DirectEvents>
                            <Click OnEvent="btnCancel_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="for(var i=0;i<#{vpStoreinBill}.items.length;i++){#{vpStoreinBill}.getComponent(i).disable(true);} #{store1}.reload();" />
                    <Hide Handler="for(var i=0;i<#{vpStoreinBill}.items.length;i++){#{vpStoreinBill}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>
            <%--<ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="true" Title="导入送检单信息"
                Width="550" Height="550" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                BodyPadding="10" Layout="Form">
                <Items>
                    <ext:FormPanel ID="pnlAdd" runat="server" Layout="ColumnLayout" Height="85" BodyPadding="10">
                        <Items>
                            <ext:Container ID="container4" runat="server" Layout="FormLayout" ColumnWidth=".5"
                                Padding="5">
                                <Items>
                                    <ext:TriggerField ID="txtStorageName4" runat="server" FieldLabel="库房名称" LabelAlign="Left"
                                        Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <RemoteValidation OnValidation="CheckField" />
                                        <Listeners>
                                            <TriggerClick Fn="AddStorage" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:DateField ID="txtInputDate4" runat="server" Format="yyyy-MM-dd" FieldLabel="入库日期" LabelAlign="Left" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container5" runat="server" Layout="FormLayout" ColumnWidth=".5"
                                Padding="5">
                                <Items>
                                    <ext:TriggerField ID="txtStoragePlaceName4" runat="server" FieldLabel="库位名称" LabelAlign="Left"
                                        Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <RemoteValidation OnValidation="CheckField" />
                                        <Listeners>
                                            <TriggerClick Fn="AddStoragePlace" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:TextField ID="txtRemark4" runat="server" FieldLabel="备注" LabelAlign="Left" />
                                </Items>
                            </ext:Container>
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{btnAddSave}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                    <ext:GridPanel ID="pnlChkBillList" runat="server" Height="200">
                        <Store>
                            <ext:Store ID="store1" runat="server" PageSize="5">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model1" runat="server" IDProperty="BillNo">
                                        <Fields>
                                            <ext:ModelField Name="BillNo" />
                                            <ext:ModelField Name="NoticeNo" />
                                            <ext:ModelField Name="FactoryID" />
                                            <ext:ModelField Name="FacName" />
                                            <ext:ModelField Name="SendChkFlag" />
                                            <ext:ModelField Name="InStockDate" Type="Date" />
                                            <ext:ModelField Name="MakerPerson" />
                                            <ext:ModelField Name="UserName" />
                                            <ext:ModelField Name="LockedFlag" />
                                            <ext:ModelField Name="FiledFlag" />
                                            <ext:ModelField Name="Remark" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Width="35" />
                                <ext:Column ID="Column1" runat="server" Text="单据号" DataIndex="BillNo" Flex="1" />
                                <ext:Column ID="Column2" runat="server" Text="通知单号" DataIndex="NoticeNo" Flex="1" />
                                <ext:Column ID="Column3" runat="server" Text="生产厂家" DataIndex="FacName" Flex="1" />
                                <ext:Column ID="Column4" runat="server" Text="制单人" DataIndex="UserName" Flex="1" />
                                <ext:DateColumn ID="inStockDate" runat="server" Text="接收日期" DataIndex="InStockDate"
                                    Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionMulti1" runat="server" Mode="Simple">
                                <DirectEvents>
                                    <Select OnEvent="RowSelect" Buffer="250">
                                        <ExtraParams>
                                            <ext:Parameter Name="BillNo" Value="record.getId()" Mode="Raw" />
                                        </ExtraParams>
                                    </Select>
                                </DirectEvents>
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar2" runat="server">
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager2" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                    <ext:GridPanel ID="pnlDetailList" runat="server" Height="200">
                        <Store>
                            <ext:Store ID="storeDetail" runat="server" PageSize="10">
                                <Model>
                                    <ext:Model ID="modelDetail" runat="server" IDProperty="BillNo, Barcode">
                                        <Fields>
                                            <ext:ModelField Name="BillNo" />
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="MaterCode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="ProcDate" Type="Date" />
                                            <ext:ModelField Name="SendNum" Type="Int" />
                                            <ext:ModelField Name="SendWeight" Type="Float" />
                                            <ext:ModelField Name="ChkDate" Type="Date" />
                                            <ext:ModelField Name="InStockDate" Type="Date" />
                                            <ext:ModelField Name="ChkResultFlag" />
                                            <ext:ModelField Name="PassWeight" Type="Float" />
                                            <ext:ModelField Name="SendChkFlag" />
                                            <ext:ModelField Name="PassNum" Type="Int" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModelDetail" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="RowNumbererColumn2" runat="server" Width="35" />
                                <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                <ext:DateColumn ID="DateColumn1" runat="server" Text="生产日期" DataIndex="ProcDate" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:Column ID="PassNum" runat="server" Text="合格数量" DataIndex="PassNum" Flex="1" />
                                <ext:Column ID="PassWeight" runat="server" Text="合格重量" DataIndex="PassWeight" Flex="1" />
                            </Columns>
                        </ColumnModel>
                    </ext:GridPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept">
                        <Listeners>
                            <Click Handler="AddStoreIn();" />
                        </Listeners>
                    </ext:Button>
                    <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
                        <DirectEvents>
                            <Click OnEvent="btnCancel_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <Show Handler="for(var i=0;i<#{vpChkBill}.items.length;i++){#{vpChkBill}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpChkBill}.items.length;i++){#{vpChkBill}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>
--%>
            <ext:Hidden ID="hiddenBillNo" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenMaterCode" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenFactoryID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenFactoryID2" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenBarcode" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenStorageID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenStoragePlaceID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenFocus" runat="server"></ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>