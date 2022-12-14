<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialCarryover.aspx.cs" Inherits="Manager_BasicInfo_StorageInfo_MaterialCarryover" %>
<%@ Register assembly="Ext.Net" namespace="Ext.Net" tagprefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <style type="text/css">
        .x-grid-row-collapsed .x-grid-cell
        {
        	background-color: #FF8C69 !important;
        }
    </style>
    <script type="text/javascript">
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

        var startTrack = function () {
            this.checkboxes = [];
            var cb;

            Ext.select(".x-form-item", false).each(function (checkEl) {
                cb = Ext.getCmp(checkEl.dom.id.selected);
                cb.setValue(false);
                this.rowselect.push(cb);
            }, this);
        };

        var SetCarryover = function () {
            var section = App.pnlList.getView().getSelectionModel().getSelection();
                
            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
                Ext.Msg.confirm("提示", '确定要结转吗？', function (btn) { commandcolumn_direct_carryover(btn) });
            }
        }

        var commandcolumn_direct_carryover = function (btn) {
            if (btn != "yes") {
                return;
            }

            //            var section = App.pnlList.getView().getSelectionModel().getSelection();
            //            var objIDs = "";
            //            var storageIDs = "";
            //            for (var i = 0; i < section.length; i++) {
            //                var bc = App.store.data.get(section[i].index).data.StorageName;
            //                var od = App.store.data.get(section[i].index).data.ResponsiblePerson;
            //                objIDs = objIDs == "" ? bc : objIDs + "," + bc;
            //                storageIDs = storageIDs == "" ? od : storageIDs + "," + od;
            //            }

            //            Ext.Msg.alert('操作', objIDs + '；' + storageIDs);

            App.direct.btnBatchCarryover_Click({
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                },
                eventMask: {
                   showMask: true 
                }
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="rmStorage" runat="server" />
        <ext:Viewport ID="vpStorage" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="pnStorageTitle" runat="server" Region="North" AutoHeight="true">
                    <TopBar>
                        <ext:Toolbar runat="server" ID="tbStorage">
                            <Items>
                                <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                    <Listeners>
                                        <Click Fn="pnlListFresh" />
                                    </Listeners>
                                </ext:Button>
                                <ext:ToolbarSeparator ID="tsBegin" />
                                <ext:Button runat="server" Icon="LockEdit" Text="库房结转" ID="Button1">
                                    <Listeners>
                                        <Click Handler="SetCarryover();" />
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
                                        <%--<ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33" Padding="5">
                                            <Items>
                                                <ext:ComboBox ID="ckxStorageType" runat="server" FieldLabel="库房类型" LabelAlign="Right">
                                                    <SelectedItems>
                                                        <ext:ListItem Value="all"></ext:ListItem>
                                                    </SelectedItems>
                                                    <Items>
                                                        <ext:ListItem Text="全部" Value="all" AutoDataBind="true"></ext:ListItem>
                                                        <ext:ListItem Text="原材料库" Value="0"></ext:ListItem>
                                                        <ext:ListItem Text="密炼原材料库" Value="1"></ext:ListItem>
                                                    </Items>
                                                </ext:ComboBox>
                                            </Items>
                                        </ext:Container>--%>
                                        <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33" Padding="5">
                                            <Items>
                                                <ext:TextField ID="txtStorageName" runat="server" FieldLabel="库房名称" LabelAlign="Right" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="container5" runat="server" Layout="FormLayout" ColumnWidth=".33" Padding="5">
                                            <Items>
                                                <ext:TextField ID="txtERPCode" runat="server" FieldLabel="ERP编号" LabelAlign="Right" />
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
                        <ext:Store ID="store" runat="server" PageSize="10"> 
                            <Proxy>
                                <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                            </Proxy>
                            <Model>
                                <ext:Model ID="model" runat="server" IDProperty="ObjID">
                                    <Fields>
                                        <ext:ModelField Name="ObjID" />
                                        <ext:ModelField Name="StorageID" />
                                        <ext:ModelField Name="StorageName" />
                                        <ext:ModelField Name="UsedFlag" />
                                        <ext:ModelField Name="StorageLevel" />
                                        <ext:ModelField Name="StorageHigherLevel" />
                                        <ext:ModelField Name="UsedDuration" />
                                        <ext:ModelField Name="UsingDuration" />
                                        <ext:ModelField Name="DurationBeginDate" />
                                        <ext:ModelField Name="DurationEndDate" />
                                        <ext:ModelField Name="DurationSet" />
                                        <ext:ModelField Name="ResponsiblePerson" />
                                        <ext:ModelField Name="ERPCode" />
                                        <ext:ModelField Name="CancelFlag" />
                                        <ext:ModelField Name="Remark" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="colModel" runat="server">
                        <Columns>
                            <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                            <ext:Column ID="objID" runat="server" Text="ID" Hidden="true" DataIndex="ObjID" Flex="1" />
                            <ext:Column ID="storageID" runat="server" Text="库房编号" DataIndex="StorageID" Hidden="true" />
                            <ext:Column ID="storageName" runat="server" Text="库房名称" DataIndex="StorageName" Flex="1" />
                            <ext:Column ID="storageHigherLevel" runat="server" Text="上级库房" DataIndex="StorageHigherLevel" Flex="1" />
                            <ext:Column ID="usingDuration" runat="server" Text="当前期间" DataIndex="UsingDuration" Flex="1" />
                            <ext:Column ID="durationBeginDate" runat="server" Text="期间开始日期" DataIndex="DurationBeginDate" Flex="1" />
                            <ext:Column ID="durationEndDate" runat="server" Text="期间截止日期" DataIndex="DurationEndDate" Flex="1" />
                            <ext:Column ID="durationSet" runat="server" Text="期间设置方式" DataIndex="DurationSet" Flex="1" />
                            <ext:Column ID="ERPCode" runat="server" Text="ERP编号" DataIndex="ERPCode" Flex="1" />
                            <ext:Column ID="responsiblePerson" runat="server" Text="负责人" DataIndex="ResponsiblePerson" Flex="1" />
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:CheckboxSelectionModel ID="rowSelectMuti" runat="server" Mode="Simple" />
                    </SelectionModel>
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
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
