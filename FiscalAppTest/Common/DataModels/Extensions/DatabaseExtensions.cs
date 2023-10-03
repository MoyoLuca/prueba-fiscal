using DataModels.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataModels.Extensions
{
    public static class DatabaseExtensions
    {
        /// <summary>
        /// AddDatabaseContext - Metodo para agregar el contexto de la base de datos
        /// </summary>
        /// <param name="services"></param>
        /// <param name="ConnectionString"></param>
        public static void AddDatabaseContext(this IServiceCollection services, string ConnectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(ConnectionString); // Reemplaza con tu cadena de conexión
            });

            services.AddScoped<ApplicationDbContext>();
        }

    }
}
