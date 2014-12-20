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
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Orders;
using Vevo.Domain.Products;
using Vevo.WebUI;

public partial class Components_OptionTextItem : Vevo.WebUI.International.BaseLanguageUserControl
{
    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {
        uxMessageDiv.Visible = false;
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
                uxMessageDiv.Visible = true;
                uxMessageLabel.Text = GetLanguageText("OptionTextInvalid");
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

    #endregion

}
