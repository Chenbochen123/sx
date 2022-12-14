using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Entity;
using NBear.Common;

public partial class Manager_Equipment_SparePart_MainType : Mesnac.Web.UI.Page
{
    protected SysCodeManager sysCodeManager = new SysCodeManager();
    protected EqmSparePartDetailTypeManager detailManager = new EqmSparePartDetailTypeManager();
    protected EqmSparePartMainTypeManager manager = new EqmSparePartMainTypeManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !X.IsAjaxRequest )
        {
            bindDeleteFlag();
            bindList();
            this.winSave.Hidden = true;
        }
    }
    #region 自定义方法
    private void bindDeleteFlag()
    {
        EntityArrayList<SysCode> list = sysCodeManager.GetListByWhere( SysCode._.TypeID == "YesNo" );
        this.storeDelete.DataSource = list;
        this.storeDelete.DataBind();
        this.cbDelete.SelectedItem.Index = 0;
    }
    private DataSet getList()
    {
        EqmSparePartMainTypeParams _params = new EqmSparePartMainTypeParams();
        _params.mainTypeName = this.txtSparePartMainTypeName.Text.Trim();

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
        this.txtMainTypeName.Text = string.Empty;
        this.txtMainTypeName.Disabled = false;

        this.winSave.Hidden = false;
    }
    protected void btnSave_Click( object sender , EventArgs e )
    {
        int iRecord = -1;
        EqmSparePartMainType type;
        WhereClip wc = EqmSparePartMainType._.MainTypeName == this.txtMainTypeName.Text.Trim();

        if ( this.hideMode.Text.Equals( "Add" ) )
        {
            if ( manager.GetRowCountByWhere( wc ) > 0 )
            {
                X.Msg.Alert( "提示" , "备件大类名称与其它记录重复！" ).Show();
                return;
            }

            type = new EqmSparePartMainType();
            type.MainTypeName = this.txtMainTypeName.Text.Trim();
            type.DeleteFlag = this.cbDelete.SelectedItem.Value;

            iRecord = manager.Insert( type );
        }
        else if ( this.hideMode.Text.Equals( "Edit" ) )
        {
            if ( manager.GetRowCountByWhere( wc & EqmSparePartMainType._.ObjID != this.txtObjID.Text ) > 0 )
            {
                X.Msg.Alert( "提示" , "备件大类名称与其它记录重复！" ).Show();
                return;
            }
            if (detailManager.GetRowCountByWhere(EqmSparePartDetailType._.DeleteFlag == "0" & EqmSparePartDetailType._.MainTypeID == txtObjID.Text) > 0 & cbDelete.SelectedItem.Value != "0")
            {
                X.Msg.Alert("提示", "此备件大类下还有可用的备件小类，不能删除！").Show();
                return;
            }
            type = manager.GetById( Convert.ToInt32( this.txtObjID.Text ) );
            type.MainTypeName = this.txtMainTypeName.Text.Trim();
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
        EqmSparePartMainType type = manager.GetById( Convert.ToInt32( ObjID ) );
        if ( type != null )
        {
            this.hideMode.Text = "Edit";
            this.txtObjID.Text = type.ObjID.ToString();
            this.txtMainTypeName.Text = type.MainTypeName;
            //this.cbDelete.SelectedItem.Value = type.DeleteFlag;
            this.cbDelete.Select(type.DeleteFlag);
            //this.txtMainTypeName.Disabled = true;

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