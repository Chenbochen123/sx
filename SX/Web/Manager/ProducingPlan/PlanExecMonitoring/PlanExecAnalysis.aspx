<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanExecAnalysis.aspx.cs"
    Inherits="Manager_ProducingPlan_PlanExecMonitoring_PlanExecAnalysis" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>计划执行分析</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <style type="text/css">
        .cbStates-list
        {
            width: 298px;
            font: 11px tahoma,arial,helvetica,sans-serif;
        }
        
        .cbStates-list th
        {
            font-weight: bold;
        }
        
        .cbStates-list td, .cbStates-list th
        {
            padding: 10px;
        }
        
        .list-item
        {
            cursor: pointer;
        }
    </style>
    <script type="text/javascript">

        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.hidden_delete_flag.setValue("0");
            App.Store1.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        ///机台信息

        var SelectEquipID = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.txtEquipCode.setValue('');
                    App.hidden_select_equip_code.setValue('');
                    App.cboAddMaterName.store.reload();
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    break;
                case 1:
                    App.hidden_select_equip.setValue("Equip");
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
            }
        }
        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {  //机台返回值处理
            var type = App.hidden_select_equip.getValue();
            App.txtEquipCode.getTrigger(0).show();
            if (type == "Equip") {
                App.txtEquipCode.setValue(record.data.EquipName);
                App.hidden_select_equip_code.setValue(record.data.EquipCode);
                App.cboMaterName.setValue('');
                App.cboMaterName.store.reload();
            }
        }
        Ext.create("Ext.window.Window", {//机台信息带回查询信息
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            height: 480,
            hidden: true,
            width: 360,
            html: "<iframe src='../../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择机台",
            modal: true
        })


        var template = '<span style="background-color:{0};">{1}</span>';

        var pctChange = function (value) {
            return Ext.String.format(template, (value.substring(0, value.indexOf('%')) > 90) ? "green" : "red", value);
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button ID="btnExportSubmit" Style="display:none" runat="server" Text="Button" OnClick="btnExportSubmit_Click" />
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:Hidden ID="hidden_select_equip" runat="server">
    </ext:Hidden>
    <ext:Hidden ID="hidden_select_equip_code" runat="server">
    </ext:Hidden>
    <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="Panel1" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ctl320">
                        <Items>
                            <ext:ToolbarSeparator ID="ctl347" />
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <Listeners>
                                    <Click Fn="pnlListFresh">
                                    </Click>
                                </Listeners>
                                <ToolTips>
                                    <ext:ToolTip runat="server" Html="查询" ID="ToolTip2" />
                                </ToolTips>
                            </ext:Button>
                            <ext:Button runat="server" Icon="PageWhiteExcel" Text="导出" ID="btnExcel">
                                <Listeners>
                                    <Click Handler="$('#btnExportSubmit').click();"></Click>
                                </Listeners>
                                <ToolTips>
                                    <ext:ToolTip runat="server" Html="导出" ID="ctl350" />
                                </ToolTips>
                            </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel ID="Panel2" runat="server" AutoHeight="true">
                        <Items>
                            <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="Container7" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:DateField ID="txtStratShiftDate" runat="server" Editable="false" Vtype="daterange"
                                                FieldLabel="开始日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd">
                                                <Listeners>
                                                    <Change Handler="App.cboMaterName.setValue('');App.cboMaterName.store.reload();">
                                                    </Change>
                                                </Listeners>
                                            </ext:DateField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container13" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:DateField ID="txtEndShiftDate" runat="server" Editable="false" Vtype="daterange"
                                                FieldLabel="结束日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd">
                                                <Listeners>
                                                    <Change Handler="App.cboMaterName.setValue('');App.cboMaterName.store.reload();">
                                                    </Change>
                                                </Listeners>
                                            </ext:DateField>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:TriggerField ID="txtEquipCode" runat="server" Flex="1" Editable="false" FieldLabel="机台"
                                                LabelAlign="Right">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="SelectEquipID" />
                                                </Listeners>
                                            </ext:TriggerField>
                                        </Items>
                                    </ext:Container>
                                  
                                    <ext:Container ID="Container3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cboShift" runat="server" Editable="false" FieldLabel="班次" LabelAlign="Right">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.getTrigger(0).show();App.cboMaterName.setValue('');App.cboMaterName.store.reload();" />
                                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { 
                                                                           this.clearValue(); 
                                                                           this.getTrigger(0).hide();
                                                                           App.cboMaterName.setValue('');App.cboMaterName.store.reload();
                                                                       }" />
                                                </Listeners>
                                            </ext:ComboBox>
                                            <%--<ext:ComboBox ID="cboClass" runat="server" Editable="false" FieldLabel="班组" LabelAlign="Right">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Listeners>
                                                    <Select Handler="this.getTrigger(0).show();App.cboMaterName.setValue('');App.cboMaterName.store.reload();" />
                                                    <BeforeQuery Handler="this.getTrigger(0)[this.getRawValue().toString().length == 0 ? 'hide' : 'show']();" />
                                                    <TriggerClick Handler="if (index == 0) { 
                                                                           this.clearValue(); 
                                                                           this.getTrigger(0).hide();
                                                                           App.cboMaterName.setValue('');App.cboMaterName.store.reload();
                                                                       }" />
                                                </Listeners>
                                            </ext:ComboBox>--%>
                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container4" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cbxclass" runat="server" FieldLabel="班组" LabelAlign="Right"
                                                Editable="false">
                                                <Items>
                                                    <ext:ListItem Text="甲" Value="1">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="乙" Value="2">
                                                    </ext:ListItem>
                                                    <ext:ListItem Text="丙" Value="3">
                                                    </ext:ListItem>
                                                </Items>
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" Qtip="清空">
                                                    </ext:FieldTrigger>
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Handler="this.setValue('');" />
                                                </Listeners>
                                            </ext:ComboBox>

                                        </Items>
                                    </ext:Container>
                                    <ext:Container ID="Container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cboMaterName" runat="server" DisplayField="Name" ValueField="Id"
                                                Editable="false" FieldLabel="物料" LabelAlign="Right">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                </Triggers>
                                                <Store>
                                                    <ext:Store runat="server" ID="MaterNameStore" AutoLoad="true" OnReadData="MaterNameRefresh">
                                                        <Model>
                                                            <ext:Model ID="Model1" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="Id" Type="Int" Mapping="Id" />
                                                                    <ext:ModelField Name="Name" Type="String" Mapping="Name" />
                                                                    <ext:ModelField Name="PlanID" Type="String" Mapping="PlanID" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <ListConfig Width="300" Height="300" ItemSelector=".x-boundlist-item">
                                                    <Tpl ID="Tpl2" runat="server">
                                                        <Html>
                                                            <tpl for=".">
						                                        <tpl if="[xindex] == 1">
							                                        <table class="cbStates-list">
								                                        <tr>
									                                         <th>物料名称</th> 
									                                         <th>计划号</th>
								                                        </tr>
						                                        </tpl>
						                                        <tr class="x-boundlist-item">
							                                        <td>{Name}</td>
							                                        <td>{PlanID}</td>
						                                        </tr>
						                                        <tpl if="[xcount-xindex]==0">
							                                        </table>
						                                        </tpl>
					                                        </tpl>
                                                        </Html>
                                                    </Tpl>
                                                </ListConfig>
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
                                      <ext:Container ID="Container8" runat="server" Layout="FormLayout" ColumnWidth=".33" Hidden="true"
                                        Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cbxChejian" runat="server" FieldLabel="车间" LabelAlign="Right" Editable="false">
                                                <Items>
                                                    <ext:ListItem Text="全部" Value="all"></ext:ListItem>
                                                    <ext:ListItem Text="M2车间" Value="2"></ext:ListItem>
                                                    <ext:ListItem Text="M3车间" Value="3"></ext:ListItem>
                                                    <ext:ListItem Text="M4车间" Value="4"></ext:ListItem>
                                                    <ext:ListItem Text="M5车间" Value="5"></ext:ListItem>
                                                </Items>
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:GridPanel ID="pnlList" runat="server" Region="Center" Layout="FitLayout">
                <Store>
                    <ext:Store ID="Store1" GroupField="ID" runat="server">
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="Model2" runat="server">
                                <Fields>
                                    <ext:ModelField Name="ID" Type="String" />
                                    <ext:ModelField Name="PlanDate" Type="Date" />
                                    <ext:ModelField Name="EquipName" Type="String" />
                                    <ext:ModelField Name="ShiftName" Type="String" />
                                    <ext:ModelField Name="ClassName" Type="String" />
                                    <ext:ModelField Name="MaterialName" Type="String" />
                                    <ext:ModelField Name="PlanNum" Type="Int" />
                                    <ext:ModelField Name="RealNum" Type="Int" />
                                    <ext:ModelField Name="PlanWeight" Type="Int"  />
                                    <ext:ModelField Name="RealWeight" Type="Int" />
                                    <ext:ModelField Name="DiffWeight" Type="Int" />
                                    <ext:ModelField Name="RunRate" Type="String" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="ColumnModel1" runat="server">
                    <Columns>
                        <ext:DateColumn ID="DateColumn1" runat="server" Text="生产日期" Width="100" DataIndex="PlanDate"
                            Format="yyyy-MM-dd" />
                        <ext:Column ID="Column3" runat="server" Text="机台" Width="100" DataIndex="EquipName">
                        </ext:Column>
                        <ext:Column ID="Column4" runat="server" Text="班次" Width="40" DataIndex="ShiftName">
                        </ext:Column>
                        <ext:Column ID="Column5" runat="server" Text="班组" Width="40" DataIndex="ClassName">
                        </ext:Column>
                        <ext:Column ID="Column2" runat="server" Text="物料名称" Width="150" DataIndex="MaterialName">
                        </ext:Column>
                        <ext:SummaryColumn ID="Column8" runat="server" Text="计划数量" Width="60" DataIndex="PlanNum"
                            SummaryType="Sum">
                        </ext:SummaryColumn>
                        <ext:SummaryColumn ID="SummaryColumn1" runat="server" Text="完成数量" Width="60" DataIndex="RealNum"
                            SummaryType="Sum">
                        </ext:SummaryColumn>
                        <ext:SummaryColumn ID="Column10" runat="server" Text="定额重量" Width="80" DataIndex="PlanWeight"
                            SummaryType="Sum">
                        </ext:SummaryColumn>
                        <ext:SummaryColumn ID="Column11" runat="server" Text="实际重量" Width="80" DataIndex="RealWeight"
                            SummaryType="Sum">
                        </ext:SummaryColumn>
                        <ext:SummaryColumn ID="Column12" runat="server" Text="架子差值" Width="100" DataIndex="DiffWeight" SummaryType="Average">
                         <SummaryRenderer Handler="return ('AVG='+ Math.round(value));" />
                        </ext:SummaryColumn>
                        <ext:SummaryColumn ID="Column1" runat="server" Text="完成率" Width="50" DataIndex="RunRate" SummaryType="Average">
                            <Renderer Fn="pctChange" />
                            <%--<SummaryRenderer Handler="return ('AVG='+ value.substring(0, value.indexOf('%')));" />--%>
                        </ext:SummaryColumn>
                    </Columns>
                </ColumnModel>
                <SelectionModel>
                    <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Multi" />
                </SelectionModel>
                <View>
                    <ext:GridView ID="GridView1" runat="server" StripeRows="true">
                        <%--<GetRowClass Fn="getRowClass" />--%>
                    </ext:GridView>
                </View>
                <Features>
                    <ext:GroupingSummary ID="GroupingSummary1" runat="server" GroupHeaderTplString="{name}"
                        HideGroupedHeader="true" EnableGroupingMenu="false" />
                </Features>
                <BottomBar>
                    <ext:PagingToolbar ID="pageToolBar" runat="server" Hidden="true">
                        <Plugins>
                            <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                        </Plugins>
                    </ext:PagingToolbar>
                </BottomBar>
                <Listeners>
                    <%-- <CellClick Handler="#{Window1}.show();" />--%>
                </Listeners>
            </ext:GridPanel>
            <ext:Hidden ID="hidden_equip_type_name" runat="server" />
            <ext:Hidden ID="hidden_delete_flag" runat="server" Text="0">
            </ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
