﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SparePartStore.aspx.cs" Inherits="Manager_Equipment_SapSparePart_SparePartStore" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>备件库存管理</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell {
            background-color: #B0FFBA !important;
        }
        .x-grid-row-error .x-grid-cell {
            background-color: #FFC0CB !important;
        }
        
    </style>
    <script type="text/javascript">
        //根据单据不同显示不同的颜色
        var SetStoreTypeRowClass = function (record, rowIndex, rowParams, store) {
            var tempStr = record.get("ReceiveNo");
            if (tempStr.indexOf('RK') < 0) {//出库
                return "x-grid-row-error";
            } else {//入库
                return "x-grid-row-collapsed";
            }
        }
        var recipeTypeDisplay = function (si, item)
        {
            var tempStr = si;
            if (tempStr.indexOf('RK') < 0) {//出库
                return "出库";
            } else {//入库
                return "入库";
            }
        }

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

        //区分删除操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
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
    </script>
     <script type="text/javascript">
         //-------备件代码-----查询带回弹出框--BEGIN
         var Manager_BasicInfo_CommonPage_QueryEqmSparePart_Request = function (record) {//备件代码返回值处理
             if (!App.winAdd.hidden) {
                 App.add_sparepart_code.setValue(record.data.SparePartName);
                 App.add_sparepart_code.getTrigger(0).show();
                 App.hidden_sparepart_code.setValue(record.data.SparePartCode);
             }
             else if (!App.winModify.hidden) {
                 App.modify_sparepart_code.setValue(record.data.SparePartName);
                 App.modify_sparepart_code.getTrigger(0).show();
                 App.hidden_sparepart_code.setValue(record.data.SparePartCode);
             } else {
                 App.txt_sparepart_code.setValue(record.data.SparePartName);
                 App.txt_sparepart_code.getTrigger(0).show();
                 App.hidden_select_sparepart_code.setValue(record.data.SparePartCode);
             }
         }

         var SelectSparePartInfo = function (field, trigger, index) {
             switch (index) {
                 case 0:
                     field.getTrigger(0).hide();
                     field.setValue('');
                     App.hidden_sparepart_code.setValue("");
                     App.hidden_select_sparepart_code.setValue("");
                     field.getEl().down('input.x-form-text').setStyle('background', "white");
                     break;
                 case 1:
                     App.Manager_BasicInfo_CommonPage_QueryEqmSparePart_Window.show();
                     break;
             }
         }

         Ext.create("Ext.window.Window", {//备件代码查询带回窗体
             id: "Manager_BasicInfo_CommonPage_QueryEqmSparePart_Window",
             height: 460,
             hidden: true,
             width: 360,
             html: "<iframe src='../../BasicInfo/CommonPage/QueryEqmSparePart.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
             bodyStyle: "background-color: #fff;",
             closable: true,
             title: "请选择备件名称",
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
                <ext:Panel ID="pnlUnitTitle" runat="server" Region="North" AutoHeight="true" Collapsible="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUnit">
                            <Items>
                                <ext:Button runat="server" Icon="Add" Text="添加" ID="btn_add" Hidden="true">
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
                                        <ext:Container ID="container_2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                            Padding="5">
                                            <Items>
                                                <ext:TriggerField ID="txt_sparepart_code" runat="server" FieldLabel="备件名称" LabelAlign="Right" Editable="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectSparePartInfo" />
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
                <ext:GridPanel ID="pnlList" runat="server" Region="Center" >
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
                                        <ext:ModelField Name="SparePartName" />
                                        <ext:ModelField Name="MajorType" />
                                        <ext:ModelField Name="MinorType" />
                                        <ext:ModelField Name="Standards" />
                                        <ext:ModelField Name="CurrentStoreNum" />
                                        <ext:ModelField Name="MaxStoreNum" />
                                        <ext:ModelField Name="MinStoreNum" />
                                        <ext:ModelField Name="PosStoragePlaceID" />
                                        <ext:ModelField Name="UseStoragePlaceID" />
                                        <ext:ModelField Name="Remark" />
                                        <ext:ModelField Name="DeleteFlag" />
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
                            <ext:Column ID="obj_id" runat="server" Text="编号" DataIndex="ObjID" Width="100"  Hidden="true" />
                            <ext:Column ID="sparepart_code" runat="server" Text="备件编号" DataIndex="SparePartCode" Width="100"  />
                            <ext:Column ID="sparepart_name" runat="server" Text="备件名称" DataIndex="SparePartName" Width="120"  />
                            <ext:Column ID="major_type" runat="server" Text="备件大类" DataIndex="MajorType" Width="100"  />
                            <ext:Column ID="minor_type" runat="server" Text="备件小类" DataIndex="MinorType" Width="100"  />
                            <ext:Column ID="standards" runat="server" Text="型号" DataIndex="Standards" Width="120"  />
                            <ext:Column ID="current_store_num" runat="server" Text="现有库存" DataIndex="CurrentStoreNum" Width="70"  />
                            <ext:Column ID="max_store_num" runat="server" Text="最大库存" DataIndex="MaxStoreNum" Width="70"  />
                            <ext:Column ID="min_store_num" runat="server" Text="最小库存" DataIndex="MinStoreNum" Width="70"  />
                            <ext:Column ID="pos_storage_place_id" runat="server" Text="存放位置" DataIndex="PosStoragePlaceID" Width="100"  />
                            <ext:Column ID="use_storage_place_id" runat="server" Text="使用位置" DataIndex="UseStoragePlaceID" Width="100"  />
                            <ext:Column ID="delete_flag" runat="server" Text="删除标志" DataIndex="DeleteFlag" Width="150" Hidden="true"  />
                            <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Width="150"  />
                            <ext:CommandColumn ID="commandCol" runat="server" Width="60" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                        <ToolTip Text="修改本条数据" />
                                    </ext:GridCommand>
                                </Commands>
                                <Listeners>
                                    <Command Handler="return commandcolumn_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                            <Listeners>
                                <Select Handler="#{storeDetail}.reload();" Buffer="250" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <View>
                        <ext:GridView ID="gvRows" runat="server">
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
                 <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="备件调拨明细" Height="250" Icon="Basket" Layout="Fit" Collapsible="true"
                    Split="true" MarginsSummary="0 5 5 5">
                    <Items>
                        <ext:GridPanel ID="pnlDetailList" runat="server" MarginsSummary="0 5 5 5">
                            <Store>
                                <ext:Store ID="storeDetail" runat="server" PageSize="10" OnReadData="RowSelect">
                                    <Model>
                                        <ext:Model ID="modelDetail" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ReceiveNo" />
                                                <ext:ModelField Name="ReceiveDate" />
                                                <ext:ModelField Name="SparePartCode" />
                                                <ext:ModelField Name="SparePartName" />
                                                <ext:ModelField Name="SparePartModel" />
                                                <ext:ModelField Name="StoreInNum" />
                                                <ext:ModelField Name="UserName" />
                                                <ext:ModelField Name="RecordDate" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Parameters>
                                        <ext:StoreParameter Name="SparePartCode" Mode="Raw" Value="#{pnlList}.getSelectionModel().hasSelection() ? #{pnlList}.getSelectionModel().getSelection()[0].data.SparePartCode : -1" />
                                    </Parameters>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModelDetail" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />
                                    <ext:Column ID="ReceiveNo" runat="server" Text="单号" DataIndex="ReceiveNo" Width="100" />
                                    <ext:Column ID="ReceiveType" runat="server" Text="种类" DataIndex="ReceiveNo" Width="100" >
                                        <Renderer Fn="recipeTypeDisplay" />
                                    </ext:Column>
                                    <ext:DateColumn Format="yyyy-MM-dd" ID="ReceiveDate" runat="server" Text="创建时间" DataIndex="ReceiveDate" Width="100" />
                                    <ext:Column ID="SparePartCode" runat="server" Text="备件编号" DataIndex="SparePartCode" Width="100" />
                                    <ext:Column ID="SparePartName" runat="server" Text="备件名称" DataIndex="SparePartName" Width="95" />
                                    <ext:Column ID="SparePartModel" runat="server" Text="型号" DataIndex="SparePartModel" Width="100" />
                                    <ext:Column ID="StoreInNum" runat="server" Text="操作数量" DataIndex="StoreInNum" Width="100" />
                                    <ext:Column ID="UserName" runat="server" Text="接收人|领取人" DataIndex="UserName" Width="100" />
                                    <ext:DateColumn Format="yyyy-MM-dd" ID="RecordDate" runat="server" Text="记录时间" DataIndex="RecordDate" Width="100" />
                                </Columns>
                            </ColumnModel>
                            <View>
                                <ext:GridView ID="GridView1" runat="server">
                                    <GetRowClass Fn="SetStoreTypeRowClass" />
                                </ext:GridView>
                            </View>
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
                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改备件出库信息"
                    Width="500" Height="250" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                               <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="Container3" runat="server" Layout="FormLayout" ColumnWidth=".5" Padding="5">
                                            <Items>
                                                <ext:TextField ID="modify_obj_id" runat="server" FieldLabel="编号" LabelAlign="Left" Hidden="true"/>
                                                <ext:TextField ID="modify_spare_part_code" runat="server" FieldLabel="备件名称" LabelAlign="Left" ReadOnly="true"/>
                                                <ext:TextField ID="modify_major_type" runat="server" FieldLabel="备件大类" LabelAlign="Left" ReadOnly="true"/>
                                                <ext:TextField ID="modify_minor_type" runat="server" FieldLabel="备件小类" LabelAlign="Left" ReadOnly="true"/>
                                                <ext:TextField ID="modify_pos_storage_place_id" runat="server" FieldLabel="存放位置" LabelAlign="Left" />
                                                <ext:TextField ID="modify_use_storage_place_id" runat="server" FieldLabel="使用位置" LabelAlign="Left"/>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container4" runat="server" Layout="FormLayout" ColumnWidth=".5" Padding="5">
                                            <Items>
                                                <ext:TextField ID="modify_standards" runat="server" FieldLabel="型号" MinValue="1" LabelAlign="Left"/>
                                                <ext:NumberField ID="modify_current_store_num" runat="server" FieldLabel="现有库存" MinValue="0" LabelAlign="Left" ReadOnly="true"/>
                                                <ext:NumberField ID="modify_max_store_num" runat="server" FieldLabel="最大库存" MinValue="1" LabelAlign="Left"/>
                                                <ext:NumberField ID="modify_min_store_num" runat="server" FieldLabel="最小库存" MinValue="0" LabelAlign="Left"/>
                                                <ext:TextField ID="modify_remark" runat="server" FieldLabel="备注" LabelAlign="Left"/>
                                            </Items>
                                        </ext:Container>
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
                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加备件入库"
                    Width="500" Height="250" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="Container5" runat="server" Layout="FormLayout" ColumnWidth=".5" Padding="5">
                                            <Items>
                                                <ext:TextField ID="add_obj_id" runat="server" FieldLabel="编号" LabelAlign="Left" Hidden="true"/>
                                                <ext:TriggerField ID="add_sparepart_code" runat="server" FieldLabel="备件名称" LabelAlign="Left" AllowBlank="false" >
                                                    <Triggers>
                                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                        <ext:FieldTrigger Icon="Search" />
                                                    </Triggers>
                                                    <Listeners>
                                                        <TriggerClick Fn="SelectSparePartInfo" />
                                                    </Listeners>
                                                </ext:TriggerField>
                                                <ext:TextField ID="add_pos_storage_place_id" runat="server" FieldLabel="存放位置" LabelAlign="Left"/>
                                                <ext:TextField ID="add_use_storage_place_id" runat="server" FieldLabel="使用位置" LabelAlign="Left"/>
                                                <ext:TextField ID="add_remark" runat="server" FieldLabel="备注" LabelAlign="Left"/>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container6" runat="server" Layout="FormLayout" ColumnWidth=".5" Padding="5">
                                            <Items>
                                                <ext:TextField ID="add_standards" runat="server" FieldLabel="型号" MinValue="1" LabelAlign="Left"/>
                                                <ext:NumberField ID="add_current_store_num" runat="server" FieldLabel="现有库存" MinValue="0" LabelAlign="Left" AllowBlank="false"/>
                                                <ext:NumberField ID="add_max_store_num" runat="server" FieldLabel="最大库存" MinValue="1" LabelAlign="Left" AllowBlank="false"/>
                                                <ext:NumberField ID="add_min_store_num" runat="server" FieldLabel="最小库存" MinValue="0" LabelAlign="Left" AllowBlank="false"/>
                                            </Items>
                                        </ext:Container>
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
                <ext:Hidden ID="hidden_sparepart_code"  runat="server" />
                <ext:Hidden ID="hidden_delete_flag"  runat="server" Text="0"></ext:Hidden>
                <ext:Hidden ID="hidden_select_sparepart_code"  runat="server"></ext:Hidden>
            </Items>
        </ext:Viewport>
        </form>
</body>
</html>