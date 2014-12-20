using System;
using System.Data;
using System.Web.UI;
using Vevo.Shared.WebUI;

public partial class PermissionCheck_Install : System.Web.UI.Page
{
    private String[] _testFolders = new String[] {
            "App_Data",
            "App_Data/Logs",
            "App_Data/System",

            "Images/Categories",
            "Images/Color",
            "Images/Configuration",
            "Images/Design",
            "Images/Gateway",
            "Images/News",
            "Images/Products",
            "Images/UploadFile",
            "Images/UploadFileTemp",

            "ContentTemplates",
            "ProductFiles",
            "Import",
            "Export" };

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!Page.IsPostBack)
        {
            ApplicationFolderPermissionTester testPermission = new ApplicationFolderPermissionTester( _testFolders );

            uxPermissionGrid.DataSource = testPermission.CreateTestFolderTable( false );
            uxPermissionGrid.DataBind();
        }
    }

    protected void uxVerifyButton_Click( object sender, EventArgs e )
    {
        ApplicationFolderPermissionTester testPermission = new ApplicationFolderPermissionTester( _testFolders );
        bool permissionPassFlag = true;

        uxPermissionGrid.DataSource = testPermission.CreateTestFolderTable( true, out permissionPassFlag );
        uxPermissionGrid.DataBind();

        if (permissionPassFlag)
        {
            uxNextButton.Enabled = true;
        }
        else
        {
            uxFilePermissionTestMessage.DisplayErrorNoNewLine( this.GetLocalResourceObject( "PermissionFail.ErrorMessage" ).ToString() );
            uxNextButton.Enabled = false;
        }
    }

    protected void uxNextButton_Click( object sender, EventArgs e )
    {
        ApplicationFolderPermissionTester testPermission = new ApplicationFolderPermissionTester( _testFolders );
        bool permissionPassFlag = true;

        DataTable table = testPermission.CreateTestFolderTable( true, out permissionPassFlag );

        if (permissionPassFlag)
            Response.Redirect( "installsetupdatabase.aspx" );
        else
        {
            uxPermissionGrid.DataSource = table;
            uxPermissionGrid.DataBind();
            uxFilePermissionTestMessage.DisplayErrorNoNewLine( this.GetLocalResourceObject( "PermissionFail.ErrorMessage" ).ToString() );
            uxNextButton.Enabled = false;
        }
    }

    protected string GetPermissionImage( object permission )
    {
        if (permission.ToString().ToLower() == "pass")
        {
            return "../Images/Icon/Icon_True.gif";
        }
        else if (permission.ToString().ToLower() == "fail")
        {
            return "../Images/Icon/Icon_MessageError.gif";
        }
        else
        {
            return "../Images/Icon/Icon_Blank.gif";
        }
    }
}
