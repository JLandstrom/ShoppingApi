using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShoppingApi.Authorization.Handlers;
using ShoppingApi.Authorization.Requirements;
using ShoppingApi.Data;
using ShoppingApi.Model.Domain;
using ShoppingApi.Repository;
using ShoppingApi.Repository.Interface;

namespace ShoppingApi
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
            //DbContext
            services.AddDbContext<ShoppingContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("ShoppingDatabase")));

            //Identity
            services.AddDefaultIdentity<AppUser>().AddEntityFrameworkStores<ShoppingContext>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            });

            //JWT Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    });

            //Policies and authorization handlers
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminUser", policy => policy.Requirements.Add(new RoleRequirement("Admin","Subscription", "Guest")));
            });
            services.AddSingleton<IAuthorizationHandler, RoleAuthorizationHandler>();

            //MVC
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Dependency Injection
            ConfigureDI(services);
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

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
        
        private void ConfigureDI(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IShoppingListRepository, ShoppingListRepository>();
            services.AddTransient<IShoppingItemRepository, ShoppingItemRepository>();
            services.AddTransient<IItemCategoryRepository, ItemCategoryRepository>();
        }
    }
}
