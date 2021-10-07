using Application.Applications;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Interfaces.Generics;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Entities.Entites;
using Infrastructure.Repository;
using Infrastructure.Repository.Generics;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Threading.Tasks;
using WebAPI.Token;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddDbContext<Context>(options =>
                                           options.UseSqlServer(
                                                   Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<AplicationUser>(options =>
                                        options.SignIn.RequireConfirmedAccount = false)
                                       .AddEntityFrameworkStores<Context>();

            // Interface x Repositorio
            services.AddSingleton(typeof(IGenerics<>), typeof(GenericRepository<>));
            services.AddSingleton<INews, RepositoryNews>();
            services.AddSingleton<IUser, RepositoryUser>();

            // Serviço x Dominio
            services.AddSingleton<INewsService, NewsService>();

            // Interface x Aplicação 
            services.AddSingleton<INewsApplication, NewsApplication>();
            services.AddSingleton<IUserApplication, UserApplication>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = "Teste.Securiry.Bearer",
                    ValidAudience = "Teste.Securiry.Bearer",
                    IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678")
                };

                option.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                        return Task.CompletedTask;
                    }
                };
            });


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WebAPI",
                    Version = "v1",
                    Description = "Cadastro de noticías",
                    Contact = new OpenApiContact
                    {
                        Email = "higorgto@hotmail.com",
                        Name = "Higor Almeida Gomes",
                        Url = new Uri("https://github.com/higoralmeidagomes")
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var urlClient = "https://dominiodocliente.com.br";

            app.UseCors(b => b.WithOrigins(urlClient));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
