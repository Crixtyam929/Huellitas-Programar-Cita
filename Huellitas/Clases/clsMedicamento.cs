using Huellitas.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Huellitas.Clases
{
    public class clsMedicamento
    {
        private DBHuellitasEntities dbHuellitas = new DBHuellitasEntities();
        public Medicamento medicamento { get; set; }

        public List<Medicamento> ConsultarTodos()
        {
            return dbHuellitas.Medicamentoes
                .OrderBy(m => m.Nombre)
                .ToList();
        }

        public Medicamento Consultar(int idMedicamento)
        {
            return dbHuellitas.Medicamentoes.FirstOrDefault(m => m.IdMedicamento == idMedicamento);
        }

        public string Eliminar(int idMedicamento)
        {
            try
            {
                Medicamento medi = Consultar(idMedicamento);
                if (medi == null)
                {
                    return "El Medicamento no existe en nuestra base de datos";
                }
                dbHuellitas.Medicamentoes.Remove(medi);
                dbHuellitas.SaveChanges();
                return "Se eliminó el medicamento: " + medi.Nombre + " en la base de datos.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Agregar(Medicamento nuevoMedicamento)
        {
            try
            {
                dbHuellitas.Medicamentoes.Add(nuevoMedicamento);
                dbHuellitas.SaveChanges();
                return "Medicamento agregado correctamente.";
            }
            catch (Exception ex)
            {
                return "Error al agregar el medicamento: " + ex.Message;
            }
        }
    }
}
