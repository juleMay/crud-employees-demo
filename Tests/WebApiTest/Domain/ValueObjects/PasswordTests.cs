using FluentAssertions;
using WebApi.Domain.ValueObjects;
using WebApi.Infrastructure.Exceptions;

namespace WebApiTest.Domain.ValueObjects;

public class PasswordTests
{
    [Fact]
    public void Create_ValidPassword_ShouldReturnPasswordInstance()
    {
        #region Arrange
        var validPassword = "SecurePassword123";
        #endregion

        #region Act
        var password = Password.Create(validPassword);

        #endregion

        #region Assert
        password.Should().NotBeNull();
        password.Hash.Should().NotBeNullOrEmpty();
        #endregion
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData("\t")]
    [InlineData("\n")]
    [InlineData(null)]
    public void Create_EmptyOrWhitespacePassword_ShouldThrowValidationException(string invalidPassword)
    {
        #region Act
        var act = () => Password.Create(invalidPassword);

        #endregion

        #region Assert
        act.Should().Throw<ValidationAppException>();
        #endregion
    }

    [Theory]
    [InlineData("12345")]
    [InlineData("abc")]
    [InlineData("a")]
    [InlineData("")]
    [InlineData("pass")]
    public void Create_PasswordLessThan6Characters_ShouldThrowValidationException(string shortPassword)
    {
        #region Act
        var act = () => Password.Create(shortPassword);

        #endregion

        #region Assert
        act.Should().Throw<ValidationAppException>();
        #endregion
    }

    [Theory]
    [InlineData("123456")]
    [InlineData("password")]
    [InlineData("SecurePass123")]
    [InlineData("VeryLongPasswordWith$pecialCh@racters123!")]
    public void Create_PasswordWithAtLeast6Characters_ShouldSucceed(string validPassword)
    {
        #region Act
        var password = Password.Create(validPassword);
        #endregion

        #region Assert
        password.Should().NotBeNull();
        password.Hash.Should().NotBeNullOrEmpty();
        #endregion
    }

    [Fact]
    public void Create_ExactlyMinimumLength_ShouldSucceed()
    {
        #region Arrange
        var minimumPassword = "123456";
        #endregion

        #region Act
        var password = Password.Create(minimumPassword);
        #endregion

        #region Assert
        password.Should().NotBeNull();
        password.Hash.Should().NotBeNullOrEmpty();
        #endregion
    }

    [Fact]
    public void Create_ShouldHashPasswordToBase64()
    {
        #region Arrange
        var plainText = "MyPassword123";
        #endregion

        #region Act
        var password = Password.Create(plainText);
        #endregion

        #region Assert
        password.Hash.Should().NotBe(plainText);
        var act = () => Convert.FromBase64String(password.Hash);
        act.Should().NotThrow();
        #endregion
    }

    [Fact]
    public void Create_SamePassword_ShouldProduceSameHash()
    {
        #region Arrange
        var plainText = "MyPassword123";
        #endregion

        #region Act
        var password1 = Password.Create(plainText);
        var password2 = Password.Create(plainText);

        #endregion

        #region Assert
        password1.Hash.Should().Be(password2.Hash);
        #endregion
    }

    [Fact]
    public void Create_DifferentPasswords_ShouldProduceDifferentHashes()
    {
        #region Arrange
        var password1Text = "Password123";
        var password2Text = "DifferentPass456";
        #endregion

        #region Act
        var password1 = Password.Create(password1Text);
        var password2 = Password.Create(password2Text);

        #endregion

        #region Assert
        password1.Hash.Should().NotBe(password2.Hash);
        #endregion
    }

    [Fact]
    public void Create_CaseSensitivePasswords_ShouldProduceDifferentHashes()
    {
        #region Arrange
        var lowerCase = "password";
        var upperCase = "PASSWORD";
        #endregion

        #region Act
        var password1 = Password.Create(lowerCase);
        var password2 = Password.Create(upperCase);

        #endregion

        #region Assert
        password1.Hash.Should().NotBe(password2.Hash);
        #endregion
    }

    [Fact]
    public void Create_PasswordWithSpecialCharacters_ShouldSucceed()
    {
        #region Arrange
        var passwordWithSpecialChars = "P@ssw0rd!#$%";
        #endregion

        #region Act
        var password = Password.Create(passwordWithSpecialChars);

        #endregion

        #region Assert
        password.Should().NotBeNull();
        password.Hash.Should().NotBeNullOrEmpty();
        #endregion
    }

    [Fact]
    public void Create_PasswordWithUnicodeCharacters_ShouldSucceed()
    {
        #region Arrange
        var unicodePassword = "contraseña123";
        #endregion

        #region Act
        var password = Password.Create(unicodePassword);

        #endregion

        #region Assert
        password.Should().NotBeNull();
        password.Hash.Should().NotBeNullOrEmpty();
        #endregion
    }

    [Fact]
    public void ToString_ShouldReturnHash()
    {
        #region Arrange
        var plainText = "MyPassword123";
        var password = Password.Create(plainText);
        #endregion

        #region Act
        var result = password.ToString();

        #endregion

        #region Assert
        result.Should().Be(password.Hash);
        result.Should().NotBe(plainText);
        #endregion
    }

