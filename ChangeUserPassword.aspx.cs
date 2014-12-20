using System;

public partial class ChangeUserPassword : Vevo.Deluxe.WebUI.Base.BaseLicenseLanguagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(uxPasswordDetails.GetMessage()))
        {
            if (uxPasswordDetails.HasErrorMessage())
                uxMessage.DisplayError(uxPasswordDetails.GetMessage());
            else
                uxMessage.DisplayMessage(uxPasswordDetails.GetMessage());
        }
    }
}
