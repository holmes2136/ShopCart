using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ContentConainer
/// </summary>

[ParseChildren( true )]
public class ContentContainer : Control, INamingContainer
{
    public ContentContainer()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
