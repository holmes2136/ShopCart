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
using Vevo.Domain;
using Vevo.Shared.DataAccess;
using Vevo.WebAppLib;
using Vevo.WebUI.Ajax;

public partial class AdminAdvanced_MainControls_LanguagePageDetails : AdminAdvancedBaseUserControl
{
    private IList<Culture> _cultureList = null;
    private const int languageColumnIndex = 2;


    private string CurrentPageID
    {
        get
        {
            if (String.IsNullOrEmpty( MainContext.QueryString["PageID"] ))
                return "0";
            else
                return MainContext.QueryString["PageID"];
        }
    }


    private bool IsContainingOnlyEmptyRow()
    {
        if (uxGrid.Rows.Count == 1 &&
            (int) uxGrid.DataKeys[0]["PageID"] == -1)
            return true;
        else
            return false;
    }

    private string GetCultureIDByDisplayName( string displayName )
    {
        if (_cultureList == null)
            _cultureList = DataAccessContext.CultureRepository.GetAll();

        for (int i = 0; i < _cultureList.Count; i++)
        {
            if (_cultureList[i].DisplayName == displayName)
                return _cultureList[i].CultureID;
        }

        return null;
    }

    private void RefreshDeleteButton()
    {
        if (IsAdminModifiable())
        {
            if (IsContainingOnlyEmptyRow())
                DeleteVisible( false );
            else
                DeleteVisible( true );
        }
        else
        {
            DeleteVisible( false );
        }
    }

    private void RefreshGrid()
    {
        uxGrid.DataBind();

        RefreshDeleteButton();

        if (IsContainingOnlyEmptyRow() &&
            !uxGrid.ShowFooter)
            uxGrid.ShowHeader = false;
        else
            uxGrid.ShowHeader = true;

        if (uxGrid.ShowFooter)
        {
            Control keyNameText = uxGrid.FooterRow.FindControl( "uxKeyNameText" );
            Control textDataText = uxGrid.FooterRow.FindControl( "uxTextDataText" );
            Control addButton = uxGrid.FooterRow.FindControl( "uxAddButton" );

            WebUtilities.TieButton( this.Page, keyNameText, addButton );
            WebUtilities.TieButton( this.Page, textDataText, addButton );
        }
    }

    private void SetFooterRowFocus()
    {
        Control textBox = uxGrid.FooterRow.FindControl( "uxKeyNameText" );
        AjaxUtilities.GetScriptManager( this ).SetFocus( textBox );
    }


    protected void uxLanguageTextSource_Selected( object sender, ObjectDataSourceStatusEventArgs e )
    {
        // Add an empty row if no data returned for this data source
        DataTable table = (DataTable) e.ReturnValue;
        if (table.Rows.Count == 0)
        {
            DataRow row = table.NewRow();
            row["PageID"] = -1;
            row["CultureID"] = -1;
            row["KeyName"] = "";
            row["TextData"] = DBNull.Value;

            table.Rows.Add( row );
        }
    }

    protected void uxGrid_DataBound( object sender, EventArgs e )
    {
        // Do not display empty row
        if (IsContainingOnlyEmptyRow())
        {
            uxGrid.Rows[0].Visible = false;
        }
    }

    protected void uxCultureDrop_PreRender( object sender, EventArgs e )
    {
        DropDownList cultureDrop = (DropDownList) sender;
        cultureDrop.SelectedValue = uxGrid.DataKeys[uxGrid.EditIndex]["CultureID"].ToString();
    }

