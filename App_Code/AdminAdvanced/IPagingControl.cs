using System;
using System.Data;
using System.Configuration;


namespace Vevo
{
    /// <summary>
    /// Summary description for IPagingControl
    /// </summary>
    public interface IPagingControl : IAdvancedPostbackControl
    {
        int CurrentPage { get; set; }
    }
}
