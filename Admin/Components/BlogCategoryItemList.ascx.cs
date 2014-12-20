using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Vevo.Domain.Blogs;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo;

public partial class Admin_Components_BlogCategoryItemList : System.Web.UI.UserControl
{
    private const int _itemListboxDefault = 8;

    private string CreateScriptMoveItem(
       string fromID,
       string toID,
       string divFromID,
       string divToID,
       string controlIDserialized )
    {
        return String.Format( "return fnMoveItems('{0}','{1}', '{2}', '{3}', {4}, '{5}', '{6}')",
            fromID, toID, divFromID, divToID, _itemListboxDefault, uxBlogCategoryIDHidden.ClientID, controlIDserialized );
    }

    private bool IsAlreadyBlogCategory( DataTable tableBlogCategory, string blogCategoryID )
    {
        DataRow[] rows = tableBlogCategory.Select( "BlogCategoryID = " + blogCategoryID );
        if (rows.Length > 0)
            return true;
        else
            return false;
    }

    private void InitDropDownList( DataTable source )
    {
        uxBlogCategoryFromList.Items.Clear();
        uxBlogCategoryToList.Items.Clear();
        string stringSerialized = String.Empty;

        DataTable currentBlogCategory = source;
        IList<BlogCategory> blogCategoryList;
        blogCategoryList = DataAccessContext.BlogCategoryRepository.GetAll( StoreContext.Culture,BoolFilter.ShowAll,"SortOrder" );

        foreach (BlogCategory blogCategory in blogCategoryList)
        {
            string displayName = blogCategory.Name + " (" + blogCategory.BlogCategoryID + ")";

            ListItem listItem = new ListItem( displayName, blogCategory.BlogCategoryID );

            if (IsAlreadyBlogCategory( currentBlogCategory, blogCategory.BlogCategoryID ))
            {
                uxBlogCategoryToList.Items.Add( listItem );
                if (uxBlogCategoryToList.Items.Count != 1)
                    stringSerialized += ",";
                stringSerialized += listItem.Value;
            }
            else
            {
                uxBlogCategoryFromList.Items.Add( listItem );
            }
        }

        uxBlogCategoryIDHidden.Value = stringSerialized;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      
    }

    protected void uxFromButton_Load( object sender, EventArgs e )
    {
        uxFromButton.Attributes.Add( "onclick", CreateScriptMoveItem(
            uxBlogCategoryFromList.ClientID, uxBlogCategoryToList.ClientID, "uxFromDiv", "uxToDiv", uxBlogCategoryToList.ClientID ) );
    }

    protected void uxToButton_Load( object sender, EventArgs e )
    {
        uxToButton.Attributes.Add( "onclick", CreateScriptMoveItem(
            uxBlogCategoryToList.ClientID, uxBlogCategoryFromList.ClientID, "uxToDiv", "uxFromDiv", uxBlogCategoryToList.ClientID ) );
    }

    public string[] ConvertToBlogCategoryIDs()
    {
        return uxBlogCategoryIDHidden.Value.Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
    }

    public void SetupDropDownList( string blogID, bool restore )
    {
        DataTable table = new DataTable();
        table.Columns.Add( new DataColumn( "BlogID", Type.GetType( "System.Int32" ) ) );
        table.Columns.Add( new DataColumn( "BlogCategoryID", Type.GetType( "System.Int32" ) ) );
        if (restore)
        {
            string[] blogCategoryIDs = uxBlogCategoryIDHidden.Value.Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );
            for (int i = 0; i < blogCategoryIDs.Length; i++)
            {
                DataRow row = table.NewRow();
                row["BlogID"] = blogID;
                row["BlogCategoryID"] = blogCategoryIDs[i];
                table.Rows.Add( row );
            }
        }
        else
        {
            Blog blog = DataAccessContext.BlogRepository.GetOne( blogID );
            for (int i = 0; i < blog.BlogCategoryIDs.Count; i++)
            {
                DataRow row = table.NewRow();
                row["BlogID"] = blogID;
                row["BlogCategoryID"] = blog.BlogCategoryIDs[i];
                table.Rows.Add( row );
            }
        }
        InitDropDownList( table );
    }
}