    [Fact]
    public void ToString_ShouldNotRevealPlainTextPassword()
    {
        #region Arrange
        var plainText = "SecretPassword";
        var password = Password.Create(plainText);
        #endregion

        #region Act
        var result = password.ToString();

        #endregion

        #region Assert
        result.Should().NotContain(plainText);
        #endregion
    }

    [Fact]
    public void Equals_SamePasswordHash_ShouldBeEqual()
    {
        #region Arrange
        var password1 = Password.Create("MyPassword123");
        var password2 = Password.Create("MyPassword123");
        #endregion

        #region Act & Assert
        password1.Should().Be(password2);
        password1.Equals(password2).Should().BeTrue();
        #endregion
    }

    [Fact]
    public void Equals_DifferentPasswordHashes_ShouldNotBeEqual()
    {
        #region Arrange
        var password1 = Password.Create("Password123");
        var password2 = Password.Create("DifferentPassword456");
        #endregion

        #region Act & Assert
        password1.Should().NotBe(password2);
        (!password1.Equals(password2)).Should().BeTrue();
        #endregion
    }

    [Fact]
    public void GetHashCode_SamePassword_ShouldReturnSameHashCode()
    {
        #region Arrange
        var password1 = Password.Create("MyPassword123");
        var password2 = Password.Create("MyPassword123");
        #endregion

        #region Act
        var hash1 = password1.GetHashCode();
        var hash2 = password2.GetHashCode();

        #endregion

        #region Assert
        hash1.Should().Be(hash2);
        #endregion
    }

    [Fact]
    public void GetHashCode_DifferentPasswords_ShouldReturnDifferentHashCodes()
    {
        #region Arrange
        var password1 = Password.Create("Password123");
        var password2 = Password.Create("DifferentPassword456");
        #endregion

        #region Act
        var hash1 = password1.GetHashCode();
        var hash2 = password2.GetHashCode();

        #endregion

        #region Assert
        hash1.Should().NotBe(hash2);
        #endregion
    }

    [Fact]
    public void Equals_CompareWithNull_ShouldNotBeEqual()
    {
        #region Arrange
        var password = Password.Create("MyPassword123");
        #endregion

        #region Act & Assert
        password.Should().NotBeNull();
        password.Equals(null).Should().BeFalse();
        #endregion
    }

    [Fact]
    public void Equals_CompareWithDifferentType_ShouldNotBeEqual()
    {
        #region Arrange
        var password = Password.Create("MyPassword123");
        var differentObject = "MyPassword123";
        #endregion

        #region Act & Assert
        password.Equals(differentObject).Should().BeFalse();
        #endregion
    }

    [Theory]
    [InlineData("Password123", "Password123", true)]
    [InlineData("Password123", "DifferentPass", false)]
    [InlineData("password", "PASSWORD", false)]
    [InlineData("123456", "123456", true)]
    public void Equality_VariousScenarios_ShouldBehaveCorrectly(
        string password1Text,
        string password2Text,
        bool shouldBeEqual)
    {
        #region Arrange
        var password1 = Password.Create(password1Text);
        var password2 = Password.Create(password2Text);
        #endregion

        #region Act & Assert
        if (shouldBeEqual)
        {
            password1.Should().Be(password2);
            password1.Equals(password2).Should().BeTrue();
        }
        else
        {
            password1.Should().NotBe(password2);
            (!password1.Equals(password2)).Should().BeTrue();
        }
        #endregion
    }

    [Fact]
    public void Create_VeryLongPassword_ShouldSucceed()
    {
        #region Arrange
        var longPassword = new string('a', 1000);
        #endregion

        #region Act
        var password = Password.Create(longPassword);

        #endregion

        #region Assert
        password.Should().NotBeNull();
        password.Hash.Should().NotBeNullOrEmpty();
        #endregion
    }

    [Fact]
    public void Create_PasswordWithLeadingAndTrailingSpaces_ShouldIncludeSpacesInHash()
    {
        #region Arrange
        var passwordWithSpaces = " password ";
        var passwordWithoutSpaces = "password";
        #endregion

        #region Act
        var password1 = Password.Create(passwordWithSpaces);
        var password2 = Password.Create(passwordWithoutSpaces);

        #endregion

        #region Assert
        password1.Hash.Should().NotBe(password2.Hash);
        #endregion
    }


    [Fact]
    public void Create_PasswordWithNewlineCharacters_ShouldSucceed()
    {
        #region Arrange
        var passwordWithNewline = "Pass\nword123";

        #endregion

        #region Act
        var password = Password.Create(passwordWithNewline);

        #endregion

        #region Assert
        password.Should().NotBeNull();
        password.Hash.Should().NotBeNullOrEmpty();
        #endregion
    }

    [Theory]
    [InlineData("123456")]
    [InlineData("abcdef")]
    [InlineData("ABCDEF")]
    public void Create_BoundaryLengthPasswords_ShouldSucceed(string boundaryPassword)
    {
        #region Arrange & Act
        var password = Password.Create(boundaryPassword);

        #endregion

        #region Assert
        password.Should().NotBeNull();
        boundaryPassword.Length.Should().Be(6);
        #endregion
    }
}