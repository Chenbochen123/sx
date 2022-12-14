using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Entity;
using Ext.Net;
using Mesnac.Business.Implements;
using NBear.Common;
using System.Text.RegularExpressions;
using Mesnac.Data.Components;
using System.Data;
using System.Text;

public partial class Manager_ProducingPlan_BalanceCheck : Mesnac.Web.UI.Page
{
    protected Ppt_GeLiJiManager manager = new Ppt_GeLiJiManager();
    protected BasEquipManager equipmanager = new BasEquipManager();
    protected Pmt_materialManager matermanager = new Pmt_materialManager();
    protected PptClassManager classmanager = new PptClassManager();
    protected BasUserManager usermanager = new BasUserManager();
    protected Ppt_balanceCheckManager balancemanager = new Ppt_balanceCheckManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            bindclass();
            bindequip();
            datestart.SelectedDate = DateTime.Now;
            dateend.SelectedDate = DateTime.Now;
            bindList();
        }
    }


    #region 初始化控件

    private void bindclass()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<PptClass> list = classmanager.GetListByWhere(where);
        foreach (PptClass main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.ClassName, main.ObjID);
            cbxclass.Items.Add(item);
            cbxclass2.Items.Add(item);
        }
    }
    private void bindequip()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<BasEquip> list = equipmanager.GetListByWhere(where);
        foreach (BasEquip main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.EquipName, main.EquipCode);
            cbxequip.Items.Add(item);
            cbxequip2.Items.Add(item);
        }
    }


    #endregion



    private DataSet getList()
    {

        return GetDataByParas();
    }

    
    public System.Data.DataSet GetDataByParas()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"SELECT t.serialid,Plan_Date '生产日期',T1.Equip_name '机台',T2.ClassName '班组',T.scale_name '秤名称',T.scale_no '编号',set_weight '标准重量',error_allow '允许误差',real_weight '实际重量',T.operTime '校准时间',T.usercode '校准人' from ppt_balanceCheck T
