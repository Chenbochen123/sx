<%@ page language="C#" autoeventwireup="true" inherits="Manager_ProducingPlan_PlanEntering_QueryDeletePlan, App_Web_qlhoypu3" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style>
        /* style rows on mouseover */
        .x-grid-row-over .x-grid-cell-inner {
            font-weight: bold;
        }
    </style>

    <script>
        var template = '<span style="color:{0};">{1}</span>';

        var change = function (value) {
            return Ext.String.format(template, (value > 0) ? "green" : "red", value);
        };

        var pctChange = function (value) {
            return Ext.String.format(template, (value > 0) ? "green" : "red", value + "%");
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server">
    </ext:ResourceManager>
    <ext:GridPanel 
        ID="GridPanel1"
        runat="server" 
        Title="Grouped Header Grid" 
        Width="600" 
        ColumnLines="true"
        Height="350">
        <Store>
            <ext:Store ID="Store1" runat="server">
                <Model>
                    <ext:Model ID="Model1" runat="server">
                        <Fields>
                            <ext:ModelField Name="company" />
                            <ext:ModelField Name="price" Type="Float" />
                            <ext:ModelField Name="change" Type="Float" />
                            <ext:ModelField Name="pctChange" Type="Float" />
                            <ext:ModelField Name="lastChange" Type="Date" DateFormat="M/d hh:mmtt" />
                        </Fields>
                    </ext:Model>
                </Model>
            </ext:Store>
        </Store>
        <ColumnModel ID="ColumnModel1" runat="server">
            <Columns>
                <ext:Column ID="Column1" runat="server" Text="Company" DataIndex="company" Flex="1" Sortable="false" />

                <ext:Column ID="Column2" runat="server" Text="Stock Price">
                    <Columns>
                        <ext:Column ID="Column3" runat="server" Text="Price" DataIndex="price" Width="75" Sortable="true">                  
                            <Renderer Format="UsMoney" />
                        </ext:Column>
                        <ext:Column ID="Column4" runat="server" Text="Change" DataIndex="change" Width="75" Sortable="true">
                            <Renderer Fn="change" />
                        </ext:Column>
                        <ext:Column ID="Column5" runat="server" Text="Change %" DataIndex="pctChange" Width="75" Sortable="true">
                            <Renderer Fn="pctChange" />
                        </ext:Column>
                    </Columns>
                </ext:Column>                
                
                <ext:DateColumn ID="DateColumn1" runat="server" Text="Last Updated" DataIndex="lastChange" Width="85" Sortable="true" Format="dd/MM/yyyy" />
            </Columns>
        </ColumnModel>
    </ext:GridPanel>
    </form>
</body>
</html>
