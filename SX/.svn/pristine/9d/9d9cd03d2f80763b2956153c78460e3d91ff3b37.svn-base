<%@ page language="C#" autoeventwireup="true" inherits="Manager_RubberQuality_BasicInfo_CheckDataImport, App_Web_1dvad4ic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="ResourceManager1" />
    <ext:Hidden runat="server" ID="HiddenGUID" />
    <ext:Hidden runat="server" ID="HiddenOper" />
    <ext:Hidden runat="server" ID="HiddenIsReCheck" />
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
                        Border="false" Height="100">
                        <Items>
                            <ext:FileUploadField runat="server" ID="FileUploadFieldNorthExcel" Icon="PageExcel"
                                ButtonText="" EmptyText="选择要上传的Excel文件" FieldLabel="胶料质检数据" ColumnWidth=".7"
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
                            <ext:Checkbox runat="server" ID="CheckBoxNorthReCheck" FieldLabel="是复检文件" ColumnWidth=".25"
                                LabelAlign="Right" />
                            <ext:ComboBox runat="server" ID="ComboBoxNorthExcelSheets" Editable="false" AllowBlank="false"
                                EmptyText="请选择Sheet页" FieldLabel="Sheet页" ColumnWidth="0.65" LabelAlign="Right"
                                Hidden="false" InputWidth="200" MatchFieldWidth="false">
                                <ListConfig Width="200" />
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthShiftId" Editable="false" AllowBlank="false"
                                EmptyText="请选择班次" FieldLabel="生产班次" ColumnWidth=".35" LabelAlign="Right" />
                            <ext:DateField runat="server" ID="DateFieldNorthCheckPlanDate" Editable="false" AllowBlank="false"
                                EmptyText="请选择日期" FieldLabel="质检日期" ColumnWidth=".3" Format="yyyy-MM-dd" LabelAlign="Right">
                                <Listeners>
                                    <Change Fn="SetComboBoxNorthShiftCheckId" />
                                </Listeners>
                            </ext:DateField>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthCheckPlanClass" Editable="false" AllowBlank="false"
                                EmptyText="请选择班组" FieldLabel="检验班组" ColumnWidth=".35" LabelAlign="Right">
                                <Listeners>
                                    <Change Fn="SetComboBoxNorthShiftCheckId" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthShiftCheckId" Editable="false" AllowBlank="false"
                                EmptyText="请选择班次" FieldLabel="检验班次" ColumnWidth=".35" LabelAlign="Right" />
                            <ext:DateField runat="server" ID="DateFieldNorthCheckDate" Editable="false" AllowBlank="false"
                                EmptyText="请选择日期" FieldLabel="检测时间" ColumnWidth=".3" Format="yyyy-MM-dd" LabelAlign="Right" />
                            <ext:TimeField runat="server" ID="TimeFieldNorthCheckTime" AllowBlank="false" EmptyText="请选择时间"
                                ColumnWidth=".15" Format="HH:mm:ss" />
                            <ext:DisplayField ColumnWidth=".1" runat="server" />
                            <ext:ComboBox runat="server" ID="ComboBoxNorthStandCode" Editable="false" AllowBlank="false"
                                EmptyText="请选择标准类型" FieldLabel="标准类型" ColumnWidth=".45" LabelAlign="Right" />
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
                                                    <ext:ModelField Name="ProductNo" />
                                                    <ext:ModelField Name="RubberName" />
                                                    <ext:ModelField Name="101" />
                                                    <ext:ModelField Name="102" />
                                                    <ext:ModelField Name="201" />
                                                    <ext:ModelField Name="202" />
                                                    <ext:ModelField Name="203" />
                                                    <ext:ModelField Name="204" />
                                                    <ext:ModelField Name="205" />
                                                    <ext:ModelField Name="206" />
                                                    <ext:ModelField Name="301" />
                                                    <ext:ModelField Name="401" />
                                                    <ext:ModelField Name="501" />
                                                    <ext:ModelField Name="flag" />
                                                    <ext:ModelField Name="errmsg" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:Column runat="server" ID="ColumnCenterErrorSeq" DataIndex="seq" MenuDisabled="true"
                                            Width="40" />
                                        <ext:Column runat="server" ID="ColumnCenterErrorErrMsg" DataIndex="errmsg" MenuDisabled="true"
                                            Text="异常信息提示" Width="200" />
                                        <ext:Column runat="server" ID="ColumnCenterErrorProductNo" DataIndex="ProductNo"
                                            Text="炼胶批次" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterErrorRubberName" DataIndex="RubberName"
                                            Text="胶料名称" Align="Center" MenuDisabled="true" />
                                        <ext:ComponentColumn runat="server" ID="ComponentColumnCenterErrorCuring" Text="硫变仪数据"
                                            Align="Center" MenuDisabled="true">
                                            <Columns>
                                                <ext:Column runat="server" ID="ColumnCenterError201" DataIndex="201" Text="ML" Align="Center"
                                                    MenuDisabled="true" />
                                                <ext:Column runat="server" ID="ColumnCenterError202" DataIndex="202" Text="MH" Align="Center"
                                                    MenuDisabled="true" />
                                                <ext:Column runat="server" ID="ColumnCenterError203" DataIndex="203" Text="Ts1" Align="Center"
                                                    MenuDisabled="true" />
                                                <ext:Column runat="server" ID="ColumnCenterError205" DataIndex="205" Text="T30" Align="Center"
                                                    MenuDisabled="true" />
                                                <ext:Column runat="server" ID="ColumnCenterError206" DataIndex="206" Text="T60" Align="Center"
                                                    MenuDisabled="true" />
                                                <ext:Column runat="server" ID="ColumnCenterError204" DataIndex="204" Text="T25" Align="Center"
                                                    MenuDisabled="true" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn runat="server" ID="ComponentColumnCenterError101" Text="粘度"
                                            Align="Center" MenuDisabled="true">
                                            <Columns>
                                                <ext:Column runat="server" ID="ColumnCenterError101" DataIndex="101" Text="4.0" Align="Center"
                                                    MenuDisabled="true" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn runat="server" ID="ComponentColumnCenterError102" Text="焦烧"
                                            Align="Center" MenuDisabled="true">
                                            <Columns>
                                                <ext:Column runat="server" ID="ColumnCenterError102" DataIndex="102" Text="T5" Align="Center"
                                                    MenuDisabled="true" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:Column runat="server" ID="ColumnCenterError401" DataIndex="401" Text="比重" Align="Center"
                                            MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterError301" DataIndex="301" Text="硬度" Align="Center"
                                            MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterError501" DataIndex="501" Text="抽出" Align="Center"
                                            MenuDisabled="true" />
                                    </Columns>
                                </ColumnModel>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="PagingToolbarCenterError" HideRefresh="true" />
                                </BottomBar>
                            </ext:GridPanel>
                            <ext:GridPanel runat="server" ID="GridPanelCenterSave" Title="提交保存的数据">
                                <Store>
                                    <ext:Store runat="server" ID="StoreCenterSave" OnSubmitData="StoreCenterSave_SubmitData">
                                        <Model>
                                            <ext:Model runat="server" ID="ModelCenterSave">
                                                <Fields>
                                                    <ext:ModelField Name="PlanDate" />
                                                    <ext:ModelField Name="EquipCode" />
                                                    <ext:ModelField Name="ShiftId" />
                                                    <ext:ModelField Name="ShiftClass" />
                                                    <ext:ModelField Name="MaterCode" />
                                                    <ext:ModelField Name="SerialId" />
                                                    <ext:ModelField Name="CheckDate" />
                                                    <ext:ModelField Name="CheckTime" />
                                                    <ext:ModelField Name="ShiftCheckId" />
                                                    <ext:ModelField Name="CheckEquipCode" />
                                                    <ext:ModelField Name="WorkshopID" />
                                                    <ext:ModelField Name="seq" />
                                                    <ext:ModelField Name="EquipName" />
                                                    <ext:ModelField Name="ShiftName" />
                                                    <ext:ModelField Name="ClassName" />
                                                    <ext:ModelField Name="MaterName" />
                                                    <ext:ModelField Name="WorkshopName" />
                                                    <ext:ModelField Name="StandCode" />
                                                    <ext:ModelField Name="NeedJudgeGrade" />
                                                    <ext:ModelField Name="NeedAssess" />
                                                    <ext:ModelField Name="CheckNum" />
                                                    <ext:ModelField Name="101" />
                                                    <ext:ModelField Name="102" />
                                                    <ext:ModelField Name="201" />
                                                    <ext:ModelField Name="202" />
                                                    <ext:ModelField Name="203" />
                                                    <ext:ModelField Name="204" />
                                                    <ext:ModelField Name="205" />
                                                    <ext:ModelField Name="206" />
                                                    <ext:ModelField Name="301" />
                                                    <ext:ModelField Name="401" />
                                                    <ext:ModelField Name="501" />
                                                    <ext:ModelField Name="BatchNo" />
                                                    <ext:ModelField Name="ZJSID" />
                                                    <ext:ModelField Name="CheckPlanDate" />
                                                    <ext:ModelField Name="CheckPlanClass" />
                                                    <ext:ModelField Name="CheckPlanClassName" />
                                                    <ext:ModelField Name="CheckDateTime" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:Column runat="server" ID="ColumnCenterSaveSeq" DataIndex="seq" MenuDisabled="true"
                                            Width="40" />
                                        <ext:Column runat="server" ID="ColumnCenterSavePlanDate" DataIndex="PlanDate" Text="生产日期"
                                            MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSaveEquipName" DataIndex="EquipName" Text="生产机台"
                                            Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSaveShiftName" DataIndex="ShiftName" Text="生产班次"
                                            Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSaveClassName" DataIndex="ClassName" Text="生产班组"
                                            Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSaveZJSID" DataIndex="ZJSID" Text="主机手"
                                            Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSaveMaterName" DataIndex="MaterName" Text="胶料名称"
                                            Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSaveCheckNum" DataIndex="CheckNum" Text="检验次数"
                                            Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSaveSerialId" DataIndex="SerialId" Text="玲珑车次"
                                            Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSaveCheckPlanDate" DataIndex="CheckPlanDate"
                                            Text="质检日期" Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSaveCheckPlanClassName" DataIndex="CheckPlanClassName"
                                            Text="质检班组" Align="Center" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSaveCheckDateTime" DataIndex="CheckDateTime"
                                            Text="检测时间" Align="Center" MenuDisabled="true" Width="200" />
                                        <ext:Column runat="server" ID="ColumnCenterSaveWorkshopName" DataIndex="WorkshopName"
                                            Text="车间" Align="Center" MenuDisabled="true" />
                                        <ext:ComponentColumn runat="server" ID="ComponentColumnCenterSaveCuring" Text="硫变仪数据"
                                            Align="Center" MenuDisabled="true">
                                            <Columns>
                                                <ext:Column runat="server" ID="ColumnCenterSave201" DataIndex="201" Text="ML" Align="Center"
                                                    MenuDisabled="true" />
                                                <ext:Column runat="server" ID="ColumnCenterSave202" DataIndex="202" Text="MH" Align="Center"
                                                    MenuDisabled="true" />
                                                <ext:Column runat="server" ID="ColumnCenterSave203" DataIndex="203" Text="Ts1" Align="Center"
                                                    MenuDisabled="true" />
                                                <ext:Column runat="server" ID="ColumnCenterSave205" DataIndex="205" Text="T30" Align="Center"
                                                    MenuDisabled="true" />
                                                <ext:Column runat="server" ID="ColumnCenterSave206" DataIndex="206" Text="T60" Align="Center"
                                                    MenuDisabled="true" />
                                                <ext:Column runat="server" ID="ColumnCenterSave204" DataIndex="204" Text="T25" Align="Center"
                                                    MenuDisabled="true" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn runat="server" ID="ComponentColumnCenterSave101" Text="粘度" Align="Center"
                                            MenuDisabled="true">
                                            <Columns>
                                                <ext:Column runat="server" ID="ColumnCenterSave101" DataIndex="101" Text="4.0" Align="Center"
                                                    MenuDisabled="true" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn runat="server" ID="ComponentColumnCenterSave102" Text="焦烧" Align="Center"
                                            MenuDisabled="true">
                                            <Columns>
                                                <ext:Column runat="server" ID="ColumnCenterSave102" DataIndex="102" Text="T5" Align="Center"
                                                    MenuDisabled="true" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:Column runat="server" ID="ColumnCenterSave401" DataIndex="401" Text="比重" Align="Center"
                                            MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSave301" DataIndex="301" Text="硬度" Align="Center"
                                            MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterSave501" DataIndex="501" Text="抽出" Align="Center"
                                            MenuDisabled="true" />
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
                                                    <ext:ModelField Name="ProductNo" />
                                                    <ext:ModelField Name="RubberName" />
                                                    <ext:ModelField Name="101" />
                                                    <ext:ModelField Name="102" />
                                                    <ext:ModelField Name="201" />
                                                    <ext:ModelField Name="202" />
                                                    <ext:ModelField Name="203" />
                                                    <ext:ModelField Name="204" />
                                                    <ext:ModelField Name="205" />
                                                    <ext:ModelField Name="206" />
                                                    <ext:ModelField Name="301" />
                                                    <ext:ModelField Name="401" />
                                                    <ext:ModelField Name="501" />
                                                    <ext:ModelField Name="flag" />
                                                    <ext:ModelField Name="errmsg" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:Column runat="server" ID="ColumnCenterOriginSeq" DataIndex="seq" MenuDisabled="true"
                                            Width="40" />
                                        <ext:Column runat="server" ID="ColumnCenterOriginProductNo" DataIndex="ProductNo"
                                            Text="炼胶批次" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterOriginRubberName" DataIndex="RubberName"
                                            Text="胶料名称" Align="Center" MenuDisabled="true" />
                                        <ext:ComponentColumn runat="server" ID="ComponentColumnCenterOrigin200" Text="硫变仪数据"
                                            Align="Center" MenuDisabled="true">
                                            <Columns>
                                                <ext:Column runat="server" ID="ColumnCenterOrigin201" DataIndex="201" Text="ML" Align="Center"
                                                    MenuDisabled="true" />
                                                <ext:Column runat="server" ID="ColumnCenterOrigin202" DataIndex="202" Text="MH" Align="Center"
                                                    MenuDisabled="true" />
                                                <ext:Column runat="server" ID="ColumnCenterOrigin203" DataIndex="203" Text="Ts1"
                                                    Align="Center" MenuDisabled="true" />
                                                <ext:Column runat="server" ID="ColumnCenterOrigin205" DataIndex="205" Text="T30"
                                                    Align="Center" MenuDisabled="true" />
                                                <ext:Column runat="server" ID="ColumnCenterOrigin206" DataIndex="206" Text="T60"
                                                    Align="Center" MenuDisabled="true" />
                                                <ext:Column runat="server" ID="ColumnCenterOrigin204" DataIndex="204" Text="T25"
                                                    Align="Center" MenuDisabled="true" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn runat="server" ID="ComponentColumnCenterOrigin101" Text="粘度"
                                            Align="Center" MenuDisabled="true">
                                            <Columns>
                                                <ext:Column runat="server" ID="ColumnCenterOrigin101" DataIndex="101" Text="4.0"
                                                    Align="Center" MenuDisabled="true" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn runat="server" ID="ComponentColumnCenterOrigin102" Text="焦烧"
                                            Align="Center" MenuDisabled="true">
                                            <Columns>
                                                <ext:Column runat="server" ID="ColumnCenterOrigin102" DataIndex="102" Text="T5" Align="Center"
                                                    MenuDisabled="true" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:Column runat="server" ID="ColumnCenterOrigin401" DataIndex="401" Text="比重" Align="Center"
                                            MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterOrigin301" DataIndex="301" Text="硬度" Align="Center"
                                            MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterOrigin501" DataIndex="501" Text="抽出" Align="Center"
                                            MenuDisabled="true" />
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
