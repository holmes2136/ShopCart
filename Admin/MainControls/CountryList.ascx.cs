using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Vevo;
using Vevo.Domain;
using Vevo.Domain.Users;
using Vevo.Shared.DataAccess;
using Vevo.WebAppLib;
using Vevo.WebUI.Ajax;
using Vevo.WebUI.Users;

public partial class AdminAdvanced_MainControls_CountryList : AdminAdvancedBaseUserControl
{
    private bool _emptyRow = false;
    private int _checkBoxColumn = 0;
    private int _editColunm = 4;

    private bool IsContainingOnlyEmptyRow
    {
        get { return _emptyRow; }
        set { _emptyRow = value; }
    }

    protected void uxGrid_Sorting( object sender, GridViewSortEventArgs e )
    {
        GridHelper.SelectSorting( e.SortExpression );
        RefreshGrid();
    }

    private GridViewHelper GridHelper
    {
        get
        {
            if (ViewState["GridHelper"] == null)
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "CommonName" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private string CurrentCountryCode
    {
        get
        {
            if (ViewState["CountryCode"] == null)
                return string.Empty;
            else
                return ViewState["CountryCode"].ToString();
        }
        set
        {
            ViewState["CountryCode"] = value;
        }
    }

    private void SetFooterRowFocus()
    {
        Control textBox = uxGrid.FooterRow.FindControl( "uxCommonNameText" );
        AjaxUtilities.GetScriptManager( this ).SetFocus( textBox );
    }

    private void CreateDummyRow( IList<Country> countryList )
    {
        Country country = new Country();
        country.CommonName = "Dummy";
        country.Enabled = true;
        countryList.Add( country );
    }


    protected void PopulateControls()
    {

        if (IsAdminModifiable())
        {
            DeleteVisible( true );
        }
        else
        {
            uxAddButton.Visible = false;
            DeleteVisible( false );
        }

        RefreshGrid();
    }

    private void DeleteVisible( bool value )
    {
        uxDeleteButton.Visible = value;
        uxEnabledButton.Visible = value;
        uxDisableButton.Visible = value;
        uxResetButton.Visible = value;
        if (value)
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                uxDeleteConfirmButton.TargetControlID = "uxDeleteButton";
                uxConfirmModalPopup.TargetControlID = "uxDeleteButton";

                uxResetConfirmButton.TargetControlID = "uxResetButton";
                uxReSetConfirmModalPopup.TargetControlID = "uxResetButton";
            }
            else
            {
                uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
                uxConfirmModalPopup.TargetControlID = "uxDummyButton";

                uxResetConfirmButton.TargetControlID = "uxResetDummyButton";
                uxReSetConfirmModalPopup.TargetControlID = "uxResetDummyButton";
            }
        }
        else
        {
            uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
            uxConfirmModalPopup.TargetControlID = "uxDummyButton";
        }
    }

    protected void uxGrid_DataBound( object sender, EventArgs e )
    {
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        bool deleted = false;
        foreach (GridViewRow row in uxGrid.Rows)
        {
            CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
            if (deleteCheck != null &&
                deleteCheck.Checked)
            {
                string countryCode = uxGrid.DataKeys[row.RowIndex]["CountryCode"].ToString();
                DataAccessContext.CountryRepository.Delete( countryCode );
                deleted = true;
            }
        }
        uxGrid.EditIndex = -1;

        if (deleted)
        {
            uxMessage.DisplayMessage( Resources.CustomerMessages.DeleteSuccess );
        }

        RefreshGrid();
    }

    protected void uxEnabledButton_Click( object sender, EventArgs e )
    {
        bool enabled = false;
        foreach (GridViewRow row in uxGrid.Rows)
        {
            CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
            if (deleteCheck != null &&
                deleteCheck.Checked)
            {
                string countryCode = uxGrid.DataKeys[row.RowIndex]["CountryCode"].ToString();
                Country country = DataAccessContext.CountryRepository.GetOne( countryCode );
                country.Enabled = true;
                DataAccessContext.CountryRepository.Update( country );
                enabled = true;
            }
        }
        uxGrid.EditIndex = -1;

        if (enabled)
        {
            uxMessage.DisplayMessage( Resources.CustomerMessages.EnabledSuccess );
        }

        RefreshGrid();

    }

    protected void uxDisableButton_Click( object sender, EventArgs e )
    {
        bool enabled = false;
        foreach (GridViewRow row in uxGrid.Rows)
        {
            CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
            if (deleteCheck != null &&
                deleteCheck.Checked)
            {
                string countryCode = uxGrid.DataKeys[row.RowIndex]["CountryCode"].ToString();
                Country country = DataAccessContext.CountryRepository.GetOne( countryCode );
                country.Enabled = false;
                DataAccessContext.CountryRepository.Update( country );
                enabled = true;
            }
        }
        uxGrid.EditIndex = -1;

        if (enabled)
        {
            uxMessage.DisplayMessage( Resources.CustomerMessages.DisableSuccess );
        }

        RefreshGrid();
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
            PopulateControls();
    }

