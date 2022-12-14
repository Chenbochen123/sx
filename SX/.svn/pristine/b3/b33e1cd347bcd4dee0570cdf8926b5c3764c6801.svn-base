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


public partial class Manager_ShopStorage_ShopCheck : Mesnac.Web.UI.Page
{
    protected Pst_mmshopcheckManager Manager = new Pst_mmshopcheckManager();
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
        txtdate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        this.winSave.Hidden = true;
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
        sb.AppendLine(@"select a.*,b.Mater_name from Pst_mmshopcheck a
left join Pmt_material b on a.Mater_code=b.Mater_code");
        sb.AppendLine("WHERE 1=1");
        if (!string.IsNullOrEmpty(txtdate.SelectedDate.ToString("yyyy-MM-dd")))
        {
            sb.AppendLine("AND Plan_date='" + txtdate.SelectedDate.ToString("yyyy-MM-dd") + "'");
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

    //添加计划确定
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Pst_mmshopcheck record = Manager.GetById(hideObjID.Text);

            if (record != null)
            {
                record.Check_weig = Convert.ToDecimal(txtweight.Text);

                if (Manager.Update(record) >= 0)
                {
                    this.AppendWebLog("重量修改", "修改号：" + record.Shop_ID);
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
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        winSave.Hidden = true;
    }
    #endregion

    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Edit(string ObjID)
    {
        EntityArrayList<Pst_mmshopcheck> list = Manager.GetListByWhere(Pst_mmshopcheck._.Shop_ID == ObjID);
        if (list.Count > 0)
        {
            Pst_mmshopcheck record = list[0];

            if (record != null)
            {
                txtweight.Text = record.Check_weig.ToString();
                hideObjID.Text = ObjID;
                this.winSave.Hidden = false;
            }
            else
            {
                bindList();
                X.Msg.Alert("提示", "此条记录已经不存在！").Show();
            }
        }
    }
    #endregion
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "车间原料结存导出");
        }
    }

}