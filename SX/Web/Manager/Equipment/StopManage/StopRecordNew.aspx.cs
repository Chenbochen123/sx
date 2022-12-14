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


public partial class Manager_Equipment_StopManage_StopRecordNew : Mesnac.Web.UI.Page
{
    protected SysCodeManager sysCodeManager = new SysCodeManager();
    protected Ppt_SetTimeManager shiftManager = new Ppt_SetTimeManager();
    protected Ppt_ShiftClassManager classManager = new Ppt_ShiftClassManager();
    protected JCZL_WorkShopManager shopManager = new JCZL_WorkShopManager();
    protected Pmt_equipManager equipManager = new Pmt_equipManager();
    protected Eqm_downikindManager typeManager = new Eqm_downikindManager();
    protected Eqm_MpParamManager typeMainManager = new Eqm_MpParamManager();
    protected Eqm_downcodeManager reasonManager = new Eqm_downcodeManager();
    protected EqmMaintainRecordManager maintainManager = new EqmMaintainRecordManager();
    protected Ppt_pmdownrecordManager manager = new Ppt_pmdownrecordManager();
    protected SYS_USERManager userManager = new SYS_USERManager();

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
        this.dStartDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        this.dEndDate.Text = DateTime.Today.AddDays(1).ToString( "yyyy-MM-dd" );
        this.dStartTime.Text = "0:00";
        this.dEndTime.Text = "0:00";

        bindList();
        bindWorkShop();
        bindEquip();
        bindMainType();
        bindClass();
        bindShift();
        bindMaintainers();
        bindFengxian();
        bindStopKind();
        bindStopType();
        bindMp_code();
        //bindFaultType();

