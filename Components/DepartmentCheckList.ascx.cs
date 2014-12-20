using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Vevo;
using Vevo.DataAccessLib.Cart;
using Vevo.Domain;
using Vevo.Shared.DataAccess;
using Vevo.Domain.Products;
using Vevo.Shared.Utilities;
using Vevo.WebUI;

public partial class Components_DepartmentCheckList : Vevo.WebUI.International.BaseLanguageUserControl
{
    //private string _categoryID;
    private bool _isSelectedAll = false;
    private IList<Department> _tableDepartment;

    private IList<Department> TableDepartment
    {
        get
        {
            return _tableDepartment;
        }
        set
        {
            _tableDepartment = value;
        }
    }

    public string CurrentID
    {
        get
        {
            //return _categoryID;
            return ViewState["DepartmentID"].ToString();
        }
        set
        {
            //_categoryID = value;
            ViewState["DepartmentID"] = value;
        }
    }

    public bool IsSeletedAll
    {
        get
        {
            return _isSelectedAll;
        }
        set
        {
            _isSelectedAll = value;
        }
    }

    private IList<Department> GetChildrenOf(string DepartmentID)
    {
        IList<Department> departmentList = new List<Department>();
        foreach (Department department in TableDepartment)
        {
            if (department.ParentDepartmentID == DepartmentID)
                departmentList.Add(department);
        }

        return departmentList;
    }

    private string[] GetChildrenDepartmentIDs(string id)
    {
        IList<Department> departmentList = GetChildrenOf(id);

        string[] categoies = new string[departmentList.Count];

        for (int i = 0; i < categoies.Length; i++)
            categoies[i] = departmentList[i].DepartmentID;
        return categoies;
    }

    private void GetLeafOfID(NameValueCollection leafList, string id)
    {
        string[] children = GetChildrenDepartmentIDs(id);
        if (children.Length == 0)
        {
            if (id != "0")
            {
                Department department = DataAccessContext.DepartmentRepository.GetOne(StoreContext.Culture, id);

                leafList[id] = id + ":" + department.Name;
            }
        }
        else
        {
            foreach (string child in children)
            {
                Department department = DataAccessContext.DepartmentRepository.GetOne(StoreContext.Culture, child);
                leafList[child] = child + ":" + department.Name;
                GetLeafOfID(leafList, child);
            }
        }
    }

    private string[] GetLeafDepartmentIDs(string departmentID)
    {
        NameValueCollection leafList = new NameValueCollection();

        GetLeafOfID(leafList, departmentID);

        string[] result = new string[leafList.Count];
        leafList.CopyTo(result, 0);

        return result;
    }

    private string[] SpiltString(string str)
    {
        return str.Split(':');
    }

    private void SetCheckList()
    {
        uxDepartmentCheckList.Items.Clear();

        TableDepartment = DataAccessContext.DepartmentRepository.GetAll(StoreContext.Culture, "SortOrder", BoolFilter.ShowTrue);

        string[] leafDepartmentIDs = GetLeafDepartmentIDs(CurrentID);

        if (leafDepartmentIDs.Length == 1 && SpiltString(leafDepartmentIDs[0])[0].Equals(CurrentID))
        {
            DepartmentCheckListPanel.Visible = false;
        }
        else
        {
            foreach (string leafDepartmentID in leafDepartmentIDs)
            {
                string[] result = SpiltString(leafDepartmentID);
                uxDepartmentCheckList.Items.Add(new ListItem(result[1], result[0]));
            }
            uxDepartmentCheckList.DataBind();
        }
    }

    public string[] CheckedDepartmentID()
    {
        string[] departments = new string[uxDepartmentCheckList.Items.Count];
        int i = 0;
        foreach (ListItem item in uxDepartmentCheckList.Items)
        {
            if (item.Selected)
            {
                departments[i++] = item.Value;
            }
        }
        string[] result = new string[i];
        for (int j = 0; j < i; j++)
        {
            if (departments[j] != "" && departments[j] != null)
                result[j] = departments[j];
            else
                break;
        }
        return result;
    }

    public void refresh()
    {
        if (ViewState["departmentID"] != null)
            SetCheckList();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            SetCheckList();
    }

}
