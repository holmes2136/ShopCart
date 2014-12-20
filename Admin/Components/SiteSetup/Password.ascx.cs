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

public partial class AdminAdvanced_Components_SiteSetup_Password : AdminAdvancedBaseUserControl
{
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

    public TextBox PasswordOldControl
    {
        get { return uxOldPassText; }
    }

    public TextBox PasswordNewControl
    {
        get { return uxNewPassText; }
    }

    public TextBox PasswordConfirmControl
    {
        get { return uxConPassText; }
    }

    public bool UpdatePassword(out string message)
    {
        message = string.Empty;
        if (uxOldPassText.Text != "" | uxNewPassText.Text != "" | uxConPassText.Text != "")
        {
            if (uxOldPassText.Text == "" ||
                uxNewPassText.Text == "" ||
                uxConPassText.Text == "")
            {
                message = Resources.SetupMessages.EmptyPassword;
                return false;
            }

            MembershipUser adminUser = Membership.GetUser( this.Page.User.Identity.Name );
            try
            {
                if (Page.IsValid &&
                    !adminUser.ChangePassword( uxOldPassText.Text, uxNewPassText.Text ))
                {
                    message = Resources.SetupMessages.ChangePasswordFailed;
                    return false;
                }

                uxOldPassText.Text = String.Empty;
                uxNewPassText.Text = String.Empty;
                uxConPassText.Text = String.Empty;
            }
            catch
            {
                message = Resources.SetupMessages.ChangePasswordFailed ;
                return false;
            }
        }
        return true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        uxCompareValidator.ValidationGroup = ValidationGroup;
    }
}
