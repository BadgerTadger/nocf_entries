﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using nocf_entries.cls;

namespace nocf_entries.Admin
{
    public partial class ClassNames : System.Web.UI.Page
    {
        string _mode = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!User.IsInRole("Admin"))
            {
                Response.Redirect("~/Default");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    int classNameID = 0;
                    int.TryParse(Request.QueryString["classnameid"], out classNameID);
                    int showID = 0;
                    int.TryParse(Request.QueryString["showid"], out showID);

                    _mode = Request.QueryString["Mode"] == null ? "" : Request.QueryString["Mode"].ToString().ToLowerInvariant();
                    switch (_mode)
                    {
                        case "a":
                            phView.Visible = false;
                            phEdit.Visible = true;
                            phSelect.Visible = false;
                            break;
                        case "e":
                            PopulateEditFields(classNameID);
                            phView.Visible = false;
                            phEdit.Visible = true;
                            phSelect.Visible = false;
                            break;
                        case "s":
                            PopulateClassList(showID);
                            phView.Visible = false;
                            phEdit.Visible = false;
                            phSelect.Visible = true;
                            break;
                        default:
                            PopulateViewFields(classNameID);
                            phEdit.Visible = false;                            
                            phView.Visible = true;
                            phSelect.Visible = false;
                            break;
                    }
                }
            }
        }

        private void PopulateClassList(int showID)
        {
            clsShow show = new clsShow();
            show.Load(showID);
            DataTable classList = clsClassName.GetClassesForSelection(show);
            rptrClasses.DataSource = classList;
            rptrClasses.DataBind();
        }

        private void PopulateViewFields(int classNameID)
        {
            rptrClassNames.DataSource = clsClassName.GetClassNameList();
            rptrClassNames.DataBind();
        }

        private void PopulateEditFields(int classNameID)
        {
            if(classNameID > 0)
            {
                clsClassName className = new clsClassName();
                className.Load(classNameID);
                txtClassName.Text = className.Class_Name_Description;
                txtWeighting.Text = className.Weighting.ToString();
                PopulateGenderList(className);
            }
        }

        private void PopulateGenderList(clsClassName className)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Descr");
            dt.Columns.Add("Value");

            DataRow rowDog = dt.NewRow();
            rowDog["Descr"] = "Dog";
            rowDog["Value"] = "1";
            dt.Rows.Add(rowDog);

            DataRow rowBitch = dt.NewRow();
            rowBitch["Descr"] = "Bitch";
            rowBitch["Value"] = "2";
            dt.Rows.Add(rowBitch);

            DataRow rowDandB = dt.NewRow();
            rowDandB["Descr"] = "Dog & Bitch";
            rowDandB["Value"] = "3";
            dt.Rows.Add(rowDandB);

            ddlGender.DataValueField = "Value";
            ddlGender.DataTextField = "Descr";
            ddlGender.DataSource = dt;
            ddlGender.DataBind();
            ddlGender.SelectedValue = className.Gender.ToString();
        }

        protected void btnAddClassName_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/ClassNames?mode=a");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                int classNameID = 0;
                int.TryParse(Request.QueryString["classnameid"], out classNameID);
                clsClassName className = new clsClassName();
                className.Class_Name_ID = classNameID;
                className.Class_Name_Description = txtClassName.Text;
                className.Weighting = int.Parse(txtWeighting.Text);
                className.Gender = int.Parse(ddlGender.SelectedValue);
                className.Save();
                Response.Redirect("~/Admin/ClassNames");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/ClassNames");
        }

        protected void rptrClassNames_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Response.Redirect("~/Admin/ClassNames?mode=e&classnameid=" + e.CommandName);
        }

        protected void btnSaveSelection_Click(object sender, EventArgs e)
        {
            int eventID = 0;
            int.TryParse(Request.QueryString["eventid"], out eventID);
            int showID = 0;
            int.TryParse(Request.QueryString["showid"], out showID);
            bool isError = false;

            decimal defaultClassCost = 0;
            int defaultClassCap = 0;
            int defaultDogsPerClassPart = 0;

            //Validate
            foreach (RepeaterItem ri in rptrClasses.Items)
            {
                HiddenField hdnDefaultClassCost = ri.FindControl("hdnDefaultClassCost") as HiddenField;
                decimal.TryParse(hdnDefaultClassCost.Value, out defaultClassCost);
                HiddenField hdnDefaultClassCap = ri.FindControl("hdnDefaultClassCap") as HiddenField;
                int.TryParse(hdnDefaultClassCap.Value, out defaultClassCap);
                HiddenField hdnDefaultDogsPerClassPart = ri.FindControl("hdnDefaultDogsPerClassPart") as HiddenField;
                int.TryParse(hdnDefaultDogsPerClassPart.Value, out defaultDogsPerClassPart);

                CheckBox chk = ri.FindControl("chkClassName") as CheckBox;
                if (chk.Checked)
                {
                    int classNo = -1;
                    TextBox txtClassNo = ri.FindControl("txtClassNo") as TextBox;
                    int.TryParse(txtClassNo.Text.ToString(), out classNo);

                    Label lblError = ri.FindControl("lblError") as Label;
                    if (classNo == -1)
                    {
                        lblError.Text = "Class Number is Required";
                        isError = true;
                    }
                    else
                    {
                        lblError.Text = "";
                    }
                }
            }

            //Save
            if (!isError)
            {
                clsShowClass.DeleteClassesForShow(showID);

                clsShowClass showClass = new clsShowClass();
                showClass.ClassCost = defaultClassCost;
                showClass.ClassCap = 9999;
                showClass.DogsPerClassPart = 0;
                showClass.Judges = "N/A";
                showClass.AddNFC(showID);
                

                foreach (RepeaterItem ri in rptrClasses.Items)
                {
                    CheckBox chk = ri.FindControl("chkClassName") as CheckBox;
                    if (chk.Checked)
                    {
                        int showClassID = 0;
                        //HiddenField hdnshowClassID = ri.FindControl("hdnShowClassID") as HiddenField;
                        //int.TryParse(hdnshowClassID.Value.ToString(), out showClassID);

                        int classNameID = 0;
                        HiddenField hdnClassNameID = ri.FindControl("hdnClassNameID") as HiddenField;
                        int.TryParse(hdnClassNameID.Value.ToString(), out classNameID);

                        int classNo = 0;
                        TextBox txtClassNo = ri.FindControl("txtClassNo") as TextBox;
                        int.TryParse(txtClassNo.Text.ToString(), out classNo);

                        showClass = new clsShowClass();
                        showClass.Judges = "";
                        showClass.ClassCost = defaultClassCost;
                        showClass.ClassCap = defaultClassCap;
                        showClass.DogsPerClassPart = defaultDogsPerClassPart;
                        showClass.SaveSelected(showClassID, showID, classNameID, classNo);
                    }
                }

                Response.Redirect("~/Admin/Shows.aspx?eventid=" + eventID + "&showid=" + showID);
            }
        }

        protected void rptrClasses_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
                     e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //DataTable dt = new DataTable();
                //dt.Columns.Add("Descr");
                //dt.Columns.Add("Value");

                //DataRow rowDog = dt.NewRow();
                //rowDog["Descr"] = "Dog";
                //rowDog["Value"] = "1";
                //dt.Rows.Add(rowDog);

                //DataRow rowBitch = dt.NewRow();
                //rowBitch["Descr"] = "Bitch";
                //rowBitch["Value"] = "2";
                //dt.Rows.Add(rowBitch);

                //DataRow rowDandB = dt.NewRow();
                //rowDandB["Descr"] = "Dog & Bitch";
                //rowDandB["Value"] = "3";
                //dt.Rows.Add(rowDandB);

                //((DropDownList)e.Item.FindControl("ddlGender")).DataValueField = "Value";
                //((DropDownList)e.Item.FindControl("ddlGender")).DataTextField = "Descr";
                //((DropDownList)e.Item.FindControl("ddlGender")).DataSource = dt;
                //((DropDownList)e.Item.FindControl("ddlGender")).DataBind();
                //((DropDownList)e.Item.FindControl("ddlGender")).SelectedValue = DataBinder.Eval(e.Item.DataItem, "Gender").ToString();
            }
        }

        protected void btnCancelSelection_Click(object sender, EventArgs e)
        {
            int eventID = 0;
            int.TryParse(Request.QueryString["eventid"], out eventID);
            int showID = 0;
            int.TryParse(Request.QueryString["showid"], out showID);
            Response.Redirect("~/Admin/Shows.aspx?eventid=" + eventID + "&showid=" + showID);
        }

        protected void ClassWeightingFormatValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bool argsValid = true;
            if (!string.IsNullOrEmpty(args.Value))
            {
                int weighting = 0;
                if (!int.TryParse(args.Value, out weighting))
                {
                    argsValid = false;
                }
            }
            args.IsValid = argsValid;
        }
    }
}