using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.WebUI;

public partial class Admin_Components_Order_ProductOptionDropDownItem : AdminAdvancedBaseUserControl
{
    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    #endregion

    #region Public Methods

    public void SetUpDropDown( IList<OptionItemDisplay> table, OptionItemValue selectedItem )
    {
        uxOptionDrop.DataSource = table;
        uxOptionDrop.DataBind();
        uxOptionDrop.Items.Insert( 0, new ListItem( "--- Please select ---", String.Empty ) );
      
        if (selectedItem != null)
            uxOptionDrop.SelectedValue = selectedItem.OptionItemID;
    }

    public void SetValidGroup( string groupName )
    {
        uxDropRequiredValid.ValidationGroup = groupName;
        uxOptionDrop.ValidationGroup = groupName;
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

    public void SetSelectedOption( OptionItem item )
    {
        uxOptionDrop.SelectedValue = item.OptionItemID;
    }

    #endregion
}
