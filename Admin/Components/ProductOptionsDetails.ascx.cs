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
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.WebAppLib;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;

public partial class Components_ProductOptionsDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string CurrentID
    {
        get
        {
            return MainContext.QueryString["OptionGroupID"];
        }
    }

    private string CurrentCulture
    {
        get
        {
            return uxLanguageControl.CurrentCultureID;
        }
    }

    private void PopulateDetails()
    {
        OptionGroup optionGroup = DataAccessContext.OptionGroupRepository.GetOne(
            uxLanguageControl.CurrentCulture, CurrentID );

        uxTypeDrop.SelectedValue = optionGroup.Type.ToString();
        uxNameOptionText.Text = optionGroup.Name;
        uxDisplayNameText.Text = optionGroup.DisplayText;
    }

    private bool CheckChangeOptionType()
    {
        if (uxTypeDrop.SelectedValue == "Text" ||
            uxTypeDrop.SelectedValue == "Upload" ||
            uxTypeDrop.SelectedValue == "UploadRequired"
            )
        {
            IList<OptionItem> list = DataAccessContext.OptionItemRepository.GetByOptionGroupID(
                uxLanguageControl.CurrentCulture, CurrentID );

            if (list.Count > 1)
                return false;
            else
                return true;
        }
        else
            return true;

    }

    private OptionGroup.OptionGroupType GetOptionGroupType()
    {
        return (OptionGroup.OptionGroupType) Enum.Parse( typeof( OptionGroup.OptionGroupType ), uxTypeDrop.SelectedValue );
    }

    private OptionGroup SetupOptionGroup( OptionGroup optionGroup )
    {
        optionGroup.Name=uxNameOptionText.Text;
        optionGroup.Type = GetOptionGroupType();
        optionGroup.DisplayText = uxDisplayNameText.Text;
        return optionGroup;
    }

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (IsEditMode())
        {
            PopulateDetails();
            uxAddOptionGroupButton.Visible = false;
            uxUpdateOptionGroupButton.Visible = true;
            WebUtilities.TieButton( this.Page, uxNameOptionText, uxUpdateOptionGroupButton );

            if (IsAdminModifiable())
            {
                uxUpdateOptionGroupButton.Visible = true;
            }
            else
            {
                uxUpdateOptionGroupButton.Visible = false;
            }
        }
        else
        {
            if (IsAdminModifiable())
            {
                uxAddOptionGroupButton.Visible = true;
                uxUpdateOptionGroupButton.Visible = false;
                WebUtilities.TieButton( this.Page, uxNameOptionText, uxAddOptionGroupButton );
            }
            else
            {
                MainContext.RedirectMainControl( "ProductOptionList.ascx" );
            }
        }
    }

    protected void uxAddOptionGroupButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                OptionGroup optionGroup = new OptionGroup( uxLanguageControl.CurrentCulture );
                optionGroup = SetupOptionGroup( optionGroup );
                optionGroup = DataAccessContext.OptionGroupRepository.Save( optionGroup );

                MainContext.RedirectMainControl( 
                    "ProductOptionItemList.ascx", String.Format( "OptionGroupID={0}", optionGroup.OptionGroupID ) );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxUpdateOptionGroupButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (CheckChangeOptionType())
            {
                OptionGroup optionGroup = DataAccessContext.OptionGroupRepository.GetOne( uxLanguageControl.CurrentCulture, CurrentID );
                optionGroup = SetupOptionGroup( optionGroup );
                DataAccessContext.OptionGroupRepository.Save( optionGroup );

                uxMessage.DisplayMessage( Resources.ProductOptionMessages.UpdateSuccess );
            }
            else
                uxMessage.DisplayError( Resources.ProductOptionMessages.UpdateTypeError );
            
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

}
