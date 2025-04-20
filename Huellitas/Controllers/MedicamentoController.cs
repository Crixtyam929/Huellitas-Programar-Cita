using Huellitas.Clases;
using Huellitas.Models;
using System.Web.Mvc;

namespace Huellitas.Controllers
    
{
    [Route("")]
    public class MedicamentoController : Controller
    {
        private readonly clsMedicamento _cls;

        public MedicamentoController()
        {
            _cls = new clsMedicamento();
        }

        /// <summary>
        /// Muestra el listado de medicamentos
        /// </summary>
        [Route("medicamentos")]
        public ActionResult Index()
        {
            var lista = _cls.ConsultarTodos();
            return View(lista); // Asegúrate de tener una vista Index.cshtml
        }

        /// <summary>
        /// Muestra el formulario para agregar un nuevo medicamento
        /// </summary>
        [Route("medicamentos/nuevo")]
        public ActionResult Agregar()
        {
            return View(); // Retorna la vista Agregar.cshtml
        }

        /// <summary>
        /// Procesa la creación de un nuevo medicamento
        /// </summary>
        [HttpPost]
        [Route("medicamentos/nuevo")]
        public ActionResult Agregar(Medicamento medicamento)
        {
            if (ModelState.IsValid)
            {
                string mensaje = _cls.Agregar(medicamento);
                TempData["Mensaje"] = mensaje;
                return RedirectToAction("Index");
            }

            return View(medicamento); // Retorna con errores si ModelState no es válido
        }

        /// <summary>
        /// Elimina un medicamento por ID
        /// </summary>
        [HttpPost]
        [Route("medicamentos/eliminar/id")]
        public ActionResult Eliminar(int id)
        {
            string mensaje = _cls.Eliminar(id);
            TempData["Mensaje"] = mensaje;
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Muestra detalles de un medicamento (opcional)
        /// </summary>
        [Route("medicamentos/detalles/id")]
        public ActionResult Detalles(int id)
        {
            var medicamento = _cls.Consultar(id);
            if (medicamento == null)
            {
                return HttpNotFound();
            }
            return View(medicamento); // Necesitas una vista Detalles.cshtml si decides usar esto
        }
    }

}
