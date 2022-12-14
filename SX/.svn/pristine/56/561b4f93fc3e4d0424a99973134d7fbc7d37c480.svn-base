using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Business.Implements;
using NBear.Common;
using Ext.Net;
using Mesnac.Entity;
using Mesnac.Data.Components;
using System.Data;
using System.Text.RegularExpressions;

public partial class Manager_BasicInfo_EquipmentInfo_EquipmentPartInfo : Mesnac.Web.UI.Page
{
    protected BasEquipPartInfoManager manager = new BasEquipPartInfoManager();//业务对象
    protected BasEquipTypeManager equipTypeManager = new BasEquipTypeManager();
    protected BasEquipPartRelationManager relationManager = new BasEquipPartRelationManager();

    private Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框


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
            InitTreeDept();
        }
    }

    /// <summary>
    /// 初始化设备分类列表树
    /// </summary>
    private void InitTreeDept()
    {
        if (this._.查询.SeqIdx == 0)
        {
            return;
        }
        EntityArrayList<BasEquipType> equipTypeList = equipTypeManager.GetListByWhere(BasEquipType._.DeleteFlag == "0");
        treeDept.GetRootNode().RemoveAll();
        foreach (BasEquipType equipType in equipTypeList)
        {
            Node node = new Node();
            node.NodeID = equipType.ObjID.ToString();
            node.Text = equipType.EquipTypeName;
            node.Expanded = true;
            node.Icon = Icon.Monitor;
            treeDept.GetRootNode().AppendChild(node);
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<BasEquipPartInfo> GetPageResultData(PageResult<BasEquipPartInfo> pageParams)
    {
        BasEquipPartInfoManager.QueryParams queryParams = new BasEquipPartInfoManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.objID = txt_obj_id.Text.TrimEnd().TrimStart();
        queryParams.partName = txt_part_name.Text.TrimEnd().TrimStart();
        queryParams.equipType = hidden_select_equip_type.Text;
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
        PageResult<BasEquipPartInfo> pageParams = new PageResult<BasEquipPartInfo>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasEquipPartInfo> lst = GetPageResultData(pageParams);
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
        if (hidden_select_equip_type.Value == "")
        {
            msg.Alert("操作", "请选择左侧设备类型节点！");
            msg.Show();
            return;
        }
        add_obj_id.Text = "";
        add_part_name.Text = "";
        add_equip_type.Text = equipTypeManager.GetById(hidden_select_equip_type.Value).EquipTypeName;
        hidden_part_name.Value = "";
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

        //X.Msg.Notify("", objID).Show();

        string sql = "select * from JCZL_parts where Mp_code='"+ objID + "'";
        DataSet ds = manager.GetBySql(sql).ToDataSet();

        modify_obj_id.Text = objID;
        modify_part_name.Text = ds.Tables[0].Rows[0]["Mp_name"].ToString(); 
        hidden_part_name.Text = ds.Tables[0].Rows[0]["Mp_name"].ToString(); 
        modify_equip_type.Text = ds.Tables[0].Rows[0]["equip_class"].ToString();
        hidden_select_equip_type.Value = ds.Tables[0].Rows[0]["equip_class"].ToString();
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
            BasEquipPartInfo part = manager.GetById(obj_id);
            BasEquipType equipType = equipTypeManager.GetById(part.EquipType);
            if (equipType != null && equipType.DeleteFlag.Equals("1"))
            {
                return "恢复失败：请先恢复设备类型[" + equipType.EquipTypeName + "]";
            }
            part.DeleteFlag = "0";
            manager.Update(part);
            this.AppendWebLog("设备部件恢复", "设备部件编码：" + part.ObjID);
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
            //BasEquipPartInfo part = manager.GetById(objID);
            //EntityArrayList<BasEquipPartRelation> relationList = relationManager.GetListByWhere(BasEquipPartRelation._.PartCode == part.ObjID && BasEquipPartRelation._.DeleteFlag == "0");
            //if (relationList.Count > 0)
            //{
            //    return "删除失败：此部件信息已被使用，禁止删除！";
            //}
            //part.DeleteFlag = "1";
            //manager.Update(part);

            string sql = "delete from JCZL_parts where Mp_code='"+ objID + "'";
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("设备部件删除", "设备部件编码：" + objID);
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
            EntityArrayList<BasEquipPartInfo> partList = manager.GetListByWhere(BasEquipPartInfo._.PartName == add_part_name.Text.TrimStart().TrimEnd());
            if (partList.Count > 0)
            {
                X.Msg.Alert("提示", "此部件名称已被使用！").Show();
                return;
            }
            string sql = "select max (MP_code) from JCZL_parts where Equip_class = '" + hidden_select_equip_type.Text + "'";
            DataTable dt = manager.GetBySql(sql).ToDataSet().Tables[0];
            String Equip_code = "";
            if (dt.Rows.Count == 0)
            { Equip_code = hidden_select_equip_type.Text + "001"; }
            else
            {
                int i = int.Parse(dt.Rows[0][0].ToString());
                i = i + 1;
                Equip_code = i.ToString();
                Equip_code = "0" + Equip_code;

            }

             sql = "insert into JCZL_parts(Mp_code,Mp_name,equip_class,memo) values('" + Equip_code + "','" + add_part_name.Text + "','" + hidden_select_equip_type.Value.ToString() + "','" + add_remark.Text + "')"; 
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("设备部件添加", "设备部件编码：" + add_obj_id.Text);
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
        { //修改重复校验
            //EntityArrayList<BasEquipPartInfo> partList = manager.GetListByWhere(BasEquipPartInfo._.PartName == modify_part_name.Text.TrimStart().TrimEnd());
            //if (partList.Count > 0)
            //{
            //    if (partList[0].PartName != hidden_part_name.Text)
            //    {
            //        X.Msg.Alert("提示", "此部件名称已被使用！").Show();
            //        return;
            //    }
            //}

            //X.Msg.Notify(modify_obj_id.Text,modify_obj_id.Text).Show();
            //BasEquipPartInfo part = manager.GetById(modify_obj_id.Text);
            //part.PartName = modify_part_name.Text;
            //part.EquipType = hidden_select_equip_type.Value.ToString();
            //manager.Update(part);

            string sql = "update  dbo.JCZL_parts set Mp_name='" + modify_part_name.Text + "' ,memo='" + modify_remark.Text + "' where Mp_code='" + modify_obj_id.Text + "'";
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("设备部件修改", "设备部件编码：" + modify_obj_id.Text);
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
    /// <summary>
    /// 检查部件名称是否重复
    /// yuany
    /// 2013年1月31日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckPartName(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        string partname = field.Text;
        EntityArrayList<BasEquipPartInfo> partList = manager.GetListByWhere(BasEquipPartInfo._.PartName == partname && BasEquipPartInfo._.DeleteFlag == "0");
        if (partList.Count == 0)
        {
            e.Success = true;
        }
        else
        {
            if (partList[0].PartName.ToString() == hidden_part_name.Value.ToString())
            {

                e.Success = true;
            }
            else
            {
                e.Success = false;
                e.ErrorMessage = "此部件名称已被使用！";
            }
        }
    }
    #endregion
}