using Application.BusinessServices;
using Application.BusinessServices.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Security;
using WebClient;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiUrl = builder.Configuration.GetSection("ApiUrl").Value;
builder.Services.AddHttpClient("WebApi", (config) =>
{
    //cl.BaseAddress = new Uri("https://localhost:44347/");
    config.BaseAddress = new Uri(apiUrl);
    config.Timeout = new TimeSpan(0, 0, 30);
    config.DefaultRequestHeaders.Clear();
});

builder.Services.AddScoped(
    sp => sp.GetService<IHttpClientFactory>().CreateClient("WebApi"));

builder.Services.AddScoped<IPermissionService, PermissionService>();

await builder.Build().RunAsync();
