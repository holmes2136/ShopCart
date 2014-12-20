using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebUI;

namespace Vevo
{
    /// <summary>
    /// Summary description for VevoSiteMapProvider
    /// </summary>
    public class VevoSiteMapProvider : StaticSiteMapProvider
    {
        private SiteMapNode _rootNode = null;

        private string _cultureID;
        private string _catalogTitle = "[$Catalog]";
        private string _departmentTitle = "[$Department]";
        private string _manufacturerTitle = "[$Manufacturer]";
        private string _newsTitle = "[$News]";

        private string CreateUniqueProductID( string productID )
        {
            return "P" + productID;
        }

        private string CreateUniqueCategoryID( string categoryID )
        {
            return categoryID;
        }

        private string CreateUniqueDepartmentID( string departmentID )
        {
            return departmentID;
        }
        private string CreateUniqueManufacturerID( string manufacturerID )
        {
            return manufacturerID;
        }
        private string CreateUniqueNewsID(string newsID)
        {
            return newsID;
        }

        private SiteMapNode CreateSiteMapNodeCategory( Category category )
        {
            string url = String.Empty;

            if (UrlManager.IsFacebook())
            {
                url = UrlManager.GetFacebookCategoryUrl( category.CategoryID, category.UrlName );
            }
            else
            {
                url = UrlManager.GetCategoryUrl( category.CategoryID, category.UrlName );
            }

            SiteMapNode node = new SiteMapNode( this,
                CreateUniqueCategoryID( category.CategoryID ), url, category.Name, category.Description, null, null, null, null );

            return node;
        }

        private SiteMapNode CreateSiteMapNodeProduct( string productID, string name, string shortDescription, string urlName, string categoryID )
        {
            string url = String.Empty;

            if (UrlManager.IsFacebook())
            {
                url = UrlManager.GetFacebookProductUrl( productID, urlName );
            }
            else
            {
                url = UrlManager.GetProductUrl( productID, urlName );
            }

            SiteMapNode node = new SiteMapNode( this,
                CreateUniqueProductID( productID ), url, name, shortDescription, null, null, null, null );

            return node;
        }

        private SiteMapNode CreateSiteMapNodeDepartment( Department department )
        {
            string url = UrlManager.GetDepartmentUrl( department.DepartmentID, department.UrlName );

            SiteMapNode node = new SiteMapNode( this, CreateUniqueDepartmentID( department.DepartmentID ),
                url, department.Name, department.Description, null, null, null, null );

            return node;
        }
        private SiteMapNode CreateSiteMapNodeManufacturer( Manufacturer manufacturer )
        {
            string url = UrlManager.GetManufacturerUrl( manufacturer.ManufacturerID, manufacturer.UrlName );
            SiteMapNode node = new SiteMapNode( this, CreateUniqueManufacturerID( manufacturer.ManufacturerID ),
                url, manufacturer.Name, manufacturer.Description, null, null, null, null );
            return node;
        }
        private SiteMapNode CreateSiteMapNodeNews(string newsID, string topic, string shortDescription, string urlName)
        {
            string url = UrlManager.GetNewsUrl(newsID, urlName);

            SiteMapNode node = new SiteMapNode(this,
                CreateUniqueNewsID(newsID), url, topic, shortDescription, null, null, null, null);

            return node;
        }
        private SiteMapNode CreateRootSiteMapNode( string title, string url )
        {
            string id = "0";
            string description = String.Empty;

            SiteMapNode node = new SiteMapNode( this, id, url, title, description, null, null, null, null );

            return node;
        }

        private bool IsNodeExist( string key )
        {
            SiteMapNode result = base.FindSiteMapNodeFromKey( key );
            if (result == null)
                return false;
            else
                return true;
        }

        private void AddListToSitemapInReversedOrder( ArrayList nodes )
        {
            SiteMapNode parentNode = _rootNode;
            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                SiteMapNode node = (SiteMapNode) nodes[i];
                node.ParentNode = parentNode;
                if (!IsNodeExist( node.Key ))
                    AddNode( node );

                parentNode = node;
            }
        }

