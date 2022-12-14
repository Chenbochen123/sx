using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mesnac.Entity;
using Ext.Net;
using Mesnac.Business.Implements;
using NBear.Common;
using System.Text.RegularExpressions;
using Mesnac.Data.Components;
using System.Data;
using System.IO;
using System.Text;
using Microsoft.Win32;
using System.Diagnostics;
using Ionic.Zip;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Data.SqlClient;
public partial class Manager_BasicInfo_DepartmentInfo_Department : Mesnac.Web.UI.Page
{
    protected BasDeptManager manager = new BasDeptManager();//业务对象
    protected SysCodeManager sysCodeManager = new SysCodeManager();
    protected BasDeptErpInfoManager erpInfoManager = new BasDeptErpInfoManager();
    protected Ext.Net.MessageBox msg = new Ext.Net.MessageBox();//弹出框
    protected EntityArrayList<BasDept> entityList;
    private string topDepCode = "00";

    #region 权限定义
    protected __ _ = new __();
    public class __ : ___  //必须继承___   //Action不能重复，重复会被覆盖
    {
        public __()
        {
            查询 = new SysPageAction() { ActionID = 1, ActionName = "btn_search" };
            历史查询 = new SysPageAction() { ActionID = 2, ActionName = "btn_history_search" };
            导出 = new SysPageAction() { ActionID = 3, ActionName = "btnExport" };
            修改 = new SysPageAction() { ActionID = 4, ActionName = "Edit" };
            删除 = new SysPageAction() { ActionID = 5, ActionName = "Delete" };
            恢复 = new SysPageAction() { ActionID = 6, ActionName = "Recover" };
            添加 = new SysPageAction() { ActionID = 7, ActionName = "btn_add" };
        }
        public SysPageAction 查询 { get; private set; } //必须为 public
        public SysPageAction 历史查询 { get; private set; } //必须为 public
        public SysPageAction 导出 { get; private set; } //必须为 public
        public SysPageAction 修改 { get; private set; } //必须为 public
        public SysPageAction 删除 { get; private set; } //必须为 public
        public SysPageAction 恢复 { get; private set; } //必须为 public
        public SysPageAction 添加 { get; private set; } //必须为 public
    }
    #endregion

