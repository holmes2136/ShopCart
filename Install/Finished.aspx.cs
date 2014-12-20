using System;
using Vevo.Domain;

public partial class Install_Finish : System.Web.UI.Page
{
    protected void Page_Load( object sender, EventArgs e )
    {
        DataAccessContext.ApplicationSettings.InstallCompleted = true;
        DataAccessContext.ApplicationSettings.Save();
    }
}
