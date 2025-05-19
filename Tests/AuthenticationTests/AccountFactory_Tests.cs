using Authentication.Factories;
using UserProfileServiceProvider;

namespace Tests.AuthenticationTests;

public class AccountFactory_Tests
{
    [Theory]
    [InlineData("joel@domain.com")]
    public async Task AccountFactory_ShouldReturnAppUserEntity_WithValidInput(string email)
    {
        // Act
        var result = AccountFactory.ToAppUserEntity(email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(email, result.Email);
        Assert.Equal(email, result.UserName);
        Assert.True(result.EmailConfirmed);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void AccountFactoryToAppUserEntity_ShouldReturnNull_WithInvalidInput(string? email)
    {
        // Act
        var result = AccountFactory.ToAppUserEntity(email!);

        // Assert
        Assert.Null(result);
    }
    
    [Fact]
    public void AccountFactory_ShouldReturnDto_WithValidInput()
    {
        // Arrange
        var profile = new UserProfile
        {
            UserId = Guid.NewGuid().ToString(),
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            PhoneNumber = "123-456-7890",
            Address = "123 Main St",
            PostalCode = "12345",
            City = "Anytown"
        };

        // Act
        var dto = AccountFactory.ToAppUserProfileDto(profile);

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(profile.UserId, dto.UserId);
        Assert.Equal(profile.FirstName, dto.FirstName);
        Assert.Equal(profile.LastName, dto.LastName);
        Assert.Equal(profile.Email, dto.Email);
        Assert.Equal(profile.PhoneNumber, dto.PhoneNumber);
        Assert.Equal(profile.Address, dto.Address);
        Assert.Equal(profile.PostalCode, dto.PostalCode);
        Assert.Equal(profile.City, dto.City);
    }
    
    [Fact]
    public void AccountFactoryToDto_ShouldReturnNull_WithInvalidInput()
    {
        // Act
        var dto = AccountFactory.ToAppUserProfileDto(null!);

        // Assert
        Assert.Null(dto);
    }
    
    [Fact]
    public void AccountFactory_ShouldReturnHeaderUserProfileDto_WithValidInput()
    {
        // Arrange
        var profile = new UserProfile
        {
            UserId = Guid.NewGuid().ToString(),
            FirstName = "Jane",
            LastName = "Smith"
        };

        // Act
        var headerDto = AccountFactory.ToHeaderUserProfileDto(profile);

        // Assert
        Assert.NotNull(headerDto);
        Assert.Equal(profile.UserId, headerDto.UserId);
        Assert.Equal($"{profile.FirstName} {profile.LastName}", headerDto.FullName);
    }

    [Fact]
    public void AccountFactory_ShouldReturnNull_WithInvalidInput()
    {
        // Act
        var headerDto = AccountFactory.ToHeaderUserProfileDto(null!);

        // Assert
        Assert.Null(headerDto);
    }
}