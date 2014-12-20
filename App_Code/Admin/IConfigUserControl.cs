using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.Domain.Configurations;

namespace Vevo
{
    /// <summary>
    /// Summary description for IConfigUserControl
    /// </summary>
    public interface IConfigUserControl
    {
        void Populate( Configuration config );
        void Update(  );
    }
}