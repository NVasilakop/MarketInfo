
using WorkerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR(huboptions =>
{
    huboptions.EnableDetailedErrors = true;
});
builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
{
    builder.WithOrigins("https://localhost:44420").AllowCredentials().AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddHostedService<StockPubSubWorker>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");
app.UseEndpoints(endpoints => endpoints.MapHub<StockHub>("/stock"));
app.Run();
