using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Base.Domain;
using Vevo.Domain;
using Vevo.Domain.Stores;

public partial class Admin_MainControls_BannerList : AdminAdvancedBaseListControl
{
    #region Private

    private const int ColumnBannerID = 1;

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.BannerRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list );
    }

    private void PopulateControls()
    {
        RefreshGrid();

        if ( uxGridBanner.Rows.Count > 0 )
        {
            uxPagingControl.Visible = true;
            DeleteVisible( true );
        }
        else
        {
            uxPagingControl.Visible = false;
            DeleteVisible( false );
        }

        if ( !IsAdminModifiable() )
        {
            uxAddButton.Visible = false;
            DeleteVisible( false );
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

    private void SetUpGridSupportControls()
    {
        if ( !MainContext.IsPostBack )
        {
            uxPagingControl.ItemsPerPages = AdminConfig.BannerItemsPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxGridBanner, "BannerID" );
        RegisterLanguageControl( uxLanguageControl );
        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );
        RegisterCustomControl( uxStoreFilterDrop );

        uxStoreFilterDrop.BubbleEvent += new EventHandler( uxStoreFilterDrop_BubbleEvent );
        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }

    #endregion

    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected override void RefreshGrid()
    {
        int totalItems;

        uxGridBanner.DataSource = DataAccessContext.BannerRepository.SearchBanner(
            uxLanguageControl.CurrentCulture,
            GridHelper.GetFullSortText(),
            StoreID,
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems
            );

        uxPagingControl.NumberOfPages = ( int ) Math.Ceiling( ( double ) totalItems / uxPagingControl.ItemsPerPages );

        uxGridBanner.DataBind();
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach ( GridViewRow row in uxGridBanner.Rows )
            {
                CheckBox deleteCheck = ( CheckBox ) row.FindControl( "uxCheck" );
                if ( deleteCheck.Checked )
                {
                    string id = row.Cells[ ColumnBannerID ].Text.Trim();

                    DataAccessContext.BannerRepository.Delete( id );

                    deleted = true;
                }
            }

            if ( deleted )
                uxMessage.DisplayMessage( Resources.BannerMessages.DeleteSuccess );
        }
        catch ( Exception ex )
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if ( uxGridBanner.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages )
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }

        uxStatusHidden.Value = "Deleted";
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "BannerAdd.ascx" );
    }

    protected void uxBannerSortingButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "BannerSorting.ascx" );
    }

    protected string BannerDateMessage( string bannerDate )
    {
        return String.Format( "{0}", Convert.ToDateTime( bannerDate ).ToShortDateString() );
    }

    #endregion

    #region Public

    public string StoreID
    {
        get
        {
            if ( KeyUtilities.IsMultistoreLicense() )
            {
                if ( MainContext.QueryString[ "StoreID" ] != null )
                    return MainContext.QueryString[ "StoreID" ];
                else
                    return uxStoreFilterDrop.SelectedValue;
            }
            else
            {
                return Store.RegularStoreID;
            }
        }
    }

    #endregion
}
