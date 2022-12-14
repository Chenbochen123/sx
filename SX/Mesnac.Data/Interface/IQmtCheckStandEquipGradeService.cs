using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;

    public interface IQmtCheckStandEquipGradeService : IBaseService<QmtCheckStandEquipGrade>
    {
        DataSet GetDataByParas(IQmtCheckStandEquipGradeParams queryParams);
    }

    public interface IQmtCheckStandEquipGradeParams
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
        string CheckEquipCode
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
