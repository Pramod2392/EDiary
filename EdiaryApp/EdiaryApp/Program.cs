

using DataAccess;
using DataAccess.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EdiaryApp;

static partial class Program
{
    public static void Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build(); 
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;        

        try
        {
            services.GetRequiredService<IApp>().Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error has occured: {ex.Message}");
            Console.ReadLine();
        }
    }

    static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddTransient<IApp, App>();
                services.AddTransient<ISqliteDataAccess, SqliteDataAccess>();
                services.AddTransient<IOperation, Operation>();
                services.AddTransient<IAnswerModel, AnswerModel>();
            });         
    }
}




