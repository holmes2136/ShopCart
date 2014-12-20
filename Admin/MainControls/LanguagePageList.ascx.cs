using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.Shared.DataAccess;
using Vevo.WebAppLib;
using Vevo.WebUI.Ajax;

public partial class AdminAdvanced_MainControls_LanguagePageList : AdminAdvancedBaseUserControl
{
    private bool IsContainingOnlyEmptyRow()
    {
        if (uxGrid.Rows.Count == 1 &&
            (int) uxGrid.DataKeys[0]["PageID"] == -1)
            return true;
        else
            return false;
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
            //foreach (GridViewRow gr in uxGrid.Rows)
            //{
            //    gr.FindControl( "uxEditLinkButton" ).Visible = false;
            //}
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
            Control pageIDText = uxGrid.FooterRow.FindControl( "uxPageIDText" );
            Control pathText = uxGrid.FooterRow.FindControl( "uxPathText" );
            Control addButton = uxGrid.FooterRow.FindControl( "uxAddButton" );

            WebUtilities.TieButton( this.Page, pageIDText, addButton );
            WebUtilities.TieButton( this.Page, pathText, addButton );
        }
    }

    private void SetFooterRowFocus()
    {
        Control textBox = uxGrid.FooterRow.FindControl( "uxPageIDText" );
        AjaxUtilities.GetScriptManager( this ).SetFocus( textBox );
    }


    protected void uxPageSource_Selected( object sender, ObjectDataSourceStatusEventArgs e )
    {
        // Add an empty row if no data returned for this data source
        DataTable table = (DataTable) e.ReturnValue;
        if (table.Rows.Count == 0)
        {
            DataRow row = table.NewRow();
            row["PageID"] = -1;
            row["Path"] = DBNull.Value;

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

    protected void PopulateControls()
    {

        if (IsAdminModifiable())
        {
            RefreshDeleteButton();
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
                string pageID = ((TextBox) uxGrid.FooterRow.FindControl( "uxPageIDText" )).Text.Trim();
                string path = ((TextBox) uxGrid.FooterRow.FindControl( "uxPathText" )).Text.Trim();

                uxPageSource.InsertParameters.Add( "pageID", pageID );
                uxPageSource.InsertParameters.Add( "path", path );

                uxPageSource.Insert();

                ((TextBox) uxGrid.FooterRow.FindControl( "uxPageIDText" )).Text = "";
                ((TextBox) uxGrid.FooterRow.FindControl( "uxPathText" )).Text = "";

                uxMessage.DisplayMessage( Resources.LanguagePageMessages.AddSuccess );
            }
            catch (Exception ex)
            {
                string message;
                if (ex.InnerException is DuplicatedPrimaryKeyException)
                    message = Resources.LanguagePageMessages.AddErrorDuplicated;
                else
                    message = Resources.LanguagePageMessages.AddError;
                throw new VevoException( message );
            }
            finally
            {
            }

            RefreshGrid();
            AdminUtilities.ClearLanguageCache();
        }
    }

    protected void uxGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            string newPageID = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxPageIDText" )).Text.Trim();
            string newPath = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxPathText" )).Text.Trim();

            uxPageSource.UpdateParameters.Add( "pageID", e.Keys["PageID"].ToString() );
            uxPageSource.UpdateParameters.Add( "newPageID", newPageID );
            uxPageSource.UpdateParameters.Add( "newPath", newPath );

            uxPageSource.Update();

            // End editing
            uxGrid.EditIndex = -1;

            RefreshGrid();

            uxMessage.DisplayMessage( Resources.LanguagePageMessages.UpdateSuccess );
        }
        catch (Exception ex)
        {
            string message;
            if (ex.InnerException is DuplicatedPrimaryKeyException)
                message = Resources.LanguagePageMessages.UpdateErrorDuplicated;
            else
                message = Resources.LanguagePageMessages.UpdateError;
            throw new ApplicationException( message );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;

            AdminUtilities.ClearLanguageCache();
        }
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
                string pageID = uxGrid.DataKeys[row.RowIndex]["PageID"].ToString().Trim();
                PageAccess.Delete( pageID );
                deleted = true;
            }
        }
        uxGrid.EditIndex = -1;

        if (deleted)
            uxMessage.DisplayMessage( Resources.CustomerMessages.DeleteSuccess );

        RefreshGrid();

        AdminUtilities.ClearLanguageCache();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        uxGrid.EditIndex = -1;
        uxGrid.ShowFooter = true;
        RefreshGrid();

        uxAddButton.Visible = false;

        SetFooterRowFocus();

        AdminUtilities.ClearLanguageCache();
    }

    protected void uxCultureLinkButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "CultureList.ascx", "" );
    }

    protected void uxSearchLinkButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "LanguageTextSearch.ascx", "" );
    }
}
