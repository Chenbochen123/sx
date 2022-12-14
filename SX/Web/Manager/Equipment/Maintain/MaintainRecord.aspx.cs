using System;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Data.Components;
using System.Collections.Generic;

public partial class Manager_Equipment_Maintain_MaintainRecord : Mesnac.Web.UI.Page
{
    protected SysCodeManager sysCodeManager = new SysCodeManager();
    protected PptShiftManager shiftManager = new PptShiftManager();
    protected BasWorkShopManager shopManager = new BasWorkShopManager();
    protected BasEquipManager equipManager = new BasEquipManager();
    protected EqmStopTypeManager typeManager = new EqmStopTypeManager();
    protected EqmStopFaultManager faultManager = new EqmStopFaultManager();
    protected EqmFaultReasonManager reasonManager = new EqmFaultReasonManager();
    protected EqmMaintainRecordManager manager = new EqmMaintainRecordManager();
    protected EqmSparePartManager sparePartManager = new EqmSparePartManager();
    protected EqmSparePartMainTypeManager majorTypeManager = new EqmSparePartMainTypeManager();
    protected EqmSparePartDetailTypeManager minorTypeManager = new EqmSparePartDetailTypeManager();
    protected EqmSparePartRepairOutManager outManager = new EqmSparePartRepairOutManager();
    protected EqmSparePartStoreManager storeManager = new EqmSparePartStoreManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();
    protected BasUserManager userManager = new BasUserManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            PageInit();
        }
    }
    #region 自定义方法
    private void PageInit()
    {
        this.dStartDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        this.dEndDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        this.dStartTime.Text = "0:00";
        this.dEndTime.Text = "0:00";
        hidden_send_user.Text = this.UserID;
        add_send_user.Text = userManager.GetListByWhere(BasUser._.WorkBarcode == this.UserID)[0].UserName;

        bindList();
        bindWorkShop();
        bindEquip();
        bindMainType();
        bindFaultType();

        this.winSave.Hidden = true;
    }
    private void bindWorkShop()
    {
        EntityArrayList<BasWorkShop> list = shopManager.GetListByWhere(BasWorkShop._.DeleteFlag == "0");
        this.storeWorkShop.DataSource = list;
        this.storeWorkShop.DataBind();
    }
    private void bindEquip()
    {
        this.cbStopEquip.ClearValue();
        EntityArrayList<BasEquip> list = equipManager.GetListByWhereAndOrder(BasEquip._.DeleteFlag == "0" & BasEquip._.WorkShopCode == cbWorkShop.SelectedItem.Value, BasEquip._.EquipName.Asc);
        this.storeEquip.DataSource = list;
        this.storeEquip.DataBind();
    }
    private void bindMainType()
    {
        EntityArrayList<SysCode> list = sysCodeManager.GetListByWhereAndOrder(SysCode._.TypeID == "StopMainType", SysCode._.ItemName.Asc);
        this.storeStopMainType.DataSource = list;
        this.storeStopMainType.DataBind();
    }
    private void bindStopType()
    {
        this.cbStopType.ClearValue();
        EntityArrayList<EqmStopType> list = typeManager.GetListByWhereAndOrder(EqmStopType._.DeleteFlag == "0" & EqmStopType._.MainTypeID == cbStopMainType.SelectedItem.Value, EqmStopType._.TypeName.Asc);
        this.storeStopType.DataSource = list;
        this.storeStopType.DataBind();
    }
    private void bindStopFault()
    {
        this.cbStopFault.ClearValue();
        EntityArrayList<EqmStopFault> list = faultManager.GetListByWhereAndOrder(EqmStopFault._.DeleteFlag == "0" & EqmStopFault._.TypeID == cbStopType.SelectedItem.Value, EqmStopFault._.FaultName.Asc);
        this.storeStopFault.DataSource = list;
        this.storeStopFault.DataBind();
    }

    private void bindFaultType()
    {
        EntityArrayList<SysCode> list = sysCodeManager.GetListByWhere(SysCode._.TypeID == "FaultType");
        this.storeFaultType.DataSource = list;
        this.storeFaultType.DataBind();
    }
    private DataSet getList()
    {
        EqmMaintainRecordParams _params = new EqmMaintainRecordParams();
        _params.startTime = Convert.ToDateTime(dStartDate.Text).ToString("yyyy-MM-dd") + " " + dStartTime.Text;
        _params.endTime = Convert.ToDateTime(dEndDate.Text).AddDays(1).ToString("yyyy-MM-dd") + " " + dEndTime.Text;
        _params.workShopCode = cbWorkShop.SelectedItem.Value;
        _params.equipCode = cbStopEquip.SelectedItem.Value;
        _params.mainTypeID = cbStopMainType.SelectedItem.Value;
        _params.stopTypeID = cbStopType.SelectedItem.Value;
        _params.faultID = cbStopFault.SelectedItem.Value;

        return manager.GetDataByParas(_params);
    }
    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }
    #endregion

    #region 下拉列表事件响应
    protected void cbWorkShop_SelectChanged(object sender, EventArgs e)
    {
        bindEquip();
    }
    protected void cbStopMainType_SelectChanged(object sender, EventArgs e)
    {
        bindStopType();
    }
    protected void cbStopType_SelectChanged(object sender, EventArgs e)
    {
        bindStopFault();
    }
    #endregion

    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Edit(string ObjID)
    {
        EqmMaintainRecord record = manager.GetById(int.Parse(ObjID));
        if (record != null)
        {
            EntityArrayList<BasEquip> equipList = equipManager.GetListByWhere(BasEquip._.EquipCode == record.EquipCode);
            txtShift.Text = shiftManager.GetById(record.ShiftID).ShiftName;
            txtEquip.Text = equipList.Count > 0 ? equipList[0].EquipName : string.Empty;
            txtStartTime.Text = record.StartTime.ToString("yyyy-MM-dd hh:mm:ss");
            txtEndTime.Text = record.EndTime == null ? string.Empty : Convert.ToDateTime(record.EndTime).ToString("yyyy-MM-dd HH:mm:ss");
            txtReportTime.Text = record.ReportTime == null ? string.Empty : Convert.ToDateTime(record.ReportTime).ToString("yyyy-MM-dd HH:mm:ss");
            txtMaintainers.Text = record.Maintainers;
            txtRemark.Text = record.Remark;
            try
            {
                EntityArrayList<SysCode> faultTypeList = sysCodeManager.GetListByWhere(SysCode._.TypeID == "FaultType" && SysCode._.ItemCode == record.FaultTypeID.ToString());
                cbFaultType.Text = faultTypeList[0].ItemName;
            }
            catch (Exception)
            {
                cbFaultType.Text = "";
            }
            try
            {
                EntityArrayList<EqmStopType> stopTypelist = typeManager.GetListByWhere(EqmStopType._.TypeCode == record.StopTypeID);
                cbType.Text = stopTypelist[0].TypeName;
                hidden_stop_type.Text = stopTypelist[0].TypeCode;
            }
            catch (Exception)
            {
                cbType.Text = "";
            }
            try
            {
                EntityArrayList<EqmStopFault> stpFaultlist = faultManager.GetListByWhere(EqmStopFault._.FaultCode == record.FaultID);
                cbFault.Text = stpFaultlist[0].FaultName;
                hidden_stop_fault.Text = stpFaultlist[0].FaultCode;
            }
            catch (Exception)
            {
                cbFault.Text = "";
            }
            hideObjID.Text = ObjID;
            if (record.ReportTime != null)
                dfMaintainEndDate.MinDate = dfMaintainStartDate.MinDate = Convert.ToDateTime(record.ReportTime);
            try
            {
                dfMaintainStartDate.Text = Convert.ToDateTime(record.MaintainStartTime).ToString("yyyy-MM-dd");
                dfMaintainStartTime.Text = Convert.ToDateTime(record.MaintainStartTime).ToString("HH:mm:ss");

                dfMaintainEndDate.Text = Convert.ToDateTime(record.MaintainEndTime).ToString("yyyy-MM-dd");
                dfMaintainEndTime.Text = Convert.ToDateTime(record.MaintainEndTime).ToString("HH:mm:ss");
            }
            catch (Exception)
            {
            }
            try
            {
                string[] arrRs = record.StopReason.Split(',');
                string reasonStr = "";
                EntityArrayList<EqmFaultReason> reasonList = new EntityArrayList<EqmFaultReason>();
                foreach (string item in arrRs)
                {
                    reasonList.Add(reasonManager.GetById(item));
                    reasonStr += reasonManager.GetById(item).ReasonName + ",";
                }
                cbReason.Value = reasonStr.TrimEnd(',');
                hidden_fault_reason.Value = record.StopReason;
                reasonStore.Data = reasonList;
                reasonStore.DataBind();
            }
            catch
            {
            }

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
        manager.Delete(int.Parse(ObjID));
        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }
    [DirectMethod]
    public void pnlList_Add(string ObjID)
    {
        EqmMaintainRecord record = manager.GetById(int.Parse(ObjID));
        if ("1".Equals(record.Status))
        {
            X.Msg.Alert("提示", "该数据已经领用了备件，如需修改请在出库单功能中进行操作！").Show();
            return;
        }
        hidden_ObjID.Text = ObjID;
        add_send_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
        add_number.Text = "1";
        this.store1.Data = new EntityArrayList<EqmSparePartRepairOut>();
        this.store1.DataBind();
        this.winAdd.Hidden = false;
    }

    [DirectMethod]
    public string commandcolumn_direct_delete(string sendDate , string sparepartCode , string number ,string sendUser)
    {
        string temp = Convert.ToDateTime(sendDate).ToString("yyyy-MM-dd") + ","
              + sparepartCode + ","
              + (number == "" ? "1" : number) + ","
              + sendUser;
        string[] storeOutArr = hidden_storeOutInfo.Text.TrimEnd('|').Split('|');
        hidden_storeOutInfo.Text = "";
        EntityArrayList<EqmSparePartRepairOut> list = new EntityArrayList<EqmSparePartRepairOut>();
        foreach (string storeOut in storeOutArr)
        {
            if (storeOut != temp)
            {
                string[] infoArr = storeOut.Split(',');
                EqmSparePartRepairOut partOut = new EqmSparePartRepairOut();
                partOut.SendDate = Convert.ToDateTime(infoArr[0]);
                partOut.SparePartCode = infoArr[1];
                partOut.SparePartModel = sparePartManager.GetListByWhere(EqmSparePart._.SparePartCode == infoArr[1])[0].SparePartName;
                partOut.StoreOutNum = Convert.ToInt32(infoArr[2]);
                partOut.SendUser = infoArr[3];
                partOut.Remark = userManager.GetListByWhere(BasUser._.WorkBarcode == infoArr[3])[0].UserName;
                EntityArrayList<EqmSparePartStore> storeList = storeManager.GetListByWhere(EqmSparePartStore._.SparePartCode == infoArr[1]);
                if (storeList.Count > 0)
                {
                    partOut.SendNo = storeList[0].CurrentStoreNum.ToString();
                }
                else
                {
                    partOut.SendNo = "0";
                }
                list.Add(partOut);
                hidden_storeOutInfo.Text = hidden_storeOutInfo.Text + storeOut + "|";
            }
        }
        this.store1.Data = list;
        this.store1.DataBind();
        return "";
    }
    #endregion

    #region 按钮事件响应
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindList();
    }

    /// <summary>
    /// 点击添加备件领用信息中添加按钮触发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Store_Out_Btn_Click_Event(object sender, EventArgs e)
    {
        if (add_sparepart_code.Text.Equals(string.Empty))
        {
            msg.Alert("提示", "请选择备件名称").Show();
            return;
        }
        string temp = Convert.ToDateTime(add_send_date.Text).ToString("yyyy-MM-dd") + ","
            + hidden_sparepart_code.Text + ","
            + (add_number.Text == "" ? "1" : add_number.Text) + ","
            + hidden_send_user.Text;
        hidden_storeOutInfo.Text = hidden_storeOutInfo.Text + temp + "|";

        string[] storeOutArr = hidden_storeOutInfo.Text.TrimEnd('|').Split('|');
        EntityArrayList<EqmSparePartRepairOut> list = new EntityArrayList<EqmSparePartRepairOut>();
        foreach (string storeOut in storeOutArr)
        {
            string[] infoArr = storeOut.Split(',');
            EqmSparePartRepairOut partOut = new EqmSparePartRepairOut();
            partOut.SendDate = Convert.ToDateTime(infoArr[0]);
            partOut.SparePartCode = infoArr[1];
            partOut.SparePartModel = sparePartManager.GetListByWhere(EqmSparePart._.SparePartCode == infoArr[1])[0].SparePartName;
            partOut.StoreOutNum = Convert.ToInt32(infoArr[2]);
            partOut.SendUser = infoArr[3];
            partOut.Remark = userManager.GetListByWhere(BasUser._.WorkBarcode == infoArr[3])[0].UserName;
            EntityArrayList<EqmSparePartStore> storeList = storeManager.GetListByWhere(EqmSparePartStore._.SparePartCode == infoArr[1]);
            if (storeList.Count > 0)
            {
                partOut.SendNo = storeList[0].CurrentStoreNum.ToString();
            }
            else
            {
                partOut.SendNo = "0";
            }
            list.Add(partOut);
        }
        this.store1.Data = list;
        this.store1.DataBind();
    }
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {
            string[] storeOutArr = hidden_storeOutInfo.Text.TrimEnd('|').Split('|');
            if (storeOutArr.Length == 1 & storeOutArr[0].Equals(string.Empty))
            {
                msg.Alert("提示", "请添加备件领用信息").Show();
                return;
            }
            EntityArrayList<EqmSparePartRepairOut> list = new EntityArrayList<EqmSparePartRepairOut>();
            foreach (string storeOut in storeOutArr)
            {
                string[] infoArr = storeOut.Split(',');
                string partName = sparePartManager.GetListByWhere(EqmSparePart._.SparePartCode == infoArr[1])[0].SparePartName;
                int storeOutNum = Convert.ToInt32(infoArr[2]);
                int currentStoreNum = 0;
                int minStoreNum = 0;
                EntityArrayList<EqmSparePartStore> storeList = storeManager.GetListByWhere(EqmSparePartStore._.SparePartCode == infoArr[1]);
                if (storeList.Count > 0)
                {
                    currentStoreNum = Convert.ToInt32(storeList[0].CurrentStoreNum);
                    minStoreNum = Convert.ToInt32(storeList[0].MinStoreNum);
                }
                if (storeOutNum > Convert.ToDecimal(currentStoreNum))
                {
                    msg.Alert("提示", partName + "    出库数量[" + storeOutNum + "]超过现有库存数量[" + currentStoreNum + "]").Show();
                    return;
                }
                if (minStoreNum > (currentStoreNum - storeOutNum))
                {
                    msg.Alert("提示", partName + "    出库后库存数量[" + (currentStoreNum - storeOutNum) + "]低于最小库存[" + minStoreNum + "]").Show();
                    return;
                }
            }
            foreach (string storeOut in storeOutArr)
            {
                string[] infoArr = storeOut.Split(',');
                EqmSparePartRepairOut partOut = new EqmSparePartRepairOut();
                partOut.SendNo = outManager.GetNextSparePartStoreOutCode(Convert.ToDateTime(infoArr[0]));
                partOut.SendDate = Convert.ToDateTime(infoArr[0]);
                partOut.SparePartCode = infoArr[1];
                partOut.SparePartModel = "";
                partOut.StoreOutNum = Convert.ToInt32(infoArr[2]);
                partOut.SendUser = infoArr[3];
                partOut.RecordDate = DateTime.Now;
                partOut.OrderID = hidden_ObjID.Text;
                partOut.DeleteFlag = "0";

                string flag = storeManager.UpdateSparePartStore(false, partOut.SparePartCode, partOut.SparePartModel, Convert.ToDecimal(partOut.StoreOutNum));
                if (flag.IndexOf("ERROR") > -1)
                {
                   msg.Alert("提示",flag).Show();
                   continue;
                }

                outManager.Insert(partOut);
                this.AppendWebLog("备件出库记录增加", "出库单号：" + partOut.SendNo);
            }

            EqmMaintainRecord record = manager.GetById(hidden_ObjID.Text);
            record.Status = "1";
            manager.Update(record);

            bindList();
            this.winAdd.Close();
            add_sparepart_code.Text = string.Empty;
            add_send_user.Text = string.Empty;
            hidden_send_user.Text = string.Empty;
            hidden_select_sparepart_code.Text = string.Empty;
            hidden_sparepart_code.Text = string.Empty;
            hidden_storeOutInfo.Text = string.Empty;
            msg.Notify("操作", "保存成功").Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败：" + ex);
            msg.Show();
        }
    }
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winAdd.Close();
    }
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = getList();
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
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "维修记录");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        EqmMaintainRecord record = manager.GetById(int.Parse(hideObjID.Text));
        if (record != null)
        {
            record.MaintainStartTime = Convert.ToDateTime(dfMaintainStartDate.RawText + " " + dfMaintainStartTime.RawText);
            record.MaintainEndTime = Convert.ToDateTime(dfMaintainEndDate.RawText + " " + dfMaintainEndTime.RawText);
            record.ReportTime = Convert.ToDateTime(txtReportTime.Value);
            record.Maintainers = txtMaintainers.Text;
            record.Remark = txtRemark.Text.Trim();
            record.StopReason = hidden_fault_reason.Value.ToString();
            record.DealDesc = hidden_DealDesc.Text;
            record.Status = "1";
            if (record.MaintainStartTime < record.ReportTime)
            {
                X.Msg.Alert("提示", "维修开始时间必须大于报修时间！").Show();
                return;
            }
            if (record.MaintainStartTime >= record.MaintainEndTime)
            {
                X.Msg.Alert("提示", "开始时间必须早于结束时间！").Show();
                return;
            }
            
            else
            {
                if (manager.Update(record) >= 0)
                {
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
    #endregion

    #region 设定故障原因及处理方式
    [DirectMethod]
    public string setFaultReason(string rs)
    {
        try
        {
            hidden_fault_reason.Value = rs;
            string[] arrRs = rs.Split(',');
            string reasonName = "";
            string reasonDesc = "";
            EntityArrayList<EqmFaultReason> reasonList = new EntityArrayList<EqmFaultReason>();
            foreach (string item in arrRs)
            {
                reasonList.Add(reasonManager.GetById(item));
                reasonName += reasonManager.GetById(item).ReasonName + ",";
                reasonDesc += reasonManager.GetById(item).DealDesc + ",";
            }
            cbReason.Value = reasonName.TrimEnd(',');
            hidden_DealDesc.Text = reasonDesc.TrimEnd(',');
            reasonStore.Data = reasonList;
            reasonStore.DataBind();
            return "SUCCESS";
        }
        catch (Exception)
        {
            return "ERROR";
        }
    }

    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        EntityArrayList<EqmFaultReason> reasonList = new EntityArrayList<EqmFaultReason>();
        string rs = e.Parameters["StopReason"];
        if (rs != "" && rs != null && rs != "null")
        {
            string[] arrRs = rs.Split(',');
            foreach (string item in arrRs)
            {
                reasonList.Add(reasonManager.GetById(item));
            }
            this.detailStore.DataSource = reasonList;
            this.detailStore.DataBind();
        }
        else
        {

            this.detailStore.DataSource = reasonList;
            this.detailStore.DataBind();
        }
    }

    protected void SendRowSelect(object sender, StoreReadDataEventArgs e)
    {
        string orderId = e.Parameters["OrderID"];
        PageResult<EqmSparePartRepairOut> pageParams = new PageResult<EqmSparePartRepairOut>();
        pageParams.PageIndex = 1;
        pageParams.PageSize = 100;
        EqmSparePartRepairOutManager.QueryParams queryParams = new EqmSparePartRepairOutManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.orderId = orderId;
        queryParams.deleteFlag = "0";
        PageResult<EqmSparePartRepairOut> lst = outManager.GetTablePageDataBySql(queryParams);
        DataTable data = lst.DataSet.Tables[0];
        this.sendStore.DataSource = data;
        this.sendStore.DataBind();
    }
    #endregion

}