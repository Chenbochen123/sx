<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WorkerSet.aspx.cs" Inherits="Manager_ProducingPlan_WorkerSet" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>应出勤人数设定</title>
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
                App.direct.pnlList_Edit(record.data.plan_date, record.data.shiftname, {
                    success: function () { },
                    failure: function () { }
                });
            }
            //else if (command == "Delete") {
            //    Ext.Msg.confirm("提示", '您确定需要删除此信息？删除后将不能恢复！', function (btn) { deleteStopRecord(btn, record) });
            //}
        }

        //var deleteStopRecord = function (btn, record) {
        //    if (btn != "yes") {
        //        return;
        //    }
        //    var Oil_code = record.data.Oil_code;
        //    App.direct.pnlList_Delete(Oil_code, {
        //        success: function () { },
        //        failure: function () { }
        //    });
        //}

    </script>
</head>
<body>
    <form id="form1" runat="server">
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
                                        <ext:DateField ID="datetime" runat="server" Disabled="false" Width="300" AnchorHorizontal="100%" FieldLabel="日期" Format="yyyy-MM-dd"/>
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
                                <ext:Model ID="model" runat="server" IDProperty="plan_date,shiftname">
                                    <Fields>
                                        <ext:ModelField Name="plan_date" />
                                        <ext:ModelField Name="shiftname" />
                                        <ext:ModelField Name="setNum" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:Column ID="plan_date" runat="server" Text="日期" DataIndex="plan_date" Width="100"/>
                            <ext:Column ID="Column2" runat="server" Text="班次" DataIndex="shiftname" Width="80"/>
                            <ext:Column ID="Column1" runat="server" Text="人数" DataIndex="setNum" Width="80"/>
                            <ext:CommandColumn ID="cmdCol" runat="server" Align="Center" Text="操作" Width="185">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改"/>
                                    <%--<ext:GridCommand Icon="TableDelete" CommandName="Delete" Text="删除"/>--%>
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
                <ext:Window ID="winSave" runat="server" Icon="MonitorAdd" Closable="false" Title="人员维护" Width="550" Height="500" Resizable="false" Modal="true" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="FormLayout">
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
                                <ext:Hidden runat="server" ID="hideObjID1" />
                                <ext:Hidden runat="server" ID="hideObjID2" />
                                <ext:Hidden runat="server" ID="hideObjID3" />
                                <ext:FieldSet ID="FieldSet1" runat="server" Title="人员维护">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer12" runat="server" Layout="HBoxLayout" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:DateField ID="date1" runat="server" Disabled="false" Width="300" AnchorHorizontal="100%" FieldLabel="日期" Format="yyyy-MM-dd"/>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer1" runat="server" Layout="HBoxLayout" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:ComboBox ID="cbxshift" runat="server" FieldLabel="班次" LabelAlign="left" Width="300"
                                                    Editable="false">
                                                    <Items>
                                                        <ext:ListItem Text="白" Value="1">
                                                        </ext:ListItem>
                                                        <%--<ext:ListItem Text="中" Value="2">
                                                        </ext:ListItem>--%>
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
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer2" runat="server" Layout="HBoxLayout" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtnum" runat="server" Width="300" AllowBlank="false" FieldLabel="人数" />
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

            </Items>
        </ext:Viewport>
    </form>
</body>
</html>