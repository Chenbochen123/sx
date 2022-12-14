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
using System.Text;
public partial class Manager_Technology_XCSet : Mesnac.Web.UI.Page
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


            修改 = new SysPageAction() { ActionID = 4, ActionName = "Edit" };


            添加 = new SysPageAction() { ActionID = 7, ActionName = "btn_add" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public

        public SysPageAction 修改 { get; private set; } //必须为 public

        public SysPageAction 添加 { get; private set; } //必须为 public
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

           
          

        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<BasMaterialMajorType> GetPageResultData(PageResult<BasMaterialMajorType> pageParams)
    {
        BasMaterialMajorTypeManager.QueryParams queryParams = new BasMaterialMajorTypeManager.QueryParams();
        queryParams.pageParams = pageParams;
        //queryParams.objID = txt_obj_id.Text.TrimEnd().TrimStart();

        //queryParams.deleteFlag = hidden_delete_flag.Text;

        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<BasMaterialMajorType> GetTablePageDataBySql(BasMaterialMajorTypeManager.QueryParams queryParams)
    {
        PageResult<BasMaterialMajorType> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@" select * from pmtRecipeXC");
        //if (!string.IsNullOrEmpty(queryParams.deleteFlag))
        //{
        //    sqlstr.AppendLine(" AND BasMaterialDL.DeleteFlag ='" + queryParams.deleteFlag + "'");
        //}

        pageParams.QueryStr = sqlstr.ToString();
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

        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasMaterialMajorType> pageParams = new PageResult<BasMaterialMajorType>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "Objid ASC";

        PageResult<BasMaterialMajorType> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];




        int total = lst.RecordCount;
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
    protected void btn_add_Click(object sender, EventArgs e)
    {
        TextID.Text = "";
      
       
       
        Addnum.SetValue("");
        AddRecipe.SetValue("");
        AddXC.SetValue("");
        AddRecipe2.SetValue("");
        AddXC2.SetValue("");

        this.winAdd.Show();
    }


    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string objID)
    {
        TextID.Text = objID;
        String sql = "select * from pmtRecipeXC where objid = '" + objID + "'";
        DataSet ds = manager.GetBySql(sql).ToDataSet();
      DataRow dr=  ds.Tables[0].Rows[0];
        Addnum.SetValue(dr["XCNum"].ToString());
        AddRecipe.SetValue(dr["MaterialName"].ToString());
        AddXC.SetValue(dr["XCName"].ToString());
        AddRecipe2.SetValue(dr["MaterialCode"].ToString());
        AddXC2.SetValue(dr["XCCode"].ToString());

        this.winAdd.Show();
        //this.winModify.Show();
    }

    /// <summary>
    /// 点击恢复激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_recover(string obj_id, string MaterialCode, string EquipCode)
    {
        try
        {
            //String sql = "update BasMaterialDL set deleteflag = '1' where MaterialCode = '" + MaterialCode + "' and EquipCode = '" + EquipCode + "'";
            //DataSet ds = manager.GetBySql(sql).ToDataSet();
            String sql = "update BasMaterialDL set deleteflag = '0' where objid = '" + obj_id + "'";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("定量包装恢复", "定量包装编码：" + obj_id);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "恢复失败：" + e;
        }
        return "恢复成功";
    }

    /// <summary>
    /// 点击删除触发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string objID)
    {
        try
        {
            String sql = "delete from  pmtRecipeXC  where objid = '" + objID + "'";
            DataSet ds = manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("配方洗车设置删除", "配方洗车设置编码：" + objID);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }


    /// <summary>
    /// 点击取消按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winAdd.Close();
        //this.winModify.Close();
    }

    /// <summary>
    /// 点击添加信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    [Ext.Net.DirectMethod()]
    public void BtnAddSave_Click()
    {
        try
        {


            //if (Addnum.Text == "")
            //{ msg.Alert("操作", "请输入洗车配方车数"); return; }

            if (String.IsNullOrEmpty(TextID.Text))
            {
                String sql = "select isnull(max (ObjID),0)+1 from pmtRecipeXC ";
                DataSet ds = manager.GetBySql(sql).ToDataSet();
                String i = ds.Tables[0].Rows[0][0].ToString();

                sql = String.Format("insert into pmtRecipeXC values('{0}','{1}','{2}','{3}','{4}','{5}')",
                i, AddRecipe2.Text, AddRecipe.Text, AddXC2.Text, AddXC.Text, Addnum.Text);

                manager.GetBySql(sql).ToDataSet();
                //this.AppendWebLog("hiddenMaterCode维护", "内容：" + hiddenMaterCode.Text + ComboBox1.Text);
                pageToolBar.DoRefresh();
                this.winAdd.Close();
                msg.Alert("操作", "保存成功");
                msg.Show();
            }
            else
            {
                String sql = "update pmtRecipeXC  set materialcode ='" + AddRecipe2.Text + "',materialname = '" + AddRecipe.Text +
                    "' , XCCode='" + AddXC2.Text + "',XCName = '" + AddXC.Text + "',XCNum='" + Addnum.Text + "' where objid = '" + TextID.Text + "'";
                DataSet ds = manager.GetBySql(sql).ToDataSet();
                pageToolBar.DoRefresh();
                this.winAdd.Close();
                msg.Alert("操作", "保存成功");
                msg.Show();
            
            
            }
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 点击修改信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>object sender, EventArgs e)
    /// <param name="e"></param>
    [Ext.Net.DirectMethod()]
    public void BtnModifySave_Click()
    {
        String remark = "";
        try
        {




            String sql = "";

            sql = "update BasAlarmItem set Remark = '" + TextField9.Text + "',ZiAddress = '" + NumberField1.Text + "',WeiAddress = '" + NumberField2.Text
                + "',AlarmNo = '" + TextField6.Text + "',AlarmName = '" + TextField7.Text + "',AlarmAddress = '" + TextField8.Text
                + "' where objid = '" + modify_ID.Text + "' ";
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("报警信息修改", "编码：" + modify_ID.Text);
            pageToolBar.DoRefresh();
            this.winModify.Close();
            msg.Alert("操作", "更新成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "更新失败：" + ex);
            msg.Show();
        }
    }

    [Ext.Net.DirectMethod()]
    public void AddMaterial(string MaterialID, string MaterialName)
    {
        String s = MaterialID + ",";
        String sn = MaterialName + "\n";
        if (AddRecipe2.Text.IndexOf(s) >= 0)
        {
            AddRecipe2.Text = AddRecipe2.Text.Replace(s, "");
            if (AddRecipe.Text.LastIndexOf(sn) == 0)
            {
                AddRecipe.Text = "\n" + AddRecipe.Text;
                AddRecipe.Text = AddRecipe.Text.Replace("\n" + sn, "");
            }
            else
            {
                AddRecipe.Text = AddRecipe.Text.Replace("\n" + sn, "\n");
            }
        }
        else
        {
            AddRecipe2.Text = AddRecipe2.Text + s;

            AddRecipe.Text = AddRecipe.Text + sn;
        }
    }
    [Ext.Net.DirectMethod()]
    public void AddXCRecipe(string MaterialID, string MaterialName)
    {
        String s = MaterialID + ",";
        String sn = MaterialName + "\n";
        if (AddXC2.Text.IndexOf(s) >= 0)
        {
            AddXC2.Text = AddXC2.Text.Replace(s, "");
            if (AddXC.Text.LastIndexOf(sn) == 0)
            {
                AddXC.Text = "\n" + AddXC.Text;
                AddXC.Text = AddXC.Text.Replace("\n" + sn, "");
            }
            else
            {
                AddXC.Text = AddXC.Text.Replace("\n" + sn, "\n");
            }
        }
        else
        {
            AddXC2.Text = AddXC2.Text + s;

            AddXC.Text = AddXC.Text + sn;
        }
    }

    [Ext.Net.DirectMethod()]
    public void ClearMaterial(object sender, EventArgs e)
    {
        AddRecipe.Text = "";
        AddRecipe2.Text = "";

    }

    [Ext.Net.DirectMethod()]
    public void ClearXC(object sender, EventArgs e)
    {
        AddXC.Text = "";
        AddXC2.Text = "";

    }
    #endregion



}