﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecipeCopyToEquip.aspx.cs" Inherits="Manager_Technology_Manage_RecipeCopyToEquip" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />

    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/default.js"></script>
    <script type="text/javascript">
        var dobtnCopyPmtRecipeSuccess = function () {
            parent.refreshLoaction();
            parent.App.Manager_Technology_Manage_RecipeCopyToEquip_Window.close();
        }
        var dobtnCopyPmtRecipeClick = function (btn) {
            if (btn != "yes") {
                return;
            }
            var after = function () {
                App.waitProgressWindow.close();
            }
            var before = function () {
                App.waitProgressWindow.show();
            }
            try {
                before();
                App.direct.PmtRecipeCopyToEquip({
                    success: function (result) {
                        after();
                        if (result == "") {
                            Ext.Msg.alert('成功', "配方另存成功！", function (btn) { dobtnCopyPmtRecipeSuccess() });
                        } else {
                            Ext.Msg.alert('失败', result);
                        }
                    },
                    failure: function (errorMsg) {
                        after();
                        Ext.Msg.alert('错误', errorMsg);
                    }
                });
            }
            catch (ex) {
                after();
            }
        }

        var btnCopyPmtRecipeClick = function () {
            Ext.Msg.confirm("提示", '您确定需要将拷贝的工艺配方信息另存到选中的机台？', function (btn) { dobtnCopyPmtRecipeClick(btn); });
            return false;
        }


        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {
            App.txtRubberName.getTrigger(0).show();
            App.hiddenRubberCode.setValue(record.data.MaterialCode);
            App.txtRubberName.setValue(record.data.MaterialName);
        }
        var QueryMaterialInfo = function (field, trigger, index) {
            var MajorTypeID = '';
            MajorTypeID = App.hiddenMajorId.value;
            Ext.create("Ext.window.Window", {
                id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
                hidden: true,
                width: 370,
                height: 470,
                html: "<iframe src='../../BasicInfo/CommonPage/QueryMaterial.aspx?MajorTypeID=2,3,4,5'width=100% height=100% scrolling=no  frameborder=0></iframe>",
                bodyStyle: "background-color: #fff;",
                closable: true,
                title: "请选择物料",
                modal: true
            })
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenRubberCode.setValue('');
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
                    break;
            }
        }
//        ?MajorTypeID=" + MajorTypeID + "
    </script>
      <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>


</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="resourceManager" runat="server" />
        <ext:Viewport ID="viewport" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="northPanel" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="northToolbar">
                            <Items>
                                <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                                <ext:Button runat="server" Icon="PageCopy" Text="配方另存" ID="btnCopyPmtRecipe">
                                    <ToolTips>
                                        <ext:ToolTip ID="ttSearch" runat="server" Html="确认将配方拷贝到选中的机台" />
                                    </ToolTips>
                                    <Listeners>
                                        <Click Fn="btnCopyPmtRecipeClick"></Click>
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="toolbarSeparator_end" />
                                <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                                <ext:ToolbarFill ID="toolbarFill_end" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:Panel ID="panelNorthQuery" runat="server" AutoHeight="true" Layout="AnchorLayout">
                            <Items>
                                <ext:Container ID="container1" runat="server" Layout="HBoxLayout" Padding="5" ColumnWidth="1">
                                    <Items>
                                 
                                        <ext:ComboBox ID="txtRecipeType" runat="server" FieldLabel="配方类型" LabelAlign="Right" Flex="1" Editable="false" />
                                        <ext:TriggerField ID="txtRubberName" runat="server" Flex="1" FieldLabel="胶料名称" LabelAlign="Right" Editable="false">
                                            <Triggers>
                                                <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                <ext:FieldTrigger Icon="Search" />
                                            </Triggers>
                                            <Listeners>
                                                <TriggerClick Fn="QueryMaterialInfo" />
                                            </Listeners>
                                        </ext:TriggerField>
                                        <ext:NumberField ID="txtRecipeVer" runat="server" LabelAlign="Right" Flex="1" FieldLabel="配方版本" DecimalPrecision ="0" />

                                    </Items>
                                </ext:Container>
                            </Items>
                    
                           
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="gridPanelCenter" runat="server" Region="Center" MultiSelect="true" SimpleSelect="true">
                    <Store>
                        <ext:Store ID="store" runat="server" PageSize="10">
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server" IDProperty="EquipCode">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="EquipName" />
                                        <ext:ModelField Name="EquipCode" />
                                        <ext:ModelField Name="Remark" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <SelectionModel>
                        <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" />
                    </SelectionModel>
                    <ColumnModel ID="columnModel" runat="server">
                        <Columns>
                            <ext:Column ID="EquipCode" DataIndex="EquipCode" runat="server" Text="设备编码" Width="80" />
                            <ext:Column ID="EquipName" DataIndex="EquipName" runat="server" Text="设备名称" Flex="1" />
                            <ext:Column ID="Remark" DataIndex="Remark" runat="server" Text="备注" Flex="1" />
                        </Columns>
                    </ColumnModel>
        
                </ext:GridPanel>
                <ext:Hidden ID="hiddenRubberCode" runat="server"></ext:Hidden>
                <ext:Hidden ID="hiddenMajorId" runat="server"></ext:Hidden>
              
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
