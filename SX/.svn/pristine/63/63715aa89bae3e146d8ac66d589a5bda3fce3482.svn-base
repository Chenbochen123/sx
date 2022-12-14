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

public partial class Manager_Equipment_Environmental_EnvironmentalStandards : Mesnac.Web.UI.Page
{
    protected Eqm_LuDaiInfoManager manager = new Eqm_LuDaiInfoManager();
    protected Eqm_lvdaiPlanManager planManager = new Eqm_lvdaiPlanManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            bindList();

            this.winPlanSave.Hidden = true;
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
        sb.AppendLine(@"select T.*,CONVERT(VARCHAR(10),DATEADD (DD,T.cycle*30-useday,GETDATE()),120) Next_date
                        from Eqm_LuDaiInfo T");
        sb.AppendLine("WHERE 1=1 order by serialid desc");
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "环保设施定义导出");
        }
    }
    protected void btnCreatePlan_Click(object sender, EventArgs e)
    {
        if (rowSelectMuti.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "您没有选择任何行,请选择").Show();
            return;
        }
        string objid = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            objid = row.RecordID;
            EntityArrayList<Eqm_LuDaiInfo> listEdit = manager.GetListByWhere(Eqm_LuDaiInfo._.Serialid == row.RecordID);
            if (listEdit.Count == 0)
            {
                X.Msg.Alert("提示", "无此维修标准！").Show();
                return;
            }
            Eqm_LuDaiInfo record = listEdit[0];

            txtMp_plandate.SetValue(DateTime.Now);
            txtMp_memo.Text = record.Memo;
            hidePlanObjID.Text = row.RecordID;
        }
        this.winPlanSave.Hidden = false;
    }


    protected void btnPlanSave_Click(object sender, EventArgs e)
    {
        Eqm_lvdaiPlan record = new Eqm_lvdaiPlan();
        EntityArrayList<Eqm_LuDaiInfo> listEdit = manager.GetListByWhere(Eqm_LuDaiInfo._.Serialid == hidePlanObjID.Text);
        if (listEdit.Count == 0)
        {
            X.Msg.Alert("提示", "无此维修标准！").Show();
            return;
        }
        record.INo = listEdit[0].No;
        record.EquipName = listEdit[0].Equip_name;
        record.Plandate = txtMp_plandate.SelectedDate.Date;
        record.UserName = this.UserID;
        record.Plan_state = 1;
        record.Remark = txtMp_memo.Text;


        if (planManager.Insert(record) >= 0)
        {
            this.AppendWebLog("环保检修计划添加", "添加标准：" + record.INo);
            winPlanSave.Hidden = true;
            bindList();
            X.Msg.Alert("提示", "添加完成！").Show();
        }
        else
        {
            X.Msg.Alert("提示", "添加失败！").Show();
        }
    }
    protected void btnPlanCancel_Click(object sender, EventArgs e)
    {
        winPlanSave.Hidden = true;
        txtMp_memo.Text = "";
        txtMp_plandate.SetValue(null);
    }
    #endregion


    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Delete(string ObjID)
    {
        Eqm_LuDaiInfo record = manager.GetById(int.Parse(ObjID));
        manager.Delete(int.Parse(ObjID));
        this.AppendWebLog("环保设施定义删除", "删除标准：" + record.Serialid.ToString());

        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }
    [DirectMethod]
    public void pnlList_Add(string serialid, string work_Type,
                    string equip_name, string check_content, string Admin_user,
                    string safe_user, string check_user, int user_num,
                    decimal check_hour, decimal cycle, string mater_type,
                    decimal mater_num, DateTime start_date, DateTime replace_date,
                    decimal UseDay, string memo)
    {
        if(Convert.ToInt32(serialid)==0)//新增
        {
            Eqm_LuDaiInfo record = new Eqm_LuDaiInfo();
            DataSet ds = manager.GetBySql("select max(No) No from Eqm_LuDaiInfo").ToDataSet();
            if(ds.Tables.Count>0)
            {
                if(ds.Tables[0].Rows.Count>0)
                {
                    record.No = Convert.ToInt32(ds.Tables[0].Rows[0]["No"].ToString()) + 1;
                }
                else
                {
                    record.No = 1;
                }
            }
            else
            {
                record.No = 1;
            }
            record.Work_Type = work_Type;
            record.Equip_name = equip_name;
            record.Check_content = check_content;
            record.Admin_user = Admin_user;
            record.Safe_user = safe_user;
            record.Check_user = check_user;
            record.User_num = user_num;
            record.Check_hour = check_hour;
            record.Cycle = cycle;
            record.Mater_type = mater_type;
            record.Mater_num = mater_num;
            record.Start_date = start_date;
            record.Replace_date = replace_date;
            record.UseDay = UseDay;
            record.Memo = memo;
            if (manager.Insert(record) >= 0)
            {
                X.Msg.Alert("提示", "添加完成！").Show();
            }
            else
            {
                X.Msg.Alert("提示", "添加失败！").Show();
            }
        }
        else//修改
        {
            Eqm_LuDaiInfo record = manager.GetById(int.Parse(serialid));
            record.Work_Type = work_Type;
            record.Equip_name = equip_name;
            record.Check_content = check_content;
            record.Admin_user = Admin_user;
            record.Safe_user = safe_user;
            record.Check_user = check_user;
            record.User_num = user_num;
            record.Check_hour = check_hour;
            record.Cycle = cycle;
            record.Mater_type = mater_type;
            record.Mater_num = mater_num;
            record.Start_date = start_date;
            record.Replace_date = replace_date;
            record.UseDay = UseDay;
            record.Memo = memo;
            if (manager.Update(record) >= 0)
            {
                X.Msg.Alert("提示", "修改完成！").Show();
            }
            else
            {
                X.Msg.Alert("提示", "修改失败！").Show();
            }
        }
    }
    #endregion
}