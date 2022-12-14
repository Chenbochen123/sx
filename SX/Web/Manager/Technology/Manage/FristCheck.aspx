<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FristCheck.aspx.cs" Inherits="Manager_Technology_Manage_FristCheck" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>首件检查报表</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />

    <script src="<%= Page.ResolveUrl("~/") %>resources/js/default.js"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    
    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>FristCheck.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    <style type="text/css">
        .x-grid-body .x-grid-cell-Cost {
            background-color: #f1f2f4;
        }

        .x-grid-row-summary .x-grid-cell-Cost .x-grid-cell-inner {
            background-color: #e1e2e4;
        }

        .task .x-grid-cell-inner {
            padding-left: 15px;
        }

        .x-grid-row-summary .x-grid-cell-inner {
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

        var cmdcol_click = function (command, record) {
            if (command == "detail") {
                commandcolumn_direct_detail(record);
            }
        }
        var commandcolumn_direct_detail = function (record) {
            var ObjID = record.data.Barcode;
            var equip = record.data.Equip_Code;
            var date = record.data.Plan_Date;
            var shift = record.data.Shift_Id;
            var url = "../Manager/Technology/Manage/FristWeight.aspx?Barcode=" + ObjID + "&Plan_Date=" + date + "&Equip_Code=" + equip + "&Shift_Id=" + shift + "";
            var tabid = "Manager_Technology_Manage_FristWeight";
            var tabp = parent.App.mainTabPanel;
            var tab = tabp.getComponent(tabid);

            if (tab) {
                tab.close();
            }
            var title;
            if (record != null)
                title = "称量明细";
            else
                title = "设备信息";
            parent.addTab(tabid, title, url, true);
            parent.refresh("");
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnExportSubmit" runat="server" Text="Button" OnClick="btnExportSubmit_Click" Style="display: none" />
        <ext:ResourceManager ID="ResourceManager1" runat="server">
        </ext:ResourceManager>
        <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
            <Items>

                <ext:Panel ID="pnlStopType" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barStopType">
                            <Items>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip2" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btnSearch_Click">
                                            <EventMask ShowMask="true" Target="Page"></EventMask>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip3" runat="server" Html="点击进行结果导出" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
                                </ext:Button>
                                
                              <%--  <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="Button1">
                                    <ToolTips><ext:ToolTip ID="ToolTip1" runat="server" Html="点击进行结果导出" /></ToolTips>
                                    <DirectEvents>
                                    <Click OnEvent="btnExportSubmit_Click" IsUpload="true">
                                        <ExtraParams>
                                            <ext:Parameter Name="fields" Value="#{model}.getFields()" Mode="Raw" />
                                            <ext:Parameter Name="records" Value="#{store}.getRecordsValues({ excludeId: true })"
                                                Mode="Raw" />
                                        </ExtraParams>
                                    </Click>
                                    </DirectEvents>
                                </ext:Button>--%>
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
                                <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth=".25">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer9" runat="server" Layout="HBoxLayout" FieldLabel="开始时间">
                                            <Items>
                                                <ext:DateField ID="dStartDate" runat="server" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container2" runat="server" Layout="FormLayout" ColumnWidth=".25">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer2" runat="server" Layout="HBoxLayout" FieldLabel="结束时间">
                                            <Items>
                                                <ext:DateField ID="dEndDate" runat="server" Editable="false" AllowBlank="false" Format="yyyy-MM-dd" Margins="0 3 0 0" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container4" runat="server" Layout="FormLayout" ColumnWidth=".25">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer7" runat="server" Layout="HBoxLayout" FieldLabel="班组">
                                            <Items>
                                                <ext:ComboBox ID="cbxClass" runat="server" Editable="true" />
                                            </Items>
                                        </ext:FieldContainer>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container3" runat="server" Layout="FormLayout" ColumnWidth=".25">
                                    <Items>
                                        <ext:TriggerField ID="txtRecipeEquipName" runat="server" Flex="1" FieldLabel="机台名称" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryEquipInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                    </Items>
                                </ext:Container>

                            </Items>
                        </ext:FormPanel>
                        
                        <ext:Hidden ID="hiddenRecipeEquipCode" runat="server"></ext:Hidden>
                    </Items>
                </ext:Panel>

                <ext:Panel ID="Panel1" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout">
                    <Items>
                        <ext:GridPanel ID="pnlList" runat="server" Region="West" Frame="true" Weight="500" >
                            <Store>
                                <ext:Store ID="store" runat="server" PageSize="50">
                                    <Sorters>
                                        <ext:DataSorter Property="Barcode" />
                                    </Sorters>
                                    <Model>
                                        <ext:Model ID="model" runat="server" IDProperty="Barcode">
                                            <Fields>
                                                <ext:ModelField Name="Barcode" />
                                                <ext:ModelField Name="Plan_Date" />
                                                <ext:ModelField Name="ShiftName" />
                                                <ext:ModelField Name="Equip_Code" />
                                                <ext:ModelField Name="Shift_Id"  Type="Int"/>
                                                <ext:ModelField Name="shift_ClassName" />
                                                <ext:ModelField Name="Equip_name" />
                                                <ext:ModelField Name="Mater_Name" />
                                                <ext:ModelField Name="Start_Datetime" />
                                                <ext:ModelField Name="Serial_Id" Type="Int" />
                                                <ext:ModelField Name="Pj_Temp" Type="Int" />
                                                <ext:ModelField Name="Rooltemp" Type="Int" />
                                                <ext:ModelField Name="CBtemp" Type="Int" />
                                                <ext:ModelField Name="XLMtemp" Type="Int" />
                                                <ext:ModelField Name="speed" Type="Int" />
                                                <ext:ModelField Name="max_press" Type="Float" />
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
                                    <%--<ext:Column ID="主键1" runat="server" Text="主键1" DataIndex="Barcode" Hidden="true" Width="65">
                                    </ext:Column>
                                    <ext:Column ID="Column18" runat="server" Text="机台代码" DataIndex="Equip_Code" Hidden="true"  Width="75">
                                    </ext:Column>
                                    <ext:Column ID="Column19" runat="server" Text="班次编码" DataIndex="Shift_Id" Hidden="true" Width="75">
                                    </ext:Column>--%>
                                    <ext:Column ID="zhu2" runat="server" Text="计划日期" DataIndex="Plan_Date" Width="75">
                                    </ext:Column>
                                    <ext:Column ID="Column3" runat="server" Text="班次" DataIndex="ShiftName" Width="35">
                                    </ext:Column>
                                    <ext:Column ID="Column4" runat="server" Text="班组" DataIndex="shift_ClassName" Width="35">
                                    </ext:Column>
                                    <ext:Column ID="Column5" runat="server" Text="机台" DataIndex="Equip_name" Width="75">
                                    </ext:Column>
                                    <ext:Column ID="Column6" runat="server" Text="物料名称" DataIndex="Mater_Name" Width="130">
                                    </ext:Column>
                                    <ext:Column ID="Column7" runat="server" Text="生产时间" DataIndex="Start_Datetime" Width="125">
                                    </ext:Column>
                                    <ext:Column ID="Column8" runat="server" Text="车次" DataIndex="Serial_Id" Width="40">
                                    </ext:Column>
                                    <ext:Column ID="Column9" runat="server" Text="排胶温度" DataIndex="Pj_Temp" Width="65">
                                    </ext:Column>
                                    <ext:Column ID="Column10" runat="server" Text="转子温度" DataIndex="Rooltemp" Width="65">
                                    </ext:Column>
                                    <ext:Column ID="Column11" runat="server" Text="室壁温度" DataIndex="CBtemp" Width="65">
                                    </ext:Column>
                                    <ext:Column ID="Column12" runat="server" Text="下顶栓温度" DataIndex="XLMtemp" Width="70">
                                    </ext:Column>
                                    <ext:Column ID="Column2" runat="server" Text="转速" DataIndex="speed" Width="40">
                                    </ext:Column>
                                    <ext:Column ID="Column14" runat="server" Text="上顶栓压力" DataIndex="max_press" Width="70">
                                    </ext:Column>
                                    <ext:CommandColumn ID="cmdCol" runat="server" Align="Center" Text="操作" Width="75">
                                        <Commands>
                                            <ext:GridCommand Icon="ApplicationViewDetail" CommandName="detail" Text="明细数据" />
                                        </Commands>
                                        <Listeners>
                                            <Command Handler="cmdcol_click(command, record);" />
                                        </Listeners>
                                    </ext:CommandColumn>
                                </Columns>
                            </ColumnModel>
                          <%--  <Listeners>
                                <SelectionChange Handler="App.btnRemove.setDisabled(!selected.length);" />
                            </Listeners>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                                    <Listeners>
                                        <Select Handler="#{detailStore}.reload();" Buffer="250" />
                                    </Listeners>
                                </ext:RowSelectionModel>
                            </SelectionModel>--%>
                            <BottomBar>
                                <ext:PagingToolbar ID="pageToolBar" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>



                        <ext:GridPanel ID="GridPanel1" runat="server" Region="Center" Frame="true" Flex="1">
                            <Store>
                                <ext:Store ID="store1" runat="server" PageSize="50">
                                    <Sorters>
                                      <%--  <ext:DataSorter Property="Barcode" />--%>
                                    </Sorters>
                                    <Model>
                                        <ext:Model ID="model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="scale_name" />
                                                <ext:ModelField Name="Plan_Date" />
                                                <ext:ModelField Name="real_weight" />
                                                <ext:ModelField Name="shift_ClassName" />
                                                <ext:ModelField Name="Equip_name" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <Plugins>
                                <ext:CellEditing runat="server" ClicksToMoveEditor="1" AutoCancel="false" />
                            </Plugins>
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:Column ID="Column1" runat="server" Text="秤名称" DataIndex="scale_name" Width="65">
                                    </ext:Column>
                                    <ext:Column ID="Column13" runat="server" Text="秤校准值" DataIndex="real_weight" Width="85">
                                    </ext:Column>
                                    <ext:Column ID="Column15" runat="server" Text="计划日期" DataIndex="Plan_Date" Width="75">
                                    </ext:Column>
                                    <ext:Column ID="Column16" runat="server" Text="班组" DataIndex="shift_ClassName" Width="50">
                                    </ext:Column>
                                    <ext:Column ID="Column17" runat="server" Text="机台" DataIndex="Equip_name" Width="80">
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                           <%-- <Listeners>
                                <SelectionChange Handler="App.btnRemove.setDisabled(!selected.length);" />
                            </Listeners>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single">
                                    <Listeners>
                                        <Select Handler="#{detailStore}.reload();" Buffer="250" />
                                    </Listeners>
                                </ext:RowSelectionModel>
                            </SelectionModel>--%>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server">
                                    <Plugins>
                                        <ext:ProgressBarPager ID="ProgressBarPager1" runat="server" />
                                    </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
