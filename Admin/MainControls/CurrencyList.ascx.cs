using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Base.Domain;

public partial class AdminAdvanced_MainControls_CurrencyList : AdminAdvancedBaseListControl
{
    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.CurrencyRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list );
    }



    private void PopulateControls()
    {
        RefreshGrid();

        if (uxGrid.Rows.Count > 0)
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

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.CurrencyItemsPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxGrid, "CurrencyCode" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }


    //private void PopulateSymbolDropdown()
    //{
    //    uxSymbolDrop.DataSource = DataContext.CurrencyRepository.GetByEnabled( BoolFilter.ShowTrue );
    //    uxSymbolDrop.DataBind();
    //}

    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();

        //CurrencyUtilities.StoreCurrencyCode = DataAccessContext.Configurations.GetValue( "BaseWebsiteCurrency" );
        //PopulateSymbolDropdown();
        //uxSymbolDrop.SelectedValue = CurrencyUtilities.StoreCurrencyCode;
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "CurrencyAdd.ascx", "" );
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
                    string ID = row.Cells[1].Text.Trim();

                    Currency currency = DataAccessContext.CurrencyRepository.GetOne( ID );
                    if (currency.CurrencyCode == DataAccessContext.Configurations.GetValue( "BaseWebsiteCurrency" ))
                    {
                        uxMessage.DisplayError
                            ( Resources.CurrencyMessages.DeleteDefaultError );
                        continue;
                    }
                    DataAccessContext.CurrencyRepository.Delete( ID );

                    deleted = true;
                }
            }

            if (deleted)
                uxMessage.DisplayMessage( Resources.CurrencyMessages.DeleteSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();
        //PopulateSymbolDropdown();

        if (uxGrid.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
    }

    protected void uxObjectSource_Selecting( object sender, ObjectDataSourceSelectingEventArgs e )
    {
        e.InputParameters["Enabled"] = true;
    }

    //protected void uxUpdateDefault_Click( object sender, EventArgs e )
    //{
    //    try
    //    {
    //        ConfigurationAccess.UpdateValueByName( "BaseWebsiteCurrency", uxSymbolDrop.SelectedValue );
    //        CurrencyUtilities.StoreCurrencyCode = DataAccessContext.Configurations.GetValue( "BaseWebsiteCurrency" );

    //        Currency currency = DataContext.CurrencyRepository.GetOne( uxSymbolDrop.SelectedValue );
    //        currency.ConversionRate = 1;
    //        DataContext.CurrencyRepository.Save( currency, uxSymbolDrop.SelectedValue );

    //        AdminUtilities.LoadSystemConfig();

    //        uxMessageDefault.DisplayMessage( Resources.SetupMessages.UpdateSuccess );
    //    }
    //    catch (Exception ex)
    //    {
    //        uxMessage.DisplayException( ex );
    //    }
    //}

    protected override void RefreshGrid()
    {
        int totalItems;

        uxGrid.DataSource = DataAccessContext.CurrencyRepository.SearchCurrency(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGrid.DataBind();
    }
    protected string GetCurrencyPosition( string CurrencyPosition )
    {
        if (CurrencyPosition == "Before")
            return "Before the Price";
        else
            return "After the Price";
    }
}
