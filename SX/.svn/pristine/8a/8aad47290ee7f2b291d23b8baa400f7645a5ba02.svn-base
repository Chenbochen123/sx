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
using Mesnac.Business.Interface;

public partial class Manager_BasicInfo_WorkPositionInfo_WorkPositionInfo : Mesnac.Web.UI.Page
{
    protected BasWorkManager manager = new BasWorkManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<BasWork> entityList;

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
    private PageResult<BasWork> GetPageResultData(PageResult<BasWork> pageParams)
    {
        BasWorkManager.QueryParams queryParams = new BasWorkManager.QueryParams();
        queryParams.pageParams = pageParams;
       
        queryParams.objID = txt_obj_id.Text.TrimEnd().TrimStart();
        queryParams.workName = txt_work_name.Text.TrimEnd().TrimStart();
        queryParams.remark = txt_remark.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = hidden_delete_flag.Text;

        return manager.GetTablePageDataBySql(queryParams);
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
        PageResult<BasWork> pageParams = new PageResult<BasWork>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasWork> lst = GetPageResultData(pageParams);
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
        if (!Regex.IsMatch(txt_obj_id.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_obj_id.Text = "";
        }
        PageResult<BasWork> pageParams = new PageResult<BasWork>();
        pageParams.PageSize = -100;
        PageResult<BasWork> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "岗位信息");
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
        //CreateSampleLedger(36996);
        //return;
        add_work_name.Text = "";
        hidden_work_name.Value = "";
        //add_hr_code.Text = "";
        //add_erp_code.Text = "";
        add_Remark.Text = "";
        btnAddSave.Disable(true);
        this.winAdd.Show();
        return;


       
    }

   
    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string obj_id)
    {
        //X.Msg.Notify("obj_id", obj_id).Show();

        string sql = "select * from BasWork where objid ='"+ obj_id + "'";
        DataSet ds =manager.GetBySql(sql).ToDataSet();
        modify_obj_id.Value = ds.Tables[0].Rows[0]["ObjID"].ToString();
        modify_work_name.Value = ds.Tables[0].Rows[0]["WorkName"].ToString();
        hidden_work_name.Value = ds.Tables[0].Rows[0]["WorkName"].ToString();
        modify_hr_code.Value = ds.Tables[0].Rows[0]["HRCode"].ToString();
        modify_erp_code.Value = ds.Tables[0].Rows[0]["ERPCode"].ToString();
        modify_Remark.Value = ds.Tables[0].Rows[0]["Remark"].ToString();
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
            BasWork work = manager.GetById(obj_id);
            work.DeleteFlag = "0";
            manager.Update(work);
            this.AppendWebLog("岗位信息恢复", "岗位编号：" + obj_id);
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
    /// <param name="unit_num"></param>`
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string obj_id)
    {
        try
        {
            //BasWork work = manager.GetById(Convert.ToInt32(obj_id));
            //work.DeleteFlag = "1";
            //manager.Update(work);
            //X.Msg.Notify(obj_id,"").Show();
            String sql = "delete  from JCZL_work where work_num='"+ obj_id + "'";
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("岗位信息删除", "岗位编号：" + obj_id);
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
            //添加校验重复
            EntityArrayList<BasWork> workList = manager.GetListByWhere(BasWork._.WorkName == add_work_name.Text.TrimStart().TrimEnd());
            if (workList.Count > 0)
            {
                X.Msg.Alert("提示", "此岗位名称已被使用！").Show();
                return;
            }
            //BasWork work = new BasWork();
            //work.ObjID = Convert.ToInt32(manager.GetNextWorkPositionCode());
            //work.WorkName = (string)(add_work_name.Text);
            //work.Remark = (string)(add_Remark.Text);
            //work.ERPCode = add_erp_code.Text;
            //work.HRCode = add_hr_code.Text;
            //work.DeleteFlag = "0";
            //manager.Insert(work);

            string sql = "select max(work_num)+1 from JCZL_work";
            DataSet ds = manager.GetBySql(sql).ToDataSet();

            sql = "insert into JCZL_work values('"+ds.Tables[0].Rows[0][0].ToString()+"','"+ add_work_name.Text + "',0,'"+ add_Remark.Text + "','','','')";
            manager.GetBySql(sql).ToDataSet();

            this.AppendWebLog("岗位信息添加", "岗位编号：" + ds.Tables[0].Rows[0][0].ToString());
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
            //修改重复校验
            EntityArrayList<BasWork> workList = manager.GetListByWhere(BasWork._.WorkName == modify_work_name.Text.TrimStart().TrimEnd());
            if (workList.Count > 0)
            {
                if (workList[0].WorkName != hidden_work_name.Text)
                {
                    X.Msg.Alert("提示", "此岗位名称已被使用！").Show();
                    return;
                }
            }
            //BasWork work = new BasWork();
            //work.ObjID = Convert.ToInt32(modify_obj_id.Text);
            //work.Attach();
            //work.WorkName = (string)(modify_work_name.Text);
            //work.Remark = (string)(modify_Remark.Text);
            //work.ERPCode = modify_erp_code.Text;
            //work.HRCode = modify_hr_code.Text;
            //manager.Update(work);
  
            string sql = "update dbo.JCZL_work set work_name='" + modify_work_name.Text + "' , Remark = '" + modify_Remark .Text+ "' where work_num='" + modify_obj_id.Text + "'";
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("岗位信息修改", "岗位编号：" + modify_obj_id.Text);
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
    protected void CheckWorkName(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        string workname = field.Text;
        EntityArrayList<BasWork> workList = manager.GetListByWhere(BasWork._.WorkName == workname);
        if (workList.Count == 0)
        {
            e.Success = true;
        }
        else
        {
            if (workList[0].WorkName.ToString() == hidden_work_name.Value.ToString())
            {

                e.Success = true;
            }
            else
            {
                e.Success = false;
                e.ErrorMessage = "此岗位名称已被使用！";
            }
        }
    }
    #endregion
}