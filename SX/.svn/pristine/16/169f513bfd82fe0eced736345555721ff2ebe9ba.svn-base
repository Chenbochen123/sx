<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Standard.aspx.cs" Inherits="Manager_RawMaterialQuality_Standard" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>执行标准维护</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript" language="javascript">
        //列表刷新数据重载方法
        var pnlCenterRefresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            if (record != null) {
                var StandardId = record.data.StandardId;
                App.direct.commandcolumn_direct_edit(StandardId, {
                    success: function (result) {
                    },

                    failure: function (errorMsg) {
                        Ext.Msg.alert('操作', errorMsg);
                    }
                });
            }
        }

        //点击删除按钮
        var commandcolumn_direct_delete = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            var StandardId = record.data.StandardId;
            App.direct.commandcolumn_direct_delete(StandardId, {
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="rmStandard" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnlNorth" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barUnit1">
                            <Items>
                                <ext:Button runat="server" Icon="Add" Text="新建执行标准" ID="btnAdd">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttAdd" runat="server" Html="点击新建一个执行标准" />
                                    </ToolTips>
                                    <DirectEvents>
                                        <Click OnEvent="btn_add_Click">
                                            <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="点击进行查询" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="pnlCenterRefresh"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end1" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end1" />
                                <ext:ToolbarFill ID="toolbarFill_end1" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:FormPanel runat="server" ID="pnlForm" Layout="AnchorLayout" AutoHeight="true">
                            <Items>
                                <ext:Container ID="container1" runat="server" Layout="HBoxLayout" Padding="5" >
                                    <Items>   
                                        <ext:TextField ID="txtCurrentStandard" runat="server" FieldLabel="当前执行标准" LabelAlign="Right"
                                            Visible="true"  LabelWidth="80" ReadOnly="true">
                                        </ext:TextField>                                                                                   
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="pnlCenter" runat="server" Region="Center" Frame="true" Layout="Fit"
                    MarginsSummary="0 5 0 5">
                    <Items>
                        <ext:GridPanel ID="pnlStandard" runat="server" MarginsSummary="0 5 5 5">
                          <Store>
                               <ext:Store ID="store" runat="server" PageSize="10">
                                    <Proxy>
                                        <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="model" runat="server" IDProperty="StandardId">
                                            <Fields>
                                            <ext:ModelField Name="StandardId" />
                                            <ext:ModelField Name="StandardName" />
                                            <ext:ModelField Name="CreatorId" />
                                            <ext:ModelField Name="Username" />
                                            <ext:ModelField Name="ActivateDate" Type="Date"/>
                                            <ext:ModelField Name="Remark" />
                                            <ext:ModelField Name="ActivateFlag" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                               </ext:Store>
                          </Store>
                          <ColumnModel ID="colModel1" runat="server">
                              <Columns>
                                  <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />
                                  <ext:Column ID="standardId" runat="server" Text="执行标准ID" DataIndex="StandardId" Visible="false"/>
                                  <ext:Column ID="creatorId" runat="server" Text="创建人ID" DataIndex="CreatorId" Visible="false"/>
                                  <ext:Column ID="standardName" runat="server" Text="执行标准" DataIndex="StandardName" Flex="1"/>
                                  <ext:Column ID="username" runat="server" Text="创建人" DataIndex="Username" Flex="1"/>
                                  <ext:DateColumn ID="activateDate" runat="server" Text="生效日期" DataIndex="ActivateDate" Flex="1" Format="yyyy-MM-dd"/>
                                  <ext:Column ID="remark" runat="server" Text="备注" DataIndex="Remark" Flex="1"/>
                                  <ext:Column ID="activateFlag" runat="server" Text="状态" DataIndex="ActivateFlag" Flex="1"/>
                                  <ext:CommandColumn ID="commandCol1" runat="server" Width="130" Text="操作" Align="Center">
                                        <Commands>
                                            <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改" >
                                                <ToolTip Text="修改本条数据" />
                                            </ext:GridCommand>
                                            <ext:CommandSeparator />
                                            <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除" >
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
                                            <Items>
                                                <ext:Label ID="Label1" runat="server" Text="每页条数:" />
                                                <ext:ToolbarSpacer ID="ToolbarSpacer1" runat="server" Width="10" />
                                                <ext:ComboBox ID="ComboBox1" runat="server" Width="80" Editable="false">
                                                    <Items>
                                                        <ext:ListItem Text="10" />
                                                        <ext:ListItem Text="50" />
                                                        <ext:ListItem Text="100" />
                                                        <ext:ListItem Text="200" />
                                                    </Items>
                                                    <SelectedItems>
                                                        <ext:ListItem Value="10" />
                                                    </SelectedItems>
                                                    <Listeners>
                                                        <Select Handler="#{pnlStandard}.store.pageSize = parseInt(this.getValue(), 10); #{pageToolBar}.doRefresh(); return false;" />
                                                    </Listeners>
                                                </ext:ComboBox>
                                            </Items>
                                            <Plugins>
                                                <ext:ProgressBarPager ID="ProgressBarPager1" runat="server" />
                                            </Plugins>
                                </ext:PagingToolbar>
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
                <ext:Window ID="windowModifyStandard" runat="server" Icon="MonitorEdit" Closable="false" Title="修改执行标准"
                    Width="350" Height="167" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items> 
                        <ext:FormPanel ID="pnlModifyStandard" runat="server" Flex="1" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="Width" Value="280" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="txtModifyStandardId" runat="server" FieldLabel="执行标准ID"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                                <ext:TextField ID="txtModifyCreatorId" runat="server" FieldLabel="创建人ID"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />           
                                <ext:TextField ID="txtModifyStandardName" runat="server" FieldLabel="标准名称" LabelAlign="Right" MaxLength="50" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" />
                                <ext:DateField ID="dtfModifyActivateDate" runat="server" FieldLabel="生效日期" LabelAlign="Right" Editable="false" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" />
                                <ext:TextField ID="txtModifyRemark" runat="server" FieldLabel="备注" LabelAlign="Right" MaxLength="50" Width="273"/>
                            </Items>
                                <Listeners>
                                <ValidityChange Handler="#{btnModifyStandardSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnModifyStandardSave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="BtnModifyStandardSave_Click">
                                    <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnModifyStandardCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="BtnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Window ID="windowAddStandard" runat="server" Icon="MonitorEdit" Closable="false" Title="新建执行标准"
                    Width="350" Height="190" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="Form">
                    <Items> 
                        <ext:FormPanel ID="pnlAddStandard" runat="server" Flex="1" BodyPadding="5">
                            <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="Width" Value="280" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="txtAddStandardId" runat="server" FieldLabel="执行标准ID"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />
                                <ext:TextField ID="txtAddCreatorId" runat="server" FieldLabel="创建人ID"  LabelAlign="Right" ReadOnly="true" Hidden="true" Enabled="true" />           
                                <ext:TextField ID="txtAddStandardName" runat="server" FieldLabel="标准名称" LabelAlign="Right" MaxLength="50" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" />
                                <ext:DateField ID="dtfAddActivateDate" runat="server" FieldLabel="生效日期" LabelAlign="Right" Editable="false" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text" />
                                <ext:TextField ID="txtAddRemark" runat="server" FieldLabel="备注" LabelAlign="Right" MaxLength="50" Width="273"/>
                                <ext:Checkbox  ID="cbxDoCopy" runat="server" FieldLabel="复制当前指标" LabelAlign="Right" Checked ="true" />
                            </Items>
                                <Listeners>
                                <ValidityChange Handler="#{btnAddStandardSave}.setDisabled(!valid);" />
                            </Listeners>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnAddStandardSave" runat="server" Text="确定" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="BtnAddStandardSave_Click">
                                    <EventMask ShowMask="true" Msg="操作中，请稍候…"/>
                                </Click>
                            </DirectEvents>                  
                        </ext:Button>
                        <ext:Button ID="btnAddStandardCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="BtnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                    <Listeners>
                        <Show Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).disable(true);}" />
                        <Hide Handler="for(var i=0;i<#{vwUnit}.items.length;i++){#{vwUnit}.getComponent(i).enable(true);}" />
                    </Listeners>
                </ext:Window>
                <ext:Hidden ID="txtHiddenSelectCommand" runat="server" />
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
