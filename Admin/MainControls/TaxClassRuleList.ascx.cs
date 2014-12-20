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
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Tax;
using Vevo.DataAccessLib.Cart;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.WebUI.Ajax;
using Vevo.Base.Domain;

public partial class AdminAdvanced_MainControls_TaxClassRuleList : AdminAdvancedBaseUserControl
{
    private bool _emptyRow = false;

    private bool IsContainingOnlyEmptyRow
    {
        get { return _emptyRow; }
        set { _emptyRow = value; }
    }

    private string TaxClassID
    {
        get
        {
            return MainContext.QueryString["TaxClassID"];
        }
    }

    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxTaxClassRuleGrid, "" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private string CurrentTaxClassRuleID
    {
        get
        {
            if (ViewState["CurrentTaxClassRuleID"] == null)
                return string.Empty;
            else
                return ViewState["CurrentTaxClassRuleID"].ToString();
        }
        set
        {
            ViewState["CurrentTaxClassRuleID"] = value;
        }
    }

    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.TaxClassRepository.GetTaxClassRuleTableSchemas();

        uxSearchFilter.SetUpSchema( list, "TaxClassID" );
    }

    private void SetFooterRowFocus()
    {
        Control countryList = uxTaxClassRuleGrid.FooterRow.FindControl( "uxCountryList" );
        AjaxUtilities.GetScriptManager( this ).SetFocus( countryList );
    }

    private void CreateDummyRow( IList<TaxClassRule> list )
    {
        TaxClassRule taxClassRule = new TaxClassRule();
        taxClassRule.TaxClassRuleID = "-1";
        taxClassRule.TaxClassID = "";

        list.Add( taxClassRule );
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

        if (uxTaxClassRuleGrid.Rows.Count > 0)
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

        IList<TaxClassRule> list = DataAccessContext.TaxClassRepository.SearchTaxClassRules(
            TaxClassID,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            (uxPagingControl.CurrentPage - 1) * uxPagingControl.ItemsPerPages,
            (uxPagingControl.CurrentPage * uxPagingControl.ItemsPerPages) - 1,
            out totalItems );


        if (list == null || list.Count == 0)
        {
            IsContainingOnlyEmptyRow = true;
            list = new List<TaxClassRule>();
            CreateDummyRow( list );
        }
        else
        {
            IsContainingOnlyEmptyRow = false;
        }

        uxTaxClassRuleGrid.DataSource = list;
        uxTaxClassRuleGrid.DataBind();
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        RefreshDeleteButton();

        ((AdminAdvanced_Components_CountryList) uxTaxClassRuleGrid.FooterRow.FindControl( "uxCountryList" )).Refresh();

        ResetCountryState();
    }

    private void RefreshDeleteButton()
    {
        if (IsContainingOnlyEmptyRow)
            uxDeleteButton.Visible = false;
        else
            uxDeleteButton.Visible = true;
    }

    private void AddDefaultTaxRule( string countryCode, string stateCode, string zipCode )
    {
        if ((!String.IsNullOrEmpty( stateCode )) && String.IsNullOrEmpty( zipCode ))
        {
            TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( TaxClassID );
            TaxClassRule taxClassRule = new TaxClassRule();

            for (int i = 0; i < taxClass.TaxClassRule.Count; i++)
            {
                if (taxClass.TaxClassRule[i].CountryCode == countryCode
                    && String.IsNullOrEmpty( taxClass.TaxClassRule[i].StateCode )
                    && String.IsNullOrEmpty( taxClass.TaxClassRule[i].ZipCode ))
                {
                    taxClassRule = taxClass.TaxClassRule[i];
                    break;
                }
            }

            if (taxClassRule.TaxClassRuleID == "0")
            {
                taxClassRule.CountryCode = countryCode;
                taxClassRule.IsDefaultCountry = false;
                taxClass.TaxClassRule.Add( taxClassRule );
            }
            else
            {
                taxClassRule.IsDefaultState = true;
                taxClassRule.IsDefaultZip = true;
            }

            DataAccessContext.TaxClassRepository.Save( taxClass );

        }
        else if (!String.IsNullOrEmpty( zipCode ))
        {
            TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( TaxClassID );
            TaxClassRule taxClassRule = new TaxClassRule();

            for (int i = 0; i < taxClass.TaxClassRule.Count; i++)
            {
                if (taxClass.TaxClassRule[i].CountryCode == countryCode
                    && taxClass.TaxClassRule[i].StateCode == stateCode
                    && String.IsNullOrEmpty( taxClass.TaxClassRule[i].ZipCode ))
                {
                    taxClassRule = taxClass.TaxClassRule[i];
                    break;
                }
            }

            if (taxClassRule.TaxClassRuleID == "0")
            {
                taxClassRule.CountryCode = countryCode;
                taxClassRule.StateCode = stateCode;
                taxClassRule.IsDefaultCountry = false;
                taxClassRule.IsDefaultState = false;
                taxClass.TaxClassRule.Add( taxClassRule );

                bool createDefaultState = true;
                if (!String.IsNullOrEmpty( stateCode ))
                {
                    for (int i = 0; i < taxClass.TaxClassRule.Count; i++)
                    {
                        if (taxClass.TaxClassRule[i].CountryCode == countryCode
                            && taxClass.TaxClassRule[i].StateCode == String.Empty)
                        {
                            taxClassRule = taxClass.TaxClassRule[i];
                            createDefaultState = false;
                            break;
                        }
                    }
                    if (createDefaultState)
                    {
                        taxClassRule = new TaxClassRule();
                        taxClassRule.CountryCode = countryCode;
                        taxClassRule.IsDefaultCountry = false;
                        taxClass.TaxClassRule.Add( taxClassRule );
                    }
                    else
                    {
                        taxClassRule.CountryCode = countryCode;
                        taxClassRule.IsDefaultState = true;
                    }
                }
            }
            else
            {
                taxClassRule.IsDefaultState = false;
                taxClassRule.IsDefaultZip = true;
            }
            DataAccessContext.TaxClassRepository.Save( taxClass );
        }
    }

    private bool IsExistCountry( TaxClassRule taxClassRule )
    {
        TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( TaxClassID );

        for (int i = 0; i < taxClass.TaxClassRule.Count; i++)
        {
            if (taxClass.TaxClassRule[i].CountryCode == taxClassRule.CountryCode
                && String.IsNullOrEmpty( taxClass.TaxClassRule[i].StateCode )
                && String.IsNullOrEmpty( taxClass.TaxClassRule[i].ZipCode )
                && taxClass.TaxClassRule[i].IsDefaultCountry == false
                && taxClass.TaxClassRule[i].TaxClassRuleID != taxClassRule.TaxClassRuleID)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsExistState( TaxClassRule taxClassRule )
    {
        TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( TaxClassID );

        for (int i = 0; i < taxClass.TaxClassRule.Count; i++)
        {
            if (taxClass.TaxClassRule[i].CountryCode == taxClassRule.CountryCode
                && taxClass.TaxClassRule[i].StateCode == taxClassRule.StateCode
                && String.IsNullOrEmpty( taxClass.TaxClassRule[i].ZipCode )
                && taxClass.TaxClassRule[i].IsDefaultCountry == false
                && taxClass.TaxClassRule[i].IsDefaultState == false
                && taxClass.TaxClassRule[i].TaxClassRuleID != taxClassRule.TaxClassRuleID)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsExistZip( TaxClassRule taxClassRule )
    {
        TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( TaxClassID );

        for (int i = 0; i < taxClass.TaxClassRule.Count; i++)
        {
            if (taxClass.TaxClassRule[i].CountryCode == taxClassRule.CountryCode
                && taxClass.TaxClassRule[i].StateCode == taxClassRule.StateCode
                && taxClass.TaxClassRule[i].ZipCode == taxClassRule.ZipCode
                && taxClass.TaxClassRule[i].IsDefaultCountry == false
                && taxClass.TaxClassRule[i].IsDefaultState == false
                && taxClass.TaxClassRule[i].IsDefaultZip == false
                && taxClass.TaxClassRule[i].TaxClassRuleID != taxClassRule.TaxClassRuleID)
            {
                return true;
            }
        }
        return false;
    }

    private bool IsExist( TaxClassRule taxClassRule, out string existMessage )
    {
        if (taxClassRule.IsDefaultCountry || taxClassRule.IsDefaultState || taxClassRule.IsDefaultZip)
        {
            existMessage = String.Empty;
            return false;
        }
        else if (String.IsNullOrEmpty( taxClassRule.StateCode ) && String.IsNullOrEmpty( taxClassRule.ZipCode ))
        {
            existMessage = Resources.TaxClassRuleMessages.AddErrorDuplicatedCountry;
            return IsExistCountry( taxClassRule );
        }
        else if (String.IsNullOrEmpty( taxClassRule.ZipCode ))
        {
            existMessage = Resources.TaxClassRuleMessages.AddErrorDuplicatedState;
            return IsExistState( taxClassRule );
        }
        else
        {
            existMessage = Resources.TaxClassRuleMessages.AddErrorDuplicatedZip;
            return IsExistZip( taxClassRule );
        }
    }

    private TaxClassRule GetEditDetails( int index )
    {
        TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( TaxClassID );
        TaxClassRule taxClassRule = new TaxClassRule();

        for (int i = 0; i < taxClass.TaxClassRule.Count; i++)
        {
            if (taxClass.TaxClassRule[i].TaxClassRuleID ==
                ((Label) uxTaxClassRuleGrid.Rows[index].FindControl( "uxTaxClassRuleIDLabel" )).Text.Trim())
            {
                taxClassRule = taxClass.TaxClassRule[i];
            }
        }

        taxClassRule.TaxRate = ConvertUtilities.ToDecimal(
            ((TextBox) uxTaxClassRuleGrid.Rows[index].FindControl( "uxTaxRateText" )).Text );

        return taxClassRule;
    }

    private TaxClassRule GetAddDetails()
    {
        TaxClassRule taxClassRule = new TaxClassRule();

        taxClassRule.TaxClassRuleID = String.Empty;
        taxClassRule.CountryCode =
            ((AdminAdvanced_Components_CountryList) uxTaxClassRuleGrid.FooterRow.FindControl( "uxCountryList" )).CurrentSelected;

        taxClassRule.IsDefaultCountry = false;
        taxClassRule.StateCode = ((AdminAdvanced_Components_StateList) uxTaxClassRuleGrid.FooterRow.FindControl( "uxStateList" )).CurrentSelected;
        taxClassRule.IsDefaultState = false;
        taxClassRule.ZipCode = ((TextBox) uxTaxClassRuleGrid.FooterRow.FindControl( "uxZipCodeText" )).Text.Trim();
        taxClassRule.IsDefaultZip = false;
        taxClassRule.TaxRate = ConvertUtilities.ToDecimal(
                ((TextBox) uxTaxClassRuleGrid.FooterRow.FindControl( "uxTaxRateText" )).Text );

        return taxClassRule;
    }

    private void ResetCountryState()
    {
        if (uxTaxClassRuleGrid.ShowFooter)
        {
            ((AdminAdvanced_Components_StateList) uxTaxClassRuleGrid.FooterRow.FindControl( "uxStateList" )).CountryCode = "";
            ((AdminAdvanced_Components_CountryList) uxTaxClassRuleGrid.FooterRow.FindControl( "uxCountryList" )).CurrentSelected = "";
            ((AdminAdvanced_Components_StateList) uxTaxClassRuleGrid.FooterRow.FindControl( "uxStateList" )).CurrentSelected = "";
        }
    }

    protected void uxScarchChange( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    protected void uxChangePage( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    protected void uxTaxClassRuleGrid_DataBound( object sender, EventArgs e )
    {
        if (IsContainingOnlyEmptyRow)
        {
            uxTaxClassRuleGrid.Rows[0].Visible = false;
        }
    }

    private bool HaveChildState( string countryCode )
    {
        TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( TaxClassID );

        for (int i = 0; i < taxClass.TaxClassRule.Count; i++)
        {
            if (taxClass.TaxClassRule[i].CountryCode == countryCode
                && taxClass.TaxClassRule[i].IsDefaultCountry == false
                && taxClass.TaxClassRule[i].IsDefaultState == false
                && taxClass.TaxClassRule[i].IsDefaultZip == false)
            {
                return true;
            }
        }
        return false;
    }

    private bool HaveChildZip( string countryCode, string stateCode )
    {
        TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( TaxClassID );

        for (int i = 0; i < taxClass.TaxClassRule.Count; i++)
        {
            if (taxClass.TaxClassRule[i].CountryCode == countryCode
                && taxClass.TaxClassRule[i].StateCode == stateCode
                && taxClass.TaxClassRule[i].IsDefaultCountry == false
                && taxClass.TaxClassRule[i].IsDefaultState == false
                && taxClass.TaxClassRule[i].IsDefaultZip == false)
            {
                return true;
            }
        }
        return false;
    }

    private void SetupDefaultTaxRule( string countryCode, string stateCode, string zipCode )
    {
        if ((!String.IsNullOrEmpty( stateCode )) && String.IsNullOrEmpty( zipCode ))
        {
            TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( TaxClassID );
            TaxClassRule taxClassRule = new TaxClassRule();

            for (int i = 0; i < taxClass.TaxClassRule.Count; i++)
            {
                if (taxClass.TaxClassRule[i].CountryCode == countryCode
                    && String.IsNullOrEmpty( taxClass.TaxClassRule[i].StateCode )
                    && String.IsNullOrEmpty( taxClass.TaxClassRule[i].ZipCode ))
                {
                    taxClassRule = taxClass.TaxClassRule[i];
                }
            }

            if (!HaveChildState( countryCode ))
            {
                if (!HaveChildZip( countryCode, stateCode ))
                {
                    taxClassRule.IsDefaultState = false;
                    taxClassRule.IsDefaultZip = false;
                }
                else
                {
                    taxClassRule.IsDefaultState = false;
                    taxClassRule.IsDefaultZip = true;
                }
                DataAccessContext.TaxClassRepository.Save( taxClass );
            }
            else
            {
                taxClassRule.IsDefaultState = true;
                taxClassRule.IsDefaultZip = true;
                DataAccessContext.TaxClassRepository.Save( taxClass );
            }



        }
        else if (!String.IsNullOrEmpty( zipCode ))
        {
            TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( TaxClassID );
            TaxClassRule taxClassRule = new TaxClassRule();

            for (int i = 0; i < taxClass.TaxClassRule.Count; i++)
            {
                if (taxClass.TaxClassRule[i].CountryCode == countryCode
                    && taxClass.TaxClassRule[i].StateCode == stateCode)
                {
                    taxClassRule = taxClass.TaxClassRule[i];
                }
            }

            if (!HaveChildZip( countryCode, stateCode ))
            {
                taxClassRule.IsDefaultState = false;
                taxClassRule.IsDefaultZip = false;

                DataAccessContext.TaxClassRepository.Save( taxClass );
            }
        }
    }

    private string DeleteItem( string taxClassRuleID )
    {
        int deleteIndex = -1;

        TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( TaxClassID );
        TaxClassRule taxClassRule = new TaxClassRule();
        for (int i = 0; i < taxClass.TaxClassRule.Count; i++)
        {
            if (taxClass.TaxClassRule[i].TaxClassRuleID == taxClassRuleID)
            {
                taxClassRule = taxClass.TaxClassRule[i];
                deleteIndex = i;
            }
        }

        string message = String.Empty;
        if (taxClassRule.IsDefaultState && taxClassRule.IsDefaultZip)
        {
            if (HaveChildState( taxClassRule.CountryCode ))
                message = Resources.TaxClassRuleMessages.DeleteDefaultStateError;
            else
            {
                if (deleteIndex > -1)
                {
                    taxClass.TaxClassRule.RemoveAt( deleteIndex );
                    DataAccessContext.TaxClassRepository.Save( taxClass );
                }
            }
        }
        else if (taxClassRule.IsDefaultZip)
        {
            if (HaveChildZip( taxClassRule.CountryCode, taxClassRule.StateCode ))
                message = Resources.TaxClassRuleMessages.DeleteDefaultZipError;
            else
            {
                if (deleteIndex > -1)
                {
                    taxClass.TaxClassRule.RemoveAt( deleteIndex );
                    DataAccessContext.TaxClassRepository.Save( taxClass );
                }
            }
        }
        else
        {
            if (deleteIndex > -1)
            {
                taxClass.TaxClassRule.RemoveAt( deleteIndex );
                DataAccessContext.TaxClassRepository.Save( taxClass );
            }
            SetupDefaultTaxRule( taxClassRule.CountryCode, taxClassRule.StateCode, taxClassRule.ZipCode );
        }
        return message;
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        string errorMessage = String.Empty;

        foreach (GridViewRow row in uxTaxClassRuleGrid.Rows)
        {
            CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
            if (deleteCheck != null &&
                deleteCheck.Checked)
            {
                string taxClassRuleID = uxTaxClassRuleGrid.DataKeys[row.RowIndex]["TaxClassRuleID"].ToString();
                string message = DeleteItem( taxClassRuleID );

                if (!String.IsNullOrEmpty( message ))
                {
                    if (!String.IsNullOrEmpty( errorMessage ))
                        errorMessage += "<br />";
                    errorMessage += message;
                }
            }
        }

        if (String.IsNullOrEmpty( errorMessage ))
            uxMessage.DisplayMessage( Resources.TaxClassRuleMessages.DeleteSuccess );
        else
            uxMessage.DisplayError( errorMessage );

        RefreshGrid();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (String.IsNullOrEmpty( TaxClassID ))
            MainContext.RedirectMainControl( "TaxClassList.ascx" );

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
        uxTaxClassRuleGrid.EditIndex = -1;
        uxTaxClassRuleGrid.ShowFooter = true;
        uxTaxClassRuleGrid.ShowHeader = true;
        RefreshGrid();

        uxAddButton.Visible = false;


        AdminAdvanced_Components_CountryList myCountry = ((AdminAdvanced_Components_CountryList) uxTaxClassRuleGrid.FooterRow.FindControl( "uxCountryList" ));
        myCountry.CurrentSelected = "";
        SetFooterRowFocus();
    }

    protected void uxTaxClassRuleGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
        RefreshGrid();
    }

    protected void uxTaxClassRuleGrid_RowEditing( object sender, GridViewEditEventArgs e )
    {
        uxTaxClassRuleGrid.EditIndex = e.NewEditIndex;
        RefreshGrid();
    }

    protected void uxTaxClassRuleGrid_CancelingEdit( object sender, GridViewCancelEditEventArgs e )
    {
        uxTaxClassRuleGrid.EditIndex = -1;
        CurrentTaxClassRuleID = "";
        RefreshGrid();
    }

    protected void uxTaxClassRuleGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        if (e.CommandName == "Add")
        {
            try
            {
                TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( TaxClassID );
                TaxClassRule taxClassRule = GetAddDetails();

                if (String.IsNullOrEmpty( taxClassRule.CountryCode ))
                {
                    uxMessage.DisplayError( Resources.TaxClassRuleMessages.AddErrorSelectCountry );
                    return;
                }
                string existMessage;
                if (!IsExist( taxClassRule, out existMessage ))
                {
                    taxClass.TaxClassRule.Add( taxClassRule );
                    DataAccessContext.TaxClassRepository.Save( taxClass );
                    AddDefaultTaxRule( taxClassRule.CountryCode, taxClassRule.StateCode, taxClassRule.ZipCode );

                    uxMessage.DisplayMessage( Resources.TaxClassRuleMessages.AddSuccess );
                    existMessage = String.Empty;
                }
                else
                    uxMessage.DisplayError( existMessage );

                ((TextBox) uxTaxClassRuleGrid.FooterRow.FindControl( "uxZipCodeText" )).Text = "";
                ((TextBox) uxTaxClassRuleGrid.FooterRow.FindControl( "uxTaxRateText" )).Text = "";
            }
            catch (Exception)
            {
                string message = Resources.TaxClassRuleMessages.AddError;
                throw new VevoException( message );
            }
            finally
            {
            }
            RefreshGrid();
        }
        if (e.CommandName == "Edit")
        {
            try
            {
                CurrentTaxClassRuleID = e.CommandArgument.ToString();
                uxTaxClassRuleGrid.ShowFooter = false;
                uxAddButton.Visible = true;
            }
            catch (Exception ex)
            {
                uxMessage.DisplayError( ex.Message );
            }
        }
    }

    protected void uxTaxClassRuleGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            TaxClass taxClass = DataAccessContext.TaxClassRepository.GetOne( TaxClassID );
            TaxClassRule taxClassRule = GetEditDetails( e.RowIndex );

            string existMessage;
            if (!IsExist( taxClassRule, out existMessage ))
            {
                DataAccessContext.TaxClassRepository.Save( taxClass );

                // End editing
                uxTaxClassRuleGrid.EditIndex = -1;
                CurrentTaxClassRuleID = "";
                RefreshGrid();

                existMessage = String.Empty;
                uxMessage.DisplayMessage( Resources.TaxClassRuleMessages.UpdateSuccess );
            }
            else
                uxMessage.DisplayError( existMessage );
        }
        catch (Exception)
        {
            string message = Resources.TaxClassRuleMessages.UpdateError;
            throw new ApplicationException( message );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
        }
    }

    protected void uxState_RefreshHandler( object sender, EventArgs e )
    {
        int index = uxTaxClassRuleGrid.EditIndex;
        AdminAdvanced_Components_StateList stateList =
            (AdminAdvanced_Components_StateList) uxTaxClassRuleGrid.Rows[index].FindControl( "uxStateList" );
        AdminAdvanced_Components_CountryList countryList =
            (AdminAdvanced_Components_CountryList) uxTaxClassRuleGrid.Rows[index].FindControl( "uxCountryList" );
        stateList.CountryCode = countryList.CurrentSelected;
        stateList.Refresh();
    }

    protected void uxStateFooter_RefreshHandler( object sender, EventArgs e )
    {
        AdminAdvanced_Components_StateList stateList = (AdminAdvanced_Components_StateList) uxTaxClassRuleGrid.FooterRow.FindControl( "uxStateList" );
        AdminAdvanced_Components_CountryList countryList = (AdminAdvanced_Components_CountryList) uxTaxClassRuleGrid.FooterRow.FindControl( "uxCountryList" );
        stateList.CountryCode = countryList.CurrentSelected;
        stateList.Refresh();
    }

    protected string GetCountryText( object countryCode, object isDefaultCountry )
    {
        if (ConvertUtilities.ToBoolean( isDefaultCountry ))
        {
            IList<TaxClassRule> list = DataAccessContext.TaxClassRepository.GetTaxClassRulesByTaxClassID( TaxClassID );
            if (list.Count == 1)
                return "Everywhere in the world";
            else
                return "The rest of the world";
        }
        else
            return countryCode.ToString();
    }

    protected string GetStateText( object stateCode, object isDefaultCountry, object isDefaultstate )
    {
        if (!ConvertUtilities.ToBoolean( isDefaultCountry ) && ConvertUtilities.ToBoolean( isDefaultstate ))
            return "All other state";
        else
            return stateCode.ToString();
    }

    protected string GetZipText( object zipCode, object isDefaultCountry, object isDefaultstate, object isDefaultZip )
    {
        if (!ConvertUtilities.ToBoolean( isDefaultCountry ) &&
            !ConvertUtilities.ToBoolean( isDefaultstate ) &&
            ConvertUtilities.ToBoolean( isDefaultZip ))
            return "All other zip";
        else
            return zipCode.ToString();
    }

    protected bool IsVisible( object isDefaultCountry, object isDefaultstate, object isDefaultZip )
    {
        return !(ConvertUtilities.ToBoolean( isDefaultCountry ) &&
            ConvertUtilities.ToBoolean( isDefaultstate ) &&
            ConvertUtilities.ToBoolean( isDefaultZip ));
    }

}
