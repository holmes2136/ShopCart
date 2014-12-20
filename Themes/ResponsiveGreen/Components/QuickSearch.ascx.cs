using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.WebAppLib;
using Vevo.WebUI;

public partial class Themes_ResponsiveGreen_Components_QuickSearch : Vevo.WebUI.International.BaseLanguageUserControl
{
    private void PopulateCategory()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "DisplayCategoryInQuickSearch", StoreContext.CurrentStore ))
        {
            string rootID = DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() );
            uxCategoryDropDownList.Items.Add( new ListItem( "All", rootID ) );

            IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByParentID( StoreContext.Culture, rootID, "SortOrder", BoolFilter.ShowTrue );

            for (int i = 0; i < categoryList.Count; i++)
            {
                uxCategoryDropDownList.Items.Add( new ListItem( categoryList[i].Name, categoryList[i].CategoryID ) );
            }
        }
        else
        {
            uxQuickSearchCategoryDropDiv.Visible = false;
        }
    }

    private string LimitDisplayCharactor( string input, short characterLimit )
    {
        if (input.Length > characterLimit)
        {
            string tempString = input.Substring( 0, characterLimit ).Trim();

            int trimOffset = tempString.LastIndexOf( " " );

            if (trimOffset > 0)
            {
                input = tempString.Substring( 0, trimOffset );
            }

            input += " ...";
        }

        return input;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        WebUtilities.TieButton( this.Page, uxSearchText, uxSearchButton.FindControl( "uxLinkButton" ) );

        if (!DataAccessContext.Configurations.GetBoolValue( "SearchModuleDisplay" ))
            this.Visible = false;

        if (!IsPostBack)
        {
            uxQuickSearchCategoryDropDiv.Attributes.Add( "class", "QuickSearchDropDownDiv" );
            uxSearchText.Attributes.Add( "class", "QuickSearchText" );
            PopulateCategory();
        }
    }

    protected void uxSearchButton_Click( object sender, EventArgs e )
    {
        string keyword = HttpUtility.UrlEncode( uxSearchText.Text );

        if (!DataAccessContext.Configurations.GetBoolValue( "DisplayCategoryInQuickSearch", StoreContext.CurrentStore ))
        {
            Response.Redirect( "~/SearchResult.aspx?Search=" + keyword );
        }
        else
        {
            string categoryIDs = "";

            IList<string> categoryIDList = new List<string>();
            categoryIDList = DataAccessContext.CategoryRepository.GetLeafFromCategoryID( uxCategoryDropDownList.SelectedValue, categoryIDList );

            foreach (string categoryID in categoryIDList)
                categoryIDs += categoryID + "+";

            string redirectURL = "~/AdvancedSearchResult.aspx?" +
            "Type=any" +
            "&CategoryIDs=" + categoryIDs +
            "&Keyword=" + keyword +
            "&Price1=" +
            "&Price2=" +
            "&ContentMenuItemIDs=" +
            "&DepartmentIDs=" +
            "&ManufacturerID=" +
            "&SearchType=" +
            "&Quick=true" +
            "&IsNewSearch=false";
            Response.Redirect( redirectURL );
        }
    }

    protected void uxCategoryDropDownList_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxCategoryDropDownList.SelectedValue == DataAccessContext.Configurations.GetValue( "RootCategory", new StoreRetriever().GetStore() ))
        {
            uxQuickSearchCategoryDropDiv.Attributes.Add( "class", "QuickSearchDropDownDiv" );
            uxSearchText.Attributes.Add( "class", "QuickSearchText" );
        }
        else
        {
            uxQuickSearchCategoryDropDiv.Attributes.Add( "class", "QuickSearchSelectedDropDownDiv" );
            uxSearchText.Attributes.Add( "class", "QuickSearchSelectedText" );
            uxCategoryDropDownList.SelectedItem.Text = LimitDisplayCharactor( uxCategoryDropDownList.SelectedItem.Text, 11 );
        }
    }
}
