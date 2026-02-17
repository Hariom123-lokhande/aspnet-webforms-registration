using System;

namespace DemoWebCSharp
{
    public partial class Regestration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void rblVaccinated_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlUpload.Visible = (rblVaccinated.SelectedValue == "Yes");
        }
    }
}
