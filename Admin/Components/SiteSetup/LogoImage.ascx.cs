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
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Configurations;
using Vevo.WebAppLib;
using Vevo.Domain.Stores;

public partial class AdminAdvanced_Components_SiteSetup_LogoImage : AdminAdvancedBaseUserControl
{
    private const string _pathUpload = "Images/Configuration/";

    private string StoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
                return MainContext.QueryString["StoreID"];
            }
            else
            {
                return Store.RegularStoreID;
            }
        }
    }
    
    private Store CurrentStore
    {
        get
        {
            return DataAccessContext.StoreRepository.GetOne( StoreID );
        }
    }

    private void ShowImageLogo()
    {
        if (uxLogoImageText.Text != "")
        {
            uxLogoImage.Visible = true;
            uxLogoImage.ImageUrl = "~/" + uxLogoImageText.Text;
        }
        else
            uxLogoImage.Visible = false;
    }

    public void PopulateControls()
    {
        uxLogoImageText.Text = DataAccessContext.Configurations.GetValue( "LogoImage", CurrentStore );
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["LogoImage"],
            uxLogoImageText.Text,
            CurrentStore );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxLogoImageUpload.PathDestination = _pathUpload;
        uxLogoImageUpload.ReturnTextControlClientID = uxLogoImageText.ClientID;
        if (!IsAdminModifiable())
        {
            uxPrimaryUploadLinkButton.Visible = false;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            PopulateControls();

        ShowImageLogo();
    }

    protected void uxPrimaryUploadLinkButton_Click( object sender, EventArgs e )
    {
        uxLogoImageUpload.ShowControl = true;
        uxPrimaryUploadLinkButton.Visible = false;
    }
}
