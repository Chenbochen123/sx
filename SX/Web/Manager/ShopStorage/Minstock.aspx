﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Minstock.aspx.cs" Inherits="Manager_ShopStorage_Minstock" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
        }

        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var ShopoutID = record.data.Serial_Id;
            App.hiddenSerialID.setValue(ShopoutID);
            App.direct.commandcolumn_direct_edit(ShopoutID, {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //点击删除按钮
        var commandcolumn_direct_delete = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ShopoutID = record.data.Serial_Id;
            App.direct.commandcolumn_direct_delete(ShopoutID, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
            }
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定要删除此条信息吗？', function (btn) { commandcolumn_direct_delete(btn, record) });
            }
            else if (command == "detail") {
                commandcolumn_direct_detail(record);
            }
            return false;
        };
        var commandcolumn_click = function (command, record) {
            commandcolumn_click_confirm(command, record);
            return false;
        };


        var commandcolumn_direct_detail = function (record) {
            var ObjID = record.data.Serial_Id;
            App.direct.commandcolumn_direct_detail(ObjID, {
                success: function (result) {
                },
                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
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

        var AddMaterial = function () {//物料添加
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
        }


        var AddStoragePlace = function (field, trigger, index) {//库位添加
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
            html: "<iframe src='../BasicInfo/CommonPage/QueryMaterial.aspx?MajorTypeID=1' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料",
            modal: true
        })

        var SetFXFlag = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要提交放行吗？', function (btn) { commandcolumn_direct_sendfxflag(btn) });
            }
        }
        var SetFXFlag3 = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                App.direct.btnFxSend_Click3({
                    success: function (result) {

                    },

                    failure: function (errorMsg) {

                    }
                });
            }
        }
        var SetFXFlag2 = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要取消放行吗？', function (btn) { commandcolumn_direct_sendfxflag2(btn) });
            }
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

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            if (!App.winAdd.hidden) {
                App.txtMaterialName1.setValue(record.data.MaterialName);
                App.hiddenMaterCode.setValue(record.data.MaterialCode);
            }
            else if (!App.winModify.hidden) {
                App.txtMaterialName2.setValue(record.data.MaterialName);
                App.hiddenMaterCode.setValue(record.data.MaterialCode);
            }
            else {
                App.txtMaterName.getTrigger(0).show();
                App.txtMaterName.setValue(record.data.MaterialName);
                App.hiddenMaterCode.setValue(record.data.MaterialCode);
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />

        <ext:ResourceManager ID="rmMmShopout" runat="server" />
        <ext:Viewport ID="vpMmShopout" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnMmShopoutTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="tbMmShopout">
                            <Items>
                                <ext:Button runat="server" Icon="Add" Text="添加" ID="btnAdd">
                                    <DirectEvents>
                                        <Click OnEvent="btnAdd_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsBegin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <Listeners>
                                        <Click Fn="pnlListFresh" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsEnd" />
                                <ext:Button runat="server" Icon="LockEdit" Text="放行" ID="btnLock">
                                    <Listeners>
                                        <Click Handler="SetFXFlag();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" Icon="LockEdit" Text="取消放行" ID="btnUnLock">
                                    <Listeners>
                                        <Click Handler="SetFXFlag2();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" Icon="TableEdit" Text="质检" ID="btnCheck">
                                    <Listeners>
                                        <Click Handler="SetFXFlag3();" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarFill ID="ToolbarFill1" />
                                <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                                <ext:ToolbarFill ID="tfEnd" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel ID="pnlStorageQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                            <Items>
                                                <ext:DateField ID="txtBeginTime" runat="server" Editable="false" FieldLabel="开始时间" LabelAlign="Right" />

                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                            <Items>
                                                <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束时间" Editable="false" LabelAlign="Right" />

                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                            <Items>
                                                <ext:TriggerField ID="txtMaterName" runat="server" FieldLabel="原材料名" LabelAlign="Right" Editable="false">
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

                                    </Items>
                                    <Listeners>
                                        <ValidityChange Handler="#{btnSearch}.setDisabled(!valid);" />
                                    </Listeners>
                                </ext:FormPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>

                <ext:GridPanel ID="pnlList" runat="server" Cls="x-grid-custom" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="50">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <%--  IDProperty="Serial_Id"--%>
                                <ext:Model ID="model" runat="server" IDProperty="Serial_Id">
                                    <Fields>
                                        <ext:ModelField Name="Serial_Id" />
                                        <ext:ModelField Name="Stock_date" />
                                        <ext:ModelField Name="Mater_barcode" />
                                        <ext:ModelField Name="Mater_name" />
                                        <ext:ModelField Name="Real_weig" />
                                        <ext:ModelField Name="Real_num" />
                                        <ext:ModelField Name="Mater_batch" />
                                        <ext:ModelField Name="Mater_brand" />
                                        <ext:ModelField Name="sgn" />
                                        <ext:ModelField Name="mem_no" />
                                        <ext:ModelField Name="Result" />
                                        
                                        <ext:ModelField Name="store_name" />
                                        <ext:ModelField Name="USER_NAME" />
                                        <ext:ModelField Name="Cargo_id" />
                                        <ext:ModelField Name="Fac_fname" />
                                        <ext:ModelField Name="Fac_fname1" />
                                        <ext:ModelField Name="Pro_Date" />
                                        <ext:ModelField Name="Limit_Date" />
                                        <ext:ModelField Name="Begin_Use" />
                                        <ext:ModelField Name="End_Use" />
                                        <ext:ModelField Name="Unit_Weight" />
                                        <ext:ModelField Name="Area_Code" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                            <ext:Column ID="Column7" runat="server" Text="流水号" DataIndex="Serial_Id" Flex="1" Hidden="true" />
                            <ext:Column ID="WorkShopCode" runat="server" Text="入库日期" DataIndex="Stock_date" />
                            <ext:Column ID="Shift" runat="server" Text="物料条码" DataIndex="Mater_barcode"  />
                            <ext:Column ID="Column1" runat="server" Text="物料名称" DataIndex="Mater_name" />
                            <ext:Column ID="Column2" runat="server" Text="实际数量" DataIndex="Real_num" hidden="true" />
                            <ext:Column ID="Column3" runat="server" Text="实际重量" DataIndex="Real_weig"  />
                            <ext:Column ID="Column5" runat="server" Text="批次号" DataIndex="Mater_batch" />
                            <%--    <ext:Column ID="Column4" runat="server" Text="ERP编码" DataIndex="Mater_brand" Flex="1" />--%>
                            <ext:Column ID="Column6" runat="server" Text="放行标志" DataIndex="sgn"  />
                            <ext:Column ID="Column9" runat="server" Text="质检结果" DataIndex="Result"/>

                            
                            <ext:Column ID="Column4" runat="server" Text="存放仓库" DataIndex="store_name"  />
                            <ext:Column ID="Column19" runat="server" Text="产地" DataIndex="Area_Code"  />
                            <ext:Column ID="Column10" runat="server" Text="收货人" DataIndex="USER_NAME" />
                            <ext:Column ID="Column11" runat="server" Text="物料类型" DataIndex="Cargo_id"  />
                            <ext:Column ID="Column12" runat="server" Text="供货厂家" DataIndex="Fac_fname"  />
                            <ext:Column ID="Column13" runat="server" Text="生产厂家" DataIndex="Fac_fname1" />
                            <ext:Column ID="Column14" runat="server" Text="生产日期" DataIndex="Pro_Date"  />
                            <ext:Column ID="Column15" runat="server" Text="过期日期" DataIndex="Limit_Date" />
                            <ext:Column ID="Column16" runat="server" Text="开始使用时间" DataIndex="Begin_Use"  />
                            <ext:Column ID="Column17" runat="server" Text="结束使用时间" DataIndex="End_Use"  />
                            <ext:Column ID="Column18" runat="server" Text="单重" DataIndex="Unit_Weight" />


                            <ext:Column ID="Column8" runat="server" Text="备注" DataIndex="mem_no"  />
                            <ext:CommandColumn ID="commandCol" runat="server" Width="250" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                        <ToolTip Text="修改本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                        <ToolTip Text="删除本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="ApplicationViewDetail" CommandName="detail" Text="打印">
                                        <ToolTip Text="打印本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Accept" CommandName="Recover" Text="恢复" hidden="true">
                                        <ToolTip Text="恢复本条数据" />
                                    </ext:GridCommand>
                                </Commands>
                                <PrepareToolbar Fn="prepareToolbar" />
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
                                        <ext:ListItem Text="20" />
                                        <ext:ListItem Text="50" />
                                        <ext:ListItem Text="100" />
                                    </Items>
                                    <SelectedItems>
                                        <ext:ListItem Value="50" />
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

                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改消耗信息"
                    Width="620" Height="330" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:FormPanel ID="pnlEdit" runat="server" Flex="1" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:CheckboxGroup ID="CheckboxGroup1" runat="server" ColumnsNumber="2" Flex="1" AnchorHorizontal="true">
                                    <Items>
                                        <ext:DateField ID="MoStockDate" runat="server" Editable="false" FieldLabel="入库时间" LabelAlign="Right" />
                                        <ext:DateField ID="MoProDate" runat="server" Editable="false" FieldLabel="生产时间" LabelAlign="Right" />

                                        <ext:ComboBox ID="MoStock" runat="server" LabelAlign="Right" Flex="1" FieldLabel="存放仓库" SelectOnTab="true" />
                                        <ext:TextField ID="MoBatch" runat="server" FieldLabel="批次编号" LabelAlign="Right" />


                                        <ext:ComboBox ID="MoFac" runat="server" LabelAlign="Right" Flex="1" FieldLabel="生产厂家" SelectOnTab="true" />
                                        <ext:ComboBox ID="MoFac2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="二级供应商" SelectOnTab="true" />
                                        <ext:ComboBox ID="MoHr" runat="server" LabelAlign="Right" Flex="1" FieldLabel="收货人" SelectOnTab="true" />
                                        <ext:TextField ID="txtchandi" runat="server" FieldLabel="产地" LabelAlign="Right"  />

                                        <ext:TextField ID="txtMaterName2" runat="server" FieldLabel="物料" LabelAlign="Right" ReadOnly="true" />
                                        <ext:NumberField ID="MoNum" runat="server" FieldLabel="实收数量" LabelAlign="Right" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" DecimalPrecision="0" />
                                        <ext:NumberField ID="MoWeight" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="2" FieldLabel="实收重量" />
                                        <ext:TextField ID="MoCargo_id" runat="server" FieldLabel="物料型号" LabelAlign="Right" />
                                        <ext:TextField ID="MoStand" runat="server" FieldLabel="单重" LabelAlign="Right" />
                                        <ext:TextField ID="MoRemark" runat="server" FieldLabel="备注" LabelAlign="Right" />
                                        <ext:ComboBox ID="MoFx" runat="server" LabelAlign="Right" Flex="1" FieldLabel="放行" SelectOnTab="true" ReadOnly="true" />
                                        <ext:ComboBox ID="MoZj" runat="server" LabelAlign="Right" Flex="1" FieldLabel="质检" SelectOnTab="true" ReadOnly="true" />
                                    </Items>
                                </ext:CheckboxGroup>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnModify}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnModify" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="btnModify_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vpMmShopout}.items.length;i++){#{vpMmShopout}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vpMmShopout}.items.length;i++){#{vpMmShopout}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Window ID="Window1" runat="server" Icon="MonitorEdit" Closable="false" Title="修改质检信息"
                    Width="320" Height="130" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:FormPanel ID="FormPanel1" runat="server" Flex="1" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:CheckboxGroup ID="CheckboxGroup3" runat="server" ColumnsNumber="1" Flex="1" AnchorHorizontal="true">
                                    <Items>
                                        <ext:TextField ID="TextField3" runat="server" FieldLabel="编号" LabelAlign="Right" Hidden="true" />
                                        <ext:ComboBox ID="ComboBox7" runat="server" LabelAlign="Right" Flex="1" FieldLabel="质检" SelectOnTab="true" />
                                    </Items>
                                </ext:CheckboxGroup>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnModify}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="Button4" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="btnModify_Click2">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="Button5" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vpMmShopout}.items.length;i++){#{vpMmShopout}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vpMmShopout}.items.length;i++){#{vpMmShopout}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加原料消耗信息" Width="620" Height="330"
                    Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:CheckboxGroup ID="CheckboxGroup2" runat="server" ColumnsNumber="2" Flex="1" AnchorHorizontal="true">
                                    <Items>
                                        <ext:DateField ID="AddStockDate" runat="server" Editable="false" FieldLabel="入库时间" LabelAlign="Right" />
                                        <ext:DateField ID="AddProDate" runat="server" Editable="false" FieldLabel="生产时间" LabelAlign="Right" />

                                        <ext:ComboBox ID="AddStock" runat="server" LabelAlign="Right" Flex="1" FieldLabel="存放仓库" SelectOnTab="true"  AllowBlank="false"/>
                                        <ext:TextField ID="AddBatch" runat="server" FieldLabel="批次编号" LabelAlign="Right"  AllowBlank="false"/>


                                        <ext:ComboBox ID="AddFac" runat="server" LabelAlign="Right" Flex="1" FieldLabel="生产厂家" SelectOnTab="true" AllowBlank="false"/>
                                        <ext:ComboBox ID="AddFac2" runat="server" LabelAlign="Right" Flex="1" FieldLabel="二级供应商" SelectOnTab="true"  AllowBlank="false"/>
                                        <ext:ComboBox ID="AddHr" runat="server" LabelAlign="Right" Flex="1" FieldLabel="收货人" SelectOnTab="true"  AllowBlank="false"/>

                                        <ext:TriggerField ID="txtMaterialName1" runat="server" FieldLabel="物料名称" LabelAlign="Right" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false"
                                            Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="AddMaterial" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:NumberField ID="AddNum" runat="server" FieldLabel="实收数量" LabelAlign="Right" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" DecimalPrecision="0" />
                                        <ext:NumberField ID="AddWeight" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="2" FieldLabel="实收重量"  AllowBlank="false"/>
                                        <ext:TextField ID="AddCargo_id" runat="server" FieldLabel="物料型号" LabelAlign="Right"  AllowBlank="false"/>
                                        <ext:TextField ID="AddStand" runat="server" FieldLabel="单重" LabelAlign="Right"/>
                                        <ext:TextField ID="AddRemark" runat="server" FieldLabel="备注" LabelAlign="Right"/>
                                    </Items>
                                </ext:CheckboxGroup>
                            </Items>

                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="btnAddSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>

                </ext:Window>


                <ext:Window ID="winDetail" runat="server" Icon="MonitorAdd" Closable="true" Title="仓库入库单打印" AutoScroll="true" Width="1000" Height="500" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;" BodyPadding="5">
                    <Content>
                        <cc1:WebReport ID="WebReport" runat="server" BackColor="White" Font-Bold="False" Width="99%" Height="98%" Zoom="1"
                            Padding="3, 3, 3, 3" ToolbarColor="Lavender" PrintInPdf="True" Layers="False" />
                    </Content>
                </ext:Window>

                <ext:Hidden ID="hiddenUserID" runat="server"></ext:Hidden>

                <ext:Hidden ID="hiddenMaterCode" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenSerialID" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenHigherLevel" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenStorageID" runat="server"></ext:Hidden>

            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
