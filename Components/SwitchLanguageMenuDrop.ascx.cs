using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI;

public partial class Components_SwitchLanguageMenuDrop : Vevo.WebUI.International.BaseLanguageUserControl
{
    private MenuItem NewItem( Culture culture )
    {
        MenuItem newItem = new MenuItem();
        newItem.Text = culture.DisplayName;
        newItem.Value = culture.CultureID;

        return newItem;
    }

    private string GetCurrentLanguageName( IList<Culture> list )
    {
        string cultureName = String.Empty;
        foreach (Culture culture in list)
        {
            if (culture.CultureID == CultureUtilities.StoreCultureID)
            {
                cultureName = culture.DisplayName;
            }
        }

        return cultureName;
    }

    private void PopulateDropDown()
    {
        IList<Culture> list = DataAccessContext.CultureRepository.GetByEnabled( true );

        uxLanguageMenuDropDataList.Items.Clear();
        MenuItem topItem = new MenuItem();
        topItem.Text = GetCurrentLanguageName( list );
        topItem.Value = CultureUtilities.StoreCultureID;

        foreach (Culture culture in list)
        {
            MenuItem childrenItem = NewItem( culture );
            topItem.ChildItems.Add( childrenItem );
        }

        uxLanguageMenuDropDataList.Items.Add( topItem );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (String.Compare( DataAccessContext.Configurations.GetValue( "LanguageMenuDisplayMode" ), "DropDown", true ) != 0 )
        {
            this.Visible = false;
        }
        else
        {
            PopulateDropDown();

        }
        uxCultureIDHidden.Value = CultureUtilities.StoreCultureID;
    }

    protected void uxLanguageMenuDrop_OnMenuItemClick( object sender, EventArgs e )
    {
        CultureUtilities.StoreCultureID = uxLanguageMenuDropDataList.SelectedValue;

        GetStorefrontEvents().OnStoreCultureChanged(
            this,
            new CultureEventArgs( uxLanguageMenuDropDataList.SelectedValue ) );

    }
}
