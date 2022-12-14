using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Data.Interface
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    public interface IEqmSparePartDetailTypeService : IBaseService<EqmSparePartDetailType>
    {
        //获取备件小类信息
        DataSet GetDataByParas( EqmSparePartDetailTypeParams queryParams );
    }
    public class EqmSparePartDetailTypeParams
    {
        public string objID
        {
            get;
            set;
        }
        public string detailTypeCode
        {
            get;
            set;
        }
        public string detailTypeName
        {
            get;
            set;
        }
        public string mainTypeID
        {
            get;
            set;
        }
        public string mainTypeName
        {
            get;
            set;
        }
        public string autoIn
        {
            get;
            set;
        }
        public string deleteFlag
        {
            get;
            set;
        }
        public PageResult<EqmSparePartDetailType> pageResult
        {
            get;
            set;
        }
    }
}
