using System;

public partial class AdminAdvanced_MainControls_ProductReviewEdit : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        uxDetails.SetVisibleLanguageControl = false;
        uxDetails.SetEditMode(); 
    }
}