        private void AddCategoryNodes( string categoryID )
        {
            Category category = DataAccessContext.CategoryRepository.GetOne( StoreContext.Culture, categoryID );
            SiteMapNode categoryNode = CreateSiteMapNodeCategory( category );

            ArrayList nodes = new ArrayList();
            nodes.Add( categoryNode );
            AddCategoryNodesRecursively( ConvertUtilities.ToInt32( category.ParentCategoryID ), nodes );
            AddListToSitemapInReversedOrder( nodes );
        }

        private void AddCategoryNodesRecursively( int parentID, ArrayList nodes )
        {
            Category category = DataAccessContext.CategoryRepository.GetOne( StoreContext.Culture, parentID.ToString() );

            if (parentID != 0 && category.RootID != "0")
            {
                SiteMapNode currentNode = CreateSiteMapNodeCategory( category );
                nodes.Add( currentNode );
                AddCategoryNodesRecursively( ConvertUtilities.ToInt32( category.ParentCategoryID ), nodes );
            }
        }

        private void AddProductNode( string productID, string name, string shortDescription, string urlName, string categoryID )
        {
            SiteMapNode categoryNode = base.FindSiteMapNodeFromKey( CreateUniqueCategoryID( categoryID ) );
            SiteMapNode productNode = CreateSiteMapNodeProduct( productID, name, shortDescription, urlName, categoryID );
            productNode.ParentNode = categoryNode;
            AddNode( productNode );
        }

        private void AddDepartmentNodes( string departmentID )
        {
            Department department = DataAccessContext.DepartmentRepository.GetOne( StoreContext.Culture, departmentID );
            SiteMapNode departmentNode = CreateSiteMapNodeDepartment( department );

            ArrayList nodes = new ArrayList();
            nodes.Add( departmentNode );
            AddDepartmentNodesRecursively( ConvertUtilities.ToInt32( department.ParentDepartmentID ), nodes );
            AddListToSitemapInReversedOrder( nodes );
        }

        private void AddManufacturerNodes( string manufacturerID )
        {
            Manufacturer manufacturer = DataAccessContext.ManufacturerRepository.GetOne( StoreContext.Culture, manufacturerID );
            SiteMapNode departmentNode = CreateSiteMapNodeManufacturer( manufacturer );
            ArrayList nodes = new ArrayList();
            nodes.Add( departmentNode );
            AddListToSitemapInReversedOrder( nodes );
        }
        private void AddDepartmentNodesRecursively( int parentID, ArrayList nodes )
        {
            Department department = DataAccessContext.DepartmentRepository.GetOne( StoreContext.Culture, parentID.ToString() );

            if (parentID != 0 && department.RootID != "0")
            {
                SiteMapNode currentNode = CreateSiteMapNodeDepartment( department );
                nodes.Add( currentNode );
                AddDepartmentNodesRecursively( ConvertUtilities.ToInt32( department.ParentDepartmentID ), nodes );
            }
        }
        private void AddNewsNode(string newsID, string topic, string shortDescription, string urlName)
        {
            //SiteMapNode newsRootNode = base.FindSiteMapNodeFromKey(CreateUniqueNewsID(newsID));
            SiteMapNode newsNode = CreateSiteMapNodeNews(newsID, topic, shortDescription, urlName);

            ArrayList nodes = new ArrayList();
            nodes.Add(newsNode);
            //AddCategoryNodesRecursively(ConvertUtilities.ToInt32(category.ParentCategoryID), nodes);
            AddListToSitemapInReversedOrder(nodes);
            //AddNode(newsNode);
        }
        protected override void Clear()
        {
            base.Clear();
            _rootNode = null;
        }


        public VevoSiteMapProvider()
        {
            _cultureID = CultureUtilities.StoreCultureID;
        }

