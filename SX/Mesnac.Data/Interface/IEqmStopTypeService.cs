using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IEqmStopTypeService : IBaseService<EqmStopType>
    {
        //��ȡͣ��������Ϣ
        DataSet GetDataByParas( EqmStopTypeParams queryParams );

        //��ȡ�µ����ʹ���
        string GetNextTypeCodeByParas(EqmStopType eqmStopType);

        PageResult<EqmStopType> GetEqmStopTypeBySearchKey(Mesnac.Data.Implements.EqmStopTypeService.QueryParams queryParams);
    }
    public class EqmStopTypeParams
    {
        public string objID
        {
            get;
            set;
        }
        public string mainTypeID
        {
            get;
            set;
        }
        public string typeCode
        {
            get;
            set;
        }
        public string typeName
        {
            get;
            set;
        }
        public string deleteFlag
        {
            get;
            set;
        }
        public string stopMainType
        {
            get;
            set;
        }
        public PageResult<EqmStopType> pageResult
        {
            get;
            set;
        }
    }

}
