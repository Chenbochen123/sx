﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NBear.Common;
using Mesnac.Entity;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;

public partial class Manager_ProducingPlan_ShiftSeting_PptSetTime : Mesnac.Web.UI.Page
{
    PptSetTimeManager pptSetTimeManager = new PptSetTimeManager();
    PptProcedureManager pptProcedureManager = new PptProcedureManager();
    IPptShiftManager shiftManager = new PptShiftManager();
    private Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框

    //早中夜班设置变量
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            EntityArrayList<PptProcedure> pptProcedureLists = pptProcedureManager.GetAllList();
            foreach (PptProcedure procedure in pptProcedureLists)
            {
                this.cbo_SetTime_Ppt_DeptType.Items.Add(new Ext.Net.ListItem(procedure.ProcedureName, procedure.ObjID.ToString()));
            }
            FilleTime();
            EntityArrayList<PptShift> shifts=shiftManager.GetAllListOrder(PptShift._.ObjID.Asc);
            this.FieldSet1.Title = shifts[0].ShiftName+"班设置";
            this.chcZao.BoxLabel = "禁用" + shifts[0].ShiftName+"班";
            tfZaoS.FieldLabel = shifts[0].ShiftName;
            this.FieldSet2.Title = shifts[1].ShiftName + "班设置";
            this.chcZhong.BoxLabel = "禁用" + shifts[1].ShiftName + "班";
            tfZhongS.FieldLabel = shifts[1].ShiftName;
            this.FieldSet3.Title = shifts[2].ShiftName + "班设置";
            this.chcYe.BoxLabel = "禁用" + shifts[2].ShiftName + "班";
            tfYeS.FieldLabel = shifts[2].ShiftName;

        }
    }

    // 当天=0,昨天=-1,明天=1
    const string today = "当天";
    const string tomorrow = "明天";
    const string yesterday = "昨天";

     List<string> day = new List<string>(){
        yesterday,today,tomorrow
        };
    private void FilleTime()
    {
        //昨天 =-1 当天=0 明天=1
        
        for (int i = -1; i < day.Count-1; i++)
        {
            cboYe.Items.Add(new Ext.Net.ListItem(day[i+1],i));
            cboZao.Items.Add(new Ext.Net.ListItem(day[i + 1], i));
            cboZhong.Items.Add(new Ext.Net.ListItem(day[i + 1], i));
        }
    }

    #region 工序工作日历窗体事件
    [Ext.Net.DirectMethod()]
    public void InitPptSetTimeWin(object sender, DirectEventArgs e)
    {
        InitSetTime();
    }
    private void InitSetTime()
    {
        WhereClip where = PptSetTime._.ProcedureID == cbo_SetTime_Ppt_DeptType.SelectedItem.Value;
        EntityArrayList<PptSetTime> setlists = pptSetTimeManager.GetListByWhere(where);
        this.tfZaoS.Clear(); this.tfZaoE.Clear();
        this.tfZhongS.Clear(); this.tfZhongE.Clear();
        this.tfYeS.Clear(); this.tfYeE.Clear();
        if (setlists.Count > 0)
        {
            this.tfZaoS.Disabled = false; this.tfZaoE.Disabled = false;
            this.tfZhongS.Disabled = false; this.tfZhongE.Disabled = false;
            this.tfYeS.Disabled = false; this.tfYeE.Disabled = false;
            this.cboZao.Disabled = false; this.cboZhong.Disabled = false;
            this.cboYe.Disabled = false;
            this.chcZao.Checked = false; this.chcZhong.Checked = false;
            this.chcYe.Checked = false;
            for (int i = 0; i < setlists.Count; i++)
            {
                if (setlists[i].UseFlag == 0)
                {
                    switch (Convert.ToInt32(setlists[i].ShiftID))
                    {
                        case 1: this.chcZao.Checked = true; break;
                        case 2: this.chcZhong.Checked = true; break;
                        case 3: this.chcYe.Checked = true; break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (Convert.ToInt32(setlists[i].ShiftID))
                    {
                        case 1:
                            this.tfZaoS.Text = setlists[i].StartTime;
                            this.tfZaoE.Text = setlists[i].StopTime;
                            switch (Convert.ToInt32(setlists[i].DayFlag))
                            {
                                case -1: this.cboZao.Text = yesterday; break;
                                case 0: this.cboZao.Text = today; break;
                                case 1: this.cboZao.Text = tomorrow; break;
                                default: break;
                            }
                            break;
                        case 2:
                            this.tfZhongS.Text = setlists[i].StartTime;
                            this.tfZhongE.Text = setlists[i].StopTime;
                            switch (Convert.ToInt32(setlists[i].DayFlag))
                            {
                                case -1: this.cboZhong.Text = yesterday; break;
                                case 0: this.cboZhong.Text = today; break;
                                case 1: this.cboZhong.Text = tomorrow; break;
                                default: break;
                            }
                            break;
                        case 3:
                            this.tfYeS.Text = setlists[i].StartTime;
                            this.tfYeE.Text = setlists[i].StopTime;
                            switch (Convert.ToInt32(setlists[i].DayFlag))
                            {
                                case -1: this.cboYe.Text = yesterday; break;
                                case 0: this.cboYe.Text = today; break;
                                case 1: this.cboYe.Text = tomorrow; break;
                                default: break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
    /// <summary>
    /// 设置 工序工作日历设置
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnTimeAdd_Click(object sender, DirectEventArgs e)
    {
        try
        {
            if (cbo_SetTime_Ppt_DeptType.SelectedItem.Value == null)
            {
                cbo_SetTime_Ppt_DeptType.SelectOnFocus = true;
                return;
            }
            EntityArrayList<PptShift> shifts = shiftManager.GetAllListOrder(PptShift._.ObjID.Asc);

            if (shifts.Count < 3)
            {

            }
            PptSetTime setTimeZao = new PptSetTime();
            PptSetTime setTimeZhong = new PptSetTime();
            PptSetTime setTimeYe = new PptSetTime();
            setTimeZao.ProcedureID = Convert.ToInt32(cbo_SetTime_Ppt_DeptType.SelectedItem.Value);
            setTimeZhong.ProcedureID = Convert.ToInt32(cbo_SetTime_Ppt_DeptType.SelectedItem.Value);
            setTimeYe.ProcedureID = Convert.ToInt32(cbo_SetTime_Ppt_DeptType.SelectedItem.Value);
            //X.Msg.Notify("", Convert.ToInt32(cbo_SetTime_Ppt_DeptType.SelectedItem.Value)).Show();
            //return;
            if (!Convert.ToBoolean(this.hiddenzao.Text))
            {
                setTimeZao.ShiftID = shifts[0].ObjID;
                setTimeZao.StartTime = this.tfZaoS.Text;
                setTimeZao.StopTime = this.tfZaoE.Text;
                switch (this.cboZao.SelectedItem.Text.ToString())
                {
                    case today: setTimeZao.DayFlag = 0; break;
                    case yesterday: setTimeZao.DayFlag = -1; break;
                    case tomorrow: setTimeZao.DayFlag = 1; break;
                    default:
                        break;
                }
                setTimeZao.UseFlag = 1;
            }
            else
            {
                setTimeZao.ShiftID = shifts[0].ObjID;
                setTimeZao.UseFlag = 0;
                setTimeZao.StartTime = "";
                setTimeZao.StopTime = "";
                setTimeZao.DayFlag = 0;
            }
            pptSetTimeManager.UpdateSetTime(setTimeZao);
            if (!Convert.ToBoolean(hiddenzhong.Text))
            {
                setTimeZhong.ShiftID = shifts[1].ObjID;
                setTimeZhong.StartTime = this.tfZhongS.Text;
                setTimeZhong.StopTime = this.tfZhongE.Text;
                switch (this.cboZhong.SelectedItem.Text)
                {
                    case today: setTimeZhong.DayFlag = 0; break;
                    case yesterday: setTimeZhong.DayFlag = -1; break;
                    case tomorrow: setTimeZhong.DayFlag = 1; break;
                    default:
                        break;
                }
                setTimeZhong.UseFlag = 1;
            }
            else
            {
                setTimeZhong.ShiftID = shifts[1].ObjID;
                setTimeZhong.UseFlag = 0;
                setTimeZhong.StartTime = "";
                setTimeZhong.StopTime = "";
                setTimeZhong.DayFlag = 0;
            }
            pptSetTimeManager.UpdateSetTime(setTimeZhong);
            if (!Convert.ToBoolean(hiddenye.Text))
            {
                setTimeYe.ShiftID = shifts[2].ObjID;
                setTimeYe.StartTime = this.tfYeS.Text;
                setTimeYe.StopTime = this.tfYeE.Text;
                switch (this.cboYe.SelectedItem.Text)
                {
                    case today: setTimeYe.DayFlag = 0; break;
                    case yesterday: setTimeYe.DayFlag = -1; break;
                    case tomorrow: setTimeYe.DayFlag = 1; break;
                    default:
                        break;
                }
                setTimeYe.UseFlag = 1;
            }
            else
            {
                setTimeYe.ShiftID = shifts[2].ObjID;
                setTimeYe.UseFlag = 0;
                setTimeYe.StartTime = "";
                setTimeYe.StopTime = "";
                setTimeYe.DayFlag = 0;
            }
            pptSetTimeManager.UpdateSetTime(setTimeYe);

            msg.Alert("操作", "保存成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败" + ex.Message);
            msg.Show();
        }
    }
    #endregion

}