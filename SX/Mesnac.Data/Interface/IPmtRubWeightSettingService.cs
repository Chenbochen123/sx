using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Implements;
    public interface IPmtRubWeightSettingService : IBaseService<PmtRubWeightSetting>
    {
        //��λ�ķ�ҳ����
        PageResult<PmtRubWeightSetting> GetTablePageDataBySql(Mesnac.Data.Implements.PmtRubWeightSettingService.QueryParams queryParams);
    }
}
