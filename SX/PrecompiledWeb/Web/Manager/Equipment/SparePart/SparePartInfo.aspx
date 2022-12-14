﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_Equipment_SparePart_SparePartInfo, App_Web_may0vc4q" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>备件信息</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
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
                    return;
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

        //树形结构点击刷新右侧方法
        var loadPage = function (record) {
            if (record.getDepth() == 2) {
                App.hidden_major_type_id.setValue(record.getId().split('|')[0]);
                App.hidden_minor_type_id.setValue(record.getId().split('|')[1]);
            }
            if (record.getDepth() == 1) {
                App.hidden_major_type_id.setValue(record.getId());
                App.hidden_minor_type_id.setValue("");
            }
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
        };
    </script>
    <script type="text/javascript">
        //-------单位-----查询带回弹出框--BEGIN
        var Manager_BasicInfo_CommonPage_QueryUnit_Request = function (record) {//单位返回值处理
            var type = $("#hidden_select_unit").val();
            if (type == "Unit") {
                if (!App.winAdd.hidden) {
                    App.add_unit_code.setValue(record.data.UnitName);
                    App.hidden_unit_code.setValue(record.data.ObjID);
                }
                else if (!App.winModify.hidden) {
                    App.modify_unit_code.setValue(record.data.UnitName);
                    App.hidden_unit_code.setValue(record.data.ObjID);
                }
            }
            if (type == "MinorUnit") {
                if (!App.winAdd.hidden) {
                    App.add_minor_unit_code.setValue(record.data.UnitName);
                    App.hidden_minor_unit_code.setValue(record.data.ObjID);
                }
                else if (!App.winModify.hidden) {
                    App.modify_minor_unit_code.setValue(record.data.UnitName);
                    App.hidden_minor_unit_code.setValue(record.data.ObjID);
                }
            }
        }

        var SelectUnitID = function () {
            App.hidden_select_unit.setValue("Unit");
            App.Manager_BasicInfo_CommonPage_QueryUnit_Window.show();
        }
        var SelectMinorUnitID = function () {
            App.hidden_select_unit.setValue("MinorUnit");
            App.Manager_BasicInfo_CommonPage_QueryUnit_Window.show();
        }

        Ext.create("Ext.window.Window", {//厂商类别查询带回窗体
            id: "Manager_BasicInfo_CommonPage_QueryUnit_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryUnit.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择单位",
            modal: true
        })
        //------------查询带回弹出框--END 
    </script>
