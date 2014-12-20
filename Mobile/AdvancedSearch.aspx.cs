using System;

public partial class Mobile_AdvancedSearch : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    private void ResultSearch()
    {
        Response.Redirect( "AdvancedSearchResult.aspx?" +
            "Keyword=" + uxKeywordText.Text +
            "&Price1=" + uxPrice1Text.Text +
            "&Price2=" + uxPrice2Text.Text );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void uxSearchButton_Click( object sender, EventArgs e )
    {
        ResultSearch();
    }
}
