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

public partial class Components_QuickSearch : Vevo.WebUI.International.BaseLanguageUserControl
{
    private string _keywordText = "[$Search]";

    private void RegisterScript()
    {
        uxSearchText.Text = _keywordText;
        uxSearchText.Attributes.Add( "onblur", "javascript:if(this.value=='') this.value='" + _keywordText + "';" );
        uxSearchText.Attributes.Add( "onfocus", "javascript:if(this.value=='" + _keywordText + "') this.value='';" );
    }

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

    protected void Page_Load( object sender, EventArgs e )
    {
        WebUtilities.TieButton( this.Page, uxSearchText, uxSearchButton.FindControl( "uxLinkButton" ) );

        if (!DataAccessContext.Configurations.GetBoolValue( "SearchModuleDisplay" ))
            this.Visible = false;

        if (!IsPostBack)
        {
            RegisterScript();
            uxStartKeywordTextHidden.Value = _keywordText;
            PopulateCategory();
        }
    }

    protected void uxSearchButton_Click( object sender, EventArgs e )
    {
        string keyword = HttpUtility.UrlEncode( uxSearchText.Text );

        if (uxSearchText.Text == uxStartKeywordTextHidden.Value)
        {
            keyword = "";
        }

        if (!DataAccessContext.Configurations.GetBoolValue( "DisplayCategoryInQuickSearch", StoreContext.CurrentStore ))
        {
            Response.Redirect( "SearchResult.aspx?Search=" + keyword );
        }
        else
        {
            string categoryIDs = "";

            IList<string> categoryIDList = new List<string>();
            categoryIDList = DataAccessContext.CategoryRepository.GetLeafFromCategoryID( uxCategoryDropDownList.SelectedValue, categoryIDList );

            foreach (string categoryID in categoryIDList)
                categoryIDs += categoryID + "+";

            string redirectURL = "AdvancedSearchResult.aspx?" +
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
}
