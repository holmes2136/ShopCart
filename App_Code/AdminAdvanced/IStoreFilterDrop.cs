using System;
using System.Data;
using System.Configuration;


namespace Vevo
{
    /// <summary>
    /// Summary description for IStoreFilterDrop
    /// </summary>
    public interface IStoreFilterDrop : IAdvancedPostbackControl
    {
        string SelectedValue { get; set; }
    }
}
