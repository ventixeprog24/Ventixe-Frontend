using System.Net;
using Authentication.Handlers;
using Authentication.Services;
using Authentication.Contexts;
using Authentication.Entities;
using Authentication.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserProfileServiceClient = UserProfileServiceProvider.UserProfileService.UserProfileServiceClient;
using VerificationServiceClient = VerificationServiceProvider.VerificationContract.VerificationContractClient;
using InvoiceServiceContractClient = InvoiceServiceProvider.InvoiceServiceContract.InvoiceServiceContractClient;
using Presentation.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<IdentityUserDbContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("VentixeIdentityDb")));
builder.Services.AddIdentity<AppUserEntity, IdentityRole>(x =>
{
    x.SignIn.RequireConfirmedAccount = true;
    x.Password.RequiredLength = 8;
    x.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<IdentityUserDbContext>().AddDefaultTokenProviders();

//Configure application cookie here
builder.Services.ConfigureApplicationCookie(o =>
{
    o.LoginPath = "/auth/login";
    o.AccessDeniedPath = "/auth/login";
    o.Cookie.SameSite = SameSiteMode.None;
    o.ExpireTimeSpan = TimeSpan.FromDays(14);
    o.SlidingExpiration = true;
    o.Cookie.IsEssential = true;
    o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddAuthentication(o => {
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();

//Configuring the external UserProfileService
builder.Services.AddGrpcClient<UserProfileServiceClient>(o =>
{
    o.Address = new Uri(builder.Configuration["Grpc:UserProfileService"]!);
});
//Configuring the external InvoiceServiceProvider
builder.Services.AddGrpcClient<InvoiceServiceContractClient>(o =>
{
    o.Address = new Uri(builder.Configuration["Grpc:InvoiceServiceProvider"]!);
});

builder.Services.AddGrpcClient<VerificationServiceClient>(o =>
{
    o.Address = new Uri(builder.Configuration["Grpc:VerificationServiceProvider"]!);
});


builder.Services.AddScoped<RoleHandler>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

var app = builder.Build();

//Uncomment and start app to create roles in the database. 
//await SeedData.SetRolesAsync(app);


app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

//uncomment and start app to create test user in the database.
//await TestUserSeeder.SeedTestUserAsync(app.Services);


app.Run();
