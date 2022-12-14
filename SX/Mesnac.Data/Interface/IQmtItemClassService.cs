using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Data.Components;
    using Mesnac.Entity;
    public interface IQmtItemClassService : IBaseService<QmtItemClass>
    {
        //��λ�ķ�ҳ����
        PageResult<QmtItemClass> GetTablePageDataBySql(Mesnac.Data.Implements.QmtItemClassService.QueryParams queryParams);

        //��ȡ��λ����һ������ֵ
        int GetItemClassNextPrimaryKeyValue();
    }
}
