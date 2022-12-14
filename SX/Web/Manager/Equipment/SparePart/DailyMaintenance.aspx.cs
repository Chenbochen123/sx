﻿using System;
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


public partial class Manager_Equipment_SparePart_DailyMaintenance : Mesnac.Web.UI.Page
{
    protected Eqm_MainDailyManager DailyManager = new Eqm_MainDailyManager();
    protected SYS_USERManager userManager = new SYS_USERManager();
    protected PptShiftManager shiftManager = new PptShiftManager();
    protected JCZL_WorkShopManager WorkManager = new JCZL_WorkShopManager();
    protected JCZL_partsManager pmpartsManager = new JCZL_partsManager();
    protected BasEquipManager BasEquipManager = new BasEquipManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !X.IsAjaxRequest&&!Page.IsPostBack )
        {
            PageInit();
        }
    }

    #region 初始化下拉列表
    private void PageInit()
    {
        //bindFaultType();
        DateBeginTime.SelectedDate = DateTime.Now.AddDays(-7);
        DateEndTime.SelectedDate = DateTime.Now;
        this.winSave.Hidden = true;
       // bindMainDaily();
       // bindMainStand();
        bindBasEquip();
        bindPpt_pmparts();
        bindWorkShop();
        bindShift();
        bindList();
    }
    //机台
    private void bindBasEquip()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<BasEquip> list = BasEquipManager.GetListByWhere(where);
        foreach (BasEquip main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.EquipName, main.EquipCode);
            cbxequipname.Items.Add(item);
            cbxjitai.Items.Add(item);
        }
    }
    //部件
    private void bindPpt_pmparts()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<JCZL_parts> list = pmpartsManager.GetListByWhere(where);
        foreach (JCZL_parts main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.Mp_name, main.Mp_code);
            cbxequipname.Items.Add(item);
            cbxbujian.Items.Add(item);
        }
    }
    //班次
    private void bindShift()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<PptShift> list = shiftManager.GetListByWhere(where);
        foreach (PptShift main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.ShiftName, main.ObjID);
            cbxshiftname.Items.Add(item);
            cbxshift.Items.Add(item);
        }
    }
    //车间
    private void bindWorkShop()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<JCZL_WorkShop> list = WorkManager.GetListByWhere(where);
        foreach (JCZL_WorkShop main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.WorkShop_Name, main.WorkShop_Code);
            cbxworkshop.Items.Add(item);
        }
    }
    private DataSet getList(string cbxmp_type)
    {


        return GetDataByParas(cbxmp_type);
    }

    public System.Data.DataSet GetDataByParas(string cbxmp_type)
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"select T.MainDaily_ID,T.Mp_Date,T1.ShiftName,T2.WorkShop_Name,T3.Equip_name,T4.Mp_name,T.MP_Type,T.Mp_StartTime,T.MP_EndTime,T.Mp_time,T.handle_name,T.MP_cuoshi,T.Mp_reason,T.Mp_result,
T.mem_BJ,T5.real_name,T.In_Date,CASE WHEN T.isBaiBan=0 THEN '否' WHEN T.isBaiBan=1 THEN '是' ELSE NULL end isBaiBan,T.ConfirmUser,T.Remark,T.YuSuanFY,T.ShiJiFY

