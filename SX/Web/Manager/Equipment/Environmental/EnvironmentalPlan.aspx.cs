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

public partial class Manager_Equipment_Environmental_EnvironmentalPlan : Mesnac.Web.UI.Page
{
    protected Eqm_lvdaiPlanManager manager = new Eqm_lvdaiPlanManager();
    protected Eqm_LuDaiInfoManager standManager = new Eqm_LuDaiInfoManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {

            bindList();

            this.winSave.Hidden = true;
            this.winComplete.Hidden = true;
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
        sb.AppendLine(@"SELECT T.planid,T.iNo,T1.work_Type,T1.workshopname,T1.equip_name,T1.check_content,T1.Admin_user,
                        T1.cycle,T1.mater_type,T1.mater_num,T.plandate,T.realdate,T1.check_user,T.remark,CASE T.Plan_state WHEN '1' THEN '下达' WHEN '2' THEN '完成' ELSE '取消' END Plan_state
                        FROM Eqm_lvdaiPlan T
                        LEFT JOIN Eqm_LuDaiInfo T1 ON T.iNo = T1.No");
        sb.AppendLine("WHERE 1=1");
        if (dStartDate.SelectedDate!=DateTime.MinValue)
            sb.AppendLine("AND T.plandate>='" + dStartDate.SelectedDate + "'");
        if (dEndDate.SelectedDate != DateTime.MinValue)
            sb.AppendLine("AND T.realdate<='" + dEndDate.SelectedDate + "'");
        if (!string.IsNullOrEmpty(cbxPlanState.Text))
            sb.AppendLine("AND T.Plan_state='" + cbxPlanState.SelectedItem.Value + "'");
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "环保检修计划导出");
        }
    }


    protected void btnCompletePlan_Click(object sender, EventArgs e)
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
            EntityArrayList<Eqm_lvdaiPlan> listEdit = manager.GetListByWhere(Eqm_lvdaiPlan._.Planid == row.RecordID);
            if (listEdit.Count == 0)
            {
                X.Msg.Alert("提示", "无此检修计划，修改失败！").Show();
                return;
            }
            Eqm_lvdaiPlan record = listEdit[0];
            if(record.Plan_state == 2)
            {
                X.Msg.Alert("提示", "此计划已完成，无法重复完成！").Show();
                return;
            }
            else if (record.Plan_state == 3)
            {
                X.Msg.Alert("提示", "此计划已取消，无法完成！").Show();
                return;
            }
            else
            {
                if(record.Realdate!=null)
                {
                    txtMp_plandateCom.SelectedDate = Convert.ToDateTime(record.Realdate);
                }
                txtMp_memoCom.Text = record.Remark;

                hideObjIDComplete.Text = objid;

                this.winComplete.Hidden = false;
            }

        }
        //return "true";
    }


    protected void btnComplete_Click(object sender, EventArgs e)
    {
        EntityArrayList<Eqm_lvdaiPlan> listEdit = manager.GetListByWhere(Eqm_lvdaiPlan._.Planid == hideObjIDComplete.Text);
        if (listEdit.Count == 0)
        {
            X.Msg.Alert("提示", "无此检修计划，修改失败！").Show();
            return;
        }
        Eqm_lvdaiPlan record = listEdit[0];
        if (record != null)
        {
            record.Realdate = txtMp_plandateCom.SelectedDate;
            record.Remark = txtMp_memoCom.Text;
            record.Plan_state = 2;
            if (listEdit[0].INo != null)
            {
                EntityArrayList<Eqm_LuDaiInfo> list = standManager.GetListByWhere(Eqm_LuDaiInfo._.No == listEdit[0].INo);
                if (list.Count > 0)
                {
                    if (list[0].Replace_date < txtMp_plandateCom.SelectedDate)
                    {
                        list[0].Replace_date = txtMp_plandateCom.SelectedDate;
                        list[0].UseDay = 0;
                        standManager.Update(list[0]);
                    }
                }
            }

            if (manager.Update(record) >= 0)
            {
                this.AppendWebLog("环保检修计划完成", "完成编号：" + record.Planid);
                this.winComplete.Hidden = true;
                bindList();
                X.Msg.Alert("提示", "完成成功！").Show();
            }
            else
            {
                X.Msg.Alert("提示", "完成失败！").Show();
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.hideMode.Text == "Edit")//修改
        {
            EntityArrayList<Eqm_lvdaiPlan> listEdit = manager.GetListByWhere(Eqm_lvdaiPlan._.Planid == hideObjID.Text);
            if (listEdit.Count == 0)
            {
                X.Msg.Alert("提示", "无此检修计划，修改失败！").Show();
                return;
            }
            Eqm_lvdaiPlan record = listEdit[0];
            if (record != null)
            {
                record.Plandate = txtMp_plandate.SelectedDate;
                record.Remark = txtMp_memo.Text;

                if (manager.Update(record) >= 0)
                {
                    this.AppendWebLog("环保检修计划修改", "修改编号：" + record.Planid);
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
    protected void btnCancelComplete_Click(object sender, EventArgs e)
    {
        winComplete.Hidden = true;
    }
    #endregion


    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Edit(string ObjID)
    {
        //hidden_type.Text = "2";
        EntityArrayList<Eqm_lvdaiPlan> list = manager.GetListByWhere(Eqm_lvdaiPlan._.Planid == ObjID);
        if(list.Count==0)
        {
            X.Msg.Alert("提示", "此条记录已经不存在！").Show();
            return;
        }
        Eqm_lvdaiPlan record = list[0];


        if (record != null)
        {
            if (record.Plan_state == 2)
            {
                X.Msg.Alert("提示", "此计划已完成，无法修改！").Show();
                return;
            }
            else if (record.Plan_state == 3)
            {
                X.Msg.Alert("提示", "此计划已取消，无法修改！").Show();
                return;
            }
            txtMp_plandate.SelectedDate = Convert.ToDateTime(record.Plandate);
            txtMp_memo.Text = record.Remark;

            hideObjID.Text = ObjID;
            this.hideMode.Text = "Edit";
            ////cbxEquipClass.DirectChange += this.cbxEquipClass_SelectChanged;
            ////cbxEquipClass.DirectEvents.Change.HandlerIsNotEmpty = this.cbxEquipClass_SelectChanged;
            //cbxEquipClass.DirectSelect += this.cbxEquipClass_SelectChanged;
            ////cbxEquipClass.DirectChange += new System.EventHandler(this.cbxEquipClass_SelectChanged);
            this.winSave.Hidden = false;
        }
        else
        {
            bindList();
            X.Msg.Alert("提示", "此条记录已经不存在！").Show();
        }

    }
    [DirectMethod]
    public void pnlList_Delete(string ObjID)
    {
        EntityArrayList<Eqm_lvdaiPlan> list = manager.GetListByWhere(Eqm_lvdaiPlan._.Planid == ObjID);
        if (list.Count == 0)
        {
            X.Msg.Alert("提示", "此条记录已经不存在！").Show();
            return;
        }
        Eqm_lvdaiPlan record = list[0];

        if (record.Plan_state == 2)
        {
            X.Msg.Alert("提示", "此计划已完成，无法删除！").Show();
            return;
        }
        manager.DeleteByWhere(Eqm_lvdaiPlan._.Planid == ObjID);
        this.AppendWebLog("环保检修计划删除", "删除编号：" + record.Planid);

        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }

    #endregion
}