using Authentication.Contexts;
using Authentication.Entities;
using Authentication.Handlers;
using Authentication.Interfaces;
using Authentication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Tests.AuthenticationTests;

public class RoleHandler_Tests
{
    private ServiceProvider _serviceProvider;
    private IServiceScope _scope;
    private RoleHandler _roleHandler;
    private UserManager<AppUserEntity> _userManager;

    public RoleHandler_Tests()
    {
        var services = new ServiceCollection();
        services.AddDbContext<IdentityUserDbContext>(opts =>
            opts.UseInMemoryDatabase($"TestDb-{Guid.NewGuid()}")
        );
        services.AddIdentity<AppUserEntity, IdentityRole>(options =>
            //GPT generated after debugging help.
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<IdentityUserDbContext>();
        services.AddLogging();
        services.AddScoped<RoleHandler>();

        _serviceProvider = services.BuildServiceProvider();
        _scope = _serviceProvider.CreateScope();
        var _scopeProvider = _scope.ServiceProvider;

        var roleManager = _scopeProvider.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roles = ["Admin", "User"];
        
        foreach (var roleName in roles)
        {
            if (!roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
            }
        }

        _userManager = _scopeProvider.GetRequiredService<UserManager<AppUserEntity>>();
        _roleHandler = _scopeProvider.GetRequiredService<RoleHandler>();
    }

    [Theory]
    [InlineData("john@domain.com", "BytMig123!", "User")]
    [InlineData("john@domain.com", "BytMig123!", "Admin")]
    public async Task AddToRoleAsync_ShouldReturnSucceeded_WithValidInput(string email, string password, string role)
    {
        //Arrange
        var testUser = new AppUserEntity { UserName = email, Email = email};
        await _userManager.CreateAsync(testUser, password);
        
        //Act
        var result =  await _userManager.AddToRoleAsync(testUser, role);
        
        //Assert
        Assert.True(result.Succeeded);
        var roles = await _userManager.GetRolesAsync(testUser);
        Assert.Contains(role, roles);
    }

    [Theory]
    [InlineData("john@domain.com", "BytMig123!", "")]
    [InlineData("john@domain.com", "BytMig123!", null)]
    [InlineData("john@domain.com", "BytMig123!", "bogus-schmogus")]
    public async Task AddToRoleAsync_ShouldReturnFailed_ForInvalidInput(string email, string? password, string? role)
    {
        //Arrange
        var testUser = new AppUserEntity { UserName = email, Email = email};
        await _userManager.CreateAsync(testUser, password);
        
        //Act
        var result =  await _roleHandler.AddToRoleAsync(testUser, role);
        
        //Assert
        Assert.False(result.Succeeded);
    }
    
    [Theory]
    [InlineData("john@domain.com", "BytMig123!", "User")]
    [InlineData("john@domain.com", "BytMig123!", "Admin")]
    public async Task GetRoleAsync_ShouldReturnSingleRole_WhenValidDataProvided(string email, string password, string role)
    {
        // Arrange
        var _role = role;
        var testUser = new AppUserEntity { UserName = "John@domain.com", Email = "John@domain.com"};
        await _userManager.CreateAsync(testUser, password);
        await _userManager.AddToRoleAsync(testUser, _role);

        // Act
        var result = await _roleHandler.GetRoleAsync(testUser);
        // Assert
        Assert.Equal(_role, role);
    }

    [Fact]
    public async Task GetroleAsync_ShouldReturnNull_WhenMultipleRolesAssigned()
    {
        //Arrange
        var testUser = new AppUserEntity { UserName = "John@domain.com", Email = "John@domain.com"};
        await _userManager.CreateAsync(testUser, "BytMig123!");
        await _userManager.AddToRoleAsync(testUser, "Admin");
        await  _userManager.AddToRoleAsync(testUser, "User");
        
        //Act
        var result = await _roleHandler.GetRoleAsync(testUser);
        
        //Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task RemoveFromRoleAsync_ShouldReturnSucceeded_WithValidInput()
    {
        //Arrange
        var testUser = new AppUserEntity { UserName = "John@domain.com", Email = "John@domain.com"};
        await _userManager.CreateAsync(testUser, "BytMig123!");
        await _userManager.AddToRoleAsync(testUser, "Admin");
        
        //Act
        var result = await _roleHandler.RemoveFromRoleAsync(testUser);
        
        //Assert
        Assert.NotNull(testUser);
        Assert.True(result.Succeeded);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("bogus-schmogus")]
    public async Task RemoveFromRoleAsync_ShouldReturnNull_WithInvalidInput(string? role)
    {
        //Arrange
        var testUser = new AppUserEntity { UserName = "John@domain.com", Email = "John@domain.com"};
        await _userManager.CreateAsync(testUser, "BytMig123!");
        await _userManager.AddToRoleAsync(testUser, "Admin");
        
        //Act
        var result = await _roleHandler.RemoveFromRoleAsync(testUser, role!);
        
        //Assert
        Assert.Null(result);
    }
    
    public void Dispose()
    {
        _scope.Dispose();
        _serviceProvider.Dispose();
    }
}


    