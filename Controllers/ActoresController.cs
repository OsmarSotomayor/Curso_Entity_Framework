using AutoMapper;
using AutoMapper.QueryableExtensions;
using Introduccion_A_Entity_Framework_Core.DTO_S;
using Introduccion_A_Entity_Framework_Core.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Introduccion_A_Entity_Framework_Core.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController :ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActoresController(ApplicationDbContext context, IMapper 
            mapper) {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ActorCreacionDTO
            actorCreacionDTO)
        {
            var actor = mapper.Map<Actor>(actorCreacionDTO);
            context.Add(actor);
            await context.SaveChangesAsync();
            return Ok();
        }

        //Consultar todos los actores 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Actor>>> Get()
        {
            return await context.Actores.ToListAsync();
        }

        //Filtro para buscar actor por nombre 
        [HttpGet("nombre")]
        public async Task<ActionResult<IEnumerable<Actor>>> Get(string nombreDeActor)
        {
            //usas expresiones lambda, actor es un objeto de Actores 
            return await context.Actores
                .Where(actor => actor.Nombre ==nombreDeActor).ToListAsync();
            //Como uso una igualdad el nombre debe de ser exacto
        }


        [HttpGet("nombre/contein")]
        //Buscador de actor con Contein; preguntara si contiene una palabra en el string  
        public async Task<ActionResult<IEnumerable<Actor>>> GetPalabraEnNombre(string 
            nombreDeActor)
        {
            //usas expresiones lambda, actor es un objeto de Actores 
            return await context.Actores
                .Where(actor => actor.Nombre.Contains(nombreDeActor)).ToListAsync();
            //trae los nombres que contienen nuestro parametro
        }

        //Filtro para buscar en rango de Fechas de Nacimiento
        [HttpGet("fecheNacimiento/Rango")]
        public async Task<ActionResult<IEnumerable<Actor>>> getByAgeRange(DateTime dateBegan,
             DateTime dateFinal)
        {
            return await context.Actores.Where(
                actor=> actor.FechaNacimiento >= dateBegan && 
                actor.FechaNacimiento<= dateFinal).ToListAsync();

        }

        //Metodo para buscar el primer actor que cumpla con el id 
        [HttpGet("{id:int}")]
        public async  Task<ActionResult<Actor>> get(int id)
        {
            //Aqui retorna solo un actor, no un IEnumerable de actores
            var actor = await context.Actores.
                FirstOrDefaultAsync(actor => actor.Id == id);
            if(actor is null)
            {
                return NotFound();
            }

            return actor;

        }

        //Consultar todos los actores, con fecha de nacimiento ascendente 
        [HttpGet("orderby/Date")]
        public async Task<ActionResult<IEnumerable<Actor>>> GetActorOrdenado()
        {
            return await context.Actores.OrderBy(actor =>
            actor.FechaNacimiento).ToListAsync();
        }

        [HttpGet("filtro/nombre_fecha_nacimiento")]
        public async Task<ActionResult<IEnumerable<Actor>>> filtarPorNombreYFechaDeNacimiento(string nombre)
        {
            return await context.Actores.
                Where(actor => actor.Nombre == nombre).
                OrderBy(actor => actor.Nombre).
                ThenBy(actor => actor.FechaNacimiento).ToListAsync();

            //Aqui ordenas por el campo nombre y luego por fecha de nacimiento, traes 
            // todas las columnas 
        }

        //Metodo para seleccionar solo el id y el nombre de los actores 
        [HttpGet("select/id_Y_nombre")]
        public async Task<ActionResult<IEnumerable<ActorDTO>>> getNameAndId()
        {
            //La funcion proyectTo hace el mapeo de forma automatica
            return await context.Actores.ProjectTo<ActorDTO>
                 (mapper.ConfigurationProvider).ToListAsync();
                 
            //Como sabe EFC que solo debe traer el Id Y el nombre ?
            //Lo sabe porque en el DTO solo se agregaron esos dos atributos
        }
    }
}
