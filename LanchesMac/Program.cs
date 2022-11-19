//namespace LanchesMac {
//    public class Program {
//        public static void Main(string[] args) {
//            CreateHostBuilder(args)
//            .Build()
//            .Run();
//        }

//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args)
//                .ConfigureWebHostDefaults(webBuilder => {
//                    webBuilder.UseStartup<Startup>();
//                });
//    }
//}

/*#######################################################################*/

using LanchesMac.Context;
using LanchesMac.Repositories;
using LanchesMac.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

//Add services to the container.
 var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
     ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddDbContext<AppDBContext>(options =>
//    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ILancheRepository, LancheRepository>();

builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();

//options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=CRUD_MVC_SQL_SERVER; Trusted_Connection=True; MultipleActiveResultSets=true;"));
//options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CRUD_MVC_SQL_SERVER;Integrated Security=True;" +
//"Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();