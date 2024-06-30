using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ToDoApi.DataSeeder;
using ToDoApi.Infrastructure.Data;
using ToDoApi.Mapping;
using ToDoApi.Repositories;
using ToDoApi.Services;
using Newtonsoft.Json;

namespace ToDoApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient();

            // Add services to the container.
            var configuration = builder.Configuration;

            // Configure DbContext
            builder.Services.AddDbContext<ToDoDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ToDoDb"));
            });

            // Configure Automapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TaskMappingProfile());
            });
            var mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/myapp.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                .ReadFrom.Configuration(hostingContext.Configuration)
                .WriteTo.Console()
                .WriteTo.File("Logs/myapp.log", rollingInterval: RollingInterval.Day));
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog(dispose: true);
            });

            builder.Services.AddScoped<ITaskRepository, TaskRepository>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            builder.Services.AddScoped<ITimeSheetService, TimeSheetService>();

            builder.Services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.FullName);
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Seed Assignees on application startup
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
                var seeder = new DatabaseSeeder(dbContext, configuration);
                await seeder.SeedAsync();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
