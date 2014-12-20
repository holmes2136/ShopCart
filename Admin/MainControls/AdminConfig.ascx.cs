using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
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
using Vevo.Shared.DataAccess;
using Vevo.WebAppLib;

public partial class AdminAdvanced_MainControls_AdminConfig : AdminAdvancedBaseUserControl
{
    private void RefreshDetails()
    {
        uxEnableErrorLogEmailDrop.SelectedValue =
            DataAccessContext.Configurations.GetBoolValue( "EnableErrorLogEmail" ).ToString();
        uxErrorLogEmailText.Text =
            DataAccessContext.Configurations.GetValue( "ErrorLogEmail" );
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            if (Membership.GetUser().UserName.ToLower() == "admin")
            {
                RefreshDetails();
            }
            else
                uxAdminConfigPanel.Visible = false;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        try
        {
            DataAccessContext.ConfigurationRepository.UpdateValue(
                 DataAccessContext.Configurations["EnableErrorLogEmail"],
                uxEnableErrorLogEmailDrop.SelectedValue );

            DataAccessContext.ConfigurationRepository.UpdateValue(
                 DataAccessContext.Configurations["ErrorLogEmail"],
                uxErrorLogEmailText.Text );

            uxMessage.DisplayMessage( Resources.SetupMessages.UpdateSuccess );
        }
        catch (DataAccessException ex)
        {
            uxMessage.DisplayError( "Error:<br/>" + ex.Message );
        }
        catch
        {
            uxMessage.DisplayError( Resources.SetupMessages.UpdateError );
        }

        AdminUtilities.LoadSystemConfig();
        RefreshDetails();
    }
}
