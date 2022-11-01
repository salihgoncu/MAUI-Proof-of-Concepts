using Microsoft.AspNetCore.Authentication.Cookies;
using WebApplication3.Services;

namespace WebApplication3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddGrpc(options =>
            {
                options.EnableDetailedErrors= true;
                options.MaxReceiveMessageSize= 2* 1024*1024;
                options.MaxSendMessageSize = 5* 1024*1024;
            });
            builder.Services.AddGrpcReflection();

            builder.Services.AddAuthentication(o =>
            { o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;})
                .AddCookie()
                .AddGoogle(g =>
                {
                    g.ClientId = "55517695374-mchmou24nlcahlhgmnmfl8j7tg39r8kh.apps.googleusercontent.com";
                    g.ClientSecret = "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";
                    g.SaveTokens = true;                   
                });
            var app = builder.Build();
            app.UseCors();
            app.UseRouting();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                //app.MapGrpcReflectionService();
            }

            //app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<GreeterService>();
                if (app.Environment.IsDevelopment())
                    endpoints.MapGrpcReflectionService();
            });
            //app.MapControllers();

            //app.MapGrpcService<GreeterService>();

            app.Run();
        }
    }
}