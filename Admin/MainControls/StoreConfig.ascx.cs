using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.WebAppLib;
using Vevo.Domain;
using Vevo.Shared.DataAccess;

public partial class Admin_MainControls_StoreConfig : AdminAdvancedBaseUserControl
{

    private void PopulateControls()
    {
        if (!KeyUtilities.IsDeluxeLicense( DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
            uxPointSystemTab.Visible = false;
        uxDisplay.PopulateControls();
        uxStoreLayoutConfig.PopulateControls();
        uxStoreProfileConfig.PopulateControls();
        uxEmailSetup.PopulateControls();
        uxFacebook.PopulateControls();
        uxBlogConfig.PopulateControls();
        uxPointSystemConfig.PopulateControls();
        uxSeoConfig.PopulateControls();
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private string FindSearchText()
    {
        return uxSearchText.Text;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxLanguageControl.BubbleEvent += new EventHandler( Details_RefreshHandler );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateControls();
        }

        if (!IsAdminModifiable())
        {
            uxUpdateButton.Visible = false;
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        try
        {
            string message = string.Empty;
            if (!uxEmailSetup.Update( out message ))
            {
                uxMessage.DisplayError( message );
                return;
            }

            uxDisplay.Update();
            uxStoreLayoutConfig.Update();
            uxStoreProfileConfig.Update();
            uxFacebook.Update();
            uxBlogConfig.Update();
            uxPointSystemConfig.Update();
            uxSeoConfig.Update();

            DataAccessContext.ClearConfigurationCache();
            AdminUtilities.LoadSystemConfig();

            uxMessage.DisplayMessage( Resources.SetupMessages.UpdateSuccess );
            PopulateControls();
            SystemConfig.Load();
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxSearchButton_Click( object sender, EventArgs e )
    {
        if (FindSearchText() != "")
            MainContext.RedirectMainControl( "SearchConfigurationResult.ascx",
                String.Format( "Search={0}&Store={1}&RedirectURL={2}", FindSearchText(), MainContext.QueryString["StoreID"], 
                    MainContext.LastControl ) );
    }
}
