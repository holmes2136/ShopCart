using System;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class Components_IntroductionMessage : BaseLanguageUserControl
{
    private void PopulateControls()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "EnableSiteGreetingText", StoreContext.CurrentStore ))
        {
            uxIntroductionMessagePanel.Visible = true;

            uxIntroductionMessageTitle.Text = DataAccessContext.Configurations.GetValue(
                StoreContext.Culture.CultureID, "CompanyName", StoreContext.CurrentStore );
            uxIntroductionMessageText.Text = DataAccessContext.Configurations.GetValue(
                StoreContext.Culture.CultureID, "SiteGreetingText", StoreContext.CurrentStore );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!IsPostBack)
        {
            PopulateControls();
        }
    }
}