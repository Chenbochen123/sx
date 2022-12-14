﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialChkBillInsert.aspx.cs" Inherits="Manager_Storage_MaterialChkBillInsert" %>
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
                        var tabid = "id=31";
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

        var AddMaterial = function () {//物料添加
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
        }

        var EditMaterial = function () {//物料修改
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
        }

        var AddFactory = function () {//厂家添加
            App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
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
            App.txtFactoryID.setValue(record.data.FacName);
            App.hiddenFactoryID.setValue(record.data.ObjID);
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
            var OrderID = record.data.OrderID;
            App.direct.commandcolumndetail_direct_edit(Barcode, OrderID, {
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
            var OrderID = record.data.OrderID;
            App.direct.DeleteChkDetail(Barcode, OrderID, {
                success: function (result) {
                    if (result == "false") { Ext.Msg.alert('提示', "无法删除，至少要有一条明细信息！"); }
                    else { App.store.remove(record); Ext.Msg.alert("提示", "删除成功！"); }
                },
                failure: function (err) { Ext.Msg.alert("错误提示", err); }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmChkBill" runat="server" />
    <ext:Viewport ID="vpChkBill" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnChkBill" runat="server" Region="North" Height="90">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbChkBill">
                        <Items>
                            <ext:Button ID="btnSave" runat="server" Icon="Add" Text="保存送检单" Disabled="true">
                                <Listeners>
                                    <Click Fn="refreshMain" />
                                </Listeners>
                                <%--<DirectEvents>
                                    <Click OnEvent="btnSave_Click" />
                                </DirectEvents>--%>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsBegin" />
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
                                    <ext:TextField ID="txtBillNo" runat="server" Text="(自动生成)" FieldLabel="送检单号" LabelAlign="Left" ReadOnly="true" />
                                    <ext:TextField ID="txtNoticeNo" runat="server" FieldLabel="通知单号" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" LabelAlign="Left">
                                    </ext:TextField>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:TriggerField ID="txtFactoryID" runat="server" FieldLabel="生产厂家" LabelAlign="Left" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"
                                        Editable="false">
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Search" />
                                        </Triggers>
                                        <RemoteValidation OnValidation="CheckField" />
                                        <Listeners>
                                            <TriggerClick Fn="AddFactory" />
                                        </Listeners>
                                    </ext:TriggerField>
                                    <ext:DateField ID="txtSendChkDate" runat="server" FieldLabel="送检日期" LabelAlign="Left" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"></ext:DateField>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:TextField ID="txtRemark" runat="server" FieldLabel="备注" LabelAlign="Left" />
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
                                    <ext:Model ID="model" Name="PstMaterialChkDetail" runat="server" IDProperty="Barcode">
                                        <Fields>
                                            <%--<ext:ModelField Name="BillNo" />--%>
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="OrderID" />
                                            <ext:ModelField Name="ProductNo" />
                                            <ext:ModelField Name="MaterialName"/>
                                            <ext:ModelField Name="ProcDate" Type="Date" />
                                            <ext:ModelField Name="SendNum" Type="Int" />
                                            <ext:ModelField Name="PieceWeight" Type="Float" />
                                            <ext:ModelField Name="SendWeight" Type="Float" />
                                            <ext:ModelField Name="ChkDate" Type="Date" />
                                            <ext:ModelField Name="InStockDate" Type="Date" />
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
                                 <ext:Column ID="OrderID" runat="server" Text="序号" DataIndex="OrderID" Flex="1" />
                                <ext:Column ID="ProductNo" runat="server" Text="批次号" DataIndex="ProductNo" Flex="1" />
                                <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                <ext:DateColumn ID="ProcDate" runat="server" Text="生产日期" DataIndex="ProcDate" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:Column ID="SendNum" runat="server" Text="数量" DataIndex="SendNum" Flex="1" />
                                <ext:Column ID="PieceWeight" runat="server" Text="单重" DataIndex="PieceWeight" Flex="1" />
                                <ext:Column ID="SendWeight" runat="server" Text="实际重量" DataIndex="SendWeight" Flex="1" />
                                <%--<ext:DateColumn ID="InStockDate1" runat="server" Text="接收日期" DataIndex="InStockDate" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />--%>
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
            <ext:Window ID="winAddDetail" runat="server" Icon="MonitorAdd" Closable="true" Title="添加送检单明细信息"
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
                            <ext:TextField ID="txtBarcode3" runat="server" FieldLabel="条码号" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false">
                                <RemoteValidation OnValidation="CheckField" />
                            </ext:TextField>
                            <ext:TextField ID="txtProductNo3" runat="server" FieldLabel="批次号" LabelAlign="Left" Hidden="true" />
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
                            <ext:DateField ID="txtProcDate3" runat="server" Format="yyyy-MM-dd" FieldLabel="生产日期" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" LabelAlign="Left" />
                            <ext:TextField ID="txtSendNum3" runat="server" Vtype="integer" FieldLabel="数量" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" OnDirectChange="txtSend_Change" LabelAlign="Left" >
                                <RemoteValidation OnValidation="CheckField" />
                            </ext:TextField>
                            <ext:TextField ID="txtSendWeight3" runat="server" Vtype="decimal" FieldLabel="实际重量" IndicatorText="*" IndicatorCls="red-text" OnDirectChange="txtSend_Change" AllowBlank="false" LabelAlign="Left">
                                <RemoteValidation OnValidation="CheckField" />
                            </ext:TextField>
                            <ext:TextField ID="txtPieceWeight3" runat="server" FieldLabel="单重" LabelAlign="Left" ReadOnly="true">
                            </ext:TextField>
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
                    <Show Handler="for(var i=0;i<#{vpChkBill}.items.length;i++){#{vpChkBill}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpChkBill}.items.length;i++){#{vpChkBill}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>

            <ext:Window ID="winModifyDetail" runat="server" Icon="MonitorEdit" Closable="true" Title="修改送检单明细信息"
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
                            <ext:TextField ID="txtBarcode2" runat="server" FieldLabel="条码号" LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                            <ext:TextField ID="txtProductNo2" runat="server" FieldLabel="批次号" LabelAlign="Left" Hidden="true" />
                            <ext:TriggerField ID="txtMaterialName2" runat="server" FieldLabel="物料名称" LabelAlign="Left" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"
                                Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <RemoteValidation OnValidation="CheckField" />
                                <Listeners>
                                    <TriggerClick Fn="EditMaterial" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:DateField ID="txtProcDate2" runat="server" Format="yyyy-MM-dd" FieldLabel="生产日期" AllowBlank="false" LabelAlign="Left" />
                            <ext:TextField ID="txtSendNum2" runat="server" Vtype="integer" FieldLabel="数量" OnDirectChange="txtSend2_Change" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" LabelAlign="Left" >
                                <RemoteValidation OnValidation="CheckField" />
                            </ext:TextField>
                            <ext:TextField ID="txtPieceWeight2" runat="server" FieldLabel="单重" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" LabelAlign="Left" ReadOnly="true" />
                            <ext:TextField ID="txtSendWeight2" runat="server" FieldLabel="实际重量" Vtype="decimal" OnDirectChange="txtSend2_Change" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" LabelAlign="Left" />
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
                    <Show Handler="for(var i=0;i<#{vpChkBill}.items.length;i++){#{vpChkBill}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpChkBill}.items.length;i++){#{vpChkBill}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>
            <ext:Hidden ID="hiddenBillNo" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenMaterCode" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenFactoryID" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenBarcode" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenTxtBillNo" runat="server"></ext:Hidden>
            <ext:Hidden ID="hiddenOrderID" runat="server"></ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
