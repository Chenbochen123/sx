using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;

public partial class Manager_RawMaterialQuality_FactoryNonCheck : Mesnac.Web.UI.Page
{
    protected QmcFactoryNonCheckManager manager = new QmcFactoryNonCheckManager();
    protected BasFactoryInfoManager facManager = new BasFactoryInfoManager();
    protected BasMaterialManager materialManager = new BasMaterialManager();
    private Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<BasUser> entityList;

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            添加 = new SysPageAction() { ActionID = 1, ActionName = "btn_add" };
            删除 = new SysPageAction() { ActionID = 2, ActionName = "Delete" };
            修改 = new SysPageAction() { ActionID = 3, ActionName = "Edit" };
            查询 = new SysPageAction() { ActionID = 4, ActionName = "btn_search" };
            历史查询 = new SysPageAction() { ActionID = 5, ActionName = "btn_history_search" };
            恢复 = new SysPageAction() { ActionID = 6, ActionName = "Recover" };
            导出 = new SysPageAction() { ActionID = 7, ActionName = "btnExport" };
        }
        public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 历史查询 { get; private set; } //必须为 public
        public SysPageAction 恢复 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
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
    private PageResult<QmcFactoryNonCheck> GetPageResultData(PageResult<QmcFactoryNonCheck> pageParams)
    {
        QmcFactoryNonCheckManager.QueryParams queryParams = new QmcFactoryNonCheckManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.materialCode = txt_material_name.Text.TrimEnd().TrimStart();
        queryParams.factoryCode = txt_factory_name.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = hidden_delete_flag.Text;
        return GetTablePageDataBySql(queryParams);
        //return manager.GetTablePageDataBySql(queryParams);
    }
    public PageResult<QmcFactoryNonCheck> GetTablePageDataBySql(Mesnac.Data.Implements.QmcFactoryNonCheckService.QueryParams queryParams)
    {
        PageResult<QmcFactoryNonCheck> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"SELECT 
                                t1.ObjID , t2.FacName AS FactoryCode , t3.MaterialName AS MaterialCode , 
                                t1.NonCheckNum , t1.DeleteFlag , t1.Remark,TotalNum,NonCheckWeight,TotalWeight ,t1.MSetNum,t1.NSetNum 
                                FROM QmcFactoryNonCheck t1
                                LEFT JOIN BasFactoryInfo t2 ON t1.FactoryCode  = t2.ObjID
                                LEFT JOIN BasMaterial   t3  ON t1.MaterialCode = t3.MaterialCode
                                WHERE 1 = 1");
        if (!string.IsNullOrEmpty(queryParams.factoryCode))
        {
            sqlstr.AppendFormat(" AND t2.FacName  like '%{0}%'", queryParams.factoryCode);
            sqlstr.AppendLine();
        }
        if (!string.IsNullOrEmpty(queryParams.materialCode))
        {
            sqlstr.AppendFormat(" AND t3.MaterialName  like '%{0}%'", queryParams.materialCode);
            sqlstr.AppendLine();
        }
        if (!string.IsNullOrEmpty(queryParams.deleteFlag))
        {
            sqlstr.AppendLine(" AND t1.DeleteFlag = '" + queryParams.deleteFlag + "'");
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
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<QmcFactoryNonCheck> pageParams = new PageResult<QmcFactoryNonCheck>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;

        PageResult<QmcFactoryNonCheck> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 打印
    /// <summary>
    /// 打印调用方法
    /// yuany 2013年3月2日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<QmcFactoryNonCheck> pageParams = new PageResult<QmcFactoryNonCheck>();
        pageParams.PageSize = -100;
        PageResult<QmcFactoryNonCheck> lst = GetPageResultData(pageParams);
        for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.DataSet.Tables[0].Columns[i];
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
                lst.DataSet.Tables[0].Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "免质检批次信息");
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
        add_factory_code.Text = "";
        hidden_factory_code.Text = "";
        
        add_material_code.Text = "";
        add_non_check_num.Text = "0";
        add_non_check_Weight.Text = "0";
        add_remark.Text = "";
        btnAddSave.Disable(true);
        this.winAdd.Show();
    }


    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string obj_id)
    {
        //初始化
        modify_factory_code.Text = "";
        modify_material_code.Text = "";

        hidden_factory_code_origin.Text = "";
        hidden_material_code_origin.Text = "";

        hidden_factory_code.Text = "";
        hidden_material_code.Text = "";

        modify_non_check_num.Text = "";
        modify_remark.Text = "";

        QmcFactoryNonCheck nonCheck = manager.GetById(obj_id);

        modify_obj_id.Value = nonCheck.ObjID;
        if (String.IsNullOrEmpty(nonCheck.FactoryCode))
        { modify_factory_code.Value = ""; }
        else
        {
            modify_factory_code.Value = facManager.GetListByWhere(BasFactoryInfo._.ObjID == nonCheck.FactoryCode)[0].FacName;
        }
        modify_material_code.Value =materialManager.GetListByWhere(BasMaterial._.MaterialCode == nonCheck.MaterialCode)[0].MaterialName;
        hidden_factory_code.Value = nonCheck.FactoryCode;
        hidden_factory_code_origin.Value = nonCheck.FactoryCode;
        hidden_material_code.Value = nonCheck.MaterialCode;
        hidden_material_code_origin.Value = nonCheck.MaterialCode;
        modify_non_check_num.Value = nonCheck.NonCheckNum;
        modify_non_check_Weight.Value = nonCheck.NonCheckWeight;
        modify_remark.Value = nonCheck.Remark;
        //X.Js.AddScript("App.modify_factory_code.getTrigger(0).show();");
        this.winModify.Show();
    }

    /// <summary>
    /// 点击恢复激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_recover(string obj_id)
    {
        try
        {
            QmcFactoryNonCheck nonCheck = manager.GetById(obj_id);
            nonCheck.DeleteFlag = "0";
            manager.Update(nonCheck);
        
            this.AppendWebLog("厂商对应物料免审核批次RECOVER", "自增编号：" + nonCheck.ObjID);
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
    public string commandcolumn_direct_delete(string obj_id)
    {
        try
        {
            if (obj_id == "1")
            {
                return "超级管理员禁止删除!";
            }
            QmcFactoryNonCheck nonCheck = manager.GetById(obj_id);
            nonCheck.DeleteFlag = "1";
            this.AppendWebLog("厂商对应物料免审核批次DELETE", "自增编号：" + nonCheck.ObjID);
            manager.Update(nonCheck);
            manager.Delete(obj_id);
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
        { //添加校验重复
            EntityArrayList<QmcFactoryNonCheck> nonCheckList = manager.GetListByWhere(
                QmcFactoryNonCheck._.FactoryCode == hidden_factory_code.Value.ToString() &&
                QmcFactoryNonCheck._.MaterialCode == hidden_material_code.Value.ToString());
            if (nonCheckList.Count > 0)
            {
                X.Msg.Alert("提示", "已存在此厂商对应物料的免质检批次信息！").Show();
                return;
            }
            QmcFactoryNonCheck nonCheck = new QmcFactoryNonCheck();
            nonCheck.FactoryCode = (string)(hidden_factory_code.Text);
            nonCheck.MaterialCode = (string)(hidden_material_code.Text);
            nonCheck.NonCheckNum = Convert.ToInt32(add_non_check_num.Text);
            nonCheck.NonCheckWeight = Convert.ToDecimal(add_non_check_Weight.Text);
            nonCheck.DeleteFlag = "0";
            nonCheck.Remark = add_remark.Text;
            nonCheck.TotalNum = 0;
            nonCheck.TotalWeight = 0;
            nonCheck.ErrorNum = 1;
            manager.Insert(nonCheck);
            this.AppendWebLog("厂商对应物料免审核批次ADD", "自增编号：" + nonCheck.ObjID);
            pageToolBar.DoRefresh();
            this.winAdd.Close();
            msg.Notify("操作", "保存成功").Show();
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
            //修改重复校验
            EntityArrayList<QmcFactoryNonCheck> nonCheckList = manager.GetListByWhere(
                QmcFactoryNonCheck._.FactoryCode == hidden_factory_code.Value.ToString() &&
                QmcFactoryNonCheck._.MaterialCode == hidden_material_code.Value.ToString());
            if (nonCheckList.Count > 0)
            {
                if (nonCheckList[0].MaterialCode != hidden_material_code_origin.Text
                    || nonCheckList[0].FactoryCode != hidden_factory_code_origin.Text)
                {
                    X.Msg.Alert("提示", "已存在此厂商对应物料的免质检批次信息！").Show();
                    return;
                }
            }
            QmcFactoryNonCheck nonCheck = new QmcFactoryNonCheck();
            nonCheck.ObjID = Convert.ToInt32(modify_obj_id.Text);
            nonCheck.Attach();
            nonCheck.FactoryCode = (string)(hidden_factory_code.Text);
            nonCheck.MaterialCode = (string)(hidden_material_code.Text);
            nonCheck.NonCheckNum = Convert.ToInt32(modify_non_check_num.Text);
            nonCheck.NonCheckWeight = Convert.ToDecimal(modify_non_check_Weight.Text);
           
            nonCheck.Remark = (string)(modify_remark.Text);
            this.AppendWebLog("厂商对应物料免审核批次Modify", "自增序列：" + nonCheck.ObjID);
            manager.Update(nonCheck);
            pageToolBar.DoRefresh();
            this.winModify.Close();
            msg.Notify("操作", "更新成功").Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "更新失败：" + ex);
            msg.Show();
        }
    }
    #endregion
}