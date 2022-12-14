using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Web.UI.HtmlControls;

using NBear;
using NBear.Common;

using Ext.Net;

using Mesnac.Business.Interface;
using Mesnac.Business.Implements;
using Mesnac.Data.Interface;
using Mesnac.Data.Implements;
using Mesnac.Entity;
using Mesnac.Web.UI;

public partial class Manager_RubberQuality_BasicInfo_CPKSetting : BasePage
{

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            浏览 = new SysPageAction() { ActionID = 0, ActionName = "" };
            查询 = new SysPageAction() { ActionID = 1, ActionName = "ButtonNorthQuery" };
            添加 = new SysPageAction() { ActionID = 2, ActionName = "ButtonNorthAdd" };
            修改 = new SysPageAction() { ActionID = 3, ActionName = "ButtonNorthEdit" };
            删除 = new SysPageAction() { ActionID = 4, ActionName = "ButtonNorthDelete" };
        }
        public SysPageAction 浏览 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
    }
    #endregion

    /// <summary>
    /// 页面加载
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            #region 加载CSS样式
            HtmlGenericControl cssLink = new HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/main.css"));
            this.Page.Header.Controls.Add(cssLink);

            cssLink = new System.Web.UI.HtmlControls.HtmlGenericControl("link");
            cssLink.Attributes.Add("type", "text/css");
            cssLink.Attributes.Add("rel", "stylesheet");
            cssLink.Attributes.Add("href", this.ResolveUrl("~/resources/css/ext-chinese-font.css"));
            this.Page.Header.Controls.Add(cssLink);
            #endregion 加载CSS样式


            BindGridPanel();
        }
    }

    /// <summary>
    /// 绑定
    /// </summary>
    private void BindGridPanel()
    {
        IQmtCPKSettingManager bQmtCPKSettingManager = new QmtCPKSettingManager();
        DataSet ds = bQmtCPKSettingManager.GetAllDataSet();
        StoreCenter.DataSource = ds;
        StoreCenter.DataBind();
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthQuery_Click(object sender, DirectEventArgs e)
    {
        BindGridPanel();

        X.Msg.Alert("提示", "查询完毕").Show();
    }

    /// <summary>
    /// 打开新增窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonNorthAdd_Click(object sender, DirectEventArgs e)
    {
        IQmtCPKSettingManager bQmtCPKSettingManager = new QmtCPKSettingManager();
        if (bQmtCPKSettingManager.GetRowCount() > 0)
        {
            X.Msg.Alert("提示", "CPK指标记录已存在且只允许存在一条，请核实！").Show();
            return;
        }
        InitWindowEdit();
        WindowEdit.Show();
    }

    // 编辑窗口初始化
    private void InitWindowEdit()
    {
        HiddenEditObjId.SetValue("");
        NumberFieldEditCAMin.SetValue(0);
        NumberFieldEditCAMax.SetValue(0);
        NumberFieldEditCPMin.SetValue(0);
        NumberFieldEditCPMax.SetValue(0);
        NumberFieldEditCPKMin.SetValue(0);
        NumberFieldEditCPKMax.SetValue(0);
        NumberFieldEditNDDeviation.SetValue(0);
        NumberFieldEditJSDeviation.SetValue(0);
        NumberFieldEditYDDeviation.SetValue(0);
        NumberFieldEditBZDeviation.SetValue(0);
        NumberFieldEditMLDeviation.SetValue(0);
        NumberFieldEditMHDeviation.SetValue(0);
        NumberFieldEditTs1Deviation.SetValue(0);
        NumberFieldEditT25Deviation.SetValue(0);
        NumberFieldEditT30Deviation.SetValue(0);
        NumberFieldEditT60Deviation.SetValue(0);
        NumberFieldEditT90Deviation.SetValue(0);
        NumberFieldEditCCDeviation.SetValue(0);
    }

    // 打开修改窗口
    protected void ButtonNorthEdit_Click(object sender, DirectEventArgs e)
    {
        if (CheckboxSelectionModelCenter.SelectedRows.Count == 0)
        {
            X.Msg.Alert("提示", "请选择要修改的记录").Show();
            return;
        }
        string objId = CheckboxSelectionModelCenter.SelectedRecordID;
        //X.Msg.Notify("", objId).Show(); return;
        IQmtCPKSettingManager bQmtCPKSettingManager = new QmtCPKSettingManager();
        QmtCPKSetting mQmtCPKSetting = bQmtCPKSettingManager.GetById(new object[] { objId });
        if (mQmtCPKSetting == null)
        {
            X.Msg.Alert("提示", "未找到要修改的记录").Show();
            return;
        }

        InitWindowEdit();

        SetWindowEdit(mQmtCPKSetting);

        WindowEdit.Show();
    }

    /// <summary>
    /// 编辑窗口赋值
    /// </summary>
    /// <param name="mQmtCPKSetting"></param>
    private void SetWindowEdit(QmtCPKSetting mQmtCPKSetting)
    {
        HiddenEditObjId.SetValue(mQmtCPKSetting.ObjId.ToString());
        NumberFieldEditCAMin.SetValue(mQmtCPKSetting.CAMin);
        NumberFieldEditCAMax.SetValue(mQmtCPKSetting.CAMax);
        NumberFieldEditCPMin.SetValue(mQmtCPKSetting.CPMin);
        NumberFieldEditCPMax.SetValue(mQmtCPKSetting.CPMax);
        NumberFieldEditCPKMin.SetValue(mQmtCPKSetting.CPKMin);
        NumberFieldEditCPKMax.SetValue(mQmtCPKSetting.CPKMax);
        NumberFieldEditNDDeviation.SetValue(mQmtCPKSetting.NDDeviation);
        NumberFieldEditJSDeviation.SetValue(mQmtCPKSetting.JSDeviation);
        NumberFieldEditYDDeviation.SetValue(mQmtCPKSetting.YDDeviation);
        NumberFieldEditBZDeviation.SetValue(mQmtCPKSetting.BZDeviation);
        NumberFieldEditMLDeviation.SetValue(mQmtCPKSetting.MLDeviation);
        NumberFieldEditMHDeviation.SetValue(mQmtCPKSetting.MHDeviation);
        NumberFieldEditTs1Deviation.SetValue(mQmtCPKSetting.Ts1Deviation);
        NumberFieldEditT25Deviation.SetValue(mQmtCPKSetting.T25Deviation);
        NumberFieldEditT30Deviation.SetValue(mQmtCPKSetting.T30Deviation);
        NumberFieldEditT60Deviation.SetValue(mQmtCPKSetting.T60Deviation);
        NumberFieldEditT90Deviation.SetValue(mQmtCPKSetting.T90Deviation);
        NumberFieldEditCCDeviation.SetValue(mQmtCPKSetting.CCDeviation);

    }

    /// <summary>
    /// 保存
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonEditAccept_Click(object sender, DirectEventArgs e)
    {
        string objId = HiddenEditObjId.Value.ToString();

        IQmtCPKSettingManager bQmtCPKSettingManager = new QmtCPKSettingManager();
        QmtCPKSetting mQmtCPKSetting = new QmtCPKSetting();
        if (objId != "")
        {
            mQmtCPKSetting = bQmtCPKSettingManager.GetById(new object[] { objId });
            if (mQmtCPKSetting == null)
            {
                X.Msg.Alert("提示", "未找到要修改的记录").Show();
                return;
            }

        }
        mQmtCPKSetting.CAMin = Convert.ToDecimal(NumberFieldEditCAMin.Value);
        mQmtCPKSetting.CAMax = Convert.ToDecimal(NumberFieldEditCAMax.Value);
        mQmtCPKSetting.CPMin = Convert.ToDecimal(NumberFieldEditCPMin.Value);
        mQmtCPKSetting.CPMax = Convert.ToDecimal(NumberFieldEditCPMax.Value);
        mQmtCPKSetting.CPKMin = Convert.ToDecimal(NumberFieldEditCPKMin.Value);
        mQmtCPKSetting.CPKMax = Convert.ToDecimal(NumberFieldEditCPKMax.Value);
        mQmtCPKSetting.NDDeviation = Convert.ToDecimal(NumberFieldEditNDDeviation.Value);
        mQmtCPKSetting.JSDeviation = Convert.ToDecimal(NumberFieldEditJSDeviation.Value);
        mQmtCPKSetting.YDDeviation = Convert.ToDecimal(NumberFieldEditYDDeviation.Value);
        mQmtCPKSetting.BZDeviation = Convert.ToDecimal(NumberFieldEditBZDeviation.Value);
        mQmtCPKSetting.MLDeviation = Convert.ToDecimal(NumberFieldEditMLDeviation.Value);
        mQmtCPKSetting.MHDeviation = Convert.ToDecimal(NumberFieldEditMHDeviation.Value);
        mQmtCPKSetting.Ts1Deviation = Convert.ToDecimal(NumberFieldEditTs1Deviation.Value);
        mQmtCPKSetting.T25Deviation = Convert.ToDecimal(NumberFieldEditT25Deviation.Value);
        mQmtCPKSetting.T30Deviation = Convert.ToDecimal(NumberFieldEditT30Deviation.Value);
        mQmtCPKSetting.T60Deviation = Convert.ToDecimal(NumberFieldEditT60Deviation.Value);
        mQmtCPKSetting.T90Deviation = Convert.ToDecimal(NumberFieldEditT90Deviation.Value);
        mQmtCPKSetting.CCDeviation = Convert.ToDecimal(NumberFieldEditCCDeviation.Value);

        string userName = new BasUserManager().GetTopNListWhereOrder(1, BasUser._.WorkBarcode == this.UserID, BasUser._.DeleteFlag.Asc).First().UserName;;
        if (objId == "")
        {
            mQmtCPKSetting.RecorderId = this.UserID;
            mQmtCPKSetting.RecorderName = userName;
            mQmtCPKSetting.RecordTime = DateTime.Now;

            bQmtCPKSettingManager.Insert(mQmtCPKSetting);

        }
        else
        {
            mQmtCPKSetting.ModifierId = this.UserID;
            mQmtCPKSetting.ModifierName = userName;
            mQmtCPKSetting.ModifyTime = DateTime.Now;

            bQmtCPKSettingManager.Update(mQmtCPKSetting);

        }

        WindowEdit.Close();

        BindGridPanel();

        X.Msg.Alert("提示", "保存成功").Show();
    }
}