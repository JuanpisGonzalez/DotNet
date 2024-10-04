using DotNetMvcIdentity.Data;
using DotNetMvcIdentity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace DotNetMvcIdentity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Add SqlServer connection
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")
            ));

            //add facebook authentication
            builder.Services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = builder.Configuration["Facebook:AppId"];
                options.AppSecret = builder.Configuration["Facebook:AppSecret"];
            });

            builder.Services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = builder.Configuration["Google:AppId"];
                options.ClientSecret = builder.Configuration["Google:AppSecret"];
            });

            //Directives authorization support POLICY
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", policy => policy.RequireRole("Administrator"));
                options.AddPolicy("Registered", policy => policy.RequireRole("Registered"));
                options.AddPolicy("User", policy => policy.RequireRole("User"));
                options.AddPolicy("UserAndAdmin", policy => policy.RequireRole("User").RequireRole("Administrator"));

                //Claims Permissions
                options.AddPolicy("AdminCreatePermission", policy => policy.RequireRole("Administrator").RequireClaim("Create", "True"));
                options.AddPolicy("AdminDeleteUpdatePermission", policy => policy.RequireRole("Administrator").RequireClaim("Update", "True").RequireClaim("Delete", "True"));
                options.AddPolicy("AdminCreateUpdateDeletePermission", policy => policy.RequireRole("Administrator").RequireClaim("Create","True").RequireClaim("Update", "True").RequireClaim("Delete", "True"));
            });

            builder.Services.AddTransient<IEmailSender, EmailSender>();
            //Add identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            //Identity configuration options

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
            });

            //Change login default route
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Account/Login");//"/Controller/Method"
                options.AccessDeniedPath = new PathString("/Account/AccessDenied");//"/Controller/Method"
                options.Cookie.HttpOnly = true; // Seguridad
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Solo en HTTPS
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Duración de la cookie
                options.SlidingExpiration = true; // Renueva la cookie al usarla
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Add authentication
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
