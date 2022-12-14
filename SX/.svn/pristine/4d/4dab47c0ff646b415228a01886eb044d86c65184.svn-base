using System;
using System.Collections.Generic;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Data.Components;
using System.Data;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;


/// <summary>
/// Manager_Technology_Report_LotReport 实现类
/// 孙本强 @ 2013-04-03 13:17:21
/// </summary>
/// <remarks></remarks>
public partial class Manager_Technology_Manage_PptLot : Mesnac.Web.UI.Page
{

    #region 权限定义
    protected __ _ = new __();
    public string[] COLORS = new string[] { "#ff0000", "#00ff00", "#0000FF", "#000000", "#999999", "#9900FF" };
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btnSearch," };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
    }
    #endregion

    #region 属性注入
    private IPptClassManager pptClassManager = new PptClassManager();
    private IPptShiftManager pptShiftManager = new PptShiftManager();
    private IPptLotManager Manager = new PptLotManager();
    private IBasEquipManager equipManager = new BasEquipManager();
    private IBasMaterialManager materManager = new BasMaterialManager();
    private IPptLotDataManager pptLotDataManager = new PptLotDataManager();
    #endregion


    #region 页面初始化
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:17:22
    /// </summary>
    private const string constSelectAllText = "";
    private void IniComboBox(ComboBox cb, EntityArrayList<SysCode> lst)
    {
        Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
        cb.Items.Clear();
        cb.Items.Add(allitem);
        foreach (SysCode m in lst)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(m.ItemName, m.ItemCode.ToString());
            cb.Items.Add(item);
        }
        if (cb.Items.Count > 0)
        {
            cb.Text = (cb.Items[0].Value);
        }
    }
    private void btnDisabled()
    {
    }
    /// <summary>
    /// 页面初始化
    /// 孙本强 @ 2013-04-03 13:39:51
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (X.IsAjaxRequest)
        {
            return;
        }
        cbxtype.SelectedItem.Value = "1";
        btnDisabled();
        Date.Text = DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");
        WhereClip where = new WhereClip();
        OrderByClip order = new OrderByClip();
        where = new WhereClip();
        order = new OrderByClip();
        where.And(PptClass._.UseFlag == "1");
        order = PptClass._.ObjID.Asc;
        Ext.Net.ListItem allitem = new Ext.Net.ListItem(constSelectAllText, constSelectAllText);
        cbxclass.Items.Clear();
        cbxclass.Items.Add(allitem);
        foreach (PptClass m in pptClassManager.GetListByWhereAndOrder(where, order))
        {
            cbxclass.Items.Add(new ListItem(m.ClassName, m.ObjID));
        }
        cbxclass.Text = (cbxclass.Items[0].Value);
        bindmater();
        bindequip();

    }
    private void bindmater()
    {
        WhereClip where = new WhereClip();

        EntityArrayList<BasMaterial> list = materManager.GetListByWhere(where);
        foreach (BasMaterial main in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.MaterialName, main.MaterialCode);
            cbxmater.Items.Add(item);
        }
    }
    private void bindequip()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<BasEquip> list2 = equipManager.GetListByWhere(where);
        foreach (BasEquip main in list2)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(main.EquipName, main.EquipCode);
            cbxequip.Items.Add(item);
        }
    }

    #endregion

    /// <summary>
    /// 第一条
    /// 孙本强 @ 2013-04-03 13:17:25
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="Ext.Net.DirectEventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void btnSearchFirstClick(object sender, DirectEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(cbxequip.Text))
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请选择机台！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return ;
        }
        if (string.IsNullOrWhiteSpace(cbxclass.Text))
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请选择班组！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return ;
        }
        if (string.IsNullOrWhiteSpace(cbxmater.Text))
        {
            X.Msg.Show(new MessageBoxConfig { Title = "错误提示", Message = "请选择物料！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.ERROR });
            return ;
        }

        if (cbxtype.SelectedItem.Value == "1")
        {
            Panel1.Hidden = false;
            Panel2.Hidden = true;
            Panel4.Hidden = true;
            Panel6.Hidden = true;
        }
        else if (cbxtype.SelectedItem.Value == "2")
        {
            Panel1.Hidden = true;
            Panel2.Hidden = false;
            Panel4.Hidden = true;
            Panel6.Hidden = true;
        }
        if (cbxtype.SelectedItem.Value == "3")
        {
            Panel1.Hidden = true;
            Panel2.Hidden = true;
            Panel4.Hidden = false;
            Panel6.Hidden = true;
        }
        if (cbxtype.SelectedItem.Value == "4")
        {
            Panel1.Hidden = true;
            Panel2.Hidden = true;
            Panel4.Hidden = true;
            Panel6.Hidden = false;
        }

        DataSet ds = Getcurve();
        DataSet ds2 = Getcurve2();
        Store store = this.Chart1.GetStore();
        store.DataSource = ds.Tables[0];
        store.DataBind();
        Store store2 = this.Chart2.GetStore();
        store2.DataSource = ds.Tables[0];
        store2.DataBind();
        Store store3 = this.Chart3.GetStore();
        store3.DataSource = ds.Tables[0];
        store3.DataBind();
        Store store4 = this.Chart4.GetStore();
        store4.DataSource = ds2.Tables[0];
        store4.DataBind();
    }

    public DataSet Getcurve()

    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"
  select * from PptLot where Equip_Code='"+cbxequip.SelectedItem.Value+"' and Mater_Code='"+cbxmater.SelectedItem.Value+"' and Shift_Class='"+cbxclass.SelectedItem.Value+"' and Plan_Date='"+Date.SelectedDate.ToString("yyyy-MM-dd")+"'");
        
        #endregion

        NBear.Data.CustomSqlSection css = Manager.GetBySql(sb.ToString());
        return css.ToDataSet();

    }
    public DataSet Getcurve2()
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"
  select row_number() over(order by Prod_Date) as row_number,* from ppt_shiftconfig where Equip_Code='" + cbxequip.SelectedItem.Value + "' and Mater_Code='" + cbxmater.SelectedItem.Value + "' and Shift_Class='" + cbxclass.SelectedItem.Value + "' and Plan_Date='" + Date.SelectedDate.ToString("yyyy-MM-dd") + "'");

        #endregion

        NBear.Data.CustomSqlSection css = Manager.GetBySql(sb.ToString());
        return css.ToDataSet();

    }

}