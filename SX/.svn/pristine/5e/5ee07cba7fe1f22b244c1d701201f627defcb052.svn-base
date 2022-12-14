<%@ page language="C#" autoeventwireup="true" inherits="Manager_RawMaterialQuality_TDMCheck, App_Web_drvpsf3a" %>


<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>原材料质检报告</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js" ></script>
    <script type="text/javascript" language="javascript">
   

        var setRowClass = function (record, index, rowParams, store) {
            if (record.get('AutoCheckResult') == '0') {
                return 'x-grid-row-deleted';
            }
        };

        var GridViewCenterMaster_GetRowClass = function (record, index, rowParams, store) {
            if (record.get("finalResult") != "合格") {
                return 'x-grid-row-deleted';
            }
            return "";
        };

        var ColumnCenterMasterRecordStatDes_Renderer = function (value, metadata, record, rowIndex, colIndex, store, view) {
            if (record.get("RecordStat") == "1") {
                return "<span style='background-color: lightgreen'>" + value + "</span>";
            }
            else if (record.get("RecordStat") == "0") {
                return "<span style='background-color: yellow'>" + value + "</span>";
            }
            return value;
        };

        var ColumnCenterMasterCheckResultDes_Renderer = function (value, metadata, record, rowIndex, colIndex, store, view) {
            if (record.get("CheckResult") == "0") {
                return "<span style='background-color: red'>" + value + "</span>";
            }
            return value;
        };
    </script>
   
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="rmMaterialCheckReport" />
    <ext:Viewport runat="server" ID="vwUnit" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" ID="pnlNorth" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="barUnit1">
                        <Items>
                            <ext:Button runat="server" ID="btnSearch" Icon="Find" Text="查询">
                              
                                <DirectEvents>
                                    <Click OnEvent="btnSearch_Click">
                                        <EventMask ShowMask="true" Target="Page" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator1" />
                  
                            <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end" />
                            <ext:ToolbarFill ID="toolbarFill_end" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:FormPanel runat="server" ID="pnlForm" Layout="AnchorLayout" AutoHeight="true">
                        <Items>
                           <ext:Container ID="container1" runat="server" Layout="HBoxLayout" Padding="5" >
                                <Items>
                                 
                                   
                                  
                                </Items>
                            </ext:Container>
                            <ext:Container ID="container2" runat="server" Layout="HBoxLayout" Padding="5" >
                                <Items>
                                    <ext:TextField runat="server" ID="TextFieldNorthBarcode" FieldLabel="条码号" LabelAlign="Right" InputWidth="181" >
                                    </ext:TextField>
                                    <ext:DateField runat="server" ID="StartDate" FieldLabel="开始日期" LabelAlign="Right"
                                        Format="yyyy-MM-dd" Editable="false" >
                                       <%-- <Triggers>
                                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Handler="this.setValue('');" />
                                        </Listeners>--%>
                                    </ext:DateField>
                                     <ext:DateField runat="server" ID="EndDate" FieldLabel="结束日期" LabelAlign="Right"
                                        Format="yyyy-MM-dd" Editable="false" >
                                       <%-- <Triggers>
                                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Handler="this.setValue('');" />
                                        </Listeners>--%>
                                    </ext:DateField>
                                    <ext:ComboBox runat="server" ID="ComboBoxNorthCheckResult" FieldLabel="是否合格" LabelAlign="Right"
                                        Editable="false" MatchFieldWidth="false">
                                        <ListConfig Width="110" />
                                        <Items>
                                            <ext:ListItem Value="1" Text="合格" />
                                            <ext:ListItem Value="0" Text="不合格" />
                                        </Items>
                                        <Triggers>
                                            <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                        </Triggers>
                                        <Listeners>
                                            <TriggerClick Handler="this.setValue('');" />
                                        </Listeners>
                                    </ext:ComboBox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" ID="pnlCenter" Region="Center" Layout="BorderLayout">
                <Items>
                   <ext:GridPanel runat="server" ID="GridPanelCenterMaster" Region="Center">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenterMaster" PageSize="10">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenterMaster" >
                                        <Fields>
                                            <ext:ModelField Name="Objid" />
                                            <ext:ModelField Name="BillNo" />
                                            <ext:ModelField Name="finalResult" />
                                            <ext:ModelField Name="Remark" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="Recordtime" Type="Date" />
                                         
                                        </Fields>
                                    </ext:Model>
                                </Model>
                              
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel1" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol1" runat="server" Width="35" />
                                <ext:Column runat="server" ID="ColumnCenterMasterMaterName" DataIndex="Objid"
                                    Text="编号" Width="120" />
                                <ext:Column runat="server" ID="ColumnCenterMasterMaterSpecName" DataIndex="BillNo"
                                    Text="批次号" Width="120" />
                                <ext:Column runat="server" ID="Column1" DataIndex="MaterialName" Text="物料名称" />
                                <ext:Column runat="server" ID="ColumnCenterMasterBarcode" DataIndex="finalResult" Text="质检结果" />
                                <ext:Column runat="server" ID="ColumnCenterMasterBatchCode" DataIndex="Remark" Text="条码号" Width="205" />
                                <ext:DateColumn runat="server" ID="DateColumnCenterMasterCheckDate" DataIndex="Recordtime" Width="155"
                                    Text="检验日期" Format="yyyy-MM-dd HH:mm:ss" />
                              
                            </Columns>
                        </ColumnModel>
                         
                        <SelectionModel>
                            <ext:CheckboxSelectionModel runat="server" ID="CheckboxSelectionModelCenterMaster"
                                Mode="Single" >
                                <DirectEvents>
                                    <Select OnEvent="CheckboxSelectionModelCenterMaster_SelectionChange">
                                        <ExtraParams>
                                            <ext:Parameter Mode="Raw" Name="Objid" Value="record.get('Objid')" />
                                        </ExtraParams>
                                    </Select>
                                </DirectEvents>
                            </ext:CheckboxSelectionModel>
                        </SelectionModel>
                        <View>
                            <ext:GridView runat="server" ID="GridViewCenterMaster">
                                <GetRowClass Fn="GridViewCenterMaster_GetRowClass" />
                            </ext:GridView>
                        </View>
                        <BottomBar>
                            <ext:PagingToolbar runat="server" ID="PagingToolbarCenterMaster" HideRefresh="true" />
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="pnlSouth" runat="server" Region="South" Title="质检数据" Height="200" Icon="Basket" Layout="Fit" Collapsible="true" Split="true" MarginsSummary="0 5 5 5">
                <TopBar><ext:Toolbar ID="Toolbar1" runat="server" Height="1"></ext:Toolbar></TopBar>
                <Items>
                     <ext:GridPanel runat="server" ID="GridPanelCenterDeail" Region="South" AutoScroll="true"
                        Height="150" Padding="2">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenterDetail">
                                <Model>
                                    <ext:Model runat="server" ID="ModelCenterDetail">
                                        <Fields>
