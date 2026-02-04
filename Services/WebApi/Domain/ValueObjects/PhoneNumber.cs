using WebApi.Domain.ValueObjects.Contracts;
using WebApi.Infrastructure.Exceptions;

namespace WebApi.Domain.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
    private static readonly string DEFAULT = "000.000.000";
    public string Number { get; }

    private PhoneNumber(string number)
    {
        Number = number;
    }

    public static PhoneNumber Create(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
        {
            throw new ValidationAppException("Phone number cannot be empty");
        }
        return new(number.Trim().ToLowerInvariant());
    }

    public static PhoneNumber Empty() => new(DEFAULT);

    public override string ToString() => Number;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Number;
    }
}
