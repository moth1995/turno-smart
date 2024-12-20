using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using turno_smart.Data;
using turno_smart.Interfaces;
using turno_smart.Models;
using turno_smart.Services;

namespace turno_smart
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //var builder = WebApplication.CreateBuilder(args); Inicializamos el builder dentro del tryy del logger

            // Configurar Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try
            {
                Log.Information("Starting up");

                var builder = WebApplication.CreateBuilder(args);

                // Configurar Serilog como el logger de la aplicaci�n
                builder.Host.UseSerilog();

                // Add services to the container.
                string connectionString;

                var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
                var dbName = Environment.GetEnvironmentVariable("DB_NAME");
                var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
                Log.Information($"dbHost: {dbHost}, dbName: {dbName}, dbPassword: {dbPassword}");
                if (!string.IsNullOrEmpty(dbHost) && !string.IsNullOrEmpty(dbName) && !string.IsNullOrEmpty(dbPassword))
                {
                    connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=sa;Password={dbPassword};TrustServerCertificate=True;";
                }
                else
                {                    
                    connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                }
                Log.Information($"connectionString: {connectionString}");
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseLazyLoadingProxies().UseSqlServer(connectionString));
                builder.Services.AddDatabaseDeveloperPageExceptionFilter();

                builder.Services.AddIdentity<Usuarios, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.SignIn.RequireConfirmedPhoneNumber = false;
                })
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();
                builder.Services.ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/Home/Index";
                    options.AccessDeniedPath = "/Home/Index"; // Ruta para acceso denegado
                });

                builder.Services.AddControllersWithViews();
                builder.Services.AddRazorPages();
                builder.Services.AddScoped<IPacienteService, PacienteService>();
                builder.Services.AddScoped<IEspecialidadService, EspecialidadService>();
                builder.Services.AddScoped<IHistorialMedicoService, HistorialMedicoService>();
                builder.Services.AddScoped<IEstudioService, EstudioService>();
                builder.Services.AddScoped<IMedicoService, MedicoService>();
                builder.Services.AddScoped<ITurnoService, TurnoService>();
                builder.Services.AddScoped<IRecepcionistaService, RecepcionistaService>(); 
                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseMigrationsEndPoint();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();
                app.UseStaticFiles();

                app.UseRouting();
                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                app.MapRazorPages();

                using (var scope = app.Services.CreateScope())
                {
                    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    string[] roles = new string[] { "Admin", "Medico", "Paciente", "Recepcionista" };
                    foreach (var role in roles)
                    {
                        if (!await roleManager.RoleExistsAsync(role))
                        {
                            await roleManager.CreateAsync(new IdentityRole(role));
                        }
                    }
                }
                using (var scope = app.Services.CreateScope())
                {
                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Usuarios>>();

                    List<TestUser> testUsers = new List<TestUser>()
                {
                    new TestUser()
                    {
                        Email  = "admin@turno-smart.com",
                        Password = "Admin123!",
                        Role = "Admin",
                        DNI = 12345678
                    },
                };

                    foreach (var testUser in testUsers)
                    {
                        if (await userManager.FindByEmailAsync(testUser.Email) == null)
                        {
                            var user = new Usuarios()
                            {
                                UserName = testUser.Email,
                                Email = testUser.Email,
                                DNI = 12345678,
                            };

                            await userManager.CreateAsync(user, testUser.Password);

                            await userManager.AddToRoleAsync(user, testUser.Role);
                        }
                    }
                }

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
    public class TestUser()
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role {  get; set; }
        public int DNI { get; set; }
    }
}
