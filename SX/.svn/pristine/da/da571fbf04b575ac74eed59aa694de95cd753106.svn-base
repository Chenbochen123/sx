<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YiChang.aspx.cs" Inherits="Manager_ProducingPlan_YiChang" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>异常原因录入</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell {
            background-color: #CCFF66 !important;
        }
    </style>
    <script type="text/javascript">
        //点击修改按钮
        var cmdcol_click = function (command, record) {
            if (command == "Edit") {
                App.direct.pnlList_Edit(record.data.plan_date,record.data.shift_id, {
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
            var plan_date = record.data.plan_date;
            var shift_id = record.data.shift_id;
            App.direct.pnlList_Delete(plan_date,shift_id, {
                success: function () { },
                failure: function () { }
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnExportSubmit" Style="display: none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
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
                                    <ToolTips>
                                        <ext:ToolTip ID="ttAdd" runat="server" Html="点击进行添加" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btnAdd_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btnSearch_Click">
                                            <EventMask ShowMask="true" Target="Page"></EventMask>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                    <Listeners>
                                        <Click Handler="$('#btnExportSubmit').click();"></Click>
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
                                <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                        <ext:DateField ID="txtdatestart" runat="server" FieldLabel="开始日期" LabelAlign="Left" Editable="false" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                        <ext:DateField ID="txtdateend" runat="server" FieldLabel="结束日期" LabelAlign="Left" Editable="false" />
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
                                <ext:Model ID="model" runat="server" IDProperty="plan_date,shift_id">
                                    <Fields>
                                        <ext:ModelField Name="plan_date" />
                                        <ext:ModelField Name="shift_id" />
                                        <ext:ModelField Name="ShiftName" />
                                        <ext:ModelField Name="remark" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:Column ID="Oil_code" runat="server" Text="日期" DataIndex="plan_date" Width="100" />
                            <ext:Column ID="Oil_name" runat="server" Text="班次" DataIndex="ShiftName" Width="80" />
                            <ext:Column ID="Column1" runat="server" Text="异常情况" DataIndex="remark" Width="200" />
                            <ext:CommandColumn ID="cmdCol" runat="server" Align="Center" Text="操作" Width="185">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改" />
                                    <ext:GridCommand Icon="TableDelete" CommandName="Delete" Text="删除" />
                                </Commands>
                                <Listeners>
                                    <Command Handler="cmdcol_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                        </Columns>
                    </ColumnModel>
                    <BottomBar>
                        <ext:PagingToolbar ID="pageToolBar" runat="server">
                            <Plugins>
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
                <ext:Window ID="winSave" runat="server" Icon="MonitorAdd" Closable="false" Title="异常维护" Width="550" Height="500" Resizable="false" Modal="true" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="FormLayout">
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
                                <ext:FieldSet ID="FieldSet1" runat="server" Title="异常维护">
                                    <Items>
                                        <ext:FieldContainer ID="FieldContainer12" runat="server" Layout="HBoxLayout" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:DateField ID="txtdate1" runat="server" FieldLabel="日期" LabelAlign="Left" Editable="false" />
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer1" runat="server" Layout="HBoxLayout" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:ComboBox ID="cbxShift" runat="server" FieldLabel="班次"
                                                    LabelAlign="Left">
                                                    <Items>
                                                        <ext:ListItem Text="白" Value="1">
                                                        </ext:ListItem>
                                                        <ext:ListItem Text="夜" Value="3">
                                                        </ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:FieldContainer ID="FieldContainer2" runat="server" Layout="HBoxLayout" AnchorHorizontal="100%">
                                            <Items>
                                                <ext:TextField ID="txtremark" runat="server" FieldLabel="异常情况" LabelAlign="Left" Editable="true" />
                                            </Items>
                                        </ext:FieldContainer>
                                        <ext:Hidden runat="server" ID="hideMode" Text="Add" />
                                    </Items>
                                </ext:FieldSet>
                                <ext:Hidden runat="server" ID="hiddentype" />
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

            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
