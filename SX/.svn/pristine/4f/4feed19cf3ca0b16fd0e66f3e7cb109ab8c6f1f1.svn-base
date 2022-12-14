using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using System.Text.RegularExpressions;
using Mesnac.Data.Components;
using System.Data;
using Mesnac.Business.Interface;
using System.Text;

public partial class Manager_Technology_RubWeightSetting_RubWeightSetting : Mesnac.Web.UI.Page
{
    protected PmtRubWeightSettingManager manager = new PmtRubWeightSettingManager();//业务对象
    protected BasEquipManager equipManager = new BasEquipManager();
    protected SysCodeManager sysCodeManager = new SysCodeManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btn_search" };
            全部锁定 = new SysPageAction() { ActionID = 2, ActionName = "btn_all_lock" };
            全部不锁 = new SysPageAction() { ActionID = 3, ActionName = "btn_all_unlock" };
            修改 = new SysPageAction() { ActionID = 4, ActionName = "Edit" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 全部锁定 { get; private set; } //必须为 public
        public SysPageAction 全部不锁 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
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
            EntityArrayList<SysCode> stateList = sysCodeManager.GetListByWhere(SysCode._.TypeID == "EquipState");
            foreach (SysCode state in stateList)
            {
                txt_state.AddItem(state.ItemName, state.ItemCode);
                modify_state.AddItem(state.ItemName, state.ItemCode);
            }
            EntityArrayList<SysCode> weightSettingList = sysCodeManager.GetListByWhere(SysCode._.TypeID == "WeightCtrl");
            foreach (SysCode weightSetting in weightSettingList)
            {
                modify_rub_weight_ctrl.AddItem(weightSetting.ItemName, weightSetting.ItemCode);
            }
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<PmtRubWeightSetting> GetPageResultData(PageResult<PmtRubWeightSetting> pageParams)
    {
        PmtRubWeightSettingManager.QueryParams queryParams = new PmtRubWeightSettingManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.equipCode = txt_equip_code.Text.TrimEnd().TrimStart();
        queryParams.state = txt_state.Text.TrimEnd().TrimStart();
        queryParams.remark = txt_remark.Text.TrimEnd().TrimStart();
        queryParams.workshopID = hidden_workshop_code.Value.ToString();

        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<PmtRubWeightSetting> GetTablePageDataBySql(PmtRubWeightSettingManager.QueryParams queryParams)
    {
        PageResult<PmtRubWeightSetting> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@" SELECT rws.ObjID , equip.EquipName as EquipName , equip.EquipCode as EquipCode ,  lvl.ItemName as State , rws.EquipElectricCurrent , 
                                        lvl2.ItemName as WeightSettingCtrl , rws.LockType, rws.DeleteFlag , rws.Remark  , equip.workshopCode
                                 FROM PmtRubWeightSettingNewOne rws
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
        PageResult<PmtRubWeightSetting> pageParams = new PageResult<PmtRubWeightSetting>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = prms.Sort[0].Property + " " + prms.Sort[0].Direction;

        PageResult<PmtRubWeightSetting> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
   
    #endregion

    #region 打印
    /// <summary>
    /// 打印调用方法
    /// yuany 2013年3月2日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        PageResult<PmtRubWeightSetting> pageParams = new PageResult<PmtRubWeightSetting>();
        pageParams.PageSize = -100;
        PageResult<PmtRubWeightSetting> lst = GetPageResultData(pageParams);
        for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.DataSet.Tables[0].Columns[i];
            foreach (ColumnBase cb in this.pnlList.ColumnModel.Columns)
            {
                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()))
                {
                    dc.ColumnName = cb.Text;
                    isshow = true;
                    break;
                }
            }
            if (!isshow)
            {
                lst.DataSet.Tables[0].Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "单位信息");
    }
    #endregion

