using System;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebAppLib;

public partial class Admin_Components_SpecificationGroupDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string CurrentID
    {
        get
        {
            return MainContext.QueryString["SpecificationGroupID"];
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
        SpecificationGroup specificationGroup = DataAccessContext.SpecificationGroupRepository.GetOne(
            uxLanguageControl.CurrentCulture, CurrentID );

        uxNameText.Text = specificationGroup.Name;
        uxDisplayNameText.Text = specificationGroup.DisplayName;
        uxDescriptionText.Text = specificationGroup.Description;
    }

    private SpecificationGroup SetupSpecificationGroup( SpecificationGroup specificationGroup )
    {
        specificationGroup.Name = uxNameText.Text;
        specificationGroup.DisplayName = uxDisplayNameText.Text;
        specificationGroup.Description = uxDescriptionText.Text;
        return specificationGroup;
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
            uxAddSpecificationGroupButton.Visible = false;
            uxUpdateSpecificationGroupButton.Visible = true;
            WebUtilities.TieButton( this.Page, uxNameText, uxUpdateSpecificationGroupButton );

            if (IsAdminModifiable())
            {
                uxUpdateSpecificationGroupButton.Visible = true;
            }
            else
            {
                uxUpdateSpecificationGroupButton.Visible = false;
            }
        }
        else
        {
            if (IsAdminModifiable())
            {
                uxAddSpecificationGroupButton.Visible = true;
                uxUpdateSpecificationGroupButton.Visible = false;
                WebUtilities.TieButton( this.Page, uxNameText, uxAddSpecificationGroupButton );
            }
            else
            {
                MainContext.RedirectMainControl( "SpecificationGroupList.ascx" );
            }
        }
    }

    protected void uxAddSpecificationGroupButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                SpecificationGroup specificationGroup = new SpecificationGroup( uxLanguageControl.CurrentCulture );
                specificationGroup = SetupSpecificationGroup( specificationGroup );
                specificationGroup = DataAccessContext.SpecificationGroupRepository.Save( specificationGroup );

                MainContext.RedirectMainControl(
                    "SpecificationItemList.ascx", String.Format( "SpecificationGroupID={0}", specificationGroup.SpecificationGroupID ) );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxUpdateSpecificationGroupButton_Click( object sender, EventArgs e )
    {
        try
        {
            SpecificationGroup specificationGroup = DataAccessContext.SpecificationGroupRepository.GetOne(
                uxLanguageControl.CurrentCulture, CurrentID );
            specificationGroup = SetupSpecificationGroup( specificationGroup );
            specificationGroup = DataAccessContext.SpecificationGroupRepository.Save( specificationGroup );

            uxMessage.DisplayMessage( Resources.ProductSpecificationMessages.UpdateSuccess );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

}
