using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using MvcPaginacion.Models;
using MvcPaginacion.Repository;

namespace MvcPaginacion.Controllers
{
    public class PaginacionController : Controller
    {

        private RepositoryHospital repo;

        public PaginacionController(RepositoryHospital repo)
        {
            this.repo = repo;
        }

        public IActionResult Index(int? posicion , string? oficio , int? salto)
        {
            if (posicion == null)
            {
                ViewData["REGISTROS"] = 0;
                ViewData["OFICIO"] = oficio;
                ViewData["SALTO"] = 0;
                TempData["OFICIO2"] = oficio;
                return View();
            }
            else {

                DatosEmpleados datos = new DatosEmpleados();
                datos = this.repo.GetEmpleadosOficio(oficio, posicion.Value,salto.Value);
                ViewData["REGISTROS"] = datos.NumeroRegistros;
                ViewData["OFICIO"] = oficio;
                ViewData["SALTO"] = salto;
                List<Empleado> empleados = datos.Empleados;
                TempData["OFICIO2"] = oficio;
                return View(empleados);
            }
            
            
        }

        [HttpPost]
        public IActionResult Index(int salto ,string? oficio) 
        {
            DatosEmpleados datos = new DatosEmpleados();
            
            datos = this.repo.GetEmpleadosOficio(oficio, 1, salto);
            ViewData["REGISTROS"] = datos.NumeroRegistros;
            ViewData["OFICIO"] = oficio;
            ViewData["SALTO"] = salto;
            List<Empleado> empleados = datos.Empleados;

            return View(empleados);

        }

        
    }
}
