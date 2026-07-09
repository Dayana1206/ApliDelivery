using Microsoft.EntityFrameworkCore;
using ApliDelivery.Data;

namespace ApliDelivery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Agregar controladores
            builder.Services.AddControllers();

            // Conexión con SQL Server
            builder.Services.AddDbContext<ApliDeliveryContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("ConexionSQL")
                ));

            // Configuración de Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            // Swagger en desarrollo
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}