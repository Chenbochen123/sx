using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using NBear.Common;
using Ext.Net;
using Mesnac.Data.Components;
using System.Data;
using System.Text.RegularExpressions;

public partial class Manager_BasicInfo_EquipmentInfo_EquipmentType : Mesnac.Web.UI.Page
{
    protected BasEquipTypeManager manager = new BasEquipTypeManager();//业务对象
    protected BasEquipManager equipManager = new BasEquipManager();//业务对象
    protected BasEquipPartInfoManager partInfoManager = new BasEquipPartInfoManager();//业务对象
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    private EntityArrayList<BasEquipType> entityList;



    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btn_search" };
            历史查询 = new SysPageAction() { ActionID = 2, ActionName = "btn_history_search" };
            导出 = new SysPageAction() { ActionID = 3, ActionName = "btnExport" };
            修改 = new SysPageAction() { ActionID = 4, ActionName = "Edit" };
            删除 = new SysPageAction() { ActionID = 5, ActionName = "Delete" };
            恢复 = new SysPageAction() { ActionID = 6, ActionName = "Recover" };
            添加 = new SysPageAction() { ActionID = 7, ActionName = "btn_add" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 历史查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 恢复 { get; private set; } //必须为 public
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
    private PageResult<BasEquipType> GetPageResultData(PageResult<BasEquipType> pageParams)
    {
        BasEquipTypeManager.QueryParams queryParams = new BasEquipTypeManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.objID = txt_obj_id.Text.TrimEnd().TrimStart();
        queryParams.equipTypeName = txt_equip_type_name.Text.TrimEnd().TrimStart();
        queryParams.remark = txt_remark.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = hidden_delete_flag.Text;

        return manager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (!Regex.IsMatch(txt_obj_id.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_obj_id.Text = "";
        }
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasEquipType> pageParams = new PageResult<BasEquipType>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasEquipType> lst = GetPageResultData(pageParams);
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
        add_equip_type_name.Text = "";
        hidden_equip_type_name.Text = "";
        add_remark.Text = "";
        btnAddSave.Disable(true);
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
        //BasEquipType equipType = manager.GetById(objID);
        //modify_obj_id.Text = equipType.ObjID.ToString();
        //modify_equip_type_name.Text = equipType.EquipTypeName;
        //hidden_equip_type_name.Text = equipType.EquipTypeName;
        //modify_remark.Text = equipType.Remark;

        string sql = "select * from Pmt_equipclass where Equip_class='"+ objID + "'";
        DataSet ds = manager.GetBySql(sql).ToDataSet();
        modify_obj_id.Text = ds.Tables[0].Rows[0][0].ToString();
        modify_equip_type_name.Text = ds.Tables[0].Rows[0][1].ToString();
        hidden_equip_type_name.Text = ds.Tables[0].Rows[0][1].ToString();
        modify_remark.Text = ds.Tables[0].Rows[0][2].ToString();

        this.winModify.Show();
    }

    /// <summary>
    /// 点击恢复激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_recover(string obj_id)
    {
        try
        {
            BasEquipType equipType = manager.GetById(obj_id);
            equipType.DeleteFlag = 0;
            manager.Update(equipType);
            this.AppendWebLog("设备类型恢复", "设备类型编码：" + equipType.ObjID);
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

            //EntityArrayList<BasEquip> equipList = equipManager.GetListByWhere(BasEquip._.EquipType == objID.PadLeft(2, '0') && BasEquip._.DeleteFlag == 0);
            //EntityArrayList<BasEquipPartInfo> equipPartInfoList = partInfoManager.GetListByWhere(BasEquipPartInfo._.EquipType == objID.PadLeft(2, '0') && BasEquipPartInfo._.DeleteFlag == 0);
            //if (equipList.Count > 0 || equipPartInfoList.Count > 0)
            //{
            //    return "删除失败：此设备类型已被使用，禁止删除！";
            //}
            //BasEquipType equipType = manager.GetById(objID);
            //equipType.DeleteFlag = "1";
            //manager.Update(equipType);

            string sql = "delete Pmt_equipclass where Equip_class='"+ objID + "'";
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("设备类型删除", "设备类型编码：" + objID);
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
        this.winModify.Close();
    }

    /// <summary>
    /// 点击添加信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            //添加校验重复
            EntityArrayList<BasEquipType> equipTypeList = manager.GetListByWhere(BasEquipType._.EquipTypeName == add_equip_type_name.Text.TrimStart().TrimEnd());
            if (equipTypeList.Count > 0)
            {
                X.Msg.Alert("提示", "此设备类型名称已被使用！").Show();
                return;
            }
            BasEquipType equipType = new BasEquipType();
            equipType.ObjID = manager.GetNextEquipTypeCode();
            equipType.EquipTypeName = (string)(add_equip_type_name.Text);
            equipType.Remark = (string)(add_remark.Text);
            equipType.DeleteFlag = 0;
            //manager.Insert(equipType);

            string sql = "insert into Pmt_equipclass values('"+ equipType.ObjID + "','"+ equipType.EquipTypeName + "','"+ equipType.Remark + "')";
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("设备类型添加", "设备类型编码：" + equipType.ObjID);
            pageToolBar.DoRefresh();
            this.winAdd.Close();
            msg.Alert("操作", "保存成功");
            msg.Show();
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
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
        try
        {
            //修改重复校验
            //EntityArrayList<BasEquipType> equipTypeList = manager.GetListByWhere(BasEquipType._.EquipTypeName == modify_equip_type_name.Text.TrimStart().TrimEnd());
            //if (equipTypeList.Count > 0)
            //{
            //    if (equipTypeList[0].EquipTypeName != hidden_equip_type_name.Text)
            //    {
            //        X.Msg.Alert("提示", "此设备类型名称已被使用！").Show();
            //        return;
            //    }
            //}
             
            string sql = "update Pmt_equipclass set Equip_classname ='"+ modify_equip_type_name.Text + "', Remark='"+ modify_remark.Text + "' where Equip_class='"+ modify_obj_id.Text + "'";
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("设备类型修改", "设备类型编码：" + modify_obj_id.Text);
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
    #endregion

    #region 校验方法
    #endregion
}