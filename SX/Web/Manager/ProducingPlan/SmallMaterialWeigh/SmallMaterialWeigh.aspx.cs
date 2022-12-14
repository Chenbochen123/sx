using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using NBear.Common;
using System.Data;
using System.Text;
using Mesnac.Data.Implements;
public partial class Manager_ProducingPlan_SmallMaterialWeigh_SmallMaterialWeigh : Mesnac.Web.UI.Page
{
    #region 属性注入
    IPptPlanManager pptPlanManager = new PptPlanManager();
    IPptShiftManager pptShiftManaer = new PptShiftManager();
    IPptWeighDataManager pptWeighDataManager = new PptWeighDataManager();
    IBasEquipManager basEquipManager = new BasEquipManager();
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch,btnPlanSearch" };
            浏览 = new SysPageAction() { ActionID = 2 };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 浏览 { get; private set; } //必须为 public
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            this.txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            FillShift();
           
        }
    }
    #region 页面初始化
    private void FillShift()
    {
        EntityArrayList<PptShift> lstShift = pptShiftManaer.GetListByWhereAndOrder(PptShift._.UseFlag == 1,PptShift._.ObjID.Asc);
        if (lstShift.Count >= 0)
        {
            foreach (PptShift shift in lstShift)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = shift.ShiftName;
                item.Value = shift.ObjID.ToString();
                cboShift.Items.Add(item);
            }
        }
        EntityArrayList<BasEquip> equipList = basEquipManager.GetListByWhere(BasEquip._.EquipType == "02");
        if (equipList.Count >= 0)
        {
            foreach (BasEquip equip in equipList)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = equip.EquipName;
                item.Value = equip.EquipCode.ToString();
                cboEquip.Items.Add(item);
            }
        }
    }
    #endregion

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return null;
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PptPlan> pageParams = new PageResult<PptPlan>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        PageResult<PptPlan> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    private PageResult<PptPlan> GetPageResultData(PageResult<PptPlan> pageParams)
    {
        PptPlanManager.QueryParams queryParams = new PptPlanManager.QueryParams();
        queryParams.pageParams = pageParams;
        if (cboEquip.SelectedItem != null)
        {
            queryParams.equipCode = cboEquip.SelectedItem.Value;
        }
        queryParams.planStartDate = Convert.ToDateTime(txtBeginTime.Text).ToString("yyyy-MM-dd");
        if (cboShift.SelectedItem != null)
        {
            queryParams.shiftID = this.cboShift.SelectedItem.Value;
        }
        queryParams.planID = this.txtPlanID.Text.Trim();
        return GetSmallPlanTablePageDataBySql(queryParams);
    }
    public PageResult<PptPlan> GetSmallPlanTablePageDataBySql(PptPlanService.QueryParams queryParams)
    {
        PageResult<PptPlan> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@" SELECT distinct p.PlanID,p.PlanDate,s.ShiftName,c.ClassName,RecipeMaterialCode,RecipeMaterialName,PlanNum,RealNum,RealStartTime,RealEndtime 
                                    FROM dbo.PptPlan p
                                    LEFT JOIN dbo.PptShift s ON p.ShiftID=s.ObjID
                                    LEFT JOIN dbo.PptClass c ON c.ObjID=p.ClassID
                               
                                 WHERE      1 = 1 and LEFT(RecipeMaterialCode,1)=2  ");
             //LEFT JOIN dbo.PptWeighData w ON p.PlanID=w.PlanID   
        if (!string.IsNullOrEmpty(queryParams.planID))
        {
            sqlstr.AppendLine(" AND p.PlanID='" + queryParams.planID + "'");
        }
        else
        {
            if (!string.IsNullOrEmpty(queryParams.equipCode))
            {
                sqlstr.AppendLine(" AND p.RecipeEquipCode= '" + queryParams.equipCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.planStartDate))
            {
                sqlstr.AppendLine(" AND p.PlanDate >='" + queryParams.planStartDate + "'");
            }

            if (!string.IsNullOrEmpty(txtEndTime.Text))
            {
                sqlstr.AppendLine(" AND p.PlanDate <='" + txtEndTime.Text + "'");
            }


            if (!string.IsNullOrEmpty(queryParams.shiftID))
            {
                sqlstr.AppendLine(" AND p.ShiftID= " + queryParams.shiftID);
            }

        }
        if (!string.IsNullOrEmpty(TextMatetial.Text))
        {
            sqlstr.AppendLine(" AND  RecipeMaterialName like '%" + TextMatetial.Text + "%'");
        }
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = pptPlanManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return pptPlanManager.GetPageDataBySql(pageParams);
        }
    }

}