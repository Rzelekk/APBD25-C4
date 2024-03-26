namespace LegacyApp.Tests;

public class UserServiceTests
{
    
    //AddUser_ReturnsFalseWhenFirstNameIsEmpty
    [Fact]
    public void AddUser_ReturnsFalseWhenFirstNameIsEmpty()
    {
        
        // Arrange
        var userService = new UserService();

        // Act
        var result = userService.AddUser(
            null, 
            "Kowalski", 
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            1
        );

        // Assert
        
        Assert.False(result);
    }
    
    
    
    // AddUser_ReturnsFalseWhenMissingAtSignAndDotInEmail
    [Fact]
    public void AddUser_ReturnsFalseWhenMissingAtSignAndDotInEmail()
    {
        // Arrange
        var userService = new UserService();
        

        // Act
        var result = userService.AddUser(
            "Jan", 
            "Kowalski", 
            "kowalskikowalskipl",
            DateTime.Parse("2000-01-01"),
            1
        );

        // Assert
        
        Assert.False(result);
    }
    
    
    // AddUser_ReturnsFalseWhenYoungerThen21YearsOld
    [Fact]
    public void AddUser_ReturnsFalseWhenYoungerThen21YearsOld()
    {
        // Arrange
        var userService = new UserService();

        // Act
        var result = userService.AddUser(
            "Jan", 
            "Kowalski", 
            "kowalski@kowalski.pl",
            DateTime.Parse("2023-04-20"),
            1
        );

        // Assert
        
        Assert.False(result);
    }
    
    // AddUser_ReturnsTrueWhenVeryImportantClient
    [Fact]
    public void AddUser_ReturnsTrueWhenVeryImportantClient()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        var result = userService.AddUser(
            "Jan", 
            "Majewski", 
            "kowalski@kowal.pl",
            DateTime.Parse("2000-01-01"),
            2
        );

        // Assert
        
        Assert.True(result);
    }
    
    // AddUser_ReturnsTrueWhenImportantClient
    [Fact]
    public void AddUser_ReturnsTrueWhenImportantClient()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        var result = userService.AddUser(
            "Jan", 
            "Smith", 
            "kowalski@kowal.pl",
            DateTime.Parse("2000-01-01"),
            3
        );

        // Assert
        
        Assert.True(result);
    }
    
    // AddUser_ReturnsTrueWhenNormalClient
    [Fact]
    public void AddUser_ReturnsTrueWhenNormalClient()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        var result = userService.AddUser(
            "Jan", 
            "Kwiatkowski", 
            "kowalski@kowal.pl",
            DateTime.Parse("2000-01-01"),
            1
        );

        // Assert
        
        Assert.True(result);
    }
    
    // AddUser_ReturnsFalseWhenNormalClientWithNoCreditLimit
    
    [Fact]
    public void AddUser_ReturnsFalseWhenNormalClientWithNoCreditLimit()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        var result = userService.AddUser(
            "Jan", 
            "Kowalski", 
            "andrzejewicz@wp.pl",
            DateTime.Parse("2000-01-01"),
            1
        );
    
        // Assert
        
        Assert.False(result);
    }
    
    // // AddUser_ThrowsExceptionWhenUserDoesNotExist
    [Fact]
    public void AddUser_ThrowsExceptionWhenUserDoesNotExist()
    {
        // Arrange
        var userService = new UserService();
        
        // Act
        Action action = () =>  userService.AddUser(
            "Jan", 
            "Cokolwiek", 
            "kowalski@koowwal.pl",
            DateTime.Parse("2000-01-01"),
            1
        );
    
        // Assert
        
        Assert.Throws<ArgumentException>(action);
    }
    
   
    // AddUser_ThrowsArgumentExceptionWhenClientDoesNotExist
    [Fact]
    public void AddUser_ThrowsArgumentExceptionWhenClientDoesNotExist()
    {
        
        // Arrange
        var userService = new UserService();

        // Act
        Action action = () => userService.AddUser(
            "Jan", 
            "Kowalski", 
            "kowalski@kowalski.pl",
            DateTime.Parse("2000-01-01"),
            100
        );

        // Assert
        Assert.Throws<ArgumentException>(action);
    }
    
}