LEFT JOIN Pmt_equip T1 ON T1.Equip_code=T.equip_Code
LEFT JOIN PptClass T2 ON T2.ObjID=T.shift  where 1=1");

        if (!string.IsNullOrEmpty(datestart.Text))
        {
            sb.AppendLine("AND T.Plan_Date>='" + datestart.SelectedDate.ToString("yyyy-MM-dd") + "'");
        }
        if (!string.IsNullOrEmpty(dateend.Text))
        {
            sb.AppendLine("AND T.Plan_Date<='" + dateend.SelectedDate.ToString("yyyy-MM-dd") + "'");
        }
        #endregion

        NBear.Data.CustomSqlSection css = manager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }


    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }

    #region 按钮事件响应
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindList();
    }
    protected void btnExportSubmit_Click(object sender, DirectEventArgs e)
    {
        string fields = e.ExtraParams["fields"];
        string records = e.ExtraParams["records"];
        Newtonsoft.Json.JavaScriptArray jsArrayFields = Newtonsoft.Json.JavaScriptConvert.DeserializeObject(fields) as Newtonsoft.Json.JavaScriptArray;
        Newtonsoft.Json.JavaScriptArray jsArrayRecords = Newtonsoft.Json.JavaScriptConvert.DeserializeObject(records) as Newtonsoft.Json.JavaScriptArray;

        DataTable dt = new DataTable();

        foreach (Newtonsoft.Json.JavaScriptObject jsObjectField in jsArrayFields)
        {
            if (jsObjectField["name"].ToString().ToLower() != "id")
            {
                dt.Columns.Add(new DataColumn(jsObjectField["name"].ToString(), typeof(string)));
            }
        }

        foreach (Newtonsoft.Json.JavaScriptObject jsObjectRecord in jsArrayRecords)
        {
            DataRow dr = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                dr[dc.ColumnName] = jsObjectRecord[dc.ColumnName];
            }
            dt.Rows.Add(dr);
        }

        if (dt.Rows.Count == 0)
        {
            X.Msg.Alert("提示", "没有找到符合条件的记录").Show();
            return;
        }

        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(dt, "校秤记录导出");
    }

    #endregion

    /// <summary>
    /// 点击添加按钮激发的事件
    /// sunyj   2013年3月27日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        txtdate.SelectedDate = DateTime.Now;
        this.cbxclass.Text = "";
        this.cbxequip.Text = "";
        this.txtno.Text = "";
        this.txtbiaozhun.Text = "";
        this.txtwucha.Text = "";
        this.txtshiji.Text = "";
        btnAddSave.Disable(true);
        this.AddConfigWin.Show();
    }


    /// <summary>
    /// 点击添加信息中保存按钮激发的事件
    /// sunyj   2013年3月27日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
        string date1 = "";
        string user = this.UserID;
        string username = "";
        WhereClip where = new WhereClip();
        EntityArrayList<BasUser> list = usermanager.GetListByWhere(BasUser._.WorkBarcode == user);
        if (list.Count > 0)
        { username = list[0].UserName; }

        date1 = txtdate.SelectedDate.ToString("yyyy-MM-dd");
        try
        {
            string sqlstr = @"insert into ppt_balanceCheck values('" + cbxequip.SelectedItem.Value + "','" + date1 + "','" + cbxclass.SelectedItem.Value + "','" + txtbiaozhun.Text + "','" + txtshiji.Text + "','" + txtwucha.Text + "','" + txtno.Text + "','" + txtcheng.Text + "','" + username + "','0',GETDATE())";
            manager.GetBySql(sqlstr).ToDataSet();

            bindList();
            X.Msg.Alert("提示", "添加成功").Show();
            //pageToolBar.DoRefresh();
            this.AddConfigWin.Close();
        }
        catch (Exception)
        {
            throw;
        }

    }

    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.AddConfigWin.Close();
    }


    //点击修改
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string barcode)
    {
        Ppt_balanceCheck config = balancemanager.GetById(barcode);
        this.txtdate2.Text = config.Plan_Date;
        WhereClip where = new WhereClip();
        //EntityArrayList<PptClass> list = classmanager.GetListByWhere(PptClass._.ObjID==config.Shift);
        //if(list.Count>0)
        //{ this.cbxclass2.Text = list[0].ClassName; }
        this.cbxclass2.Value = config.Shift;

        //EntityArrayList<BasEquip> list1 = equipmanager.GetListByWhere(BasEquip._.EquipCode == config.Equip_Code);
        //if (list1.Count > 0)
        //{ this.cbxequip2.Text = list1[0].EquipName;; }
        this.cbxequip2.Value = config.Equip_Code;
       
        this.txtcheng2.Text = config.Scale_name;
        this.txtno2.Text = config.Scale_no;
        this.txtbiaozhun2.Text = config.Set_weight.ToString();
        this.txtwucha2.Text = config.Error_allow.ToString();
        this.txtshiji2.Text = config.Real_weight.ToString();
        hidden_update_barcode.Text = barcode;
        this.ModifyConfigWin.Show();
    }
    //修改数据
    public void BtnModifySave_Click(object sender, DirectEventArgs e)
    {

        try
        {
            string sqlstr = @"update ppt_balanceCheck set equip_Code='" + cbxequip2.Value + "',Plan_Date='" + txtdate2.SelectedDate.ToString("yyyy-MM-dd") + "',shift='" + cbxclass2.Value + "',set_weight='" + txtbiaozhun2.Text + "',real_weight='" + txtshiji2.Text + "',error_allow='" + txtwucha2.Text + "',scale_no='" + txtno2.Text + "',scale_name='" + txtcheng2.Text + "' where serialid='" + hidden_update_barcode.Text + "'";
            manager.GetBySql(sqlstr).ToDataSet();

            bindList();
            X.Msg.Alert("提示", "修改成功").Show();
            this.ModifyConfigWin.Close();
        }
        catch (Exception)
        {
            throw;
        }

    }
    public void BtnModifyCancel_Click(object sender, DirectEventArgs e)
    {
        this.ModifyConfigWin.Close();
    }

      /// <summary>
    /// 点击删除触发的事件
    /// sunyj   2013年3月27日
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string barcode)
    {
        try
        {
            balancemanager.Delete(barcode);
            bindList();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }

}