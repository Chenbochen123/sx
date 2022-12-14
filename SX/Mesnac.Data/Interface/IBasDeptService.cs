using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasDeptService : IBaseService<BasDept>
    {
        //���ŷ�ҳ����
        PageResult<BasDept> GetTablePageDataBySql(Mesnac.Data.Implements.BasDeptService.QueryParams queryParams);
        //��ȡ������һ�����
        string GetNextDepCode();
    }
}
