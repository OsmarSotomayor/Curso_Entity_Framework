using Introduccion_A_Entity_Framework_Core.Entidades;
using Introduccion_A_Entity_Framework_Core.Entidades.Seeding;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Introduccion_A_Entity_Framework_Core
{
    //Esta clase es la parte central de EFC, donde se configura todo
    public class ApplicationDbContext: DbContext
    {
        //constructor vvv

        /*En este constructor puedes agregar el conexion string, referencias a 
        la base como postgres u Oracle*/
        public ApplicationDbContext(DbContextOptions options): base(options) { 
        
        
        }

        //Inicializo la entidad genero
        //DbSet configura una clase como Entidad , Generos sera el nombre de la tabla 
        public DbSet<Genero> Generos => Set<Genero>();
        public DbSet<Actor> Actores => Set<Actor>();
        public DbSet<Pelicula> Peliculas => Set<Pelicula>();
        public DbSet<Comentario> Comentarios => Set<Comentario>();

        public DbSet<PeliculaActor> PeliculasActores => Set<PeliculaActor>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //Para aplicar las configuraciones de la carptea configuraciones
            //Se debe aplicar la siguiente linea 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //Agregas la config para seedings 
            SeedingInicial.Seed(modelBuilder);
        } 

        //Agrego convencion 
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveMaxLength(150); //A si por defecto los strin se congfig como varchar(150)
        }

    }
}
