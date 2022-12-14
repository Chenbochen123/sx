<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KongTiaoRecord.aspx.cs" Inherits="Manager_Equipment_EquipRepairProtectPlan_KongTiaoRecord" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>空调维修记录</title>
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
                App.direct.pnlList_Add(record.data.serialid, record.data.INO, record.data.EquipNO, record.data.PosName, record.data.lastdate, record.data.lastfac,
                    record.data.WX_reason, record.data.WX_money, record.data.pseron, record.data.memo, 
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
            var ID = record.data.serialid;
            App.direct.pnlList_Delete(ID, {
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
                INO: '',
                EquipNO: '',
                PosName: '',
                lastdate:  Ext.Date.clearTime(new Date()),
                lastfac: '',
                WX_reason: '',
                WX_money: '',
                pseron: '',
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
        var ImportBill = function () {
            App.WinUpLoadBill.show();
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
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                    <ToolTips><ext:ToolTip ID="ToolTip3" runat="server" Html="点击进行结果导出" /></ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" IconCls="fa fa-search fabtn color-primary" Text="单据导入" ID="btnImport" ToolTip="点击进行备件单导入">
                                    <Listeners>
                                        <Click Fn="ImportBill">
                                        </Click>
                                    </Listeners>
                                </ext:Button>
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
                                        <ext:ModelField Name="INO"  Type="Int"/>
                                        <ext:ModelField Name="EquipNO" />
                                        <ext:ModelField Name="PosName"/>
                                        <ext:ModelField Name="lastdate" Type="Date"/>
                                        <ext:ModelField Name="lastfac"/>
                                        <ext:ModelField Name="WX_reason" />
                                        <ext:ModelField Name="WX_money" type="Float"/>
                                        <ext:ModelField Name="pseron"/>
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
                            <ext:Column ID="Column18" runat="server" Text="主键" DataIndex="serialid" Hidden="true" Width="65">
                            </ext:Column>
                            <ext:Column ID="Column1" runat="server" Text="序号" DataIndex="INO" Width="80">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column3" runat="server" Text="设备编号" DataIndex="EquipNO" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column4" runat="server" Text="安装位置" DataIndex="PosName" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:DateColumn ID="DateColumn4" runat="server" Text="维修日期" DataIndex="lastdate" Width="150" Format="yyyy-MM-dd" >
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
                            <ext:Column ID="Column6" runat="server" Text="维修厂家" DataIndex="lastfac" Width="200">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column7" runat="server" Text="修理原因" DataIndex="WX_reason" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column8" runat="server" Text="修理费用" DataIndex="WX_money" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column9" runat="server" Text="负责人" DataIndex="pseron" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="false" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column14" runat="server" Text="备注" DataIndex="memo" Width="200">
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
               
                <ext:Hidden ID="hidden_equip_code" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hidden_type" runat="server">
                </ext:Hidden>
             <ext:Window ID="WinUpLoadBill" runat="server" IconCls="fa fa-upload" Closable="true" Title="Excel导入"
                    Width="400" Height="170" Resizable="true" Hidden="true" Modal="true" Layout="FitLayout">
                    <Items>
                        <ext:Container runat="server">
                            <Items>
                                <ext:FormPanel ID="pnlUpLoadBill" runat="server" Layout="ColumnLayout" BodyPadding="5">
                                    <Defaults>
                                        <ext:Parameter Name="anchor" Value="95%" Mode="Value" />
                                        <ext:Parameter Name="allowBlank" Value="false" Mode="Raw" />
                                        <ext:Parameter Name="msgTarget" Value="side" Mode="Value" />
                                    </Defaults>
                                    <Items>
                                        <ext:FileUploadField ID="FileUploadField2" runat="server" ButtonText="请选择..."
                                            IconCls="fa fa-link fa-rotate-90 fabtn" ColumnWidth="1" />
                                    </Items>
                                    <Listeners>
                                        <ValidityChange Handler="#{btnUploadSaveBill}.setDisabled(!valid);" />
                                    </Listeners>
                                </ext:FormPanel>
                                <ext:Container runat="server" Layout="ColumnLayout" PaddingSpec="5 5 5 5">
                                    <Items>
                                        <ext:ProgressBar ID="Progress2" runat="server" ColumnWidth="1" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnDownloadBill" runat="server" IconCls="fa fa-download fabtn" Text="模板下载"
                            ToolTip="点击下载模板" UI="Info">
                            <DirectEvents>
                                <Click OnEvent="btnDownload_ClickEvent" />
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnUploadSaveBill" runat="server" Text="导入数据" Disabled="true" IconCls="fa fa-check-circle fabtn">
                            <DirectEvents>
                                <Click OnEvent="UploadClickBill"
                                    Before="App.Progress2.wait({interval: 200,duration: 600000,increment: 20,text: '数据读取……'});App.btnUploadSaveBill.setDisabled(true);App.btnUploadCloseBill.setDisabled(true);"
                                    Failure="App.Progress2.updateProgress(0,'数据读取错误');"
                                    Success="App.btnUploadSaveBill.setDisabled(false);App.btnUploadCloseBill.setDisabled(false);">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnUploadCloseBill" runat="server" Text="关闭" IconCls="fa fa-times-circle fabtn" UI="Danger">
                            <Listeners>
                                <Click Handler="#{pnlUpLoadBill}.getForm().reset();#{WinUpLoadBill}.close();App.Progress2.updateProgress(0,'0%');" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:Window>
                </Items>
    </ext:Viewport>
    </form>
</body>
</html>
