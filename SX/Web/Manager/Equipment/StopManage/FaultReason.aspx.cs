using System;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Entity;
using NBear.Common;

public partial class Manager_Equipment_StopManage_FaultReason : Mesnac.Web.UI.Page
{
    protected SysCodeManager sysCodeManager = new SysCodeManager();
    protected EqmStopTypeManager typeManager = new EqmStopTypeManager();
    protected EqmStopFaultManager faultManager = new EqmStopFaultManager();
    protected EqmFaultReasonManager manager = new EqmFaultReasonManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !X.IsAjaxRequest )
        {
            bindDeleteFlag();
            bindList();
            loadtpFault();
            this.winSave.Hidden = true;
        }
    }
    #region
    private void bindDeleteFlag()
    {
        EntityArrayList<SysCode> list = sysCodeManager.GetListByWhere( SysCode._.TypeID == "YesNo" );
        this.storeDelete.DataSource = list;
        this.storeDelete.DataBind();
        this.cbDelete.SelectedItem.Index = 0;
    }
    private DataSet getList()
    {
        EqmFaultReasonParams _params = new EqmFaultReasonParams();
        if ( tpFault.SelectedNodes != null )
        {
            int f = tpFault.SelectedNodes[0].Path.TrimStart('/').Split('/').Length;
            switch ( tpFault.SelectedNodes[ 0 ].Path.TrimStart( '/' ).Split( '/' ).Length )
            {
                case 3:
                    _params.mainTypeID = this.tpFault.SelectedNodes[ 0 ].NodeID;
                    break;
                case 4:
                    _params.typeID = this.tpFault.SelectedNodes[ 0 ].NodeID;
                    break;
                case 5:
                    _params.faultID = this.tpFault.SelectedNodes[ 0 ].NodeID;
                    break;
                default:
                    break;
            }
        }
        _params.reasonName = this.tfStopFaultReason.Text.Trim();
        _params.dealDesc = this.tfStopDealDesc.Text.Trim();

        return manager.GetDataByParas( _params );
    }
    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }
    private void loadtpFault()
    {
        Ext.Net.Node nodeL1, nodeL2, nodeL3, nodeL4;
        nodeL1 = new Ext.Net.Node()
        {
            Text = "全部",
            Expanded = true
        };
        this.tpFault.GetRootNode().RemoveAll();
        EntityArrayList<SysCode> mainList = sysCodeManager.GetListByWhereAndOrder( SysCode._.TypeID == "StopMainType" , SysCode._.ItemCode.Asc );
        EntityArrayList<EqmStopType> typeList = typeManager.GetListByWhereAndOrder( EqmStopType._.DeleteFlag == "0" , EqmStopType._.TypeCode.Asc );
        EntityArrayList<EqmStopFault> faultList = faultManager.GetListByWhereAndOrder( EqmStopFault._.DeleteFlag == "0" , EqmStopFault._.FaultCode.Asc );

        foreach ( SysCode itemL2 in mainList )
        {
            nodeL2 = new Ext.Net.Node()
            {
                NodeID = itemL2.ItemCode ,
                Text = itemL2.ItemName ,
                Expanded = false
            };
            foreach ( EqmStopType itemL3 in typeList.Filter( EqmStopType._.MainTypeID == itemL2.ItemCode ) )
            {
                nodeL3 = new Ext.Net.Node()
                {
                    NodeID = itemL3.TypeCode ,
                    Text = itemL3.TypeName ,
                    Expanded = false
                };
                foreach ( EqmStopFault itemL4 in faultList.Filter( EqmStopFault._.TypeID == itemL3.TypeCode ) )
                {
                    nodeL4 = new Ext.Net.Node()
                    {
                        NodeID = itemL4.FaultCode ,
                        Text = itemL4.FaultName ,
                        Leaf = true
                    };
                    nodeL3.Children.Add( nodeL4 );
                }
                nodeL2.Children.Add( nodeL3 );
            }
            nodeL1.Children.Add(nodeL2);
        }
        this.tpFault.GetRootNode().AppendChild(nodeL1);
        this.tpFault.GetRootNode().Expand( true );
    }
    #endregion
    #region 停机类型与故障点树事件响应
    //protected void tpFault_ReadData( object sender , NodeLoadEventArgs e )
    //{
    //    loadtpFault();
    //}
    protected void tpFault_Refresh( object sender , EventArgs e )
    {
        loadtpFault();
    }
    protected void tpFault_SelectedChange( object sender , EventArgs e )
    {
        bindList();
    }
    #endregion
    #region 按钮事件响应
    protected void btnAdd_Click( object sender , EventArgs e )
    {
        if ( tpFault.SelectedNodes == null || tpFault.SelectedNodes[ 0 ].Path.TrimStart( '/' ).Split( '/' ).Length < 4 )
        {
            X.Msg.Alert( "提示" , "请先在停机类型与故障点树形列表中选择一个故障点" ).Show();
            return;
        }

        this.tfObjID.Text = string.Empty;
        this.tfReasonName.Text = string.Empty;
        this.tfDealDesc.Text = string.Empty;
        this.hideFaultID.Text = tpFault.SelectedNodes[ 0 ].NodeID;
        this.tfFaultName.Value = tpFault.SelectedNodes[ 0 ].Text;
        this.cbDelete.Value = string.Empty;
        this.cbDelete.Disabled = false;

        this.winSave.Hidden = false;
    }
    protected void btnSearch_Click( object sender , EventArgs e )
    {
        bindList();
    }
    protected void btnSave_Click( object sender , EventArgs e )
    {
        int iRecord = -1;
        EqmFaultReason reason;
        WhereClip wc = EqmFaultReason._.ReasonName == this.tfReasonName.Text.Trim() & EqmFaultReason._.FaultID == this.hideFaultID.Text;

        if ( this.tfObjID.Text.Equals( string.Empty ) )
        {
            if ( manager.GetRowCountByWhere( wc ) > 0 )
            {
                X.Msg.Alert( "提示" , "故障点[" + this.tfFaultName.Text + "]已经存在此故障原因！" ).Show();
                return;
            }

            reason = new EqmFaultReason();
            reason.ObjID = Convert.ToInt32( manager.GetMaxValueByProperty( EqmFaultReason._.ObjID ) ) + 1;
            reason.FaultID = this.hideFaultID.Text;
            reason.ReasonName = this.tfReasonName.Text.Trim();
            reason.DealDesc = this.tfDealDesc.Text.Trim();
            reason.DeleteFlag = this.cbDelete.SelectedItem.Value;

            iRecord = manager.Insert( reason );
        }
        else
        {
            if ( manager.GetRowCountByWhere( wc & EqmFaultReason._.ObjID != this.tfObjID.Text ) > 0 )
            {
                X.Msg.Alert( "提示" , "故障点[" + this.tfFaultName.Text + "]已经存在此故障原因！" ).Show();
                return;
            }
            reason = manager.GetById( Convert.ToInt32( this.tfObjID.Text ) );
            reason.ReasonName = this.tfReasonName.Text.Trim();
            reason.DealDesc = this.tfDealDesc.Text.Trim();
            reason.DeleteFlag = this.cbDelete.SelectedItem.Value;

            iRecord = manager.Update( reason );
        }

        if ( iRecord >= 0 )
        {
            X.Msg.Alert( "提示" , "保存成功！" ).Show();
            this.winSave.Hidden = true;
            bindList();
        }
        else
        {
            X.Msg.Alert( "提示" , "保存失败！" ).Show();
        }
    }
    protected void btnCancel_Click( object sender , EventArgs e )
    {
        winSave.Hidden = true;
    }
    #endregion
    #region GridPanel事件响应
    [DirectMethod]
    public void pnlList_Edit( string commandName , string ObjID )
    {
        EqmFaultReason reason = manager.GetById( Convert.ToInt32( ObjID ) );
        if ( reason != null )
        {
            this.tfObjID.Text = reason.ObjID.ToString();
            this.tfReasonName.Text = reason.ReasonName;
            this.tfDealDesc.Text = reason.DealDesc;
            this.hideFaultID.Value = reason.FaultID;
            this.tfFaultName.Text = faultManager.GetListByWhere( EqmStopFault._.FaultCode == Convert.ToInt32( reason.FaultID ) )[ 0 ].FaultName;
            //this.cbDelete.SelectedItem.Value = reason.DeleteFlag;
            this.cbDelete.Select(reason.DeleteFlag);

            this.winSave.Hidden = false;
        }
        else
        {
            X.Msg.Alert( "提示" , "此记录没有找到，请重新操作" ).Show();
            bindList();
        }
    }
    protected void refreshList( object sender , StoreReadDataEventArgs e )
    {
        bindList();
    }
    #endregion
}