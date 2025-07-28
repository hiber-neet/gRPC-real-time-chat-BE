using GrpcRealTimeAssignment.Services;

namespace GrpcRealTimeAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

       
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

            var app = builder.Build();

        
            app.UseRouting();
            app.UseCors("AllowReact");
            app.UseGrpcWeb();

         
            app.MapGrpcService<ChatService>()
               .EnableGrpcWeb()
               .RequireCors("AllowReact");

            app.MapGet("/", () => "gRPC Chat Service is running...");

            app.Run();
        }
    }
}
