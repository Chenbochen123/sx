<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EuipementControl.aspx.cs"
    Inherits="Manager_System_UserAction_EuipementControl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Panel ID="pnlUnitTitle" runat="server" AutoHeight="true">
        <TopBar>
            <ext:Toolbar runat="server" ID="barUnit">
                <Items>
                    <ext:Button runat="server" Icon="Cog" Text="设置" ID="btn_add">
                        <ToolTips>
                            <ext:ToolTip ID="ttAdd" runat="server" Html="点击进行修改" />
                        </ToolTips>
                        <DirectEvents>
                            <Click OnEvent="btn_add_Click">
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                </Items>
            </ext:Toolbar>
        </TopBar>
        <Items>
            <ext:Panel ID="pnlUnitQuery" Title="开关设置" runat="server" AutoHeight="true">
                <Items>
                    <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                        <Items>
                            <ext:Container ID="container_1" runat="server" Layout="FormLayout" ColumnWidth="1"
                                Padding="5">
                                <Items>
                                    <ext:Checkbox ID="cbxSapInterfaceCtrl" BoxLabel="SAP接口开关" runat="server">
                                    </ext:Checkbox>
                                     <ext:Checkbox ID="cbxpd" BoxLabel="盘点记录明细" runat="server">
                                    </ext:Checkbox>
                                     <ext:Checkbox ID="cbxxc" BoxLabel="计划验证洗车胶" runat="server">
                                    </ext:Checkbox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="Panel1" Title="先入先出设置" runat="server" AutoHeight="true">
                <Items>
                    <ext:FormPanel ID="FormPanel1" runat="server" Layout="ColumnLayout" AutoHeight="true">
                        <Items>
                            <ext:Container ID="container1" runat="server" Layout="FormLayout">
                                <Items>
                                    <ext:Checkbox ID="cbxFirstInOutCtrl" BoxLabel="先入先出控制开关" runat="server">
                                    </ext:Checkbox>
                                <ext:Checkbox ID="Checkbox2" BoxLabel="原料胶料先入先出" runat="server">
                                    </ext:Checkbox>
                                    <ext:Checkbox ID="Checkbox3" BoxLabel="车间胶料先入先出" runat="server">
                                    </ext:Checkbox>
                                     <ext:Checkbox ID="Checkbox4" BoxLabel="线边库先入先出" runat="server">
                                    </ext:Checkbox>
                                     <ext:Checkbox ID="Checkbox5" BoxLabel="胶料领料小料控制" runat="server">
                                    </ext:Checkbox>
                                     <ext:Checkbox ID="Checkbox6" BoxLabel="胶料领料母料控制" runat="server">
                                    </ext:Checkbox>
                                     <ext:Checkbox ID="Checkbox7" BoxLabel="胶料领料终炼控制" runat="server">
                                    </ext:Checkbox>
                                     <ext:Checkbox ID="Checkbox8" BoxLabel="胶料领料返回控制" runat="server">
                                    </ext:Checkbox>
                                    <ext:RadioGroup ID="rdogroup" runat="server" Cls="x-check-group-alt" ColumnsNumber="1" FieldLabel="先入先出规则" Hidden="true">
                                        <Items>
                                            <ext:Radio ID="rdo1" runat="server" BoxLabel="第一级库房">
                                            </ext:Radio>
                                            <ext:Radio ID="rdo2" runat="server" BoxLabel="第二级库房">
                                            </ext:Radio>
                                            <ext:Radio ID="rdo3" runat="server" BoxLabel="第三级库房">
                                            </ext:Radio>
                                        </Items>
                                    </ext:RadioGroup>
                                    <ext:Checkbox ID="cbxpro" BoxLabel="按照生产商控制" runat="server" Hidden="true">
                                    </ext:Checkbox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>
               <ext:Panel ID="Panel3" Title="领料设置" runat="server" AutoHeight="true">
                <Items>
                    <ext:FormPanel ID="FormPanel3" runat="server" Layout="ColumnLayout" AutoHeight="true">
                        <Items>
                            <ext:Container ID="container3" runat="server" Layout="FormLayout"  ColumnsNumber="2"
                                Padding="5">
                                <Items>
                                          <ext:NumberField ID="FIFOTime" runat="server" LabelAlign="Right" Flex="1" Width="200"  DecimalPrecision="0" FieldLabel="母胶先入先出时间"  />   
                                             <ext:NumberField ID="FIFOTimeXL" runat="server" LabelAlign="Right" Flex="1" Width="200"  DecimalPrecision="0" FieldLabel="小料先入先出时间"  />     
                                        
                                             <ext:Checkbox ID="Checkbox1" BoxLabel="超出领料重量控制标志" runat="server" >
                                    </ext:Checkbox>   
                               </Items>
                            </ext:Container>
                        </Items>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>
             <ext:Panel ID="Panel2" Title="配方精确位数" runat="server" AutoHeight="true">
                <Items>
                    <ext:FormPanel ID="FormPanel2" runat="server" Layout="ColumnLayout" AutoHeight="true">
                        <Items>
                            <ext:Container ID="container2" runat="server" Layout="FormLayout"  ColumnsNumber="2"
                                Padding="5">
                                <Items>
                                    <ext:NumberField ID="NumberField1" runat="server" LabelAlign="Right" Flex="1" Width="200"  DecimalPrecision="0" FieldLabel="炭黑"  />
                                     <ext:NumberField ID="NumberField2" runat="server" LabelAlign="Right" Flex="1"  Width="200"  DecimalPrecision="0" FieldLabel="油"  />
                                     <ext:NumberField ID="NumberField3" runat="server" LabelAlign="Right" Flex="1"  Width="200"  DecimalPrecision="0" FieldLabel="胶料"  />
                                     <ext:NumberField ID="NumberField4" runat="server" LabelAlign="Right" Flex="1"  Width="200"  DecimalPrecision="0" FieldLabel="小料校核"  />          
                                    <ext:NumberField ID="NumberField5" runat="server" LabelAlign="Right" Flex="1"  Width="200"  DecimalPrecision="0" FieldLabel="小料检量"  />          
                               </Items>
                            </ext:Container>
                        </Items>
                    </ext:FormPanel>
                </Items>
            </ext:Panel>
        </Items>
    </ext:Panel>
    </form>
</body>
</html>