</head>
<body>
    <form id="fmUnit" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
        <ext:ResourceManager ID="rmUnit" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Region="West" Width="200" Layout="BorderLayout" Title="备件分类" Collapsible="true">
                    <Items>
                        <ext:TreePanel ID="treeDept" runat="server"  Region="West" Icon="FolderGo"
                            AutoHeight="true" RootVisible="false" >
                            <Store>
                                <ext:TreeStore ID="treeDeptStore" runat="server">
                                    <Proxy>
                                        <ext:PageProxy>
                                            <RequestConfig Method="GET" Type="Load" />
                                        </ext:PageProxy>
                                    </Proxy>
                                    <Root>
                                        <ext:Node NodeID="root" Expanded="true" />
                                    </Root>
                                </ext:TreeStore>
                            </Store>
                            <Listeners>
                                <ItemClick Handler="loadPage(record)" />
                            </Listeners>
                        </ext:TreePanel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUnit">
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
                                 <ext:ToolbarSeparator ID="toolbarSeparator_middle_1" />
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
                        <ext:Panel ID="pnlUnitQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_sparepart_code" Vtype="integer" runat="server" FieldLabel="备件编号" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_sparepare_name" runat="server" FieldLabel="备件名称" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container_3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TextField ID="txt_remark" runat="server" FieldLabel="备注" LabelAlign="Right" />
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
                                        <ext:ModelField Name="SparePartCode" />
                                        <ext:ModelField Name="SparePartMainType"  />
                                        <ext:ModelField Name="SparePartDetailType" />
                                        <ext:ModelField Name="SparePartName" />
                                        <ext:ModelField Name="SparePartOtherName" />
                                        <ext:ModelField Name="SparePartSimpleName" />
                                        <ext:ModelField Name="SparePartStandards" />
                                        <ext:ModelField Name="UnitCode" />
                                        <ext:ModelField Name="MinorUnitCode" />
                                        <ext:ModelField Name="Price" />
                                        <ext:ModelField Name="SAPCode" />
                                        <ext:ModelField Name="DefineDate" />
                                        <ext:ModelField Name="DeleteFlag" />
                                        <ext:ModelField Name="Remark" />
                                        <ext:ModelField Name="Ext_1" />
                                        <ext:ModelField Name="Ext_2" />
                                        <ext:ModelField Name="Ext_3" />
                                        <ext:ModelField Name="Ext_4" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Sorters>
                                <ext:DataSorter Property="ObjID" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                            <ext:Column ID="obj_id" runat="server" Text="自增序列" DataIndex="ObjID" Width="150" Hidden="true"  />
                            <ext:Column ID="spare_part_code" runat="server" Text="备件编号" DataIndex="SparePartCode" Width="100"  />
                            <ext:Column ID="spare_part_main_type" runat="server" Text="备件大类" DataIndex="SparePartMainType" Width="80"  />
                            <ext:Column ID="spare_part_minor_type" runat="server" Text="备件细类" DataIndex="SparePartDetailType" Width="80"  />
                            <ext:Column ID="spare_part_name" runat="server" Text="备件名称" DataIndex="SparePartName" Width="80"  />
                            <ext:Column ID="spare_part_other_name" runat="server" Text="备件别名" DataIndex="SparePartOtherName" Width="80"  />
                            <ext:Column ID="spare_part_simple_name" runat="server" Text="备件简称" DataIndex="SparePartSimpleName" Width="80"  />
                            <ext:Column ID="spare_part_standards" runat="server" Text="规格" DataIndex="SparePartStandards" Width="80"  />
                            <ext:Column ID="unit_code" runat="server" Text="单位" DataIndex="UnitCode" Width="80"  />
                            <ext:Column ID="minor_unit_code" runat="server" Text="小单位" DataIndex="MinorUnitCode" Width="60"  />
                            <ext:Column ID="price" runat="server" Text="单价" DataIndex="Price" Width="60"  />
                            <ext:Column ID="sap_code" runat="server" Text="SAP编号" DataIndex="SAPCode" Width="80"  />
                            <ext:DateColumn Format="yyyy-MM-dd" ID="define_date" runat="server" Text="定义日期" DataIndex="DefineDate" Width="80"  />
                            <ext:Column ID="delete_flag" runat="server" Text="删除标志" DataIndex="DeleteFlag" Width="80"  Hidden="true" />
                            <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Width="80"  />
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
                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改单位信息"
                    Width="600" Height="330" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                <ext:Container ID="Container7" runat="server" AutoHeight="true">
                                    <Items>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Title="基本信息" Layout="AnchorLayout" DefaultAnchor="100%" Padding="5">
                                            <Items>
                                                <ext:Container ID="Container9" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                    <Items>
                                                        <ext:TextField ID="modify_obj_id" runat="server" FieldLabel="自增序列" LabelAlign="Right" Flex="1" Editable="false" Hidden="true" />
                                                        <ext:TextField ID="modify_spare_part_code" runat="server" FieldLabel="备件编号" LabelAlign="Right" Flex="1"  ReadOnly="true" />
                                                        <ext:TextField ID="modify_spare_part_major_type" runat="server" FieldLabel="备件大类" LabelAlign="Right" Flex="1" ReadOnly="true" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container10" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                    <Items>
                                                        <ext:TextField ID="modify_spare_part_name" runat="server" FieldLabel="备件名称" LabelAlign="Right" Flex="1" AllowBlank="false" />
                                                        <ext:TextField ID="modify_spare_part_minor_type" runat="server" FieldLabel="备件细类" LabelAlign="Right" Flex="1" ReadOnly="true" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container11" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                    <Items>
                                                        <ext:TextField ID="modify_spare_part_other_name" runat="server" FieldLabel="备件别名" LabelAlign="Right" Flex="1" />
                                                        <ext:TextField ID="modify_spare_part_simple_name" runat="server" FieldLabel="备件简称" LabelAlign="Right" Flex="1" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container12" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                    <Items>
                                                        <ext:TextField ID="modify_spare_part_standards" runat="server" FieldLabel="规格" LabelAlign="Right" Flex="1" />
                                                        <ext:NumberField ID="modify_price" runat="server" FieldLabel="单价" LabelAlign="Right" Flex="1" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container13" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                    <Items>
                                                        <ext:TriggerField ID="modify_unit_code" runat="server" FieldLabel="单位" LabelAlign="Right" Flex="1" Editable="false" AllowBlank="false" >
                                                             <Triggers>
                                                                <ext:FieldTrigger Icon="Search" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <TriggerClick Fn="SelectUnitID" />
                                                            </Listeners>
                                                        </ext:TriggerField>
                                                        <ext:TriggerField ID="modify_minor_unit_code" runat="server" FieldLabel="小单位" LabelAlign="Right" Flex="1" Editable="false" >
                                                             <Triggers>
                                                                <ext:FieldTrigger Icon="Search" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <TriggerClick Fn="SelectMinorUnitID" />
                                                            </Listeners>
                                                        </ext:TriggerField>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container14" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                    <Items>
                                                        <ext:TextField ID="modify_sap_code" runat="server" FieldLabel="SAP编号" LabelAlign="Right" Flex="1" />
                                                        <ext:TextField ID="modify_remark" runat="server" FieldLabel="备注" LabelAlign="Right" Width="271" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                    </Items>
                                </ext:Container>
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
                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加单位信息"
                    Width="600" Height="330" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                <ext:Container ID="Container4" runat="server" AutoHeight="true">
                                    <Items>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Title="基本信息" Layout="AnchorLayout" DefaultAnchor="100%" Padding="5">
                                            <Items>
                                                <ext:Container ID="Container8" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                    <Items>
                                                        <ext:TextField ID="add_spare_part_major_type" runat="server" FieldLabel="备件大类" LabelAlign="Right" Flex="1" ReadOnly="true" />
                                                        <ext:TextField ID="add_spare_part_minor_type" runat="server" FieldLabel="备件细类" LabelAlign="Right" Flex="1" ReadOnly="true" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container2" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                    <Items>
                                                        <ext:TextField ID="add_spare_part_name" runat="server" FieldLabel="备件名称" LabelAlign="Right" Flex="1" AllowBlank="false" />
                                                        <ext:TextField ID="add_spare_part_other_name" runat="server" FieldLabel="备件别名" LabelAlign="Right" Flex="1" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container3" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                    <Items>
                                                        <ext:TextField ID="add_spare_part_simple_name" runat="server" FieldLabel="备件简称" LabelAlign="Right" Flex="1" />
                                                        <ext:TextField ID="add_spare_part_standards" runat="server" FieldLabel="规格" LabelAlign="Right" Flex="1" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container5" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                    <Items>
                                                        <ext:TriggerField ID="add_unit_code" runat="server" FieldLabel="单位" LabelAlign="Right" Flex="1" Editable="false" AllowBlank="false" >
                                                             <Triggers>
                                                                <ext:FieldTrigger Icon="Search" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <TriggerClick Fn="SelectUnitID" />
                                                            </Listeners>
                                                        </ext:TriggerField>
                                                        <ext:TriggerField ID="add_minor_unit_code" runat="server" FieldLabel="小单位" LabelAlign="Right" Flex="1" Editable="false" >
                                                             <Triggers>
                                                                <ext:FieldTrigger Icon="Search" />
                                                            </Triggers>
                                                            <Listeners>
                                                                <TriggerClick Fn="SelectMinorUnitID" />
                                                            </Listeners>
                                                        </ext:TriggerField>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container1" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                    <Items>
                                                        <ext:NumberField ID="add_price" runat="server" FieldLabel="单价" LabelAlign="Right" Flex="1" />
                                                        <ext:TextField ID="add_sap_code" runat="server" FieldLabel="SAP编号" LabelAlign="Right" Flex="1" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container6" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                                    <Items>
                                                        <ext:TextField ID="add_remark" runat="server" FieldLabel="备注" LabelAlign="Right" Width="271" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                    </Items>
                                </ext:Container>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnAddSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                     <Buttons>
                        <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
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
                <ext:Hidden ID="hidden_modify_spare_part_name"  runat="server"></ext:Hidden>
                <ext:Hidden ID="hidden_delete_flag"  runat="server" Text="0"></ext:Hidden>
                <ext:Hidden Hidden="true" ID="hidden_major_type_id" runat="server" />
                <ext:Hidden Hidden="true" ID="hidden_minor_type_id" runat="server" />
                <ext:Hidden Hidden="true" ID="hidden_unit_code" runat="server" />
                <ext:Hidden Hidden="true" ID="hidden_minor_unit_code" runat="server" />
                <ext:Hidden Hidden="true" ID="hidden_select_unit" runat="server" />
            </Items>
        </ext:Viewport>
        </form>
</body>
</html>
