using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Vevo;
using Vevo.Deluxe.Domain.EBay;
using Vevo.eBay;

public partial class Admin_Components_EBayCategorySelect : AdminAdvancedBaseUserControl
{
    private string _listSite = "US";

    public string ListSite
    {
        get
        {
            if (uxListSiteHidden != null)
                _listSite = uxListSiteHidden.Value;
            return _listSite;
        }
        set
        {
            uxListSiteHidden.Value = value;
            _listSite = value;
        }
    }

    public string GetSelectedCategoryList()
    {
        string PimaryCategoryNameList = String.Empty;
        if (uxPrimaryCategoryList1Panel.Visible)
        {
            if (uxCategory1RadioList.SelectedItem.Selected)
                PimaryCategoryNameList += uxCategory1RadioList.SelectedItem.Text;
        }
        if (uxPrimaryCategoryList2Panel.Visible)
        {
            if (uxCategory2RadioList.SelectedItem.Selected)
                PimaryCategoryNameList += uxCategory2RadioList.SelectedItem.Text;
        }
        if (uxPrimaryCategoryList3Panel.Visible)
        {
            if (uxCategory3RadioList.SelectedItem.Selected)
                PimaryCategoryNameList += uxCategory3RadioList.SelectedItem.Text;
        }
        if (uxPrimaryCategoryList4Panel.Visible)
        {
            if (uxCategory4RadioList.SelectedItem.Selected)
                PimaryCategoryNameList += uxCategory4RadioList.SelectedItem.Text;
        }
        if (uxPrimaryCategoryList5Panel.Visible)
        {
            if (uxCategory5RadioList.SelectedItem.Selected)
                PimaryCategoryNameList += uxCategory5RadioList.SelectedItem.Text;
        }
        if (uxPrimaryCategoryList6Panel.Visible)
        {
            if (uxCategory6RadioList.SelectedItem.Selected)
                PimaryCategoryNameList += uxCategory6RadioList.SelectedItem.Text;
        }

        return PimaryCategoryNameList;
    }

    public string GetSelectedCategoryID()
    {
        if (uxPrimaryCategoryList6Panel.Visible)
        {
            return uxCategory6RadioList.SelectedValue;
        }
        if (uxPrimaryCategoryList5Panel.Visible)
        {
            return uxCategory5RadioList.SelectedValue;
        }
        if (uxPrimaryCategoryList4Panel.Visible)
        {
            return uxCategory4RadioList.SelectedValue;
        }
        if (uxPrimaryCategoryList3Panel.Visible)
        {
            return uxCategory3RadioList.SelectedValue;
        }
        if (uxPrimaryCategoryList2Panel.Visible)
        {
            return uxCategory2RadioList.SelectedValue;
        }
        return uxCategory1RadioList.SelectedValue;
    }

    public void PopulateControl()
    {
        uxPrimaryCategoryListPanel.Visible = true;
        uxPrimaryCategoryList1Panel.Visible = true;
        EBayAccess eBayAccess = new EBayAccess();
        IList<EBayCategory> eBayCategoryList = eBayAccess.GetCategories( "-1", 0, ListSite );
        uxCategory1RadioList.Items.Clear();
        uxCategory2RadioList.Items.Clear();
        uxCategory3RadioList.Items.Clear();
        uxCategory4RadioList.Items.Clear();
        uxCategory5RadioList.Items.Clear();
        uxCategory6RadioList.Items.Clear();

        foreach (EBayCategory eBayCategory in eBayCategoryList)
        {
            ListItem list = new ListItem();
            if (!eBayCategory.IsExpire && !eBayCategory.IsVirtual)
            {
                if (eBayCategory.IsLeafCategory)
                {
                    list.Text = eBayCategory.PimaryCategoryName;
                }
                else
                {
                    list.Text = eBayCategory.PimaryCategoryName + " >";
                }
                list.Value = eBayCategory.PimaryCategoryID;
                //list.Attributes.Add( "IsLeaf", eBayCategory.IsLeafCategory.ToString() );
                uxCategory1RadioList.Items.Add( list );
            }
        }
    }

