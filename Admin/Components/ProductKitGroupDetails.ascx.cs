using System;
using System.Web.UI;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;

public partial class Admin_Components_ProductKitGroupDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string CurrentID
    {
        get
        {
            if (IsEditMode())
                return MainContext.QueryString["ProductKitGroupID"];
            else
                return "0";
        }
    }

    private void PopulateControls()
    {
        if (CurrentID != null && int.Parse( CurrentID ) >= 0)
        {
            ProductKitGroup productKitGroup = DataAccessContext.ProductKitGroupRepository.GetOne( uxLanguageControl.CurrentCulture, CurrentID );
            uxProductKitGroupNameText.Text = productKitGroup.Name;
            uxTypeDrop.SelectedValue = productKitGroup.Type.ToString();
            uxIsRequiredCheck.Checked = productKitGroup.IsRequired;
            uxDescriptionText.Text = productKitGroup.Description;
        }
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private void ClearInputFields()
    {
        uxProductKitGroupNameText.Text = "";
        uxTypeDrop.SelectedValue = ProductKitGroup.ProductKitGroupType.Radio.ToString();
        uxIsRequiredCheck.Checked = false;
        uxDescriptionText.Text = "";
    }

    private ProductKitGroup SetupProductKitGroup( ProductKitGroup productKitGroup )
    {
        productKitGroup.Name = uxProductKitGroupNameText.Text;
        productKitGroup.Description = uxDescriptionText.Text;
        productKitGroup.IsRequired = uxIsRequiredCheck.Checked;
        productKitGroup.Type = (ProductKitGroup.ProductKitGroupType) Enum.Parse( typeof( ProductKitGroup.ProductKitGroupType ), uxTypeDrop.SelectedValue );
        return productKitGroup;
    }

    private void Update()
    {
        try
        {
            if (Page.IsValid)
            {
                ProductKitGroup productKitGroup = DataAccessContext.ProductKitGroupRepository.GetOne( uxLanguageControl.CurrentCulture, CurrentID );
                productKitGroup = SetupProductKitGroup( productKitGroup );
                productKitGroup = DataAccessContext.ProductKitGroupRepository.Save( productKitGroup );

                PopulateControls();
                uxMessage.DisplayMessage( Resources.ProductKitGroupMessages.UpdateSuccess );
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxLanguageControl.BubbleEvent += new EventHandler( Details_RefreshHandler );
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            if (IsEditMode())
            {
                PopulateControls();
                uxAddButton.Visible = false;

                if (IsAdminModifiable())
                {
                    uxUpdateButton.Visible = true;
                }
                else
                {
                    uxUpdateButton.Visible = false;
                }

            }
            else
            {
                if (IsAdminModifiable())
                {
                    uxAddButton.Visible = true;
                    uxUpdateButton.Visible = false;
                }
                else
                {
                    MainContext.RedirectMainControl( "ProductKitGroupList.ascx" );
                }
            }
        }

    }

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        try
        {
            if (Page.IsValid)
            {
                ProductKitGroup newGroup = new ProductKitGroup( uxLanguageControl.CurrentCulture );
                newGroup = SetupProductKitGroup( newGroup );
                newGroup = DataAccessContext.ProductKitGroupRepository.Save( newGroup );
                uxMessage.DisplayMessage( Resources.ProductKitGroupMessages.AddSuccess );
                ClearInputFields();

                MainContext.RedirectMainControl(
                    "ProductKitGroupItemList.ascx", String.Format( "ProductKitGroupID={0}", newGroup.ProductKitGroupID ) );
            }
            else
                uxMessage.DisplayMessage( Resources.ProductKitGroupMessages.UpdateError );
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        Update();
    }
}
