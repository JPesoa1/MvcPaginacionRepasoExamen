using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcPaginacion.Data;
using MvcPaginacion.Models;
using System.Diagnostics.Metrics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MvcPaginacion.Repository
{

    #region
    //    CREATE PROCEDURE SP_GRUPO_EMPLEADOS_OFICIO
    //(@POSICION INT, @OFICIO NVARCHAR(25))
    //AS

    //    SELECT* FROM
    //    (SELECT CAST(
    //        ROW_NUMBER() OVER (ORDER BY EMP_NO) AS INT) AS POSICION,
    //        EMP_NO, APELLIDO, OFICIO, DIR, FECHA_ALT, SALARIO, DEPT_NO
    //    FROM EMP
    //    WHERE OFICIO = @OFICIO) AS QUERY

    //    WHERE QUERY.POSICION >= @POSICION AND QUERY.POSICION<(@POSICION + 2)
    //GO





    //    ALTER PROCEDURE SP_GRUPO_EMPLEADOS_OFICIO
    //(@POSICION INT, @OFICIO NVARCHAR(50), @SALTO INT
    //, @NUMEROREGISTROS INT OUT)
    //AS
    //    SELECT @NUMEROREGISTROS = COUNT(EMP_NO) FROM
    //    EMP WHERE OFICIO = @OFICIO
    //    SELECT EMP_NO, APELLIDO, OFICIO, SALARIO, DEPT_NO FROM
    //        (SELECT CAST(
    //            ROW_NUMBER() OVER (ORDER BY APELLIDO) AS INT) AS POSICION,
    //            EMP_NO, APELLIDO, OFICIO, SALARIO, DEPT_NO
    //        FROM EMP
    //        WHERE OFICIO = @OFICIO) AS QUERY
    //    WHERE QUERY.POSICION >= @POSICION AND QUERY.POSICION<(@POSICION + @SALTO)
    //GO



    #endregion
    public class RepositoryHospital
    {
        private HospitalContext context;

        public RepositoryHospital(HospitalContext context)
        {
            this.context = context;
        }


        public int GetRegistrosEmpleadosOficio(string oficio)
        {
            return this.context.empleados.Where(x => x.Oficio == oficio).ToList().Count();
        }

        public DatosEmpleados GetEmpleadosOficio(string oficio, int posicion, int salto)
        {
            string sql = "SP_GRUPO_EMPLEADOS_OFICIO @POSICION , @OFICIO, @SALTO, @NUMEROREGISTROS OUT";
            SqlParameter pamposicion = new SqlParameter("@POSICION", posicion);
            SqlParameter pamoficio = new SqlParameter("@OFICIO", oficio);
            SqlParameter pamsalto = new SqlParameter("@SALTO", salto);
            SqlParameter pamnumeros = new SqlParameter("@NUMEROREGISTROS", -1);

            pamnumeros.Direction = System.Data.ParameterDirection.Output;
            var consulta =
                this.context.empleados.FromSqlRaw(sql, pamposicion, pamoficio,pamsalto,pamnumeros);
            List<Empleado> lista = consulta.ToList();
            int registros = (int)pamnumeros.Value;

            return new DatosEmpleados
            {
                NumeroRegistros = registros,
                Empleados = lista
            };


            

        }


        
        public List<string> GetOficios()
        {
            var consulta = (from datos in this.context.empleados select datos.Oficio).Distinct().ToList();

            return consulta;
        }



    }
}
