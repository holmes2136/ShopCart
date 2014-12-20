using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Shared.Utilities;

public partial class AdminAdvanced_Components_GiftCertificate : AdminAdvancedBaseUserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected string GetGiftCertificateCode( object giftCertificateCode )
    {
        return giftCertificateCode.ToString().ToUpper();
    }

    protected string GetNeedPhysical( object needPhysical )
    {
        if (ConvertUtilities.ToBoolean( needPhysical ))
            return "Yes";
        else
            return "No";
    }

    protected string GetExpireDate( string isExpirable, object expireDate )
    {
        if (!ConvertUtilities.ToBoolean( isExpirable ))
            return "-";
        else
        {
            return String.Format( "{0:dd} {0:MMM} {0:yyyy}", (DateTime) expireDate );
        }
    }

    protected string GetPrintUrl( string giftCode )
    {
        return "../GiftCertifiCatePrint.aspx?GiftCertificateCode=" + giftCode;
    }
}
