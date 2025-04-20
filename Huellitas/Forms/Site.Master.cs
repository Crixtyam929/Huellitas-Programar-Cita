using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Huellitas.Forms
{
    public partial class Site : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Código opcional para ejecutar cuando se carga la página maestra
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            // Lógica para cerrar sesión o redirigir
            Response.Redirect("~/Login.aspx");
        }
    }
}