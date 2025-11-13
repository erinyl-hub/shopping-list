using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using ShoppingList.Application.Interfaces;
using ShoppingList.Application.Services;
using ShoppingList.Domain.Models;
using Xunit;

namespace ShoppingList.Tests;

/// <summary>
/// Example unit tests for the ShoppingItem domain model.
/// These tests demonstrate best practices for unit testing including:
/// - Clear, descriptive test names following the pattern: Method_Scenario_ExpectedBehavior
/// - Arrange-Act-Assert structure for test organization
/// - Testing both happy paths and edge cases
/// - Comprehensive coverage of validation rules
/// </summary>
///

public class TestDataShoppingList : IEnumerable<object[]>
{

    private List<object[]> Data { get; } =
    [
        [CreateLong()],
        [""],
        [null],
        ["Items6"],
    ];
    public IEnumerator<object[]> GetEnumerator()
    {
        return Data.GetEnumerator();
    }

    private static string CreateLong()
    {
        string res = string.Empty;
        for(int i =0; i < 20000;i++)
        {
            res += i.ToString();
        }

        return res;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class ShoppingListItemFixture
{
    public ShoppingListService _sut { get; set; }

    public ShoppingListItemFixture()
    {
        _sut = new ShoppingListService();
    }
}


public class ShoppingItemTests : IClassFixture<ShoppingListItemFixture>
{
    private ShoppingListItemFixture _fixture { get; set; }
    private IShoppingListService _sut { get; set; }

    public ShoppingItemTests(ShoppingListItemFixture fixture)
    {
        _fixture = fixture;
        _sut = fixture._sut;
    }

    #region Constructor Tests

    [Fact]
    public void Constructor_ShouldSetDefaultValues()
    {
        // Arrange & Act
        var item = new ShoppingItem();

        // Assert
        Assert.NotNull(item.Id);
        Assert.NotEmpty(item.Id);
        Assert.Equal(string.Empty, item.Name);
        Assert.Equal(1, item.Quantity);
        Assert.Null(item.Notes);
        Assert.False(item.IsPurchased);
    }

    [Fact]
    public void Constructor_ShouldGenerateUniqueId()
    {
        // Arrange & Act
        var item1 = new ShoppingItem();
        var item2 = new ShoppingItem();

        // Assert
        Assert.NotEqual(item1.Id, item2.Id);
    }

    #endregion

    #region Name Validation Tests

    [Fact]
    public void Name_WithValidValue_ShouldSetName()
    {
        // Arrange
        var item = new ShoppingItem();
        var expectedName = "Milk";

        // Act
        item.Name = expectedName;

        // Assert
        Assert.Equal(expectedName, item.Name);
    }

    [Fact]
    public void Name_WithLeadingAndTrailingWhitespace_ShouldTrimValue()
    {
        // Arrange
        var item = new ShoppingItem();
        var nameWithWhitespace = "  Bread  ";
        var expectedName = "Bread";

        // Act
        item.Name = nameWithWhitespace;

        // Assert
        Assert.Equal(expectedName, item.Name);
    }

    [Fact]
    public void Name_WithNull_ShouldThrowArgumentException()
    {
        // Arrange
        var item = new ShoppingItem();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => item.Name = null!);
        Assert.Contains("Name cannot be null, empty, or whitespace", exception.Message);
        Assert.Equal("name", exception.ParamName);
    }

    [Fact]
    public void Name_WithEmptyString_ShouldThrowArgumentException()
    {
        // Arrange
        var item = new ShoppingItem();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => item.Name = string.Empty);
        Assert.Contains("Name cannot be null, empty, or whitespace", exception.Message);
    }

