using Microsoft.EntityFrameworkCore;
using MvcPaginacion.Models;

namespace MvcPaginacion.Data
{
    public class HospitalContext : DbContext
    {
        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options)
        {
        }

        public DbSet<Empleado> empleados { get; set; }

    }
}
