﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_Equipment_StopManage_StopFault, App_Web_whc5u0u2" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>停机故障点</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <script type="text/javascript">
        //点击修改按钮
        var cmdcol_click = function (command, record) {
            if (command == "Edit") {
                App.direct.pnlList_Edit(command, record.data.ObjID, {
                    success: function () { },
                    failure: function () { }
                });
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="vwUnit" runat="server" Layout="BorderLayout">
            <Items>
                <ext:TreePanel ID="tpType" runat="server" Region="West" Title="停机类型" Width="200" RootVisible="false" Icon="ApplicationSideTree">
                    <BottomBar>
                        <ext:Toolbar runat="server" ID="Toolbar1">
                            <Items>
                                <ext:ToolbarFill />
                                <ext:Button ID="Button1" runat="server" Text="刷新">
                                    <DirectEvents><Click OnEvent="tpType_Refresh" /></DirectEvents>
                                </ext:Button>
                                <ext:ToolbarSeparator />
                                <ext:ToolbarFill />
                            </Items>
                        </ext:Toolbar>
                    </BottomBar>
                    <Store>
                        <ext:TreeStore ID="ts_01" runat="server" OnReadData="tpType_ReadData">
                            <Proxy>
                                <ext:PageProxy>
                                    <RequestConfig Method="GET" Type="Load" />
                                </ext:PageProxy>
                            </Proxy>
                            <Root>
                                <ext:Node NodeID="Root" />
                            </Root>
                        </ext:TreeStore>
                    </Store>
                    <DirectEvents>
                        <SelectionChange OnEvent="tp_SelectedChange" />
                    </DirectEvents>
                </ext:TreePanel>
                <ext:Panel runat="server" Region="North" AutoHeight="true">
                    <Items>
                        <ext:Panel ID="pnlStopFault" runat="server" Region="East" AutoHeight="true">
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
                                            <DirectEvents><Click OnEvent="btnSearch_Click"/></DirectEvents>
                                        </ext:Button>
                                        <ext:ToolbarSeparator />
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Items>
                                <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                    <Defaults>
                                        <ext:Parameter Name="Padding" Value="5" />
                                    </Defaults>
                                    <Items>
                                        <ext:TextField ID="tfStopFaultName" runat="server" FieldLabel="故障点名称" LabelAlign="Right">
                                            <ToolTips>
                                                <ext:ToolTip ID="ToolTip1" runat="server" Html="模糊查询"></ext:ToolTip>
                                            </ToolTips>
                                        </ext:TextField>
                                    </Items>
                                </ext:FormPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="pnlList" runat="server" Region="Center">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="15" OnReadData="refreshList">
                            <Model>
                                <ext:Model ID="model" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="TypeID" />
                                        <ext:ModelField Name="TypeName" />
                                        <ext:ModelField Name="FaultCode" />
                                        <ext:ModelField Name="FaultName" />
                                        <ext:ModelField Name="DeleteFlag" />
                                        <ext:ModelField Name="DeleteName" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35"/>
                            <ext:Column ID="clObjID" runat="server" Text="编号" DataIndex="ObjID" Width="50"/>
                            <ext:Column ID="clTypeName" runat="server" Text="停机类型" DataIndex="TypeName" Width="150"/>
                            <ext:Column ID="clFaultCode" runat="server" Text="故障点代码" DataIndex="FaultCode" Width="90"/>
                            <ext:Column ID="clFaultName" runat="server" Text="故障点名称" DataIndex="FaultName" Width="235"/>
                            <ext:Column ID="clDeleteName" runat="server" Text="停用" DataIndex="DeleteName" Width="50" Align="Center"/><%----%>
                            <ext:CommandColumn ID="cmdCol" runat="server" Align="Center" Text="操作" Width="60">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改"/>
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
                                <ext:ProgressBarPager ID="ProgressBarPager" runat="server"/>
                            </Plugins>
                        </ext:PagingToolbar>
                    </BottomBar>
                </ext:GridPanel>
                <ext:Window ID="winSave" runat="server" Icon="MonitorAdd" Closable="false" Title="维护停机故障点"
                    Width="360" Height="240" Resizable="false" Modal="true" BodyStyle="background-color:#fff;"
                    BodyPadding="5" Layout="FormLayout">
                    <Items>
                        <ext:FormPanel ID="pnlAdd" runat="server" BodyPadding="5">
                             <FieldDefaults>
                                <CustomConfig>
                                    <ext:ConfigItem Name="LabelWidth" Value="80" Mode="Raw" />
                                    <ext:ConfigItem Name="PreserveIndicatorIcon" Value="true" Mode="Raw" />
                                </CustomConfig>
                            </FieldDefaults>
                            <Items>
                                <ext:TextField ID="tfObjID" runat="server" FieldLabel="故障点编号" LabelAlign="Right" Disabled="true" EmptyText="自动生成" AllowBlank="true"/>
                                <ext:TextField ID="tfFaultCode" runat="server" FieldLabel="故障点代码" LabelAlign="Right" Disabled="true" EmptyText="自动生成" AllowBlank="true"/>
                                <ext:TextField ID="tfFaultName" runat="server" FieldLabel="故障点名称" AllowBlank="false" LabelAlign="Right" MaxLength="120"/>
                                <ext:TextField ID="tfTypeName" runat="server" FieldLabel="停机类型" LabelAlign="Right" Disabled="true"/>
                                <ext:ComboBox ID="cbDelete" runat="server" FieldLabel="停用" DisplayField="ItemName" ValueField="ItemCode" Editable="false" LabelAlign="Right" AllowBlank="false">
                                    <Store>
                                        <ext:Store runat="server" ID="storeDelete">
                                            <Model>
                                                <ext:Model runat="server" ID="Model2">
                                                    <Fields>
                                                        <ext:ModelField Name="ItemCode" />
                                                        <ext:ModelField Name="ItemName" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Triggers>
                                        <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                    </Triggers>
                                </ext:ComboBox>
                                <ext:Hidden runat="server" ID="hideTypeID" Text="" />
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
