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
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Configurations;
using Vevo.WebAppLib;
using Vevo;
using Vevo.Domain.Stores;

public partial class AdminAdvanced_Components_SiteSetup_EmailSetup : AdminAdvancedBaseUserControl
{
    private Store CurrentStore
    {
        get
        {
            return DataAccessContext.StoreRepository.GetOne( StoreID );
        }
    }

    public string StoreID
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

    public string CultureID
    {
        get
        {
            if (ViewState["CultureID"] == null)
                return String.Empty;
            else
                return (String) ViewState["CultureID"];
        }
        set { ViewState["CultureID"] = value; }
    }

    public string ValidationGroup
    {
        get
        {
            if (ViewState["ValidationGroup"] == null)
                return String.Empty;
            else
                return (String) ViewState["ValidationGroup"];
        }
        set { ViewState["ValidationGroup"] = value; }
    }

    public void PopulateControls()
    {
        uxSmtpAuthenRequiredDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "SmtpAuthenRequired", CurrentStore );
        uxSmtpUserNameText.Text = DataAccessContext.Configurations.GetValue( "SmtpUserName", CurrentStore );
        uxHiddenPassword.Value = DataAccessContext.Configurations.GetValue( "SmtpPassword", CurrentStore );
        uxSmtpServerText.Text = DataAccessContext.Configurations.GetValue( "SmtpServer", CurrentStore );
        uxSmtpPortText.Text = DataAccessContext.Configurations.GetValue( "SmtpPort", CurrentStore );
        uxRequireEmailSslDrop.SelectedValue = DataAccessContext.Configurations.GetValue( "SmtpSslRequired", CurrentStore );
    }

    public bool Update( out string message )
    {
        message = String.Empty;
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["SmtpServer"],
            uxSmtpServerText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["SmtpPort"],
            uxSmtpPortText.Text,
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["SmtpAuthenRequired"],
            uxSmtpAuthenRequiredDrop.SelectedValue.ToString(),
            CurrentStore );

        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["SmtpSslRequired"],
            uxRequireEmailSslDrop.SelectedValue.ToString(),
            CurrentStore );

        if (Convert.ToBoolean( uxSmtpAuthenRequiredDrop.SelectedValue.ToString() ))
        {
            if ((uxSmtpUserNameText.Text == "") || (uxSmtpPassword.Text == "$$$$$$"))
            {
                if (uxHiddenPassword.Value == "")
                {
                    message = String.Format( "Warnning:<br/>{0}", Resources.SetupMessages.ValidateEmailPassword );
                    return false;
                }
                else
                {
                    if (uxSmtpPassword.Text != "$$$$$$")
                        DataAccessContext.ConfigurationRepository.UpdateValue(
                            DataAccessContext.Configurations["SmtpPassword"],
                            uxSmtpPassword.Text.Trim(),
                            CurrentStore );
                    else
                        DataAccessContext.ConfigurationRepository.UpdateValue(
                            DataAccessContext.Configurations["SmtpPassword"],
                            uxHiddenPassword.Value,
                            CurrentStore );
                }
            }
            else
            {
                DataAccessContext.ConfigurationRepository.UpdateValue(
                    DataAccessContext.Configurations["SmtpPassword"],
                    uxSmtpPassword.Text.Trim(),
                    CurrentStore );
                uxHiddenPassword.Value = uxSmtpPassword.Text;
            }

            DataAccessContext.ConfigurationRepository.UpdateValue(
                DataAccessContext.Configurations["SmtpUserName"],
                uxSmtpUserNameText.Text.Trim(),
				CurrentStore );
        }
        else
        {
            DataAccessContext.ConfigurationRepository.UpdateValue(
                DataAccessContext.Configurations["SmtpUserName"],
                string.Empty,
                CurrentStore );
            DataAccessContext.ConfigurationRepository.UpdateValue(
                DataAccessContext.Configurations["SmtpPassword"],
                string.Empty,
                CurrentStore );
        }
        return true;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxSmtpPassword.Attributes.Add( "value", "$$$$$$" );
        uxRequiredEmailValidator.ValidationGroup = ValidationGroup;
        uxSmtpPortRequired.ValidationGroup = ValidationGroup;
        uxSmtpPortCompare.ValidationGroup = ValidationGroup;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (Convert.ToBoolean( uxSmtpAuthenRequiredDrop.SelectedValue.ToString() ))
        {
            uxSmtpUserNameTR.Visible = true;
            uxSmtpPasswordTR.Visible = true;
        }
        else
        {
            uxSmtpUserNameTR.Visible = false;
            uxSmtpPasswordTR.Visible = false;
            uxSmtpUserNameText.Text = string.Empty;
            uxSmtpPassword.Text = string.Empty;
            uxHiddenPassword.Value = "";
        }
    }
}
