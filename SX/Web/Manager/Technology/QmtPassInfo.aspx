<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QmtPassInfo.aspx.cs" Inherits="Manager_Technology_QmtPassInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>质检不合格技术放行</title>
    <script language="javascript" type="text/javascript">
        var Barcode_Renderer = function (value, metadata, record, rowIndex, colIndex, store, view) {
            if (record.data.PassFlag == "1") {
                metadata.style = "color: Green;";
            }
            else {
                metadata.style = "color: Red;";
            }
            return value;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager runat="server" />
    <ext:Viewport runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel runat="server" Region="North">
                <TopBar>
                    <ext:Toolbar runat="server">
                        <Items>
                            <ext:Button runat="server" ID="ButtonNorthQuery" Text="查询" Icon="Magnifier">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthQuery_Click">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button runat="server" ID="ButtonNorthPass" Text="放行" Icon="Accept">
                                <DirectEvents>
                                    <Click OnEvent="ButtonNorthPass_Click">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                             <ext:Button runat="server" ID="Button1" Text="掺用" Icon="ApplicationEdit">
                                <DirectEvents>
                                    <Click OnEvent="ButtonCY_Click">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                                <ext:Button runat="server" Icon="LockEdit" Text="掺用审核" ID="Button2">
                                
                                    <DirectEvents>
                                        <Click OnEvent="ButtonSH_Click"></Click>
                                    </DirectEvents>
                                </ext:Button>
                            
                                <ext:Button runat="server" Icon="LockEdit" Text="撤销审核" ID="Button3">
                                    <DirectEvents>
                                        <Click OnEvent="ButtonCSH_Click"></Click>
                                    </DirectEvents>
                                </ext:Button>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel runat="server" Layout="ColumnLayout">
                        <Items>
                            <ext:RadioGroup runat="server" ID="RadioGroupQuerySearchType">
                                <Items>
                                    <ext:Radio InputValue="1" FieldLabel="按生产日期" LabelAlign="Right" LabelWidth="70" Checked="true" />
                                    <ext:Radio InputValue="2" FieldLabel="按放行日期" LabelAlign="Right" LabelWidth="70" />
                                </Items>
                            </ext:RadioGroup>
                            <ext:DateField runat="server" ID="DateFieldQueryBeginDate" LabelAlign="Right" FieldLabel="开始日期"
                                AllowBlank="false" Editable="false" Format="yyyy-MM-dd" LabelWidht="80" InputWidth="110"
                                Padding="2">
                            </ext:DateField>
                            <ext:DateField runat="server" ID="DateFieldQueryEndDate" LabelAlign="Right" FieldLabel="截止日期"
                                AllowBlank="false" Editable="false" Format="yyyy-MM-dd" LabelWidht="80" InputWidth="110"
                                Padding="2">
                            </ext:DateField>
                            <ext:ComboBox runat="server" ID="ComboBoxQueryPassFlag" LabelAlign="Right" FieldLabel="放行状态"
                                Editable="false" LabelWidth="80" InputWidth="70" EmptyText="全部" Padding="2">
                                <Items>
                                    <ext:ListItem Mode="Value" Value="0" Text="否">
                                    </ext:ListItem>
                                    <ext:ListItem Mode="Value" Value="1" Text="是">
                                    </ext:ListItem>
                                </Items>
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="this.setValue('');" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:ComboBox runat="server" ID="ComboBoxQueryShift" LabelAlign="Right" FieldLabel="班次"
                                Editable="false" LabelWidth="80" InputWidth="70" EmptyText="全部" Padding="2">
                                <Triggers>
                                    <ext:FieldTrigger Icon="Clear" Qtip="清空" />
                                </Triggers>
                                <Listeners>
                                    <TriggerClick Handler="this.setValue('');" />
                                </Listeners>
                            </ext:ComboBox>
                            <ext:TextField runat="server" ID="TextFieldQueryZJS" LabelAlign="Right" FieldLabel="主机手"
                                LabelWidth="80" InputWidth="70" EmptyText="主机手编号..." Padding="2">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="TextFieldQueryBarcode" LabelAlign="Right" FieldLabel="条码号"
                                LabelWidth="80" InputWidth="110" EmptyText="条码号..." Padding="2">
                            </ext:TextField>
                            <ext:TextField runat="server" ID="TextFieldQueryLLBarcode" LabelAlign="Right" FieldLabel="玲珑条码号"
                                LabelWidth="80" InputWidth="110" EmptyText="玲珑条码号..." Padding="2">
                            </ext:TextField>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Panel runat="server" Region="Center" Layout="FitLayout">
                <Items>
                    <ext:GridPanel runat="server" ID="GridPanelMain" Region="Center">
                        <TopBar>
                            <ext:PagingToolbar runat="server" ID="PagingToolbarMain" HideRefresh="true">
                                <Plugins>
                                    <ext:ProgressBarPager />
                                </Plugins>
                            </ext:PagingToolbar>
                        </TopBar>
                        <Store>
                            <ext:Store runat="server" ID="StoreMain" AutoLoad="false" PageSize="10">
                                <Model>
                                    <ext:Model runat="server" ID="ModelMain" IDProperty="Barcode">
                                        <Fields>
                                            <ext:ModelField Name="Barcode" />
                                            <ext:ModelField Name="PlanDate" Type="Date" />
                                            <ext:ModelField Name="EquipName" />
                                            <ext:ModelField Name="ShiftName" />
                                            <ext:ModelField Name="ClassName" />
                                            <ext:ModelField Name="MaterialName" />
                                            <ext:ModelField Name="BarcodeStart" />
                                            <ext:ModelField Name="BarcodeEnd" />
                                            <ext:ModelField Name="TotalWeight" />
                                            <ext:ModelField Name="ShelfNum" />
                                            <ext:ModelField Name="CheckFlag" />
                                            <ext:ModelField Name="ZJSID" />
                                            <ext:ModelField Name="LLBarcode" />
                                            <ext:ModelField Name="PassFlag" />
                                            <ext:ModelField Name="PassUserName" />
                                            <ext:ModelField Name="PassTime" Type="Date" />
                                            <ext:ModelField Name="PassMemo" />
                                             <ext:ModelField Name="CMaterialName" />
                                              <ext:ModelField Name="Name" />
                                                <ext:ModelField Name="Weight" />
                                                  <ext:ModelField Name="AuditFlag" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel>
                            <Columns>
                                <ext:CheckColumn DataIndex="PassFlag" Text="已放行" Width="60">
                                </ext:CheckColumn>
                                <ext:Column DataIndex="Barcode" Text="条码号" Width="200">
                                    <Renderer Fn="Barcode_Renderer" />
                                </ext:Column>
                                <ext:Column DataIndex="PassUserName" Text="放行人">
                                </ext:Column>
                                <ext:DateColumn DataIndex="PassTime" Text="放行时间" Format="yyyy-MM-dd">
                                </ext:DateColumn>
                                <ext:Column DataIndex="PassMemo" Text="放行原因">
                                </ext:Column>
                                <ext:DateColumn DataIndex="PlanDate" Text="生产日期" Format="yyyy-MM-dd">
                                </ext:DateColumn>
                                <ext:Column DataIndex="EquipName" Text="生产机台">
                                </ext:Column>
                                <ext:Column DataIndex="ShiftName" Text="生产班次">
                                </ext:Column>
                                <ext:Column DataIndex="ClassName" Text="生产班组">
                                </ext:Column>
                                <ext:Column DataIndex="MaterialName" Text="胶料名称">
                                </ext:Column>
                                <ext:Column DataIndex="BarcodeStart" Text="开始车次">
                                </ext:Column>
                                <ext:Column DataIndex="BarcodeEnd" Text="结束车次">
                                </ext:Column>
                                <ext:Column DataIndex="TotalWeight" Text="重量">
                                </ext:Column>
                                <ext:Column DataIndex="ShelfNum" Text="总车数">
                                </ext:Column>
                                <ext:Column DataIndex="ZJSID" Text="主机手">
                                </ext:Column>
                                <ext:Column DataIndex="LLBarcode" Text="玲珑条码">
                                </ext:Column>
                                  <ext:Column DataIndex="CMaterialName" Text="掺用等同物料">
                                </ext:Column>
                                 <ext:Column DataIndex="Name" Text="掺用原因">
                                </ext:Column>
                                   <ext:Column DataIndex="Weight" Text="掺用重量">
                                </ext:Column>
                                   <ext:Column DataIndex="AuditFlag" Text="掺用审核">
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
                        <SelectionModel>
                            <ext:RowSelectionModel runat="server" ID="SelectionModelMain" Mode="Single"
                                AllowDeselect="true">
                            </ext:RowSelectionModel>
                        </SelectionModel>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    <ext:Window runat="server" ID="WindowPass" Title="不合格胶放行" Width="600" Height="350"
        Hidden="true" Modal="false">
        <Items>
            <ext:FormPanel runat="server" ShadowMode="Frame">
                <Items>
                    <ext:TextField runat="server" ID="TextFieldEditBarcode" FieldLabel="条码号" LabelAlign="Right"
                        ReadOnly="true" />
                    <ext:TextField runat="server" ID="TextFieldEditPassMemo" FieldLabel="放行原因" LabelAlign="Right"
                        Width="550" />
                </Items>
            </ext:FormPanel>
        </Items>
        <Buttons>
            <ext:Button runat="server" ID="ButtonEditAccept" Icon="Accept" Text="确定">
                <DirectEvents>
                    <Click OnEvent="ButtonEditAccept_Click">
                        <Confirmation Title="提示" Message="确定要放行吗" ConfirmRequest="true" />
                    </Click>
                </DirectEvents>
            </ext:Button>
            <ext:Button runat="server" ID="ButtonEditCancel" Icon="Cancel" Text="取消">
                <Listeners>
                    <Click Handler="#{WindowPass}.close();" />
                </Listeners>
            </ext:Button>
        </Buttons>
        <Listeners>
            <Show Handler="Ext.fly('form1').mask();" />
            <Hide Handler="Ext.fly('form1').unmask();" />
        </Listeners>
    </ext:Window>
     <ext:Window runat="server" ID="WindowCY" Title="不合格胶掺用" Width="400" Height="250"
        Hidden="true" Modal="false">
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" ShadowMode="Frame">
                <Items>
                    <ext:TextField runat="server" ID="TextBarcode" FieldLabel="条码号" LabelAlign="Right"
                        ReadOnly="true" />
                          <ext:ComboBox runat="server" ID="ComboBox1" FieldLabel="掺用物料"  LabelAlign="Right"
                                Editable="false" LabelWidth="100" InputWidth="140" EmptyText="全部" Padding="2" Hidden="true">
                             
                            </ext:ComboBox>
                              <ext:ComboBox runat="server" ID="ComboBox2"  FieldLabel="掺用原因"  LabelAlign="Right"
                                Editable="false" LabelWidth="100" InputWidth="140" EmptyText="全部" Padding="2">
                               
                            </ext:ComboBox>
                    <ext:TextField runat="server" ID="TextWeight" FieldLabel="掺用重量" LabelAlign="Right"
                        Width="250" />
                </Items>
            </ext:FormPanel>
        </Items>
        <Buttons>
            <ext:Button runat="server" ID="Button4" Icon="Accept" Text="确定">
                <DirectEvents>
                    <Click OnEvent="ButtonEditCY_Click">
                        <Confirmation Title="提示" Message="确定要掺用吗" ConfirmRequest="true" />
                    </Click>
                </DirectEvents>
            </ext:Button>
            <ext:Button runat="server" ID="Button5" Icon="Cancel" Text="取消">
                <Listeners>
                    <Click Handler="#{WindowCY}.close();" />
                </Listeners>
            </ext:Button>
        </Buttons>
        <Listeners>
            <Show Handler="Ext.fly('form1').mask();" />
            <Hide Handler="Ext.fly('form1').unmask();" />
        </Listeners>
    </ext:Window>
    </form>
</body>
</html>
