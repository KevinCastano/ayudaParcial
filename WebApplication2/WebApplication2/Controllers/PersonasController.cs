using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Nucleo;

namespace asp_servicios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonasController : ControllerBase
    {
        private static int contadorTotalPersonas = 0;

        [HttpGet("Get")]
        public IEnumerable<Personas> Get()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=DESKTOP-TFNR1V0\\DEV;database=bancoPracticaKevinadasda;uid=sa;pwd=123456789;TrustServerCertificate=true;";
            conexion.Database.Migrate();

            var lista = conexion!.Listar<Personas>();

            contadorTotalPersonas = conexion.Buscar<Personas>(p => p.Id > 0).Count();
            return lista;
        }

        [HttpPost("Post")]
        public IEnumerable<Personas> Post()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=DESKTOP-TFNR1V0\\DEV;database=bancoPracticaKevinadasda;uid=sa;pwd=123456789;TrustServerCertificate=true;";
            conexion.Database.Migrate();

            conexion.Guardar(new Personas()
            {
                Cedula = "1234567",
                Nombre = "Juliana",
                Apellido = "Gonzalez",
                Activo = true,
            });
            conexion.GuardarCambios();
            return conexion.Listar<Personas>();
        }

        [HttpPost("Put")]
        public IEnumerable<Personas> Put()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=DESKTOP-TFNR1V0\\DEV;database=bancoPracticaKevinadasda;uid=sa;pwd=123456789;TrustServerCertificate=true;";
            conexion.Database.Migrate();

            var entidadPrueba = conexion.Buscar<Personas>(Persona => Persona.Id == 1).FirstOrDefault();

            entidadPrueba!.Apellido = "Perez";

            conexion.Modificar<Personas>(entidadPrueba);
            conexion.GuardarCambios();

            return conexion.Listar<Personas>();
        }

        [HttpPost("Delete")]
        public IEnumerable<Personas> Delete()
        {
            var conexion = new Conexion();
            conexion.StringConnection = "server=DESKTOP-TFNR1V0\\DEV;database=bancoPracticaKevinadasda;uid=sa;pwd=123456789;TrustServerCertificate=true;";
            conexion.Database.Migrate();

            var EntidadPrueba = conexion.Buscar<Personas>(b => b.Id == 3).FirstOrDefault();
            conexion.Borrar<Personas>(EntidadPrueba!);
            conexion.GuardarCambios();

            return conexion.Listar<Personas>();
        }
    }


}

