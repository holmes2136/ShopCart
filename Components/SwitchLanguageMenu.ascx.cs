using System;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.Domain;
using Vevo.WebAppLib;
using Vevo.WebUI;

public partial class Components_SwitchLanguageMenu : Vevo.WebUI.BaseControls.BaseLayoutControl
{
    private void uxLanguageLinkButton_Command( object sender, CommandEventArgs e )
    {
        string cultureID = e.CommandArgument.ToString();
        CultureUtilities.StoreCultureID = cultureID;

        uxCultureIDHidden.Value = CultureUtilities.StoreCultureID;

        GetStorefrontEvents().OnStoreCultureChanged(
            this,
            new CultureEventArgs( CultureUtilities.StoreCultureID ) );
    }

    private void PopulateControls()
    {
        IList<Culture> list = DataAccessContext.CultureRepository.GetByEnabled( true );
        for (int i = 0; i < list.Count; i++)
        {
            LinkButton uxCultureLink = new LinkButton();
            uxCultureLink.Command += new CommandEventHandler( uxLanguageLinkButton_Command );

            uxCultureLink.CommandArgument = list[i].CultureID;

            if (i == list.Count - 1)
                uxCultureLink.Text = list[i].DisplayName;
            else
                uxCultureLink.Text = list[i].DisplayName + " | ";

            uxPanel.Controls.Add( uxCultureLink );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (String.Compare( DataAccessContext.Configurations.GetValue( "LanguageMenuDisplayMode" ), "Horizontal", true ) != 0)
        {
            this.Visible = false;
        }
        else
        {
            PopulateControls();
        }
    }

}
