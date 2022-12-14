using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IBasRubInfoService : IBaseService<BasRubInfo>
    {
        //������Ϣ��ҳ����
        PageResult<BasRubInfo> GetTablePageDataBySql(Mesnac.Data.Implements.BasRubInfoService.QueryParams queryParams);
        //��ȡ������һ�����
        string GetNextRubInfoCode();
    }
}
