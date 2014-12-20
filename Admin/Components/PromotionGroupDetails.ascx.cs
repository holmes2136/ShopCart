using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Stores;
using Vevo.Domain.Tax;
using Vevo.Shared.Utilities;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.Domain;

public partial class Admin_Components_PromotionGroupDetails : AdminAdvancedBaseUserControl
{
    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string CurrentID
    {
        get
        {
            if (IsEditMode())
                return MainContext.QueryString["PromotionGroupID"];
            else
                return "0";
        }
    }

    private string StoreID
    {
        get
        {
            if (KeyUtilities.IsMultistoreLicense())
            {
                return uxStoreDrop.SelectedValue;
            }
            else
            {
                return Store.RegularStoreID;
            }
        }
    }

    private void PopulateControls()
    {
        if (CurrentID != null && int.Parse( CurrentID ) >= 0)
        {
            PromotionGroup promotionGroup = DataAccessContextDeluxe.PromotionGroupRepository.GetOne( uxLanguageControl.CurrentCulture, CurrentID );
            uxPromotionGroupNameText.Text = promotionGroup.Name;
            uxPriceText.Text = String.Format( "{0:f2}", promotionGroup.Price );
            uxTaxClassDrop.SelectedValue = promotionGroup.TaxClassID;
            uxWeightText.Text = promotionGroup.Weight.ToString();
            uxDescriptionText.Text = promotionGroup.Description;
            uxIsEnabeldCheck.Checked = promotionGroup.IsEnabled;
            uxIsFreeShippingCheck.Checked = promotionGroup.IsFreeShipping;
            uxStoreDrop.SelectedValue = promotionGroup.StoreID;
            uxPromotionImage.PopulateControls( promotionGroup );
            InitMultiSubGroup();
        }
    }

    private void InsertStoreInDropDownList()
    {
        uxStoreDrop.Items.Clear();
        uxStoreDrop.AutoPostBack = false;

        IList<Store> storeList = DataAccessContext.StoreRepository.GetAll( "StoreID" );

        for (int index = 0; index < storeList.Count; index++)
        {
            uxStoreDrop.Items.Add( new ListItem( storeList[index].StoreName, storeList[index].StoreID ) );
        }
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private void ClearInputFields()
    {
        uxPromotionGroupNameText.Text = "";
        uxPriceText.Text = "";
        uxTaxClassDrop.SelectedValue = TaxClass.NonTaxClassID;
        uxWeightText.Text = "";
        uxDescriptionText.Text = "";
        uxIsEnabeldCheck.Checked = true;
        uxIsFreeShippingCheck.Checked = false;
        uxMultiSubGroup.SetupDropDownList( CurrentID, uxLanguageControl.CurrentCulture, false );
        uxPromotionImage.ClearInputField();
    }

    private PromotionGroup SetUpPromotionGroup( PromotionGroup promotionGroup )
    {
        promotionGroup.Name = uxPromotionGroupNameText.Text;
        promotionGroup.Description = uxDescriptionText.Text;
        promotionGroup.Price = ConvertUtilities.ToDecimal( uxPriceText.Text );
        promotionGroup.TaxClassID = uxTaxClassDrop.SelectedValue;
        promotionGroup.Weight = ConvertUtilities.ToDouble( uxWeightText.Text );
        promotionGroup.IsEnabled = uxIsEnabeldCheck.Checked;
        promotionGroup.IsFreeShipping = uxIsFreeShippingCheck.Checked;
        promotionGroup.StoreID = StoreID;
        promotionGroup.SubGroupIDs = uxMultiSubGroup.ConvertToSubGroupIDs();
        promotionGroup = uxPromotionImage.SetupImage( promotionGroup );

        return promotionGroup;
    }

    private void Update()
    {
        try
        {
            if (Page.IsValid)
            {
                if (uxMultiSubGroup.ConvertToSubGroupIDs().Length > 0)
                {
                    PromotionGroup promotionGroup = DataAccessContextDeluxe.PromotionGroupRepository.GetOne( uxLanguageControl.CurrentCulture, CurrentID );
                    promotionGroup = SetUpPromotionGroup( promotionGroup );
                    promotionGroup = DataAccessContextDeluxe.PromotionGroupRepository.Save( promotionGroup );

                    PopulateControls();
                    uxMessage.DisplayMessage( Resources.PromotionGroupMessage.UpdateSuccess );
                }
            	else
            	{
                	uxMessage.DisplayError( Resources.PromotionGroupMessage.AddErrorPromotionSubGroupEmpty );
               		return;
            	}
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    private void InitTaxClassDrop()
    {
        uxTaxClassDrop.DataSource = DataAccessContext.TaxClassRepository.GetAll( "TaxClassID" );
        uxTaxClassDrop.DataTextField = "TaxClassName";
        uxTaxClassDrop.DataValueField = "TaxClassID";
        uxTaxClassDrop.DataBind();
        uxTaxClassDrop.Items.Insert( 0, new ListItem( "None", "0" ) );
    }

    private void InitMultiSubGroup()
    {
        if (!MainContext.IsPostBack)
        {
            uxMultiSubGroup.SetupDropDownList( CurrentID, uxLanguageControl.CurrentCulture, false );
        }
        else
        {
            uxMultiSubGroup.SetupDropDownList( CurrentID, uxLanguageControl.CurrentCulture, true );
        }
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            InsertStoreInDropDownList();
            InitTaxClassDrop();
            InitMultiSubGroup();
        }

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
                    MainContext.RedirectMainControl( "PromotionGroupList.ascx" );
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
                if (uxMultiSubGroup.ConvertToSubGroupIDs().Length > 0)
                {
                    PromotionGroup promotionGroup = new PromotionGroup( uxLanguageControl.CurrentCulture );

                    promotionGroup = SetUpPromotionGroup( promotionGroup );
                    promotionGroup = DataAccessContextDeluxe.PromotionGroupRepository.Save( promotionGroup );
                    string newPromotionGroupID = promotionGroup.PromotionGroupID;

                    uxMessage.DisplayMessage( Resources.PromotionGroupMessage.AddSuccess );

                    ClearInputFields();
                }
                else
                {
                    uxMessage.DisplayError( Resources.PromotionGroupMessage.AddErrorPromotionSubGroupEmpty );
                    return;
                }
            }
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