    #region 增删改查按钮激发的事件

    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string objID)
    {
        PmtRubWeightSetting rubWeightSetting = showWindow(objID);// manager.GetById(Convert.ToInt32(objID));
        modify_obj_id.Text = rubWeightSetting.ObjID.ToString();
        modify_equip_code.Text = equipManager.GetListByWhere(BasEquip._.EquipCode == rubWeightSetting.EquipCode)[0].EquipName;
        modify_state.Text = rubWeightSetting.State;
        modify_equip_electric_current.Text = rubWeightSetting.EquipElectricCurrent.ToString();
        modify_rub_weight_ctrl.Text = rubWeightSetting.WeightSettingCtrl;
        modify_lock_type.Text = rubWeightSetting.LockType;
        modify_lock_type_old.Text = rubWeightSetting.LockType;
        modify_remark.Text = rubWeightSetting.Remark;
        this.winModify.Show();
    }
    public PmtRubWeightSetting showWindow(string objid)
    {

        string sql = "select * from PmtRubWeightSettingNewone where objid='" + objid + "'";
        DataSet ds = manager.GetBySql(sql).ToDataSet();

        PmtRubWeightSetting ps = new PmtRubWeightSetting();
        ps.ObjID = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        ps.EquipCode = ds.Tables[0].Rows[0][1].ToString();
        ps.State = ds.Tables[0].Rows[0][2].ToString();
        ps.EquipElectricCurrent = Convert.ToInt32(ds.Tables[0].Rows[0][3].ToString());
        ps.WeightSettingCtrl = ds.Tables[0].Rows[0][4].ToString();
        ps.LockType = ds.Tables[0].Rows[0][7].ToString();
        ps.Remark = ds.Tables[0].Rows[0][6].ToString();
        return ps;
    }
    /// <summary>
    /// 点击取消按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winModify.Close();
    }

    /// <summary>
    /// 点击修改信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
        try
        {
            PmtRubWeightSetting rubWeightSetting = manager.GetById(modify_obj_id.Text);
            rubWeightSetting.State = modify_state.Text;
            try
            {
                rubWeightSetting.EquipElectricCurrent = Convert.ToInt32(modify_equip_electric_current.Text);
            }
            catch (Exception)
            {
                msg.Alert("操作", "更新失败：设备电流需为整数!");
                msg.Show();
                return;
            }
            rubWeightSetting.WeightSettingCtrl = modify_rub_weight_ctrl.Text;
            rubWeightSetting.LockType = modify_lock_type.Text;
            rubWeightSetting.Remark = (string)(modify_remark.Text);




            string equip = getEquipCode(modify_equip_code.Text);
            rubWeightSetting.EquipCode = equip;

            string equipType = equip.Substring(1, 1);
            if (equipType == "1")
            {
                updateSetting(rubWeightSetting);
            }
            else
            {
                manager.Update(rubWeightSetting);
            }










            //manager.Update(rubWeightSetting);




            //updateSetting(rubWeightSetting);
            string modify_lock_typeLog = string.Empty;
            if (modify_lock_type.Text != modify_lock_type_old.Text)
            {
                modify_lock_typeLog = ";物料锁定由" + modify_lock_type_old.Text + "改为" + modify_lock_type.Text;
            }
            this.AppendWebLog("胶料称锁定信息修改:" + modify_equip_code.Text + " " + modify_rub_weight_ctrl.SelectedItem.Text + modify_lock_typeLog, "自动编号：" + modify_obj_id.Text + " " + modify_rub_weight_ctrl.SelectedItem.Text);
            pageToolBar.DoRefresh();
            this.winModify.Close();
            msg.Notify("操作", "更新成功").Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "更新失败：" + ex);
            msg.Show();
        }
    }
    public string getEquipCode(string equipName)
    {
        string str = "select EquipCode from BasEquip where EquipName='" + equipName + "'";
        DataSet ds = manager.GetBySql(str).ToDataSet();
        str = ds.Tables[0].Rows[0][0].ToString();
        return str;
    }
    public void updateSetting(PmtRubWeightSetting rubWeightSetting)
    {
        string str = "update PmtRubWeightSetting" + rubWeightSetting.EquipCode + "  set EquipElectricCurrent='" + rubWeightSetting.EquipElectricCurrent + "',WeightSettingCtrl='" + rubWeightSetting.WeightSettingCtrl + "', LockType='" + rubWeightSetting.LockType + "',Remark='" + rubWeightSetting.Remark + "'";
        manager.GetBySql(str).ToDataSet();

    }

    [Ext.Net.DirectMethod()]
    public string equip_lock_func(string ctrl)
    {
        try
        {
            EntityArrayList<PmtRubWeightSetting> rubWeightSettingList = manager.GetAllList();
            foreach (PmtRubWeightSetting item in rubWeightSettingList)
            {
                item.WeightSettingCtrl = ctrl;
                manager.Update(item);
            }
            return "设置成功!";
        }
        catch (Exception)
        {

            return "设置出错!";
        }
      
    }
    #endregion
}