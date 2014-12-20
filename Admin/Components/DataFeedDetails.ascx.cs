using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Domain.Shipping;
using Vevo.Domain.Stores;

public partial class AdminAdvanced_Components_DataFeedDetails : AdminAdvancedBaseUserControl
{
    private const string _directory = "Export/";
    private bool _notRenameFileName = false;
    private string _cultureID = String.Empty;

    private string filePathGoogleCategory = "../DataFiles/GoogleFeed/";
    private string fileNameGoogleCategory = "Google Category.txt";

    public string CultureID
    {
        get
        {
            if (ViewState["CultureID"] == null)
                return String.Empty;
            else
                return (String) ViewState["CultureID"];


        }
        set
        {
            ViewState["CultureID"] = value;
        }
    }

    private Culture Culture
    {
        get
        {
            return DataAccessContext.CultureRepository.GetOne( CultureID );
        }
    }

    private DataTable CreateDataSource()
    {
        string rootID = DataAccessContext.Configurations.GetValue(
            "RootCategory",
            DataAccessContext.StoreRepository.GetOne( uxStoreFilterDrop.SelectedValue ) );

        IList<Category> categoryList = DataAccessContext.CategoryRepository.GetByRootIDLeafOnly( Culture, rootID, "ParentCategoryID", BoolFilter.ShowAll );

        DataTable result = new DataTable();
        result.Columns.Add( "CategoryID" );
        result.Columns.Add( "Name" );

        string currentParentID = "";
        string tmpFullPath = "";
        for (int i = 0; i < categoryList.Count; i++)
        {
            if (currentParentID != categoryList[i].ParentCategoryID)
            {
                tmpFullPath = categoryList[i].CreateFullCategoryPathParentOnly();
                currentParentID = categoryList[i].ParentCategoryID;
            }
            DataRow row;
            row = result.NewRow();
            row["CategoryID"] = categoryList[i].CategoryID;
            row["Name"] = tmpFullPath + categoryList[i].Name;
            result.Rows.Add( row );
        }
        return result;
    }

    private void PopulateCategoryList()
    {
        DataTable dataSource = CreateDataSource();
        uxGridCategory.DataSource = null;
        uxGridCategory.DataSource = dataSource;
        uxGridCategory.DataBind();
    }

    private void LoadGoogleMainCategory()
    {
        uxGoogleProductCategoryDrop.Items.Clear();
        string filePath = Server.MapPath( filePathGoogleCategory + fileNameGoogleCategory );
        string[] categoriesList = File.ReadAllLines( filePath );
        foreach (string category in categoriesList)
        {
            uxGoogleProductCategoryDrop.Items.Add( new ListItem( category, category ) );
        }
    }

    private void LoadGoogleSubCategory()
    {
        uxGoogleSubCategoryDrop.Items.Clear();
        string filePath = Server.MapPath( filePathGoogleCategory + uxGoogleProductCategoryDrop.SelectedValue + ".txt" );
        string[] categoriesList = File.ReadAllLines( filePath );
        foreach (string category in categoriesList)
        {
            uxGoogleSubCategoryDrop.Items.Add( new ListItem( category, category ) );
        }
    }

    public void PopulateControl()
    {
        DataTable dataSource = CreateDataSource();
        uxGridCategory.DataSource = dataSource;
        uxGridCategory.DataBind();
        InitShippingMethod();
        if (!MainContext.IsPostBack)
            InitGoogleCategoryDropdown();

        if (!KeyUtilities.IsMultistoreLicense())
            uxStoreFilterPanel.Visible = false;
    }

    public void InitShippingMethod()
    {
        uxShippingMethodDrop.Items.Clear();
        IList<ShippingOption> shippingList = DataAccessContext.ShippingOptionRepository.GetShipping(
            Culture, BoolFilter.ShowFalse );

        for (int i = 0; i < shippingList.Count; i++)
        {
            uxShippingMethodDrop.Items.Add(
                new ListItem( shippingList[i].ShippingName, shippingList[i].ShippingID ) );
        }
    }

    public void InitGoogleCategoryDropdown()
    {
        uxGoogleProductCategoryCheck.Checked = false;
        uxMainCategoryDiv.Visible = false;

        LoadGoogleMainCategory();
    }

    protected void uxGoogleProductCategoryCheck_CheckedChanged( object sender, EventArgs e )
    {
        uxMainCategoryDiv.Visible = uxGoogleProductCategoryCheck.Checked;
        uxSubCategoryDiv.Visible = uxGoogleProductCategoryCheck.Checked;

        if (uxMainCategoryDiv.Visible)
        {
            LoadGoogleMainCategory();
            LoadGoogleSubCategory();
        }
    }

