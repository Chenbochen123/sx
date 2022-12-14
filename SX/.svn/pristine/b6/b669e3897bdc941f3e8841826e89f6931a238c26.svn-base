<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IntervalTime.aspx.cs" Inherits="Manager_Technology_Manage_IntervalTime" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>间隔时间分类维护</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/default.js"></script>
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <style type="text/css">
        .x-grid-body .x-grid-cell-Cost {
            background-color: #f1f2f4;
        }
        .x-grid-row-collapsedYellow .x-grid-cell {     
            background-color: #FFFF00 !important;
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
    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>Search.js?_dc=<%= DateTime.Now.ToString("yyyyMMddHHmmss") %>"></script>
    
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

        var SetRowClass = function (record, rowIndex, rowParams, store) {
            var rowClass = '';
            if (record.get("Bwb_Time") >= "300" && record.get("Serial_Id")!="1") {
                rowClass = 'x-grid-row-collapsedYellow';
            }

            return rowClass;
        }
    </script>
    <script type="text/javascript">

        //点击修改按钮
        var cmdcol_click = function (command, record) {
            if (command == "Edit") {
                App.direct.pnlList_Edit(record.data.Barcode, record.data.Plan_Date, {
                    success: function () { },
                    failure: function () { }
                });
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
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
                                <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                    Padding="5">
                                    <Items>
                                        <ext:DateField ID="datetime" runat="server" Disabled="false" Width="260" AnchorHorizontal="100%" FieldLabel="生产日期" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container5" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                    Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="cbxequip" runat="server" Disabled="false" Width="260" AnchorHorizontal="100%" FieldLabel="机台" Editable="false">
                                            
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
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                    Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="cbxshift" runat="server" FieldLabel="班次" LabelAlign="left" Width="260"
                                            Editable="false">
                                            <Items>
                                                <ext:ListItem Text="白" Value="1">
                                                </ext:ListItem>
                                                <ext:ListItem Text="夜" Value="3">
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
                                <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".2"
                                    Padding="5">
                                    <Items>
                                        <ext:ComboBox ID="cbxyuanyin" runat="server" FieldLabel="原因分类为空" LabelAlign="left" Width="260"
                                            Editable="false" Text="是">
                                            <Items>
                                                <ext:ListItem Text="是" Value="1">
                                                </ext:ListItem>
                                                <ext:ListItem Text="否" Value="2">
                                                </ext:ListItem>
                                            </Items>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                    </Items>

                </ext:Panel>
                <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="50">
                            <Sorters>
                                <ext:DataSorter Property="Barcode" />
                            </Sorters>
                            <Model>
                                <ext:Model ID="model" runat="server" IDProperty="Barcode,Plan_Date">
                                    <Fields>
                                        <ext:ModelField Name="Barcode" />
                                        <ext:ModelField Name="Plan_Date" />
                                        <ext:ModelField Name="Mater_Name" />
                                        <ext:ModelField Name="Serial_Id" Type="Int" />
                                        <ext:ModelField Name="Start_Datetime" />
                                        <ext:ModelField Name="Bwb_Time" Type="Int" />
                                        <ext:ModelField Name="mkind_name" />
                                        <ext:ModelField Name="ikind_name" />
                                        <ext:ModelField Name="Equip_name" />
                                        <ext:ModelField Name="ShiftName" />
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
                            <ext:Column ID="Column18" runat="server" Text="Barcode" DataIndex="Barcode" Hidden="true" Width="65">
                            </ext:Column>
                            <ext:Column runat="server" ID="Plan_date" DataIndex="Plan_Date" Text="生产日期" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column3" DataIndex="Equip_name" Text="机台" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column4" DataIndex="ShiftName" Text="班次" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column2" DataIndex="Mater_Name" Text="胶料名称" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column1" DataIndex="Serial_Id" Text="辊次" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column8" DataIndex="Start_Datetime" Text="开始时间" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Bwb_Time" DataIndex="Bwb_Time" Text="间隔时间" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column10" DataIndex="mkind_name" Text="原因分类" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column11" DataIndex="ikind_name" Text="详细分类" MenuDisabled="true" />

                            <ext:CommandColumn ID="cmdCol" runat="server" Align="Center" Text="操作" Width="185">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改" />
                                </Commands>
                                <Listeners>
                                    <Command Handler="cmdcol_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                            <View>
                                <ext:GridView runat="server" ID="GridViewCenter">
                                    <GetRowClass Fn="SetRowClass" />
                                </ext:GridView>
                            </View>
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
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>

                <ext:Window ID="winSave" runat="server" Icon="MonitorAdd" Closable="false" Title="间隔时间分类维护" Width="550" Height="500" Resizable="false" Modal="true" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="FormLayout">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5" Layout="FormLayout">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="70" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:Hidden runat="server" ID="hideObjID1" />
                                <ext:Hidden runat="server" ID="hideObjID2" />
                                <ext:FieldSet ID="FieldSet1" runat="server" Title="间隔时间分类维护">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer12" runat="server" Layout="HBoxLayout" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:ComboBox ID="cbxdalei" runat="server" Disabled="false" Width="230" AnchorHorizontal="100%" FieldLabel="原因分类"  Editable="false" AllowBlank="false">
                                                    <DirectEvents>
                                                        <Change OnEvent="cbxdalei_SelectChanged" />
                                                    </DirectEvents>
                                                    </ext:ComboBox>
                                                <ext:ComboBox ID="cbxxiaolei" runat="server" Disabled="false" Width="230" AnchorHorizontal="100%" FieldLabel="详细分类"  DisplayField="Ikind_name" ValueField="Ikind_Code" Editable="false" AllowBlank="false">
                                                    <Store>
                                                        <ext:Store runat="server" ID="storeXiaoLei">
                                                            <Model>
                                                                <ext:Model runat="server" ID="Model5">
                                                                    <Fields>
                                                                        <ext:ModelField Name="Ikind_Code" />
                                                                        <ext:ModelField Name="Ikind_name" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    </ext:ComboBox>
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
                                <Click OnEvent="btnCancel_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:Window>
<%-- 
                <ext:Hidden ID="hidden_equip_code" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hidden_type" runat="server">
                </ext:Hidden>
                <ext:Hidden ID="hidden_EType" runat="server">
                </ext:Hidden>--%>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
