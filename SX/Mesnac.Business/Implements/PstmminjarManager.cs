using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Mesnac.Business.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using Mesnac.Data.Implements;
    using Mesnac.Business.Interface;
    using NBear.Common;
    public class PstmminjarManager : BaseManager<Pstmminjar>, IPstmminjarManager
    {
		#region 属性注入与构造方法
		
        private IPstmminjarService service;

        public PstmminjarManager()
        {
            this.service = new PstmminjarService();
            base.BaseService = this.service;
        }

		public PstmminjarManager(string connectStringKey)
        {
			this.service = new PstmminjarService(connectStringKey);
            base.BaseService = this.service;
        }

        public PstmminjarManager(NBear.Data.Gateway way)
        {
			this.service = new PstmminjarService(way);
            base.BaseService = this.service;
        }

        #endregion

        public class QueryParams : PstmminjarService.QueryParams
        {
        }

        public PageResult<Pstmminjar> GetTablePageDataBySql(QueryParams queryParams)
        {
            return this.service.GetTablePageDataBySql(queryParams);
        }
        public DataSet  GethandLenameBySql()
        {
            StringBuilder sqlstr = new StringBuilder();
            return this.GetDataSetByStoreProcedure("GetPstmminjar");
            DataSet ds = this.GetBySql(@"select distinct handLename from dbo.Pstmminjar
where handLename is not null
order by handLename").ToDataSet();
            return ds;
        }
        public DataSet GetTotalBySql(string beginDate, string endDate, string UserName, string MaterName)
        {
            String sqlstr = "";
            //return this.GetDataSetByStoreProcedure("GetPstmminjar");
            sqlstr = @" select isnull(PositionName,'') as PositionName,MaterialName,EquipName,SUM(RealWeight) as TotalWeight from dbo.Pstmminjar
 left join BasMaterial on Pstmminjar.MaterCode= BasMaterial.MaterialCode
 left join BasEquip on Pstmminjar.EquipCode =BasEquip.EquipCode
 left join PstmminjarPosition on Pstmminjar.FeedPosition=PstmminjarPosition.PositionID
where 1=1  ";

            if (!String.IsNullOrEmpty(UserName)) { sqlstr = sqlstr + " and  handLename ='" + UserName + "'"; }
            if (!String.IsNullOrEmpty(MaterName)) { sqlstr = sqlstr + " and  MaterCode ='" + MaterName + "'"; }
            sqlstr = sqlstr + " and  InTime >='" + beginDate + "'";
            sqlstr = sqlstr + " and  InTime <='" + endDate + "'";
            sqlstr = sqlstr + "  group by MaterialName,EquipName,PositionName";
            DataSet ds = this.GetBySql(sqlstr).ToDataSet();
            return ds;
        }
        public String GetTotalBySql(string beginDate, string endDate, string UserName, string MaterName,String s)
        {
            String sqlstr = "";
            //return this.GetDataSetByStoreProcedure("GetPstmminjar");
            sqlstr = @" select isnull(PositionName,'') as PositionName,MaterialName,EquipName,SUM(RealWeight) as TotalWeight from dbo.Pstmminjar
 left join BasMaterial on Pstmminjar.MaterCode= BasMaterial.MaterialCode
 left join BasEquip on Pstmminjar.EquipCode =BasEquip.EquipCode
 left join PstmminjarPosition on Pstmminjar.FeedPosition=PstmminjarPosition.PositionID
where 1=1 ";

            if (!String.IsNullOrEmpty(UserName)) { sqlstr = sqlstr + " and  handLename ='" + UserName + "'"; }
            if (!String.IsNullOrEmpty(MaterName)) { sqlstr = sqlstr + " and  MaterCode ='" + MaterName + "'"; }
            sqlstr = sqlstr + " and  InTime >='" + beginDate + "'";
            sqlstr = sqlstr + " and  InTime <='" + endDate + "'";
            sqlstr = sqlstr + "  group by MaterialName,EquipName,PositionName";
            return sqlstr;
            DataSet ds = this.GetBySql(sqlstr).ToDataSet();
            return sqlstr;
        }
    }
}
