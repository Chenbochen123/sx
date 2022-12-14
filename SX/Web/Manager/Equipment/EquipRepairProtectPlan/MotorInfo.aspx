﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MotorInfo.aspx.cs" Inherits="Manager_Equipment_EquipRepairProtectPlan_MotorInfo" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>电机信息管理</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
     <style type="text/css">
        .x-grid-body .x-grid-cell-Cost
        {
            background-color: #f1f2f4;
        }
        
        .x-grid-row-summary .x-grid-cell-Cost .x-grid-cell-inner
        {
            background-color: #e1e2e4;
        }
        
        .task .x-grid-cell-inner
        {
            padding-left: 15px;
        }
        
        .x-grid-row-summary .x-grid-cell-inner
        {
            font-weight: bold;
            font-size: 11px;
            background-color: #f1f2f4;
        }
    </style>
    <script type="text/javascript">
        var prepareGroupToolbar = function (grid, toolbar, groupId, records) {
            // you can prepare ready toolbar
        };


        var getAdditionalData = function (data, idx, record, orig) {
            var o = Ext.grid.feature.RowBody.prototype.getAdditionalData.apply(this, arguments),
                d = data;

            Ext.apply(o, {
                rowBodyColspan: record.fields.getCount(),
                rowBody: Ext.String.format('<div style=\'padding:0 5px 5px 5px;\'>The {0} [{1}] requires light conditions of <i>{2}</i>.<br /><b>Price: {3}</b></div>', d.Common, d.Botanical, d.Light, Ext.util.Format.usMoney(d.Price)),
                rowBodyCls: ""
            });

            return o;
        };


        //下次维修日期报警
        var validDateChange = function (value, metadata, record, rowIndex, colIndex, store, view) {
            if (value != null && value != "") {
                if (parseInt((new Date(value) - new Date()) / 1000 / 60 / 60 / 24) < 0) {

                    return Ext.String.format('<div style="color:red;font-weight:bolder;" title="最后维修日期已超期，请联系处理！">{0}</div>', value);
                }
                else {
                    return Ext.String.format('<div style="color:black;font-weight:bolder;" title="">{0}</div>', value);
                }
            }
        }

    </script>
    
    <script type="text/javascript">


        //点击修改按钮
        var cmdcol_click = function (command, record) {
            if (command == "Delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此信息？删除后将不能恢复！', function (btn) { deleteStopRecord(btn, record) });
            }
            else if (command == "Save") {
                App.direct.pnlList_Add(record.data.INo, record.data.depName,
                    record.data.processName, record.data.workshop, record.data.equipName,
                    record.data.EquipPart, record.data.motorType, record.data.power,
                    record.data.voltage, record.data.motorNo, record.data.Cbtype,
                    record.data.DianShu, record.data.LiCi, record.data.Cycle,
                    record.data.checkstand, record.data.startDate, record.data.lastDate,
                    record.data.Memo, record.data.beiyong,
                    {
                        success: function () { },
                        failure: function () { }
                    });
            }
            else if (command == "detail") {
                commandcolumn_direct_detail(record);
            }
        }
            var commandcolumn_direct_detail = function (record) {
                var ObjID = record.data.motorNo;

                var url = "../Manager/Equipment/EquipRepairProtectPlan/MotorPlan.aspx?motorNo=" + ObjID;
                var tabid = "Manager_Equipment_EquipRepairProtectPlan_MotorPlan";
                var tabp = parent.App.mainTabPanel;
                var tab = tabp.getComponent(tabid);

                if (tab) {
                    tab.close();
                }
                var title;
                if (record != null)
                    title = "检测记录";
                else
                    title = "电机信息";
                parent.addTab(tabid, title, url, true);

                parent.refresh("");
                //App.direct.commandcolumn_direct_detail(ObjID, {
                //    success: function (result) {
                //    },
                //    failure: function (errorMsg) {
                //        Ext.Msg.alert('操作', errorMsg);
                //    }
                //});
            }

            var deleteStopRecord = function (btn, record) {
                if (btn != "yes") {
                    return;
                }
                var INo = record.data.INo;
                App.direct.pnlList_Delete(INo, {
                    success: function () { },
                    failure: function () { }
                });
            }


            var addEmployee = function () {
                var grid = App.pnlList,
                    store = grid.getStore();

                grid.editingPlugin.cancelEdit();

                //store.getSorters().removeAll(); // We have to remove sorting to avoid auto-sorting on insert
                grid.getView().headerCt.setSortState(); // To update columns sort UI

                store.insert(0, {
                    INo: '0',
                    depName: '',
                    processName: '',
                    workshop: '',
                    equipName: '',
                    EquipPart: '',
                    motorType: '',
                    power: '',
                    voltage: '',
                    motorNo: '',
                    Cbtype: '',
                    DianShu: '',
                    LiCi: '',
                    Cycle: '',
                    checkstand: '',
                    startDate: Ext.Date.clearTime(new Date()),
                    lastDate: Ext.Date.clearTime(new Date()),
                    Memo: '',
                    beiyong: ''
                });

                grid.editingPlugin.startEdit(0, 0);
            };


            var removeEmployee = function () {
                var grid = App.pnlList,
                    sm = grid.getSelectionModel(),
                    store = grid.getStore();

                grid.editingPlugin.cancelEdit();
                store.remove(sm.getSelection());

                if (store.getCount() > 0) {
                    sm.select(0);
                }
            };
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" runat="server" Text="Button" OnClick="btnExportSubmit_Click" Style="display:none" />
      <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
        <Items>
            
                <ext:Panel ID="pnlStopType" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barStopType">
                            <Items>
                                <ext:Button runat="server" Text="新建" Icon="Add" ID="btnAdd">
                                    <Listeners>
                                        <Click Fn="addEmployee" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button
                                    ID="btnRemove"
                                    runat="server"
                                    Text="取消"
                                    Icon="ControlRemove"
                                    Disabled="true">
                                    <Listeners>
                                        <Click Fn="removeEmployee" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips><ext:ToolTip ID="ToolTip2" runat="server" Html="点击进行查询" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page"></EventMask>
                                    </Click></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="FolderConnect" Text="生成检修记录" ID="btnCreatePlan">
                                    <ToolTips><ext:ToolTip ID="ToolTip5" runat="server" Html="点击生成记录" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnCreatePlan_Click"/></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                    <ToolTips><ext:ToolTip ID="ToolTip3" runat="server" Html="点击进行结果导出" /></ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                </ext:Panel>
            <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="50">
                            <Sorters>
                                <ext:DataSorter Property="INo" />
                            </Sorters>
                            <Model>
                                <ext:Model ID="model" runat="server" IDProperty="INo">
                                    <Fields>
                                        <ext:ModelField Name="INo" Type="Int" />
                                        <ext:ModelField Name="depName" />
                                        <ext:ModelField Name="processName" />
                                        <ext:ModelField Name="workshop" />
                                        <ext:ModelField Name="equipName" />
                                        <ext:ModelField Name="EquipPart" />
                                        <ext:ModelField Name="motorType" />
                                        <ext:ModelField Name="power"  Type="Float" />
                                        <ext:ModelField Name="voltage" Type="Float"  />
                                        <ext:ModelField Name="motorNo"/>
                                        <ext:ModelField Name="Cbtype" />
                                        <ext:ModelField Name="DianShu" Type="Float"/>
                                        <ext:ModelField Name="LiCi" Type="Float" />
                                        <ext:ModelField Name="Cycle" type="Int"/>
                                        <ext:ModelField Name="checkstand" />
                                        <ext:ModelField Name="startDate" Type="Date" />
                                        <ext:ModelField Name="lastDate" Type="Date" />
                                        <ext:ModelField Name="Memo" />
                                        <ext:ModelField Name="beiyong" />
                                        <ext:ModelField Name="NEXTDAY" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <Plugins>
                        <ext:CellEditing runat="server" ClicksToMoveEditor="1" AutoCancel="false" />
                    </Plugins>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:Column ID="clserialid" runat="server" Text="序号" DataIndex="INo" Hidden="false" Width="65">
                            </ext:Column>
                            <ext:Column ID="clwork_Type" runat="server" Text="部门名称" DataIndex="depName" Width="80">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column3" runat="server" Text="工段名称" DataIndex="processName" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column4" runat="server" Text="车间名称" DataIndex="workshop" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column5" runat="server" Text="机台名称" DataIndex="equipName" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column8" runat="server" Text="设备部位" DataIndex="EquipPart" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column9" runat="server" Text="电机型号" DataIndex="motorType" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column11" runat="server" Text="功率(kW)" DataIndex="power" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number"/>
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column13" runat="server" Text="额定电压(V)" DataIndex="voltage" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number"/>
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column14" runat="server" Text="电机编号" DataIndex="motorNo" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column15" runat="server" Text="碳刷型号" DataIndex="Cbtype" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column16" runat="server" Text="电枢绝缘(MΩ)" DataIndex="DianShu" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number"/>
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column6" runat="server" Text="励磁绝缘(MΩ)" DataIndex="LiCi" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column10" runat="server" Text="保养周期（月）" DataIndex="Cycle" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column17" runat="server" Text="检测标准" DataIndex="checkstand" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:DateColumn ID="Column7" runat="server" Text="启用日期" DataIndex="startDate" Width="100" Format="yyyy-MM-dd" >
                                <Editor>
                                    <ext:DateField
                                        runat="server"
                                        AllowBlank="false"
                                        Format="yyyy-MM-dd"
                                        MinDate="1999-01-01"
                                        MinText="不能早于1999-01-01"
                                        MaxDate="<%# DateTime.Now %>"
                                        AutoDataBind="true"
                                        />
                                </Editor>
                            </ext:DateColumn>
                            <ext:DateColumn ID="Column12" runat="server" Text="最后保养日期" DataIndex="lastDate" Width="100" Format="yyyy-MM-dd">
                                <Editor>
                                    <ext:DateField
                                        runat="server"
                                        AllowBlank="false"
                                        Format="yyyy-MM-dd"
                                        MinDate="1999-01-01"
                                        MinText="不能早于1999-01-01"
                                        MaxDate="<%# DateTime.Now %>"
                                        AutoDataBind="true"
                                        />
                                </Editor>
                            </ext:DateColumn>
                            <ext:Column ID="txtnextdate" runat="server" Text="下次检测日期" DataIndex="NEXTDAY" Width="100">
                                <Renderer Fn="validDateChange" />
                            </ext:Column>
                            <ext:Column ID="Column1" runat="server" Text="备注" DataIndex="Memo" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column2" runat="server" Text="是否有备用" DataIndex="beiyong" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:CommandColumn ID="cmdCol" runat="server" Align="Center" Text="操作" Width="185">
                                <Commands>
                                    <ext:GridCommand Icon="DatabaseSave" CommandName="Save" Text="保存"/>
                                    <ext:GridCommand Icon="TableDelete" CommandName="Delete" Text="删除"/>
                                    <ext:GridCommand Icon="ApplicationViewDetail" CommandName="detail" Text="检测记录" />
                                </Commands>
                                <Listeners>
                                    <Command Handler="cmdcol_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <Listeners>
                        <SelectionChange Handler="App.btnRemove.setDisabled(!selected.length);" />
                    </Listeners>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                            <Listeners>
                                <Select Handler="#{detailStore}.reload();" Buffer="250" />
                            </Listeners>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolBar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server"/>
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
               
             <ext:Window ID="winDetail" runat="server" Icon="MonitorAdd" Closable="true" Title="电机检测记录" AutoScroll="true" Width="1000" Height="500" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;" BodyPadding="5">
                <Content>
                    <cc1:webreport id="WebReport" runat="server" backcolor="White" font-bold="False" width="99%" height="98%" zoom="1"
                        padding="3, 3, 3, 3" toolbarcolor="Lavender" printinpdf="True" layers="False" />
                </Content>
            </ext:Window>

             <ext:Window ID="winPlanSave" runat="server" Icon="MonitorAdd" Closable="false" Title="生成电机检测记录" Width="550" Height="500" Resizable="false" Modal="true" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="FormLayout">
                    <Items>
                        <ext:FormPanel ID="pnlPlanAdd" runat="server" BodyPadding="5" Layout="FormLayout">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="70" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Hidden runat="server" ID="hidePlanObjID" />
                                <ext:FieldSet runat="server" Title="检测记录" Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer1"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtINo" runat="server" Width="450" FieldLabel="系统编号"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer2"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtMotorNo" runat="server" Width="450" FieldLabel="电机编号"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer17"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:DateField ID="txtRealday" runat="server" FieldLabel="测量日期" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="200"/>
                                                
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer3"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtDianshu" runat="server" Width="450" FieldLabel="电枢绝缘"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer4"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtLici" runat="server" Width="450" FieldLabel="励磁绝缘"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer5"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtman" runat="server" Width="450" FieldLabel="检测负责人"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer6"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:DateField ID="txtoutday" runat="server" FieldLabel="外委出厂日期" Editable="false" AllowBlank="true" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="200"/>
                                                
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer7"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:DateField ID="txtinday" runat="server" FieldLabel="外委入厂日期" Editable="false" AllowBlank="true" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="200"/>
                                                
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer9"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtInfo" runat="server" Width="450" FieldLabel="大修处理内容"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer10"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtFac" runat="server" Width="450" FieldLabel="处理厂家"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer8"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtremark" runat="server" Width="450" FieldLabel="备注"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:FieldSet>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnPlanSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                     <Buttons>
                        <ext:Button ID="btnPlanSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <DirectEvents>
                                <Click OnEvent="btnPlanSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnPlanCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnPlanCancel_Click"/>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:Window>

                <ext:Hidden ID="hidden_equip_code" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hidden_type" runat="server">
                </ext:Hidden>
                </Items>
    </ext:Viewport>
    </form>
</body>
</html>
