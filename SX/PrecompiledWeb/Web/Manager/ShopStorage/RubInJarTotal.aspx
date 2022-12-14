<%@ page language="C#" autoeventwireup="true" inherits="Manager_ShopStorage_RubInJarTotal, App_Web_ampjtxsw" %>


<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>料仓统计和清空</title>
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/ext-chinese-font.css" />
    <link rel="Stylesheet" type="text/css" href="<%= Page.ResolveUrl("~/") %>resources/css/main.css" />
    <script type="text/javascript">


        var commandcolumn_click = function (command, record) {
   
            if (command.toLowerCase() == "delete") {
                Ext.Msg.confirm("提示", '您确定需要删除此条信息？', function (btn) { commandcolumn_direct_delete(btn, record) });
            }
        
            return false;
        };

        var commandcolumn_direct_delete = function (btn, record) {
            if (btn != "yes") {
                return;
            }
            App.hiddenSelectEquipCode.setValue(record.data.EquipCode);
            //            hiddenEquipCode.Text = record.data.EquipCode;
            App.hiddenJarNum.setValue(record.data.JarNum);
           
//            var jarnunm = record.data.JarNum;
            App.direct.btnBatchSend_Click( {
                success: function (result) {
                    Ext.Msg.alert('操作', result);
                },

                failure: function (errorMsg) {
                    Ext.Msg.alert('操作', errorMsg);
                }
            });
        }

        var pnlListFresh = function () {
            App.store.currentPage = 1;
            App.pageToolBar.doRefresh();
            return false;
        }

    

      
    
        var QueryEquipmentInfo = function (field, trigger, index) { //机台添加
            switch (index) {
                case 0:
                    field.getTrigger(0).hide();
                    field.setValue('');
                    App.hiddenEquipCode.setValue("");
                    field.getEl().down('input.x-form-text').setStyle('background', "white");
                    //App.pageToolBar.doRefresh();
                    break;
                case 1:
                    App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
                    break;
            }
        }
        //点击修改按钮
      
    
        var QueryEquipmentInfo1 = function (field, trigger, index) {
            App.Manager_BasicInfo_CommonPage_QueryEquipInfo_Window.show();
        }


      

        Ext.create("Ext.window.Window", {
            id: "Manager_BasicInfo_CommonPage_QueryEquipInfo_Window",
            height: 450,
            hidden: true,
            width: 370,
            html: "<iframe src='../BasicInfo/CommonPage/QueryEquipment.aspx' width=100% height=100% scrolling=no  frameborder=0></iframe>",
            bodyStyle: "background-color: #fff;",
            closable: true,
            title: "请选择机台",
            modal: true
        })

      
     
      

        var Manager_BasicInfo_CommonPage_QueryEquipInfo_Request = function (record) {//机台返回信息处理
        
                App.txtEquip.getTrigger(0).show();
                App.txtEquip.setValue(record.data.EquipName);
                App.hiddenEquipCode.setValue(record.data.EquipCode);
                //App.pageToolBar.doRefresh();
  
        }

    

       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="rmMminjar" runat="server" />
    <ext:Viewport ID="vpMminjar" runat="server" Layout="border">
        <Items>
            <ext:Panel ID="pnMminjarTitle" runat="server" Region="North" AutoHeight="true">
                <TopBar>
                    <ext:Toolbar runat="server" ID="tbMminjar">
                        <Items>
                          
                            <ext:ToolbarSeparator ID="tsBegin" />
                            <ext:Button runat="server" Icon="Find" Text="查询" ID="btnSearch">
                                <Listeners>
                                    <Click Fn="pnlListFresh" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsMiddle" />
                            <ext:Button runat="server" Icon="LockEdit" Text="清空" ID="Button1">
                                <Listeners>
                                    <Click Handler="SetChkFlag();" />
                                </Listeners>
                            </ext:Button>
                            <ext:ToolbarSeparator ID="tsEnd" />
                            <ext:ToolbarSpacer runat="server" ID="tspacerEnd" />
                            <ext:ToolbarFill ID="tfEnd" />
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel ID="pnlStorageQuery" runat="server" AutoHeight="true">
                        <Items>
                            <ext:FormPanel ID="container_top" runat="server" Layout="ColumnLayout" AutoHeight="true">
                                <Items>
                                    <ext:Container ID="container1" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                             <ext:TriggerField ID="txtEquip" runat="server" FieldLabel="机台" LabelAlign="Right"
                                                Editable="false">
                                                <Triggers>
                                                    <ext:FieldTrigger Icon="Clear" HideTrigger="true" />
                                                    <ext:FieldTrigger Icon="Search" />
                                                </Triggers>
                                                <Listeners>
                                                    <TriggerClick Fn="QueryEquipmentInfo" />
                                                </Listeners>
                                            </ext:TriggerField>
                                         </Items>
                                    </ext:Container>
                                    <ext:Container ID="container2" runat="server" Layout="FormLayout" ColumnWidth=".33"
                                        Padding="5">
                                        <Items>
                                                        <ext:TextField ID="txtJarNum" runat="server" FieldLabel="料仓号" LabelAlign="Right"/>
                                    
                                        </Items>
                                    </ext:Container>
                        
                                </Items>
                                <Listeners>
                                    <ValidityChange Handler="#{btnSearch}.setDisabled(!valid);" />
                                </Listeners>
                            </ext:FormPanel>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:GridPanel ID="pnlList" runat="server" Cls="x-grid-custom" Region="Center">
                <Store>
                    <ext:Store ID="store" runat="server" PageSize="15">
                        <Proxy>
                            <ext:PageProxy DirectFn="App.direct.GridPanelBindData" />
                        </Proxy>
                        <Model>
                            <ext:Model ID="model" runat="server" IDProperty="JarID">
                                <Fields>
                        
                                    <ext:ModelField Name="JarNum" />
                                 
                                    <ext:ModelField Name="MaterialName" />
                                    <ext:ModelField Name="RealWeight" />
                                            <ext:ModelField Name="EquipName" />
                                               <ext:ModelField Name="EquipCode" />
                                </Fields>
                            </ext:Model>
                        </Model>
                    </ext:Store>
                </Store>
                <ColumnModel ID="colModel" runat="server">
                    <Columns>
                        <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                          <ext:Column ID="EquipName" runat="server" Text="机台" DataIndex="EquipName" Flex="1" />
                       <ext:Column ID="JarNum" runat="server" Text="料仓号" DataIndex="JarNum" Width="45" />
                         <ext:Column ID="MaterialName" runat="server" Text="物料名称" DataIndex="MaterialName"
                            Flex="1" />
                        <ext:Column ID="RealWeight" runat="server" Text="库存重量" DataIndex="RealWeight" Flex="1" />
                           <ext:CommandColumn ID="commandCol" runat="server" Width="60" Text="操作" Align="Center">
                                <Commands>
                            
                                    <ext:GridCommand Icon="Delete" CommandName="Delete" Text="清空">
                                        <ToolTip Text="清空" />
                                    </ext:GridCommand>
                                   
                                    
                                </Commands>
                              <%--  <PrepareToolbar Fn="prepareToolbar"  />--%>
                                <Listeners>
                                    <Command Handler="return commandcolumn_click(command, record);" />
                                </Listeners>
                            </ext:CommandColumn>
                    </Columns>
                </ColumnModel>
                <SelectionModel>
                    <ext:RowSelectionModel ID="rowSelectMuti" runat="server" Mode="Single" />
                </SelectionModel>
                <BottomBar>
                    <ext:PagingToolbar ID="pageToolBar" runat="server">
                        <Items>
                            <ext:Label ID="Label2" runat="server" Text="每页条数:" />
                            <ext:ToolbarSpacer ID="ToolbarSpacer2" runat="server" Width="10" />
                            <ext:ComboBox ID="ComboBox2" runat="server" Width="80">
                                <Items>
                                    <ext:ListItem Text="10" />
                                    <ext:ListItem Text="50" />
                                    <ext:ListItem Text="100" />
                                    <ext:ListItem Text="200" />
                                </Items>
                                <SelectedItems>
                                    <ext:ListItem Value="10" />
                                </SelectedItems>
                                <Listeners>
                                    <Select Handler="#{pnlList}.store.pageSize = parseInt(this.getValue(), 10); #{pageToolBar}.doRefresh(); return false;" />
                                </Listeners>
                            </ext:ComboBox>
                        </Items>
                        <Plugins>
                            <ext:ProgressBarPager ID="ProgressBarPager" runat="server" />
                        </Plugins>
                    </ext:PagingToolbar>
                </BottomBar>
            </ext:GridPanel>
         
     
         
            <ext:Hidden ID="hiddenEquipCode" runat="server">
            </ext:Hidden>
              <ext:Hidden ID="hiddenSelectEquipCode" runat="server">
            </ext:Hidden>
            <ext:Hidden ID="hiddenJarNum" runat="server">
            </ext:Hidden>
     
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
