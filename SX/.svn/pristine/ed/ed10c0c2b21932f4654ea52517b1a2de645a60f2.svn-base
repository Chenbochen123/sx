using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Mesnac.Data.Components;
using Mesnac.Entity;
using System.Data;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using NBear.Common;

public partial class Manager_ProducingPlan_MonthlyEntering_RawPlan : Mesnac.Web.UI.Page
{
    private Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    IPptMonthlyEnteringManager pptMonthlyEnteringManager = new PptMonthlyEnteringManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            txtQueryDate.Text = DateTime.Now.ToString("yyyy-MM");
            txtDate.Text = DateTime.Now.ToString("yyyy-MM");
        }
    }

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
            新增 = new SysPageAction() { ActionID = 2, ActionName = "btnAdd" };
            删除 = new SysPageAction() { ActionID = 3, ActionName = "Delete" };
            修改 = new SysPageAction() { ActionID = 4, ActionName = "Edit" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 新增 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
    }
    #endregion

    #region 分页相关方法
    private PageResult<PptMonthlyEntering> GetPageResultData(PageResult<PptMonthlyEntering> pageParams)
    {
        PptMonthlyEnteringManager.QueryParams queryParams = new PptMonthlyEnteringManager.QueryParams();
        queryParams.PageParams = pageParams;
        if (!String.IsNullOrEmpty(this.txtQueryDate.Text))
        {
            queryParams.YearMonth = this.txtQueryDate.Text;
        }
        if (!String.IsNullOrEmpty(this.txtMaterialMinorID.Text))
        {
            queryParams.MaterialCode = this.txtMaterialMinorID.Text;
        }
        queryParams.TypeID = 0;
        return pptMonthlyEnteringManager.GetPptMonthlyEnteringPageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return null;
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PptMonthlyEntering> pageParams = new PageResult<PptMonthlyEntering>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        PageResult<PptMonthlyEntering> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];
        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion


      /// <summary>
    /// 点击添加信息中保存按钮激发的事件
    /// sunyj   2013年5月6日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            PptMonthlyEntering pptMonthly = new PptMonthlyEntering();
            pptMonthly.MaterialCode = this.hiddenRubberCode.Text;
            pptMonthly.MaterialName = this.txtRubberName.Text;
            pptMonthly.YearMonth = this.txtDate.Text;
            try
            {
                pptMonthly.PlanNum = Convert.ToDouble(this.txtPlanNum.Text);
            }
            catch (Exception)
            {
                msg.Alert("提示", "设置月总使用量失败！");
                msg.Show();
                return;
            }
            pptMonthly.TypeID = 0;//胶料信息为1 原材料使用为0
            EntityArrayList<PptMonthlyEntering> pptlist = pptMonthlyEnteringManager.GetListByWhere(PptMonthlyEntering._.MaterialCode == pptMonthly.MaterialCode && PptMonthlyEntering._.YearMonth == pptMonthly.YearMonth);
            if (pptlist.Count > 0)
            {
                msg.Alert("提示", "该物料的本月份的计划量已录入！");
                msg.Show();
                return;
            }
            try
            {
                pptMonthlyEnteringManager.Insert(pptMonthly);
                msg.Alert("提示", "添加成功！");
                msg.Show();
                this.AddRubPlanWin.Close();
                this.pageToolBar.DoRefresh();
            }
            catch (Exception)
            {
                msg.Alert("提示", "添加失败！");
                msg.Show();
            }
        }
        catch (Exception)
        {
            msg.Alert("提示", "添加失败！");
            msg.Show();
        }
    }
    /// <summary>
    /// 点击添加信息中取消按钮激发的事件
    /// sunyj   2013年5月5日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.AddRubPlanWin.Close();
    }

    /// <summary>
    /// 点击修改激发的事件
    /// sunyj   2013年3月27日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string objID)
    {
        try
        {
            PptMonthlyEntering up = pptMonthlyEnteringManager.GetById(objID);
            this.hidden_update_materialCode.Text = objID;
            this.txtUpMaterialName.Text = up.MaterialName;
            this.txtUpPlanNum.Text = up.PlanNum.ToString();
            this.txtUpDate.Text = up.YearMonth;
            this.ModifyRubPlanWin.Show();
        }
        catch (Exception)
        {
            msg.Alert("提示", "修改失败！");
            msg.Show();
        }
    }
    /// <summary>
    /// 点击删除触发的事件
    /// sunyj   2013年3月27日
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string objID)
    {
        try
        {
            pptMonthlyEnteringManager.Delete(objID);
            this.pageToolBar.DoRefresh();
            return "";
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
    }
     /// <summary>
    /// 修改窗体中的确定按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnModifySave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            PptMonthlyEntering up = pptMonthlyEnteringManager.GetById(this.hidden_update_materialCode.Text);
            try
            {
                up.PlanNum = Convert.ToDouble(this.txtUpPlanNum.Text);
            }
            catch (Exception)
            {
                msg.Alert("提示", "月总使用量不合法！");
                msg.Show();
                return;
            }
            up.YearMonth = this.txtUpDate.Text;
            pptMonthlyEnteringManager.Update(up);
            this.ModifyRubPlanWin.Close();
            this.pageToolBar.DoRefresh();
        }
        catch (Exception)
        {
            msg.Alert("提示", "修改失败！");
            msg.Show();
        }
    }
    /// <summary>
    /// 修改窗体中的取消按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnModifyCancel_Click(object sender, DirectEventArgs e)
    {
        this.ModifyRubPlanWin.Close();
    }
    /// <summary>
    /// 点击添加按钮激发的事件
    /// sunyj   2013年5月1日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        txtRubberName.Text = "";
        txtPlanNum.Text = "";
        this.AddRubPlanWin.Show();
    }
}