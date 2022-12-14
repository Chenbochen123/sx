using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;

    using Mesnac.Data.Interface;

    public interface IQmtCheckStandEquipGradeManager : IBaseManager<QmtCheckStandEquipGrade>
    {
        DataSet GetDataByParas(IQmtCheckStandEquipGradeParams queryParams);

        void DeleteWithLogic(QmtCheckStandEquipGrade entity);
    }
}
