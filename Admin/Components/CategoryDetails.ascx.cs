using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Stores;
using Vevo.Shared.Utilities;

public partial class AdminAdvanced_Components_CategoryDetails : AdminAdvancedBaseUserControl
{
    private const string _pathUploadCategory = "Images/Categories/";

    private enum Mode { Add, Edit };

    private Mode _mode = Mode.Add;

    private string CurrentID
    {
        get
        {
            return MainContext.QueryString["CategoryID"];
        }
    }

    private string RootCategoryID
    {
        get
        {
            return MainContext.QueryString["RootID"];
        }
    }

    private string ParentCategoryID
    {
        get
        {
            if (ViewState["ParentCategoryID"] == null)
                return "0";
            else
                return (string) ViewState["ParentCategoryID"];
        }
        set
        {
            ViewState["ParentCategoryID"] = value;
        }
    }

    private bool OriginalIsEnabled
    {
        get
        {
            if (ViewState["OriginalIsEnabled"] == null)
                return uxIsEnabledCheck.Checked;
            else
                return (bool) ViewState["OriginalIsEnabled"];
        }
        set
        {
            ViewState["OriginalIsEnabled"] = value;
        }
    }

    private bool IsEnabledChanged()
    {
        if (uxIsEnabledCheck.Checked != OriginalIsEnabled)
            return true;
        else
            return false;
    }

    private void InitParentDropDownList()
    {
        int selectedIndex = uxParentDrop.SelectedIndex;
        uxParentDrop.Items.Clear();
        uxParentDrop.Items.Add( new ListItem( "Root", RootCategoryID, true ) );

        IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByRootIDNoProduct(
            uxLanguageControl.CurrentCulture, RootCategoryID, "SortOrder", BoolFilter.ShowAll );

        for (int i = 0; i < categoryList.Count; i++)
        {
            if (categoryList[i].DiscountGroupID.ToString() == "0")
                uxParentDrop.Items.Add( new ListItem(
                    categoryList[i].CreateFullCategoryPath() + " (" + categoryList[i].CategoryID + ")",
                    categoryList[i].CategoryID ) );

        }

        if (IsEditMode())
        {
            uxParentDrop.Items.Remove( uxParentDrop.Items.FindByValue( CurrentID ) );
        }

        uxParentDrop.SelectedIndex = selectedIndex;

        if (!String.IsNullOrEmpty( CurrentID ))
        {
            Category category = DataAccessContext.CategoryRepository.GetOne( uxLanguageControl.CurrentCulture, CurrentID );
            if (!category.IsLeafCategory())
            {
                uxProductListLayoutPanel.Style["display"] = "none";
                uxLayoutRadioPanel.Style["display"] = "none";
                uxLayoutTypeRadioButtonList.Items[0].Selected = true;
                uxHiddenStatus.Value = "category";
            }
        }
    }

    private void ClearInputFields()
    {
        uxNameText.Text = "";
        uxDescriptionText.Text = "";
        uxImageText.Text = "";
        uxParentDrop.SelectedIndex = 0;
        uxQuantityDiscount.Clear();
        uxOtherOneText.Text = "";
        uxOtherTwoText.Text = "";
        uxOtherThreeText.Text = "";
        uxOtherFourText.Text = "";
        uxOtherFiveText.Text = "";
        uxMetaKeywordText.Text = "";
        uxMetaDescriptionText.Text = "";
        uxIsEnabledCheck.Checked = true;
        uxLayoutOverrideDrop.SelectedValue = "False";
        uxImageCategoryUpload.ShowControl = false;
        uxImgAlternateTextbox.Text = "";
        uxImgTitleTextbox.Text = "";
        uxIsAnchorCheck.Checked = false;
        uxIsShowNewArrivalCheck.Checked = false;
        uxNewArrivalAmountPanel.Visible = uxIsShowNewArrivalCheck.Checked;
        uxNewArrivalAmountText.Text = "";
    }

