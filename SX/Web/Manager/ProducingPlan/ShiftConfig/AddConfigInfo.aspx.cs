using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using NBear.Common;
using System.Data;
using Mesnac.Entity;
using Mesnac.Business.Interface;
using Mesnac.Business.Implements;

public partial class Manager_ProducingPlan_ShiftConfig_AddConfigInfo : Mesnac.Web.UI.Page
{
    #region 属性注入
    private IPmtConfigManager pmtConfigManager = new PmtConfigManager();
    private IPptShiftConfigManager pptShiftConfigManager = new PptShiftConfigManager();
    private IPptShiftManager pptShiftManaer = new PptShiftManager();
    private IPptPlanManager pptPlanManager = new PptPlanManager();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //FillComboBox(PmtConfigManager.TypeCode.Equip, cboEquipCode);
        txtStratShiftDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        FillShift();
    }

    #region 页面初始化

    private void FillShift()
    {
        EntityArrayList<PptShift> lstShift = pptShiftManaer.GetListByWhere(PptShift._.UseFlag == 1);
        if (lstShift.Count >= 0)
        {
            foreach (PptShift shift in lstShift)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = shift.ShiftName;
                item.Value = shift.ObjID.ToString();
                cboShift.Items.Add(item);
            }
        }
    }
    private void FillComboBox(PmtConfigManager.TypeCode typeCode, Ext.Net.ComboBox cb)
    {
        WhereClip where = new WhereClip();
        where.And(PmtConfig._.DeleteFlag == 0);
        where.And(PmtConfig._.TypeCode == typeCode.ToString());
        string sqlstr = pmtConfigManager.GetListByWhere(where)[0].ItemInfo;
        DataSet data = pmtConfigManager.GetBySql(sqlstr).ToDataSet();
        cb.Items.Clear();
        if (data != null && data.Tables.Count > 0)
        {
            foreach (DataRow dr in data.Tables[0].Rows)
            {
                Ext.Net.ListItem item = new Ext.Net.ListItem();
                item.Text = dr["ShowInfo"].ToString();
                item.Value = dr["ValueInfo"].ToString();
                cb.Items.Add(item);
            }
        }
        //if (cb.Items.Count > 0)
        //{
        //    cb.Text = (cb.Items[0].Value);
        //}
    }
    #endregion

    /// <summary>
    /// 点击添加按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnAddConfig_Click(object sender, DirectEventArgs e)
    {
    }
}