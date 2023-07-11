namespace Introduccion_A_Entity_Framework_Core.DTO_S
{
    public class PeliculaCreacionDTO
    {
        public string Titulo { get; set; } = null!;
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }

        /*Generos es una lista de enteros vacia ya que es 
        El id del genero, la llave foranea .0*/
        public List<int> Generos { get; set; } = new List<int>();

        public List<PeliculaActorCreacionDTO> PeliculasActores { get; set; }=
            new List<PeliculaActorCreacionDTO>();

    }
}
