using System;
using System.Data;
using System.Configuration;

namespace Vevo
{
    /// <summary>
    /// Summary description for IDepartmentFilterDrop
    /// </summary>
    public interface IDepartmentFilterDrop : IAdvancedPostbackControl
    {
        string CultureID { get; set; }
    }
}
