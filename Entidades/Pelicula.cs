namespace Introduccion_A_Entity_Framework_Core.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public bool EnCines { get; set; }
        public DateTime FechaEstreno { get; set; }

        //Agrego Propiedad de navegacion de 1-muchos 
        //Usamos el tipo de datos HashSet porque es mas rapido para trabajar con colecciones 
        // que el tipo listado, este tipo de datos no puede ordenar, si nececitas eso usa List
        public HashSet<Comentario> Comentarios { get; set; } = new HashSet<Comentario>();


        //Relacion-directa
        public HashSet<Genero> Generos { get; set; }= new HashSet<Genero>();

        //Relacion Pelicula-PeliculaActor (usamos listas para mantener el orden)
        public List<PeliculaActor> PeliculasActores { get; set; } = new List<PeliculaActor>();

    }
}
