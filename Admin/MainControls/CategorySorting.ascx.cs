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

public partial class AdminAdvanced_MainControls_CategorySorting : AdminAdvancedBaseUserControl
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
        PopulateListControls( "CategoryID" );
    }

    protected void uxSaveButton_Click( object sender, EventArgs e )
    {
        try
        {
            string[] result;
            if (!String.IsNullOrEmpty( uxStatusHidden.Value ))
            {
                string checkValue = uxStatusHidden.Value.Replace( "category[]=", "" );
                result = checkValue.Split( '&' );
            }
            else
            {
                MatchCollection mc;
                Regex r = new Regex( @"category_(\d+)" );
                mc = r.Matches( uxListLabel.Text );
                result = new string[mc.Count];
                for (int i = 0; i < mc.Count; i++)
                {
                    result[i] = mc[i].Value.Replace( "category_", "" );
                }
            }

            DataAccessContext.CategoryRepository.UpdateSortOrder( result );
            PopulateListControls( "SortOrder" );
            HttpContext.Current.Session[SystemConst.CategoryTreeViewLeftKey] = null;
            uxMessage.DisplayMessage( "Update sort order successfully." );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayError( ex.Message );
        }
    }

    protected void uxCancelButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "CategoryList.ascx" );
    }

    private void PopulateControl()
    {
        uxRootCategoryDrop.Items.Clear();
        IList<Category> rootCategoryList =
            DataAccessContext.CategoryRepository.GetRootCategory( uxLanguageControl.CurrentCulture, "CategoryID", BoolFilter.ShowAll );

        foreach (Category rootCategory in rootCategoryList)
        {
            uxRootCategoryDrop.Items.Add( new ListItem( rootCategory.Name, rootCategory.CategoryID ) );
        }

        uxCategoryDrop.Items.Clear();
        uxCategoryDrop.Items.Add( new ListItem( "Root", uxRootCategoryDrop.SelectedValue, true ) );

        IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByRootIDNotLeaf(
                    uxLanguageControl.CurrentCulture, uxRootCategoryDrop.SelectedValue, "SortOrder", BoolFilter.ShowAll );

        for (int i = 0; i < categoryList.Count; i++)
        {
            uxCategoryDrop.Items.Add( new ListItem(
                categoryList[i].CreateFullCategoryPath() + " (" + categoryList[i].CategoryID + ")",
                categoryList[i].CategoryID ) );
        }

        if (!KeyUtilities.IsMultistoreLicense())
            uxRootCategoryFilterPanel.Visible = false;

        PopulateListControls( "SortOrder" );
    }

    private void PopulateListControls( string sortBy )
    {
        uxListLabel.Text = "";
        uxStatusHidden.Value = "";

        IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByParentID(
            uxLanguageControl.CurrentCulture,
            uxCategoryDrop.SelectedValue,
            sortBy,
            BoolFilter.ShowTrue );

        string categoryListString = String.Empty;
        ArrayList categoryControlID = new ArrayList();

        if (categoryList.Count > 0)
        {
            foreach (Category category in categoryList)
            {
                categoryListString += String.Format( "<li id='category_{0}'>{1} {2}</li>", category.CategoryID, category.CategoryID, category.Name );
            }
        }
        uxListLabel.Text = String.Format( "<ul id='sortCategoryList'>{0}</ul>", categoryListString );

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
        sb.AppendLine( "$(\"#sortCategoryList\").sortable({" );
        sb.AppendLine( "stop: function() { Sortable_Changed(); }" );
        sb.AppendLine( "});" );
        sb.AppendLine( "});" );

        sb.AppendLine( "function Sortable_Changed()" );
        sb.AppendLine( "{" );
        sb.AppendLine( "var hiddenfield = document.getElementById( '" + uxStatusHidden.ClientID + "' );" );
        sb.AppendLine( "var state = $(\"#sortCategoryList\").sortable( 'serialize' )" );
        sb.AppendLine( "hiddenfield.value = state;" );
        sb.AppendLine( "}" );

        ScriptManager.RegisterStartupScript( this, typeof( Page ), "sortingListScript", sb.ToString(), true );
    }

    protected void uxCategoryDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        PopulateListControls( "SortOrder" );
    }

    protected void uxRootCategoryDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        uxCategoryDrop.Items.Clear();
        uxCategoryDrop.Items.Add( new ListItem( "Root", uxRootCategoryDrop.SelectedValue, true ) );

        IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByRootIDNotLeaf(
                    uxLanguageControl.CurrentCulture, uxRootCategoryDrop.SelectedValue, "SortOrder", BoolFilter.ShowAll );

        for (int i = 0; i < categoryList.Count; i++)
        {
            uxCategoryDrop.Items.Add( new ListItem(
                categoryList[i].CreateFullCategoryPath() + " (" + categoryList[i].CategoryID + ")",
                categoryList[i].CategoryID ) );
        }

        PopulateListControls( "SortOrder" );
    }
}