    private void PouplateThemControlsInPreRender()
    {
        if (!ConvertUtilities.ToBoolean( uxLayoutOverrideDrop.SelectedValue ))
        {
            uxLayoutOverrideDrop.SelectedValue = "False";
            uxLayoutRadioPanel.Style["display"] = "none";
            uxCategoryLayoutPanel.Style["display"] = "none";
            uxProductListLayoutPanel.Style["display"] = "none";
        }
        else
        {
            //uxLayoutRadioPanel.Style["display"] = "none";
            if (uxLayoutTypeRadioButtonList.SelectedValue == "Category")
            {
                uxCategoryLayoutPanel.Style["display"] = "";
                uxProductListLayoutPanel.Style["display"] = "none";
            }
            else
            {
                uxCategoryLayoutPanel.Style["display"] = "none";
                uxProductListLayoutPanel.Style["display"] = "";
            }

            if (!IsEditMode())
                uxLayoutRadioPanel.Style["display"] = "";
        }
    }

    private void PopulateControls()
    {
        if (CurrentID != null &&
             int.Parse( CurrentID ) >= 0)
        {
            Category category = DataAccessContext.CategoryRepository.GetOne(
                uxLanguageControl.CurrentCulture, CurrentID );

            uxNameText.Text = category.Name;
            uxDescriptionText.Text = category.Description;
            uxImageText.Text = category.ImageFile;
            uxParentDrop.SelectedValue = category.ParentCategoryID.ToString();
            uxQuantityDiscount.Refresh( category.DiscountGroupID.ToString() );
            uxIsEnabledCheck.Checked = category.IsEnabled;
            ParentCategoryID = category.ParentCategoryID.ToString();
            OriginalIsEnabled = category.IsEnabled;
            uxOtherOneText.Text = category.Other1;
            uxOtherTwoText.Text = category.Other2;
            uxOtherThreeText.Text = category.Other3;
            uxOtherFourText.Text = category.Other4;
            uxOtherFiveText.Text = category.Other5;
            uxMetaKeywordText.Text = category.MetaKeyword;
            uxMetaDescriptionText.Text = category.MetaDescription;
            uxCategoryLayoutDrop.SelectedValue = category.CategoryListLayoutPath;
            uxProductListLayoutDrop.SelectedValue = category.ProductListLayoutPath;
            uxImgAlternateTextbox.Text = category.ImageAlt;
            uxImgTitleTextbox.Text = category.ImageTitle;
            uxIsAnchorCheck.Checked = category.IsAnchor;
            uxIsShowNewArrivalCheck.Checked = category.IsShowNewArrival;
            uxNewArrivalAmountText.Text = category.NewArrivalAmount.ToString();
            uxNewArrivalAmountPanel.Visible = uxIsShowNewArrivalCheck.Checked;
            if (String.IsNullOrEmpty( category.CategoryListLayoutPath ) && String.IsNullOrEmpty( category.ProductListLayoutPath ))
            {
                uxLayoutOverrideDrop.SelectedValue = "False";
                uxLayoutRadioPanel.Style["display"] = "none";
                uxCategoryLayoutPanel.Style["display"] = "none";
                uxProductListLayoutPanel.Style["display"] = "none";
            }
            else
            {
                uxLayoutRadioPanel.Style["display"] = "none";
                uxLayoutOverrideDrop.SelectedValue = "True";
                IList<Category> categoryList = DataAccessContext.CategoryRepository.GetAll( uxLanguageControl.CurrentCulture, "SortOrder" );
                int productCount = DataAccessContext.ProductRepository.GetProductCountByCategoryID( category.CategoryID, BoolFilter.ShowAll );
                if ((!String.IsNullOrEmpty( category.CategoryListLayoutPath ) && productCount == 0) || !category.IsLeafCategory( categoryList ))
                {
                    uxLayoutTypeRadioButtonList.SelectedValue = "Category";
                    uxCategoryLayoutDrop.SelectedValue = category.CategoryListLayoutPath;
                    uxProductListLayoutDrop.SelectedValue = "";
                    uxCategoryLayoutPanel.Style["display"] = "";
                    uxProductListLayoutPanel.Style["display"] = "none";
                }
                else
                {
                    uxLayoutTypeRadioButtonList.SelectedValue = "Product";
                    uxCategoryLayoutDrop.SelectedValue = "";
                    uxProductListLayoutDrop.SelectedValue = category.ProductListLayoutPath;
                    uxCategoryLayoutPanel.Style["display"] = "none";
                    uxProductListLayoutPanel.Style["display"] = "";
                }
            }

        }
    }

