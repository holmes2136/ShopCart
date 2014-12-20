using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.WebUI;
using Vevo.WebUI.International;

public partial class Components_StoreBanner : BaseLanguageUserControl
{
    private IList<Banner> bannerList;
    private int maxSizeHeight;
    private int maxSizeWidth;
    private float _bannerRatio = 0;

    private void RegisterJavaScript()
    {
        string effect = DataAccessContext.Configurations.GetValue( "StoreBannerEffectMode", StoreContext.CurrentStore );

        ScriptManager.RegisterClientScriptInclude(
            this, this.GetType(), "JqueryMobile", ResolveClientUrl( "~/ClientScripts/CameraSlideshow/jquery.mobile.customized.min.js" ) );
        ScriptManager.RegisterClientScriptInclude(
            this, this.GetType(), "JqueryEasing", ResolveClientUrl( "~/ClientScripts/CameraSlideshow/jquery.easing.1.3.js" ) );
        ScriptManager.RegisterClientScriptInclude(
            this, this.GetType(), "CameraSlideshow", ResolveClientUrl( "~/ClientScripts/CameraSlideshow/camera.js" ) );

        StringBuilder sb = new StringBuilder();
        sb.AppendLine( "jQuery(function(){" );
        sb.AppendLine( "	jQuery('#" + uxStoreBannerPanel.ClientID + "').camera({" );

        if (bannerList.Count == 1 || effect.Equals( "none" ))
        {
            sb.AppendLine( "		pagination: false," );
            sb.AppendLine( "		autoAdvance: false," );
        }
        else
        {
            sb.AppendLine( "		fx: '" + effect + "'," );
        }

        sb.AppendLine( "		alignment: 'topCenter'," );
        sb.AppendLine( "		loader: 'none'," );
        sb.AppendLine( "		navigation: false," );
        sb.AppendLine( "		navigationHover: false," );
        sb.AppendLine( "		playPause: false," );
        sb.AppendLine( "		thumbnails: false," );
        sb.AppendLine( "		portrait: true," );
        sb.AppendLine( "		height: '" + _bannerRatio.ToString() + "%'," );
        sb.AppendLine( "		time: " + DataAccessContext.Configurations.GetValue( "StoreBannerSlideSpeed", StoreContext.CurrentStore ) + "," );
        sb.AppendLine( "		transPeriod: " + DataAccessContext.Configurations.GetValue( "StoreBannerEffectPeriod", StoreContext.CurrentStore ) + "" );
        sb.AppendLine( "	});" );
        sb.AppendLine( "});" );

        ScriptManager.RegisterClientScriptBlock( this, this.GetType(), "BannerSlideshow", sb.ToString(), true );
    }

    private void AddCssFile()
    {
        HtmlLink cssLink = new HtmlLink();
        cssLink.Href = "~/ClientScripts/CameraSlideshow/camera.css";
        cssLink.Attributes.Add( "rel", "stylesheet" );
        cssLink.Attributes.Add( "type", "text/css" );
        Page.Header.Controls.Add( cssLink );
    }

    private void PopulateControl()
    {
        uxStoreBannerPanel.Controls.Clear();
        maxSizeWidth = 0;
        maxSizeHeight = 0;
        System.Drawing.Image image;

        bannerList = DataAccessContext.BannerRepository.GetAllByStoreID(
            StoreContext.Culture, StoreContext.CurrentStore.StoreID, BoolFilter.ShowTrue, "SortOrder DESC" );

        if (DataAccessContext.Configurations.GetBoolValue( "StoreBannerModuleDisplay" ) && bannerList.Count != 0)
        {
            Panel bannerPanel;
            foreach (Banner banner in bannerList)
            {
                bannerPanel = new Panel();
                bannerPanel.Attributes.Add( "data-src", ResolveClientUrl( "~/" + banner.ImageURL ) );
                bannerPanel.Attributes.Add( "data-link", banner.LinkURL );

                if (!String.IsNullOrEmpty( banner.Description ))
                {
                    Panel panel2 = new Panel();
                    panel2.Attributes.Add( "class", "camera_caption" );

                    Label label = new Label();
                    label.Text = banner.Description;

                    panel2.Controls.Add( label );
                    bannerPanel.Controls.Add( panel2 );
                }

                uxStoreBannerPanel.Controls.AddAt( 0, bannerPanel );

                image = System.Drawing.Image.FromFile( Server.MapPath( "~/" + banner.ImageURL ) );
                if (maxSizeHeight < image.Height)
                {
                    maxSizeHeight = image.Height;
                }

                if (maxSizeWidth < image.Width)
                {
                    maxSizeWidth = image.Width;
                }
            }

            _bannerRatio = (maxSizeHeight * 100 / maxSizeWidth) + 1;
            RegisterJavaScript();
            AddCssFile();
            this.Visible = true;
        }
        else
        {
            this.Visible = false;
        }
    }

    private void Components_StoreBanner_StoreCultureChanged( object sender, CultureEventArgs e )
    {
        PopulateControl();
    }

    protected void Page_Load( object sender, EventArgs e )
    {
        GetStorefrontEvents().StoreCultureChanged +=
            new StorefrontEvents.CultureEventHandler( Components_StoreBanner_StoreCultureChanged );

        PopulateControl();
    }
}