        public VevoSiteMapProvider( string cultureID )
        {
            _cultureID = cultureID;
        }


        public override void Initialize( string name, NameValueCollection config )
        {
            // Verify that config isn't null
            if (config == null)
                throw new ArgumentNullException( "config" );

            // Assign the provider a default name if it doesn't have one
            if (String.IsNullOrEmpty( name ))
                name = "VevoSiteMapProvider" + _cultureID;

            // Add a default "description" attribute to config if the
            // attribute doesn’t exist or is empty
            if (string.IsNullOrEmpty( config["description"] ))
            {
                config.Remove( "description" );
                config.Add( "description", "Vevo site map provider" );
            }

            base.Initialize( name, config );
        }

        public override SiteMapNode BuildSiteMap()
        {
            return _rootNode;
        }

        public override SiteMapNode RootNode
        {
            get
            {
                return _rootNode;
            }
        }

        protected override SiteMapNode GetRootNodeCore()
        {
            return _rootNode;
        }

        public void StackCategory( string categoryID )
        {
            if (_rootNode == null || _rootNode.Title == _departmentTitle || _rootNode.Title == _newsTitle || _rootNode.Title == _manufacturerTitle)
            {
                Clear();

                string title = String.Empty;
                string url = String.Empty;

                if (UrlManager.IsFacebook())
                {
                    title = "[$Catalog]";
                    url = "~/" + UrlManager.FacebookFolder + "/Default.aspx?Index=3";
                }
                else
                {
                    title = "[$Catalog]";
                    url = "~/Default.aspx";
                }
                _rootNode = CreateRootSiteMapNode( title, url );
                AddNode( _rootNode );
            }

            if (!IsNodeExist( CreateUniqueCategoryID( categoryID ) ))
            {
                AddCategoryNodes( categoryID );
            }

        }

        public void StackProduct( string productID, string name, string shortDescription, string urlName, string categoryID )
        {
            if (!IsNodeExist( CreateUniqueProductID( productID ) ) && !String.IsNullOrEmpty( categoryID ))
            {
                StackCategory( categoryID );
                AddProductNode( productID, name, shortDescription, urlName, categoryID );
            }
        }

        public void StackDepartment( string departmentID )
        {
            if (_rootNode == null || _rootNode.Title == _catalogTitle || _rootNode.Title == _newsTitle || _rootNode.Title == _manufacturerTitle)
            {
                Clear();

                string title = "[$Department]";
                string url = "~/Default.aspx";
                _rootNode = CreateRootSiteMapNode( title, url );
                AddNode( _rootNode );
            }

            if (!IsNodeExist( CreateUniqueDepartmentID( departmentID ) ))
            {
                AddDepartmentNodes( departmentID );
            }
        }
        public void StackManufacturer( string manufacturerID )
        {
            if (_rootNode == null || _rootNode.Title == _catalogTitle || _rootNode.Title == _departmentTitle || _rootNode.Title == _newsTitle)
            {
                Clear();
                string title = "[$Manufacturer]";
                string url = "~/Default.aspx";
                _rootNode = CreateRootSiteMapNode( title, url );
                AddNode( _rootNode );
            }
            if (!IsNodeExist( CreateUniqueManufacturerID( manufacturerID ) ))
            {
                AddManufacturerNodes( manufacturerID );
            }
        }
        public void StackNews(string newsID, string topic, string shortDescription, string urlName)
        {
            if (_rootNode == null || _rootNode.Title == _departmentTitle || _rootNode.Title == _catalogTitle || _rootNode.Title == _manufacturerTitle)
            {
                Clear();

                _rootNode = CreateRootSiteMapNode(_newsTitle, "~/News.aspx");
                AddNode(_rootNode);
            }
            if (!IsNodeExist(CreateUniqueNewsID(newsID)))
            {
                AddNewsNode(newsID, topic, shortDescription, urlName);
            }
        }
    }
}