    protected void PopulateControls()
    {
        if (IsAdminModifiable())
        {
            if (uxGrid.Rows.Count > 0)
                DeleteVisible( true );
            else
                DeleteVisible( false );
        }
        else
        {
            uxAddButton.Visible = false;
            DeleteVisible( false );
        }

        uxPathLabel.Text = PageAccess.GetPathByID( CurrentPageID );

        RefreshGrid();
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

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            uxLanguageTextSource.SelectParameters.Add( new Parameter( "pageID", TypeCode.String, CurrentPageID ) );
            uxLanguageTextSource.SelectParameters.Add( new Parameter( "sortBy", TypeCode.String, "KeyName, LanguageName" ) );
            PopulateControls();
        }
    }

    protected void uxEditLinkButton_PreRender( object sender, EventArgs e )
    {
        if (!IsAdminModifiable())
        {
            LinkButton linkButton = (LinkButton) sender;
            linkButton.Visible = false;
        }
    }

    protected void uxGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        if (e.CommandName == "Add")
        {
            try
            {
                string cultureID = ((DropDownList) uxGrid.FooterRow.FindControl( "uxCultureDrop" ))
                    .SelectedValue;
                string keyName = ((TextBox) uxGrid.FooterRow.FindControl( "uxKeyNameText" )).Text.Trim();
                string textData = ((TextBox) uxGrid.FooterRow.FindControl( "uxTextDataText" )).Text.Trim();

                uxLanguageTextSource.InsertParameters.Add( "pageID", CurrentPageID );
                uxLanguageTextSource.InsertParameters.Add( "cultureID", cultureID );
                uxLanguageTextSource.InsertParameters.Add( "keyName", keyName );
                uxLanguageTextSource.InsertParameters.Add( "textData", textData );

                uxLanguageTextSource.Insert();

                ((TextBox) uxGrid.FooterRow.FindControl( "uxKeyNameText" )).Text = "";
                ((TextBox) uxGrid.FooterRow.FindControl( "uxTextDataText" )).Text = "";

                uxMessage.DisplayMessage( Resources.LanguagePageMessages.KeywordAddSuccess );
            }
            catch (Exception ex)
            {
                string message;
                if (ex.InnerException is DuplicatedPrimaryKeyException)
                    message = Resources.LanguagePageMessages.KeywordAddErrorDuplicated;
                else
                    message = Resources.LanguagePageMessages.KeywordAddError;
                throw new ApplicationException( message );
            }

            RefreshGrid();
            AdminUtilities.ClearLanguageCache();

            uxStatusHidden.Value = "Added";
        }
    }

    protected void uxGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            string newCultureID = ((DropDownList) uxGrid.Rows[e.RowIndex].FindControl( "uxCultureDrop" ))
                .SelectedValue;
            string newKeyName = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxKeyNameText" )).Text.Trim();
            string newTextData = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxTextDataText" )).Text.Trim();

            uxLanguageTextSource.UpdateParameters.Add( "pageID", e.Keys["PageID"].ToString() );
            uxLanguageTextSource.UpdateParameters.Add( "cultureID", e.Keys["CultureID"].ToString() );
            uxLanguageTextSource.UpdateParameters.Add( "keyName", e.Keys["KeyName"].ToString() );
            uxLanguageTextSource.UpdateParameters.Add( "newCultureID", newCultureID );
            uxLanguageTextSource.UpdateParameters.Add( "newKeyName", newKeyName );
            uxLanguageTextSource.UpdateParameters.Add( "newTextData", newTextData );

            uxLanguageTextSource.Update();

            // End editing
            uxGrid.EditIndex = -1;

            uxMessage.DisplayMessage( Resources.LanguagePageMessages.KeywordUpdateSuccess );
            uxStatusHidden.Value = "Updated";
        }
        catch (Exception ex)
        {
            string message;
            if (ex.InnerException is DuplicatedPrimaryKeyException)
                message = Resources.LanguagePageMessages.KeywordUpdateErrorDuplicated;
            else
                message = Resources.LanguagePageMessages.KeywordUpdateError;
            throw new ApplicationException( message );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
        }

        AdminUtilities.ClearLanguageCache();
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
                string cultureID = uxGrid.DataKeys[row.RowIndex]["CultureID"].ToString();
                string keyName = uxGrid.DataKeys[row.RowIndex]["KeyName"].ToString();

                LanguageTextAccess.Delete( CurrentPageID, cultureID, keyName );
                deleted = true;
            }
        }
        uxGrid.EditIndex = -1;

        if (deleted)
            uxMessage.DisplayMessage( Resources.LanguagePageMessages.KeywordDeleteSuccess );

        RefreshGrid();
        AdminUtilities.ClearLanguageCache();
        uxStatusHidden.Value = "Deleted";
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        uxGrid.EditIndex = -1;
        uxGrid.ShowFooter = true;
        RefreshGrid();

        uxAddButton.Visible = false;

        SetFooterRowFocus();

        uxStatusHidden.Value = "FooterShown";
    }
}
