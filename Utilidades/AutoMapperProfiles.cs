using AutoMapper;
using Introduccion_A_Entity_Framework_Core.DTO_S;
using Introduccion_A_Entity_Framework_Core.Entidades;

namespace Introduccion_A_Entity_Framework_Core.Utilidades
{
    //clase para configurar Automapper, profile es una clase de Automapper
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles() {
            //creas la relacion de la Entidad con el DTO
            CreateMap<GeneroCreacionDTO, Genero>();
            CreateMap<ActorCreacionDTO, Actor>();

            //Hago el mapeo pero como generos y peliculas
            /*en PeliculaCreacionDTO y Pelicula los generos son tipos de 
             datos distintos, por ello */
            CreateMap<PeliculaCreacionDTO, Pelicula>().
                ForMember(entidad => entidad.Generos, dto =>
                dto.MapFrom(atributo => atributo.Generos.Select(id =>
                new Genero { Id = id })));
            //por cada valor en pelicreadto se creara un genero relacionado con el id
            //Select(id = new Genero { Id = id }) se le llama proyeccion llevas de un entero a un genero

            CreateMap<PeliculaActorCreacionDTO, PeliculaActor>();

            CreateMap<ComentarioCreacionDTO, Comentario>();

            CreateMap<Actor, ActorDTO>();
        }
        
    }
}