<%--                                            <ext:ModelField Name="ItemName" />--%>
                                            <ext:ModelField Name="ZhuJian" />
                                            <ext:ModelField Name="CanShu" />
                                            <ext:ModelField Name="FangFa" />
                                            <ext:ModelField Name="BiaoZhun" />
                                            <ext:ModelField Name="FanWei" />
                                            <ext:ModelField Name="JieGuo" />
                                            <ext:ModelField Name="JieLun" />
                                     
                                            <ext:ModelField Name="XiangMu" />
                                          
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <%--<ext:Column runat="server" ID="ColumnCenterDetailItemName" DataIndex="ZhuJian" Text="业务主键"
                                    Width="200" />--%>
                      <ext:Column runat="server" ID="ColumnCenterDetailCheckMethod" DataIndex="XiangMu"
                                    Text="检测项目" Width="200" />
                                <ext:Column runat="server" ID="ColumnCenterDetailCheckRange" DataIndex="CanShu"
                                    Text="检测参数" Width="150"/>
                                <ext:Column runat="server" ID="ColumnCenterDetailCheckValue" DataIndex="FangFa"
                                    Text="检测方法" />
                                <ext:Column runat="server" ID="CheckColumnCenterDetailTextCheckResult" DataIndex="BiaoZhun"
                                    Text="执行标准" />
                                <ext:Column runat="server" ID="CheckColumnCenterDetailTextIsPrime" DataIndex="FanWei"
                                    Text="判定范围" />
                                <ext:Column runat="server" ID="ColumnCenterDetailAutoPrimeCheckRange" DataIndex="JieGuo"
                                    Text="测试结果" Width="150"/>
                                <ext:Column runat="server" ID="ColumnCenterDetailFrequency" DataIndex="JieLun"
                                    Text="分项结论" />
                               
                            </Columns>
                        </ColumnModel>
                         <View>
                            <ext:GridView runat="server" ID="GridViewCenterDetail" StripeRows="true" TrackOver="true">
                                <GetRowClass Fn="setRowClass" />
                            </ext:GridView>
                         </View>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        
        
            <ext:Hidden ID="txtHiddenCheckId" runat="server" />
            <ext:Hidden ID="txtHiddenSelectCommand" runat="server" />
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
