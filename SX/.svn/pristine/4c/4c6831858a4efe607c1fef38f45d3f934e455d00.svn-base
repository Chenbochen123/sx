<%@ page language="C#" autoeventwireup="true" inherits="Manager_Storage_StoragePlaceMonitor, App_Web_p5ht2o2r" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Grouping DataView - Ext.NET Examples</title>
    <link href="/resources/css/examples.css" rel="stylesheet" />

    <style>
        div.item-wrap {
            float: left;
            border: 1px solid transparent;
            margin: 0px 0px 0px 0px;
            /*width: 80px;*/
            cursor: pointer;
            /*height: 120px;*/
            text-align: center;
        }

            div.item-wrap img {
                margin: 5px 5px 5px 5px;
               /* width: 80px;
                height: 20px;*/
            }

            div.item-wrap h6 {
                font-size: 1px;
                line-height:1px;
                /*color: #3A4B5B;
                font-family: tahoma,arial,san-serif;*/
                margin: 0px; /*visibility:hidden;*/
            }
            div.item-wrap h5 {
                font-size: 1px;
                line-height:1px;
                position:relative;
                top:-15px;
                color:red;
                /*color: #3A4B5B;
                font-family: tahoma,arial,san-serif;*/
                /*visibility:hidden;*/
            }
        div.item-wrap span {
            visibility:hidden;
        }
        div.x-view-over {
             border: solid 1px silver;
        }

        #items-ct {
            padding: 0px 30px 24px 30px;
        }

            #items-ct h2 {
                border-bottom: 2px solid #3A4B5B;
                cursor: pointer;
            }

                #items-ct h2 div {
                    background: transparent url(../../resources/images/group-expand-sprite.gif) no-repeat 3px -47px;
                    padding: 4px 4px 4px 17px;
                    font-family: tahoma,arial,san-serif;
                    font-size: 12px;
                    color: #3A4B5B;
                }

            #items-ct .collapsed h2 div {
                background-position: 3px 3px;
            }

             #items-ct .group-body {
                margin-left: 2px;
            }

            #items-ct .collapsed .group-body {
                display: none;
            }
    </style>

    <script>
        var itemClick = function (view, record, item, index, e) {
            debugger;
            var group = e.getTarget("h2", 3, true),
                subItem;

            if (group) {
                debugger;
                group.up("div").toggleCls("collapsed");
                return false;
            }
            subItem = e.getTarget(".item-wrap");

            if (subItem) {
                debugger;
                var a = subItem.childNodes[1].textContent;
                App.direct.Dashboard_ItemClick(a, {
                    success: function (result) {
                        if (result != "") {
                            Ext.Msg.alert("提示", result);
                            return false;
                        }
                        //chartMain.redraw();
                        //App.MainTabPanel.closeTab(App.PressTempPanel);
                        App.MainTabPanel.addTab(App.pnlList);
                        App.MainTabPanel.setActiveTab(App.pnlList);
                        // App.TaskManager1.startTask("EquipState");
                        debugger;
                        App.direct.GridPanelBindData(a, {
                            success: function (result) {
                                if (result != "") {
                                    Ext.Msg.alert("提示", result);
                                    return false;
                                }
                                //App.TaskManager1.startTask("PressTemp");
                            },
                            failure: function (errorMsg) {
                                Ext.Msg.alert("提示", errorMsg);
                                return false;
                            },
                        });
                    },
                    failure: function (errorMsg) {
                        Ext.Msg.alert("提示", errorMsg);
                        return false;
                    }
                });
                //Ext.Msg.alert("Click", "The node with id='" + subItem.id + "' has been clicked");
            }
        };


        var onShow = function (toolTip, grid) {
            debugger;
            //var view = grid.getView(),
            //    store = grid.getStore(),
            //    record = view.getRecord(view.findItemByChild(toolTip.triggerElement)),
            //    column = view.getHeaderByCell(toolTip.triggerElement),
            //    data = record.get(column.dataIndex);

            //toolTip.update(data);

            var value = grid.textContent;
                //grid.textContent;
            toolTip.update(value);
        };
        var specialStoragePlace = function (value)
        {
            return Ext.String.format(value == 1 ? "是" : "否");
        }
    </script>
