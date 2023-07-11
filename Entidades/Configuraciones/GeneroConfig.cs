using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Introduccion_A_Entity_Framework_Core.Entidades.Configuraciones
{
    public class GeneroConfig : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            //Usando HasData
            //Denben ser id que no esten ya en la tabla 
            var cienciaFiccion = new Genero { Id = 17, Name = "Erotico" };
            var biografico = new Genero { Id = 18, Name = "Biografico" };
            builder.HasData(cienciaFiccion, biografico);
        }
    }
}
