using System;
using System.Linq;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Products;
using Vevo.WebUI;
using Vevo.Deluxe.Domain;
using System.Collections;

public partial class Admin_Components_PromotionProductSelectedList : AdminAdvancedBaseListControl
{
    private const int ColumnProductID = 1;

    private bool _isDeleteClick = false;

    public Culture CurrentCulture
    {
        get
        {
            if (ViewState["CurrentCulture"] == null)
                return new Culture();
            else
                return (Culture) ViewState["CurrentCulture"];
        }
        set
        {
            ViewState["CurrentCulture"] = value;
        }

    }

    private string CurrentID
    {
        get
        {
            return MainContext.QueryString["PromotionSubGroupID"];
        }
    }

    private void DeleteVisible( bool value )
    {
        uxDeleteButton.Visible = value;
        if (value)
        {
            if (AdminConfig.CurrentTestMode == AdminConfig.TestMode.Normal)
            {
                uxDeleteConfirmButton.TargetControlID = "uxDeleteButton";
                uxConfirmModalPopup.TargetControlID = "uxDeleteButton";
            }
            else
            {
                uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
                uxConfirmModalPopup.TargetControlID = "uxDummyButton";
            }
        }
        else
        {
            uxDeleteConfirmButton.TargetControlID = "uxDummyButton";
            uxConfirmModalPopup.TargetControlID = "uxDummyButton";
        }
    }

    protected void uxAddButton_Click( object sender, EventArgs e )
    {
        OnBubbleEvent( e );
    }

    protected void uxDeleteButton_Click( object sender, EventArgs e )
    {
        try
        {
            _isDeleteClick = true;
            bool deleted = false;
            int delCount = 0;
            ArrayList delIDList = new ArrayList();
            foreach (GridViewRow row in uxPromotionProductGrid.Rows)
            {
                CheckBox deleteCheck = (CheckBox) row.FindControl( "uxCheck" );
                if (deleteCheck.Checked)
                {
                    string id = row.Cells[ColumnProductID].Text.Trim();

                    delIDList.Add( id );
                    delCount++;
                }
            }

            if (delCount >= uxPromotionProductGrid.Rows.Count)
            {
                uxMessage.DisplayError( Resources.PromotionSubGroupMessage.DeleteErrorEmpty );
            }
            else
            {
                foreach (string id in delIDList)
                {
                    DataAccessContextDeluxe.PromotionProductRepository.Delete( CurrentID, id );
                    deleted = true;
                }
                if (deleted)
                {
                    uxMessage.DisplayMessage( Resources.PromotionSubGroupMessage.DeleteSuccess );
                    uxStatusHidden.Value = "Deleted";
                }
            }
        }
        catch (Exception ex)
        {
            uxMessage.DisplayException( ex );
        }
        RefreshGrid();
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected override void RefreshGrid()
    {
        uxPromotionProductGrid.DataSource = DataAccessContextDeluxe.PromotionProductRepository.GetAllByPromotionSubGroupID( "SortOrder", CurrentID );
        uxPromotionProductGrid.DataBind();
        _isDeleteClick = false;
    }

    protected string GetProductName( object productID )
    {
        Product product = DataAccessContext.ProductRepository.GetOne( CurrentCulture, productID.ToString(), StoreContext.CurrentStore.StoreID );

        return product.Name;
    }

    protected string GetOptionName( object productID, object optionItemIDList )
    {
        string displayName = String.Empty;

        Product product = DataAccessContext.ProductRepository.GetOne( CurrentCulture, productID.ToString(), StoreContext.CurrentStore.StoreID );
        if (product.ProductOptionGroups.Count() > 0)
        {
            if (!String.IsNullOrEmpty( optionItemIDList.ToString() ) && optionItemIDList.ToString() != "0")
            {
                string[] optionItemIDs = optionItemIDList.ToString().Split( ',' );
                int optionNumber = 0;
                foreach (string optionItemID in optionItemIDs)
                {
                    OptionItem optionItem = DataAccessContext.OptionItemRepository.GetOne( CurrentCulture, optionItemID );
                    if (optionNumber > 0)
                    {
                        displayName += ", " + optionItem.Name;
                    }
                    else
                        displayName += optionItem.Name;

                    optionNumber++;
                }

                return displayName;
            }
            else
            {
                return "All Options";
            }

        }

        return "None";
    }

    public void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }
        if (uxPromotionProductGrid.Rows.Count > 0)
        {
            DeleteVisible( true );
        }
        else
        {
            DeleteVisible( false );
        }

        if (!IsAdminModifiable())
        {
            DeleteVisible( false );
        }
    }

    public void UpdateLanguage()
    {
        if (!_isDeleteClick)
            RefreshGrid();
    }

    public void AddSuccessMessage()
    {
        uxMessage.DisplayMessage( Resources.PromotionSubGroupMessage.AddSuccess );
    }
}
