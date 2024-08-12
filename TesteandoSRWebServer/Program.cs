using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TesteandoSRWebServer;
using TesteandoSRWebServer.Models;


internal class Program
{
    private static void Main(string[] args)
    {
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", Utils.GoogleCredentialsPath);
        Environment.SetEnvironmentVariable("FIRESTORE_ID", Utils.FirestoreId);

        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile(Utils.AdminSdkFile),
            ProjectId = Utils.FirestoreId
        });

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "test",
                              policy =>
                              {
                                  policy.WithOrigins("*")
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                              });
        });

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

        app.UseCors("test");

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}