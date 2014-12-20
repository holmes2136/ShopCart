using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Domain.DataInterfaces;
using Vevo.Shared.Utilities;
using Vevo.WebAppLib;
using Vevo.Base.Domain;

public partial class AdminAdvanced_MainControls_GiftCertificateList : AdminAdvancedBaseListControl
{
    private void SetUpSearchFilter()
    {
        IList<TableSchemaItem> list = DataAccessContext.GiftCertificateRepository.GetTableSchema();
        uxSearchFilter.SetUpSchema( list );
    }

    private void PopulateControls()
    {
        if (!MainContext.IsPostBack)
        {
            RefreshGrid();
        }

        if (uxGridGiftCertificate.Rows.Count > 0)
        {
            uxPagingControl.Visible = true;
        }
        else
        {
            uxPagingControl.Visible = false;
        }
    }

    private void SetUpGridSupportControls()
    {
        if (!MainContext.IsPostBack)
        {
            uxPagingControl.ItemsPerPages = AdminConfig.GiftCertificateItemPerPage;
            SetUpSearchFilter();
        }

        RegisterGridView( uxGridGiftCertificate, "GiftCertificateCode" );

        RegisterSearchFilter( uxSearchFilter );
        RegisterPagingControl( uxPagingControl );

        uxSearchFilter.BubbleEvent += new EventHandler( uxSearchFilter_BubbleEvent );
        uxPagingControl.BubbleEvent += new EventHandler( uxPagingControl_BubbleEvent );
    }


    protected void Page_Load( object sender, EventArgs e )
    {
        SetUpGridSupportControls();
    }

    protected void Page_PreRender( object sender, EventArgs e )
    {
        PopulateControls();
    }

    protected string GetExpireDate( string isExpirable, object expireDate )
    {
        if (!ConvertUtilities.ToBoolean( isExpirable ))
            return "-";
        else
        {
            return String.Format( "{0:dd} {0:MMM} {0:yyyy}", (DateTime) expireDate );
        }
    }

    protected string GetUpper( object giftCertificateCode )
    {
        return giftCertificateCode.ToString().ToUpper();
    }

    protected override void RefreshGrid()
    {
        int totalItems;
        uxGridGiftCertificate.DataSource = DataAccessContext.GiftCertificateRepository.SearchGiftCertificate(
            GridHelper.GetFullSortText(),
            uxSearchFilter.SearchFilterObj,
            uxPagingControl.StartIndex,
            uxPagingControl.EndIndex,
            out totalItems );

        uxPagingControl.NumberOfPages = (int) Math.Ceiling( (double) totalItems / uxPagingControl.ItemsPerPages );

        uxGridGiftCertificate.DataBind();
    }

}
