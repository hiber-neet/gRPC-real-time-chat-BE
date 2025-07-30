using GrpcRealTimeAssignment.Services;
using Microsoft.OpenApi.Models;
using Service;
using System.Text.Json.Serialization;

namespace GrpcRealTimeAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowReact", policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            builder.Services.AddGrpc();

          
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Handbag API", Version = "v1" });
            });
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<RoomService>();
            builder.Services.AddScoped<MembershipService>();
            builder.Services.AddScoped<MessageService>();
            builder.Services.AddScoped<FileUploadService>();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseCors("AllowReact");
            app.UseGrpcWeb(); 

            app.MapGrpcService<ChatService>()
               .EnableGrpcWeb()
               .RequireCors("AllowReact");

            app.MapGet("/", () => "gRPC Chat Service is running...");
            app.MapControllers();

            app.Run();
        }
    }
}
