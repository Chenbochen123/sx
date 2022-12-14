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

public partial class Manager_System_SysProblemRecord_SysProblemRecord : Mesnac.Web.UI.Page
{
    protected SysProblemRecordManager manager = new SysProblemRecordManager();//业务对象
    protected SysCodeManager sysCodeManager = new SysCodeManager();
    protected BasUserManager userManager = new BasUserManager();
    private Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框

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
            导出 = new SysPageAction() { ActionID = 5, ActionName = "btnExport" };
        }
        public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
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
            hidden_txt_create_user.Text = this.UserID;
            string currentName = userManager.GetListByWhere(BasUser._.WorkBarcode == this.UserID)[0].UserName;
            txt_create_user.Text = currentName;
            hidden_txt_create_usename.Text = currentName;

            EntityArrayList<SysCode> proDeptList = sysCodeManager.GetListByWhere(SysCode._.TypeID == "ProblemDept");
            foreach (SysCode code in proDeptList)
            {
                txt_create_dept.AddItem(code.ItemName, code.ItemCode);
                txt_deal_dept.AddItem(code.ItemName, code.ItemCode);
                txt_validate_dept.AddItem(code.ItemName, code.ItemCode);

                add_create_dept.AddItem(code.ItemName, code.ItemCode);
                add_deal_dept.AddItem(code.ItemName, code.ItemCode);
                add_validate_dept.AddItem(code.ItemName, code.ItemCode);

                modify_create_dept.AddItem(code.ItemName, code.ItemCode);
                modify_deal_dept.AddItem(code.ItemName, code.ItemCode);
                modify_validate_dept.AddItem(code.ItemName, code.ItemCode);
            }

            EntityArrayList<SysCode> proTypeList = sysCodeManager.GetListByWhere(SysCode._.TypeID == "ProblemType");
            foreach (SysCode code in proTypeList)
            {
                add_problem_type.AddItem(code.ItemName, code.ItemCode);
                modify_problem_type.AddItem(code.ItemName, code.ItemCode);
            }

            EntityArrayList<SysCode> errorLevelList = sysCodeManager.GetListByWhere(SysCode._.TypeID == "ProblemLevel");
            foreach (SysCode code in errorLevelList)
            {
                add_error_level.AddItem(code.ItemName, code.ItemCode);
                modify_error_level.AddItem(code.ItemName, code.ItemCode);
            }
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<SysProblemRecord> GetPageResultData(PageResult<SysProblemRecord> pageParams)
    {
        SysProblemRecordManager.QueryParams queryParams = new SysProblemRecordManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.createUser = hidden_txt_create_user.Value.ToString();
        queryParams.createDept = txt_create_dept.Value.ToString();
        queryParams.dealUser = hidden_txt_deal_user.Value.ToString();
        queryParams.dealDept = txt_deal_dept.Value.ToString();
        queryParams.validateUser = hidden_txt_validate_user.Value.ToString();
        queryParams.validateDept = txt_validate_dept.Value.ToString();
        queryParams.startCreateDate = Convert.ToDateTime(txt_start_create_date.Text);
        queryParams.endCreateDate = Convert.ToDateTime(txt_end_create_date.Text);
        queryParams.startDealDate = Convert.ToDateTime(txt_start_deal_date.Text);
        queryParams.endDealDate = Convert.ToDateTime(txt_end_deal_date.Text);
        queryParams.startValidateDate = Convert.ToDateTime(txt_start_validate_date.Text);
        queryParams.endValidateDate = Convert.ToDateTime(txt_end_validate_date.Text);
        //queryParams.deleteFlag = "0";
        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<SysProblemRecord> pageParams = new PageResult<SysProblemRecord>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;

        PageResult<SysProblemRecord> lst = GetPageResultData(pageParams);
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
        PageResult<SysProblemRecord> pageParams = new PageResult<SysProblemRecord>();
        pageParams.PageSize = -100;
        PageResult<SysProblemRecord> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "问题记录表");
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
        btnAddSave.Disable(true);
        add_create_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
        add_create_user.Text = userManager.GetListByWhere(BasUser._.WorkBarcode == this.UserID)[0].UserName;
        hidden_add_create_user.Text = this.UserID;
        add_create_dept.Value = "";

        add_deal_date.Value = "";
        add_deal_user.Value = "";
        hidden_add_deal_user.Value = "";
        add_deal_dept.Value = "";

        add_validate_date.Value = "";
        add_validate_user.Value = "";
        hidden_add_validate_user.Value = "";
        add_validate_dept.Value = "";

        add_problem_type.Value = "";
        add_error_level.Value = "";
        add_problem_desc.Value = "";
        add_problem_reason.Value = "";
        add_solution.Value = "";
        add_deal_result.Value = "";
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
        SysProblemRecord record = manager.GetById(obj_id);
        modify_obj_id.Value = record.ObjID;
        modify_create_date.Value = record.CreateDate;
        try
        {
            modify_create_user.Value = userManager.GetListByWhere(BasUser._.WorkBarcode == record.CreateUser)[0].UserName;
            hidden_modify_create_user.Value = record.CreateUser;
        }
        catch (Exception)
        {
        }
        modify_create_dept.Value = record.CreateDept;

        modify_deal_date.Value = record.DealDate;
        try
        {
            modify_deal_user.Value = userManager.GetListByWhere(BasUser._.WorkBarcode == record.DealUser)[0].UserName;
            hidden_modify_deal_user.Value = record.DealUser;
        }
        catch (Exception)
        {
        }
        modify_deal_dept.Value = record.DealDept;

        modify_validate_date.Value = record.ValidateDate;
        try
        {
            modify_validate_user.Value = userManager.GetListByWhere(BasUser._.WorkBarcode == record.ValidateUser)[0].UserName;
            hidden_modify_validate_user.Value = record.ValidateUser;
        }
        catch (Exception)
        {
        }
        modify_validate_dept.Value = record.ValidateDept;

        modify_problem_type.Value = record.ProblemType;
        modify_error_level.Value = record.ErrorLevel;
        modify_problem_desc.Value = record.ProblemDesc;
        modify_problem_reason.Value = record.ProblemReason;
        modify_solution.Value = record.Solution;
        modify_deal_result.Value = record.DealResult;
        modify_remark.Value = record.Remark;
        this.winModify.Show();
        
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
            BasUser currentUser = userManager.GetListByWhere(BasUser._.WorkBarcode == this.UserID)[0];
            SysProblemRecord record = manager.GetById(obj_id);
            if (currentUser.WorkBarcode != record.CreateUser)
            {
                return "非发起人不能关闭问题记录信息!";
            }
            if (record.DeleteFlag == "1")
            {
                return "此问题已经关闭!";
            }
            record.DeleteFlag = "1";
            this.AppendWebLog("问题记录信息关闭", "问题记录序号：" + record.ObjID);
            manager.Update(record);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "关闭失败：" + e;
        }
        return "关闭成功";
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
            SysProblemRecord record = new SysProblemRecord();
            record.CreateDate = Convert.ToDateTime(add_create_date.Value);
            record.CreateDept = add_create_dept.Value.ToString();
            record.CreateUser = hidden_add_create_user.Value.ToString();

            record.DealDate = Convert.ToDateTime(add_deal_date.Text) == DateTime.MinValue ? DateTime.Now : Convert.ToDateTime(add_deal_date.Value);
            record.DealDept = string.IsNullOrEmpty(add_deal_dept.Text) ? null : add_deal_dept.Value.ToString();
            record.DealUser = string.IsNullOrEmpty(hidden_add_deal_user.Text) ? null : hidden_add_deal_user.Value.ToString();

            record.ValidateDate = Convert.ToDateTime(add_validate_date.Text) == DateTime.MinValue ? DateTime.Now : Convert.ToDateTime(add_validate_date.Value);
            record.ValidateDept = string.IsNullOrEmpty(add_validate_dept.Text) ? null : add_validate_dept.Value.ToString();
            record.ValidateUser = string.IsNullOrEmpty(hidden_add_validate_user.Text) ? null : hidden_add_validate_user.Value.ToString();

            record.ProblemType = add_problem_type.Value.ToString();
            record.ErrorLevel = add_error_level.Value.ToString();
            record.ProblemDesc = add_problem_desc.Value.ToString();
            record.ProblemReason = string.IsNullOrEmpty(add_problem_reason.Text) ? null : add_problem_reason.Value.ToString();
            record.Solution = string.IsNullOrEmpty(add_solution.Text) ? null : add_solution.Value.ToString();
            record.DealResult = string.IsNullOrEmpty(add_deal_result.Text) ? null : add_deal_result.Value.ToString();
            record.DeleteFlag = "0";
            record.Remark = string.IsNullOrEmpty(add_remark.Text) ? null : add_remark.Value.ToString();
            manager.Insert(record);
            this.AppendWebLog("问题记录信息添加", "问题记录序号：" + record.ObjID);
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
            SysProblemRecord record = new SysProblemRecord();
            record.ObjID = Convert.ToInt32(modify_obj_id.Value);
            record.Attach();
            record.CreateDate = Convert.ToDateTime(modify_create_date.Value);
            record.CreateDept = modify_create_dept.Value.ToString();
            record.CreateUser = hidden_modify_create_user.Value.ToString();

            record.DealDate = Convert.ToDateTime(modify_deal_date.Text) == DateTime.MinValue ? DateTime.Now : Convert.ToDateTime(modify_deal_date.Value);
            record.DealDept = string.IsNullOrEmpty(modify_deal_dept.Text) ? null : modify_deal_dept.Value.ToString();
            record.DealUser = string.IsNullOrEmpty(hidden_modify_deal_user.Text) ? null : hidden_modify_deal_user.Value.ToString();

            record.ValidateDate = Convert.ToDateTime(modify_validate_date.Text) == DateTime.MinValue ? DateTime.Now : Convert.ToDateTime(modify_validate_date.Value);
            record.ValidateDept = string.IsNullOrEmpty(modify_validate_dept.Text) ? null : modify_validate_dept.Value.ToString();
            record.ValidateUser = string.IsNullOrEmpty(hidden_modify_validate_user.Text) ? null : hidden_modify_validate_user.Value.ToString();

            record.ProblemType = modify_problem_type.Value.ToString();
            record.ErrorLevel = modify_error_level.Value.ToString();
            record.ProblemDesc = modify_problem_desc.Value.ToString();
            record.ProblemReason = string.IsNullOrEmpty(modify_problem_reason.Text) ? null : modify_problem_reason.Value.ToString();
            record.Solution = string.IsNullOrEmpty(modify_solution.Text) ? null : modify_solution.Value.ToString();
            record.DealResult = string.IsNullOrEmpty(modify_deal_result.Text) ? null : modify_deal_result.Value.ToString();
            record.DeleteFlag = "0";
            record.Remark = string.IsNullOrEmpty(modify_remark.Text) ? null : modify_remark.Value.ToString();
            this.AppendWebLog("问题记录信息修改", "问题记录序号：" + record.ObjID);
            manager.Update(record);
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

    #region 点击问题记录详细信息
    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_detail(string obj_id)
    {
        SysProblemRecord record = manager.GetById(obj_id);
        detail_create_date.Value = ((DateTime)record.CreateDate).ToString("yyyy-MM-dd");
        try
        {
            detail_create_user.Value = userManager.GetListByWhere(BasUser._.WorkBarcode == record.CreateUser)[0].UserName;
        }
        catch (Exception)
        {
        }
        detail_create_dept.Value = sysCodeManager.GetListByWhere(SysCode._.ItemCode == record.CreateDept
            && SysCode._.TypeID == "ProblemDept")[0].ItemName;

        detail_deal_date.Value = ((DateTime)record.DealDate).ToString("yyyy-MM-dd");;
        if(!string.IsNullOrEmpty(record.DealUser))
            detail_deal_user.Value = userManager.GetListByWhere(BasUser._.WorkBarcode == record.DealUser)[0].UserName;
        else
            detail_deal_user.Value = "";
        if (!string.IsNullOrEmpty(record.DealDept))
            detail_deal_dept.Value = sysCodeManager.GetListByWhere(SysCode._.ItemCode == record.DealDept
                && SysCode._.TypeID == "ProblemDept")[0].ItemName;
        else
            detail_deal_dept.Value = "";

        detail_validate_date.Value = ((DateTime)record.ValidateDate).ToString("yyyy-MM-dd"); ;
        if(!string.IsNullOrEmpty(record.ValidateUser))
        {
            detail_validate_user.Value = userManager.GetListByWhere(BasUser._.WorkBarcode == record.ValidateUser)[0].UserName;
        }
        else
        {
            detail_validate_user.Value = "";
        }
        if (!string.IsNullOrEmpty(record.ValidateDept))
            detail_validate_dept.Value = sysCodeManager.GetListByWhere(SysCode._.ItemCode == record.ValidateDept
                && SysCode._.TypeID == "ProblemDept")[0].ItemName;
        else
            detail_validate_dept.Value = "";

        detail_problem_type.Value = sysCodeManager.GetListByWhere(SysCode._.ItemCode == record.ProblemType
            && SysCode._.TypeID == "ProblemType")[0].ItemName; 
        detail_problem_level.Value = sysCodeManager.GetListByWhere(SysCode._.ItemCode == record.ErrorLevel
            && SysCode._.TypeID == "ProblemLevel")[0].ItemName; 
        detail_problem_desc.Value = record.ProblemDesc;
        detail_problem_reason.Value = record.ProblemReason;
        detail_solution.Value = record.Solution;
        detail_deal_result.Value = record.DealResult;
        detail_remark.Value = record.Remark;
        this.winDetail.Show();
        
    }
    #endregion

}