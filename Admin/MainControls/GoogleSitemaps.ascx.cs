using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using Vevo.WebUI;
using Vevo.Domain.Products;
using Vevo.Domain.Contents;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.Domain.Stores;
using Vevo.Domain.DataInterfaces;
using Vevo.Shared.Utilities;
using Vevo.Base.Domain;

public partial class AdminAdvanced_MainControls_GoogleSitemaps : AdminAdvancedBaseUserControl
{
    #region Private

    private const string _fileName = "GoogleXMLSitemaps";
    private const string _fileExtension = ".xml";
    private string[] _staticContentNameList = new string[] { "AboutUs", "Policy", "LinkToUs", "Affiliate" };

    private Store CurrentStore
    {
        get
        {
            if (!KeyUtilities.IsMultistoreLicense())
                return DataAccessContext.StoreRepository.GetOne( Store.RegularStoreID );
            else
                return DataAccessContext.StoreRepository.GetOne( uxStoreDrop.SelectedValue );
        }
    }

    private void PopulateStoreFilterDropDown()
    {
        uxStoreDrop.Items.Clear();

        IList<Store> list = DataAccessContext.StoreRepository.GetAll( "StoreID" );

        for (int i = 0; i < list.Count; i++)
        {
            string name = list[i].StoreName;
            name += " (" + list[i].StoreID + ")";

            uxStoreDrop.Items.Add( new System.Web.UI.WebControls.ListItem( name, list[i].StoreID ) );
        }
    }

    private void PopulateControls()
    {
        if (!IsAdminModifiable())
        {
            uxSaveSettingButton.Visible = false;
            uxGenerateButton.Visible = false;
        }

        uxSiteMapIncludesCategoriesDropDown.SelectedValue
            = DataAccessContext.Configurations.GetBoolValue( "GoogleSitemapsIsIncludesCategories", CurrentStore ).ToString();
        PopulateCategoryRange();

        uxSiteMapIncludesProductsDropDown.SelectedValue
            = DataAccessContext.Configurations.GetBoolValue( "GoogleSitemapsIsIncludesProducts", CurrentStore ).ToString();
        PopulateProductRange();

        uxSiteMapIncludesContentsDropDown.SelectedValue
            = DataAccessContext.Configurations.GetBoolValue( "GoogleSitemapsIsIncludesContents", CurrentStore ).ToString();
        PopulateContentPagesRange();

        uxDefaultChangeFreqDrop.SelectedValue
            = DataAccessContext.Configurations.GetValue( "GoogleSitemapsChangeFrequency", CurrentStore );
        uxDefaultPriorityText.Text = DataAccessContext.Configurations.GetValue( "GoogleSitemapsURLPriority", CurrentStore );
    }

    private void AddCategories( GoogleXmlSitemaps sitemap )
    {
        int howMayItems;
        sitemap.AddCategories(
            DataAccessContext.CategoryRepository.GetByRootID(
            StoreContext.Culture,
            DataAccessContext.Configurations.GetValue( "RootCategory", CurrentStore ),
            "CategoryID",
            BoolFilter.ShowAll,
            ConvertUtilities.ToInt32( uxCategoryStartText.Text.Trim() ) - 1,
            ConvertUtilities.ToInt32( uxCategoryEndText.Text.Trim() ) - 1,
            out howMayItems ) );

    }

    private void PopulateCategoryRange()
    {
        int all = DataAccessContext.CategoryRepository.GetByRootID(
            StoreContext.Culture,
            DataAccessContext.Configurations.GetValue( "RootCategory", CurrentStore ),
            "CategoryID",
            BoolFilter.ShowAll ).Count;

        uxCategoryStartText.Text = "1";
        uxCategoryEndText.Text = all.ToString();
        uxAllCategoryLabel.Text = all.ToString();

        if (ConvertUtilities.ToBoolean( uxSiteMapIncludesCategoriesDropDown.SelectedValue ))
            uxCategoryRangePanel.Visible = true;
        else
            uxCategoryRangePanel.Visible = false;
    }

