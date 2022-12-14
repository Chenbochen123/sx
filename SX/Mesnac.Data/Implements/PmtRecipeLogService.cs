using System;
using System.Collections.Generic;
using System.Text;

namespace Mesnac.Data.Implements
{
    using Mesnac.Entity;
    using Mesnac.Data.Components;
    using Mesnac.Data.Interface;
    /// <summary>
    /// �����䷽��־
    /// �ﱾǿ @ 2013-04-03 13:02:44
    /// </summary>
    /// <remarks></remarks>
    public class PmtRecipeLogService : BaseService<PmtRecipeLog>, IPmtRecipeLogService
    {
        #region ���췽��

        /// <summary>
        /// �� PmtRecipeLogService ���캯��
        /// �ﱾǿ @ 2013-04-03 13:02:44
        /// </summary>
        /// <remarks></remarks>
        public PmtRecipeLogService() : base() { }

        /// <summary>
        /// �� PmtRecipeLogService ���캯��
        /// �ﱾǿ @ 2013-04-03 13:02:44
        /// </summary>
        /// <param name="connectStringKey">The connect string key.</param>
        /// <remarks></remarks>
        public PmtRecipeLogService(string connectStringKey) : base(connectStringKey) { }

        /// <summary>
        /// �� PmtRecipeLogService ���캯��
        /// �ﱾǿ @ 2013-04-03 13:02:44
        /// </summary>
        /// <param name="way">The way.</param>
        /// <remarks></remarks>
        public PmtRecipeLogService(NBear.Data.Gateway way) : base(way) { }

        #endregion ���췽��

