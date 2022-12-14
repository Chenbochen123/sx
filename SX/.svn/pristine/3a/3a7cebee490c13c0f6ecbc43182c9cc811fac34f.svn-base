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

public partial class Manager_Technology_Manage_WLTH : Mesnac.Web.UI.Page
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
            编辑 = new SysPageAction() { ActionID = 1, ActionName = "btn_edit" };

        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 编辑 { get; private set; } //必须为 public

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
            Ext.Net.ListItem li = new Ext.Net.ListItem("全部", "");
            Fmaterial.Items.Add(li);
            Smaterial.Items.Add(li);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                li = new Ext.Net.ListItem(dr["Mater_name"].ToString(), dr["Mater_Code"].ToString());
                Fmaterial.Items.Add(li);
                Smaterial.Items.Add(li);



            }
            sql = @"select * from pmt_recipetype";
             ds = manager.GetBySql(sql).ToDataSet();
             li = new Ext.Net.ListItem("全部", "");
            CRetype.Items.Add(li);
           
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                li = new Ext.Net.ListItem(dr["Recipe_typename"].ToString(), dr["Recipe_type"].ToString());
                CRetype.Items.Add(li);
            
            }
        

        }
    }
    #endregion

    #region 分页相关方法


    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if ( string.IsNullOrEmpty(Fmaterial.Text.ToString())) return null;
        String sql = @"select pw.objid,pr.mater_name,pr.edt_code,bq.equipname,br.recipe_typename,pw.Set_weight,pw.Error_allow,pw.mater_name as Wname from Pmt_Weight pw
left join pmt_recipe pr on pw.recipeobjid = pr.objid
left join basequip bq on pw.equip_code =bq.equipcode
left join pmt_recipetype br on pr.recipe_type =br.recipe_type
where  pr.audit_flag ='1' ";
        if (Fmaterial.Text.ToString() != "全部")
        {
            if (!string.IsNullOrEmpty(Fmaterial.Value.ToString()))
            {
                sql = sql + " and  pw.mater_code = '" + Fmaterial.Value.ToString() + "'";
            }
        }

        if (CRetype.Text.ToString() != "全部")
        {
            if (!string.IsNullOrEmpty(CRetype.Value.ToString()))
            {
                sql = sql + " and  pr.recipe_type = '" + CRetype.Value.ToString() + "'";
            }
        }
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

      
        if (string.IsNullOrEmpty(Smaterial.Value.ToString()))
        {
            X.Js.Alert("请被目标物料");

            return;
        }
        string json = e.ExtraParams["Values"];
        Dictionary<string, string>[] materialDic = JSON.Deserialize<Dictionary<string, string>[]>(json);
        foreach (Dictionary<string, string> row in materialDic)
        {

            String sql = "update Pmt_Weight  set Mater_code = '" + Smaterial.Value.ToString() + "',child_code = '" + Smaterial.Value.ToString() + "' ,mater_name ='" + Smaterial.Text.ToString() + "' where objid ='" + row["objid"] + "'";
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("替换物料", "物料代码：" + row["objid"] + "  " + Smaterial.Value.ToString());
        }
        pageToolBar.DoRefresh();
        X.Js.Alert("替换成功");


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