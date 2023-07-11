using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Introduccion_A_Entity_Framework_Core.DTO_S
{
    public class ActorCreacionDTO
    {
        [StringLength(150)]
        public string Nombre { get; set; } = null!;
        public decimal Fortuna { get; set; }
        public DateTime FechaNacimiento { get; set; }
       
    }
}
