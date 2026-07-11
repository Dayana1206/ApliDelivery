using ApliDelivery.Web.Services;


namespace ApliDelivery.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Agregar MVC
            builder.Services.AddControllersWithViews();


            //Patrón de diseño FACADE
            builder.Services.AddHttpClient<IAuthFacade, AuthFacade>();


            // Permite consumir la API
            builder.Services.AddHttpClient();

            // Registrar UsuarioService
            builder.Services.AddHttpClient<IUsuarioService, UsuarioService>();

            // Registrar RestauranteService
            builder.Services.AddHttpClient<IRestauranteService, RestauranteService>();

            // Registrar ProductoService
            builder.Services.AddHttpClient<IProductoService, ProductoService>();

            // Registrar CarritoService
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<ICarritoService, CarritoService>();

            // Habilitar almacenamiento temporal para Session
            builder.Services.AddDistributedMemoryCache();

            // Configurar Session
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configuración de errores
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Activar Session antes de Authorization
            app.UseSession();

            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}