using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Vevo;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.WebAppLib;
using System.Collections.Generic;
using Vevo.Data;
using Vevo.Base.Domain;

public partial class Admin_MainControls_GoogleSpecMappingList : AdminAdvancedBaseListControl
{
    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.GoogleFeedMappingRepository.GetGoogleSpecMappingTableSchema();
        uxSearchFilter.SetUpSchema( list );
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

        if (uxGoogleSpecMappingGrid.Rows.Count > 0)
        {
            DeleteVisible( true );
            uxPagingControl.Visible = true;
        }
        else
        {
            DeleteVisible( false );
            uxPagingControl.Visible = false;
        }

        if (!IsAdminModifiable())
        {
            uxAddButton.Visible = false;
            DeleteVisible( false );
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
            uxPagingControl.ItemsPerPages = AdminConfig.GoogleSpecMappingPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxGoogleSpecMappingGrid, "GoogleFeedTagID" );

        RegisterLanguageControl( uxLanguageControl );
        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
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

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxGoogleSpecMappingGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                HiddenField uxGoogleFeedTagValueID = (HiddenField) row.FindControl( "uxGoogleFeedTagValueID" );
                HiddenField uxSpecificationItemValue = (HiddenField) row.FindControl( "uxSpecificationItemValue" );
                if (deleteCheck.Checked)
                {
                    DataAccessContext.GoogleFeedMappingRepository.DeleteGoogleSpecMapping( uxGoogleFeedTagValueID.Value, uxSpecificationItemValue.Value );
                    
                    deleted = true;
                }
            }

            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.ProductSpecificationMessages.DeleteSuccess );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if (uxGoogleSpecMappingGrid.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "GoogleSpecMappingAdd.ascx" );
    }

    protected override void RefreshGrid()
    {
        int totalItems = 0;

        uxGoogleSpecMappingGrid.DataSource = DataAccessContext.GoogleFeedMappingRepository.SearchGoogleSpecMapping(
            uxLanguageControl.CurrentCulture,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems
        );

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
        uxGoogleSpecMappingGrid.DataBind();
    }
}
