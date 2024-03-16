using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.VisualBasic;

internal class Program
{
    private static void Main(string[] args)
    {
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "D:\\Proyectos\\TesteandoSRWebServer\\TesteandoSRWebServer\\necessito-proyecto-app-firebase-adminsdk-yyy5w-66ab3b278f.json");

        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile("necessito-proyecto-app-firebase-adminsdk-yyy5w-66ab3b278f.json"),
            ProjectId = "necessito-proyecto-app"
        });

        var builder = WebApplication.CreateBuilder(args);

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

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}