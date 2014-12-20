using System;
using System.Data;
using System.Configuration;


namespace Vevo
{
    /// <summary>
    /// Summary description for ICategoryFilterDrop
    /// </summary>
    public interface ICategoryFilterDrop : IAdvancedPostbackControl
    {
        string CultureID { get; set; }
    }
}