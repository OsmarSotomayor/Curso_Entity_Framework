using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Introduccion_A_Entity_Framework_Core.Entidades.Configuraciones
{
    //Heredo de esta Interfaz para implementar configuraciones
    public class ActorConfig : IEntityTypeConfiguration<Actor>
    {
        //Implemento la interfaz
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            //Agrego las configuraciones para la entidad actor
            builder.Property(a => a.Nombre).HasMaxLength(150);
            builder.Property(a => a.FechaNacimiento).HasColumnType("date");
            builder.Property(a => a.Fortuna).HasPrecision(5, 2); //5 digitos y 2 decimales
            
        }
    }
}
