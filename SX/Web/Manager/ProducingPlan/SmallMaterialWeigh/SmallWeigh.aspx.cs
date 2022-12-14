using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;

public partial class Manager_ProducingPlan_SmallMaterialWeigh_SmallWeigh : System.Web.UI.Page
{
    IPptWeighDataManager pptWeighDataManager = new PptWeighDataManager();
    String Dian = "点";//小数点无法展示的替换字
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            if (Request.QueryString["PlanID"] == null)
            {
                return;
            }
            else
            {
                string planID=Request.QueryString["PlanID"].ToString();
                BuildSet(planID);
            }
        }
    }

    #region 绑定详细信息 根据DataTable动态生成grid的列
    private void BindSet(DataTable dt, string planID)
    {
        #region 添加 统计行
        
        if (dt.Rows.Count <= 0)
        {
            X.Js.Alert("无信息！");
            return; }
        DataTable temp = new DataTable();

        temp = pptWeighDataManager.GetSmallMaterWeighListByPlanID(planID);

        foreach (DataColumn colum in temp.Columns)
        {
            colum.ColumnName = colum.ColumnName.Replace(".", Dian);
        }
        foreach (DataColumn colum in dt.Columns)
        {
            colum.ColumnName = colum.ColumnName.Replace(".", Dian);
        }
        DataRow drRow = temp.NewRow();
        drRow[0] = "平均";
        temp.Rows.Add(drRow);
        //汇总的表达式
        //lColName为要计算的列列表。这是城List<string>型，在别的地方赋值。这里不作说明。
        foreach (DataColumn sColName in dt.Columns)
        {
            if (sColName.ColumnName == "物料"
               || sColName.ColumnName == "车条码"
               || sColName.ColumnName == "车次号"
               || sColName.ColumnName == "物料条码")
            {
                continue;
            }
            try
            {
                drRow[sColName.ColumnName] =Math.Round(Convert.ToDecimal(dt.Compute("Avg([" + sColName.ColumnName.Trim() + "])", "true")),3);
            }
            catch (Exception e)
            {

            }
        }

        //Max
        DataRow drMaxRow = temp.NewRow();
        drMaxRow[0] = "最大";
        temp.Rows.Add(drMaxRow);
        //汇总的表达式
        //lColName为要计算的列列表。这是城List<string>型，在别的地方赋值。这里不作说明。
        foreach (DataColumn sColName in dt.Columns)
        {
            if (sColName.ColumnName == "物料"
               || sColName.ColumnName == "车条码"
               || sColName.ColumnName == "车次号"
               || sColName.ColumnName == "物料条码")
            {
                continue;
            }
            try
            {
                drMaxRow[sColName.ColumnName] = dt.Compute("Max([" + sColName.ColumnName.Trim() + "])", "true");
            }
            catch (Exception e)
            {

            }
        }
        //Min
        DataRow drMinRow = temp.NewRow();
        drMinRow[0] = "最小";
        temp.Rows.Add(drMinRow);
        //汇总的表达式
        //lColName为要计算的列列表。这是城List<string>型，在别的地方赋值。这里不作说明。
        foreach (DataColumn sColName in dt.Columns)
        {
            if (sColName.ColumnName == "物料" 
               || sColName.ColumnName == "车条码" 
               || sColName.ColumnName == "车次号"
               || sColName.ColumnName == "物料条码")
            {
                continue;
            }
            try
            {
                drMinRow[sColName.ColumnName] = dt.Compute("Min([" + sColName.ColumnName.Trim() + "])", "true");
            }
            catch (Exception e)
            {

            }
        }

        //Sum
        DataRow drSumRow = temp.NewRow();
        drSumRow[0] = "总计";
        temp.Rows.Add(drSumRow);
        //汇总的表达式
        foreach (DataColumn sColName in dt.Columns)
        {
            if (sColName.ColumnName == "物料"
               || sColName.ColumnName == "车条码"
               || sColName.ColumnName == "车次号"
               || sColName.ColumnName == "物料条码")
            {
                continue;
            }
            try
            {
                drSumRow[sColName.ColumnName] =dt.Compute("Sum([" + sColName.ColumnName.Trim() + "])", "true");
            }
            catch (Exception e)
            {

            }
        }
        #endregion

        //Standard 添加标准行
        DataTable sdt = pptWeighDataManager.GetSmallMaterWeighStandardByPlanID(planID);
        foreach (DataColumn colum in sdt.Columns)
        {
            colum.ColumnName = colum.ColumnName.Replace(".", Dian);
        }

        DataRow drStandardRow = temp.NewRow();
        for (int i = 0; i < sdt.Columns.Count; i++)
        {
            try
            {
                string strTemp = sdt.Rows[0][i].ToString();
                drStandardRow[i] = strTemp == null ? "" : strTemp;
            }
            catch (Exception){
            }
        }
        temp.Rows.InsertAt(drStandardRow, 0);

        DataRow drErrRow = temp.NewRow();
        for (int i = 0; i < sdt.Columns.Count; i++)
        {
            try
            {
                drErrRow[i] = sdt.Rows[1][i].ToString();
            }
            catch (Exception)
            {
            }
        }
        temp.Rows.InsertAt(drErrRow, 1);
       Session["XLDT"] = temp;
        this.storeDetail.DataSource = temp;
        this.storeDetail.DataBind();
    }
    private void BuildSet(string planID)
    {
        DataTable dt = pptWeighDataManager.GetSmallMaterWeighListByPlanID(planID);
        if (X.IsAjaxRequest)
        {
            this.storeDetail.RemoveFields();
        }
        this.storeDetail.RebuildMeta();
      
        foreach (DataColumn colum in dt.Columns)
        {
            //colum.ColumnName = colum.ColumnName.Replace(".", Dian);
            this.AddField(new ModelField(colum.ColumnName.Replace(".", Dian)));

            Column c = new Column { DataIndex = colum.ColumnName.Replace(".", Dian), Text = colum.ColumnName, Width = 120 };
            if (colum.ColumnName != "物料"
               || colum.ColumnName == "车条码"
               || colum.ColumnName == "车次号"
               || colum.ColumnName == "物料条码")
            {
                Renderer r = new Renderer();
                //r.Fn = "change";   去掉颜色变化，因为Sql取数已经将不合适的数据去掉了，并且该内容展示错误
                c.Renderer = r;
            }
            this.pnlDetailList.ColumnModel.Columns.Add(c);
           
           
        }
     
        this.BindSet(dt, planID);
        if (X.IsAjaxRequest)
        {
            this.pnlDetailList.Reconfigure();
        }
    }
    private void AddField(ModelField field)
    {
        if (X.IsAjaxRequest)
        {
            this.storeDetail.AddField(field);
        }
        else
        {
            this.storeDetail.Model[0].Fields.Add(field);
        }
    }
    #endregion

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        //DataSet ds = adjustDetailManager.GetDetailInfo(Convert.ToDateTime(txtBeginTime.Text).ToString("yyyy-MM-dd"));
        //X.Js.Alert("123");
        //return;
        HttpResponse resp = Page.Response;
        resp.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
        resp.ContentType = "application/ms-excel";

        resp.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode("小料称量信息", System.Text.Encoding.UTF8) + ".xls");
        this.EnableViewState = false;

        string colHeaders = "", Is_item = "";
        int i = 0;

        //DataTable dt = ds.Tables[0];
        DataTable dt = (DataTable)Session["XLDT"];
        DataRow[] myRow = dt.Select();
        for (i = 0; i < dt.Columns.Count; i++)
        {
            colHeaders += dt.Columns[i].Caption.ToString() + "\t";
        }
        colHeaders += "\n";

        resp.Write(colHeaders);
        foreach (DataRow row in myRow)
        {
            for (i = 0; i < dt.Columns.Count; i++)
            {
                Is_item += row[i].ToString() + "\t";
            }
            Is_item += "\n";
            resp.Write(Is_item);
            Is_item = "";
        }
        resp.End();
    }
}