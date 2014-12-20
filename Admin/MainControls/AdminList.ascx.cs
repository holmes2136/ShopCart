using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.WebAppLib;
using Vevo.Base.Domain;

public partial class AdminAdvanced_MainControls_AdminList : AdminAdvancedBaseListControl
{
    private const int ColumnAdminID = 1;
    private const int ColumnUserName = 2;

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.AdminRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list );
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

        if (IsAdminModifiable())
        {
            if (uxGrid.Rows.Count > 0)
            {
                DeleteVisible( true );
                uxPagingControl.Visible = true;
            }
            else
            {
                DeleteVisible( false );
                uxPagingControl.Visible = false;
            }
        }
        else
        {
            uxAddButton.Visible = false;
            DeleteVisible( false );

            if (uxGrid.Rows.Count > 0)
            {
                uxPagingControl.Visible = true;
            }
            else
            {
                uxPagingControl.Visible = false;
            }
        }
    }

    private void DeleteVisible( bool value )
    {
        uxDeleteButton.Visible = value;
        if (value)
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                uxDeleteConfirmButton.TargetControlID = "uxDeleteButton";
                uxConfirmModalPopup.TargetControlID = "uxDeleteButton";
            }
            else
            {
                uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
                uxConfirmModalPopup.TargetControlID = "uxDummyButton";
            }
        }
        else
        {
            uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
            uxConfirmModalPopup.TargetControlID = "uxDummyButton";
        }
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.AdminItemsPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxGrid, "AdminID" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "AdminAdd.ascx", "" );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string ID = row.Cells[ColumnAdminID].Text.Trim();
                    string userName = row.Cells[ColumnUserName].Text.Trim();
                    if ((Membership.GetUser().UserName != userName) && (userName.ToLower() != "admin"))
                    {
                        DataAccessContext.AdminRepository.Delete( ID );
                        Membership.DeleteUser( row.Cells[ColumnUserName].Text.Trim() );
                        AdminMenuPermissionAccess.Delete( ID );
                        deleted = true;

                    }
                    else
                    {
                        uxMessage.DisplayError( Resources.AdminMessage.DeleteDefaultUserError );
                    }
                }
            }

            if (deleted)
            {
                AdminUtilities.ClearAdminCache();
                uxMessage.DisplayMessage( Resources.AdminMessage.DeleteSuccess );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if (uxGrid.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
    }

    protected override void RefreshGrid()
    {
        int totalItems;

        uxGrid.DataSource = DataAccessContext.AdminRepository.SearchAdmin(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGrid.DataBind();
    }

}
