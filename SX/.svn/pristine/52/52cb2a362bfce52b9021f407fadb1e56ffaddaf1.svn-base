using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using System;
using System.Data;

/// <summary>
/// Manager_Technology_Comparison_Main 实现类
/// 孙本强 @ 2013-04-03 13:05:38
/// </summary>
/// <remarks></remarks>
public partial class Manager_Technology_Comparison_Main : Mesnac.Web.UI.Page
{
    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:05:38
    /// </summary>
    private IPmtRecipeLogManager pmtRecipeManager = new PmtRecipeLogManager();
    #endregion
    /// <summary>
    /// Gets the request.
    /// 孙本强 @ 2013-04-03 13:05:38
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
    /// Handles the Load event of the Page control.
    /// 孙本强 @ 2013-04-03 13:05:39
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    /// <remarks></remarks>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (X.IsAjaxRequest)
        {
            return;
        }
        try
        {
            IniForm();
            Comparison();
        }
        catch
        {
            return;
        }
    }
    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:05:39
    /// </summary>
    private IBasUserManager userManager = new BasUserManager();
    #endregion
    /// <summary>
    /// Gets the user.
    /// 孙本强 @ 2013-04-03 13:05:39
    /// </summary>
    /// <param name="code">The code.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string GetUser(string code)
    {
        string Result = code;
        if (string.IsNullOrWhiteSpace(code))
        {
            return Result;
        }
        EntityArrayList<BasUser> lst = userManager.GetListByWhere(BasUser._.WorkBarcode == code);
        if (lst.Count > 0)
        {
            Result = lst[0].UserName;
        }
        return Result;
    }
    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:05:39
    /// </summary>
    private IBasEquipManager basEquipManager = new BasEquipManager();
    #endregion
    /// <summary>
    /// Gets the equip.
    /// 孙本强 @ 2013-04-03 13:05:39
    /// </summary>
    /// <param name="code">The code.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string GetEquip(string code)
    {
        string Result = code;
        if (string.IsNullOrWhiteSpace(code))
        {
            return Result;
        }
        EntityArrayList<BasEquip> lst = basEquipManager.GetListByWhere(BasEquip._.EquipCode == code);
        if (lst.Count > 0)
        {
            Result = lst[0].EquipName;
        }
        return Result;
    }
    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:05:40
    /// </summary>
    private IBasMaterialManager basMaterialManager = new BasMaterialManager();
    #endregion
    /// <summary>
    /// Gets the material.
    /// 孙本强 @ 2013-04-03 13:05:40
    /// </summary>
    /// <param name="code">The code.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string GetMaterial(string code)
    {
        string Result = code;
        if (string.IsNullOrWhiteSpace(code))
        {
            return Result;
        }
        EntityArrayList<BasMaterial> lst = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == code);
        if (lst.Count > 0)
        {
            Result = lst[0].MaterialName;
        }
        return Result;
    }

    #region 属性注入
    /// <summary>
    /// 
    /// 孙本强 @ 2013-04-03 13:05:40
    /// </summary>
    private ISysCodeManager sysCodeManager = new SysCodeManager();
    #endregion
    /// <summary>
    /// Gets the name of the sys code item.
    /// 孙本强 @ 2013-04-03 13:05:40
    /// </summary>
    /// <param name="TypeID">The type ID.</param>
    /// <param name="ItemCode">The item code.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    private string GetSysCodeItemName(SysCodeManager.SysCodeType TypeID, string ItemCode)
    {
        string Result = ItemCode;
        WhereClip where = new WhereClip();
        where.And(SysCode._.TypeID == TypeID.ToString());
        where.And(SysCode._.ItemCode == ItemCode);
        EntityArrayList<SysCode> lst = sysCodeManager.GetListByWhere(where);
        if (lst.Count > 0)
        {
            Result = lst[0].ItemName;
        }
        return Result;
    }

    /// <summary>
    /// Inis the form.
    /// 孙本强 @ 2013-04-03 13:05:40
    /// </summary>
    /// <remarks></remarks>
    private void IniForm()
    {
        PmtRecipeLog aRecipe = pmtRecipeManager.GetById(Convert.ToInt32(GetRequest("a")));
        PmtRecipeLog bRecipe = pmtRecipeManager.GetById(Convert.ToInt32(GetRequest("b")));
        txtRecipeName1.Text = aRecipe.RecipeName.ToString();	//配方编号
        txtRecipeMaterialCode1.Text = GetMaterial(aRecipe.RecipeMaterialCode.ToString());	//物料名称
        txtRecipeEquipCode1.Text = GetEquip(aRecipe.RecipeEquipCode.ToString());	//机台名称
        txtRecipeType1.Text = GetSysCodeItemName(SysCodeManager.SysCodeType.PmtType, aRecipe.RecipeType.ToString());	//配方类型
        txtRecipeState1.Text = GetSysCodeItemName(SysCodeManager.SysCodeType.PmtState, aRecipe.RecipeState.ToString());	//配方状态
        txtRecipeVersionID1.Text = aRecipe.RecipeVersionID.ToString();	//版本号
        txtLotTotalWeight1.Text = aRecipe.LotTotalWeight.ToString();	//配方总重
        txtShelfLotCount1.Text = aRecipe.ShelfLotCount.ToString();	//每架车数
        txtLotDoneTime1.Text = aRecipe.LotDoneTime.ToString();	//每车标准时间
        txtOverTimeSetTime1.Text = aRecipe.OverTimeSetTime.ToString();	//超时排胶时间
        txtOverTempSetTemp1.Text = aRecipe.OverTempSetTemp.ToString();	//紧急排胶温度
        txtOverTempMinTime1.Text = aRecipe.OverTempMinTime.ToString();	//超温排胶最短时间
        txtInPolyMaxTemp1.Text = aRecipe.InPolyMaxTemp.ToString();	//最高进胶温度
        txtInPolyMinTemp1.Text = aRecipe.InPolyMinTemp.ToString();	//最低进胶温度
        txtMakeUpTemp1.Text = aRecipe.MakeUpTemp.ToString();	//补偿温度
        txtCarbonRecycleType1.Text = GetSysCodeItemName(SysCodeManager.SysCodeType.YesNo, aRecipe.CarbonRecycleType.ToString());	//炭黑是否回收
        txtCarbonRecycleTime1.Text = aRecipe.CarbonRecycleTime.ToString();	//炭黑回收时间
        txtIsUseAreaTemp1.Text = GetSysCodeItemName(SysCodeManager.SysCodeType.YesNo, aRecipe.IsUseAreaTemp.ToString());	//使用三区温度
        txtSideTemp1.Text = aRecipe.SideTemp.ToString();	//侧壁温度
        txtRollTemp1.Text = aRecipe.RollTemp.ToString();	//转子温度
        txtDdoorTemp1.Text = aRecipe.DdoorTemp.ToString();	//卸料门温度
        txtRecipeModifyUser1.Text = GetUser(aRecipe.RecipeModifyUser);	//修改人
        txtRecipeModifyTime1.Text = aRecipe.RecipeModifyTime.ToString();	//修改时间
        txtAuditUser1.Text = GetUser(aRecipe.AuditUser);	//审核人
        txtAuditDateTime1.Text = aRecipe.AuditDateTime.ToString();	//审核时间
        txtAuditFlag1.Text = GetSysCodeItemName(SysCodeManager.SysCodeType.YesNo, aRecipe.AuditFlag.ToString());	//审核状态

        txtRecipeName2.Text = bRecipe.RecipeName.ToString();	//配方编号
        txtRecipeMaterialCode2.Text = GetMaterial(bRecipe.RecipeMaterialCode.ToString());	//物料名称
        txtRecipeEquipCode2.Text = GetEquip(bRecipe.RecipeEquipCode.ToString());	//机台名称
        txtRecipeType2.Text = GetSysCodeItemName(SysCodeManager.SysCodeType.PmtType, bRecipe.RecipeType.ToString());	//配方类型
        txtRecipeState2.Text = GetSysCodeItemName(SysCodeManager.SysCodeType.PmtState, bRecipe.RecipeState.ToString());	//配方状态
        txtRecipeVersionID2.Text = bRecipe.RecipeVersionID.ToString();	//版本号
        txtLotTotalWeight2.Text = bRecipe.LotTotalWeight.ToString();	//配方总重
        txtShelfLotCount2.Text = bRecipe.ShelfLotCount.ToString();	//每架车数
        txtLotDoneTime2.Text = bRecipe.LotDoneTime.ToString();	//每车标准时间
        txtOverTimeSetTime2.Text = bRecipe.OverTimeSetTime.ToString();	//超时排胶时间
        txtOverTempSetTemp2.Text = bRecipe.OverTempSetTemp.ToString();	//紧急排胶温度
        txtOverTempMinTime2.Text = bRecipe.OverTempMinTime.ToString();	//超温排胶最短时间
        txtInPolyMaxTemp2.Text = bRecipe.InPolyMaxTemp.ToString();	//最高进胶温度
        txtInPolyMinTemp2.Text = bRecipe.InPolyMinTemp.ToString();	//最低进胶温度
        txtMakeUpTemp2.Text = bRecipe.MakeUpTemp.ToString();	//补偿温度
        txtCarbonRecycleType2.Text = GetSysCodeItemName(SysCodeManager.SysCodeType.YesNo, bRecipe.CarbonRecycleType.ToString());	//炭黑是否回收
        txtCarbonRecycleTime2.Text = bRecipe.CarbonRecycleTime.ToString();	//炭黑回收时间
        txtIsUseAreaTemp2.Text = GetSysCodeItemName(SysCodeManager.SysCodeType.YesNo, bRecipe.IsUseAreaTemp.ToString());	//使用三区温度
        txtSideTemp2.Text = bRecipe.SideTemp.ToString();	//侧壁温度
        txtRollTemp2.Text = bRecipe.RollTemp.ToString();	//转子温度
        txtDdoorTemp2.Text = bRecipe.DdoorTemp.ToString();	//卸料门温度
        txtRecipeModifyUser2.Text = GetUser(bRecipe.RecipeModifyUser);	//修改人
        txtRecipeModifyTime2.Text = bRecipe.RecipeModifyTime.ToString();	//修改时间
        txtAuditUser2.Text = GetUser(bRecipe.AuditUser);	//审核人
        txtAuditDateTime2.Text = bRecipe.AuditDateTime.ToString();	//审核时间
        txtAuditFlag2.Text = GetSysCodeItemName(SysCodeManager.SysCodeType.YesNo, bRecipe.AuditFlag);	//审核状态
    }
    /// <summary>
    /// Comparisons this instance.
    /// 孙本强 @ 2013-04-03 13:05:41
    /// </summary>
    /// <remarks></remarks>
    private void Comparison()
    {
        if (txtRecipeName1.Text != txtRecipeName2.Text) { txtRecipeName1.Cls = "HighlightHint"; txtRecipeName2.Cls = "HighlightHint"; };	//配方编号
        if (txtRecipeMaterialCode1.Text != txtRecipeMaterialCode2.Text) { txtRecipeMaterialCode1.Cls = "HighlightHint"; txtRecipeMaterialCode2.Cls = "HighlightHint"; };	//物料名称
        if (txtRecipeEquipCode1.Text != txtRecipeEquipCode2.Text) { txtRecipeEquipCode1.Cls = "HighlightHint"; txtRecipeEquipCode2.Cls = "HighlightHint"; };	//机台名称
        if (txtRecipeType1.Text != txtRecipeType2.Text) { txtRecipeType1.Cls = "HighlightHint"; txtRecipeType2.Cls = "HighlightHint"; };	//配方类型
        if (txtRecipeState1.Text != txtRecipeState2.Text) { txtRecipeState1.Cls = "HighlightHint"; txtRecipeState2.Cls = "HighlightHint"; };	//配方状态
        if (txtRecipeVersionID1.Text != txtRecipeVersionID2.Text) { txtRecipeVersionID1.Cls = "HighlightHint"; txtRecipeVersionID2.Cls = "HighlightHint"; };	//版本号
        if (txtLotTotalWeight1.Text != txtLotTotalWeight2.Text) { txtLotTotalWeight1.Cls = "HighlightHint"; txtLotTotalWeight2.Cls = "HighlightHint"; };	//配方总重
        if (txtShelfLotCount1.Text != txtShelfLotCount2.Text) { txtShelfLotCount1.Cls = "HighlightHint"; txtShelfLotCount2.Cls = "HighlightHint"; };	//每架车数
        if (txtLotDoneTime1.Text != txtLotDoneTime2.Text) { txtLotDoneTime1.Cls = "HighlightHint"; txtLotDoneTime2.Cls = "HighlightHint"; };	//每车标准时间
        if (txtOverTimeSetTime1.Text != txtOverTimeSetTime2.Text) { txtOverTimeSetTime1.Cls = "HighlightHint"; txtOverTimeSetTime2.Cls = "HighlightHint"; };	//超时排胶时间
        if (txtOverTempSetTemp1.Text != txtOverTempSetTemp2.Text) { txtOverTempSetTemp1.Cls = "HighlightHint"; txtOverTempSetTemp2.Cls = "HighlightHint"; };	//紧急排胶温度
        if (txtOverTempMinTime1.Text != txtOverTempMinTime2.Text) { txtOverTempMinTime1.Cls = "HighlightHint"; txtOverTempMinTime2.Cls = "HighlightHint"; };	//超温排胶最短时间
        if (txtInPolyMaxTemp1.Text != txtInPolyMaxTemp2.Text) { txtInPolyMaxTemp1.Cls = "HighlightHint"; txtInPolyMaxTemp2.Cls = "HighlightHint"; };	//最高进胶温度
        if (txtInPolyMinTemp1.Text != txtInPolyMinTemp2.Text) { txtInPolyMinTemp1.Cls = "HighlightHint"; txtInPolyMinTemp2.Cls = "HighlightHint"; };	//最低进胶温度
        if (txtMakeUpTemp1.Text != txtMakeUpTemp2.Text) { txtMakeUpTemp1.Cls = "HighlightHint"; txtMakeUpTemp2.Cls = "HighlightHint"; };	//补偿温度
        if (txtCarbonRecycleType1.Text != txtCarbonRecycleType2.Text) { txtCarbonRecycleType1.Cls = "HighlightHint"; txtCarbonRecycleType2.Cls = "HighlightHint"; };	//炭黑是否回收
        if (txtCarbonRecycleTime1.Text != txtCarbonRecycleTime2.Text) { txtCarbonRecycleTime1.Cls = "HighlightHint"; txtCarbonRecycleTime2.Cls = "HighlightHint"; };	//炭黑回收时间
        if (txtIsUseAreaTemp1.Text != txtIsUseAreaTemp2.Text) { txtIsUseAreaTemp1.Cls = "HighlightHint"; txtIsUseAreaTemp2.Cls = "HighlightHint"; };	//使用三区温度
        if (txtSideTemp1.Text != txtSideTemp2.Text) { txtSideTemp1.Cls = "HighlightHint"; txtSideTemp2.Cls = "HighlightHint"; };	//侧壁温度
        if (txtRollTemp1.Text != txtRollTemp2.Text) { txtRollTemp1.Cls = "HighlightHint"; txtRollTemp2.Cls = "HighlightHint"; };	//转子温度
        if (txtDdoorTemp1.Text != txtDdoorTemp2.Text) { txtDdoorTemp1.Cls = "HighlightHint"; txtDdoorTemp2.Cls = "HighlightHint"; };	//卸料门温度
        if (txtRecipeModifyUser1.Text != txtRecipeModifyUser2.Text) { txtRecipeModifyUser1.Cls = "HighlightHint"; txtRecipeModifyUser2.Cls = "HighlightHint"; };	//修改人
        if (txtRecipeModifyTime1.Text != txtRecipeModifyTime2.Text) { txtRecipeModifyTime1.Cls = "HighlightHint"; txtRecipeModifyTime2.Cls = "HighlightHint"; };	//修改时间
        if (txtAuditUser1.Text != txtAuditUser2.Text) { txtAuditUser1.Cls = "HighlightHint"; txtAuditUser2.Cls = "HighlightHint"; };	//审核人
        if (txtAuditDateTime1.Text != txtAuditDateTime2.Text) { txtAuditDateTime1.Cls = "HighlightHint"; txtAuditDateTime2.Cls = "HighlightHint"; };	//审核时间
        if (txtAuditFlag1.Text != txtAuditFlag2.Text) { txtAuditFlag1.Cls = "HighlightHint"; txtAuditFlag2.Cls = "HighlightHint"; };	//审核状态
    }
}