using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using NBear.Common;
using Mesnac.Entity;
using Ext.Net;
using System.Text.RegularExpressions;
using Mesnac.Data.Components;
using System.Data;

public partial class Manager_Equipment_EquipState_EquipProductionSummary : System.Web.UI.Page
{
    #region 属性注入
    protected BasWorkShopManager manager = new BasWorkShopManager();
    protected BasEquipManager equipManager = new BasEquipManager();
    BasWorkShopManager workShopManager = new BasWorkShopManager();
    protected PptPlanManager planManager = new PptPlanManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    protected static DataTable tempDt = new DataTable();
    protected static bool inhibitor = false;
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查看 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            导出 = new SysPageAction() { ActionID = 1, ActionName = "btnExport" };
        }
        public SysPageAction 查看 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            this.txtBeginTime.Value = DateTime.Now.Date.AddDays(-1);
            this.txtEndTime.Value = DateTime.Now.Date;
            InitEquipTree();
            //InitEquip();
            if (!inhibitor)
            {
               LoadList();
            }
            inhibitor = true;
            txtHiddenWorkShopCode.Text = "0";
        }
    }
    #region 初始化树
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

    /// <summary>
    /// 初始化设备下拉菜单
    /// </summary>
    //private void InitEquip()
    //{
    //    cbxEquip.Items.Clear();
    //    Ext.Net.ListItem item0 = new Ext.Net.ListItem();
    //    item0.Text = "全部";
    //    item0.Value = "全部";
    //    cbxEquip.Items.Add(item0);
    //    EntityArrayList<BasEquip> lst = equipManager.GetListByWhere(BasEquip._.EquipType == "01" && BasEquip._.DeleteFlag == "0");
    //    if (lst.Count > 0)
    //    {
    //        foreach (BasEquip equip in lst)
    //        {
    //            Ext.Net.ListItem item = new Ext.Net.ListItem();
    //            item.Text = equip.EquipName;
    //            item.Value = equip.EquipCode;
    //            cbxEquip.Items.Add(item);
    //        }
    //    }
    //}

    [DirectMethod]
    public void LoadList()
    {
        DateTime BeginTime = DateTime.Now.Date;
        DateTime EndTime = DateTime.Now.Date;
        string workshop = txtHiddenWorkShopCode.Text;
        string shift = "0";
        //string equip = "";
        if (cbxShift.Value != null)
        {
            shift = cbxShift.Value.ToString();
        }
        //if (cbxEquip.Value != null)
        //{
        //    if (cbxEquip.Value.ToString() != "全部")
        //    {
        //        equip = cbxEquip.Value.ToString();
        //    }
        //}
        string equipCodes = string.Empty;
        if (treeStr.Value != null)
        {
            string[] tempEquip = treeStr.Value.ToString().Split(',');
            foreach (string item in tempEquip)
            {
                if (!string.IsNullOrEmpty(item) && item.Length == 5)
                    equipCodes += item + ",";
            }
            
        }
        
        //if (string.IsNullOrEmpty(equipCodes))
        //{
        //    X.Msg.Alert("提示", "请至少选择一个机台！").Show();
        //    return;
        //}
        if (txtBeginTime.Text != "0001/1/1 0:00:00")
        {
            BeginTime = Convert.ToDateTime(txtBeginTime.Value).Date;
        }
        else
        {
            BeginTime = DateTime.Now.Date;
        }
        if (txtEndTime.Text != "0001/1/1 0:00:00")
        {
            EndTime = Convert.ToDateTime(txtEndTime.Value).Date.AddDays(1).AddSeconds(-1);
        }
        else
        {
            EndTime = DateTime.Now.Date.AddDays(1).AddSeconds(-1);
        }
        if (BeginTime > EndTime)
        {
            msg.Alert("操作", "起始时间不能晚于结束时间！");
            msg.Show();
        }
        DataSet ds = planManager.GetEquipmentPruductionSummary(BeginTime, EndTime, workshop, shift, equipCodes);
        DataTable dt = ds.Tables[0];
        tempDt = dt.Copy();
        pnlSummary.GetStore().DataSource = dt;
        pnlSummary.GetStore().DataBind();
    }
    #region 分页相关方法

    #endregion

    /// <summary>
    /// 点击导出按钮激发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt = tempDt.Copy();
        foreach (DataColumn col in dt.Columns)
        {
            bool isshow = false;
            foreach (ColumnBase cb in this.pnlSummary.ColumnModel.Columns)
            {
                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == col.ColumnName.ToUpper()) && (cb.Visible == true))
                {
                    col.ColumnName = cb.Text;
                    isshow = true;
                    break;
                }
            }
            if (!isshow)
            {
                dt.Columns.Remove(col.ColumnName);
            }
        }
        if (dt.Rows.Count > 0)
        {
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(dt, "设备产量统计报告");//生成Excel文件下载
        }
        else
        {
            msg.Alert("操作", "没有可以导出的内容！");
            msg.Show();
        }
    }
}