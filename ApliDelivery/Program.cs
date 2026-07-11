using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using ApliDelivery.Data;

namespace ApliDelivery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Agregar controladores e ignorar referencias circulares
            builder.Services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler =
                        ReferenceHandler.IgnoreCycles;
                });

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