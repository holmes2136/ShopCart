using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Xml;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Vevo.Domain;
using Vevo.Domain.Users;
using Vevo.WebUI;
using Vevo.Shared.Utilities;
using Vevo;
using Vevo.Base.Domain;

public partial class SaveBillingAddress : System.Web.UI.Page
{
    #region Private

    private string FirstName
    {
        get
        {
            return Request.QueryString["FirstName"].ToString();
        }
    }

    private string LastName
    {
        get
        {
            return Request.QueryString["LastName"].ToString();
        }
    }

    private string Company
    {
        get
        {
            return Request.QueryString["Company"].ToString();
        }
    }

    private string Address1
    {
        get
        {
            return Request.QueryString["Address1"].ToString();
        }
    }

    private string Address2
    {
        get
        {
            return Request.QueryString["Address2"].ToString();
        }
    }

    private string City
    {
        get
        {
            return Request.QueryString["City"].ToString();
        }
    }

    private string State
    {
        get
        {
            return Request.QueryString["State"].ToString();
        }
    }

    private string Zip
    {
        get
        {
            return Request.QueryString["Zip"].ToString();
        }
    }

    private string Country
    {
        get
        {
            return Request.QueryString["Country"].ToString();
        }
    }

    private string Phone
    {
        get
        {
            return Request.QueryString["Phone"].ToString();
        }
    }

    private string Fax
    {
        get
        {
            return Request.QueryString["Fax"].ToString();
        }
    }

    private string Token
    {
        get
        {
            return Request.QueryString["Token"].ToString();
        }
    }

    private string DecodeSpecialCharacters( string text )
    {
        text = text.Replace( "%3B", ";" );
        text = text.Replace( "%3F", "?" );
        text = text.Replace( "%2F", "/" );
        text = text.Replace( "%3A", ":" );
        text = text.Replace( "%23", "#" );
        text = text.Replace( "%26", "&" );
        text = text.Replace( "%3D", "=" );
        text = text.Replace( "%2B", "+" );
        text = text.Replace( "%24", "$" );
        text = text.Replace( "%2C", "," );
        text = text.Replace( "%20", " " );
        text = text.Replace( "%25", "%" );
        text = text.Replace( "%3C", "<" );
        text = text.Replace( "%3E", ">" );
        text = text.Replace( "%7E", "~" );
        return text;
    }

    private void SetBillingAddressToSession()
    {
        if (!String.IsNullOrEmpty( FirstName ))
        {
            Address billing = new Address(
                DecodeSpecialCharacters( FirstName ),
                DecodeSpecialCharacters( LastName ),
                DecodeSpecialCharacters( Company ),
                DecodeSpecialCharacters( Address1 ),
                DecodeSpecialCharacters( Address2 ),
                DecodeSpecialCharacters( City ),
                DecodeSpecialCharacters( State ),
                DecodeSpecialCharacters( Zip ),
                DecodeSpecialCharacters( Country ),
                DecodeSpecialCharacters( Phone ),
                DecodeSpecialCharacters( Fax ) );

            StoreContext.CheckoutDetails.BillingAddress = billing;
            StoreContext.CheckoutDetails.PaymentToken = Token;
        }
    }


    private bool IsCreatedByAdmin()
    {
        return StoreContext.CheckoutDetails.IsCreatedByAdmin;
    }
    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        SetBillingAddressToSession();

        if (IsCreatedByAdmin())
            Response.Redirect( "admin/Default.aspx#OrderCreateCheckOutSummary" );

        if (!IsAnonymousCheckout())
            Response.Redirect( "OrderSummary.aspx" );
        else
            Response.Redirect( "OrderSummary.aspx?skiplogin=true" );
    }

    protected bool IsAnonymousCheckout()
    {
        if (Request.QueryString["skiplogin"] == "true")
            return true;
        else
            return false;
    }

    #endregion

}
