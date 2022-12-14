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

public partial class Manager_Equipment_EquipRepairProtectPlan_CleanRecord : Mesnac.Web.UI.Page
{
    protected Eqm_CleanRecordManager manager = new Eqm_CleanRecordManager();
  //  protected Eqm_EquipArchivesManager manager = new Eqm_EquipArchivesManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["serialid"]))
            {
                hidden_type.Text = Request.QueryString["serialid"].ToString();
            }
            bindList();
        }
    }


    #region 初始化控件
    


    #endregion



    private DataSet getList()
    {

        return GetDataByParas();
    }


    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"select * from Eqm_CleanRecord  ");
        sb.AppendLine("WHERE 1=1 ");
        if(!string.IsNullOrEmpty(hidden_type.Text))
        {
            sb.AppendLine("AND serialid='" + hidden_type.Text + "'");
        }
        sb.AppendLine("order by id desc");
        #endregion

        NBear.Data.CustomSqlSection css = manager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }


    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }

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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "设备清理记录导出");
        }
    }

    #endregion


    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Delete(int ObjID)
    {
        Eqm_CleanRecord record = manager.GetById(ObjID);
        manager.Delete(ObjID);
        this.AppendWebLog("记录删除", "删除序号：" + record.Serialid.ToString());

        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }
    [DirectMethod]
    public void pnlList_Add(int id, int serialid, string workshop, string equipname,
                    string Posname, string equipNo, float before_temp, float after_temp, string clean_type, DateTime clean_date, string Clean_fac, string memo)
    {
        if (Convert.ToInt32(id) == 0)//新增
        {
            Eqm_CleanRecord record = new Eqm_CleanRecord();

            record.Id = Convert.ToInt32(GetMaxPlanID());
            record.Serialid = serialid;
            record.Workshop = workshop.ToString();
            record.Equipname = equipname;
            record.Posname = Posname;
            record.EquipNo = equipNo;
            record.Before_temp = Convert.ToDecimal(before_temp);
            record.After_temp = Convert.ToDecimal(after_temp);
            record.Clean_type = clean_type;
            record.Clean_date = clean_date;
            record.Clean_fac = Clean_fac;
            record.Memo = memo;

            if (manager.Insert(record) >= 0)
            {
                X.Msg.Alert("提示", "添加完成！").Show(); bindList();
            }
            else
            {
                X.Msg.Alert("提示", "添加失败！").Show();
            }
        }
        else//修改
        {
            Eqm_CleanRecord record = manager.GetById(id);

            record.Serialid = serialid;
            record.Workshop = workshop.ToString();
            record.Equipname = equipname;
            record.Posname = Posname;
            record.EquipNo = equipNo;
            record.Before_temp = Convert.ToDecimal(before_temp);
            record.After_temp = Convert.ToDecimal(after_temp);
            record.Clean_type = clean_type;
            record.Clean_date = clean_date;
            record.Clean_fac = Clean_fac;
            record.Memo = memo;
            if (manager.Update(record) >= 0)
            {
                X.Msg.Alert("提示", "修改完成！").Show(); bindList();
            }
            else
            {
                X.Msg.Alert("提示", "修改失败！").Show();
            }
        }
    }
    protected string GetMaxPlanID()
    {
        string planID = "";
        EntityArrayList<Eqm_CleanRecord> list = manager.GetAllListOrder(Eqm_CleanRecord._.Id.Desc);
        if (list.Count > 0)
        {
            planID = (list[0].Id + 1).ToString();
        }
        else { planID = "1"; }
        return planID;
    }
    #endregion
}