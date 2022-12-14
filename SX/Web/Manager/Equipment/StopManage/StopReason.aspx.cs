using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Entity;
using NBear.Common;

public partial class Manager_Equipment_StopManage_StopReason : Mesnac.Web.UI.Page
{
    protected SysCodeManager sysCodeManager = new SysCodeManager();
    protected Eqm_downcodeManager reasonManager = new Eqm_downcodeManager();
    protected Eqm_downikindManager manager = new Eqm_downikindManager();
    protected Eqm_MpParamManager mpParamManager = new Eqm_MpParamManager();

    protected void Page_Load(object sender, EventArgs e)
    {
        if ( !X.IsAjaxRequest )
        {
            bindMainType();
            bindMinorType();
            bindList();
            this.winSave.Hidden = true;
        }
    }

    #region 自定义方法
    private void bindMainType()
    {
        EntityArrayList<Eqm_MpParam> list = mpParamManager.GetListByWhere(Eqm_MpParam._.Param_Type == "4");
        this.sMainType.DataSource = list;
        this.sMainType.DataBind();
        this.storeMainType.DataSource = list;
        this.storeMainType.DataBind();
    }
    private void bindMinorType()
    {
        EntityArrayList<Eqm_downikind> list = manager.GetAllList();
        this.storeMp_ikindcode.DataSource = list;
        this.storeMp_ikindcode.DataBind();
        this.storeMp_ikindcodeAdd.DataSource = list;
        this.storeMp_ikindcodeAdd.DataBind();
    }
    private DataSet getList()
    {
        Eqm_downcode _params = new Eqm_downcode();
        _params.Mp_ikindcode = this.cbMp_ikindcode.SelectedItem.Value;
        _params.Mp_name = this.tfStopReasonName.Text.Trim();

        return this.GetDataByParas(_params, this.cbStopMainType.SelectedItem.Value);
    }


