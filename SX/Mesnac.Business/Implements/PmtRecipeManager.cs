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
    /// PmtRecipeManager ʵ����
    /// �ﱾǿ @ 2013-04-03 12:46:24
    /// </summary>
    /// <remarks></remarks>
    public class PmtRecipeManager : BaseManager<PmtRecipe>, IPmtRecipeManager
    {

        #region ����ע���빹�췽��

        /// <summary>
        /// �䷽���ݲ�����
        /// �ﱾǿ @ 2013-04-03 12:46:24
        /// </summary>
        private IPmtRecipeService service;

        /// <summary>
        /// �� PmtRecipeManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:46:24
        /// </summary>
        /// <remarks></remarks>
        public PmtRecipeManager()
        {
            this.service = new PmtRecipeService();
            base.BaseService = this.service;
        }

        /// <summary>
        /// �� PmtRecipeManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:46:25
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public PmtRecipeManager(string connectStringKey)
        {
            this.service = new PmtRecipeService(connectStringKey);
            base.BaseService = this.service;
        }

        /// <summary>
        /// �� PmtRecipeManager ���캯��
        /// �ﱾǿ @ 2013-04-03 12:46:25
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtRecipeManager(NBear.Data.Gateway way)
        {
            this.service = new PmtRecipeService(way);
            base.BaseService = this.service;
        }

        #endregion

        #region ��Ϣ��ѯ
        #region ��ѯ�����ඨ��
        /// <summary>
        /// ��ѯ����������
        /// �ﱾǿ @ 2013-04-03 12:46:25
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams : PmtRecipeService.QueryParams
        {
        }
        #endregion

        /// <summary>
        /// �����������ƻ�ȡ�����䷽������Ϣ
        /// ���˽�
        /// 2013-2-25
        /// �ﱾǿ @ 2013-04-03 12:46:26
        /// </summary>
        /// <param name="recipeMaterialName">��������</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public DataSet GetRecipeNameByRecipeMaterialName(string recipeMaterialName)
        {
            return this.service.GetRecipeNameByRecipeMaterialName(recipeMaterialName);
        }
        /// <summary>
        /// ��ȡ������Ϣ
        /// �ﱾǿ @ 2013-04-03 12:18:08
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public EntityArrayList<BasMaterial> GetBasMaterial(QueryParams queryParams)
        {
            return this.service.GetBasMaterial(queryParams);
        }
        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 12:46:26
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        public PageResult<PmtRecipe> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        /// <summary>
        /// ͨ��ƴ����ȡǰ������Ϣ
        /// �ﱾǿ @ 2013-04-03 12:18:08
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

        #region �䷽��Ϣ���

        #region ����ע��
        /// <summary>
        /// �䷽��־
        /// �ﱾǿ @ 2013-04-03 12:46:27
        /// </summary>
        private IPmtRecipeLogManager pmtRecipeLogManager = new PmtRecipeLogManager();
        #endregion

        /// <summary>
        /// ��˹����䷽
        /// �ﱾǿ @ 2013-04-03 12:18:09
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
                    Result = "��ǰ�䷽��������״̬�����ܽ�����ˣ�";
                    return Result;
                }
                else if (m.AuditFlag != "0")
                {
                    Result = "��ǰ�䷽�Ѿ���ˣ�";
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
                Result = "��ǰ�û�������˴��䷽";
            }
            return Result;
        }
        #endregion

        #region �䷽��Ϣɾ��
        /// <summary>
        /// ɾ�������䷽
        /// �ﱾǿ @ 2013-04-03 12:18:09
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
                    return "�����䷽��Ҫ���Ϻ��ٽ���ɾ����";
                }
            }
            if (!string.IsNullOrWhiteSpace(Result))
            {
                Result = "��ǰ�û�����ɾ�����䷽";
            }
            return Result;
        }
        #endregion

        #region �䷽��Ϣ����
        #region ����ע��
        /// <summary>
        /// ϵͳ����
        /// �ﱾǿ @ 2013-04-03 12:46:28
        /// </summary>
        private ISysCodeManager sysCodeManager = new SysCodeManager();
        /// <summary>
        /// ������Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:29
        /// </summary>
        private IBasMaterialManager basMaterialManager = new BasMaterialManager();
        /// <summary>
        /// ��̨��Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:29
        /// </summary>
        private IBasEquipManager basEquipManager = new BasEquipManager();

        /// <summary>
        /// ������Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:29
        /// </summary>
        private IPmtRecipeWeightManager weightManager = new PmtRecipeWeightManager();
        /// <summary>
        /// ��������
        /// �ﱾǿ @ 2013-04-03 12:46:30
        /// </summary>
        private IPmtWeightActionManager pmtWeightActionManager = new PmtWeightActionManager();
        /// <summary>
        /// �ϲ���Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:30
        /// </summary>
        private IPmtEquipJarStoreManager pmtEquipJarStoreManager = new PmtEquipJarStoreManager();

        /// <summary>
        /// ������Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:30
        /// </summary>
        private IPmtRecipeMixingManager mixingManager = new PmtRecipeMixingManager();

        /// <summary>
        /// ������Ϣ
        /// Ԭ�� @ 2013-04-03 12:46:30
        /// </summary>
        private IPmtRecipeOpenMixingManager openManager = new PmtRecipeOpenMixingManager();
        /// <summary>
        /// Ԥ��ɢ��Ϣ
        /// ��־�� @ 2015-01-05 12:46:30
        /// </summary>
        private IPmtRecipeWeightMidManager WeightMidManager = new PmtRecipeWeightMidManager();
        private IPmtPMILLMainManager PMILLMainManager = new PmtPMILLMainManager();
        private IPmtSMILLMainManager SMILLMainManager = new PmtSMILLMainManager();
        private IPmtCoolMILLMainManager CoolMILLMainManager = new PmtCoolMILLMainManager();
        /// <summary>
        /// ��������
        /// �ﱾǿ @ 2013-04-03 12:46:30
        /// </summary>
        private IPmtActionManager pmtActionManager = new PmtActionManager();
        /// <summary>
        /// ��������
        /// �ﱾǿ @ 2013-04-03 12:46:30
        /// </summary>
        private IPmtOpenActionManager pmtOpenActionManager = new PmtOpenActionManager();
        /// <summary>
        /// ��������
        /// �ﱾǿ @ 2013-04-03 12:46:31
        /// </summary>
        private IPmtTermManager pmtTermManager = new PmtTermManager();
        #endregion
        #region ������������
        /// <summary>
        /// RecipeInfo ʵ����
        /// �ﱾǿ @ 2013-04-03 12:46:31
        /// </summary>
        /// <remarks></remarks>
        private class RecipeInfo
        {
            /// <summary>
            /// �䷽��Ϣ
            /// �ﱾǿ @ 2013-04-03 12:46:31
            /// </summary>
            /// <value>The PMT recipe.</value>
            /// <remarks></remarks>
            public PmtRecipe PmtRecipe { get; set; }
            /// <summary>
            /// ������Ϣ
            /// �ﱾǿ @ 2013-04-03 12:46:31
            /// </summary>
            /// <value>The bas material.</value>
            /// <remarks></remarks>
            public BasMaterial BasMaterial { get; set; }
            /// <summary>
            /// ��̨��Ϣ
            /// �ﱾǿ @ 2013-04-03 12:46:32
            /// </summary>
            /// <value>The bas equip.</value>
            /// <remarks></remarks>
            public BasEquip BasEquip { get; set; }
            /// <summary>
            /// �ϲ���Ϣ
            /// �ﱾǿ @ 2013-04-03 12:46:32
            /// </summary>
            /// <value>The PMT equip jar store.</value>
            /// <remarks></remarks>
            public EntityArrayList<PmtEquipJarStore> PmtEquipJarStore { get; set; }
        }
        /// <summary>
        /// RecipeMixingInfo ʵ����
        /// �ﱾǿ @ 2013-04-03 12:46:32
        /// </summary>
        /// <remarks></remarks>
        private class RecipeMixingInfo
        {
            /// <summary>
            /// ������Ϣ
            /// �ﱾǿ @ 2013-04-03 12:46:32
            /// </summary>
            /// <value>The PMT recipe mixing.</value>
            /// <remarks></remarks>
            public PmtRecipeMixing PmtRecipeMixing { get; set; }
            /// <summary>
            /// ��������
            /// �ﱾǿ @ 2013-04-03 12:46:32
            /// </summary>
            /// <value>The PMT term.</value>
            /// <remarks></remarks>
            public PmtTerm PmtTerm { get; set; }
            /// <summary>
            /// ��������
            /// �ﱾǿ @ 2013-04-03 12:46:32
            /// </summary>
            /// <value>The PMT action.</value>
            /// <remarks></remarks>
            public PmtAction PmtAction { get; set; }
        }
        /// <summary>
        /// RecipeOpenMixingInfo ʵ����
        /// Ԭ�� @ 2013-04-03 12:46:32
        /// </summary>
        /// <remarks></remarks>
        private class RecipeOpenMixingInfo
        {
            /// <summary>
            /// ������Ϣ
            /// Ԭ�� @ 2013-04-03 12:46:32
            /// </summary>
            /// <value>The PMT recipe mixing.</value>
            /// <remarks></remarks>
            public PmtRecipeOpenMixing PmtRecipeOpenMixing { get; set; }
            /// <summary>
            /// ��������
            /// Ԭ�� @ 2013-04-03 12:46:32
            /// </summary>
            /// <value>The PMT action.</value>
            /// <remarks></remarks>
            public PmtOpenAction PmtOpenAction { get; set; }
        }
        /// <summary>
        /// RecipeWeightInfo ʵ����
        /// �ﱾǿ @ 2013-04-03 12:46:32
        /// </summary>
        /// <remarks></remarks>
        private class RecipeWeightInfo
        {
            /// <summary>
            /// ������Ϣ
            /// �ﱾǿ @ 2013-04-03 12:46:33
            /// </summary>
            /// <value>The PMT recipe weight.</value>
            /// <remarks></remarks>
            public PmtRecipeWeight PmtRecipeWeight { get; set; }
            /// <summary>
            /// ������Ϣ
            /// �ﱾǿ @ 2013-04-03 12:46:33
            /// </summary>
            /// <value>The bas material.</value>
            /// <remarks></remarks>
            public BasMaterial BasMaterial { get; set; }
            /// <summary>
            /// ��������
            /// �ﱾǿ @ 2013-04-03 12:46:33
            /// </summary>
            /// <value>The PMT weight action.</value>
            /// <remarks></remarks>
            public PmtWeightAction PmtWeightAction { get; set; }
        }
        /// <summary>
        /// ��������
        /// �ﱾǿ @ 2013-04-03 12:46:33
        /// </summary>
        /// <remarks></remarks>
        private enum WeightType
        {
            /// <summary>
            /// 
            /// </summary>
            ̿�ڳ�����Ϣ = 0,
            /// <summary>
            /// 
            /// </summary>
            ��1������Ϣ = 1,
            /// <summary>
            /// 
            /// </summary>
            ���ϳ�����Ϣ = 2,
            /// <summary>
            /// 
            /// </summary>
            С��У�˳�����Ϣ = 3,
            /// <summary>
            /// 
            /// </summary>
            ���ϳ�����Ϣ = 4,
            /// <summary>
            /// 
            /// </summary>
            ��2������Ϣ = 5,
            /// <summary>
            /// 
            /// </summary>
            С�ϳ�����Ϣ = 9,
        }
        /// <summary>
        /// ����״̬
        /// �ﱾǿ @ 2013-04-03 12:46:34
        /// </summary>
        /// <remarks></remarks>
        private enum PmtState
        {
            /// <summary>
            /// 
            /// </summary>
            ���� = 0,
            /// <summary>
            /// 
            /// </summary>
            ���� = 1,
            /// <summary>
            /// 
            /// </summary>
            ���� = 2,
        }
        /// <summary>
        /// ��������
        /// �ﱾǿ @ 2013-04-03 12:46:35
        /// </summary>
        /// <remarks></remarks>
        private enum WeightAction
        {
            /// <summary>
            /// 
            /// </summary>
            ���� = 0,
            /// <summary>
            /// 
            /// </summary>
            �Ƶ� = 1,
            /// <summary>
            /// 
            /// </summary>
            ж�� = 2,
        }
        /// <summary>
        /// ���ϴ���
        /// �ﱾǿ @ 2013-04-03 12:46:36
        /// </summary>
        /// <remarks></remarks>
        private enum MajorType
        {
            /// <summary>
            /// 
            /// </summary>
            ԭ���� = 1,
            /// <summary>
            /// 
            /// </summary>
            С�� = 2,
            /// <summary>
            /// 
            /// </summary>
            �� = 3,
        }
        /// <summary>
        /// ��̨����
        /// �ﱾǿ @ 2013-04-03 12:46:36
        /// </summary>
        /// <remarks></remarks>
        private enum EquipType
        {
            /// <summary>
            /// 
            /// </summary>
            ������ = 1,
            /// <summary>
            /// 
            /// </summary>
            С�ϳ� = 2,
            /// <summary>
            /// 
            /// </summary>
            ̿������ = 3,
            /// <summary>
            /// 
            /// </summary>
            �⹩��̨ = 4,
            /// <summary>
            /// 
            /// </summary>
            �����豸 = 5,
        }
        /// <summary>
        /// ��������
        /// �ﱾǿ @ 2013-04-03 12:46:37
        /// </summary>
        /// <remarks></remarks>
        private enum MixingAction
        {
            /// <summary>
            /// 
            /// </summary>
            �ӽ��� = 1,
            /// <summary>
            /// 
            /// </summary>
            ��̿�� = 2,
            /// <summary>
            /// 
            /// </summary>
            ������ = 3,
            /// <summary>
            /// 
            /// </summary>
            �ӷ��� = 3,
            ѹ�϶�˨ = 4,
            ��ж���� = 5,
            ���� = 6,
            ���϶�˨�ϵ�λ = 7,
            ���϶�˨�е�λ = 7,
            �϶�˨���� = 9,
            �������� = 10,
            �ؼ����� = 11,
            /// <summary>
            /// 
            /// </summary>
            ��С�� = 12,
            ��ж���� = 13,
            �������� = 14,
            ��˨��ж���� = 15,
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
            //�϶�˨���� = 14,
            ///// <summary>
            ///// 
            ///// </summary>
            //������ʱ = 15,
            ///// <summary>
            ///// 
            ///// </summary>
            //������ʼ�� = 16,
            ///// <summary>
            ///// 
            ///// </summary>
            //������2 = 17,
        }
        #endregion
        #region ��ʼ������
        /// <summary>
        /// ��ȡ�䷽�汾��
        /// �ﱾǿ @ 2013-04-03 12:46:40
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
        /// ��ȡ��Ϣ����
        /// �ﱾǿ @ 2013-04-03 12:46:40
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
        /// ��ʼ���䷽��Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:40
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
                return "������Ϣ����Ϊ��";
            }
            if (pmtRecipe.ShelfLotCount == null || (int)pmtRecipe.ShelfLotCount <= 0)
            {
                return "ÿ�ܳ�������Ϊ��";
            }
            if (string.IsNullOrWhiteSpace(pmtRecipe.RecipeEquipCode))
            {
                return "��̨��Ϣ����Ϊ��";
            }
            EntityArrayList<BasMaterial> basMaterialList = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == pmtRecipe.RecipeMaterialCode);
            if (basMaterialList.Count == 0)
            {
                return "������Ϣ������";
            }
            EntityArrayList<BasEquip> basEquipList = basEquipManager.GetListByWhere(BasEquip._.EquipCode == pmtRecipe.RecipeEquipCode);
            if (basMaterialList.Count == 0)
            {
                return "��̨��Ϣ������";
            }
            if (pmtRecipe.RecipeType == null)
            {
                return "�������䷽����";
            }
            recipeInfo.BasMaterial = basMaterialList[0];
            recipeInfo.BasEquip = basEquipList[0];
            //    recipeInfo.PmtEquipJarStore = pmtEquipJarStoreManager.GetListByWhere(PmtEquipJarStore._.EquipCode == pmtRecipe.RecipeEquipCode);
            return Result;
        }
        /// <summary>
        /// ��ʼ��������Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:41
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
                    return "��" + i.ToString() + "����Ҫ���ӻ���������";
                }
                if (string.IsNullOrWhiteSpace(mixing.ActionCode))
                {
                    recipeMixingInfo.Add(new RecipeMixingInfo());
                    continue;
                }
                EntityArrayList<PmtAction> pmtActionList = pmtActionManager.GetListByWhere(PmtAction._.ActionCode == mixing.ActionCode);
                if (pmtActionList.Count == 0)
                {
                    return "�������������ڣ�" + mixing.ActionCode;
                }
                info.PmtAction = pmtActionList[0];
                if (!string.IsNullOrWhiteSpace(mixing.TermCode))
                {
                    EntityArrayList<PmtTerm> pmtTermList = pmtTermManager.GetListByWhere(PmtTerm._.TermCode == mixing.TermCode);
                    if (pmtTermList.Count == 0)
                    {
                        return "�������������ڣ�" + mixing.TermCode;
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
        /// ��ʼ��������Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:41
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
                    return "��" + i.ToString() + "����Ҫ���ӿ���������";
                }
                if (string.IsNullOrWhiteSpace(mixing.OpenActionCode))
                {
                    recipeOpenMixingInfo.Add(new RecipeOpenMixingInfo());
                    continue;
                }
                EntityArrayList<PmtOpenAction> pmtOpenActionList = pmtOpenActionManager.GetListByWhere(PmtOpenAction._.ActionCode == mixing.OpenActionCode);
                if (pmtOpenActionList.Count == 0)
                {
                    return "��������������";
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
        /// ��ʼ��������Ϣ
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
                //if (string.IsNullOrEmpty(weight.MaterialCode))  ���һ���յ�ж��
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
                    return "��������������";
                }

                if ((!string.IsNullOrEmpty(weight.RecipeMaterialCode)) && (!string.IsNullOrEmpty(weight.MaterialCode)))
                {
                    if (weight.RecipeMaterialCode.Substring(0, 1) == "5" && weight.MaterialCode.Substring(0, 1) == "2")//�ս��䷽���������
                    {
                        if (!string.IsNullOrEmpty(weight.CheckWeight.ToString()))
                        {
                            if (weight.CheckWeight != 0)
                            {
                                if (string.IsNullOrEmpty(weight.CheckError.ToString()))
                                {
                                    return "������" + weight.MaterialName + "�������";
                                }
                                if (weight.CheckError == 0)
                                {

                                    return "������" + weight.MaterialName + "�������";

                                }

                            }

                        }
                    }
                }

                info.PmtWeightAction = pmtWeightActionList[0];
                if (weight.ActCode.Trim() == ((int)WeightAction.ж��).ToString())
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
        #region У������
        /// <summary>
        /// ����������ж��
        /// �ﱾǿ @ 2013-04-03 12:46:42
        /// </summary>
        /// <param name="weightAction">The weight action.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool WeightActionIsж��(string weightAction)
        {
            if (string.IsNullOrWhiteSpace(weightAction))
            {
                return false;
            }
            if (weightAction.Trim() == ((int)WeightAction.ж��).ToString())
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// �����Ƿ���Ҫж��
        /// �ﱾǿ @ 2013-04-03 12:46:42
        /// </summary>
        /// <param name="weightType">Type of the weight.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool WeightTypeNeedж��(string weightType)
        {
            if (string.IsNullOrWhiteSpace(weightType))
            {
                return false;
            }
            if (weightType.Trim() == ((int)WeightType.̿�ڳ�����Ϣ).ToString())
            {
                return true;
            }
            if (weightType.Trim() == ((int)WeightType.��1������Ϣ).ToString())
            {
                return true;
            }
            if (weightType.Trim() == ((int)WeightType.��2������Ϣ).ToString())
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// У��������Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:42
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
                    return "����¶Ȳ���Ϊ���ұ������0";
                }
                if ((pmtRecipe.RollTemp == null) || ((int)pmtRecipe.RollTemp) <= 0)
                {
                    return "ж�����¶Ȳ���Ϊ���ұ������0";
                }
                if ((pmtRecipe.DdoorTemp == null) || ((int)pmtRecipe.DdoorTemp) <= 0)
                {
                    return "ת���¶Ȳ���Ϊ���ұ������0";
                }
            }

            if (string.IsNullOrWhiteSpace(pmtRecipe.RecipeMaterialCode))
            {
                return "������Ϣ����Ϊ��";
            }
            EntityArrayList<BasMaterial> basMaterialList = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == pmtRecipe.RecipeMaterialCode);
            if (basMaterialList.Count == 0)
            {
                return "������Ϣ������";
            }

            BasMaterial m = basMaterialList[0];
            if (m.MajorTypeID.ToString().Trim() == ((int)MajorType.С��).ToString())
            {
                isMixing = false;
            }
            if (recipeWeightInfo.Count == 0)
            {
                return "������ڳ�����Ϣ";
            }
            if (isMixing)
            {
                if (recipeMixingInfo.Count == 0)
                {
                    return "������ڻ�����Ϣ";
                }
            }
            if (string.IsNullOrWhiteSpace(pmtRecipe.RecipeEquipCode))
            {
                return "��̨��Ϣ����Ϊ��";
            }
            if (pmtRecipe.RecipeType == null)
            {
                return "�䷽���Ͳ���Ϊ��";
            }
            if (isMixing)
            {
                if ((pmtRecipe.OverTimeSetTime == null) || ((int)pmtRecipe.OverTimeSetTime) <= 0)
                {
                    return "��ʱ�Ž�ʱ�䲻��Ϊ��<br />�ұ������0";
                }
                if ((pmtRecipe.OverTempSetTemp == null) || ((int)pmtRecipe.OverTempSetTemp) <= 0)
                {
                    return "�����Ž��¶Ȳ���Ϊ��<br />�ұ������0";
                }
                if ((pmtRecipe.OverTempMinTime == null) || ((int)pmtRecipe.OverTempMinTime) <= 0)
                {
                    return "�����Ž����ʱ�䲻��Ϊ��<br />�ұ������0";
                }
                if ((pmtRecipe.OverTempMinTime == null) || ((int)pmtRecipe.OverTempMinTime) <= 0)
                {
                    return "�����Ž����ʱ�䲻��Ϊ��<br />�ұ������0";
                }
            }
            EntityArrayList<BasEquip> basEquipList = basEquipManager.GetListByWhere(BasEquip._.EquipCode == pmtRecipe.RecipeEquipCode);
            if (basMaterialList.Count == 0)
            {
                return "��̨��Ϣ������";
            }
            BasEquip e = basEquipList[0];
            if (pmtRecipe.RecipeState == null)
            {
                return "�䷽״̬����Ϊ��";
            } if (pmtRecipe.RecipeState.Trim() == ((int)PmtState.����).ToString())
            {
                EntityArrayList<PmtRecipe> lst = this.GetListByWhere(PmtRecipe._.RecipeEquipCode == pmtRecipe.RecipeEquipCode
                    && PmtRecipe._.RecipeMaterialCode == pmtRecipe.RecipeMaterialCode
                    && PmtRecipe._.RecipeState == ((int)PmtState.����).ToString()
                    && PmtRecipe._.RecipeType == pmtRecipe.RecipeType);
                if (lst.Count > 0)
                {
                    bool isHas���� = false;
                    foreach (PmtRecipe m���� in lst)
                    {
                        if (m����.RecipeVersionID != pmtRecipe.RecipeVersionID)
                        {
                            isHas���� = true;
                            break;
                        }
                    }
                    if (isHas����)
                    {
                        return "," + pmtRecipe.RecipeMaterialCode + "," + pmtRecipe.RecipeEquipCode + "," + pmtRecipe.RecipeType + "," + "��ǰ���������䷽���������������䷽��" + "," + pmtRecipe.R_Version;
                    }
                }
                //if (string.IsNullOrWhiteSpace(pmtRecipe.CanAuditUser))
                //{
                //    return "�䷽״̬Ϊ���ã��������������";
                //}
            }
            if ((pmtRecipe.InPolyMaxTemp == null) || ((int)pmtRecipe.InPolyMaxTemp) <= 0)
            {
                pmtRecipe.InPolyMaxTemp = 200;
            }
            pmtRecipe.RecipeMaterialName = m.MaterialName;
            if ((m.MajorTypeID != null) && (m.MajorTypeID == (int)MajorType.С��))
            {
                if (!(Convert.ToInt32(e.EquipType) == (int)EquipType.С�ϳ�))
                {

                    return "������ѡ����ȷѡ����ȷ�Ļ�̨";
                }
            }
            else
            {
                if (!(Convert.ToInt32(e.EquipType) == (int)EquipType.������))
                {
                    return "������ѡ����ȷѡ����ȷ�Ļ�̨";
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
                    //�����䷽��ȷλ��������
                    if (info.PmtRecipeWeight.WeightType == "0")//̿��
                    { dWeight += decimal.Round(Convert.ToDecimal(info.PmtRecipeWeight.SetWeight), 3); }

                    else if (info.PmtRecipeWeight.WeightType == "1" || info.PmtRecipeWeight.WeightType == "5")//��
                    { dWeight += decimal.Round(Convert.ToDecimal(info.PmtRecipeWeight.SetWeight), 3); }

                    else if (info.PmtRecipeWeight.WeightType == "2")//����
                    { dWeight += decimal.Round(Convert.ToDecimal(info.PmtRecipeWeight.SetWeight), 3); }


                    else if (info.PmtRecipeWeight.WeightType == "3")//С��
                    { dWeight += decimal.Round(Convert.ToDecimal(info.PmtRecipeWeight.SetWeight), 3); }
                    else { dWeight += decimal.Round(Convert.ToDecimal(info.PmtRecipeWeight.SetWeight), 3); }
                }

            }

            pmtRecipe.LotTotalWeight = decimal.Round(Convert.ToDecimal(dWeight), 3); ;
            return string.Empty;
        }
        /// <summary>
        /// У�������Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:43
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
            if (recipeInfo.BasMaterial.MajorTypeID.ToString().Trim() == ((int)MajorType.С��).ToString())
            {
                isMixing = false;
            }
            if (!isMixing)
            {
                return string.Empty;
            }
            #region ��һ��
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
                return "������һ��ת�ٲ���Ϊ��";
            }
            if ((thisMixing.MixingPress == null) || (thisMixing.MixingPress == 0))
            {
                return "������һ��ѹ������Ϊ��";
            }
            //if ((thisMixing.MixingPress > (decimal)0.6) || (thisMixing.MixingPress < (decimal)0.1))
            //{
            //    return "������һ��ѹ������0.1 - 0.6֮��";
            //}
            #endregion
            #region У�鲽��
            for (int i = 0; i < recipeMixingInfo.Count; i++)    // ��PmtTerm�� �����ֶ��޷��ж�
            {
                RecipeMixingInfo info = recipeMixingInfo[i];
                if (info.PmtRecipeMixing == null)
                {
                    continue;
                }
                if (info.PmtRecipeMixing.ActionCode == "4" && recipeMixingInfo[i - 1].PmtRecipeMixing.ActionCode != "6")
                {
                    return "��[" + (i + 1).ToString() + "]�����������Ϊ��ж�������[" + (i).ToString() + "]������Ϊ���϶�˨";
                }
                if (info.PmtRecipeMixing.ActionCode == "13" && recipeMixingInfo[i - 1].PmtRecipeMixing.ActionCode == "2")
                {
                    return "��[" + (i + 1).ToString() + "]�����������Ϊ��С�����[" + (i).ToString() + "]������Ϊ��̿��";
                }
                if ((info.PmtTerm != null) && (info.PmtTerm.ShowName.Contains("�¶�"))
                    && ((info.PmtRecipeMixing.MixingTemp == null) || (info.PmtRecipeMixing.MixingTemp <= 0)))
                {
                    return "��[" + (i + 1).ToString() + "]������������¶�";
                }
                if ((info.PmtTerm != null) && (info.PmtTerm.ShowName.Contains("�¶�"))
                     && ((info.PmtRecipeMixing.MixingTemp == null) || (info.PmtRecipeMixing.MixingTemp > 200)))
                {
                    return "��[" + (i + 1).ToString() + "]���������¶ȵ��¶Ȳ��ܴ���200";
                }
                if ((info.PmtRecipeMixing.MixingTemp != null) && (info.PmtRecipeMixing.MixingTemp > 0)
                    && ((info.PmtTerm == null) || (!info.PmtTerm.ShowName.Contains("�¶�"))))
                {
                    return "��[" + (i + 1).ToString() + "]���������¶�,���������¶��������";
                }
                if ((info.PmtAction != null) && (info.PmtAction.ActionCode == ((int)MixingAction.����).ToString())
                    && ((info.PmtRecipeMixing.MixingTime == null) || (info.PmtRecipeMixing.MixingTime <= 0)))
                {
                    return "��[" + (i + 1).ToString() + "]��Ϊ���֣���������ʱ��";
                }
                if ((info.PmtTerm != null) && (info.PmtTerm.ShowName.Contains("����"))
                    && ((info.PmtRecipeMixing.MixingPower == null) || (info.PmtRecipeMixing.MixingPower <= 0)))
                {
                    return "��[" + (i + 1).ToString() + "]���������ù���";
                }
                if ((info.PmtRecipeMixing.MixingPower != null) && (info.PmtRecipeMixing.MixingPower > 0)
                    && ((info.PmtTerm == null) || (!info.PmtTerm.ShowName.Contains("����"))))
                {
                    return "��[" + (i + 1).ToString() + "]�������˹���,�������ӹ����������";
                }
                if ((info.PmtTerm != null) && (info.PmtTerm.ShowName.Contains("����"))
                    && ((info.PmtRecipeMixing.MixingEnergy == null) || (info.PmtRecipeMixing.MixingEnergy <= 0)))
                {
                    return "��[" + (i + 1).ToString() + "]��������������";
                }
                if ((info.PmtRecipeMixing.MixingEnergy != null) && (info.PmtRecipeMixing.MixingEnergy > 0)
                    && ((info.PmtTerm == null) || (!info.PmtTerm.ShowName.Contains("����"))))
                {
                    return "��[" + (i + 1).ToString() + "]������������,�������������������";
                }
            }
            #endregion
            #region ���ͻ�̿�ڴ���
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
            #region �����䷽����

            int icount = 0;


            foreach (RecipeMixingInfo info in recipeMixingInfo)
            {
                PmtRecipeMixing mixing = info.PmtRecipeMixing;
                if (mixing == null)
                {
                    continue;
                }
                if ((!string.IsNullOrWhiteSpace(mixing.ActionCode)) && (mixing.ActionCode.Trim() == ((int)MixingAction.��С��).ToString())) //��̿�� 2019-01-16 �������������ͽ�������Ϊ̿�� �� ���� ����������С�ϣ�ĸ�����޷���ϸ���֣��ʽ��˶δ�����ʱע��
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
                    && weight.WeightType.Trim() == ((int)WeightType.С��У�˳�����Ϣ).ToString())
                {
                    if (icount != 0)
                    {
                        icount = 0;
                    }
                }
            }
            //if (icount != 0)
            //{
            //    //return "С�ϼ��Ϻ�С�ϳƳ�����һ�£�";
            //}

            foreach (RecipeMixingInfo info in recipeMixingInfo)
            {
                PmtRecipeMixing mixing = info.PmtRecipeMixing;
                if (mixing == null)
                {
                    continue;
                }
                if ((!string.IsNullOrWhiteSpace(mixing.ActionCode)) && (mixing.ActionCode.Trim() == ((int)MixingAction.��̿��).ToString())) //��̿��
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
                    && weight.WeightType.Trim() == ((int)WeightType.̿�ڳ�����Ϣ).ToString()
                    && (isFromPDM ||
                       (!string.IsNullOrWhiteSpace(weight.ActCode) && weight.ActCode.Trim() == ((int)WeightAction.ж��).ToString())
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
                return "̿�ڼ��Ϻ�ж�ϲ�һ��";
            }
            foreach (RecipeMixingInfo info in recipeMixingInfo)
            {
                PmtRecipeMixing mixing = info.PmtRecipeMixing;
                if (mixing == null)
                {
                    continue;
                }
                if ((!string.IsNullOrWhiteSpace(mixing.ActionCode)) && (mixing.ActionCode.Trim() == ((int)MixingAction.������).ToString())) //����
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
                    && weight.WeightType.Trim() == ((int)WeightType.��1������Ϣ).ToString()
                    &&
                       (!string.IsNullOrWhiteSpace(weight.ActCode) && weight.ActCode.Trim() == ((int)WeightAction.ж��).ToString())
                    )
                {
                    icount--;
                }
            }
            if (icount != 0)
            {
                return "����1���Ϻ�ж�ϲ�һ��";
            }
            foreach (RecipeMixingInfo info in recipeMixingInfo)
            {
                PmtRecipeMixing mixing = info.PmtRecipeMixing;
                if (mixing == null)
                {
                    continue;
                }
                //if ((!string.IsNullOrWhiteSpace(mixing.ActionCode)) && (mixing.ActionCode.Trim() == ((int)MixingAction.������2).ToString())) //����
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
                    && weight.WeightType.Trim() == ((int)WeightType.��2������Ϣ).ToString()
                     &&
                        (!string.IsNullOrWhiteSpace(weight.ActCode) && weight.ActCode.Trim() == ((int)WeightAction.ж��).ToString())
                    )
                {
                    icount--;
                }
            }
            if (icount != 0)
            {
                return "����2���Ϻ�ж�ϲ�һ��";
            }
            #endregion

            #endregion
            #region ���ϲ���
            bool isHas = false;
            foreach (RecipeMixingInfo info in recipeMixingInfo)
            {
                if (info.PmtRecipeMixing == null)
                {
                    continue;
                }
                if (info.PmtRecipeMixing.ActionCode.Trim() == ((int)MixingAction.�ӽ���).ToString())
                {
                    isHas = true;
                    break;
                }
            }
            if (!isHas)
            {
                return "������ڼӽ��ϵĻ�������";
            }
            isHas = false;
            foreach (RecipeMixingInfo info in recipeMixingInfo)
            {
                if (info.PmtRecipeMixing == null)
                {
                    continue;
                }
                if (info.PmtRecipeMixing.ActionCode.Trim() == ((int)MixingAction.��ж����).ToString())
                {
                    isHas = true;
                    break;
                }
            }
            if (!isHas)
            {
                return "������ڿ�ж���ŵĻ�������";
            }
            #endregion
            return string.Empty;
        }
        /// <summary>
        /// У�鿪����Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:43
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <param name="recipeMixingInfo">The recipe mixing info.</param>
        /// <param name="recipeWeightInfo">The recipe weight info.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private string ValidityPmtRecipeOpenMixing(RecipeInfo recipeInfo, List<RecipeOpenMixingInfo> recipeOpenMixingInfo, List<RecipeWeightInfo> recipeWeightInfo)
        {
            return string.Empty;//yanzx 2015.11.18 ����֤������Ϣ
            if (recipeOpenMixingInfo.Count == 0)
            {
                return string.Empty;
            }
            bool isMixing = true;
            if (recipeInfo.BasMaterial.MajorTypeID.ToString().Trim() == ((int)MajorType.С��).ToString())
            {
                isMixing = false;
            }
            if (!isMixing)
            {
                return string.Empty;
            }
            #region ��һ��
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
                return "������һ������ʱ�䲻��Ϊ��";
            }
            if ((thisOpenMixing.CoolMixSpeed == null) || (thisOpenMixing.CoolMixSpeed == 0))
            {
                return "������һ����ȴ���ٶȲ���Ϊ��";
            }
            if ((thisOpenMixing.OpenMixSpeed == null) || (thisOpenMixing.OpenMixSpeed == 0))
            {
                return "������һ���������ٶȲ���Ϊ��";
            }
            if ((thisOpenMixing.MixRollor == null) || (thisOpenMixing.MixRollor == 0))
            {
                return "������һ�����಻��Ϊ��";
            }
            if ((thisOpenMixing.WaterTemp == null) || (thisOpenMixing.WaterTemp == 0))
            {
                return "������һ��ˮ�²���Ϊ��";
            }
            if ((thisOpenMixing.RubberTemp == null) || (thisOpenMixing.RubberTemp == 0))
            {
                return "������һ�����²���Ϊ��";
            }
            if ((thisOpenMixing.CarSpeed == null) || (thisOpenMixing.CarSpeed == 0))
            {
                return "������һ��С���ٶȲ���Ϊ��";
            }
            #endregion

            #region У�鲽��
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
                        return "������" + i + "������ʱ�䲻��Ϊ��";
                    }
                    if ((info.PmtRecipeOpenMixing.CoolMixSpeed == null) || (info.PmtRecipeOpenMixing.CoolMixSpeed == 0))
                    {
                        return "������" + i + "����ȴ���ٶȲ���Ϊ��";
                    }
                    if ((info.PmtRecipeOpenMixing.OpenMixSpeed == null) || (info.PmtRecipeOpenMixing.OpenMixSpeed == 0))
                    {
                        return "������" + i + "���������ٶȲ���Ϊ��";
                    }
                    if ((info.PmtRecipeOpenMixing.MixRollor == null) || (info.PmtRecipeOpenMixing.MixRollor == 0))
                    {
                        return "������" + i + "�����಻��Ϊ��";
                    }
                    if ((info.PmtRecipeOpenMixing.WaterTemp == null) || (info.PmtRecipeOpenMixing.WaterTemp == 0))
                    {
                        return "������" + i + "��ˮ�²���Ϊ��";
                    }
                    if ((info.PmtRecipeOpenMixing.RubberTemp == null) || (info.PmtRecipeOpenMixing.RubberTemp == 0))
                    {
                        return "������" + i + "�����²���Ϊ��";
                    }
                    if ((info.PmtRecipeOpenMixing.CarSpeed == null) || (info.PmtRecipeOpenMixing.CarSpeed == 0))
                    {
                        return "������" + i + "��С���ٶȲ���Ϊ��";
                    }
                }
            }
            #endregion
            return string.Empty;
        }
        /// <summary>
        /// У���䷽��Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:43
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
            if (recipeInfo.BasMaterial.MajorTypeID.ToString().Trim() == ((int)MajorType.С��).ToString())
            {
                isMixing = false;
            }
            #region ��һ��  ���һ��
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
                if (WeightTypeNeedж��(thisWeight.WeightType.Trim()) && WeightActionIsж��(thisWeight.ActCode))
                {
                    return ((WeightType)(Convert.ToInt32(thisWeight.WeightType.Trim()))).ToString() + "����ǰ������[ж��]";
                }
                thisWeight = infoLast.PmtRecipeWeight;
                if (WeightTypeNeedж��(thisWeight.WeightType.Trim()) && !WeightActionIsж��(thisWeight.ActCode))
                {
                    return ((WeightType)(Convert.ToInt32(thisWeight.WeightType.Trim()))).ToString() + "�����������һ����������Ϊ[ж��]";
                }
                if (WeightTypeNeedж��(thisWeight.WeightType.Trim()))
                {
                    infoLast.BasMaterial = null;
                    thisWeight.MaterialCode = "";
                    thisWeight.MaterialName = "";
                    thisWeight.ErrorAllow = 0;
                    thisWeight.SetWeight = 0;
                }
            }
            #endregion
            #region ����У��
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
                #region ����
                if ((weight.SetWeight == null) ||
                    (weight.ErrorAllow == null) ||
                    (weight.SetWeight == 0) ||
                    (weight.ErrorAllow == 0))
                {
                    return ((WeightType)Convert.ToInt32(weight.WeightType.Trim())).ToString() + "��[" + weight.MaterialName + "]������Ϣ���趨<br/>�������������������0";
                }
                #endregion
                if (!isMixing)
                {
                    isHasRub = true;
                    continue;
                }
                if (weight.WeightType.Trim() == ((int)WeightType.���ϳ�����Ϣ).ToString())
                {
                    isHasRub = true;
                }
                #region �ϲ�У��
                BasMaterial material = info.BasMaterial;
                if ((material.MajorTypeID != null)
                    && (material.MajorTypeID == 1)   //ԭ����
                    && ((weight.WeightType == ((int)WeightType.̿�ڳ�����Ϣ).ToString().Trim())
                       || (weight.WeightType == ((int)WeightType.��1������Ϣ).ToString().Trim())
                       || (weight.WeightType == ((int)WeightType.��2������Ϣ).ToString().Trim())))
                {
                    //�ϲ���֤
                    //EntityArrayList<PmtEquipJarStore> pmtEquipJarStoreList =
                    //    pmtEquipJarStoreManager.GetListByWhere(PmtEquipJarStore._.MaterialCode == weight.MaterialCode
                    //    && PmtEquipJarStore._.EquipCode == recipeInfo.PmtRecipe.RecipeEquipCode);
                    //if (pmtEquipJarStoreList.Count <= 0)
                    //{
                    //    return recipeInfo.BasEquip.EquipName + "������[" + weight.MaterialName + "]���ϲֹޣ�";
                    //}
                }
                #endregion
                #region ��С��
                if (weight.WeightType == ((int)WeightType.С��У�˳�����Ϣ).ToString().Trim())  //̿��
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
                        return "������Ϣ�д���С�ϳƳ�����<br />�����ڻ�����Ϣ������[��С��]������";
                    }
                }
                #endregion
                #region ��̿�ڻ���
                if (weight.WeightType == ((int)WeightType.̿�ڳ�����Ϣ).ToString().Trim())  //̿��  2019-1-2 �����޴��ֶ�(WeightType) ����A��
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
                        return "������Ϣ�д���̿�����ϣ�<br />�����ڻ�����Ϣ������[��̿��]������";
                    }
                }

                if (weight.WeightType == ((int)WeightType.��1������Ϣ).ToString().Trim()) //��1
                {
                    bool isHaveMixing = false;
                    foreach (RecipeMixingInfo mixing in recipeMixingInfo)
                    {
                        if (mixing.PmtRecipeMixing == null)
                        {
                            continue;
                        }
                        if ((!string.IsNullOrWhiteSpace(mixing.PmtRecipeMixing.ActionCode)) && (mixing.PmtRecipeMixing.ActionCode.Trim() == ((int)MixingAction.������).ToString()))
                        {
                            isHaveMixing = true;
                            break;
                        }
                    }
                    if (!isHaveMixing)
                    {
                        return "������Ϣ�д����ͳƣ�1��������Ϣ��<BR />�����ڻ�����Ϣ������[������]������";
                    }
                }
                //if (weight.WeightType == ((int)WeightType.��2������Ϣ).ToString().Trim()) //��2
                //{
                //    bool isHaveMixing = false;
                //    foreach (RecipeMixingInfo mixing in recipeMixingInfo)
                //    {
                //        if (mixing.PmtRecipeMixing == null)
                //        {
                //            continue;
                //        }
                //        if ((!string.IsNullOrWhiteSpace(mixing.PmtRecipeMixing.ActionCode)) && (mixing.PmtRecipeMixing.ActionCode.Trim() == ((int)MixingAction.������2).ToString()))
                //        {
                //            isHaveMixing = true;
                //            break;
                //        }
                //    }
                //    if (!isHaveMixing)
                //    {
                //        return "������Ϣ�д����ͳƣ�2��������Ϣ��<br />�����ڻ�����Ϣ������[������2]������";
                //    }
                //}
                #endregion
            }
            if ((isMixing) && (!isHasRub))
            {
                return "������Ϣ�б���������ϣ�";
            }
            #endregion
            return string.Empty;
        }
        /// <summary>
        /// �洢������Ϣ��У��������Ϣ����
        /// �ﱾǿ @ 2013-04-03 12:46:42
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
            #region ��ʼ������
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

            #region У������
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
            //        return materialName + "�䷽���ر������140kg����";
            //    }
            //}
            #endregion
            #region ��������
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
                    //info.PmtRecipeOpenMixing.MixingStep = step; //yanzx �����߼�
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
        /// У��������Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:42
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
                return "�䷽У��ʧ�ܣ�" + ex.Message.ToString();
            }
        }
        #region У��������ͬ
        /// <summary>
        /// �Ա�����
        /// �ﱾǿ @ 2013-04-03 12:46:44
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
        /// �Ա��䷽
        /// �ﱾǿ @ 2013-04-03 12:46:44
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
        #region ��������
        /// <summary>
        /// �����䷽��Ϣ�ɹ�
        /// �ﱾǿ @ 2013-04-03 12:46:44
        /// </summary>
        /// <param name="recipeInfo">The recipe info.</param>
        /// <remarks></remarks>
        private void SavePmtRecipeSuccess(RecipeInfo recipeInfo)
        {
            this.service.SavePmtRecipeLog(recipeInfo.PmtRecipe.ObjID.ToString());
            //this.service.RefreshPmtRecipe(recipeInfo.PmtRecipe.ObjID.ToString());
        }
        /// <summary>
        /// ���������Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:45
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
        /// ��������������Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:45
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
                    m.TermCode = recipeMixingInfo[i].PmtTerm.TermCode;//���п��ܶ���
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
        /// ��������������Ϣ
        /// Ԭ�� @ 2013-04-03 12:46:45
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
        /// �����䷽��Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:46
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
        /// �����䷽��Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:46
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
                //Result = "��Ϣ���Ӵ���";
                Result = Result + ex.Message;
                return Result;
            }
            Result = string.Empty;
            return Result;
        }
        /// <summary>
        /// �����䷽��Ϣ
        /// �ﱾǿ @ 2013-04-03 12:46:46
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
                Result = "��Ϣ���Ӵ���";
                Result = ex.Message;
            }

            return Result;
        }
        #endregion


        /// <summary>
        /// ���湤���䷽
        /// �ﱾǿ @ 2013-04-03 12:18:09
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

            #region ��������
            if (!string.IsNullOrEmpty(recipeInfo.PmtRecipe.RecipeEquipCode) && !string.IsNullOrEmpty(recipeInfo.PmtRecipe.RecipeMaterialCode) && recipeInfo.PmtRecipe.RecipeVersionID > 0)
            {
                if (PmtRecipeChanged(recipeInfo, recipeMixingInfo, recipeWeightInfo, recipeOpenMixingInfo) == 0)
                {
                    return "û�н��������޸ģ�����Ҫ����";
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
        /// ����һ�η������䷽
        /// ��־�� @ 2015-01-14 12:18:09
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
            #region ��ʼ������
            if (pmtRecipe.ObjID > 0)
            {

            }
            EntityArrayList<BasMaterial> basMaterialList = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == pmtRecipe.RecipeMaterialCode);
            string materialName = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == pmtRecipe.RecipeMaterialCode)[0].MaterialName;
            string Result = IniPmtRecipyici(pmtRecipe, ref recipe);

            #endregion

            #region У������

            #endregion
            #region ��������
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
        //һ�η���֤�䷽��Ϣ
        private string IniPmtRecipyici(PmtRecipe pmtRecipe, ref RecipeInfo recipeInfo)
        {
            string Result = string.Empty;
            recipeInfo = new RecipeInfo();
            recipeInfo.PmtRecipe = pmtRecipe;
            if (string.IsNullOrWhiteSpace(pmtRecipe.RecipeMaterialCode))
            {
                return "������Ϣ����Ϊ��";
            }

            if (string.IsNullOrWhiteSpace(pmtRecipe.RecipeEquipCode))
            {
                return "��̨��Ϣ����Ϊ��";
            }
            EntityArrayList<BasMaterial> basMaterialList = basMaterialManager.GetListByWhere(BasMaterial._.MaterialCode == pmtRecipe.RecipeMaterialCode);
            if (basMaterialList.Count == 0)
            {
                return "������Ϣ������";
            }
            EntityArrayList<BasEquip> basEquipList = basEquipManager.GetListByWhere(BasEquip._.EquipCode == pmtRecipe.RecipeEquipCode);
            if (basMaterialList.Count == 0)
            {
                return "��̨��Ϣ������";
            }
            if (pmtRecipe.RecipeType == null)
            {
                return "�������䷽����";
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
                Result = "��Ϣ���Ӵ���";
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
                Result = "��Ϣ���Ӵ���";
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
        #region �䷽����
        /// <summary>
        /// ��̨����
        /// �ﱾǿ @ 2013-04-03 12:46:47
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

            //if (RecipeState == ((int)PmtState.����).ToString())
            //{
            //    EntityArrayList<PmtRecipe> pmtRecipeEquip = this.GetListByWhere(PmtRecipe._.RecipeEquipCode == EquipCode
            //                                            && PmtRecipe._.RecipeMaterialCode == pmtRecipe.RecipeMaterialCode
            //                                            && PmtRecipe._.RecipeState == ((int)PmtState.����).ToString());

            //    foreach (PmtRecipe r in pmtRecipeEquip)
            //    {
            //        r.RecipeState = ((int)PmtState.����).ToString();
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
        /// �����䷽����
        /// �ﱾǿ @ 2013-04-03 12:18:09
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

        #region �䷽����
        /// <summary>
        /// ��̨����ͬ���Ͽ���
        /// ��С�� @ 2014-1-09 12:46:47
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
        /// �����䷽����
        /// �ﱾǿ @ 2013-04-03 12:18:09
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
        /// ���䷽���������޸�
        /// ��С�� @  2013-05-13 11:18:09
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
                result = "��Ϣ���Ӵ���";
                result = ex.Message;
            }

            return result;
        }


        /// <summary>
        /// �����䷽����
        /// �ﱾǿ @ 2013-04-03 12:18:09
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
        /// ��̨����
        /// �ﱾǿ @ 2013-04-03 12:46:47
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
        /// ��̨����ͬ���Ͽ���
        /// ��С�� @ 2014-1-09 12:46:47
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