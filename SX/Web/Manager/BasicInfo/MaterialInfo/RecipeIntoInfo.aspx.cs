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
using Mesnac.Data.Components;
using System.Data;
using System.Text.RegularExpressions;

public partial class Manager_BasicInfo_MaterialInfo_RecipeIntoInfo : Mesnac.Web.UI.Page
{
    protected BasMaterialMajorTypeManager manager = new BasMaterialMajorTypeManager();//业务对象
    protected BasMaterialMinorTypeManager minorManager = new BasMaterialMinorTypeManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<BasMaterialMajorType> entityList;


    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btn_search" };

        }
        public SysPageAction 查询 { get; private set; } //必须为 public

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
            String sql = @"select distinct RecipeMaterialCode,RecipeMaterialName from PmtRecipe where RecipeMaterialCode>'3' and RecipeMaterialCode in (select MaterialCode from BasMaterial) order by RecipeMaterialName";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            Ext.Net.ListItem li = new       Ext.Net.ListItem("全部","");
            material_group.Items.Add(li);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                li = new Ext.Net.ListItem(dr["RecipeMaterialName"].ToString(), dr["RecipeMaterialCode"].ToString());
                material_group.Items.Add(li);
            
            
            
            }

            //sql = @"select * from dbo.SysCodePmt  order by id  ";
            sql = @"select * from SysCode   where TypeID = 'PmtType'  order by LEN(ItemCode),ItemCode";
             ds = manager.GetBySql(sql).ToDataSet();
          li = new Ext.Net.ListItem("", "");
          pmttype.Items.Add(li);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //li = new Ext.Net.ListItem(dr["name"].ToString(), dr["id"].ToString());
                li = new Ext.Net.ListItem(dr["ItemName"].ToString(), dr["Remark"].ToString());
                pmttype.Items.Add(li);



            }

        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<BasMaterialMajorType> GetPageResultData(PageResult<BasMaterialMajorType> pageParams)
    {


        return null;
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        String sql = @"select distinct w.RecipeMaterialCode,s.remark as RecipeType,S.ItemName as ItemName,t2.GroupName as dtName ,BasMaterial.MaterialName ,isnull(B.materialname,'') +isnull(C.ItemName,'') as Name
 from PmtRecipe W
left join SysCode S on w.recipetype=S.Itemcode     and S.TypeID='PmtType'
left join PmtRecipeIntoDT t2 on w.RecipeMaterialCode=t2.materialid and t2.RecipeType = s.remark
left join BasMaterial on w.RecipeMaterialCode=BasMaterial.MaterialCode
left join BasMaterial B on left(dbo.FuncGetGroupName(GroupName),13)=B.MaterialCode
left join SysCode C  on right(dbo.FuncGetGroupName(GroupName),2)=C.Remark collate Chinese_PRC_CS_AS_WS and C.TypeID='PmtType'
where  w.RecipeMaterialCode>'3' and recipestate = '1'  and BasMaterial.MaterialName is not null ";
        if (!(String.IsNullOrEmpty(material_group.Text) || material_group.Text =="全部"))
        { sql = sql + "and ( t2.Groupname = '" + material_group.SelectedItem.Value + pmttype.SelectedItem.Value + "' or    BasMaterial.MaterialName  like '%" + material_group.SelectedItem.Text + "%' ) "; }
        sql = sql + " group by w.RecipeMaterialCode,s.remark,S.ItemName,t2.GroupName  ,BasMaterial.MaterialName ,isnull(B.materialname,'') +isnull(C.ItemName,'')  order by BasMaterial.MaterialName";
        DataSet ds = manager.GetBySql(sql).ToDataSet();
        DataTable data = ds.Tables[0];

        int total = data.Rows.Count;
        return new { data, total };
    }
    #endregion

    #region 增删改查按钮激发的事件
    /// <summary>
    /// 点击添加按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    [Ext.Net.DirectMethod()]
    protected void btn_add_Click(object sender, DirectEventArgs e)
    {

        //if (String.IsNullOrEmpty(material_group.Text))
        //{ X.Js.Alert("请选择要等同的物料！"); return; }
        //if (String.IsNullOrEmpty(pmttype.Text))
        //{ X.Js.Alert("请选择要等同的物料类型！"); return; }
     string json = e.ExtraParams["Values"];
   
        Dictionary<string, string>[] materialDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        List<object> data = new List<object>();
        List<BasMaterial> materialList = new List<BasMaterial>();
        string mname = "";
        String groupname = material_group.SelectedItem.Value + pmttype.SelectedItem.Value;
        if (material_group.SelectedItem.Value == "") groupname = "";
         if (   material_group.Text =="全部") groupname = "";

     
        foreach (Dictionary<string, string> row in materialDic)
        {


            String sql = "select * from PmtRecipeIntoDT where MaterialID = '" + row["RecipeMaterialCode"] + "' and RecipeType = '" + row["RecipeType"] + "'";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                sql = "update PmtRecipeIntoDT set GroupName = '" + groupname + "' where materialid ='" + row["RecipeMaterialCode"] + "' and RecipeType = '" + row["RecipeType"] + "'";
                manager.GetBySql(sql).ToDataSet();
            }
            else
            {
                sql = "insert into PmtRecipeIntoDT values ('" + row["RecipeMaterialCode"] + "','" + row["RecipeType"] + "','" + groupname + "')";
                manager.GetBySql(sql).ToDataSet();
            }
            
            
            
            
            
            
            
            
            
            //X.Js.Alert(row["RecipeMaterialCode"]);

            //return;
        
        
        
        
        }
        pageToolBar.DoRefresh();
    }









    #endregion


}