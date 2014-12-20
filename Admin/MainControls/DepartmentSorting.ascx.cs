using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.DataAccess;
using Vevo.WebUI;

public partial class AdminAdvanced_MainControls_DepartmentSorting : AdminAdvancedBaseUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {
        uxLanguageControl.BubbleEvent += new EventHandler( Details_RefreshHandler );

        if (!MainContext.IsPostBack)
            PopulateControl();

        RegisterJavaScript();

        if (!IsAdminModifiable())
        {
            uxSortByNameButton.Visible = false;
            uxSortByIDButton.Visible = false;
            uxSaveButton.Visible = false;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {

    }

    protected void uxSortByNameButton_Click( object sender, EventArgs e )
    {
        PopulateListControls( "Name" );
    }

    protected void uxSortByIDButton_Click( object sender, EventArgs e )
    {
        PopulateListControls( "DepartmentID" );
    }

    protected void uxSaveButton_Click( object sender, EventArgs e )
    {
        try
        {
            string[] result;
            if (!String.IsNullOrEmpty( uxStatusHidden.Value ))
            {
                string checkValue = uxStatusHidden.Value.Replace( "department[]=", "" );
                result = checkValue.Split( '&' );
            }
            else
            {
                MatchCollection mc;
                Regex r = new Regex( @"department_(\d+)" );
                mc = r.Matches( uxListLabel.Text );
                result = new string[mc.Count];
                for (int i = 0; i < mc.Count; i++)
                {
                    result[i] = mc[i].Value.Replace( "department_", "" );
                }
            }

            DataAccessContext.DepartmentRepository.UpdateSortOrder( result );
            PopulateListControls( "SortOrder" );
            HttpContext.Current.Session[SystemConst.DepartmentTreeViewLeftKey] = null;
            uxMessage.DisplayMessage( "Update sort order successfully." );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayError( ex.Message );
        }
    }

    protected void uxCancelButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "DepartmentList.ascx" );
    }

    private void PopulateControl()
    {
        uxRootDepartmentDrop.Items.Clear();
        IList<Department> rootDepartmentList =
            DataAccessContext.DepartmentRepository.GetRootDepartment( uxLanguageControl.CurrentCulture, "DepartmentID", BoolFilter.ShowAll );

        foreach (Department rootDepartment in rootDepartmentList)
        {
            uxRootDepartmentDrop.Items.Add( new ListItem( rootDepartment.Name, rootDepartment.DepartmentID ) );
        }

        uxDepartmentDrop.Items.Clear();
        uxDepartmentDrop.Items.Add( new ListItem( "Root", uxRootDepartmentDrop.SelectedValue, true ) );

        IList<Department> departmentList = DataAccessContext.DepartmentRepository.GetByRootIDNotLeaf(
                    uxLanguageControl.CurrentCulture, uxRootDepartmentDrop.SelectedValue, "SortOrder", BoolFilter.ShowAll );

        for (int i = 0; i < departmentList.Count; i++)
        {
            uxDepartmentDrop.Items.Add( new ListItem(
                departmentList[i].CreateFullDepartmentPath() + " (" + departmentList[i].DepartmentID + ")",
                departmentList[i].DepartmentID ) );
        }

        if (!KeyUtilities.IsMultistoreLicense())
            uxRootDepartmentFilterPanel.Visible = false;

        PopulateListControls( "SortOrder" );
    }

    private void PopulateListControls( string sortBy )
    {
        uxListLabel.Text = "";
        uxStatusHidden.Value = "";

        IList<Department> departmentList = DataAccessContext.DepartmentRepository.GetByParentID(
            uxLanguageControl.CurrentCulture,
            uxDepartmentDrop.SelectedValue,
            sortBy,
            BoolFilter.ShowTrue );

        string departmentListString = String.Empty;
        ArrayList departmentControlID = new ArrayList();

        if (departmentList.Count > 0)
        {
            foreach (Department department in departmentList)
            {
                departmentListString += String.Format( "<li id='department_{0}'>{1} {2}</li>", department.DepartmentID, department.DepartmentID, department.Name );
            }
        }
        uxListLabel.Text = String.Format( "<ul id='sortDepartmentList'>{0}</ul>", departmentListString );

        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Test)
            uxMessage.DisplayMessage( "Test Sort Success" );
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControl();
    }

    private void RegisterJavaScript()
    {
        RegisterCustomScript();
    }

    private void RegisterCustomScript()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine( "$(document).ready(function(){" );
        sb.AppendLine( "$(\"#sortDepartmentList\").sortable({" );
        sb.AppendLine( "stop: function() { Sortable_Changed(); }" );
        sb.AppendLine( "});" );
        sb.AppendLine( "});" );

        sb.AppendLine( "function Sortable_Changed()" );
        sb.AppendLine( "{" );
        sb.AppendLine( "var hiddenfield = document.getElementById( '" + uxStatusHidden.ClientID + "' );" );
        sb.AppendLine( "var state = $(\"#sortDepartmentList\").sortable( 'serialize' )" );
        sb.AppendLine( "hiddenfield.value = state;" );
        sb.AppendLine( "}" );

        ScriptManager.RegisterStartupScript( this, typeof( Page ), "sortingListScript", sb.ToString(), true );
    }

    protected void uxDepartmentDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        PopulateListControls( "SortOrder" );
    }

    protected void uxRootDepartmentDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        uxDepartmentDrop.Items.Clear();
        uxDepartmentDrop.Items.Add( new ListItem( "Root", uxRootDepartmentDrop.SelectedValue, true ) );

        IList<Department> departmentList = DataAccessContext.DepartmentRepository.GetByRootIDNotLeaf(
                    uxLanguageControl.CurrentCulture, uxRootDepartmentDrop.SelectedValue, "SortOrder", BoolFilter.ShowAll );

        for (int i = 0; i < departmentList.Count; i++)
        {
            uxDepartmentDrop.Items.Add( new ListItem(
                departmentList[i].CreateFullDepartmentPath() + " (" + departmentList[i].DepartmentID + ")",
                departmentList[i].DepartmentID ) );
        }

        PopulateListControls( "SortOrder" );
    }
}
