//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Huellitas.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vacunacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vacunacion()
        {
            this.HistorialMedicoes = new HashSet<HistorialMedico>();
        }
    
        public int IdVacunacion { get; set; }
        public int IdCita { get; set; }
        public int IdMedicamento { get; set; }
        public Nullable<System.DateTime> FechaAplicacion { get; set; }
        public Nullable<System.DateTime> ProximaDosisRecomendada { get; set; }
        public string Notas { get; set; }
    
        public virtual Cita Cita { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistorialMedico> HistorialMedicoes { get; set; }
        public virtual Medicamento Medicamento { get; set; }
    }
}
