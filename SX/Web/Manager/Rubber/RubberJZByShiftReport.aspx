<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RubberJZByShiftReport.aspx.cs" Inherits="Manager_Rubber_RubberJZByShiftReport" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>胶料按照班次结转（允许调整数量）</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript">
        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        var prepareToolbar = function (grid, toolbar, rowIndex, record) {
            if (record.get("AuditFlag") == "1") {
                toolbar.items.getAt(0).hide();
                toolbar.items.getAt(1).hide();
                toolbar.items.getAt(2).hide();
            }
        }

        //点击修改按钮
        var commandcolumn_direct_edit = function (record) {
            var FID = record.data.FID;
            var currentTZ = record.data.CurrentTZ;
            App.direct.commandcolumn_direct_edit(FID, currentTZ, {
                success: function (result) {
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var commandcolumn_click_confirm = function (command, record) {
            if (command.toLowerCase() == "reject") {
                record.reject();
            }
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
            }
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
            integerText: "此填入项格式为正整数",
            decimal: function (val, field) {
                if (!val) {
                    return true;
                }
                try {
                    if (/^[\d.\d]+$/.test(val)) {
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
            decimalText: "此填入项格式为浮点数"
        });

        var edit2 = function (editor, e) {
            if (e.record.data.LastJZ + e.record.data.CurrentCL - e.record.data.CurrentXH + e.value < 0) {
                Ext.Msg.alert("提示", "您的调整数使当前结存变为负数了，请重新设置！");
                e.record.reject();
            }
        };

        var AddMaterial = function () {//物料添加
            App.Manager_BasicInfo_CommonPage_QueryMaterial_Window.show();
        }
        Ext.create("Ext.window.Window", {//物料带窗体
            id: "Manager_BasicInfo_CommonPage_QueryMaterial_Window",
            height: 460,
            hidden: true,
            width: 360,
            html: "<iframe src='../BasicInfo/CommonPage/QueryMaterial.aspx?MinMajorTypeID=2' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择物料",
            modal: true
        })

        var Manager_BasicInfo_CommonPage_QueryMaterial_Request = function (record) {//物料信息返回值处理
            App.txtMaterialName1.setValue(record.data.MaterialName);
            App.hiddenMaterCode.setValue(record.data.MaterialCode);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
    <ext:ResourceManager ID="rmJZByShift" runat="server" />
    <ext:Viewport ID="vpJZByShift" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnJZByShiftTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbJZByShift">
                        <Items>
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <Listeners>
                                    <Click Fn="pnlListFresh" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsBegin" />
                            <ext:Button runat="server" Icon="Add" Text="添加" ID="btnAdd">
                                <DirectEvents>
                                    <Click OnEvent="btnAdd_Click" />
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsMiddle" />
                            <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExport">
                                <Listeners>
                                    <Click Handler="$('#btnExportSubmit').click();"></Click>
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
                                    <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                        Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cbxWorkShopCode" runat="server" FieldLabel="车间" LabelAlign="Right" Editable="false">
                                                <SelectedItems>
                                                    <ext:ListItem Value="2"></ext:ListItem>
                                                </SelectedItems>
                                                <Items>
                                                    <ext:ListItem Text="M2车间" Value="2" AutoDataBind="true"></ext:ListItem>
                                                    <ext:ListItem Text="M3车间" Value="3"></ext:ListItem>
                                                    <ext:ListItem Text="M4车间" Value="4"></ext:ListItem>
                                                    <ext:ListItem Text="M5车间" Value="5"></ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".25"
                                        Padding="5">
                                        <Items>
                                            <ext:DateField ID="txtPlanDate" runat="server" FieldLabel="结存日期" LabelAlign="Right" Editable="false" />
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cbxShiftID" runat="server" FieldLabel="班次" LabelAlign="Right" Editable="false">
                                                <SelectedItems>
                                                    <ext:ListItem Value="3"></ext:ListItem>
                                                </SelectedItems>
                                                <Items>
                                                    <ext:ListItem Text="早" Value="3"></ext:ListItem>
                                                    <ext:ListItem Text="中" Value="1"></ext:ListItem>
                                                    <ext:ListItem Text="夜" Value="2"></ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="container4" runat="server" Layout="FormLayout" ColumnWidth=".25" Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cbxRubType" runat="server" FieldLabel="胶料类型" LabelAlign="Right" Editable="false">
                                                <SelectedItems>
                                                    <ext:ListItem Value="all"></ext:ListItem>
                                                </SelectedItems>
                                                <Items>
                                                    <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                                    <ext:ListItem Text="全钢" Value="1"></ext:ListItem>
                                                    <ext:ListItem Text="半钢" Value="2"></ext:ListItem>
                                                    <ext:ListItem Text="特胎" Value="3"></ext:ListItem>
                                                    <ext:ListItem Text="斜胶" Value="4"></ext:ListItem>
                                                    <ext:ListItem Text="杂胶" Value="5"></ext:ListItem>
                                                    <ext:ListItem Text="其它" Value="6"></ext:ListItem>
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
            <ext:GridPanel ID="pnlList" runat="server" Cls="x-grid-custom" Region="Center">
                <Store>
                    <ext:Store ID="store" runat="server" PageSize="15">
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="model" runat="server" IDProperty="FID">
                                <Fields>
                                    <ext:ModelField Name="FID" />
                                    <ext:ModelField Name="PlanDate" />
                                    <ext:ModelField Name="ShiftID" />
                                    <ext:ModelField Name="ShiftName" />
                                    <ext:ModelField Name="WorkShopCode" />
                                    <ext:ModelField Name="WorkShopName" />
                                    <ext:ModelField Name="MaterCode" />
                                    <ext:ModelField Name="MaterialName" />
                                    <ext:ModelField Name="RubTypeName" />
                                    <ext:ModelField Name="RubCode" />
                                    <ext:ModelField Name="MaterType" />
                                    <ext:ModelField Name="LastJZ" Type="Float" />
                                    <ext:ModelField Name="CurrentOut" Type="Float" />
                                    <ext:ModelField Name="CurrentIn" Type="Float" />
                                    <ext:ModelField Name="CurrentFP" Type="Float" />
                                    <ext:ModelField Name="CurrentCL" Type="Float" />
                                    <ext:ModelField Name="CurrentXH" Type="Float" />
                                    <ext:ModelField Name="CurrentTZ" Type="Float" />
                                    <ext:ModelField Name="CurrentJZ" Type="Float" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="colModel" runat="server">
                    <Columns>
                        <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                        <ext:Column ID="FID" runat="server" Text="ID" Hidden="true" DataIndex="FID" Flex="1" />
                        <ext:Column ID="MaterialName" runat="server" Text="胶料名称" DataIndex="MaterialName" Flex="1" />
                        <ext:Column ID="RubTypeName" runat="server" Text="胶料类型" DataIndex="RubTypeName" Flex="1" />
                        <ext:Column ID="LastJZ" runat="server" Text="上次结存" DataIndex="LastJZ" Flex="1" />
                        <ext:Column ID="CurrentCL" runat="server" Text="当班产量" DataIndex="CurrentCL" Flex="1" />
                        <ext:Column ID="CurrentXH" runat="server" Text="当班消耗" DataIndex="CurrentXH" Flex="1" />
                        <ext:Column ID="CurrentOut" runat="server" Text="当班发出" DataIndex="CurrentOut" Flex="1" />
                        <ext:Column ID="CurrentIn" runat="server" Text="当班接收" DataIndex="CurrentIn" Flex="1" />
                        <ext:Column ID="CurrentFP" runat="server" Text="当班废品" DataIndex="CurrentFP" Flex="1" />
                        <ext:Column ID="CurrentTZ" runat="server" Text="当班调整" DataIndex="CurrentTZ" Flex="1">
                            <Editor>
                                <ext:NumberField ID="NumberField" runat="server" />
                            </Editor>
                        </ext:Column>
                        <ext:CommandColumn ID="CommandColumn2" runat="server" Width="50">
                            <Commands>
                                <ext:GridCommand Text="取消" ToolTip-Text="取消修改" CommandName="reject" Icon="ArrowUndo" />
                            </Commands>
                            <PrepareToolbar Handler="toolbar.items.get(0).setVisible(record.dirty);" />
                            <Listeners>
                                <Command Handler="return commandcolumn_click_confirm(command, record);" />
                            </Listeners>
                        </ext:CommandColumn>
                        <ext:Column ID="CurrentJZ" runat="server" Text="当班结存" DataIndex="CurrentJZ" Flex="1" />
                        <ext:Column ID="PlanDate" runat="server" Text="结存日期" DataIndex="PlanDate" Flex="1" />
                        <ext:Column ID="WorkShopName" runat="server" Text="车间" DataIndex="WorkShopName" Flex="1" />
                        <ext:Column ID="ShiftName" runat="server" Text="班次" DataIndex="ShiftName" Flex="1" />
                        <ext:CommandColumn ID="commandCol" runat="server" Width="80" Text="操作" Align="Center">
                            <Commands>
                                <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="保存调整">
                                    <ToolTip Text="调整本条数据" />
                                </ext:GridCommand>
                            </Commands>
                            <PrepareToolbar Fn="prepareToolbar" />
                            <Listeners>
                                <Command Handler="return commandcolumn_click_confirm(command, record);" />
                            </Listeners>
                        </ext:CommandColumn>
                    </Columns>
                </ColumnModel>
                <SelectionModel>
                    <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single" />
                </SelectionModel>
                <Plugins>
                    <ext:CellEditing ID="CellEditing2" runat="server">
                        <Listeners>
                            <Edit Fn="edit2" />
                        </Listeners>
                    </ext:CellEditing>
                </Plugins>
                <BottomBar>
                    <ext:PagingToolbar ID="pageToolBar" runat="server">
                        <Items>
                            <ext:Label ID="Label2" runat="server" Text="每页条数:" />
                            <ext:ToolbarSpacer ID="ToolbarSpacer2" runat="server" Width="10" />
                            <ext:ComboBox ID="ComboBox2" runat="server" Width="80">
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
                                    <Select Handler="#{pnlList}.store.pageSize = parseInt(this.getValue(), 10); #{pageToolBar}.doRefresh(); return false;" />
                                </Listeners>
                            </ext:ComboBox>
                        </Items>
                        <Plugins>
                            <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                        </Plugins>
                    </ext:PagingToolbar>
                </BottomBar>
            </ext:GridPanel>
            <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="修改调整数"
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
                            <%--<ext:TextField ID="txtJarID2" runat="server" FieldLabel="投料编号" Hidden="true" LabelAlign="Left"
                                ReadOnly="true" />
                            <ext:TextField ID="txtBarcode2" runat="server" FieldLabel="物料条码" LabelAlign="Left"
                                ReadOnly="true" />
                            <ext:DateField ID="txtStockDate2" runat="server" FieldLabel="入仓日期" LabelAlign="Left"
                                ReadOnly="true" />
                            <ext:TextField ID="txtMaterName2" runat="server" FieldLabel="物料名称" LabelAlign="Left"
                                ReadOnly="true" />
                            <ext:ComboBox ID="cbxShiftID2" runat="server" FieldLabel="班次" LabelAlign="Left">
                                <SelectedItems>
                                    <ext:ListItem Value="1">
                                    </ext:ListItem>
                                </SelectedItems>
                                <Items>
                                    <ext:ListItem Text="早" Value="1">
                                    </ext:ListItem>
                                    <ext:ListItem Text="中" Value="2">
                                    </ext:ListItem>
                                    <ext:ListItem Text="夜" Value="3">
                                    </ext:ListItem>
                                </Items>
                            </ext:ComboBox>
                            <ext:TriggerField ID="txtEquipName2" runat="server" Flex="1" FieldLabel="机台名称" LabelAlign="Left"
                                Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="QueryEquipmentInfo1" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtRealNum2" runat="server" Vtype="integer" FieldLabel="投入数量"
                                LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                            <ext:TextField ID="txtRealWeight2" runat="server" Vtype="decimal" FieldLabel="投入重量"
                                LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                            <ext:TriggerField ID="txtUserName2" runat="server" FieldLabel="经办人" LabelAlign="Left"
                                Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <RemoteValidation OnValidation="CheckField" />
                                <Listeners>
                                    <TriggerClick Fn="QueryUser" />
                                </Listeners>
                            </ext:TriggerField>--%>
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
                <Listeners>
                    <Show Handler="for(var i=0;i<#{vpJZByShift}.items.length;i++){#{vpJZByShift}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpJZByShift}.items.length;i++){#{vpJZByShift}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>
            <ext:Window ID="winAdd" runat="server" Icon="MonitorAdd" Closable="false" Title="添加结转信息"
                Width="320" Height="380" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                            <ext:DateField ID="txtPlanDate1" runat="server" FieldLabel="结存日期" LabelAlign="Left" Editable="false" />
                            <ext:ComboBox ID="cbxShiftID1" runat="server" FieldLabel="班次" LabelAlign="Left" Editable="false">
                                <SelectedItems>
                                    <ext:ListItem Value="1">
                                    </ext:ListItem>
                                </SelectedItems>
                                <Items>
                                    <ext:ListItem Text="早" Value="3">
                                    </ext:ListItem>
                                    <ext:ListItem Text="中" Value="1">
                                    </ext:ListItem>
                                    <ext:ListItem Text="夜" Value="2">
                                    </ext:ListItem>
                                </Items>
                            </ext:ComboBox>
                            <ext:ComboBox ID="cbxWorkShopCode1" runat="server" FieldLabel="车间" LabelAlign="Left" Editable="false">
                                <SelectedItems>
                                    <ext:ListItem Value="2"></ext:ListItem>
                                </SelectedItems>
                                <Items>
                                    <ext:ListItem Text="M2车间" Value="2" AutoDataBind="true"></ext:ListItem>
                                    <ext:ListItem Text="M3车间" Value="3"></ext:ListItem>
                                    <ext:ListItem Text="M4车间" Value="4"></ext:ListItem>
                                    <ext:ListItem Text="M5车间" Value="5"></ext:ListItem>
                                </Items>
                            </ext:ComboBox>
                            <ext:TriggerField ID="txtMaterialName1" runat="server" FieldLabel="物料名称" LabelAlign="Left"
                                IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" Editable="false">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Search" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Fn="AddMaterial" />
                                </Listeners>
                            </ext:TriggerField>
                            <ext:TextField ID="txtTZWeight1" runat="server" Vtype="decimal" FieldLabel="调整数量"
                                LabelAlign="Left" IndicatorText="*" IndicatorCls="red-text" AllowBlank="false" />
                            
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
                <Listeners>
                    <Show Handler="for(var i=0;i<#{vpJZByShift}.items.length;i++){#{vpJZByShift}.getComponent(i).disable(true);}" />
                    <Hide Handler="for(var i=0;i<#{vpJZByShift}.items.length;i++){#{vpJZByShift}.getComponent(i).enable(true);}" />
                </Listeners>
            </ext:Window>
            <ext:Hidden ID="hiddenMaterCode" runat="server" />
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
