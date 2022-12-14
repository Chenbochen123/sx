<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckStandImport.aspx.cs"
    Inherits="Manager_RubberQuality_BasicInfo_CheckStandImport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" ID="ResourceManager1" />
    <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" ID="PanelNorth" Region="North">
                <Items>
                    <ext:FormPanel runat="server" ID="FormPanelNorth" Height="150" Width="500" Frame="true"
                        LabelWidth="50" Layout="ColumnLayout">
                        <Items>
                            <ext:FileUploadField runat="server" ID="FileUploadFieldNorthExcel" EmptyText="选择导入的Excel文件"
                                FieldLabel="胶料质检标准" ButtonText="" Icon="PageExcel" AllowBlank="false" LabelWidth="100"
                                LabelAlign="Right" ColumnWidth="1">
                                <Listeners>
                                    <Change Fn="FileUploadFieldNorthExcel_Change" />
                                </Listeners>
                            </ext:FileUploadField>
                            <ext:Button runat="server" ID="ButtonNorthClearExcel" Icon="PageDelete" ToolTip="清除">
                                <Listeners>
                                    <Click Handler="#{FileUploadFieldNorthExcel}.reset();#{ComboBoxNorthExcelSheets}.store.removeAll();" />
                                </Listeners>
                            </ext:Button>
                            <ext:ComboBox runat="server" ID="ComboBoxNorthExcelSheets" Editable="false" AllowBlank="false"
                                EmptyText="请选择Sheet页" FieldLabel="Sheet页" ColumnWidth="1" LabelAlign="Right"
                                Hidden="false" InputWidth="200" MatchFieldWidth="false">
                                <ListConfig Width="200" />
                            </ext:ComboBox>
                            <ext:DateField runat="server" ID="DateFieldNorthRegDateTime" EmptyText="选择生效日期" FieldLabel="生效日期"
                                AllowBlank="false" Editable="false" Width="250" Format="yyyy-MM-dd" LabelWidth="100"
                                LabelAlign="Right" ColumnWidth=".45" />
                            <ext:TimeField runat="server" ID="TimeFieldNorthRegDateTime" EmptyText="选择生效时间" Text="00:00:00"
                                Format="HH:mm:ss" AllowBlank="false" Width="150" ColumnWidth=".2" />
                            <ext:TextField runat="server" ID="TextFieldNorthLLStandVision" EmptyText="填写玲珑版本"
                                FieldLabel="玲珑版本" LabelWidth="100" AllowBlank="false" InputWidth="210" MaxLength="50"
                                MaxLengthText="最多不超过50个字符" LabelAlign="Right" ColumnWidth="1" />
                            <ext:ComboBox runat="server" ID="ComboBoxNorthCheckStandTypeId" Editable="false" EmptyText="选择标准类型"
                                FieldLabel="标准类型" LabelWidth="100" AllowBlank="false" InputWidth="210" MatchFieldWidth="false"
                                LabelAlign="Right" ColumnWidth="1">
                                <ListConfig Width="210" />
                            </ext:ComboBox>
                        </Items>
                        <Listeners>
                            <ValidityChange Handler="#{ButtonNorthUpload}.setDisabled(!valid);" />
                        </Listeners>
                   
                        <Buttons>
                             <ext:Button runat="server" ID="Button1" Text="下载模板">
                                <DirectEvents>
                                    <Click OnEvent="btnExport_Click" IsUpload="true">
                                 </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button ID="ButtonNorthUpload" runat="server" Text="上传" Disabled="true">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthUpload_Click" Before="Ext.Msg.wait('正在上传文件...', '上传中');"
                                        Failure="Ext.Msg.show({ 
                                                title   : '错误', 
                                                msg     : '上传文件处理失败', 
                                                minWidth: 200, 
                                                modal   : true, 
                                                icon    : Ext.Msg.ERROR, 
                                                buttons : Ext.Msg.OK 
                                        });">
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthSave" Text="保存">
                                <Listeners>
                                    <Click Fn="ButtonNorthSave_Click" />
                                </Listeners>
                            </ext:Button>
                            <ext:Button ID="ButtonNorthReset" runat="server" Text="重置">
                                <Listeners>
                                    <Click Handler="#{FormPanelNorth}.getForm().reset();" />
                                </Listeners>
                            </ext:Button>
                        </Buttons>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>
            <ext:TabPanel ID="TabPanelCenter" runat="server" Region="Center" Layout="FitLayout">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelCenterError" Title="Excel文件异常数据">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenterError">
                                <Model>
                                    <ext:Model ID="ModelCenterError" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="sheetname" />
                                            <ext:ModelField Name="typename" />
                                            <ext:ModelField Name="index" />
                                            <ext:ModelField Name="matername" />
                                            <ext:ModelField Name="min" />
                                            <ext:ModelField Name="101" />
                                            <ext:ModelField Name="102" />
                                            <ext:ModelField Name="201" />
                                            <ext:ModelField Name="202" />
                                            <ext:ModelField Name="205" />
                                            <ext:ModelField Name="206" />
                                            <ext:ModelField Name="301" />
                                            <ext:ModelField Name="302" />
                                            <ext:ModelField Name="401" />
                                            <ext:ModelField Name="501" />
                                            <ext:ModelField Name="seq" Type="Int" />
                                            <ext:ModelField Name="typeid" />
                                            <ext:ModelField Name="flag" />
                                            <ext:ModelField Name="errmsg" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:RowNumbererColumn runat="server" Width="40" MenuDisabled="true" Draggable="false"
                                    Hidden="false" />
                                <ext:Column runat="server" ID="ColumnCenterErrorInfo" DataIndex="errmsg" Text="异常信息提示"
                                    Draggable="false" Width="260" Hideable="false" />
                                <ext:Column ID="Column23" runat="server" DataIndex="sheetname" Text="车间" Align="Center"
                                    Draggable="false" Width="60" Hideable="false" />
                                <ext:Column ID="Column26" runat="server" DataIndex="typename" Text="类型" Align="Center"
                                    MenuDisabled="true" Draggable="false" Width="60" Hideable="false" />
                                <ext:Column ID="Column29" runat="server" DataIndex="index" Text="序号" Align="Center"
                                    MenuDisabled="true" Draggable="false" Width="40" Hideable="false" />
                                <ext:Column ID="Column32" runat="server" DataIndex="matername" Text="胶料代号" Align="Center"
                                    MenuDisabled="true" Draggable="false" Hideable="false" />
                                <ext:ComponentColumn ID="ComponentColumn12" runat="server" Text="流变性能" Align="Center"
                                    Hideable="false">
                                    <Columns>
                                        <ext:Column ID="Column35" runat="server" DataIndex="205" Text="T30" Align="Center"
                                            MenuDisabled="true" Draggable="false" Hideable="false" />
                                        <ext:Column ID="Column38" runat="server" DataIndex="206" Text="T60" Align="Center"
                                            MenuDisabled="true" Draggable="false" Hideable="false" />
                                        <ext:Column ID="Column41" runat="server" DataIndex="201" Text="ML" Align="Center"
                                            MenuDisabled="true" Draggable="false" Hideable="false" />
                                        <ext:Column ID="Column44" runat="server" DataIndex="202" Text="MH" Align="Center"
                                            MenuDisabled="true" Draggable="false" Hideable="false" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:Column ID="Column46" runat="server" DataIndex="101" Text="ML(1+4)" MenuDisabled="true"
                                    Draggable="false" Hideable="false" />
                                <ext:Column ID="Column47" runat="server" DataIndex="102" Text="T5" MenuDisabled="true"
                                    Draggable="false" Hideable="false" />
                                <ext:Column ID="Column48" runat="server" DataIndex="301" Text="硬度" MenuDisabled="true"
                                    Draggable="false" Hideable="false" />
                                <ext:Column ID="Column49" runat="server" DataIndex="401" Text="比重" MenuDisabled="true"
                                    Draggable="false" Hideable="false" />
                                <ext:Column ID="Column50" runat="server" DataIndex="501" Text="H抽出" MenuDisabled="true"
                                    Draggable="false" Hideable="false" />
                            </Columns>
                        </ColumnModel>
                        <ToolTips>
                            <ext:ToolTip ID="ToolTipCenterError" runat="server" Delegate="tr.x-grid-row" TrackMouse="true">
                                <Listeners>
                                    <Show Fn="ToolTipCenterError_Show" />
                                </Listeners>
                            </ext:ToolTip>
                        </ToolTips>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbarCenterError" runat="server" HideRefresh="true">
                                <Plugins>
                                    <ext:ProgressBarPager />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                    <ext:GridPanel runat="server" ID="GridPanelCenterSave" Title="提交保存的数据">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenterSave" OnSubmitData="StoreCenterSave_SubmitData">
                                <Model>
                                    <ext:Model ID="ModelCenterSave" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="sheetname" />
                                            <ext:ModelField Name="typename" />
                                            <ext:ModelField Name="index" />
                                            <ext:ModelField Name="matername" />
                                            <ext:ModelField Name="min" />
                                            <ext:ModelField Name="101" />
                                            <ext:ModelField Name="102" />
                                            <ext:ModelField Name="201" />
                                            <ext:ModelField Name="202" />
                                            <ext:ModelField Name="205" />
                                            <ext:ModelField Name="206" />
                                            <ext:ModelField Name="301" />
                                            <ext:ModelField Name="302" />
                                            <ext:ModelField Name="401" />
                                            <ext:ModelField Name="501" />
                                            <ext:ModelField Name="seq" Type="Int" />
                                            <ext:ModelField Name="typeid" />
                                            <ext:ModelField Name="matercode" />
                                            <ext:ModelField Name="min205" />
                                            <ext:ModelField Name="max205" />
                                            <ext:ModelField Name="min206" />
                                            <ext:ModelField Name="max206" />
                                            <ext:ModelField Name="min201" />
                                            <ext:ModelField Name="max201" />
                                            <ext:ModelField Name="min202" />
                                            <ext:ModelField Name="max202" />
                                            <ext:ModelField Name="min101" />
                                            <ext:ModelField Name="max101" />
                                            <ext:ModelField Name="min102" />
                                            <ext:ModelField Name="max102" />
                                            <ext:ModelField Name="min301" />
                                            <ext:ModelField Name="max301" />
                                            <ext:ModelField Name="min401" />
                                            <ext:ModelField Name="max401" />
                                            <ext:ModelField Name="min501" />
                                            <ext:ModelField Name="max501" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:Column ID="Column15" runat="server" DataIndex="seq" Width="40" MenuDisabled="true"
                                    Draggable="false" />
                                <ext:Column ID="Column16" runat="server" DataIndex="sheetname" Text="车间" Align="Center"
                                    MenuDisabled="true" Draggable="false" Width="60" />
                                <ext:Column ID="Column17" runat="server" DataIndex="typename" Text="类型" Align="Center"
                                    MenuDisabled="true" Draggable="false" Width="60" />
                                <ext:Column ID="Column18" runat="server" DataIndex="index" Text="序号" MenuDisabled="true"
                                    Draggable="false" Width="40" />
                                <ext:Column ID="Column19" runat="server" DataIndex="matername" Text="胶料代号" MenuDisabled="true"
                                    Draggable="false" />
                                <ext:ComponentColumn ID="ComponentColumn2" runat="server" Text="流变性能" Sortable="true">
                                    <Columns>
                                        <ext:ComponentColumn ID="ComponentColumn3" runat="server" Text="T30" MenuDisabled="true">
                                            <Columns>
                                                <ext:Column ID="Column21" runat="server" DataIndex="min205" Text="min" Align="Center"
                                                    MenuDisabled="true" Draggable="false" />
                                                <ext:Column ID="Column22" runat="server" DataIndex="max205" Text="max" Align="Center"
                                                    MenuDisabled="true" Draggable="false" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn ID="ComponentColumn4" runat="server" Text="T60" MenuDisabled="true">
                                            <Columns>
                                                <ext:Column ID="Column24" runat="server" DataIndex="min206" Text="min" MenuDisabled="true"
                                                    Draggable="false" />
                                                <ext:Column ID="Column25" runat="server" DataIndex="max206" Text="max" MenuDisabled="true"
                                                    Draggable="false" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn ID="ComponentColumn5" runat="server" Text="ML" MenuDisabled="true">
                                            <Columns>
                                                <ext:Column ID="Column27" runat="server" DataIndex="min201" Text="min" MenuDisabled="true"
                                                    Draggable="false" />
                                                <ext:Column ID="Column28" runat="server" DataIndex="max201" Text="max" MenuDisabled="true"
                                                    Draggable="false" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                        <ext:ComponentColumn ID="ComponentColumn6" runat="server" Text="MH" MenuDisabled="true">
                                            <Columns>
                                                <ext:Column ID="Column30" runat="server" DataIndex="min202" Text="min" MenuDisabled="true"
                                                    Draggable="false" />
                                                <ext:Column ID="Column31" runat="server" DataIndex="max202" Text="max" MenuDisabled="true"
                                                    Draggable="false" />
                                            </Columns>
                                        </ext:ComponentColumn>
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn ID="ComponentColumn7" runat="server" Text="ML(1+4)" MenuDisabled="true">
                                    <Columns>
                                        <ext:Column ID="Column33" runat="server" DataIndex="min101" Text="min" MenuDisabled="true"
                                            Draggable="false" />
                                        <ext:Column ID="Column34" runat="server" DataIndex="max101" Text="max" MenuDisabled="true"
                                            Draggable="false" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn ID="ComponentColumn8" runat="server" Text="T5" MenuDisabled="true">
                                    <Columns>
                                        <ext:Column ID="Column36" runat="server" DataIndex="min102" Text="min" MenuDisabled="true"
                                            Draggable="false" />
                                        <ext:Column ID="Column37" runat="server" DataIndex="max102" Text="max" MenuDisabled="true"
                                            Draggable="false" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn ID="ComponentColumn9" runat="server" Text="硬度" MenuDisabled="true">
                                    <Columns>
                                        <ext:Column ID="Column39" runat="server" DataIndex="min301" Text="min" MenuDisabled="true"
                                            Draggable="false" />
                                        <ext:Column ID="Column40" runat="server" DataIndex="max301" Text="max" MenuDisabled="true"
                                            Draggable="false" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn ID="ComponentColumn10" runat="server" Text="比重" MenuDisabled="true">
                                    <Columns>
                                        <ext:Column ID="Column42" runat="server" DataIndex="min401" Text="min" MenuDisabled="true"
                                            Draggable="false" />
                                        <ext:Column ID="Column43" runat="server" DataIndex="max401" Text="max" MenuDisabled="true"
                                            Draggable="false" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:ComponentColumn ID="ComponentColumn11" runat="server" Text="H抽出" MenuDisabled="true">
                                    <Columns>
                                        <ext:Column ID="Column" runat="server" DataIndex="min501" Text="min" MenuDisabled="true"
                                            Draggable="false" />
                                        <ext:Column ID="Column45" runat="server" DataIndex="max501" Text="max" MenuDisabled="true"
                                            Draggable="false" />
                                    </Columns>
                                </ext:ComponentColumn>
                            </Columns>
                        </ColumnModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbarCenterSave" runat="server" HideRefresh="true">
                                <Plugins>
                                    <ext:ProgressBarPager />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                    <ext:GridPanel runat="server" ID="GridPanelCenterOrigin" Title="Excel文件全部数据">
                        <Store>
                            <ext:Store runat="server" ID="StoreCenterOrigin">
                                <Model>
                                    <ext:Model ID="ModelCenterOrigin" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="sheetname" />
                                            <ext:ModelField Name="typename" />
                                            <ext:ModelField Name="index" />
                                            <ext:ModelField Name="matername" />
                                            <ext:ModelField Name="min" />
                                            <ext:ModelField Name="101" />
                                            <ext:ModelField Name="102" />
                                            <ext:ModelField Name="201" />
                                            <ext:ModelField Name="202" />
                                            <ext:ModelField Name="205" />
                                            <ext:ModelField Name="206" />
                                            <ext:ModelField Name="301" />
                                            <ext:ModelField Name="302" />
                                            <ext:ModelField Name="401" />
                                            <ext:ModelField Name="501" />
                                            <ext:ModelField Name="seq" Type="Int" />
                                            <ext:ModelField Name="typeid" />
                                            <ext:ModelField Name="flag" />
                                            <ext:ModelField Name="errmsg" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:Column ID="Column1" runat="server" DataIndex="seq" Width="40" MenuDisabled="true"
                                    Draggable="false" Hideable="false" />
                                <ext:Column ID="Column2" runat="server" DataIndex="sheetname" Text="车间" Align="Center"
                                    Draggable="false" Width="60" Hideable="false" />
                                <ext:Column ID="Column3" runat="server" DataIndex="typename" Text="类型" Align="Center"
                                    MenuDisabled="true" Draggable="false" Width="60" Hideable="false" />
                                <ext:Column ID="Column4" runat="server" DataIndex="index" Text="序号" Align="Center"
                                    MenuDisabled="true" Draggable="false" Width="40" Hideable="false" />
                                <ext:Column ID="Column5" runat="server" DataIndex="matername" Text="胶料代号" Align="Center"
                                    MenuDisabled="true" Draggable="false" Hideable="false" />
                                <ext:ComponentColumn ID="ComponentColumn1" runat="server" Text="流变性能" Align="Center"
                                    Hideable="false">
                                    <Columns>
                                        <ext:Column ID="Column6" runat="server" DataIndex="205" Text="T30" Align="Center"
                                            MenuDisabled="true" Draggable="false" Hideable="false" />
                                        <ext:Column ID="Column7" runat="server" DataIndex="206" Text="T60" Align="Center"
                                            MenuDisabled="true" Draggable="false" Hideable="false" />
                                        <ext:Column ID="Column8" runat="server" DataIndex="201" Text="ML" Align="Center"
                                            MenuDisabled="true" Draggable="false" Hideable="false" />
                                        <ext:Column ID="Column9" runat="server" DataIndex="202" Text="MH" Align="Center"
                                            MenuDisabled="true" Draggable="false" Hideable="false" />
                                    </Columns>
                                </ext:ComponentColumn>
                                <ext:Column ID="Column10" runat="server" DataIndex="101" Text="ML(1+4)" MenuDisabled="true"
                                    Draggable="false" Hideable="false" />
                                <ext:Column ID="Column11" runat="server" DataIndex="102" Text="T5" MenuDisabled="true"
                                    Draggable="false" Hideable="false" />
                                <ext:Column ID="Column12" runat="server" DataIndex="301" Text="硬度" MenuDisabled="true"
                                    Draggable="false" Hideable="false" />
                                <ext:Column ID="Column13" runat="server" DataIndex="401" Text="比重" MenuDisabled="true"
                                    Draggable="false" Hideable="false" />
                                <ext:Column ID="Column14" runat="server" DataIndex="501" Text="H抽出" MenuDisabled="true"
                                    Draggable="false" Hideable="false" />
                            </Columns>
                        </ColumnModel>
                        <View>
                            <ext:GridView ID="GridViewCenterOrigin" runat="server" StripeRows="true" TrackOver="true">
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
                            <ext:PagingToolbar ID="PagingToolbarCenterOrigin" runat="server" HideRefresh="true">
                                <Plugins>
                                    <ext:ProgressBarPager />
                                </Plugins>
                            </ext:PagingToolbar>
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:TabPanel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