        this.winSave.Hidden = true;
    }
    private void bindWorkShop()
    {
        //WhereClip where = new WhereClip();
        //EntityArrayList<JCZL_WorkShop> list = shopManager.GetListByWhere(where);
        //this.storeWorkShop.DataSource = list;
        //this.storeWorkShop.DataBind();
    }
    private void bindEquip()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<Pmt_equip> list = equipManager.GetListByWhereAndOrder(where, Pmt_equip._.Equip_name.Asc);
        this.storeEquip.DataSource = list;
        this.storeEquip.DataBind();
        foreach (Pmt_equip equip in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(equip.Equip_name, equip.Equip_code);
            cbxEquip.Items.Add(item);
        }
    }
    private void bindStopKind()
    {
        Ext.Net.ListItem item = new Ext.Net.ListItem("A", "A");
        Ext.Net.ListItem item2 = new Ext.Net.ListItem("B", "B");
        Ext.Net.ListItem item3 = new Ext.Net.ListItem("C", "C");
        cbStopKind.Items.Add(item);
        cbStopKind.Items.Add(item2);
        cbStopKind.Items.Add(item3);
    }
    private void bindMainType()
    {
        EntityArrayList<Eqm_MpParam> list = typeMainManager.GetListByWhereAndOrder(Eqm_MpParam._.Param_Type == "4", Eqm_MpParam._.Param_id.Asc);
        this.storeStopMainType.DataSource = list;
        this.storeStopMainType.DataBind();
    }
    private void bindStopType()
    {
        //this.cbStopType.ClearValue();
        //this.cbMp_ikindcode.ClearValue();
        EntityArrayList<Eqm_downikind> list = typeManager.GetAllList();
        this.storeStopType.DataSource = list;
        this.storeStopType.DataBind();
        this.storeMp_ikindcode.DataSource = list;
        this.storeMp_ikindcode.DataBind();
    }


    private void changeStopType()
    {
        //this.cbMp_ikindcode.ClearValue();
        EntityArrayList<Eqm_downikind> list = typeManager.GetListByWhereAndOrder(Eqm_downikind._.Mp_mkindcode == cbStopMainType.SelectedItem.Value, Eqm_downikind._.Mp_ikindname.Asc);
        this.storeMp_ikindcode.DataSource = list;
        this.storeMp_ikindcode.DataBind();
    }

    private void bindMp_code()
    {
        //this.cbStopReason.ClearValue();
        //this.cbMp_code.ClearValue();
        EntityArrayList<Eqm_downcode> list = reasonManager.GetAllList();
        this.storeStopReason.DataSource = list;
        this.storeStopReason.DataBind();
        this.storeMp_code.DataSource = list;
        this.storeMp_code.DataBind();
    }


    private void changeMp_code()
    {
        //this.cbMp_code.ClearValue();
        EntityArrayList<Eqm_downcode> list = reasonManager.GetListByWhereAndOrder(Eqm_downcode._.Mp_ikindcode == cbMp_ikindcode.SelectedItem.Value, Eqm_downcode._.Mp_name.Asc);
        this.storeMp_code.DataSource = list;
        this.storeMp_code.DataBind();
    }

    private void bindShift()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<Ppt_SetTime> list = shiftManager.GetListByWhere(Ppt_SetTime._.DeptType == 1 && Ppt_SetTime._.UseFlag == 1);
        foreach (Ppt_SetTime pptShift in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(pptShift.Shift_Name, pptShift.Shift_id.ToString());
            cbxShift.Items.Add(item);
        }
    }

    private void bindClass()
    {
        EntityArrayList<Ppt_ShiftClass> list = classManager.GetAllList();
        foreach (Ppt_ShiftClass pptClass in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(pptClass.Shift_ClassName, pptClass.Shift_ClassId.ToString());
            cbxClass.Items.Add(item);
        }
    }


    private void bindMaintainers()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<SYS_USER> list = userManager.GetListByWhere(where);
        foreach (SYS_USER user in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(user.Real_name, user.USER_ID);
            cbxMaintainers.Items.Add(item);
        }
    }

    private void bindFengxian()
    {
        for(int i = 1;i<13;i++)
        {
            cbxFaSheng.AddItem(i.ToString(), i);
            cbxTanCe.AddItem(i.ToString(), i);
            cbxYanZhong.AddItem(i.ToString(), i);
        }
    }
    private DataSet getList()
    {
        Ppt_pmdownrecord _params = new Ppt_pmdownrecord();
        _params.Mp_startdate = Convert.ToDateTime(Convert.ToDateTime(dStartDate.Text).ToString("yyyy-MM-dd") + " " + dStartTime.Text);
        _params.Mp_enddate = Convert.ToDateTime(Convert.ToDateTime(dEndDate.Text).ToString("yyyy-MM-dd") + " " + dEndTime.Text);
        //_params.workShopCode = cbWorkShop.SelectedItem.Value;
        _params.Equip_code = cbStopEquip.SelectedItem.Value;
        _params.StopType = cbStopKind.SelectedItem.Value;
        _params.Mp_ikindcode = cbStopType.SelectedItem.Value;
        _params.Mp_code = cbStopReason.SelectedItem.Value;

        return GetDataByParas( _params );
    }

    public System.Data.DataSet GetDataByParas(Ppt_pmdownrecord queryParams)
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"SELECT TA.Serial_id, TA.Equip_code, TB.Equip_name, TA.shift_id, TG.shift_Name, TA.shift_Class, 
                            TA.Mp_startdate, TA.Mp_enddate, CONVERT(VARCHAR(20),TA.Mp_startdate,120) AS BeginTime, case CONVERT(varchar(4), 
                            TA.Mp_enddate, 120) when '1900' then '' else CONVERT(varchar(19), TA.Mp_enddate, 120) end OverTime, 
                           cast( case CONVERT(varchar(4), TA.Mp_enddate, 120) when '1900' then '' else CONVERT(varchar, DATEDIFF(MI,TA.Mp_startdate,TA.Mp_enddate)) end  as int )AS DurationMi,
						   TD.param_Name,TE.Mp_ikindname,TF.Mp_name,TA.Xianxiang,TA.YuanYin,TA.Handle_detail,TA.Mp_Analyse,TA.Mp_Describe,TA.YanZhong,TA.FaSheng,TA.TanCe,TA.fengxian,
						   TA.StopType,TH.USER_NAME Handle_name,CONVERT(VARCHAR(20),TA.Maintain_StartTime,120) AS Maintain_StartTime,CONVERT(VARCHAR(20),TA.Maintain_EndTime,120) AS Maintain_EndTime,
                            cast( case CONVERT(varchar(4), TA.Maintain_EndTime, 120) when '1900' then '' else CONVERT(varchar, DATEDIFF(MI,TA.Maintain_StartTime,TA.Maintain_EndTime)) end  as int )AS Maintain_Time,TI.USER_NAME Maintain_Person
		FROM Ppt_pmdownrecord TA
		LEFT JOIN Pmt_equip TB ON TA.Equip_code = TB.Equip_code
		LEFT JOIN Ppt_SetTime TG ON TA.shift_id = TG.shift_id AND DeptType = 1
		LEFT JOIN eqm_Mpparam TD ON TA.Mp_mkindcode = TD.param_id AND TD.Param_Type = 4
		LEFT JOIN Eqm_downikind TE ON TA.Mp_ikindcode = TE.Mp_ikindcode
		LEFT JOIN Eqm_downcode TF ON TA.Mp_code = TF.Mp_code
		LEFT JOIN Sys_User TH ON TA.Handle_name = TH.USER_ID
		LEFT JOIN Sys_User TI ON TA.Maintain_Person = TI.USER_ID");
        sb.AppendLine("WHERE 1=1");
        if (!string.IsNullOrEmpty(queryParams.Equip_code))
            sb.AppendLine("AND TA.Equip_code='" + queryParams.Equip_code + "'");
        if (queryParams.Mp_startdate != null)
            sb.AppendLine("AND TA.Mp_startdate>='" + queryParams.Mp_startdate + "'");
        if (queryParams.Mp_enddate != null)
            sb.AppendLine("AND TA.Mp_enddate<='" + queryParams.Mp_enddate + "'");
        if (!string.IsNullOrEmpty(queryParams.StopType))
            sb.AppendLine("AND TA.StopType='" + queryParams.StopType + "'");
        if (!string.IsNullOrEmpty(queryParams.Mp_ikindcode))
            sb.AppendLine("AND TA.Mp_ikindcode='" + queryParams.Mp_ikindcode + "'");
        if (!string.IsNullOrEmpty(queryParams.Mp_code))
            sb.AppendLine("AND TA.Mp_code=" + queryParams.Mp_code);
        #endregion

        NBear.Data.CustomSqlSection css = manager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }
    private void bindList()
    {
        this.store.DataSource = getList();
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "停机记录");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.hideMode.Text = "Add";

        cbxShift.Select(0);
        cbxClass.Select(0);
        cbxEquip.Select(0);
        txtStartDate.SetValue(DateTime.MinValue);
        txtStartTime.SetValue(DateTime.MinValue.TimeOfDay);
        txtEndDate.SetValue(DateTime.MinValue);
        txtEndTime.SetValue(DateTime.MinValue.TimeOfDay);
        txtFixStartDate.SetValue(DateTime.MinValue);
        txtFixStartTime.SetValue(DateTime.MinValue.TimeOfDay);
        txtFixEndDate.SetValue(DateTime.MinValue);
        txtFixEndTime.SetValue(DateTime.MinValue.TimeOfDay);
        cbxMaintainers.SetValue(null);
        cbStopMainType.SetValue(null);
        cbMp_ikindcode.SetValue(null);
        cbMp_code.SetValue(null);
        txtXianXiang.Text = "";
        txtYuanYin.Text = "";
        cbxYanZhong.Select(0);
        cbxTanCe.Select(0);
        txtFengXian.Text = "";
        cbxFaSheng.Select(0);
        txtHandle_detail.Text = "";
        txtMp_Analyse.Text = "";
        txtMp_Describe.Text = "";
        this.winSave.Hidden = false;
    }
    protected void btnSave_Click( object sender , EventArgs e )
    {
        if(this.hideMode.Text == "Add")//添加
        {
            Ppt_pmdownrecord record = new Ppt_pmdownrecord(); 
            record.Equip_code = cbxEquip.Value.ToString();
            if (!string.IsNullOrEmpty(cbxFaSheng.Value.ToString()))
            { record.FaSheng = Convert.ToInt32(cbxFaSheng.Value); }
            if (!string.IsNullOrEmpty(cbxFaSheng.Value.ToString()) && !string.IsNullOrEmpty(cbxYanZhong.Value.ToString()) && !string.IsNullOrEmpty(cbxTanCe.Value.ToString()))
            { record.Fengxian = Convert.ToInt32(cbxFaSheng.Value) * Convert.ToInt16(cbxYanZhong.Value) * Convert.ToInt16(cbxTanCe.Value); }
            
            record.Handle_detail = txtHandle_detail.Text;
            record.Handle_flag = "1";
            record.Handle_name = this.UserID;
            record.Mp_mkindcode = cbStopMainType.SelectedItem.Value.ToString();
            record.Mp_ikindcode = cbMp_ikindcode.SelectedItem.Value.ToString();
            record.Mp_code = cbMp_code.SelectedItem.Value.ToString();

            if (!string.IsNullOrEmpty(cbxShift.Value.ToString()))
            { record.Shift_id = Convert.ToInt16(cbxShift.Value); }
            record.Shift_Class = cbxClass.RawText;
            TimeSpan fixMinute = txtEndDate.SelectedDate + txtEndTime.SelectedTime - (txtStartDate.SelectedDate + txtStartTime.SelectedTime);
            if (fixMinute.TotalMinutes > 240)
            {
                record.StopType = "A";
            }
            else if (fixMinute.TotalMinutes > 110)
            {
                record.StopType = "B";
            }
            else if (fixMinute.TotalMinutes > 0)
            {
                record.StopType = "C";
            }
            else
            {
                X.Msg.Alert("提示", "停机结束时间应晚于开始时间！").Show();
                return;
            }

            if (!string.IsNullOrEmpty(cbxTanCe.Value.ToString()))
            { record.TanCe = Convert.ToInt16(cbxTanCe.Value); }
            record.Xianxiang = txtXianXiang.Text;
            if (!string.IsNullOrEmpty(cbxYanZhong.Value.ToString()))
            { record.YanZhong = Convert.ToInt16(cbxYanZhong.Value); }
            record.YuanYin = txtYuanYin.Text;
            record.Mp_startdate = txtStartDate.SelectedDate + txtStartTime.SelectedTime;
            record.Mp_enddate = txtEndDate.SelectedDate + txtEndTime.SelectedTime;
            if (txtFixStartDate.SelectedDate != DateTime.MinValue && txtFixEndDate.SelectedDate != DateTime.MinValue && txtFixStartTime.Text != null && txtFixEndTime.Text != null)
            {
                record.Maintain_StartTime = txtFixStartDate.SelectedDate + txtFixStartTime.SelectedTime;
                record.Maintain_EndTime = txtFixEndDate.SelectedDate + txtFixEndTime.SelectedTime;
                TimeSpan fixtimeAll = txtFixEndDate.SelectedDate + txtFixEndTime.SelectedTime - (txtFixStartDate.SelectedDate + txtFixStartTime.SelectedTime);
                if ((fixtimeAll.TotalMinutes) <= 0)
                {
                    X.Msg.Alert("提示", "维修结束时间应晚于开始时间！").Show();
                    return;
                }
            }
            if (cbxMaintainers.SelectedItem.Value!=null)
            {
                record.Maintain_Person = cbxMaintainers.SelectedItem.Value.ToString();
            }
            else
            {
                record.Maintain_Person = null;
            }
            record.Mp_Analyse = txtMp_Analyse.Text;
            record.Mp_Describe = txtMp_Describe.Text;

            if (manager.Insert(record) >= 0)
            {
                this.AppendWebLog("停机信息添加", "添加机台：" + record.Equip_code);
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
            Ppt_pmdownrecord record = manager.GetById(int.Parse(hideObjID.Text));
            if (record != null)
            {
                record.Equip_code = cbxEquip.Value.ToString();
                if (!string.IsNullOrEmpty(cbxFaSheng.Value.ToString()))
                { record.FaSheng = Convert.ToInt32(cbxFaSheng.Value); }
                if (!string.IsNullOrEmpty(cbxFaSheng.Value.ToString()) && !string.IsNullOrEmpty(cbxYanZhong.Value.ToString()) && !string.IsNullOrEmpty(cbxTanCe.Value.ToString()))
                { record.Fengxian = Convert.ToInt32(cbxFaSheng.Value) * Convert.ToInt16(cbxYanZhong.Value) * Convert.ToInt16(cbxTanCe.Value); }
            
            //    record.FaSheng = Convert.ToInt32(cbxFaSheng.Value);
             //   record.Fengxian = Convert.ToInt32(cbxFaSheng.Value) * Convert.ToInt16(cbxYanZhong.Value) * Convert.ToInt16(cbxTanCe.Value);
                record.Handle_detail = txtHandle_detail.Text;
                record.Handle_flag = "1";
                record.Handle_name = this.UserID;
                record.Mp_mkindcode = cbStopMainType.SelectedItem.Value.ToString();

                EntityArrayList<Eqm_downikind> list = typeManager.GetListByWhere(Eqm_downikind._.Mp_ikindname == cbMp_ikindcode.SelectedItem.Value.ToString());
                if (list.Count > 0)
                {
                    record.Mp_ikindcode = list[0].Mp_ikindcode;
                }
                else { record.Mp_ikindcode = cbMp_ikindcode.SelectedItem.Value.ToString(); }
                EntityArrayList<Eqm_downcode> list2 = reasonManager.GetListByWhere(Eqm_downcode._.Mp_name == cbMp_code.SelectedItem.Value.ToString());
                if (list2.Count > 0)
                {
                    record.Mp_code = list2[0].Mp_code;
                }
                else { record.Mp_code = cbMp_code.SelectedItem.Value.ToString(); }


                if (!string.IsNullOrEmpty(cbxShift.Value.ToString()))
                { record.Shift_id = Convert.ToInt16(cbxShift.Value); }
              //  record.Shift_id = Convert.ToInt16(cbxShift.Value);
                record.Shift_Class = cbxClass.RawText;
                TimeSpan fixMinute = txtEndDate.SelectedDate + txtEndTime.SelectedTime - (txtStartDate.SelectedDate + txtStartTime.SelectedTime);
                if (fixMinute.TotalMinutes > 240)
                {
                    record.StopType = "A";
                }
                else if (fixMinute.TotalMinutes > 110)
                {
                    record.StopType = "B";
                }
                else if (fixMinute.TotalMinutes > 0)
                {
                    record.StopType = "C";
                }
                else
                {
                    X.Msg.Alert("提示", "停机结束时间应晚于开始时间！").Show();
                    return;
                }

                if (!string.IsNullOrEmpty(cbxTanCe.Value.ToString()))
                { record.TanCe = Convert.ToInt16(cbxTanCe.Value); }
                record.Xianxiang = txtXianXiang.Text;
                if (!string.IsNullOrEmpty(cbxYanZhong.Value.ToString()))
                { record.YanZhong = Convert.ToInt16(cbxYanZhong.Value); }
               // record.TanCe = Convert.ToInt16(cbxTanCe.Value);
                record.Xianxiang = txtXianXiang.Text;
               // record.YanZhong = Convert.ToInt16(cbxYanZhong.Value);
                record.YuanYin = txtYuanYin.Text;
                record.Mp_startdate = txtStartDate.SelectedDate + txtStartTime.SelectedTime;
                record.Mp_enddate = txtEndDate.SelectedDate + txtEndTime.SelectedTime;
                if (txtFixStartDate.SelectedDate != DateTime.MinValue && txtFixEndDate.SelectedDate != DateTime.MinValue && txtFixStartTime.Text != null && txtFixEndTime.Text != null)
                {
                    record.Maintain_StartTime = txtFixStartDate.SelectedDate + txtFixStartTime.SelectedTime;
                    record.Maintain_EndTime = txtFixEndDate.SelectedDate + txtFixEndTime.SelectedTime;
                    TimeSpan fixtimeAll = txtFixEndDate.SelectedDate + txtFixEndTime.SelectedTime - (txtFixStartDate.SelectedDate + txtFixStartTime.SelectedTime);
                    if ((fixtimeAll.TotalMinutes) <= 0)
                    {
                        X.Msg.Alert("提示", "维修结束时间应晚于开始时间！").Show();
                        return;
                    }
                }
                if (cbxMaintainers.SelectedItem.Value != null)
                {
                    record.Maintain_Person = cbxMaintainers.SelectedItem.Value.ToString();
                }
                else
                {
                    record.Maintain_Person = null;
                }
                record.Mp_Analyse = txtMp_Analyse.Text;
                record.Mp_Describe = txtMp_Describe.Text;

                if (manager.Update(record) >= 0)
                {
                    this.AppendWebLog("停机信息修改", "修改机台：" + record.Equip_code);
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

    #region 下拉列表事件响应
    protected void cbWorkShop_SelectChanged( object sender , EventArgs e )
    {
        //bindEquip();
    }
    protected void cbStopMainType_SelectChanged( object sender , EventArgs e )
    {
        changeStopType();
    }
    protected void cbStopType_SelectChanged( object sender , EventArgs e )
    {
        changeMp_code();
    }
    #endregion

    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Edit( string ObjID )
    {
        Ppt_pmdownrecord record = manager.GetById(int.Parse(ObjID));

        //if (record.IsReportMaintain == "1")
        //{
        //    X.Msg.Alert("提示", "该数据已经报修，不能修改！").Show();
        //    return;
        //}

        if ( record != null )
        {
            cbxShift.SetValue(record.Shift_id.ToString());
            cbxClass.SetValue(record.Shift_Class);
            cbxEquip.SetValue(record.Equip_code);
            txtStartDate.SetValue(record.Mp_startdate.Value.Date);
            txtStartTime.SetValue(record.Mp_startdate.Value.TimeOfDay);
            if(record.Mp_enddate!=null)
            {
                txtEndDate.SetValue(record.Mp_enddate.Value.Date);
                txtEndTime.SetValue(record.Mp_enddate.Value.TimeOfDay);
            }
            if (record.Maintain_StartTime != null)
            {
                txtFixStartDate.SetValue(record.Maintain_StartTime.Value.Date);
                txtFixStartTime.SetValue(record.Maintain_StartTime.Value.TimeOfDay);
            }
            else
            {
                txtFixStartDate.SetValue(DateTime.MinValue);
                txtFixStartTime.SetValue(DateTime.MinValue.TimeOfDay);
            }
            if (record.Maintain_EndTime != null)
            {
                txtFixEndDate.SetValue(record.Maintain_EndTime.Value.Date);
                txtFixEndTime.SetValue(record.Maintain_EndTime.Value.TimeOfDay);
            }
            else
            {
                txtFixEndDate.SetValue(DateTime.MinValue);
                txtFixEndTime.SetValue(DateTime.MinValue.TimeOfDay);
            }
            cbxMaintainers.SetValue(record.Maintain_Person);
            cbStopMainType.SetValue(record.Mp_mkindcode);
            cbMp_ikindcode.SetValue(record.Mp_ikindcode);
            cbMp_code.SetValue(record.Mp_code);
            txtXianXiang.Text = record.Xianxiang;
            txtYuanYin.Text = record.YuanYin;
            cbxYanZhong.SetValue(record.YanZhong);
            cbxTanCe.SetValue(record.TanCe);
            txtFengXian.Text = record.Fengxian.ToString();
            cbxFaSheng.SetValue(record.FaSheng);
            txtHandle_detail.Text = record.Handle_detail;
            txtMp_Analyse.Text = record.Mp_Analyse;
            txtMp_Describe.Text = record.Mp_Describe;
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
    [DirectMethod]
    public void pnlList_Delete(string ObjID)
    {
        Ppt_pmdownrecord record = manager.GetById(int.Parse(ObjID));
        manager.Delete(int.Parse(ObjID));
        this.AppendWebLog("停机信息删除", "修改机台："+record.Equip_code );
          
        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }

    #endregion

    
}