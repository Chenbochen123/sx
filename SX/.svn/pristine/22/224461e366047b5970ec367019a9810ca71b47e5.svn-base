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

public partial class Manager_Technology_Report_WeightReport : System.Web.UI.Page
{
    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch" };
           
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
       
    }
    #endregion

    private BasMaterialManager materManager = new BasMaterialManager();
    private BasEquipManager equipManager = new BasEquipManager();
    private PpmRubConsumeManager ppmManager = new PpmRubConsumeManager();
    private BasUserManager userManager = new BasUserManager();
    private BasMaterialMinorTypeManager minorTypeManager = new BasMaterialMinorTypeManager();
    private PstStorageManager storageManager = new PstStorageManager();
    private PptShiftTimeManager shiftTimeManager = new PptShiftTimeManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
           
        }
    }

    #region 分页相关方法
   

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        //,Dbo.FuncGetWeight4ByObjID(PmtRecipe.objid) as Weight4,Dbo.FuncGetWeight6ByObjID(PmtRecipe.objid) as Weight6
        string sql = @"select RecipeMaterialName as 配方名称,EquipName as 机台名称 ,LotTotalWeight as 配方总重,母胶出库,不合格胶  from dbo.PmtRecipe 
left join BasEquip on PmtRecipe.RecipeEquipCode = BasEquip.EquipCode 
left join (select recipeobjid,sum(setweight) as 母胶出库 from pmtrecipeweight
where left (materialcode,1)='4'
group by recipeobjid) t4 on PmtRecipe.objid =t4.recipeobjid
left join (select recipeobjid,sum(setweight) as 不合格胶 from pmtrecipeweight
where left (materialcode,1)='6'
group by recipeobjid) t6 on PmtRecipe.objid =t6.recipeobjid
where RecipeState = '1' and AuditFlag='1' ";
            if(!string.IsNullOrEmpty(cbxChejian.SelectedItem.Value))
            { sql = sql + " and workshopcode = '" + cbxChejian.SelectedItem.Value + "'"; }
            if (!string.IsNullOrEmpty(ComboBox1.SelectedItem.Value))
            { sql = sql + " and left(RecipeMaterialCode,1 ) = '" + ComboBox1.SelectedItem.Value + "'"; }
            else
            {
                sql = sql + " and left(RecipeMaterialCode,1 )in ('4','5')";
            }
            if (!string.IsNullOrEmpty(txt_name.Text))
            { sql = sql + " and RecipeMaterialName like '%" + txt_name.Text + "%'"; }

        PpmRubConsumeManager.QueryParams queryParams = new PpmRubConsumeManager.QueryParams();
     //   txt_name.Text = sql;
     //return null;
        DataSet ds= ppmManager.GetBySql(sql).ToDataSet();
    
        DataTable data = ds.Tables[0];
        Session["WeightR"] = ds;
        int total = data.Rows.Count;
        return new { data, total };
    }



    #endregion

    #region 增删改查按钮激发的事件


    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = (DataSet)Session["WeightR"];
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "配方称量报表");
    }

   

  
    #endregion


}