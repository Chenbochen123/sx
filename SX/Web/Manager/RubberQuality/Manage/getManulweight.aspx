<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getManulweight.aspx.cs" Inherits="Manager_RubberQuality_Manage_getManulweight" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>手动炼胶查询</title>
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
     <%--   <asp:Button ID="btnExportSubmit" runat="server" Text="Button" OnClick="btnExportSubmit_Click" Style="display: none" />--%>
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
                                        <Click OnEvent="btnSearch_Click" Timeout="600000">
                                            <EventMask ShowMask="true" Target="Page"></EventMask>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <%--<ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                    <ToolTips>
                                        <ext:ToolTip ID="ToolTip3" runat="server" Html="点击进行结果导出" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
                                    </Listeners>
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
                                        <ext:ComboBox ID="cbxType" runat="server" FieldLabel="控制方式" LabelAlign="Left">
                                            <Items>
                                                <ext:ListItem Value="1" Text="本控"></ext:ListItem>
                                                <ext:ListItem Value="2" Text="遥控"></ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                        <ext:Hidden ID="hiddenbarcode" runat="server"></ext:Hidden>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="PanelCenter" runat="server" Region="Center" AutoHeight="true" Layout="BorderLayout" AutoScroll="true">
                    <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenter" Region="North" Height="300" AutoScroll="true"> 
                            <Store>
                                <ext:Store ID="store2" runat="server">
                                    <Model>
                                        <ext:Model ID="ModelCenter" runat="server" IDProperty="barcode">
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel>
                                <Columns>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                                    <Listeners>
                                        <Select Handler="#{store1}.reload();" Buffer="250" />
                                    </Listeners>
                                </ext:RowSelectionModel>
                            </SelectionModel>
                        </ext:GridPanel>

                          <ext:GridPanel ID="GridPanel1" runat="server" Region="Center" Frame="true" Flex="1" AutoScroll="true">
                            <Store>
                                <ext:Store ID="store1" runat="server" PageSize="50"  OnReadData="RowSelect">
                                    <Sorters>
                                    </Sorters>
                                    <Model>
                                        <ext:Model ID="model1" runat="server" IDProperty="Barcode">
                                            <Fields>
                                                <ext:ModelField Name="Barcode" />
                                                <ext:ModelField Name="Mater_name" />
                                                <ext:ModelField Name="Set_weight" />
                                                <ext:ModelField Name="Real_weight" />
                                                <ext:ModelField Name="state" />
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
                                    <ext:Column ID="Column1" runat="server" Text="物料名称" DataIndex="Mater_name" Width="65">
                                    </ext:Column>
                                    <ext:Column ID="Column13" runat="server" Text="设定值" DataIndex="Set_weight" Width="85">
                                    </ext:Column>
                                    <ext:Column ID="Column15" runat="server" Text="实际值" DataIndex="Real_weight" Width="75">
                                    </ext:Column>
                                    <ext:Column ID="Column16" runat="server" Text="称量" DataIndex="state" Width="50">
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
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
