using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IEqmSapSparePartService : IBaseService<EqmSapSparePart>
    {
        //Sap�����ķ�ҳ����
        PageResult<EqmSapSparePart> GetTablePageDataBySql(Mesnac.Data.Implements.EqmSapSparePartService.QueryParams queryParams);
        //��ȡ��һ�������
        string GetNextSparePartStoreInCode(DateTime storeInDate);
    }
}
