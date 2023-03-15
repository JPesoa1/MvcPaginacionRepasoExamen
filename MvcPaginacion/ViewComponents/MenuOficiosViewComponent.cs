using Microsoft.AspNetCore.Mvc;
using MvcPaginacion.Repository;

namespace MvcPaginacion.ViewComponents
{

    
    public class MenuOficiosViewComponent:ViewComponent
    {
        private RepositoryHospital repo;

        public MenuOficiosViewComponent(RepositoryHospital repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        { 
        
            List<string> oficios = this.repo.GetOficios();
            return View(oficios);
        }
    }
}
