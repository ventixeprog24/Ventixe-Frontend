using Authentication.Contexts;
using Authentication.Entities;
using Authentication.Handlers;
using Authentication.Interfaces;
using Authentication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.AuthenticationTests;

public class AuthService_Tests : IDisposable
{
    private readonly IAuthService _authService;
    private readonly IServiceScope _scope;
    private readonly ServiceProvider _serviceProvider;

    public AuthService_Tests()
    {
        var services = new ServiceCollection();
        services.AddDbContext<IdentityUserDbContext>(options =>
            options.UseInMemoryDatabase($"TestDb-{Guid.NewGuid()}"));

        services.AddIdentity<AppUserEntity, IdentityRole>()
            .AddEntityFrameworkStores<IdentityUserDbContext>();
        
        services.AddLogging();
        services.AddScoped<RoleHandler>();
        services.AddScoped<IAuthService, AuthService>();
        
        _serviceProvider = services.BuildServiceProvider();
        _scope = _serviceProvider.CreateScope();
        
        var rm = _scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roles = ["Admin", "User"];
        
        foreach (var roleName in roles)
        {
            if (!rm.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                rm.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
        }
        _authService = _scope.ServiceProvider.GetRequiredService<IAuthService>();
    }

    #region UserNameExists Tests
    [Fact]
    public async Task UserNameExists_ReturnsTrue_WhenUserNameExistsInDatabase()
    {
        //Arrange
        var email = "john@domain.com";
        var password = "johnDoe123!";
        await _authService.SignUpAsync(email, password);
        
        //Act
        var result = await _authService.DoesUsernameExistAsync(email);
        
        //Assert
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
    }

    [Fact]
    public async Task UserNameExists_ReturnsFalse_WhenUserNameDoesNotExistInDatabase()
    {
        //Arrange
        var email = "john@domain.com";
        //Act
        var result = await _authService.DoesUsernameExistAsync(email);
        
        //Assert
        Assert.NotNull(result);
        Assert.False(result.Succeeded);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public async Task UserNameExists_ReturnsTrue_WhenInvalidEmail(string? email)
    {
        //Act
        var result = await _authService.DoesUsernameExistAsync(email!);
        
        //Assert
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
    }
    #endregion

    #region SignUp Tests

    [Fact]
    public async Task SignUp_ReturnsTrueWhenUserIsSuccessfullyCreated_WithValidInputData()
    {
        //Arrange
        var email = "john@domain.com";
        var password = "johnDoe123!";
        
        //Act
        var result =  await _authService.SignUpAsync(email, password);
        
        //Assert
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
    }

    [Theory]
    [InlineData("", "BytMig123!")]
    [InlineData(null, "BytMig123!")]
    [InlineData("john@domain.com", "")]
    [InlineData("john@domain.com", null)]
    public async Task SignUp_ReturnsFalseWhenUserIsNotSuccessfullyCreated_WithoutValidInputData(string? email, string? password)
    {
        //Act
        var result = await _authService.SignUpAsync(email!, password!);
        
        //Assert
        Assert.NotNull(result);
        Assert.False(result.Succeeded);
    }
    #endregion
    
    #region Delete Tests

    [Fact]
    public async Task Delete_ReturnsTrueWhenUserIsSuccessfullyDeleted_WithValidInputData()
    {
        //Arrange
        var email = "john@domain.com";
        var password = "johnDoe123!";
        var createResult = await _authService.SignUpAsync(email, password);
        Assert.NotNull(createResult);
        Assert.True(createResult.Succeeded);
        Assert.NotNull(createResult.UserId);
        
        //Act
        var result = await _authService.DeleteAccountAsync(createResult.UserId);
        
        //Assert
        Assert.NotNull(result);
        Assert.True(result.Succeeded);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("bogus-schmogus")]
    public async Task Delete_ReturnsFalseWhenUserIsNotSuccessfullyDeleted_WithoutValidInputData(string? id)
    {
        //Act
        var result = await _authService.DeleteAccountAsync(id!);
        
        //Assert
        Assert.NotNull(result);
        Assert.False(result.Succeeded);
    }
    #endregion
    
    public void Dispose()
    {
        _scope.Dispose();
        _serviceProvider.Dispose();
    }
}