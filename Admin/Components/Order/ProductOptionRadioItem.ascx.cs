using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Vevo;
using System.Collections.Generic;
using Vevo.Domain.Products;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.Domain.Orders;

public partial class Admin_Components_Order_ProductOptionRadioItem : AdminAdvancedBaseUserControl
{
    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    #endregion

    #region Public Methods

    public void SetupRadio( IList<OptionItemDisplay> table, OptionItemValue selectedItem )
    {
        uxOptionRadioButtonList.DataSource = table;
        uxOptionRadioButtonList.DataBind();
     
        if (selectedItem != null)
        {
            uxOptionRadioButtonList.SelectedValue = selectedItem.OptionItemID;
        }
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
