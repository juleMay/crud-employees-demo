using FluentAssertions;
using WebApi.Domain.ValueObjects;
using WebApi.Infrastructure.Exceptions;

namespace WebApiTest.Domain.ValueObjects;

public class PhoneNumberTests
{
    [Fact]
    public void Create_ValidPhoneNumber_ShouldReturnPhoneNumberInstance()
    {
        #region Arrange
        var validNumber = "123-456-7890";

        #endregion

        #region Act
        var phoneNumber = PhoneNumber.Create(validNumber);

        #endregion

        #region Assert
        phoneNumber.Should().NotBeNull();
        phoneNumber.Number.Should().Be(validNumber);
        #endregion
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData(null)]
    public void Create_EmptyOrWhitespacePhoneNumber_ShouldThrowValidationException(string invalidNumber)
    {

        #region Act
        var act = () => PhoneNumber.Create(invalidNumber);

        #endregion

        #region Assert
        act.Should().Throw<ValidationAppException>();
        #endregion
    }

    [Theory]
    [InlineData("123-456-7890")]
    [InlineData("(123) 456-7890")]
    [InlineData("+1-123-456-7890")]
    [InlineData("1234567890")]
    [InlineData("+57 300 123 4567")]
    [InlineData("300.123.4567")]
    [InlineData("123 456 7890")]
    public void Create_VariousPhoneNumberFormats_ShouldSucceed(string phoneNumber)
    {

        #region Act
        var result = PhoneNumber.Create(phoneNumber);

        #endregion

        #region Assert
        result.Number.Should().Be(phoneNumber);
        #endregion
    }

    [Fact]
    public void Create_PhoneNumberWithSpaces_ShouldPreserveSpaces()
    {
        #region Arrange
        var numberWithSpaces = "123 456 7890";

        #endregion

        #region Act
        var phoneNumber = PhoneNumber.Create(numberWithSpaces);

        #endregion

        #region Assert
        phoneNumber.Number.Should().Be(numberWithSpaces);
        #endregion
    }

    [Fact]
    public void Create_PhoneNumberWithSpecialCharacters_ShouldPreserveFormat()
    {
        #region Arrange
        var numberWithSpecialChars = "+1 (123) 456-7890 ext. 123";

        #endregion

        #region Act
        var phoneNumber = PhoneNumber.Create(numberWithSpecialChars);

        #endregion

        #region Assert
        phoneNumber.Number.Should().Be(numberWithSpecialChars);
        #endregion
    }

    [Fact]
    public void Empty_ShouldReturnDefaultPhoneNumber()
    {

        #region Act
        var emptyPhoneNumber = PhoneNumber.Empty();

        #endregion

        #region Assert
        emptyPhoneNumber.Should().NotBeNull();
        emptyPhoneNumber.Number.Should().Be("000.000.000");
        #endregion
    }

    [Fact]
    public void Empty_CalledMultipleTimes_ShouldReturnDifferentInstances()
    {
        #region Act
        var empty1 = PhoneNumber.Empty();
        var empty2 = PhoneNumber.Empty();

        #endregion

        #region Assert
        empty1.Should().NotBeSameAs(empty2);
        empty1.Should().Be(empty2);
        #endregion
    }

    [Fact]
    public void ToString_ShouldReturnPhoneNumber()
    {
        #region Arrange
        var number = "123-456-7890";
        var phoneNumber = PhoneNumber.Create(number);

        #endregion

        #region Act
        var result = phoneNumber.ToString();

        #endregion

        #region Assert
        result.Should().Be(number);
        #endregion
    }

    [Fact]
    public void ToString_EmptyPhoneNumber_ShouldReturnDefaultValue()
    {
        #region Arrange
        var phoneNumber = PhoneNumber.Empty();

        #endregion

        #region Act
        var result = phoneNumber.ToString();

        #endregion

        #region Assert
        result.Should().Be("000.000.000");
        #endregion
    }

    [Fact]
    public void Equals_SamePhoneNumber_ShouldBeEqual()
    {
        #region Arrange
        var phoneNumber1 = PhoneNumber.Create("123-456-7890");
        var phoneNumber2 = PhoneNumber.Create("123-456-7890");

        #endregion

        #region Act & Assert
        phoneNumber1.Should().Be(phoneNumber2);
        phoneNumber1.Equals(phoneNumber2).Should().BeTrue();
        #endregion
    }

    [Fact]
    public void Equals_DifferentPhoneNumbers_ShouldNotBeEqual()
    {
        #region Arrange
        var phoneNumber1 = PhoneNumber.Create("123-456-7890");
        var phoneNumber2 = PhoneNumber.Create("098-765-4321");

        #endregion

        #region Act & Assert
        phoneNumber1.Should().NotBe(phoneNumber2);
        (!phoneNumber1.Equals(phoneNumber2)).Should().BeTrue();
        #endregion
    }

