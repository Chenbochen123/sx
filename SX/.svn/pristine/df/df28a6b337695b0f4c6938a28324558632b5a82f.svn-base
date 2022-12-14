using System;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using NBear.Common;


public partial class Manager_Equipment_StopManage_StopRecord : Mesnac.Web.UI.Page
{
    protected SysCodeManager sysCodeManager = new SysCodeManager();
    protected PptShiftManager shiftManager = new PptShiftManager();
    protected BasWorkShopManager shopManager = new BasWorkShopManager();
    protected BasEquipManager equipManager = new BasEquipManager();
    protected EqmStopTypeManager typeManager = new EqmStopTypeManager();
    protected EqmStopFaultManager faultManager = new EqmStopFaultManager();
    protected EqmFaultReasonManager reasonManager = new EqmFaultReasonManager();
    protected EqmMaintainRecordManager maintainManager = new EqmMaintainRecordManager();
    protected EqmStopRecordManager manager = new EqmStopRecordManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !X.IsAjaxRequest&&!Page.IsPostBack )
        {
            PageInit();
        }
    }

    #region 初始化下拉列表
    private void PageInit()
    {
        this.dStartDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
        this.dEndDate.Text = DateTime.Today.ToString( "yyyy-MM-dd" );
        this.dStartTime.Text = "0:00";
        this.dEndTime.Text = "0:00";

        bindList();
        bindWorkShop();
        bindEquip();
        bindMainType();
        bindFaultType();

        this.winSave.Hidden = true;
    }
    private void bindWorkShop()
    {
        EntityArrayList<BasWorkShop> list = shopManager.GetListByWhere( BasWorkShop._.DeleteFlag == "0" );
        this.storeWorkShop.DataSource = list;
        this.storeWorkShop.DataBind();
    }
    private void bindEquip()
    {
        EntityArrayList<BasEquip> list = equipManager.GetListByWhereAndOrder( BasEquip._.DeleteFlag == "0" & BasEquip._.WorkShopCode == cbWorkShop.SelectedItem.Value, BasEquip._.EquipName.Asc );
        this.storeEquip.DataSource = list;
        this.storeEquip.DataBind();
    }
    private void bindMainType()
    {
        EntityArrayList<SysCode> list = sysCodeManager.GetListByWhereAndOrder( SysCode._.TypeID == "StopMainType" , SysCode._.ItemName.Asc );
        this.storeStopMainType.DataSource = list;
        this.storeStopMainType.DataBind();
    }
    private void bindStopType()
    {
        this.cbStopType.ClearValue();
        EntityArrayList<EqmStopType> list = typeManager.GetListByWhereAndOrder( EqmStopType._.DeleteFlag == "0" & EqmStopType._.MainTypeID == cbStopMainType.SelectedItem.Value, EqmStopType._.TypeName.Asc );
        this.storeStopType.DataSource = list;
        this.storeStopType.DataBind();
    }
    private void bindStopFault()
    {
        this.cbStopFault.ClearValue();
        EntityArrayList<EqmStopFault> list = faultManager.GetListByWhereAndOrder( EqmStopFault._.DeleteFlag == "0" & EqmStopFault._.TypeID == cbStopType.SelectedItem.Value , EqmStopFault._.FaultName.Asc );
        this.storeStopFault.DataSource = list;
        this.storeStopFault.DataBind();
    }

    private void bindFaultType()
    {
        EntityArrayList<SysCode> list = sysCodeManager.GetListByWhere( SysCode._.TypeID == "FaultType" );
        this.storeFaultType.DataSource = list;
        this.storeFaultType.DataBind();
    }
    private DataSet getList()
    {
        EqmStopRecordParams _params = new EqmStopRecordParams();
        _params.startTime = Convert.ToDateTime(dStartDate.Text).ToString("yyyy-MM-dd") + " " + dStartTime.Text;
        _params.endTime = Convert.ToDateTime(dEndDate.Text).ToString("yyyy-MM-dd") + " " + dEndTime.Text;
        _params.workShopCode = cbWorkShop.SelectedItem.Value;
        _params.equipCode = cbStopEquip.SelectedItem.Value;
        _params.mainTypeID = cbStopMainType.SelectedItem.Value;
        _params.stopTypeID = cbStopType.SelectedItem.Value;
        _params.faultID = cbStopFault.SelectedItem.Value;

        return manager.GetDataByParas( _params );
    }
    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }
    #endregion

    #region 按钮事件响应
    protected void btnSearch_Click( object sender , EventArgs e )
    {
        bindList();
    }
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = getList();
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
            new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(ds, "停机记录");
        }
    }
    protected void btnSave_Click( object sender , EventArgs e )
    {
        EqmStopRecord record = manager.GetById( int.Parse( hideObjID.Text ) );
        if ( record != null )
        {
            try
            {
                record.FaultTypeID = Convert.ToInt32(cbFaultType.SelectedItem.Value);
            }
            catch { }
            record.Maintainers = txtMaintainers.Text;
            record.Remark = txtRemark.Text.Trim();
            record.StopTypeID = hidden_stop_type.Value.ToString();
            record.FaultID = hidden_stop_fault.Value.ToString();
            record.StopReason = hidden_fault_reason.Value.ToString();
            record.UserID = this.UserID;
            
            if ( manager.Update( record ) >= 0 )
            {
                this.AppendWebLog("停机信息修改", "修改机台：" + record.EquipCode);
                winSave.Hidden = true;
                bindList();
                X.Msg.Alert("提示","修改完成！").Show();
            }
            else
            {
                X.Msg.Alert( "提示" , "修改失败！" ).Show();
            }
        }
    }
    protected void btnCancel_Click( object sender , EventArgs e )
    {
        winSave.Hidden = true;
    }
    #endregion

    #region 下拉列表事件响应
    protected void cbWorkShop_SelectChanged( object sender , EventArgs e )
    {
        bindEquip();
    }
    protected void cbStopMainType_SelectChanged( object sender , EventArgs e )
    {
        bindStopType();
    }
    protected void cbStopType_SelectChanged( object sender , EventArgs e )
    {
        bindStopFault();
    }
    #endregion

    #region 信息列表事件响应
    [DirectMethod]
    public void pnlList_Edit( string ObjID )
    {
        EqmStopRecord record = manager.GetById( int.Parse( ObjID ) );

        if (record.IsReportMaintain == "1")
        {
            X.Msg.Alert("提示", "该数据已经报修，不能修改！").Show();
            return;
        }

        if ( record != null )
        {
            EntityArrayList<BasEquip> equipList = equipManager.GetListByWhere( BasEquip._.EquipCode == record.EquipCode );
            txtShift.Text = shiftManager.GetById( record.ShiftID ).ShiftName;
            txtEquip.Text = equipList.Count > 0 ? equipList[ 0 ].EquipName : string.Empty;
            txtStartTime.Text = record.StartTime.ToString( "yyyy-MM-dd hh:mm:ss" );
            txtEndTime.Text = record.EndTime == null ? string.Empty : Convert.ToDateTime(record.EndTime).ToString( "yyyy-MM-dd hh:mm:ss" );
            cbFaultType.SelectedItem.Value = record.FaultTypeID.ToString();
            txtMaintainers.Text = record.Maintainers;
            try
            {
                cbType.Value = typeManager.GetListByWhere(EqmStopType._.TypeCode == record.StopTypeID)[0].TypeName;
                hidden_stop_type.Value = record.StopTypeID;
            }
            catch
            {
            }

            try
            {
                cbFault.Value = faultManager.GetListByWhere(EqmStopFault._.FaultCode == record.FaultID)[0].FaultName;
                hidden_stop_fault.Value = record.FaultID;
            }
            catch
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


            txtRemark.Text = record.Remark;
            hideObjID.Text = ObjID;

            this.winSave.Hidden = false;
        }
        else
        {
            bindList();
            X.Msg.Alert( "提示" , "此条记录已经不存在！" ).Show();
        }
    }
    [DirectMethod]
    public void pnlList_Delete(string ObjID)
    {
        EqmStopRecord record = manager.GetById(int.Parse(ObjID));
        if (record.IsReportMaintain == "1")
        {
            X.Msg.Alert("提示", "该数据已经报修，不能修改！").Show();
            return;
        }
        manager.Delete(int.Parse(ObjID));
        this.AppendWebLog("停机信息删除", "修改机台："+record.EquipCode );
          
        bindList();
        X.Msg.Alert("提示", "删除完成！").Show();
    }

    [DirectMethod]
    public void pnlList_Add(string ObjID)
    {
        EqmStopRecord record = manager.GetById(int.Parse(ObjID));
        //首先检查是否已报修
        if (record.IsReportMaintain == "1")
        {
            X.Msg.Alert("提示", "该数据已经报修，不能重复报修！").Show();
            return;
        }

        if (record != null)
        {
            EqmMaintainRecord maintainRecord = new EqmMaintainRecord();
            maintainRecord.EquipCode = record.EquipCode;
            maintainRecord.ShiftID = record.ShiftID;
            maintainRecord.ClassName = record.ClassName;
            maintainRecord.StartTime = record.StartTime;
            maintainRecord.EndTime = record.EndTime;
            maintainRecord.ReportTime = DateTime.Now;
            maintainRecord.FaultTypeID = record.FaultTypeID;
            maintainRecord.StopTypeID = record.StopTypeID;
            maintainRecord.FaultID = record.FaultID;
            maintainRecord.StopReason = record.StopReason;
            maintainRecord.DealDesc = record.DealDesc;
            maintainRecord.MaintainStartTime = record.MaintainStartTime;
            maintainRecord.MaintainEndTime = record.MaintainEndTime;
            maintainRecord.Maintainers = record.Maintainers;
            maintainRecord.Remark = record.Remark;
            maintainRecord.Status = "0";
            maintainRecord.UserID = this.UserID;

            if (maintainManager.InsertRecord(maintainRecord) >= 0)
            {
                //修改源数据为已报修
                record.ReportTime = DateTime.Now;
                record.IsReportMaintain = "1";
                manager.Update(record);

                X.Msg.Alert("提示", "已生成维修记录！").Show();
            }
            else
            {
                X.Msg.Alert("提示", "维修记录生成失败！").Show();
            }

        }
        else
        {
            bindList();
            X.Msg.Alert("提示", "此条记录已经不存在！").Show();
        }
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
            EntityArrayList<EqmFaultReason> reasonList = new EntityArrayList<EqmFaultReason>();
            foreach (string item in arrRs)
            {
                reasonList.Add(reasonManager.GetById(item));
                reasonName += reasonManager.GetById(item).ReasonName + ",";
            }
            cbReason.Value = reasonName.TrimEnd(',');
            reasonStore.Data = reasonList;
            reasonStore.DataBind();
            return "SUCCESS";
        }
        catch (Exception)
        {
            return "ERROR";
        }
    }
    #endregion
    
    protected void RowSelect(object sender, StoreReadDataEventArgs e)
    {
        EntityArrayList<EqmFaultReason> reasonList = new EntityArrayList<EqmFaultReason>();
        string rs = e.Parameters["StopReason"];
        if (rs != "null" && rs != null)
        {
            string[] arrRs = rs.Split(',');
            
            foreach (string item in arrRs)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    reasonList.Add(reasonManager.GetById(item));
                }
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
}