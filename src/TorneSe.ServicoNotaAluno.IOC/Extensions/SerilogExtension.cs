using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NpgsqlTypes;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.PostgreSQL;
using Serilog.Sinks.PostgreSQL.ColumnWriters;

namespace TorneSe.ServicoNotaAluno.IOC.Extensions;

public static class SerilogExtension
{
    public static IServiceCollection ConfigureSerilog(this IServiceCollection services, IConfiguration configuration, IHostEnvironment hostEnvironment)
    {
        // LogEventLevel minimumLevelDefault =
        //                 Enum.TryParse(configuration["Logging:MinLoggingLevel"], true, out minimumLevelDefault) ? minimumLevelDefault : LogEventLevel.Warning;

        // LogEventLevel minimumLevelConsoleDefault =
        //                 Enum.TryParse(configuration["Logging:MinLoggingLevelConsole"], true, out minimumLevelConsoleDefault) ? minimumLevelConsoleDefault : LogEventLevel.Warning;

        Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Is(LogEventLevel.Information)
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                        .MinimumLevel.Override("System", LogEventLevel.Error)
                        .Enrich.WithProperty("Application", configuration["Application:ApplicationName"])
                        .Enrich.WithProperty("Environment", hostEnvironment.EnvironmentName)
                        .Enrich.FromLogContext()
                        .Enrich.WithExceptionDetails()
                        .Enrich.WithMachineName()
                        .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information)
                        .WriteTo.File($"log-{hostEnvironment.EnvironmentName}.txt", rollingInterval: RollingInterval.Day)
                        // .WriteTo
                        //     .PostgreSQL(configuration.GetConnectionString("DefaultConnection"), 
                        //     configuration["PostgresLogs:TableName"], GetColumnsOptions())
                        .CreateLogger();

        return services;
    }

    private static IDictionary<string, ColumnWriterBase> GetColumnsOptions()
    {
        return new Dictionary<string, ColumnWriterBase>
        {
            { "message", new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
            { "message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
            { "level", new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
            { "raise_date", new TimestampColumnWriter(NpgsqlDbType.TimestampTz) },
            { "exception", new ExceptionColumnWriter(NpgsqlDbType.Text) },
            { "properties", new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
            { "props_test", new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
            { "machine_name", new SinglePropertyColumnWriter("MachineName", PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") }
        };
    }
}


