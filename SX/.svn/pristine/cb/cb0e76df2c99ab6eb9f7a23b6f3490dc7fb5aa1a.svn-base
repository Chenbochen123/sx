using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using NBear.Common;
using Mesnac.Entity;
using Ext.Net;
using System.Text.RegularExpressions;
using Mesnac.Data.Components;
using System.Data;
using System.Text;

public partial class Manager_Equipment_EquipState_EquipState : System.Web.UI.Page
{
    protected BasWorkShopManager manager = new BasWorkShopManager();
    protected BasEquipManager equipManager = new BasEquipManager();
    protected PmtRubWeightSettingManager rubWeightSettingManager = new PmtRubWeightSettingManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            manager.GetBySql(@" update PmtRubWeightSetting set State = '0'
where LEFT(EquipCode,2)='01' and  dateadd(MINUTE,-3,GETDATE()) > LastEditTime").ToDataSet();
           
            InitTreeDept();
        }
    }

    /// <summary>
    /// 初始化车间列表
    /// </summary>
    private void InitTreeDept()
    {
        treeDept.GetRootNode().RemoveAll();
        WhereClip where = new WhereClip();
        where.And(BasWorkShop._.DeleteFlag == "0");
        EntityArrayList<BasWorkShop> lst = manager.getAllMiLanWorkShop();//获得所有密炼机所在的车间
        foreach (BasWorkShop workShop in lst)
        {
            Node node = new Node();
            node.NodeID = workShop.ObjID.ToString();
            node.Text = workShop.WorkShopName;
            node.Icon = Icon.Building;
            node.Leaf = true;
            treeDept.GetRootNode().AppendChild(node);
        }
    }
    #region 分页相关方法
    private PageResult<PmtRubWeightSetting> GetPageResultData(PageResult<PmtRubWeightSetting> pageParams)
    {
        PmtRubWeightSettingManager.QueryParams queryParams = new PmtRubWeightSettingManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.workshopID = Session["WorkShopID"] != null ? Session["WorkShopID"].ToString():"";

        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<PmtRubWeightSetting> GetTablePageDataBySql(PmtRubWeightSettingManager.QueryParams queryParams)
    {
        PageResult<PmtRubWeightSetting> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@" SELECT rws.ObjID , equip.EquipName as EquipName , equip.EquipCode as EquipCode ,  lvl.ItemName as State , rws.EquipElectricCurrent , 
                                        lvl2.ItemName as WeightSettingCtrl , rws.LockType, rws.DeleteFlag , rws.Remark  , equip.workshopCode
                                 FROM PmtRubWeightSettingNewone rws
                                 LEFT JOIN  BasEquip    equip   on  equip.EquipCode = rws.EquipCode
                                 LEFT JOIN  SysCode     lvl     on  lvl.ItemCode    = rws.State                 AND lvl.TypeID = 'EquipState'
                                 LEFT JOIN  SysCode     lvl2    on  lvl2.ItemCode   = rws.WeightSettingCtrl     AND lvl2.TypeID = 'WeightCtrl'
                                 WHERE 1=1 ");
        if (!string.IsNullOrEmpty(queryParams.objID))
        {
            sqlstr.AppendLine(" AND rws.ObjID = " + queryParams.objID);
        }
        if (!string.IsNullOrEmpty(queryParams.workshopID))
        {
            sqlstr.AppendLine(" AND equip.workshopCode = '" + queryParams.workshopID + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.state))
        {
            sqlstr.AppendLine(" AND  rws.State = '" + queryParams.state + "'");
        }
        if (!string.IsNullOrEmpty(queryParams.equipCode))
        {
            sqlstr.AppendLine(" AND rws.EquipCode like '%" + queryParams.equipCode + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.remark))
        {
            sqlstr.AppendLine(" AND  rws.Remark like '%" + queryParams.remark + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.deleteFlag))
        {
            sqlstr.AppendLine(" AND  rws.DeleteFlag ='" + queryParams.deleteFlag + "'");
        }
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = rubWeightSettingManager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return rubWeightSettingManager.GetPageDataBySql(pageParams);
        }
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PmtRubWeightSetting> pageParams = new PageResult<PmtRubWeightSetting>();
        pageParams.PageIndex = 1;
        pageParams.PageSize = 1000;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<PmtRubWeightSetting> lst = GetPageResultData(pageParams);
        DataTable dataSet = lst.DataSet.Tables[0];
        List<object> data = new List<object>();
        foreach (DataRow row in dataSet.Rows)
        {
            string picName = "";
            switch (row["State"].ToString())
            {
                case "运转": picName = "ml_green.jpg";
                    break;
                case "空转": picName = "ml_yellow.jpg";
                    break;
                case "停机": picName = "ml_red.jpg";
                    break;
                default: picName = "ml_red.jpg";
                    break;
            }
            data.Add(new
            {
                name = row["EquipName"].ToString(),
                url = "images/" + picName
            });
        }
        int total = lst.RecordCount;
        return new { data, total };
    }
    /// <summary>
    /// 点击左侧车间列表引发的事件
    /// </summary>
    /// <param name="workshopId"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod]
    public string EquipStoreReload(string workshopId)
    {
        try
        {
            if (workshopId != "")
            { 
                Session["WorkShopID"] = workshopId;
            }
            BasWorkShop workshop = manager.GetById(workshopId);
            Panel2.Title = workshop.WorkShopName;
            PageResult<PmtRubWeightSetting> pageParams = new PageResult<PmtRubWeightSetting>();
            pageParams.PageIndex = 1;
            pageParams.PageSize = 15;
            pageParams.Orderfld = "ObjID ASC";

            PageResult<PmtRubWeightSetting> lst = GetPageResultData(pageParams);
            DataTable dataSet = lst.DataSet.Tables[0];
            List<object> data = new List<object>();
            foreach (DataRow row in dataSet.Rows)
            {
                string picName = "";
                switch (row["State"].ToString())
                {
                    case "运转": picName = "ml_green.png";
                        break;
                    case "空转": picName = "ml_yellow.png";
                        break;
                    case "停机": picName = "ml_red.png";
                        break;
                    default: picName = "ml_red.png";
                        break;
                }
                data.Add(new
                {
                    name = row["EquipName"].ToString(),
                    url = "images/" + picName
                });
            }
            this.store.DataSource = data;
            this.store.DataBind();
            this.store.Reload();
            return "SUCCESS";
        }
        catch (Exception)
        {

            return "FAIL";
        }
        
    }
    #endregion
}