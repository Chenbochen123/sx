using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Entity;
using NBear.Common;

public partial class Manager_Equipment_SparePart_DetailType : Mesnac.Web.UI.Page
{
    protected SysCodeManager sysCodeManager = new SysCodeManager();
    protected EqmSparePartMainTypeManager mainManager = new EqmSparePartMainTypeManager();
    protected EqmSparePartDetailTypeManager manager = new EqmSparePartDetailTypeManager();

    protected void Page_Load( object sender , EventArgs e )
    {
        if ( !X.IsAjaxRequest )
        {
            bindMainType();
            bindDeleteFlagAutoIn();
            bindList();
            this.winSave.Hidden = true;
        }
    }
    #region 自定义方法
    private void bindMainType()
    {
        EntityArrayList<EqmSparePartMainType> list = mainManager.GetListByWhere( EqmSparePartMainType._.DeleteFlag == "0" );
        this.storeSpareParMainType.DataSource = list;
        this.storeSpareParMainType.DataBind();
        this.storeMainType.DataSource = list;
        this.storeMainType.DataBind();
    }
    private void bindDeleteFlagAutoIn()
    {
        EntityArrayList<SysCode> list = sysCodeManager.GetListByWhere( SysCode._.TypeID == "YesNo" );
        this.storeDelete.DataSource = list;
        this.storeAutoIn.DataSource = list;
        this.storeDelete.DataBind();
        this.storeAutoIn.DataBind();
        this.cbDelete.SelectedItem.Index = 0;
        this.cbAutoIn.SelectedItem.Index = 0;
    }
    private DataSet getList()
    {
        EqmSparePartDetailTypeParams _params = new EqmSparePartDetailTypeParams();
        _params.mainTypeID = this.cbSparePartMainType.SelectedItem.Value;
        _params.detailTypeName = this.txtSparePartDetailTypeName.Text.Trim();

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
    protected void btnAdd_Click( object sender , EventArgs e )
    {
        this.hideMode.Text = "Add";
        this.txtObjID.Text = string.Empty;
        this.txtDetailTypeCode.Text = string.Empty;
        this.txtDetailTypeName.Text = string.Empty;
        this.txtRemark.Text = string.Empty;
        this.cbMainType.Disabled = false;

        this.winSave.Hidden = false;
    }
    protected void btnSave_Click( object sender , EventArgs e )
    {
        int iRecord = -1;
        EqmSparePartDetailType type;
        WhereClip wc = EqmSparePartDetailType._.DetailTypeName == this.txtDetailTypeName.Text.Trim();

        if ( this.hideMode.Text.Equals( "Add" ) )
        {
            if ( manager.GetRowCountByWhere( wc ) > 0 )
            {
                X.Msg.Alert( "提示" , "备件小类名称与其它记录重复！" ).Show();
                return;
            }

            type = new EqmSparePartDetailType();
            type.DetailTypeCode = manager.GetNextCode();
            type.DetailTypeName = this.txtDetailTypeName.Text.Trim();
            type.MainTypeID = int.Parse( this.cbMainType.SelectedItem.Value );
            type.AutoIn = this.cbAutoIn.SelectedItem.Value;
            type.Remark = this.txtRemark.Text.Trim();
            type.DeleteFlag = this.cbDelete.SelectedItem.Value;

            iRecord = manager.Insert( type );
        }
        else if ( this.hideMode.Text.Equals( "Edit" ) )
        {
            if ( manager.GetRowCountByWhere( wc & EqmSparePartDetailType._.ObjID != this.txtObjID.Text ) > 0 )
            {
                X.Msg.Alert( "提示" , "备件小类名称与其它记录重复！" ).Show();
                return;
            }
            type = manager.GetById( Convert.ToInt32( this.txtObjID.Text ) );
            type.DetailTypeName = this.txtDetailTypeName.Text.Trim();
            type.AutoIn = this.cbAutoIn.SelectedItem.Value;
            type.Remark = this.txtRemark.Text.Trim();
            type.DeleteFlag = this.cbDelete.SelectedItem.Value;

            iRecord = manager.Update( type );
        }

        if ( iRecord >= 0 )
        {
            X.Msg.Notify( "提示" , "保存成功！" ).Show();
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
        this.winSave.Hidden = true;
    }
    #endregion

    #region GridPanel事件响应
    [DirectMethod]
    public void pnlList_Edit( string commandName , string ObjID )
    {
        EqmSparePartDetailType type = manager.GetById( Convert.ToInt32( ObjID ) );
        if ( type != null )
        {
            this.hideMode.Text = "Edit";
            this.txtObjID.Text = type.ObjID.ToString();
            this.txtObjID.Disabled = true;
            this.txtDetailTypeCode.Text = type.DetailTypeCode;
            this.txtDetailTypeCode.Disabled = true;
            this.txtDetailTypeName.Text = type.DetailTypeName;
            this.txtRemark.Text = type.Remark;
            this.cbMainType.SelectedItem.Value = type.MainTypeID.ToString();
            this.cbMainType.Disabled = true;
            //this.cbAutoIn.SelectedItem.Value = type.AutoIn;
            //this.cbDelete.SelectedItem.Value = type.DeleteFlag;
            this.cbAutoIn.Select(type.AutoIn);
            this.cbDelete.Select(type.DeleteFlag);

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