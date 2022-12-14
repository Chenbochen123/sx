<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SpecMappingImport.aspx.cs" Inherits="Manager_RawMaterialQuality_SpecMappingImport" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>规格对应关系导入</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/") %>resources/js/jquery-1.7.1.js"></script>
    <script type="text/javascript">
        var fulExcel_Change = function () {
            App.direct.SelectExcel({
                success: function (result) {

                },
                failure: function (errorMessage) {
                    Ext.Msg.alert('错误', errorMessage);
                },
                eventMask: {
                    showMask: true
                }
            });
        };

        var btnImport_Click = function () {
            App.direct.UploadExcel({
                success: function (result) {

                },
                failure: function (errorMessage) {
                    Ext.Msg.alert('错误', errorMessage);
                },
                eventMask: {
                    showMask: true
                }
            });
        };

        var setRowClass = function (record, index, rowParams, store) {
            if (record.get('validFlag') == '0') {
                return 'x-grid-row-deleted';
            }
        };

        var ButtonSave_Click = function () {
            if (App.StoreCenterValid.data.length == 0) {
                Ext.Msg.alert('提示', '没有保存的数据!');
                return false;
            }
            var o = Ext.Msg.confirm({
                title: '提示',
                msg: '确定要保存吗?',
                icon: Ext.Msg.QUESTION,
                buttons: Ext.Msg.OKCANCEL,
                callback: function (btn) {
                    if (btn == 'ok') {
                        App.StoreCenterValid.submitData(null, {
                            success: function (result) {
                                var serviceResponse = Ext.decode(result.responseText).serviceResponse;
                                if (serviceResponse.success == false) {
                                    Ext.Msg.alert('错误', serviceResponse.message);
                                }
                            },
                            failure: function (result) {
                                Ext.Msg.alert('提示', '保存失败!');
                            },
                            eventMask: {
                                showMask: true,
                                msg: '数据保存中...'
                            }
                        });
                    }
                }
            });
            return true;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmSpecMappingImport" runat="server" />
    <ext:Viewport ID="vwUnit" runat="server" Layout="border">
        <Items>
          <ext:Panel runat="server" ID="pnlNorth" Region="North">
                <TopBar>
                    <ext:Toolbar runat="server" ID="barUnit1">
                        <Items>
                             <ext:Button runat="server" Icon="DiskDownload" Text="下载模板" ID="btnDownload">
                                <ToolTips>
                                    <ext:ToolTip ID="ttAdd" runat="server" Html="点击下载规格映射Excel模板" />
                                </ToolTips>
                                <DirectEvents>
                                    <Click OnEvent="btn_download_Click">
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_begin" />
                            <ext:Button runat="server" Icon="PageWhiteExcel" Text="导入" ID="btnImport">
                                <Listeners>
                                    <Click Fn="btnImport_Click" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsMiddle" />
                            <ext:Button runat="server" Icon="DatabaseSave" Text="保存" ID="btnSave">
                                <Listeners>
                                    <Click Fn="ButtonSave_Click" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="toolbarSeparator_end1" />
                            <ext:ToolbarSpacer runat="server" ID="toolbarSpacer_end1" />
                            <ext:ToolbarFill ID="toolbarFill_end1" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:FormPanel runat="server" ID="pnlForm" Layout="AnchorLayout" AutoHeight="true">
                        <Items>
                           <ext:Container ID="container1" runat="server" Layout="HBoxLayout" Padding="5" >
                                <Items>
                                    <ext:ComboBox ID="cbxSeriesName" runat="server" FieldLabel="原材料系列" LabelAlign="Left"
                                       Editable="false" LabelWidth="73">
                                       <Listeners>
                                           <Select Handler="#{btnDownload}.enable();"></Select>
                                       </Listeners>
                                    </ext:ComboBox>
                                    <ext:FileUploadField runat="server" ID="fulExcel" Icon="PageExcel" Width="280"
                                        ButtonText="" EmptyText="选择要上传的Excel文件" FieldLabel="规格映射文件" LabelAlign="Right">
                                        <Listeners>
                                            <Change Fn="fulExcel_Change" />
                                        </Listeners>
                                    </ext:FileUploadField>
                                    <ext:Button runat="server" ID="btnClear" Icon="PageDelete" ToolTip="清除">
                                        <Listeners>
                                            <Click Handler="#{fulExcel}.reset();" />
                                        </Listeners>
                                    </ext:Button>
                                    </Items>
                          </ext:Container>
                        </Items>
                    </ext:FormPanel>
                </Items>
                <BottomBar>
                </BottomBar>
            </ext:Panel>
            <ext:Panel runat="server" ID="pnlCenter" Region="Center" Layout="BorderLayout">
                <Items>
                    <ext:TabPanel runat="server" ID="tabPnlCenter" Region="Center">
                        <TopBar>
                            <ext:StatusBar runat="server" ID="statusBarCenter" Layout="ColumnLayout" Height="25">
                                <Items>
                                    <ext:Label runat="server" ID="LabelCenterOper" ColumnWidth=".5" />
                                    <ext:Label runat="server" ID="LabelCenterTime" ColumnWidth=".5" />
                                </Items>
                            </ext:StatusBar>
                        </TopBar>
                        <Items>
                            <ext:GridPanel runat="server" ID="gridPanelCenterCorrupted" Title="Excel文件异常数据">
                                <Store>
                                    <ext:Store runat="server" ID="StoreCenterCorrupted">
                                        <Model>
                                            <ext:Model runat="server" ID="ModelCenterCorrupted">
                                                <Fields>
                                                    <ext:ModelField Name="rowNum" />
                                                    <ext:ModelField Name="seriesId" />
                                                    <ext:ModelField Name="seriesName" />
                                                    <ext:ModelField Name="materialCode" />
                                                    <ext:ModelField Name="materialName" />
                                                    <ext:ModelField Name="materialERPCode" />
                                                    <ext:ModelField Name="specName" />   
                                                    <ext:ModelField Name="deleteFlag" />                                             
                                                    <ext:ModelField Name="remark" />
                                                    <ext:ModelField Name="validFlag" />
                                                    <ext:ModelField Name="errMsg" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:Column runat="server" ID="ColumnCenterCorruptedNum" DataIndex="rowNum" MenuDisabled="true"
                                            Width="40" />
                                        <ext:Column runat="server" ID="ColumnCenterCorruptedErrMsg" DataIndex="errMsg" MenuDisabled="true"
                                            Text="异常信息提示" Width="200" />
                                        <ext:Column runat="server" ID="ColumnCenterCorruptedMaterialName" DataIndex="materialName" MenuDisabled="true" 
                                            Text="原材料型号" />
                                        <ext:Column runat="server" ID="ColumnCenterCorruptedMaterialERPCode" DataIndex="materialERPCode" MenuDisabled="true" 
                                            Text="原材料ERP代码" />
                                        <ext:Column runat="server" ID="ColumnCenterCorruptedSpecName" DataIndex="specName" MenuDisabled="true" 
                                            Text="规格名称" />
                                        <ext:Column runat="server" ID="ColumnCenterCorruptedDeleteFlag" DataIndex="deleteFlag" MenuDisabled="true" 
                                            Text="是否启用" />
                                        <ext:Column runat="server" ID="ColumnCenterCorruptedRemark" DataIndex="remark"
                                            Text="备注" MenuDisabled="true" />
                                    </Columns>
                                </ColumnModel>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="PagingToolbarCenterCorrupted" HideRefresh="true" />
                                </BottomBar>
                            </ext:GridPanel>
                            <ext:GridPanel runat="server" ID="GridPanelCenterValid" Title="提交保存的数据">
                                <Store>
                                    <ext:Store runat="server" ID="StoreCenterValid" OnSubmitData="StoreCenterValid_SubmitData">
                                        <Model>
                                            <ext:Model runat="server" ID="ModelCenterValid">
                                                <Fields>
                                                    <ext:ModelField Name="rowNum" />
                                                    <ext:ModelField Name="seriesId" />
                                                    <ext:ModelField Name="seriesName" />
                                                    <ext:ModelField Name="materialCode" />
                                                    <ext:ModelField Name="materialName" />
                                                    <ext:ModelField Name="materialERPCode" />
                                                    <ext:ModelField Name="specName" />
                                                    <ext:ModelField Name="deleteFlag" />   
                                                    <ext:ModelField Name="remark" />
                                                    <ext:ModelField Name="validFlag" />
                                                    <ext:ModelField Name="errMsg" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                        <ext:Column runat="server" ID="ColumnCenterValidNum" DataIndex="rowNum" MenuDisabled="true"
                                            Width="40" />
                                        <ext:Column runat="server" ID="ColumnCenterValidSeriesId" DataIndex="seriesId" Visible="false" />
                                        <ext:Column runat="server" ID="ColumnCenterValidSeriesName" DataIndex="seriesName" Visible="false" />
                                        <ext:Column runat="server" ID="ColumnCenterValidMaterialCode" DataIndex="materialCode" Visible="false" />
                                        <ext:Column runat="server" ID="ColumnCenterValidMaterialName" DataIndex="materialName"
                                            Text="原材料型号" MenuDisabled="true"  />
                                        <ext:Column runat="server" ID="ColumnCenterValidMaterialERPCode" DataIndex="materialERPCode"
                                            Text="原材料ERP代码" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterValidSpecName" DataIndex="specName" MenuDisabled="true" 
                                            Text="规格名称" />
                                        <ext:Column runat="server" ID="ColumnCenterValidDeleteFlag" DataIndex="deleteFlag" MenuDisabled="true" 
                                            Text="是否启用" />
                                        <ext:Column runat="server" ID="ColumnCenterValidRemark" DataIndex="remark"
                                            Text="备注" MenuDisabled="true" />
                                    </Columns>
                                </ColumnModel>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="PagingToolbarCenterValid" HideRefresh="true" />
                                </BottomBar>
                            </ext:GridPanel>
                            <ext:GridPanel runat="server" ID="GridPanelCenterAll" Title="Excel文件全部数据">
                                <Store>
                                    <ext:Store runat="server" ID="StoreCenterAll">
                                        <Model>
                                            <ext:Model runat="server" ID="ModelCenterAll">
                                                <Fields>
                                                    <ext:ModelField Name="rowNum" />
                                                    <ext:ModelField Name="seriesId" />
                                                    <ext:ModelField Name="seriesName" />
                                                    <ext:ModelField Name="materialCode" />
                                                    <ext:ModelField Name="materialName" />
                                                    <ext:ModelField Name="materialERPCode" />
                                                    <ext:ModelField Name="specName" />
                                                    <ext:ModelField Name="deleteFlag" />  
                                                    <ext:ModelField Name="remark" />
                                                    <ext:ModelField Name="validFlag" />
                                                    <ext:ModelField Name="errMsg" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>
                                </Store>
                                <ColumnModel>
                                    <Columns>
                                       <ext:Column runat="server" ID="ColumnCenterAllNum" DataIndex="rowNum" MenuDisabled="true"
                                            Width="40" />
                                        <ext:Column runat="server" ID="ColumnCenterAllSeriesId" DataIndex="seriesId" Visible="false" />
                                        <ext:Column runat="server" ID="ColumnCenterAllSeriesName" DataIndex="seriesName" Visible="false" />
                                        <ext:Column runat="server" ID="ColumnCenterAllMaterialCode" DataIndex="materialCode" Visible="false" />
                                        <ext:Column runat="server" ID="ColumnCenterAllValidFlag" DataIndex="validFlag" Visible="false" />
                                        <ext:Column runat="server" ID="ColumnCenterAllErrMsg" DataIndex="errMsg" MenuDisabled="true"
                                            Text="校验信息" Width="200" />
                                        <ext:Column runat="server" ID="ColumnCenterAllMaterialName" DataIndex="materialName"
                                            Text="原材料型号" MenuDisabled="true"  />
                                        <ext:Column runat="server" ID="ColumnCenterAllMaterialERPCode" DataIndex="materialERPCode"
                                            Text="原材料ERP代码" MenuDisabled="true" />
                                        <ext:Column runat="server" ID="ColumnCenterAllSpecName" DataIndex="specName" MenuDisabled="true" 
                                            Text="规格名称" />
                                        <ext:Column runat="server" ID="ColumnCenterAllDeleteFlag" DataIndex="deleteFlag" MenuDisabled="true" 
                                            Text="是否启用" />
                                        <ext:Column runat="server" ID="ColumnCenterAllRemark" DataIndex="remark"
                                            Text="备注" MenuDisabled="true" />
                                    </Columns>
                                </ColumnModel>
                                <View>
                                    <ext:GridView runat="server" ID="GridViewCenterAll" StripeRows="true" TrackOver="true">
                                        <GetRowClass Fn="setRowClass" />
                                    </ext:GridView>
                                </View>
                                <BottomBar>
                                    <ext:PagingToolbar runat="server" ID="PagingToolbarCenterAll" HideRefresh="true" />
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
