using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using Mesnac.Data.Components;
using Mesnac.Business.Interface;
using Mesnac.Data.Implements;
using System.Text;


public partial class Manager_Rubber_Report_YieldReport : System.Web.UI.Page
{
    protected PptPlanManager planManager = new PptPlanManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            cbxShift.Value = "all";
            cbxShiftClass.Value = "all";
           // cbxChejian.Value = "1";
          
        }
    }

    private PageResult<PptPlan> GetPageResultData(PageResult<PptPlan> pageParams)
    {
        //首先判断是否最少包含一个分组,目前允许
        //if (!cbxByPlanDate.Checked && !cbxByShift.Checked && !cbxByClass.Checked && !cbxByEquip.Checked && !cbxByMaterCode.Checked)
        //{
        //    X.Msg.Alert("提示", "至少要有一个分组！").Show();
        //    return;
        //}
        
        string byGroup = string.Empty;
        if (cbxByPlanDate.Checked)
            byGroup += "CONVERT(varchar(10), PlanDate, 120) ProdDate, ";
        if (cbxByShift.Checked)
            byGroup += "B.ShiftName, ";
        if (cbxByClass.Checked)
            byGroup += "C.ClassName, ";
        if (cbxByEquip.Checked)
            byGroup += "D.EquipName, ";
        if (cbxByMaterCode.Checked)
            byGroup += "A.RecipeMaterialName, ";

        PptPlanManager.QueryParams queryParams = new PptPlanManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.byGroup = byGroup;
        queryParams.beginDate = Convert.ToDateTime(txtBeginTime.Text);
        queryParams.endDate = Convert.ToDateTime(txtEndTime.Text);
        queryParams.shiftID = cbxShift.SelectedItem.Value;
        queryParams.classID = cbxShiftClass.SelectedItem.Value;
        queryParams.equipCode = hiddenEquipCode.Text;
        queryParams.materialCode = hiddenMaterCode.Text;
        queryParams.workShopCode = "";
        queryParams.BasUser = hiddenOperPerson.Text;

        return GetTableTotalDataBySql(queryParams);
    }
    public PageResult<PptPlan> GetTableTotalDataBySql(PptPlanManager.QueryParams queryParams)
    {
        PageResult<PptPlan> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine("select " + queryParams.byGroup);
        sqlstr.AppendLine(@"F.UserName, SUM(RealNum) TotalNum, SUM(RealNum*TotalWeight) TotalWeight,syscode.itemname,P.sapversionid
                        from PptPlan A
	                        left join PptShift B on A.ShiftID = B.ObjID
	                        left join PptClass C on A.ClassID = C.ObjID
	                        left join BasEquip D on A.RecipeEquipCode = D.EquipCode
	                        left join BasUser F on A.UserID = F.HRCode 
left join syscode on A.recipetype = syscode.itemcode and syscode.typeid = 'PmtType'
left join pmtrecipe P on A.recipetype = P.recipetype and A.Recipeequipcode = P.Recipeequipcode and A.Recipematerialcode = P.Recipematerialcode and A.Recipeversionid = P.Recipeversionid
                        where 1=1 ");
        if (queryParams.beginDate != DateTime.MinValue)
        {
            sqlstr.AppendLine(" AND A.PlanDate >= '" + queryParams.beginDate.ToString() + "'");
        }
        if (queryParams.endDate != DateTime.MinValue)
        {
            sqlstr.AppendLine(" AND A.PlanDate <= '" + queryParams.endDate.ToString() + "'");
        }
        if (queryParams.shiftID != "all")
        {
            sqlstr.AppendLine(" AND A.ShiftID = '" + queryParams.shiftID + "'");
        }
        if (queryParams.classID != "all")
        {
            sqlstr.AppendLine(" AND A.ClassID = '" + queryParams.classID + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.equipCode))
        {
            sqlstr.AppendLine(" AND A.RecipeEquipCode = '" + queryParams.equipCode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.materialCode))
        {
            sqlstr.AppendLine(" AND A.RecipeMaterialCode = '" + queryParams.materialCode + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.BasUser))
        {
            sqlstr.AppendLine(" AND A.UserID = '" + queryParams.BasUser + "'");
        }
        //sqlstr.AppendLine(" AND SUBSTRING(D.EquipName, 2, 1) = '" + queryParams.workShopCode + "'");
        sqlstr.AppendLine(" group by " + queryParams.byGroup.Replace(" ProdDate", ""));

        sqlstr.AppendLine(" F.UserName,syscode.itemname,P.sapversionid ");
        sqlstr.AppendLine(" having  SUM(RealNum*TotalWeight)<>0 ");
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = planManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return planManager.GetPageDataBySql(pageParams);
        }
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PptPlan> pageParams = new PageResult<PptPlan>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;

        PageResult<PptPlan> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }

    protected void cbxByPlanDate_Change(object sender, EventArgs e)
    {
        if (cbxByPlanDate.Checked)
        {
            ctnBeginTime.Hidden = false;
            ctnEndTime.Hidden = false;
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            ProdDate.Hidden = false;
        }
        else
        {
            ctnBeginTime.Hidden = true;
            ctnEndTime.Hidden = true;
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            ProdDate.Hidden = true;
        }
    }
    protected void cbxByShift_Change(object sender, EventArgs e)
    {
        if (cbxByShift.Checked)
        {
            ctnShift.Hidden = false;
            cbxShift.Value = "all";
            ShiftName.Hidden = false;
        }
        else
        {
            ctnShift.Hidden = true;
            ShiftName.Hidden = true;
        }
    }
    protected void cbxByClass_Change(object sender, EventArgs e)
    {
        if (cbxByClass.Checked)
        {
            ctnShiftClass.Hidden = false;
            cbxShiftClass.Value = "all";
            ClassName.Hidden = false;
        }
        else
        {
            ctnShiftClass.Hidden = true;
            ClassName.Hidden = true;
        }
    }
    protected void cbxByEquip_Change(object sender, EventArgs e)
    {
        if (cbxByEquip.Checked)
        {
            ctnEquipName.Hidden = false;
            txtEquipName.Text = string.Empty;
            hiddenEquipCode.Text = string.Empty;
            EquipName.Hidden = false;
        }
        else
        {
            ctnEquipName.Hidden = true;
            EquipName.Hidden = true;
        }
    }
    protected void cbxByMaterCode_Change(object sender, EventArgs e)
    {
        if (cbxByMaterCode.Checked)
        {
            ctnMaterName.Hidden = false;
            txtMaterName.Text = string.Empty;
            hiddenMaterCode.Text = string.Empty;
            RecipeMaterialName.Hidden = false;
        }
        else
        {
            ctnMaterName.Hidden = true;
            RecipeMaterialName.Hidden = true;
        }
    }

    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PptPlan> pageParams = new PageResult<PptPlan>();
        pageParams.PageSize = -100;
        PageResult<PptPlan> lst = GetPageResultData(pageParams);
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "产量报表");
    }
}