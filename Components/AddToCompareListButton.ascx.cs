using System;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.WebUI.Ajax;
using Vevo.WebUI.International;

public partial class Components_AddToCompareListButton : BaseLanguageUserControl
{
    private void AddToCompareList( string productID )
    {
        if ( DataAccessContext.Configurations.GetIntValue( "CompareProductShow" ) <= StoreContext.ProductIDsCompareList.Count )
            Response.Redirect( "~/ComparisonList.aspx?ErrorID=1" );

        if ( !StoreContext.ProductIDsCompareList.Contains( productID ) )
        {
            StoreContext.ProductIDsCompareList.Add( productID );

            if ( UserUtilities.GetCurrentCustomerID() != "0" )
            {
                StoreContext.Customer.SetProductIDsCompare( StoreContext.ProductIDsCompareList );
                DataAccessContext.CustomerRepository.Save( StoreContext.Customer );
            }
        }

    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if ( !DataAccessContext.Configurations.GetBoolValue( "CompareListEnabled" ) )
        {
            uxAddCompareListImageButton.Visible = false;
        }
        else
        {
            AjaxUtilities.GetScriptManager( this ).RegisterAsyncPostBackControl( uxAddCompareListImageButton );

            if ( !String.IsNullOrEmpty( CssClass ) )
                uxAddCompareListImageButton.CssClass = CssClass;

            uxAddCompareListImageButton.Text = Text;
        }

    }


    public string CssClass
    {
        get
        {
            if ( ViewState[ "CssClass" ] == null )
                return "";
            return ViewState[ "CssClass" ].ToString();
        }
        set
        {
            ViewState[ "CssClass" ] = value;
        }
    }

    public string ProductID
    {
        get
        {
            if ( ViewState[ "ProductID" ] == null )
                return "";
            return ViewState[ "ProductID" ].ToString();
        }
        set
        {
            ViewState[ "ProductID" ] = value;
        }
    }
    public string Text
    {
        get
        {
            if ( ViewState[ "Text" ] == null )
                return "[$BtnCompareProduct]";
            return ViewState[ "Text" ].ToString();
        }
        set
        {
            ViewState[ "Text" ] = value;
        }
    }
    public void AddItemToCompareListCart( string productID )
    {
        AddToCompareList( productID );
    }

    protected void uxAddCompareListImageButton_Click( object sender, EventArgs e )
    {
        OnBubbleEvent( e );
    }

}
