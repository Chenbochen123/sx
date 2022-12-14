<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EquipProg.aspx.cs" Inherits="Manager_Equipment_EquipProg" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>设备问题清单</title>
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


        //点击修改按钮
        var cmdcol_click = function (command, record) {
            if (command == "Delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此信息？删除后将不能恢复！', function (btn) { deleteStopRecord(btn, record) });
            }
            else if (command == "Save") {
                App.direct.pnlList_Add(record.data.serialid, record.data.workshop,
                    record.data.equipName, record.data.P_kind, record.data.P_describe,
                    record.data.P_classify, record.data.P_measures, record.data.P_FindUser,
                    record.data.P_FindDate, record.data.P_FinishDate, record.data.P_CheckUser,
                    record.data.P_memo, record.data.P_State, 
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
            var Serial_id = record.data.serialid;
            App.direct.pnlList_Delete(Serial_id, {
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

            debugger;
            store.insert(0, {
                serialid: '0',
                workshop:'',
                equipName: '',
                P_kind: '',
                P_describe: '',
                P_classify: '',
                P_measures: '',
                P_FindUser: '',
                P_FindDate: '',
                P_FinishDate:'', 
                P_CheckUser: '',
                P_CheckUser: '',
                P_memo: '',
                P_State: '',
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
                                <ext:ToolbarSeparator />
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
                    <Items>
                        <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Container ID="Container1"  runat="server" Layout="FormLayout" ColumnWidth=".25">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer9"  runat="server" Layout="HBoxLayout" FieldLabel="开始时间" LabelAlign="Right">
                                            <Items>
                                                <ext:DateField ID="dStartDate" runat="server" Editable="false"  Format="yyyy-MM-dd" Margins="0 3 0 0" Width="120"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container2"  runat="server" Layout="FormLayout" ColumnWidth=".25">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer7"  runat="server" Layout="HBoxLayout" FieldLabel="结束时间" LabelAlign="Right">
                                            <Items>
                                                <ext:DateField ID="dEndDate" runat="server" Editable="false"  Format="yyyy-MM-dd" Margins="0 3 0 0" Width="120"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container3"  runat="server" Layout="FormLayout" ColumnWidth=".25">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer1"  runat="server" Layout="HBoxLayout" FieldLabel="机台" LabelAlign="Right">
                                            <Items>
                                                <ext:TextField ID="txtequipname" runat="server" Editable="false"   Width="120"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container4"  runat="server" Layout="FormLayout" ColumnWidth=".25">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer2"  runat="server" Layout="HBoxLayout" FieldLabel="状态" LabelAlign="Right">
                                            <Items>
                                                <ext:TextField ID="txtstate" runat="server" Editable="false"  Width="120"/>
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                        
                        <%--<ext:FormPanel ID="FormPanel1" runat="server" Layout="ColumnLayout" AutoHeight="true" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Container ID="Container2"  runat="server" Layout="FormLayout" ColumnWidth=".25">
                                    <Items>
                                        <ext:ComboBox ID="cbxFac" runat="server"  AllowBlank="false" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>--%>
                    </Items>
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
                                        <ext:ModelField Name="serialid" Type="Int"  />
                                        <ext:ModelField Name="workshop" />
                                        <ext:ModelField Name="equipName" />
                                        <ext:ModelField Name="P_kind" />
                                        <ext:ModelField Name="P_describe" />
                                        <ext:ModelField Name="P_classify" />
                                        <ext:ModelField Name="P_measures" />
                                        <ext:ModelField Name="P_FindUser" />
                                        <ext:ModelField Name="P_FindDate"  Type="Date" />
                                        <ext:ModelField Name="P_FinishDate" Type="Date"/>
                                        <ext:ModelField Name="P_CheckUser" />
                                        <ext:ModelField Name="P_memo" />
                                        <ext:ModelField Name="P_State" />
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
                            <ext:Column ID="clserialid" runat="server" Text="序号" DataIndex="serialid" Hidden="true" Width="65">
                            </ext:Column>
                            <%--<ext:DateColumn ID="Column7" runat="server" Text="开始日期" DataIndex="Start_date" Width="100" Format="yyyy-MM-dd" >
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
                            </ext:DateColumn>--%>
                            <ext:Column ID="Column17" runat="server" Text="厂房" DataIndex="workshop" Width="80">
                                <Editor>
                                     <ext:TextField runat="server" AllowBlank="true" ID="TextField4" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column4" runat="server" Text="机台" DataIndex="equipName" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" ID="txtequip" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column3" runat="server" Text="部位" DataIndex="P_kind" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" ID="txtkind" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column7" runat="server" Text="问题描述" DataIndex="P_describe" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" ID="txtdesc" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column11" runat="server" Text="问题分类" DataIndex="P_classify" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" ID="txtclalss" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column12" runat="server" Text="整改措施" DataIndex="P_measures" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" ID="txtmeas" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column13" runat="server" Text="提出人" DataIndex="P_FindUser" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" ID="txtuser" />
                                </Editor>
                            </ext:Column>
                            <ext:DateColumn ID="Column77" runat="server" Text="提出日期" DataIndex="P_FindDate" Width="100" Format="yyyy-MM-dd" >
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
                            <ext:DateColumn ID="DateColumn1" runat="server" Text="完成日期" DataIndex="P_FinishDate" Width="100" Format="yyyy-MM-dd" >
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
                            <ext:Column ID="Column14" runat="server" Text="完成人" DataIndex="P_CheckUser" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" ID="TextField1" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column15" runat="server" Text="备注" DataIndex="P_memo" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" ID="TextField2" />
                                </Editor>
                            </ext:Column>
                            <ext:Column ID="Column16" runat="server" Text="状态" DataIndex="P_State" Width="100">
                                <Editor>
                                    <ext:TextField runat="server" AllowBlank="true" ID="TextField3" />
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
                <ext:Hidden ID="hiddenDian_price" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hiddenFeng_Price" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hiddenWater_price" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hiddenQi_price" runat="server">
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
                        <ext:Button ID="btnUploadSaveBill" runat="server" Text="导入设备问题" Disabled="true" IconCls="fa fa-check-circle fabtn">
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
