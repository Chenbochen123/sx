using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    using System.Data;
    public class PptWeighService : BaseService<PptWeigh>, IPptWeighService
    {
        #region ���췽��

        public PptWeighService() : base() { }

        public PptWeighService(string connectStringKey) : base(connectStringKey) { }

        public PptWeighService(NBear.Data.Gateway way) : base(way) { }

        #endregion ���췽��

        #region IPptWeighService ��Ա
        /// <summary>
        /// ���ݼƻ�ID��ѯ��С�ϳ�����Ϣ
        /// ���˽�
        /// 2013-4-2
        /// </summary>
        /// <param name="planID"></param>
        /// <returns></returns>
        public DataTable GetSmallMaterWeighListByPlanID(string planID)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(@"declare @sql varchar(8000)
                            set @sql = 'select CAST(ROW_NUMBER() OVER(ORDER BY Barcode) AS NVARCHAR(max)) ���� '
                            select @sql = @sql + ' , max(case MaterName when ''' + MaterName + ''' then RealWeight else 0 end) [' + MaterName + ']'
                            from (select DISTINCT MaterName FROM dbo.PptWeigh WHERE PlanID='{0}') as a
                            set @sql = @sql +' ,sum(RealWeight) ���� from PptWeigh WHERE  PlanID=''{0}''  group by Barcode'
                            PRINT @sql
                            exec(@sql) ");
            string sql = string.Format(sb.ToString(), planID);
            return this.GetBySql(sql).ToDataSet().Tables[0];

        }

        #endregion
    }
}
