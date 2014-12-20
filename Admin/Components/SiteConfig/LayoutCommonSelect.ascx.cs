using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo.Domain;
using Vevo.Domain.Stores;
using Vevo.WebAppLib;
using Vevo.WebUI;

public partial class AdminAdvanced_Components_SiteConfig_LayoutCommonSelect : System.Web.UI.UserControl
{
    private string _panelCss, _labelCss, _labelText, _dropdownCss, _imagePanelCss;
    private AjaxControlToolkit.BoxCorners _boxCorners;
    private int _boxRadius;
    private System.Drawing.Color _borderColor;
    private string _configName;
    private Image GetImageFile( string folderPath, string fileName, string emptyFileName, bool showImage )
    {
        string fileNamePath = String.Format( "{0}{1}.gif", folderPath, fileName );
        if (!File.Exists( Server.MapPath( fileNamePath ) ))
        {
            fileNamePath = String.Format( "{0}{1}.gif", folderPath, emptyFileName );
        }
        Image image = new Image();
        image.ImageUrl = fileNamePath;

        if (!showImage)
            image.Style["display"] = "none";

        return image;
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        uxPanel.CssClass = PanelCss;
        uxLabel.Text = LabelText;
        // uxLabel.CssClass = LabelCss;
        uxLabelPanel.CssClass = LabelCss;
        uxImageLabel.Text = "&nbsp;";
        uxImageLabel.CssClass = LabelCss;
        uxItemDrop.CssClass = DropDownCss;
        //uxImagePanel.CssClass = ImagePanelCss;
        //uxImagePanelRoundedCornersExtender.Corners = BoxCorners;
        //uxImagePanelRoundedCornersExtender.Radius = BoxRadius;
        //uxImagePanelRoundedCornersExtender.BorderColor = BoxBorderColor;

        uxHelp.ConfigName = ConfigName;
    }

    public void PopulateFilesControls( string configName, string folderPath )
    {
        string[] fileList = Directory.GetFiles( Server.MapPath( folderPath ) );
        uxItemDrop.Items.Clear();
        uxImagePanel.Controls.Clear();
        string defaultValue = DataAccessContext.Configurations.GetValueNoThrow( configName );
        foreach (string item in fileList)
        {
            string itemName = item.Substring( item.LastIndexOf( "\\" ) + 1 );
            if (itemName.Substring( itemName.LastIndexOf( "." ) + 1 ) == "ascx")
            {
                uxItemDrop.Items.Add( new ListItem( itemName, itemName ) );
                uxImagePanel.Controls.Add(
                    GetImageFile(
                    folderPath,
                    itemName.Substring( 0, itemName.LastIndexOf( "." ) ),
                    "EmptyLayout",
                    defaultValue == itemName ) );
            }
        }
        uxItemDrop.SelectedValue = defaultValue;
        uxItemDrop.Attributes.Add( "onchange", String.Format( "ShowImageInDiv('{0}', this.selectedIndex)", uxImagePanel.ClientID ) );
    }

    public void PopulateFilesControls( string configName, string folderPath, Store store )
    {
        string[] fileList = Directory.GetFiles( Server.MapPath( folderPath ) );
        uxItemDrop.Items.Clear();
        uxImagePanel.Controls.Clear();
        string defaultValue = DataAccessContext.Configurations.GetValueNoThrow( configName, store );
        foreach (string item in fileList)
        {
            string itemName = item.Substring( item.LastIndexOf( "\\" ) + 1 );
            if (itemName.Substring( itemName.LastIndexOf( "." ) + 1 ) == "ascx")
            {
                uxItemDrop.Items.Add( new ListItem( itemName, itemName ) );
                uxImagePanel.Controls.Add(
                    GetImageFile(
                    folderPath,
                    itemName.Substring( 0, itemName.LastIndexOf( "." ) ),
                    "EmptyLayout",
                    defaultValue == itemName ) );
            }
        }
        uxItemDrop.SelectedValue = defaultValue;
        uxItemDrop.Attributes.Add( "onchange", String.Format( "ShowImageInDiv('{0}', this.selectedIndex)", uxImagePanel.ClientID ) );
    }

    public void PopulateDirectoriesControls( string configName, string folderPath )
    {
        PopulateDirectoriesControls( configName, folderPath, Store.Null );
    }

    public void PopulateDirectoriesControls( string configName, string folderPath, Store store )
    {
        string[] directoryList = Directory.GetDirectories( Server.MapPath( folderPath ) );
        uxItemDrop.Items.Clear();
        uxImagePanel.Controls.Clear();
        string defaultValue = DataAccessContext.Configurations.GetValueNoThrow( configName, store );
        foreach (string item in directoryList)
        {
            string itemName = item.Substring( item.LastIndexOf( "\\" ) + 1 );
            if (itemName != ".svn")
            {
                uxItemDrop.Items.Add( new ListItem( itemName ) );
                uxImagePanel.Controls.Add( GetImageFile( folderPath, itemName + "/Thumbnail", "EmptyThumbnail", itemName == defaultValue ) );
            }
        }
        uxItemDrop.Attributes.Add( "onchange", String.Format( "ShowImageInDiv('{0}', this.selectedIndex)", uxImagePanel.ClientID ) );
        uxItemDrop.SelectedValue = defaultValue;
    }

    public void Update( string configName )
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations[configName],
            uxItemDrop.SelectedValue );
    }

    public void Update( string configName, Store store )
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations[configName],
            uxItemDrop.SelectedValue,
            store );
    }

    public String PanelCss
    {
        get { return _panelCss; }
        set { _panelCss = value; }
    }

    public String LabelCss
    {
        get { return _labelCss; }
        set { _labelCss = value; }
    }

    public String LabelText
    {
        get { return _labelText; }
        set { _labelText = value; }
    }

    public String DropDownCss
    {
        get { return _dropdownCss; }
        set { _dropdownCss = value; }
    }

    public String ImagePanelCss
    {
        get { return _imagePanelCss; }
        set { _imagePanelCss = value; }
    }

    public AjaxControlToolkit.BoxCorners BoxCorners
    {
        get { return _boxCorners; }
        set { _boxCorners = value; }
    }

    public Int32 BoxRadius
    {
        get { return _boxRadius; }
        set { _boxRadius = value; }
    }

    public System.Drawing.Color BoxBorderColor
    {
        get { return _borderColor; }
        set { _borderColor = value; }
    }

    public string ConfigName
    {
        get { return _configName; }
        set { _configName = value; }
    }
}
