<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShiftimeRule.aspx.cs" Inherits="Manager_ProducingPlan_ShiftSeting_ShiftimeRule" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        var validateSave = function () {
            App.direct.InitSetTime({
                success: function (result) {

                    if (result.valid) {
                        Ext.Msg.alert("Error", result.msg);
                        return;
                    }
                }
            });
        };
        var invoking = function () {
            App.direct.InvokingTime({
                success: function (result) {

                    if (result.valid) {
                        Ext.Msg.alert("Error", result.msg);
                        return;
                    }
                }
            });

        };
    </script>
    <script type="text/javascript">
        var close = function () {
            parent.App.ShiftimeRuleWin.close();
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Store ID="ShiftClassStore" runat="server">
        <Model>
            <ext:Model ID="Model4" runat="server">
                <Fields>
                    <ext:ModelField Name="ClassName" Type="String" Mapping="Text" />
                </Fields>
            </ext:Model>
        </Model>
    </ext:Store>
    <ext:Viewport ID="Viewport2" runat="server" Layout="border">
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" Layout="ColumnLayout" AutoHeight="true"
                Region="North">
                <Items>
                    <ext:Container ID="container6" runat="server" Layout="FormLayout" ColumnWidth=".33"
                        Padding="5">
                        <Items>
                            <ext:ComboBox ID="cbo_dept1" runat="server" AllowBlank="false" Editable="false" FieldLabel="工序"
                                LabelAlign="Right">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                </Triggers>
                                <Listeners>
                                    <Select Handler=" #{sShiftRule}.reload(); #{sSetTime}.reload();" />
                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                    <TriggerClick Handler="if (index == 0) { 
                                           this.clearValue(); 
                                           this.getTrigger(0).hide();
                                       }" />
                                </Listeners>
                                <SelectedItems>
                                    <ext:ListItem Value="1" />
                                </SelectedItems>
                            </ext:ComboBox>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="container7" runat="server" Layout="FormLayout" ColumnWidth=".33"
                        Padding="5">
                        <Items>
                            <ext:NumberField ID="txtWeekNum" LabelAlign="Right" runat="server" FieldLabel="周期天数"
                                MinValue="0" MaxValue="30">
                            </ext:NumberField>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="container11" runat="server" Layout="FormLayout" ColumnWidth=".33"
                        Padding="5">
                        <Items>
                            <ext:ComboBox ID="cboDiaoYong" runat="server" Editable="false" FieldLabel="调用工序"
                                LabelAlign="Right">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                </Triggers>
                                <Listeners>
                                    <Select Handler="this.getTrigger(0).show();" />
                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                    <TriggerClick Handler="if (index == 0) { 
                                                                           this.clearValue(); 
                                                                           this.getTrigger(0).hide();
                                                                       }" />
                                </Listeners>
                            </ext:ComboBox>
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button ID="Button1" runat="server" Text="初始化">
                        <Listeners>
                            <Click Fn="validateSave" />
                        </Listeners>
                    </ext:Button>
                    <ext:Button ID="Button3" runat="server" Text="加载调用" >
                        <Listeners>
                            <Click Fn="invoking" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
                <Listeners>
                    <%--<ValidityChange Handler="#{btnSearch}.setDisabled(!valid);" />--%>
                </Listeners>
            </ext:FormPanel>
            <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" Hidden="true"
                AutoHeight="true">
                <Items>
                    <ext:Container ID="Container4" runat="server" Layout="FormLayout" ColumnWidth=".33"
                        Padding="5">
                        <Items>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container5" runat="server" Layout="FormLayout" ColumnWidth=".33"
                        Padding="5">
                        <Items>
                            <ext:TextField ID="cboEquip" runat="server" FieldLabel="机台" LabelAlign="Right" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                        Padding="5">
                        <Items>
                            <ext:Button ID="Button2" runat="server" Text="调用">
                                <Listeners>
                                    <Click Fn="validateSave" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:FormPanel>
            <ext:Panel ID="Panel12" runat="server" Layout="Fit" Region="Center" AutoHeight="true">
                <Items>
                    <ext:Container ID="Container8" runat="server" Layout="ColumnLayout">
                        <Items>
                            <ext:Container ID="Container9" runat="server" Layout="FormLayout" ColumnWidth=".6"
                                Padding="5">
                                <Items>
                                    <ext:GridPanel ID="gpSetTime" runat="server" AutoHeight="true" Title="班次">
                                        <Store>
                                            <ext:Store ID="sSetTime" runat="server" PageSize="20" OnReadData="SetTimeRefresh">
                                                <Model>
                                                    <ext:Model ID="Model2" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="ShiftID" Type="String" />
                                                            <ext:ModelField Name="ShiftName" Type="String" />
                                                            <ext:ModelField Name="StartTime" Type="String" />
                                                            <ext:ModelField Name="StopTime" Type="String" />
                                                            <ext:ModelField Name="UseFlag" Type="Boolean" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel1" runat="server">
                                            <Columns>
                                                <ext:Column ID="Column1" runat="server" Text="班次序号" Width="60" DataIndex="ShiftID" />
                                                <ext:Column ID="Column2" runat="server" Text="班次名称" Width="60" DataIndex="ShiftName">
                                                </ext:Column>
                                                <ext:Column ID="Column3" runat="server" Text="起始时间" Width="80" DataIndex="StartTime">
                                                </ext:Column>
                                                <ext:Column ID="Column4" runat="server" Text="结束时间" Width="80" DataIndex="StopTime">
                                                </ext:Column>
                                                <ext:CheckColumn ID="CheckColumn1" Text="启用标志" Width="60" runat="server" DataIndex="UseFlag">
                                                </ext:CheckColumn>
                                            </Columns>
                                        </ColumnModel>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container10" runat="server" Layout="FormLayout" ColumnWidth=".4"
                                Padding="5">
                                <Items>
                                    <ext:GridPanel ID="gpShiftClass" runat="server" AutoHeight="true" Title="班组">
                                        <Store>
                                            <ext:Store ID="sShiftClass" runat="server" PageSize="20">
                                                <Model>
                                                    <ext:Model ID="Model1" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="ObjID" Type="Int" />
                                                            <ext:ModelField Name="ClassName" Type="String" />
                                                            <ext:ModelField Name="UseFlag" Type="Boolean" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <ColumnModel ID="ColumnModel2" runat="server">
                                            <Columns>
                                                <ext:Column ID="Column6" runat="server" Text="班组序号" Width="60" DataIndex="ObjID" />
                                                <ext:Column ID="Column7" runat="server" Text="班组名称" Width="60" DataIndex="ClassName">
                                                </ext:Column>
                                                <ext:CheckColumn ID="Column10" runat="server" Text="启用标志" Width="60" DataIndex="UseFlag">
                                                </ext:CheckColumn>
                                            </Columns>
                                        </ColumnModel>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:Panel>
            <ext:Panel ID="Panel8" runat="server" Region="South" Split="true">
                <Items>
                    <ext:GridPanel ID="gpShiftRule" runat="server" Title="规律" Height="230">
                        <Store>
                            <ext:Store ID="sShiftRule" runat="server" PageSize="20" OnReadData="ShiftRuleRefresh">
                                <Model>
                                    <ext:Model ID="Model3" IDProperty="SerialID" Name="PptSetTime" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="ObjID" Type="Int" />
                                            <ext:ModelField Name="SerialID" Type="Int" />
                                            <ext:ModelField Name="ShiftClass1ID" Type="String" />
                                            <ext:ModelField Name="ShiftClass2ID" Type="String" />
                                            <ext:ModelField Name="ShiftClass3ID" Type="String" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModel3" runat="server">
                            <Columns>
                                <ext:Column ID="Column13" runat="server" Text="ObjID" Hidden="true" DataIndex="ID" />
                                <ext:Column ID="Column8" runat="server" Text="序号" Flex="1" DataIndex="SerialID" />
                                <ext:Column ID="Column9" runat="server" Text="早" Flex="1" DataIndex="ShiftClass1ID">
                                    <Editor>
                                        <ext:ComboBox ID="cboZao" runat="server" QueryMode="Local" TriggerAction="All" StoreID="ShiftClassStore"
                                            ValueField="ClassName" DisplayField="ClassName">
                                        </ext:ComboBox>
                                    </Editor>
                                </ext:Column>
                                <ext:Column ID="Column11" runat="server" Text="中" Flex="1" DataIndex="ShiftClass2ID">
                                    <Editor>
                                        <ext:ComboBox ID="cboZhong" runat="server" QueryMode="Local" TriggerAction="All"
                                            StoreID="ShiftClassStore" ValueField="ClassName" DisplayField="ClassName">
                                        </ext:ComboBox>
                                    </Editor>
                                </ext:Column>
                                <ext:Column ID="Column12" runat="server" Text="夜" Flex="1" DataIndex="ShiftClass3ID">
                                    <Editor>
                                        <ext:ComboBox ID="cboYe" runat="server" QueryMode="Local" TriggerAction="All" StoreID="ShiftClassStore"
                                            ValueField="ClassName" DisplayField="ClassName">
                                        </ext:ComboBox>
                                    </Editor>
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
                        <Plugins>
                            <ext:CellEditing>
                                <Listeners>
                                    <%--<BeforeEdit Fn="beforeEdit" />--%>
                                </Listeners>
                                <DirectEvents>
                                    <Edit OnEvent="Edit">
                                        <EventMask ShowMask="true" CustomTarget="#{gpShiftRule}" />
                                        <ExtraParams>
                                            <ext:Parameter Name="field" Value="e.field" Mode="Raw" />
                                            <ext:Parameter Name="index" Value="e.rowIdx" Mode="Raw" />
                                            <ext:Parameter Name="record" Value="e.record.data" Mode="Raw" Encode="true" />
                                        </ExtraParams>
                                    </Edit>
                                </DirectEvents>
                            </ext:CellEditing>
                        </Plugins>
                    </ext:GridPanel>
                </Items>
                <Buttons>
                    <ext:Button ID="btnAddRule" runat="server" Text="保存" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="BtnAddShiftRule_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="btnRuleCancel" runat="server" Text="关闭" Icon="Cancel">
                        <Listeners>
                            <Click Handler="close();" />
                        </Listeners>
                        <DirectEvents>
                            <Click OnEvent="BtnCancelShiftRule_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
