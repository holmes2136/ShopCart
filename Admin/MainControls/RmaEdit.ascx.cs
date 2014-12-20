using System;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Returns;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;
using Vevo.WebUI;
using Vevo.WebAppLib;

public partial class AdminAdvanced_MainControls_RmaEdit : AdminAdvancedBaseUserControl
{
    #region Private

    private string CurrentID
    {
        get
        {
            return MainContext.QueryString[ "RmaID" ];
        }
    }

    private string GetStoreName( string storeID )
    {
        Store store = DataAccessContext.StoreRepository.GetOne( storeID );
        return store.StoreName;
    }

    private string GetCustomerID( string username )
    {
        return DataAccessContext.CustomerRepository.GetIDFromUserName( username );
    }

    private void PopulateReturnAction()
    {
        uxActionDrop.DataSource = DataAccessContext.RmaActionRepository.GetAll( BoolFilter.ShowTrue );
        uxActionDrop.DataTextField = "Name";
        uxActionDrop.DataValueField = "RmaActionID";
        uxActionDrop.DataBind();
    }

    private void PopulateControl()
    {
        Rma rma = DataAccessContext.RmaRepository.GetOne( CurrentID );

        uxRmaIDLabel.Text = rma.RmaID;
        uxStoreText.Text = GetStoreName( rma.StoreID );
        uxUserNameText.Text = rma.UserName;
        uxOrderText.Text = rma.OrderID;
        uxRmaDateText.Text = rma.RequestDate.ToShortDateString();
        uxProductText.Text = rma.ProductName;
        uxQuantityText.Text = rma.Quantity.ToString();
        uxReasonText.Text = rma.ReturnReason;
        uxActionDrop.SelectedValue = rma.RmaActionID;
        uxStatusDrop.SelectedValue = rma.RequestStatus;
        uxNoteText.Text = rma.RmaNote;
        uxUpdateButton.CommandArgument = rma.CustomerID;
    }

    private void UpdateRma()
    {
        Rma rma = DataAccessContext.RmaRepository.GetOne( CurrentID );

        rma.Quantity = ConvertUtilities.ToInt32( uxQuantityText.Text.Trim() );
        rma.ReturnReason = uxReasonText.Text;
        rma.RmaActionID = uxActionDrop.SelectedValue;
        rma.RmaNote = uxNoteText.Text;
        rma.RequestStatus = uxStatusDrop.SelectedValue;

        DataAccessContext.RmaRepository.Save( rma );

        SendMailToCustomer( rma );
    }

    private void SendMailToCustomer( Rma rma )
    {
        string subjectText = String.Empty;
        string bodyText = String.Empty;
        bool isSend = false;

        if ( rma.RequestStatus.Equals( Rma.RmaStatus.Returned.ToString() ) )
        {
            EmailTemplateTextVariable.ReplaceRMAApprovalText( rma, out subjectText, out bodyText );
            isSend = true;
        }
        else if ( rma.RequestStatus.Equals( Rma.RmaStatus.Rejected.ToString() ) )
        {
            EmailTemplateTextVariable.ReplaceRMARejectedText( rma, out subjectText, out bodyText );
            isSend = true;
        }

        if ( isSend )
        {
            Store store = DataAccessContext.StoreRepository.GetOne( rma.StoreID ); 
            string companyEmail = DataAccessContext.Configurations.GetValue( "CompanyEmail", store ); 
            
            WebUtilities.SendHtmlMail(
                companyEmail,
                rma.GetCustomer.Email,
                subjectText,
                bodyText,
                store );
        }
    }

    #endregion

    #region Protected

    protected void uxUpdateButton_Command( object sender, CommandEventArgs e )
    {
        try
        {
            UpdateRma();
            uxMessage.DisplayMessage( Resources.RmaMessages.UpdateSuccess );
        }
        catch ( Exception ex )
        {
            uxMessage.DisplayException( ex );
        }
    }

    #endregion

    #region Public

    protected void Page_Load( object sender, EventArgs e )
    {
        PopulateReturnAction();
        PopulateControl();
    }

    #endregion
}
