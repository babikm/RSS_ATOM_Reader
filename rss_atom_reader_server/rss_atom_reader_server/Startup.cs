using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using rss_atom_reader_server.DataBaseModels;
using rss_atom_reader_server.IRepository;
using rss_atom_reader_server.Repository;
using System.Text;

namespace rss_atom_reader_server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration =(IConfigurationRoot) configuration;
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string securityKey = "this_is_our_supper_long_security_key_for_token_validation_for_rss_atom_reader_2019$smesk.in";

           var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            services.AddMvc();
            services.Configure<Settings>(o => { o.iConfigurationRoot = Configuration; });

            services.AddTransient<INewsFeedRepository, NewsFeedRepository>();

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                   .AllowAnyMethod()
                                                                    .AllowAnyHeader()));

            services.AddMvc();

            services.AddTransient<INewsFeedRepository, NewsFeedRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "smesk.in",
                        ValidAudience = "readers",
                        IssuerSigningKey = symetricSecurityKey
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            else
            {
                app.UseHsts();
            }

            app.UseCors(builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
