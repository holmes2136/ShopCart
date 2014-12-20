using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.CustomerReward;
using Vevo.Shared.DataAccess;
using Vevo.Shared.Utilities;

public partial class Admin_Components_CustomerRewardPointList : AdminAdvancedBaseListControl
{
    #region Private

    private bool _emptyRow = false;

    private bool IsContainingOnlyEmptyRow
    {
        get { return _emptyRow; }
        set { _emptyRow = value; }
    }

    private string CurrentID
    {
        get
        {
            return MainContext.QueryString["CustomerID"];
        }
    }

    private int TotalPoint()
    {
        return ConvertUtilities.ToInt32( 
            DataAccessContextDeluxe.CustomerRewardPointRepository.SumCustomerIDAndStoreID( 
                CurrentID, uxStoreList.SelectedValue ) );
    }

    private void CreateDummyRow( IList<CustomerRewardPoint> list )
    {
        CustomerRewardPoint customerRewardPoint = new CustomerRewardPoint();
        customerRewardPoint.RewardPointID = "-1";

        list.Add( customerRewardPoint );
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

    private void RefreshDeleteButton()
    {
        if (IsContainingOnlyEmptyRow)
        {
            uxDeleteButton.Visible = false;
        }
        else
        {
            uxDeleteButton.Visible = true;
        }
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.CustomerItemsPerPage;
        }

        RegisterGridView( uxCustomerGrid, "RewardPointID" );
        RegisterPagingControl( uxPagingControl );

        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }

    private void ClearPointInputFiels()
    {
        uxPointText.Text = "";
        uxReferenceText.Text = "";
    }

    private void DeleteItem( string id )
    {
        IList<CustomerRewardPoint> rewardPointList = 
            DataAccessContextDeluxe.CustomerRewardPointRepository.GetByCustomerIDAndStoreID( 
                CurrentID, uxStoreList.SelectedValue );

        for (int i = 0; i < rewardPointList.Count; i++)
        {
            if (rewardPointList[i].RewardPointID == id)
            {
                DataAccessContextDeluxe.CustomerRewardPointRepository.Delete( rewardPointList[i].RewardPointID );
            }
        }

    }

    private bool IsPointReferenceLabelVisible( string pointID )
    {
        CustomerRewardPoint rewardPoint = DataAccessContextDeluxe.CustomerRewardPointRepository.GetOne( pointID );
        if (ConvertUtilities.ToInt32( rewardPoint.OrderID ) > 0)
        {
            return false;
        }
        else return true;
    }

    private void PopulateControl()
    {
        RefreshGrid();

        if (uxCustomerGrid.Rows.Count > 0)
        {
            DeleteVisible( true );
            uxPagingControl.Visible = true;
        }
        else
        {
            DeleteVisible( false );
            uxPagingControl.Visible = false;
        }
    }

    #endregion


    #region Protected

    protected void SetFooter( object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            TableCellCollection cells = e.Row.Cells;
            cells.RemoveAt( 0 );
            cells[0].ColumnSpan = 2;

            cells[0].Text = "Total Points";
            cells[0].HorizontalAlign = HorizontalAlign.Center;
            cells[0].Font.Bold = true;

            cells[1].Text = ConvertUtilities.ToString( TotalPoint() );
            cells[1].CssClass = "OrderListTotalPrice";
        }
    }

    protected override void RefreshGrid()
    {
        int totalItems;
        IList<CustomerRewardPoint> list = 
            DataAccessContextDeluxe
            .CustomerRewardPointRepository
            .SearchCustomerRewardPointByCustomerIDAndStoreID(
                CurrentID,
                uxStoreList.SelectedValue,
                "ReferenceDate",
                uxPagingControl.StartIndex,
                uxPagingControl.EndIndex,
                out totalItems );

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );


        if (list == null || list.Count == 0)
        {
            IsContainingOnlyEmptyRow = true;
            list = new List<CustomerRewardPoint>();
            CreateDummyRow( list );
        }
        else
        {
            IsContainingOnlyEmptyRow = false;
        }

        uxCustomerGrid.DataSource = list;
        uxCustomerGrid.DataBind();

        RefreshDeleteButton();
    }

    protected void uxCustomerGrid_DataBound( object sender, EventArgs e )
    {
        if (IsContainingOnlyEmptyRow)
        {
            uxCustomerGrid.Rows[0].Visible = false;
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!KeyUtilities.IsDeluxeLicense(
            DataAccessHelper.DomainRegistrationkey, DataAccessHelper.DomainName ))
        {
            MainContext.RedirectMainControl( "Default.ascx", String.Empty );
        }

        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateControl();
        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        uxAddModalPopup.Show();
    }

    protected void uxAddPointButton_Click( object sender, EventArgs e )
    {
        CustomerRewardPoint customerRewardPoint = new CustomerRewardPoint();
        customerRewardPoint.Point = ConvertUtilities.ToInt32( uxPointText.Text );
        customerRewardPoint.Reference = uxReferenceText.Text;
        customerRewardPoint.ReferenceDate = DateTime.Now;
        customerRewardPoint.StoreID = uxStoreList.SelectedValue;
        customerRewardPoint.CustomerID = CurrentID;
        DataAccessContextDeluxe.CustomerRewardPointRepository.Save( customerRewardPoint );

        RefreshGrid();

        ClearPointInputFiels();

        uxMessage.DisplayMessage( Resources.CustomerRewardPointMessages.AddSuccess );
    }

    protected void uxStoreList_RefreshHandler( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;

            ArrayList deleteIDs = new ArrayList();
            foreach (GridViewRow row in uxCustomerGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string id = uxCustomerGrid.DataKeys[row.RowIndex]["RewardPointID"].ToString().Trim();
                    deleteIDs.Add( id );
                }
            }

            IList<CustomerRewardPoint> rewardPointList = DataAccessContextDeluxe.CustomerRewardPointRepository.GetByCustomerIDAndStoreID( CurrentID, uxStoreList.SelectedValue );
            if (deleteIDs.Count > rewardPointList.Count)
            {
                uxMessage.DisplayError( Resources.CustomerRewardPointMessages.DeleteError );
            }
            else
            {
                foreach (string id in deleteIDs)
                {
                    DeleteItem( id );
                    deleted = true;
                }

                uxCustomerGrid.EditIndex = -1;

                if (deleted)
                {
                    uxMessage.DisplayMessage( Resources.CustomerRewardPointMessages.DeleteSuccess );
                }
            }

        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        RefreshGrid();

        if (uxCustomerGrid.Rows.Count == 0 && uxPagingControl.CurrentPage >= uxPagingControl.NumberOfPages)
        {
            uxPagingControl.CurrentPage = uxPagingControl.NumberOfPages;
            RefreshGrid();
        }
    }

    protected bool IsReferenceLabelVisible( object rewardPointID )
    {
        string pointID = ConvertUtilities.ToString( rewardPointID );
        return IsPointReferenceLabelVisible( pointID );

    }

    protected bool IsReferenceLinkVisible( object rewardPointID )
    {
        string pointID = ConvertUtilities.ToString( rewardPointID );
        return !IsPointReferenceLabelVisible( pointID );
    }

    #endregion

}
