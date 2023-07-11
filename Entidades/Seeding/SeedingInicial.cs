using Microsoft.EntityFrameworkCore;

namespace Introduccion_A_Entity_Framework_Core.Entidades.Seeding
{
    public class SeedingInicial
    {
        //Agrego un actor 
        public static void Seed(ModelBuilder modelBuilder)
        {
            var JeniferLawrence = new Actor
            {
                Id = 2,
                Nombre = "Jennifer Lawrence",
                FechaNacimiento = new DateTime(1994, 12, 21),
                Fortuna = 1
            };

            modelBuilder.Entity<Actor>().
                HasData(JeniferLawrence);

            //Agrego pelicula
            var laIsla = new Pelicula
            {
                Id = 2,
                Titulo = "La Isla Sinistra",
                FechaEstreno = new DateTime(2000, 01, 05)
            };
            modelBuilder.Entity<Pelicula>().
               HasData(laIsla);

            //Agrego comentarios 
            var comentarioLaIsla = new Comentario()
            {
                Id = 2,
                recomenadar = true,
                Contenido = "Magnifica",
                PeliculaId = laIsla.Id
            };

            modelBuilder.Entity<Comentario>().
              HasData(comentarioLaIsla);

            //muchos a muchos con salto (avanzado)
            //Agremamos registro a la tabla generoPelicula 
            /*Insertar datos muchos-muchos con data seeding */
            var tablaGeneroPelicula = "GeneroPelicula";
            var GenerosId = "GenerosId";
            var PeliculasId = "PeliculasId"; //deben ser nombres iguales a los de las tablas 
            var idGenero = 13;

            //con el salto es diferente agregar los datos
            modelBuilder.Entity(tablaGeneroPelicula).HasData(
                new Dictionary<string, object>
                {
                    [GenerosId] = idGenero,
                    [PeliculasId]=laIsla.Id
                }
                );

            //relacion muchos a muchos sin el salto peliculas-actores
            var jeniferLaIsla = new PeliculaActor
            {
                ActorId = JeniferLawrence.Id,
                PeliculaId = laIsla.Id,
                Order = 1
            };

            modelBuilder.Entity<PeliculaActor>().
                HasData(jeniferLaIsla);
        }
    }
}
