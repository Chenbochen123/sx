using System;
using System.Data;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Entity;
using NBear.Common;

public partial class Manager_Equipment_StopManage_StopFault : Mesnac.Web.UI.Page
{
    protected SysCodeManager sysCodeManager = new SysCodeManager();
    protected EqmStopTypeManager typeManager = new EqmStopTypeManager();
    protected EqmFaultReasonManager reasonManager = new EqmFaultReasonManager();
    protected EqmStopFaultManager manager = new EqmStopFaultManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !X.IsAjaxRequest )
        {
            bindDeleteFlag();
            bindList();
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
        EqmStopFaultParams _params = new EqmStopFaultParams();
        if ( tpType.SelectedNodes != null )
        {
            switch ( tpType.SelectedNodes[ 0 ].Path.TrimStart('/').Split('/').Length )
            {
                case 3:
                    _params.mainTypeID = this.tpType.SelectedNodes[ 0 ].NodeID;
                    break;
                case 4:
                    _params.typeID = this.tpType.SelectedNodes[ 0 ].NodeID;
                    break;
                default:
                    break;
            }
        }
        _params.faultName = this.tfStopFaultName.Text.Trim();

        return manager.GetDataByParas( _params );
    }
    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }
    private void loadtpType()
    {
        this.tpType.GetRootNode().RemoveAll();
        Ext.Net.Node nodeL1;
        Ext.Net.Node nodeL2;
        Ext.Net.Node nodeL3;
        nodeL1 = new Ext.Net.Node()
        {
            Text = "全部",
            Expanded = true
        };
        EntityArrayList<SysCode> mainList = sysCodeManager.GetListByWhereAndOrder( SysCode._.TypeID == "StopMainType" , SysCode._.ItemCode.Asc );
        EntityArrayList<EqmStopType> detailList = typeManager.GetListByWhereAndOrder( EqmStopType._.DeleteFlag == "0" , EqmStopType._.TypeCode.Asc );
        foreach ( SysCode itemL2 in mainList )
        {
            nodeL2 = new Ext.Net.Node()
            {
                NodeID = itemL2.ItemCode ,
                Text = itemL2.ItemName ,
                Expanded = false
            };
            foreach ( EqmStopType itemL3 in detailList )
            {
                if ( itemL3.MainTypeID == itemL2.ItemCode )
                {
                    nodeL3 = new Ext.Net.Node()
                    {
                        NodeID = itemL3.TypeCode ,
                        Text = itemL3.TypeName ,
                        Leaf = true
                    };
                    nodeL2.Children.Add( nodeL3 );
                }
            }
            nodeL1.Children.Add(nodeL2);
        }
        this.tpType.GetRootNode().AppendChild(nodeL1);
        this.tpType.GetRootNode().Expand( true );
    }
    #endregion
    #region 停机类型树事件响应
    protected void tpType_ReadData( object sender , NodeLoadEventArgs e )
    {
        loadtpType();
    }
    protected void tpType_Refresh( object sender , EventArgs e )
    {
        loadtpType();
    }
    protected void tp_SelectedChange( object sender , EventArgs e )
    {
        bindList();
    }
    #endregion
    #region 按钮事件响应
    protected void btnAdd_Click( object sender , EventArgs e )
    {
        if ( tpType.SelectedNodes == null )
        {
            X.Msg.Alert( "提示" , "请先在停机类型树形列表中选择一个停机类型" ).Show();
            return;
        }
        else if ( tpType.SelectedNodes[ 0 ].Path.TrimStart( '/' ).Split( '/' ).Length < 3 )
        {
            X.Msg.Alert( "提示" , "请先在停机类型树形列表中选择一个停机类型" ).Show();
            return;
        }

        this.tfObjID.Text = string.Empty;
        this.tfFaultCode.Text = string.Empty;
        this.tfFaultName.Value = string.Empty;
        this.hideTypeID.Text = tpType.SelectedNodes[ 0 ].NodeID;
        this.tfTypeName.Text = tpType.SelectedNodes[0].Text;
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
        EqmStopFault fault;
        WhereClip wc = EqmStopFault._.FaultName == this.tfFaultName.Text.Trim();

        if ( this.tfObjID.Text.Equals( string.Empty ) )
        {
            if ( manager.GetRowCountByWhere( wc ) > 0 )
            {
                X.Msg.Alert( "提示" , "停机故障点名称与其它记录重复！" ).Show();
                return;
            }

            fault = new EqmStopFault();
            fault.ObjID = Convert.ToInt32( manager.GetMaxValueByProperty( EqmStopFault._.ObjID ) ) + 1;
            fault.TypeID = this.hideTypeID.Text;
            fault.FaultCode = manager.GetNextFaultCodeByParas( fault );
            fault.FaultName = this.tfFaultName.Text.Trim();
            fault.DeleteFlag = this.cbDelete.SelectedItem.Value;

            iRecord = manager.Insert( fault );
        }
        else
        {
            if ( manager.GetRowCountByWhere( wc & EqmStopFault._.ObjID != this.tfObjID.Text ) > 0 )
            {
                X.Msg.Alert( "提示" , "停机故障点名称与其它记录重复！" ).Show();
                return;
            }
            fault = manager.GetById( Convert.ToInt32( this.tfObjID.Text ) );
            fault.FaultName = this.tfFaultName.Text.Trim();
            fault.DeleteFlag = this.cbDelete.SelectedItem.Value;
            if (reasonManager.GetRowCountByWhere(EqmFaultReason._.DeleteFlag == "0" & EqmFaultReason._.FaultID == fault.FaultCode) > 0 & cbDelete.SelectedItem.Value != "0")
            {
                X.Msg.Alert("提示", "此故障点下还有可用的故障原因，不能删除！").Show();
                return;
            }

            iRecord = manager.Update( fault );
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
        EqmStopFault fault = manager.GetById( Convert.ToInt32( ObjID ) );
        if ( fault != null )
        {
            this.tfObjID.Text = fault.ObjID.ToString();
            this.tfFaultCode.Text = fault.FaultCode;
            this.tfFaultName.Text = fault.FaultName;
            this.hideTypeID.Value = fault.TypeID;
            this.tfTypeName.Text = typeManager.GetListByWhere( EqmStopType._.TypeCode == Convert.ToInt32( fault.TypeID ) )[ 0 ].TypeName;
            //this.cbDelete.SelectedItem.Value = fault.DeleteFlag;
            this.cbDelete.Select(fault.DeleteFlag);

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