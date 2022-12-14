<%@ page language="C#" autoeventwireup="true" inherits="Manager_Technology_Manage_MaterialRecipeDetail_MILL, App_Web_a5rwyiqa" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>MILL示方</title>
    <!--通用-->
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <link href="<%= Page.ResolveUrl("~/") %>resources/css/examples.css" rel="stylesheet" />
    <script type="text/javascript">
        var gridPanelRefresh = function () {
            //            App.gridPanelQDrug.store.currentPage = 1;
            //            App.gridPanelQDrug.store.reload();
            return false;
        }
    </script>
    <!--特殊-->
    <script src="<%= Page.ResolveUrl("~/") %>resources/js/waitwindow.js"></script>
    <script src="<%= Page.ResolveUrl("./") %>MILL.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
        <Items>
        <ext:Panel runat="server"  >
        <Items>
            <ext:Panel ID="Panel1" runat="server"  Height="220"  Title="PRIMARY MILL 运行示方书"  Header="true"   >
                <Items>
                    <ext:Container ID="container5" runat="server" MarginSpec="5 5 5 5">
                        <Items>
                            <ext:Panel runat="server" Layout="ColumnLayout" Flex="1" MarginSpec="5 5 5 5" Border="false">
                                <Items>
                                    <ext:Label ID="Label2" runat="server" LabelAlign="right" PaddingSpec="0 0 0 100"
                                        Text="PMILL炼胶步骤" Width="200" />
                                    <ext:Label ID="Label3" runat="server" LabelAlign="left" DecimalPrecision="3" Text="Step1"
                                        Width="80" />
                                    <ext:Label ID="Label4" runat="server" LabelAlign="left" DecimalPrecision="3" Text="Step2"
                                        Width="80" />
                                    <ext:Label ID="Label5" runat="server" LabelAlign="left" DecimalPrecision="3" Text="Step3"
                                        Width="80" />
                                    <ext:Label ID="Label6" runat="server" LabelAlign="left" DecimalPrecision="3" Text="Step4"
                                        Width="80" />
                                    <ext:Label ID="Label7" runat="server" LabelAlign="left" DecimalPrecision="3" Text="Step5"
                                        Width="80" />
                                    <ext:Label ID="Label8" runat="server" LabelAlign="left" DecimalPrecision="3" Text="Step6"
                                        Width="80" />
                                    <ext:Label ID="Label9" runat="server" LabelAlign="left" DecimalPrecision="3" Text="Step7"
                                        Width="80" />
                                    <ext:Label ID="Label10" runat="server" LabelAlign="left" DecimalPrecision="3" Text="Step8"
                                        Width="80" />
                                </Items>
                            </ext:Panel>
                            <ext:Panel runat="server" Layout="ColumnLayout" Flex="1" MarginSpec="5 5 5 5" Border="false">
                                <Items>
                                    <ext:Label ID="NumberField1" runat="server" LabelAlign="Right" PaddingSpec="0 0 0 100"
                                        Text="时间参数(s)" Width="200" />
                                    <ext:NumberField ID="Step1SetTime" runat="server" LabelAlign="Right" DecimalPrecision="0"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="Step2SetTime" runat="server" LabelAlign="Right" DecimalPrecision="0"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="Step3SetTime" runat="server" LabelAlign="Right" DecimalPrecision="0"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="Step4SetTime" runat="server" LabelAlign="Right" DecimalPrecision="0"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="Step5SetTime" runat="server" LabelAlign="Right" DecimalPrecision="0"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="Step6SetTime" runat="server" LabelAlign="Right" DecimalPrecision="0"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="Step7SetTime" runat="server" LabelAlign="Right" DecimalPrecision="0"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="Step8SetTime" runat="server" LabelAlign="Right" DecimalPrecision="0"
                                        FieldLabel="" Width="80" />
                                </Items>
                            </ext:Panel>
                            <ext:Panel runat="server" Layout="ColumnLayout" Flex="1" MarginSpec="5 5 5 5" Border="false">
                                <Items>
                                    <ext:Label ID="Label1" runat="server" LabelAlign="Right" PaddingSpec="0 0 0 100"
                                        Text="辊距参数(mm)" Width="200" />
                                    <ext:NumberField ID="Step1SetRollerSpace" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="Step2SetRollerSpace" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="Step3SetRollerSpace" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="Step4SetRollerSpace" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="Step5SetRollerSpace" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="Step6SetRollerSpace" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="Step7SetRollerSpace" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="Step8SetRollerSpace" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="container1" runat="server" Layout="HBoxLayout" MarginSpec="1 1 1 1">
                        <Items>
                            <ext:CheckboxGroup ID="CheckboxGroup2" runat="server" ColumnsNumber="3" Flex="1">
                                <Items>
                                    <ext:NumberField ID="PInitalTime" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="3"
                                        FieldLabel="初期时间(s)"  />
                                    <ext:NumberField ID="PEndTime" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="3"
                                        FieldLabel="末期时间(s)" />
                                    <ext:NumberField ID="PMixTemp" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="3"
                                        FieldLabel="混合温度(℃)" />
                                    <ext:NumberField ID="PStartV" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="3"
                                        FieldLabel="初期速度(mpm)" />
                                    <ext:NumberField ID="PEndV" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="3"
                                        FieldLabel="末期速度(mpm)" />
                                    <ext:NumberField ID="PMixTime" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="3"
                                        FieldLabel="混合时间(s)" />
                                    <ext:NumberField ID="PRatioCoef" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="3"
                                        FieldLabel="速比系数(%)" />
                                    <ext:ComboBox ID="PIsInject" runat="server" LabelAlign="Right" Flex="1" FieldLabel="喷射使用与否"
                                        SelectOnTab="true" Editable="false" />
                                    <ext:NumberField ID="PStartSpeed" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="3"
                                        FieldLabel="起始速度(mpm)" />
                                         <ext:ComboBox ID="PJDUSE" runat="server" LabelAlign="Right" Flex="1" FieldLabel="胶刀使用与否"
                                        SelectOnTab="true" Editable="false" />
                                </Items>
                            </ext:CheckboxGroup>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:Panel>
            <ext:Panel ID="Panel2" runat="server"  Height="250"   Title="SECONDARY MILL 传送示方书"  Header="true"  >
                <Items>
                    <ext:Container ID="container2" runat="server" MarginSpec="5 5 5 5">
                        <Items>
                            <ext:Panel ID="Panel3" runat="server" Layout="ColumnLayout" Flex="1" MarginSpec="5 5 5 5"
                                Border="false">
                                <Items>
                                    <ext:Label ID="Label11" runat="server" LabelAlign="right" PaddingSpec="0 0 0 100"
                                        Text="SMILL炼胶步骤" Width="200" />
                                    <ext:Label ID="Label12" runat="server" LabelAlign="left" DecimalPrecision="3" Text="Step1"
                                        Width="80" />
                                    <ext:Label ID="Label13" runat="server" LabelAlign="left" DecimalPrecision="3" Text="Step2"
                                        Width="80" />
                                    <ext:Label ID="Label14" runat="server" LabelAlign="left" DecimalPrecision="3" Text="Step3"
                                        Width="80" />
                                    <ext:Label ID="Label15" runat="server" LabelAlign="left" DecimalPrecision="3" Text="Step4"
                                        Width="80" />
                                    <ext:Label ID="Label16" runat="server" LabelAlign="left" DecimalPrecision="3" Text="Step5"
                                        Width="80" />
                                    <ext:Label ID="Label17" runat="server" LabelAlign="left" DecimalPrecision="3" Text="Step6"
                                        Width="80" />
                                    <ext:Label ID="Label18" runat="server" LabelAlign="left" DecimalPrecision="3" Text="Step7"
                                        Width="80" />
                                    <ext:Label ID="Label19" runat="server" LabelAlign="left" DecimalPrecision="3" Text="Step8"
                                        Width="80" />
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="Panel4" runat="server" Layout="ColumnLayout" Flex="1" MarginSpec="5 5 5 5"
                                Border="false">
                                <Items>
                                    <ext:Label ID="Label20" runat="server" LabelAlign="Right" PaddingSpec="0 0 0 100"
                                        Text="时间参数(s)" Width="200" />
                                    <ext:NumberField ID="SStep1SetTime" runat="server" LabelAlign="Right" DecimalPrecision="0"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep2SetTime" runat="server" LabelAlign="Right" DecimalPrecision="0"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep3SetTime" runat="server" LabelAlign="Right" DecimalPrecision="0"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep4SetTime" runat="server" LabelAlign="Right" DecimalPrecision="0"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep5SetTime" runat="server" LabelAlign="Right" DecimalPrecision="0"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep6SetTime" runat="server" LabelAlign="Right" DecimalPrecision="0"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep7SetTime" runat="server" LabelAlign="Right" DecimalPrecision="0"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep8SetTime" runat="server" LabelAlign="Right" DecimalPrecision="0"
                                        FieldLabel="" Width="80" />
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="Panel5" runat="server" Layout="ColumnLayout" Flex="1" MarginSpec="5 5 5 5"
                                Border="false">
                                <Items>
                                    <ext:Label ID="Label21" runat="server" LabelAlign="Right" PaddingSpec="0 0 0 100"
                                        Text="辊距参数(mm)" Width="200" />
                                    <ext:NumberField ID="SStep1SetRollerSpace" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep2SetRollerSpace" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep3SetRollerSpace" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep4SetRollerSpace" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep5SetRollerSpace" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep6SetRollerSpace" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep7SetRollerSpace" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep8SetRollerSpace" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                </Items>
                            </ext:Panel>
                            <ext:Panel ID="Panel6" runat="server" Layout="ColumnLayout" Flex="1" MarginSpec="5 5 5 5"
                                Border="false">
                                <Items>
                                    <ext:Label ID="Label22" runat="server" LabelAlign="Right" PaddingSpec="0 0 0 100"
                                        Text="速度参数(mpm)" Width="200" />
                                    <ext:NumberField ID="SStep1SetVelocity" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep2SetVelocity" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep3SetVelocity" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep4SetVelocity" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep5SetVelocity" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep6SetVelocity" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep7SetVelocity" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                    <ext:NumberField ID="SStep8SetVelocity" runat="server" LabelAlign="Right" DecimalPrecision="1"
                                        FieldLabel="" Width="80" />
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="container3" runat="server" Layout="HBoxLayout" MarginSpec="1 1 1 1">
                        <Items>
                            <ext:CheckboxGroup ID="CheckboxGroup1" runat="server" ColumnsNumber="4" Flex="1">
                                <Items>
                                    <ext:ComboBox ID="SIsInject" runat="server" LabelAlign="Right" Flex="1" FieldLabel="喷射使用与否"
                                        SelectOnTab="true" Editable="false" />
                                    <ext:NumberField ID="SBeforeFOpen" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="0"
                                        FieldLabel="投药前平开(s)" />
                                    <ext:ComboBox ID="SIsPutInto" runat="server" LabelAlign="Right" Flex="1" FieldLabel="投药与否"
                                        SelectOnTab="true" Editable="false" />
                                    <ext:NumberField ID="SMixTime" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="0"
                                        FieldLabel="混合时间(s)" />
                                    <ext:NumberField ID="SInStartTime" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="0"
                                        FieldLabel="投药开始时间" />
                                    <ext:NumberField ID="SAfterFOpen" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="0"
                                        FieldLabel="投药后平开(s)" />
                                    <ext:NumberField ID="SInTimeLen" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="0"
                                        FieldLabel="药品输入时长" />
                                    <ext:NumberField ID="SMixTemp" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="1"
                                        FieldLabel="混合温度(℃)" />
                                         <ext:ComboBox ID="SFUse" runat="server" LabelAlign="Right" Flex="1" FieldLabel="返回胶使用与否"
                                        SelectOnTab="true" Editable="false" />
                                    <ext:NumberField ID="SFUseTime" runat="server" LabelAlign="Right" Flex="1" DecimalPrecision="0"
                                        FieldLabel="返回胶使用时间(s)" />
                                         <ext:ComboBox ID="SJDUSE" runat="server" LabelAlign="Right" Flex="1" FieldLabel="胶刀使用与否"
                                        SelectOnTab="true" Editable="false" />
                                </Items>
                            </ext:CheckboxGroup>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:Panel>
            <ext:Hidden ID="txtRecipeObjID" runat="server">
            </ext:Hidden>
        </Items>
        </ext:Panel>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