    protected void uxGoogleProductCategoryDrop_SelectIndexChanged( object sender, EventArgs e )
    {
        uxSubCategoryDiv.Visible = true;

        LoadGoogleSubCategory();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxDirectoryLabel.Text = _directory;
        uxFileNameText.ReadOnly = _notRenameFileName;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControl();
    }

    public void SetFileName( string fileName, string cultureID )
    {
        uxFileNameText.Text = fileName;
        CultureID = cultureID;
        if (fileName.ToLower() == "googlexmlfile")
        {
            uxDataFeedTitlePanel.Visible = true;
            uxDataFeedDescriptionPanel.Visible = true;
            uxDataFeedTitleText.Text = DataAccessContext.Configurations.GetValue( CultureID, "SiteName" );
            uxDataFeedDescriptionText.Text = DataAccessContext.Configurations.GetValue( CultureID, "SiteDescription" );
        }
        else
        {
            uxDataFeedTitlePanel.Visible = false;
            uxDataFeedDescriptionPanel.Visible = false;
            uxDataFeedTitleText.Text = String.Empty;
            uxDataFeedDescriptionText.Text = String.Empty;
        }
        //PopulateControl();
    }

    public void SetFileExtension( string fileExtension )
    {
        uxFileExtensionLabel.Text = fileExtension;
    }

    public void SetInvisibleMedium()
    {
        uxMediumTR.Visible = false;
    }

    public void SetInvisibleShippingMethod()
    {
        uxShippingMethodTR.Visible = false;
    }

    public void SetInvisibleProductCondition()
    {
        uxProductConditionTR.Visible = false;
    }

    public void SetVisibleGoogleProductConditionDropDown()
    {
        uxProductConditionTR.Visible = false;
        uxGoogleProductConditionTR.Visible = true;
    }

    public void SetInvisibleStockDescription()
    {
        uxStockDescriptionTR.Visible = false;
    }

    public void NotRenameFilename()
    {
        _notRenameFileName = true;
    }

    public void SetVisibleGoogleProductCategory()
    {
        uxGoogleProductCategoryPanel.Visible = true;
    }

    public ArrayList GetSelectedCategory()
    {
        ArrayList items = new ArrayList();

        foreach (GridViewRow row in uxGridCategory.Rows)
        {
            CheckBox selecteCheck = (CheckBox) row.FindControl( "uxCheck" );
            if (selecteCheck.Checked)
            {
                string id = ((HiddenField) row.FindControl( "uxCategoryIDHidden" )).Value.Trim();
                items.Add( id );
            }
        }
        return items;
    }

    public string GetFileName()
    {
        return uxFileNameText.Text.Trim();
    }

    public string GetDirectory()
    {
        return _directory;
    }

    public string GetShippingMethod()
    {
        return uxShippingMethodDrop.SelectedValue;
    }

    public string GetMedium()
    {
        return uxMediumDrop.SelectedValue;
    }

    public bool GetIncludeOutOfStock()
    {
        return uxOutOfStockCheck.Checked;
    }

    public string GetProductCondition()
    {
        return uxProductConditionDrop.SelectedValue;
    }

    public string GetGoogleProductCondition()
    {
        return uxGoogleProductConditionDrop.SelectedValue;
    }

    public string GetStockDescription()
    {
        return uxStockDescriptionText.Text;
    }

    public string GetDataFeedTitle()
    {
        return uxDataFeedTitleText.Text;
    }

    public string GetDataFeedDescription()
    {
        return uxDataFeedDescriptionText.Text;
    }

    public Store GetCurrentStore()
    {
        if (!KeyUtilities.IsMultistoreLicense())
            return DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID );
        else
            return DataAccessContext.StoreRepository.GetOne( uxStoreFilterDrop.SelectedValue );
    }

    public string GetGoogleCategory()
    {
        return uxGoogleSubCategoryDrop.SelectedValue.ToString();
    }

    public bool GetIsUseGoogleCategory()
    {
        return uxGoogleProductCategoryCheck.Checked;
    }

    public string GetProductBreadcrumb( string categoryID )
    {
        string currentParentID = "";
        string tmpFullPath = "";

        Category category = DataAccessContext.CategoryRepository.GetOne( Culture, categoryID );

        if (currentParentID != category.ParentCategoryID)
        {
            tmpFullPath = category.CreateFullCategoryPathParentOnly();
            currentParentID = category.ParentCategoryID;
        }

        return (tmpFullPath + category.Name).Replace( ">>", ">" );
    }

    public string GetGoogleCountryCode()
    {
        return uxGoogleCountryDrop.SelectedValue;
    }
}
