﻿<%@ page language="C#" autoeventwireup="true" inherits="Manager_ProducingPlan_SmallMaterialWeigh_SmallMaterialWeigh, App_Web_s5lbchza" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>小料称量查询</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">
        var pnlListFresh = function () {
            App.txtPlanID.setValue("");
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
          
            return false;
        }
        var pnlListFreshByPlanID = function () {
            App.cboEquip.setValue("");
            App.cboShift.setValue("");
            var flag = App.txtPlanID.getValue();
            if (flag=='') {
                alert('请输入计划号');
                return;
            }
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();

            return false;
        }

        //
        var commandcolumn_click = function (command, record) {
            var PlanID = record.data.PlanID;
            var queryWindow = App.SmallWeighWin;
            var html = "<iframe src='SmallWeigh.aspx?PlanID=" + PlanID + "' width=100% height=100% scrolling=no  frameborder=0></iframe>";
            if (queryWindow.getBody()) {
                queryWindow.getBody().update(html);
            } else {
                queryWindow.html = html;
            }
            queryWindow.show();
        }
        Ext.create("Ext.window.Window", {
            id: "SmallWeighWin",
            hidden: true,
            maximized: true,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryWorkShop.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "小料称量详情",
            modal: true
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmChkBill" runat="server" />
    <ext:Viewport ID="vpChkBill" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnChkBill" runat="server" Region="North">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbChkBill">
                        <Items>
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <Listeners>
                                    <Click Fn="pnlListFresh" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsMiddle" />
                            <ext:Button runat="server" Icon="LockEdit" Text="按照计划号查询" ID="btnPlanSearch">
                                <Listeners>
                                      <Click Fn="pnlListFreshByPlanID" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsEnd" />
                            <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                            <ext:ToolbarFill ID="tfEnd" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                        <Items>
                            <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                 <ext:ComboBox ID="cboEquip" runat="server" Editable="false" FieldLabel="机台" LabelAlign="Right">
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
                                    <%--<ext:TextField ID="cboEquip" runat="server" FieldLabel="机台" LabelAlign="Right" />--%>
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:DateField ID="txtBeginTime" runat="server" FieldLabel="开始日期" LabelAlign="Right"
                                        Format="yyyy-MM-dd" />
                                </Items>
                            </ext:Container>
                             <ext:Container ID="container5" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:DateField ID="txtEndTime" runat="server" FieldLabel="结束日期" LabelAlign="Right"
                                        Format="yyyy-MM-dd" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container4" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
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
                            <ext:Container ID="container3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:TextField ID="txtPlanID" runat="server" FieldLabel="计划编号" LabelAlign="Right" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container6" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                Padding="5">
                                <Items>
                                    <ext:TextField ID="TextMatetial" runat="server" FieldLabel="物料名称" LabelAlign="Right" />
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="Panel2" runat="server" Region="Center" Frame="true" Layout="Fit" MarginsSummary="0 5 0 5">
                <Items>
                    <ext:GridPanel ID="pnlList" runat="server">
                        <Store>
                            <ext:Store ID="store" runat="server" PageSize="10">
                                <Proxy>
                                    <ext:PageProxy DirectFn="App.direct.GridPanelBindData">
                                    </ext:PageProxy>
                                </Proxy>
                                <Model>
                                    <ext:Model ID="model" runat="server" IDProperty="PlanID">
                                        <Fields>
                                            <ext:ModelField Name="PlanID" />
                                            <ext:ModelField Name="PlanDate" Type="Date" />
                                            <ext:ModelField Name="ShiftName" Type="String" />
                                            <ext:ModelField Name="ClassName" Type="String" />
                                            <ext:ModelField Name="RecipeMaterialCode" Type="String" />
                                            <ext:ModelField Name="RecipeMaterialName" Type="String" />
                                            <ext:ModelField Name="PlanNum" Type="Int" />
                                            <ext:ModelField Name="RealNum" Type="Int" />
                                            <ext:ModelField Name="RealStartTime" Type="Date" />
                                            <ext:ModelField Name="RealEndtime" Type="Date" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                <ext:Column ID="PlanID" runat="server" Text="计划号" DataIndex="PlanID" Flex="1" />
                                <ext:DateColumn ID="inStockDate" runat="server" Text="计划日期" DataIndex="PlanDate"
                                    Width="150" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:Column ID="shiftName" runat="server" Text="班次" DataIndex="ShiftName" Width="40" />
                                <ext:Column ID="className" runat="server" Text="班组" DataIndex="ClassName" Width="40" />
                                <ext:Column ID="RecipeMaterialCode" runat="server" Text="物料编码" DataIndex="RecipeMaterialCode"
                                    Flex="1">
                                </ext:Column>
                                <ext:Column ID="remark" runat="server" Text="物料名称" DataIndex="RecipeMaterialName"
                                    Flex="1" />
                                <ext:Column ID="Column1" runat="server" Text="计划数量" DataIndex="PlanNum" Width="60" />
                                <ext:Column ID="Column2" runat="server" Text="完成数量" DataIndex="RealNum" Width="60" />
                                <ext:DateColumn ID="Column3" runat="server" Text="开始称量时间" DataIndex="RealStartTime"
                                    Width="150" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:DateColumn ID="Column4" runat="server" Text="称量结束时间" DataIndex="RealEndtime"
                                    Width="150" Format="yyyy-MM-dd HH:mm:ss" />
                                <ext:CommandColumn ID="commandCol" runat="server" Width="60" Text="称量查询" Align="Center">
                                    <Commands>
                                        <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="详情">
                                            <ToolTip Text="小料称量相信信息" />
                                        </ext:GridCommand>
                                    </Commands>
                                    <PrepareToolbar />
                                    <Listeners>
                                        <Command Handler="return commandcolumn_click(command, record);" />
                                    </Listeners>
                                </ext:CommandColumn>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single">
                            </ext:RowSelectionModel>
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="pageToolBar" runat="server">
                                <Plugins>
                                    <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