</head>
<body>
    <form runat="server">
        <ext:ResourceManager runat="server" />

        <ext:Viewport runat="server" ID="Viewport1" Layout="BorderLayout">
            <Items>
                <ext:Panel runat="server" Region="North" Height="0">
                    <Items>
                    </Items>
                </ext:Panel>
                <ext:TabPanel runat="server" ID="MainTabPanel" Region="Center" Layout="FitLayout">
                <Items>
                <ext:Panel Title="图像"
                    ID="Panel1"
                    runat="server"
                    Cls="items-view"
                    Layout="FitLayout"
                    AutoHeight="true"
                    Width="800"
                    Border="false">
                    <TopBar>
                        <ext:Toolbar runat="server" Flat="true">
                            <Items>
                                <ext:ToolbarFill />

                                <ext:Button runat="server" Icon="BulletPlus" Text="Expand All">
                                    <Listeners>
                                        <Click Handler="#{Dashboard}.el.select('.group-header').removeCls('collapsed');" />
                                    </Listeners>
                                </ext:Button>

                                <ext:Button runat="server" Icon="BulletMinus" Text="Collapse All">
                                    <Listeners>
                                        <Click Handler="#{Dashboard}.el.select('.group-header').addCls('collapsed');" />
                                    </Listeners>
                                </ext:Button>


                                <ext:ToolbarSpacer runat="server" Width="30" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:DataView
                            ID="Dashboard"
                            runat="server"
                            SingleSelect="true" 
                            AutoHeight="true"
                            ItemSelector="div.group-header"
                            EmptyText="No items to display"
                             AutoScroll="true">
                            <Store>
                                <ext:Store ID="Store1" runat="server">
                                    <Model>
                                        <ext:Model runat="server">
                                            <Fields>
                                                <ext:ModelField Name="Title" />
                                                <ext:ModelField Name="Item" IsComplex="true"/>
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <Tpl runat="server">
                                <Html>
                                   <div id="items-ct">
                                    <tpl for=".">
									        <div class="group-header">
										        <h2><div>{Title}</div></h2>
										        <div class="group-body">
											        <tpl for="Item" >
												        <div id="{ID}" class="item-wrap">
                                                            <%--<div>
													            <H5>{StorageNum}</H5> 
                                                            </div>--%>
													        <div>
														        <H6>{StoragePlaceName}</H6> 
													        </div>

                                                            <div>
													            <span>{StoragePlaceId}</span> 
                                                            </div>

                                                            <div>
                                                                <%--<input type="image" src="{Icon}" />--%>
													            <img  src="{Icon}">
                                                                    <H5>{StorageNum}</H5> 
													            </img>
                                                            </div>
                                                            
                                                            
												        </div>
											          </tpl>
											        <div style="clear:left"></div>
										         </div>
									        </div>
								        </tpl>
                                     </div>
                                </Html>
                            </Tpl>
                            <Listeners>
                                <ItemClick Fn="itemClick" />
                                <Refresh Handler="this.el.select('.item-wrap').addClsOnOver('x-view-over');" Delay="100" />
                            </Listeners>
                    
                        </ext:DataView>
                        <ext:ToolTip runat="server"  Target="${div.item-wrap}"
                                TrackMouse="true">
                            <Listeners>
                                <Show Handler="onShow(this,'${div.item-wrap}');" />
                            </Listeners>
                        </ext:ToolTip>
                    </Items>
                </ext:Panel>
            
                <ext:GridPanel ID="pnlList" runat="server" Region="Center" Title="数据">
                        <Store>
                            <ext:Store ID="store" runat="server" PageSize="15"> 
                                <Model>
                                    <ext:Model ID="model" runat="server" IDProperty="ObjID">
                                        <Fields>
                                            <ext:ModelField Name="ObjID" />
                                            <ext:ModelField Name="StorageID" />
                                            <ext:ModelField Name="StorageName" />
                                            <ext:ModelField Name="StoragePlaceID" />
                                            <ext:ModelField Name="StoragePlaceName" />
                                            <ext:ModelField Name="StorageNumber" />
                                            <ext:ModelField Name="StorageCapacity" />
                                            <ext:ModelField Name="SpecialPlace" />
                                            <ext:ModelField Name="MaterialName" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="colModel" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="rowNumCol" runat="server" Width="35" />
                                <ext:Column ID="objID" runat="server" Text="ID" Hidden="true" DataIndex="ObjID" Flex="1" />
                                <ext:Column ID="storageID" runat="server" Text="库房编号" Hidden="true" DataIndex="StorageID" Flex="1" />
                                <%--<ext:Column ID="storageName" runat="server" Text="库房名称" DataIndex="StorageName" Flex="1" />--%>
                                <ext:Column ID="storagePlaceID" runat="server" Text="库位编号" Hidden="true" DataIndex="StoragePlaceID" />
                                <ext:Column ID="storagePlaceName" runat="server" Text="库位名称" DataIndex="StoragePlaceName" />
                                <ext:Column ID="materialName" runat="server" Text="物料名称" DataIndex="MaterialName" />
                                <ext:Column ID="storageCapacity" runat="server" Text="最大存储数量" DataIndex="StorageCapacity" />
                                <ext:Column ID="storageNumber" runat="server" Text="当前存储数量" DataIndex="StorageNumber" />
                                <ext:Column ID="specialPlace" runat="server" Text="特殊库位" DataIndex="SpecialPlace" >
                                    <Renderer Fn="specialStoragePlace"></Renderer>
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
                    </ext:GridPanel>
                </Items>
                </ext:TabPanel>
             </Items>
        </ext:Viewport>
        
         <%--<ext:ImageButton
        runat="server"
        ToggleGroup="Group1"
        ImageUrl="../../resources/images/hege2.png"
        OverImageUrl="overButton.gif"
        DisabledImageUrl="disabled.gif"
        PressedImageUrl="pressed.gif"
        />--%>

    </form>
</body>
</html>
