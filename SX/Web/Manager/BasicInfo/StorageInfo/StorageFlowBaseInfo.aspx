<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StorageFlowBaseInfo.aspx.cs" Inherits="Manager_BasicInfo_StorageFlowBaseInfo_StorageFlowBaseInfo" %>
<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var ObjID = record.data.ObjID;
            App.direct.commandcolumn_direct_edit(ObjID, {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //点击删除按钮
        var commandcolumn_direct_delete = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var ObjID = record.data.ObjID;
            App.direct.commandcolumn_direct_delete(ObjID, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        //区分删除操作，并进行二次确认操作
        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
            }
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此条信息？', function (btn) { commandcolumn_direct_delete(btn, record) });
            }
            return false;
        };

        //根据按钮类别进行删除和编辑操作
        var commandcolumn_click = function (command, record) {
            commandcolumn_click_confirm(command, record);
            return false;
        };

        Ext.apply(Ext.form.VTypes, {
            integer: function (val, field) {
                if (!val) {
                    return true;
                }
                try {
                    if (/^[\d]+$/.test(val)) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                catch (e) {
                    return false;
                }
            },
            integerText: "此填入项格式为正整数"
        });


        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="rmStorageFlow" runat="server" />
        <ext:Viewport ID="vpStorageFlow" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnStorageFlowTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="tbStorageFlow">
                            <Items>
                                <ext:Button runat="server" Icon="Add" Text="添加" ID="btnAdd">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttAdd" runat="server" Html="点击进行添加" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btnAdd_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsBegin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="pnlListFresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsEnd" />
                                <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                                <ext:ToolbarFill ID="tfEnd" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>

                    <Items>
                        <ext:Panel ID="pnlStorageQuery" runat="server" AutoHeight="true">
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Items>
                                        <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                            <Items>
                                                <ext:TextField ID="txtSourceStorage" runat="server" FieldLabel="来源库房" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                            <Items>
                                                <ext:TextField ID="txtTargetStorage" runat="server" FieldLabel="目的库房" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container5" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                            <Items>
                                                <ext:ComboBox ID="cbxForbiddenFlag" runat="server" FieldLabel="是否禁用" LabelAlign="Right" ColumnWidth="0.25" Padding="5">
                                                    <SelectedItems>
                                                        <ext:ListItem Value="all"></ext:ListItem>
                                                    </SelectedItems>
                                                    <Items>
                                                        <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                                        <ext:ListItem Text="是" Value="1"></ext:ListItem>
                                                        <ext:ListItem Text="否" Value="0"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container> 
                                    </Items>
                                    <Listeners>
                                        <ValidityChange Handler="#{btnSearch}.setDisabled(!valid);" />
                                    </Listeners>
                                </ext:FormPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="10"> 
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="SourceStorage" />
                                        <ext:ModelField Name="TargetStorage" />
                                        <ext:ModelField Name="LimitTimes" />
                                        <ext:ModelField Name="ForbiddenFlag" />
                                        <ext:ModelField Name="Remark" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                            <ext:Column ID="objID" runat="server" Text="流程编号" DataIndex="ObjID" Flex="1" />
                            <ext:Column ID="sourceStorage" runat="server" Text="来源库房" DataIndex="SourceStorage" Flex="1" />
                            <ext:Column ID="targetStorage" runat="server" Text="目的库房" DataIndex="TargetStorage" Flex="1" />
                            <ext:Column ID="limitTimes" runat="server" Text="限制时间" DataIndex="LimitTimes" Flex="1" />
                            <ext:CheckColumn ID="forbiddenFlag" runat="server" Text="是否默认库位" DataIndex="ForbiddenFlag" Flex="1" />
                            <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Flex="1" />
                            <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                        <ToolTip Text="修改本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                        <ToolTip Text="删除本条数据" />
                                    </ext:GridCommand>
                                </Commands>
                                <Listeners>
                                    <Command Handler="return commandcolumn_click(command, record);" />
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
                <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改流程设置信息"
                    Width="320" Height="380" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items> 
                        <ext:FormPanel ID="pnlEdit" runat="server" Flex="1" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="txtObjID1" runat="server" FieldLabel="流程编号" LabelAlign="Left" ReadOnly="true" Hidden="true" Enabled="true" />
                                <ext:TextField ID="txtSourceStorage1" runat="server" FieldLabel="来源库房" AllowBlank="false" LabelAlign="Left" IsRemoteValidation="true" >
                                     <RemoteValidation OnValidation="CheckField" />
                                </ext:TextField>
                                <ext:TextField ID="txtTargetStorage1" runat="server" FieldLabel="目的库房" AllowBlank="false" LabelAlign="Left" IsRemoteValidation="true" >
                                     <RemoteValidation OnValidation="CheckField" />
                                </ext:TextField>
                                <ext:TextField ID="txtLimitTimes1" runat="server" FieldLabel="限制时间" AllowBlank="false" LabelAlign="Left" IsRemoteValidation="true" >
                                     <RemoteValidation OnValidation="CheckField" />
                                </ext:TextField>
                                <ext:Checkbox ID="chkForbiddenFlag1" runat="server" FieldLabel="是否禁止使用" LabelAlign="Left" />
                                <ext:TextField ID="txtRemark1" runat="server" FieldLabel="备注" LabelAlign="Left" />
                            </Items>
                             <Listeners>
                                <ValidityChange Handler="#{btnModify}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnModify" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="btnModify_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:Window>
                <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加库位信息" Width="320" Height="380" 
                    Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;" BodyPadding="5" Layout="Form">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="txtSourceStorage2" runat="server" FieldLabel="来源库房" AllowBlank="false" LabelAlign="Left" IsRemoteValidation="true" >
                                     <RemoteValidation OnValidation="CheckField" />
                                </ext:TextField>
                                <ext:TextField ID="txtTargetStorage2" runat="server" FieldLabel="目的库房" LabelAlign="Left" />
                                <ext:TextField ID="txtLimitTimes2" runat="server" FieldLabel="限制时间" LabelAlign="Left" />
                                <ext:Checkbox ID="chkForbiddenFlag2" runat="server" Checked="true" FieldLabel="是否默认库位" LabelAlign="Left" />
                                <ext:TextField ID="txtRemark2" runat="server" FieldLabel="备注" LabelAlign="Left" />
                            </Items>
                            <Listeners>
                                <ValidityChange Handler="#{btnAddSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                     <Buttons>
                        <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                            <DirectEvents>
                                <Click OnEvent="btnAddSave_Click">
                                    <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="btnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:Window>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
