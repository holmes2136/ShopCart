using System;
using System.Text;
using System.Web.UI;
using Vevo.Domain;
using Vevo.Domain.Discounts;
using Vevo.Shared.Utilities;
using Vevo.WebUI.International;

public partial class Components_QuantityDiscount : BaseLanguageUserControl
{
    #region Private

    private void RegisterScripts()
    {
        uxShowLink.Attributes.Add( "onclick", "return DisplayQuantityDiscountDetail(" +
            "'" + uxShowLink.ClientID + "'," +
            "'" + uxHideLink.ClientID + "'," +
            "'" + uxQuantityDiscountGridViewPanel.ClientID + "');" );
        uxHideLink.Attributes.Add( "onclick", "return DisplayQuantityDiscountDetail(" +
            "'" + uxShowLink.ClientID + "'," +
            "'" + uxHideLink.ClientID + "'," +
            "'" + uxQuantityDiscountGridViewPanel.ClientID + "');" );
        
        String csname = "QuantityDiscountPanel";
        ClientScriptManager cs = Page.ClientScript;
        
        StringBuilder sb = new StringBuilder();
        sb.AppendLine( "function DisplayQuantityDiscountDetail(ShowLink,HideLink,DetailPanel) {" );
        sb.AppendLine( "    var ShowLinkId = document.getElementById(ShowLink);" );
        sb.AppendLine( "    var HideLinkId = document.getElementById(HideLink);" );
        sb.AppendLine( "    var DetailPanelId = document.getElementById(DetailPanel);" );
        sb.AppendLine( "    if( ShowLinkId.style.display == 'block' ) {" );
        sb.AppendLine( "        ShowLinkId.style.display = 'none';" );
        sb.AppendLine( "        HideLinkId.style.display = 'block';" );
        sb.AppendLine( "        DetailPanelId.style.display = 'block';" );
        sb.AppendLine( "        return false; " );
        sb.AppendLine( "    } " );
        sb.AppendLine( "    else {" );
        sb.AppendLine( "        ShowLinkId.style.display = 'block';" );
        sb.AppendLine( "        HideLinkId.style.display = 'none';" );
        sb.AppendLine( "        DetailPanelId.style.display = 'none';" );
        sb.AppendLine( "        return false; " );
        sb.AppendLine( "    } " );
        sb.AppendLine( " } " );

        if ( !cs.IsClientScriptBlockRegistered( this.GetType(), csname ) )
        {
            cs.RegisterClientScriptBlock( this.GetType(), csname, sb.ToString(), true );
        }
    }

    #endregion


    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        RegisterScripts();
        uxQuantityDiscountGridViewPanel.Style.Add( "display", "none" );
        uxShowLink.Style.Add( "display", "block" );
        uxHideLink.Style.Add( "display", "none" );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if ( DataAccessContext.Configurations.GetBoolValue( "PriceRequireLogin" ) && !Page.User.Identity.IsAuthenticated )
        {
            uxShowLink.Visible = false;
        }

        DiscountGroup discountGroup = DataAccessContext.DiscountGroupRepository.GetOne( DiscountGroupID );

        if ( discountGroup.DiscountRules.Count > 0 )
        {
            uxQuantityDiscountView.DataSource = discountGroup.DiscountRules;
            uxQuantityDiscountView.DataBind();
        }
        else
        {
            uxQuantityDiscountPanel.Visible = false;
        }
    }

    protected string ShowFromItem( object toItem )
    {
        int fromItems = 0;
        if ( DiscountGroupID != "0" )
        {
            DiscountGroup discountGroup = DataAccessContext.DiscountGroupRepository.GetOne( DiscountGroupID );

            for ( int i = 0; i < discountGroup.DiscountRules.Count; i++ )
            {
                if ( discountGroup.DiscountRules[ i ].ToItems == ConvertUtilities.ToInt32( toItem ) )
                    return String.Format( "{0}", ++fromItems );
                else
                    fromItems = discountGroup.DiscountRules[ i ].ToItems;
            }
        }
        return String.Format( "{0}", ++fromItems );
    }

    protected string LastToItems( string toItems )
    {
        if ( toItems == DiscountRule.MaxNumberOfItems.ToString() )
            return "Above";
        else
            return toItems;
    }

    #endregion


    #region Public Properties

    public string DiscountGroupID
    {
        get
        {
            if (ViewState["DiscountGroupID"] == null)
                return "0";
            return ViewState["DiscountGroupID"].ToString();
        }
        set
        {
            ViewState["DiscountGroupID"] = value;
        }
    }

    #endregion
}
