using System;

public partial class ChangeAffiliateUserPassword : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
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