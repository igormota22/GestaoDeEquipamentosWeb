// ASP.Net Core

//Builder de um servidor web
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//MVC - Model,View,Controller
builder.Services.AddControllersWithViews();

//Criaçao da instancia de um servidor web
WebApplication app = builder.Build();

//Middlewares - funçoes que executam em cada chamada que o nosso servidor vai receber
app.UseStaticFiles();
app.UseRouting();
app.MapDefaultControllerRoute();

//Inicia o loop da aplicação
app.Run();
