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
using System.Threading;

public partial class Manager_Equipment_Repair_RepairPlan : Mesnac.Web.UI.Page
{
    BasEquipManager baseEquipManager = new BasEquipManager();
    protected SysCodeManager sysCodeManager = new SysCodeManager();
    protected Pmt_equipclassManager equipClassManager = new Pmt_equipclassManager();
    protected Pmt_equipManager equipManager = new Pmt_equipManager();
    protected Ppt_pmpartsManager equipTypeManager = new Ppt_pmpartsManager();
    protected Eqm_MpParamManager typeMainManager = new Eqm_MpParamManager();
    protected Eqm_MaintainPlanManager manager = new Eqm_MaintainPlanManager();
    protected SYS_USERManager userManager = new SYS_USERManager();
    protected JCZL_WorkShopManager workshopManager = new JCZL_WorkShopManager();
    protected Eqm_MainStandManager standManager = new Eqm_MainStandManager();
    protected Eqm_MainDailyManager dailyManager = new Eqm_MainDailyManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitTreeDept();

            bindList();
            bindImport();
            bindType(); 
            bindEquipClass();
            //bindEquipType();
            bindEquip();
            bindMaintainers();

            this.winSave.Hidden = true;
            this.winComplete.Hidden = true;
        }
    }


    #region 初始化控件
    private void bindImport()
    {
        cbxImport.Clear();
        EntityArrayList<Eqm_MpParam> list = typeMainManager.GetListByWhereAndOrder(Eqm_MpParam._.Param_Type == 1, Eqm_MpParam._.Param_id.Asc);
        foreach (Eqm_MpParam type in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(type.Param_Name, type.Param_id);
            cbxImport.Items.Add(item);
        }
    }
    private void bindType()
    {
        cbxType.Clear();
        EntityArrayList<Eqm_MpParam> list = typeMainManager.GetListByWhereAndOrder(Eqm_MpParam._.Param_Type == 2, Eqm_MpParam._.Param_id.Asc);
        foreach (Eqm_MpParam type in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(type.Param_Name, type.Param_id);
            cbxType.Items.Add(item);
        }
    }
    private void bindEquipClass()
    {
        cbxEquipClass.Clear();
        WhereClip where = new WhereClip();
        EntityArrayList<Pmt_equipclass> list = equipClassManager.GetListByWhereAndOrder(where, Pmt_equipclass._.Equip_class.Asc);
        this.storeEquipClass.DataSource = list;
        this.storeEquipClass.DataBind();
        this.storeEquipClassQuery.DataSource = list;
        this.storeEquipClassQuery.DataBind();
    }
    private void bindEquip()
    {
        cbxEquip.Clear();
        WhereClip where = new WhereClip();
        EntityArrayList<Pmt_equip> list = equipManager.GetListByWhereAndOrder(where, Pmt_equip._.Equip_code.Asc);
        this.storeEquip.DataSource = list;
        this.storeEquip.DataBind();
        this.storeEquipQuery.DataSource = list;
        this.storeEquipQuery.DataBind();
    }
    private void bindEquipType()
    {
        cbxEquipType.Clear();
        WhereClip where = new WhereClip();
        EntityArrayList<Ppt_pmparts> list = equipTypeManager.GetListByWhereAndOrder(where, Ppt_pmparts._.Mp_code.Asc);
        this.storeEquipType.DataSource = list;
        this.storeEquipType.DataBind();
        this.storeEquipTypeQuery.DataSource = list;
        this.storeEquipTypeQuery.DataBind();
    }

    private void bindMaintainers()
    {
        WhereClip where = new WhereClip();
        EntityArrayList<SYS_USER> list = userManager.GetListByWhere(where);
        foreach (SYS_USER user in list)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem(user.Real_name, user.USER_ID);
            cbxMaintainers.Items.Add(item);
            cbxMaintainersCom.Items.Add(item);
        }
    }
    [DirectMethod]
    private void changeEquipClass()
    {
        object value = cbxEquip.SelectedItem.Value;
        cbxEquip.Clear();
        EntityArrayList<Pmt_equip> list = equipManager.GetListByWhereAndOrder(Pmt_equip._.Equip_class == cbxEquipClass.SelectedItem.Value, Pmt_equip._.Equip_code.Asc);
        this.storeEquip.DataSource = list;
        this.storeEquip.DataBind();
        cbxEquip.SetValue(value);
    }
    [DirectMethod]
    private void changeEquip()
    {
        object value = cbxEquipType.SelectedItem.Value;
        cbxEquipType.Clear();
        EntityArrayList<Ppt_pmparts> list = equipTypeManager.GetListByWhereAndOrder(Ppt_pmparts._.Equip_code == cbxEquip.SelectedItem.Value, Ppt_pmparts._.Mp_code.Asc);
        this.storeEquipType.DataSource = list;
        this.storeEquipType.DataBind();
        cbxEquipType.SetValue(value);
    }
    [DirectMethod]
    private void changeEquipClassQuery()
    {
        object value = cbxEquipQuery.SelectedItem.Value;
        cbxEquipQuery.Clear();
        EntityArrayList<Pmt_equip> list = equipManager.GetListByWhereAndOrder(Pmt_equip._.Equip_class == cbxEquipClassQuery.SelectedItem.Value, Pmt_equip._.Equip_code.Asc);
        this.storeEquipQuery.DataSource = list;
        this.storeEquipQuery.DataBind();
        cbxEquipQuery.SetValue(value);
    }
    [DirectMethod]
    private void changeEquipQuery()
    {
        object value = cbxEquipTypeQuery.SelectedItem.Value;
        cbxEquipTypeQuery.Clear();
        EntityArrayList<Ppt_pmparts> list = equipTypeManager.GetListByWhereAndOrder(Ppt_pmparts._.Equip_code == cbxEquipQuery.SelectedItem.Value, Ppt_pmparts._.Mp_code.Asc);
        this.storeEquipTypeQuery.DataSource = list;
        this.storeEquipTypeQuery.DataBind();
        cbxEquipTypeQuery.SetValue(value);
    }


    #endregion

    #region 树
    /// <summary>
    /// 初始化机台列表树
    /// </summary>
    private void InitTreeDept()
    {
        treeEquip.GetRootNode().RemoveAll();
        treeEquip.GetRootNode().AppendChild(getTreeNodeByDelLevel());
    }

    /// <summary>
    /// 获取机台树的算法
    /// </summary>
    /// <param name="dep_num"></param>
    /// <returns></returns>
    private Node getTreeNodeByDelLevel()
    {
        Node node = new Node();
        node.NodeID = "0";
        node.Text = "机台分组";
        node.Expanded = true;
        Dictionary<string, string> depChildFristList = new Dictionary<string, string>();
        var query = equipClassManager.GetListByWhereAndOrder(Pmt_equipclass._.Equip_class.Length > 0, Pmt_equipclass._.Equip_class.Asc);
        foreach (var info in query)
        {
            Node childNode = new Node();
            childNode.NodeID = info.Equip_class;
            childNode.Text = info.Equip_classname;
            childNode.Expanded = false;
            var child = baseEquipManager.GetListByWhereAndOrder(BasEquip._.EquipType == info.Equip_class && BasEquip._.DeleteFlag == 0, BasEquip._.EquipCode.Asc);
            foreach (var item in child)
            {
                Node nodeLeaf = new Node();
                nodeLeaf.Text = item.EquipName;
                nodeLeaf.Qtip = item.EquipCode;
                nodeLeaf.Leaf = true;
                childNode.Children.Add(nodeLeaf);
            }
            node.Children.Add(childNode);
        }
        return node;
    }
    #endregion


    /// <summary>
    /// 相应点击机台树事件
    /// </summary>
    /// <param name="group"></param>
    [DirectMethod]
    public void LoadGridData(string group)
    {
        //判断当前机台当前时间是否设置班组信息
        //hidden_parent_num.Value = group;
        hidden_equip_code.Text = group;
        this.store.DataSource = getList(group);
        this.store.DataBind();


    }

    private DataSet getList(string equip_Code)
    {

        return GetDataByParas(equip_Code);
    }


    public System.Data.DataSet GetDataByParas(string equip_Code)
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"SELECT T.Mp_planid,T5.Equip_classname,T1.param_Name ImportName,T2.param_Name TypeName,T3.Mp_name,T6.workshop,
                        T4.Equip_name,CONVERT(varchar(10), T.Mp_plandate) Mp_plandate,CAST(T.Mp_planday AS INT) Mp_planday,CONVERT(varchar(10), T.Mp_realend) Mp_realend,CAST(T.Mp_realday AS INT) Mp_realday,
						CASE T.Plan_state WHEN '1' THEN '下达' WHEN '2' THEN '完成' ELSE '取消' END Plan_state,
						T.Mp_result,T7.USER_NAME,T.Mp_info,T.Mp_memo,T.Mp_StandId,T.repairuser
                        FROM Eqm_MaintainPlan T
                        LEFT JOIN eqm_Mpparam T1 ON T.Mp_import = T1.param_id AND T1.Param_Type = 1
                        LEFT JOIN eqm_Mpparam T2 ON T.Mp_type = T2.param_id AND T2.Param_Type = 2
                        LEFT JOIN Ppt_pmparts T3 ON T.Mp_code = T3.Mp_code AND T.Equip_code = T3.Equip_code
                        LEFT JOIN Pmt_equip T4 ON T.Equip_code = T4.Equip_code
						LEFT JOIN Pmt_equipclass T5 ON T.Equip_class = T5.Equip_class
						LEFT JOIN Eqm_MainStand T6 ON T.Mp_StandId = T6.Mp_StandId
                        LEFT JOIN SYS_USER T7 ON T.handle_name = T7.Worker_barcode");
        sb.AppendLine("WHERE 1=1");
        if (!string.IsNullOrEmpty(equip_Code))
            sb.AppendLine("AND T.Equip_code='" + equip_Code + "'");
        if (dStartDate.SelectedDate!=DateTime.MinValue)
            sb.AppendLine("AND T.Mp_plandate>='" + dStartDate.SelectedDate + "'");
        if (dEndDate.SelectedDate != DateTime.MinValue)
            sb.AppendLine("AND T.Mp_plandate<='" + dEndDate.SelectedDate + "'");
        if (!string.IsNullOrEmpty(cbxPlanState.Text))
            sb.AppendLine("AND T.Plan_state='" + cbxPlanState.SelectedItem.Value + "'");
        if (!string.IsNullOrEmpty(cbxBJState.Text))
            sb.AppendLine("AND T.ifnot_BJ='" + cbxBJState.SelectedItem.Value + "'");
        if (!string.IsNullOrEmpty(cbxEquipQuery.Text))
            sb.AppendLine("AND T.Equip_code='" + cbxEquipQuery.SelectedItem.Value + "'");
        if (!string.IsNullOrEmpty(cbxEquipTypeQuery.Text))
            sb.AppendLine("AND T.Mp_code='" + cbxEquipTypeQuery.SelectedItem.Value + "'");
        #endregion

        NBear.Data.CustomSqlSection css = manager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }


    private void bindList()
    {
        this.store.DataSource = getList(hidden_equip_code.Text);
        this.store.DataBind();
    }

    protected string GetMaxPlanID()
    {
        string planID = DateTime.Now.Date.ToString("yyyyMMdd") + "001";
        DataSet ds = dailyManager.GetBySql("select max(Mp_planid) Mp_planid from Eqm_MaintainPlan").ToDataSet();
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                planID = ds.Tables[0].Rows[0]["Mp_planid"].ToString();
                if (planID.Substring(0, 8) == DateTime.Now.Date.ToString("yyyyMMdd"))
                {
                    planID = planID.Substring(0, 8) + (Convert.ToInt32(planID.Substring(8, 3)) + 1).ToString().PadLeft(3, '0');
                }
                else
                {
                    planID = DateTime.Now.Date.ToString("yyyyMMdd") + "001";
                }
            }
        }
        return planID;
    }
    protected string GetMaxDailyID()
    {
        string dailyID = DateTime.Now.Date.ToString("yyyyMMdd") + "001";
        DataSet ds = dailyManager.GetBySql("select max(MainDaily_ID) MainDaily_ID from Eqm_MainDaily").ToDataSet();
        if (ds.Tables.Count > 0)
        {
            if(ds.Tables[0].Rows.Count>0)
            {
                dailyID = ds.Tables[0].Rows[0]["MainDaily_ID"].ToString();
                if (dailyID.Substring(0, 8) == DateTime.Now.Date.ToString("yyyyMMdd"))
                {
                    dailyID = dailyID.Substring(0, 8) + (Convert.ToInt32(dailyID.Substring(8, 3)) + 1).ToString().PadLeft(3, '0');
                }
                else
                {
                    dailyID = DateTime.Now.Date.ToString("yyyyMMdd") + "001";
                }
            }
        }
        return dailyID;
    }

    #region 按钮事件响应
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindList();
    }
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = getList(hidden_equip_code.Text);
        //huiw,2013-10-28添加，判断不为空时才导出excel
        if (ds == null || ds.Tables[0].Rows.Count == 0)
        {
            X.Msg.Alert("提示", "未查询出数据！").Show();
        }
        else
        {
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                bool isshow = false;
                DataColumn dc = ds.Tables[0].Columns[i];
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
                    ds.Tables[0].Columns.Remove(dc.ColumnName);
                    i--;
                }
            }
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "设备维修计划导出");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.hideMode.Text = "Add";

        txtMp_plandate.SetValue(DateTime.Now.Date);
        cbxImport.SetValue(null);
        cbxType.SetValue(null);
        cbxEquipClass.SetValue(null);
        cbxEquip.SetValue(null);
        cbxEquipType.SetValue(null);
        cbxMaintainers.SetValue(null);
        cbxPlanStateAdd.Select(0);
        txtMp_planday.Text = "0";
        txtMp_realday.Text = "0";
        txtRepairuser.Text = "";
        txtMp_info.Text = "";
        txtMp_result.Text = "";
        txtMp_memo.Text = "";
        this.winSave.Hidden = false;
    }


    protected void btnRecord_Click(object sender, EventArgs e)
    {
        if (rowSelectMuti.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "您没有选择任何行,请选择").Show();
            return;
        }
        string objid = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            objid = row.RecordID; EntityArrayList<Eqm_MaintainPlan> listEdit = manager.GetListByWhere(Eqm_MaintainPlan._.Mp_planid == row.RecordID);
            if (listEdit.Count == 0)
            {
                X.Msg.Alert("提示", "无此维修计划，生成失败！").Show();
                return;
            }
            Eqm_MaintainPlan record = listEdit[0];
            if(record.Plan_state != "2")
            {
                X.Msg.Alert("提示", "此维修计划还未完成！").Show();
                return;
            }
            Eqm_MainDaily recordNew = new Eqm_MainDaily();
            recordNew.MainDaily_ID = GetMaxDailyID();
            recordNew.Equip_code = record.Equip_code;
            recordNew.Mp_Date = record.Mp_realend;
            recordNew.Mp_StartTime = Convert.ToDateTime(record.Mp_realend);
            recordNew.MP_EndTime = Convert.ToDateTime(record.Mp_realend);
            recordNew.Mp_time = record.Mp_realday;
            recordNew.Handle_name = record.Repairuser;
            EntityArrayList<Eqm_MpParam> listType = typeMainManager.GetListByWhere(Eqm_MpParam._.Param_Type == "2" && Eqm_MpParam._.Param_id == record.Mp_type);
            if (listType.Count>0)
            {
                recordNew.MP_Type = listType[0].Param_Name;
            }
            recordNew.Mp_reason = record.Mp_info;
            recordNew.Mp_result = record.Mp_result;
            recordNew.In_Date = record.Mp_plandate;
            recordNew.Worker_barcode = record.Handle_name;
            recordNew.Mem_BJ = record.Mp_memo;
            recordNew.Finish_flag = 1;
            recordNew.Mp_Code = record.Mp_code;

            if (dailyManager.Insert(recordNew) >= 0)
            {
                this.AppendWebLog("设备维修计划完成", "修改机台：" + record.Equip_code);
                X.Msg.Alert("提示", "生成成功！").Show();
                return;
            }
            else
            {
                X.Msg.Alert("提示", "生成失败！").Show();
                return;
            }
        }
    }

    protected void btnCompletePlan_Click(object sender, EventArgs e)
    {
        if (rowSelectMuti.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "您没有选择任何行,请选择").Show();
            return;
        }
        string objid = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            objid = row.RecordID;
            EntityArrayList<Eqm_MaintainPlan> listEdit = manager.GetListByWhere(Eqm_MaintainPlan._.Mp_planid == row.RecordID);
            if (listEdit.Count == 0)
            {
                X.Msg.Alert("提示", "无此维修标准，修改失败！").Show();
                return;
            }
            Eqm_MaintainPlan record = listEdit[0];
            if(record.Plan_state == "2")
            {
                X.Msg.Alert("提示", "此计划已完成，无法重复完成！").Show();
                return;
            }
            else if (record.Plan_state == "3")
            {
                X.Msg.Alert("提示", "此计划已取消，无法完成！").Show();
                return;
            }
            else
            {
                cbxMaintainersCom.SetValue(record.Handle_name);
                txtMp_realendCom.SelectedDate = Convert.ToDateTime(record.Mp_realend);
                txtMp_realdayCom.Text = record.Mp_realday.ToString();
                txtRepairuserCom.Text = record.Repairuser;
                txtMp_resultCom.Text = record.Mp_result;

                hideObjIDComplete.Text = objid;

                this.winComplete.Hidden = false;
            }

        }
        //return "true";
    }


    protected void btnComplete_Click(object sender, EventArgs e)
    {
        EntityArrayList<Eqm_MaintainPlan> listEdit = manager.GetListByWhere(Eqm_MaintainPlan._.Mp_planid == hideObjIDComplete.Text);
        if (listEdit.Count == 0)
        {
            X.Msg.Alert("提示", "无此维修标准，修改失败！").Show();
            return;
        }
        Eqm_MaintainPlan record = listEdit[0];
        if (record != null)
        {
            record.Mp_realday = Convert.ToDecimal(txtMp_realdayCom.Text);
            record.Mp_realend = txtMp_realendCom.SelectedDate.ToString("yyyy-MM-dd");
            record.Handle_name = cbxMaintainersCom.SelectedItem.Value.ToString();
            record.Repairuser = txtRepairuserCom.Text;
            record.Mp_result = txtMp_resultCom.Text;
            record.Plan_state = "2";
            if (listEdit[0].Mp_StandId != null)
            {
                EntityArrayList<Eqm_MainStand> list = standManager.GetListByWhere(Eqm_MainStand._.Mp_StandId == listEdit[0].Mp_StandId);
                if (list.Count > 0)
                {
                    if (list[0].In_date < txtMp_realendCom.SelectedDate)
                    {
                        list[0].In_date = txtMp_realendCom.SelectedDate;
                        standManager.Update(list[0]);
                    }
                }
            }

            if (manager.Update(record) >= 0)
            {
                this.AppendWebLog("设备维修计划完成", "修改机台：" + record.Equip_code);
                this.winComplete.Hidden = true;
                bindList();
                X.Msg.Alert("提示", "完成成功！").Show();
            }
            else
            {
                X.Msg.Alert("提示", "完成失败！").Show();
            }
        }
    }
    [DirectMethod]
    public string btnCancelPlan_Click()
    {
        if (rowSelectMuti.SelectedRows.Count == 0)
        {
            return "您没有选择任何行,请选择";
        }
        string objid = string.Empty;
        foreach (SelectedRow row in this.rowSelectMuti.SelectedRows)
        {
            objid = row.RecordID; EntityArrayList<Eqm_MaintainPlan> listEdit = manager.GetListByWhere(Eqm_MaintainPlan._.Mp_planid == row.RecordID);
            if (listEdit.Count == 0)
            {
                return "无此维修计划，修改失败！";
            }
            Eqm_MaintainPlan record = listEdit[0];
            if (record.Plan_state == "2")
            {
                return "此计划已完成，无法取消！";
            }
            else
            {
                record.Plan_state = "3";

                if (manager.Update(record) >= 0)
                {
                    this.AppendWebLog("设备维修计划完成", "修改机台：" + record.Equip_code);
                    bindList();
                    return "取消成功！";
                }
                else
                {
                    return "取消失败！";
                }
            }
        }
        return "true";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.hideMode.Text == "Add")//添加
        {
            Eqm_MaintainPlan record = new Eqm_MaintainPlan();
            record.Mp_planid = GetMaxPlanID();
            record.Mp_import = cbxImport.SelectedItem.Value.ToString();
            record.Mp_type = cbxType.SelectedItem.Value.ToString();
            record.Mp_code = cbxEquipType.SelectedItem.Value.ToString();
            record.Equip_code = cbxEquip.SelectedItem.Value.ToString();
            if (cbxMaintainers.SelectedItem.Value != null)
            {
                record.Handle_name = cbxMaintainers.SelectedItem.Value.ToString();
            }
            record.Equip_class = cbxEquipClass.SelectedItem.Value.ToString();
            record.Mp_planday = Convert.ToDecimal(txtMp_planday.Text);
            record.Mp_plandate = txtMp_plandate.SelectedDate.ToString("yyyy-MM-dd");
            record.Mp_realday = Convert.ToDecimal(txtMp_realday.Text);
            if (txtMp_realend.SelectedDate != DateTime.MinValue)
            {
                record.Mp_realend = txtMp_realend.SelectedDate.ToString("yyyy-MM-dd");
            }
            record.Repairuser = txtRepairuser.Text;
            record.Mp_info = txtMp_info.Text;
            record.Mp_memo = txtMp_memo.Text;
            if (!string.IsNullOrEmpty(txtMp_result.Text))
            {
                record.Mp_result = txtMp_result.Text;
            }
            else
            {
                record.Mp_result = "正常";
            }
            EntityArrayList<Eqm_MainStand> list = standManager.GetListByWhere(Eqm_MainStand._.Mp_import == cbxImport.SelectedItem.Value.ToString() && Eqm_MainStand._.Mp_type == cbxType.SelectedItem.Value.ToString() && Eqm_MainStand._.Equip_code == cbxEquip.SelectedItem.Value.ToString() && Eqm_MainStand._.Mp_code == cbxEquipType.SelectedItem.Value.ToString() && Eqm_MainStand._.Mp_stand == txtMp_info.Text);
            if(list.Count>0)
            {
                record.Mp_StandId = list[0].Mp_StandId;
            }
            else
            {
                X.Msg.Alert("提示", "无此维修标准，添加失败！").Show();
                return;
            }
            if (cbxPlanStateAdd.SelectedItem.Value.ToString() == "2")
            {
                record.Mp_resulted = "1";
            }
            else
            {
                record.Mp_resulted = "0";
            }
            record.Ifnot_BJ = "0";
            record.Plan_state = cbxPlanStateAdd.SelectedItem.Value.ToString();
            if (cbxPlanStateAdd.SelectedItem.Value.ToString() == "2")//计划完成
            {
                if (list[0].Mp_StandId != null)
                {
                    if (list[0].In_date < txtMp_realend.SelectedDate)
                    {
                        list[0].In_date = txtMp_realend.SelectedDate;
                        standManager.Update(list[0]);
                    }
                }
            }

            if (manager.Insert(record) >= 0)
            {
                this.AppendWebLog("设备维修计划添加", "添加机台：" + record.Equip_code);
                winSave.Hidden = true;
                bindList();
                X.Msg.Alert("提示", "添加完成！").Show();
            }
            else
            {
                X.Msg.Alert("提示", "添加失败！").Show();
            }
        }
        else//修改
        {
            EntityArrayList<Eqm_MaintainPlan> listEdit = manager.GetListByWhere(Eqm_MaintainPlan._.Mp_planid == hideObjID.Text);
            if (listEdit.Count == 0)
            {
                X.Msg.Alert("提示", "无此维修标准，修改失败！").Show();
                return;
            }
            Eqm_MaintainPlan record = listEdit[0];
            if (record != null)
            {
                record.Mp_import = cbxImport.SelectedItem.Value.ToString();
                record.Mp_type = cbxType.SelectedItem.Value.ToString();
                record.Mp_code = cbxEquipType.SelectedItem.Value.ToString();
                record.Equip_code = cbxEquip.SelectedItem.Value.ToString();
                if (cbxMaintainers.SelectedItem.Value != null)
                {
                    record.Handle_name = cbxMaintainers.SelectedItem.Value.ToString();
                }
                record.Equip_class = cbxEquipClass.SelectedItem.Value.ToString();
                record.Mp_planday = Convert.ToDecimal(txtMp_planday.Text);
                record.Mp_plandate = txtMp_plandate.SelectedDate.ToString("yyyy-MM-dd");
                record.Mp_realday = Convert.ToDecimal(txtMp_realday.Text);
                if (txtMp_realend.SelectedDate != DateTime.MinValue)
                {
                    record.Mp_realend = txtMp_realend.SelectedDate.ToString("yyyy-MM-dd");
                }
                record.Repairuser = txtRepairuser.Text;
                record.Mp_info = txtMp_info.Text;
                record.Mp_memo = txtMp_memo.Text;
                if (!string.IsNullOrEmpty(txtMp_result.Text))
                {
                    record.Mp_result = txtMp_result.Text;
                }
                else
                {
                    record.Mp_result = "正常";
                }
                EntityArrayList<Eqm_MainStand> list = standManager.GetListByWhere(Eqm_MainStand._.Mp_import == cbxImport.SelectedItem.Value.ToString() && Eqm_MainStand._.Mp_type == cbxType.SelectedItem.Value.ToString() && Eqm_MainStand._.Equip_code == cbxEquip.SelectedItem.Value.ToString() && Eqm_MainStand._.Mp_code == cbxEquipType.SelectedItem.Value.ToString() && Eqm_MainStand._.Mp_stand == txtMp_info.Text);
                if (list.Count > 0)
                {
                    record.Mp_StandId = list[0].Mp_StandId;
                }
                else
                {
                    X.Msg.Alert("提示", "无此维修标准，修改失败！").Show();
                    return;
                }
                if (cbxPlanStateAdd.SelectedItem.Value.ToString() == "2")
                {
                    record.Mp_resulted = "1";
                }
                else
                {
                    record.Mp_resulted = "0";
                }
                record.Ifnot_BJ = "0";
                record.Plan_state = cbxPlanStateAdd.SelectedItem.Value.ToString();
                if (cbxPlanStateAdd.SelectedItem.Value.ToString() == "2")//计划完成
                {
                    if (list[0].Mp_StandId != null)
                    {
                        if(list[0].In_date < txtMp_realend.SelectedDate)
                        {
                            list[0].In_date = txtMp_realend.SelectedDate;
                            standManager.Update(list[0]);
                        }
                    }
                }

                if (manager.Update(record) >= 0)
                {
                    this.AppendWebLog("设备维修计划修改", "修改机台：" + record.Equip_code);
                    winSave.Hidden = true;
                    bindList();
                    X.Msg.Alert("提示", "修改完成！").Show();
                }
                else
                {
                    X.Msg.Alert("提示", "修改失败！").Show();
                }
            }
        }
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        winSave.Hidden = true;
    }
    protected void btnCancelComplete_Click(object sender, EventArgs e)
    {
        winComplete.Hidden = true;
    }
    #endregion

    #region 下拉列表事件响应
    [DirectMethod]
    protected void cbxEquipClass_SelectChanged(object sender, EventArgs e)
    {
        changeEquipClass();
    }
    protected void cbxEquip_SelectChanged(object sender, EventArgs e)
    {
        changeEquip();
    }
    protected void cbxEquipClassQuery_SelectChanged(object sender, EventArgs e)
    {
        changeEquipClassQuery();
    }
    protected void cbxEquipQuery_SelectChanged(object sender, EventArgs e)
    {
        changeEquipQuery();
    }
    #endregion

    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Edit(string ObjID)
    {
        //hidden_type.Text = "2";
        EntityArrayList<Eqm_MaintainPlan> list = manager.GetListByWhere(Eqm_MaintainPlan._.Mp_planid == ObjID);
        if(list.Count==0)
        {
            X.Msg.Alert("提示", "此条记录已经不存在！").Show();
            return;
        }
        Eqm_MaintainPlan record = list[0];


        if (record != null)
        {
            if (record.Plan_state == "2")
            {
                X.Msg.Alert("提示", "此计划已完成，无法修改！").Show();
                return;
            }
            else if (record.Plan_state == "3")
            {
                X.Msg.Alert("提示", "此计划已取消，无法修改！").Show();
                return;
            }
            cbxEquipClass.SetValue(record.Equip_class);
            cbxEquip.SetValue(record.Equip_code);
            cbxImport.SetValue(record.Mp_import);
            cbxType.SetValue(record.Mp_type);
            cbxMaintainers.SetValue(record.Handle_name);
            txtMp_plandate.SelectedDate = Convert.ToDateTime(record.Mp_plandate);
            txtMp_planday.Text = record.Mp_planday.ToString();
            txtMp_realend.SelectedDate = Convert.ToDateTime(record.Mp_realend);
            txtMp_realday.Text = record.Mp_realday.ToString();
            txtRepairuser.Text = record.Repairuser;
            txtMp_info.Text = record.Mp_info;
            txtMp_memo.Text = record.Mp_memo;
            txtMp_result.Text = record.Mp_result;
            cbxPlanStateAdd.SetValue(record.Plan_state);

            cbxEquipType.SetValue(record.Mp_code);
            hideObjID.Text = ObjID;
            this.hideMode.Text = "Edit";
            ////cbxEquipClass.DirectChange += this.cbxEquipClass_SelectChanged;
            ////cbxEquipClass.DirectEvents.Change.HandlerIsNotEmpty = this.cbxEquipClass_SelectChanged;
            //cbxEquipClass.DirectSelect += this.cbxEquipClass_SelectChanged;
            ////cbxEquipClass.DirectChange += new System.EventHandler(this.cbxEquipClass_SelectChanged);
            this.winSave.Hidden = false;
        }
        else
        {
            bindList();
            X.Msg.Alert("提示", "此条记录已经不存在！").Show();
        }

    }
    [DirectMethod]
    public void pnlList_Delete(string ObjID)
    {
        EntityArrayList<Eqm_MaintainPlan> list = manager.GetListByWhere(Eqm_MaintainPlan._.Mp_planid == ObjID);
        if (list.Count == 0)
        {
            X.Msg.Alert("提示", "此条记录已经不存在！").Show();
            return;
        }
        Eqm_MaintainPlan record = list[0];

        if (record.Plan_state == "2")
        {
            X.Msg.Alert("提示", "此计划已完成，无法删除！").Show();
            return;
        }
        manager.DeleteByWhere(Eqm_MaintainPlan._.Mp_planid == ObjID);
        this.AppendWebLog("设备维修计划删除", "修改机台：" + record.Equip_code);

        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }

    #endregion
}