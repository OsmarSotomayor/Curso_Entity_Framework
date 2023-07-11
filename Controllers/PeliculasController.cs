using AutoMapper;
using Introduccion_A_Entity_Framework_Core.DTO_S;
using Introduccion_A_Entity_Framework_Core.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Introduccion_A_Entity_Framework_Core.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PeliculasController(ApplicationDbContext context, 
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(PeliculaCreacionDTO
            peliculaCreacionDTO)
        {
            var pelicula = mapper.Map<Pelicula>(peliculaCreacionDTO);

            //El siguiente codigo es para que al momento de agregar el genero
            //No se cree un nuevo genero sino use uno ya existente 
            if (pelicula.Generos is not null)
            {
                //Relacion muchos a muchos sin tener yabla intermedia 
                foreach (var genero in pelicula.Generos )
                {
                    context.Entry(genero).State = EntityState.Unchanged;
                    /*EFC da segumi a los objetos para ver si corresponde a una BBD
                     Entry(genero).State = EntityState.Unchanged; QUIERE DECIR QUE
                    EL OBJETO genero tiene un estatus sin cambiar (es decir representa
                    un registro en bbd y no le hemos hecho cambios) con esto le decimos
                    a EFC que no tiene que crear un nuevo genero pues ya es uno existente
                    */

                }
            }

            
            if (pelicula.PeliculasActores is not null){ 
                for(int i =0; i<pelicula.PeliculasActores.Count; i++)
                {
                    //Asigno un orden en base a como lleguen los actores a si
                    // se van guardando en BBD
                    pelicula.PeliculasActores[i].Order = i + 1;
                }
            
            }

            context.Add(pelicula);
            await context.SaveChangesAsync();
            return Ok();
        }

        //Metodo par consultar pelicual y comentarios
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pelicula>> getPeliculaConComentarios(int id)
        {
            var pelicula = await context.Peliculas.
                Include(pelicula => pelicula.Comentarios). //Con esta linea nos aseguramos de que se traiga el comentario de las peliculas
                Include(pelicula => pelicula.Generos).
                Include(pelicula => pelicula.PeliculasActores.OrderBy(pa => pa.Order)).
                ThenInclude(peliculasActores => peliculasActores.Actor). //Para traer el nombre del actor 
                FirstOrDefaultAsync(pelicula => pelicula.Id == id);
                if (pelicula is null)
            {
                return NotFound();
            }
                return pelicula;

        }

        //Metodo segundo metodo para consultar pelicual y comentarios
        [HttpGet("select/{id:int}")]
        public async Task<ActionResult<Pelicula>> selectPeliculaConComentarios(int id)
        {
            var pelicula = await context.Peliculas.Select
                (peli => new
                {
                    peli.Id,
                    peli.Titulo, //De generos solo traes el nombre 
                    Generos = peli.Generos.Select(x => x.Name).ToList(),
                    Actores = peli.PeliculasActores.
                    OrderBy(actores => actores.Order).Select(actor =>
                    new
                    {
                        //De peliculas actores solo traes su nombre y id
                        id = actor.ActorId,
                        actor.Actor.Nombre
                        //Es decir solo veremos nombre y id del actor
                    }),
                    cantidadComentarios = peli.Comentarios.Count()

                }).FirstOrDefaultAsync(p=> p.Id ==id);
            if (pelicula is null)
            {
                return NotFound();
            }
            return Ok(pelicula);
        }
    }
}
