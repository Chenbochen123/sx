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

public partial class Manager_BasicInfo_MaterialInfo_RecipeInfo : Mesnac.Web.UI.Page
{
    protected BasMaterialMajorTypeManager manager = new BasMaterialMajorTypeManager();//业务对象
    protected BasMaterialMinorTypeManager minorManager = new BasMaterialMinorTypeManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<BasMaterialMajorType> entityList;


    #region 权限定义
    protected __ _ = new __();
    private object colModel2;

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
            String sql = @"select * from Pmt_material order by mater_name";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            Ext.Net.ListItem li = new       Ext.Net.ListItem("全部","");
            Fmaterial.Items.Add(li);
            Smaterial.Items.Add(li);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                li = new Ext.Net.ListItem(dr["Mater_name"].ToString(), dr["Mater_Code"].ToString());
                Fmaterial.Items.Add(li);
                Smaterial.Items.Add(li);
            
            
            
            }

          

        }
    }
    #endregion

    #region 分页相关方法
   

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        String sql = @"select t1.F_MaterCode '物料ID',t2.Mater_name as '物料',t1.S_MaterCode '被等同ID',t3.Mater_name as '被等同物料' from Pmt_MaterEqual t1
left join Pmt_material t2 on t1.F_MaterCode=t2.Mater_code
left join Pmt_material t3 on t1.S_MaterCode=t3.Mater_code  where 1=1 ";
        if (Fmaterial.Text.ToString() != "全部")
        {
            if (!string.IsNullOrEmpty(Fmaterial.Value.ToString()))
            {
                sql = sql + " and t1.F_MaterCode = '" + Fmaterial.Value.ToString() + "'";
            }
        }
        if (Smaterial.Text.ToString() != "全部")
        {
            if (!string.IsNullOrEmpty(Smaterial.Value.ToString()))
            {
                sql = sql + " and t1.S_MaterCode = '" + Smaterial.Value.ToString() + "'";
            }
        }
//        sql = sql + " order by MaterialName,r.RecipeType";
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

        if (string.IsNullOrEmpty(Fmaterial.Value.ToString()))
        {
            X.Js.Alert("请选择物料");

            return;
        }
        if (string.IsNullOrEmpty(Smaterial.Value.ToString()))
        {
            X.Js.Alert("请被等同选择物料");

            return;
        }
            String sql = "select * from Pmt_MaterEqual where F_matercode = '" + Fmaterial.Value + "' and S_matercode = '" + Smaterial.Value + "'";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                X.Js.Alert("当前对应关系已存在");

                return;
            }
            else
            {
                sql = "insert into Pmt_MaterEqual values ('" + Fmaterial.Value + "','" + Smaterial.Value + "','','')";
                manager.GetBySql(sql).ToDataSet();
                X.Js.Alert("添加成功");
                this.AppendWebLog("添加等同物料", "物料代码：" + Fmaterial.Value + "  " + Smaterial.Value);
                pageToolBar.DoRefresh();
                return;
            }
            
       
    }
    [Ext.Net.DirectMethod()]
    protected void btn_del_Click(object sender, DirectEventArgs e)
    {
        string json = e.ExtraParams["Values"];
        Dictionary<string, string>[] materialDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        foreach (Dictionary<string, string> row in materialDic)
        {

            String sql = "delete from Pmt_MaterEqual where F_Matercode ='" + row["物料ID"] + "' and S_Matercode = '" + row["被等同ID"] + "'";
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("删除等同物料", "物料代码：" + row["物料ID"] + "  " + row["被等同ID"]);
        }
        pageToolBar.DoRefresh();
        X.Js.Alert("删除成功");

    }








    #endregion


}