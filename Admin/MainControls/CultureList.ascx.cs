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
using Vevo.Shared.DataAccess;
using Vevo.WebAppLib;
using Vevo.WebUI.International;
using Vevo.Base.Domain;

public partial class AdminAdvanced_MainControls_CultureList : AdminAdvancedBaseListControl
{
    private const int ColumnCultureID = 1;
    private const int ColumnName = 2;


    private void PopulateControls()
    {
        RefreshGrid();

        if (uxGridCulture.Rows.Count > 0)
        {
            uxPagingControl.Visible = true;
            DeleteVisible( true );
        }
        else
        {
            uxPagingControl.Visible = false;
            DeleteVisible( false );
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

    private void uxGridCulture_ClearSearchtHandler( object sender, EventArgs e )
    {

        uxSearchFilter.ClearFilter();
        RefreshGrid();
    }

    private void uxGridCulture_RefreshHandler( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    private void uxGridCulture_ResetHandler( object sender, EventArgs e )
    {
        uxPagingControl.CurrentPage = 1;
        RefreshGrid();
    }

    private void DeleteAllRelateFieldCultureID( string cultureID )
    {
        DataAccessContext.CategoryRepository.DeleteLocalesByCultureID( cultureID );
        DataAccessContext.ContentRepository.DeleteLocalesByCultureID( cultureID );
        DataAccessContext.OptionItemRepository.DeleteLocalesByCultureID( cultureID );
        DataAccessContext.OptionGroupRepository.DeleteLocalesByCultureID( cultureID );
        DataAccessContext.ProductRepository.DeleteLocalesByCultureID( cultureID );
        DataAccessContext.NewsRepository.DeleteLocalesByCultureID( cultureID );
        DataAccessContext.ShippingOptionRepository.DeleteLocalesByCultureID( cultureID );
        LanguageTextAccess.DeleteByCultureID( cultureID );
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.CultureRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list );
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.CultureItemsPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxGridCulture, "CultureID" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }

    private void DeleteCultureInformation( string cultureID )
    {
        DeleteEmailTemplate( cultureID );
    }

    private void DeleteEmailTemplate( string cultureID )
    {
        DataAccessContext.EmailTemplateDetailRepository.DeleteLocalesByCultureID( cultureID );
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
        MainContext.RedirectMainControl( "CultureAdd.ascx", "" );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxGridCulture.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string defaultLanguage = row.Cells[ColumnName].Text.Trim();

                    CultureConfigs cultureConfigs = new CultureConfigs();
                    if (cultureConfigs.UseCultureName( defaultLanguage ))
                    {
                        uxMessage.DisplayError( Resources.CultureMessages.DeleteErrorExistingConfig );
                        return;
                    }

                    string id = row.Cells[ColumnCultureID].Text.Trim();
                    DataAccessContext.CultureRepository.Delete( id );

                    //Delete Relate Table (CultureID)
                    DeleteAllRelateFieldCultureID( id );
                    deleted = true;

                    //Delete Email Template
                    DeleteCultureInformation( id );
                }
            }

            if (deleted)
            {
                AdminUtilities.ClearLanguageCache();
                uxMessage.DisplayMessage( Resources.CultureMessages.DeleteSuccess );
            }
        }
        catch (DataAccessException ex)
        {
            uxMessage.DisplayError( "Error:<br/>" + ex.Message );
        }
        catch
        {
            uxMessage.DisplayError( Resources.CultureMessages.DeleteError );
        }

        RefreshGrid();

        if (uxGridCulture.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
    }

    protected override void RefreshGrid()
    {
        int totalItems;

        uxGridCulture.DataSource = DataAccessContext.CultureRepository.SearchCulture(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems
        );
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGridCulture.DataBind();
    }

}
