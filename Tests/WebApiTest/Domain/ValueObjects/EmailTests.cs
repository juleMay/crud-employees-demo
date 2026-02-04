using FluentAssertions;
using WebApi.Domain.ValueObjects;
using WebApi.Infrastructure.Exceptions;

namespace WebApiTest.Domain.ValueObjects;

public class EmailTests
{
    [Fact]
    public void Create_ValidEmail_ShouldReturnEmailInstance()
    {
        #region Arrange
        var validEmail = "test@example.com";

        #endregion

        #region Act
        var email = Email.Create(validEmail);

        #endregion

        #region Assert
        email.Should().NotBeNull();
        email.Address.Should().Be(validEmail);
        #endregion
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData("invalid-email")]
    [InlineData("@example.com")]
    [InlineData("test@")]
    [InlineData("test@.com")]
    [InlineData("test@@example.com")]
    [InlineData(null)]
    public void Create_InvalidEmailFormat_ShouldThrowValidationException(string invalidEmail)
    {
        #region Act
        var email = () => Email.Create(invalidEmail);

        #endregion

        #region Assert
        email.Should().Throw<ValidationAppException>();
        #endregion
    }

    [Theory]
    [InlineData("Test@Example.COM", "test@example.com")]
    [InlineData("  user@domain.com  ", "user@domain.com")]
    [InlineData("USER@DOMAIN.COM", "user@domain.com")]
    [InlineData("  MixedCase@Example.Com  ", "mixedcase@example.com")]
    public void Create_EmailWithWhitespaceOrMixedCase_ShouldNormalizeToLowerCaseAndTrim(
        string inputEmail,
        string expectedEmail)
    {
        #region Act
        var email = Email.Create(inputEmail);

        #endregion

        #region Assert
        email.Address.Should().Be(expectedEmail);
        #endregion
    }

    [Theory]
    [InlineData("test@example.com")]
    [InlineData("user.name@example.com")]
    [InlineData("user+tag@example.co.uk")]
    [InlineData("test_user123@sub.domain.example.com")]
    [InlineData("a@b.co")]
    public void Create_VariousValidEmailFormats_ShouldSucceed(string validEmail)
    {
        #region Act
        var email = Email.Create(validEmail);

        #endregion

        #region Assert
        email.Address.Should().Be(validEmail.ToLowerInvariant());
        #endregion
    }

    [Fact]
    public void ToString_ShouldReturnEmailAddress()
    {
        #region Arrange
        var emailAddress = "test@example.com";
        var email = Email.Create(emailAddress);

        #endregion

        #region Act
        var result = email.ToString();

        #endregion

        #region Assert
        result.Should().Be(emailAddress);
        #endregion
    }

    [Fact]
    public void Equals_SameEmailAddress_ShouldBeEqual()
    {
        #region Arrange
        var email1 = Email.Create("test@example.com");
        var email2 = Email.Create("test@example.com");

        #endregion

        #region Act & Assert
        email1.Should().Be(email2);
        email1.Equals(email2).Should().BeTrue();
        #endregion
    }

    [Fact]
    public void Equals_DifferentEmailAddresses_ShouldNotBeEqual()
    {
        #region Arrange
        var email1 = Email.Create("test1@example.com");
        var email2 = Email.Create("test2@example.com");

        #endregion

        #region Act & Assert
        email1.Should().NotBe(email2);
        (!email1.Equals(email2)).Should().BeTrue();
        #endregion
    }

    [Fact]
    public void Equals_SameEmailDifferentCase_ShouldBeEqual()
    {
        #region Arrange
        var email1 = Email.Create("Test@Example.COM");
        var email2 = Email.Create("test@example.com");

        #endregion

        #region Act & Assert
        email1.Should().Be(email2);
        #endregion
    }

    [Fact]
    public void GetHashCode_SameEmailAddress_ShouldReturnSameHashCode()
    {
        #region Arrange
        var email1 = Email.Create("test@example.com");
        var email2 = Email.Create("test@example.com");

        #endregion

        #region Act
        var hash1 = email1.GetHashCode();
        var hash2 = email2.GetHashCode();

        #endregion

        #region Assert
        hash1.Should().Be(hash2);
        #endregion
    }

    [Fact]
    public void GetHashCode_DifferentEmailAddresses_ShouldReturnDifferentHashCodes()
    {
        #region Arrange
        var email1 = Email.Create("test1@example.com");
        var email2 = Email.Create("test2@example.com");

        #endregion

        #region Act
        var hash1 = email1.GetHashCode();
        var hash2 = email2.GetHashCode();

        #endregion

        #region Assert
        hash1.Should().NotBe(hash2);
        #endregion
    }

    [Fact]
    public void Equals_CompareWithNull_ShouldNotBeEqual()
    {
        #region Arrange
        var email = Email.Create("test@example.com");

        #endregion

        #region Act & Assert
        email.Should().NotBeNull();
        email.Equals(null).Should().BeFalse();
        #endregion
    }

    [Fact]
    public void Equals_CompareWithDifferentType_ShouldNotBeEqual()
    {
        #region Arrange
        var email = Email.Create("test@example.com");
        var differentObject = "test@example.com";

        #endregion

        #region Act & Assert
        email.Equals(differentObject).Should().BeFalse();
        #endregion
    }
}