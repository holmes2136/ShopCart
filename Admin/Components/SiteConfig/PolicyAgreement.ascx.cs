using System;
using System.IO;
using Vevo;
using Vevo.Domain;
using Vevo.WebUI;

public partial class Admin_Components_SiteConfig_PolicyAgreement : System.Web.UI.UserControl ,IConfigUserControl
{
    public void PopulateControls()
    {
        uxPolicyAgreementEnabledDrop.SelectedValue = DataAccessContext.Configurations.GetValue("IsPolicyAgreementEnabled");
        enablePolicyAgreementEditor();
    }

    public void Update()
    {
        DataAccessContext.ConfigurationRepository.UpdateValue(
            DataAccessContext.Configurations["IsPolicyAgreementEnabled"],
            uxPolicyAgreementEnabledDrop.SelectedValue);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void uxPolicyAgreementEnabledDrop_SelectedIndexChanged(object sender, EventArgs e)
    {
        enablePolicyAgreementEditor();
    }

    protected void ChangePage_Click(object sender, EventArgs e)
    {
        String PolicyAgreementText = EmailTemplates.ReadAdminTemplate("PolicyAgreement.txt");
        uxPolicyArgreementText.Text = PolicyAgreementText;
        uxPolicyAgreementEnabledModalPopup.Show();
    }

    protected void uxUpdatePolicyArgreement_Click(object sender, EventArgs e)
    {
        String filePath = Server.MapPath("../ContentTemplates/");
        String PolicyAgreementText = uxPolicyArgreementText.Text;
        using (StreamWriter sw = File.CreateText(filePath + "PolicyAgreement.txt"))
        {
            sw.Write(PolicyAgreementText);
        }
    }

    protected void enablePolicyAgreementEditor()
    {
        if (uxPolicyAgreementEnabledDrop.SelectedValue.Equals("True"))
        {
            uxPolicyAgreementEnabledLink.Visible = true;
        }
        else
        {
            uxPolicyAgreementEnabledLink.Visible = false;
        }
    }

    #region IConfigUserControl Members

    public void Populate(Vevo.Domain.Configurations.Configuration config)
    {
        PopulateControls();
    }

    #endregion
}
