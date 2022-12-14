using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Entity;
using Ext.Net;
using Mesnac.Business.Implements;
using NBear.Common;
using System.Text.RegularExpressions;
using Mesnac.Data.Components;
using System.Data;
using System.Text;
using System.Threading;
using FastReport;

using System.IO;
using Mesnac.Util.Excel;
using Microsoft.Office;

public partial class Manager_Equipment_SparePart_BJStock : Mesnac.Web.UI.Page
{
    protected Eqm_bjStockManager StockManager = new Eqm_bjStockManager();
    protected Eqm_bjtpcdManager bjTypeManager = new Eqm_bjtpcdManager();
    protected Eqm_bjsparecdManager bjsparecdManager = new Eqm_bjsparecdManager();
    protected SYS_USERManager USERManager = new SYS_USERManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitTreeDept();
          //  bindMaintainers();
            bindList();

            this.winSave.Hidden = true;
        }
    }

    #region 树
    /// <summary>
    /// 初始化机台列表树
    /// </summary>
    private void InitTreeDept()
    {
        treeEquip.GetRootNode().RemoveAll();
        treeEquip.GetRootNode().AppendChild(getTreeNodeByDelLevel());
    }

    /// <summary>
    /// 获取机台树的算法
    /// </summary>
    /// <param name="dep_num"></param>
    /// <returns></returns>
    private Node getTreeNodeByDelLevel()
    {
        Node node = new Node();
        node.NodeID = "0";
        node.Text = "全部";
        node.Expanded = true;
        Dictionary<string, string> depChildFristList = new Dictionary<string, string>();
        var query = bjTypeManager.GetListByWhereAndOrder(Eqm_bjtpcd._.BJ_tpcode.Length == 2, Eqm_bjtpcd._.BJ_tpcode.Asc);
        foreach (var info in query)
        {
            Node childNode = new Node();
            childNode.NodeID = info.BJ_tpcode;
            childNode.Text = info.BJ_tpname;
            childNode.Expanded = false;
            var child = bjTypeManager.GetListByWhereAndOrder(Eqm_bjtpcd._.BJ_ParentCode == info.BJ_tpcode & Eqm_bjtpcd._.BJ_tpcode.Length > 2, Eqm_bjtpcd._.BJ_tpcode.Asc);
            foreach (var item in child)
            {
                Node nodeLeaf = new Node();
                nodeLeaf.Text = item.BJ_tpname;
                nodeLeaf.Qtip = item.BJ_tpcode;
                nodeLeaf.Leaf = true;
                childNode.Children.Add(nodeLeaf);
            }
            node.Children.Add(childNode);
        }
        return node;
    }
    #endregion


    /// <summary>
    /// 相应点击机台树事件
    /// </summary>
    /// <param name="group"></param>
    [DirectMethod]
    public void LoadGridData(string group)
    {
        //判断当前机台当前时间是否设置班组信息
        //hidden_parent_num.Value = group;
        hidden_equip_code.Text = group;
        this.store.DataSource = getList(group);
        this.store.DataBind();


    }

    private DataSet getList(string BJ_tpcode)
    {

        return GetDataByParas(BJ_tpcode);
    }

   
    public System.Data.DataSet GetDataByParas(string BJ_tpcode)
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"
SELECT T.Dep_Code,T1.BJ_tpname,T.BJ_CODE,T.Batch_Code,T.BJ_Name,T.BJ_specType,T.Stock_num,T.Total_Price,T2.Fac_Name,T3.BJ_Class,T.Pos_Name,T.bjprice,
(DATEDIFF(day,T.in_date,getdate())) KULING,T.planuser,T3.low_reserve,T3.high_reserve,T3.memo
 FROM eqm_bjstock  T
 LEFT JOIN Eqm_bjtpcd T1 ON T1.BJ_tpcode=T.BJ_tpCode
 LEFT JOIN JCZL_SubFac T2 ON T2.Dep_Code=T.Dep_Code
 LEFT JOIN Eqm_bjsparecd T3 ON T3.BJ_code=T.BJ_code");
        sb.AppendLine("WHERE 1=1");
        if (!string.IsNullOrEmpty(BJ_tpcode))
            sb.AppendLine("AND T1.BJ_tpcode='" + BJ_tpcode + "'");
        if (!string.IsNullOrEmpty(txtBJ_code.Text))
            sb.AppendLine("AND T.BJ_code='" + txtBJ_code.Text + "'");
        if (!string.IsNullOrEmpty(txtBJ_name.Text))
            sb.AppendLine("AND T.BJ_name='" + txtBJ_name.Text + "'");
        if (!string.IsNullOrEmpty(cbxType.Text))
            sb.AppendLine("AND T3.BJ_Class='" + cbxType.Text + "'");
        #endregion

        NBear.Data.CustomSqlSection css = StockManager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }


    private void bindList()
    {
        this.store.DataSource = getList(hidden_equip_code.Text);
        this.store.DataBind();
    }

    #region 按钮事件响应
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindList();
    }
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = getList(hidden_equip_code.Text);
        //huiw,2013-10-28添加，判断不为空时才导出excel
        if (ds == null || ds.Tables[0].Rows.Count == 0)
        {
            X.Msg.Alert("提示", "未查询出数据！").Show();
        }
        else
        {
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                bool isshow = false;
                DataColumn dc = ds.Tables[0].Columns[i];
                foreach (ColumnBase cb in this.pnlList.ColumnModel.Columns)
                {
                    if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()))
                    {
                        dc.ColumnName = cb.Text;
                        isshow = true;
                        break;
                    }
                }
                if (!isshow)
                {
                    ds.Tables[0].Columns.Remove(dc.ColumnName);
                    i--;
                }
            }
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "备件库存管理导出");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.hideMode.Text = "Add";
        txtbjcode.Text = "";
        txtspectype.Text = "";
        txtnum.Text = "";
        txtmoney.Text = "";
        txtman.Text = "";
        this.winSave.Hidden = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.hideMode.Text == "Add")//添加
        {
            Eqm_bjStock record = new Eqm_bjStock();
            EntityArrayList<Eqm_bjsparecd> listEdit = bjsparecdManager.GetListByWhere(Eqm_bjsparecd._.BJ_code == txtbjcode.Text);  
            if (listEdit.Count == 0)
            { X.Msg.Alert("提示", "没有此备件编码！").Show(); return; }
            record.Dep_Code="01";
            record.Batch_Code = GetMaxPlanID();
            record.BJ_code = txtbjcode.Text;
            record.BJ_Name = listEdit[0].BJ_name;
            record.BJ_specType = txtspectype.Text;
            record.Total_Price = Convert.ToDecimal(txtmoney.Text);
            record.Stock_num = Convert.ToDecimal(txtnum.Text);
            //record.BJ_tpCode = listEdit[0].BJ_tpcode;
            record.BJ_tpCode = hidden_equip_code.Text;
            record.In_date = DateTime.Now;
            record.Pos_Name = listEdit[0].Pos_code;

            TimeSpan ts = DateTime.Now - record.In_date.Value;
            record.ExistDays = ts.Days;
            record.Bjprice = listEdit[0].Plan_price;
            //EntityArrayList<SYS_USER> listEdit1 = USERManager.GetListByWhere(SYS_USER._.USER_ID == this.UserID); 
            //record.Planuser = listEdit1[0].Real_name;
            record.Planuser = txtman.Text;

            if (StockManager.Insert(record) >= 0)
            {
                this.AppendWebLog("备件库存添加", "添加备件名称：" + record.BJ_Name);
                winSave.Hidden = true;
                bindList();
                X.Msg.Alert("提示", "添加完成！").Show();
            }
            else
            {
                X.Msg.Alert("提示", "添加失败！").Show();
            }
        }
        else//修改
        {
            EntityArrayList<Eqm_bjStock> listEdit = StockManager.GetListByWhere(Eqm_bjStock._.Batch_Code == hideObjID.Text);
            if (listEdit.Count == 0)
            {
                X.Msg.Alert("提示", "无此备件，修改失败！").Show();
                return;
            }
            Eqm_bjStock record = listEdit[0];
            EntityArrayList<Eqm_bjsparecd> listEdit2 = bjsparecdManager.GetListByWhere(Eqm_bjsparecd._.BJ_code == txtbjcode.Text);  
            if (record != null)
            {
                if (record.BJ_code != txtbjcode.Text)
                {
                    X.Msg.Alert("提示", "不允许修改备件代码！").Show(); return;
                }
                record.BJ_specType = txtspectype.Text;
                record.Total_Price = Convert.ToDecimal(txtmoney.Text);
                record.Stock_num = Convert.ToDecimal(txtnum.Text);
                record.Pos_Name = listEdit2[0].Pos_code;
                TimeSpan ts = DateTime.Now - record.In_date.Value;
                record.ExistDays = ts.Days;
                //EntityArrayList<SYS_USER> listEdit1 = USERManager.GetListByWhere(SYS_USER._.USER_ID == this.UserID);
                //record.Planuser = listEdit1[0].Real_name;
                record.Planuser = txtman.Text;

                if (StockManager.Update(record) >= 0)
                {
                    this.AppendWebLog("备件库存修改", "修改备件名称：" + record.BJ_Name);
                    winSave.Hidden = true;
                    bindList();
                    X.Msg.Alert("提示", "修改完成！").Show();
                }
                else
                {
                    X.Msg.Alert("提示", "修改失败！").Show();
                }
            }
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        winSave.Hidden = true;
    }
    #endregion


    //private void bindMaintainers()
    //{
    //    WhereClip where = new WhereClip();
    //    EntityArrayList<Eqm_bjtpcd> list = bjTypeManager.GetListByWhere(where);
    //    foreach (Eqm_bjtpcd type in list)
    //    {
    //        Ext.Net.ListItem item = new Ext.Net.ListItem(type.BJ_tpname, type.BJ_tpcode);
    //        cbxtpname.Items.Add(item);
    //    }
    //}

    //#region 下拉列表事件响应
    //[DirectMethod]
    //protected void cbxEquipClass_SelectChanged(object sender, EventArgs e)
    //{
    //    bindMaintainers();
    //}
    //#endregion


    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Edit(string ObjID)
    {
        EntityArrayList<Eqm_bjStock> list = StockManager.GetListByWhere(Eqm_bjStock._.Batch_Code == ObjID);
        if (list.Count == 0)
        {
            X.Msg.Alert("提示", "此条记录已经不存在！").Show();
            return;
        }
        Eqm_bjStock record = list[0];


        if (record != null)
        {
            txtbjcode.SetValue(record.BJ_code);
            txtspectype.SetValue(record.BJ_specType);
            txtnum.SetValue(record.Stock_num);
            txtmoney.SetValue(record.Total_Price);
            txtman.SetValue(record.Planuser);

            hideObjID.Text = ObjID;
            this.hideMode.Text = "Edit";
            this.winSave.Hidden = false;
        }
        else
        {
            bindList();
            X.Msg.Alert("提示", "此条记录已经不存在！").Show();
        }


    }

    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_detail(string ObjID)
    {
        DataSet ds = StockManager.GetBySql("SELECT BJ_code,Batch_Code,BJ_Name  FROM eqm_bjstock where Batch_Code = '" + ObjID + "'").ToDataSet();
        ds.Tables[0].TableName = "BJStock";
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            winDetail.Hidden = false;
            //加载模板
            FastReport.Report report = this.WebReport.Report;
            report.Load(Server.MapPath("../SparePart/BJStock.frx"));
            report.SetParameterValue("Batch_Code", ds.Tables[0].Rows[0]["Batch_Code"].ToString());
            //绑定信息
            report.RegisterData(ds.Tables[0], "BJStock");
            report.Refresh();
            WebReport.Update();
            WebReport.Refresh();
        }
    }
    

    [DirectMethod]
    public void pnlList_Delete(string ObjID)
    {
        EntityArrayList<Eqm_bjStock> list = StockManager.GetListByWhere(Eqm_bjStock._.Batch_Code == ObjID);
        if (list.Count == 0)
        {
            X.Msg.Alert("提示", "此条记录已经不存在！").Show();
            return;
        }
        Eqm_bjStock record = list[0];

        StockManager.DeleteByWhere(Eqm_bjStock._.Batch_Code == ObjID);
        this.AppendWebLog("备件库存信息删除", "修改备件名称：" + record.BJ_Name);

        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }
    #endregion

    //自动生成计划号
    protected string GetMaxPlanID()
    {
        string planID = "";
        EntityArrayList<Eqm_bjStock> list = StockManager.GetAllListOrder(Eqm_bjStock._.Batch_Code.Desc);
        if (list.Count > 0)
        {
            planID = list[0].Batch_Code;
            if (planID.Substring(0, 8) == DateTime.Now.Date.ToString("yyyyMMdd"))
            {
                if (Convert.ToInt32(planID.Substring(9, 4))<= 8)
                {
                    planID = planID.Substring(0, 8) + "-" + "000" + (Convert.ToInt32(planID.Substring(9, 4)) + 1).ToString();
                }
                else if (Convert.ToInt32(planID.Substring(9, 4)) <= 98)
                {
                    planID = planID.Substring(0, 8) + "-" + "00" + (Convert.ToInt32(planID.Substring(9, 4)) + 1).ToString();
                }

                else if (Convert.ToInt32(planID.Substring(9, 4)) <= 198)
                {
                    planID = planID.Substring(0, 8) + "-" + "0" + (Convert.ToInt32(planID.Substring(9, 4)) + 1).ToString();
                }
            }
            else
            {
                planID = DateTime.Now.Date.ToString("yyyyMMdd") + "-" + "0001";
            }
        }
        return planID;
    }

}