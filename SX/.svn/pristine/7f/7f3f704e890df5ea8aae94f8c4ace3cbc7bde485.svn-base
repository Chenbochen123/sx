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

public partial class Manager_ProducingPlan_PlanEntering_PlanEntering : Mesnac.Web.UI.Page
{

    BasEquipManager baseEquipManager =null;// new BasEquipManager();
    PptPlanManager pptPlanManager =null;// new PptPlanManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        baseEquipManager = new BasEquipManager();
        pptPlanManager = new PptPlanManager();
        if (!X.IsAjaxRequest)
        {
            txtStratPlanDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
            InitTreeDept();
        }
    }

    private object[] Data
    {
        get
        {
            return new object[] 
         { 
            new object[]{100, "A1机台", 112, "Integrate 2.0 Forms with 2.0 Layouts", 6, 150, 0, new DateTime(2007, 06, 24),4},
            new object[]{100, "A1机台", 113, "Implement AnchorLayout", 4, 140, 0, new DateTime(2007, 06, 25),2},
            new object[]{100, "A1机台", 114, "Add support for multiple types of anchors", 4, 130, 0, new DateTime(2007, 06, 27),3},
            new object[]{100, "A1机台", 115, "Testing and debugging", 8, 0, 0, new DateTime(2007, 06, 29),1},
            new object[]{101, "A2机台", 101, "Add required rendering \"hooks\" to GridView", 6, 100, 0, new DateTime(2007, 07, 01),1},
            new object[]{101, "A2机台", 102, "Extend GridView and override rendering functions", 6, 100, 0, new DateTime(2007, 07, 03),2},
            new object[]{101, "A2机台", 103, "Extend Store with grouping functionality", 4, 100, 0, new DateTime(2007, 07, 04),4},
            new object[]{101, "A2机台", 121, "Default CSS Styling", 2, 100, 0, new DateTime(2007, 07, 05),3},
            new object[]{101, "A2机台", 104, "Testing and debugging", 6, 100, 0, new DateTime(2007, 07, 06),5},
            new object[]{102, "A3机台", 105, "Ext Grid plugin integration", 4, 125, 0, new DateTime(2007, 07, 01),1},
            new object[]{102, "A3机台", 106, "Summary creation during rendering phase", 4, 125, 0, new DateTime(2007, 07, 02),2},
            new object[]{102, "A3机台", 107, "Dynamic summary updates in editor grids", 6, 125, 0, new DateTime(2007, 07, 05),3},
            new object[]{102, "A3机台", 108, "Remote summary integration", 4, 125, 0, new DateTime(2007, 07, 05),4},
            new object[]{102, "A3机台", 109, "Summary renderers and calculators", 4, 125, 0, new DateTime(2007, 07, 06),7},
            new object[]{102, "A3机台", 110, "Integrate summaries with GroupingView", 10, 125, 0, new DateTime(2007, 07, 11),5},
            new object[]{102, "A3机台", 111, "Testing and debugging", 8, 125, 0, new DateTime(2007, 07, 15),6}
         };
        }
    }



    #region 树
    /// <summary>
    /// 初始化机台列表树
    /// </summary>
    private void InitTreeDept()
    {
        treeEquip.GetRootNode().RemoveAll();
        treeEquip.GetRootNode().AppendChild(getTreeNodeByDelLevel());
    }

    /// <summary>
    /// 获取机台树的算法
    /// </summary>
    /// <param name="dep_num"></param>
    /// <returns></returns>
    private Node getTreeNodeByDelLevel()
    {
        Node node = new Node();
        node.NodeID = "0";
        node.Text = "机台分组";
        node.Expanded = true;
        Dictionary<string, string> depChildFristList = new Dictionary<string, string>(); 
        var query=baseEquipManager.GetListByWhere(BasEquip._.EquipType < "03").GroupBy(pet=>pet.EquipGroup);
        foreach(var info in query) 
        {
            Node childNode = new Node();
            childNode.NodeID = info.Key; 
            childNode.Text = info.Key;
            childNode.Qtip = info.Key;
            childNode.Expanded = true;
            childNode.Leaf = true;
            node.Children.Add(childNode);
        }
        return node;
    }
    #endregion

    public DataTable GetDate()
    {
        DataTable table1= new DataTable();

        table1.Columns.AddRange(new DataColumn[] {
            new DataColumn("ProjectID")   { ColumnName = "ProjectID",    DataType = typeof(int) },
            new DataColumn("Name")     { ColumnName = "Name",      DataType = typeof(string) },
            new DataColumn("TaskID")    { ColumnName = "TaskID",     DataType = typeof(int) },
            new DataColumn("Description") { ColumnName = "Description",  DataType = typeof(string) },
            new DataColumn("Estimate") { ColumnName = "Estimate", DataType = typeof(int) },
            new DataColumn("Rate")    { ColumnName = "Rate",     DataType = typeof(double) },
            new DataColumn("Cost") { ColumnName = "Cost",  DataType = typeof(double) },
            new DataColumn("Due") { ColumnName = "Due", DataType = typeof(DateTime) },
            new DataColumn("SerialNum"){ ColumnName = "SerialNum", DataType = typeof(int) }
           
        });

        foreach (object[] obj in this.Data)
        {
            table1.Rows.Add(obj);
        }
        return table1;
    }




    /// <summary>
    /// 相应点击机台树事件
    /// </summary>
    /// <param name="group"></param>
    [DirectMethod]
    public void LoadGridData(string group)
    {
        //判断当前机台当前时间是否设置班组信息
        //hidden_parent_num.Value = group;
        clZao.Text = "早-甲";
        string planDate = this.txtStratPlanDate.Text;
        WhereClip where = BasEquip._.EquipGroup == group;
        //EntityArrayList<BasEquip> list=baseEquipManager.GetListByWhere(where);
        //pptPlanManager.GetDataSetByWhere("EquipGroup='"+group+"'").Tables[0];
        DataTable dataSource = new DataTable();
        dataSource = pptPlanManager.GetEquipPlan(group, planDate).Tables[0];

        System.Data.DataView dv = dataSource.DefaultView;
        dv.RowFilter = "ShiftID = '1' or ShiftID is null";
        //1.过滤后直接获取DataTable
        DataTable zaoTable = dv.ToTable();
        dv.RowFilter = "ShiftID = '2' or ShiftID is null";
        DataTable zhongTable = dv.ToTable();

        
        dv.RowFilter = "ShiftID = '3' or ShiftID is null";
        DataTable yeTable = dv.ToTable();

        this.ZaoStore.DataSource = zaoTable;
        this.ZaoStore.DataBind();

        this.ZhongStore.DataSource = zhongTable;
        this.ZhongStore.DataBind();
        this.YeStore.DataSource = yeTable;
        this.YeStore.DataBind();


        
    }


    /// <summary>
    /// 点击添加按钮激发的事件
    /// yuany   2013年1月22日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_add_Click(object sender, EventArgs e)
    {
    }

     /// <summary>
    /// 点击修改信息中保存按钮激发的事件
    /// 孙宜建   2013年2月16日
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnModifySave_Click(object sender, EventArgs e)
    {
    }
}