    [Fact]
    public void Equals_SameNumberDifferentFormat_ShouldNotBeEqual()
    {
        #region Arrange
        var phoneNumber1 = PhoneNumber.Create("1234567890");
        var phoneNumber2 = PhoneNumber.Create("123-456-7890");

        #endregion

        #region Act & Assert
        phoneNumber1.Should().NotBe(phoneNumber2);
        #endregion
    }

    [Fact]
    public void Equals_EmptyPhoneNumbers_ShouldBeEqual()
    {
        #region Arrange
        var empty1 = PhoneNumber.Empty();
        var empty2 = PhoneNumber.Empty();

        #endregion

        #region Act & Assert
        empty1.Should().Be(empty2);
        #endregion
    }

    [Fact]
    public void GetHashCode_SamePhoneNumber_ShouldReturnSameHashCode()
    {
        #region Arrange
        var phoneNumber1 = PhoneNumber.Create("123-456-7890");
        var phoneNumber2 = PhoneNumber.Create("123-456-7890");

        #endregion

        #region Act
        var hash1 = phoneNumber1.GetHashCode();
        var hash2 = phoneNumber2.GetHashCode();

        #endregion

        #region Assert
        hash1.Should().Be(hash2);
        #endregion
    }

    [Fact]
    public void GetHashCode_DifferentPhoneNumbers_ShouldReturnDifferentHashCodes()
    {
        #region Arrange
        var phoneNumber1 = PhoneNumber.Create("123-456-7890");
        var phoneNumber2 = PhoneNumber.Create("098-765-4321");

        #endregion

        #region Act
        var hash1 = phoneNumber1.GetHashCode();
        var hash2 = phoneNumber2.GetHashCode();

        #endregion

        #region Assert
        hash1.Should().NotBe(hash2);
        #endregion
    }

    [Fact]
    public void Equals_CompareWithNull_ShouldNotBeEqual()
    {
        #region Arrange
        var phoneNumber = PhoneNumber.Create("123-456-7890");

        #endregion

        #region Act & Assert
        phoneNumber.Should().NotBeNull();
        phoneNumber.Equals(null).Should().BeFalse();
        #endregion
    }

    [Fact]
    public void Equals_CompareWithDifferentType_ShouldNotBeEqual()
    {
        #region Arrange
        var phoneNumber = PhoneNumber.Create("123-456-7890");
        var differentObject = "123-456-7890";

        #endregion

        #region Act & Assert
        phoneNumber.Equals(differentObject).Should().BeFalse();
        #endregion
    }

    [Fact]
    public void Create_PhoneNumberWithOnlyNumbers_ShouldSucceed()
    {
        #region Arrange
        var numbersOnly = "1234567890";
        #endregion

        #region Act
        var phoneNumber = PhoneNumber.Create(numbersOnly);
        #endregion

        #region Assert
        phoneNumber.Number.Should().Be(numbersOnly);
        #endregion
    }

    [Theory]
    [InlineData("123-456-7890", "123-456-7890", true)]
    [InlineData("123-456-7890", "098-765-4321", false)]
    [InlineData("1234567890", "1234567890", true)]
    [InlineData("000.000.000", "000.000.000", true)]
    public void Equality_VariousScenarios_ShouldBehaveCorrectly(
        string number1,
        string number2,
        bool shouldBeEqual)
    {
        #region Arrange
        var phoneNumber1 = PhoneNumber.Create(number1);
        var phoneNumber2 = PhoneNumber.Create(number2);
        #endregion

        #region Act & Assert
        if (shouldBeEqual)
        {
            phoneNumber1.Should().Be(phoneNumber2);
            phoneNumber1.Equals(phoneNumber2).Should().BeTrue();
        }
        else
        {
            phoneNumber1.Should().NotBe(phoneNumber2);
            (!phoneNumber1.Equals(phoneNumber2)).Should().BeTrue();
        }
        #endregion
    }

    [Fact]
    public void Create_VeryLongPhoneNumber_ShouldSucceed()
    {
        #region Arrange
        var longNumber = "+1 (123) 456-7890 ext. 12345";
        #endregion

        #region Act
        var phoneNumber = PhoneNumber.Create(longNumber);
        #endregion

        #region Assert
        phoneNumber.Number.Should().Be(longNumber);
        #endregion
    }


    [Fact]
    public void Empty_CreatedPhoneNumber_ShouldEqualAnotherDefaultPhoneNumber()
    {
        #region Arrange
        var emptyPhone = PhoneNumber.Empty();
        var defaultPhone = PhoneNumber.Create("000.000.000");
        #endregion

        #region Act & Assert
        emptyPhone.Should().Be(defaultPhone);
        #endregion
    }
}