    private void AddCategory()
    {
        try
        {
            if (Page.IsValid)
            {
                Category category = new Category( uxLanguageControl.CurrentCulture );
                category = SetUpCategory( category );
                category = DataAccessContext.CategoryRepository.Save( category );

                HttpContext.Current.Session[SystemConst.CategoryTreeViewLeftKey] = null;
                uxMessage.DisplayMessage( Resources.CategoryMessages.AddSuccess );
                ClearInputFields();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        uxStatusHidden.Value = "Added";

        AdminUtilities.ClearSiteMapCache();
    }

    private void UpdateCategory()
    {
        try
        {
            if (Page.IsValid)
            {
                if (uxParentDrop.SelectedValue != ParentCategoryID)
                {
                    DataAccessContext.CategoryRepository.ReArrangeSortOrder( uxLanguageControl.CurrentCulture, uxParentDrop.SelectedValue, CurrentID, BoolFilter.ShowTrue );
                }

                Category category = DataAccessContext.CategoryRepository.GetOne(
                    uxLanguageControl.CurrentCulture, CurrentID );
                category = SetUpCategory( category );
                category = DataAccessContext.CategoryRepository.Save( category );

                if (IsEnabledChanged())
                    DataAccessContext.ProductRepository.RefreshIsParentVisibleByCategoryID( CurrentID, new StoreRetriever().GetCurrentStoreID() );

                DataAccessContext.CategoryRepository.ReArrangeSortOrderByParentCategoryID(
                    uxLanguageControl.CurrentCulture,
                    ParentCategoryID,
                    BoolFilter.ShowTrue );

                HttpContext.Current.Session[SystemConst.CategoryTreeViewLeftKey] = null;
                uxMessage.DisplayMessage( Resources.CategoryMessages.UpdateSuccess );

                InitParentDropDownList();
                PopulateControls();
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }

        uxStatusHidden.Value = "Updated";

        AdminUtilities.ClearSiteMapCache();
    }

    private void Details_RefreshHandler( object sender, EventArgs e )
    {
        PopulateControls();
    }

    private string SetParentCategoryID()
    {
        if (uxParentDrop.SelectedValue == "0")
        {
            return RootCategoryID;
        }
        else
        {
            return uxParentDrop.SelectedValue;
        }
    }

    private Category SetUpCategory( Category category )
    {
        category.Name = uxNameText.Text;
        category.Description = uxDescriptionText.Text;
        category.ImageFile = uxImageText.Text;
        category.ParentCategoryID = SetParentCategoryID();
        category.DiscountGroupID = ConvertUtilities.ToInt32( uxQuantityDiscount.DiscountGounpID );
        category.IsEnabled = uxIsEnabledCheck.Checked;
        category.Other1 = uxOtherOneText.Text;
        category.Other2 = uxOtherTwoText.Text;
        category.Other3 = uxOtherThreeText.Text;
        category.Other4 = uxOtherFourText.Text;
        category.Other5 = uxOtherFiveText.Text;
        category.MetaKeyword = uxMetaKeywordText.Text;
        category.MetaDescription = uxMetaDescriptionText.Text;
        category.CategoryListLayoutPath = uxCategoryLayoutDrop.SelectedValue;
        category.ProductListLayoutPath = uxProductListLayoutDrop.SelectedValue;
        category.RootID = RootCategoryID;
        category.ImageAlt = uxImgAlternateTextbox.Text;
        category.ImageTitle = uxImgTitleTextbox.Text;
        category.IsAnchor = uxIsAnchorCheck.Checked;
        category.IsShowNewArrival = uxIsShowNewArrivalCheck.Checked;
        category.NewArrivalAmount = ConvertUtilities.ToInt32( uxNewArrivalAmountText.Text );

        if (!ConvertUtilities.ToBoolean( uxLayoutOverrideDrop.SelectedValue ))
        {
            category.ProductListLayoutPath = String.Empty;
            category.CategoryListLayoutPath = String.Empty;
        }
        else
        {
            if (uxLayoutTypeRadioButtonList.SelectedValue == "Category")
            {
                category.ProductListLayoutPath = String.Empty;
                category.CategoryListLayoutPath = uxCategoryLayoutDrop.SelectedValue;
            }
            else
            {
                category.ProductListLayoutPath = uxProductListLayoutDrop.SelectedValue;
                category.CategoryListLayoutPath = String.Empty;
            }
        }

        return category;
    }

    private void PopulateDropdown()
    {
        string[] fileList = Directory.GetFiles( Server.MapPath( SystemConst.LayoutCategoryListPath ) );
        uxCategoryLayoutDrop.Items.Clear();
        uxCategoryLayoutDrop.Items.Add( new ListItem( "Please Select...", "" ) );
        foreach (string item in fileList)
        {
            string itemName = item.Substring( item.LastIndexOf( "\\" ) + 1 );
            if (itemName.Substring( itemName.LastIndexOf( "." ) + 1 ) == "ascx")
            {
                uxCategoryLayoutDrop.Items.Add( new ListItem( itemName, itemName ) );
            }
        }
        uxCategoryLayoutDrop.SelectedValue =
            DataAccessContext.Configurations.GetValueNoThrow( "DefaultCategoryListLayout" );

        fileList = Directory.GetFiles( Server.MapPath( SystemConst.LayoutProductListPath ) );
        uxProductListLayoutDrop.Items.Clear();
        uxProductListLayoutDrop.Items.Add( new ListItem( "Please Select...", "" ) );
        foreach (string item in fileList)
        {
            string itemName = item.Substring( item.LastIndexOf( "\\" ) + 1 );
            if (itemName.Substring( itemName.LastIndexOf( "." ) + 1 ) == "ascx")
            {
                uxProductListLayoutDrop.Items.Add( new ListItem( itemName, itemName ) );
            }
        }
        uxProductListLayoutDrop.SelectedValue =
            DataAccessContext.Configurations.GetValueNoThrow( "DefaultProductListLayout" );
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxLanguageControl.BubbleEvent += new EventHandler( Details_RefreshHandler );

        uxLayoutRadioPanel.Style["display"] = "none";
        uxCategoryLayoutPanel.Style["display"] = "none";
        uxProductListLayoutPanel.Style["display"] = "none";
        uxHiddenStatus.Value = "";
        uxLayoutOverrideDrop.Attributes.Add( "onchange", String.Format( "LayoutOverrideChange( this,'{0}', '{1}', '{2}', '{3}' );", uxLayoutRadioPanel.ClientID, uxCategoryLayoutPanel.ClientID, uxProductListLayoutPanel.ClientID, uxHiddenStatus.ClientID ) );
        foreach (ListItem item in uxLayoutTypeRadioButtonList.Items)
        {
            item.Attributes.Add( "onClick", String.Format( "ShowSubPanel('{0}','{1}','{2}')", uxLayoutRadioPanel.ClientID, uxCategoryLayoutPanel.ClientID, uxProductListLayoutPanel.ClientID ) );
        }

        if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Test)
        {
            uxImageText.Visible = true;
        }

        if (!MainContext.IsPostBack)
        {
            uxNewArrivalAmountPanel.Visible = false;

            uxImageCategoryUpload.PathDestination = _pathUploadCategory;
            uxImageCategoryUpload.ReturnTextControlClientID = uxImageText.ClientID;
            PopulateDropdown();
        }

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (IsEditMode())
        {
            InitParentDropDownList();

            if (!MainContext.IsPostBack)
            {
                PopulateControls();
            }

            if (IsAdminModifiable())
            {
                uxUpdateButton.Visible = true;
            }
            else
            {
                uxUpdateButton.Visible = false;
                uxImageCategoryLinkButton.Visible = false;
            }

            uxAddButton.Visible = false;
        }
        else
        {
            InitParentDropDownList();

            if (IsAdminModifiable())
            {
                uxAddButton.Visible = true;
                uxUpdateButton.Visible = false;
            }
            else
            {
                MainContext.RedirectMainControl( "CategoryList.ascx" );
            }
        }
        PouplateThemControlsInPreRender();
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        AddCategory();
    }

    protected void uxUpdateButton_Click( object sender, EventArgs e )
    {
        UpdateCategory();
    }

    public bool IsEditMode()
    {
        return (_mode == Mode.Edit);
    }

    public void SetEditMode()
    {
        _mode = Mode.Edit;
    }

    protected void uxImageCategoryLinkButton_Click( object sender, EventArgs e )
    {
        uxImageCategoryUpload.ShowControl = true;
    }

    protected void uxIsShowNewArrivalCheck_CheckedChanged( object sender, EventArgs e )
    {
        uxNewArrivalAmountPanel.Visible = uxIsShowNewArrivalCheck.Checked;
    }
}
