using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Introduccion_A_Entity_Framework_Core.Entidades.Configuraciones
{
    public class PeliculaActorConfig : IEntityTypeConfiguration<PeliculaActor>
    {
        public void Configure(EntityTypeBuilder<PeliculaActor> builder)
        {
      //Agrego config para indicar que la entidad tendra llave primaria compuesta

            builder.HasKey(peliactor => new {peliactor.ActorId, peliactor.PeliculaId });
        }
    }
}
