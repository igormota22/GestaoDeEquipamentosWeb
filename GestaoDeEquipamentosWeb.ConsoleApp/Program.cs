// ASP.Net Core

//Builder de um servidor web
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//MVC
builder.Services.AddControllersWithViews();

//Criaçao
WebApplication app = builder.Build();

app.UseRouting();
app.MapDefaultControllerRoute();

//Inicia o loop da aplicação
app.Run();
