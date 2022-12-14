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

public partial class Manager_BasicInfo_WorkInfo_WorkCoefficientInfo : Mesnac.Web.UI.Page
{
    protected BasWorkManager manager = new BasWorkManager();//业务对象
    protected BasEquipManager equipmanager = new BasEquipManager();//业务对象
    protected PptShiftManager shiftmanager = new PptShiftManager();//业务对象
    protected BasWorkCoefficientManager coefficientmanager = new BasWorkCoefficientManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<BasWork> entityList;
    private const string constSelectAllText = "---请选择---";


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
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            #region 岗位名称
            Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
            EntityArrayList<BasWork> lstWork = manager.GetListByWhereAndOrder(BasWork._.DeleteFlag == "0", BasWork._.WorkName.Asc);
            cmoWorkName.Items.Clear();
            cmoWorkName.Items.Add(allitem);
            foreach (BasWork m in lstWork)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Value = m.ObjID.ToString();
                item.Text = m.WorkName;
                cmoWorkName.Items.Add(item);
            }
            if (cmoWorkName.Items.Count > 0)
            {
                cmoWorkName.Text = (cmoWorkName.Items[0].Text);
            }

            add_WorkName.Items.Clear();
            add_WorkName.Items.Add(allitem);
            foreach (BasWork m in lstWork)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Value = m.ObjID.ToString();
                item.Text = m.WorkName;
                add_WorkName.Items.Add(item);
            }
            if (add_WorkName.Items.Count > 0)
            {
                add_WorkName.Text = (add_WorkName.Items[0].Text);
            }


            #endregion
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<BasWorkCoefficient> GetPageResultData(PageResult<BasWorkCoefficient> pageParams)
    {
        BasWorkCoefficientManager.QueryParams queryParams = new BasWorkCoefficientManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.workID = cmoWorkName.Text.Replace(constSelectAllText,"");
        queryParams.deleteFlag = hidden_delete_flag.Text;
        return coefficientmanager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasWorkCoefficient> pageParams = new PageResult<BasWorkCoefficient>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasWorkCoefficient> lst = GetPageResultData(pageParams);
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
        PageResult<BasWorkCoefficient> pageParams = new PageResult<BasWorkCoefficient>();
        pageParams.PageSize = -100;
        PageResult<BasWorkCoefficient> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "岗位系数信息");
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
        add_WorkName.Text = "";
        add_Coefficient.Text = "";
        add_Remark.Text = "";
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
        BasWorkCoefficient workCoefficient = coefficientmanager.GetById(obj_id);
        modify_WorkID.Value = workCoefficient.WorkID;
        modify_ObjID.Value = workCoefficient.ObjID;
        BasWork work2 = manager.GetById(Convert.ToInt32(workCoefficient.WorkID));
        modify_WorkName.Value = work2.WorkName;
        modify_Coefficient.Value = workCoefficient.Coefficient;
        modify_Remark.Value = workCoefficient.Remark;
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
            BasWorkCoefficient workCoefficient = coefficientmanager.GetById(obj_id);
            workCoefficient.DeleteFlag = "0";
            coefficientmanager.Update(workCoefficient);
            this.AppendWebLog("岗位系数信息恢复", "编号：" + obj_id);
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
            BasWorkCoefficient workCoefficient = coefficientmanager.GetById(Convert.ToInt32(obj_id));
            workCoefficient.DeleteFlag = "1";
            coefficientmanager.Update(workCoefficient);
            this.AppendWebLog("岗位系数信息删除", "编号：" + obj_id);
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
            BasWorkCoefficient workCoefficient = new BasWorkCoefficient();
            workCoefficient.ObjID = Convert.ToInt32(coefficientmanager.GetNextObjID());
            workCoefficient.WorkID = Convert.ToInt32(add_WorkName.Value);
            workCoefficient.Coefficient = Convert.ToDecimal(add_Coefficient.Text);
            workCoefficient.Remark = (string)(add_Remark.Text);
            workCoefficient.DeleteFlag = "0";
            coefficientmanager.Insert(workCoefficient);
            this.AppendWebLog("岗位系数信息添加", "编号：" + workCoefficient.ObjID);
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

            BasWorkCoefficient workCoefficient = new BasWorkCoefficient();
            workCoefficient.ObjID = Convert.ToInt32(modify_ObjID.Text);
            workCoefficient.Attach();
            workCoefficient.WorkID = Convert.ToInt32(modify_WorkID.Text);
            workCoefficient.Coefficient = Convert.ToDecimal(modify_Coefficient.Text.ToString());
            workCoefficient.Remark = (string)(modify_Remark.Text);
            coefficientmanager.Update(workCoefficient);
            this.AppendWebLog("岗位系数信息修改", "编号：" + modify_ObjID.Text);
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