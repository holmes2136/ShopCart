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
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.Domain.Users;
using Vevo.Shared.DataAccess;
using Vevo.WebAppLib;
using Vevo.WebUI.Ajax;
using Vevo.WebUI.Users;

public partial class AdminAdvanced_MainControls_StateList : AdminAdvancedBaseUserControl
{
    private bool _emptyRow = false;
    private int _checkBoxColumn = 0;
    private int _editColunm = 4;

    private bool IsContainingOnlyEmptyRow
    {
        get { return _emptyRow; }
        set { _emptyRow = value; }
    }

    private bool IsStateAlreadyExisted( string stateCode )
    {
        State state = DataAccessContext.StateRepository.GetOne( CountryCode, stateCode );
        return !state.IsNull;
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
                ViewState["GridHelper"] = new GridViewHelper( uxGrid, "StateName" );

            return (GridViewHelper) ViewState["GridHelper"];
        }
    }

    private string CurrentStateCode
    {
        get
        {
            if (ViewState["StateCode"] == null)
                return String.Empty;
            else
                return ViewState["StateCode"].ToString();
        }
        set
        {
            ViewState["StateCode"] = value;
        }
    }

    private string CountryCode
    {
        get
        {
            return MainContext.QueryString["CountryCode"];
        }
    }

    private void SetFooterRowFocus()
    {
        Control textBox = uxGrid.FooterRow.FindControl( "uxStateNameText" );
        AjaxUtilities.GetScriptManager( textBox ).SetFocus( textBox );
    }

    protected void PopulateControls()
    {

        if (IsAdminModifiable())
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
            uxAddButton.Visible = false;
            uxEnabledButton.Visible = false;
            uxDisableButton.Visible = false;
        }

        RefreshGrid();
    }

    protected void uxGrid_DataBound( object sender, EventArgs e )
    {
        // Do not display empty row
        //if (IsContainingOnlyEmptyRow())
        //{
        //    uxGrid.Rows[0].Visible = false;
        //}
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
                string stateCode = uxGrid.DataKeys[row.RowIndex]["StateCode"].ToString();
                DataAccessContext.StateRepository.Delete( CountryCode, stateCode );
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
                string stateCode = uxGrid.DataKeys[row.RowIndex]["StateCode"].ToString();
                State state = DataAccessContext.StateRepository.GetOne( CountryCode, stateCode );
                state.Enabled = true;
                DataAccessContext.StateRepository.Update( state );
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
                string stateCode = uxGrid.DataKeys[row.RowIndex]["StateCode"].ToString();
                State state = DataAccessContext.StateRepository.GetOne( CountryCode, stateCode );
                state.Enabled = false;
                DataAccessContext.StateRepository.Update( state );
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
        {
            PopulateControls();
        }
    }

    protected void uxResetButton_Click( object sender, EventArgs e )
    {
        ResetStateData();
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

    private void InsertEmptyRow( IList<State> stateList )
    {
        State state = new State();
        state.SortOrder = -1;
        state.CountryCode = String.Empty;
        state.StateCode = String.Empty;
        state.StateName = String.Empty;
        state.Enabled = true;
        stateList.Add( state );
    }

    private IList<State> CreateSourceList()
    {
        IList<State> stateList = DataAccessContext.StateRepository.GetAllByCountryCode( CountryCode, "StateName", BoolFilter.ShowAll );

        IList<State> stateListSource = new List<State>();

        for (int i = 0; i < stateList.Count; i++)
        {
            stateListSource.Add( stateList[i] );
        }

        return stateListSource;
    }

    private void RefreshGrid()
    {
        if (!String.IsNullOrEmpty( CountryCode ))
        {
            if (CountryCode == "US" && IsAdminModifiable())
                ResetVisible( true );
            else
                ResetVisible( false );

            IList<State> stateList = CreateSourceList();

            if (stateList.Count == 0)
            {
                IsContainingOnlyEmptyRow = true;
                InsertEmptyRow( stateList );
            }
            else
            {
                IsContainingOnlyEmptyRow = false;
                uxGrid.ShowHeader = true;
            }

            uxGrid.DataSource = stateList;
            uxGrid.DataBind();

            if (IsContainingOnlyEmptyRow)
            {
                uxGrid.Rows[0].Visible = false;
            }

            RefreshDeleteButton();

            if (!uxAddButton.Visible)
            {
                if (IsAdminModifiable())
                {
                    uxGrid.ShowFooter = true;
                }
            }

            if (uxGrid.ShowFooter)
            {
                Control stateNameText = uxGrid.FooterRow.FindControl( "uxStateNameText" );
                Control stateCodeText = uxGrid.FooterRow.FindControl( "uxStateCodeText" );
                Control addButton = uxGrid.FooterRow.FindControl( "uxAddButton" );

                WebUtilities.TieButton( this.Page, stateNameText, addButton );
                WebUtilities.TieButton( this.Page, stateCodeText, addButton );
            }
        }
        else
            MainContext.RedirectMainControl( "CountryList.ascx", "" );
    }

    private void ResetVisible( bool value )
    {
        uxResetButton.Visible = value;
        if (value)
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                uxResetConfirmButton.TargetControlID = "uxResetButton";
                uxReSetConfirmModalPopup.TargetControlID = "uxResetButton";
            }
            else
            {
                uxResetConfirmButton.TargetControlID = "uxResetDummyButton";
                uxReSetConfirmModalPopup.TargetControlID = "uxResetDummyButton";
            }
        }
        else
        {
            uxResetConfirmButton.TargetControlID = "uxResetDummyButton";
            uxReSetConfirmModalPopup.TargetControlID = "uxResetDummyButton";
        }
    }

    private void DeleteVisible( bool value )
    {
        uxDeleteButton.Visible = value;
        uxEnabledButton.Visible = value;
        uxDisableButton.Visible = value;
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

    protected void uxGrid_RowEditing( object sender, GridViewEditEventArgs e )
    {
        uxGrid.EditIndex = e.NewEditIndex;
        RefreshGrid();
    }

    protected void uxGrid_CancelingEdit( object sender, GridViewCancelEditEventArgs e )
    {
        uxGrid.EditIndex = -1;
        CurrentStateCode = "";
        RefreshGrid();
    }

    protected void uxGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        if (e.CommandName == "Add")
        {
            try
            {
                string stateName = ((TextBox) uxGrid.FooterRow.FindControl( "uxStateNameText" )).Text;
                string stateCode = ((TextBox) uxGrid.FooterRow.FindControl( "uxStateCodeText" )).Text;
                bool enabled = ((CheckBox) uxGrid.FooterRow.FindControl( "uxEnabledCheck" )).Checked;
                State state = DataAccessContext.StateRepository.GetOne( CountryCode, stateCode );

                if (state.IsNull)
                {
                    State newState = new State();
                    newState.CountryCode = CountryCode;
                    newState.StateCode = stateCode;
                    newState.StateName = stateName;
                    newState.SortOrder = DataAccessContext.StateRepository.GetAllByCountryCode( CountryCode, "", BoolFilter.ShowAll ).Count;
                    newState.Enabled = enabled;
                    DataAccessContext.StateRepository.Create( newState );

                    ((TextBox) uxGrid.FooterRow.FindControl( "uxStateNameText" )).Text = "";
                    ((TextBox) uxGrid.FooterRow.FindControl( "uxStateCodeText" )).Text = "";

                    uxMessage.DisplayMessage( Resources.StateListMessages.AddSuccess );
                }
                else
                    uxMessage.DisplayError( "State code can't duplicate." );
            }
            catch (Exception ex)
            {
                string message;
                if (ex.InnerException is DuplicatedPrimaryKeyException)
                    message = Resources.StateListMessages.AddErrorDuplicated;
                else
                    message = Resources.StateListMessages.AddError;
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
                CurrentStateCode = e.CommandArgument.ToString();
                uxGrid.ShowFooter = false;
                uxAddButton.Visible = true;
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
            string stateName = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxStateNameText" )).Text;
            string stateCode = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxStateCodeText" )).Text;
            bool enabled = ((CheckBox) uxGrid.Rows[e.RowIndex].FindControl( "uxEnabledCheck" )).Checked;

            if (!String.IsNullOrEmpty( CurrentStateCode ))
            {
                if (CurrentStateCode == stateCode ||
                    !IsStateAlreadyExisted( stateCode ))
                {
                    State state = DataAccessContext.StateRepository.GetOne( CountryCode, CurrentStateCode );
                    state.StateName = stateName;
                    state.Enabled = enabled;
                    DataAccessContext.StateRepository.Update( state, stateCode );
                    uxMessage.DisplayMessage( Resources.StateListMessages.UpdateSuccess );
                }
                else
                    uxMessage.DisplayError(Resources.StateListMessages.UpdateErrorDuplicated);
            }

            // End editing
            uxGrid.EditIndex = -1;
            CurrentStateCode = "";
            RefreshGrid();
           
        }
        catch (Exception ex)
        {
            string message;
            if (ex.InnerException is DuplicatedPrimaryKeyException)
                message = Resources.StateListMessages.UpdateErrorDuplicated;
            else
                message = Resources.StateListMessages.UpdateError;
            throw new ApplicationException( message );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
        }
    }

    private void ResetStateData()
    {
        try
        {
            AddressUtilities.RestoreDefaultStateCode();
            uxMessage.DisplayMessage( "Reset state list successfully." );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayError( ex.Message );
        }
    }
}
