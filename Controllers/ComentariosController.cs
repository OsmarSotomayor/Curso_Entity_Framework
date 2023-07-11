using AutoMapper;
using Introduccion_A_Entity_Framework_Core.DTO_S;
using Introduccion_A_Entity_Framework_Core.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Introduccion_A_Entity_Framework_Core.Controllers
{
    [ApiController]
    [Route("api/peliculas/{peliculaId:int}/comentarios")]
    public class ComentariosController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper mapper;

        public ComentariosController(ApplicationDbContext context,
            IMapper mapper) {
            this._context = context;
            this.mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult> Post (int peliculaId, //peliculaId viene de {peliculaId:int} en la ruta
            ComentarioCreacionDTO comentarioCreacionDTO)
        {
            var comentario = mapper.Map<Comentario>(comentarioCreacionDTO);
            comentario.PeliculaId = peliculaId;
            _context.Add(comentario);
            await _context.SaveChangesAsync();
            return Ok();

        }

    }
}
