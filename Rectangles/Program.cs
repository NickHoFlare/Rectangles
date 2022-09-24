// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Rectangles;

// Set up Dependency Injection
var services = new ServiceCollection();
ConfigureServices(services);

var serviceProvider = services
    .AddSingleton<IRectanglesService, RectanglesService>()
    .BuildServiceProvider();

// Program begins here
var rectanglesService = serviceProvider.GetService<IRectanglesService>();
if (rectanglesService != null)
{
    rectanglesService.Run();
} 
else
{
    Console.WriteLine("Something went wrong when initializing Rectangles");
}

/// <summary>
/// Add auxiliary (non-core) dependencies to DI
/// </summary>
void ConfigureServices(IServiceCollection services)
{

}