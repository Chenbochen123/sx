﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using NBear.Common;
using Mesnac.Entity;
using Ext.Net;
using Mesnac.Data.Components;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
public partial class Manager_RubberQuality_BasicInfo_PrintReaSon_other : Mesnac.Web.UI.Page
{
    protected BasMaterialMajorTypeManager manager = new BasMaterialMajorTypeManager();//业务对象
    protected BasMaterialMinorTypeManager minorManager = new BasMaterialMinorTypeManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<BasMaterialMajorType> entityList;


    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btn_search" };
            历史查询 = new SysPageAction() { ActionID = 2, ActionName = "btn_history_search" };
            导出 = new SysPageAction() { ActionID = 3, ActionName = "btnExport" };
            修改 = new SysPageAction() { ActionID = 4, ActionName = "Edit" };
            添加 = new SysPageAction() { ActionID = 7, ActionName = "btn_add" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 历史查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
     
        public SysPageAction 添加 { get; private set; } //必须为 public
    }
    #endregion


    #region 初始化方法
    /// <summary>
    /// 页面初始化方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {

            String sql = "select * from BasReturnReason ";
            DataSet ds = manager.GetBySql(sql).ToDataSet();


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Ext.Net.Checkbox item = new Ext.Net.Checkbox();
                item.BoxLabel = dr["Objid"].ToString();
                item.InputValue = dr["Reason"].ToString();
                CheckboxGroupxm.Add(item);
                //CheckboxGroupAdd.Add(item);

            }
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Ext.Net.Checkbox item = new Ext.Net.Checkbox();
                item.BoxLabel = dr["Objid"].ToString();
                item.InputValue = dr["Reason"].ToString();

                CheckboxGroupAdd.Add(item);

            }
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<BasMaterialMajorType> GetPageResultData(PageResult<BasMaterialMajorType> pageParams)
    {
        BasMaterialMajorTypeManager.QueryParams queryParams = new BasMaterialMajorTypeManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.objID = txt_obj_id.Text.TrimEnd().TrimStart();

        queryParams.deleteFlag = hidden_delete_flag.Text;

        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<BasMaterialMajorType> GetTablePageDataBySql(BasMaterialMajorTypeManager.QueryParams queryParams)
    {
        PageResult<BasMaterialMajorType> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@" select * from BasReturnReason ");
        //if (!string.IsNullOrEmpty(queryParams.deleteFlag))
        //{
        //    sqlstr.AppendLine(" AND BasMaterialDL.DeleteFlag ='" + queryParams.deleteFlag + "'");
        //}
        //if (!string.IsNullOrEmpty(txt_obj_id.Text))
        //{
        //    sqlstr.AppendLine(" AND MaterialName like '%" + txt_obj_id.Text + "%'");
        //}
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = manager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return manager.GetPageDataBySql(pageParams);
        }
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (!Regex.IsMatch(txt_obj_id.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_obj_id.Text = "";
        }
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasMaterialMajorType> pageParams = new PageResult<BasMaterialMajorType>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "Objid ASC";

        PageResult<BasMaterialMajorType> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        //foreach (DataRow dr in data.Rows)
        //{ dr[3] = Into(dr[3].ToString()); }

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    public string Into(string str)
    {
        String re = "";
        string[] ss = str.Split('|');
        foreach (string s in ss)
        {
            if (string.IsNullOrEmpty(s)) break;
            String sql = "select * from BasReturnReason  where objid = '" + s + "'";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            re = re + ds.Tables[0].Rows[0][3].ToString() + "|";
        }

        return re;
    }
    #region 增删改查按钮激发的事件
    /// <summary>
    /// 点击添加按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_add_Click(object sender, EventArgs e)
    {
        add_nr.Text = "";
        this.winAdd.Show();
    }


    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_edit(string objID)
    {
        try
        {
            String sql = "delete from  BasReturnReason   where objid = '" + objID + "'";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("定量包装删除", "定量包装编码：" + objID);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
     
    }

    ///// <summary>
    ///// 点击恢复激发的事件
    ///// yuany   2013年1月22日
    ///// </summary>
    ///// <param name="unit_num"></param>
    //[Ext.Net.DirectMethod()]
    //public string commandcolumn_direct_recover(string obj_id, string MaterialCode, string EquipCode)
    //{
    //    try
    //    {
    //        //String sql = "update BasMaterialDL set deleteflag = '1' where MaterialCode = '" + MaterialCode + "' and EquipCode = '" + EquipCode + "'";
    //        //DataSet ds = manager.GetBySql(sql).ToDataSet();
    //        String sql = "update BasMaterialDL set deleteflag = '0' where objid = '" + obj_id + "'";
    //        DataSet ds = manager.GetBySql(sql).ToDataSet();
    //        this.AppendWebLog("定量包装恢复", "定量包装编码：" + obj_id);
    //        pageToolBar.DoRefresh();
    //    }
    //    catch (Exception e)
    //    {
    //        return "恢复失败：" + e;
    //    }
    //    return "恢复成功";
    //}

    /// <summary>
    /// 点击删除触发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string objID)
    {
        try
        {
            String sql = "delete from  BasReturnReason   where objid = '" + objID + "'";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("定量包装删除", "定量包装编码：" + objID);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }


    /// <summary>
    /// 点击取消按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winAdd.Close();
        this.winModify.Close();
    }

    /// <summary>
    /// 点击添加信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
 [Ext.Net.DirectMethod()]
    public void BtnAddSave_Click(String remark)
    {
        try
        {

            if (String.IsNullOrEmpty(add_nr.Text))
            {
                msg.Alert("操作", "内容不能为空！");
                msg.Show();
                return;
            }
            String sql = "";
         
            sql = String.Format("insert into BasReturnReason   values('{0}',convert(varchar(19),getdate(),120))",
               add_nr.Text, remark);
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("备注", "内容：" + add_nr.Text);
            pageToolBar.DoRefresh();
            this.winAdd.Close();
            msg.Alert("操作", "保存成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 点击修改信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>object sender, EventArgs e)
    /// <param name="e"></param>
    //[Ext.Net.DirectMethod()]
    //public void BtnModifySave_Click(String remark)
    //{
    //    try
    //    {


    //        for (int i = 0; i < CheckboxGroupxm.Items.Count; i++)
    //        {

    //            if (((Ext.Net.Checkbox)CheckboxGroupxm.Items[i]).Checked == true)
    //            //{ remark = remark + ((Ext.Net.Checkbox)CheckboxGroupxm.Items[i]).InputValue + "|"; }

    //            { remark = remark + "ssss" + "|"; }
    //        }


    //        String sql = "";

    //        sql = "update BasPrintReaSon set Remark = '" + remark + "',reason = '" + modify_MN.Text + "' where objid = '" + modify_ID.Text + "' ";
    //        manager.GetBySql(sql).ToDataSet();
    //        this.AppendWebLog("自检物料修改", "编码：" + modify_ID.Text);
    //        pageToolBar.DoRefresh();
    //        this.winModify.Close();
    //        msg.Alert("操作", "更新成功");
    //        msg.Show();
    //    }
    //    catch (Exception ex)
    //    {
    //        msg.Alert("操作", "更新失败：" + ex);
    //        msg.Show();
    //    }
    //}
    //#endregion

    #endregion
}