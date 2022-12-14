<%@ page language="C#" autoeventwireup="true" inherits="Manager_Storage_StorageSearch, App_Web_p5ht2o2r" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        var pnlListFresh = function () {
            var ddlStorage = document.getElementById("<%=ddlStorageName.ClientID %>"); //ddlStorageName
            var ddlStorageValue = ddlStorage.options[ddlStorage.selectedIndex].value; //获取选择项的值

            var ddlStoragePlace = document.getElementById("<%=ddlStoragePlaceName.ClientID %>");
            var ddlStoragePlaceValue;
            try {
                ddlStoragePlaceValue = ddlStoragePlace.options[ddlStoragePlace.selectedIndex].value;
            }
            catch (e) {
                ddlStoragePlaceValue = "";
            }
            if (ddlStorageValue == "0")
                ddlStorageValue = "";
            if (ddlStoragePlaceValue == "0")
                ddlStoragePlaceValue = "";
            window.open("StorageAutoScroll.aspx?StorageID=" + ddlStorageValue + "&&StoragePlaceID=" + ddlStoragePlaceValue + "&&MaterCode=", "_blank");
        }
    </script>
</head>
<body>
    <form id="frmStorageSearch" runat="server">
        <table>
            <tr>
                <td>
                    库房名称：
                </td>
                <td>
                    <asp:DropDownList ID="ddlStorageName" runat="server" Width="180" AppendDataBoundItems="true" onselectedindexchanged="ddlStorageName_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="0">----全部----</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    库位名称：
                </td>
                <td>
                    <asp:DropDownList ID="ddlStoragePlaceName" runat="server" Width="180"></asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="查询" Width="60" OnClientClick="pnlListFresh()" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
