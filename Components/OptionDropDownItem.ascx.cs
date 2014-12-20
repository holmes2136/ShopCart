using System;
using System.Data;
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
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.WebUI;

public partial class Components_OptionDropDownItem : Vevo.WebUI.International.BaseLanguageUserControl
{
    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    #endregion


    #region Public Methods

    public void SetUpDropDown( IList<OptionItemDisplay> table )
    {
        uxOptionDrop.DataSource = table;
        uxOptionDrop.DataBind();
        uxOptionDrop.Items.Insert( 0, new ListItem( "--- Please select ---", String.Empty ) );
    }

    public void SetValidGroup( string groupName )
    {
        uxDropRequiredValid.ValidationGroup = groupName;
        uxDropRequiredValid.ValidationGroup = groupName;
    }

    public bool ValidateInput()
    {
        bool result = false;
        if (uxOptionDrop.SelectedValue != "")
            result = true;
        return result;
    }

    public void CreateOption( ArrayList selectedList, bool useStock )
    {
        OptionItem optionItem = DataAccessContext.OptionItemRepository.GetOne(
            StoreContext.Culture, uxOptionDrop.SelectedValue );

        OptionItemValue itemSelected =
                new OptionItemValue(
                    optionItem, OptionGroup.OptionGroupType.Radio, String.Empty, useStock );
        selectedList.Add( itemSelected );
    }

    #endregion
}
