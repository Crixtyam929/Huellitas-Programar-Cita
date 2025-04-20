using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Huellitas.Forms
{
    public partial class FormAgendarCita : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAgendar_Click(object sender, EventArgs e)
        {
            lblError.Text = ""; // Limpiar errores

            string nombre = txtNombreMascota.Text.Trim();
            string fecha = txtFecha.Text.Trim();
            string hora = txtHora.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string email = txtEmail.Text.Trim();
            string tipoCita = ddlTipoCita.SelectedValue;

            // Validar nombre
            if (string.IsNullOrWhiteSpace(nombre) || nombre.Length > 24 || !System.Text.RegularExpressions.Regex.IsMatch(nombre, @"^[a-zA-Z\s]+$"))
            {
                lblError.Text = "Ingrese un nombre válido (solo letras, máximo 24 caracteres).";
                return;
            }

            // Validar teléfono
            if (!System.Text.RegularExpressions.Regex.IsMatch(telefono, @"^\d{10}$"))
            {
                lblError.Text = "Ingrese un número de teléfono válido (10 dígitos).";
                return;
            }

            // Validar email
            if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                lblError.Text = "Ingrese un correo electrónico válido.";
                return;
            }

            // Validar fecha y hora
            if (string.IsNullOrEmpty(fecha) || string.IsNullOrEmpty(hora))
            {
                lblError.Text = "Debe seleccionar una fecha y una hora.";
                return;
            }

            // Validar fecha actual o futura
            DateTime fechaSeleccionada;
            if (!DateTime.TryParse(fecha, out fechaSeleccionada))
            {
                lblError.Text = "Fecha no válida.";
                return;
            }

            if (fechaSeleccionada.Date < DateTime.Today)
            {
                lblError.Text = "La fecha debe ser hoy o una posterior.";
                return;
            }

            // Validar hora dentro de rango
            TimeSpan horaSeleccionada;
            if (!TimeSpan.TryParse(hora, out horaSeleccionada))
            {
                lblError.Text = "Hora no válida.";
                return;
            }

            TimeSpan horaInicio = new TimeSpan(7, 0, 0);  // 7:00 AM
            TimeSpan horaFin = new TimeSpan(17, 0, 0);    // 5:00 PM

            if (horaSeleccionada < horaInicio || horaSeleccionada > horaFin)
            {
                lblError.Text = "La hora debe estar entre 7:00 AM y 5:00 PM.";
                return;
            }

            // Validar tipo de cita
            if (string.IsNullOrEmpty(tipoCita))
            {
                
                lblError.Text = "Seleccione un tipo de cita válido.";
                return;
            }

            // Si pasa todo:
            lblError.ForeColor = System.Drawing.Color.Green;
            lblError.Text = "✅ ¡Cita agendada correctamente para " + nombre + " el " + fecha + " a las " + hora + "!";
        }
    }
}

