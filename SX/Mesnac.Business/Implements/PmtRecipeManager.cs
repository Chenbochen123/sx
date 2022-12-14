using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NBear.Common;
using NBear.Data;
using System.Reflection;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    /// <summary>
    /// PmtRecipeManager 实现类
    /// 孙本强 @ 2013-04-03 12:46:24
    /// </summary>
    /// <remarks></remarks>
    public class PmtRecipeManager : BaseManager<PmtRecipe>, IPmtRecipeManager
    {

        #region 属性注入与构造方法

        /// <summary>
        /// 配方数据操作类
        /// 孙本强 @ 2013-04-03 12:46:24
        /// </summary>
        private IPmtRecipeService service;

        /// <summary>
        /// 类 PmtRecipeManager 构造函数
        /// 孙本强 @ 2013-04-03 12:46:24
        /// </summary>
        /// <remarks></remarks>
        public PmtRecipeManager()
        {
            this.service = new PmtRecipeService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtRecipeManager 构造函数
        /// 孙本强 @ 2013-04-03 12:46:25
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public PmtRecipeManager(string connectStringKey)
        {
            this.service = new PmtRecipeService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// 类 PmtRecipeManager 构造函数
        /// 孙本强 @ 2013-04-03 12:46:25
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtRecipeManager(NBear.Data.Gateway way)
        {
            this.service = new PmtRecipeService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region 信息查询
        #region 查询条件类定义
        /// <summary>
        /// 查询条件定义类
        /// 孙本强 @ 2013-04-03 12:46:25
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : PmtRecipeService.QueryParams
        {
        }
        #endregion

        /// <summary>
        /// 根据物料名称获取物料配方别名信息
        /// 孙宜建
        /// 2013-2-25
        /// 孙本强 @ 2013-04-03 12:46:26
        /// </summary>
        /// <param name="recipeMaterialName">物料名称</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataSet GetRecipeNameByRecipeMaterialName(string recipeMaterialName)
        {
            return this.service.GetRecipeNameByRecipeMaterialName(recipeMaterialName);
        }
        /// <summary>
        /// 获取物料信息
        /// 孙本强 @ 2013-04-03 12:18:08
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<BasMaterial> GetBasMaterial(QueryParams queryParams)
        {
            return this.service.GetBasMaterial(queryParams);
        }
        /// <summary>
        /// 获取分页数据集
        /// 孙本强 @ 2013-04-03 12:46:26
        /// </summary>
        /// <param name="queryParams">查询参数</param>
        /// <returns>分页数据集</returns>
        /// <remarks></remarks>
        public PageResult<PmtRecipe> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        /// <summary>
        /// 通过拼音获取前物料信息
        /// 孙本强 @ 2013-04-03 12:18:08
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="equipCode">The equip code.</param>
        /// <param name="searchkey">The searchkey.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<PmtRecipe> GetDistinctRecipeMaterialNameAndCode(int top, string equipCode, string searchkey)
        {
            return this.service.GetDistinctRecipeMaterialNameAndCode(top, equipCode, searchkey);
        }
        #endregion

        #region 配方信息审核

        #region 属性注入
        /// <summary>
        /// 配方日志
        /// 孙本强 @ 2013-04-03 12:46:27
        /// </summary>
        private IPmtRecipeLogManager pmtRecipeLogManager = new PmtRecipeLogManager();
        #endregion

        /// <summary>
        /// 审核工艺配方
        /// 孙本强 @ 2013-04-03 12:18:09
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="pass">if set to <c>true</c> [pass].</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string AuditPmtRecipe(string id, bool pass, string userid)
        {
            string AuditFlag = pass ? "1" : "0";
            string Result = string.Empty;
            WhereClip where = new WhereClip();
            where.And(PmtRecipe._.ObjID == id.ToString());
            EntityArrayList<PmtRecipe> lst = this.GetListByWhere(where);
            for (int i = 0; i < lst.Count; i++)
            {
                PmtRecipe m = lst[i];

                Result = ValidityPmtInfo(m);

                if (!string.IsNullOrWhiteSpace(Result))
                {
                    return Result;
                }
                if (m.RecipeState != "1")
                {
                    Result = "当前配方不是正用状态，不能进行审核！";
                    return Result;
                }
                else if (m.AuditFlag != "0")
                {
                    Result = "当前配方已经审核！";
                    return Result;
                }
                else
                {
                    // if (string.IsNullOrWhiteSpace(m.CanAuditUser) || m.CanAuditUser.Contains(userid))
                    {
                        m.AuditFlag = AuditFlag;
                        m.AuditDateTime = DateTime.Now;
                        m.AuditUser = userid;
                        //this.Update(m);
                        string sql = "update Pmt_Recipe set audit_flag ='" + AuditFlag + "',audit_name = '" + userid + @"',
                                      audit_date=convert(char(10),getdate(),120) where objid ='" + m.ObjID.ToString() + "'";
                        this.GetBySql(sql).ToDataSet();
                        this.service.RefreshPmtRecipe(m.ObjID.ToString());
                    }
                    EntityArrayList<PmtRecipeLog> loglst = pmtRecipeLogManager.GetListByWhereAndOrder(PmtRecipeLog._.LogObjID == m.ObjID, PmtRecipeLog._.ObjID.Desc);
                    if (loglst.Count > 0)
                    {
                        loglst[0].AuditFlag = AuditFlag;
                        loglst[0].AuditDateTime = DateTime.Now;
                        loglst[0].AuditUser = userid;
                        pmtRecipeLogManager.Update(loglst[0]);
                    }

                }
            }
            if (!string.IsNullOrWhiteSpace(Result))
            {
                Result = "当前用户不能审核此配方";
            }
            return Result;
        }
        #endregion

        #region 配方信息删除
        /// <summary>
        /// 删除工艺配方
        /// 孙本强 @ 2013-04-03 12:18:09
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string DeletePmtRecipe(string id, string userid)
        {
            string Result = string.Empty;
            WhereClip where = new WhereClip();
            where.And(PmtRecipe._.ObjID == id.ToString());
            EntityArrayList<PmtRecipe> lst = this.GetListByWhere(where);
            for (int i = 0; i < lst.Count; i++)
            {
                PmtRecipe m = lst[i];
                if (m.RecipeState == "2")
                {
                    //  weightManager.DeleteByWhere(PmtRecipeWeight._.RecipeObjID == m.ObjID);
                    //  mixingManager.DeleteByWhere(PmtRecipeMixing._.RecipeObjID == m.ObjID);
                    // openManager.DeleteByWhere(PmtRecipeOpenMixing._.RecipeObjID == m.ObjID);
                    // this.Delete(m.ObjID);
                    // this.DeleteByWhere(PmtRecipe._.ObjID == m.ObjID);
                    string sql = "delete from pmt_recipe where mater_Code='" + m.RecipeMaterialCode + "' and equip_Code='" + m.RecipeEquipCode + "' and edt_Code='" + m.RecipeVersionID + "'";
                    this.GetBySql(sql).ToDataSet();
                    sql = "delete from pmt_weight where recipe_Code='" + m.RecipeMaterialCode + "' and equip_Code='" + m.RecipeEquipCode + "' and edt_Code='" + m.RecipeVersionID + "'";
                    this.GetBySql(sql).ToDataSet();
                    sql = "delete from pmt_mixing where recipe_Code='" + m.RecipeMaterialCode + "' and equip_Code='" + m.RecipeEquipCode + "' and edt_Code='" + m.RecipeVersionID + "'";
                    this.GetBySql(sql).ToDataSet();
                }
                else
                {
                    return "工艺配方需要作废后再进行删除！";
                }
            }
            if (!string.IsNullOrWhiteSpace(Result))
            {
                Result = "当前用户不能删除此配方";
            }
            return Result;
        }
        #endregion

        #region 配方信息保存
        #region 属性注入
        /// <summary>
        /// 系统编码
        /// 孙本强 @ 2013-04-03 12:46:28
        /// </summary>
        private ISysCodeManager sysCodeManager = new SysCodeManager();
        /// <summary>
        /// 物料信息
        /// 孙本强 @ 2013-04-03 12:46:29
        /// </summary>
        private IBasMaterialManager basMaterialManager = new BasMaterialManager();
        /// <summary>
        /// 机台信息
        /// 孙本强 @ 2013-04-03 12:46:29
        /// </summary>
        private IBasEquipManager basEquipManager = new BasEquipManager();

        /// <summary>
        /// 称量信息
        /// 孙本强 @ 2013-04-03 12:46:29
        /// </summary>
        private IPmtRecipeWeightManager weightManager = new PmtRecipeWeightManager();
        /// <summary>
        /// 称量动作
        /// 孙本强 @ 2013-04-03 12:46:30
        /// </summary>
        private IPmtWeightActionManager pmtWeightActionManager = new PmtWeightActionManager();
        /// <summary>
        /// 料仓信息
        /// 孙本强 @ 2013-04-03 12:46:30
        /// </summary>
        private IPmtEquipJarStoreManager pmtEquipJarStoreManager = new PmtEquipJarStoreManager();

        /// <summary>
        /// 混炼信息
        /// 孙本强 @ 2013-04-03 12:46:30
        /// </summary>
        private IPmtRecipeMixingManager mixingManager = new PmtRecipeMixingManager();

        /// <summary>
        /// 开炼信息
        /// 袁洋 @ 2013-04-03 12:46:30
        /// </summary>
        private IPmtRecipeOpenMixingManager openManager = new PmtRecipeOpenMixingManager();
        /// <summary>
        /// 预分散信息
        /// 闫志旭 @ 2015-01-05 12:46:30
        /// </summary>
        private IPmtRecipeWeightMidManager WeightMidManager = new PmtRecipeWeightMidManager();
        private IPmtPMILLMainManager PMILLMainManager = new PmtPMILLMainManager();
        private IPmtSMILLMainManager SMILLMainManager = new PmtSMILLMainManager();
        private IPmtCoolMILLMainManager CoolMILLMainManager = new PmtCoolMILLMainManager();
        /// <summary>
        /// 密炼动作
        /// 孙本强 @ 2013-04-03 12:46:30
        /// </summary>
        private IPmtActionManager pmtActionManager = new PmtActionManager();
        /// <summary>
        /// 开炼动作
        /// 孙本强 @ 2013-04-03 12:46:30
        /// </summary>
        private IPmtOpenActionManager pmtOpenActionManager = new PmtOpenActionManager();
        /// <summary>
        /// 密炼条件
        /// 孙本强 @ 2013-04-03 12:46:31
        /// </summary>
        private IPmtTermManager pmtTermManager = new PmtTermManager();
        #endregion
        #region 定义数据类型
        /// <summary>
        /// RecipeInfo 实现类
        /// 孙本强 @ 2013-04-03 12:46:31
        /// </summary>
        /// <remarks></remarks>
        private class RecipeInfo
        {
            /// <summary>
            /// 配方信息
            /// 孙本强 @ 2013-04-03 12:46:31
            /// </summary>
            /// <value>The PMT recipe.</value>
            /// <remarks></remarks>
            public PmtRecipe PmtRecipe { get; set; }
            /// <summary>
            /// 物料信息
            /// 孙本强 @ 2013-04-03 12:46:31
            /// </summary>
            /// <value>The bas material.</value>
            /// <remarks></remarks>
            public BasMaterial BasMaterial { get; set; }
            /// <summary>
            /// 机台信息
            /// 孙本强 @ 2013-04-03 12:46:32
            /// </summary>
            /// <value>The bas equip.</value>
            /// <remarks></remarks>
            public BasEquip BasEquip { get; set; }
            /// <summary>
            /// 料仓信息
            /// 孙本强 @ 2013-04-03 12:46:32
            /// </summary>
            /// <value>The PMT equip jar store.</value>
            /// <remarks></remarks>
            public EntityArrayList<PmtEquipJarStore> PmtEquipJarStore { get; set; }
        }
        /// <summary>
        /// RecipeMixingInfo 实现类
        /// 孙本强 @ 2013-04-03 12:46:32
        /// </summary>
        /// <remarks></remarks>
        private class RecipeMixingInfo
        {
            /// <summary>
            /// 密炼信息
            /// 孙本强 @ 2013-04-03 12:46:32
            /// </summary>
            /// <value>The PMT recipe mixing.</value>
            /// <remarks></remarks>
            public PmtRecipeMixing PmtRecipeMixing { get; set; }
            /// <summary>
            /// 密炼条件
            /// 孙本强 @ 2013-04-03 12:46:32
            /// </summary>
            /// <value>The PMT term.</value>
            /// <remarks></remarks>
            public PmtTerm PmtTerm { get; set; }
            /// <summary>
            /// 密炼动作
            /// 孙本强 @ 2013-04-03 12:46:32
            /// </summary>
            /// <value>The PMT action.</value>
            /// <remarks></remarks>
            public PmtAction PmtAction { get; set; }
        }
        /// <summary>
        /// RecipeOpenMixingInfo 实现类
        /// 袁洋 @ 2013-04-03 12:46:32
        /// </summary>
        /// <remarks></remarks>
        private class RecipeOpenMixingInfo
        {
            /// <summary>
            /// 开炼信息
            /// 袁洋 @ 2013-04-03 12:46:32
            /// </summary>
            /// <value>The PMT recipe mixing.</value>
            /// <remarks></remarks>
            public PmtRecipeOpenMixing PmtRecipeOpenMixing { get; set; }
            /// <summary>
            /// 开炼动作
            /// 袁洋 @ 2013-04-03 12:46:32
            /// </summary>
            /// <value>The PMT action.</value>
            /// <remarks></remarks>
            public PmtOpenAction PmtOpenAction { get; set; }
        }
        /// <summary>
        /// RecipeWeightInfo 实现类
        /// 孙本强 @ 2013-04-03 12:46:32
        /// </summary>
        /// <remarks></remarks>
        private class RecipeWeightInfo
        {
            /// <summary>
            /// 称量信息
            /// 孙本强 @ 2013-04-03 12:46:33
            /// </summary>
            /// <value>The PMT recipe weight.</value>
            /// <remarks></remarks>
            public PmtRecipeWeight PmtRecipeWeight { get; set; }
            /// <summary>
            /// 物料信息
            /// 孙本强 @ 2013-04-03 12:46:33
            /// </summary>
            /// <value>The bas material.</value>
            /// <remarks></remarks>
            public BasMaterial BasMaterial { get; set; }
            /// <summary>
            /// 称量动作
            /// 孙本强 @ 2013-04-03 12:46:33
            /// </summary>
            /// <value>The PMT weight action.</value>
            /// <remarks></remarks>
            public PmtWeightAction PmtWeightAction { get; set; }
        }
        /// <summary>
        /// 称量类型
        /// 孙本强 @ 2013-04-03 12:46:33
        /// </summary>
        /// <remarks></remarks>
        private enum WeightType
        {
            /// <summary>
            /// 
            /// </summary>
            炭黑称量信息 = 0,
            /// <summary>
            /// 
            /// </summary>
            油1称量信息 = 1,
            /// <summary>
            /// 
            /// </summary>
            胶料称量信息 = 2,
            /// <summary>
            /// 
            /// </summary>
            小料校核称量信息 = 3,
            /// <summary>
            /// 
            /// </summary>
            粉料称量信息 = 4,
            /// <summary>
            /// 
            /// </summary>
            油2称量信息 = 5,
            /// <summary>
            /// 
            /// </summary>
            小料称量信息 = 9,
        }
        /// <summary>
        /// 密炼状态
        /// 孙本强 @ 2013-04-03 12:46:34
        /// </summary>
        /// <remarks></remarks>
        private enum PmtState
        {
            /// <summary>
            /// 
            /// </summary>
            用完 = 0,
            /// <summary>
            /// 
            /// </summary>
            正用 = 1,
            /// <summary>
            /// 
            /// </summary>
            作废 = 2,
        }
        /// <summary>
        /// 称量动作
        /// 孙本强 @ 2013-04-03 12:46:35
        /// </summary>
        /// <remarks></remarks>
        private enum WeightAction
        {
            /// <summary>
            /// 
            /// </summary>
            称量 = 0,
            /// <summary>
            /// 
            /// </summary>
            称到 = 1,
            /// <summary>
            /// 
            /// </summary>
            卸料 = 2,
        }
        /// <summary>
        /// 物料大类
        /// 孙本强 @ 2013-04-03 12:46:36
        /// </summary>
        /// <remarks></remarks>
        private enum MajorType
        {
            /// <summary>
            /// 
            /// </summary>
            原材料 = 1,
            /// <summary>
            /// 
            /// </summary>
            小料 = 2,
            /// <summary>
            /// 
            /// </summary>
            塑 = 3,
        }
        /// <summary>
        /// 机台类型
        /// 孙本强 @ 2013-04-03 12:46:36
        /// </summary>
        /// <remarks></remarks>
        private enum EquipType
        {
            /// <summary>
            /// 
            /// </summary>
            混炼机 = 1,
            /// <summary>
            /// 
            /// </summary>
            小料称 = 2,
            /// <summary>
            /// 
            /// </summary>
            炭黑输送 = 3,
            /// <summary>
            /// 
            /// </summary>
            外供机台 = 4,
            /// <summary>
            /// 
            /// </summary>
            其他设备 = 5,
        }
        /// <summary>
        /// 密炼动作
        /// 孙本强 @ 2013-04-03 12:46:37
        /// </summary>
        /// <remarks></remarks>
        private enum MixingAction
        {
            /// <summary>
            /// 
            /// </summary>
            加胶料 = 1,
            /// <summary>
            /// 
            /// </summary>
            加炭黑 = 2,
            /// <summary>
            /// 
            /// </summary>
            加油料 = 3,
            /// <summary>
            /// 
            /// </summary>
            加粉料 = 3,
            压上顶栓 = 4,
            开卸料门 = 5,
            保持 = 6,
            升上顶栓上到位 = 7,
            升上顶栓中到位 = 7,
            上顶栓浮动 = 9,
            开加料门 = 10,
            关加料门 = 11,
            /// <summary>
            /// 
            /// </summary>
            加小料 = 12,
            关卸料门 = 13,
            恒温炼胶 = 14,
            升栓开卸料门 = 15,
            /// <summary>
            /// 
            /// </summary>

            /// <summary>
            /// 
            /// </summary>

            /// <summary>
            /// 
            /// </summary>

            /// <summary>
            /// 
            /// </summary>

            /// <summary>
            /// 
            /// </summary>

            /// <summary>
            /// 
            /// </summary>

            /// <summary>
            /// 
            /// </summary>

            /// <summary>
            /// 
            /// </summary>

            ///// <summary>
            ///// 
            ///// </summary>
            //上顶栓浮动 = 14,
            ///// <summary>
            ///// 
            ///// </summary>
            //混炼计时 = 15,
            ///// <summary>
            ///// 
            ///// </summary>
            //能量初始化 = 16,
            ///// <summary>
            ///// 
            ///// </summary>
            //加油料2 = 17,
        }
        #endregion
        #region 初始化数据
        /// <summary>
        /// 获取配方版本号
        /// 孙本强 @ 2013-04-03 12:46:40
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private int GetMaxRecipeVersionID(RecipeInfo recipeInfo)
        {
            int Result = 0;
            PmtRecipe pmtRecipe = recipeInfo.PmtRecipe;
            EntityArrayList<PmtRecipe> lst = this.GetListByWhere(
                PmtRecipe._.RecipeEquipCode == pmtRecipe.RecipeEquipCode
                && PmtRecipe._.RecipeMaterialCode == pmtRecipe.RecipeMaterialCode);
            foreach (PmtRecipe m in lst)
            {
                if (recipeInfo.PmtRecipe.ObjID == m.ObjID)
                {
                    return Convert.ToInt32(m.RecipeVersionID);
                }
                if (Result < m.RecipeVersionID)
                {
                    Result = Convert.ToInt32(m.RecipeVersionID);
                }
            }
            return Result + 1;
        }
        /// <summary>
        /// 获取信息编码
        /// 孙本强 @ 2013-04-03 12:46:40
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private SysCode GetSysCode(SysCodeManager.SysCodeType type, string code)
        {
            SysCode Result = null;
            EntityArrayList<SysCode> lst = sysCodeManager.GetListByWhere(SysCode._.TypeID == type.ToString() && SysCode._.ItemCode == code);
            if (lst.Count > 0)
            {
                Result = lst[0];
            }
            return Result;
        }
        /// <summary>
        /// 初始化配方信息
        /// 孙本强 @ 2013-04-03 12:46:40
        /// </summary>
        /// <param name="pmtRecipe">The PMT recipe.</param>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string IniPmtRecip(PmtRecipe pmtRecipe, ref RecipeInfo recipeInfo)
        {
            string Result = string.Empty;
            recipeInfo = new RecipeInfo();
            recipeInfo.PmtRecipe = pmtRecipe;
            if (string.IsNullOrWhiteSpace(pmtRecipe.RecipeMaterialCode))
            {
                return "物料信息不能为空";
            }
            if (pmtRecipe.ShelfLotCount == null || (int)pmtRecipe.ShelfLotCount <= 0)
            {
                return "每架车数不能为空";
            }
            if (string.IsNullOrWhiteSpace(pmtRecipe.RecipeEquipCode))
            {
                return "机台信息不能为空";
            }
            EntityArrayList<BasMaterial> basMaterialList = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == pmtRecipe.RecipeMaterialCode);
            if (basMaterialList.Count == 0)
            {
                return "物料信息不存在";
            }
            EntityArrayList<BasEquip> basEquipList = basEquipManager.GetListByWhere(BasEquip._.EquipCode == pmtRecipe.RecipeEquipCode);
            if (basMaterialList.Count == 0)
            {
                return "机台信息不存在";
            }
            if (pmtRecipe.RecipeType == null)
            {
                return "请设置配方类型";
            }
            recipeInfo.BasMaterial = basMaterialList[0];
            recipeInfo.BasEquip = basEquipList[0];
            //    recipeInfo.PmtEquipJarStore = pmtEquipJarStoreManager.GetListByWhere(PmtEquipJarStore._.EquipCode == pmtRecipe.RecipeEquipCode);
            return Result;
        }
        /// <summary>
        /// 初始化混炼信息
        /// 孙本强 @ 2013-04-03 12:46:41
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <param name="pmtRecipeMixing">The PMT recipe mixing.</param>
        /// <param name="recipeMixingInfo">The recipe mixing info.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string IniPmtRecipeMixing(RecipeInfo recipeInfo, EntityArrayList<PmtRecipeMixing> pmtRecipeMixing, ref List<RecipeMixingInfo> recipeMixingInfo)
        {
            string Result = string.Empty;
            recipeMixingInfo = new List<RecipeMixingInfo>();
            int i = 0;
            foreach (PmtRecipeMixing mixing in pmtRecipeMixing)
            {
                i++;
                RecipeMixingInfo info = new RecipeMixingInfo();
                info.PmtRecipeMixing = mixing;
                if (string.IsNullOrWhiteSpace(mixing.ActionCode) && (
                    ((mixing.MixingTime != null) && ((int)mixing.MixingTime > 0))
                    || ((mixing.MixingTemp != null) && ((int)mixing.MixingTemp > 0))
                    || ((mixing.MixingEnergy != null) && ((int)mixing.MixingEnergy > 0))
                    || ((mixing.MixingPower != null) && ((int)mixing.MixingPower > 0))
                    || ((mixing.MixingPress != null) && ((int)mixing.MixingPress > 0))
                    || ((mixing.MixingSpeed != null) && ((int)mixing.MixingSpeed > 0))
                    || (!string.IsNullOrWhiteSpace(mixing.TermCode))
                    ))
                {
                    return "第" + i.ToString() + "步需要添加混炼动作！";
                }
                if (string.IsNullOrWhiteSpace(mixing.ActionCode))
                {
                    recipeMixingInfo.Add(new RecipeMixingInfo());
                    continue;
                }
                EntityArrayList<PmtAction> pmtActionList = pmtActionManager.GetListByWhere(PmtAction._.ActionCode == mixing.ActionCode);
                if (pmtActionList.Count == 0)
                {
                    return "混炼动作不存在：" + mixing.ActionCode;
                }
                info.PmtAction = pmtActionList[0];
                if (!string.IsNullOrWhiteSpace(mixing.TermCode))
                {
                    EntityArrayList<PmtTerm> pmtTermList = pmtTermManager.GetListByWhere(PmtTerm._.TermCode == mixing.TermCode);
                    if (pmtTermList.Count == 0)
                    {
                        return "混炼条件不存在：" + mixing.TermCode;
                    }
                    info.PmtTerm = pmtTermList[0];
                }
                info.PmtRecipeMixing.RecipeObjID = recipeInfo.PmtRecipe.ObjID;
                info.PmtRecipeMixing.RecipeEquipCode = recipeInfo.PmtRecipe.RecipeEquipCode;
                info.PmtRecipeMixing.RecipeMaterialCode = recipeInfo.PmtRecipe.RecipeMaterialCode;
                info.PmtRecipeMixing.RecipeVersionID = Convert.ToInt32(recipeInfo.PmtRecipe.RecipeVersionID);
                recipeMixingInfo.Add(info);
            }
            return Result;
        }

        /// <summary>
        /// 初始化开炼信息
        /// 孙本强 @ 2013-04-03 12:46:41
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <param name="pmtRecipeMixing">The PMT recipe mixing.</param>
        /// <param name="recipeMixingInfo">The recipe mixing info.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string IniPmtRecipeOpenMixing(RecipeInfo recipeInfo, EntityArrayList<PmtRecipeOpenMixing> pmtRecipeOpenMixing, ref List<RecipeOpenMixingInfo> recipeOpenMixingInfo)
        {
            string Result = string.Empty;
            recipeOpenMixingInfo = new List<RecipeOpenMixingInfo>();
            int i = 0;
            foreach (PmtRecipeOpenMixing mixing in pmtRecipeOpenMixing)
            {
                i++;
                RecipeOpenMixingInfo info = new RecipeOpenMixingInfo();
                info.PmtRecipeOpenMixing = mixing;
                if (string.IsNullOrWhiteSpace(mixing.OpenActionCode) && (
                    ((mixing.MixTime != null) && ((int)mixing.MixTime > 0))
                    || ((mixing.CoolMixSpeed != null) && ((int)mixing.CoolMixSpeed > 0))
                    || ((mixing.OpenMixSpeed != null) && ((int)mixing.OpenMixSpeed > 0))
                    || ((mixing.MixRollor != null) && ((int)mixing.MixRollor > 0))
                    || ((mixing.WaterTemp != null) && ((int)mixing.WaterTemp > 0))
                    || ((mixing.RubberTemp != null) && ((int)mixing.RubberTemp > 0))
                    || ((mixing.CarSpeed != null) && ((int)mixing.CarSpeed > 0))
                    ))
                {
                    return "第" + i.ToString() + "步需要添加开炼动作！";
                }
                if (string.IsNullOrWhiteSpace(mixing.OpenActionCode))
                {
                    recipeOpenMixingInfo.Add(new RecipeOpenMixingInfo());
                    continue;
                }
                EntityArrayList<PmtOpenAction> pmtOpenActionList = pmtOpenActionManager.GetListByWhere(PmtOpenAction._.ActionCode == mixing.OpenActionCode);
                if (pmtOpenActionList.Count == 0)
                {
                    return "混炼动作不存在";
                }
                info.PmtOpenAction = pmtOpenActionList[0];

                info.PmtRecipeOpenMixing.RecipeObjID = Convert.ToInt32(recipeInfo.PmtRecipe.ObjID);
                info.PmtRecipeOpenMixing.RecipeEquipCode = recipeInfo.PmtRecipe.RecipeEquipCode;
                info.PmtRecipeOpenMixing.RecipeMaterialCode = recipeInfo.PmtRecipe.RecipeMaterialCode;
                info.PmtRecipeOpenMixing.RecipeVersionID = Convert.ToInt32(recipeInfo.PmtRecipe.RecipeVersionID);
                recipeOpenMixingInfo.Add(info);
            }
            return Result;
        }

        /// <summary>
        /// 初始化称量信息
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <param name="pmtRecipeWeight">The PMT recipe weight.</param>
        /// <param name="recipeWeightInfo">The recipe weight info.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string IniPmtRecipeWeight(RecipeInfo recipeInfo, EntityArrayList<PmtRecipeWeight> pmtRecipeWeight, ref List<RecipeWeightInfo> recipeWeightInfo)
        {
            string Result = string.Empty;
            recipeWeightInfo = new List<RecipeWeightInfo>();
            int IndexInt = 1;
            foreach (PmtRecipeWeight weight in pmtRecipeWeight)
            {
                //if (string.IsNullOrEmpty(weight.MaterialCode))  会加一个空的卸料
                //    continue;

                RecipeWeightInfo info = new RecipeWeightInfo();
                info.PmtRecipeWeight = weight;
                if (string.IsNullOrWhiteSpace(weight.ActCode))
                {
                    recipeWeightInfo.Add(new RecipeWeightInfo());
                    continue;
                }
                EntityArrayList<PmtWeightAction> pmtWeightActionList = pmtWeightActionManager.GetListByWhere(PmtWeightAction._.ActionCode == weight.ActCode);
                if (pmtWeightActionList.Count == 0)
                {
                    return "称量动作不存在";
                }

                if ((!string.IsNullOrEmpty(weight.RecipeMaterialCode)) && (!string.IsNullOrEmpty(weight.MaterialCode)))
                {
                    if (weight.RecipeMaterialCode.Substring(0, 1) == "5" && weight.MaterialCode.Substring(0, 1) == "2")//终胶配方检测检量误差
                    {
                        if (!string.IsNullOrEmpty(weight.CheckWeight.ToString()))
                        {
                            if (weight.CheckWeight != 0)
                            {
                                if (string.IsNullOrEmpty(weight.CheckError.ToString()))
                                {
                                    return "请输入" + weight.MaterialName + "检量误差";
                                }
                                if (weight.CheckError == 0)
                                {

                                    return "请输入" + weight.MaterialName + "检量误差";

                                }

                            }

                        }
                    }
                }

                info.PmtWeightAction = pmtWeightActionList[0];
                if (weight.ActCode.Trim() == ((int)WeightAction.卸料).ToString())
                {
                    info.BasMaterial = null;
                    info.PmtRecipeWeight.MaterialCode = "";
                    info.PmtRecipeWeight.MaterialName = "";
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(weight.MaterialCode))
                    {
                        recipeWeightInfo.Add(new RecipeWeightInfo());
                        continue;
                    }
                    EntityArrayList<BasMaterial> basMaterialList = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == weight.MaterialCode);
                    if (basMaterialList.Count > 0)
                    {
                        info.BasMaterial = basMaterialList[0];
                        info.PmtRecipeWeight.MaterialName = info.BasMaterial.MaterialName;
                    }
                }
                info.PmtRecipeWeight.RecipeObjID = recipeInfo.PmtRecipe.ObjID;
                info.PmtRecipeWeight.RecipeEquipCode = recipeInfo.PmtRecipe.RecipeEquipCode;
                info.PmtRecipeWeight.RecipeMaterialCode = recipeInfo.PmtRecipe.RecipeMaterialCode;
                info.PmtRecipeWeight.RecipeVersionID = Convert.ToInt32(recipeInfo.PmtRecipe.RecipeVersionID);
                info.PmtRecipeWeight.WeightID = IndexInt++;
                recipeWeightInfo.Add(info);
            }

            return Result;
        }
        #endregion
        #region 校验数据
        /// <summary>
        /// 称量动作是卸料
        /// 孙本强 @ 2013-04-03 12:46:42
        /// </summary>
        /// <param name="weightAction">The weight action.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool WeightActionIs卸料(string weightAction)
        {
            if (string.IsNullOrWhiteSpace(weightAction))
            {
                return false;
            }
            if (weightAction.Trim() == ((int)WeightAction.卸料).ToString())
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 称量是否需要卸料
        /// 孙本强 @ 2013-04-03 12:46:42
        /// </summary>
        /// <param name="weightType">Type of the weight.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool WeightTypeNeed卸料(string weightType)
        {
            if (string.IsNullOrWhiteSpace(weightType))
            {
                return false;
            }
            if (weightType.Trim() == ((int)WeightType.炭黑称量信息).ToString())
            {
                return true;
            }
            if (weightType.Trim() == ((int)WeightType.油1称量信息).ToString())
            {
                return true;
            }
            if (weightType.Trim() == ((int)WeightType.油2称量信息).ToString())
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 校验密炼信息
        /// 孙本强 @ 2013-04-03 12:46:42
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <param name="recipeMixingInfo">The recipe mixing info.</param>
        /// <param name="recipeWeightInfo">The recipe weight info.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string ValidityPmtRecipe(RecipeInfo recipeInfo, List<RecipeMixingInfo> recipeMixingInfo, List<RecipeWeightInfo> recipeWeightInfo)
        {
            PmtRecipe pmtRecipe = recipeInfo.PmtRecipe;
            bool isMixing = true;
            if (pmtRecipe.IsUseAreaTemp.Trim() == "1")
            {
                if ((pmtRecipe.SideTemp == null) || ((int)pmtRecipe.SideTemp) <= 0)
                {
                    return "侧壁温度不能为空且必须大于0";
                }
                if ((pmtRecipe.RollTemp == null) || ((int)pmtRecipe.RollTemp) <= 0)
                {
                    return "卸料门温度不能为空且必须大于0";
                }
                if ((pmtRecipe.DdoorTemp == null) || ((int)pmtRecipe.DdoorTemp) <= 0)
                {
                    return "转子温度不能为空且必须大于0";
                }
            }

            if (string.IsNullOrWhiteSpace(pmtRecipe.RecipeMaterialCode))
            {
                return "物料信息不能为空";
            }
            EntityArrayList<BasMaterial> basMaterialList = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == pmtRecipe.RecipeMaterialCode);
            if (basMaterialList.Count == 0)
            {
                return "物料信息不存在";
            }

            BasMaterial m = basMaterialList[0];
            if (m.MajorTypeID.ToString().Trim() == ((int)MajorType.小料).ToString())
            {
                isMixing = false;
            }
            if (recipeWeightInfo.Count == 0)
            {
                return "必须存在称量信息";
            }
            if (isMixing)
            {
                if (recipeMixingInfo.Count == 0)
                {
                    return "必须存在混炼信息";
                }
            }
            if (string.IsNullOrWhiteSpace(pmtRecipe.RecipeEquipCode))
            {
                return "机台信息不能为空";
            }
            if (pmtRecipe.RecipeType == null)
            {
                return "配方类型不能为空";
            }
            if (isMixing)
            {
                if ((pmtRecipe.OverTimeSetTime == null) || ((int)pmtRecipe.OverTimeSetTime) <= 0)
                {
                    return "超时排胶时间不能为空<br />且必须大于0";
                }
                if ((pmtRecipe.OverTempSetTemp == null) || ((int)pmtRecipe.OverTempSetTemp) <= 0)
                {
                    return "紧急排胶温度不能为空<br />且必须大于0";
                }
                if ((pmtRecipe.OverTempMinTime == null) || ((int)pmtRecipe.OverTempMinTime) <= 0)
                {
                    return "超温排胶最短时间不能为空<br />且必须大于0";
                }
                if ((pmtRecipe.OverTempMinTime == null) || ((int)pmtRecipe.OverTempMinTime) <= 0)
                {
                    return "超温排胶最短时间不能为空<br />且必须大于0";
                }
            }
            EntityArrayList<BasEquip> basEquipList = basEquipManager.GetListByWhere(BasEquip._.EquipCode == pmtRecipe.RecipeEquipCode);
            if (basMaterialList.Count == 0)
            {
                return "机台信息不存在";
            }
            BasEquip e = basEquipList[0];
            if (pmtRecipe.RecipeState == null)
            {
                return "配方状态不能为空";
            } if (pmtRecipe.RecipeState.Trim() == ((int)PmtState.正用).ToString())
            {
                EntityArrayList<PmtRecipe> lst = this.GetListByWhere(PmtRecipe._.RecipeEquipCode == pmtRecipe.RecipeEquipCode
                    && PmtRecipe._.RecipeMaterialCode == pmtRecipe.RecipeMaterialCode
                    && PmtRecipe._.RecipeState == ((int)PmtState.正用).ToString()
                    && PmtRecipe._.RecipeType == pmtRecipe.RecipeType);
                if (lst.Count > 0)
                {
                    bool isHas正用 = false;
                    foreach (PmtRecipe m正用 in lst)
                    {
                        if (m正用.RecipeVersionID != pmtRecipe.RecipeVersionID)
                        {
                            isHas正用 = true;
                            break;
                        }
                    }
                    if (isHas正用)
                    {
                        return "," + pmtRecipe.RecipeMaterialCode + "," + pmtRecipe.RecipeEquipCode + "," + pmtRecipe.RecipeType + "," + "当前已有正用配方，不能添加正用配方！" + "," + pmtRecipe.R_Version;
                    }
                }
                //if (string.IsNullOrWhiteSpace(pmtRecipe.CanAuditUser))
                //{
                //    return "配方状态为正用，必须添加审核人";
                //}
            }
            if ((pmtRecipe.InPolyMaxTemp == null) || ((int)pmtRecipe.InPolyMaxTemp) <= 0)
            {
                pmtRecipe.InPolyMaxTemp = 200;
            }
            pmtRecipe.RecipeMaterialName = m.MaterialName;
            if ((m.MajorTypeID != null) && (m.MajorTypeID == (int)MajorType.小料))
            {
                if (!(Convert.ToInt32(e.EquipType) == (int)EquipType.小料称))
                {

                    return "请重新选择正确选择正确的机台";
                }
            }
            else
            {
                if (!(Convert.ToInt32(e.EquipType) == (int)EquipType.混炼机))
                {
                    return "请重新选择正确选择正确的机台";
                }
            }
            decimal dWeight = 0;





            SysUserCtrlManager ubll = new SysUserCtrlManager();
            int Num1 = 0; int Num2 = 0; int Num3 = 0; int Num4 = 0; int Num5 = 0;
            EntityArrayList<SysUserCtrl> mlist = ubll.GetAllList();
            foreach (SysUserCtrl model in mlist)
            {
                if (model.TypeID == "Num1")
                {

                    Num1 = int.Parse(model.ItemCode);
                }
                if (model.TypeID == "Num2")
                {
                    Num2 = int.Parse(model.ItemCode);
                }
                if (model.TypeID == "Num3")
                {
                    Num3 = int.Parse(model.ItemCode);

                }
                if (model.TypeID == "Num4")
                {
                    Num4 = int.Parse(model.ItemCode);
                }
                if (model.TypeID == "Num5")
                {
                    Num5 = int.Parse(model.ItemCode);
                }
            }


            foreach (RecipeWeightInfo info in recipeWeightInfo)
            {
                if (info.PmtRecipeWeight == null)
                {
                    continue;
                }
                if (info.PmtRecipeWeight.SetWeight != null)
                {
                    //dWeight += (decimal)info.PmtRecipeWeight.SetWeight;
                    //根据配方精确位数求总重
                    if (info.PmtRecipeWeight.WeightType == "0")//炭黑
                    { dWeight += decimal.Round(Convert.ToDecimal(info.PmtRecipeWeight.SetWeight), 3); }

                    else if (info.PmtRecipeWeight.WeightType == "1" || info.PmtRecipeWeight.WeightType == "5")//油
                    { dWeight += decimal.Round(Convert.ToDecimal(info.PmtRecipeWeight.SetWeight), 3); }

                    else if (info.PmtRecipeWeight.WeightType == "2")//胶料
                    { dWeight += decimal.Round(Convert.ToDecimal(info.PmtRecipeWeight.SetWeight), 3); }


                    else if (info.PmtRecipeWeight.WeightType == "3")//小料
                    { dWeight += decimal.Round(Convert.ToDecimal(info.PmtRecipeWeight.SetWeight), 3); }
                    else { dWeight += decimal.Round(Convert.ToDecimal(info.PmtRecipeWeight.SetWeight), 3); }
                }

            }

            pmtRecipe.LotTotalWeight = decimal.Round(Convert.ToDecimal(dWeight), 3); ;
            return string.Empty;
        }
        /// <summary>
        /// 校验称量信息
        /// 孙本强 @ 2013-04-03 12:46:43
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <param name="recipeMixingInfo">The recipe mixing info.</param>
        /// <param name="recipeWeightInfo">The recipe weight info.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string ValidityPmtRecipeMixing(RecipeInfo recipeInfo, List<RecipeMixingInfo> recipeMixingInfo, List<RecipeWeightInfo> recipeWeightInfo)
        {
            if (recipeMixingInfo.Count == 0)
            {
                return string.Empty;
            }
            bool isMixing = true;
            if (recipeInfo.BasMaterial.MajorTypeID.ToString().Trim() == ((int)MajorType.小料).ToString())
            {
                isMixing = false;
            }
            if (!isMixing)
            {
                return string.Empty;
            }
            #region 第一步
            int iFirst = 0;
            for (int i = 0; i < recipeMixingInfo.Count; i++)
            {
                RecipeMixingInfo info = recipeMixingInfo[i];
                if (info.PmtRecipeMixing == null)
                {
                    continue;
                }
                iFirst = i;
                break;
            }
            PmtRecipeMixing thisMixing = recipeMixingInfo[iFirst].PmtRecipeMixing;
            if ((thisMixing.MixingSpeed == null) || (thisMixing.MixingSpeed == 0))
            {
                return "混炼第一步转速不能为空";
            }
            if ((thisMixing.MixingPress == null) || (thisMixing.MixingPress == 0))
            {
                return "混炼第一步压力不能为空";
            }
            //if ((thisMixing.MixingPress > (decimal)0.6) || (thisMixing.MixingPress < (decimal)0.1))
            //{
            //    return "混炼第一步压力需在0.1 - 0.6之间";
            //}
            #endregion
            #region 校验步骤
            for (int i = 0; i < recipeMixingInfo.Count; i++)    // 无PmtTerm表 以下字段无法判断
            {
                RecipeMixingInfo info = recipeMixingInfo[i];
                if (info.PmtRecipeMixing == null)
                {
                    continue;
                }
                if (info.PmtRecipeMixing.ActionCode == "4" && recipeMixingInfo[i - 1].PmtRecipeMixing.ActionCode != "6")
                {
                    return "第[" + (i + 1).ToString() + "]步骤混炼动作为开卸料门则第[" + (i).ToString() + "]步必须为提上顶栓";
                }
                if (info.PmtRecipeMixing.ActionCode == "13" && recipeMixingInfo[i - 1].PmtRecipeMixing.ActionCode == "2")
                {
                    return "第[" + (i + 1).ToString() + "]步骤混炼动作为加小料则第[" + (i).ToString() + "]步不能为加炭黑";
                }
                if ((info.PmtTerm != null) && (info.PmtTerm.ShowName.Contains("温度"))
                    && ((info.PmtRecipeMixing.MixingTemp == null) || (info.PmtRecipeMixing.MixingTemp <= 0)))
                {
                    return "第[" + (i + 1).ToString() + "]步骤必须设置温度";
                }
                if ((info.PmtTerm != null) && (info.PmtTerm.ShowName.Contains("温度"))
                     && ((info.PmtRecipeMixing.MixingTemp == null) || (info.PmtRecipeMixing.MixingTemp > 200)))
                {
                    return "第[" + (i + 1).ToString() + "]步骤设置温度的温度不能大于200";
                }
                if ((info.PmtRecipeMixing.MixingTemp != null) && (info.PmtRecipeMixing.MixingTemp > 0)
                    && ((info.PmtTerm == null) || (!info.PmtTerm.ShowName.Contains("温度"))))
                {
                    return "第[" + (i + 1).ToString() + "]步设置了温度,必须增加温度相关条件";
                }
                if ((info.PmtAction != null) && (info.PmtAction.ActionCode == ((int)MixingAction.保持).ToString())
                    && ((info.PmtRecipeMixing.MixingTime == null) || (info.PmtRecipeMixing.MixingTime <= 0)))
                {
                    return "第[" + (i + 1).ToString() + "]步为保持，必须设置时间";
                }
                if ((info.PmtTerm != null) && (info.PmtTerm.ShowName.Contains("功率"))
                    && ((info.PmtRecipeMixing.MixingPower == null) || (info.PmtRecipeMixing.MixingPower <= 0)))
                {
                    return "第[" + (i + 1).ToString() + "]步必须设置功率";
                }
                if ((info.PmtRecipeMixing.MixingPower != null) && (info.PmtRecipeMixing.MixingPower > 0)
                    && ((info.PmtTerm == null) || (!info.PmtTerm.ShowName.Contains("功率"))))
                {
                    return "第[" + (i + 1).ToString() + "]步设置了功率,必须增加功率相关条件";
                }
                if ((info.PmtTerm != null) && (info.PmtTerm.ShowName.Contains("能量"))
                    && ((info.PmtRecipeMixing.MixingEnergy == null) || (info.PmtRecipeMixing.MixingEnergy <= 0)))
                {
                    return "第[" + (i + 1).ToString() + "]步必须设置能量";
                }
                if ((info.PmtRecipeMixing.MixingEnergy != null) && (info.PmtRecipeMixing.MixingEnergy > 0)
                    && ((info.PmtTerm == null) || (!info.PmtTerm.ShowName.Contains("能量"))))
                {
                    return "第[" + (i + 1).ToString() + "]步设置了能量,必须增加能量相关条件";
                }
            }
            #endregion
            #region 加油或炭黑次数
            bool isFromPDM = false;
            string InsertType = recipeInfo.PmtRecipe.OperCode;
            if (string.IsNullOrWhiteSpace(InsertType))
            {
                InsertType = "";
            }
            else
            {
                InsertType = InsertType.Substring(InsertType.Length - 1);
            }
            isFromPDM = InsertType == "+";
            if (isFromPDM)
            {
                recipeInfo.PmtRecipe.OperCode = string.Empty;
            }
            #region 导入配方类型

            int icount = 0;


            foreach (RecipeMixingInfo info in recipeMixingInfo)
            {
                PmtRecipeMixing mixing = info.PmtRecipeMixing;
                if (mixing == null)
                {
                    continue;
                }
                if ((!string.IsNullOrWhiteSpace(mixing.ActionCode)) && (mixing.ActionCode.Trim() == ((int)MixingAction.加小料).ToString())) //加炭黑 2019-01-16 因新昆称量类型仅仅划分为炭黑 油 胶料 ，导致诸如小料，母胶等无法详细划分，故将此段代码暂时注释
                {
                    icount++;
                }
            }
            foreach (RecipeWeightInfo weightInfo in recipeWeightInfo)
            {
                PmtRecipeWeight weight = weightInfo.PmtRecipeWeight;
                if (weight == null)
                {
                    continue;
                }
                if (!string.IsNullOrWhiteSpace(weight.WeightType)
                    && weight.WeightType.Trim() == ((int)WeightType.小料校核称量信息).ToString())
                {
                    if (icount != 0)
                    {
                        icount = 0;
                    }
                }
            }
            //if (icount != 0)
            //{
            //    //return "小料加料和小料称称量不一致！";
            //}

            foreach (RecipeMixingInfo info in recipeMixingInfo)
            {
                PmtRecipeMixing mixing = info.PmtRecipeMixing;
                if (mixing == null)
                {
                    continue;
                }
                if ((!string.IsNullOrWhiteSpace(mixing.ActionCode)) && (mixing.ActionCode.Trim() == ((int)MixingAction.加炭黑).ToString())) //加炭黑
                {
                    icount++;
                }
            }
            foreach (RecipeWeightInfo weightInfo in recipeWeightInfo)
            {
                PmtRecipeWeight weight = weightInfo.PmtRecipeWeight;
                if (weight == null)
                {
                    continue;
                }
                if (!string.IsNullOrWhiteSpace(weight.WeightType)
                    && weight.WeightType.Trim() == ((int)WeightType.炭黑称量信息).ToString()
                    && (isFromPDM ||
                       (!string.IsNullOrWhiteSpace(weight.ActCode) && weight.ActCode.Trim() == ((int)WeightAction.卸料).ToString())
                    ))
                {
                    if (icount != 0)
                    {
                        icount = 0;
                    }
                }
            }
            if (icount != 0)
            {
                return "炭黑加料和卸料不一致";
            }
            foreach (RecipeMixingInfo info in recipeMixingInfo)
            {
                PmtRecipeMixing mixing = info.PmtRecipeMixing;
                if (mixing == null)
                {
                    continue;
                }
                if ((!string.IsNullOrWhiteSpace(mixing.ActionCode)) && (mixing.ActionCode.Trim() == ((int)MixingAction.加油料).ToString())) //加油
                {
                    icount++;
                }
            }
            foreach (RecipeWeightInfo weightInfo in recipeWeightInfo)
            {
                PmtRecipeWeight weight = weightInfo.PmtRecipeWeight;
                if (weight == null)
                {
                    continue;
                }
                if (!string.IsNullOrWhiteSpace(weight.WeightType)
                    && weight.WeightType.Trim() == ((int)WeightType.油1称量信息).ToString()
                    &&
                       (!string.IsNullOrWhiteSpace(weight.ActCode) && weight.ActCode.Trim() == ((int)WeightAction.卸料).ToString())
                    )
                {
                    icount--;
                }
            }
            if (icount != 0)
            {
                return "油料1加料和卸料不一致";
            }
            foreach (RecipeMixingInfo info in recipeMixingInfo)
            {
                PmtRecipeMixing mixing = info.PmtRecipeMixing;
                if (mixing == null)
                {
                    continue;
                }
                //if ((!string.IsNullOrWhiteSpace(mixing.ActionCode)) && (mixing.ActionCode.Trim() == ((int)MixingAction.加油料2).ToString())) //加油
                //{
                //    icount++;
                //}
            }
            foreach (RecipeWeightInfo weightInfo in recipeWeightInfo)
            {
                PmtRecipeWeight weight = weightInfo.PmtRecipeWeight;
                if (weight == null)
                {
                    continue;
                }
                if (!string.IsNullOrWhiteSpace(weight.WeightType)
                    && weight.WeightType.Trim() == ((int)WeightType.油2称量信息).ToString()
                     &&
                        (!string.IsNullOrWhiteSpace(weight.ActCode) && weight.ActCode.Trim() == ((int)WeightAction.卸料).ToString())
                    )
                {
                    icount--;
                }
            }
            if (icount != 0)
            {
                return "油料2加料和卸料不一致";
            }
            #endregion

            #endregion
            #region 胶料操作
            bool isHas = false;
            foreach (RecipeMixingInfo info in recipeMixingInfo)
            {
                if (info.PmtRecipeMixing == null)
                {
                    continue;
                }
                if (info.PmtRecipeMixing.ActionCode.Trim() == ((int)MixingAction.加胶料).ToString())
                {
                    isHas = true;
                    break;
                }
            }
            if (!isHas)
            {
                return "必须存在加胶料的混炼动作";
            }
            isHas = false;
            foreach (RecipeMixingInfo info in recipeMixingInfo)
            {
                if (info.PmtRecipeMixing == null)
                {
                    continue;
                }
                if (info.PmtRecipeMixing.ActionCode.Trim() == ((int)MixingAction.开卸料门).ToString())
                {
                    isHas = true;
                    break;
                }
            }
            if (!isHas)
            {
                return "必须存在开卸料门的混炼动作";
            }
            #endregion
            return string.Empty;
        }
        /// <summary>
        /// 校验开炼信息
        /// 孙本强 @ 2013-04-03 12:46:43
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <param name="recipeMixingInfo">The recipe mixing info.</param>
        /// <param name="recipeWeightInfo">The recipe weight info.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string ValidityPmtRecipeOpenMixing(RecipeInfo recipeInfo, List<RecipeOpenMixingInfo> recipeOpenMixingInfo, List<RecipeWeightInfo> recipeWeightInfo)
        {
            return string.Empty;//yanzx 2015.11.18 不验证开炼信息
            if (recipeOpenMixingInfo.Count == 0)
            {
                return string.Empty;
            }
            bool isMixing = true;
            if (recipeInfo.BasMaterial.MajorTypeID.ToString().Trim() == ((int)MajorType.小料).ToString())
            {
                isMixing = false;
            }
            if (!isMixing)
            {
                return string.Empty;
            }
            #region 第一步
            int iFirst = 0;
            for (int i = 0; i < recipeOpenMixingInfo.Count; i++)
            {
                RecipeOpenMixingInfo info = recipeOpenMixingInfo[i];
                if (info.PmtRecipeOpenMixing == null)
                {
                    continue;
                }
                iFirst = i;
                break;
            }
            PmtRecipeOpenMixing thisOpenMixing = recipeOpenMixingInfo[iFirst].PmtRecipeOpenMixing;
            if ((thisOpenMixing.MixTime == null) || (thisOpenMixing.MixTime == 0))
            {
                return "开炼第一步开炼时间不能为空";
            }
            if ((thisOpenMixing.CoolMixSpeed == null) || (thisOpenMixing.CoolMixSpeed == 0))
            {
                return "开炼第一步冷却鼓速度不能为空";
            }
            if ((thisOpenMixing.OpenMixSpeed == null) || (thisOpenMixing.OpenMixSpeed == 0))
            {
                return "开炼第一步开炼机速度不能为空";
            }
            if ((thisOpenMixing.MixRollor == null) || (thisOpenMixing.MixRollor == 0))
            {
                return "开炼第一步辊距不能为空";
            }
            if ((thisOpenMixing.WaterTemp == null) || (thisOpenMixing.WaterTemp == 0))
            {
                return "开炼第一步水温不能为空";
            }
            if ((thisOpenMixing.RubberTemp == null) || (thisOpenMixing.RubberTemp == 0))
            {
                return "开炼第一步胶温不能为空";
            }
            if ((thisOpenMixing.CarSpeed == null) || (thisOpenMixing.CarSpeed == 0))
            {
                return "开炼第一步小车速度不能为空";
            }
            #endregion

            #region 校验步骤
            for (int i = 0; i < recipeOpenMixingInfo.Count; i++)
            {
                RecipeOpenMixingInfo info = recipeOpenMixingInfo[i];
                if (info.PmtRecipeOpenMixing == null)
                {
                    continue;
                }
                if (info.PmtRecipeOpenMixing.OpenActionCode != null)
                {
                    if ((info.PmtRecipeOpenMixing.MixTime == null) || (info.PmtRecipeOpenMixing.MixTime == 0))
                    {
                        return "开炼第" + i + "步开炼时间不能为空";
                    }
                    if ((info.PmtRecipeOpenMixing.CoolMixSpeed == null) || (info.PmtRecipeOpenMixing.CoolMixSpeed == 0))
                    {
                        return "开炼第" + i + "步冷却鼓速度不能为空";
                    }
                    if ((info.PmtRecipeOpenMixing.OpenMixSpeed == null) || (info.PmtRecipeOpenMixing.OpenMixSpeed == 0))
                    {
                        return "开炼第" + i + "步开炼机速度不能为空";
                    }
                    if ((info.PmtRecipeOpenMixing.MixRollor == null) || (info.PmtRecipeOpenMixing.MixRollor == 0))
                    {
                        return "开炼第" + i + "步辊距不能为空";
                    }
                    if ((info.PmtRecipeOpenMixing.WaterTemp == null) || (info.PmtRecipeOpenMixing.WaterTemp == 0))
                    {
                        return "开炼第" + i + "步水温不能为空";
                    }
                    if ((info.PmtRecipeOpenMixing.RubberTemp == null) || (info.PmtRecipeOpenMixing.RubberTemp == 0))
                    {
                        return "开炼第" + i + "步胶温不能为空";
                    }
                    if ((info.PmtRecipeOpenMixing.CarSpeed == null) || (info.PmtRecipeOpenMixing.CarSpeed == 0))
                    {
                        return "开炼第" + i + "步小车速度不能为空";
                    }
                }
            }
            #endregion
            return string.Empty;
        }
        /// <summary>
        /// 校验配方信息
        /// 孙本强 @ 2013-04-03 12:46:43
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <param name="recipeMixingInfo">The recipe mixing info.</param>
        /// <param name="recipeWeightInfo">The recipe weight info.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string ValidityPmtRecipeWeight(RecipeInfo recipeInfo, List<RecipeMixingInfo> recipeMixingInfo, List<RecipeWeightInfo> recipeWeightInfo)
        {
            if (recipeWeightInfo.Count == 0)
            {
                return string.Empty;
            }

            bool isMixing = true;
            if (recipeInfo.BasMaterial.MajorTypeID.ToString().Trim() == ((int)MajorType.小料).ToString())
            {
                isMixing = false;
            }
            #region 第一步  最后一步
            List<string> typeList = new List<string>();
            foreach (RecipeWeightInfo info in recipeWeightInfo)
            {
                if (info.PmtRecipeWeight == null)
                {
                    continue;
                }
                bool isExist = false;
                foreach (string type in typeList)
                {
                    if (type == info.PmtRecipeWeight.WeightType)
                    {
                        isExist = true;
                        break;
                    }
                }
                if (!isExist)
                {
                    typeList.Add(info.PmtRecipeWeight.WeightType);
                }
            }
            foreach (string type in typeList)
            {
                int iweightid = 0;
                RecipeWeightInfo infoFirst = null;
                RecipeWeightInfo infoLast = null;
                foreach (RecipeWeightInfo info in recipeWeightInfo)
                {
                    if (info.PmtRecipeWeight == null)
                    {
                        continue;
                    }
                    if (type == info.PmtRecipeWeight.WeightType)
                    {
                        iweightid++;
                        if (iweightid == 1)
                        {
                            infoFirst = info;
                        }
                        if (iweightid >= 1)
                        {
                            infoLast = info;
                        }
                        info.PmtRecipeWeight.WeightID = iweightid;
                    }
                }
                PmtRecipeWeight thisWeight = infoFirst.PmtRecipeWeight;
                if (WeightTypeNeed卸料(thisWeight.WeightType.Trim()) && WeightActionIs卸料(thisWeight.ActCode))
                {
                    return ((WeightType)(Convert.ToInt32(thisWeight.WeightType.Trim()))).ToString() + "称量前不允许[卸料]";
                }
                thisWeight = infoLast.PmtRecipeWeight;
                if (WeightTypeNeed卸料(thisWeight.WeightType.Trim()) && !WeightActionIs卸料(thisWeight.ActCode))
                {
                    return ((WeightType)(Convert.ToInt32(thisWeight.WeightType.Trim()))).ToString() + "称量动作最后一步动作必须为[卸料]";
                }
                if (WeightTypeNeed卸料(thisWeight.WeightType.Trim()))
                {
                    infoLast.BasMaterial = null;
                    thisWeight.MaterialCode = "";
                    thisWeight.MaterialName = "";
                    thisWeight.ErrorAllow = 0;
                    thisWeight.SetWeight = 0;
                }
            }
            #endregion
            #region 数据校验
            bool isHasRub = false;
            foreach (RecipeWeightInfo info in recipeWeightInfo)
            {
                PmtRecipeWeight weight = info.PmtRecipeWeight;
                if (weight == null)
                {
                    continue;
                }
                if (string.IsNullOrWhiteSpace(weight.MaterialName))
                {
                    continue;
                }
                #region 称量
                if ((weight.SetWeight == null) ||
                    (weight.ErrorAllow == null) ||
                    (weight.SetWeight == 0) ||
                    (weight.ErrorAllow == 0))
                {
                    return ((WeightType)Convert.ToInt32(weight.WeightType.Trim())).ToString() + "的[" + weight.MaterialName + "]称量信息中设定<br/>重量和允许误差必须大于0";
                }
                #endregion
                if (!isMixing)
                {
                    isHasRub = true;
                    continue;
                }
                if (weight.WeightType.Trim() == ((int)WeightType.胶料称量信息).ToString())
                {
                    isHasRub = true;
                }
                #region 料仓校验
                BasMaterial material = info.BasMaterial;
                if ((material.MajorTypeID != null)
                    && (material.MajorTypeID == 1)   //原材料
                    && ((weight.WeightType == ((int)WeightType.炭黑称量信息).ToString().Trim())
                       || (weight.WeightType == ((int)WeightType.油1称量信息).ToString().Trim())
                       || (weight.WeightType == ((int)WeightType.油2称量信息).ToString().Trim())))
                {
                    //料仓验证
                    //EntityArrayList<PmtEquipJarStore> pmtEquipJarStoreList =
                    //    pmtEquipJarStoreManager.GetListByWhere(PmtEquipJarStore._.MaterialCode == weight.MaterialCode
                    //    && PmtEquipJarStore._.EquipCode == recipeInfo.PmtRecipe.RecipeEquipCode);
                    //if (pmtEquipJarStoreList.Count <= 0)
                    //{
                    //    return recipeInfo.BasEquip.EquipName + "不存在[" + weight.MaterialName + "]的料仓罐！";
                    //}
                }
                #endregion
                #region 加小料
                if (weight.WeightType == ((int)WeightType.小料校核称量信息).ToString().Trim())  //炭黑
                {
                    bool isHaveMixing = false;
                    foreach (RecipeMixingInfo mixing in recipeMixingInfo)
                    {
                        if (mixing.PmtRecipeMixing == null)
                        {
                            continue;
                        }
                        if ((!string.IsNullOrWhiteSpace(mixing.PmtRecipeMixing.ActionCode)) && (mixing.PmtRecipeMixing.ActionCode.Trim() == "13"))
                        {
                            isHaveMixing = true;
                            break;
                        }
                    }
                    if (!isHaveMixing)
                    {
                        return "称量信息中存在小料称称量，<br />必须在混炼信息中添加[加小料]动作！";
                    }
                }
                #endregion
                #region 加炭黑或油
                if (weight.WeightType == ((int)WeightType.炭黑称量信息).ToString().Trim())  //炭黑  2019-1-2 新昆无此字段(WeightType) 故先A掉
                {
                    bool isHaveMixing = false;
                    foreach (RecipeMixingInfo mixing in recipeMixingInfo)
                    {
                        if (mixing.PmtRecipeMixing == null)
                        {
                            continue;
                        }
                        if ((!string.IsNullOrWhiteSpace(mixing.PmtRecipeMixing.ActionCode)) && (mixing.PmtRecipeMixing.ActionCode.Trim() == "2"))
                        {
                            isHaveMixing = true;
                            break;
                        }
                    }
                    if (!isHaveMixing)
                    {
                        return "称量信息中存在炭黑物料，<br />必须在混炼信息中添加[加炭黑]动作！";
                    }
                }

                if (weight.WeightType == ((int)WeightType.油1称量信息).ToString().Trim()) //油1
                {
                    bool isHaveMixing = false;
                    foreach (RecipeMixingInfo mixing in recipeMixingInfo)
                    {
                        if (mixing.PmtRecipeMixing == null)
                        {
                            continue;
                        }
                        if ((!string.IsNullOrWhiteSpace(mixing.PmtRecipeMixing.ActionCode)) && (mixing.PmtRecipeMixing.ActionCode.Trim() == ((int)MixingAction.加油料).ToString()))
                        {
                            isHaveMixing = true;
                            break;
                        }
                    }
                    if (!isHaveMixing)
                    {
                        return "称量信息中存在油称（1）物料信息，<BR />必须在混炼信息中添加[加油料]动作！";
                    }
                }
                //if (weight.WeightType == ((int)WeightType.油2称量信息).ToString().Trim()) //油2
                //{
                //    bool isHaveMixing = false;
                //    foreach (RecipeMixingInfo mixing in recipeMixingInfo)
                //    {
                //        if (mixing.PmtRecipeMixing == null)
                //        {
                //            continue;
                //        }
                //        if ((!string.IsNullOrWhiteSpace(mixing.PmtRecipeMixing.ActionCode)) && (mixing.PmtRecipeMixing.ActionCode.Trim() == ((int)MixingAction.加油料2).ToString()))
                //        {
                //            isHaveMixing = true;
                //            break;
                //        }
                //    }
                //    if (!isHaveMixing)
                //    {
                //        return "称量信息中存在油称（2）物料信息，<br />必须在混炼信息中添加[加油料2]动作！";
                //    }
                //}
                #endregion
            }
            if ((isMixing) && (!isHasRub))
            {
                return "称量信息中必须包含胶料！";
            }
            #endregion
            return string.Empty;
        }
        /// <summary>
        /// 存储密炼信息的校验密炼信息方法
        /// 孙本强 @ 2013-04-03 12:46:42
        /// </summary>
        /// <param name="pmtRecipe"></param>
        /// <param name="pmtRecipeWeight"></param>
        /// <param name="pmtRecipeMixing"></param>
        /// <param name="recipe"></param>
        /// <param name="recipeMixing"></param>
        /// <param name="recipeWeight"></param>
        /// <returns></returns>
        private string ValidityPmtInfo(PmtRecipe pmtRecipe,
                                        EntityArrayList<PmtRecipeWeight> pmtRecipeWeight,
                                        EntityArrayList<PmtRecipeMixing> pmtRecipeMixing,
                                        EntityArrayList<PmtRecipeOpenMixing> pmtRecipeOpenMixing,
                                       ref RecipeInfo recipe,
                                       ref List<RecipeMixingInfo> recipeMixing,
                                       ref List<RecipeWeightInfo> recipeWeight,
                                       ref List<RecipeOpenMixingInfo> recipeOpenMixing)
        {
            #region 初始化数据
            if (pmtRecipe.ObjID > 0)
            {
                if (pmtRecipeWeight.Count == 0)
                {
                    pmtRecipeWeight = this.weightManager.GetListByWhere(PmtRecipeWeight._.RecipeObjID == pmtRecipe.ObjID);
                }
                if (pmtRecipeMixing.Count == 0)
                {
                    OrderByClip order = new OrderByClip();
                    order = PmtRecipeMixing._.MixingStep.Asc;
                    pmtRecipeMixing = this.mixingManager.GetListByWhereAndOrder(PmtRecipeMixing._.RecipeObjID == pmtRecipe.ObjID, order);
                }

                if (pmtRecipeOpenMixing.Count == 0)
                {
                    OrderByClip order = new OrderByClip();
                    order = PmtRecipeOpenMixing._.MixingStep.Asc;
                    pmtRecipeOpenMixing = this.openManager.GetListByWhereAndOrder(PmtRecipeOpenMixing._.RecipeObjID == pmtRecipe.ObjID, order);
                }
            }
            EntityArrayList<BasMaterial> basMaterialList = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == pmtRecipe.RecipeMaterialCode);
            string materialName = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == pmtRecipe.RecipeMaterialCode)[0].MaterialName;
            string Result = IniPmtRecip(pmtRecipe, ref recipe);
            if (!string.IsNullOrWhiteSpace(Result))
            {
                return materialName + Result;
            }
            List<RecipeWeightInfo> recipeWeightInfo = new List<RecipeWeightInfo>();
            Result = IniPmtRecipeWeight(recipe, pmtRecipeWeight, ref recipeWeightInfo);
            if (!string.IsNullOrWhiteSpace(Result))
            {
                return materialName + Result;
            }
            List<RecipeMixingInfo> recipeMixingInfo = new List<RecipeMixingInfo>();
            Result = IniPmtRecipeMixing(recipe, pmtRecipeMixing, ref recipeMixingInfo);
            if (!string.IsNullOrWhiteSpace(Result))
            {
                return materialName + Result;
            }

            List<RecipeOpenMixingInfo> recipeOpenMixingInfo = new List<RecipeOpenMixingInfo>();
            Result = IniPmtRecipeOpenMixing(recipe, pmtRecipeOpenMixing, ref recipeOpenMixingInfo);
            if (!string.IsNullOrWhiteSpace(Result))
            {
                return materialName + Result;
            }
            #endregion

            #region 校验数据
            Result = ValidityPmtRecipe(recipe, recipeMixingInfo, recipeWeightInfo);
            if (!string.IsNullOrWhiteSpace(Result))
            {
                return materialName + Result;
            }
            Result = ValidityPmtRecipeWeight(recipe, recipeMixingInfo, recipeWeightInfo);
            if (!string.IsNullOrWhiteSpace(Result))
            {
                return materialName + Result;
            }
            Result = ValidityPmtRecipeMixing(recipe, recipeMixingInfo, recipeWeightInfo);
            if (!string.IsNullOrWhiteSpace(Result))
            {
                return materialName + Result;
            }

            Result = ValidityPmtRecipeOpenMixing(recipe, recipeOpenMixingInfo, recipeWeightInfo);
            if (!string.IsNullOrWhiteSpace(Result))
            {
                return materialName + Result;
            }
            //if (recipe.PmtRecipe.RecipeMaterialCode.Substring(0, 1) == "4" || recipe.PmtRecipe.RecipeMaterialCode.Substring(0, 1) == "3" || recipe.PmtRecipe.RecipeMaterialCode.Substring(0, 1) == "5")
            //{
            //    if (recipe.PmtRecipe.LotTotalWeight < 140)
            //    {
            //        return materialName + "配方总重必须大于140kg！！";
            //    }
            //}
            #endregion
            #region 数据整理
            int step = 0;
            foreach (RecipeMixingInfo info in recipeMixingInfo)
            {
                if (info.PmtRecipeMixing != null)
                {
                    step++;
                    info.PmtRecipeMixing.MixingStep = step;
                    recipeMixing.Add(info);
                }
            }
            step = 0;
            foreach (RecipeOpenMixingInfo info in recipeOpenMixingInfo)
            {
                if (info.PmtRecipeOpenMixing != null)
                {
                    step++;
                    //info.PmtRecipeOpenMixing.MixingStep = step; //yanzx 错误逻辑
                    recipeOpenMixing.Add(info);
                }
            }

            foreach (RecipeWeightInfo info in recipeWeightInfo)
            {
                if (info.PmtRecipeWeight != null)
                {
                    recipeWeight.Add(info);
                }
            }
            #endregion
            return Result;
        }
        /// <summary>
        /// 校验密炼信息
        /// 孙本强 @ 2013-04-03 12:46:42
        /// </summary>
        /// <param name="pmtRecipe"></param>
        /// <returns></returns>
        private string ValidityPmtInfo(PmtRecipe pmtRecipe)
        {
            string Result = "";
            try
            {
                EntityArrayList<PmtRecipeWeight> pmtRecipeWeight = this.weightManager.GetListByWhere(PmtRecipeWeight._.RecipeObjID == pmtRecipe.ObjID);
                EntityArrayList<PmtRecipeMixing> pmtRecipeMixing = this.mixingManager.GetListByWhereAndOrder(PmtRecipeMixing._.RecipeObjID == pmtRecipe.ObjID, PmtRecipeMixing._.MixingStep.Asc);
                EntityArrayList<PmtRecipeOpenMixing> pmtRecipeOpenMixing = this.openManager.GetListByWhereAndOrder(PmtRecipeOpenMixing._.RecipeObjID == pmtRecipe.ObjID, PmtRecipeOpenMixing._.MixingStep.Asc);

                RecipeInfo recipeInfo = new RecipeInfo();
                List<RecipeMixingInfo> recipeMixingInfo = new List<RecipeMixingInfo>();
                List<RecipeWeightInfo> recipeWeightInfo = new List<RecipeWeightInfo>();
                List<RecipeOpenMixingInfo> recipeOpenMixingInfo = new List<RecipeOpenMixingInfo>();
                Result = ValidityPmtInfo(pmtRecipe, pmtRecipeWeight, pmtRecipeMixing, pmtRecipeOpenMixing,
                   ref recipeInfo, ref recipeMixingInfo, ref recipeWeightInfo, ref recipeOpenMixingInfo);
                if (!string.IsNullOrWhiteSpace(Result))
                {
                    return Result;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return "配方校验失败！" + ex.Message.ToString();
            }
        }
        #region 校验数据相同
        /// <summary>
        /// 对比数据
        /// 孙本强 @ 2013-04-03 12:46:44
        /// </summary>
        /// <param name="a">A.</param>
        /// <param name="b">The b.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool ObjectEquals(object a, object b)
        {
            if (a.GetType() != b.GetType())
            {
                return false;
            }
            PropertyInfo[] fields = a.GetType().GetProperties();
            foreach (PropertyInfo f in fields)
            {
                if (f.Name.ToString() == "RecipeModifyUser")
                {
                    continue;
                }
                if (f.Name.ToString() == "ObjID")
                {
                    continue;
                }
                if (f.Name.ToString() == "FactoryCode")
                {
                    //continue;
                    return false;
                }
                if (f.GetValue(a, null) == null && f.GetValue(b, null) == null)
                {
                    continue;
                }
                if (f.GetValue(a, null) == null)
                {
                    if (string.IsNullOrWhiteSpace(f.GetValue(b, null).ToString()))
                    {
                        continue;
                    }
                    try
                    {
                        if (Convert.ToDecimal(f.GetValue(a, null).ToString().Trim()) == 0)
                        {
                            continue;
                        }
                    }
                    catch
                    {
                    }
                }
                if (f.GetValue(b, null) == null)
                {
                    if (string.IsNullOrWhiteSpace(f.GetValue(a, null).ToString()))
                    {
                        continue;
                    }
                    try
                    {
                        if (Convert.ToDecimal(f.GetValue(a, null).ToString().Trim()) == 0)
                        {
                            continue;
                        }
                    }
                    catch
                    {
                    }
                }
                try
                {
                    if (Convert.ToDecimal(f.GetValue(a, null).ToString().Trim()) == Convert.ToDecimal(f.GetValue(b, null).ToString().Trim()))
                    {
                        continue;
                    }
                }
                catch
                {
                }
                if (f.GetValue(a, null).ToString().Trim() != f.GetValue(b, null).ToString().Trim())
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 对比配方
        /// 孙本强 @ 2013-04-03 12:46:44
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <param name="recipeMixingInfo">The recipe mixing info.</param>
        /// <param name="recipeWeightInfo">The recipe weight info.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private int PmtRecipeChanged(RecipeInfo recipeInfo, List<RecipeMixingInfo> recipeMixingInfo,
            List<RecipeWeightInfo> recipeWeightInfo, List<RecipeOpenMixingInfo> recipeOpenMixingInfo)
        {
            int Result = 0;
            PmtRecipe oldPmtRecipe = this.GetById(recipeInfo.PmtRecipe.ObjID);
            if (!ObjectEquals(oldPmtRecipe, recipeInfo.PmtRecipe))
            {
                return 1;
            }
            EntityArrayList<PmtRecipeMixing> oldMixingInfoList = this.mixingManager.GetListByWhere(PmtRecipeMixing._.RecipeObjID == recipeInfo.PmtRecipe.ObjID);
            if (oldMixingInfoList.Count != recipeMixingInfo.Count)
            {
                return 2;
            }
            for (int i = 0; i < oldMixingInfoList.Count; i++)
            {
                if (!ObjectEquals(oldMixingInfoList[i], recipeMixingInfo[i].PmtRecipeMixing))
                {
                    return 2;
                }
            }

            EntityArrayList<PmtRecipeWeight> oldWeightInfoList = this.weightManager.GetListByWhere(PmtRecipeWeight._.RecipeObjID == recipeInfo.PmtRecipe.ObjID);
            if (oldWeightInfoList.Count != recipeWeightInfo.Count)
            {
                return 3;
            }
            for (int i = 0; i < oldWeightInfoList.Count; i++)
            {
                if (!ObjectEquals(oldWeightInfoList[i], recipeWeightInfo[i].PmtRecipeWeight))
                {
                    return 3;
                }
            }

            EntityArrayList<PmtRecipeOpenMixing> oldOpenMixingInfoList = this.openManager.GetListByWhere(PmtRecipeOpenMixing._.RecipeObjID == recipeInfo.PmtRecipe.ObjID);
            if (oldOpenMixingInfoList.Count != recipeOpenMixingInfo.Count)
            {
                return 4;
            }
            //for (int i = 0; i < oldMixingInfoList.Count; i++)
            for (int i = 0; i < oldOpenMixingInfoList.Count; i++)
            {
                if (!ObjectEquals(oldOpenMixingInfoList[i], recipeOpenMixingInfo[i].PmtRecipeOpenMixing))
                {
                    return 4;
                }
            }
            return Result;
        }
        #endregion
        #endregion
        #region 保存数据
        /// <summary>
        /// 保存配方信息成功
        /// 孙本强 @ 2013-04-03 12:46:44
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <remarks></remarks>
        private void SavePmtRecipeSuccess(RecipeInfo recipeInfo)
        {
            this.service.SavePmtRecipeLog(recipeInfo.PmtRecipe.ObjID.ToString());
            //this.service.RefreshPmtRecipe(recipeInfo.PmtRecipe.ObjID.ToString());
        }
        /// <summary>
        /// 保存称量信息
        /// 孙本强 @ 2013-04-03 12:46:45
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <param name="recipeWeightInfo">The recipe weight info.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string InsertPmtRecipeWeight(RecipeInfo recipeInfo, List<RecipeWeightInfo> recipeWeightInfo)
        {
            string Result = string.Empty;
            if (recipeWeightInfo.Count == 0)
            {
                return Result;
            }

            string sql = "delete Pmt_Weight where Recipe_Code='" + recipeInfo.PmtRecipe.RecipeMaterialCode + "' and Equip_Code='" + recipeInfo.PmtRecipe.RecipeEquipCode + "' and Edt_Code='" + recipeInfo.PmtRecipe.RecipeVersionID + "'";
            weightManager.GetBySql(sql).ToDataSet();

            List<PmtRecipeWeight> lst = new List<PmtRecipeWeight>();
            for (int i = 0; i < recipeWeightInfo.Count; i++)
            {
                PmtRecipeWeight m = recipeWeightInfo[i].PmtRecipeWeight;
                m.Detach();
                m.RecipeObjID = recipeInfo.PmtRecipe.ObjID;
                m.RecipeEquipCode = recipeInfo.PmtRecipe.RecipeEquipCode;
                m.RecipeMaterialCode = recipeInfo.PmtRecipe.RecipeMaterialCode;
                m.RecipeVersionID = Convert.ToInt32(recipeInfo.PmtRecipe.RecipeVersionID);

                //Pmt_Weight pw = GetPmt_Weight(m);
                //IPmt_WeightManager pmm = new Pmt_WeightManager();
                //pmm.Insert(pw);




                sql = "insert into Pmt_Weight([Recipe_Code],[Weight_ID],[Equip_Code],[Edt_Code],[Weight_Type],[Scale_Code],[Act_Code] ,"
                 + "[Mater_Code],[Mater_Name] ,[Set_Weight] ,[Error_Allow] ,[Father_code] ,[Unit_name],[child_code]   ,[RecipeObjID]) values('"
                 + recipeInfo.PmtRecipe.RecipeMaterialCode + "','" + m.WeightID + "','" + recipeInfo.PmtRecipe.RecipeEquipCode + "','" + recipeInfo.PmtRecipe.RecipeVersionID + "','" + m.WeightType + "','2','" + m.ActCode + "','"
                 + m.MaterialCode + "','" + m.MaterialName + "','" + m.SetWeight + "','" + m.ErrorAllow + "','" + recipeInfo.PmtRecipe.RecipeMaterialCode + "','','" + m.MaterialCode + "','" + recipeInfo.PmtRecipe.ObjID + "')";
                weightManager.GetBySql(sql).ToDataSet();

                //lst.Add(m);
            }
            //weightManager.BatchInsert(lst);
            return Result;
        }
        public Pmt_Weight GetPmt_Weight(PmtRecipeWeight m)
        {
            Pmt_Weight pm = new Pmt_Weight();
            pm.RecipeObjID = m.RecipeObjID;
            pm.Equip_Code = m.RecipeEquipCode;
            pm.Recipe_Code = m.RecipeMaterialCode;
            pm.Edt_Code = m.RecipeVersionID;
            pm.Weight_ID = m.WeightID;
            pm.Weight_Type = m.WeightType;
            pm.Act_Code = m.ActCode;
            pm.Mater_Code = m.MaterialCode;
            pm.Mater_Name = m.MaterialName;
            pm.Set_Weight = m.SetWeight;
            pm.Error_Allow = m.ErrorAllow;


            return pm;

        }
        /// <summary>
        /// 保存密炼混炼信息
        /// 孙本强 @ 2013-04-03 12:46:45
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <param name="recipeMixingInfo">The recipe mixing info.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string InsertPmtRecipeMixing(RecipeInfo recipeInfo, List<RecipeMixingInfo> recipeMixingInfo)
        {
            string Result = string.Empty;
            if (recipeMixingInfo.Count == 0)
            {
                return Result;
            }
            string sql = "delete Pmt_Mixing where Recipe_Code='" + recipeInfo.PmtRecipe.RecipeMaterialCode + "' and Equip_Code='" + recipeInfo.PmtRecipe.RecipeEquipCode + "' and Edt_Code='" + recipeInfo.PmtRecipe.RecipeVersionID + "'";
            mixingManager.GetBySql(sql).ToDataSet();
            List<PmtRecipeMixing> lst = new List<PmtRecipeMixing>();
            for (int i = 0; i < recipeMixingInfo.Count; i++)
            {
                PmtRecipeMixing m = recipeMixingInfo[i].PmtRecipeMixing;
                m.Detach();
                m.RecipeObjID = recipeInfo.PmtRecipe.ObjID;
                m.RecipeEquipCode = recipeInfo.PmtRecipe.RecipeEquipCode;
                m.RecipeMaterialCode = recipeInfo.PmtRecipe.RecipeMaterialCode;
                m.RecipeVersionID = Convert.ToInt32(recipeInfo.PmtRecipe.RecipeVersionID);

                m.MixingTemp = m.MixingTemp.ToString() == "" ? 0 : m.MixingTemp;
                m.MixingEnergy = m.MixingEnergy.ToString() == "" ? 0 : m.MixingEnergy;
                m.MixingPower = m.MixingPower.ToString() == "" ? 0 : m.MixingPower;
                m.MixingPress = m.MixingPress.ToString() == "" ? 0 : m.MixingPress;

                m.CondCode = m.TermCode;

                m.Time_diff = m.Time_diff.ToString() == "" ? 0 : m.Time_diff;
                m.Ener_diff = m.Ener_diff.ToString() == "" ? 0 : m.Ener_diff;
                m.Temp_diff = m.Temp_diff.ToString() == "" ? 0 : m.Temp_diff;
                m.CondCode = m.TermCode;


                try
                {
                    m.TermCode = recipeMixingInfo[i].PmtTerm.TermCode;//此行可能多余
                }
                catch (Exception ex)
                { }
                Pmt_Mixing pm = GetPmt_Mixing(m);
                IPmt_MixingManager pmm = new Pmt_MixingManager();
                pmm.Insert(pm);


     

                //sql = "insert into [Pmt_Mixing] ([Recipe_Code],[Father_code]  ,[Mix_Id],[Equip_Code],[Edt_Code],[Cond_Code],[Term_code]  ,"
                //    + " [Mixing_Time] ,[Mixing_Temp],[Mixing_Energy],[Mixing_Power],[Mixing_Press]  ,[Mixing_Speed] "
                //   + " ,[Act_Code] ,[Set_time]  ,[Set_temp]     ,[Set_ener]   ,[Set_power]    ,[Set_pres]    ,[Set_rota]  ,"
                //+ "   [time_diff] ,[temp_diff]    ,[ener_diff]  ,[RecipeObjID]) values('"
                //+ m.RecipeMaterialCode + "','" + m.RecipeMaterialCode + "','" + m.MixingStep + "','" + m.RecipeEquipCode + "','" + m.RecipeVersionID + "','" + m.TermCode + "','" + m.TermCode + "','"
                //+ m.MixingTime + "','" + m.MixingTemp + "','" + m.MixingEnergy + "','" + m.MixingPower + "','" + m.MixingPress + "','" + m.MixingSpeed + "','"
                //+ m.ActionCode + "','" + m.MixingTime + "','" + m.MixingTemp + "','" + m.MixingEnergy + "','" + m.MixingPower + "','" + m.MixingPress + "','" + m.MixingSpeed + "','"
                //+ m.Time_diff + "','" + m.Temp_diff + "','" + m.Ener_diff + "','" + m.RecipeObjID + "')";
                //mixingManager.GetBySql(sql).ToDataSet();

                //lst.Add(m);
            }

            //mixingManager.BatchInsert(lst);
            return Result;
        }
        public Pmt_Mixing GetPmt_Mixing(PmtRecipeMixing m)
        {
            Pmt_Mixing pm = new Pmt_Mixing();
            pm.RecipeObjID = m.RecipeObjID;
            pm.Equip_Code = m.RecipeEquipCode;
            pm.Recipe_Code = m.RecipeMaterialCode;
            pm.Edt_Code = m.RecipeVersionID;
            pm.Mix_Id = m.MixingStep;
            pm.Mixing_Time = m.MixingTime;
            pm.Mixing_Temp = m.MixingTemp;
            pm.Mixing_Energy = m.MixingEnergy;
            pm.Mixing_Power = m.MixingPower;
            pm.Mixing_Press = m.MixingPress;
            pm.Mixing_Speed = m.MixingSpeed;
            pm.Act_Code = m.ActionCode;
            pm.Time_diff = m.Time_diff;
            pm.Temp_diff = m.Temp_diff;
            pm.Ener_diff = m.Ener_diff;
            pm.Term_code = m.TermCode;
            pm.Cond_Code = m.TermCode;
            pm.Set_ener = m.MixingEnergy;
            pm.Set_time = m.MixingTime;
            pm.Set_power = m.MixingPower;
            pm.Set_pres = m.MixingPress;
            pm.Set_rota = m.MixingSpeed;
            return pm;

        }
        /// <summary>
        /// 保存密炼开炼信息
        /// 袁洋 @ 2013-04-03 12:46:45
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <param name="recipeMixingInfo">The recipe mixing info.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string InsertPmtRecipeOpenMixing(RecipeInfo recipeInfo, List<RecipeOpenMixingInfo> recipeOpenMixingInfo)
        {
            string Result = string.Empty;
            if (recipeOpenMixingInfo.Count == 0)
            {
                return Result;
            }
            openManager.DeleteByWhere(PmtRecipeOpenMixing._.RecipeObjID == recipeInfo.PmtRecipe.ObjID);
            List<PmtRecipeOpenMixing> lst = new List<PmtRecipeOpenMixing>();
            for (int i = 0; i < recipeOpenMixingInfo.Count; i++)
            {
                PmtRecipeOpenMixing m = recipeOpenMixingInfo[i].PmtRecipeOpenMixing;
                m.Detach();
                m.RecipeObjID = Convert.ToInt32(recipeInfo.PmtRecipe.ObjID);
                m.RecipeEquipCode = recipeInfo.PmtRecipe.RecipeEquipCode;
                m.RecipeMaterialCode = recipeInfo.PmtRecipe.RecipeMaterialCode;
                m.RecipeVersionID = Convert.ToInt32(recipeInfo.PmtRecipe.RecipeVersionID);
                lst.Add(m);
            }
            openManager.BatchInsert(lst);
            return Result;
        }
        /// <summary>
        /// 保存配方信息
        /// 孙本强 @ 2013-04-03 12:46:46
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <remarks></remarks>
        private void IniRecipeName(RecipeInfo recipeInfo)
        {
            SysCode sysCode = GetSysCode(SysCodeManager.SysCodeType.PmtType, ((int)recipeInfo.PmtRecipe.RecipeType).ToString());
            if (sysCode == null)
            {
                return;
            }
            // recipeInfo.PmtRecipe.RecipeName = DateTime.Now.ToString("yyMMdd") + sysCode.ItemName + recipeInfo.PmtRecipe.RecipeVersionID.ToString("D3");
            recipeInfo.PmtRecipe.RecipeName = DateTime.Now.ToString("yyMMdd") + sysCode.ItemName + recipeInfo.PmtRecipe.RecipeVersionID.ToString();
        }
        /// <summary>
        /// 保存配方信息
        /// 孙本强 @ 2013-04-03 12:46:46
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <param name="recipeMixingInfo">The recipe mixing info.</param>
        /// <param name="recipeWeightInfo">The recipe weight info.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string InsertPmtRecipe(RecipeInfo recipeInfo, List<RecipeMixingInfo> recipeMixingInfo,
            List<RecipeWeightInfo> recipeWeightInfo, List<RecipeOpenMixingInfo> recipeOpenMixingInfo)
        {
            string Result = string.Empty;
            try
            {
                int objid = ((int)this.GetMaxValueByProperty(PmtRecipe._.ObjID) + 1);
                recipeInfo.PmtRecipe.ObjID = objid;
                recipeInfo.PmtRecipe.RecipeVersionID = GetMaxRecipeVersionID(recipeInfo);
                recipeInfo.PmtRecipe.Detach();
                IniRecipeName(recipeInfo);
                //this.Insert(recipeInfo.PmtRecipe);

                string sql = @" insert into pmt_recipe(ObjId,Oper_code,Modify_time,Equip_Code,Mater_Code,Edt_Code,Mater_Name,Recipe_Type,Recipe_State,Define_Date,
                        done_time,Shelf_Num,Total_Weight,CB_RecycleType,Black_reuse,CB_RecycleTime,OverTemp_MinTime,OverTime_Time,
                        OverTemp_Temp,Max_InPolyTemp,Min_InPolyTemp,Ddoor_Temp,Side_Temp,Roll_Temp,Is_UseAreaTemp,Recipe_Code,Rearch_Code,User_EdtCode) values('" + recipeInfo.PmtRecipe.ObjID + "','" + recipeInfo.PmtRecipe.RecipeModifyUser + "', CONVERT(char(19), GETDATE(), 120),'" + recipeInfo.PmtRecipe.RecipeEquipCode + @"', '" + recipeInfo.PmtRecipe.RecipeMaterialCode + @"', '" + recipeInfo.PmtRecipe.RecipeVersionID + @"', '" + recipeInfo.PmtRecipe.RecipeMaterialName + @"', '" + recipeInfo.PmtRecipe.RecipeType + @"', '" + recipeInfo.PmtRecipe.RecipeState + @"', CONVERT(char(19), GETDATE(), 121)
                        , '" + recipeInfo.PmtRecipe.LotDoneTime + @"', '" + recipeInfo.PmtRecipe.ShelfLotCount + @"', '" + recipeInfo.PmtRecipe.LotTotalWeight + @"', '" + recipeInfo.PmtRecipe.CarbonRecycleType + @"', '" + recipeInfo.PmtRecipe.CarbonRecycleType + @"', '" + recipeInfo.PmtRecipe.CarbonRecycleTime + @"', '" + recipeInfo.PmtRecipe.OverTempMinTime + @"', '" + recipeInfo.PmtRecipe.OverTimeSetTime + @"'
                        , '" + recipeInfo.PmtRecipe.OverTempSetTemp + @"', '" + recipeInfo.PmtRecipe.InPolyMaxTemp + @"', '" + recipeInfo.PmtRecipe.InPolyMinTemp + @"', '" + recipeInfo.PmtRecipe.DdoorTemp + @"', '" + recipeInfo.PmtRecipe.SideTemp + @"', '" + recipeInfo.PmtRecipe.RollTemp + @"', '" + recipeInfo.PmtRecipe.IsUseAreaTemp + "','" + recipeInfo.PmtRecipe.RecipeName + "', '" + recipeInfo.PmtRecipe.RearchCode + @"', '" + recipeInfo.PmtRecipe.UseredtCode + @"') ";
                this.GetBySql(sql).ToDataSet();
                PmtRecipe thisPmtRecipe = this.GetById(objid);
                Result = Result + "a";
                InsertPmtRecipeMixing(recipeInfo, recipeMixingInfo);
                Result = Result + "a";
                InsertPmtRecipeWeight(recipeInfo, recipeWeightInfo);

                sql = "update pmt_recipe set total_weight=(select sum(set_weight) from pmt_weight where recipe_Code=pmt_recipe.mater_Code and equip_Code=pmt_recipe.equip_Code and edt_Code=pmt_recipe.edt_Code) where objId='" + recipeInfo.PmtRecipe.ObjID + "'";
                this.GetBySql(sql).ToDataSet();

                //InsertPmtRecipeOpenMixing(recipeInfo, recipeOpenMixingInfo);

                //Result = Result + "a";
            }
            catch (Exception ex)
            {
                //Result = "信息添加错误";
                Result = Result + ex.Message;
                return Result;
            }
            Result = string.Empty;
            return Result;
        }
        /// <summary>
        /// 更新配方信息
        /// 孙本强 @ 2013-04-03 12:46:46
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <param name="recipeMixingInfo">The recipe mixing info.</param>
        /// <param name="recipeWeightInfo">The recipe weight info.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string UpdatePmtRecipe(RecipeInfo recipeInfo, List<RecipeMixingInfo> recipeMixingInfo,
            List<RecipeWeightInfo> recipeWeightInfo, List<RecipeOpenMixingInfo> recipeOpenMixingInfo)
        {
            string Result = string.Empty;
            try
            {
                //   recipeInfo.PmtRecipe.RecipeVersionID = GetMaxRecipeVersionID(recipeInfo);
                IniRecipeName(recipeInfo);
                string sql = @"update pmt_recipe set audit_flag ='0',audit_name='',audit_date='',Recipe_Type='" + recipeInfo.PmtRecipe.RecipeType + @"',Recipe_State='" + recipeInfo.PmtRecipe.RecipeState + @"',Total_Weight='" + recipeInfo.PmtRecipe.LotTotalWeight + @"',Shelf_Num='" + recipeInfo.PmtRecipe.ShelfLotCount + @"',Done_Time='" + recipeInfo.PmtRecipe.LotDoneTime + @"',OverTime_Time='" + recipeInfo.PmtRecipe.OverTimeSetTime + @"',OverTemp_Temp='" + recipeInfo.PmtRecipe.OverTempSetTemp + @"',Rearch_code='" + recipeInfo.PmtRecipe.RearchCode + @"',User_EdtCode='" + recipeInfo.PmtRecipe.UseredtCode + @"'
                               , Min_InPolyTemp = '" + recipeInfo.PmtRecipe.InPolyMinTemp + @"', CB_RecycleTime = '" + recipeInfo.PmtRecipe.CarbonRecycleTime + @"', Ddoor_Temp = '" + recipeInfo.PmtRecipe.DdoorTemp + @"', Side_Temp = '" + recipeInfo.PmtRecipe.SideTemp + @"', Roll_Temp = '" + recipeInfo.PmtRecipe.RollTemp + @"',Is_UseAreaTemp='" + recipeInfo.PmtRecipe.IsUseAreaTemp + "',Max_InPolyTemp='" + recipeInfo.PmtRecipe.InPolyMaxTemp + @"',OverTemp_MinTime='" + recipeInfo.PmtRecipe.OverTempMinTime + @"',CB_RecycleType='" + recipeInfo.PmtRecipe.CarbonRecycleType +
 @"' ,Modify_time= CONVERT(char(19), GETDATE(), 120),Oper_Code ='" + recipeInfo.PmtRecipe.RecipeModifyUser + @"' where objid = '" + recipeInfo.PmtRecipe.ObjID + "'";
                //@"'  where Equip_Code = '" + recipeInfo.PmtRecipe.RecipeEquipCode + "' and Mater_Code = '" + recipeInfo.PmtRecipe.RecipeMaterialCode + "' and Edt_Code = '" + recipeInfo.PmtRecipe.RecipeVersionID + "'";
                this.GetBySql(sql).ToDataSet();

                InsertPmtRecipeMixing(recipeInfo, recipeMixingInfo);
                InsertPmtRecipeWeight(recipeInfo, recipeWeightInfo);
                //InsertPmtRecipeOpenMixing(recipeInfo, recipeOpenMixingInfo);
            }
            catch (Exception ex)
            {
                Result = "信息添加错误";
                Result = ex.Message;
            }

            return Result;
        }
        #endregion


        /// <summary>
        /// 保存工艺配方
        /// 孙本强 @ 2013-04-03 12:18:09
        /// </summary>
        /// <param name="pmtRecipe">The PMT recipe.</param>
        /// <param name="pmtRecipeWeight">The PMT recipe weight.</param>
        /// <param name="pmtRecipeMixing">The PMT recipe mixing.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string SavePmtRecipe(PmtRecipe pmtRecipe, EntityArrayList<PmtRecipeWeight> pmtRecipeWeight,
            EntityArrayList<PmtRecipeMixing> pmtRecipeMixing, EntityArrayList<PmtRecipeOpenMixing> pmtRecipeOpenMixing)
        {
            RecipeInfo recipeInfo = new RecipeInfo();
            List<RecipeMixingInfo> recipeMixingInfo = new List<RecipeMixingInfo>();
            List<RecipeWeightInfo> recipeWeightInfo = new List<RecipeWeightInfo>();
            List<RecipeOpenMixingInfo> recipeOpenMixingInfo = new List<RecipeOpenMixingInfo>();



            string Result = ValidityPmtInfo(pmtRecipe, pmtRecipeWeight, pmtRecipeMixing, pmtRecipeOpenMixing,
                ref recipeInfo, ref recipeMixingInfo, ref recipeWeightInfo, ref recipeOpenMixingInfo);

            PmtRecipe p = recipeInfo.PmtRecipe;

            if (!string.IsNullOrWhiteSpace(Result))
            {
                return Result;
            }

            #region 保存数据
            if (!string.IsNullOrEmpty(recipeInfo.PmtRecipe.RecipeEquipCode) && !string.IsNullOrEmpty(recipeInfo.PmtRecipe.RecipeMaterialCode) && recipeInfo.PmtRecipe.RecipeVersionID > 0)
            {
                if (PmtRecipeChanged(recipeInfo, recipeMixingInfo, recipeWeightInfo, recipeOpenMixingInfo) == 0)
                {
                    return "没有进行数据修改，不需要保存";
                }
                recipeInfo.PmtRecipe.AuditUser = string.Empty;
                recipeInfo.PmtRecipe.AuditDateTime = DateTime.Now;
                recipeInfo.PmtRecipe.AuditFlag = "0";
                recipeInfo.PmtRecipe.RecipeModifyTime = DateTime.Now;
                Result = UpdatePmtRecipe(recipeInfo, recipeMixingInfo, recipeWeightInfo, recipeOpenMixingInfo);
            }
            else
            {
                recipeInfo.PmtRecipe.RecipeModifyTime = DateTime.Now;
                Result = InsertPmtRecipe(recipeInfo, recipeMixingInfo, recipeWeightInfo, recipeOpenMixingInfo);
            }
            if (string.IsNullOrWhiteSpace(Result))
            {
                SavePmtRecipeSuccess(recipeInfo);
            }

            #endregion
            return Result;
        }
        #endregion
        /// <summary>
        /// 保存一次法工艺配方
        /// 闫志旭 @ 2015-01-14 12:18:09
        /// </summary>
        /// <param name="pmtRecipe">The PMT recipe.</param>
        /// <param name="pmtRecipeWeight">The PMT recipe weight.</param>
        /// <param name="pmtRecipeMixing">The PMT recipe mixing.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string SavePmtRecipe(PmtRecipe pmtRecipe, EntityArrayList<PmtRecipeWeightMid> PmtRecipeWeightMid, PmtPMILLMain m, PmtSMILLMain s, PmtCoolMILLMain c, EntityArrayList<PmtRecipeWeight> recipeWeight)
        {
            return "";

        }
        private string ValidityPmtInfo(PmtRecipe pmtRecipe,
                                    EntityArrayList<PmtRecipeWeightMid> PmtRecipeWeightMid,

                                   ref RecipeInfo recipe,

                                   ref List<PmtRecipeWeightMid> recipeWeightMidInfo)
        {
            #region 初始化数据
            if (pmtRecipe.ObjID > 0)
            {

            }
            EntityArrayList<BasMaterial> basMaterialList = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == pmtRecipe.RecipeMaterialCode);
            string materialName = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == pmtRecipe.RecipeMaterialCode)[0].MaterialName;
            string Result = IniPmtRecipyici(pmtRecipe, ref recipe);

            #endregion

            #region 校验数据

            #endregion
            #region 数据整理
            int step = 0;
            foreach (PmtRecipeWeightMid info in PmtRecipeWeightMid)
            {
                if (info.WeightID != null)
                {
                    step++;
                    info.WeightID = step;
                    recipeWeightMidInfo.Add(info);
                }
            }

            #endregion
            return Result;
        }
        //一次法验证配方信息
        private string IniPmtRecipyici(PmtRecipe pmtRecipe, ref RecipeInfo recipeInfo)
        {
            string Result = string.Empty;
            recipeInfo = new RecipeInfo();
            recipeInfo.PmtRecipe = pmtRecipe;
            if (string.IsNullOrWhiteSpace(pmtRecipe.RecipeMaterialCode))
            {
                return "物料信息不能为空";
            }

            if (string.IsNullOrWhiteSpace(pmtRecipe.RecipeEquipCode))
            {
                return "机台信息不能为空";
            }
            EntityArrayList<BasMaterial> basMaterialList = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == pmtRecipe.RecipeMaterialCode);
            if (basMaterialList.Count == 0)
            {
                return "物料信息不存在";
            }
            EntityArrayList<BasEquip> basEquipList = basEquipManager.GetListByWhere(BasEquip._.EquipCode == pmtRecipe.RecipeEquipCode);
            if (basMaterialList.Count == 0)
            {
                return "机台信息不存在";
            }
            if (pmtRecipe.RecipeType == null)
            {
                return "请设置配方类型";
            }
            recipeInfo.BasMaterial = basMaterialList[0];
            recipeInfo.BasEquip = basEquipList[0];
            //   recipeInfo.PmtEquipJarStore = pmtEquipJarStoreManager.GetListByWhere(PmtEquipJarStore._.EquipCode == pmtRecipe.RecipeEquipCode);
            return Result;
        }
        private string InsertPmtRecipe(RecipeInfo recipeInfo, List<PmtRecipeWeightMid> PmtRecipeWeightMid, PmtPMILLMain m, PmtSMILLMain s, PmtCoolMILLMain c, List<RecipeWeightInfo> recipeWeightInfo)
        {
            string Result = string.Empty;
            try
            {
                int objid = ((int)this.GetMaxValueByProperty(PmtRecipe._.ObjID) + 1);
                recipeInfo.PmtRecipe.ObjID = objid;
                recipeInfo.PmtRecipe.RecipeVersionID = GetMaxRecipeVersionID(recipeInfo);
                recipeInfo.PmtRecipe.Detach();
                IniRecipeName(recipeInfo);
                this.Insert(recipeInfo.PmtRecipe);
                PmtRecipe thisPmtRecipe = this.GetById(objid);
                m.EdtCode = thisPmtRecipe.RecipeVersionID;
                s.EdtCode = thisPmtRecipe.RecipeVersionID;
                c.EdtCode = thisPmtRecipe.RecipeVersionID;
                for (int i = 0; i < PmtRecipeWeightMid.Count; i++)
                {
                    PmtRecipeWeightMid[i].EdtCode = thisPmtRecipe.RecipeVersionID;


                }
                InsertPmtRecipeWeightMid(recipeInfo, PmtRecipeWeightMid);
                InsertPmtPMILLMain(recipeInfo, m);
                InsertPmtSMILLMain(recipeInfo, s);
                InsertPmtCoolMILLMain(recipeInfo, c);
                InsertPmtRecipeWeight(recipeInfo, recipeWeightInfo);
            }
            catch (Exception ex)
            {
                Result = "信息添加错误";
                Result = ex.Message;
            }

            return Result;
        }
        private string UpdatePmtRecipe(RecipeInfo recipeInfo, List<PmtRecipeWeightMid> PmtRecipeWeightMid, PmtPMILLMain m, PmtSMILLMain s, PmtCoolMILLMain c, List<RecipeWeightInfo> recipeWeightInfo)
        {
            string Result = string.Empty;
            try
            {
                recipeInfo.PmtRecipe.RecipeVersionID = GetMaxRecipeVersionID(recipeInfo);
                IniRecipeName(recipeInfo);
                //this.Update(recipeInfo.PmtRecipe);
                string sql = @"update pmt_recipe set Recipe_Type='" + recipeInfo.PmtRecipe.RecipeType + @"',Recipe_State='" + recipeInfo.PmtRecipe.RecipeState + @"',Total_Weight='" + recipeInfo.PmtRecipe.LotTotalWeight + @"',Shelf_Num='" + recipeInfo.PmtRecipe.ShelfLotCount + @"',Done_Time='" + recipeInfo.PmtRecipe.LotDoneTime + @"',OverTime_Time='" + recipeInfo.PmtRecipe.OverTimeSetTime + @"',OverTemp_Temp='" + recipeInfo.PmtRecipe.OverTempSetTemp + @"'
                               , Min_InPolyTemp = '" + recipeInfo.PmtRecipe.InPolyMinTemp + @"', CB_RecycleTime = '" + recipeInfo.PmtRecipe.CarbonRecycleTime + @"', Ddoor_Temp = '" + recipeInfo.PmtRecipe.DdoorTemp + @"', Side_Temp = '" + recipeInfo.PmtRecipe.SideTemp + @"', Roll_Temp = '" + recipeInfo.PmtRecipe.RollTemp + @"',Rearch_code='" + recipeInfo.PmtRecipe.RearchCode + @"',User_EdtCode='" + recipeInfo.PmtRecipe.UseredtCode + @"'
                    , Modify_time= CONVERT(char(19), GETDATE(), 120),Oper_Code ='" + recipeInfo.PmtRecipe.RecipeModifyUser + @"'       where Equip_Code = '" + recipeInfo.PmtRecipe.RecipeEquipCode + "' and Mater_Code = '" + recipeInfo.PmtRecipe.RecipeMaterialCode + "' and Edt_Code = '" + recipeInfo.PmtRecipe.RecipeVersionID + "'";
                this.GetBySql(sql).ToDataSet();
                InsertPmtRecipeWeightMid(recipeInfo, PmtRecipeWeightMid);
                InsertPmtPMILLMain(recipeInfo, m);
                InsertPmtSMILLMain(recipeInfo, s);
                InsertPmtCoolMILLMain(recipeInfo, c);
                InsertPmtRecipeWeight(recipeInfo, recipeWeightInfo);
            }
            catch (Exception ex)
            {
                Result = "信息添加错误";
                Result = ex.Message;
            }

            return Result;
        }
        private string InsertPmtRecipeWeightMid(RecipeInfo recipeInfo, List<PmtRecipeWeightMid> PmtRecipeWeightMidList)
        {

            string Result = string.Empty;
            if (PmtRecipeWeightMidList.Count == 0)
            {
                return Result;
            }

            WeightMidManager.DeleteByWhere(PmtRecipeWeightMid._.RecipeObjID == recipeInfo.PmtRecipe.ObjID);
            List<PmtRecipeWeightMid> lst = new List<PmtRecipeWeightMid>();
            for (int i = 0; i < PmtRecipeWeightMidList.Count; i++)
            {
                PmtRecipeWeightMid m = PmtRecipeWeightMidList[i];
                m.Detach();
                m.RecipeObjID = recipeInfo.PmtRecipe.ObjID;
                //m.RecipeEquipCode = recipeInfo.PmtRecipe.RecipeEquipCode;
                //m.RecipeMaterialCode = recipeInfo.PmtRecipe.RecipeMaterialCode;
                //m.RecipeVersionID = recipeInfo.PmtRecipe.RecipeVersionID;
                lst.Add(m);
            }
            WeightMidManager.BatchInsert(lst);
            return Result;
        }
        private string InsertPmtPMILLMain(RecipeInfo recipeInfo, PmtPMILLMain m)
        {

            string Result = string.Empty;
            if (m == null)
            {
                return Result;
            }

            PMILLMainManager.DeleteByWhere(PmtPMILLMain._.RecipeObjID == recipeInfo.PmtRecipe.ObjID);


            m.Detach();
            m.RecipeObjID = recipeInfo.PmtRecipe.ObjID;
            //m.RecipeEquipCode = recipeInfo.PmtRecipe.RecipeEquipCode;
            //m.RecipeMaterialCode = recipeInfo.PmtRecipe.RecipeMaterialCode;
            //m.RecipeVersionID = recipeInfo.PmtRecipe.RecipeVersionID;


            PMILLMainManager.Insert(m);
            return Result;
        }
        private string InsertPmtSMILLMain(RecipeInfo recipeInfo, PmtSMILLMain m)
        {

            string Result = string.Empty;
            if (m == null)
            {
                return Result;
            }

            SMILLMainManager.DeleteByWhere(PmtSMILLMain._.RecipeObjID == recipeInfo.PmtRecipe.ObjID);


            m.Detach();
            m.RecipeObjID = recipeInfo.PmtRecipe.ObjID;
            //m.RecipeEquipCode = recipeInfo.PmtRecipe.RecipeEquipCode;
            //m.RecipeMaterialCode = recipeInfo.PmtRecipe.RecipeMaterialCode;
            //m.RecipeVersionID = recipeInfo.PmtRecipe.RecipeVersionID;


            SMILLMainManager.Insert(m);
            return Result;
        }
        private string InsertPmtCoolMILLMain(RecipeInfo recipeInfo, PmtCoolMILLMain m)
        {

            string Result = string.Empty;
            if (m == null)
            {
                return Result;
            }

            CoolMILLMainManager.DeleteByWhere(PmtCoolMILLMain._.RecipeObjID == recipeInfo.PmtRecipe.ObjID);


            m.Detach();
            m.RecipeObjID = recipeInfo.PmtRecipe.ObjID;
            //m.RecipeEquipCode = recipeInfo.PmtRecipe.RecipeEquipCode;
            //m.RecipeMaterialCode = recipeInfo.PmtRecipe.RecipeMaterialCode;
            //m.RecipeVersionID = recipeInfo.PmtRecipe.RecipeVersionID;


            CoolMILLMainManager.Insert(m);
            return Result;
        }
        #region 配方另存
        /// <summary>
        /// 机台拷贝
        /// 孙本强 @ 2013-04-03 12:46:47
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <param name="RecipeID">The recipe ID.</param>
        /// <param name="EquipCode">The equip code.</param>
        /// <param name="RecipeState">State of the recipe.</param>
        /// <param name="RecipeType">Type of the recipe.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string CopyToEquip(string UserID, string RecipeID, string EquipCode, string RecipeState, string RecipeType)
        {
            string Result = string.Empty;
            //PmtRecipe pmtRecipe = this.GetById(RecipeID);
            EntityArrayList<PmtRecipe> pmtRecipeList = this.GetListByWhere(PmtRecipe._.ObjID== RecipeID);
            PmtRecipe pmtRecipe = pmtRecipeList[0];

            EntityArrayList <PmtRecipeWeight> pmtRecipeWeight = weightManager.GetListByWhere(PmtRecipeWeight._.RecipeObjID == RecipeID);
            EntityArrayList<PmtRecipeMixing> pmtRecipeMixing = mixingManager.GetListByWhere(PmtRecipeMixing._.RecipeObjID == RecipeID);

            //if (RecipeState == ((int)PmtState.用完).ToString())
            //{
            //    EntityArrayList<PmtRecipe> pmtRecipeEquip = this.GetListByWhere(PmtRecipe._.RecipeEquipCode == EquipCode
            //                                            && PmtRecipe._.RecipeMaterialCode == pmtRecipe.RecipeMaterialCode
            //                                            && PmtRecipe._.RecipeState == ((int)PmtState.正用).ToString());

            //    foreach (PmtRecipe r in pmtRecipeEquip)
            //    {
            //        r.RecipeState = ((int)PmtState.正用).ToString();
            //        r.AuditFlag = "0";
            //        r.AuditUser = "";
            //        r.AuditDateTime = null;
            //     //   this.Update(r);
            //        RecipeInfo m = new RecipeInfo();
            //        m.PmtRecipe = r;
            //        this.service.SavePmtRecipeLog(r.ObjID.ToString());
            //    }
            //}
            pmtRecipe.Detach();

            RecipeInfo info = new RecipeInfo();
            info.PmtRecipe = pmtRecipe;
            pmtRecipe.ObjID = ((int)this.GetMaxValueByProperty(PmtRecipe._.ObjID) + 1);
            pmtRecipe.RecipeEquipCode = EquipCode;
            pmtRecipe.RecipeState = RecipeState;
            pmtRecipe.RecipeType = Convert.ToInt32(RecipeType);
            pmtRecipe.AuditFlag = "0";
            pmtRecipe.AuditUser = "";
            pmtRecipe.AuditDateTime = DateTime.Now;
            pmtRecipe.CanAuditUser = "";
            pmtRecipe.RecipeModifyUser = UserID;
            pmtRecipe.RecipeModifyTime = DateTime.Now;
            pmtRecipe.RecipeVersionID = GetMaxRecipeVersionID(info);
            IniRecipeName(info);
            string sql = @" insert into pmt_recipe(ObjId,Oper_Code,Modify_time,Equip_Code,Mater_Code,Edt_Code,Mater_Name,Recipe_Type,Recipe_State,Define_Date,
                        done_time,Shelf_Num,Total_Weight,CB_RecycleType,CB_RecycleTime,OverTemp_MinTime,OverTime_Time,
                        OverTemp_Temp,Max_InPolyTemp,Min_InPolyTemp,Ddoor_Temp,Side_Temp,Roll_Temp,Is_UseAreaTemp,Recipe_Code,Rearch_code,User_EdtCode) values( '" 
                        + pmtRecipe.ObjID + "','" + UserID + @"',CONVERT(char(19), GETDATE(), 120),'" + info.PmtRecipe.RecipeEquipCode + @"', '" + info.PmtRecipe.RecipeMaterialCode + @"', '" + info.PmtRecipe.RecipeVersionID + @"', '" + info.PmtRecipe.RecipeMaterialName + @"', '" + info.PmtRecipe.RecipeType + @"', '" + info.PmtRecipe.RecipeState + @"', CONVERT(char(19), GETDATE(), 121)
                        , '" + info.PmtRecipe.LotDoneTime + @"', '" + info.PmtRecipe.ShelfLotCount + @"', '" + info.PmtRecipe.LotTotalWeight + @"', '" + info.PmtRecipe.CarbonRecycleType + @"', '" + info.PmtRecipe.CarbonRecycleTime + @"', '" + info.PmtRecipe.OverTempMinTime + @"', '" + info.PmtRecipe.OverTimeSetTime + @"'
                        , '" + info.PmtRecipe.OverTempSetTemp + @"', '" + info.PmtRecipe.InPolyMaxTemp + @"', '" + info.PmtRecipe.InPolyMinTemp + @"', '" + info.PmtRecipe.DdoorTemp + @"', '" + info.PmtRecipe.SideTemp + @"', '" + info.PmtRecipe.RollTemp + @"', '" + info.PmtRecipe.IsUseAreaTemp + "','" + info.PmtRecipe.RecipeName + "','" + info.PmtRecipe.RearchCode + "','" + info.PmtRecipe.UseredtCode + "') ";
            this.GetBySql(sql).ToDataSet();
            //this.Insert(pmtRecipe);
            foreach (PmtRecipeWeight m in pmtRecipeWeight)
            {
                m.Detach();
                m.RecipeObjID = pmtRecipe.ObjID;
                m.RecipeEquipCode = pmtRecipe.RecipeEquipCode;
                m.RecipeVersionID = Convert.ToInt32(pmtRecipe.RecipeVersionID);
                //weightManager.Insert(m);
                Pmt_Weight pw = GetPmt_Weight(m);
                IPmt_WeightManager pmm = new Pmt_WeightManager();
                pmm.Insert(pw);
            }
            foreach (PmtRecipeMixing m in pmtRecipeMixing)
            {
                m.Detach();
                m.RecipeObjID = pmtRecipe.ObjID;
                m.RecipeEquipCode = pmtRecipe.RecipeEquipCode;
                m.RecipeVersionID = Convert.ToInt32(pmtRecipe.RecipeVersionID);
                //mixingManager.Insert(m);

                Pmt_Mixing pm = GetPmt_Mixing(m);
                IPmt_MixingManager pmm = new Pmt_MixingManager();
                pmm.Insert(pm);
            }
            this.service.SavePmtRecipeLog(pmtRecipe.ObjID.ToString());
            return Result;
        }
        /// <summary>
        /// 物料配方另存
        /// 孙本强 @ 2013-04-03 12:18:09
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <param name="RecipeID">The recipe ID.</param>
        /// <param name="EquipCodeList">The equip code list.</param>
        /// <param name="RecipeState">State of the recipe.</param>
        /// <param name="RecipeType">Type of the recipe.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string CopyToEquip(string UserID, string RecipeID, string[] EquipCodeList, string RecipeState, string RecipeType)
        {
            string Result = string.Empty;
            foreach (string EquipCode in EquipCodeList)
            {
                if (string.IsNullOrWhiteSpace(EquipCode))
                {
                    continue;
                }
                Result = CopyToEquip(UserID, RecipeID, EquipCode, RecipeState, RecipeType);
                if (!string.IsNullOrWhiteSpace(Result))
                {
                    return Result;
                }
            }
            return Result;
        }
        #endregion

        #region 配方另存
        /// <summary>
        /// 机台、不同物料拷贝
        /// 于小鹏 @ 2014-1-09 12:46:47
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <param name="RecipeID">The recipe ID.</param>
        /// <param name="EquipCode">The equip code.</param>
        /// <param name="RecipeState">State of the recipe.</param>
        /// <param name="RecipeType">Type of the recipe.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string CopyToEquip(string UserID, string RecipeID, string EquipCode, string RecipeState, string RecipeType, string materialcode)
        {
            string Result = string.Empty;
           // PmtRecipe pmtRecipe = this.GetById(RecipeID);
            EntityArrayList<PmtRecipe> pmtRecipeList = this.GetListByWhere(PmtRecipe._.ObjID == RecipeID);
            PmtRecipe pmtRecipe = pmtRecipeList[0];
            EntityArrayList<PmtRecipeWeight> pmtRecipeWeight = weightManager.GetListByWhere(PmtRecipeWeight._.RecipeObjID == RecipeID);
            EntityArrayList<PmtRecipeMixing> pmtRecipeMixing = mixingManager.GetListByWhere(PmtRecipeMixing._.RecipeObjID == RecipeID);


            pmtRecipe.Detach();

            RecipeInfo info = new RecipeInfo();
            info.PmtRecipe = pmtRecipe;
            pmtRecipe.ObjID = ((int)this.GetMaxValueByProperty(PmtRecipe._.ObjID) + 1);
            pmtRecipe.RecipeMaterialCode = materialcode;
            pmtRecipe.RecipeMaterialName = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == materialcode)[0].MaterialName;
            pmtRecipe.RecipeEquipCode = EquipCode;
            pmtRecipe.RecipeState = RecipeState;
            pmtRecipe.RecipeType = Convert.ToInt32(RecipeType);
            pmtRecipe.AuditFlag = "0";
            pmtRecipe.AuditUser = "";
            pmtRecipe.AuditDateTime = DateTime.Now;
            pmtRecipe.CanAuditUser = "";
            pmtRecipe.RecipeModifyUser = UserID;
            pmtRecipe.RecipeModifyTime = DateTime.Now;
            pmtRecipe.RecipeVersionID = GetMaxRecipeVersionID(info);
            IniRecipeName(info);
            string sql = @" insert into pmt_recipe(ObjId,Oper_Code,Modify_time,Equip_Code,Mater_Code,Edt_Code,Mater_Name,Recipe_Type,Recipe_State,Define_Date,
                        done_time,Shelf_Num,Total_Weight,CB_RecycleType,CB_RecycleTime,OverTemp_MinTime,OverTime_Time,
                        OverTemp_Temp,Max_InPolyTemp,Min_InPolyTemp,Ddoor_Temp,Side_Temp,Roll_Temp,Is_UseAreaTemp,Recipe_Code,Rearch_code,User_EdtCode) values( '" + pmtRecipe.ObjID + "','" + UserID + @"',CONVERT(char(19), GETDATE(), 120),'" + info.PmtRecipe.RecipeEquipCode + @"', '" + info.PmtRecipe.RecipeMaterialCode + @"', '" + info.PmtRecipe.RecipeVersionID + @"', '" + info.PmtRecipe.RecipeMaterialName + @"', '" + info.PmtRecipe.RecipeType + @"', '" + info.PmtRecipe.RecipeState + @"', CONVERT(char(19), GETDATE(), 121)
                        , '" + info.PmtRecipe.LotDoneTime + @"', '" + info.PmtRecipe.ShelfLotCount + @"', '" + info.PmtRecipe.LotTotalWeight + @"', '" + info.PmtRecipe.CarbonRecycleType + @"', '" + info.PmtRecipe.CarbonRecycleTime + @"', '" + info.PmtRecipe.OverTempMinTime + @"', '" + info.PmtRecipe.OverTimeSetTime + @"'
                        , '" + info.PmtRecipe.OverTempSetTemp + @"', '" + info.PmtRecipe.InPolyMaxTemp + @"', '" + info.PmtRecipe.InPolyMinTemp + @"', '" + info.PmtRecipe.DdoorTemp + @"', '" + info.PmtRecipe.SideTemp + @"', '" + info.PmtRecipe.RollTemp + @"', '" + info.PmtRecipe.IsUseAreaTemp + "','" + info.PmtRecipe.RecipeName + "','" + info.PmtRecipe.RearchCode + "','" + info.PmtRecipe.UseredtCode + "') ";
            this.GetBySql(sql).ToDataSet();
            //this.Insert(pmtRecipe);
            foreach (PmtRecipeWeight m in pmtRecipeWeight)
            {
                m.Detach();
                m.RecipeObjID = pmtRecipe.ObjID;
                m.RecipeMaterialCode = materialcode;
                m.RecipeEquipCode = pmtRecipe.RecipeEquipCode;
                m.RecipeVersionID = Convert.ToInt32(pmtRecipe.RecipeVersionID);
                //weightManager.Insert(m);

                Pmt_Weight pw = GetPmt_Weight(m);
                IPmt_WeightManager pmm = new Pmt_WeightManager();
                pmm.Insert(pw);
            }
            foreach (PmtRecipeMixing m in pmtRecipeMixing)
            {
                m.Detach();
                m.RecipeObjID = pmtRecipe.ObjID;
                m.RecipeMaterialCode = materialcode;
                m.RecipeEquipCode = pmtRecipe.RecipeEquipCode;
                m.RecipeVersionID = Convert.ToInt32(pmtRecipe.RecipeVersionID);
                //mixingManager.Insert(m);

                Pmt_Mixing pm = GetPmt_Mixing(m);
                IPmt_MixingManager pmm = new Pmt_MixingManager();
                pmm.Insert(pm);
            }
            this.service.SavePmtRecipeLog(pmtRecipe.ObjID.ToString());
            return Result;
        }
        /// <summary>
        /// 物料配方另存
        /// 孙本强 @ 2013-04-03 12:18:09
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <param name="RecipeID">The recipe ID.</param>
        /// <param name="EquipCodeList">The equip code list.</param>
        /// <param name="RecipeState">State of the recipe.</param>
        /// <param name="RecipeType">Type of the recipe.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string CopyToEquip(string UserID, string RecipeID, string[] EquipCodeList, string RecipeState, string RecipeType, string matercode)
        {
            string Result = string.Empty;
            foreach (string EquipCode in EquipCodeList)
            {
                if (string.IsNullOrWhiteSpace(EquipCode))
                {
                    continue;
                }
                if (string.IsNullOrWhiteSpace(matercode))
                {
                    Result = CopyToEquip(UserID, RecipeID, EquipCode, RecipeState, RecipeType);
                }
                else
                {
                    Result = CopyToEquip(UserID, RecipeID, EquipCode, RecipeState, RecipeType, matercode);
                }

                if (!string.IsNullOrWhiteSpace(Result))
                {
                    return Result;
                }
            }
            return Result;
        }
        #endregion
        /// <summary>
        /// 对配方进行作废修改
        /// 于小鹏 @  2013-05-13 11:18:09
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        public string UpdateRecipe(PmtRecipe recipe)
        {
            string result = string.Empty;
            try
            {
                this.Update(recipe);
            }
            catch (Exception ex)
            {
                result = "信息添加错误";
                result = ex.Message;
            }

            return result;
        }


        /// <summary>
        /// 物料配方另存
        /// 孙本强 @ 2013-04-03 12:18:09
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <param name="RecipeID">The recipe ID.</param>
        /// <param name="EquipCodeList">The equip code list.</param>
        /// <param name="RecipeState">State of the recipe.</param>
        /// <param name="RecipeType">Type of the recipe.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string CopyToEquipNew(string UserID, string RecipeID, string[] EquipCodeList, string RecipeState, string RecipeType, string matercode, string Version)
        {
            string Result = string.Empty;
            foreach (string EquipCode in EquipCodeList)
            {
                if (string.IsNullOrWhiteSpace(EquipCode))
                {
                    continue;
                }
                if (string.IsNullOrWhiteSpace(matercode))
                {
                    Result = CopyToEquipNew(UserID, RecipeID, EquipCode, RecipeState, RecipeType,Version);
                }
                else
                {
                    Result = CopyToEquipNew(UserID, RecipeID, EquipCode, RecipeState, RecipeType, matercode, Version);
                }

                if (!string.IsNullOrWhiteSpace(Result))
                {
                    return Result;
                }
            }
            return Result;
        }

        /// <summary>
        /// 机台拷贝
        /// 孙本强 @ 2013-04-03 12:46:47
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <param name="RecipeID">The recipe ID.</param>
        /// <param name="EquipCode">The equip code.</param>
        /// <param name="RecipeState">State of the recipe.</param>
        /// <param name="RecipeType">Type of the recipe.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string CopyToEquipNew(string UserID, string RecipeID, string EquipCode, string RecipeState, string RecipeType, string Version)
        {
            string Result = string.Empty;
            //PmtRecipe pmtRecipe = this.GetById(RecipeID);
            EntityArrayList<PmtRecipe> pmtRecipeList = this.GetListByWhere(PmtRecipe._.ObjID == RecipeID);
            PmtRecipe pmtRecipe = pmtRecipeList[0];

            EntityArrayList<PmtRecipeWeight> pmtRecipeWeight = weightManager.GetListByWhere(PmtRecipeWeight._.RecipeObjID == RecipeID);
            EntityArrayList<PmtRecipeMixing> pmtRecipeMixing = mixingManager.GetListByWhere(PmtRecipeMixing._.RecipeObjID == RecipeID);

            pmtRecipe.Detach();

            RecipeInfo info = new RecipeInfo();
            info.PmtRecipe = pmtRecipe;
            pmtRecipe.ObjID = ((int)this.GetMaxValueByProperty(PmtRecipe._.ObjID) + 1);
            pmtRecipe.RecipeEquipCode = EquipCode;
            pmtRecipe.RecipeState = RecipeState;
            pmtRecipe.RecipeType = Convert.ToInt32(RecipeType);
            pmtRecipe.AuditFlag = "0";
            pmtRecipe.AuditUser = "";
            pmtRecipe.AuditDateTime = DateTime.Now;
            pmtRecipe.CanAuditUser = "";
            pmtRecipe.RecipeModifyUser = UserID;
            pmtRecipe.RecipeModifyTime = DateTime.Now;
            //pmtRecipe.RecipeVersionID = GetMaxRecipeVersionID(info);
            pmtRecipe.RecipeVersionID = Convert.ToInt32(Version);
            IniRecipeName(info);
            string sql = @" insert into pmt_recipe(ObjId,Oper_Code,Modify_time,Equip_Code,Mater_Code,Edt_Code,Mater_Name,Recipe_Type,Recipe_State,Define_Date,
                        done_time,Shelf_Num,Total_Weight,CB_RecycleType,CB_RecycleTime,OverTemp_MinTime,OverTime_Time,
                        OverTemp_Temp,Max_InPolyTemp,Min_InPolyTemp,Ddoor_Temp,Side_Temp,Roll_Temp,Is_UseAreaTemp,Recipe_Code,Rearch_code,User_EdtCode) values( '"
                        + pmtRecipe.ObjID + "','" + UserID + @"',CONVERT(char(19), GETDATE(), 120),'" + info.PmtRecipe.RecipeEquipCode + @"', '" + info.PmtRecipe.RecipeMaterialCode + @"', '" + info.PmtRecipe.RecipeVersionID + @"', '" + info.PmtRecipe.RecipeMaterialName + @"', '" + info.PmtRecipe.RecipeType + @"', '" + info.PmtRecipe.RecipeState + @"', CONVERT(char(19), GETDATE(), 121)
                        , '" + info.PmtRecipe.LotDoneTime + @"', '" + info.PmtRecipe.ShelfLotCount + @"', '" + info.PmtRecipe.LotTotalWeight + @"', '" + info.PmtRecipe.CarbonRecycleType + @"', '" + info.PmtRecipe.CarbonRecycleTime + @"', '" + info.PmtRecipe.OverTempMinTime + @"', '" + info.PmtRecipe.OverTimeSetTime + @"'
                        , '" + info.PmtRecipe.OverTempSetTemp + @"', '" + info.PmtRecipe.InPolyMaxTemp + @"', '" + info.PmtRecipe.InPolyMinTemp + @"', '" + info.PmtRecipe.DdoorTemp + @"', '" + info.PmtRecipe.SideTemp + @"', '" + info.PmtRecipe.RollTemp + @"', '" + info.PmtRecipe.IsUseAreaTemp + "','" + info.PmtRecipe.RecipeName + "','" + info.PmtRecipe.RearchCode + "','" + info.PmtRecipe.UseredtCode + "') ";
            this.GetBySql(sql).ToDataSet();
            //this.Insert(pmtRecipe);
            foreach (PmtRecipeWeight m in pmtRecipeWeight)
            {
                m.Detach();
                m.RecipeObjID = pmtRecipe.ObjID;
                m.RecipeEquipCode = pmtRecipe.RecipeEquipCode;
                m.RecipeVersionID = Convert.ToInt32(pmtRecipe.RecipeVersionID);
                //weightManager.Insert(m);
                Pmt_Weight pw = GetPmt_Weight(m);
                IPmt_WeightManager pmm = new Pmt_WeightManager();
                pmm.Insert(pw);
            }
            foreach (PmtRecipeMixing m in pmtRecipeMixing)
            {
                m.Detach();
                m.RecipeObjID = pmtRecipe.ObjID;
                m.RecipeEquipCode = pmtRecipe.RecipeEquipCode;
                m.RecipeVersionID = Convert.ToInt32(pmtRecipe.RecipeVersionID);
                //mixingManager.Insert(m);

                Pmt_Mixing pm = GetPmt_Mixing(m);
                IPmt_MixingManager pmm = new Pmt_MixingManager();
                pmm.Insert(pm);
            }
            this.service.SavePmtRecipeLog(pmtRecipe.ObjID.ToString());
            return Result;
        }

        /// <summary>
        /// 机台、不同物料拷贝
        /// 于小鹏 @ 2014-1-09 12:46:47
        /// </summary>
        /// <param name="UserID">The user ID.</param>
        /// <param name="RecipeID">The recipe ID.</param>
        /// <param name="EquipCode">The equip code.</param>
        /// <param name="RecipeState">State of the recipe.</param>
        /// <param name="RecipeType">Type of the recipe.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string CopyToEquipNew(string UserID, string RecipeID, string EquipCode, string RecipeState, string RecipeType, string materialcode, string Version)
        {
            string Result = string.Empty;
            // PmtRecipe pmtRecipe = this.GetById(RecipeID);
            EntityArrayList<PmtRecipe> pmtRecipeList = this.GetListByWhere(PmtRecipe._.ObjID == RecipeID);
            PmtRecipe pmtRecipe = pmtRecipeList[0];
            EntityArrayList<PmtRecipeWeight> pmtRecipeWeight = weightManager.GetListByWhere(PmtRecipeWeight._.RecipeObjID == RecipeID);
            EntityArrayList<PmtRecipeMixing> pmtRecipeMixing = mixingManager.GetListByWhere(PmtRecipeMixing._.RecipeObjID == RecipeID);


            pmtRecipe.Detach();

            RecipeInfo info = new RecipeInfo();
            info.PmtRecipe = pmtRecipe;
            pmtRecipe.ObjID = ((int)this.GetMaxValueByProperty(PmtRecipe._.ObjID) + 1);
            pmtRecipe.RecipeMaterialCode = materialcode;
            pmtRecipe.RecipeMaterialName = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == materialcode)[0].MaterialName;
            pmtRecipe.RecipeEquipCode = EquipCode;
            pmtRecipe.RecipeState = RecipeState;
            pmtRecipe.RecipeType = Convert.ToInt32(RecipeType);
            pmtRecipe.AuditFlag = "0";
            pmtRecipe.AuditUser = "";
            pmtRecipe.AuditDateTime = DateTime.Now;
            pmtRecipe.CanAuditUser = "";
            pmtRecipe.RecipeModifyUser = UserID;
            pmtRecipe.RecipeModifyTime = DateTime.Now;
            //pmtRecipe.RecipeVersionID = GetMaxRecipeVersionID(info);
            pmtRecipe.RecipeVersionID = Convert.ToInt32(Version);
            IniRecipeName(info);
            string sql = @" insert into pmt_recipe(ObjId,Oper_Code,Modify_time,Equip_Code,Mater_Code,Edt_Code,Mater_Name,Recipe_Type,Recipe_State,Define_Date,
                        done_time,Shelf_Num,Total_Weight,CB_RecycleType,CB_RecycleTime,OverTemp_MinTime,OverTime_Time,
                        OverTemp_Temp,Max_InPolyTemp,Min_InPolyTemp,Ddoor_Temp,Side_Temp,Roll_Temp,Is_UseAreaTemp,Recipe_Code,Rearch_code,User_EdtCode) values( '" + pmtRecipe.ObjID + "','" + UserID + @"',CONVERT(char(19), GETDATE(), 120),'" + info.PmtRecipe.RecipeEquipCode + @"', '" + info.PmtRecipe.RecipeMaterialCode + @"', '" + info.PmtRecipe.RecipeVersionID + @"', '" + info.PmtRecipe.RecipeMaterialName + @"', '" + info.PmtRecipe.RecipeType + @"', '" + info.PmtRecipe.RecipeState + @"', CONVERT(char(19), GETDATE(), 121)
                        , '" + info.PmtRecipe.LotDoneTime + @"', '" + info.PmtRecipe.ShelfLotCount + @"', '" + info.PmtRecipe.LotTotalWeight + @"', '" + info.PmtRecipe.CarbonRecycleType + @"', '" + info.PmtRecipe.CarbonRecycleTime + @"', '" + info.PmtRecipe.OverTempMinTime + @"', '" + info.PmtRecipe.OverTimeSetTime + @"'
                        , '" + info.PmtRecipe.OverTempSetTemp + @"', '" + info.PmtRecipe.InPolyMaxTemp + @"', '" + info.PmtRecipe.InPolyMinTemp + @"', '" + info.PmtRecipe.DdoorTemp + @"', '" + info.PmtRecipe.SideTemp + @"', '" + info.PmtRecipe.RollTemp + @"', '" + info.PmtRecipe.IsUseAreaTemp + "','" + info.PmtRecipe.RecipeName + "','" + info.PmtRecipe.RearchCode + "','" + info.PmtRecipe.UseredtCode + "') ";
            this.GetBySql(sql).ToDataSet();
            //this.Insert(pmtRecipe);
            foreach (PmtRecipeWeight m in pmtRecipeWeight)
            {
                m.Detach();
                m.RecipeObjID = pmtRecipe.ObjID;
                m.RecipeMaterialCode = materialcode;
                m.RecipeEquipCode = pmtRecipe.RecipeEquipCode;
                m.RecipeVersionID = Convert.ToInt32(pmtRecipe.RecipeVersionID);
                //weightManager.Insert(m);

                Pmt_Weight pw = GetPmt_Weight(m);
                IPmt_WeightManager pmm = new Pmt_WeightManager();
                pmm.Insert(pw);
            }
            foreach (PmtRecipeMixing m in pmtRecipeMixing)
            {
                m.Detach();
                m.RecipeObjID = pmtRecipe.ObjID;
                m.RecipeMaterialCode = materialcode;
                m.RecipeEquipCode = pmtRecipe.RecipeEquipCode;
                m.RecipeVersionID = Convert.ToInt32(pmtRecipe.RecipeVersionID);
                //mixingManager.Insert(m);

                Pmt_Mixing pm = GetPmt_Mixing(m);
                IPmt_MixingManager pmm = new Pmt_MixingManager();
                pmm.Insert(pm);
            }
            this.service.SavePmtRecipeLog(pmtRecipe.ObjID.ToString());
            return Result;
        }
    }


}
