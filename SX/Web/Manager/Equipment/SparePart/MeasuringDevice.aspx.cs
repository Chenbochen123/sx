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


public partial class Manager_Equipment_SparePart_MeasuringDevice : Mesnac.Web.UI.Page
{
    protected Eqm_MeasureManager measureManager = new Eqm_MeasureManager();
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
        bindList();
        //bindFaultType();
        this.winSave.Hidden = true;
        bindMaintainers();
    }


    private DataSet getList()
    {


        return GetDataByParas(txtJLName.Text);
    }

    public System.Data.DataSet GetDataByParas(string JL_name)
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"select t.* ,t1.UserName,CONVERT(varchar(100),T.YX_Date, 23) YX_Date1
from Eqm_Measure t
left join BasUser t1 on t1.WorkBarcode=t.handle_name");
        sb.AppendLine("WHERE 1=1");
        if (!string.IsNullOrEmpty(JL_name))
            sb.AppendLine("AND JL_name='" + JL_name + "'");
      
        #endregion

        NBear.Data.CustomSqlSection css = measureManager.GetBySql(sb.ToString());
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "计量器具表");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.hideMode.Text = "Add";

        //ComboBox1.SetValue(null);
        //ComboBox2.SetValue(null);
        JL_name.Text = "";
        JL_IDNO.Text = "";
        JL_spec.Text = "";
        JL_type.Text = "";
        JL_Grade.Text = "";
        Unit_name.Text = "";
        Fac_name.Text = "";
        Pos_code.Text = "";
        Check_media.Text = "";
        WorkShop.Text = "";
        remark.Text = "";
        cbxHandleName.Text=this.UserID;
        txtState.Text = "";
        txtMemNote.Text = "";
        txtYXdate.Text = "";
        txtConfirmDate.Text = "";
        txtBeginDate.Text = "";
        txtLastCheckDate.Text = "";
        txtCheckStand.Text = "";
        txtCheckTerm.Text = "";
        txtManage.Text = "";
        txtCheckUser.Text = "";
        txtConfirmUser.Text = "";
        this.winSave.Hidden = false;
    }

    protected void btnSave_Click( object sender , EventArgs e )
    {
        if(this.hideMode.Text == "Add")//添加
        {
            Eqm_Measure record = new Eqm_Measure();
            record.JL_name = JL_name.Text;
            record.JL_IDNO = JL_IDNO.Text;
            record.JL_spec = JL_spec.Text;
            record.JL_type = JL_type.Text;
            record.JL_Grade = JL_Grade.Text;
            record.Unit_name = Unit_name.Text;
            record.Pos_code = Pos_code.Text;
            record.Check_media = Check_media.Text;
            record.WorkShop = WorkShop.Text;
            record.Remark = remark.Text;
            record.Fac_name = Fac_name.Text;
            record.JL_Precis = JL_Precis.Text;
            record.Handle_name = this.UserID;
            record.Use_state = txtState.Text;
            record.Mem_note = txtMemNote.SelectedDate.ToString("yyyy-MM-dd");
            record.YX_Date = txtYXdate.SelectedDate;
            record.Confirm_date = txtConfirmDate.SelectedDate;
            record.Begin_date = txtBeginDate.SelectedDate;
            record.LastCheckDate = txtLastCheckDate.SelectedDate;
            record.Check_stand = Convert.ToInt32(txtCheckStand.Text);
            record.Check_term = txtCheckTerm.Text;
            record.Manage_type = txtManage.Text;
            record.Check_user = txtCheckUser.Text;
            record.Confirm_user = txtConfirmUser.Text;


            if (measureManager.Insert(record) >= 0)
            {
                this.AppendWebLog("计量器具信息添加", "添加计量器具：" + record.JL_name);
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
            Eqm_Measure record = measureManager.GetById(int.Parse(hideObjID.Text));
            if (record != null)
            {
                record.JL_name = JL_name.Text;
                record.JL_IDNO = JL_IDNO.Text;
                record.JL_spec = JL_spec.Text;
                record.JL_type = JL_type.Text;
                record.JL_Grade = JL_Grade.Text;
                record.Unit_name = Unit_name.Text;
                record.Pos_code = Pos_code.Text;
                record.Check_media = Check_media.Text;
                record.WorkShop = WorkShop.Text;
                record.Remark = remark.Text;
                record.Fac_name = Fac_name.Text;
                record.JL_Precis = JL_Precis.Text;
                record.Handle_name = this.UserID;
                record.Use_state = txtState.Text;
                record.Mem_note = txtMemNote.SelectedDate.ToString("yyyy-MM-dd");
                record.YX_Date = txtYXdate.SelectedDate;
                record.Confirm_date = txtConfirmDate.SelectedDate;
                record.Begin_date = txtBeginDate.SelectedDate;
                record.LastCheckDate = txtLastCheckDate.SelectedDate;
                record.Check_stand = Convert.ToInt32(txtCheckStand.Text);
                record.Check_term = txtCheckTerm.Text;
                record.Manage_type = txtManage.Text;
                record.Check_user = txtCheckUser.Text;
                record.Confirm_user = txtConfirmUser.Text;


                if (measureManager.Update(record) >= 0)
                {
                    this.AppendWebLog("计量器具信息修改", "修改计量器具：" + record.JL_name);
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

    private void bindMaintainers()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<SYS_USER> list = userManager.GetListByWhere(where);
        foreach (SYS_USER user in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(user.Real_name, user.USER_ID);
            cbxHandleName.Items.Add(item);
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
        Eqm_Measure record = measureManager.GetById(int.Parse(ObjID));

        

        if ( record != null )
        {
            cbxHandleName.SetValue(record.Handle_name);
            txtState.SetValue(record.Use_state);
            txtMemNote.SetValue(record.Mem_note);
            txtYXdate.SetValue(record.YX_Date);
            txtBeginDate.SetValue(record.Begin_date);
            txtLastCheckDate.SetValue(record.LastCheckDate);
            txtCheckStand.SetValue(record.Check_stand);
            txtCheckTerm.SetValue(record.Check_term);
            txtManage.SetValue(record.Manage_type);
            txtCheckUser.SetValue(record.Check_user);
            txtConfirmDate.SetValue(record.Confirm_date);
            txtConfirmUser.SetValue(record.Confirm_user);
            JL_name.SetValue(record.JL_name);
            JL_IDNO.SetValue(record.JL_IDNO);
            JL_spec.SetValue(record.JL_spec);
            JL_type.SetValue(record.JL_type);
            JL_Grade.Text = record.JL_Grade;
            Unit_name.Text = record.Unit_name;
            Pos_code.SetValue(record.Pos_code);
            Check_media.SetValue(record.Check_media);
            WorkShop.Text = record.WorkShop.ToString();
            remark.SetValue(record.Remark);
            Fac_name.Text = record.Fac_name;
            JL_Precis.Text = record.JL_Precis;
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
        Eqm_Measure record = measureManager.GetById(int.Parse(ObjID));
        measureManager.Delete(int.Parse(ObjID));
        this.AppendWebLog("计量器具信息删除", "修改计量器具："+record.JL_code );
          
        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }

    #endregion

    
}