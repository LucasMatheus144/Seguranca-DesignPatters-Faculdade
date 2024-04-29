using System.Globalization;
using EspacoPotencial.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Espacopotencial;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
         // Configuração do serviço de banco de dados
        services.AddDbContext<ApaDbContext>(options =>
        {
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        });



        // Configuração da autenticação de identidade
        services.AddIdentity<IdentityUser, IdentityRole>()
       .AddEntityFrameworkStores<ApaDbContext>()
               .AddDefaultTokenProviders();


        // Configuração da autenticação de cookies
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);//Definir o logout caso ficar o tempo ausente
                options.SlidingExpiration = true;
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Exige HTTPS para os cookies
                options.LoginPath = "/Account/Login";
            });

        // Configuração sobre o Login / Senha
        services.Configure<IdentityOptions>(options =>
        {
            options.Lockout.MaxFailedAccessAttempts = 3; // Numero máximo de tentativas falhadas antes do bloqueio
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2); 
            //senha
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequiredLength = 8;
        });

        services.AddSession();
        services.AddMvc();

        services.AddControllersWithViews();
    }


    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Definir a cultura como Português do Brasil
        CultureInfo culture = new CultureInfo("pt-BR"); 
        System.Threading.Thread.CurrentThread.CurrentCulture = culture;
        System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {   
             // Habilitar o HTTP Strict Transport Security (HSTS)
            app.UseHsts();
        }

        // Configuração de cabeçalhos de segurança
       app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
            context.Response.Headers.Add("Referrer-Policy", "no-referrer-when-downgrade");
            context.Response.Headers.Add("X-XSS-Protection", "1; mode=block"); // Ativa a proteção contra ataques de XSS (Cross-Site Scripting) no navegador.
            context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains"); // proteger contra ataques de interceptação de TLS/SSL.


            await next();
        });


        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        app.UseEndpoints(endpoints =>
        {

            endpoints.MapControllerRoute(
                name: "Areas",
                pattern: "{area:exists}/{controller=Cadastro}/{action=Index}/{id?}"
                );

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");
        });

    }
}
