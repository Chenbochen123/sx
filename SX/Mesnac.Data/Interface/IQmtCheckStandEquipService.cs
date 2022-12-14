using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    public interface IQmtCheckStandEquipService : IBaseService<QmtCheckStandEquip>
    {
        DataSet GetDataByParas(IQmtCheckStandEquipParams queryParams);
    }

    public interface IQmtCheckStandEquipParams
    {
        string StandId
        {
            get;
            set;
        }
        string ItemCd
        {
            get;
            set;
        }
        string DeleteFlag
        {
            get;
            set;
        }

    }
}
