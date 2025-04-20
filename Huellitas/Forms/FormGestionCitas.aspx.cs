using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Huellitas.Forms
{
    public partial class FormGestionCitas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCitasDummy();
            }
        }

        private void CargarCitasDummy()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("NombreMascota");
            dt.Columns.Add("Servicio");
            dt.Columns.Add("Fecha");
            dt.Columns.Add("Hora");
            dt.Columns.Add("Telefono");
            dt.Columns.Add("Email");

            dt.Rows.Add("1", "Luna", "Vacunación", "2024-04-20", "10:00", "1234567890", "luna@email.com");
            dt.Rows.Add("2", "Max", "Grooming", "2024-04-21", "11:30", "9876543210", "max@email.com");
            dt.Rows.Add("3", "Bobby", "Chequeo", "2024-04-22", "09:45", "5556667777", "bobby@email.com");

            rptCitas.DataSource = dt;
            rptCitas.DataBind();
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string citaId = btn.CommandArgument;

            // Aquí podrías mostrar un mensaje o log simulado
            System.Diagnostics.Debug.WriteLine($"Cita confirmada: {citaId}");

            // Recargar datos simulados
            CargarCitasDummy();
        }

        protected void btnBuscarFecha_Click(object sender, EventArgs e)
        {
            string fechaSeleccionada = txtBuscarFecha.Text;

            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("NombreMascota");
            dt.Columns.Add("Servicio");
            dt.Columns.Add("Fecha");
            dt.Columns.Add("Hora");
            dt.Columns.Add("Telefono");
            dt.Columns.Add("Email");

            // Simula búsqueda por fecha
            if (fechaSeleccionada == "2024-04-21")
            {
                dt.Rows.Add("2", "Max", "Grooming", "2024-04-21", "11:30", "9876543210", "max@email.com");
            }
            else if (fechaSeleccionada == "2024-04-20")
            {
                dt.Rows.Add("1", "Luna", "Vacunación", "2024-04-20", "10:00", "1234567890", "luna@email.com");
            }
            else if (fechaSeleccionada == "2024-04-22")
            {
                dt.Rows.Add("3", "Bobby", "Chequeo", "2024-04-22", "09:45", "5556667777", "bobby@email.com");
            }

            rptCitas.DataSource = dt;
            rptCitas.DataBind();
        }
    }
}