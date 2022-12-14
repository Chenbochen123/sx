<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BalanceCheck.aspx.cs" Inherits="Manager_ProducingPlan_BalanceCheck" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>校秤记录查询</title>
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

        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var serialid = record.data.serialid;
            App.direct.commandcolumn_direct_edit(serialid, {
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
            var serialid = record.data.serialid;
            App.direct.commandcolumn_direct_delete(serialid, {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }


    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <%--<asp:Button ID="btnExportSubmit" runat="server" Text="Button" OnClick="btnExportSubmit_Click" Style="display:none" />--%>
      <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
        <Items>
        <ext:Hidden ID="hidden_update_barcode" runat="server">
        </ext:Hidden>
            
                <ext:Panel ID="pnlStopType" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="barStopType">
                            <Items>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <ToolTips><ext:ToolTip ID="ToolTip2" runat="server" Html="点击进行查询" /></ToolTips>
                                    <DirectEvents><Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page"></EventMask>
                                    </Click></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="Add" Text="新增" ID="btnAdd">
                                    <DirectEvents>
                                        <Click OnEvent="btnAdd_Click">
                                        </Click>
                                    </DirectEvents>
                                    <ToolTips>
                                        <ext:ToolTip runat="server" Html="新增" ID="ctl350" />
                                    </ToolTips>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                    <ToolTips><ext:ToolTip ID="ToolTip3" runat="server" Html="点击进行结果导出" /></ToolTips>
                                    <DirectEvents>
                                    <Click OnEvent="btnExportSubmit_Click" IsUpload="true">
                                        <ExtraParams>
                                            <ext:Parameter Name="fields" Value="#{model}.getFields()" Mode="Raw" />
                                            <ext:Parameter Name="records" Value="#{store}.getRecordsValues({ excludeId: true })"
                                                Mode="Raw" />
                                        </ExtraParams>
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
                                <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                        <ext:DateField ID="datestart" runat="server" Disabled="false" Width="300" AnchorHorizontal="100%" FieldLabel="开始时间" />
                                    </Items>
                                </ext:Container>
                              
                                <ext:Container ID="container4" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                    Padding="5">
                                    <Items>
                                        <ext:DateField ID="dateend" runat="server" Disabled="false" Width="300" AnchorHorizontal="100%" FieldLabel="结束时间" />
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
                            <ext:DataSorter Property="serialid" />
                        </Sorters>
                            <Model>
                                <ext:Model ID="model" runat="server"  IDProperty="serialid">
                                    <Fields>
                                        <ext:ModelField Name="生产日期" />
                                        <ext:ModelField Name="机台"/>
                                        <ext:ModelField Name="班组"/>
                                        <ext:ModelField Name="秤名称"/>
                                        <ext:ModelField Name="编号"/>
                                        <ext:ModelField Name="标准重量"/>
                                        <ext:ModelField Name="允许误差"/>
                                        <ext:ModelField Name="实际重量"/>
                                        <ext:ModelField Name="校准时间" type="Date"/>
                                        <ext:ModelField Name="校准人"/>
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
                            
                            <ext:Column runat="server" ID="Plan_date" DataIndex="生产日期" Text="生产日期" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column2" DataIndex="机台" Text="机台" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column1" DataIndex="班组" Text="班组" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column8" DataIndex="秤名称" Text="秤名称" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column9" DataIndex="编号" Text="编号" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column10" DataIndex="标准重量" Text="标准重量" MenuDisabled="true" />
                            <ext:Column runat="server" ID="Column11" DataIndex="允许误差" Text="允许误差" MenuDisabled="true"/>
                            <ext:Column runat="server" ID="Column3" DataIndex="实际重量" Text="实际重量" MenuDisabled="true" />
                            <ext:DateColumn runat="server" ID="Column12" DataIndex="校准时间" Text="校准时间" MenuDisabled="true" Format="yyyy-MM-dd HH:mm:ss"/>
                            <ext:Column runat="server" ID="Column4" DataIndex="校准人" Text="校准人" MenuDisabled="true" />
                            <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                        <ToolTip Text="修改本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
                                    <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除">
                                        <ToolTip Text="删除本条数据" />
                                    </ext:GridCommand>
                                    <ext:CommandSeparator />
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
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server"/>
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
                </Items>
    </ext:Viewport>
         <ext:Window ID="AddConfigWin" runat="server" Icon="MonitorAdd" Closable="false" Title="添加校秤记录"
            Width="580" Height="480" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
            BodyPadding="5" Layout="Form">
            <Items>
                <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5">
                    <FieldDefaults>
                        <CustomConfig>
                            <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                            <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                        </CustomConfig>
                    </FieldDefaults>
                    <Items>
                        <ext:Container ID="Container2" runat="server" AutoHeight="true">
                            <Items>
                                        <ext:Container ID="Container3" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                            <Items>
                                                <ext:DateField ID="txtdate" runat="server" Flex="1" Editable="false" Vtype="daterange"
                                                    FieldLabel="生产日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd"/>
                                                <ext:ComboBox ID="cbxclass" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"
                                                    runat="server" Flex="1" Editable="false" FieldLabel="班组" LabelAlign="Right"/>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container114" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                            <Items>
                                                <ext:ComboBox ID="cbxequip" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"
                                                    runat="server" Flex="1" Editable="false" FieldLabel="机台" LabelAlign="Right"/>
                                                <ext:TextField ID="txtcheng" Flex="1" FieldLabel="秤名称" LabelAlign="Right" Disabled="false"
                                                    runat="server">
                                                </ext:TextField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container6" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                            <Items>
                                                <ext:TextField ID="txtno" Flex="1" FieldLabel="编号" LabelAlign="Right" Disabled="false"
                                                    runat="server">
                                                </ext:TextField>
                                                <ext:TextField ID="txtbiaozhun" Flex="1" FieldLabel="标准重量" LabelAlign="Right" Disabled="false"
                                                    runat="server"  AllowBlank="false" IndicatorText="*" IndicatorCls="red-text">
                                                </ext:TextField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container5" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                            <Items>
                                                <ext:TextField ID="txtwucha" Flex="1" FieldLabel="允许误差" LabelAlign="Right" Disabled="false"
                                                    runat="server"  AllowBlank="false" IndicatorText="*" IndicatorCls="red-text">
                                                </ext:TextField>
                                                <ext:TextField ID="txtshiji" Flex="1" FieldLabel="实际重量" LabelAlign="Right" Disabled="false"
                                                    runat="server"  AllowBlank="false" IndicatorText="*" IndicatorCls="red-text">
                                                </ext:TextField>
                                            </Items>
                                        </ext:Container>
                            </Items>
                        </ext:Container>
                    </Items>
                    <Listeners>
                        <ValidityChange Handler="#{btnAddSave}.setDisabled(!valid);" />
                    </Listeners>
                </ext:FormPanel>
            </Items>
            <Buttons>
                <ext:Button ID="btnAddSave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                    <DirectEvents>
                        <Click OnEvent="BtnAddSave_Click">
                            <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="btnAddCancel" runat="server" Text="取消" Icon="Cancel">
                    <DirectEvents>
                        <Click OnEvent="BtnCancel_Click">
                        </Click>
                    </DirectEvents>
                </ext:Button>
            </Buttons>
            <Listeners>
                <Show Handler="for(var i=0;i<#{Viewport1}.items.length;i++){#{Viewport1}.getComponent(i).disable(true);}" />
                <Hide Handler="for(var i=0;i<#{Viewport1}.items.length;i++){#{Viewport1}.getComponent(i).enable(true);}" />
            </Listeners>
        </ext:Window>

           <ext:Window ID="ModifyConfigWin" runat="server" Icon="MonitorAdd" Closable="false" Title="修改校秤记录"
            Width="580" Height="480" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
            BodyPadding="5" Layout="Form">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" BodyPadding="5">
                    <FieldDefaults>
                        <CustomConfig>
                            <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                            <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                        </CustomConfig>
                    </FieldDefaults>
                    <Items>
                        <ext:Container ID="Container7" runat="server" AutoHeight="true">
                            <Items>
                                        <ext:Container ID="Container8" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                            <Items>
                                                <ext:DateField ID="txtdate2" runat="server" Flex="1" Editable="false" Vtype="daterange"
                                                    FieldLabel="生产日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd"/>
                                                <ext:ComboBox ID="cbxclass2" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"
                                                    runat="server" Flex="1" Editable="false" FieldLabel="班组" LabelAlign="Right"/>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container9" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                            <Items>
                                                <ext:ComboBox ID="cbxequip2" AllowBlank="false" IndicatorText="*" IndicatorCls="red-text"
                                                    runat="server" Flex="1" Editable="false" FieldLabel="机台" LabelAlign="Right"/>
                                                <ext:TextField ID="txtcheng2" Flex="1" FieldLabel="秤名称" LabelAlign="Right" Disabled="false"
                                                    runat="server">
                                                </ext:TextField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container10" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                            <Items>
                                                <ext:TextField ID="txtno2" Flex="1" FieldLabel="编号" LabelAlign="Right" Disabled="false"
                                                    runat="server">
                                                </ext:TextField>
                                                <ext:TextField ID="txtbiaozhun2" Flex="1" FieldLabel="标准重量" LabelAlign="Right" Disabled="false"
                                                    runat="server"  AllowBlank="false" IndicatorText="*" IndicatorCls="red-text">
                                                </ext:TextField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container11" runat="server" Layout="HBoxLayout" MarginSpec="5 5 5 5">
                                            <Items>
                                                <ext:TextField ID="txtwucha2" Flex="1" FieldLabel="允许误差" LabelAlign="Right" Disabled="false"
                                                    runat="server"  AllowBlank="false" IndicatorText="*" IndicatorCls="red-text">
                                                </ext:TextField>
                                                <ext:TextField ID="txtshiji2" Flex="1" FieldLabel="实际重量" LabelAlign="Right" Disabled="false"
                                                    runat="server"  AllowBlank="false" IndicatorText="*" IndicatorCls="red-text">
                                                </ext:TextField>
                                            </Items>
                                        </ext:Container>
                            </Items>
                        </ext:Container>
                    </Items>
                    <Listeners>
                        <ValidityChange Handler="#{btnModifySave}.setDisabled(!valid);" />
                    </Listeners>
                </ext:FormPanel>
            </Items>
            <Buttons>
                <ext:Button ID="btnModifySave" runat="server" Text="确定" Icon="Accept" Disabled="true">
                    <DirectEvents>
                        <Click OnEvent="BtnModifySave_Click">
                            <EventMask ShowMask="true" Msg="Saving..." MinDelay="50" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                    <DirectEvents>
                        <Click OnEvent="BtnModifyCancel_Click">
                        </Click>
                    </DirectEvents>
                </ext:Button>
            </Buttons>
            <Listeners>
                <Show Handler="for(var i=0;i<#{Viewport1}.items.length;i++){#{Viewport1}.getComponent(i).disable(true);}" />
                <Hide Handler="for(var i=0;i<#{Viewport1}.items.length;i++){#{Viewport1}.getComponent(i).enable(true);}" />
            </Listeners>
        </ext:Window>
    </form>
</body>
</html>
