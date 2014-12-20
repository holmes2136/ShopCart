using System.Web;
using System.Web.SessionState;
using Vevo.Domain;
using Vevo.Shared.WebUI;

namespace Vevo
{
    /// <summary>
    /// Summary description for AdminConfig
    /// </summary>
    public static class AdminConfig
    {
        public enum TestMode
        {
            Test,
            Normal
        }

        static AdminConfig()
        {
            ResetToDefault();
        }

        private static HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        public static string UrlFront
        {
            get
            {
                return UrlPath.StorefrontUrl;
            }
        }

        public static int BannerItemsPerPage;
        public static int CategoryItemsPerPage;
        public static int ProductItemsPerPage;
        public static int CustomerItemsPerPage;
        public static int OrderItemsPerPage;
        public static int OptionItemsPerPage;
        public static int CouponItemsPerPage;
        public static int NewsItemsPerPage;
        public static int BlogItemPerPage;
        public static int CultureItemsPerPage;
        public static int CurrencyItemsPerPage;
        public static int ArticleItemsPerPage;
        public static int ContentItemsPerPage;
        public static int CustomerReviewItemsPerPage;
        public static int AdminItemsPerPage;
        public static int NewsletterItemsPerPage;
        public static int ShippingItemPerPage;
        public static int GiftCertificateItemPerPage;
        public static int AffiliatePerPage;
        public static int AffiliateOrderPerPage;
        public static int AffiliatePaymentPerPage;
        public static int AffiliatePayCommissionPerPage;
        public static int AffiliateCommissionPendingPerPage;
        public static int StoreItemPerPage;
        public static int EBayTemplateItemsPerPage;
        public static int SpecificationItemsPerPage;
        public static int GoogleSpecMappingPerPage;
        public static int GoogleOptionMappingPerPage;
        public static int PromotionGroupItemsPerPage;
        public static int PromotionSubGroupItemsPerPage;
        public static int RmaItemsPerPage;
        public static int ProductKitGroupItemsPerPage;
        public static int BlogCommentItemsPerPage;

        public static TestMode CurrentTestMode = TestMode.Normal;

        public static void ResetToDefault()
        {
            BannerItemsPerPage = 20;
            CategoryItemsPerPage = 20;
            ProductItemsPerPage = 20;
            CustomerItemsPerPage = 20;
            OrderItemsPerPage = 20;
            OptionItemsPerPage = 20;
            CouponItemsPerPage = 20;
            NewsItemsPerPage = 20;
            BlogItemPerPage = 20;
            CultureItemsPerPage = 20;
            CurrencyItemsPerPage = 20;
            ArticleItemsPerPage = 20;
            ContentItemsPerPage = 20;
            CustomerReviewItemsPerPage = 20;
            AdminItemsPerPage = 20;
            NewsletterItemsPerPage = 20;
            ShippingItemPerPage = 20;
            GiftCertificateItemPerPage = 20;
            AffiliatePerPage = 20;
            AffiliateOrderPerPage = 20;
            AffiliatePaymentPerPage = 20;
            AffiliatePayCommissionPerPage = 20;
            AffiliateCommissionPendingPerPage = 20;
            StoreItemPerPage = 20;
            EBayTemplateItemsPerPage = 20;
            SpecificationItemsPerPage = 20;
            GoogleSpecMappingPerPage = 20;
            GoogleOptionMappingPerPage = 20;
            PromotionGroupItemsPerPage = 20;
            PromotionSubGroupItemsPerPage = 20;
            RmaItemsPerPage = 20;
            ProductKitGroupItemsPerPage = 20;
            BlogCommentItemsPerPage = 20;
        }

        public static string CurrentCultureName
        {
            get
            {
                if ( Session[ "AdminCulture" ] == null )
                {
                    Session[ "AdminCulture" ] = SystemConst.AdminDefaultCultureName;
                    Session[ "AdminCultureID" ] = null;
                }
                return ( string ) Session[ "AdminCulture" ];
            }
            set
            {
                Session[ "AdminCulture" ] = value;
                Session[ "AdminCultureID" ] = null;
            }
        }

        public static string CurrentCultureID
        {
            get
            {
                if ( Session[ "AdminCultureID" ] == null )
                    Session[ "AdminCultureID" ] = DataAccessContext.CultureRepository.GetIDByName( CurrentCultureName );

                return ( string ) Session[ "AdminCultureID" ];
            }
        }

        public static Culture CurrentCulture
        {
            get
            {
                return DataAccessContext.CultureRepository.GetOne( CurrentCultureID );
            }
        }

        public static string CurrentContentCultureID
        {
            get
            {
                if ( Session[ "AdminContentCultureID" ] == null )
                    return CurrentCultureID;
                else
                    return ( string ) Session[ "AdminContentCultureID" ];
            }
            set
            {
                Session[ "AdminContentCultureID" ] = value;
            }
        }
    }
}