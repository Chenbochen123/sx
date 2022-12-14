<%@ page language="C#" autoeventwireup="true" inherits="Manager_Storage_MaterialChkBill, App_Web_p5ht2o2r" %>
<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell {
            background-color: #B0FFBA !important;
        }
    </style>
    <script type="text/javascript">
        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("ChkResultFlag") == "合格" && record.get("stockinflag") == "未入库") {
                return "x-grid-row-collapsed";
            }
        }
        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var BillNo = "";
            if (record != null)
                BillNo = record.data.BillNo;
            var url = "Storage/MaterialChkBillInsert.aspx?BillNo=" + BillNo;
            var tabid = "Manager_Storage_MaterialChkBillInsert";
            var tabp = parent.App.mainTabPanel;
            var tab = tabp.getComponent(tabid);
            if (tab) {
                tab.close();
            }
            var title;
            if (record != null)
                title = "送检单编辑";
            else
                title = "送检单录入";
            parent.addTab(tabid, title, url, true);
            parent.refresh("");
            //            App.direct.commandcolumn_direct_edit(BillNo, {
            //                success: function (result) {
            //                },

            //                failure: function (errorMsg) {
            //                    Ext.Msg.alert('操作', errorMsg);
            //                }
            //            });
        }

        var showDetailWindow = function (id) {
            var url = "MaterialChkBillInsert.aspx?BillNo=" + id;
            var tabid = "Manager_Storage_MaterialChkBillInsert";
            var tabp = parent.App.mainTabPanel;
            var tab = tabp.getComponent(tabid);
            if (tab) {
                tab.close();
            }
            parent.addTab(tabid, "送检单录入", url, true);
        }

        var refreshLoaction = function () {
            //App.direct.GridPanelBindData;
        }

        //点击删除按钮
        var commandcolumn_direct_delete = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var BillNo = record.data.BillNo;
            App.direct.commandcolumn_direct_delete(BillNo, {
                success: function (result) {
                    if (result == "false") {
                        Ext.Msg.alert('操作', "删除失败，单据正在审核中！");
                    }
                    else
                        Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //点击归档按钮
        var commandcolumn_direct_filed = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var BillNo = record.data.BillNo;
            App.direct.commandcolumn_direct_filed(BillNo, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //根据按钮类别进行删除和编辑操作
        var commandcolumn_click = function (command, record) {
            commandcolumn_click_confirm(command, record);
            return false;
        };

        //区分删除操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
            }
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定要删除此条信息吗？', function (btn) { commandcolumn_direct_delete(btn, record) });
            }
            if (command.toLowerCase() == "cancel") {
                Ext.Msg.confirm("提示", '您确定要作废此条信息吗？', function (btn) { commandcolumn_direct_cancel(btn, record) });
            }
            if (command.toLowerCase() == "filed") {
                Ext.Msg.confirm("提示", '您确定要归档吗？', function (btn) { commandcolumn_direct_filed(btn, record) });
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
            integerText: "此填入项格式为正整数"
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

        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
            if (record.get("FiledFlag") == "1") {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
                toolbar.items.getAt(3).hide();
            }
            else if (record.get("SendChkFlag") == "1") {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
            }
            else {
                toolbar.items.getAt(3).hide();
            }
        };

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
            if (!App.winPrint2.hidden) {
                App.txtFacName2.getTrigger(0).show();
                App.txtFacName2.setValue(record.data.FacName);
                App.hdnFacId2.setValue(record.data.ObjID);
            }
            
            else {
                App.txtFactoryID.getTrigger(0).show();
                App.txtFactoryID.setValue(record.data.FacName);
                App.hiddenFactoryID.setValue(record.data.ObjID);
                App.pageToolBar.doRefresh();
            }
          
        }

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理


            if (!App.winPrint2.hidden) {
                App.txtMatName2.getTrigger(0).show();
                App.txtMatName2.setValue(record.data.MaterialName);
                App.hdnMatCode2.setValue(record.data.MaterialCode);

            }
            //if (!App.winAddDetail.hidden) {
            //    App.txtMaterialName3.setValue(record.data.MaterialName);
            //    App.hiddenMaterCode.setValue(record.data.MaterialCode);
            //}
            //else if (!App.winModifyDetail.hidden) {
            //    //                App.hiddenBillNo.setValue(record.data.BillNo);
            //    //                App.hiddenFactoryID.setValue(record.data.FactoryID);
            //}
        }

        var Manager_BasicInfo_CommonPage_QueryBasUser_Request = function (record) {//用户返回值处理
            App.txtMakerPerson.getTrigger(0).show();
            App.txtMakerPerson.setValue(record.data.UserName);
            App.hiddenMakerPerson.setValue(record.data.HRCode);
            App.pageToolBar.doRefresh();
        }

        var QueryFactory = function (field, trigger, index) {//厂家查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenFactoryID.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    var url = "../BasicInfo/CommonPage/QueryFactory.aspx?QueryFlag=1";
                    var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
                    if (App.Manager_BasicInfo_CommonPage_QueryFactory_Window.getBody()) {
                        App.Manager_BasicInfo_CommonPage_QueryFactory_Window.getBody().update(html);
                    } else {
                        App.Manager_BasicInfo_CommonPage_QueryFactory_Window.html = html;
                    }
                    App.Manager_BasicInfo_CommonPage_QueryFactory_Window.show();
                    break;
            }
        }

        var QueryUser = function (field, trigger, index) {//人员查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenMakerPerson.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryBasUser_Window.show();
                    break;
            }
        }
        var QueryMaterial = function (field, trigger, index) {//物料查询
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    field.getEl().down('input.x-form-text').setStyle('background', "white");

                    if (!App.winPrint2.hidden) {
                        App.hdnMatCode2.setValue("");
                    }

                    break;
                case 1:
                    var url = "../BasicInfo/CommonPage/QueryMaterial.aspx";
                    var html = "<iframe src='" + url + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
                    if (App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.getBody()) {
                        App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.getBody().update(html);
                    } else {
                        App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.html = html;
                    }
                    App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
                    break;
            }

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

        //点击取消作废按钮
        var commandcolumn_direct_usedflag = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnBatchUsing_Click({
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var commandcolumn_direct_sendchkflag = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnBatchSend_Click({
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }
        var commandcolumn_direct_sendfxflag = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnFxSend_Click({
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }
        var commandcolumn_direct_sendfxflag2 = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnFxSend_Click2({
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }
        var SetSendChkFlag = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要提交审核吗？', function (btn) { commandcolumn_direct_sendchkflag(btn) });
            }
        }

        var SetFXFlag = function () {
            var section = App.pnlDetailList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要提交放行吗？', function (btn) { commandcolumn_direct_sendfxflag(btn) });
            }
        }
        var SetFXFlag2 = function () {
            var section = App.pnlDetailList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要提交放行吗？', function (btn) { commandcolumn_direct_sendfxflag2(btn) });
            }
        }
        var commandcolumn_direct_cancelsendchk = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnCancelSend_Click({
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var CancelSendChk = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要撤销审核吗？', function (btn) { commandcolumn_direct_cancelsendchk(btn) });
            }
        }

        var sendChkChange = function (value) {
            return Ext.String.format(value ? "已审核" : "未审核");
        };

        var filedChange = function (value) {
            return Ext.String.format(value ? "是" : "否");
        };

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
                                <ext:Button runat="server" Icon="Add" Text="添加" ID="btnAdd">
                                    <%--<DirectEvents>
                                    <Click OnEvent="btnAdd_Click" />
                                </DirectEvents>--%>
                                    <Listeners>
                                        <Click Handler="commandcolumn_direct_edit(null)" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsBegin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <Listeners>
                                        <Click Fn="pnlListFresh" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsMiddle" />
                                <ext:Button runat="server" Icon="LockEdit" Text="送检审核" ID="Button1">
                                    <Listeners>
                                        <Click Handler="SetSendChkFlag();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsMiddle2" />
                                <ext:Button runat="server" Icon="LockEdit" Text="撤销审核" ID="Button2">
                                    <Listeners>
                                        <Click Handler="CancelSendChk();" />
                                    </Listeners>
                                </ext:Button>
                                    <ext:ToolbarSeparator ID="tsMiddle1" />
                                <ext:Button runat="server" Icon="LockEdit" Text="放行" ID="Button3">
                                    <Listeners>
                                        <Click Handler="SetFXFlag();" />
                                    </Listeners>
                                </ext:Button>
                                 <ext:ToolbarSeparator ID="tsMiddle3" />
                                <ext:Button runat="server" Icon="LockEdit" Text="取消放行" ID="Button4">
                                    <Listeners>
                                        <Click Handler="SetFXFlag2();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsEnd" />

                                
                                <ext:Button runat="server" ID="btnPrintBarcode2" Icon="ChartBar" Text="调拨单条码打印">
                                    <DirectEvents>
                                        <Click OnEvent="btnPrintBarcode2_Click"></Click>
                                    </DirectEvents>
                                </ext:Button>

                                  <ext:ToolbarSpacer runat="server" ID="ToolbarSpacer3" />
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
                                        <ext:TextField ID="txtBillNo" runat="server" FieldLabel="送检单号" LabelAlign="Right" />
                                        <ext:DateField ID="txtBeginTime" runat="server" OnDirectChange="txtBeginTime_change" FieldLabel="开始时间" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                        <ext:TextField ID="txtNoticeNo" runat="server" FieldLabel="通知单号" LabelAlign="Right" />
                                        <ext:DateField ID="txtEndTime" runat="server" OnDirectChange="txtEndTime_change" FieldLabel="结束时间" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                        <ext:TriggerField ID="txtFactoryID" runat="server" FieldLabel="生产厂家" LabelAlign="Right"
                                            Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <RemoteValidation OnValidation="CheckField" />
                                            <Listeners>
                                                <TriggerClick Fn="QueryFactory" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:ComboBox ID="cbxSendChkFlag" runat="server" FieldLabel="审核状态" OnDirectChange="cbxSendChkFlag_change" LabelAlign="Right">
                                            <SelectedItems>
                                                <ext:ListItem Value="all">
                                                </ext:ListItem>
                                            </SelectedItems>
                                            <Items>
                                                <ext:ListItem Text="全部" Value="all" AutoDataBind="true">
                                                </ext:ListItem>
                                                <ext:ListItem Text="已审核" Value="1">
                                                </ext:ListItem>
                                                <ext:ListItem Text="未审核" Value="0">
                                                </ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container4" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                    Padding="5">
                                    <Items>
                                          <ext:ComboBox ID="ComboBox3" runat="server" FieldLabel="单据状态" LabelAlign="Right" >
                                            <SelectedItems>
                                                <ext:ListItem Value="all">
                                                </ext:ListItem>
                                            </SelectedItems>
                                            <Items>
                                                <ext:ListItem Text="全部" Value="all" AutoDataBind="true">
                                                </ext:ListItem>
                                                <ext:ListItem Text="合格未入库" Value="1">
                                                </ext:ListItem>
                                               
                                            </Items>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cbxFiledFlag" runat="server" FieldLabel="是否归档" LabelAlign="Right" Visible="false">
                                            <SelectedItems>
                                                <ext:ListItem Value="all">
                                                </ext:ListItem>
                                            </SelectedItems>
                                            <Items>
                                                <ext:ListItem Text="全部" Value="all" AutoDataBind="true">
                                                </ext:ListItem>
                                                <ext:ListItem Text="是" Value="1">
                                                </ext:ListItem>
                                                <ext:ListItem Text="否" Value="0">
                                                </ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
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
                                        <ext:Model ID="model" runat="server" IDProperty="BillNo">
                                            <Fields>
                                                <ext:ModelField Name="BillNo" />
                                                <ext:ModelField Name="NoticeNo" />
                                                <ext:ModelField Name="FactoryID" />
                                                <ext:ModelField Name="FacName" />
                                                <ext:ModelField Name="SendChkFlag" Type="Boolean" />
                                                <ext:ModelField Name="MakerPerson" />
                                                <ext:ModelField Name="UserName" />
                                                <ext:ModelField Name="LockedFlag" />
                                                <ext:ModelField Name="FiledFlag" />
                                                <ext:ModelField Name="Remark" />
                                                  <ext:ModelField Name="ChkResultFlag" />
                                                   <ext:ModelField Name="stockinflag" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="colModel" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                    <ext:Column ID="billNo" runat="server" Text="单据号" DataIndex="BillNo" Flex="1" />
                                    <ext:Column ID="noticeNo" runat="server" Text="通知单号" DataIndex="NoticeNo" Flex="1" />
                                    <ext:Column ID="factoryName" runat="server" Text="生产厂家" DataIndex="FacName" Flex="1" />
                                    <ext:Column ID="userName" runat="server" Text="制单人" DataIndex="UserName" Flex="1" />
                                     <ext:Column ID="Column1" runat="server" Text="质检" DataIndex="ChkResultFlag" Flex="1" />
                                     <ext:Column ID="Column2" runat="server" Text="入库状态" DataIndex="stockinflag" Flex="1" />
                                    <ext:Column ID="sendChkFlag" runat="server" Text="审核状态" DataIndex="SendChkFlag" Flex="1">
                                        <Renderer Fn="sendChkChange" />
                                    </ext:Column>
                                    <ext:Column ID="filedFlag" runat="server" Text="是否归档" Visible="false" DataIndex="FiledFlag" Flex="1">
                                        <Renderer Fn="filedChange" />
                                    </ext:Column>
                                    <%--<ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Flex="1" />--%>
                                    <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                        <Commands>
                                            <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改" />
                                            <ext:CommandSeparator />
                                            <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除" />
                                            <ext:GridCommand Icon="TableEdit" CommandName="Filed" Text="归档" />
                                            <%--<ext:CommandSeparator />
                                        <ext:GridCommand Icon="Add" CommandName="Add" Text="添加明细数据" />--%>
                                        </Commands>
                                        <PrepareToolbar Fn="prepareToolbar" />
                                        <Listeners>
                                            <Command Handler="return commandcolumn_click(command, record);" />
                                        </Listeners>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                                    <%--<DirectEvents>
                                    <Select OnEvent="RowSelect" Buffer="250">
                                        <ExtraParams>
                                            <ext:Parameter Name="BillNo" Value="record.getId()" Mode="Raw" />
                                        </ExtraParams>
                                    </Select>
                                </DirectEvents>--%>
                                    <Listeners>
                                        <Select Handler="#{storeDetail}.reload();" Buffer="250" />
                                    </Listeners>
                                </ext:RowSelectionModel>
                            </SelectionModel>
                            <View>
                                <ext:GridView ID="gvRows" runat="server">
                                    <GetRowClass Fn="SetRowClass" />
                                </ext:GridView>
                            </View>
                            <BottomBar>
                                <ext:PagingToolbar ID="pageToolBar" runat="server">
                                    <Items>
                                        <ext:Label ID="Label1" runat="server" Text="每页条数:" />
                                        <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
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
                <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="送检单明细数据" Height="200" Icon="Basket" Layout="Fit" Collapsible="true"
                    Split="true" MarginsSummary="0 5 5 5">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:Button runat="server" ID="btnPrintBarcode" Icon="ChartBar" Text="打印条码">
                                    <Listeners>
                                        <Click Handler="if (#{rowSelectMutiDetail}.selected.items.length == 0) { debugger; Ext.Msg.alert('提示', '请选择要打印的明细条码'); return false; }" />
                                    </Listeners>
                                    <DirectEvents>
                                        <Click OnEvent="btnPrintBarcode_Click">
                                            <ExtraParams>
                                                <ext:Parameter Name="Barcode" Mode="Raw" Value="#{rowSelectMutiDetail}.selected.items[0].data.Barcode"></ext:Parameter>
                                                <ext:Parameter Name="MatName" Mode="Raw" Value="#{rowSelectMutiDetail}.selected.items[0].data.MaterialName"></ext:Parameter>
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar> <Items>
                        <ext:GridPanel ID="pnlDetailList" runat="server" MarginsSummary="0 5 5 5">
                            <Store>
                                <ext:Store ID="storeDetail" runat="server" PageSize="10" OnReadData="RowSelect">
                                    <%-- <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindDetail" />
                                </Proxy>--%>
                                    <Model>
                                        <ext:Model ID="modelDetail" runat="server" IDProperty="Barcode">
                                            <Fields>
                                                <ext:ModelField Name="BillNo" />
                                                <ext:ModelField Name="Barcode" />
                                                <ext:ModelField Name="ProductNo" />
                                                <ext:ModelField Name="MaterCode" />
                                                <ext:ModelField Name="MaterialName" />
                                                <ext:ModelField Name="ProcDate" Type="Date" />
                                                <ext:ModelField Name="SendChkDate" Type="Date" />
                                                <ext:ModelField Name="SendNum" Type="Int" />
                                                <ext:ModelField Name="SendWeight" Type="Float" />
                                                <ext:ModelField Name="ChkDate" Type="Date" />
                                                <ext:ModelField Name="InStockDate" Type="Date" />
                                                <ext:ModelField Name="ChkResultFlag" />
                                                <ext:ModelField Name="PassWeight" Type="Float" />
                                                <ext:ModelField Name="SendChkFlag" />
                                                <ext:ModelField Name="PassNum" Type="Int" />
                                                <ext:ModelField Name="StoreInNum" Type="Int" />
                                                <ext:ModelField Name="StoreInWeight" Type="Float" />
                                                <ext:ModelField Name="LLBarcode" />
                                                  <ext:ModelField Name="FXFlag" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Parameters>
                                        <ext:StoreParameter Name="BillNo" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.BillNo : -1" />
                                    </Parameters>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModelDetail" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />
                                    <%--<ext:Column ID="ProductNo" runat="server" Text="批次号" DataIndex="ProductNo" Flex="1" />--%>
                                    <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName" Flex="1" />
                                    <ext:DateColumn ID="SendChkDate" runat="server" Text="送检日期" DataIndex="SendChkDate" Flex="1" Format="yyyy-MM-dd HH:mm:ss" />
                                    <ext:Column ID="SendNum" runat="server" Text="数量" DataIndex="SendNum" Flex="1" />
                                    <ext:Column ID="SendWeight" runat="server" Text="实际重量" DataIndex="SendWeight" Flex="1" />
                                    <ext:Column ID="PassNum" runat="server" Text="合格数量" DataIndex="PassNum" Flex="1" />
                                    <ext:Column ID="PassWeight" runat="server" Text="合格重量" DataIndex="PassWeight" Flex="1" />
                                    <ext:Column ID="StoreInNum" runat="server" Text="入库数量" DataIndex="StoreInNum" Flex="1" />
                                    <ext:Column ID="StoreInWeight" runat="server" Text="入库重量" DataIndex="StoreInWeight" Flex="1" />
                                    <ext:Column ID="LLBarcode" runat="server" Text="批次号" DataIndex="LLBarcode" Flex="1" />
                                    <ext:Column ID="Barcode" runat="server" Text="条码号" DataIndex="Barcode" Flex="1" />
                                     <ext:Column ID="Column3" runat="server" Text="放行" DataIndex="FXFlag" Flex="1" />
                                   
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectMutiDetail" runat="server" Mode="Multi" />
                            </SelectionModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server">
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
                                                <Select Handler="#{pnlDetailList}.store.pageSize = parseInt(this.getValue(), 10); #{PagingToolbar1}.doRefresh(); return false;" />
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                    <Plugins>
                                        <ext:ProgressBarPager ID="ProgressBarPager1" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>

            
          

                <ext:Hidden ID="hiddenBillNo" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenFactoryID" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenMaterCode" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenMakerPerson" runat="server"></ext:Hidden>
            </Items>
        </ext:Viewport>
            <ext:Window runat="server" ID="winPrint" Width="500" Height="400" Hidden="true" Modal="true">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button runat="server" ID="btnBuildBarcode" Text="生成条码" Icon="ChartBar">
                            <DirectEvents>
                                <Click OnEvent="btnBuildBarcode_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Items>
                <ext:Panel runat="server" Region="North" Layout="FormLayout">
                    <Items>
                        <ext:TextField runat="server" ID="txtMatName" FieldLabel="物料" ReadOnly="true" InputWidth="160"></ext:TextField>
                        <ext:TextField runat="server" ID="txtBarcode" FieldLabel="条码号" ReadOnly="true" InputWidth="160"></ext:TextField>
                        <ext:TextField runat="server" ID="txtLLProductNo" FieldLabel="批次" InputWidth="160"></ext:TextField>
                    </Items>
                </ext:Panel>
                <ext:Panel runat="server" Region="Center" Layout="FitLayout">
                    <Items>
                        <ext:Panel runat="server">
                            <Content>
                                <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" Font-Bold="False"
                                    Width="100%" Zoom="1" ShowRefreshButton="false" ShowZoomButton="false" ShowExports="false"
                                    Padding="3, 3, 3, 3" ToolbarColor="Lavender" PrintInPdf="True" PdfEmbeddingFonts="True" Layers="False" />
                            </Content>

                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Window>

        <ext:Window runat="server" ID="winPrint2" Width="500" Height="400" Hidden="true" Modal="true">
            <TopBar>
                <ext:Toolbar runat="server">
                    <Items>
                        <ext:Button runat="server" ID="btnBuildBarcode2" Text="生成条码" Icon="ChartBar">
                            <DirectEvents>
                                <Click OnEvent="btnBuildBarcode2_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
            <Items>
                <ext:Panel runat="server" Region="North" Layout="FormLayout">
                    <Items>
                        <ext:TriggerField runat="server" ID="txtMatName2" FieldLabel="物料" Editable="false" InputWidth="260">
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                <ext:FieldTrigger Icon="Search" />
                            </Triggers>
                            <Listeners>
                                <TriggerClick Fn="QueryMaterial" />
                            </Listeners>
                            <DirectEvents>
                                <Change OnEvent="BuildBarcode2" />
                            </DirectEvents>
                        </ext:TriggerField>
                        <ext:Hidden runat="server" ID="hdnMatCode2" />
                        <ext:TriggerField runat="server" ID="txtFacName2" FieldLabel="厂家" Editable="false" InputWidth="260">
                            <Triggers>
                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                <ext:FieldTrigger Icon="Search" />
                            </Triggers>
                            <Listeners>
                                <TriggerClick Fn="QueryFactory" />
                            </Listeners>
                            <DirectEvents>
                                <Change OnEvent="BuildBarcode2" />
                            </DirectEvents>
                        </ext:TriggerField>
                        <ext:Hidden runat="server" ID="hdnFacId2" />
                        <ext:DateField runat="server" ID="txtProductDate2" FieldLabel="生产日期" InputWidth="160">
                            <DirectEvents>
                                <Change OnEvent="BuildBarcode2" />
                            </DirectEvents>
                        </ext:DateField>
                        <ext:TextField runat="server" ID="txtBarcode2" FieldLabel="条码号" InputWidth="260"></ext:TextField>
                        <ext:TextField runat="server" ID="txtLLProductNo2" FieldLabel="批次" InputWidth="160"></ext:TextField>
                    </Items>
                </ext:Panel>
                <ext:Panel runat="server" Region="Center" Layout="FitLayout">
                    <Items>
                        <ext:Panel runat="server">
                            <Content>
                                <cc1:WebReport ID="WebReport2" runat="server" BackColor="White" Font-Bold="False"
                                    Width="100%" Zoom="1" ShowRefreshButton="false" ShowZoomButton="false" ShowExports="false"
                                    Padding="3, 3, 3, 3" ToolbarColor="Lavender" PrintInPdf="True" PdfEmbeddingFonts="True" Layers="False" />
                            </Content>

                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Window>
    </form>
</body>
</html>
