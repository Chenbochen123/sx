using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NBear.Common;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasMaterialManager : IBaseManager<BasMaterial>
    {
        PageResult<BasMaterial> GetTablePageDataBySql(Mesnac.Data.Implements.BasMaterialService.QueryParams queryParams);

        string GetNextMaterialCode(string MajorTypeID, string MinorTypeID, string RubCode, bool isOriginal);
        //获取物料自增ObjID;
        string GetNextMaterialObjID();
        string GetMaterName(string MaterCode);
        DataSet GetMaterInfo(string ErpCode);
        bool IsMinorType(string MaterialCode, string MinorType);
        EntityArrayList<BasMaterial> GetMaterialNameAndCodeBySearchKey(int top, string majorId, string searchKey);
        PageResult<BasMaterial> GetMaterialBySearchKey(Mesnac.Data.Implements.BasMaterialService.QueryParams queryParams);
        PageResult<BasMaterial> GetMaterialByChineseSearchKey(Mesnac.Data.Implements.BasMaterialService.QueryParams queryParams);
        void UpdateMaterialTime(string materialcode, decimal maxparktime, decimal minparktime, string restoredate, string oper);
        string GetSapMaterialShortCode(string erpCode);
    }
}
