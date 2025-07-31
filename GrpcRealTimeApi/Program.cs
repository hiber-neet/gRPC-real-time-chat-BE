using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.Models;
using Service;
using System.Text.Json.Serialization;

namespace GrpcRealTimeApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
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

            
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Chat API", Version = "v1" });
                });


				builder.Services.AddDbContext<ChatApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


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

                app.UseAuthorization();

                // API endpoints
                app.MapControllers();
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var dbContext = services.GetRequiredService<ChatApplicationDbContext>();
                    dbContext.Database.Migrate();
                }
                app.Run();
            }
        }
    }
}
