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

public partial class Manager_Equipment_XLjiaozhun : Mesnac.Web.UI.Page
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
    private PstmminjarManager injarManager = new PstmminjarManager();
  
   

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
            txtBeginTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
            txtEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    #region 分页相关方法
    private PageResult<Pstmminjar> GetPageResultData(PageResult<Pstmminjar> pageParams)
    {
        PstmminjarManager.QueryParams queryParams = new PstmminjarManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.beginDate = Convert.ToDateTime("2016-10-01");
        queryParams.endDate = Convert.ToDateTime("2016-10-11");
        queryParams.storagePlaceId = "";
        queryParams.equipCode = hiddenEquipCode.Text;



        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<Pstmminjar> GetTablePageDataBySql(PstmminjarManager.QueryParams queryParams)
    {
        PageResult<Pstmminjar> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"select case when left(t1.equipcode,2)='01' then ErrorAllow/1000 else ErrorAllow end as ErrorAllow, case when ErrorAllow-abs(setweight-realweight) >=0 then '0' else '1' end as Er,t2.EquipName,t3.ShiftName 
,t1.WareNum, t1.SetWeight,t1.RealWeight,t1.SaveTime  from PptBalanceCheck t1
left join basequip t2 on t1.equipcode =t2.equipcode
left join PptShift t3  on t1.shiftid=t3.objid
where SaveTime >'" + Convert.ToDateTime(txtBeginTime.Text).ToString("yyyy-MM-dd") + "' and SaveTime <'" + Convert.ToDateTime(txtEndTime.Text).AddDays(1).ToString("yyyy-MM-dd") + "' ");
        if (!string.IsNullOrEmpty(queryParams.equipCode))
        {
            sqlstr.AppendLine(" AND t1.EquipCode = '" + queryParams.equipCode + "'");
        }
        //txtBillNo.Text = sqlstr.ToString();
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
          
            NBear.Data.CustomSqlSection css = injarManager.GetBySql(sqlstr.ToString());
            DataSet ds = css.ToDataSet();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //dr["ErrorAllow"] = "0.001";
                //continue;
                //dr["SetWeight"] = Double.Parse(dr["SetWeight"].ToString()).ToString("F3");
                dr["SetWeight"] = "123";
                if (dr["EquipCode"].ToString().Substring(0, 2) == "01")
                { dr["ErrorAllow"] = (double.Parse(dr["ErrorAllow"].ToString())/1000).ToString(); }
            
            }
            pageParams.DataSet = ds;
            return pageParams;
        }
        else
        {
        
            return injarManager.GetPageDataBySql(pageParams);
        }
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<Pstmminjar> pageParams = new PageResult<Pstmminjar>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = " EquipName";

        PageResult<Pstmminjar> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

   






    [Ext.Net.DirectMethod()]
    public string btnBatchSend_Click()
    {
     
        return null;
    }


  
}