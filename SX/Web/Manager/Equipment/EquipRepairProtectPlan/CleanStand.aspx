﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CleanStand.aspx.cs" Inherits="Manager_Equipment_EquipRepairProtectPlan_CleanStand" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设备清洗标准</title>
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

    </script>
    
    <script type="text/javascript">

        //点击修改按钮
        var cmdcol_click = function (command, record) {
            if (command == "Delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此信息？删除后将不能恢复！', function (btn) { deleteStopRecord(btn, record) });
            }
            else if (command == "Save") {
                App.direct.pnlList_Add(record.data.serialid, record.data.workshop, record.data.equipName, record.data.posname, record.data.equipno, record.data.cycle,
                    record.data.before_temp, record.data.after_temp, record.data.Clean_type, record.data.begin_date, record.data.last_date, record.data.Next_date,
                    record.data.memo,
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
            var ObjID = record.data.serialid

            var url = "../Manager/Equipment/EquipRepairProtectPlan/CleanRecord.aspx?serialid=" + ObjID + "";
            var tabid = "Manager_Equipment_EquipRepairProtectPlan_CleanRecord";
            var tabp = parent.App.mainTabPanel;
            var tab = tabp.getComponent(tabid);

            if (tab) {
                tab.close();
            }
            var title;
            if (record != null)
                title = "检测记录";
            else
                title = "设备信息";
            parent.addTab(tabid, title, url, true);
            parent.refresh("");
        }

        var deleteStopRecord = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var serialid = record.data.serialid;
            App.direct.pnlList_Delete(serialid, {
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
                serialid: '',
                workshop: '',
                equipName: '',
                posname: '',
                equipno: '',
                cycle: '',
                before_temp: '',
                after_temp: '',
                Clean_type: '',
                begin_date: Ext.Date.clearTime(new Date()),
                last_date: Ext.Date.clearTime(new Date()),
                Next_date: Ext.Date.clearTime(new Date()),
                memo: '',
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

        var trans = function (date) {
            var year = date.getFullYear();
            var month = (date.getMonth() + 1).toString();
            var day = (date.getDate()).toString();
            if (month.length == 1) {
                month = "0" + month;
            }
            if (day.length == 1) {
                day = "0" + day;
            }
            var dateTime = year + "-" + month + "-" + day;
            return dateTime;
        }

        //下次维修日期报警
        var validDateChange = function (value, metadata, record, rowIndex, colIndex, store, view) {
            if (value != null && value != "") {
                if (parseInt((new Date(value) - new Date()) / 1000 / 60 / 60 / 24) < 0) {
                    debugger;
                    return Ext.String.format('<div style="color:red;font-weight:bolder;" title="最后维修日期已超期，请联系处理！"  >{0}</div>', trans(value));
                }
                else {
                    return Ext.String.format('<div style="color:black;font-weight:bolder;" title="">{0}</div>', trans(value));
                }
            }
        }

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
                   <%-- <Items>
                        <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                        <ext:TextField ID="txtmainid" Vtype="integer" runat="server" FieldLabel="设备序号" LabelAlign="Right" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="cbxType" runat="server" FieldLabel="设备类型" LabelAlign="Right"
                                            Editable="false">
                                            <Items>
                                                <ext:ListItem Text="电梯" Value="2">
                                                </ext:ListItem>
                                                <ext:ListItem Text="压力容器" Value="5">
                                                </ext:ListItem>
                                            </Items>
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" Qtip="清空">
                                                </ext:FieldTrigger>
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Handler="this.setValue('');" />
                                            </Listeners>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>

                            </Items>
                        </ext:FormPanel>
                    </Items>--%>

                </ext:Panel>
            <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                <Store>
                    <ext:Store ID="store" runat="server" PageSize="50">
                        <Sorters>
                            <ext:DataSorter Property="serialid" />
                        </Sorters>
                            <Model>
                                <ext:Model ID="model" runat="server" IDProperty="serialid">
                                    <Fields>
                                        <ext:ModelField Name="serialid" Type="Int" />
                                        <ext:ModelField Name="workshop" />
                                        <ext:ModelField Name="equipName" />
                                        <ext:ModelField Name="posname"/>
                                        <ext:ModelField Name="equipno" />
                                        <ext:ModelField Name="cycle" Type="Int"/>
                                        <ext:ModelField Name="before_temp" Type="float" />
                                        <ext:ModelField Name="after_temp" Type="float"/>
                                        <ext:ModelField Name="Clean_type" />
                                        <ext:ModelField Name="begin_date" type="Date"/>
                                        <ext:ModelField Name="last_date" type="Date" />
                                        <ext:ModelField Name="Next_date" type="Date"/>
                                        <ext:ModelField Name="memo"/>
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
                            <ext:Column ID="Column18" runat="server" Text="序号" DataIndex="serialid" Hidden="true" Width="65">
                            </ext:Column>
                            <ext:Column ID="Column1" runat="server" Text="厂房" DataIndex="workshop" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column3" runat="server" Text="机台" DataIndex="equipName" Width="95">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column4" runat="server" Text="设备部位" DataIndex="posname" Width="150">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column5" runat="server" Text="设备编号" DataIndex="equipno" Width="180">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column6" runat="server" Text="周期" DataIndex="cycle" Width="90">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column7" runat="server" Text="清洗前温度" DataIndex="before_temp" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column8" runat="server" Text="清洗后温度" DataIndex="after_temp" Width="80">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column9" runat="server" Text="清洗方式" DataIndex="Clean_type" Width="80">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:DateColumn ID="DateColumn1" runat="server" Text="启用日期" DataIndex="begin_date" Width="110" Format="yyyy-MM-dd" >
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
                            <ext:DateColumn ID="DateColumn3" runat="server" Text="最后清洗日期" DataIndex="last_date" Width="110" Format="yyyy-MM-dd" >
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
                            <ext:DateColumn ID="DateColumn2" runat="server" Text="下次日期" DataIndex="Next_date" Width="110" Format="yyyy-MM-dd" >
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
                                
                                <Renderer Fn="validDateChange" />
                            </ext:DateColumn>
                            
                            <ext:Column ID="Column15" runat="server" Text="备注" DataIndex="memo" Width="100">
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
               
             <ext:Window ID="winPlanSave" runat="server" Icon="MonitorAdd" Closable="false" Title="生成设备清洗记录" Width="550" Height="500" Resizable="false" Modal="true" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="FormLayout">
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
                                <ext:FieldSet runat="server" Title="清洗记录" Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer1"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtno" runat="server" Width="230" FieldLabel="设备序号" AllowBlank="false"/>
                                                <ext:TextField ID="txtworkshop" runat="server" Width="230" FieldLabel="厂房" AllowBlank="false"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer5"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtequip" runat="server" Width="230" FieldLabel="机台" AllowBlank="false"/>
                                                <ext:TextField ID="txtequipno" runat="server" Width="230" FieldLabel="设备编号" AllowBlank="false"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer17"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtposname" runat="server" Width="230" FieldLabel="设备部位" AllowBlank="false"/>
                                                <ext:DateField ID="datetime" runat="server" FieldLabel="检验日期" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="230"/>
                                                
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer2"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtbefore" runat="server" Width="230" FieldLabel="清洗前温度" AllowBlank="false"/>
                                                <ext:TextField ID="txtfangshi" runat="server" Width="230" FieldLabel="清洗方式" AllowBlank="false"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer3"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtafter" runat="server" Width="230" FieldLabel="清洗后温度" AllowBlank="false"/>
                                                <ext:TextField ID="txtchangjia" runat="server" Width="230" FieldLabel="清洗厂家" AllowBlank="false"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer4"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtmemo" runat="server" Width="450" FieldLabel="备注"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        
                                        <ext:Hidden runat="server" ID="hideMode" Text="Add" />
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
