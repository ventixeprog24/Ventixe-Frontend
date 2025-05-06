using Authentication.Handlers;
using Authentication.Services;
using Authentication.Contexts;
using Authentication.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserProfileServiceClient = UserProfileServiceProvider.UserProfileService.UserProfileServiceClient;
using VerificationServiceClient = VerificationServiceProvider.VerificationContract.VerificationContractClient;
using Presentation.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<IdentityUserDbContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString("VentixeIdentityDb")));
builder.Services.AddIdentity<AppUserEntity, IdentityRole>(x =>
{
    x.SignIn.RequireConfirmedAccount = true;
    x.Password.RequiredLength = 8;
    x.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<IdentityUserDbContext>().AddDefaultTokenProviders();
//Configure application cookie here

//Configuring the external UserProfileService
builder.Services.AddGrpcClient<UserProfileServiceClient>(o =>
{
    o.Address = new Uri(builder.Configuration["Grpc:UserProfileService"]!);
});

builder.Services.AddGrpcClient<VerificationServiceClient>(o =>
{
    o.Address = new Uri(builder.Configuration["Grpc:VerificationServiceProvider"]!);
});

builder.Services.AddScoped<RoleHandler>();
builder.Services.AddScoped<AuthService>();

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


app.Run();
