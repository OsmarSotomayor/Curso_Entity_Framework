namespace Introduccion_A_Entity_Framework_Core.Entidades
{
    public class Comentario
    {
        public int Id { get; set; }
        public string? Contenido { get; set; }  //puede ser null
        public bool recomenadar { get; set; }

        //Agrega propiedades de navegacion que va de comentario a pelicula 
        public int PeliculaId { get; set; } //nos dice a que pelicula corresponde ese comentario
        public Pelicula Pelicula { get; set; } = null!; //LLamo a la entidad
        //Al hacer esto estoy configurando PeliculaId como una llave foranea


    }
}
