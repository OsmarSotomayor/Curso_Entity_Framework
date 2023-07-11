using AutoMapper;
using Introduccion_A_Entity_Framework_Core.DTO_S;
using Introduccion_A_Entity_Framework_Core.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Introduccion_A_Entity_Framework_Core.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController: ControllerBase //controladores reciben peticiones HTTP
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public GenerosController(ApplicationDbContext context, IMapper mapper) { 

            this._context = context;
            this.mapper = mapper;
        }

        /*Usamos un metodo asincrono porque es buena practica cuando
         nuestro sistema se comunica con otro asp.net-databse
        Ira a la api api/generos */
        [HttpPost]
        public async Task<ActionResult> Post(GeneroCreacionDTO generoCreacion) {
            /*Esto no agrega el genero como tal, solo marca que deseamos
             agregar ese objeto genero al contexto, el guardado se agrega
            cuando guardamos los cambios con SaveChanges */

            var genero = mapper.Map<Genero>(generoCreacion); //Automapper hace el mapeo de forma automatica
            //<tipo de dato>(A LO QUE LO VAS A RELACIONAR O MAPPEAR(DTO en este caso))

            _context.Add(genero);
            await _context.SaveChangesAsync();

            /*Quien se comunica con la BBD es SaveChangesAsync que empujara
             los objetos que agregamos/elimina/act etc con anterioridad 
            por eso el await va en su linea */

            return Ok(); //solo para indicar que todo salio bien

        }

        //Add range para agregar varios generos a la ves
        [HttpPost("varios")] // varios es un nuevo endpoint para ponerlo en la url
        public async Task<ActionResult> Post(GeneroCreacionDTO[] generosCreacionDTOs) { 
           
            var generos = mapper.Map<Genero[]> (generosCreacionDTOs);
            _context.AddRange(generos);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //Metodo para consultaro todos los registros de la tabla Generos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> Get()
        {
            return await _context.Generos.ToListAsync();
        }

        /*Update metodo para actualizar registro agregandole un actualizado
         */
        [HttpPut("{id:int}/nombreNuevo")]
        public async Task<ActionResult> UpdateNuevoNombreGenero(int idGenero)
        {
            var genero = await _context.Generos.FirstOrDefaultAsync(
                genero => genero.Id== idGenero);
            if(genero is null)
            {
                return NotFound();
            }
            genero.Name = genero.Name + " Actualizado";

            //Se guarda el genero con firstofdefault y con el saveChanges se actualiza
            // el nuevo nombre en la columna genero
            /*Se llama conectado porque traigo una instancia de genrro
             y actualizo el nombre a partir de la misma instancia de ese genero*/
            await _context.SaveChangesAsync();

            //Es la misma istancia
            return Ok();
        }
    }
 }
