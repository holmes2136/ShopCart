using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.Marketing;
using Vevo.Domain.Users;
using Vevo.Shared.Utilities;
using Vevo.Shared.WebUI;
using Vevo.WebAppLib;
using Vevo.WebUI;
using Vevo.Deluxe.Domain;
using Vevo.Deluxe.Domain.DataInterfaces;
using Vevo.Deluxe.Domain.Marketing;

namespace Vevo
{
    /// <summary>
    /// Summary description for UserUtilities
    /// </summary>
    public static class UserUtilities
    {
        private const int IVLength = 16;

        private static string GetTemplateEmailFromFile( string fileName )
        {
            StreamReader vevoReadText;
            vevoReadText = File.OpenText( HttpContext.Current.Server.MapPath( "~/ContentTemplates/" ) + fileName );
            string result = vevoReadText.ReadToEnd();
            vevoReadText.Close();
            return result;
        }

        private static string EncryptUserName( string userName )
        {
            string originalKey = ConfigurationManager.AppSettings["SecretKey"];
            byte[] Key = Encoding.Unicode.GetBytes( originalKey );
            return SymmetricEncryption.Encrypt( userName, originalKey );
        }

        private static string DecryptUserName( string userNameHash )
        {
            string originalKey = ConfigurationManager.AppSettings["SecretKey"];
            byte[] Key = Encoding.Unicode.GetBytes( originalKey );
            return SymmetricEncryption.Decrypt( userNameHash, originalKey );
        }

        public static void ResetPasswordAndSendEmail( string userNameHash )
        {
            string userName;

            userName = DecryptUserName( userNameHash );

            MembershipUser user = Membership.GetUser( userName );
            if (user != null)
            {
                string newPassword = user.ResetPassword();

                string email;
                if (Roles.IsUserInRole( userName, "Administrators" ))
                {
                    string adminID = DataAccessContext.AdminRepository.GetIDFromUserName( userName );
                    Admin admin = DataAccessContext.AdminRepository.GetOne( adminID );
                    email = admin.Email;
                }
                else if (Roles.IsUserInRole( userName, "Affiliates" ))
                {
                    string affiliateCode = DataAccessContextDeluxe.AffiliateRepository.GetCodeFromUserName( userName );
                    Affiliate affiliate = DataAccessContextDeluxe.AffiliateRepository.GetOne( affiliateCode );
                    email = affiliate.Email;
                }
                else
                {
                    Customer customer = DataAccessContext.CustomerRepository.GetOne(
                        DataAccessContext.CustomerRepository.GetIDFromUserName( userName ) );
                    email = customer.Email;
                }

                string subject;
                string body;

                EmailTemplateTextVariable.ReplaceResetPasswordText( userName, newPassword, out subject, out body );

                WebUtilities.SendHtmlMail(
                    DataAccessContext.Configurations.GetValue( "CompanyEmail" ),
                    email,
                    subject,
                    body );
            }
            else
            {
                throw new VevoException( "Unknown user: " + userName + "." );
            }
        }

        public static void SendResetPasswordConfirmationEmail( string userName, string URL )
        {
            MembershipUser user = Membership.GetUser( userName );
            if (user != null)
            {
                user.UnlockUser();

                string email;

                if (Roles.IsUserInRole( userName, "Administrators" ))
                {
                    string adminID = DataAccessContext.AdminRepository.GetIDFromUserName( userName );
                    Admin admin = DataAccessContext.AdminRepository.GetOne( adminID );
                    email = admin.Email;
                }
                else if (Roles.IsUserInRole( userName, "Affiliates" ))
                {
                    string affiliateCode = DataAccessContextDeluxe.AffiliateRepository.GetCodeFromUserName( userName );
                    Affiliate affiliate = DataAccessContextDeluxe.AffiliateRepository.GetOne( affiliateCode );
                    email = affiliate.Email;
                }
                else
                {
                    Customer customer = DataAccessContext.CustomerRepository.GetOne(
                        DataAccessContext.CustomerRepository.GetIDFromUserName( userName ) );
                    email = customer.Email;
                    URL = UrlPath.StorefrontUrl;
                }

                string resetPasswordLink = URL + "ResetPassword.aspx?username=" +
                    HttpContext.Current.Server.UrlEncode( EncryptUserName( userName ) );
                
                string subject;
                string body;

                EmailTemplateTextVariable.ReplaceConfirmationPasswordText( userName, resetPasswordLink, out subject, out body );

                WebUtilities.SendHtmlMail(
                    DataAccessContext.Configurations.GetValue( "CompanyEmail" ),
                    email,
                    subject,
                    body );
            }
            else
            {
                throw new VevoException( "Unknown user: " + userName + "." );
            }
        }

        public static string GetCurrentCustomerID()
        {
            if (Roles.IsUserInRole( "Customers" ))
                return DataAccessContext.CustomerRepository.GetIDFromUserName( HttpContext.Current.User.Identity.Name );
            else
                return "0";
        }

    }
}
