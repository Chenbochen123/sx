﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_ProducingPlan_ShiftSeting_PptSetTime, App_Web_vi5vqyds" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        function toggleBillingAddressFields(cb, checked) {
            var fieldset = cb.ownerCt;
            Ext.Array.forEach(fieldset.query('textfield'), function (field) {
                var id = cb.id;
                if (id == 'chcZao') {
                    App.hiddenzao.setValue(checked);
                }
                else if (id == 'chcZhong') {
                    App.hiddenzhong.setValue(checked);
                }
                else if (id == 'chcYe') {
                    App.hiddenye.setValue(checked);
                }
                field.setDisabled(checked);
                if (!Ext.isIE6) {
                    field.el.animate({ opacity: checked ? 0.3 : 1 });
                }
            });
        }
    </script>
    <script type="text/javascript">
        var close = function () {
            parent.App.PptSetTimeWin.close();
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:FormPanel ID="FormPanel1" runat="server" Frame="true">
        <FieldDefaults LabelAlign="Right" LabelWidth="90" MsgTarget="Qtip" />
        <Items>
            <ext:ComboBox ID="cbo_SetTime_Ppt_DeptType" runat="server" Editable="false" FieldLabel="工序"
                IsRemoteValidation="true" AnchorHorizontal="100%">
                <Listeners>
                    <Select Handler="this.getTrigger(0).show();" />
                </Listeners>
                <DirectEvents>
                    <Select OnEvent="InitPptSetTimeWin" />
                </DirectEvents>
            </ext:ComboBox>
            <ext:FieldSet ID="FieldSet1" runat="server" Title="早班设置" Layout="AnchorLayout" DefaultAnchor="100%">
                <Items>
                    <ext:Checkbox ID="chcZao" runat="server" Name="billingSameAsMailing" BoxLabel="禁用早班"
                        HideLabel="true" Checked="false" StyleSpec="margin-bottom:10px;" Handler="toggleBillingAddressFields">
                    </ext:Checkbox>
                    <ext:Container ID="Container2" runat="server" Layout="HBoxLayout" MarginSpec="0 0 5 0">
                        <Items>
                            <ext:TimeField ID="tfZaoS" Flex="1" runat="server" Increment="1" FieldLabel="早" Format="HH:mm:ss"
                                InvalidText="请选择时间或输入有效格式的时间" PickerMaxHeight="100">
                            </ext:TimeField>
                            <ext:TimeField ID="tfZaoE" Flex="1" runat="server" Increment="1" FieldLabel="至" Format="HH:mm:ss"
                                InvalidText="请选择时间或输入有效格式的时间" PickerMaxHeight="100">
                            </ext:TimeField>
                            <ext:ComboBox ID="cboZao" Flex="1" runat="server" Editable="false" FieldLabel="起始日"
                                LabelAlign="Right" >
                                <SelectedItems>
                                    <ext:ListItem Value="当天" />
                                </SelectedItems>
                            </ext:ComboBox>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:FieldSet>
            <ext:FieldSet ID="FieldSet2" runat="server" Title="中班设置" Layout="AnchorLayout" DefaultAnchor="100%">
                <Items>
                    <ext:Checkbox ID="chcZhong" runat="server" Name="billingSameAsMailing" BoxLabel="禁用中班"
                        HideLabel="true" Checked="false" StyleSpec="margin-bottom:10px;" Handler="toggleBillingAddressFields">
                    </ext:Checkbox>
                    <ext:Container ID="Container1" runat="server" Layout="HBoxLayout" MarginSpec="0 0 5 0">
                        <Items>
                            <ext:TimeField ID="tfZhongS" Flex="1" runat="server" Increment="1" FieldLabel="中"
                                Format="HH:mm:ss" InvalidText="请选择时间或输入有效格式的时间" PickerMaxHeight="100">
                            </ext:TimeField>
                            <ext:TimeField ID="tfZhongE" Flex="1" runat="server" Increment="1" FieldLabel="至"
                                Format="HH:mm:ss" InvalidText="请选择时间或输入有效格式的时间" PickerMaxHeight="100">
                            </ext:TimeField>
                            <ext:ComboBox ID="cboZhong" Flex="1" runat="server" Editable="false" FieldLabel="起始日"
                                LabelAlign="Right">
                                <SelectedItems>
                                    <ext:ListItem Value="当天" />
                                </SelectedItems>
                            </ext:ComboBox>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:FieldSet>
            <ext:FieldSet ID="FieldSet3" runat="server" Title="夜班设置" Layout="AnchorLayout" DefaultAnchor="100%">
                <Items>
                    <ext:Checkbox ID="chcYe" runat="server" Name="billingSameAsMailing" BoxLabel="禁用夜班"
                        HideLabel="true" Checked="false" StyleSpec="margin-bottom:10px;" Handler="toggleBillingAddressFields">
                    </ext:Checkbox>
                    <ext:Container ID="Container3" runat="server" Layout="HBoxLayout" MarginSpec="0 0 5 0">
                        <Items>
                            <ext:TimeField ID="tfYeS" Flex="1" runat="server" Increment="1" FieldLabel="夜" Format="HH:mm:ss"
                                InvalidText="请选择时间或输入有效格式的时间" PickerMaxHeight="100">
                            </ext:TimeField>
                            <ext:TimeField ID="tfYeE" Flex="1" runat="server" Increment="1" FieldLabel="至" Format="HH:mm:ss"
                                InvalidText="请选择时间或输入有效格式的时间" PickerMaxHeight="100">
                            </ext:TimeField>
                            <ext:ComboBox ID="cboYe" Flex="1" runat="server" Editable="false" FieldLabel="起始日"
                                LabelAlign="Right" >
                                <SelectedItems>
                                    <ext:ListItem Value="当天" />
                                </SelectedItems>
                            </ext:ComboBox>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:FieldSet>
        </Items>
        <Buttons>
            <ext:Button ID="btnReSet" runat="server" Text="重置" Icon="CommentEdit">
                <Listeners>
                    <Click Handler="this.up('form').getForm().reset();" />
                </Listeners>
            </ext:Button>
            <ext:Button ID="btnSave" runat="server" Text="保存" Icon="Accept">
                <DirectEvents>
                    <Click OnEvent="BtnTimeAdd_Click">
                    </Click>
                </DirectEvents>
            </ext:Button>
            <ext:Button ID="btnSetTimeClose" runat="server" Text="关闭" Icon="Cancel">
                <Listeners>
                    <Click Handler="close();" />
                </Listeners>
            </ext:Button>
        </Buttons>
    </ext:FormPanel>
    <ext:Hidden ID="hiddenzao" runat="server" Text="false">
    </ext:Hidden>
    <ext:Hidden ID="hiddenzhong" runat="server" Text="false">
    </ext:Hidden>
    <ext:Hidden ID="hiddenye" runat="server" Text="false">
    </ext:Hidden>
    </form>
</body>
</html>
