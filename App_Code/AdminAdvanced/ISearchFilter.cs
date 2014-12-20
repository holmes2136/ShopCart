using System;
using System.Data;
using System.Configuration;

namespace Vevo
{
    /// <summary>
    /// Summary description for ISearchFilter
    /// </summary>
    public interface ISearchFilter : IAdvancedPostbackControl
    {
        void ClearFilter();
    }
}
