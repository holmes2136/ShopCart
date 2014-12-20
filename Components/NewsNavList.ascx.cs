using System;
using Vevo.Domain;
using Vevo.Domain.Stores;
using Vevo.WebUI;

public partial class Components_NewsNavList : Vevo.WebUI.International.BaseLanguageUserControl
{
    private void PopulateControls()
    {
        uxList.DataSource = DataAccessContext.NewsRepository.GetLatestNews( "5",
            StoreContext.Culture, new StoreRetriever().GetCurrentStoreID() );
        uxList.DataBind();
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
        }
    }

    public void Refresh()
    {
        PopulateControls();
    }
}