using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MudBlazor.Services;
using MultipleEventHandlers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
var jsRuntime = builder.Services.BuildServiceProvider().GetRequiredService<IJSRuntime>();
Lazy<Task<IJSObjectReference>> _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
   "import", "./file.js").AsTask());
var module = await _moduleTask.Value;
await module.InvokeVoidAsync("mountAndInitializeDb");

await builder.Build().RunAsync();
