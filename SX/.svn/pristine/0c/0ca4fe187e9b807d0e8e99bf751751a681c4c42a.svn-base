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


public partial class Manager_ProducingPlan_YiChang : Mesnac.Web.UI.Page
{
    protected DZKB_YiChangManager Manager = new DZKB_YiChangManager();
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
        txtdatestart.Text = DateTime.Now.ToString("yyyy-MM-dd");
        txtdateend.Text = DateTime.Now.ToString("yyyy-MM-dd");
        txtdate1.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
        sb.AppendLine(@"select a.*,b.ShiftName
from  DZKB_YiChang a left join PptShift b on b.ObjID=a.shift_id ");
        sb.AppendLine("WHERE 1=1");
        if (!string.IsNullOrEmpty(txtdatestart.SelectedDate.ToString("yyyy-MM-dd")))
        {
            sb.AppendLine("AND plan_date>='" + txtdatestart.SelectedDate.ToString("yyyy-MM-dd") + "'");
            sb.AppendLine("AND plan_date<='" + txtdateend.SelectedDate.ToString("yyyy-MM-dd") + "'");
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.hideMode.Text = "Add";
        txtremark.Text = "";
        cbxShift.Text = "";
        this.winSave.Hidden = false;
    }
    //添加计划确定
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.hideMode.Text == "Add")//添加
        {
            EntityArrayList<DZKB_YiChang> listAdd = Manager.GetListByWhere(DZKB_YiChang._.Plan_date==txtdate1.SelectedDate.ToString("yyyy-MM-dd")&&DZKB_YiChang._.Shift_id==cbxShift.SelectedItem.Value);
            if (listAdd.Count > 0)
            { X.Msg.Alert("提示", "已有该记录，不允许重复！").Show(); return; }
            DZKB_YiChang record = new DZKB_YiChang();
            record.Plan_date = txtdate1.SelectedDate.ToString("yyyy-MM-dd");
            record.Shift_id = Convert.ToInt32(cbxShift.Value);
            record.Remark = txtremark.Text;

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
            EntityArrayList<DZKB_YiChang> listAdd = Manager.GetListByWhere(DZKB_YiChang._.Plan_date == txtdate1.SelectedDate.ToString("yyyy-MM-dd") && DZKB_YiChang._.Shift_id == cbxShift.Value);
            DZKB_YiChang record = listAdd[0];

            if (record != null)
            {
                record.Remark = txtremark.Text;
                if (Manager.Update(record) >= 0)
                {
                    winSave.Hidden = true;
                    txtdate1.ReadOnly = false;
                    cbxShift.ReadOnly = false;
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

    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Edit(string plan_date , string shift_id)
    {
        EntityArrayList<DZKB_YiChang> listAdd = Manager.GetListByWhere(DZKB_YiChang._.Plan_date == plan_date && DZKB_YiChang._.Shift_id == shift_id);
        if (listAdd.Count > 0)
        {
            DZKB_YiChang record = listAdd[0];

            if (record != null)
            {
                txtdate1.Text = record.Plan_date;
                txtremark.Text = record.Remark;
                if (record.Shift_id == 1)
                {
                    cbxShift.Text = "白";
                    cbxShift.Value = "1";
                }
                else
                {
                    cbxShift.Text = "夜";
                    cbxShift.Value = "3";
                }
               
                this.hideMode.Text = "Edit";
                txtdate1.ReadOnly = true;
                cbxShift.ReadOnly = true;
                this.winSave.Hidden = false;
            }
            else
            {
                bindList();
                X.Msg.Alert("提示", "此条记录已经不存在！").Show();
            }
        }
    }
    [DirectMethod]
    public void pnlList_Delete(string plan_date,string shift_id)
    {
       try
        {
            string sql = "delete from DZKB_YiChang where plan_date='" + plan_date + "' and shift_id = '" + shift_id + "'";
            Manager.GetBySql(sql.ToString()).ToDataSet();
            bindList();
            X.Msg.Alert("提示", "删除成功！").Show();
        }
        catch
        {
            X.Msg.Alert("提示", "该记录已经不存在！").Show();
            return;
        }
    }


    #endregion
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = getList();
        DataTable data = ds.Tables[0];
        for (int i = 0; i < data.Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = data.Columns[i];
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
                data.Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "异常原因录入");
    }

}