using System;
using System.Web.Security;
using System.Web.UI;

public partial class Components_PasswordDetails : Vevo.WebUI.International.BaseLanguageUserControl
{
    private bool _isErrorMessage = false;
    private bool IsErrorMessage
    {
        get
        {
            return _isErrorMessage;
        }
        set
        {
            _isErrorMessage = value;
        }
    }
    private string ErrorMessage
    {
        get
        {
            if (ViewState["ErrorMessage"] == null)
                return String.Empty;
            return ViewState["ErrorMessage"].ToString();
        }
        set
        {
            ViewState["ErrorMessage"] = value;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxUserNameText.Text = Membership.GetUser().UserName;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
    }

    protected void uxSubmitImageButton_Click( object sender, EventArgs e )
    {
        MembershipUser user = Membership.GetUser();
        if (user.ChangePassword( uxOldText.Text, uxNewText.Text ))
        {
            ErrorMessage = "[$SuccessMessage]";
        }
        else
        {
            ErrorMessage = "[$WrongPass]";
            _isErrorMessage = true;
            uxStatusHidden.Value = "Error";
        }
    }
    public bool HasErrorMessage()
    {
        return _isErrorMessage;
    }
    public string GetMessage()
    {
        return ErrorMessage;
    }
}
