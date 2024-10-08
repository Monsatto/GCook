using GCook.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

string conexao = builder.Configuration.GetConnectionString("Conexao");
var versao = ServerVersion.AutoDetect(conexao);
builder.Services.AddDbContext<AppDbContext>(
    Options => Options.UseMySql(conexao, versao)
);
builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    opt => opt.SignIn.RequireConfirmedEmail = true
)
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name:  "default",
    pattern: "{controller=Home}/{action=index}/{id?}");

app.Run();