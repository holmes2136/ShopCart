using System;
using System.Data;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;

public partial class AdminAdvanced_MainControls_ProductReviewList : AdminAdvancedBaseListControl
{
    private const int ColumnReviewID = 1;
    //private string productID;

    public string ProductID
    {
        get { return MainContext.QueryString["ProductID"]; }
    }

    private void SetUpSearchFilter()
    {
        DataTable table = CustomerReviewAccess.GetTableSchema();
        uxSearchFilter.SetUpSchema( table.Columns, "CultureID", "Body", "Subject", "ProductID" );
    }

    private void SetProductName()
    {
        Product product = DataAccessContext.ProductRepository.GetOne( uxLanguageControl.CurrentCulture, ProductID, new StoreRetriever().GetCurrentStoreID() );

            uxProductNameLabel.Text = product.Name;
    }

    private string GetValue( string value )
    {
        if (uxSearchFilter.FieldName.ToLower() == "reviewrating")
            return ConvertUtilities.ToString( ConvertUtilities.ToDouble( value ) /
               ConvertUtilities.ToDouble( DataAccessContext.Configurations.GetValue( "StarRatingAmount" ) ) );
        else
            return value;
    }

    private DataTable ConvertDetail( DataTable dt )
    {
        DataTable dtConverted = dt.Clone();

        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {

                DataRow drConverted = dtConverted.NewRow();

                drConverted["ReviewID"] = dr["ReviewID"].ToString();
                drConverted["ProductID"] = dr["ProductID"].ToString();

                if (dr["UserName"].ToString() == "")
                    drConverted["UserName"] = "Anonymous";
                else
                    drConverted["UserName"] = dr["UserName"].ToString();

                drConverted["ReviewRating"] =
                   Convert.ToString( Convert.ToDouble( dr["ReviewRating"] ) *
                    Convert.ToDouble( DataAccessContext.Configurations.GetValue( "StarRatingAmount" ) ) );

                drConverted["Enabled"] = dr["Enabled"].ToString();
                drConverted["ReviewDate"] = dr["ReviewDate"].ToString();
                drConverted["Subject"] = dr["Subject"].ToString();
                drConverted["Body"] = dr["Body"].ToString();
                drConverted["CultureID"] = dr["CultureID"].ToString();
                dtConverted.Rows.Add( drConverted );
            }

        }

        return dtConverted;
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
            DeleteVisible( true );
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
        SetProductName();
        GetProductDetailLink();
        GetProductImageListLink();
    }

    private void GetProductDetailLink()
    {
        if (ProductID == "0")
        {
            uxProductDetailLink.PageName = "ProductAdd.ascx";
            uxProductDetailLink.CommandName = "Edit";
        }
        else
            uxProductDetailLink.PageName = "ProductEdit.ascx";

        uxProductDetailLink.PageQueryString = String.Format( "ProductID={0}", ProductID );
    }

    private void GetProductImageListLink()
    {
        if (ProductID == "0")
        {
            uxImageListLink.PageName = "ProductImageList.ascx";
            uxImageListLink.PageQueryString = String.Format( "ProductID={0}", ProductID);
        }
        else
        {
            uxImageListLink.PageName = "ProductEdit.ascx";
            uxImageListLink.PageQueryString = String.Format( "ProductID={0}&{1}", ProductID, "TabName=Image" );
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

    private void uxGrid_ResetPageHandler( object sender, EventArgs e )
    {
        uxPagingControl.CurrentPage = 1;
        RefreshGrid();
    }

    private void uxGrid_RefreshHandler( object sender, EventArgs e )
    {
        RefreshGrid();
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.CustomerReviewItemsPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxGrid, "ProductID" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
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
        MainContext.RedirectMainControl( "ProductReviewAdd.ascx", String.Format( "ProductID={0}", ProductID ) );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            bool deleted = false;
            foreach (GridViewRow row in uxGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string ID = row.Cells[ColumnReviewID].Text.Trim();

                    CustomerReviewAccess.Delete( ID );

                    deleted = true;
                }
            }

            if (deleted)
                uxMessage.DisplayMessage( Resources.ProductReviewMessage.DeleteSuccess );
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

    protected void uxAddButton_Click1( object sender, EventArgs e )
    {
        MainContext.RedirectMainControl( "ProductReviewAdd.ascx", "" );
    }

    protected override void RefreshGrid()
    {
        int totalItems;

        uxGrid.DataSource = ConvertDetail( CustomerReviewAccess.SearchReview(
            ProductID,
            uxLanguageControl.CurrentCultureID,
            //"ReviewID",
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchType,
            uxSearchFilter.FieldName,
            GetValue( uxSearchFilter.Value1 ),
            GetValue( uxSearchFilter.Value2 ),
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems ) );
        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGrid.DataBind();
    }

}
