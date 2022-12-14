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

public partial class Manager_Equipment_WenShi : Mesnac.Web.UI.Page
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
        sqlstr.AppendLine(@"select EqmHumiture.*,EquipName,case(tempalarm) when '0' then '无报警' when '1' then '超上限' when '2' then '超下限'  end as TAlarm
,case(humialarm) when '0' then '无报警' when '1' then '超上限' when '2' then '超下限'  end as HAlarm
,convert(varchar(20),savetime,120) as savetime2
 from EqmHumiture
left join BasEquip on EqmHumiture.equipcode = BasEquip.EquipCode
where SaveTime >'" + Convert.ToDateTime(txtBeginTime.Text).ToString("yyyy-MM-dd") + "' and SaveTime <'" + Convert.ToDateTime(txtEndTime.Text).AddDays(1).ToString("yyyy-MM-dd") + "' ");
        if (!string.IsNullOrEmpty(queryParams.equipCode))
        {
            sqlstr.AppendLine(" AND EqmHumiture.EquipCode = '" + queryParams.equipCode + "'");
        }
        String scan = b_Scan.Value.ToString().Equals("True") ? "1" : "0";
        if (scan=="1")
        {
            sqlstr.AppendLine(" AND (tempalarm <> '0' or humialarm <>'0')");
        }
        //txtBillNo.Text = sqlstr.ToString();
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {

            NBear.Data.CustomSqlSection css = injarManager.GetBySql(sqlstr.ToString());
            DataSet ds = css.ToDataSet();
        
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