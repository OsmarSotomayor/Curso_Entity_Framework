using System.ComponentModel.DataAnnotations;

namespace Introduccion_A_Entity_Framework_Core.Entidades
{
    public class Genero
    {
        public int Id { get; set; }
        /* public string? Name { get; set; }
         Con el ? estamos diciendo que permitimos valores null 
         */

        //con el igual a null quitamos la advertencia (no diciendo que se permiten valores null)

        //Si deseo por ejeplo configurar el numero maximo de caracteres 
        //por anotacion de datos queda:
        [StringLength(maximumLength: 150)] 
        public string Name { get; set; } = null!;

        //Entidad de navegacion genero-pelicula
        public HashSet<Pelicula> Peliculas { get; set;} = new HashSet<Pelicula>();
    }
}
