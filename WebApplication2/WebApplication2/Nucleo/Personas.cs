using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Nucleo
{
    public class Personas
    {
        //modelo de la tabla personas
        [Key] public int Id { get; set; }
        public string? Cedula { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public bool Activo { get; set; }
    }
}
