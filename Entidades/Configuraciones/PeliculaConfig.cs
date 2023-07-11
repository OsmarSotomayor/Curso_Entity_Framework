using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Introduccion_A_Entity_Framework_Core.Entidades.Configuraciones
{
    public class PeliculaConfig : IEntityTypeConfiguration<Pelicula>
    {
        public void Configure(EntityTypeBuilder<Pelicula> builder)
        {
            //configuraciones para entidad pelicula 
            builder.Property(a => a.Titulo).HasMaxLength(150);
            builder.Property(a => a.FechaEstreno).HasColumnType("date");
        }
    }
}
