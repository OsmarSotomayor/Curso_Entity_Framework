using System.ComponentModel.DataAnnotations;

namespace Introduccion_A_Entity_Framework_Core.DTO_S
{
    public class GeneroCreacionDTO
    {
        [StringLength(maximumLength: 150)]
        public string Name { get; set; } = null!;
    }
}
