using Authentication.Contexts;
using Authentication.Entities;
using Authentication.Handlers;
using Authentication.Interfaces;
using Authentication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presentation.Services;
using BookingServiceClient = BookingServiceProvider.BookingServiceContract.BookingServiceContractClient;
using InvoiceServiceContractClient = InvoiceServiceProvider.InvoiceServiceContract.InvoiceServiceContractClient;
using UserProfileServiceClient = UserProfileServiceProvider.UserProfileService.UserProfileServiceClient;
using VerificationServiceClient = VerificationServiceProvider.VerificationContract.VerificationContractClient;
using EventServiceContractClient = EventServiceProvider.EventContract.EventContractClient;
using LocationServiceProvider;
using EventServiceProvider;
using LocationServiceContractClient = LocationServiceProvider.LocationServiceContract.LocationServiceContractClient;

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

//Configure Identity Cookie
builder.Services.ConfigureApplicationCookie(o =>
{
    o.LoginPath = new PathString("/auth/Login");
    o.AccessDeniedPath = new PathString("/unauthorized");
    o.ExpireTimeSpan = TimeSpan.FromDays(30);
    o.SlidingExpiration = true;
    o.Cookie.SameSite = SameSiteMode.None;
    o.Cookie.IsEssential = true;
});

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
builder.Services.AddGrpcClient<EventServiceContractClient>(o =>
    o.Address = new Uri(builder.Configuration["Grpc:EventServiceProvider"]!)
);
builder.Services.AddGrpcClient<CategoryContract.CategoryContractClient>(o =>
    o.Address = new Uri(builder.Configuration["Grpc:EventServiceProvider"]!));
builder.Services.AddGrpcClient<StatusContract.StatusContractClient>(o =>
    o.Address = new Uri(builder.Configuration["Grpc:EventServiceProvider"]!));
builder.Services.AddGrpcClient<LocationServiceContract.LocationServiceContractClient>(o =>
    o.Address = new Uri(builder.Configuration["Grpc:LocationServiceProvider"]!));



builder.Services.AddGrpcClient<BookingServiceClient>(o =>
{
    o.Address = new Uri(builder.Configuration["Grpc:BookingServiceProvider"]!);
});

builder.Services.AddGrpcClient<LocationServiceContractClient>(o =>
{
    o.Address = new Uri(builder.Configuration["Grpc:LocationServiceProvider"]!);
});


builder.Services.AddScoped<RoleHandler>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<InvoiceService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<LocationService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<IEventService, EventService>();

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
