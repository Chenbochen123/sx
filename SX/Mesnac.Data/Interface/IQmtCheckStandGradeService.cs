using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    public interface IQmtCheckStandGradeService : IBaseService<QmtCheckStandGrade>
    {
        DataSet GetDataByParas(IQmtCheckStandGradeParams queryParams);
    }

    public interface IQmtCheckStandGradeParams
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
