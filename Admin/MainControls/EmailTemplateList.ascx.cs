using System;
using System.Collections;
using System.Collections.Generic;
using Vevo;
using Vevo.Domain;
using Vevo.Domain.EmailTemplates;
using Vevo.Domain.Stores;


public partial class AdminAdvanced_MainControls_EmailTemplateList : AdminAdvancedBaseUserControl
{
    private void PopulateControl()
    {
        EmailTemplateDetail template;

        Culture culture = DataAccessContext.CultureRepository.GetOne(CultureUtilities.DefaultCultureID);

        IList<EmailTemplateDetail> list = DataAccessContext.EmailTemplateDetailRepository.GetAll(
            culture, "EmailTemplateDetailName", new StoreRetriever().GetCurrentStoreID());

        for (int i = 0; i < list.Count; i++)
        {
            template = list[i];

            if (template.EmailTemplateDetailName.Contains("Gift Registry"))
                list.Remove(template);
        }

        uxGrid.DataSource = list;
        uxGrid.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!MainContext.IsPostBack)
            PopulateControl();
    }
}