    [Fact]
    public void Name_WithWhitespaceOnly_ShouldThrowArgumentException()
    {
        // Arrange
        var item = new ShoppingItem();

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => item.Name = "   ");
        Assert.Contains("Name cannot be null, empty, or whitespace", exception.Message);
    }

    #endregion

    #region Quantity Validation Tests

    [Fact]
    public void Quantity_WithValidPositiveValue_ShouldSetQuantity()
    {
        // Arrange
        var item = new ShoppingItem();
        var expectedQuantity = 5;

        // Act
        item.Quantity = expectedQuantity;

        // Assert
        Assert.Equal(expectedQuantity, item.Quantity);
    }

    [Fact]
    public void Quantity_WithOne_ShouldSetQuantity()
    {
        // Arrange
        var item = new ShoppingItem();

        // Act
        item.Quantity = 1;

        // Assert
        Assert.Equal(1, item.Quantity);
    }

    [Fact]
    public void Quantity_WithZero_ShouldDefaultToOne()
    {
        // Arrange
        var item = new ShoppingItem();

        // Act
        item.Quantity = 0;

        // Assert
        Assert.Equal(1, item.Quantity);
    }

    [Fact]
    public void Quantity_WithNegativeValue_ShouldDefaultToOne()
    {
        // Arrange
        var item = new ShoppingItem();

        // Act
        item.Quantity = -5;

        // Assert
        Assert.Equal(1, item.Quantity);
    }

    [Fact]
    public void Quantity_WithLargeValue_ShouldSetQuantity()
    {
        // Arrange
        var item = new ShoppingItem();
        var largeQuantity = 1000;

        // Act
        item.Quantity = largeQuantity;

        // Assert
        Assert.Equal(largeQuantity, item.Quantity);
    }

    #endregion

    #region IsPurchased Tests

    [Fact]
    public void IsPurchased_DefaultValue_ShouldBeFalse()
    {
        // Arrange & Act
        var item = new ShoppingItem();

        // Assert
        Assert.False(item.IsPurchased);
    }

    [Fact]
    public void IsPurchased_CanBeSetToTrue()
    {
        // Arrange
        var item = new ShoppingItem();

        // Act
        item.IsPurchased = true;

        // Assert
        Assert.True(item.IsPurchased);
    }

    [Fact]
    public void IsPurchased_CanBeToggled()
    {
        // Arrange
        var item = new ShoppingItem();
        var originalValue = item.IsPurchased;

        // Act
        item.IsPurchased = !item.IsPurchased;
        var toggledValue = item.IsPurchased;
        item.IsPurchased = !item.IsPurchased;
        var toggledBackValue = item.IsPurchased;

        // Assert
        Assert.False(originalValue);
        Assert.True(toggledValue);
        Assert.False(toggledBackValue);
    }

    #endregion

    #region Integration Tests

    [Fact]
    public void ShoppingItem_WithAllValidProperties_ShouldCreateSuccessfully()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var name = "Apples";
        var quantity = 10;
        var notes = "Pink Lady";

        // Act
        var item = new ShoppingItem
        {
            Id = id,
            Name = name,
            Quantity = quantity,
            Notes = notes
        };

        // Assert
        Assert.Equal(id, item.Id);
        Assert.Equal(name, item.Name);
        Assert.Equal(quantity, item.Quantity);
        Assert.Equal(notes, item.Notes);
        Assert.False(item.IsPurchased);
    }

    [Fact]
    public void ShoppingItem_WithAutoTrimming_ShouldTrimName()
    {
        // Arrange & Act
        var item = new ShoppingItem
        {
            Name = "  Milk  ",
            Notes = "  Organic  "
        };

        // Assert
        Assert.Equal("Milk", item.Name);
        Assert.Equal("  Organic  ", item.Notes); // Notes are not trimmed
    }

    #endregion

    #region Tests

    [Theory]
    [ClassData(typeof(TestDataShoppingList))]
    public void Testing_CreatesValuedMultiple(string name, int amount, string desc)
    {
        // Arrange
        var service = new ShoppingListService();
        var expected = new ShoppingItem()
        {
            Name = "Apples",
            Notes = "Pink Lady",
            Quantity = 10
        };
        // Act
        var actual = service.Add(name, amount, desc); 

        // Assert
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Notes, actual.Notes);
        Assert.Equal(expected.Quantity, actual.Quantity);
    }

    [Fact]
    public void Testing_CreatesValuedItem()
    {
        // Arrange
        var service = new ShoppingListService();
        var expected = new ShoppingItem()
        {
            Name = "Apples",
            Notes = "Pink Lady",
            Quantity = 10
        };
        // Act
        var actual = service.Add("Apples", 10, "Pink Lady"); 

        // Assert
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Notes, actual.Notes);
        Assert.Equal(expected.Quantity, actual.Quantity);
    }
    
    
    [Fact]
    public void Testing_SaveObjectToList()
    {
        // Arrange
        var sut = new ShoppingListService();
        var expected = sut.Add("Appeles", 10, "Pink Lady");

        // Act
        var result = sut.GetAll();

        // Assert

        Assert.Contains(expected, result);
        

    }
    [Fact]
    public void Has_Guid_CorrectId()
    {
        //Arrange
        var sut = new ShoppingListService();
        var result = sut.Add("Appeles", 10, "Pink Lady");
        
        //Act
        var expected = sut.GetById(result.Id);
        
        //Assert
        Assert.Equal(expected.Id, result.Id);
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(0)]
    [InlineData(100)]
    public void ArryaIsEmpty(int value)
    {
        //Arrange
        var sut = new ShoppingListService();
        
        for (int i = 0; i < value; i++)
        {
            sut.Add("Appeles", 10, "Pink Lady");
            
        }

        var resultItems = sut.GetAll();
        var result = resultItems[0];
        
        //Act
        var expected = sut.GetById(result.Id);
        
        //Assert
        Assert.Equal(expected.Id, result.Id);
    }
    


    
    #endregion
    
    
    
    
}
