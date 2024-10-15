using FiladelfiaFunction.Akrun;
using FiladelfiaFunction;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FiladelfiaFunction.Filadelfia;


var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })

    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        // Bind ApiSettings from appsettings.json
        services.Configure<AkrualSettings>(configuration.GetSection("AkrualSettings"));
        services.Configure<FiladelfiaSettings>(configuration.GetSection("FiladelfiaSettings"));


        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();


        //Akrun
        services.AddHttpClient<AkrualApiServices>();
        services.AddSingleton<AuthAkrual>();

        //filadelfia
        services.AddHttpClient<FiladelfiaApiServices>();



    })
    .Build();

host.Run();
