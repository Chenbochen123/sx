﻿using System;
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

public partial class Manager_Equipment_EquipRepairProtectPlan_CleanStand : Mesnac.Web.UI.Page
{
    protected Eqm_CleanStandManager manager = new Eqm_CleanStandManager();
    protected Eqm_CleanRecordManager recordmanager = new Eqm_CleanRecordManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            winPlanSave.Hidden = true;
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
        sb.AppendLine(@"select serialid,workshop,equipName,posname,equipno,cycle,before_temp,after_temp,Clean_type,begin_date,last_date,Next_date,memo from Eqm_CleanStand");
        sb.AppendLine("WHERE 1=1 ");
        //if (!string.IsNullOrEmpty(cbxType.Text))
        //{
        //    sb.AppendLine("AND Etype='" + cbxType.Value + "'");
        //}
        //if (!string.IsNullOrEmpty(txtmainid.Text))
        //{
        //    sb.AppendLine("AND mainid='" + txtmainid.Text + "'");
        //}
        if(!string.IsNullOrEmpty(hidden_type.Text))
        {
            sb.AppendLine("AND serialid='" + hidden_type.Text + "'");
        }
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "设备清洗标准信息导出");
        }
    }

    #endregion


    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Delete(string ObjID)
    {
        Eqm_CleanStand record = manager.GetById(int.Parse(ObjID));
        manager.Delete(int.Parse(ObjID));
        this.AppendWebLog("记录删除", "删除序号：" + record.Serialid.ToString());

        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }
    [DirectMethod]
    public void pnlList_Add(int serialid, string workshop, string equipName, string posname, string equipno, int cycle, float before_temp, float after_temp,
        string Clean_type, DateTime begin_date, DateTime last_date, DateTime Next_date, string memo)
    {
        if (Convert.ToInt32(serialid) == 0)//新增
        {
            Eqm_CleanStand record = new Eqm_CleanStand();

            record.Serialid = Convert.ToInt32(GetMaxPlanID2());
            record.Workshop = workshop;
            record.EquipName = equipName;
            record.Posname = posname;
            record.Equipno = equipno;
            record.Cycle = cycle;
            record.Before_temp = Convert.ToDecimal(before_temp);
            record.After_temp =Convert.ToDecimal(after_temp);
            record.Clean_type = Clean_type;
            record.Begin_date = begin_date;
            record.Last_date = last_date;
            record.Next_date = last_date.AddDays(cycle);
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
            Eqm_CleanStand record = manager.GetById(serialid);

            record.Workshop = workshop;
            record.EquipName = equipName;
            record.Posname = posname;
            record.Equipno = equipno;
            record.Cycle = cycle;
            record.Before_temp = Convert.ToDecimal(before_temp);
            record.After_temp = Convert.ToDecimal(after_temp);
            record.Clean_type = Clean_type;
            record.Begin_date = begin_date;
            record.Last_date = last_date;
            record.Next_date = last_date.AddDays(cycle);
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
    #endregion

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

            EntityArrayList<Eqm_CleanStand> listEdit = manager.GetListByWhere(Eqm_CleanStand._.Serialid == row.RecordID);
            if (listEdit.Count == 0)
            {
                X.Msg.Alert("提示", "无此维修标准！").Show();
                return;
            }
            txtno.Text = listEdit[0].Serialid.ToString();
            txtworkshop.Text = listEdit[0].Workshop;
            txtequip.Text = listEdit[0].EquipName;
            txtequipno.Text = listEdit[0].Equipno;
            txtposname.Text = listEdit[0].Posname;
            txtbefore.Text = listEdit[0].Before_temp.ToString();
            txtafter.Text = listEdit[0].After_temp.ToString();
            txtfangshi.Text = listEdit[0].Clean_type;

            datetime.Text = DateTime.Now.ToString();
            txtmemo.Text = listEdit[0].Memo;
            hidePlanObjID.Text = row.RecordID;
        }
        this.winPlanSave.Hidden = false;
    }

    protected void btnPlanSave_Click(object sender, EventArgs e)
    {
        Eqm_CleanRecord record = new Eqm_CleanRecord();
        if (record != null)
        {
            record.Id = Convert.ToInt32(GetMaxPlanID());
            record.Serialid = Convert.ToInt32(txtno.Text);
            record.Workshop = txtworkshop.Text;
            record.Equipname = txtequip.Text;
            record.EquipNo = txtequipno.Text;
            record.Posname = txtposname.Text;
            record.Before_temp = Convert.ToDecimal(txtbefore.Text);
            record.After_temp = Convert.ToDecimal(txtafter.Text);
            
            record.Clean_type = txtfangshi.Text;
            record.Clean_fac = txtchangjia.Text;
            record.Clean_date = Convert.ToDateTime(datetime.Text);
            record.Memo = txtmemo.Text;

            //在维护清洗记录页面，修改记录表的同时，修改设备表
            EntityArrayList<Eqm_CleanStand> listEdit = manager.GetListByWhere(Eqm_CleanStand._.Serialid == hidePlanObjID.Text);
            if (listEdit.Count == 0)
            {
                X.Msg.Alert("提示", "无此设备，请核实！").Show();
                return;
            }
            listEdit[0].Before_temp = Convert.ToDecimal(txtbefore.Text);
            listEdit[0].After_temp = Convert.ToDecimal(txtafter.Text);
            listEdit[0].Clean_type = txtfangshi.Text;
            listEdit[0].Last_date = datetime.SelectedDate;
            listEdit[0].Next_date = datetime.SelectedDate.AddDays(Convert.ToInt32(listEdit[0].Cycle));
           

            manager.Update(listEdit[0]);

            if (recordmanager.Insert(record) >= 0)
            {
                this.AppendWebLog("设备维修记录添加完成", "完成编号：" + record.Serialid);
                this.winPlanSave.Hidden = true;
                bindList();
                X.Msg.Alert("提示", "完成成功！").Show();
            }
            else
            {
                X.Msg.Alert("提示", "完成失败！").Show();
            }
        }
    }

    protected void btnPlanCancel_Click(object sender, EventArgs e)
    {
        winPlanSave.Hidden = true;
    }

    //自动生成主键（非自增）
    protected string GetMaxPlanID()
    {
        string planID = "";
        EntityArrayList<Eqm_CleanRecord> list = recordmanager.GetAllListOrder(Eqm_CleanRecord._.Id.Desc);
        if (list.Count > 0)
        {
            planID = (list[0].Id+1).ToString();
        }
        else { planID = "1"; }
        return planID;
    }

    protected string GetMaxPlanID2()
    {
        string planID = "";
        EntityArrayList<Eqm_CleanStand> list = manager.GetAllListOrder(Eqm_CleanStand._.Serialid.Desc);
        if (list.Count > 0)
        {
            planID = (list[0].Serialid + 1).ToString();
        }
        else { planID = "1"; }
        return planID;
    }
}