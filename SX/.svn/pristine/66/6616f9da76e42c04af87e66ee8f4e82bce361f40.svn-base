<%@ page language="C#" autoeventwireup="true" inherits="Manager_Storage_MaterialStorageAdjustingInsert, App_Web_p5ht2o2r" %>
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
                        Ext.Msg.alert('操作', "您没有添加明细数据，不能添加！");
                    else {
                        var tabid = "id=104";
                        var tabp = parent.App.mainTabPanel;
                        var tab = tabp.getComponent(tabid);
                        if (tab) {
                            tab.reload();
                            tabp.activeTab.close();
//                            parent.refresh("");
//                            tabp.setActiveTab(tab);
//                            parent.refresh("");
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

        //--查询带弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryBasStorage_Request = function (record) {//库房信息返回值处理
            if (!App.winAdd.hidden) {
                App.txtStorageName.getTrigger(0).show();
                App.txtStorageName.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
            }
            else {
                App.txtStorageName1.setValue(record.data.StorageName);
                App.hiddenStorageID.setValue(record.data.StorageID);
                App.txtStoragePlaceName1.setValue(record.data.StoragePlaceName);
                App.hiddenStoragePlaceID.setValue(record.data.StoragePlaceID);
            }
        }
        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            App.txtMaterCode.getTrigger(0).show();
            App.txtMaterCode.setValue(record.data.MaterialName);
            App.hiddenMaterCode.setValue(record.data.MaterialCode);
        }

        var QueryMaterial = function (field, trigger, index) {
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
        var AddStorage = function (field, trigger, index) {//库房添加
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenStorageID.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasStorage_Window.show();
                    break;
            }
        }

        var EditMaterial = function () {//物料修改
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
        }

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

        var AddStoreIn = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                Ext.Msg.alert("提示", '您没有选择任何项，请选择！');
            }
            else {
                var billNo = App.store.data.get(0).data.BillNo;
                App.direct.btnAddSave_Click(billNo, {
                    success: function (result) { },
                    failure: function (err) { Ext.Msg.alert("错误", err); }
                });
            }
        }

        var commandcolumn_click = function (command, record) {
            commandcolumn_click_confirm(command, record);
            return false;
        };

        //区分删除操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "reject") {
                record.reject();
            }
            if (command.toLowerCase() == "edit") {
                commandcolumndetail_direct_edit(record);
            }
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定要删除此条信息吗？', function (btn) { commandcolumn_direct_delete(btn, record) });
            }
            return false;
        };

        var commandcolumndetail_direct_edit = function (record) {
            var Barcode = record.data.Barcode;
            var OrderID = record.data.OrderID;
            App.direct.commandcolumndetail_direct_edit(Barcode, OrderID, {
                success: function (result) {
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
            var Barcode = record.data.Barcode;
            var OrderID = record.data.OrderID;
            App.direct.DeleteStorage(Barcode, OrderID, {
                success: function (result) { App.store.remove(record); },
                failure: function (err) { Ext.Msg.alert("错误提示", err); }
            });
        }

        var edit = function (editor, e) {
            if (e.value > e.record.data.Num) {
                Ext.Msg.alert("提示", "您设置的数量超出当前库存数量，请重新设置！");
                e.record.reject();
            }
        };

        var edit2 = function (editor, e) {
            if (e.value > e.record.data.Num) {
                Ext.Msg.alert("提示", "您设置的数量超出当前库存数量，请重新设置！");
                e.record.reject();
            }
        };

        var daChange = function (value) {
            if (value == "1")
                return Ext.String.format("调增");
            else if (value == "2")
                return Ext.String.format("调减");
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmAdjustingBill" runat="server" />
    <ext:Viewport ID="vpAdjustingBill" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnAdjustingBill" runat="server" Region="North" Height="90">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbAdjustingBill">
                        <Items>
                            <ext:ToolbarSeparator ID="tsBegin" />
                            <ext:Button runat="server" Icon="TableEdit" Text="生成调整单" ID="GenAdjusting" Disabled="true">
                                <Listeners>
                                    <Click Fn="refreshMain" />
                                </Listeners>
                                <%--<DirectEvents>
                                    <Click OnEvent="SaveClick">
                                        <ExtraParams>
                                            <ext:Parameter Name="data" Value="#{store1}.getChangedData({skipIdForNewRecords : false})" Mode="Raw" Encode="true" />
                                        </ExtraParams>
                                    </Click>
                                </DirectEvents>--%>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsMiddle" />
                            <ext:Button runat="server" Icon="Find" Text="导入盘点信息" ID="btnInport">
                                <%--<Listeners>
                                    <Click Handler="App.winAdd.show();" />
                                </Listeners>--%>
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
                            <ext:Container ID="container4" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:TextField ID="txtBillNo1" runat="server" FieldLabel="调整单号" Text="(自动生成)" LabelAlign="Left" ReadOnly="true" />
                                    <ext:TextField ID="txtStorageName1" runat="server" FieldLabel="库房名称" LabelAlign="Left" ReadOnly="true" AllowBlank="false" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container5" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:TextField ID="txtInventoryNo1" runat="server" FieldLabel="盘点单号" LabelAlign="Left" ReadOnly="true" AllowBlank="false" />
                                    <ext:TextField ID="txtMakerPerson1" runat="server" FieldLabel="制单人" LabelAlign="Left" ReadOnly="true" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container6" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:DateField ID="txtAdjustingDate1" runat="server" FieldLabel="调整日期" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                                    <ext:TextField ID="txtRemark1" runat="server" FieldLabel="备注" LabelAlign="Left" />
                                </Items>
                            </ext:Container>
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{GenAdjusting}.setDisabled(!valid);" />
                        </Listeners>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>

            <ext:Panel ID="Panel2" runat="server" Region="Center" Frame="true" Layout="Fit" MarginsSummary="0 5 0 5">
                <Items>
                    <ext:GridPanel ID="pnlTempList" runat="server">
                        <Store>
                            <ext:Store ID="store1" runat="server" PageSize="10">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindDataDetail" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model1" runat="server" IDProperty="BillNo,Barcode,OrderID">
                                        <Fields>
                                            <ext:ModelField Name="BillNo" />
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="OrderID" />
                                            <ext:ModelField Name="StoragePlaceID" />
                                            <ext:ModelField Name="StoragePlaceName" />
                                            <ext:ModelField Name="MaterCode" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="DecreaseOrAddFlag" />
                                            <ext:ModelField Name="AdjustingNum" Type="Int" />
                                            <ext:ModelField Name="AdjustingWeight" Type="Float" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Width="35" />
                                <ext:Column ID="Column1" runat="server" Text="条码号" DataIndex="Barcode" Flex="1" />
                                <ext:Column ID="Column2" runat="server" Text="库位名称" DataIndex="StoragePlaceName" Flex="1" />
                                <ext:Column ID="Column3" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                <ext:Column ID="Column4" runat="server" Text="增减标志" DataIndex="DecreaseOrAddFlag" Flex="1">
                                    <Renderer Fn="daChange" />
                                </ext:Column>
                                <ext:Column ID="Column5" runat="server" Text="调整数量" DataIndex="AdjustingNum" Flex="1" />
                                <ext:Column ID="Column6" runat="server" Text="调整重量" DataIndex="AdjustingWeight" Flex="1" />
                                <ext:CommandColumn ID="CommandColumn1" runat="server" Width="110">
                                    <Commands>
                                        <ext:GridCommand CommandName="Edit" Icon="TableEdit" Text="修改" />
                                        <ext:GridCommand CommandName="delete" Icon="Delete" Text="删除" />
                                    </Commands>
                                    <Listeners>
                                        <Command Handler="return commandcolumn_click(command, record);" />
                                    </Listeners>
                                </ext:CommandColumn>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single">
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar2" runat="server">
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
                                            <Select Handler="#{pnlTempList}.store.pageSize = parseInt(this.getValue(), 10); #{PagingToolbar2}.doRefresh(); return false;" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager2" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>

            <ext:Window ID="winModifyDetail" runat="server" Icon="MonitorEdit" Closable="true" Title="修改调整明细信息"
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
                            <ext:TextField ID="txtBarcode2" runat="server" FieldLabel="条码号" ReadOnly="true" LabelAlign="Left" />
                            <%--<ext:TextField ID="txtProductNo2" runat="server" FieldLabel="批次号" LabelAlign="Left" ReadOnly="true" /> --%>                           
                            <ext:TextField ID="txtStoragePlaceName2" runat="server" FieldLabel="库位名称" ReadOnly="true" LabelAlign="Left" />
                            <ext:TriggerField ID="txtMaterialName2" runat="server" FieldLabel="物料名称" ReadOnly="true" LabelAlign="Left"
                                Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="EditMaterial" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:ComboBox ID="ckxDecreaseOrAdd2" runat="server" FieldLabel="调增/减" LabelAlign="Right">
                                <SelectedItems>
                                    <ext:ListItem Value="1"></ext:ListItem>
                                </SelectedItems>
                                <Items>
                                    <ext:ListItem Text="调增" Value="1"></ext:ListItem>
                                    <ext:ListItem Text="调减" Value="2"></ext:ListItem>
                                </Items>
                            </ext:ComboBox>
                            <ext:TextField ID="txtAdjustingNum2" runat="server" Vtype="integer" FieldLabel="调整数量" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" >
                                <RemoteValidation OnValidation="CheckField" />
                            </ext:TextField>
                            <ext:TextField ID="txtAdjustingWeight2" runat="server" Vtype="decimal" FieldLabel="调整重量" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
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
                    <Show Handler="for(var i=0;i<#{vpAdjustingBill}.items.length;i++){#{vpAdjustingBill}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpAdjustingBill}.items.length;i++){#{vpAdjustingBill}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>

            <ext:Window ID="winAdd" runat="server" Width="500" Height="450" Hidden="true" Title="盘点单查询"  Modal="false" Resizable="true" BodyStyle="background-color:#fff;"
                BodyPadding="10" Layout="Form" Icon="MonitorAdd" Closable="true">
                <Items>
                    <ext:Panel ID="Panel1" runat="server" Region="North" AutoHeight="true">
                        <TopBar>
                            <ext:Toolbar runat="server" ID="Toolbar2">
                                <Items>
                                    <ext:Button runat="server" Icon="Find" Text="查询" ID="btnQuery" Disabled="true">
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
                                    <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".5"
                                        Padding="5">
                                        <Items>
                                            <ext:TextField ID="txtBillNo" runat="server" FieldLabel="盘点单号" LabelWidth="55" LabelAlign="Right"></ext:TextField>
                                            <ext:TriggerField ID="txtStorageName" runat="server" FieldLabel="库房名称" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" LabelWidth="55" LabelAlign="Right" Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <RemoteValidation OnValidation="CheckField" />
                                                <Listeners>
                                                    <TriggerClick Fn="AddStorage" />
                                                </Listeners>
                                            </ext:TriggerField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".5" Padding="5">
                                        <Items>
                                            <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始时间" LabelWidth="55" LabelAlign="Right" />
                                            <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" LabelWidth="55" LabelAlign="Right" />
                                        </Items>
                                    </ext:Container>
                                </Items>
                                <Listeners>
                                    <ValidityChange Handler="#{btnQuery}.setDisabled(!valid);" />
                                </Listeners>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                    <ext:GridPanel ID="pnlList" runat="server" Height="260">
                        <Store>
                            <ext:Store ID="store" runat="server" PageSize="10" AutoLoad="false">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model" runat="server" IDProperty="BillNo">
                                        <Fields>
                                            <ext:ModelField Name="BillNo" />
                                            <ext:ModelField Name="StorageID" />
                                            <ext:ModelField Name="StorageName" />
                                            <ext:ModelField Name="UserName" />
                                            <ext:ModelField Name="InventoryDate" Type="Date" />
                                            <ext:ModelField Name="ChkResultFlag" />
                                            <ext:ModelField Name="Remark" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                <ext:Column ID="BillNo" runat="server" Text="盘点单号" DataIndex="BillNo" Flex="1" />
                                <ext:Column ID="StorageName" runat="server" Text="库房名称" DataIndex="StorageName" Flex="1" />
                                <ext:Column ID="UserName" runat="server" Text="盘点人" DataIndex="UserName" Flex="1" />
                                <ext:DateColumn ID="InventoryDate" runat="server" Text="盘点日期" Format="yyyy-MM-dd" DataIndex="InventoryDate" Flex="1" />
                            </Columns>
                        </ColumnModel>
                        <Listeners>
                            <CellDblClick Fn="AddStoreIn" />
                        </Listeners>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single" />
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="pageToolBar" runat="server">
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
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
                    <Show Handler="for(var i=0;i<#{vpAdjustingBill}.items.length;i++){#{vpAdjustingBill}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpAdjustingBill}.items.length;i++){#{vpAdjustingBill}.getComponent(i).enable(true);#{store}.reload();}" />
                </Listeners>
            </ext:Window>

            <ext:Hidden ID="hiddenBillNo" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenStorageID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenStoragePlaceID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenMaterCode" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenOpenEdit" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenOrderID" runat="server"></ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>