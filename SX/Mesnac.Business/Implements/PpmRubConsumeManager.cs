using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using Mesnac.Data.Components;
    using System.Data;
    public class PpmRubConsumeManager : BaseManager<PpmRubConsume>, IPpmRubConsumeManager
    {
        #region 属性注入与构造方法

        private IPpmRubConsumeService service;

        public PpmRubConsumeManager()
        {
            this.service = new PpmRubConsumeService();
            base.BaseService = this.service;
        }

        public PpmRubConsumeManager(string connectStringKey)
        {
            this.service = new PpmRubConsumeService(connectStringKey);
            base.BaseService = this.service;
        }

        public PpmRubConsumeManager(NBear.Data.Gateway way)
        {
            this.service = new PpmRubConsumeService(way);
            base.BaseService = this.service;
        }

        #endregion
        public class QueryParams : PpmRubConsumeService.QueryParams
        {
        }
        public PageResult<PpmRubConsume> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public DataTable GetTotalPageDataBySql(string begindate, string enddate, string chejian, string equipcode, string matertype, string matercode)
        {
            return this.service.GetTotalPageDataBySql(begindate,enddate,chejian,equipcode,matertype,matercode);
        }
    }
}
