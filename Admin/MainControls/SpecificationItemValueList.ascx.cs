using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Products;
using Vevo.WebUI.Ajax;
using Vevo.Base.Domain;

public partial class Admin_MainControls_SpecificationItemValueList : AdminAdvancedBaseListControl
{
    #region Private

    private bool _emptyRow = false;

    private bool IsContainingOnlyEmptyRow
    {
        get { return _emptyRow; }
        set { _emptyRow = value; }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.SpecificationItemValueRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list );
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

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
            uxPagingControl.ItemsPerPages = AdminConfig.StoreItemPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxGrid, "SpecificationItemID" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );
        //RegisterLanguageControl( uxLanguageControl );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
        //uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
    }

    private void RefreshDeleteButton()
    {
        if (IsContainingOnlyEmptyRow)
            uxDeleteButton.Visible = false;
        else
            uxDeleteButton.Visible = true;
    }

    private void CreateDummyRow( IList<SpecificationItemValue> list )
    {
        SpecificationItemValue specificationItemValue = new SpecificationItemValue( DataAccessContext.CultureRepository.GetOne( CultureUtilities.BaseCultureID ) );
        specificationItemValue.SpecificationItemValueID = "-1";
        specificationItemValue.Value = string.Empty;
        specificationItemValue.DisplayValue = string.Empty;

        list.Add( specificationItemValue );
    }

    private void SetFooterRowFocus()
    {
        Control textBox = uxGrid.FooterRow.FindControl( "uxValueText" );
        AjaxUtilities.GetScriptManager( this ).SetFocus( textBox );
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
        int totalItems = 0;
        IList<SpecificationItemValue> list = DataAccessContext.SpecificationItemValueRepository.SearchSpecificationItemValue(
            DataAccessContext.CultureRepository.GetOne( CultureUtilities.BaseCultureID ),
            SpecificationItemID,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );

        if (list == null || list.Count == 0)
        {
            IsContainingOnlyEmptyRow = true;
            list = new List<SpecificationItemValue>();
            CreateDummyRow( list );
        }

        uxGrid.DataSource = list;
        uxGrid.DataBind();

        RefreshDeleteButton();

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            bool deletedWithError = false;
            string errorValue = "<br/><ul>";
            foreach (GridViewRow row in uxGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    Label uxValueLabel = (Label) row.FindControl( "uxValueLabel" );
                    IList<ProductSpecification> list = DataAccessContext.SpecificationItemRepository.GetAllProductSpecificationValueByItemIDAndValue( SpecificationItemID, uxValueLabel.Text );

                    if (list.Count > 0)
                    {
                        errorValue += "<li>" + uxValueLabel.Text + " (" + uxGrid.DataKeys[row.RowIndex]["SpecificationItemValueID"].ToString().Trim() + ")</li>";
                        deletedWithError = true;
                    }
                    else
                    {
                        string id = uxGrid.DataKeys[row.RowIndex]["SpecificationItemValueID"].ToString().Trim();
                        DataAccessContext.SpecificationItemValueRepository.Delete( id );
                        deleted = true;
                    }
                }
            }
            uxGrid.EditIndex = -1;
            if (deletedWithError)
            {
                uxMessage.DisplayError( Resources.ProductSpecificationMessages.DeleteItemValueError + errorValue + "</ul>" );
            }
            else if (deleted)
            {
                uxMessage.DisplayMessage( Resources.ProductSpecificationMessages.DeleteSuccess );
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

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        uxGrid.EditIndex = -1;
        uxGrid.ShowFooter = true;
        uxGrid.ShowHeader = true;
        RefreshGrid();

        uxAddButton.Visible = false;

        SetFooterRowFocus();
    }


    protected void uxGrid_RowEditing( object sender, GridViewEditEventArgs e )
    {
        uxGrid.EditIndex = e.NewEditIndex;
        RefreshGrid();
    }

    protected void uxGrid_CancelingEdit( object sender, GridViewCancelEditEventArgs e )
    {
        uxGrid.EditIndex = -1;
        RefreshGrid();
    }

    private SpecificationItemValue SetupSpecificationItemValueDetails()
    {
        SpecificationItemValue item = new SpecificationItemValue( DataAccessContext.CultureRepository.GetOne( CultureUtilities.BaseCultureID ) );
        item.Value = ((TextBox) uxGrid.FooterRow.FindControl( "uxValueText" )).Text.Trim();
        item.DisplayValue = ((TextBox) uxGrid.FooterRow.FindControl( "uxDisplayValueText" )).Text.Trim();
        item.SpecificationItemID = SpecificationItemID;
        return item;
    }

    private bool IsSpecificationItemValueExist( string value )
    {
        return DataAccessContext.SpecificationItemValueRepository.IsSpecificationItemValueExist( value, SpecificationItemID );
    }

    protected void uxGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        try
        {
            if (e.CommandName == "Add")
            {
                SpecificationItemValue item = SetupSpecificationItemValueDetails();
                if (IsSpecificationItemValueExist( item.Value ))
                {
                    uxMessage.DisplayError( Resources.ProductSpecificationMessages.DuplicateValueError );
                    return;
                }
                DataAccessContext.SpecificationItemValueRepository.Save( item );
                uxMessage.DisplayMessage( Resources.ProductSpecificationMessages.ValueAddSuccess );
                RefreshGrid();
            }
            else if (e.CommandName == "Edit")
            {
                uxGrid.ShowFooter = false;
                uxAddButton.Visible = true;
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

    }

    protected void uxGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            string id = ((Label) uxGrid.Rows[e.RowIndex].FindControl( "uxSpecificationItemValueIDLabel" )).Text.Trim();
            string value = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxValueText" )).Text.Trim();
            string displayValue = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxDisplayValueText" )).Text.Trim();

            if (!String.IsNullOrEmpty( id ))
            {
                SpecificationItemValue item = DataAccessContext.SpecificationItemValueRepository.GetOne( DataAccessContext.CultureRepository.GetOne( CultureUtilities.BaseCultureID ), id );
                if (!item.Value.Equals( value ) && IsSpecificationItemValueExist( value ))
                {
                    uxMessage.DisplayError( Resources.ProductSpecificationMessages.DuplicateValueError );
                    return;
                }

                item.Value = value;
                item.DisplayValue = displayValue;
                item.SpecificationItemID = SpecificationItemID;

                DataAccessContext.SpecificationItemValueRepository.Save( item );
                uxGrid.EditIndex = -1;
                uxMessage.DisplayMessage( Resources.ProductSpecificationMessages.ValueUpdateSuccess );

                RefreshGrid();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
        }
    }

    protected void uxGrid_DataBound( object sender, EventArgs e )
    {
        if (IsContainingOnlyEmptyRow)
        {
            uxGrid.Rows[0].Visible = false;
        }
    }

    #endregion

    #region Public
    public string SpecificationItemID
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["SpecificationItemID"] ))
                return MainContext.QueryString["SpecificationItemID"];
            else
                return "0";
        }
    }
    #endregion
}