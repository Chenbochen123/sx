<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImportRepairProtectPlan.aspx.cs" Inherits="Manager_Equipment_EquipRepairProtectPlan_ImportRepairProtectPlan" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="ResourceManager1" />
    <ext:Hidden runat="server" ID="HiddenGUID" />
    <ext:Hidden runat="server" ID="HiddenOper" />
    <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" ID="PanelNorth" Region="North">
                <TopBar>
                    <ext:Toolbar runat="server" ID="ToolbarNorth">
                        <Items>
                            <ext:Button runat="server" ID="ButtonNorthImport" Icon="PageExcel" Text="上传">
                                <Listeners>
                                    <Click Fn="ButtonNorthImport_Click" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthSave" Icon="PageSave" Text="保存">
                                <Listeners>
                                    <Click Fn="ButtonNorthSave_Click" />
                                </Listeners>
                            </ext:Button>

                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel runat="server" ID="PanelNorthContainer" Width="700" Layout="ColumnLayout"
                        Border="false" Height="70">
                        <Items>
                            <ext:FileUploadField runat="server" ID="FileUploadFieldNorthExcel" Icon="PageExcel"
                                ButtonText="" EmptyText="选择要上传的Excel文件" FieldLabel="生产计划数据" ColumnWidth="0.6"
                                LabelAlign="Right">
                                <Listeners>
                                    <Change Fn="FileUploadFieldNorthExcel_Change" />
                                </Listeners>
                            </ext:FileUploadField>
                            <ext:Button runat="server" ID="ButtonNorthClearExcel" Icon="PageDelete" ToolTip="清除">
                                <Listeners>
                                    <Click Handler="#{FileUploadFieldNorthExcel}.reset();" />
                                </Listeners>
                            </ext:Button>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthExcelSheets" Editable="false" AllowBlank="false"
                                EmptyText="请选择Sheet页" FieldLabel="Sheet页" ColumnWidth="0.7" LabelAlign="Right"
                                Hidden="false" InputWidth="200" MatchFieldWidth="false">
                                <ListConfig Width="200" />
                            </ext:ComboBox>
                            <ext:DateField runat="server" ID="DateFieldNorthPlanDate" Editable="false" AllowBlank="false"
                                EmptyText="请选择月份" FieldLabel="计划月份" ColumnWidth="0.452" Format="yyyy-MM" LabelAlign="Right" Type="Month">
                            </ext:DateField>
                            <ext:ComboBox runat="server" ID="ComboBoxPlanType" Editable="false" AllowBlank="false"
                                EmptyText="请选择计划类型" FieldLabel="计划类型" ColumnWidth="0.452" LabelAlign="Right">
                            </ext:ComboBox>
                        </Items>
                    </ext:Panel>
                </Items>
                <BottomBar>
                </BottomBar>
            </ext:Panel>
            <ext:Panel runat="server" ID="PanelCenter" Region="Center" Layout="BorderLayout">
                <Items>
                    <ext:TabPanel runat="server" ID="TabPanelCenter" Region="Center">
                        <TopBar>
                            <ext:StatusBar runat="server" ID="StatusBarCenter" Layout="ColumnLayout" Height="25">
                                <Items>
                                    <ext:Label runat="server" ID="LabelCenterOper" ColumnWidth=".5" />
                                    <ext:Label runat="server" ID="LabelCenterTime" ColumnWidth=".5" />
                                </Items>
                            </ext:StatusBar>
                        </TopBar>
                        <Items>
                            <ext:GridPanel runat="server" ID="GridPanelCenterError" Title="Excel文件异常数据">
                                <Store>
                                    <ext:Store runat="server" ID="StoreCenterError">
                                        <Model>
                                            <ext:Model runat="server" ID="ModelCenterError">
                                                <Fields>
                                                    <ext:ModelField Name="seq" />
                                                    <ext:ModelField Name="EquipName" />
                                                    <ext:ModelField Name="RepairProtectPlanContent"  />
                                                    <ext:ModelField Name="RepairTime" />
                                                    <ext:ModelField Name="RepairDate" />
                                                    <ext:ModelField Name="ResponseUser"  />
                                                    <ext:ModelField Name="NeedStopTime"  />
                                                    <ext:ModelField Name="PlanStopTime"  />
                                                    <ext:ModelField Name="flag" />
                                                    <ext:ModelField Name="errmsg" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:Column runat="server" ID="ColumnCenterErrorSeq" DataIndex="seq" MenuDisabled="true" Width="40" />
                                        <ext:Column runat="server" ID="ColumnCenterErrorErrMsg" DataIndex="errmsg" MenuDisabled="true"  Text="异常信息提示" Width="200" />
                                        <ext:Column runat="server" ID="ColumnCenterErrorEquipName" DataIndex="EquipName" Text="设备名称" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterErrorRepairProtectPlanContent" DataIndex="RepairProtectPlanContent" Text="计划内容" Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterErrorRepairTime" DataIndex="RepairTime" Text="检修时间" Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterErrorRepairDate" DataIndex="RepairDate" Text="检修日期" Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterErrorResponseUser" DataIndex="ResponseUser" Text="负责人" Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterErrorNeedStopTime" DataIndex="NeedStopTime" Text="需要停机时间" Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterErrorPlanStopTime" DataIndex="PlanStopTime" Text="计划停机日期" Align="Center" MenuDisabled="true" />
                                    </Columns>
                                </ColumnModel>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="PagingToolbarCenterError" HideRefresh="true" />
                                </BottomBar>
                            </ext:GridPanel>
                            <ext:GridPanel runat="server" ID="GridPanelCenterSave" Title="可以提交保存的数据">
                                <Store>
                                    <ext:Store runat="server" ID="StoreCenterSave" OnSubmitData="StoreCenterSave_SubmitData">
                                        <Model>
                                            <ext:Model runat="server" ID="ModelCenterSave">
                                                <Fields>
                                                    <ext:ModelField Name="EquipName" />
                                                    <ext:ModelField Name="RepairProtectPlanContent"  />
                                                    <ext:ModelField Name="RepairTime" />
                                                    <ext:ModelField Name="RepairDate" />
                                                    <ext:ModelField Name="ResponseUser"  />
                                                    <ext:ModelField Name="NeedStopTime"  />
                                                    <ext:ModelField Name="PlanStopTime"  />
                                                    <ext:ModelField Name="seq" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:Column runat="server" ID="ColumnCenterSaveSeq" DataIndex="seq" MenuDisabled="true" Width="40" />
                                        <ext:Column runat="server" ID="ColumnCenterSaveEquipName" DataIndex="EquipName" Text="设备名称" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSaveRepairProtectPlanContent" DataIndex="RepairProtectPlanContent" Text="计划内容" Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSaveRepairTime" DataIndex="RepairTime" Text="检修时间" Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSaveRepairDate" DataIndex="RepairDate" Text="检修日期" Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSaveResponseUser" DataIndex="ResponseUser" Text="负责人" Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSaveNeedStopTime" DataIndex="NeedStopTime" Text="需要停机时间" Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSavePlanStopTime" DataIndex="PlanStopTime" Text="计划停机日期" Align="Center" MenuDisabled="true" />
                                    </Columns>
                                </ColumnModel>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="PagingToolbarCenterSave" HideRefresh="true" />
                                </BottomBar>
                            </ext:GridPanel>
                            <ext:GridPanel runat="server" ID="GridPanelCenterOrigin" Title="Excel文件全部数据">
                                <Store>
                                    <ext:Store runat="server" ID="StoreCenterOrigin">
                                        <Model>
                                            <ext:Model runat="server" ID="ModelCenterOrigin">
                                                <Fields>
                                                    <ext:ModelField Name="seq" />
                                                    <ext:ModelField Name="EquipName" />
                                                    <ext:ModelField Name="RepairProtectPlanContent"  />
                                                    <ext:ModelField Name="RepairTime" />
                                                    <ext:ModelField Name="RepairDate" />
                                                    <ext:ModelField Name="ResponseUser"  />
                                                    <ext:ModelField Name="NeedStopTime"  />
                                                    <ext:ModelField Name="PlanStopTime"  />
                                                    <ext:ModelField Name="flag" />
                                                    <ext:ModelField Name="errmsg" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:Column runat="server" ID="ColumnCenterOriginSeq" DataIndex="seq" MenuDisabled="true" Width="40" />
                                        <ext:Column runat="server" ID="ColumnCenterOriginEquipName" DataIndex="EquipName" Text="设备名称" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterOriginRepairProtectPlanContent" DataIndex="RepairProtectPlanContent" Text="计划内容" Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterOriginRepairTime" DataIndex="RepairTime" Text="检修时间" Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterOriginRepairDate" DataIndex="RepairDate" Text="检修日期" Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterOriginResponseUser" DataIndex="ResponseUser" Text="负责人" Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterOriginNeedStopTime" DataIndex="NeedStopTime" Text="需要停机时间" Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterOriginPlanStopTime" DataIndex="PlanStopTime" Text="计划停机日期" Align="Center" MenuDisabled="true" />
                                    </Columns>
                                </ColumnModel>
                                <View>
                                    <ext:GridView runat="server" ID="GridViewCenterOrigin" StripeRows="true" TrackOver="true">
                                        <GetRowClass Fn="setRowClass" />
                                    </ext:GridView>
                                </View>
                                <ToolTips>
                                    <ext:ToolTip ID="ToolTipCenterOrigin" runat="server" Delegate="tr.x-grid-row" TrackMouse="true">
                                        <Listeners>
                                            <Show Fn="ToolTipCenterOrigin_Show" />
                                        </Listeners>
                                    </ext:ToolTip>
                                </ToolTips>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="PagingToolbarCenterOrigin" HideRefresh="true" />
                                </BottomBar>
                            </ext:GridPanel>
                        </Items>
                    </ext:TabPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>