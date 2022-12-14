<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BJplan.aspx.cs" Inherits="Manager_Equipment_BJplan" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>备件采购计划</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell
        {
        	background-color: #CCFF66 !important;
        }
    </style>
    <script type="text/javascript">
        //点击修改按钮
        var cmdcol_click = function (command, record) {
            if (command == "Edit") {
                App.direct.pnlList_Edit(record.data.Plan_Date, record.data.SerialID, record.data.BJ_code, {
                    success: function () { },
                    failure: function () { }
                });
            }
            else if (command == "Delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此信息？删除后将不能恢复！', function (btn) { deleteStopRecord(btn, record) });
            }
        }

        var deleteStopRecord = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var Plan_Date = record.data.Plan_Date;
            var SerialID = record.data.SerialID;
            var BJ_code = record.data.BJ_code;
            App.direct.pnlList_Delete(Plan_Date,SerialID, BJ_code,{
                success: function () { },
                failure: function () { }
            });
        }

        var ImportBill = function () {
            App.WinUpLoadBill.show();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" runat="server" Text="Button" OnClick="btnExportSubmit_Click" Style="display:none" />
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Hidden ID="hidden_stop_type" runat="server" />
    <ext:Hidden ID="hidden_stop_fault" runat="server" />
    <ext:Hidden ID="hidden_fault_reason" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlStopType" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barStopType">
                            <Items>

                                <ext:Button runat="server" Icon="Add" Text="添加" ID="btnAdd">
                                    <ToolTips><ext:ToolTip ID="ttAdd" runat="server" Html="点击进行添加" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnAdd_Click"/></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips><ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page"></EventMask>
                                    </Click></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" IconCls="fa fa-search fabtn color-primary" Text="单据导入" ID="btnImport" ToolTip="点击进行备件单导入">
                                    <Listeners>
                                        <Click Fn="ImportBill">
                                        </Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                    <ToolTips><ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行结果导出" /></ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator />
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
                                 <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                         <ext:DateField ID="DateBeginTime" runat="server" FieldLabel="开始时间"  Editable="false" />
                                    </Items>
                                </ext:Container>
                                 <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                        <ext:DateField ID="DateEndTime" runat="server" FieldLabel="结束时间"   Editable="false" />
                                    </Items>
                                </ext:Container>
                               
                            </Items>
                        </ext:FormPanel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="50">
                            <Model>
                                <ext:Model ID="model" runat="server" IDProperty="Plan_Date,SerialID,BJ_code">
                                    <Fields>
                                        <ext:ModelField Name="Plan_Date" />
                                        <ext:ModelField Name="SerialID" />
                                        <ext:ModelField Name="BJ_code" />
                                        <ext:ModelField Name="BJ_name" />
                                        <ext:ModelField Name="Stock_Code" />
                                        <ext:ModelField Name="BJ_specType" />
                                        <ext:ModelField Name="BJ_tpcode" />
                                        <ext:ModelField Name="Plan_Num" />
                                        <ext:ModelField Name="unit_Code" />
                                        <ext:ModelField Name="plan_price" />
                                        <ext:ModelField Name="Total_Price" />
                                        <ext:ModelField Name="Stock_Worker" />
                                        <ext:ModelField Name="state" />
                                        <ext:ModelField Name="prefer_date" />
                                        <ext:ModelField Name="RealIn_Date" />
                                        <ext:ModelField Name="Remark" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:Column ID="Column3" runat="server" Text="计划日期" DataIndex="Plan_Date" Width="80"/>
                            <ext:Column ID="clEquip" runat="server" Text="序号" DataIndex="SerialID" Width="100"/>
                            <ext:Column ID="Column10" runat="server" Text="备件代码" DataIndex="BJ_code" Width="120"/>
                            <ext:Column ID="Column21" runat="server" Text="备件名称" DataIndex="BJ_name" Width="120"/>
                            <ext:Column ID="Column22" runat="server" Text="仓库" DataIndex="Stock_Code" Width="120"/>
                            <ext:Column ID="clStartTime" runat="server" Text="规格型号" DataIndex="BJ_specType" Width="135"/>
                            <ext:Column ID="clEndTime" runat="server" Text="备件分类" DataIndex="BJ_tpcode" Width="135"/>
                            <ext:Column ID="Column1" runat="server" Text="计划数量" DataIndex="Plan_Num" Width="65" />
                            <ext:Column ID="Column2" runat="server" Text="单位" DataIndex="unit_Code" Width="80"/>
                            <ext:Column ID="Column4" runat="server" Text="单价" DataIndex="plan_price" Width="80"/>
                            <ext:Column ID="Column5" runat="server" Text="总金额" DataIndex="Total_Price" Width="80"/>
                            <ext:Column ID="Column20" runat="server" Text="申请人" DataIndex="Stock_Worker" Width="80"/>
                            <ext:Column ID="Column6" runat="server" Text="状态" DataIndex="state" Width="80"/>
                            <ext:Column ID="Column7" runat="server" Text="需求到货日期" DataIndex="prefer_date" Width="120"/>
                            <ext:Column ID="Column8" runat="server" Text="实际到货日期" DataIndex="RealIn_Date" Width="120"/>
                            <ext:Column ID="Column12" runat="server" Text="用途" DataIndex="Remark"  Width="120"/>

                            <ext:CommandColumn ID="cmdCol" runat="server" Align="Center" Text="操作" Width="185">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改"/>
                                    <ext:GridCommand Icon="TableDelete" CommandName="Delete" Text="删除"/>
                                    <%--<ext:GridCommand Icon="Add" CommandName="Add" Text="报修"/>--%>
                                </Commands>
                                <Listeners>
                                    <Command Handler="cmdcol_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
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
                <ext:Window ID="winSave" runat="server" Icon="MonitorAdd" Closable="false" Title="采购计划维护" Width="550" Height="500" Resizable="false" Modal="true" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="FormLayout">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5" Layout="FormLayout">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="70" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Hidden runat="server" ID="hideObjID" />
                                <ext:FieldSet ID="FieldSet1" runat="server" Title="采购计划维护">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer12"  runat="server" Layout="HBoxLayout"  AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="dateplan" runat="server" FieldLabel="计划日期" Width="230" Editable="false" allowblank="false" ReadOnly="true"/>
                                                 <ext:TextField ID="txtno" runat="server" FieldLabel="序号" Width="230" />
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer16" runat="server" Layout="HBoxLayout" AnchorHorizontal="100%">
                                            <Items>
                                                 <ext:TextField ID="txtcangku" runat="server" FieldLabel="仓库"  Width="230"/>
                                                 <ext:TextField ID="txtcode" runat="server" FieldLabel="备件代码" Width="230"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer2" runat="server" Layout="HBoxLayout" AnchorHorizontal="100%">
                                            <Items>
                                                 <ext:TextField ID="txtnum" runat="server" FieldLabel="计划数量" Width="230" />
                                                 <ext:TextField ID="txtuser" runat="server" FieldLabel="申请人" Width="230" />
                                            </Items>
                                        </ext:FieldContainer>

                                        <ext:FieldContainer ID="FieldContainer1" runat="server" Layout="HBoxLayout" AnchorHorizontal="100%">
                                            <Items>

                                                <ext:ComboBox ID="cbxstate" runat="server" FieldLabel="状态" Width="230">

                                                    <Items>
                                                        <ext:ListItem Mode="Value" Value="0" Text="新计划" />
                                                        <ext:ListItem Mode="Value" Value="1" Text="完成" />
                                                    </Items>
                                                </ext:ComboBox>
                                                <ext:TextField ID="txtxuqiu" runat="server" FieldLabel="需求到货日期" Width="230" />
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer34" runat="server" Layout="HBoxLayout" AnchorHorizontal="100%">
                                            <Items>
                                                 <ext:TextField ID="txtshiji" runat="server" FieldLabel="实际到货日期" Width="230" />
                                                 <ext:TextField ID="txtyongtu" runat="server" FieldLabel="用途" Width="230" />
                                            </Items>
                                        </ext:FieldContainer>

                                        <ext:Hidden runat="server" ID="hideMode" Text="Add" />
                                    </Items>
                                </ext:FieldSet>
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                     <Buttons>
                        <ext:Button ID="btnSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <DirectEvents>
                                <Click OnEvent="btnSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click"/>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:Window>

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
                        <ext:Button ID="btnUploadSaveBill" runat="server" Text="导入备件采购单" Disabled="true" IconCls="fa fa-check-circle fabtn">
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