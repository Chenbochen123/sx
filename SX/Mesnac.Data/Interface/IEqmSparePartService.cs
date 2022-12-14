using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IEqmSparePartService : IBaseService<EqmSparePart>
    {
        //��λ�ķ�ҳ����
        PageResult<EqmSparePart> GetTablePageDataBySql(Mesnac.Data.Implements.EqmSparePartService.QueryParams queryParams);
        //��ȡ������һ�����
        string GetNextSparePartCode(string MajorTypeID, string MinorTypeID);

        PageResult<EqmSparePart> GetSparePartBySearchKey(Mesnac.Data.Implements.EqmSparePartService.QueryParams queryParams);
    }
}