    private void PopulateProductRange()
    {
        SearchFilter searchFilterObj = SearchFilter.GetFactory().Create();
        int howMayItems;
        int all = DataAccessContext.ProductRepository.SearchProduct(
                StoreContext.Culture,
                "",
                "ProductID",
                searchFilterObj,
                0,
                DataAccessContext.ProductRepository.GetAllProductCount(),
                out howMayItems,
                CurrentStore.StoreID,
                DataAccessContext.Configurations.GetValue( "RootCategory", CurrentStore ) ).Count;

        uxProductStartText.Text = "1";
        uxProductEndText.Text = all.ToString();
        uxAllProductLabel.Text = all.ToString();

        if (ConvertUtilities.ToBoolean( uxSiteMapIncludesProductsDropDown.SelectedValue ))
            uxProductRangePanel.Visible = true;
        else
            uxProductRangePanel.Visible = false;
    }

    private void AddProducts( GoogleXmlSitemaps sitemap )
    {
        SearchFilter searchFilterObj = SearchFilter.GetFactory().Create();
        int howMayItems;
        sitemap.AddProducts(
            DataAccessContext.ProductRepository.SearchProduct(
                StoreContext.Culture,
                "",
                "ProductID",
                searchFilterObj,
                ConvertUtilities.ToInt32( uxProductStartText.Text.Trim() ) - 1,
                ConvertUtilities.ToInt32( uxProductEndText.Text.Trim() ) - 1,
                out howMayItems,
                CurrentStore.StoreID,
                DataAccessContext.Configurations.GetValue( "RootCategory", CurrentStore ) ) );
    }

    private void PopulateContentPagesRange()
    {
        int all = GetAllContentsInCurrentStore().Count;
        uxContentStartText.Text = "1";
        uxContentEndText.Text = all.ToString();
        uxAllContentLabel.Text = all.ToString();

        if (ConvertUtilities.ToBoolean( uxSiteMapIncludesContentsDropDown.SelectedValue ))
            uxContentRangePanel.Visible = true;
        else
            uxContentRangePanel.Visible = false;
    }

    private IList<Content> GetAllContentsInCurrentStore()
    {
        IList<Content> resultList = new List<Content>();

        foreach (string name in _staticContentNameList)
        {
            Content content = DataAccessContext.ContentRepository.GetOne( StoreContext.Culture,
                DataAccessContext.ContentRepository.GetIDByName( StoreContext.Culture, name ) );

            if (!content.IsNull)
                resultList.Add( content );
        }

        IList<ContentMenuItem> list = DataAccessContext.ContentMenuItemRepository.GetByStoreID(
            StoreContext.Culture, CurrentStore.StoreID, "ContentMenuItemID", BoolFilter.ShowAll );

        foreach (ContentMenuItem item in list)
        {
            if (item.LinksToContent())
            {
                Content newContent = DataAccessContext.ContentRepository.GetOne( StoreContext.Culture, item.ContentID );

                if (!IsExits( resultList, newContent ))
                {
                    resultList.Add( newContent );
                }
            }
        }

        return resultList;
    }

    private void AddContentPages( GoogleXmlSitemaps sitemap )
    {
        IList<Content> allList = GetAllContentsInCurrentStore();

        IList<Content> resultList = new List<Content>();

        int startIndex = ConvertUtilities.ToInt32( uxContentStartText.Text.Trim() ) - 1;
        int endIndex = ConvertUtilities.ToInt32( uxContentEndText.Text.Trim() ) - 1;

        for (int i = startIndex; i <= endIndex && i < allList.Count; i++)
        {
            resultList.Add( allList[i] );
        }

        sitemap.AddContentPages( resultList );
    }

    private bool IsExits( IList<Content> list, Content newContent )
    {
        foreach (Content content in list)
        {
            if (content.ContentID == newContent.ContentID)
                return true;
        }
        return false;
    }

    private XmlWriter CreateXmlWriter( string filePhysicalPathName )
    {
        XmlWriterSettings xmlSetting = new XmlWriterSettings();
        xmlSetting.Encoding = System.Text.Encoding.UTF8;
        xmlSetting.Indent = true;
        return XmlWriter.Create( filePhysicalPathName, xmlSetting );
    }

