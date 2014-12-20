using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using System.Collections.Generic;
using Vevo.Domain.Users;
using Vevo.Domain.Contents;
using Vevo.Base.Domain;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.Users;
using Vevo.Deluxe.Domain.Contents;

public partial class Admin_MainControls_CustomerSubscriptionList : AdminAdvancedBaseListControl
{
    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContextDeluxe.CustomerSubscriptionRepository.GetCustomerSubscriptionTableSchemas();
        uxSearchFilter.SetUpSchema( list, "CustomerID" );
    }

    private void HilightExpiredRow()
    {
        foreach (GridViewRow row in uxGrid.Rows)
        {
            CheckBox uxCheck = (CheckBox) row.FindControl( "uxIsExpiredCheck" );
            if (uxCheck.Checked)
            {
                foreach (TableCell cell in row.Cells)
                {
                    cell.Style.Add( "color", "#ff0000" );
                }
            }
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
            uxPagingControl.Visible = true;
        }
        else
        {
            uxPagingControl.Visible = false;
        }
    }

    private void DeleteItem( string id )
    {
        Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );
        IList<CustomerSubscription> subscriptionList = DataAccessContextDeluxe.CustomerSubscriptionRepository.GetCustomerSubscriptionsByCustomerID( CustomerID );

        int deleteIndex = -1;

        for (int i = 0; i < subscriptionList.Count; i++)
        {
            if (subscriptionList[i].SubscriptionLevelID == id)
            {
                deleteIndex = i;
            }
        }

        subscriptionList.RemoveAt( deleteIndex );

        DataAccessContextDeluxe.CustomerSubscriptionRepository.Save( customer.CustomerID, subscriptionList );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (String.IsNullOrEmpty( CustomerID ))
            MainContext.RedirectMainControl( "CustomerList.ascx" );

        RegisterGridView( uxGrid, "EndDate" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );

        if (!MainContext.IsPostBack)
        {
            SetUpSearchFilter();
            uxPagingControl.ItemsPerPages = AdminConfig.CustomerItemsPerPage;
        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected override void RefreshGrid()
    {
        int totalItems;

        uxGrid.DataSource = DataAccessContextDeluxe.CustomerSubscriptionRepository.SearchCustomerSubscription(
            CustomerID,
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );
        uxGrid.DataBind();

        HilightExpiredRow();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "CustomerSubscriptionAdd.ascx", String.Format( "CustomerID={0}", CustomerID ) );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            ArrayList deleteIDs = new ArrayList();
            foreach (GridViewRow row in uxGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string id = uxGrid.DataKeys[row.RowIndex]["SubscriptionLevelID"].ToString().Trim();
                    deleteIDs.Add( id );
                }
            }

            Customer customer = DataAccessContext.CustomerRepository.GetOne( CustomerID );


            foreach (string id in deleteIDs)
            {
                DeleteItem( id );
                deleted = true;
            }

            uxGrid.EditIndex = -1;

            if (deleted)
            {
                uxMessage.DisplayMessage( Resources.CustomerMessages.DeleteSuccess );
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
    public string GetSubscriptionLevel( object subscriptionID )
    {
        if (subscriptionID.ToString() != "0")
        {
            SubscriptionLevel level = DataAccessContextDeluxe.SubscriptionLevelRepository.GetOne( subscriptionID.ToString() );
            return level.Level.ToString();
        }
        else
        {
            return "0";
        }

    }

    public string GetDisplayDate( object date )
    {
        return String.Format( "{0:dd} {0:MMM} {0:yyyy}", (DateTime) date );
    }

    public Customer CurrentCustomer
    {
        get
        {
            return DataAccessContext.CustomerRepository.GetOne( CustomerID );
        }
    }
    public string CustomerID
    {
        get
        {
            if (string.IsNullOrEmpty( MainContext.QueryString["CustomerID"] ))
                return "0";
            else
                return MainContext.QueryString["CustomerID"];
        }
    }

    public bool IsExpired( object expireDate )
    {
        if ((DateTime) expireDate < DateTime.Now)
            return true;
        else
            return false;
    }
}
