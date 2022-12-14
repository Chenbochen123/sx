<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddConfigInfo.aspx.cs" Inherits="Manager_ProducingPlan_ShiftConfig_AddConfigInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var close = function () {
            parent.App.AddConfigWin.close();
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:FormPanel ID="FormPanel1" runat="server" Frame="true" Width="550">
        <FieldDefaults LabelAlign="Right" LabelWidth="90" MsgTarget="Qtip" />
        <Items>
            <ext:FieldSet ID="FieldSet3" runat="server" Layout="AnchorLayout" DefaultAnchor="100%">
                <Items>
                    <ext:Container ID="Container2" runat="server" Layout="HBoxLayout" MarginSpec="0 0 5 0">
                        <Items>
                            <ext:DateField ID="txtStratShiftDate" runat="server" Editable="false" Vtype="daterange"
                                FieldLabel="生产日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd">
                            </ext:DateField>
                            <ext:ComboBox ID="cboShift" runat="server" Editable="false" FieldLabel="班次" LabelAlign="Right">
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
                    <ext:Container>
                    </ext:Container>
                </Items>
            </ext:FieldSet>
            <ext:FieldSet ID="FieldSet2" runat="server" Layout="AnchorLayout" DefaultAnchor="100%">
                <Items>
                    <ext:Container ID="Container1" runat="server" Layout="HBoxLayout" MarginSpec="0 0 5 0">
                        <Items>
                            <ext:TimeField ID="tfZhongS" Flex="1" runat="server" Increment="1" FieldLabel="中"
                                Format="HH:mm:ss" InvalidText="请选择时间或输入有效格式的时间" PickerMaxHeight="100">
                            </ext:TimeField>
                            <ext:TimeField ID="tfZhongE" Flex="1" runat="server" Increment="1" FieldLabel="至"
                                Format="HH:mm:ss" InvalidText="请选择时间或输入有效格式的时间" PickerMaxHeight="100">
                            </ext:TimeField>
                            <ext:ComboBox ID="cboZhong" Flex="1" runat="server" Editable="false" FieldLabel="起始日"
                                LabelAlign="Right" IsRemoteValidation="true">
                                <Items>
                                    <ext:ListItem Text="当天" Value="0">
                                    </ext:ListItem>
                                    <ext:ListItem Text="前天" Value="-1">
                                    </ext:ListItem>
                                    <ext:ListItem Text="后天" Value="1">
                                    </ext:ListItem>
                                </Items>
                                <SelectedItems>
                                    <ext:ListItem Value="当天" />
                                </SelectedItems>
                            </ext:ComboBox>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:FieldSet>
            <ext:FieldSet ID="FieldSet1" runat="server" Layout="AnchorLayout" DefaultAnchor="100%">
                <Items>
                    <ext:Container ID="Container3" runat="server" Layout="HBoxLayout" MarginSpec="0 0 5 0">
                        <Items>
                            <ext:TimeField ID="tfYeS" Flex="1" runat="server" Increment="1" FieldLabel="夜" Format="HH:mm:ss"
                                InvalidText="请选择时间或输入有效格式的时间" PickerMaxHeight="100">
                            </ext:TimeField>
                            <ext:TimeField ID="tfYeE" Flex="1" runat="server" Increment="1" FieldLabel="至" Format="HH:mm:ss"
                                InvalidText="请选择时间或输入有效格式的时间" PickerMaxHeight="100">
                            </ext:TimeField>
                            <ext:ComboBox ID="cboYe" Flex="1" runat="server" Editable="false" FieldLabel="起始日"
                                LabelAlign="Right" IsRemoteValidation="true">
                                <Items>
                                    <ext:ListItem Text="当天" Value="0">
                                    </ext:ListItem>
                                    <ext:ListItem Text="前天" Value="-1">
                                    </ext:ListItem>
                                    <ext:ListItem Text="后天" Value="1">
                                    </ext:ListItem>
                                </Items>
                                <SelectedItems>
                                    <ext:ListItem Value="当天" />
                                </SelectedItems>
                            </ext:ComboBox>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:FieldSet>
        </Items>
        <Listeners>
            <%-- <ValidityChange Handler="#{btnSave}.setDisabled(!valid);" />--%>
        </Listeners>
        <Buttons>
            <ext:Button ID="btnReSet" runat="server" Text="重置" Icon="CommentEdit">
                <Listeners>
                    <Click Handler="this.up('form').getForm().reset();" />
                </Listeners>
            </ext:Button>
            <ext:Button ID="btnSave" runat="server" Text="保存" Icon="Accept">
                <DirectEvents>
                    <Click OnEvent="BtnAddConfig_Click">
                    </Click>
                    <%--<Click Handler="var form = this.up('form').getForm(); form.isValid() && Ext.MessageBox.alert('Submitted Values', form.getValues(true));" />--%>
                </DirectEvents>
            </ext:Button>
            <ext:Button ID="btnSetTimeClose" runat="server" Text="关闭" Icon="Cancel">
                <Listeners>
                    <Click Handler="close();" />
                </Listeners>
            </ext:Button>
        </Buttons>
    </ext:FormPanel>
    </form>
</body>
</html>
