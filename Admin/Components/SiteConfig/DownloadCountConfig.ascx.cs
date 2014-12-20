using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Vevo;
using Vevo.Domain;
using Vevo.Domain.Configurations;
using Vevo.Shared.Utilities;

public partial class Admin_Components_SiteConfig_DownloadCountConfig
    : AdminAdvancedBaseUserControl, IConfigUserControl
{
    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void uxDownloadCountUnlimitDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        uxNumberOfDownloadCountPanel.Visible
            = !ConvertUtilities.ToBoolean( uxDownloadCountUnlimitDrop.SelectedValue );
    }

    #region IConfigUserControl Members

    public void PopulateControls()
    {
        uxDownloadCountUnlimitDrop.SelectedValue
            = DataAccessContext.Configurations.GetValue( "IsUnlimitDownload" );
        uxNumberOfDownloadCountText.Text
            = DataAccessContext.Configurations.GetValue( "NumberOfDownloadCount" );

        if (ConvertUtilities.ToBoolean( DataAccessContext.Configurations.GetValue( "IsUnlimitDownload" ) ))
            uxNumberOfDownloadCountPanel.Visible = false;
        else
            uxNumberOfDownloadCountPanel.Visible = true;

    }

    public void Populate( Vevo.Domain.Configurations.Configuration config )
    {
        PopulateControls();
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["IsUnlimitDownload"],
             uxDownloadCountUnlimitDrop.SelectedValue );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["NumberOfDownloadCount"],
             uxNumberOfDownloadCountText.Text );
    }

    #endregion
}