        #region ��ѯ�����ඨ��
        /// <summary>
        /// ��ѯ����������
        /// �ﱾǿ @ 2013-04-03 13:02:44
        /// </summary>
        /// <remarks></remarks>
        public class QueryParams
        {
            /// <summary>
            /// �� QueryParams ���캯��
            /// �ﱾǿ @ 2013-04-03 13:02:44
            /// </summary>
            /// <remarks></remarks>
            public QueryParams()
            {
                PageParams = new PageResult<PmtRecipeLog>();
                BaginTime = DateTime.Now.ToString("yyyy-MM-dd");
                EndTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
                RecipeEquipCode = null;
                RecipeWorkShopCode = null;
                RecipeMaterialCode = null;
                RecipeVersionID = null;
                RecipeName = null;
            }
            /// <summary>
            /// ҳ���ѯ��������
            /// �ﱾǿ @ 2013-04-03 13:02:44
            /// </summary>
            /// <value>The page params.</value>
            /// <remarks></remarks>
            public PageResult<PmtRecipeLog> PageParams { get; set; }
            /// <summary>
            /// Gets or sets the workshop code.
            /// yuany @ 2013-04-03 13:02:28
            /// </summary>
            /// <value>The recipe name.</value>
            /// <remarks></remarks>
            public string RecipeName { get; set; }
            /// <summary>
            /// Gets or sets the workshop code.
            /// �ﱾǿ @ 2013-04-03 13:02:28
            /// </summary>
            /// <value>The workshop code.</value>
            /// <remarks></remarks>
            public string RecipeWorkShopCode { get; set; }
            /// <summary>
            /// ��ʼʱ��
            /// �ﱾǿ @ 2013-04-03 13:02:44
            /// </summary>
            /// <value>The bagin time.</value>
            /// <remarks></remarks>
            public string BaginTime { get; set; }
            /// <summary>
            /// ����ʱ��
            /// �ﱾǿ @ 2013-04-03 13:02:44
            /// </summary>
            /// <value>The end time.</value>
            /// <remarks></remarks>
            public string EndTime { get; set; }
            /// <summary>
            /// ��̨���
            /// �ﱾǿ @ 2013-04-03 13:02:44
            /// </summary>
            /// <value>The recipe equip code.</value>
            /// <remarks></remarks>
            public string RecipeEquipCode { get; set; }
            /// <summary>
            /// ���ϱ��
            /// �ﱾǿ @ 2013-04-03 13:02:45
            /// </summary>
            /// <value>The recipe material code.</value>
            /// <remarks></remarks>
            public string RecipeMaterialCode { get; set; }
            /// <summary>
            /// ���հ汾
            /// �ﱾǿ @ 2013-04-03 13:02:45
            /// </summary>
            /// <value>The recipe version ID.</value>
            /// <remarks></remarks>
            public string RecipeVersionID { get; set; }
        }
        #endregion
        /// <summary>
        /// ��ȡ��ҳ���ݼ�
        /// �ﱾǿ @ 2013-04-03 13:02:45
        /// </summary>
        /// <param name="queryParams">��ѯ����</param>
        /// <returns>��ҳ���ݼ�</returns>
        /// <remarks></remarks>
        public PageResult<PmtRecipeLog> GetTablePageDataBySql(QueryParams queryParams)
        {
            PageResult<PmtRecipeLog> pageParams = queryParams.PageParams;
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append(@"SELECT t1.*,
							t2.EquipName AS ShowEquipName,
                            t2.WorkShopName AS WorkShopCode,
							t3.MaterialName AS ShowMaterialName,
                            t4.ItemName AS RecipeTypeName,
                            t5.ItemName AS RecipeStateName,
                            t6.ItemName AS AuditFlagName,
                            t7.UserName AS RecipeModifyUserName,
                            t8.UserName AS AuditUserName
                            FROM dbo.PmtRecipeLog t1
                            LEFT JOIN (SELECT e.* , w.WorkShopName FROM BasEquip e left join BasWorkShop w ON e.WorkShopCode = w.ObjID) t2 ON t2.EquipCode=t1.RecipeEquipCode
                            LEFT JOIN dbo.BasMaterial t3 ON t3.MaterialCode=t1.RecipeMaterialCode
                            LEFT JOIN dbo.SysCode t4 ON t4.ItemCode=t1.RecipeType AND t4.TypeID='PmtType'
                            LEFT JOIN dbo.SysCode t5 ON t5.ItemCode=t1.RecipeState AND t5.TypeID='PmtState'
                            LEFT JOIN dbo.SysCode t6 ON t6.ItemCode=t1.AuditFlag AND t6.TypeID='Audit'
                            LEFT JOIN dbo.BasUser t7 ON t1.RecipeModifyUser=t7.WorkBarcode
                            LEFT JOIN dbo.BasUser t8 ON t1.AuditUser=t8.WorkBarcode
                            WHERE 1=1 ");
            if (!string.IsNullOrEmpty(queryParams.BaginTime))
            {
                sqlstr.AppendLine("AND t1.RecipeModifyTime >= '" + Convert.ToDateTime(queryParams.BaginTime).ToString("yyyy-MM-dd") + "'");
            }
            try
            {
                if (!string.IsNullOrEmpty(queryParams.EndTime))
                {
                    sqlstr.AppendLine("AND t1.RecipeModifyTime <= '" + Convert.ToDateTime(queryParams.EndTime).AddDays(1).ToString("yyyy-MM-dd") + "'");
                }
            }
            catch { }
            if (!string.IsNullOrEmpty(queryParams.RecipeEquipCode))
            {
                sqlstr.AppendLine("AND t1.RecipeEquipCode = '" + queryParams.RecipeEquipCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.RecipeName))
            {
                sqlstr.AppendLine("AND t1.RecipeName Like '%" + queryParams.RecipeName + "%'");
            }
            if (!string.IsNullOrEmpty(queryParams.RecipeWorkShopCode))
            {
                sqlstr.AppendLine("AND t2.WorkShopCode = '" + queryParams.RecipeWorkShopCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.RecipeMaterialCode))
            {
                sqlstr.AppendLine("AND t1.RecipeMaterialCode = '" + queryParams.RecipeMaterialCode + "'");
            }
            if (!string.IsNullOrEmpty(queryParams.RecipeVersionID))
            {
                sqlstr.AppendLine("AND t1.RecipeVersionID = '" + queryParams.RecipeVersionID + "'");
            }
            pageParams.QueryStr = sqlstr.ToString();
            if (pageParams.PageSize < 0)
            {
                NBear.Data.CustomSqlSection css = this.GetBySql(sqlstr.ToString());
                pageParams.DataSet = css.ToDataSet();
                return pageParams;
            }
            else
            {
                return this.GetPageDataBySql(pageParams);
            }
        }
    }
}