    public void ClosePanel()
    {
        uxPrimaryCategoryList1Panel.Visible = false;
        uxPrimaryCategoryList2Panel.Visible = false;
        uxPrimaryCategoryList3Panel.Visible = false;
        uxPrimaryCategoryList4Panel.Visible = false;
        uxPrimaryCategoryList5Panel.Visible = false;
        uxPrimaryCategoryList6Panel.Visible = false;
        uxPrimaryCategoryListPanel.Visible = false;
        uxCategory1RadioList.Items.Clear();
        uxCategory2RadioList.Items.Clear();
        uxCategory3RadioList.Items.Clear();
        uxCategory4RadioList.Items.Clear();
        uxCategory5RadioList.Items.Clear();
        uxCategory6RadioList.Items.Clear();
    }

    protected void Page_Load( object sender, EventArgs e )
    {

    }

    protected void uxCategory1RadioList_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxCategory1RadioList.SelectedItem.Text.Contains( ">" ))
        {
            uxPrimaryCategoryList2Panel.Visible = true;
            string parentPimaryCategoryID = uxCategory1RadioList.SelectedValue.ToString();
            EBayAccess eBayAccess = new EBayAccess();
            IList<EBayCategory> eBayCategoryList = eBayAccess.GetCategories( parentPimaryCategoryID, 1, ListSite );
            uxCategory2RadioList.Items.Clear();
            uxCategory3RadioList.Items.Clear();
            uxCategory4RadioList.Items.Clear();
            uxCategory5RadioList.Items.Clear();
            uxCategory6RadioList.Items.Clear();
            uxPrimaryCategoryList3Panel.Visible = false;
            uxPrimaryCategoryList4Panel.Visible = false;
            uxPrimaryCategoryList5Panel.Visible = false;
            uxPrimaryCategoryList6Panel.Visible = false;

            foreach (EBayCategory eBayCategory in eBayCategoryList)
            {
                ListItem list = new ListItem();
                if (!eBayCategory.IsExpire && !eBayCategory.IsVirtual && parentPimaryCategoryID != eBayCategory.PimaryCategoryID)
                {
                    if (eBayCategory.IsLeafCategory)
                    {
                        list.Text = eBayCategory.PimaryCategoryName;
                    }
                    else
                    {
                        list.Text = eBayCategory.PimaryCategoryName + " >";
                    }
                    list.Value = eBayCategory.PimaryCategoryID;
                    list.Attributes.Add( "IsLeaf", eBayCategory.IsLeafCategory.ToString() );
                    uxCategory2RadioList.Items.Add( list );
                }
            }
        }
        else
        {

        }
    }
    protected void uxCategory2RadioList_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxCategory2RadioList.SelectedItem.Text.Contains( ">" ))
        {
            uxPrimaryCategoryList3Panel.Visible = true;
            string parentPimaryCategoryID = uxCategory2RadioList.SelectedValue.ToString();
            EBayAccess eBayAccess = new EBayAccess();
            IList<EBayCategory> eBayCategoryList = eBayAccess.GetCategories( parentPimaryCategoryID, 2, ListSite );
            uxCategory3RadioList.Items.Clear();
            uxCategory4RadioList.Items.Clear();
            uxCategory5RadioList.Items.Clear();
            uxCategory6RadioList.Items.Clear();
            uxPrimaryCategoryList4Panel.Visible = false;
            uxPrimaryCategoryList5Panel.Visible = false;
            uxPrimaryCategoryList6Panel.Visible = false;

            foreach (EBayCategory eBayCategory in eBayCategoryList)
            {
                ListItem list = new ListItem();
                if (!eBayCategory.IsExpire && !eBayCategory.IsVirtual && parentPimaryCategoryID != eBayCategory.PimaryCategoryID)
                {
                    if (eBayCategory.IsLeafCategory)
                    {
                        list.Text = eBayCategory.PimaryCategoryName;
                    }
                    else
                    {
                        list.Text = eBayCategory.PimaryCategoryName + " >";
                    }
                    list.Value = eBayCategory.PimaryCategoryID;
                    list.Attributes.Add( "IsLeaf", eBayCategory.IsLeafCategory.ToString() );
                    uxCategory3RadioList.Items.Add( list );
                }
            }
        }
        else
        {

        }
    }
    protected void uxCategory3RadioList_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxCategory3RadioList.SelectedItem.Text.Contains( ">" ))
        {
            uxPrimaryCategoryList4Panel.Visible = true;
            string parentPimaryCategoryID = uxCategory3RadioList.SelectedValue.ToString();
            EBayAccess eBayAccess = new EBayAccess();
            IList<EBayCategory> eBayCategoryList = eBayAccess.GetCategories( parentPimaryCategoryID, 3, ListSite );
            uxCategory4RadioList.Items.Clear();
            uxCategory5RadioList.Items.Clear();
            uxCategory6RadioList.Items.Clear();
            uxPrimaryCategoryList5Panel.Visible = false;
            uxPrimaryCategoryList6Panel.Visible = false;

            foreach (EBayCategory eBayCategory in eBayCategoryList)
            {
                ListItem list = new ListItem();
                if (!eBayCategory.IsExpire && !eBayCategory.IsVirtual && parentPimaryCategoryID != eBayCategory.PimaryCategoryID)
                {
                    if (eBayCategory.IsLeafCategory)
                    {
                        list.Text = eBayCategory.PimaryCategoryName;
                    }
                    else
                    {
                        list.Text = eBayCategory.PimaryCategoryName + " >";
                    }
                    list.Value = eBayCategory.PimaryCategoryID;
                    list.Attributes.Add( "IsLeaf", eBayCategory.IsLeafCategory.ToString() );
                    uxCategory4RadioList.Items.Add( list );
                }
            }
        }
        else
        {

        }
    }
    protected void uxCategory4RadioList_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxCategory4RadioList.SelectedItem.Text.Contains( ">" ))
        {
            uxPrimaryCategoryList5Panel.Visible = true;
            string parentPimaryCategoryID = uxCategory4RadioList.SelectedValue.ToString();
            EBayAccess eBayAccess = new EBayAccess();
            IList<EBayCategory> eBayCategoryList = eBayAccess.GetCategories( parentPimaryCategoryID, 4, ListSite );
            uxCategory5RadioList.Items.Clear();
            uxCategory6RadioList.Items.Clear();
            uxPrimaryCategoryList6Panel.Visible = false;

            foreach (EBayCategory eBayCategory in eBayCategoryList)
            {
                ListItem list = new ListItem();
                if (!eBayCategory.IsExpire && !eBayCategory.IsVirtual && parentPimaryCategoryID != eBayCategory.PimaryCategoryID)
                {
                    if (eBayCategory.IsLeafCategory)
                    {
                        list.Text = eBayCategory.PimaryCategoryName;
                    }
                    else
                    {
                        list.Text = eBayCategory.PimaryCategoryName + " >";
                    }
                    list.Value = eBayCategory.PimaryCategoryID;
                    list.Attributes.Add( "IsLeaf", eBayCategory.IsLeafCategory.ToString() );
                    uxCategory5RadioList.Items.Add( list );
                }
            }
        }
        else
        {

        }
    }
    protected void uxCategory5RadioList_SelectedIndexChanged( object sender, EventArgs e )
    {
        if (uxCategory5RadioList.SelectedItem.Text.Contains( ">" ))
        {
            uxPrimaryCategoryList6Panel.Visible = true;
            string parentPimaryCategoryID = uxCategory5RadioList.SelectedValue.ToString();
            EBayAccess eBayAccess = new EBayAccess();
            IList<EBayCategory> eBayCategoryList = eBayAccess.GetCategories( parentPimaryCategoryID, 5, ListSite );
            uxCategory6RadioList.Items.Clear();

            foreach (EBayCategory eBayCategory in eBayCategoryList)
            {
                ListItem list = new ListItem();
                if (!eBayCategory.IsExpire && !eBayCategory.IsVirtual && parentPimaryCategoryID != eBayCategory.PimaryCategoryID)
                {
                    if (eBayCategory.IsLeafCategory)
                    {
                        list.Text = eBayCategory.PimaryCategoryName;
                    }
                    else
                    {
                        list.Text = eBayCategory.PimaryCategoryName + " >";
                    }
                    list.Value = eBayCategory.PimaryCategoryID;
                    list.Attributes.Add( "IsLeaf", eBayCategory.IsLeafCategory.ToString() );
                    uxCategory6RadioList.Items.Add( list );
                }
            }
        }
        else
        {

        }
    }
    protected void uxCategory6RadioList_SelectedIndexChanged( object sender, EventArgs e )
    {
    }

    public bool PanelVisibility
    {
        set { uxPrimaryCategoryListPanel.Visible = value; }
    }
}
