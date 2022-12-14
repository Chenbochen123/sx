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

public partial class Manager_ShopStorage_RubInJarTotal : Mesnac.Web.UI.Page
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
    private BasUserManager userManager = new BasUserManager();
    private PstShopStorageManager shopStorageManager = new PstShopStorageManager();
    private BasStorageManager storage = new BasStorageManager();
    private PstShopStorageDetailManager shopStorageDetailManager = new PstShopStorageDetailManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest && !IsPostBack)
        {
         
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

        pageParams.PageSize =999;

        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<Pstmminjar> GetTablePageDataBySql(PstmminjarManager.QueryParams queryParams)
    {
        PageResult<Pstmminjar> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@"select  B.MaterialName, sum( A.RealWeight-isnull(UsedWeigh,'0')) as RealWeight, C.EquipName,   FeedjarNo as JarNum,A.EquipCode
                                from dbo.Pstmminjar A with (nolock)
                                left join BasMaterial B on A.MaterCode = B.MaterialCode
                              left join BasEquip C on A.EquipCode = C.EquipCode
                                 where A.DeleteFlag = '0' and A.ClearFlag is null and FeedjarNo is not null"); //G.JarNum
                                //left join 
                                //(
                                //SELECT * FROM (SELECT  RANK() OVER(ORDER BY EquipCode,StoragePlaceCode,MaterialCode,JarNum,ObjID) AS id,* FROM dbo.PmtEquipJarStore) b
                                //WHERE id IN (
                                //SELECT MIN(id) FROM (
                                //SELECT  RANK() OVER(ORDER BY EquipCode,StoragePlaceCode,MaterialCode,JarNum,ObjID) AS id,* FROM dbo.PmtEquipJarStore) a GROUP BY EquipCode,StoragePlaceCode,MaterialCode )
                                //) G on A.EquipCode = G.EquipCode and A.StoragePlaceID = G.StoragePlaceCode and A.MaterCode = G.MaterialCode
                              
        if (!string.IsNullOrEmpty(queryParams.equipCode))
        {
            sqlstr.AppendLine(" AND A.EquipCode = '" + queryParams.equipCode + "'");
        }
        if (!string.IsNullOrEmpty(txtJarNum.Text))
        {
            sqlstr.AppendLine(" AND FeedjarNo= '" + txtJarNum.Text + "'");
        }
        sqlstr.AppendLine(" group by  C.EquipName, B.MaterialName,FeedjarNo ,A.EquipCode ");
        pageParams.QueryStr = sqlstr.ToString();
        //txtJarNum.Text = sqlstr.ToString();
        //X.Js.Alert(sqlstr.ToString()); return null;
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = injarManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
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
        pageParams.Orderfld = " EquipName,JarNum ";

        PageResult<Pstmminjar> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 增删改查按钮激发的事件
 

 
    [Ext.Net.DirectMethod()]
    public string btnBatchSend_Click()
    {






        if (String.IsNullOrEmpty(hiddenSelectEquipCode.Text))
        { return "请选择机台！"; }
        if (String.IsNullOrEmpty(hiddenJarNum.Text))
        { return "请选择料仓！"; }
        String sql = "update Pstmminjar set ClearFlag = '1',ClearTime=getdate() where EquipCode = '" + hiddenSelectEquipCode.Text + "' and FeedjarNo= '" + hiddenJarNum.Text + "' and ( Usedweigh < RealWeight  or Usedweigh is null)";
        DataSet ds = injarManager.GetBySql(sql).ToDataSet();

        sql = "insert into PstJarClear values ('" + hiddenSelectEquipCode.Text + "','" + hiddenJarNum.Text + "',getdate(),'1')";
        ds = injarManager.GetBySql(sql).ToDataSet();
        pageToolBar.DoRefresh();
        this.AppendWebLog("料仓清空记录", "机台编号：" + hiddenSelectEquipCode.Text + "料仓" + hiddenJarNum.Text);
        return "料仓清空成功！";
    }
   

    [Ext.Net.DirectMethod()]
    public string btnBatchSend_ClickOld()
    {
        if (String.IsNullOrEmpty(hiddenEquipCode.Text))
        { return "请选择机台！"; }
        if (String.IsNullOrEmpty(txtJarNum.Text))
        { return "请选择料仓！"; }

        String ss = String.Format(@"   update Pstmminjar set ClearFlag = '1'
                                where jarid in (
                                select jarid  from dbo.Pstmminjar A
                                 left join 
                                (
                                SELECT * FROM (SELECT  RANK() OVER(ORDER BY EquipCode,StoragePlaceCode,MaterialCode,JarNum,ObjID) AS id,* FROM dbo.PmtEquipJarStore) b
                                WHERE id IN (
                                SELECT MIN(id) FROM (
                                SELECT  RANK() OVER(ORDER BY EquipCode,StoragePlaceCode,MaterialCode,JarNum,ObjID) AS id,* FROM dbo.PmtEquipJarStore) a GROUP BY EquipCode,StoragePlaceCode,MaterialCode )
                                ) G on A.EquipCode = G.EquipCode and A.StoragePlaceID = G.StoragePlaceCode and A.MaterCode = G.MaterialCode
                                where A.EquipCode = '{0}' and G.JarNum= '{1}')", hiddenEquipCode.Text, txtJarNum.Text);
      



        String s = String.Format(@"select  B.MaterialName, sum( A.RealWeight-isnull(UsedWeigh,'0')) as RealWeight, C.EquipName,   G.JarNum
                                from dbo.Pstmminjar A
                                left join BasMaterial B on A.MaterCode = B.MaterialCode
                              left join BasEquip C on A.EquipCode = C.EquipCode
                               
                                left join 
                                (
                                SELECT * FROM (SELECT  RANK() OVER(ORDER BY EquipCode,StoragePlaceCode,MaterialCode,JarNum,ObjID) AS id,* FROM dbo.PmtEquipJarStore) b
                                WHERE id IN (
                                SELECT MIN(id) FROM (
                                SELECT  RANK() OVER(ORDER BY EquipCode,StoragePlaceCode,MaterialCode,JarNum,ObjID) AS id,* FROM dbo.PmtEquipJarStore) a GROUP BY EquipCode,StoragePlaceCode,MaterialCode )
                                ) G on A.EquipCode = G.EquipCode and A.StoragePlaceID = G.StoragePlaceCode and A.MaterCode = G.MaterialCode
                                where A.DeleteFlag = '0' and A.ClearFlag is null and G.JarNum is not null");
      
            s = s + " AND A.EquipCode = '" + hiddenEquipCode.Text + "'";
       
        
             s = s+" AND G.JarNum= '" + txtJarNum.Text + "'";
             s = s + " group by  C.EquipName, B.MaterialName, G.JarNum  ";

             DataSet ds = injarManager.GetBySql(s).ToDataSet();
             s = ds.Tables[0].Rows[0][1].ToString();
             injarManager.GetBySql(ss).ToDataSet();
             this.AppendWebLog("料仓清空记录", "机台编号：" + hiddenEquipCode.Text + "料仓" + txtJarNum.Text + "原库存" + s);
        return "料仓清空成功！";
        return null;
    }

  
    #endregion
}