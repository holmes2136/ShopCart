using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Domain.Tax;
using Vevo.WebAppLib;
using Vevo.WebUI.Ajax;
using Vevo.Base.Domain;

public partial class AdminAdvanced_MainControls_TaxClassList : AdminAdvancedBaseUserControl
{
    private bool _emptyRow = false;

    private bool IsContainingOnlyEmptyRow
    {
        get { return _emptyRow; }
        set { _emptyRow = value; }
    }

    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxTaxClassGrid, "TaxClassID" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.TaxClassRepository.GetTableSchemas();
        uxSearchFilter.SetUpSchema( list, "TaxClassID" );
    }

    private void SetFooterRowFocus()
    {
        Control textBox = uxTaxClassGrid.FooterRow.FindControl( "uxNameText" );
        AjaxUtilities.GetScriptManager( this ).SetFocus( textBox );
    }

    private void CreateDummyRow( IList<TaxClass> list )
    {
        TaxClass taxClass = new TaxClass();
        taxClass.TaxClassID = "-1";

        list.Add( taxClass );
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

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

        if (uxTaxClassGrid.Rows.Count > 0)
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

    private void RefreshGrid()
    {
        int totalItems;

        IList<TaxClass> list = DataAccessContext.TaxClassRepository.SearchTaxClasses(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            (uxPagingControl.CurrentPage - 1) * uxPagingControl.ItemsPerPages,
            (uxPagingControl.CurrentPage * uxPagingControl.ItemsPerPages) - 1,
            out totalItems );

        if (list == null || list.Count == 0)
        {
            IsContainingOnlyEmptyRow = true;
            list = new List<TaxClass>();
            CreateDummyRow( list );
        }
        else
        {
            IsContainingOnlyEmptyRow = false;
        }

        uxTaxClassGrid.DataSource = list;
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
        uxTaxClassGrid.DataBind();

        RefreshDeleteButton();

        if (uxTaxClassGrid.ShowFooter)
        {
            Control nameText = uxTaxClassGrid.FooterRow.FindControl( "uxNameText" );
            Control addButton = uxTaxClassGrid.FooterRow.FindControl( "uxAddLinkButton" );

            WebUtilities.TieButton( this.Page, nameText, addButton );
        }
    }

    private void RefreshDeleteButton()
    {
        if (IsContainingOnlyEmptyRow)
            uxDeleteButton.Visible = false;
        else
            uxDeleteButton.Visible = true;
    }

    private string[] GetCheckedIDs()
    {
        ArrayList items = new ArrayList();

        foreach (GridViewRow row in uxTaxClassGrid.Rows)
        {
            CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
            if (deleteCheck.Checked)
            {
                string id = uxTaxClassGrid.DataKeys[row.RowIndex]["TaxClassID"].ToString().Trim();
                items.Add( id );
            }
        }

        string[] result = new string[items.Count];
        items.CopyTo( result );
        return result;
    }

    private bool ContainsProducts( string[] idArray, out string containingTaxClassID )
    {
        string storeID = new StoreRetriever().GetCurrentStoreID();
        foreach (string id in idArray)
        {
            IList<Product> productList = DataAccessContext.ProductRepository.GetAllByTaxClassID( Culture.Null, id, storeID );
            if (productList.Count > 0)
            {
                containingTaxClassID = id;
                return true;
            }
        }

        containingTaxClassID = "";
        return false;
    }

    protected void uxScarchChange( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    protected void uxChangePage( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    protected void uxTaxClassGrid_DataBound( object sender, EventArgs e )
    {
        if (IsContainingOnlyEmptyRow)
        {
            uxTaxClassGrid.Rows[0].Visible = false;
        }
    }

    private void DeleteItems( string[] checkedIDs )
    {
        try
        {
            bool deleted = false;
            foreach (string id in checkedIDs)
            {
                DataAccessContext.TaxClassRepository.Delete( id );
                deleted = true;
            }
            uxTaxClassGrid.EditIndex = -1;

            if (deleted)
                uxMessage.DisplayMessage( Resources.TaxClassMessages.DeleteSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        string[] checkedIDs = GetCheckedIDs();

        string containingID;
        if (ContainsProducts( checkedIDs, out containingID ))
        {
            TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( containingID );
            uxMessage.DisplayError(
                Resources.TaxClassMessages.DeleteErrorContainingProducts,
                taxClass.TaxClassName, containingID );
        }
        else
        {
            DeleteItems( checkedIDs );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxSearchFilter.BubbleEvent += new EventHandler( uxScarchChange );
        uxPagingControl.BubbleEvent += new EventHandler( uxChangePage );

        if (!MainContext.IsPostBack)
            SetUpSearchFilter();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        uxTaxClassGrid.EditIndex = -1;
        uxTaxClassGrid.ShowFooter = true;
        uxTaxClassGrid.ShowHeader = true;
        RefreshGrid();

        uxAddButton.Visible = false;

        SetFooterRowFocus();
    }

    protected void uxTaxClassGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
        RefreshGrid();
    }

    protected void uxTaxClassGrid_RowEditing( object sender, GridViewEditEventArgs e )
    {
        uxTaxClassGrid.EditIndex = e.NewEditIndex;
        RefreshGrid();
    }

    protected void uxTaxClassGrid_CancelingEdit( object sender, GridViewCancelEditEventArgs e )
    {
        uxTaxClassGrid.EditIndex = -1;
        RefreshGrid();
    }

    protected void uxTaxClassGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        if (e.CommandName == "Add")
        {
            try
            {
                string name = ((TextBox) uxTaxClassGrid.FooterRow.FindControl( "uxNameText" )).Text;

                TaxClass taxClass = new TaxClass();
                taxClass.TaxClassName = name;
                TaxClassRule taxClassRule = new TaxClassRule();
                taxClass.TaxClassRule.Add( taxClassRule );
                DataAccessContext.TaxClassRepository.Save( taxClass );

                ((TextBox) uxTaxClassGrid.FooterRow.FindControl( "uxNameText" )).Text = "";

                uxMessage.DisplayMessage( Resources.TaxClassMessages.AddSuccess );

            }
            catch (Exception)
            {
                string message = Resources.TaxClassMessages.AddError;
                throw new VevoException( message );
            }
            finally
            {
            }
            RefreshGrid();
        }

        if (e.CommandName == "Edit")
        {
            uxTaxClassGrid.ShowFooter = false;
            uxAddButton.Visible = true;
        }
    }

    protected void uxTaxClassGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            string taxClassID = ((Label) uxTaxClassGrid.Rows[e.RowIndex].FindControl( "uxTaxClassIDLabel" )).Text;
            string name = ((TextBox) uxTaxClassGrid.Rows[e.RowIndex].FindControl( "uxNameText" )).Text;

            if (!String.IsNullOrEmpty( taxClassID ))
            {
                TaxClass taxClass = new TaxClass();
                taxClass.TaxClassID = taxClassID;
                taxClass.TaxClassName = name;
                DataAccessContext.TaxClassRepository.Save( taxClass );
            }

            // End editing
            uxTaxClassGrid.EditIndex = -1;
            RefreshGrid();

            uxMessage.DisplayMessage( Resources.TaxClassMessages.UpdateSuccess );
        }
        catch (Exception)
        {
            string message = Resources.TaxClassMessages.UpdateError;
            throw new ApplicationException( message );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
        }
    }

}
