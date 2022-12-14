using System;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using NBear.Common;
using System.Collections.Generic;
using System.Text;
using Mesnac.Entity;

using System.IO;
using Mesnac.Util.Excel;
using Microsoft.Office;

public partial class Manager_Equipment_BJplan : Mesnac.Web.UI.Page
{
    protected Eqm_SparePlanManager Manager = new Eqm_SparePlanManager();
    protected Eqm_bjsparecdManager bjManager = new Eqm_bjsparecdManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !Page.IsPostBack)
        {
            PageInit();
        }
    }

    #region 初始化下拉列表
    private void PageInit()
    {
        this.winSave.Hidden = true;
        DateBeginTime.SelectedDate = DateTime.Now.AddDays(-7);
        DateEndTime.SelectedDate = DateTime.Now;
        bindList();
    }

    private DataSet getList()
    {
        return GetDataByParas();
    }

    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"select *,case Plan_State when 1 then '完成' when 0 then '新计划' end state from eqm_SparePlan");
        sb.AppendLine("WHERE 1=1");
        if (!string.IsNullOrEmpty(DateBeginTime.SelectedDate.ToString()))
        {
            sb.AppendLine("AND Plan_Date>='" + DateBeginTime.SelectedDate.ToString("yyyy-MM-dd") + "'");
        }
        if (!string.IsNullOrEmpty(DateEndTime.SelectedDate.ToString()))
        {
            sb.AppendLine("AND Plan_Date<='" + DateEndTime.SelectedDate.ToString("yyyy-MM-dd") + "'");
        }
        #endregion

        NBear.Data.CustomSqlSection css = Manager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }
    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }
    #endregion



    #region 按钮事件响应
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindList();
    }
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = getList();
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "备件采购计划");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.hideMode.Text = "Add";
        dateplan.Text = DateTime.Now.ToString("yyyy-MM-dd");
        txtno.Text = "";
        txtcangku.Text = "";
        txtcode.Text = "";
        txtnum.Text = "";
        txtuser.Text = "";
        cbxstate.Text = "";
        txtxuqiu.Text = "";
        txtshiji.Text = "";
        txtyongtu.Text = "";
        this.winSave.Hidden = false;
    }

    //添加计划确定
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.hideMode.Text == "Add")//添加
        {

            EntityArrayList<Eqm_SparePlan> list2 = Manager.GetListByWhere(Eqm_SparePlan._.BJ_code == txtcode.Text && Eqm_SparePlan._.SerialID == Convert.ToInt16(txtno.Text) && Eqm_SparePlan._.Plan_Date == dateplan.Text);
            if (list2.Count > 0)
            {
                X.Msg.Alert("提示", "已经有该条信息，请核实！").Show();
                return;
            }
            Eqm_SparePlan record = new Eqm_SparePlan();
            decimal price = 0;
            record.Plan_Date = dateplan.Text;
            record.SerialID = Convert.ToInt16(txtno.Text);
            record.BJ_code = txtcode.Text;
            record.DepCode = "01";
            EntityArrayList<Eqm_bjsparecd> list = bjManager.GetListByWhere(Eqm_bjsparecd._.BJ_code == txtcode.Text);
            if (list.Count > 0)
            {
                record.BJ_name = list[0].BJ_name;
                record.BJ_specType = list[0].BJ_specType;
                record.BJ_tpcode = list[0].BJ_tpcode;
                record.Unit_Code = list[0].Unit_name;
                record.Plan_price = list[0].Plan_price;
                price = Convert.ToDecimal(list[0].Plan_price);
            }
            else
            {
                X.Msg.Alert("提示", "没有改备件代码，请核实！").Show();
                return;
            }
            record.Stock_Code = txtcangku.Text;
            record.Plan_Num = Convert.ToDecimal(txtnum.Text);
            record.Stock_Worker = txtuser.Text;
            if (cbxstate.SelectedItem.Value == "0")
            { record.Plan_State = "0"; }
            else if (cbxstate.SelectedItem.Value == "1")
            { record.Plan_State = "1"; }
            else
            { record.Plan_State = ""; }

            record.Prefer_date = txtxuqiu.Text;
            record.RealIn_Date = txtshiji.Text;
            record.Remark = txtyongtu.Text;
            record.Total_Price = price * Convert.ToDecimal(txtnum.Text);

            if (Manager.Insert(record) >= 0)
            {
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
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(@"update eqm_SparePlan set Plan_State = '"+cbxstate.Value+"', Stock_Code='" + txtcangku.Text + "' ,Plan_Num='" + txtnum.Text + "',Stock_Worker='" + txtuser.Text + "',prefer_date='" + txtxuqiu.Text + "',RealIn_Date='" + txtshiji.Text + "',Remark='" + txtyongtu.Text + "' where Plan_Date='" + dateplan.Text + "' and SerialID='" + txtno.Text + "' and BJ_code='" + txtcode.Text + "' ");
                Manager.GetBySql(sb.ToString()).ToDataSet();
                winSave.Hidden = true;
                bindList();
                X.Msg.Alert("提示", "修改完成！").Show();
            }
            catch
            {
                X.Msg.Alert("提示", "修改失败！").Show();
            }
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        winSave.Hidden = true;
    }
    #endregion

    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Edit(string Plan_Date, string SerialID, string BJ_code)
    {
        try
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"select * from eqm_SparePlan where Plan_Date='" + Plan_Date + "' and SerialID='" + SerialID + "' and BJ_code='" + BJ_code + "'");

            DataTable dt = Manager.GetBySql(sb.ToString()).ToDataSet().Tables[0];
            dateplan.Text = dt.Rows[0]["Plan_Date"].ToString();
            txtno.Text = dt.Rows[0]["SerialID"].ToString();
            txtcangku.Text = dt.Rows[0]["Stock_Code"].ToString();
            txtcode.Text = dt.Rows[0]["BJ_code"].ToString();
            txtnum.Text = dt.Rows[0]["Plan_Num"].ToString();
            txtuser.Text = dt.Rows[0]["Stock_Worker"].ToString();
            if (dt.Rows[0]["Plan_State"].ToString() == "1")
            {
                cbxstate.Text = "完成";
                cbxstate.Value = "1";
            }
            else if (dt.Rows[0]["Plan_State"].ToString() == "0")
            {
                cbxstate.Text = "新计划";
                cbxstate.Value = "0";
            }
            else
            { cbxstate.Text = ""; }
            txtxuqiu.Text = dt.Rows[0]["prefer_date"].ToString();
            txtshiji.Text = dt.Rows[0]["RealIn_Date"].ToString();
            txtyongtu.Text = dt.Rows[0]["Remark"].ToString();


            this.hideMode.Text = "Edit";
            this.winSave.Hidden = false;
        }
        catch
        {
            bindList();
            X.Msg.Alert("提示", "此条记录已经不存在！").Show();
        }
    }
    #endregion
    [DirectMethod]
    public void pnlList_Delete(string Plan_Date, string SerialID, string BJ_code)
    {
        try
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(@"delete from eqm_SparePlan where Plan_Date='" + Plan_Date + "' and SerialID='" + SerialID + "' and BJ_code='" + BJ_code + "'");
            Manager.GetBySql(sb.ToString()).ToDataSet();
            X.Msg.Alert("提示", "删除成功！").Show();
            bindList();
        }
        catch 
        {
            X.Msg.Alert("提示", "删除失败！").Show();
        }
    }

    [DirectMethod]
    public void btnDownload_ClickEvent(object sender, DirectEventArgs args)
    {
        try
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=备件采购导入模板.xls");
            //Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.ContentType = "application/ms-excel";
            Response.WriteFile(Path.Combine(Request.PhysicalApplicationPath, "\\resources\\xls\\备件采购导入模板.xls"));
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            X.Msg.Alert("系统错误", "下载失败：" + ex.Message);
        }

    }


    //备件采购模板导入
    public void UploadClickBill(object sender, DirectEventArgs e)
    {
        var rowcount = 0;
        try
        {
            //Sheet名：Sheet1
            var file = FileUploadField2.PostedFile.InputStream;
            //var file = FileUploadField2.PostedFile.InputStream.ToString();
            //Mesnac.Util.Excel.DataToFile dtf = new Mesnac.Util.Excel.DataToFile();
            DataTable dt = Mesnac.Util.Excel.DataToFile.RenderFromExcel(file, "Sheet1");
            string[] oldColName = { "序号",
                                    "申请部门",
                                    "计划申报日期",
                                    "计划类别",
                                    "物料编码",
                                    "物料短文本",
                                    "单位",
                                    "申请数量",
                                    "库存数量",
                                    "月均用量",
                                    "单价",
                                    "申请金额（元）",
                                    "申请人",
                                    "用途",
                                    "要求到货时间",
                                    "历史供应商",
                                    "历史/最新价格",
                                    "备注"};
            string[] newColName = { "xuhao",
                                    "bumen",
                                    "riqi" ,
                                    "leibie",
                                    "wuliaobianma",
                                    "duanwenben",
                                    "danwei",
                                    "shenqingshu",
                                    "kucunshu",
                                    "yuejun",
                                    "danjia",
                                    "shenqingjine",
                                    "shenqingren",
                                    "yongtu",
                                    "yaoqiudaohuo",
                                    "lishigongying",
                                    "lishijiage",
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
                    if (!string.IsNullOrEmpty(dr["riqi"].ToString()))
                    {
                        try
                        {
                            string Plan_Date = Convert.ToDateTime(dr["riqi"]).ToString("yyyy-MM-dd");
                            string Plan_Date2 = Convert.ToDateTime(dr["yaoqiudaohuo"]).ToString("yyyy-MM-dd");
                        }
                        catch
                        {
                            X.Msg.Alert("提示", "计划申报日期、到货日期-请输入这种格式（2020-02-02），请检查！").Show(); return;
                        }
                    }
                    EntityArrayList<Eqm_SparePlan> list = Manager.GetListByWhere(Eqm_SparePlan._.SerialID == dr["xuhao"].ToString() && Eqm_SparePlan._.Plan_Date == Convert.ToDateTime(dr["riqi"]).ToString("yyyy-MM-dd") && Eqm_SparePlan._.BJ_code == dr["wuliaobianma"].ToString());
                    if (list.Count > 0)
                    { X.Msg.Alert("提示", "存在此采购计划，不允许重复添加！").Show(); return; }
                }

                foreach (DataRow dr in dt.Rows)
                {
                    Eqm_SparePlan record = new Eqm_SparePlan();
                    //Eqm_bjtpcd record1 = new Eqm_bjtpcd();
                    //EntityArrayList<Eqm_bjtpcd> list = bjTypeManager.GetListByWhere(Eqm_bjtpcd._.BJ_tpname == dr["BJ_tpname"].ToString());
                    //if (list.Count == 0)
                    //{
                    //    X.Msg.Alert("提示", "不存在该备件分类，请先添加！").Show(); return;
                    //}
                    //EntityArrayList<Eqm_bjsparecd> list1 = Eqm_bjsparecdManager.GetListByWhere(Eqm_bjsparecd._.BJ_code == dr["BJ_code"].ToString());
                    //if (list1.Count > 0)
                    //{
                    //    X.Msg.Alert("提示", "备件编码有重复，请检查！").Show(); return;
                    //}

                    record.DepCode = "01";
                    record.Plan_Date = Convert.ToDateTime(dr["riqi"]).ToString("yyyy-MM-dd");
                    record.SerialID = Convert.ToInt16(dr["xuhao"].ToString());
                    record.BJ_code = dr["wuliaobianma"].ToString();
                    record.BJ_name = dr["duanwenben"].ToString();
                    record.Unit_Code = dr["danwei"].ToString();
                    record.Plan_Num = Convert.ToDecimal(dr["shenqingshu"].ToString());
                    record.Stock_Num = Convert.ToDecimal(dr["kucunshu"].ToString());
                    record.Month_Num = Convert.ToDecimal(dr["yuejun"].ToString());
                    record.Plan_price = Convert.ToDecimal(dr["danjia"].ToString());
                    record.Total_Price = Convert.ToDecimal(dr["shenqingjine"].ToString());
                    record.Stock_Worker = dr["shenqingren"].ToString();
                    record.Remark = dr["yongtu"].ToString();
                    record.Prefer_date = Convert.ToDateTime(dr["yaoqiudaohuo"]).ToString("yyyy-MM-dd");
                    Manager.Insert(record);

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