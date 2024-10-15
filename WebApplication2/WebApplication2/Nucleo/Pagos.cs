using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Nucleo
{
    public class Pagos
    {
        //modelo de la tabla pagos
        [Key] public int Id { get; set; }
        public int Id_persona { get; set; }
        public bool Estado_pago { get; set; }
        public DateTime Fecha { get; set; }
        public Decimal Total { get; set; }

        //objeto de persona que no se mapea en la bd
        [NotMapped] public Personas? persona { get; set; }

    }
}