    private void Generate()
    {
        string saveFileName = "Export/" + (_fileName + _fileExtension);
        string filePhysicalPathName = Server.MapPath( "../" + saveFileName );
        XmlWriter xmlWriter = CreateXmlWriter( filePhysicalPathName );

        GoogleXmlSitemaps sitemap = new GoogleXmlSitemaps();
        sitemap.Changefreq = DataAccessContext.Configurations.GetValue( "GoogleSitemapsChangeFrequency", CurrentStore );
        sitemap.Priority = DataAccessContext.Configurations.GetValue( "GoogleSitemapsURLPriority", CurrentStore );

        if (!KeyUtilities.IsMultistoreLicense())
            sitemap.StorefrontUrl = UrlPath.StorefrontUrl;
        else
            sitemap.StorefrontUrl = "http://" + CurrentStore.UrlName + "/";

        string message = String.Empty;

        if (DataAccessContext.Configurations.GetBoolValue( "GoogleSitemapsIsIncludesCategories", CurrentStore ))
            AddCategories( sitemap );

        if (DataAccessContext.Configurations.GetBoolValue( "GoogleSitemapsIsIncludesProducts", CurrentStore ))
            AddProducts( sitemap );

        if (DataAccessContext.Configurations.GetBoolValue( "GoogleSitemapsIsIncludesContents", CurrentStore ))
            AddContentPages( sitemap );

        if (sitemap.GenerateSitemap( xmlWriter, out message ))
        {
            uxFileNameLink.Text = Path.GetFileName( saveFileName );
            uxFileNameLink.NavigateUrl = "../DownloadFile.aspx?FilePath=../" + saveFileName;
            uxFileNameLink.Target = "_blank";

            uxMessage.DisplayMessage( Resources.GoogleXMLSitemapsMessages.CreateSuccess );
        }
        else
            uxMessage.DisplayMessage( message );
    }

    private void SaveSettingConfiguration()
    {
        try
        {
            DataAccessContext.ConfigurationRepository.UpdateValue(
                 DataAccessContext.Configurations["GoogleSitemapsIsIncludesCategories"],
                 uxSiteMapIncludesCategoriesDropDown.SelectedValue,
                 CurrentStore );
            DataAccessContext.ConfigurationRepository.UpdateValue(
                 DataAccessContext.Configurations["GoogleSitemapsIsIncludesProducts"],
                 uxSiteMapIncludesProductsDropDown.SelectedValue,
                 CurrentStore );
            DataAccessContext.ConfigurationRepository.UpdateValue(
                 DataAccessContext.Configurations["GoogleSitemapsIsIncludesContents"],
                 uxSiteMapIncludesContentsDropDown.SelectedValue,
                 CurrentStore );
            DataAccessContext.ConfigurationRepository.UpdateValue(
                DataAccessContext.Configurations["GoogleSitemapsChangeFrequency"],
                uxDefaultChangeFreqDrop.SelectedValue,
                CurrentStore );
            DataAccessContext.ConfigurationRepository.UpdateValue(
                DataAccessContext.Configurations["GoogleSitemapsURLPriority"],
                uxDefaultPriorityText.Text,
                CurrentStore );

            uxMessage.DisplayMessage( Resources.GoogleXMLSitemapsMessages.UpdateSuccess );
            SystemConfig.Load();
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
    }

    #endregion

    #region Protected

    protected void uxStoreDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        PopulateControls();

    }

    protected void uxSiteMapIncludesCategoriesDropDown_SelectedIndexChanged( object sender, EventArgs e )
    {
        PopulateCategoryRange();
    }

    protected void uxSiteMapIncludesProductsDropDown_SelectedIndexChanged( object sender, EventArgs e )
    {
        PopulateProductRange();
    }

    protected void uxSiteMapIncludesContentsDropDown_SelectedIndexChanged( object sender, EventArgs e )
    {
        PopulateContentPagesRange();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        if (!MainContext.IsPostBack)
        {
            PopulateStoreFilterDropDown();
            PopulateControls();
        }

        if (!KeyUtilities.IsMultistoreLicense())
            uxStoreFilterPanel.Visible = false;
    }

    protected void uxGenerateButton_Click( object sender, EventArgs e )
    {
        SaveSettingConfiguration();

        Generate();
    }

    protected void uxSaveSettingButton_Click( object sender, EventArgs e )
    {
        SaveSettingConfiguration();
    }

    #endregion
}
