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
    public class TblRecipeManager : BaseManager<TblRecipe>, ITblRecipeManager
    {
		#region 属性注入与构造方法
		
        private ITblRecipeService service;

        public TblRecipeManager()
        {
            this.service = new TblRecipeService();
            base.BaseService = this.service;
        }

		public TblRecipeManager(string connectStringKey)
        {
			this.service = new TblRecipeService(connectStringKey);
            base.BaseService = this.service;
        }

        public TblRecipeManager(NBear.Data.Gateway way)
        {
			this.service = new TblRecipeService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        public class QueryParams : TblRecipeService.QueryParams
        {
        }
        #endregion

        public PageResult<TblRecipe> GetTablePageDataBySql(Mesnac.Data.Implements.TblRecipeService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }

        public string GetNextRubInfoCode()
        {
            return this.service.GetNextRubInfoCode();
        }
    }
}
