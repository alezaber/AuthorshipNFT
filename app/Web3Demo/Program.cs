using iText.Kernel.Pdf;
using Web3Demo.Models.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient("GLO", c => c.BaseAddress = new System.Uri("https://practice-api-dot-practice-portal.appspot.com"));
builder.Services.AddHttpClient("glo-storage", c => c.BaseAddress = new System.Uri("https://practice-cloud-storage-api-s4qr6key4q-uc.a.run.app/"));

builder.Services.AddTransient<DocumentsService>();

builder.Services.AddTransient<NFTService>(); 
 

builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
    const string DEFAULT_CORS_POLICY = "AllowAllOrigins";
    options.AddPolicy(name: DEFAULT_CORS_POLICY,
                builder =>
                {
                    builder.AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetPreflightMaxAge(TimeSpan.FromDays(1));
                });

    options.DefaultPolicyName = DEFAULT_CORS_POLICY;
});

var app = builder.Build();

app.UseExceptionHandler(err => err.UseCustomErrors(builder.Environment, app.Logger));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
