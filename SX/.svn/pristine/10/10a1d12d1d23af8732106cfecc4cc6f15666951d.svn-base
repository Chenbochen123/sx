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
using System.Text.RegularExpressions;
using Mesnac.Data.Components;
using System.Data;
using Mesnac.Business.Interface;
using System.Text;


public partial class Manager_Technology_Report_RubberDE : Mesnac.Web.UI.Page
{
    protected PmtRubWeightSettingManager manager = new PmtRubWeightSettingManager();//业务对象
    protected BasEquipManager equipManager = new BasEquipManager();
    protected SysCodeManager sysCodeManager = new SysCodeManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        //public __()
        //{
        //    查询 = new SysPageAction() { ActionID = 1, ActionName = "btn_search" };
           
        //}
        //public SysPageAction 查询 { get; private set; } //必须为 public
       
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
            hidden1.Text = "1";
            IPptShiftManager pptShiftManager = new PptShiftManager();
        string constSelectAllText = "---请选择---";
                txtBeginDate.Text = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
                //txtEndDate.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
                txtPptShift.Items.Clear();
                txtPptShift.Items.Add(allitem);
                WhereClip where = new WhereClip();
                OrderByClip order = new OrderByClip();
                where.And(PptShift._.UseFlag == "1");
                order = PptShift._.ObjID.Asc;
                foreach (PptShift m in pptShiftManager.GetListByWhereAndOrder(where, order))
                {
                    txtPptShift.Items.Add(new Ext.Net.ListItem(m.ShiftName, m.ObjID));
                }
                txtPptShift.Text = (txtPptShift.Items[0].Value);
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<PmtRubWeightSetting> GetPageResultData(PageResult<PmtRubWeightSetting> pageParams)
    {
        PmtRubWeightSettingManager.QueryParams queryParams = new PmtRubWeightSettingManager.QueryParams();
        queryParams.pageParams = pageParams;
     
     
        queryParams.workshopID = hidden_workshop_code.Value.ToString();

        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<PmtRubWeightSetting> GetTablePageDataBySql(PmtRubWeightSettingManager.QueryParams queryParams)
    {
        PageResult<PmtRubWeightSetting> pageParams = queryParams.pageParams;
        //--convert(varchar,CONVERT(decimal(18, 2), ((sum(Real_weight)*100)/( (27000-BasDCBZ.BZTime*count(distinct (pptplan.planid)))/(pmtRecipe.LotDoneTime +JZTime)*dbo.FuncGetLotTotalWeight(pptshiftconfig.equipcode,pptshiftconfig.materialcode,pptplan.recipetype)*0.95  ) )))+'%'  as wcl
        //(27000-60*BasDCBZ.BZTime*count( dbo.FuncGetPlanNum(left(pptplan.planid,10)))) 
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@" 
select convert (varchar(10),pptshiftconfig.plandate,120) as plandate,shiftname,count(*) as sumber,pptshiftconfig.equipcode,materialname,itemname,username,EquipName, dbo.FuncGetPlanNum(left(pptplan.planid,10))  as PlanCount,
pmtRecipe.LotDoneTime as avgDoneAllRtime,avg(case serial_id when 1 then null else Bwb_Time end ) as avgBwbTime,
sum(Real_weight) as sumRealweight,dbo.FuncGetLotTotalWeight(pptshiftconfig.equipcode,pptshiftconfig.materialcode,pptplan.recipetype) as LotTotalWeight,
BasDCBZ.BZTime,JZTime,pmtRecipe.LotDoneTime +JZTime as DETime,pptshiftconfig.shiftid,
CONVERT(decimal(18, 2), (27000-60*BasDCBZ.BZTime* dbo.FuncGetPlanNum(left(pptplan.planid,10)))/(pmtRecipe.LotDoneTime +JZTime)*dbo.FuncGetLotTotalWeight(pptshiftconfig.equipcode,pptshiftconfig.materialcode,pptplan.recipetype)*0.95 ) as RubberDE
 ,convert(varchar,CONVERT(decimal(18, 2), ((sum(Real_weight)*100)/( (27000-BasDCBZ.BZTime*count(distinct (pptplan.planid)))/(pmtRecipe.LotDoneTime +JZTime)*dbo.FuncGetLotTotalWeight(pptshiftconfig.equipcode,pptshiftconfig.materialcode,pptplan.recipetype)*0.95  ) )))+'%'  as wcl 
from pptshiftconfig
left join pptplan on left(barcode,12) = pptplan.planid
left join basequip on pptshiftconfig.equipcode=basequip.equipcode
left join  syscode on pptplan.recipetype=itemcode and typeid ='pmttype'
left join   BasMainHander on   pptshiftconfig.zjsid=mainhandercode
left join   Basuser on   BasMainHander.usercode=hrcode
inner join   PptLot on   shelf_barcode=pptshiftconfig.barcode
left join PptShift on pptshiftconfig.shiftid=PptShift.objid
left join BasDCBZ on pptshiftconfig.materialcode=BasDCBZ.materialcode and  pptplan.recipetype=BasDCBZ.recipetype and
 pptshiftconfig.equipcode=BasDCBZ.equipcode 
left join pmtrecipe on pptshiftconfig.materialcode=pmtrecipe.recipematerialcode and  pptplan.recipetype=pmtrecipe.recipetype and
 pptshiftconfig.equipcode=pmtrecipe.recipeequipcode  and pmtrecipe.recipestate = '1'
where 
");
        sqlstr.AppendLine("  pptshiftconfig.plandate='" + DateTime.Parse(txtBeginDate.Text).ToString("yyyy-MM-dd") + " 00:00:00.000'");
        if(  hidden1.Text == "1")
        { sqlstr.AppendLine(" AND  1=2 "); hidden1.Text = "2"; }

        if (!(txtPptShift.SelectedItem.Value == "---请选择---"))
        {
            sqlstr.AppendLine(" AND pptshiftconfig.shiftid = '" + txtPptShift.SelectedItem.Value + "'");
        }
        if (!string.IsNullOrEmpty(txt_user.Text))
        {
            sqlstr.AppendLine(" AND username like '%" + txt_user.Text + "%'");
        }
        if (!string.IsNullOrEmpty(hiddenEquipCode.Text))
        {
            sqlstr.AppendLine(" AND pptshiftconfig.equipcode = '" + hiddenEquipCode.Text + "'");
        }
        if (!string.IsNullOrEmpty(hidden_workshop_code.Text))
        {
            sqlstr.AppendLine(" AND basequip.workshopcode = '" + hidden_workshop_code.Text + "'");
        }
       //and ( left(pptshiftconfig.materialcode,1)='5' or left(pptshiftconfig.materialcode,1)='4') 

        sqlstr.AppendLine(@"   and ( left(pptshiftconfig.materialcode,1) <> '2' ) 
group by convert (varchar(10),pptshiftconfig.plandate,120) ,shiftname,pptshiftconfig.equipcode,materialname,itemname,username,equipname,pptshiftconfig.materialcode,pptplan.recipetype ,BasDCBZ.BZTime,JZTime,pptshiftconfig.shiftid,left(pptplan.planid,10),pmtRecipe.LotDoneTime");
        pageParams.QueryStr = sqlstr.ToString();
        pageParams.PageSize = 9999;
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
        PageResult<PmtRubWeightSetting> pageParams = new PageResult<PmtRubWeightSetting>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        //pageParams.Orderfld = prms.Sort[0].Property + " " + prms.Sort[0].Direction;

        PageResult<PmtRubWeightSetting> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];
        Session["WeightDE"] = lst.DataSet;
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

        DataSet ds = (DataSet)Session["WeightDE"];
        //new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "配方称量报表");
        //return;

        //PageResult<PmtRubWeightSetting> pageParams = new PageResult<PmtRubWeightSetting>();
        //pageParams.PageSize = -100;
        //PageResult<PmtRubWeightSetting> lst = GetPageResultData(pageParams);
        for (int i = 0; i <ds.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = ds.Tables[0].Columns[i];
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
               ds.Tables[0].Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "配方称量报表");
        //new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "胶料定额计算");
    }
    #endregion

  
    
    public string getEquipCode(string equipName)
    {
        string str = "select EquipCode from BasEquip where EquipName='" + equipName + "'";
        DataSet ds = manager.GetBySql(str).ToDataSet();
        str = ds.Tables[0].Rows[0][0].ToString();
        return str;
    }
 
   
}