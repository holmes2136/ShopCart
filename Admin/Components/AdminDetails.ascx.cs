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
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Users;
using Vevo.Shared.DataAccess;

public partial class AdminAdvanced_Components_AdminDetails : AdminAdvancedBaseUserControl
{
    private const int ColumnView = 0;
    private const int ColumnModified = 1;
    private const int ColumnMenuPageName = 3;


    private enum Mode { Add, Edit };
    private Mode _mode = Mode.Add;

    private string CurrentAdminID
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["AdminID"] ))
                return MainContext.QueryString["AdminID"];
            else
                return "0";
        }
    }

    private void ClearInputField()
    {
        uxUserName.Text = string.Empty;

        uxPassword.Text = string.Empty;
        uxRePassword.Text = string.Empty;
        uxEmail.Text = string.Empty;
    }

    private void PopulateControls()
    {
        ClearInputField();

        if (CurrentAdminID != null &&
            int.Parse( CurrentAdminID ) >= 0)
        {
            Admin admin = DataAccessContext.AdminRepository.GetOne( CurrentAdminID );
            uxUserName.Text = admin.UserName;

            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
                uxRegisterDateCalendarPopup.SelectedDate = admin.RegisterDate;

            MembershipUserCollection memberShip = Membership.FindUsersByName( uxUserName.Text );

            uxEmail.Text = admin.Email;
        }
    }

    private bool UpdatePassword( out string errorMessage )
    {
        errorMessage = String.Empty;

        if (uxPassword.Text != "")
        {
            if (uxPassword.Text.Trim() != uxRePassword.Text.Trim())
            {
                uxPassword.Text = string.Empty;
                uxRePassword.Text = string.Empty;
                errorMessage = Resources.AdminMessage.PasswordNotMatch;
                return false;
            }
            else if (uxOldPassword.Text == "")
            {
                errorMessage = Resources.AdminMessage.PasswordEmpty;
                return false;
            }
            else
            {
                MembershipUser user = Membership.GetUser( uxUserName.Text );

                if (!user.ChangePassword( uxOldPassword.Text, uxPassword.Text ))
                {
                    errorMessage = Resources.AdminMessage.UpdatePasswordError;
                    return false;
                }
            }
        }

        return true;
    }

    private bool UpdatePermission( out string errorMessage )
    {
        errorMessage = String.Empty;

        Admin admin = DataAccessContext.AdminRepository.GetOne( CurrentAdminID );
        admin = SetUpAdmin( admin );
        DataAccessContext.AdminRepository.Save( admin );

        if (String.Compare( admin.UserName, "Admin", true ) != 0)
        {
            foreach (GridViewRow row in uxGrid.Rows)
            {
                CheckBox uxCheckView = (CheckBox) row.FindControl( "uxCheckVeiw" );
                CheckBox uxCheckModified = (CheckBox) row.FindControl( "uxCheckModify" );

                string menuPageName = uxGrid.DataKeys[row.RowIndex]["MenuPageName"].ToString();

                AdminMenuPermissionAccess.Update(
                    CurrentAdminID,
                    menuPageName,
                    uxCheckView.Checked,
                    uxCheckModified.Checked );
            }
        }

        return true;
    }

    private Admin SetUpAdmin( Admin admin )
    {
        admin.UserName = uxUserName.Text.Trim();
        admin.Email = uxEmail.Text.Trim();
        admin.RegisterDate = uxRegisterDateCalendarPopup.SelectedDate;
        return admin;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        UpdateMenuInPermission( CurrentAdminID );
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private void EnableDisableCheckAllAndRegisterJavascript()
    {
        Admin admin = DataAccessContext.AdminRepository.GetOne( CurrentAdminID );

        CheckBox SelectAllCheckBoxView = (CheckBox) uxGrid.HeaderRow.FindControl( "SelectAllCheckBoxView" );
        SelectAllCheckBoxView.Attributes.Add( "onclick", "SelectAllCheckBoxes(this , 'uxCheckVeiw')" );

        CheckBox SelectAllCheckBoxModify = (CheckBox) uxGrid.HeaderRow.FindControl( "SelectAllCheckBoxModify" );
        SelectAllCheckBoxModify.Attributes.Add( "onclick", "SelectAllCheckBoxes(this , 'uxCheckModify')" );

        if (!admin.IsNull && admin.UserName.ToLower() == "admin")
        {
            SelectAllCheckBoxView.Enabled = false;
            SelectAllCheckBoxModify.Enabled = false;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (IsEditMode())
        {
            if (!MainContext.IsPostBack)
            {
                PopulateControls();
                uxUserName.Enabled = false;
                uxOldPwd.Visible = true;

                if (IsAdminModifiable())
                {
                    uxUpdateButton.Visible = true;
                }
                else
                {
                    uxUpdateButton.Visible = false;
                }

                uxAddButton.Visible = false;
            }
        }
        else
        {
            if (IsAdminModifiable())
            {
                lcRegisterDate.Visible = true;

                uxAddButton.Visible = true;
                uxUpdateButton.Visible = false;
                uxOldPwd.Visible = false;

                uxRegisterDateCalendarPopup.SelectedDate = DateTime.Now;
            }
            else
            {
                MainContext.RedirectMainControl( "AdminList.ascx" );
            }
        }

        PopulatePermission();
        EnableDisableCheckAllAndRegisterJavascript();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        if (Page.IsValid)
        {
            string adminID = DataAccessContext.AdminRepository.GetIDFromUserName( uxUserName.Text.Trim() );
            if (adminID != "0")
            {
                uxUserName.Text = string.Empty;
                uxMessage.DisplayError( Resources.AdminMessage.DuplicateUserName );
                return;
            }

            if ((uxPassword.Text.Trim() == "") || (uxRePassword.Text.Trim() == ""))
            {
                uxPassword.Text = string.Empty;
                uxRePassword.Text = string.Empty;
                uxMessage.DisplayError( Resources.AdminMessage.PasswordEmpty );
                return;
            }

            if (uxPassword.Text.Trim() != uxRePassword.Text.Trim())
            {
                uxPassword.Text = string.Empty;
                uxRePassword.Text = string.Empty;
                uxMessage.DisplayError( Resources.AdminMessage.PasswordNotMatch );
                return;
            }

            Admin admin = new Admin();
            admin = SetUpAdmin( admin );
            admin = DataAccessContext.AdminRepository.Save( admin );

            Membership.CreateUser(
                uxUserName.Text.Trim(),
                uxPassword.Text.Trim(),
                uxEmail.Text.Trim()
                );
            Roles.AddUserToRole( uxUserName.Text.Trim(), "Administrators" );

            foreach (GridViewRow row in uxGrid.Rows)
            {
                CheckBox uxCheckView = (CheckBox) row.FindControl( "uxCheckVeiw" );
                CheckBox uxCheckModified = (CheckBox) row.FindControl( "uxCheckModify" );

                string menuPageName = uxGrid.DataKeys[row.RowIndex]["MenuPageName"].ToString();

                string result = AdminMenuPermissionAccess.Create(
                    admin.AdminID,
                    menuPageName,
                    uxCheckView.Checked,
                    uxCheckModified.Checked );
            }

            uxMessage.DisplayMessage( Resources.AdminMessage.AddSuccess );
            ClearInputField();

            AdminUtilities.ClearAdminCache();

            uxStatusHidden.Value = "Added";
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                string errorMessage;
                if (UpdatePassword( out errorMessage ) &&
                    UpdatePermission( out errorMessage ))
                {
                    uxMessage.DisplayMessage( Resources.AdminMessage.UpdateSuccess );
                }
                else
                {
                    uxMessage.DisplayError( errorMessage );
                }
                PopulateControls();
                PopulatePermission();

                AdminUtilities.ClearAdminCache();

                uxStatusHidden.Value = "Updated";
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    private void PopulatePermission()
    {
        DataTable table = new DataTable();
        table = AdminMenuAdvancedPermissionAccess.GetAll( AdminConfig.CurrentCultureID, GridHelper.GetFullSortText(), FlagFilter.ShowTrue );
        uxGrid.DataSource = table;
        uxGrid.DataBind();

        if (IsEditMode())
        {
            DataTable adminTable = AdminMenuAdvancedPermissionAccess.GetAllByAdminID( AdminConfig.CurrentCultureID,
                 CurrentAdminID, GridHelper.GetFullSortText(), FlagFilter.ShowTrue );

            Admin admin = DataAccessContext.AdminRepository.GetOne( CurrentAdminID );

            if (admin.UserName.ToLower() == "admin")
            {
                uxMessagePermissionTable.Text = "* Permissions for user 'Admin' cannot be changed." ;
            }
            else
            {
                uxMessagePermissionTable.Text = "";
            }

            if (adminTable.Rows.Count > 0)
            {
                int i = 0;

                foreach (GridViewRow row in uxGrid.Rows)
                {
                    CheckBox uxCheckView = (CheckBox) row.FindControl( "uxCheckVeiw" );
                    CheckBox uxCheckModified = (CheckBox) row.FindControl( "uxCheckModify" );
                    String menuPageName = uxGrid.DataKeys[row.RowIndex]["MenuPageName"].ToString();

                    DataRow[] dataRow = adminTable.Select( String.Format( "MenuPagename = '{0}'", menuPageName ), GridHelper.GetFullSortText() );
                    if (dataRow.Length > 0)
                    {
                        uxCheckView.Checked = Convert.ToBoolean( dataRow[0]["ViewMode"].ToString() );
                        uxCheckModified.Checked = Convert.ToBoolean( dataRow[0]["ModifyMode"].ToString() );
                    }

                    if (admin.UserName.ToLower() == "admin")
                    {
                        uxCheckView.Enabled = false;
                        uxCheckModified.Enabled = false;
                    }

                    i += 1;
                }
            }
        }
    }

    private void UpdateMenuInPermission( string permissionID )
    {
        DataTable menuTable = AdminMenuAdvancedPermissionAccess.GetAll( AdminConfig.CurrentCultureID, "SortOrder", FlagFilter.ShowTrue );

        foreach (DataRow dr in menuTable.Rows)
        {
            string menuPageName = dr["MenuPageName"].ToString();
            if (!AdminMenuPermissionAccess.IsExistingPermission( permissionID, menuPageName ))
            {
                AdminMenuPermissionAccess.Create( permissionID, menuPageName, false, false );
            }
        }
    }

    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "SortOrder" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
        PopulatePermission();
    }


    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

}
