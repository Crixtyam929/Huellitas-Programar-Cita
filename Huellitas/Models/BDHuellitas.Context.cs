﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DBHuellitasEntities : DbContext
    {
        public DBHuellitasEntities()
            : base("name=DBHuellitasEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cita> Citas { get; set; }
        public virtual DbSet<Empleado> Empleadoes { get; set; }
        public virtual DbSet<EnfermedadPrevia> EnfermedadPrevias { get; set; }
        public virtual DbSet<Especie> Especies { get; set; }
        public virtual DbSet<Factura> Facturas { get; set; }
        public virtual DbSet<FacturaDetalle> FacturaDetalles { get; set; }
        public virtual DbSet<HistorialCambio> HistorialCambios { get; set; }
        public virtual DbSet<HistorialMedico> HistorialMedicoes { get; set; }
        public virtual DbSet<Inventario> Inventarios { get; set; }
        public virtual DbSet<Mascota> Mascotas { get; set; }
        public virtual DbSet<Medicamento> Medicamentoes { get; set; }
        public virtual DbSet<MetodoPago> MetodoPagoes { get; set; }
        public virtual DbSet<Operacion> Operacions { get; set; }
        public virtual DbSet<Raza> Razas { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Vacunacion> Vacunacions { get; set; }
        public virtual DbSet<Veterinaria> Veterinarias { get; set; }
    }
}
