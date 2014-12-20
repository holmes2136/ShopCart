using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vevo.Domain.Marketing;
using Vevo.Domain;
using Vevo.WebUI;
using Vevo.Deluxe.Domain.BundlePromotion;
using Vevo.Deluxe.Domain;

public partial class Mobile_Components_PromotionList : Vevo.WebUI.Products.BaseProductListControl
{
    private int ItemPerPage = 5;

    private string SortField
    {
        get
        {
            if (ViewState["SortField"] == null)
            {
                if (!IsSearchResult)
                    ViewState["SortField"] = uxSortField.Items[0].Value.ToString();
                else
                    ViewState["SortField"] = uxSortField.Items[1].Value.ToString();
            }

            return (string) ViewState["SortField"];
        }

        set
        {
            ViewState["SortField"] = value;
        }
    }

    private string SortType
    {
        get
        {
            if (ViewState["SortType"] == null)
                ViewState["SortType"] = "ASC";

            return (string) ViewState["SortType"];
        }
        set
        {
            ViewState["SortType"] = value;
        }
    }

    private IList<PromotionGroup> GetPromotionGroupList( int itemsPerPage, string sortBy, out int totalItem )
    {
        return DataAccessContextDeluxe.PromotionGroupRepository.GetPromotionGroupList(
            StoreContext.Culture,
            sortBy,
            StoreContext.CurrentStore.StoreID,
            BoolFilter.ShowTrue,
            (uxMobilePagingControl.CurrentPage - 1) * itemsPerPage,
            (uxMobilePagingControl.CurrentPage * itemsPerPage) - 1,
            out totalItem );
    }

    private void PopulateProductControls()
    {
        Refresh();
    }

    private ScriptManager GetScriptManager()
    {
        return (ScriptManager) Page.Master.FindControl( "uxScriptManager" );
    }

    private void AddHistoryPoint()
    {
        GetScriptManager().AddHistoryPoint( "page", uxMobilePagingControl.CurrentPage.ToString() );
        GetScriptManager().AddHistoryPoint( "sortField", SortField );
        GetScriptManager().AddHistoryPoint( "sortType", SortType );
    }

    protected void uxSortUpLink_Click( object sender, EventArgs e )
    {
        SortType = "DESC";
        AddHistoryPoint();
        Refresh();
    }

    protected void uxSortDownLink_Click( object sender, EventArgs e )
    {
        SortType = "ASC";
        AddHistoryPoint();
        Refresh();
    }

    protected void uxFieldSortDrop_SelectedIndexChanged( object sender, EventArgs e )
    {
        SortField = uxSortField.SelectedValue;
        uxSortValueHidden.Value = uxSortField.SelectedValue;
        AddHistoryPoint();
        Refresh();
    }

    protected void uxMobilePagingControl_BubbleEvent( object sender, EventArgs e )
    {
        AddHistoryPoint();
        Refresh();
    }

    protected void DisplaySortType()
    {
        if (SortType == "ASC")
        {
            uxSortUpLink.Visible = true;
            uxSortDownLink.Visible = false;
        }
        else
        {
            uxSortUpLink.Visible = false;
            uxSortDownLink.Visible = true;
        }
    }

    protected void ScriptManager_Navigate( object sender, HistoryEventArgs e )
    {
        string args;

        if (!string.IsNullOrEmpty( e.State["sortField"] ))
        {
            SortField = e.State["sortField"].ToString();
        }
        else
        {
            SortField = uxSortField.Items[0].Value.ToString();
        }

        if (!string.IsNullOrEmpty( e.State["sortType"] ))
        {
            SortType = e.State["sortType"].ToString();
        }
        else
        {
            SortType = "ASC";
        }

        int totalItems;
        GetPromotionGroupList( ItemPerPage, SortField + " " + SortType, out totalItems );

        uxMobilePagingControl.NumberOfPages = (int) System.Math.Ceiling( (double) totalItems / ItemPerPage );

        if (!string.IsNullOrEmpty( e.State["page"] ))
        {
            args = e.State["page"];
            uxMobilePagingControl.CurrentPage = int.Parse( args );
        }
        else
        {
            uxMobilePagingControl.CurrentPage = 1;
        }

        Refresh();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxMobilePagingControl.BubbleEvent += new EventHandler( uxMobilePagingControl_BubbleEvent );
        GetScriptManager().Navigate += new EventHandler<HistoryEventArgs>( ScriptManager_Navigate );

        uxMobileList.RepeatColumns = DataAccessContext.Configurations.GetIntValue( "NumberOfProductColumn" );
        uxMobileList.RepeatDirection = RepeatDirection.Horizontal;
        uxMobileList.Visible = true;
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateProductControls();
    }

    protected void uxMobileList_OnItemDataBound( object sender, DataListItemEventArgs e )
    {
        int totalItems;
        if (e.Item.ItemIndex == GetPromotionGroupList( ItemPerPage, SortField + " " + SortType, out totalItems ).Count - 1)
            e.Item.BorderStyle = BorderStyle.None;
    }

    public void Refresh()
    {
        DisplaySortType();
        uxSortField.SelectedValue = SortField;

        int totalItems;

        uxMobileList.DataSource = GetPromotionGroupList( ItemPerPage, SortField + " " + SortType, out totalItems );
        uxMobileList.DataBind();

        uxMobilePagingControl.NumberOfPages = (int) System.Math.Ceiling( (double) totalItems / ItemPerPage );
    }

    public string GetImageUrl( object promotionGroupID )
    {
        string imageUrl = "";

        PromotionGroup promotionGroup = DataAccessContextDeluxe.PromotionGroupRepository.GetOne(
            StoreContext.Culture,
            StoreContext.CurrentStore.StoreID,
            (string) promotionGroupID,
            BoolFilter.ShowAll );
        if (String.IsNullOrEmpty( promotionGroup.ImageFile ) || promotionGroup.ImageFile.Equals( "~/" ))
        {
            imageUrl = "~/" + DataAccessContext.Configurations.GetValue( "DefaultImageUrl" );
        }
        else
        {
            imageUrl = "~/" + promotionGroup.ImageFile;
        }

        return imageUrl;
    }

    protected static bool IsCatalogMode()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "IsCatalogMode" ))
            return true;
        else
            return false;
    }

    protected bool IsAuthorizedToViewPrice()
    {
        if (DataAccessContext.Configurations.GetBoolValue( "PriceRequireLogin" ) && !Page.User.Identity.IsAuthenticated)
            return false;

        return true;
    }
}