from Eqm_MainDaily T
LEFT JOIN PptShift T1 ON T.shift_id=T1.ObjID
LEFT JOIN  Pmt_equip T3 ON T3.Equip_code=T.Equip_code
LEFT JOIN JCZL_WorkShop T2 ON  t3.WorkShop_Code=t2.WorkShop_Code
LEFT JOIN JCZL_parts T4 ON T4.Mp_code=T.mp_Code 
LEFT JOIN SYS_USER T5 ON T5.USER_ID=T.Worker_barcode");
        sb.AppendLine("WHERE 1=1");

        if (!string.IsNullOrEmpty(DateBeginTime.SelectedDate.ToString()))
            sb.AppendLine("AND T.Mp_Date>='" + DateBeginTime.SelectedDate.ToString("yyyy-MM-dd") + "'");
        if (!string.IsNullOrEmpty(DateEndTime.SelectedDate.ToString()))
            sb.AppendLine("AND T.Mp_Date<='" + DateEndTime.SelectedDate.ToString("yyyy-MM-dd") + "'");
        if (!string.IsNullOrEmpty(cbxmp_type))
            sb.AppendLine("AND T.MP_Type='" + cbxmp_type + "'");
        //if (!string.IsNullOrEmpty(cbxworkshop.Text))
        //    sb.AppendLine("AND T2.workshop='" + cbxworkshop.Text + "'");
        if (!string.IsNullOrEmpty(cbxequipname.Text))
            sb.AppendLine("AND T3.Equip_code='" + cbxequipname.Value + "'");
        if (!string.IsNullOrEmpty(cbxshiftname.Text))
            sb.AppendLine("AND T1.ObjID='" + cbxshiftname.Value + "'");
        if (!string.IsNullOrEmpty(cbxworkshop.Text))
            sb.AppendLine("AND T2.WorkShop_Code='" + cbxworkshop.Value + "'");
      
        #endregion

        NBear.Data.CustomSqlSection css = DailyManager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }
    private void bindList()
    {
        string plan = "";
        if (cbxmp_type.SelectedItem.Value != null)
        {
            plan = cbxmp_type.SelectedItem.Value.ToString();
        }
        this.store.DataSource = getList(plan);
        this.store.DataBind();
    }
    #endregion



    #region 按钮事件响应
    protected void btnSearch_Click( object sender , EventArgs e )
    {
        bindList();
    }
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        string plan = "";
        if (cbxmp_type.SelectedItem.Value != null)
        {
            plan = cbxmp_type.SelectedItem.Value.ToString();
        }
        DataSet ds = getList(plan);
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "日常检修记录");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.hideMode.Text = "Add";

        Dateweixiu.SetValue(DateTime.Now);
        cbxleixing.Text = "";
        cbxjitai.Text = "";
        cbxshift.Text = "";
        cbxbujian.Text = "";
        cbxchangbaiban.Text = "";
        DateStart.SetValue(DateTime.Now);
        DateStartTime.SetValue(DateTime.Now.TimeOfDay);
        DateEnd1.SetValue(DateTime.Now);
        DateEndTime1.SetValue(DateTime.Now.TimeOfDay);
        txtuser.Text = "";
        cbxfinish.Text = "";
        txtguzhang.Text = "";
        txtcuoshi.Text = "";
        txtjieguo.Text = "";
        txtshiyong.Text = "";
        txtremark.Text = "";
        txtyusuan.Text = "0";
        txtshiji.Text = "0";
        this.winSave.Hidden = false;
    }

    //自动生成计划号
    protected string GetMaxPlanID()
    {
        string planID = "";
        EntityArrayList<Eqm_MainDaily> list = DailyManager.GetAllListOrder(Eqm_MainDaily._.MainDaily_ID.Desc);
        if (list.Count > 0)
        {
            planID = list[0].MainDaily_ID;
            if (planID.Substring(0, 8) == DateTime.Now.Date.ToString("yyyyMMdd"))
            {
                planID = planID.Substring(0, 8) + (Convert.ToInt32(planID.Substring(8, 2)) + 1).ToString().PadLeft(2, '0');
            }
            else
            {
                planID = DateTime.Now.Date.ToString("yyyyMMdd") + "01";
            }
        }
        return planID;
    }

    protected void btnSave_Click( object sender , EventArgs e )
    {
        if(this.hideMode.Text == "Add")//添加
        {
            Eqm_MainDaily record = new Eqm_MainDaily();
            record.MainDaily_ID = GetMaxPlanID();
            record.Mp_Date = Dateweixiu.SelectedDate.ToString("yyyy-MM-dd");
            record.MP_Type = cbxleixing.Value.ToString();
            record.Equip_code = cbxjitai.SelectedItem.Value;
            record.Shift_id = Convert.ToInt16(cbxshift.SelectedItem.Value);
            record.Mp_Code = cbxbujian.SelectedItem.Value;
            TimeSpan ts = DateEnd1.SelectedDate + DateEndTime1.SelectedTime - (DateStart.SelectedDate + DateStartTime.SelectedTime);
            record.Mp_time = ts.Days * 24 + ts.Hours + ts.Minutes / 60;
            record.IsBaiBan = Convert.ToInt16(cbxchangbaiban.Value);
            record.Mp_StartTime = DateStart.SelectedDate + DateStartTime.SelectedTime;
            record.MP_EndTime = DateEnd1.SelectedDate + DateEndTime1.SelectedTime;
            if (record.Mp_StartTime > record.MP_EndTime)
            {
                X.Msg.Alert("提示", "开始时间不能大于结束时间！").Show(); return;
            }
            record.Handle_name = txtuser.Text;
            record.Finish_flag = Convert.ToInt16(cbxfinish.SelectedItem.Value);
            record.Mp_reason = txtguzhang.Text;
            record.MP_cuoshi = txtcuoshi.Text;
            record.Mp_result = txtjieguo.Text;
           // record.Mem_note = txtMemNote.SelectedDate.ToString("yyyy-MM-dd");
            record.Mem_BJ = txtshiyong.Text;
            record.Remark = txtremark.Text;
            record.YuSuanFY = Convert.ToDecimal(txtyusuan.Text);
            record.ShiJiFY = Convert.ToDecimal(txtshiji.Text);
            record.Worker_barcode = this.UserID;
            record.In_Date = DateTime.Now.ToString("yyyy-MM-dd");


            if (DailyManager.Insert(record) >= 0)
            {
                this.AppendWebLog("日常检修记录添加", "添加记录号：" + record.MainDaily_ID);
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
            Eqm_MainDaily record = DailyManager.GetById(int.Parse(hideObjID.Text));
            if (record != null)
            {
                record.Mp_Date = Dateweixiu.SelectedDate.ToString("yyyy-MM-dd");
                record.MP_Type = cbxleixing.Value.ToString();
                record.Equip_code = cbxjitai.SelectedItem.Value;
                record.Shift_id = Convert.ToInt16(cbxshift.SelectedItem.Value);
                record.Mp_Code = cbxbujian.SelectedItem.Value;
                TimeSpan ts = DateEnd1.SelectedDate + DateEndTime1.SelectedTime - (DateStart.SelectedDate + DateStartTime.SelectedTime);
                record.Mp_time = ts.Days * 24 + ts.Hours + ts.Minutes/60;
                record.IsBaiBan = Convert.ToInt16(cbxchangbaiban.Value);
                record.Mp_StartTime = DateStart.SelectedDate + DateStartTime.SelectedTime;
                record.MP_EndTime = DateEnd1.SelectedDate + DateEndTime1.SelectedTime;
                if (record.Mp_StartTime > record.MP_EndTime)
                {
                    X.Msg.Alert("提示", "开始时间不能大于结束时间！").Show(); return;
                }
                record.Handle_name = txtuser.Text;
                record.Finish_flag = Convert.ToInt16(cbxfinish.SelectedItem.Value);
                record.Mp_reason = txtguzhang.Text;
                record.MP_cuoshi = txtcuoshi.Text;
                record.Mp_result = txtjieguo.Text;
                // record.Mem_note = txtMemNote.SelectedDate.ToString("yyyy-MM-dd");
                record.Mem_BJ = txtshiyong.Text;
                record.Remark = txtremark.Text;
                record.YuSuanFY = Convert.ToDecimal(txtyusuan.Text);
                record.ShiJiFY = Convert.ToDecimal(txtshiji.Text);


                if (DailyManager.Update(record) >= 0)
                {
                    this.AppendWebLog("日常检修记录修改", "修改记录号：" + record.MainDaily_ID);
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

    protected void btnCancel_Click( object sender , EventArgs e )
    {
        winSave.Hidden = true;
    }
    #endregion

    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Edit( string ObjID )
    {
        Eqm_MainDaily record = DailyManager.GetById(ObjID);

        

        if ( record != null )
        {

            Dateweixiu.SetValue(record.Mp_Date);
            cbxleixing.SetValue(record.MP_Type);
            cbxjitai.SetValue(record.Equip_code);
            cbxshift.SetValue(record.Shift_id);
            cbxbujian.SetValue(record.Mp_Code);
            if (record.IsBaiBan == 1)
            {
                cbxchangbaiban.Text = "是";
                cbxchangbaiban.Value = "1";
            }
            else  if (record.IsBaiBan == 0)
            {
                cbxchangbaiban.Text = "否";
                cbxchangbaiban.Value = "0";
            }
           // cbxchangbaiban.SetValue(record.IsBaiBan);
            DateStart.SetValue(record.Mp_StartTime.Value.Date);
            DateStartTime.SetValue(record.Mp_StartTime.Value.TimeOfDay);
            DateEnd1.SetValue(record.MP_EndTime.Value.Date);
            DateEndTime1.SetValue(record.MP_EndTime.Value.TimeOfDay);
            txtuser.SetValue(record.Handle_name);
            if (record.Finish_flag == 1)
            {
                cbxfinish.Text = "是";
                cbxfinish.Value = "1";
            }
            else if (record.Finish_flag == 0)
            {
                cbxfinish.Text = "否";
                cbxfinish.Value = "0";
            }
           // cbxfinish.SetValue(record.Finish_flag);
            txtguzhang.SetValue(record.Mp_reason);
            txtcuoshi.SetValue(record.MP_cuoshi);
            txtjieguo.SetValue(record.Mp_result);
            txtshiyong.SetValue(record.Mem_BJ);
            txtremark.SetValue(record.Remark);
            txtyusuan.SetValue(record.YuSuanFY);
            txtshiji.SetValue(record.ShiJiFY);

            hideObjID.Text = ObjID;
            this.hideMode.Text = "Edit";

            this.winSave.Hidden = false;
        }
        else
        {
            bindList();
            X.Msg.Alert( "提示" , "此条记录已经不存在！" ).Show();
        }
    }

    //组长确认
    protected void btnConfirm_Click(object sender, EventArgs e)
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
            EntityArrayList<Eqm_MainDaily> listEdit = DailyManager.GetListByWhere(Eqm_MainDaily._.MainDaily_ID == row.RecordID);
            if (listEdit.Count == 0)
            {
                X.Msg.Alert("提示", "无此维修记录，确认失败！").Show();
                return;
            }
            Eqm_MainDaily record = listEdit[0];
            if (!string.IsNullOrEmpty(record.ConfirmUser))
            { X.Msg.Alert("提示", "该计划已确认，不允许重复操作！").Show(); return; }

            EntityArrayList<SYS_USER> listEdit2 = userManager.GetListByWhere(SYS_USER._.USER_ID == this.UserID);
            if (listEdit2.Count == 0)
            {
                X.Msg.Alert("提示", "无此确认人，确认失败！").Show();
                return;
            }
            record.ConfirmUser = listEdit2[0].Real_name;

            if (DailyManager.Update(record) >= 0)
            {
                bindList();
                X.Msg.Alert("提示", "确认完成！").Show();
            }
            else
            {
                X.Msg.Alert("提示", "确认失败！").Show();
            }
            hideObjID.Text = objid;
        }
    }

    [DirectMethod]
    public void pnlList_Delete(string ObjID)
    {
        Eqm_MainDaily record = DailyManager.GetById(ObjID);
        DailyManager.Delete(ObjID);
        this.AppendWebLog("日常检修记录删除", "修改记录号：" + record.MainDaily_ID);
          
        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }

    #endregion

    
}