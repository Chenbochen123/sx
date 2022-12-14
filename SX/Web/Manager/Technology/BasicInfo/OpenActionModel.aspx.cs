using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using NBear.Common;
using Mesnac.Business.Implements;
using Mesnac.Entity;
using System.Text.RegularExpressions;
using Mesnac.Data.Components;
using System.Data;
using Mesnac.Business.Interface;
using Newtonsoft.Json;

public partial class Manager_Technology_BasicInfo_OpenActionModel : Mesnac.Web.UI.Page
{

    #region 属性注入
    /// <summary>
    /// 
    /// 袁洋 @ 2013-04-03 13:06:35
    /// </summary>
    private IPmtOpenActionModelDetailManager openActionModelDetailManager = new PmtOpenActionModelDetailManager();
    private IPmtOpenActionModelMainManager openActionModelMainManager = new PmtOpenActionModelMainManager();
    private IPmtOpenActionManager openActionManager = new PmtOpenActionManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    #endregion

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            添加 = new SysPageAction() { ActionID = 1, ActionName = "btn_add" };
            查询 = new SysPageAction() { ActionID = 2, ActionName = "btn_search" };
            历史查询 = new SysPageAction() { ActionID = 3, ActionName = "btn_history_search" };
            修改 = new SysPageAction() { ActionID = 4, ActionName = "Edit" };
            删除 = new SysPageAction() { ActionID = 5, ActionName = "Delete" };
            恢复 = new SysPageAction() { ActionID = 6, ActionName = "Recover" };
            编辑 = new SysPageAction() { ActionID = 7, ActionName = "btnCanSave" };
            保存 = new SysPageAction() { ActionID = 8, ActionName = "btnSave" };

        }
        public SysPageAction 添加 { get; private set; } //必须为 public
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 历史查询 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 恢复 { get; private set; } //必须为 public
        public SysPageAction 编辑 { get; private set; } //必须为 public
        public SysPageAction 保存 { get; private set; } //必须为 public
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
            FillsetOpenActionCode();
        }
    }

    /// <summary>
    /// Gets the request.
    /// 袁洋 @ 2013-04-03 13:06:36
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string GetRequest(string key)
    {
        if (this.Request[key] != null)
        {
            return this.Request[key].ToString();
        }
        return string.Empty;
    }

    /// <summary>
    /// Fillsets the tem code.
    /// 袁洋 @ 2013-04-03 13:06:35
    /// </summary>
    /// <remarks></remarks>
    private void FillsetOpenActionCode()
    {
        WhereClip where = new WhereClip();
        where.And(PmtOpenAction._.DeleteFlag == 0);
        OrderByClip order = new OrderByClip();
        order = PmtOpenAction._.SeqIdx.Asc;
        EntityArrayList<PmtOpenAction> lst = openActionManager.GetListByWhereAndOrder(where, order);
        setOpenActionCode0.Items.Clear();
        setOpenActionCode1.Items.Clear();
        setOpenActionCode2.Items.Clear();
        setOpenActionCode3.Items.Clear();
        setOpenActionCode4.Items.Clear();
        setOpenActionCode5.Items.Clear();
        setOpenActionCode6.Items.Clear();
        if (true)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Text = "　";
            item.Value = "　";
            setOpenActionCode0.Items.Add(item);
            setOpenActionCode1.Items.Add(item);
            setOpenActionCode2.Items.Add(item);
            setOpenActionCode3.Items.Add(item);
            setOpenActionCode4.Items.Add(item);
            setOpenActionCode5.Items.Add(item);
            setOpenActionCode6.Items.Add(item);
        }
        foreach (PmtOpenAction m in lst)
        {
            Ext.Net.ListItem item = new Ext.Net.ListItem();
            item.Text = m.ShowName;
            item.Value = m.ActionCode;
            setOpenActionCode0.Items.Add(item);
            setOpenActionCode1.Items.Add(item);
            setOpenActionCode2.Items.Add(item);
            setOpenActionCode3.Items.Add(item);
            setOpenActionCode4.Items.Add(item);
            setOpenActionCode5.Items.Add(item);
            setOpenActionCode6.Items.Add(item);
        }
    }
    #endregion

    #region 分页相关方法
    private PageResult<PmtOpenActionModelMain> GetPageResultData(PageResult<PmtOpenActionModelMain> pageParams)
    {
        PmtOpenActionModelMainManager.QueryParams queryParams = new PmtOpenActionModelMainManager.QueryParams();
        queryParams.ModelName = txt_model_name.Text.TrimEnd().TrimStart();
        queryParams.DeleteFlag = hidden_delete_flag.Text;

        return openActionModelMainManager.GetTablePageDataBySql(queryParams);
    }

    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<PmtOpenActionModelMain> pageParams = new PageResult<PmtOpenActionModelMain>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = prms.Sort[0].Property + " " + prms.Sort[0].Direction;

        PageResult<PmtOpenActionModelMain> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 开炼信息
    /// <summary>
    /// Mixings the grid panel bind data.
    /// 袁洋 @ 2013-04-03 13:06:36
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="extraParams">The extra params.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public object MixingGridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        int total = 0;
        EntityArrayList<PmtOpenActionModelDetail> lst = new EntityArrayList<PmtOpenActionModelDetail>();
        string mainId = "";
        string MixingNo = extraParams["MixingNo"].ToString();
        
        if (!string.IsNullOrWhiteSpace(hidden_main_id.Text))
        {
            mainId = hidden_main_id.Text;
        }
        if (!string.IsNullOrWhiteSpace(mainId))
        {
            lst = openActionModelDetailManager.GetListByWhereAndOrder(
                PmtOpenActionModelDetail._.MainModelID == mainId && PmtOpenActionModelDetail._.OpenMixingNo == MixingNo,
                PmtOpenActionModelDetail._.MixingStep.Asc );
        }
        int modelcount = lst.Count;
        int pagesize = 20;
        for (int i = pagesize; i > modelcount; i--)
        {
            PmtOpenActionModelDetail m = new PmtOpenActionModelDetail();
            m.MixingStep = i;
            lst.Add(m);
        }
        total = lst.Count;
        return new { data = lst, total = lst.Count };
    }

    /// <summary>
    /// Saves the json info.
    /// 袁洋 @ 2013-04-03 13:06:17
    /// </summary>
    /// <param name="main">The main.</param>
    /// <param name="mixing">The mixing.</param>
    /// <param name="weight">The weight.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    [DirectMethod]
    public string SaveJsonInfo(string mix0, string mix1, string mix2, string mix3, string mix4, string mix5, string mix6)
    {
        if (this._.编辑.SeqIdx == 0)
        {
            return "您没有进行编辑保存的权限！";
        }
        mix0 = Unescape(mix0).Replace("　", "").Replace("<br>", "");
        mix1 = Unescape(mix1).Replace("　", "").Replace("<br>", "");
        mix2 = Unescape(mix2).Replace("　", "").Replace("<br>", "");
        mix3 = Unescape(mix3).Replace("　", "").Replace("<br>", "");
        mix4 = Unescape(mix4).Replace("　", "").Replace("<br>", "");
        mix5 = Unescape(mix5).Replace("　", "").Replace("<br>", "");
        mix6 = Unescape(mix6).Replace("　", "").Replace("<br>", "");
        string Result = string.Empty;
        JavaScriptArray arry = new JavaScriptArray();
        #region 开炼动作#0
        EntityArrayList<PmtOpenActionModelDetail> mixlist0 = new EntityArrayList<PmtOpenActionModelDetail>();
        arry = (JavaScriptArray)JavaScriptConvert.DeserializeObject(mix0);
        IniPmtOpenActionModelDetail(arry, ref mixlist0);
        Result = openActionModelDetailManager.SaveOpenActionModelDetail(hidden_main_id.Text, mixlist0 , "0");
        #endregion
        #region 开炼动作#1
        EntityArrayList<PmtOpenActionModelDetail> mixlist1 = new EntityArrayList<PmtOpenActionModelDetail>();
        arry = (JavaScriptArray)JavaScriptConvert.DeserializeObject(mix1);
        IniPmtOpenActionModelDetail(arry, ref mixlist1);
        Result = openActionModelDetailManager.SaveOpenActionModelDetail(hidden_main_id.Text, mixlist1, "1");
        #endregion
        #region 开炼动作#2
        EntityArrayList<PmtOpenActionModelDetail> mixlist2 = new EntityArrayList<PmtOpenActionModelDetail>();
        arry = (JavaScriptArray)JavaScriptConvert.DeserializeObject(mix2);
        IniPmtOpenActionModelDetail(arry, ref mixlist2);
        Result = openActionModelDetailManager.SaveOpenActionModelDetail(hidden_main_id.Text, mixlist2, "2");
        #endregion
        #region 开炼动作#3
        EntityArrayList<PmtOpenActionModelDetail> mixlist3 = new EntityArrayList<PmtOpenActionModelDetail>();
        arry = (JavaScriptArray)JavaScriptConvert.DeserializeObject(mix3);
        IniPmtOpenActionModelDetail(arry, ref mixlist3);
        Result = openActionModelDetailManager.SaveOpenActionModelDetail(hidden_main_id.Text, mixlist3, "3");
        #endregion
        #region 开炼动作#4
        EntityArrayList<PmtOpenActionModelDetail> mixlist4 = new EntityArrayList<PmtOpenActionModelDetail>();
        arry = (JavaScriptArray)JavaScriptConvert.DeserializeObject(mix4);
        IniPmtOpenActionModelDetail(arry, ref mixlist4);
        Result = openActionModelDetailManager.SaveOpenActionModelDetail(hidden_main_id.Text, mixlist4, "4");
        #endregion
        #region 开炼动作#5
        EntityArrayList<PmtOpenActionModelDetail> mixlist5 = new EntityArrayList<PmtOpenActionModelDetail>();
        arry = (JavaScriptArray)JavaScriptConvert.DeserializeObject(mix5);
        IniPmtOpenActionModelDetail(arry, ref mixlist5);
        Result = openActionModelDetailManager.SaveOpenActionModelDetail(hidden_main_id.Text, mixlist5, "5");
        #endregion
        #region 开炼动作#6
        EntityArrayList<PmtOpenActionModelDetail> mixlist6 = new EntityArrayList<PmtOpenActionModelDetail>();
        arry = (JavaScriptArray)JavaScriptConvert.DeserializeObject(mix6);
        IniPmtOpenActionModelDetail(arry, ref mixlist6);
        Result = openActionModelDetailManager.SaveOpenActionModelDetail(hidden_main_id.Text, mixlist6, "6");
        #endregion
        return Result;
    }

    /// <summary>
    /// Inis the PMT recipe mixing.
    /// 袁洋 @ 2013-04-03 13:06:16
    /// </summary>
    /// <param name="arry">The arry.</param>
    /// <param name="lst">The LST.</param>
    /// <remarks></remarks>
    private void IniPmtOpenActionModelDetail(JavaScriptArray arry, ref  EntityArrayList<PmtOpenActionModelDetail> lst)
    {
        if (arry == null)
        {
            return;
        }

        int MixingStep = 1;
        Mesnac.Util.BaseInfo.ObjectConverter converter = new Mesnac.Util.BaseInfo.ObjectConverter();
        for (int i = 0; i < arry.Count; i++)
        {
            JavaScriptObject record = (JavaScriptObject)arry[i];
            PmtOpenActionModelDetail m = new PmtOpenActionModelDetail();
            m.OpenActionCode = converter.ToString(record["OpenActionCode"]);
            m.MixingStep = MixingStep++;
            m.MainModelID = converter.ToString(record["MainModelID"]);
            m.OpenMixingNo = converter.ToString(record["OpenMixingNo"]);
            m.MixTime = converter.ToInt(record["MixTime"]);
            m.CoolMixSpeed = converter.ToDecimal(record["CoolMixSpeed"]);
            m.OpenMixSpeed = converter.ToDecimal(record["OpenMixSpeed"]);
            m.MixRollor = converter.ToDecimal(record["MixRollor"]);
            m.WaterTemp = converter.ToDecimal(record["WaterTemp"]);
            m.RubberTemp = converter.ToDecimal(record["RubberTemp"]);
            m.CarSpeed = converter.ToDecimal(record["CarSpeed"]);
            lst.Add(m);
        }
    }

    /// <summary>
    /// Unescapes the specified ss.
    /// 袁洋 @ 2013-04-03 13:06:16
    /// </summary>
    /// <param name="ss">The ss.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string Unescape(string ss)
    {
        return System.Text.RegularExpressions.Regex.Unescape(ss);
    }
    #endregion

    #region 模板信息增删改查按钮激发的事件
    /// <summary>
    /// 点击添加按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_add_Click(object sender, EventArgs e)
    {
        btnAddSave.Disable(true);
        add_model_name.Text = "";
        add_model_valid_date.Text = "";
        add_model_detail.Text = "";
        add_remark.Text = "";
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
        PmtOpenActionModelMain modelMain = openActionModelMainManager.GetById(Convert.ToInt32(objID));
        modify_obj_id.Text = modelMain.ObjID.ToString();
        modify_model_name.Text = modelMain.ModelName;
        hidden_model_name.Text = modelMain.ModelName;
        modify_model_valid_date.Text = modelMain.ModelValidDate.ToString();
        modify_model_detail.Text = modelMain.ModelDetail;
        modify_remark.Text = modelMain.Remark;
        this.winModify.Show();
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
            PmtOpenActionModelMain modelMain = openActionModelMainManager.GetById(objID);
            modelMain.DeleteFlag = "1";
            openActionModelMainManager.Update(modelMain);
            this.AppendWebLog("模板信息删除", "模板编号：" + objID);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return "删除成功";
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
            PmtOpenActionModelMain modelMain = openActionModelMainManager.GetById(obj_id);
            modelMain.DeleteFlag = "0";
            openActionModelMainManager.Update(modelMain);
            this.AppendWebLog("模板信息恢复", "模板编号：" + obj_id);
            pageToolBar.DoRefresh();
        }
        catch (Exception e)
        {
            return "恢复失败：" + e;
        }
        return "恢复成功";
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
            EntityArrayList<PmtOpenActionModelMain> modelMainList = openActionModelMainManager.GetListByWhere(
                PmtOpenActionModelMain._.ModelName == add_model_name.Text.TrimStart().TrimEnd());
            if (modelMainList.Count > 0)
            {
                X.Msg.Alert("提示", "此模板名称已被使用！").Show();
                return;
            }

            PmtOpenActionModelMain modelMain = new PmtOpenActionModelMain();
            modelMain.ModelName = (string)(add_model_name.Text);
            modelMain.ModelCreateUser = this.UserID;
            modelMain.ModelCreateDate = DateTime.Now;
            modelMain.ModelValidDate = Convert.ToDateTime(add_model_valid_date.Text);
            modelMain.ModelDetail = add_model_detail.Text;
            modelMain.Remark = (string)(add_remark.Text);
            modelMain.DeleteFlag = "0";
            openActionModelMainManager.Insert(modelMain);
            this.AppendWebLog("模板信息增加", "模板编号：" + modelMain.ObjID);
            pageToolBar.DoRefresh();
            this.winAdd.Close();
            msg.Notify("操作", "保存成功").Show();
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
        {
            EntityArrayList<PmtOpenActionModelMain> modelMainList = openActionModelMainManager.GetListByWhere(
                PmtOpenActionModelMain._.ModelName == modify_model_name.Text.TrimStart().TrimEnd());
            if (modelMainList.Count > 0)
            {
                if (modelMainList[0].ModelName != hidden_model_name.Text)
                {
                    X.Msg.Alert("提示", "此模板名称已被使用！").Show();
                    return;
                }
            }
            PmtOpenActionModelMain modelMain = openActionModelMainManager.GetById(modify_obj_id.Text);
            modelMain.ModelName = (string)(modify_model_name.Text);
            modelMain.ModelValidDate = Convert.ToDateTime(modify_model_valid_date.Text);
            modelMain.ModelDetail = modify_model_detail.Text;
            modelMain.Remark = (string)(modify_remark.Text);
            openActionModelMainManager.Update(modelMain);
            this.AppendWebLog("模板信息修改", "模板编号：" + modify_obj_id.Text);
            pageToolBar.DoRefresh();
            this.winModify.Close();
            msg.Notify("操作", "更新成功").Show();
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
    /// 检查单位名称是否重复
    /// yuany
    /// 2013年1月31日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckModelName(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        string unitname = field.Text;
        EntityArrayList<PmtOpenActionModelMain> unitList = openActionModelMainManager.GetListByWhere(PmtOpenActionModelMain._.ModelName == unitname);
        if (unitList.Count == 0)
        {
            e.Success = true;
        }
        else
        {
            if (unitList[0].ModelName.ToString() == hidden_model_name.Value.ToString())
            {

                e.Success = true;
            }
            else
            {
                e.Success = false;
                e.ErrorMessage = "此模板名称已被使用！";
            }
        }
    }
    #endregion
}