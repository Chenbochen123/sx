using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Components;
    public interface IEqmStopTypeManager : IBaseManager<EqmStopType>
    {
        //获取停机类型信息
        DataSet GetDataByParas( EqmStopTypeParams queryParams );

        //获取新的类型代码
        string GetNextTypeCodeByParas(EqmStopType eqmStopType);

        PageResult<EqmStopType> GetEqmStopTypeBySearchKey(Mesnac.Data.Implements.EqmStopTypeService.QueryParams queryParams);
    }
}
