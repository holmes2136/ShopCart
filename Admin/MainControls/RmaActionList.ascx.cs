using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Domain.Returns;
using Vevo.WebAppLib;
using Vevo.WebUI.Ajax;
using Vevo.Base.Domain;

public partial class AdminAdvanced_MainControls_RmaActionList : AdminAdvancedBaseListControl
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
        IList<TableSchemaItem> list = DataAccessContext.RmaActionRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list, "RmaActionID", "IsEnabled" );
    }

    private void SetFooterRowFocus()
    {
        Control textBox = uxGrid.FooterRow.FindControl( "uxNameText" );
        AjaxUtilities.GetScriptManager( this ).SetFocus( textBox );
    }

    private void CreateDummyRow( IList<RmaAction> list )
    {
        RmaAction action = new RmaAction();
        action.RmaActionID = "-1";

        list.Add( action );
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

        if (uxGrid.Rows.Count > 0)
        {
            DeleteVisible( true );
        }
        else
        {
            DeleteVisible( false );
        }

        if (!IsAdminModifiable())
        {
            uxAddButton.Visible = false;
            DeleteVisible( false );
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

        foreach (GridViewRow row in uxGrid.Rows)
        {
            CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
            if (deleteCheck.Checked)
            {
                string id = uxGrid.DataKeys[row.RowIndex]["RmaActionID"].ToString().Trim();
                items.Add( id );
            }
        }

        string[] result = new string[items.Count];
        items.CopyTo( result );
        return result;
    }

    private void DeleteItems( string[] checkedIDs )
    {
        try
        {
            bool deleted = false;
            foreach (string id in checkedIDs)
            {
                DataAccessContext.RmaActionRepository.Delete( id );
                deleted = true;
            }
            uxGrid.EditIndex = -1;

            if (deleted)
                uxMessage.DisplayMessage( Resources.RmaActionMessages.DeleteSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
            SetUpSearchFilter();

        RegisterGridView( uxGrid, "RmaActionID" );
        RegisterSearchFilter( uxSearchFilter );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
    }

    #endregion


    #region Protected

    protected override void RefreshGrid()
    {
        IList<RmaAction> list = DataAccessContext.RmaActionRepository.SearchRmaAction(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj );

        if (list == null || list.Count == 0)
        {
            IsContainingOnlyEmptyRow = true;
            list = new List<RmaAction>();
            CreateDummyRow( list );
        }
        else
        {
            IsContainingOnlyEmptyRow = false;
        }

        uxGrid.DataSource = list;
        uxGrid.DataBind();

        RefreshDeleteButton();

        if (uxGrid.ShowFooter)
        {
            Control nameText = uxGrid.FooterRow.FindControl( "uxNameText" );
            Control addButton = uxGrid.FooterRow.FindControl( "uxAddlinkButton" );

            WebUtilities.TieButton( this.Page, nameText, addButton );
        }
    }

    protected void uxGrid_DataBound( object sender, EventArgs e )
    {
        if (IsContainingOnlyEmptyRow)
        {
            uxGrid.Rows[0].Visible = false;
        }
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        string[] checkedIDs = GetCheckedIDs();
        DeleteItems( checkedIDs );
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

    protected void uxGrid_RowCommand( object sender, GridViewCommandEventArgs e )
    {
        if (e.CommandName == "Add")
        {
            try
            {
                string name = ((TextBox) uxGrid.FooterRow.FindControl( "uxNameText" )).Text;
                bool isCheck = ((CheckBox) uxGrid.FooterRow.FindControl( "uxEnabledCheck" )).Checked;

                RmaAction action = new RmaAction();
                action.Name = name;
                action.IsEnabled = isCheck;
                DataAccessContext.RmaActionRepository.Save( action );

                ((TextBox) uxGrid.FooterRow.FindControl( "uxNameText" )).Text = "";
                ((CheckBox) uxGrid.FooterRow.FindControl( "uxEnabledCheck" )).Checked = false;

                uxMessage.DisplayMessage( Resources.RmaActionMessages.AddSuccess );
            }
            catch (Exception)
            {
                string message = Resources.RmaActionMessages.AddError;
                throw new VevoException( message );
            }

            RefreshGrid();
        }

        if (e.CommandName == "Edit")
        {
            uxGrid.ShowFooter = false;
            uxAddButton.Visible = true;
        }
    }

    protected void uxGrid_RowUpdating( object sender, GridViewUpdateEventArgs e )
    {
        try
        {
            string rmaActionID = ((HiddenField) uxGrid.Rows[e.RowIndex].FindControl( "uxRmaActionIDHidden" )).Value;
            string name = ((TextBox) uxGrid.Rows[e.RowIndex].FindControl( "uxNameText" )).Text;
            bool isCheck = ((CheckBox) uxGrid.Rows[e.RowIndex].FindControl( "uxEnabledCheck" )).Checked;

            if (!String.IsNullOrEmpty( rmaActionID ))
            {
                RmaAction action = new RmaAction();
                action.RmaActionID = rmaActionID;
                action.Name = name;
                action.IsEnabled = isCheck;
                DataAccessContext.RmaActionRepository.Save( action );
            }

            //End editing
            uxGrid.EditIndex = -1;
            RefreshGrid();

            uxMessage.DisplayMessage( Resources.RmaActionMessages.UpdateSuccess );
        }
        catch (Exception)
        {
            string message = Resources.RmaActionMessages.UpdateError;
            throw new ApplicationException( message );
        }
        finally
        {
            // Avoid calling Update() automatically by GridView
            e.Cancel = true;
        }
    }

    #endregion
}
