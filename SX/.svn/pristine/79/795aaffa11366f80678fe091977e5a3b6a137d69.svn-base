<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EnvironmentalStandards.aspx.cs" Inherits="Manager_Equipment_Environmental_EnvironmentalStandards" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>环保设施定义</title>
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
                App.direct.pnlList_Add(record.data.serialid, record.data.work_Type,
                    record.data.equip_name, record.data.check_content, record.data.Admin_user,
                    record.data.safe_user, record.data.check_user, record.data.user_num,
                    record.data.check_hour, record.data.cycle, record.data.mater_type,
                    record.data.mater_num, record.data.start_date, record.data.replace_date,
                    record.data.UseDay, record.data.memo,
                    {
                    success: function () { },
                    failure: function () { }
                });
            }
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
                serialid: '0',
                work_Type: '',
                equip_name: '',
                process_name: '',
                check_content: '',
                Admin_user: '',
                safe_user: '',
                check_user: '',
                user_num: '1',
                check_hour: '0',
                construction: '',
                cycle: '12',
                safe_measures: '',
                start_date: Ext.Date.clearTime(new Date()),
                replace_date: Ext.Date.clearTime(new Date()),
                mater_type: '',
                mater_num: '0',
                memo: '',
                UseDay: '0'
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
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips><ext:ToolTip ID="ToolTip2" runat="server" Html="点击进行查询" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page"></EventMask>
                                    </Click></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="FolderConnect" Text="生成检修计划" ID="btnCreatePlan">
                                    <ToolTips><ext:ToolTip ID="ToolTip5" runat="server" Html="点击生成计划" /></ToolTips>
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
                                <ext:DataSorter Property="serialid" />
                            </Sorters>
                            <Model>
                                <ext:Model ID="model" runat="server" IDProperty="serialid">
                                    <Fields>
                                        <ext:ModelField Name="serialid" Type="Int" />
                                        <ext:ModelField Name="work_Type" />
                                        <ext:ModelField Name="process_name" />
                                        <ext:ModelField Name="equip_name" />
                                        <ext:ModelField Name="check_content" />
                                        <ext:ModelField Name="Admin_user" />
                                        <ext:ModelField Name="safe_user" />
                                        <ext:ModelField Name="check_user" />
                                        <ext:ModelField Name="user_num" Type="Int" />
                                        <ext:ModelField Name="check_hour" Type="Float" />
                                        <ext:ModelField Name="construction" />
                                        <ext:ModelField Name="cycle" Type="Float" />
                                        <ext:ModelField Name="safe_measures" />
                                        <ext:ModelField Name="start_date" Type="Date" />
                                        <ext:ModelField Name="replace_date" Type="Date" />
                                        <ext:ModelField Name="mater_type" />
                                        <ext:ModelField Name="mater_num" Type="Float" />
                                        <ext:ModelField Name="memo" />
                                        <ext:ModelField Name="workshopname" />
                                        <ext:ModelField Name="UseDay" Type="Float" />
                                        <ext:ModelField Name="Next_date" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <Plugins>
                        <ext:CellEditing runat="server" ClicksToMoveEditor="1" AutoCancel="false" />
                        <%--<ext:RowEditing runat="server" ClicksToMoveEditor="1" AutoCancel="false" ClicksToEdit="1" />--%>
                    </Plugins>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:Column ID="clserialid" runat="server" Text="序号" DataIndex="serialid" Hidden="false" Width="65">
                            </ext:Column>
                            <ext:Column ID="clwork_Type" runat="server" Text="工段名称" DataIndex="work_Type" Width="80">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column3" runat="server" Text="设备名称" DataIndex="equip_name" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column4" runat="server" Text="检查修理内容" DataIndex="check_content" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column5" runat="server" Text="管理员" DataIndex="Admin_user" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column8" runat="server" Text="安全负责人" DataIndex="safe_user" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column9" runat="server" Text="检查负责人" DataIndex="check_user" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column11" runat="server" Text="维修定员(人)" DataIndex="user_num" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number" Text="0" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column13" runat="server" Text="维修定时(时)" DataIndex="check_hour" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number" Text="0" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column14" runat="server" Text="周期(月)" DataIndex="cycle" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number" Text="12" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column15" runat="server" Text="滤袋型号" DataIndex="mater_type" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column16" runat="server" Text="滤袋数量" DataIndex="mater_num" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number" Text="1" />
                                </Editor>
                            </ext:Column>
                            <ext:DateColumn ID="Column7" runat="server" Text="启用日期" DataIndex="start_date" Width="100" Format="yyyy-MM-dd" >
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
                            <ext:DateColumn ID="Column12" runat="server" Text="更换日期" DataIndex="replace_date" Width="100" Format="yyyy-MM-dd">
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
                            <ext:Column ID="Column2" runat="server" Text="开动时间" DataIndex="UseDay" Width="65">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" InputType="Number" Text="0" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column21" runat="server" Text="下次更换日期" DataIndex="Next_date" Width="100">
                                <Renderer Fn="validDateChange" />
                            </ext:Column>
                            <ext:Column ID="Column1" runat="server" Text="备注" DataIndex="memo" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" />
                                </Editor>
                            </ext:Column>
                            <ext:CommandColumn ID="cmdCol" runat="server" Align="Center" Text="操作" Width="185">
                                <Commands>
                                    <ext:GridCommand Icon="DatabaseSave" CommandName="Save" Text="保存"/>
                                    <ext:GridCommand Icon="TableDelete" CommandName="Delete" Text="删除"/>
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
                <ext:Window ID="winPlanSave" runat="server" Icon="MonitorAdd" Closable="false" Title="环保计划单" Width="550" Height="500" Resizable="false" Modal="true" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="FormLayout">
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
                                <ext:FieldSet runat="server" Title="计划信息" Layout="AnchorLayout" DefaultAnchor="100%">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer17"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:DateField ID="txtMp_plandate" runat="server" FieldLabel="计划维修日期" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" Width="200"/>
                                                
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer8"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtMp_memo" runat="server" Width="450" FieldLabel="备注"/>
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
