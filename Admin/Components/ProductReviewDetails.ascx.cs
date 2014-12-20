using System;
using System.Web.UI;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Domain.Users;
using Vevo.WebUI;

public partial class AdminAdvanced_Components_ProductReviewDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public bool SetVisibleLanguageControl
    {
        set { uxLanguageControl.Visible = value; }
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

    private string CurrentID
    {
        get
        {
            return MainContext.QueryString["ReviewID"];
        }
    }

    private string CurrentProductID
    {
        get
        {
            if (MainContext.QueryString["ProductID"] != null)
            {
                return MainContext.QueryString["ProductID"];
            }
            else
            {
                if (ViewState["CurrentProductID"] == null)
                    ViewState["CurrentProductID"] = CustomerReviewAccess.GetProductIDByReviewID( CurrentID );

                return (string) ViewState["CurrentProductID"];
            }
        }
    }

    private void SetProductName()
    {
        Product product = DataAccessContext.ProductRepository.GetOne( uxLanguageControl.CurrentCulture, CurrentProductID, new StoreRetriever().GetCurrentStoreID() );
        uxProductNameLabel.Text = product.Name;
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected void uxLanguageControl_BubbleEvent( object sender, EventArgs e )
    {
        SetProductName();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            uxReviewCheck.Checked = true;

            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                uxDateText.Visible = false;
            }
            else
            {
                uxDateText.Visible = true;
            }

            SetProductName();
        }

        uxLanguageControl.BubbleEvent += new EventHandler( uxLanguageControl_BubbleEvent );
    }

    private void ClearTextFeilds()
    {
        uxLongDescriptionText.Text = "";
        uxCustomerID.Text = string.Empty;
        uxReviewRating.Text = string.Empty;
        uxSubject.Text = string.Empty;
        uxReviewCheck.Checked = true;
        uxCalendar.SelectedDate = DateTime.Now;
    }

    private void ClearTextFieldsCrossCulture()
    {
        uxLongDescriptionText.Text = "";
        uxSubject.Text = string.Empty;
    }

    private void PopulateControls()
    {

        if (CurrentID != null && int.Parse( CurrentID ) >= 0)
        {
            CustomerReviewDetails details = CustomerReviewAccess.GetDetails( CurrentID, uxLanguageControl.CurrentCultureID );

            if (details != null)
            {
                Customer customer = DataAccessContext.CustomerRepository.GetOne( details.CustomerID );

                uxCustomerID.Text = customer.UserName;
                uxReviewRating.Text =

                Convert.ToString( Convert.ToDouble( details.ReviewRating ) *
                Convert.ToDouble( DataAccessContext.Configurations.GetValue( "StarRatingAmount" ) ) );

                uxSubject.Text = details.Subject;

                uxLongDescriptionText.Text = details.Body;
                uxReviewCheck.Checked = details.IsEnabled;
                uxCalendar.SelectedDate = details.ReviewDate;
            }
            else
            {
                ClearTextFieldsCrossCulture();
            }

        }
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (IsEditMode())
        {
            if (!MainContext.IsPostBack)
            {
                PopulateControls();
            }
            uxAddButton.Visible = false;


            if (IsAdminModifiable())
            {
                uxEditButton.Visible = true;
            }
            else
            {
                uxEditButton.Visible = false;
            }

        }
        else
        {
            if (IsAdminModifiable())
            {
                uxAddButton.Visible = true;
                uxEditButton.Visible = false;
                uxCalendar.SelectedDate = DateTime.Now;
            }
            else
            {
                MainContext.RedirectMainControl( "ProductReviewList.ascx", "" );
            }
        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                double reviewRating =
                    Convert.ToDouble( uxReviewRating.Text.Trim() ) /
                    Convert.ToDouble( DataAccessContext.Configurations.GetValue( "StarRatingAmount" ) );

                string customerID = DataAccessContext.CustomerRepository.GetIDFromUserName( uxCustomerID.Text.Trim() );
                if (customerID == "")
                    customerID = "0";

                string ReviewID = CustomerReviewAccess.Create(
                    CurrentProductID,
                    uxLanguageControl.CurrentCultureID,
                    customerID,
                    reviewRating,
                    bool.Parse( uxReviewCheck.Checked.ToString() ),
                    uxSubject.Text.Trim(),
                    uxLongDescriptionText.Text
                    );

                uxMessage.DisplayMessage( Resources.ProductReviewMessage.AddSuccess );

                ClearTextFeilds();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxEditButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                DateTime date;
                double reviewRating =
                    Convert.ToDouble( uxReviewRating.Text.Trim() ) /
                    Convert.ToDouble( DataAccessContext.Configurations.GetValue( "StarRatingAmount" ) );

                string customerID = DataAccessContext.CustomerRepository.GetIDFromUserName( uxCustomerID.Text.Trim() );
                if (customerID == "")
                    customerID = "0";

                if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
                {
                    date = Convert.ToDateTime( uxCalendar.SelectedDate );
                }
                else
                {
                    date = Convert.ToDateTime( uxDateText.Text );
                }

                CustomerReviewAccess.Update(
                    CurrentID,
                    CustomerReviewAccess.GetProductIDByReviewID( CurrentID ),
                    uxLanguageControl.CurrentCultureID,
                    customerID,
                    reviewRating,
                    date,
                    uxReviewCheck.Checked,
                    uxSubject.Text.Trim(),
                    uxLongDescriptionText.Text
                    );


                uxMessage.DisplayMessage( Resources.ProductReviewMessage.UpdateSuccess );

                PopulateControls();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }
}
