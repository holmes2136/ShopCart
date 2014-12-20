using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.Deluxe.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Users;
using Vevo.Deluxe.Domain.GiftRegistry;

public partial class GiftRegistryItemList : Vevo.Deluxe.WebUI.Base.BaseDeluxeLanguagePage
{
    private string GiftRegistryID
    {
        get
        {
            return Request.QueryString["GiftRegistryID"];
        }
    }

    private bool IsExitsInCurrentStore( string productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne(
            StoreContext.Culture, productID, DataAccessContext.StoreRetriever.GetCurrentStoreID() );

        string rootID = DataAccessContext.Configurations.GetValue(
                "RootCategory",
                DataAccessContext.StoreRepository.GetOne( DataAccessContext.StoreRetriever.GetCurrentStoreID() ) );

        return product.IsAvailable( rootID );
    }

    private void PopulateControls()
    {
        GiftRegistry giftRegistry = DataAccessContextDeluxe.GiftRegistryRepository.GetOne( GiftRegistryID );
        uxEventNameLable.Text = giftRegistry.EventName;
        uxEventDateLabel.Text = giftRegistry.EventDate.ToShortDateString();

        IList<GiftRegistryItem> itemList = DataAccessContextDeluxe.GiftRegistryItemRepository.GetAllByGiftRegistryID( GiftRegistryID );

        for (int i = 0; i < itemList.Count; i++)
        {
            if (!IsExitsInCurrentStore( itemList[i].ProductID ))
                itemList.RemoveAt( i );
        }

        uxGrid.DataSource = itemList;
        uxGrid.DataBind();

        if (uxGrid.Rows.Count == 0)
        {
            uxUpdateQuantityButton.Visible = false;
        }
    }

    private void UpdateQuantity()
    {
        ArrayList productNameList = new ArrayList();
        int rowIndex;
        for (rowIndex = 0; rowIndex < uxGrid.Rows.Count; rowIndex++)
        {
            GridViewRow row = uxGrid.Rows[rowIndex];
            if (row.RowType == DataControlRowType.DataRow)
            {
                string wantQuantityText = ((TextBox) row.FindControl( "uxWantQuantityText" )).Text;
                string hasQuantityText = ((Label) row.FindControl( "uxHasQuantityLabel" )).Text.Trim();
                int wantQuantity, hasQuantity;
                if (int.TryParse( wantQuantityText, out wantQuantity ) && int.TryParse( hasQuantityText, out hasQuantity ))
                {
                    GiftRegistryItem item = DataAccessContextDeluxe.GiftRegistryItemRepository.GetOne( uxGrid.DataKeys[rowIndex].Value.ToString() );

                    if (wantQuantity >= hasQuantity)
                    {
                        item.WantQuantity = wantQuantity;
                        DataAccessContextDeluxe.GiftRegistryItemRepository.Save( item );
                    }
                    else
                    {
                        productNameList.Add( item.ProductName );
                    }
                }
                else
                {
                    uxMessage.DisplayError( "Quantity is invalid." );
                    return;
                }
            }
        }

        if (productNameList.Count > 0)
        {
            System.Text.StringBuilder errorMessage = new System.Text.StringBuilder();
            errorMessage.Append( "Cannot update quantity. Want Quantity cannot be less than Has Quantity.<br/>" );
            errorMessage.Append( "Please correct the following product quantity:" );
            errorMessage.Append( "<ul>" );
            for (int i = 0; i < productNameList.Count; i++)
            {
                errorMessage.Append( "<li>" );
                errorMessage.Append( productNameList[i].ToString() );
                errorMessage.Append( "</li>" );
            }
            errorMessage.Append( "</ul>" );

            uxMessage.DisplayError( errorMessage.ToString() );
        }
        else
        {
            uxMessage.DisplayMessage( "Update quantity successfully." );
        }
    }

    private bool IsValidUserName( string userName )
    {
        return Page.User.Identity.IsAuthenticated &&
            String.Compare( userName, Page.User.Identity.Name, true ) == 0;
    }

    private void RegisterJavaScript()
    {
        string script = "var win=null;" +
            "function NewWindow(mypage,myname,w,h,pos,infocus){" +
            "if(pos==\"random\"){myleft=(screen.width)?Math.floor(Math.random()*" +
            "(screen.width-w)):100;mytop=(screen.height)?Math.floor(Math.random()*((screen.height-h)-75)):100;}" +
            "if(pos==\"center\"){myleft=(screen.width)?(screen.width-w)/2:100;mytop=(screen.height)?(screen.height-h)/2:100;}" +
            "else if((pos!='center' && pos!=\"random\") || pos==null){myleft=0;mytop=20}" +
            "settings=\"width=\" + w + \",height=\" + h + \",top=\" + mytop + \",left=\" + myleft + \",scrollbars=yes," +
            "location=yes,directories=no,status=no,menubar=no,toolbar=no,resizable=yes\";win=window.open(mypage,myname,settings);" +
            "win.focus();}";

        ScriptManager.RegisterStartupScript( this, this.GetType(), "PopupWindow", script, true );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterJavaScript();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        string userName = DataAccessContextDeluxe.GiftRegistryRepository.GetOne( GiftRegistryID ).UserName;
        if (!IsValidUserName( userName ))
        {
            uxErrorLiteral.Visible = true;
            uxGiftRegistryItemListPanel.Visible = false;
            uxUpdateQuantityButton.Visible = false;
        }
        else
            PopulateControls();
    }

    protected void uxGrid_RowDeleting( object sender, GridViewDeleteEventArgs e )
    {
        string giftRegistryItemID = uxGrid.DataKeys[e.RowIndex].Value.ToString();
        DataAccessContextDeluxe.GiftRegistryItemRepository.Delete( giftRegistryItemID );
        PopulateControls();
    }

    protected void uxUpdateQuantityButton_Click( object sender, EventArgs e )
    {
        UpdateQuantity();
        PopulateControls();
    }

    protected void uxAddNewItemButton_Click( object sender, EventArgs e )
    {
        Session["GiftRegistryID"] = GiftRegistryID;
        Response.Redirect( "~/Catalog.aspx" );
    }

    protected string GetUrl( string productID )
    {
        return "ProductPopUp.aspx?ProductID=" + productID;
    }

    protected void uxNameLink_PreRender( object sender, EventArgs e )
    {
        HyperLink link = (HyperLink) sender;
        string strRedirect = link.NavigateUrl;
        link.NavigateUrl = "javascript:NewWindow('" + strRedirect + "','acepopup','700','500','center','front');";
    }

    protected string GetPrice( string giftregistryItemID )
    {
        GiftRegistryItem giftRegistryItem = DataAccessContextDeluxe.GiftRegistryItemRepository.GetOne( giftregistryItemID );

        return StoreContext.Currency.FormatPrice(
            giftRegistryItem.GetUnitPrice( StoreContext.WholesaleStatus) );
    }

    protected bool IsVisble( string giftRegistryItemID )
    {
        return DataAccessContextDeluxe.GiftRegistryItemRepository.CanBeDeletedByGiftRegistryItemID( giftRegistryItemID );
    }

}
