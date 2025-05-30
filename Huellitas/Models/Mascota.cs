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
    
    public partial class Mascota
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Mascota()
        {
            this.Citas = new HashSet<Cita>();
            this.HistorialMedicoes = new HashSet<HistorialMedico>();
        }
    
        public int IdMascota { get; set; }
        public string Nombre { get; set; }
        public int IdRaza { get; set; }
        public string Sexo { get; set; }
        public string Color { get; set; }
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public string Microchip { get; set; }
        public string Notas { get; set; }
        public string UsuarioCedula { get; set; }
        public bool Activa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cita> Citas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistorialMedico> HistorialMedicoes { get; set; }
        public virtual Raza Raza { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
