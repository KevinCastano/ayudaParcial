using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Nucleo;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PagosController : ControllerBase
    {
        private static int contadorTotalPersonas = 0;
        PersonasController pController = new PersonasController();
        private int contadorTotalPagos;

        [HttpGet("Get")] //Metodo Get: Mostrar/obtener
        public IEnumerable<Pagos> Get()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=DESKTOP-TFNR1V0\\DEV;database=bancoPracticaKevinadasda;uid=sa;pwd=123456789;TrustServerCertificate=true;";
            conexion.Database.Migrate();

            var lista = conexion!.Listar<Pagos>();

            contadorTotalPersonas = conexion.Buscar<Pagos>(p => p.Id > 0).Count();

            return lista;
        }

        [HttpPost("Post")]
        public IEnumerable<Pagos> Post()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=DESKTOP-TFNR1V0\\DEV;database=bancoPracticaKevinadasda;uid=sa;pwd=123456789;TrustServerCertificate=true;";
            conexion.Database.Migrate();

            conexion.Guardar(new Pagos()
            {
                Id_persona = 1,
                Estado_pago = true,
                Fecha = DateTime.Now,
                Total = 2000000,
            });
            conexion.GuardarCambios();
            return conexion.Listar<Pagos>();
        }

        [HttpPost("Put")]
        public IEnumerable<Pagos> Put()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=DESKTOP-TFNR1V0\\DEV;database=bancoPracticaKevinadasda;uid=sa;pwd=123456789;TrustServerCertificate=true;";
            conexion.Database.Migrate();

            var entidadPrueba = conexion.Buscar<Pagos>(Pago => Pago.Id == 1).FirstOrDefault();

            entidadPrueba!.Estado_pago = false;

            conexion.Modificar<Pagos>(entidadPrueba);
            conexion.GuardarCambios();

            return conexion.Listar<Pagos>();
        }

        [HttpPost("Delete")]
        public IEnumerable<Pagos> Delete()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=DESKTOP-TFNR1V0\\DEV;database=bancoPracticaKevinadasda;uid=sa;pwd=123456789;TrustServerCertificate=true;";
            conexion.Database.Migrate();

            var EntidadPrueba = conexion.Buscar<Pagos>(b => b.Id == 4).FirstOrDefault();
            conexion.Borrar<Pagos>(EntidadPrueba!);
            conexion.GuardarCambios();

            return conexion.Listar<Pagos>();
        }

        //Implementacion metodos para operaciones
        [NonAction] //No aparece en el swagger, pero es posible usarlo dentro de la clase IActionResult
        public int ContarTotalPersonas()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=DESKTOP-TFNR1V0\\DEV;database=bancoPracticaKevinadasda;uid=sa;pwd=123456789;TrustServerCertificate=true;";
            contadorTotalPersonas = conexion.Buscar<Personas>(personas => personas.Id > 0).Count();
            return contadorTotalPersonas;
        }
        [NonAction] //No aparece en el swagger, pero es posible usarlo dentro de la clase IActionResult
        public int ContarTotalPagos()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=DESKTOP-TFNR1V0\\DEV;database=bancoPracticaKevinadasda;uid=sa;pwd=123456789;TrustServerCertificate=true;";
            conexion.Database.Migrate();

            contadorTotalPagos = conexion.Buscar<Pagos>(pagos => pagos.Id > 0).GroupBy(p => p.Id_persona).Count();   // Agrupar por IdPersona o el campo que identifica a la persona

            return contadorTotalPagos;
        }

        [HttpPost("PostPromedioDePersonasQPagaron")]
        public float Promedio()
        {
            if ((float)ContarTotalPersonas() >= 1)
            {
                return (float)ContarTotalPagos() / (float)ContarTotalPersonas();
            }
            return 0;
        }
    }
}
