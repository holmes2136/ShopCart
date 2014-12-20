using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Returns;
using Vevo.Domain.Stores;
using Vevo.Domain.Users;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Base.Domain;

public partial class AdminAdvanced_MainControls_RmaList : AdminAdvancedBaseListControl
{
    #region Private

    private const int RmaIDIndex = 1;

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.RmaRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list, "StoreID" );
    }

    private void LoadProcessedDropFromQuery()
    {
        if ( MainContext.QueryString[ "Processed" ] != null )
        {
            uxProcessedDrop.SelectedValue = MainContext.QueryString[ "Processed" ];
        }
    }

    private void PopulateControls()
    {
        if ( !MainContext.IsPostBack )
        {
            RefreshGrid();
        }

        if ( uxGrid.Rows.Count > 0 )
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

    private void DeleteVisible( bool value )
    {
        uxDeleteButton.Visible = value;
        if ( value )
        {
            if ( AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal )
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

    private void ApplyPermissions()
    {
        if ( !IsAdminModifiable() )
        {
            uxProcessedButton.Visible = false;
            DeleteVisible( false );
        }
    }

    private void AdminAdvanced_MainControls_RmaList_BrowseHistoryAdding(
        object sender, BrowseHistoryAddEventArgs e )
    {
        e.BrowseHistoryQuery.AddQuery( "Processed", uxProcessedDrop.SelectedValue );
    }

    private void SetUpGridSupportControls()
    {
        if ( !MainContext.IsPostBack )
        {
            uxPagingControl.ItemsPerPages = AdminConfig.RmaItemsPerPage;
            SetUpSearchFilter();
            LoadProcessedDropFromQuery();
        }

        RegisterGridView( uxGrid, "RmaID" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );
        RegisterStoreFilterDrop( uxStoreFilterDrop );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        uxStoreFilterDrop.BubbleEvent += new EventHandler( uxStoreFilterDrop_BubbleEvent );

        BrowseHistoryAdding += new BrowseHistoryAddEventHandler(
            AdminAdvanced_MainControls_RmaList_BrowseHistoryAdding );
    }

    private Rma.RmaStatus GetRmaStatus()
    {
        Rma.RmaStatus status;

        switch ( uxProcessedDrop.SelectedValue )
        {
            case "New":
                status = Rma.RmaStatus.New;
                break;
            case "Processing":
                status = Rma.RmaStatus.Processing;
                break;
            case "Rejected":
                status = Rma.RmaStatus.Rejected;
                break;
            case "Returned":
                status = Rma.RmaStatus.Returned;
                break;
            default:
                status = Rma.RmaStatus.All;
                break;
        }

        return status;
    }

    private void SendMailToCustomer( Rma rma )
    {
        string subjectText = String.Empty;
        string bodyText = String.Empty;

        EmailTemplateTextVariable.ReplaceRMAApprovalText( rma, out subjectText, out bodyText );

        Store store = DataAccessContext.StoreRepository.GetOne( rma.StoreID );
        string companyEmail = DataAccessContext.Configurations.GetValue( "CompanyEmail", store );

        WebUtilities.SendHtmlMail(
            companyEmail,
            rma.GetCustomer.Email,
            subjectText,
            bodyText,
            store );
    }

    #endregion

    #region Protected

    protected string StoreID
    {
        get
        {
            if ( KeyUtilities.IsMultistoreLicense() )
            {
                return uxStoreFilterDrop.SelectedValue;
            }
            else
            {
                return Store.RegularStoreID;
            }
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if ( !KeyUtilities.IsMultistoreLicense() )
        {
            uxStoreFilterDrop.Visible = false;
        }

        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
        ApplyPermissions();
    }

    protected void uxProcessedDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        uxPagingControl.CurrentPage = 1;
        RefreshGrid();

        AddBrowseHistory();
    }

    protected void uxProcessedButton_Click( object sender, EventArgs e )
    {
        bool hasCheck = false;
        bool hasError = false;

        foreach ( GridViewRow row in uxGrid.Rows )
        {
            CheckBox check = ( CheckBox ) row.FindControl( "uxCheck" );

            if ( check.Checked )
            {
                try
                {
                    string rmaID = row.Cells[ 1 ].Text.Trim();
                    Rma rma = DataAccessContext.RmaRepository.GetOne( rmaID );
                    rma.RequestStatus = "Returned";
                    DataAccessContext.RmaRepository.Save( rma );

                    SendMailToCustomer( rma );
                    hasCheck = true;
                }
                catch ( Exception ex )
                {
                    uxMessage.DisplayException( ex );
                    hasError = true;
                }
            }

        }

        if ( hasCheck && !hasError )
            uxMessage.DisplayMessage( Resources.RmaMessages.ProcessedSuccess );

        RefreshGrid();
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach ( GridViewRow row in uxGrid.Rows )
            {
                CheckBox deleteCheck = ( CheckBox ) row.FindControl( "uxCheck" );
                if ( deleteCheck.Checked )
                {
                    string id = row.Cells[ RmaIDIndex ].Text.Trim();
                    DataAccessContext.RmaRepository.Delete( id );
                    deleted = true;
                }
            }

            if ( deleted )
                uxMessage.DisplayMessage( Resources.RmaMessages.DeleteSuccess );
        }
        catch ( Exception ex )
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if ( uxGrid.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages )
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
    }

    protected override void RefreshGrid()
    {
        int totalItems;

        IList<Rma> list = DataAccessContext.RmaRepository.SearchRma(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            StoreID,
            GetRmaStatus(),
            out totalItems );

        uxPagingControl.NumberOfPages = ( int ) Math.Ceiling( ( double ) totalItems / uxPagingControl.ItemsPerPages );
        uxGrid.DataSource = list;
        uxGrid.DataBind();
    }

    protected string GetCustomerUserName( object sender )
    {
        string customerID = ( string ) sender;
        Customer customer = DataAccessContext.CustomerRepository.GetOne( customerID );

        return customer.UserName;
    }

    #endregion
}
