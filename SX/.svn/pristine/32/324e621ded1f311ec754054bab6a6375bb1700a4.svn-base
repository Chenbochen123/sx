<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarCodeMissScan.aspx.cs"
    Inherits="Manager_ProducingPlan_BarcodeScan_BarCodeMissScan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>条码漏扫查询</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        //列表刷新数据重载方法
        var pnlListFresh = function () {
            App.hidden_delete_flag.setValue("0");
            //App.Store1.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

        var SelectEquipID = function (field, trigger, index) {
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hidden_select_equip_code.setValue("");
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
        var SetChkFlag = function () {

            Ext.Msg.confirm("提示", '确定要维护漏扫原因吗？', function (btn) { commandcolumn_direct_sendchkflag(btn) });

        }
        var commandcolumn_direct_sendchkflag = function (btn) {
            if (btn != "yes") {
                return;
            }
            App.direct.btnBatchSend_Click({
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var SetSendChkFlag = function () {
            var section = App.GridPanel1.getView().getSelectionModel().getSelection();

            if (section && section.length == 0) {
                alert('您没有选择任何项，请选择！');
            }
            else {
             
            }
        }

        var commandcolumn_click = function (command, record) {
            if (command.toLowerCase() == "edit") {
                commandcolumn_direct_edit(record);
            }
          
            return false;
        };


        var commandcolumn_direct_edit = function (record) {
            var ObjID = record.data.ObjID;

            App.modify_fbarcode.setValue(record.data.Barcode);
            App.modify_matername.setValue(record.data.WMaterName);
            App.modify_weight.setValue(record.data.SetWeight);
            App.TextYY.setValue(record.data.Memo);

            App.modify_sbarcode.setValue('');
            App.winModify.show();

            //Ext.Msg.alert('操作', record.data.Barcode);
          
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
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

                                <ext:ToolbarSeparator ID="tsMiddle" />
                           
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
                                                FieldLabel="生产开始日期" LabelAlign="Right" EnableKeyEvents="true" Format="yyyy-MM-dd">
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
                                    <ext:Container ID="Container8" runat="server" Layout="FormLayout" ColumnWidth=".33"
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
                                    <ext:Container ID="Container3" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="cboClass" runat="server" Editable="false" FieldLabel="班组" LabelAlign="Right">
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
                                     <ext:Container ID="Container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                            <ext:ComboBox ID="ComboBox1" runat="server" Editable="false" FieldLabel="状态" LabelAlign="Right">
                                               
                                            </ext:ComboBox>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:GridPanel ID="GridPanel1" runat="server" Region="Center" AnimCollapse="true">
                <Store>
                    <ext:Store ID="Store2" runat="server" IgnoreExtraFields="false" PageSize="50">
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="Model1" runat="server">
                                <Fields>
                                    <ext:ModelField Name="EquipName" Type="String" />
                                    <ext:ModelField Name="ShiftName" Type="String" />
                                    <ext:ModelField Name="ClassName" Type="String" />
                                    <ext:ModelField Name="MaterName" Type="String" />
                                    <ext:ModelField Name="SerialBatchID" Type="String" />
                                    <ext:ModelField Name="StartDatetime"/>
                                    <ext:ModelField Name="Barcode" Type="String" />
                                    <ext:ModelField Name="WeightID" Type="Int" />
                                    <ext:ModelField Name="WMaterName" Type="String" />
                                    <ext:ModelField Name="SetWeight" Type="Float" />
                                    <ext:ModelField Name="RealWeight" Type="Float" />
                                    <ext:ModelField Name="MaterBarcode" Type="String" />
                                    <ext:ModelField Name="ErrorAllow" Type="Float" />
                                    <ext:ModelField Name="ErrorOut" Type="Float" />
                                    <ext:ModelField Name="WarningSgn" Type="String" />
                                    <ext:ModelField Name="MaterQua" Type="String" />
                                    <ext:ModelField Name="WeighTime" Type="String" />
                                    <ext:ModelField Name="WeighType" Type="String" />
                                    <ext:ModelField Name="WeighState" Type="String" />
                                      <ext:ModelField Name="Memo" Type="String" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="ColumnModel2" runat="server">
                    <Columns>
                        <ext:Column ID="Column3" runat="server" Text="机台" Width="100" DataIndex="EquipName">
                        </ext:Column>
                        <ext:Column ID="Column4" runat="server" Text="班次" Width="40" DataIndex="ShiftName">
                        </ext:Column>
                        <ext:Column ID="Column5" runat="server" Text="班组" Width="40" DataIndex="ClassName">
                        </ext:Column>
                        <ext:Column ID="Column2" runat="server" Text="物料名称" Width="150" DataIndex="MaterName">
                        </ext:Column>
                        <ext:Column ID="Column8" runat="server" Text="累计车次号" Width="70" DataIndex="SerialBatchID">
                        </ext:Column>
                        <ext:Column ID="Column10" runat="server" Text="开始混炼时间" Width="180" DataIndex="StartDatetime">
                        </ext:Column>
                           <ext:Column ID="Column1" runat="server" Text="漏扫原因" Width="180" DataIndex="Memo">
                        </ext:Column>
                         <ext:CommandColumn ID="commandCol" runat="server" Width="120" Text="操作" Align="Center">
                                <Commands>
                                    <ext:GridCommand Icon="TableEdit" CommandName="Edit" Text="修改">
                                        <ToolTip Text="修改本条数据" />
                                    </ext:GridCommand>
                            
                                </Commands>
                      
                                <Listeners>
                                    <Command Handler="return commandcolumn_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                    </Columns>
                </ColumnModel>
                <Plugins>
                    <ext:RowExpander ID="RowExpander1" runat="server" SingleExpand="false">
                        <Component>
                            <ext:FormPanel ID="FormPanel2" runat="server" BodyPadding="6" Height="420" Border="false"
                                DefaultAnchor="-5" Cls="white-footer">
                                <Items>
                                    <ext:TextField ID="TextField3" runat="server" Name="Barcode" Width="50" ReadOnly="true" FieldLabel="车条码" />
                                    <ext:TextField ID="TextField4" runat="server" Name="WeightID" ReadOnly="true" FieldLabel="序号" />
                                    <ext:TextField ID="TextField5" runat="server" Name="WMaterName" ReadOnly="true" FieldLabel="物料名称" />
                                    <ext:TextField ID="TextField1" runat="server" Name="SetWeight" ReadOnly="true" FieldLabel="设定重量" />
                                    <ext:TextField ID="TextField2" runat="server" Name="RealWeight" ReadOnly="true" FieldLabel="实际重量" />
                                    <ext:TextField ID="TextField6" runat="server" Name="MaterBarcode" ReadOnly="true"
                                        FieldLabel="物料条码" />
                                    <ext:TextField ID="TextField7" runat="server" Name="ErrorAllow" ReadOnly="true" FieldLabel="允许条码" />
                                    <ext:TextField ID="TextField8" runat="server" Name="ErrorOut" ReadOnly="true" FieldLabel="称量误差" />
                                    <ext:TextField ID="TextField9" runat="server" Name="WarningSgn" ReadOnly="true" FieldLabel="称量报警" />
                               <%--     <ext:TextField ID="TextField10" runat="server" Name="SetWeight" ReadOnly="true" FieldLabel="物料质量" />--%>
                                    <ext:DateField ID="DateField1" runat="server" Name="WeighTime" FieldLabel="称量时间"
                                        ReadOnly="true" Format="yyyy-MM-dd HH:mm:ss" />
                                    <ext:TextField ID="TextField11" runat="server" Name="WeighType" ReadOnly="true" FieldLabel="称量类型" />
                                    <ext:TextField ID="TextField12" runat="server" Name="WeighState" ReadOnly="true" FieldLabel="称量状态" />
                             

                                </Items>
                                <Listeners>
                                    <AfterRender Handler="this.getForm().loadRecord(this.record);" />
                                </Listeners>
                            </ext:FormPanel>
                        </Component>
                    </ext:RowExpander>
                </Plugins>
                <BottomBar>
                   <ext:PagingToolbar ID="pageToolBar" runat="server">
                        <Plugins>
                            <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                        </Plugins>
                    </ext:PagingToolbar>
                </BottomBar>
            </ext:GridPanel>
     


              <ext:Window ID="winModify" runat="server" Icon="MonitorEdit" Closable="false" Title="补充条码追溯"
                    Width="320" Height="240" Resizable="false" Hidden="true" Modal="false" BodyStyle="background-color:#fff;"
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
                                <ext:TextField ID="modify_fbarcode" runat="server" FieldLabel="车条码"   LabelAlign="Left" ReadOnly=true  Enabled="true" />
                                <ext:TextField ID="modify_matername" runat="server" FieldLabel="物料名称"   LabelAlign="Left" ReadOnly=true Enabled="true" />
                                <ext:TextField ID="modify_weight" runat="server" FieldLabel="设定重量"  LabelAlign="Left" IndicatorCls="red-text"   ReadOnly=true />
                                  <ext:TextField ID="TextYY" runat="server" FieldLabel="漏扫原因" LabelAlign="Left"    />
                                <ext:TextField ID="modify_sbarcode" runat="server" FieldLabel="补充条码" LabelAlign="Left"  AllowBlank="false"  Hidden="true" />
                               </Items>
                           
                        </ext:FormPanel>
                    </Items>
                    <Buttons>
                         <ext:Button runat="server" Icon="Add" Text="补充" ID="Button3"  Width="30">
                                <Listeners>
                                    <Click Fn="SetChkFlag">
                                    </Click>
                                </Listeners>
                              
                            </ext:Button>
                        <ext:Button ID="btnModifyCancel" runat="server" Text="取消" Icon="Cancel">
                            <DirectEvents>
                                <Click OnEvent="BtnCancel_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                   
                </ext:Window>
            <ext:Hidden ID="hidden_equip_type_name" runat="server" />
            <ext:Hidden ID="hidden_delete_flag" runat="server" Text="0">
            </ext:Hidden>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
