using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NBear.Common;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasMaterialService : IBaseService<BasMaterial>
    {
        //物料分页方法
        PageResult<BasMaterial> GetTablePageDataBySql(Mesnac.Data.Implements.BasMaterialService.QueryParams queryParams);
        //获取物料下一个编号
        string GetNextMaterialCode(string MajorTypeID, string MinorTypeID, string RubCode, bool isOriginal);
        //获取物料自增ObjID;
        string GetNextMaterialObjID();
        //根据物料编号取得物料名称
        string GetMaterName(string MaterCode);
        //根据物料ERP编号获取物料编码和物料名称
        DataSet GetMaterInfo(string ErpCode);
        // 判断物料是否属于指定的小类
        bool IsMinorType(string MaterialCode, string MinorType);
        //模糊查询
        EntityArrayList<BasMaterial> GetMaterialNameAndCodeBySearchKey(int top, string majorId, string searchKey);
        PageResult<BasMaterial> GetMaterialBySearchKey(Mesnac.Data.Implements.BasMaterialService.QueryParams queryParams);
        PageResult<BasMaterial> GetMaterialByChineseSearchKey(Mesnac.Data.Implements.BasMaterialService.QueryParams queryParams);
        void UpdateMaterialTime(string materialcode, decimal maxparktime, decimal minparktime, string restoredate, string oper);
        string GetSapMaterialShortCode(string erpCode);
    }
}
