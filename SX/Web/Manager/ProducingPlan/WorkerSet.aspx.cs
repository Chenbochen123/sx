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
using Mesnac.Entity;


public partial class Manager_ProducingPlan_WorkerSet : Mesnac.Web.UI.Page
{
    protected ppt_workerSetManager Manager = new ppt_workerSetManager();
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
        datetime.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
        date1.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
        this.winSave.Hidden = true;
        bindList();
    }


    private DataSet getList()
    {
        return GetDataByParas();
       // return GetDataByParas(cboPlan.Value.ToString);
    }

    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"select  * from ppt_workerset ");
        sb.AppendLine("WHERE 1=1");
        if (!string.IsNullOrEmpty(datetime.SelectedDate.ToShortDateString()))
        {
            sb.AppendLine("AND plan_date='" + datetime.SelectedDate.ToString("yyyy-MM-dd") + "'");
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
    protected void btnSearch_Click( object sender , EventArgs e )
    {
        bindList();
    }
   
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.hideMode.Text = "Add";
        txtnum.Text = "";
        cbxshift.Select(1);
        this.winSave.Hidden = false;
    }
    //添加计划确定
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.hideMode.Text == "Add")//添加
        {
            EntityArrayList<ppt_workerSet> listAdd = Manager.GetListByWhere(ppt_workerSet._.plan_date == date1.SelectedDate.ToString("yyyy-MM-dd")&& ppt_workerSet._.shiftname==cbxshift.SelectedItem.Text);
            if (listAdd.Count > 0)
            { X.Msg.Alert("提示", "已有该记录！").Show(); return; }
            ppt_workerSet record = new ppt_workerSet();

            record.plan_date = date1.SelectedDate.ToString("yyyy-MM-dd");
            record.shift_id = Convert.ToInt32(cbxshift.SelectedItem.Value);
            record.shiftname = cbxshift.SelectedItem.Text;
            record.setNum = Convert.ToInt32(txtnum.Text);

            if (Manager.Insert(record) >= 0)
            {
                //this.AppendWebLog("油品添加", "添加油品号：" + record.Oil_code);
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
          //  ppt_workerSet record = Manager.GetBySql(hideObjID.Text);

            StringBuilder sb = new StringBuilder();
            #region
            sb.AppendLine(@"update ppt_workerset set setNum='"+txtnum.Text+"' ");
            sb.AppendLine("WHERE plan_date='" + hideObjID.Text + "' and shiftname = '" + hideObjID1.Text + "'");
            #endregion
            if (date1.SelectedDate.ToString("yyyy-MM-dd") != hideObjID.Text || cbxshift.SelectedItem.Text != hideObjID1.Text)
            {
                X.Msg.Alert("提示", "不允许修改日期或班次！").Show();
                return;
            }
            Manager.GetBySql(sb.ToString()).ToDataSet();
            winSave.Hidden = true;
            bindList();
            X.Msg.Alert("提示", "修改完成！").Show();

            //if (record != null)
            //{
            //    record.Oil_name = txtOilname.Text;

            //    if (Manager.Update(record) >= 0)
            //    {
            //        //this.AppendWebLog("油品修改", "修改油品号：" + record.Oil_code);
            //        winSave.Hidden = true;
            //        bindList();
            //        X.Msg.Alert("提示", "修改完成！").Show();
            //    }
            //    else
            //    {
            //        X.Msg.Alert("提示", "修改失败！").Show();
            //    }
            //}
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        winSave.Hidden = true;
    }
    #endregion

    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Edit(string date,string shiftname)
    {
        EntityArrayList<ppt_workerSet> list = Manager.GetListByWhere(ppt_workerSet._.plan_date == date && ppt_workerSet._.shiftname == shiftname);
        if(list.Count>0)
        {
            ppt_workerSet record = list[0];

            if (record != null)
            {
                date1.Text = list[0].plan_date;
                cbxshift.Text = list[0].shiftname;
                txtnum.Text = list[0].setNum.ToString();
                hideObjID.Text = list[0].plan_date.Substring(0,10);
                hideObjID1.Text = list[0].shiftname;
                this.hideMode.Text = "Edit";
                this.winSave.Hidden = false;
            }
            else
            {
                bindList();
                X.Msg.Alert("提示", "此条记录已经不存在！").Show();
            }
        }
    }
    //[DirectMethod]
    //public void pnlList_Delete(string ObjID)
    //{
    //    Eqm_lube record = lubeManager.GetById(ObjID);
    //    if(record != null)
    //    {
    //        lubeManager.Delete(ObjID);
    //        this.AppendWebLog("油品删除", "修改油品号：" + record.Oil_code);
    //        bindList();
    //        X.Msg.Alert("提示", "删除成功！").Show();
    //    }
    //    else
    //    {
    //        X.Msg.Alert("提示", "该记录已经不存在！").Show();
    //        return;
    //    }
    //}


    ////自动生成油品主键号
    //protected string GetMaxPlanID()
    //{
    //    string planID = "";
    //    EntityArrayList<Eqm_lube> list = lubeManager.GetAllListOrder(Eqm_lube._.Oil_code.Desc);
    //    if (list.Count > 0)
    //    {
    //        planID = list[0].Oil_code;
    //        if (Convert.ToInt32(list[0].Oil_code) < 9)
    //        {
    //            planID = "00" + (Convert.ToInt32(list[0].Oil_code) + 1).ToString();
    //        }
    //        else if (Convert.ToInt32(list[0].Oil_code) < 99)
    //        {
    //            planID = "0" + (Convert.ToInt32(list[0].Oil_code) + 1).ToString();
    //        }
    //        else
    //        {
    //            planID = (Convert.ToInt32(list[0].Oil_code) + 1).ToString();
    //        }
    //    }
    //    else 
    //    {
    //        planID = "001";
    //    }
    //    return planID;
    //}

    #endregion


}