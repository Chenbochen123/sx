using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

namespace Mesnac.Business.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;

    using NBear.Common;
    public interface IQmtCheckStandMasterManager : IBaseManager<QmtCheckStandMaster>
    {
        DataSet GetDataByParas(IQmtCheckStandMasterParams queryParams);

        void Upgrade(QmtCheckStandMaster entity);

        void DeleteWithLogic(QmtCheckStandMaster entity);

        void AddCopy(QmtCheckStandMaster mQmtCheckStandMaster, EntityArrayList<QmtCheckStandDetail> mQmtCheckStandDetailList
            , EntityArrayList<QmtCheckStandGrade> mQmtCheckStandGradeList, EntityArrayList<QmtCheckStandEquip> mQmtCheckStandEquipList
            , EntityArrayList<QmtCheckStandEquipGrade> mQmtCheckStandEquipGradeList);

        void UpgradeCopy(QmtCheckStandMaster mQmtCheckStandMaster, EntityArrayList<QmtCheckStandDetail> mQmtCheckStandDetailList
            , EntityArrayList<QmtCheckStandGrade> mQmtCheckStandGradeList, EntityArrayList<QmtCheckStandEquip> mQmtCheckStandEquipList
            , EntityArrayList<QmtCheckStandEquipGrade> mQmtCheckStandEquipGradeList);

        void SaveImport(EntityArrayList<QmtCheckStandMaster> mQmtCheckStandMasterList
            , EntityArrayList<QmtCheckStandDetail> mQmtCheckStandDetailList);

        void Audit(QmtCheckStandMaster entity);

        DataSet GetCheckStandInfoByParas(IQmtCheckStandMasterQueryInfoParams queryParams);
        QmtCheckStandMaster GetById(string id);
    }


}
