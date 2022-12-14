using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Implements
{
    using NBear.Common;
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using Mesnac.Data.Components;
    public class BasMaterialManager : BaseManager<BasMaterial>, IBasMaterialManager
    {
        #region 属性注入与构造方法

        private IBasMaterialService service;

        public BasMaterialManager()
        {
            this.service = new BasMaterialService();
            base.BaseService = this.service;
        }

        public BasMaterialManager(string connectStringKey)
        {
            this.service = new BasMaterialService(connectStringKey);
            base.BaseService = this.service;
        }

        public BasMaterialManager(NBear.Data.Gateway way)
        {
            this.service = new BasMaterialService(way);
            base.BaseService = this.service;
        }

        #endregion
        #region 查询条件类定义
        public class QueryParams : BasMaterialService.QueryParams
        {
        }
        #endregion

        public PageResult<BasMaterial> GetTablePageDataBySql(Mesnac.Data.Implements.BasMaterialService.QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public string GetNextMaterialCode(string MajorTypeID, string MinorTypeID, string RubCode, bool isOriginal)
        {
            return this.service.GetNextMaterialCode(MajorTypeID, MinorTypeID, RubCode, isOriginal);
        }
        public string GetNextMaterialObjID()
        {
            return this.service.GetNextMaterialObjID();
        }
        public string GetMaterName(string MaterCode)
        {
            return this.service.GetMaterName(MaterCode);
        }
        public DataSet GetMaterInfo(string ErpCode)
        {
            return this.service.GetMaterInfo(ErpCode);
        }
        public bool IsMinorType(string MaterialCode, string MinorType)
        {
            return this.service.IsMinorType(MaterialCode, MinorType);
        }
        public EntityArrayList<BasMaterial> GetMaterialNameAndCodeBySearchKey(int top, string majorId, string searchKey)
        {
            return this.service.GetMaterialNameAndCodeBySearchKey(top, majorId , searchKey);
        }
        public PageResult<BasMaterial> GetMaterialBySearchKey(Mesnac.Data.Implements.BasMaterialService.QueryParams queryParams)
        {
            return this.service.GetMaterialBySearchKey(queryParams);
        }
        public PageResult<BasMaterial> GetMaterialByChineseSearchKey(Mesnac.Data.Implements.BasMaterialService.QueryParams queryParams)
        {
            return this.service.GetMaterialByChineseSearchKey(queryParams);
        }
        public void UpdateMaterialTime(string materialcode, decimal maxparktime, decimal minparktime, string restoredate, string oper)
        {
             this.service.UpdateMaterialTime(materialcode,maxparktime,minparktime,restoredate,oper);
        }
        public string GetSapMaterialShortCode(string erpCode)
        {
            return this.service.GetSapMaterialShortCode(erpCode);
        }
    }
}
