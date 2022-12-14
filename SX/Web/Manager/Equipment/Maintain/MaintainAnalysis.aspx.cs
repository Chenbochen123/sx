using System;
using System.Data;
using System.Collections.Generic;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Entity;
using NBear.Common;

public partial class Manager_Equipment_Maintain_MaintainAnalysis : Mesnac.Web.UI.Page
{
    BasEquipManager equipManager = new BasEquipManager();
    BasWorkShopManager workShopManager = new BasWorkShopManager();
    EqmMaintainRecordManager manger = new EqmMaintainRecordManager();
    SysCodeManager sysCodeManager = new SysCodeManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            dStartDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dEndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            dStartTime.Text = dEndTime.Text = "0:0";
            InitEquipTree();
            bindMainType();
        }
    }
    private void bindMainType()
    {
        EntityArrayList<SysCode> list = sysCodeManager.GetListByWhereAndOrder(SysCode._.TypeID == "StopMainType", SysCode._.ItemName.Asc);
        this.storeStopMainType.DataSource = list;
        this.storeStopMainType.DataBind();
    }
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {


    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string equipCodes = string.Empty;
        string[] tempEquip = treeStr.Value.ToString().Split(',');
        foreach (string item in tempEquip)
        {
            if (!string.IsNullOrEmpty(item) && item.Length == 5)
                equipCodes += item + ",";
        }
        if (string.IsNullOrEmpty(equipCodes))
        {
            X.Msg.Alert("提示", "请至少选择一个机台！").Show();
            return;
        }
        //X.Msg.Alert("提示", equipCodes).Show();

        List<string> list = new List<string>();
        list.Add(Convert.ToDateTime(dStartDate.Text).ToString("yyyy-MM-dd") + " " + dStartTime.RawText);
        list.Add(Convert.ToDateTime(dEndDate.Text).AddDays(1).ToString("yyyy-MM-dd") + " " + dEndTime.RawText);
      
        list.Add(equipCodes);
        list.Add(cbStopMainType.Value.ToString());
        store1.DataSource = manger.GetGroupDataByParas(list);
        store1.DataBind();
        ViewState["list"] = list;
    }
    [DirectMethod]
    public void refreshTips(string stopTypeID)
    {
        string equipCodes = string.Empty;

        string[] tempEquip = treeStr.Value.ToString().Split(',');
        foreach (string item in tempEquip)
        {
            if (!string.IsNullOrEmpty(item) && item.Length == 5)
                equipCodes += item + ",";
        }
        List<string> list = new List<string>();
        list.Add(dStartDate.RawText + " " + dStartTime.RawText);
        list.Add(dEndDate.RawText + " " + dEndTime.RawText);
        list.Add(equipCodes);
        list.Add(stopTypeID);

        store2.DataSource = manger.GetGroupDetailDataByParas(list);
        store2.DataBind();
    }

    #region 查询条件树初始化操作
    private void InitEquipTree()
    {
        DataSet ds = workShopManager.getAllMiLanWorkShopNode();
        foreach (DataRow row in ds.Tables[0].Rows)
        {
            Node node = InitNode(row);
            node.Checked = false;
            DataSet dsEquip = equipManager.getMiLanEquipNodeByWorkShopCode(row["NodeId"].ToString());
            foreach (DataRow rowEquip in dsEquip.Tables[0].Rows)
            {
                Node nodeEquip = InitNode(rowEquip);
                nodeEquip.Leaf = true;
                nodeEquip.Icon = Icon.Monitor;
                nodeEquip.Checked = false;
                node.Children.Add(nodeEquip);
            }
            equipTree.GetRootNode().AppendChild(node);
        }
    }

    private Node InitNode(DataRow row)
    {
        Node n = new Node();
        n.Icon = Icon.Building;
        string[] ss = new string[] { "NodeId", "ShowName" };
        foreach (string s in ss)
        {
            ConfigItem c = new ConfigItem();
            c.Name = s;
            object value = row[c.Name];
            c.Value = value == null ? string.Empty : value.ToString();
            c.Mode = ParameterMode.Value;
            n.CustomAttributes.Add(c);
        }
        return n;
    }
    #endregion
    
}