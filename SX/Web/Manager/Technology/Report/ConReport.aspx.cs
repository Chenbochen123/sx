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

public partial class Manager_Technology_Report_ConReport : System.Web.UI.Page
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
            txtBeginDate.Text = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
            txtEndDate.Text = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");


            String sql = @"select * from basequip where equiptype ='02' or equiptype ='01'  order by equipname";
            DataSet ds = storageManager.GetBySql(sql).ToDataSet();
            Ext.Net.ListItem li = new Ext.Net.ListItem("全部", "");
            cbxjitai.Items.Add(li);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                li = new Ext.Net.ListItem(dr["EquipName"].ToString(), dr["EquipCode"].ToString());
                cbxjitai.Items.Add(li);
            }
        }
    }

    #region 分页相关方法


    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        //,Dbo.FuncGetWeight4ByObjID(PmtRecipe.objid) as Weight4,Dbo.FuncGetWeight6ByObjID(PmtRecipe.objid) as Weight6
        string sql = @"select  planid,matername,equipname, shiftname, avg(DoneAllRtime) as sumDoneAllRtime
,avg(DoneRtime) as sumDoneRtime,avg(BwbTime) as sumBwbTime,avg(realweight) as avgrealweight,sum(realweight) as sumrealweight,
count(*) as num
from pptlotdata
left join basequip on pptlotdata.equipcode=basequip.equipcode
left join PptShift on pptlotdata.shiftid=PptShift.objid
where 1=1 ";

        sql = sql + " and plandate >= '" + DateTime.Parse(txtBeginDate.Text).ToString("yyyy-MM-dd")+ "' ";
        sql = sql + " and plandate <= '" + DateTime.Parse(txtEndDate.Text).ToString("yyyy-MM-dd") + "' ";
        sql = sql + " and matername like '%" + txt_name.Text + "%' ";
        if (!string.IsNullOrEmpty(cbxjitai.SelectedItem.Value))
        { sql = sql + " and pptlotdata.equipcode = '" + cbxjitai.SelectedItem.Value + "'"; }
        //if (!string.IsNullOrEmpty(ComboBox1.SelectedItem.Value))
        //{ sql = sql + " and left(RecipeMaterialCode,1 ) = '" + ComboBox1.SelectedItem.Value + "'"; }
        //else
        //{
        //    sql = sql + " and left(RecipeMaterialCode,1 )in ('4','5')";
        //}
        //if (!string.IsNullOrEmpty(txt_name.Text))
        //{ sql = sql + " and RecipeMaterialName like '%" + txt_name.Text + "%'"; }


        //   txt_name.Text = sql;
        //return null;
        if (!string.IsNullOrEmpty(cbxChejian.SelectedItem.Value))
        {
            if (cbxChejian.SelectedItem.Value.ToString().Equals("全部"))
            { }
            else
            { sql = sql + " and basequip.workShopCode = '" + cbxChejian.SelectedItem.Value + "'"; }
        }


        sql = sql + "group by planid,matername,equipname,shiftname ";
     
        DataSet ds = ppmManager.GetBySql(sql).ToDataSet();

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