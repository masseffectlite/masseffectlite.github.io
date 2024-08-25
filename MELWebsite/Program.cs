using MELWebsite;
using MELWebsite.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IMDFilesServices, MDFilesServices>();

var provider = new FileExtensionContentTypeProvider();
//provider.Mappings.Add(".md", "text/markdown");
builder.Services.Configure<StaticFileOptions>(options =>
{
    options.ContentTypeProvider = provider;
});


await builder.Build().RunAsync();
