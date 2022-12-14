using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasMaterialMajorTypeService : IBaseService<BasMaterialMajorType>
    {  
        //���ϴ����ҳ����
        PageResult<BasMaterialMajorType> GetTablePageDataBySql(Mesnac.Data.Implements.BasMaterialMajorTypeService.QueryParams queryParams);
        //��ȡ���ϴ�����һ�����
        string GetNextMaterialMajorTypeCode();
    }
}
