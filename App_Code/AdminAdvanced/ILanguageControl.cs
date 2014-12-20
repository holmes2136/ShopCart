using System;
using System.Data;
using System.Configuration;


namespace Vevo
{
    /// <summary>
    /// Summary description for ILanguageControl
    /// </summary>
    public interface ILanguageControl : IAdvancedPostbackControl
    {
        string CurrentCultureID { get; set; }
    }
}