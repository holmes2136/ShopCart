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

public partial class Components_OptionInputListItem : Vevo.WebUI.International.BaseLanguageUserControl
{
    #region Protected

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    #endregion


    #region Public Methods

    public void SetupInputList( IList<OptionItemDisplay> table )
    {
        uxInputDataList.DataSource = table;
        uxInputDataList.DataBind();
    }

    public bool ValidateInput()
    {
        bool result = false;
        foreach (DataListItem item in uxInputDataList.Items)
        {
            TextBox uxQtyText = (TextBox) item.FindControl( "uxInputText" );
            if (!String.IsNullOrEmpty( uxQtyText.Text ))
            {
                if (int.Parse( uxQtyText.Text ) != 0)
                {
                    result = true;
                    break;
                }
            }
            if (!result)
                uxInputListMessageLabel.Text = GetLanguageText("OptionInputListInvalid");
        }
        return result;
    }

    public void CreateOption( ArrayList selectedList, bool useStock )
    {
        foreach (DataListItem item in uxInputDataList.Items)
        {
            TextBox uxInputText = (TextBox) (item.FindControl( "uxInputText" ));
            if (uxInputText.Text != "" && uxInputText.Text != "0")
            {
                HiddenField hidden = (HiddenField) (item.FindControl( "uxInputIDHidden" ));
                HiddenField nameHidden = (HiddenField) (item.FindControl( "uxNameHidden" ));

                OptionItem optionItem = DataAccessContext.OptionItemRepository.GetOne(
                    StoreContext.Culture, hidden.Value );

                selectedList.Add(
                    new OptionItemValue(
                        optionItem, OptionGroup.OptionGroupType.InputList, uxInputText.Text, useStock ) );
            }
        }
    }

    public void SetValidGroup( string groupName )
    {
        foreach (DataListItem item in uxInputDataList.Items)
        {
            TextBox uxInputText = (TextBox) item.FindControl( "uxInputText" );
            uxInputText.ValidationGroup = groupName;
            CompareValidator uxInputListCompare = (CompareValidator) item.FindControl( "uxInputListCompare" );
            uxInputListCompare.ValidationGroup = groupName;
        }
    }

    #endregion
}