    protected void uxResetButton_Click( object sender, EventArgs e )
    {
        ResetCountryData();
        RefreshGrid();
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

    private IList<Country> CreateSourceList()
    {
        IList<Country> countryList = DataAccessContext.CountryRepository.GetAll( BoolFilter.ShowAll, GridHelper.GetFullSortText() );

        IList<Country> countryListSource = new List<Country>();

        for (int i = 0; i < countryList.Count; i++)
        {
            countryListSource.Add( countryList[i] );
        }

        return countryListSource;
    }

    private void RefreshGrid()
    {
        IList<Country> countryList = CreateSourceList();

        if (countryList.Count == 0)
        {
            IsContainingOnlyEmptyRow = true;
            CreateDummyRow( countryList );
        }
        else
        {
            IsContainingOnlyEmptyRow = false;
            uxGrid.ShowHeader = true;
        }

        uxGrid.DataSource = countryList;
        uxGrid.DataBind();

        if (IsContainingOnlyEmptyRow)
        {
            uxGrid.Rows[0].Visible = false;
            uxGrid.Rows[0].Controls.Clear();
        }

        RefreshDeleteButton();

        if (uxGrid.ShowFooter)
        {
            Control commonNameText = uxGrid.FooterRow.FindControl( "uxCommonNameText" );
            Control countryCodeText = uxGrid.FooterRow.FindControl( "uxCountryCodeText" );
            Control addButton = uxGrid.FooterRow.FindControl( "uxAddButton" );

            WebUtilities.TieButton( this.Page, commonNameText, addButton );
            WebUtilities.TieButton( this.Page, countryCodeText, addButton );
        }
    }

    private void RefreshDeleteButton()
    {
        if (IsAdminModifiable())
        {
            if (IsContainingOnlyEmptyRow)
                DeleteVisible( false );
            else
                DeleteVisible( true );
        }
        else
        {
            DeleteVisible( false );
            uxGrid.Columns[_checkBoxColumn].Visible = false;
            uxGrid.Columns[_editColunm].Visible = false;
        }
    }

    private bool IsCountryAlreadyExisted( string countryCode )
    {
        Country country = DataAccessContext.CountryRepository.GetOne( countryCode );
        return !country.IsNull;
    }

    protected void uxGrid_RowEditing( object sender, GridViewEditEventArgs e )
    {
        uxGrid.EditIndex = e.NewEditIndex;
        RefreshGrid();
    }

    protected void uxGrid_CancelingEdit( object sender, GridViewCancelEditEventArgs e )
    {
        uxGrid.EditIndex = -1;
        CurrentCountryCode = "";
        RefreshGrid();
    }

    protected void uxGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        if (e.CommandName == "Add")
        {
            try
            {
                string commonName = ((TextBox) uxGrid.FooterRow.FindControl( "uxCommonNameText" )).Text;
                string countryCode = ((TextBox) uxGrid.FooterRow.FindControl( "uxCountryCodeText" )).Text;
                bool enabled = ((CheckBox) uxGrid.FooterRow.FindControl( "uxEnabledCheck" )).Checked;
                Country country = DataAccessContext.CountryRepository.GetOne( countryCode );

                if (country.IsNull)
                {
                    Country newCountry = new Country();
                    newCountry.CountryCode = countryCode;
                    newCountry.CommonName = commonName;
                    newCountry.SortOrder = DataAccessContext.CountryRepository.GetAll( BoolFilter.ShowAll, "CommonName" ).Count;
                    newCountry.Enabled = enabled;

                    DataAccessContext.CountryRepository.Create( newCountry );

                    ((TextBox) uxGrid.FooterRow.FindControl( "uxCommonNameText" )).Text = "";
                    ((TextBox) uxGrid.FooterRow.FindControl( "uxCountryCodeText" )).Text = "";

                    uxMessage.DisplayMessage( Resources.CountryListMessages.AddSuccess );
                }
                else
                    uxMessage.DisplayError( "Country code can't duplicate." );
            }
            catch (Exception ex)
            {
                string message;
                if (ex.InnerException is DuplicatedPrimaryKeyException)
                    message = Resources.CountryListMessages.AddErrorDuplicated;
                else
                    message = Resources.CountryListMessages.AddError;
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
                CurrentCountryCode = e.CommandArgument.ToString();
            }
            catch (Exception ex)
            {
                uxMessage.DisplayError( ex.Message );
            }
        }
    }

    protected void uxGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            string commonName = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxCommonNameText" )).Text;
            string countryCode = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxCountryCodeText" )).Text;
            bool enabled = ((CheckBox) uxGrid.Rows[e.RowIndex].FindControl( "uxEnabledCheck" )).Checked;

            if (!String.IsNullOrEmpty( CurrentCountryCode ))
            {
                if (CurrentCountryCode == countryCode ||
                    !IsCountryAlreadyExisted( countryCode ))
                {
                    Country country = DataAccessContext.CountryRepository.GetOne( CurrentCountryCode );
                    country.CommonName = commonName;
                    country.Enabled = enabled;
                    DataAccessContext.CountryRepository.Update( country, countryCode );
                    uxMessage.DisplayMessage( Resources.CountryListMessages.UpdateSuccess );
                }
                else
                    uxMessage.DisplayError(Resources.CountryListMessages.UpdateErrorDuplicated);
            }

            // End editing
            uxGrid.EditIndex = -1;
            CurrentCountryCode = "";
            RefreshGrid();

           
        }
        catch (Exception ex)
        {
            string message;
            if (ex.InnerException is DuplicatedPrimaryKeyException)
                message = Resources.CountryListMessages.UpdateErrorDuplicated;
            else
                message = Resources.CountryListMessages.UpdateError;
            throw new ApplicationException( message );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
        }
    }

    private void ResetCountryData()
    {
        try
        {
            AddressUtilities.RestoreCountryCode();
            uxMessage.DisplayMessage( "Reset country list successfully." );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayError( ex.Message );
        }
    }
}