    #region 初始化方法
    /// <summary>
    /// 页面初始化方法
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!X.IsAjaxRequest)
        {
            InitTreeDept();
        }
    }
    #region 查询显示左侧部门列表 树
    [DirectMethod]
    public string treePanelDeptLoad(string pageid)
    {
        WhereClip whereDept = new WhereClip();
        whereDept.And(BasDept._.DeleteFlag == "0");
        whereDept.And(BasDept._.ParentNum == pageid);
        EntityArrayList<BasDept> deptList = manager.GetListByWhere(whereDept);
        NodeCollection nodes = new Ext.Net.NodeCollection();
        if (deptList.Count > 0)
        {
            foreach (BasDept dept in deptList)
            {
                if (manager.GetListByWhere(BasDept._.ParentNum == dept.DepCode && BasDept._.DeleteFlag == 0).Count > 0)
                {
                    Node node = new Node();
                    node.NodeID = dept.DepCode;
                    node.Text = dept.DepName;
                    node.Icon = Icon.Building;
                    node.Leaf = false;
                    nodes.Add(node);
                }
                else
                {
                    Node node = new Node();
                    node.NodeID = dept.DepCode;
                    node.Text = dept.DepName;
                    node.Icon = Icon.BuildingGo;
                    node.Leaf = true;
                    nodes.Add(node);
                }
            }
        }
        return nodes.ToJson();
    }
    /// <summary>
    /// 初始化部门列表树
    /// </summary>
    private void InitTreeDept()
    {
        if (this._.查询.SeqIdx == 0)
        {
            return;
        }
        treeDept.GetRootNode().RemoveAll();
        WhereClip where = new WhereClip();
        where.And(BasDept._.DeleteFlag == "0");
        where.And(BasDept._.DepCode == "00");
        EntityArrayList<BasDept> lst = manager.GetListByWhere(where);
        foreach (BasDept dept in lst)
        {

            Node node = new Node();
            node.NodeID = dept.DepCode;
            node.Text = dept.DepName;
            node.Icon = Icon.Building;
            treeDept.GetRootNode().AppendChild(node);
        }
    }
    #endregion
    #endregion

    #region 分页相关方法
    private PageResult<BasDept> GetPageResultData(PageResult<BasDept> pageParams)
    {
        BasDeptManager.QueryParams queryParams = new BasDeptManager.QueryParams();
        queryParams.pageParams = pageParams;
        queryParams.depCode = txt_dep_code.Text.TrimEnd().TrimStart();
        queryParams.depName = txt_dep_name.Text.TrimEnd().TrimStart();
        queryParams.parentNum = hidden_parent_num.Value == "" ? "" : hidden_parent_num.Value.ToString();
        queryParams.erpCode = txt_erp_code.Text.TrimEnd().TrimStart();
        queryParams.remark = txt_remark.Text.TrimEnd().TrimStart();
        queryParams.deleteFlag = hidden_delete_flag.Text;
        
        return GetTablePageDataBySql(queryParams);
    }
    public PageResult<BasDept> GetTablePageDataBySql(BasDeptManager.QueryParams queryParams)
    {
        PageResult<BasDept> pageParams = queryParams.pageParams;
        StringBuilder sqlstr = new StringBuilder();
        sqlstr.AppendLine(@" SELECT	    dep1.ObjID as ObjID , dep1.DepCode as DepCode , dep1.DepName as DepName, '' as DepLevel, 
                                            dep2.DepName as ParentNum ,dep1.ERPCode as ERPCode, dep1.HRCode as HRCode, dep1.DisplayId as DisplayId,
                                            dep1.Remark as Remark, dep1.DeleteFlag as DeleteFlag 
                                 FROM	    basdept dep1  
                                 LEFT JOIN  basdept dep2 on dep1.ParentNum = dep2.DepCode  
                                 WHERE      1 = 1");
        if (!string.IsNullOrEmpty(queryParams.objID))
        {
            sqlstr.AppendLine(" AND dep1.ObjID = " + queryParams.objID);
        }
        if (!string.IsNullOrEmpty(queryParams.depCode))
        {
            sqlstr.AppendLine(" AND dep1.DepCode like '%" + queryParams.depCode + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.depName))
        {
            sqlstr.AppendLine(" AND dep1.DepName like '%" + queryParams.depName + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.depLevel))
        {
            sqlstr.AppendLine(" AND dep1.DepLevel = " + queryParams.depLevel);
        }

        if (!string.IsNullOrEmpty(queryParams.parentNum.Trim()))
        {
            sqlstr.AppendLine(" AND dep1.ParentNum = " + queryParams.parentNum);
        }
        if (!string.IsNullOrEmpty(queryParams.objID))
        {
            sqlstr.AppendLine(" AND dep1.ObjID = " + queryParams.objID);
        }
        if (!string.IsNullOrEmpty(queryParams.erpCode))
        {
            sqlstr.AppendLine(" AND dep1.ERPCode like '%" + queryParams.erpCode + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.remark))
        {
            sqlstr.AppendLine(" AND dep1.Remark like '%" + queryParams.remark + "%'");
        }
        if (!string.IsNullOrEmpty(queryParams.deleteFlag))
        {
            sqlstr.AppendLine(" AND dep1.DeleteFlag ='" + queryParams.deleteFlag + "'");
        }
        //sqlstr.AppendLine("  order by  dep1.DisplayId ");
        //txt_erp_code.Text = sqlstr.ToString() + " order by  dep1.DisplayId  ";
      
        pageParams.QueryStr = sqlstr.ToString();
        if (pageParams.PageSize < 0)
        {
            NBear.Data.CustomSqlSection css = manager.GetBySql(sqlstr.ToString());
            pageParams.DataSet = css.ToDataSet();
            return pageParams;
        }
        else
        {
            return manager.GetPageDataBySql(pageParams);
        }
    }
    [DirectMethod]
    public object GridPanelBindData(string action, Dictionary<string, object> extraParams)
    {
        if (!Regex.IsMatch(txt_dep_code.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_dep_code.Text = "";
        }
        if (this._.查询.SeqIdx == 0)
        {
            return "";
        }
        StoreRequestParameters prms = new StoreRequestParameters(extraParams);
        PageResult<BasDept> pageParams = new PageResult<BasDept>();
        pageParams.PageIndex = prms.Page;
        pageParams.PageSize = prms.Limit;
        pageParams.Orderfld = "ObjID ASC";

        PageResult<BasDept> lst = GetPageResultData(pageParams);
        DataTable data = lst.DataSet.Tables[0];

        int total = lst.RecordCount;
        return new { data, total };
    }
    #endregion

    #region 打印
    /// <summary>
    /// 打印调用方法
    /// yuany 2013年3月2日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportSubmit_Click(object sender, EventArgs e)
    {
        if (!Regex.IsMatch(txt_dep_code.Text.TrimEnd().TrimStart(), "^[0-9]*$"))
        {
            txt_dep_code.Text = "";
        }
        PageResult<BasDept> pageParams = new PageResult<BasDept>();
        pageParams.PageSize = -100;
        PageResult<BasDept> lst = GetPageResultData(pageParams);
        for (int i = 0; i < lst.DataSet.Tables[0].Columns.Count; i++)
        {
            bool isshow = false;
            DataColumn dc = lst.DataSet.Tables[0].Columns[i];
            foreach (ColumnBase cb in this.pnlList.ColumnModel.Columns)
            {
                if ((cb.DataIndex != null) && (cb.DataIndex.ToUpper() == dc.ColumnName.ToUpper()))
                {
                    dc.ColumnName = cb.Text;
                    isshow = true;
                    break;
                }
            }
            if (!isshow)
            {
                lst.DataSet.Tables[0].Columns.Remove(dc.ColumnName);
                i--;
            }
        }
        new Mesnac.Util.Excel.ExcelDownload().ExcelFileDown(lst.DataSet, "部门信息");
    }
    #endregion

    #region 增删改查按钮事件


  
    protected void btn_add_Click(object sender, EventArgs e)
  {
    
      

      

        if (hidden_parent_num.Value == "" || hidden_parent_num.Value.ToString() =="00000")
        {
            msg.Alert("操作", "请选择左侧部门节点！");
            msg.Show();
            return;
        }
        add_dep_name.Text = "";
        hidden_dept_name.Text = "";
        add_parent_num.Text = manager.GetListByWhere(BasDept._.DepCode == (hidden_parent_num.Value == "" ? topDepCode : hidden_parent_num.Value))[0].DepName;
        add_erp_code.Text = "";
        hidden_erp_code.Text = "";
      
        add_display_id.Text = "";
        add_remark.Text = "";
        btnAddSave.Disable(true);
        this.winAdd.Show();
    }

    /// <summary>
    /// 点击恢复激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_recover(string obj_id)
    {
       
        return "恢复成功";
    }

    /// <summary>
    /// 点击修改激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>

     [Ext.Net.DirectMethod()]
    public void commandcolumn_direct_edit(string dep_num)
    {
        //X.Js.Alert(dep_num); return;
        string sql = "select * from basdept where objid='" + dep_num + "' ";
        BasDept dep = manager.GetBySql(sql).ToArray<BasDept>()[0];

        modify_obj_id.Value = dep.DepCode;
        modify_dep_name.Value = dep.DepName;
        hidden_dept_name.Value = dep.DepName;
        modify_dep_level.Value = dep.DepLevel;

        //-----------------------加载父级部门下拉框数据
        if (dep.ParentNum.Trim() != "" && !string.IsNullOrEmpty(dep.ParentNum.Trim()))
        {
            string strSql = "select * from basdept where DepCode='" + dep.ParentNum + "' ";
            BasDept dept = manager.GetBySql(strSql).ToArray<BasDept>()[0];
            modify_parent_num.Value = dept.DepName;
            hidden_parent_num.Value = dept.DepCode;
        }

        modify_erp_code.Value = dep.ERPCode;
        modify_display_id.Value = dep.DisplayId;
        modify_remark.Value = dep.Remark;
        this.winModify.Show();
    }
    /// <summary>
    /// 点击删除触发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="unit_num"></param>
    /// <returns></returns>
    [Ext.Net.DirectMethod()]
    public string commandcolumn_direct_delete(string obj_id)
    {
        string sql = "delete from JCZL_dep where dep_num = '" + obj_id + "'";
        try
        {
            //判断是否可以删除，有子菜单禁止删除，部门中心禁止删除
            BasDept dep = manager.GetListByWhere(BasDept._.DepCode==obj_id)[0];
            EntityArrayList<BasDept>  depList = manager.GetTopNListOrder(1, BasDept._.ObjID.Asc);
            if (depList[0].ObjID.ToString().Equals(obj_id))
            {
                return "删除失败：顶级部门禁止删除";
            }
            depList = manager.GetListByWhere(BasDept._.ParentNum == dep.DepCode && BasDept._.DeleteFlag == "0");
            if (depList.Count > 0)
            {
                return "删除失败：有子部门挂在此部门下，禁止删除";
            }

           
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("部门信息删除", "部门编码：" + dep.DepCode);
            pageToolBar.DoRefresh();

            //左侧树刷新 
            EntityArrayList<BasDept> childDepList = manager.GetListByWhere(BasDept._.ParentNum == hidden_parent_num.Text && BasDept._.DeleteFlag == 0);
            if (childDepList.Count == 0)
            {
                treeDept.GetNodeById(hidden_parent_num.Text).ParentNode().Reload();
            }
            else
            {
                treeDept.GetNodeById(hidden_parent_num.Text).Reload();
            }
        }
        catch (Exception e)
        {
            return "删除失败：" + e;
        }
        return  "删除成功";
    }

    /// <summary>
    /// 点击取消按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnCancel_Click(object sender, DirectEventArgs e)
    {
        this.winAdd.Close();
        this.winModify.Close();
    }

    /// <summary>
    /// 点击添加信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void BtnAddSave_Click(object sender, DirectEventArgs e)
    {
        try
        {  
            //添加校验重复
            EntityArrayList<BasDept> depList = manager.GetListByWhere(BasDept._.DepName == add_dep_name.Text.TrimStart().TrimEnd());
            if (depList.Count > 0)
            {
                X.Msg.Alert("提示", "此部门名称已被使用！").Show();
                return;
            }
            BasDept dep = new BasDept();
            dep.ObjID = Convert.ToInt32(manager.GetNextDepCode());
            dep.DepCode = manager.GetNextDepCode();
            dep.DepName  = add_dep_name.Text;
            dep.DepLevel = Convert.ToInt32(manager.GetListByWhere(BasDept._.DepCode == (hidden_parent_num.Value == "" ? topDepCode : hidden_parent_num.Value))[0].DepLevel + 1);
            dep.ParentNum = hidden_parent_num.Value.ToString();
            dep.DeleteFlag = "0";
            dep.ERPCode = add_erp_code.Text;
           
            if (!string.Empty.Equals(add_display_id.Text))
            {
                dep.DisplayId = Convert.ToInt32(add_display_id.Text);
            }
            dep.Remark = add_remark.Text;
            string strsql = "select  MAX(convert(int,dep_num))+1 from JCZL_dep";
            DataSet ds = manager.GetBySql(strsql).ToDataSet();
            string sql = "insert into JCZL_dep values('" + ds.Tables[0].Rows[0][0].ToString() + "','" + dep.DepName + "','" + dep.ParentNum + "','1','"+dep.ERPCode+"','','"+dep.DisplayId+"','" + dep.Remark + "','','','','')";
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("部门信息添加", "部门编码：" + dep.DepCode);
            this.winAdd.Close();
            pageToolBar.DoRefresh();
            //左侧树刷新
            EntityArrayList<BasDept> childDepList = manager.GetListByWhere(BasDept._.ParentNum == hidden_parent_num.Text && BasDept._.DeleteFlag == 0);
            if (childDepList.Count == 1)
            {
                treeDept.GetNodeById(hidden_parent_num.Text).ParentNode().Reload();
            }
            else
            {
                treeDept.GetNodeById(hidden_parent_num.Text).Reload();
            }
            msg.Alert("操作", "保存成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "保存失败：" + ex);
            msg.Show();
        }
    }

    /// <summary>
    /// 点击修改信息中保存按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
        try
        {
            //修改重复校验
            EntityArrayList<BasDept> workList = manager.GetListByWhere(BasDept._.DepName == modify_dep_name.Text.TrimStart().TrimEnd());
            if (workList.Count > 0)
            {
                if (workList[0].DepName != hidden_dept_name.Text)
                {
                    X.Msg.Alert("提示", "此部门名称已被使用！").Show();
                    return;
                }
            }
            BasDept dep = new BasDept();
            dep.ObjID = Convert.ToInt32(modify_obj_id.Text);
            dep.Attach();
            dep.DepName = modify_dep_name.Text;
         
            dep.DeleteFlag = "0";
            dep.ERPCode = modify_erp_code.Text;
           
            if(!string.Empty.Equals(modify_display_id.Text))
            {
                dep.DisplayId = Convert.ToInt32(modify_display_id.Text);
            }
            dep.Remark = modify_remark.Text;
            string sql = "update JCZL_dep set Dep_Name='" + dep.DepName + "',Erp_code='" + dep.ERPCode + "',display_id='" + dep.DisplayId + "',Remark='" + dep.Remark + "' where Dep_num='" + modify_obj_id.Text + "'";
            //X.Js.Alert(sql); return;
            manager.GetBySql(sql).ToDataSet();
            this.AppendWebLog("部门信息修改", "部门编码：" + dep.DepCode);
            this.winModify.Close();
            pageToolBar.DoRefresh();
            //左侧树刷新
            treeDept.GetNodeById(hidden_parent_num.Text).Reload();
            msg.Alert("操作", "更新成功");
            msg.Show();
        }
        catch (Exception ex)
        {
            msg.Alert("操作", "更新失败：" + ex);
            msg.Show();
        }
    }
    #endregion

    #region 校验方法
    /// <summary>
    /// 检查用户名是否重复
    /// yuany
    /// 2013年1月31日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void CheckDeptName(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        string deptname = field.Text;
        EntityArrayList<BasDept> deptList = manager.GetListByWhere(BasDept._.DepName == deptname);
        if (deptList.Count == 0)
        {
            e.Success = true;
        }
        else
        {
            if (deptList[0].DepName.ToString() == hidden_dept_name.Value.ToString())
            {

                e.Success = true;
            }
            else
            {
                e.Success = false;
                e.ErrorMessage = "此部门名称已被使用！";
            }
        }
    }

    protected void CheckFieldInt(object sender, RemoteValidationEventArgs e)
    {
        TextField field = (TextField)sender;
        Regex regex = new Regex(@"^[\d]+$");
        if (regex.IsMatch(field.Text) || field.Text == "")
        {
            e.Success = true;
        }
        else
        {
            e.Success = false;
            e.ErrorMessage = "此填入项必须为正整数!";
        }
    }
    #endregion
}