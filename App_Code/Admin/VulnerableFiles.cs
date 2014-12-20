using System;
using System.Data;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Vevo.Domain;

namespace Vevo
{
    /// <summary>
    /// Summary description for VulnerableFiles
    /// </summary>
    public class VulnerableFiles
    {
        private readonly string[] SearchFolders = new string[] {
            "Tools\\",
            "",
            String.Format( "{0}\\", DataAccessContext.Configurations.GetValue( "AdminAdvancedFolder" ) ),
            String.Format( "{0}\\Tools\\", DataAccessContext.Configurations.GetValue( "AdminAdvancedFolder" ))
        };

        private readonly string[] FileNames = new string[] {
            "AdminRecovery.aspx",
            "AdminRecovery.aspx.cs",
            "DatabaseTester.aspx",
            "DatabaseTester.aspx.cs"
        };

        private StringCollection _existedPaths = new StringCollection();
        //private bool _existed;

        public bool Existed
        {
            get
            {
                return _existedPaths.Count > 0;
            }
        }

        public StringCollection ExistedPaths
        {
            get
            {
                return _existedPaths;
            }
        }

        public VulnerableFiles()
        {
            string applicationPath = HttpContext.Current.Server.MapPath( "~/" );

            for (int folderIndex = 0; folderIndex < SearchFolders.Length; folderIndex++)
            {
                for (int fileIndex = 0; fileIndex < FileNames.Length; fileIndex++)
                {
                    string fullPath = applicationPath + SearchFolders[folderIndex] + FileNames[fileIndex];

                    if (File.Exists( fullPath ))
                    {
                        _existedPaths.Add( fullPath );
                    }
                }
            }
        }

    }
}
