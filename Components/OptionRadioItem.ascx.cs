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

public partial class Components_OptionRadioItem : Vevo.WebUI.International.BaseLanguageUserControl
{
    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    #endregion

    #region Public Methods

    public void SetupRadio( IList<OptionItemDisplay> table )
    {
        uxOptionRadioButtonList.DataSource = table;
        uxOptionRadioButtonList.DataBind();
    }

    public void SetValidGroup( string groupName )
    {
        uxOptionRadioButtonList.ValidationGroup = groupName;
        uxRadioRequiredValid.ValidationGroup = groupName;
    }

    public bool ValidateInput()
    {
        bool result = false;
        if (uxOptionRadioButtonList.SelectedValue != "")
            result = true;
        return result;
    }

    public void CreateOption( ArrayList selectedList, bool useStock )
    {
        OptionItem optionItem = DataAccessContext.OptionItemRepository.GetOne(
            StoreContext.Culture,
            uxOptionRadioButtonList.SelectedValue );

        OptionItemValue itemSelected =
            new OptionItemValue(
                optionItem, OptionGroup.OptionGroupType.Radio, String.Empty, useStock );

        selectedList.Add( itemSelected );
    }

    #endregion

}
