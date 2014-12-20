using System;
using System.Linq;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;
using System.Collections;
using System.Collections.Generic;
using Vevo.Shared.Utilities;

public partial class Admin_Components_ProductKitGroupItemSeletedList : AdminAdvancedBaseListControl
{
    #region Private

    private bool _isDeleteClick = false;

    private string CurrentID
    {
        get
        {
            if (!String.IsNullOrEmpty( MainContext.QueryString["ProductKitGroupID"] ))
                return MainContext.QueryString["ProductKitGroupID"];
            else
                return "0";
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
        RegisterGridView( uxProductKitGroupItemGrid, "ProductID" );
    }

    #endregion


    #region Protected

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        OnBubbleEvent( e );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            _isDeleteClick = true;
            bool deleted = false;
            foreach (GridViewRow row in uxProductKitGroupItemGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string id = uxProductKitGroupItemGrid.DataKeys[row.RowIndex]["ProductID"].ToString().Trim();
                    DataAccessContext.ProductKitGroupRepository.DeleteProductKitItem( id, CurrentID );
                    deleted = true;
                }
            }
            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.ProductKitGroupItemMessages.DeleteSuccess );
                uxStatusHidden.Value = "Deleted";
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
        RefreshGrid();
    }

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
        uxProductKitGroupItemGrid.DataSource = DataAccessContext.ProductKitGroupRepository.GetProductKitItems( CurrentID, GridHelper.GetFullSortText(), CurrentCulture );
        uxProductKitGroupItemGrid.DataBind();
        _isDeleteClick = false;
    }

    protected string GetProductName( object productID )
    {
        return DataAccessContext.ProductRepository.GetOne( CurrentCulture, productID.ToString(), StoreContext.CurrentStore.StoreID ).Name;
    }

    public void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }
        if (uxProductKitGroupItemGrid.Rows.Count > 0)
        {
            DeleteVisible( true );
        }
        else
        {
            DeleteVisible( false );
        }

        if (!IsAdminModifiable())
        {
            DeleteVisible( false );
        }
    }

    protected void uxGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            GridViewRow row = uxProductKitGroupItemGrid.Rows[e.RowIndex];
            string productID = ((Label) row.FindControl( "uxProductIDLabel" )).Text;
            bool isDefault = ((CheckBox) row.FindControl( "uxIsDefaultCheck" )).Checked;
            bool isUserDefinedQuantity = ((CheckBox) row.FindControl( "uxIsUserDefinedQuantityCheck" )).Checked;
            string quantity = ((TextBox) row.FindControl( "uxQuantityText" )).Text;

            ProductKitGroup kitGroup = DataAccessContext.ProductKitGroupRepository.GetOne( CurrentCulture, CurrentID );
            IList<ProductKitItem> itemList = kitGroup.ProductKitItems;

            IList<ProductKitItem> newList = new List<ProductKitItem>();

            foreach (ProductKitItem item in itemList)
            {
                if (isDefault)
                {
                    if (item.ProductID == productID)
                    {
                        item.IsDefault = isDefault;
                        item.IsUserDefinedQuantity = isUserDefinedQuantity;
                        item.Quantity = ConvertUtilities.ToInt32( quantity );
                    }
                    else
                    {
                        item.IsDefault = false;
                    }

                    newList.Add( item );
                }
                else
                {
                    if (item.ProductID == productID)
                    {
                        item.IsDefault = isDefault;
                        item.IsUserDefinedQuantity = isUserDefinedQuantity;
                        item.Quantity = ConvertUtilities.ToInt32( quantity );
                    }

                    newList.Add( item );
                }
            }

            kitGroup.ProductKitItems = newList;
            DataAccessContext.ProductKitGroupRepository.Save( kitGroup );
            uxMessage.DisplayMessage( Resources.ProductKitGroupItemMessages.ItemUpdateSuccess );

            uxProductKitGroupItemGrid.EditIndex = -1;
            RefreshGrid();
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
        }
    }

    protected void uxGrid_RowEditing( object sender, GridViewEditEventArgs e )
    {
        uxProductKitGroupItemGrid.EditIndex = e.NewEditIndex;
        RefreshGrid();
    }

    protected void uxGrid_CancelingEdit( object sender, GridViewCancelEditEventArgs e )
    {
        uxProductKitGroupItemGrid.EditIndex = -1;
        RefreshGrid();
    }

    #endregion


    #region Public Properties

    public Culture CurrentCulture
    {
        get
        {
            if (ViewState["CurrentCulture"] == null)
                return new Culture();
            else
                return (Culture) ViewState["CurrentCulture"];
        }
        set
        {
            ViewState["CurrentCulture"] = value;
        }

    }

    #endregion


    #region Public Methods

    public void UpdateLanguage()
    {
        if (!_isDeleteClick)
            RefreshGrid();
    }

    public void AddSuccessMessage()
    {
        uxMessage.DisplayMessage( Resources.ProductKitGroupItemMessages.ItemAddSuccess );
    }

    #endregion
}
