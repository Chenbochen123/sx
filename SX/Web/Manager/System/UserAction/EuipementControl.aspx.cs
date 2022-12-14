using System;
using System.Collections.Generic;
using Ext.Net;
using Mesnac.Business.Implements;
using Mesnac.Business.Interface;
using Mesnac.Entity;
using NBear.Common;
using Mesnac.Data.Components;
using System.Data;

public partial class Manager_System_UserAction_EuipementControl : System.Web.UI.Page
{
    SysUserCtrlManager ubll = new SysUserCtrlManager();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            EntityArrayList<SysUserCtrl> mlist = ubll.GetAllList();
            foreach (SysUserCtrl model in mlist)
            {
                if (model.TypeID == "SapInterfaceCtrl")
                {
                    this.cbxSapInterfaceCtrl.Checked = model.ItemCode == "0" ? false : true;
                }
                else if (model.TypeID == "PDCtrl")
                {
                    this.cbxpd.Checked = model.ItemCode == "0" ? false : true;
                }
                else if (model.TypeID == "XCCtrl")
                {
                    this.cbxxc.Checked = model.ItemCode == "0" ? false : true;
                }
                //else if (model.TypeID == "FirstInOutCtrl")
                //{
                //    this.cbxFirstInOutCtrl.Checked = model.ItemCode == "0" ? false : true;
                //}重复
                else if (model.TypeID == "FirstInOutCtrl")
                {
                    this.cbxFirstInOutCtrl.Checked = model.ItemCode == "0" ? false : true;
                }
                else if (model.TypeID == "FIFOTime")
                {
                    FIFOTime.Text = model.ItemCode;
                }
                else if (model.TypeID == "FIFOTimeXL")
                {
                    FIFOTimeXL.Text = model.ItemCode;
                }
                else if (model.TypeID == "WeightFlag")
                {
                    this.Checkbox1.Checked = model.ItemCode == "0" ? false : true;
                }
                else if (model.TypeID == "MaterFirstInOut")
                {
                    this.Checkbox2.Checked = model.ItemCode == "0" ? false : true;
                }
                else if (model.TypeID == "ShopFirstInOut")
                {
                    this.Checkbox3.Checked = model.ItemCode == "0" ? false : true;
                }
                else if (model.TypeID == "XBFirstInOut")
                {
                    this.Checkbox4.Checked = model.ItemCode == "0" ? false : true;
                }
                else if (model.TypeID == "RubberX")
                {
                    this.Checkbox5.Checked = model.ItemCode == "0" ? false : true;
                }
                else if (model.TypeID == "RubberM")
                {
                    this.Checkbox6.Checked = model.ItemCode == "0" ? false : true;
                }
                else if (model.TypeID == "RubberZ")
                {
                    this.Checkbox7.Checked = model.ItemCode == "0" ? false : true;
                }
                else if (model.TypeID == "RubberF")
                {
                    this.Checkbox8.Checked = model.ItemCode == "0" ? false : true;
                }
                else if (model.TypeID == "FirstInOutRule")
                {
                    string c = model.ItemCode;
                    string[] carray = c.Split(new string[]{";"},StringSplitOptions.RemoveEmptyEntries);
                    switch (carray[0])
                    { 
                        case "1":
                            this.rdo3.Checked = true;
                            break;
                        case "2":
                             this.rdo2.Checked = true;
                            break;
                        case "3":
                            this.rdo2.Checked = true;
                            break;
                        default:
                            this.rdo3.Checked = true;
                            break;
                    }
                    if (carray.Length > 1)
                    {
                        this.cbxpro.Checked = carray[1] == "1";
                    }
                }



                if (model.TypeID == "Num1")
                {
                   NumberField1.Text = model.ItemCode;
                }
                if (model.TypeID == "Num2")
                {
                    NumberField2.Text = model.ItemCode;
                }
                if (model.TypeID == "Num3")
                {
                    NumberField3.Text = model.ItemCode;
                }
                if (model.TypeID == "Num4")
                {
                    NumberField4.Text = model.ItemCode;
                }
                if (model.TypeID == "Num5")
                {
                    NumberField5.Text = model.ItemCode;
                }
            }
        }
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        SysUserCtrl model = null;
        SysUserCtrl model2 = null;
        SysUserCtrl model3 = null;
        SysUserCtrl model4 = null;
        EntityArrayList<SysUserCtrl> mlist = ubll.GetAllList();
        for (int i = 0; i < mlist.Count;i++ )
        {
            SysUserCtrl m = mlist[i];
            if (m.TypeID == "SapInterfaceCtrl")
            {
                model = m;
            }
            else if (m.TypeID == "FirstInOutCtrl")
            {
                model2 = m;
            }
            else if (m.TypeID == "FirstInOutRule")
            {
                model3 = m;
            }
            if (m.TypeID == "FIFOTime")
            {
                m.ItemCode = FIFOTime.Text;
                ubll.Update(m);
            }
            if (m.TypeID == "WeightFlag")
            {
                model4 = m;
               
            }
            if (m.TypeID == "FIFOTimeXL")
            {
                m.ItemCode = FIFOTimeXL.Text;
                ubll.Update(m);
            }
            if (m.TypeID == "Num1")
            {
                m.ItemCode = NumberField1.Text;
                ubll.Update(m);
            }
            if (m.TypeID == "Num2")
            {
                m.ItemCode = NumberField2.Text;
                ubll.Update(m);
            }
            if (m.TypeID == "Num3")
            {
                m.ItemCode = NumberField3.Text;
                ubll.Update(m);
            }
            if (m.TypeID == "Num4")
            {
                m.ItemCode = NumberField4.Text;
                ubll.Update(m);
            }
            if (m.TypeID == "Num5")
            {
                m.ItemCode = NumberField5.Text;
                ubll.Update(m);
            }

            if (m.TypeID == "MaterFirstInOut")
            {
                m.ItemCode = this.Checkbox2.Checked ? "1" : "0";
                ubll.Update(m);
            }

            if (m.TypeID == "ShopFirstInOut")
            {
                m.ItemCode = this.Checkbox3.Checked ? "1" : "0";
                ubll.Update(m);
            }

            if (m.TypeID == "XBFirstInOut")
            {
                m.ItemCode = this.Checkbox4.Checked ? "1" : "0";
                ubll.Update(m);
            }

            if (m.TypeID == "RubberX")
            {
                m.ItemCode = this.Checkbox5.Checked ? "1" : "0";
                ubll.Update(m);
            }
            if (m.TypeID == "RubberM")
            {
                m.ItemCode = this.Checkbox6.Checked ? "1" : "0";
                ubll.Update(m);
            }
            if (m.TypeID == "RubberZ")
            {
                m.ItemCode = this.Checkbox7.Checked ? "1" : "0";
                ubll.Update(m);
            }
            if (m.TypeID == "RubberF")
            {
                m.ItemCode = this.Checkbox8.Checked ? "1" : "0";
                ubll.Update(m);
            }
            if (m.TypeID == "PDCtrl")
            {
                m.ItemCode = this.cbxpd.Checked ? "1" : "0";
                ubll.Update(m);
            }
            if (m.TypeID == "XCCtrl")
            {
                m.ItemCode = this.cbxxc.Checked ? "1" : "0";
                ubll.Update(m);
            }
        }
        if (model != null)
        {
            model.ItemCode = this.cbxSapInterfaceCtrl.Checked ? "1" : "0";
            ubll.Update(model);
        }
        if (model2 != null)
        {
            model2.ItemCode = this.cbxFirstInOutCtrl.Checked ? "1" : "0";
            ubll.Update(model2);
        }
        if (model4 != null)
        {
            model4.ItemCode = this.Checkbox1.Checked ? "1" : "0";
            ubll.Update(model4);
        }
        if (model3 != null)
        {
            string inout = "";
            if (rdo1.Checked)
            {
                inout = "3;" + (this.cbxpro.Checked ? "1" : "0");
            }
            else if (rdo2.Checked)
            {
                inout = "2;" + (this.cbxpro.Checked ? "1" : "0");
            }
            else if (rdo3.Checked)
            {
                inout = "1;" + (this.cbxpro.Checked ? "1" : "0");
            }
            model3.ItemCode = inout;
            ubll.Update(model3);
        }
        X.MessageBox.Show(new MessageBoxConfig { Title = "提示", Message = "操作成功！", Buttons = MessageBox.Button.OK, Icon = MessageBox.Icon.INFO });
    }
}