    public DataSet GetDataByParas(Eqm_downcode queryParams,string param_id)
    {
        StringBuilder sb = new StringBuilder();
        #region
        sb.AppendLine(@"SELECT T2.Param_Name,T1.Mp_ikindname,T.Mp_code,T.Mp_name FROM Eqm_downcode T
LEFT JOIN Eqm_downikind T1 ON T.Mp_ikindcode = T1.Mp_ikindcode
LEFT JOIN eqm_Mpparam T2 ON T1.Mp_mkindcode = T2.param_id AND T2.Param_type = 4");
        sb.AppendLine("WHERE 1=1");

        if (!string.IsNullOrEmpty(queryParams.Mp_ikindcode))
            sb.AppendLine("AND T.Mp_ikindcode=" + queryParams.Mp_ikindcode);
        if (!string.IsNullOrEmpty(queryParams.Mp_name))
            sb.AppendLine("AND T.Mp_name='" + queryParams.Mp_name + "'");
        if (!string.IsNullOrEmpty(queryParams.Mp_code))
            sb.AppendLine("AND T.Mp_code='" + queryParams.Mp_code + "'");
        if (!string.IsNullOrEmpty(param_id))
            sb.AppendLine("AND T2.param_id='" + param_id + "'");
        sb.AppendLine("ORDER BY T.Mp_code");
        #endregion

        NBear.Data.CustomSqlSection css = reasonManager.GetBySql(sb.ToString());
        return css.ToDataSet();
    }

    private void bindList()
    {
        this.store.DataSource = getList();
        this.store.DataBind();
    }


    private void changeStopType()
    {
        this.cbMp_ikindcode.ClearValue();
        EntityArrayList<Eqm_downikind> list = manager.GetListByWhereAndOrder(Eqm_downikind._.Mp_mkindcode == cbStopMainType.SelectedItem.Value, Eqm_downikind._.Mp_ikindname.Asc);
        this.storeMp_ikindcode.DataSource = list;
        this.storeMp_ikindcode.DataBind();
    }
    private void changeStopTypeAdd()
    {
        //this.cbMinorType.ClearValue();
        EntityArrayList<Eqm_downikind> list = manager.GetListByWhereAndOrder(Eqm_downikind._.Mp_mkindcode == cbMainType.SelectedItem.Value, Eqm_downikind._.Mp_ikindname.Asc);
        this.storeMp_ikindcodeAdd.DataSource = list;
        this.storeMp_ikindcodeAdd.DataBind();
    }
    protected void cbStopMainTypeAdd_SelectChanged(object sender, EventArgs e)
    {
        changeStopTypeAdd();
    }
    protected void cbStopMainType_SelectChanged(object sender, EventArgs e)
    {
        changeStopType();
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
        this.tfReasonCode.Text = string.Empty;
        this.cbMinorType.Value = string.Empty;
        this.cbMinorType.Disabled = false;
        this.cbMainType.Value = string.Empty;
        this.cbMainType.Disabled = false;
        this.tfReasonName.Text = string.Empty;

        this.winSave.Hidden = false;
    }
    protected void btnSave_Click( object sender , EventArgs e )
    {
        int iRecord = -1;
        Eqm_downcode type;
        WhereClip wc = Eqm_downcode._.Mp_code == this.tfReasonCode.Text.Trim();
        WhereClip wcName = Eqm_downcode._.Mp_name == this.tfReasonName.Text.Trim();

        if ( this.hideMode.Text.Equals( "Add" ) )
        {
            if (reasonManager.GetRowCountByWhere(wc) > 0)
            {
                X.Msg.Alert("提示", "停机详细原因编码与其它编码重复！").Show();
                return;
            }
            if (reasonManager.GetRowCountByWhere(wcName) > 0)
            {
                X.Msg.Alert("提示", "停机详细原因名称与其它记录重复！").Show();
                return;
            }

            type = new Eqm_downcode();
            type.Mp_ikindcode = this.cbMinorType.SelectedItem.Value;
            type.Mp_code = GetNextTypeCodeByParas(type);
            type.Mp_name = this.tfReasonName.Text.Trim();

            iRecord = reasonManager.Insert(type);
        }
        else if ( this.hideMode.Text.Equals( "Edit" ) )
        {
            if (reasonManager.GetRowCountByWhere(wc & Eqm_downcode._.Mp_code != this.tfReasonCode.Text) > 0)
            {
                X.Msg.Alert("提示", "停机详细原因编码与其它编码重复！").Show();
                return;
            }
            if (reasonManager.GetRowCountByWhere(wcName & Eqm_downcode._.Mp_name != this.hiddenName.Text) > 0)
            {
                X.Msg.Alert("提示", "停机详细原因名称与其它记录重复！").Show();
                return;
            }
            type = reasonManager.GetById(this.tfReasonCode.Text);
            type.Mp_name = this.tfReasonName.Text.Trim();

            iRecord = reasonManager.Update(type);
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

    public string GetNextTypeCodeByParas(Eqm_downcode eqmDowncode)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(@"SELECT '" + eqmDowncode.Mp_ikindcode + "'+RIGHT('00'+CAST(CAST(ISNULL(RIGHT(MAX(" + Eqm_downcode._.Mp_code.ColumnName + "),3),'0') AS INT)+1 AS VARCHAR(3)),3)");
        sb.AppendLine("FROM Eqm_downcode");
        sb.Append("WHERE " + Eqm_downcode._.Mp_ikindcode.ColumnName + "='" + eqmDowncode.Mp_ikindcode + "'");
        return manager.GetBySql(sb.ToString()).ToScalar().ToString();
    }

    /// <summary>
    /// 点击删除触发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string Mp_code)
    {
        try
        {
            Eqm_downcode dep = reasonManager.GetById(Mp_code);

            reasonManager.Delete(dep);
            this.AppendWebLog("停机详细原因删除", "小类编码：" + dep.Mp_code);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
    }
    protected void btnCancel_Click( object sender , EventArgs e )
    {
        this.winSave.Hidden = true;
    }
    #endregion

    #region GridPanel事件响应
    [DirectMethod]
    public void pnlList_Edit(string commandName, string Mp_code)
    {
        Eqm_downcode type = reasonManager.GetById(Convert.ToInt32(Mp_code));
        Eqm_downikind typeMinor = manager.GetById(type.Mp_ikindcode);
        Eqm_MpParam typeMain = mpParamManager.GetListByWhere(Eqm_MpParam._.Param_Type == "4" && Eqm_MpParam._.Param_id == typeMinor.Mp_mkindcode)[0];
        if ( type != null )
        {
            this.hideMode.Text = "Edit";
            this.tfReasonCode.Text = type.Mp_code;
            this.tfReasonName.Text = type.Mp_name;
            this.cbMainType.Value = typeMain.Param_id;
            this.cbMainType.Disabled = true;
            this.cbMinorType.Value = type.Mp_ikindcode;
            this.cbMinorType.Disabled = true;
            this.hiddenName.Text = type.Mp_name;
            //this.cbDelete.SelectedItem.Value = type.DeleteFlag;

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