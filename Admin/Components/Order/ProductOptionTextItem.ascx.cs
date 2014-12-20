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
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.Domain;
using Vevo.WebUI;

public partial class Admin_Components_Order_ProductOptionTextItem : AdminAdvancedBaseUserControl
{
    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    #endregion


    #region Public Properties

    public string OptionLabel
    {
        get
        {
            return uxOptionsLabel.Text;
        }
        set
        {
            uxOptionsLabel.Text = value;
        }
    }

    #endregion


    #region Public Methods

    public bool ValidateInput()
    {
        bool result = false;
        if (uxOptionCheck.Checked)
        {
            if (uxOptionsText.Text == "")
            {
                uxMessageLabel.Text = "Option Text Invalid";
            }
            else
            {
                result = true;
            }
        }
        else
        {
            result = true;
        }
        return result;
    }

    public void CreateOption( ArrayList selectedList, string optionID, bool useStock )
    {
        if (uxOptionCheck.Checked)
            if (uxOptionsText.Text != "")
            {
                OptionItem optionItem = DataAccessContext.OptionItemRepository.GetOne(
                    StoreContext.Culture, optionID );

                OptionItemValue itemSelected =
                    new OptionItemValue(
                        optionItem, OptionGroup.OptionGroupType.Text, Server.HtmlEncode( uxOptionsText.Text ), useStock );
                selectedList.Add( itemSelected );
            }
    }

    public void SetUpText( OptionItemValue selectedItem )
    {
        if (selectedItem != null)
        {
            uxOptionCheck.Checked = true;
            uxOptionsText.Text = selectedItem.Details;
        }
    }
    #endregion
}
