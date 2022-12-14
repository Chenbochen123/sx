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


public partial class Manager_BasicInfo_MaterialInfo_dingliang : Mesnac.Web.UI.Page
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
            删除 = new SysPageAction() { ActionID = 5, ActionName = "Delete" };
            恢复 = new SysPageAction() { ActionID = 6, ActionName = "Recover" };
            添加 = new SysPageAction() { ActionID = 7, ActionName = "btn_add" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 历史查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 恢复 { get; private set; } //必须为 public
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
        sqlstr.AppendLine(@" select BasMaterialDL.* ,case (ScanFlag) when '1' then '是' else '否' end as SFlag ,MaterialName,EquipName from BasMaterialDL
left join basmaterial on BasMaterialDL.materialcode =basmaterial.materialcode
left join basequip on BasMaterialDL.equipcode =basequip.equipcode where 1=1 ");
        if (!string.IsNullOrEmpty(queryParams.deleteFlag))
        {
            sqlstr.AppendLine(" AND BasMaterialDL.DeleteFlag ='" + queryParams.deleteFlag + "'");
        }
        if (!string.IsNullOrEmpty(txt_obj_id.Text))
        {
            sqlstr.AppendLine(" AND MaterialName like '%" + txt_obj_id.Text + "%'");
        }
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
        pageParams.Orderfld = "MaterialName ASC";

        PageResult<BasMaterialMajorType> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 增删改查按钮激发的事件
    /// <summary>
    /// 点击添加按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_add_Click(object sender, EventArgs e)
    {
        add_SetWeight.Text = "";
        hiddenEquipCode.Text = "";
        hiddenMaterialCode.Text = "";
        txMaterialName.Text = "";
        txtEquip.Text = "";
        add_ErrorAllow.Text = "";
        add_Scan.Value = "1";
        btnAddSave.Disable(true);
        this.winAdd.Show();
    }


    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string objID)
    {
        //BasMaterialMajorType materialMajorType = manager.GetById(Convert.ToInt32(objID));
        //modify_obj_id.Text = materialMajorType.ObjID.ToString();
        //modify_major_type_name.Text = materialMajorType.MajorTypeName;
        //hidden_major_type_name.Text = materialMajorType.MajorTypeName;
        //modify_remark.Text = materialMajorType.Remark;
        this.winModify.Show();
    }

    /// <summary>
    /// 点击恢复激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_recover(string obj_id, string MaterialCode, string EquipCode)
    {
        try
        {
            //String sql = "update BasMaterialDL set deleteflag = '1' where MaterialCode = '" + MaterialCode + "' and EquipCode = '" + EquipCode + "'";
            //DataSet ds = manager.GetBySql(sql).ToDataSet();
            String sql = "update BasMaterialDL set deleteflag = '0' where objid = '" + obj_id + "'";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("定量包装恢复", "定量包装编码：" + obj_id);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "恢复失败：" + e;
        }
        return "恢复成功";
    }

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
            String sql = "update BasMaterialDL set deleteflag = '1' where objid = '" + objID + "'";
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
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
           


            if (String.IsNullOrEmpty(hiddenEquipCode.Text))
            {
                msg.Alert("操作", "请选择机台！");
                msg.Show();
                return;
            }

            if (String.IsNullOrEmpty(hiddenMaterialCode.Text))
            {
                msg.Alert("操作", "请选择物料！");
                msg.Show();
                return;
            }
            try { Double d = Double.Parse(add_SetWeight.Text); }
            catch
            {
                msg.Alert("操作", "设定重量必须为数字");
                msg.Show();
                return;
            }
            try { Double d1 = Double.Parse(add_ErrorAllow.Text); }
            catch
            {
                msg.Alert("操作", "允许误差必须为数字");
                msg.Show();
                return;
            }
            String sql = "select max (Edtcode) from BasMaterialDL where materialcode = '" + hiddenMaterialCode.Text + "' and equipcode = '" + hiddenEquipCode.Text + "'";
            DataSet ds = manager.GetBySql(sql).ToDataSet();

            int edt = 1;
            if (!String.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()))
            { edt = int.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1; }
            sql = "select max(objid)  from BasMaterialDL ";
            ds = manager.GetBySql(sql).ToDataSet();
            int objid = 1;
            if (!String.IsNullOrEmpty(ds.Tables[0].Rows[0][0].ToString()))
            { objid = int.Parse(ds.Tables[0].Rows[0][0].ToString()) + 1; }
       
            String scan = add_Scan.Value.ToString().Equals("True") ? "1" : "0";
            sql = "update BasMaterialDL set ScanFlag = '" + scan + "' where  materialcode = '" + hiddenMaterialCode.Text + "'";
            manager.GetBySql(sql).ToDataSet();
            sql = String.Format("insert into BasMaterialDL values('{0}','{1}','{2}','{3}','{4}','{5}','{6}',getdate(),'0','{7}')",
               objid, hiddenMaterialCode.Text, hiddenEquipCode.Text, edt, add_SetWeight.Text, add_ErrorAllow.Text, this.UserID, scan);
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("定量物料添加", "物料编码：" + objid);
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
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
        try
        {
            String scan = modify_Scan.Value.ToString().Equals("True") ? "1" : "0";
            String sql = "update BasMaterialDL set setweight = '" + modify_SetWeight.Text + "',errorallow = '" + modify_ErrorAllow.Text + "' ,ScanFlag = '" + scan + "' where objid  = '" + modify_ID.Text + "' ";
            manager.GetBySql(sql).ToDataSet();
            sql = "update BasMaterialDL set ScanFlag = '" + scan + "' where  materialcode = (select top 1 materialcode from BasMaterialDL where objid = '" + modify_ID.Text + "' )";
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("定量物料修改", "物料编码：" + modify_ID.Text); 
            pageToolBar.DoRefresh();
            this.winModify.Close();
            msg.Alert("操作", "更新成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "更新失败：" + ex);
            msg.Show();
        }
    }
    #endregion

    #region 校验方法
    #endregion
}