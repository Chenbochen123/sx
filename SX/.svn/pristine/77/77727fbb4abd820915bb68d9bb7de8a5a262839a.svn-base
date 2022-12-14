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

public partial class Manager_Equipment_SparePart_SparePart : Mesnac.Web.UI.Page
{
    protected Eqm_bjsparecdManager Eqm_bjsparecdManager = new Eqm_bjsparecdManager();
    protected Eqm_bjtpcdManager bjTypeManager = new Eqm_bjtpcdManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitTreeDept();
            bindMaintainers();
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
        sb.AppendLine(@"SELECT T.BJ_code,T.BJ_name,T.BJ_specType,T.Unit_name,T.BJ_tpcode,T.high_reserve,T.low_reserve,T.Pos_code,T.Plan_price,T.BJ_Class,memo,T1.BJ_tpname
FROM Eqm_bjsparecd T
LEFT JOIN Eqm_Bjtpcd T1 ON T.BJ_tpcode=T1.BJ_tpcode");
        sb.AppendLine("WHERE 1=1");
        if (!string.IsNullOrEmpty(BJ_tpcode))
            sb.AppendLine("AND T1.BJ_tpcode='" + BJ_tpcode + "'");
        if (!string.IsNullOrEmpty(txtBJ_code.Text))
            sb.AppendLine("AND T.BJ_code='" + txtBJ_code.Text + "'");
        if (!string.IsNullOrEmpty(txtBJ_name.Text))
            sb.AppendLine("AND T.BJ_name='" + txtBJ_name.Text + "'");
        if (!string.IsNullOrEmpty(txtBJ_specType.Text))
            sb.AppendLine("AND T.BJ_specType='" + txtBJ_specType.Text + "'");
        #endregion

        NBear.Data.CustomSqlSection css = Eqm_bjsparecdManager.GetBySql(sb.ToString());
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "设备维修计划导出");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.hideMode.Text = "Add";
        txtbjcode.Text = "";
        txtbjname.Text = "";
        txtspectype.Text = "";
        txtunit.Text = "";
        cbxtpname.SetValue(hidden_equip_code.Text);
        txthigh.Text = "";
        txtlow.Text = "";
        txtpos.Text = "";
        txtprice.Text = "";
        cbxABC.SetValue(null);
        txtremark.Text = "";
        this.winSave.Hidden = false;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.hideMode.Text == "Add")//添加
        {
            Eqm_bjsparecd record = new Eqm_bjsparecd();
            record.BJ_code = txtbjcode.Text;
            record.BJ_name = txtbjname.Text;
            record.BJ_specType = txtspectype.Text;
            record.Unit_name = txtunit.Text;
            record.BJ_tpcode = cbxtpname.SelectedItem.Value.ToString();
            record.High_reserve = Convert.ToDecimal(txthigh.Text);
            record.Low_reserve = Convert.ToDecimal(txtlow.Text);
            record.Pos_code = txtpos.Text;
            record.Plan_price = Convert.ToDecimal(txtprice.Text);
            record.BJ_Class = cbxABC.SelectedItem.Value;
            record.Memo = txtremark.Text;

            if (Eqm_bjsparecdManager.Insert(record) >= 0)
            {
                this.AppendWebLog("备件信息添加", "添加备件名称：" + record.BJ_name);
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
            if (hideObjID.Text != txtbjcode.Text)
            {
                X.Msg.Alert("提示", "不允许修改备件代码，修改失败！").Show();
                return;
            }
            EntityArrayList<Eqm_bjsparecd> listEdit = Eqm_bjsparecdManager.GetListByWhere(Eqm_bjsparecd._.BJ_code == hideObjID.Text);
            if (listEdit.Count == 0)
            {
                X.Msg.Alert("提示", "无此备件，修改失败！").Show();
                return;
            }
            Eqm_bjsparecd record = listEdit[0];
            if (record != null)
            {
                record.BJ_code = txtbjcode.Text;
                record.BJ_name = txtbjname.Text;
                record.BJ_specType = txtspectype.Text;
                record.Unit_name = txtunit.Text;
                record.BJ_tpcode = cbxtpname.SelectedItem.Value.ToString();
                record.High_reserve = Convert.ToDecimal(txthigh.Text);
                record.Low_reserve = Convert.ToDecimal(txtlow.Text);
                record.Pos_code = txtpos.Text;
                record.Plan_price = Convert.ToDecimal(txtprice.Text);
                record.BJ_Class = cbxABC.SelectedItem.Value;
                record.Memo = txtremark.Text;


                if (Eqm_bjsparecdManager.Update(record) >= 0)
                {
                    this.AppendWebLog("备件信息修改", "修改备件名称：" + record.BJ_name);
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


    private void bindMaintainers()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<Eqm_bjtpcd> list = bjTypeManager.GetListByWhere(where);
        foreach (Eqm_bjtpcd type in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(type.BJ_tpname, type.BJ_tpcode);
            cbxtpname.Items.Add(item);
        }
    }

    #region 下拉列表事件响应
    [DirectMethod]
    protected void cbxEquipClass_SelectChanged(object sender, EventArgs e)
    {
        bindMaintainers();
    }
    #endregion


    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Edit(string ObjID)
    {
        //hidden_type.Text = "2";
        EntityArrayList<Eqm_bjsparecd> list = Eqm_bjsparecdManager.GetListByWhere(Eqm_bjsparecd._.BJ_code == ObjID);
        if (list.Count == 0)
        {
            X.Msg.Alert("提示", "此条记录已经不存在！").Show();
            return;
        }
        Eqm_bjsparecd record = list[0];


        if (record != null)
        {
            txtbjcode.SetValue(record.BJ_code);
            txtbjname.SetValue(record.BJ_name);
            txtspectype.SetValue(record.BJ_specType);
            txtunit.SetValue(record.Unit_name);
            cbxtpname.SetValue(record.BJ_tpcode);
            txthigh.SetValue(record.High_reserve);
            txtlow.SetValue(record.Low_reserve);
            txtpos.SetValue(record.Pos_code);
            txtprice.SetValue(record.Plan_price);
            cbxABC.SetValue(record.BJ_Class);
            txtremark.SetValue(record.Memo);

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
    public void commandcolumn_direct_detail(string objid)
    {
        DataSet ds = Eqm_bjsparecdManager.GetBySql("select BJ_code,BJ_name  from Eqm_bjsparecd where BJ_code = '" + objid + "'").ToDataSet();
        ds.Tables[0].TableName = "SparePart";
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            winDetail.Hidden = false;
            //加载模板
            FastReport.Report report = this.WebReport.Report;
            report.Load(Server.MapPath("../SparePart/SparePart.frx"));
            report.SetParameterValue("BJ_code", ds.Tables[0].Rows[0]["BJ_code"].ToString());
            //绑定信息
            report.RegisterData(ds.Tables[0], "SparePart");
            report.Refresh();
            WebReport.Update();
            WebReport.Refresh();
        }
    }
    

    [DirectMethod]
    public void pnlList_Delete(string ObjID)
    {
        EntityArrayList<Eqm_bjsparecd> list = Eqm_bjsparecdManager.GetListByWhere(Eqm_bjsparecd._.BJ_code == ObjID);
        if (list.Count == 0)
        {
            X.Msg.Alert("提示", "此条记录已经不存在！").Show();
            return;
        }
        Eqm_bjsparecd record = list[0];

        Eqm_bjsparecdManager.DeleteByWhere(Eqm_bjsparecd._.BJ_code == ObjID);
        this.AppendWebLog("备件信息删除", "修改备件名称：" + record.BJ_name);

        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }
    #endregion



    [DirectMethod]
    public void btnDownload_ClickEvent(object sender, DirectEventArgs args)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=备件导入模板.xls");
            //Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/ms-excel";
            Response.WriteFile(Path.Combine(Request.PhysicalApplicationPath, "\\resources\\xls\\备件导入模板.xls"));
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            X.Msg.Alert("系统错误", "下载失败：" + ex.Message);
        }

    }

    //备件模板导入
    public void UploadClickBill(object sender, DirectEventArgs e)
    {
        var rowcount = 0;
        try
        {
            //excel列头格式：单据编号 行号 工厂编号 库存地点编号 胎号 物料编号 物料描述 品级 生产日期 库龄 移动类型 扫描人 扫描时间
            //Sheet名：Sheet1
            var file = FileUploadField2.PostedFile.InputStream;
            //var file = FileUploadField2.PostedFile.InputStream.ToString();
            //Mesnac.Util.Excel.DataToFile dtf = new Mesnac.Util.Excel.DataToFile();
            DataTable dt = Mesnac.Util.Excel.DataToFile.RenderFromExcel(file, "Sheet1");
            string[] oldColName = { "备件代码",
                                    "备件名称",
                                    "规格型号",
                                    "标准单位",
                                    "备件分类",
                                    "最高储备定额",
                                    "最低储备定额",
                                    "存放货位",
                                    "计划价格",
                                    "ABC分类",
                                    "备注"};
            string[] newColName = { "BJ_code",
                                    "BJ_name",
                                    "BJ_specType" ,
                                    "Unit_name",
                                    "BJ_tpname",
                                    "high_reserve",
                                    "low_reserve",
                                    "Pos_code",
                                    "Plan_price",
                                    "BJ_Class",
                                    "memo"};
            rowcount = dt.Rows.Count;
            if (dt.Rows.Count == 0)
            {
                X.Msg.Show(new MessageBoxConfig { Title = "提示", Message = "要导入的文件中无数据：", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.WARNING });
                this.Progress2.Reset();
                this.Progress2.UpdateProgress(0, "要导入的文件中无数据");
                return;
            }
            bool existsCol = true;
            string msg = "";
            //判断文件列是否存在
            for (int i = 0; i < oldColName.Length; i++)
            {
                if (!dt.Columns.Contains(oldColName[i]))
                {
                    existsCol = false;
                    msg += oldColName[i] + "<br/>";
                }
                else
                {
                    dt.Columns[oldColName[i]].ColumnName = newColName[i];
                }
            }
            if (!existsCol)
            {
                X.Msg.Show(new MessageBoxConfig { Title = "提示", Message = "要导入的文件中缺少列：<br/>" + msg, Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.WARNING });
                this.Progress2.Reset();
                this.Progress2.UpdateProgress(0, "要导入的文件中缺少相关列");
                return;
            }

            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    Eqm_bjsparecd record = new Eqm_bjsparecd();
                    Eqm_bjtpcd record1 = new Eqm_bjtpcd();
                    EntityArrayList<Eqm_bjtpcd> list = bjTypeManager.GetListByWhere(Eqm_bjtpcd._.BJ_tpname == dr["BJ_tpname"].ToString());
                    if (list.Count == 0)
                    {
                        X.Msg.Alert("提示", "不存在该备件分类，请先添加！").Show(); return;
                    }
                    EntityArrayList<Eqm_bjsparecd> list1 = Eqm_bjsparecdManager.GetListByWhere(Eqm_bjsparecd._.BJ_code == dr["BJ_code"].ToString());
                    if (list1.Count > 0)
                    {
                        X.Msg.Alert("提示", "备件编码有重复，请检查！").Show(); return;
                    }

                    record.BJ_tpcode = list[0].BJ_tpcode;
                    record.BJ_code = dr["BJ_code"].ToString();
                    record.BJ_name = dr["BJ_name"].ToString();
                    record.BJ_specType = dr["BJ_specType"].ToString();
                    record.Unit_name = dr["Unit_name"].ToString();
                    record.High_reserve = Convert.ToDecimal(dr["high_reserve"]);
                    record.Low_reserve = Convert.ToDecimal(dr["low_reserve"]);
                    record.Pos_code = dr["Pos_code"].ToString();
                    record.Plan_price = Convert.ToDecimal(dr["Plan_price"]);
                    record.BJ_Class = dr["BJ_Class"].ToString();
                    record.Memo = dr["memo"].ToString();
                    Eqm_bjsparecdManager.Insert(record);

                }
            }


            this.Progress2.Reset();
            this.Progress2.UpdateProgress(1, "已完成!");
        }
        catch (Exception ex)
        {
            this.Progress2.Reset();
            this.Progress2.UpdateProgress(1, "数据读取异常：" + ex.Message);
        }
    }
}