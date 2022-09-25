// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Rectangles.Services;

// Set up Dependency Injection
var serviceProvider = new ServiceCollection()
    .AddSingleton<IRectanglesGame, RectanglesGame>()
    .AddSingleton<ICliService, CliService>()
    .AddSingleton<IActionsService, ActionsService>()
    .AddSingleton<IPromptService, PromptService>()
    .AddSingleton<IGameActionStrategyContext, GameActionStrategyContext>()
    .BuildServiceProvider();

// Program begins here
var rectanglesGame = serviceProvider.GetService<IRectanglesGame>();
if (rectanglesGame != null)
{
    rectanglesGame.Run();
} 
else
{
    Console.WriteLine("Something went wrong when initializing Rectangles");
}
