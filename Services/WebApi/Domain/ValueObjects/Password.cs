using WebApi.Domain.ValueObjects.Contracts;
using WebApi.Infrastructure.Exceptions;

namespace WebApi.Domain.ValueObjects;

public sealed class Password : ValueObject
{
    public string Hash { get; }

    private Password(string hash)
    {
        Hash = hash;
    }

    public static Password Create(string plainText)
    {
        if (string.IsNullOrWhiteSpace(plainText))
        {
            throw new ValidationAppException("Password cannot be empty");
        }
        if (plainText.Length < 6)
        {
            throw new ValidationAppException("Password must be at least 6 characters long");
        }
        var hash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(plainText));
        return new(hash);
    }

    public override string ToString() => Hash;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Hash;
    }
}
