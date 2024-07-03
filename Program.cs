using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TenisoveTurnaje.Models;
using TenisoveTurnaje.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    //options.UseSqlServer(builder.Configuration["ConnectionStrings:TourDbConnection"]);
    options.UseSqlServer(builder.Configuration["ConnectionStrings:MonsterAspDbConnection"]);

});

builder.Services.AddScoped<TournamentService>();
builder.Services.AddScoped<CourtService>();
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireLowercase = true;
    options.Password.RequiredLength = 8;
});

builder.Services.ConfigureApplicationCookie(options => {
    options.Cookie.Name = ".ASPNetCore.Identity.Application";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.SlidingExpiration = true;
});


var app = builder.Build();

if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
