<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkUserInfo.aspx.cs" Inherits="Manager_BasicInfo_WorkInfo_WorkUserInfo" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>岗位人员信息</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var ObjID = record.data.ObjID;

            App.direct.commandcolumn_direct_edit(ObjID, {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //点击恢复按钮
        var commandcolumn_direct_recover = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ObjID = record.data.ObjID;
            App.direct.commandcolumn_direct_recover(ObjID, {
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
            var ObjID = record.data.ObjID;
            App.direct.commandcolumn_direct_delete(ObjID, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //区分删除操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
            }
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此条信息？', function (btn) { commandcolumn_direct_delete(btn, record) });
            }
            if (command.toLowerCase() == "recover") {
                Ext.Msg.confirm("提示", '您确定需要恢复此条信息？', function (btn) { commandcolumn_direct_recover(btn, record) });
            }
            return false;
        };

        //根据按钮类别进行删除和编辑操作
        var commandcolumn_click = function (command, record) {
            commandcolumn_click_confirm(command, record);
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

        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.hidden_delete_flag.setValue("0");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        //历史查询按钮点击列表刷新数据重载方法
        var pnlHistoryListFresh = function () {
            App.hidden_delete_flag.setValue("");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        //历史查询根据DeleteFlag的值进行样式绑定
        var SetRowClass = function (record, rowIndex, rowParams, store) {
            if (record.get("DeleteFlag") == "1") {
                return "x-grid-row-deleted";
            }
        }
        //历史查询的每行按钮准备加载
        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
            if (record.get("DeleteFlag") == "1") {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
                toolbar.items.getAt(3).hide();
            } else {
                toolbar.items.getAt(4).hide();
            }
        };

        ////时间区间
        //var onKeyUp = function () {
        //    var me = this,
        //        v = me.getValue(),
        //        field;

        //    if (me.startDateField) {
        //        field = Ext.getCmp(me.startDateField);
        //        field.setMaxValue(v);
        //        me.dateRangeMax = v;
        //    } else if (me.endDateField) {
        //        field = Ext.getCmp(me.endDateField);
        //        field.setMinValue(v);
        //        me.dateRangeMin = v;
        //    }
        //    field.validate();
        //};
        var GetControl = function (ctrlId) {

            var comb = document.getElementById(ctrlId);
            debugger;
            App.direct.GetCombox(comb);
        };
    </script>
</head>
<body>
    <form id="fmUnit" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager ID="rmWork" runat="server" />
        <ext:Hidden ID="HiddenIndex" runat="server" Text="2" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlWorkUserTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barWork">
                            <Items>
                                <ext:Button runat="server" Icon="Add" Text="添加" ID="btn_add">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttAdd" runat="server" Html="点击进行添加" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btn_add_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btn_search">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="pnlListFresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_middle" />
                                <ext:Button runat="server" Icon="Note" Text="历史查询" ID="btn_history_search">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行历史查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="pnlHistoryListFresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_middle_2" />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip2" runat="server" Html="点击将查询结果导出到Excel中" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>

                    <Items>
                        <ext:Panel ID="pnlWorkUserQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:DateField ID="txtStratDate" runat="server" Editable="false" AllowBlank="false"
                                                    Vtype="daterange" FieldLabel="开始日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd">
                                                    <%--<CustomConfig>
                                                    <ext:ConfigItem Name="endDateField" Value="txtEndDate" Mode="Value" />
                                                </CustomConfig>
                                                <Listeners>
                                                    <KeyUp Fn="onKeyUp" />
                                                </Listeners>--%>
                                                </ext:DateField>
                                                <ext:ComboBox ID="cmoShiftID" Editable="false" runat="server" FieldLabel="班次"
                                                    LabelAlign="Right">
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:DateField ID="txtEndDate" runat="server" Editable="false" AllowBlank="false"
                                                    Vtype="daterange" FieldLabel="结束日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd">
                                                    <%--<CustomConfig>
                                                    <ext:ConfigItem Name="startDateField" Value="txtStratDate" Mode="Value" />
                                                </CustomConfig>
                                                <Listeners>
                                                    <KeyUp Fn="onKeyUp" />
                                                </Listeners>--%>
                                                </ext:DateField>
                                                <ext:ComboBox ID="cmoWorkID" Editable="false" runat="server" FieldLabel="岗位"
                                                    LabelAlign="Right">
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="cmoEquipCode" Editable="false" runat="server" FieldLabel="主机手员工编号"
                                                    LabelAlign="Right">
                                                </ext:TextField>
                                                <ext:TextField ID="cmoWorkBarcode" Editable="false" runat="server" FieldLabel="员工编号"
                                                    LabelAlign="Right">
                                                </ext:TextField>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:FormPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="15">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="PdtDate" />
                                        <ext:ModelField Name="EquipCode" />
                                        <ext:ModelField Name="EquipName" />
                                        <ext:ModelField Name="ShiftID" />
                                        <ext:ModelField Name="ShiftName" />
                                        <ext:ModelField Name="ClassID" />
                                        <ext:ModelField Name="ClassName" />
                                        <ext:ModelField Name="UserName" />
                                        <ext:ModelField Name="WorkBarcode" />
                                        <ext:ModelField Name="WorkID" />
                                        <ext:ModelField Name="WorkName" />

                                        <ext:ModelField Name="Attendance" />
                                        <ext:ModelField Name="RecordTime" />
                                        <ext:ModelField Name="RecordWorkBarcode" />
                                        <ext:ModelField Name="DeleteFlag" />
                                        <ext:ModelField Name="Remark" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                            <ext:Column ID="Column1" runat="server" Text="编号" DataIndex="ObjID" Width="120" Hidden="true" />
                            <ext:DateColumn ID="PdtDate" runat="server" Text="日期" Format="yyyy-MM-dd" DataIndex="PdtDate"
                                Width="80" />
                            <ext:Column ID="RecordWorkBarcode" runat="server" Text="主机手员工编号" DataIndex="RecordWorkBarcode" Width="120" />
                            <ext:Column ID="ShiftName" runat="server" Text="班次" DataIndex="ShiftName" Width="120" />
                            <ext:Column ID="ClassName" runat="server" Text="班组" DataIndex="ClassName" Width="120" Hidden="true" />
                            <ext:Column ID="UserName" runat="server" Text="姓名" DataIndex="UserName" Width="120" />
                            <ext:Column ID="WorkBarcode" runat="server" Text="员工编号" DataIndex="WorkBarcode" Width="120" />
                            <ext:Column ID="WorkName" runat="server" Text="岗位" DataIndex="WorkName" Width="120" />
                            <ext:Column ID="Attendance" runat="server" Text="出勤" DataIndex="Attendance" Width="120" />
                            <ext:Column ID="Remark" runat="server" Text="备注" DataIndex="Remark" Width="120" />
                            <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                        <ToolTip Text="修改本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                        <ToolTip Text="删除本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Accept" CommandName="Recover" Text="恢复">
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
                    <View>
                        <ext:GridView ID="gvRows" runat="server">
                            <GetRowClass Fn="SetRowClass" />
                        </ext:GridView>
                    </View>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolBar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改岗位人员信息"
                    Width="320" Height="300" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                <ext:TextField ID="modify_Objid" runat="server" FieldLabel="编号" LabelAlign="Right" Hidden="true" />
                                <ext:DateField ID="modify_Date" runat="server" Editable="false" AllowBlank="false"
                                    FieldLabel="日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd">
                                </ext:DateField>
                                <ext:TextField ID="modify_EquipCode" Editable="false" runat="server" FieldLabel="主机手编号" LabelAlign="Right"></ext:TextField>
                                <ext:ComboBox ID="modify_ShiftID" Editable="false" runat="server" FieldLabel="班次" LabelAlign="Right"></ext:ComboBox>
                                <ext:ComboBox ID="modify_ClassID" Editable="false" runat="server" FieldLabel="班组" LabelAlign="Right" Hidden="true"></ext:ComboBox>
                                <ext:TextField ID="modify_WorkBarcode" Editable="false" runat="server" FieldLabel="员工编号" LabelAlign="Right"></ext:TextField>
                                <ext:ComboBox ID="modify_WorkID" Editable="false" runat="server" FieldLabel="岗位" LabelAlign="Right"></ext:ComboBox>
                                <ext:TextField ID="modify_Attendance" runat="server" FieldLabel="出勤" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" />
                                <ext:TextField ID="modify_Remark" runat="server" FieldLabel="备注" LabelAlign="Right" />
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnModifySave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnModifySave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="BtnModifySave_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="BtnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加岗位人员信息"
                    Width="1000" Height="500" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Panel ID="Panel_1" runat="server" Layout="ColumnLayout" Title="基本信息">
                                    <Items>
                                        <ext:DateField ID="add_Date" runat="server" Editable="false" AllowBlank="false"
                                            FieldLabel="日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd" ColumnWidth="0.2" Padding="5">
                                        </ext:DateField>
                                        <ext:TextField ID="add_EquipCode" Editable="false" runat="server" FieldLabel="主机手编号" LabelAlign="Right" ColumnWidth="0.4" Padding="5" ></ext:TextField>
                                        <ext:ComboBox ID="add_ShiftID" Editable="false" runat="server" FieldLabel="班次" LabelAlign="Right" ColumnWidth="0.2" Padding="5"></ext:ComboBox>
                                        <ext:ComboBox ID="add_ClassID" Editable="false" runat="server" FieldLabel="班组" LabelAlign="Right" Hidden="true"></ext:ComboBox>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel_2" runat="server" Title="人员信息" Height="360" AutoScroll="true">
                                    <Items>
                                        <ext:Panel runat="server">
                                            <TopBar>
                                                <ext:Toolbar runat="server">
                                                    <Items>
                                                        <ext:Button ID="BtnAddUser" runat="server" Text="新增" Icon="Add">
                                                            <DirectEvents>
                                                                <Click OnEvent="BtnAddUser_Click">
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Toolbar>
                                            </TopBar>
                                        </ext:Panel>
                                        <ext:Panel ID="add_Panel1" runat="server" Layout="ColumnLayout" Padding="10">
                                            <Items>
                                                <ext:TextField ID="add_WorkBarcode1" Editable="false" runat="server" FieldLabel="员工编号" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:TextField>
                                                <ext:ComboBox ID="add_WorkID1" Editable="false" runat="server" FieldLabel="岗位" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:ComboBox>
                                                <ext:TextField ID="add_Attendance1" runat="server" FieldLabel="出勤" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25" />
                                                <ext:TextField ID="add_Remark1" runat="server" FieldLabel="备注" LabelAlign="Right" ColumnWidth=".25" />
                                            </Items>
                                        </ext:Panel>

                                        <ext:Panel ID="add_Panel2" runat="server" Layout="ColumnLayout" Padding="10" Hidden="true" Enabled="false">
                                            <Items>
                                                <ext:TextField ID="add_WorkBarcode2" Editable="false" runat="server" FieldLabel="员工编号" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:TextField>
                                                <ext:ComboBox ID="add_WorkID2" Editable="false" runat="server" FieldLabel="岗位" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:ComboBox>
                                                <ext:TextField ID="add_Attendance2" runat="server" FieldLabel="出勤" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25" />
                                                <ext:TextField ID="add_Remark2" runat="server" FieldLabel="备注" LabelAlign="Right" ColumnWidth=".25" />
                                            </Items>
                                        </ext:Panel>

                                        <ext:Panel ID="add_Panel3" runat="server" Layout="ColumnLayout" Padding="10" Hidden="true" Enabled="false">
                                            <Items>
                                                <ext:TextField ID="add_WorkBarcode3" Editable="false" runat="server" FieldLabel="员工编号" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:TextField>
                                                <ext:ComboBox ID="add_WorkID3" Editable="false" runat="server" FieldLabel="岗位" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:ComboBox>
                                                <ext:TextField ID="add_Attendance3" runat="server" FieldLabel="出勤" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25" />
                                                <ext:TextField ID="add_Remark3" runat="server" FieldLabel="备注" LabelAlign="Right" ColumnWidth=".25" />
                                            </Items>
                                        </ext:Panel>

                                        <ext:Panel ID="add_Panel4" runat="server" Layout="ColumnLayout" Padding="10" Hidden="true" Enabled="false">
                                            <Items>
                                                <ext:TextField ID="add_WorkBarcode4" Editable="false" runat="server" FieldLabel="员工编号" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:TextField>
                                                <ext:ComboBox ID="add_WorkID4" Editable="false" runat="server" FieldLabel="岗位" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:ComboBox>
                                                <ext:TextField ID="add_Attendance4" runat="server" FieldLabel="出勤" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25" />
                                                <ext:TextField ID="add_Remark4" runat="server" FieldLabel="备注" LabelAlign="Right" ColumnWidth=".25" />
                                            </Items>
                                        </ext:Panel>

                                        <ext:Panel ID="add_Panel5" runat="server" Layout="ColumnLayout" Padding="10" Hidden="true" Enabled="false">
                                            <Items>
                                                <ext:TextField ID="add_WorkBarcode5" Editable="false" runat="server" FieldLabel="员工编号" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:TextField>
                                                <ext:ComboBox ID="add_WorkID5" Editable="false" runat="server" FieldLabel="岗位" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:ComboBox>
                                                <ext:TextField ID="add_Attendance5" runat="server" FieldLabel="出勤" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25" />
                                                <ext:TextField ID="add_Remark5" runat="server" FieldLabel="备注" LabelAlign="Right" ColumnWidth=".25" />
                                            </Items>
                                        </ext:Panel>

                                        <ext:Panel ID="add_Panel6" runat="server" Layout="ColumnLayout" Padding="10" Hidden="true" Enabled="false">
                                            <Items>
                                                <ext:TextField ID="add_WorkBarcode6" Editable="false" runat="server" FieldLabel="员工编号" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:TextField>
                                                <ext:ComboBox ID="add_WorkID6" Editable="false" runat="server" FieldLabel="岗位" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:ComboBox>
                                                <ext:TextField ID="add_Attendance6" runat="server" FieldLabel="出勤" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25" />
                                                <ext:TextField ID="add_Remark6" runat="server" FieldLabel="备注" LabelAlign="Right" ColumnWidth=".25" />
                                            </Items>
                                        </ext:Panel>

                                        <ext:Panel ID="add_Panel7" runat="server" Layout="ColumnLayout" Padding="10" Hidden="true" Enabled="false">
                                            <Items>
                                                <ext:TextField ID="add_WorkBarcode7" Editable="false" runat="server" FieldLabel="员工编号" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:TextField>
                                                <ext:ComboBox ID="add_WorkID7" Editable="false" runat="server" FieldLabel="岗位" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:ComboBox>
                                                <ext:TextField ID="add_Attendance7" runat="server" FieldLabel="出勤" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25" />
                                                <ext:TextField ID="add_Remark7" runat="server" FieldLabel="备注" LabelAlign="Right" ColumnWidth=".25" />
                                            </Items>
                                        </ext:Panel>
                                          <ext:Panel ID="add_Panel8" runat="server" Layout="ColumnLayout" Padding="10" Hidden="true" Enabled="false">
                                            <Items>
                                                <ext:TextField ID="add_WorkBarcode8" Editable="false" runat="server" FieldLabel="员工编号" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:TextField>
                                                <ext:ComboBox ID="add_WorkID8" Editable="false" runat="server" FieldLabel="岗位" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:ComboBox>
                                                <ext:TextField ID="add_Attendance8" runat="server" FieldLabel="出勤" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25" />
                                                <ext:TextField ID="add_Remark8" runat="server" FieldLabel="备注" LabelAlign="Right" ColumnWidth=".25" />
                                            </Items>
                                        </ext:Panel>
                                          <ext:Panel ID="add_Panel9" runat="server" Layout="ColumnLayout" Padding="10" Hidden="true" Enabled="false">
                                            <Items>
                                                <ext:TextField ID="add_WorkBarcode9" Editable="false" runat="server" FieldLabel="员工编号" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:TextField>
                                                <ext:ComboBox ID="add_WorkID9" Editable="false" runat="server" FieldLabel="岗位" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:ComboBox>
                                                <ext:TextField ID="add_Attendance9" runat="server" FieldLabel="出勤" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25" />
                                                <ext:TextField ID="add_Remark9" runat="server" FieldLabel="备注" LabelAlign="Right" ColumnWidth=".25" />
                                            </Items>
                                        </ext:Panel>
                                         <ext:Panel ID="add_Panel10" runat="server" Layout="ColumnLayout" Padding="10" Hidden="true" Enabled="false">
                                            <Items>
                                                <ext:TextField ID="add_WorkBarcode10" Editable="false" runat="server" FieldLabel="员工编号" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:TextField>
                                                <ext:ComboBox ID="add_WorkID10" Editable="false" runat="server" FieldLabel="岗位" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25"></ext:ComboBox>
                                                <ext:TextField ID="add_Attendance10" runat="server" FieldLabel="出勤" LabelAlign="Right" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" ColumnWidth=".25" />
                                                <ext:TextField ID="add_Remark10" runat="server" FieldLabel="备注" LabelAlign="Right" ColumnWidth=".25" />
                                            </Items>
                                        </ext:Panel>
                                    </Items>

                                </ext:Panel>
                            </Items>
                            <%-- <Listeners>
                                <ValidityChange Handler="#{btnAddSave}.setDisabled(!valid);" />
                            </Listeners>--%>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept" Disabled="false">
                            <DirectEvents>
                                <Click OnEvent="BtnAddSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="BtnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Hidden ID="hidden_work_name" runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_delete_flag" runat="server" Text="0"></ext:Hidden>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
