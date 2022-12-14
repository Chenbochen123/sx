using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    public class Pmt_RecipeManager : BaseManager<Pmt_Recipe>, IPmt_RecipeManager
    {
		#region 属性注入与构造方法
		
        private IPmt_RecipeService service;

        public Pmt_RecipeManager()
        {
            this.service = new Pmt_RecipeService();
            base.BaseService = this.service;
        }

		public Pmt_RecipeManager(string connectStringKey)
        {
			this.service = new Pmt_RecipeService(connectStringKey);
            base.BaseService = this.service;
        }

        public Pmt_RecipeManager(NBear.Data.Gateway way)
        {
			this.service = new Pmt_RecipeService(way);
            base.BaseService = this.service;
        }

        #endregion

        public string DeletePmtRecipe(string id, string userid)
        {
            string Result = string.Empty;
            WhereClip where = new WhereClip();
            where.And(Pmt_Recipe._.ObjID == id.ToString());
            EntityArrayList<Pmt_Recipe> lst = this.GetListByWhere(where);
            for (int i = 0; i < lst.Count; i++)
            {
                Pmt_Recipe m = lst[i];

                if (m.Recipe_State == "2")
                {
                    weightManager.DeleteByWhere(PmtRecipeWeight._.RecipeObjID == m.ObjID);
                    mixingManager.DeleteByWhere(PmtRecipeMixing._.RecipeObjID == m.ObjID);
                   // openManager.DeleteByWhere(PmtRecipeOpenMixing._.RecipeObjID == m.ObjID);
                    this.Delete(m);
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
        private IPmtRecipeWeightManager weightManager = new PmtRecipeWeightManager();
        private IPmtRecipeMixingManager mixingManager = new PmtRecipeMixingManager();
    }
}
