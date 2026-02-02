using System.Net.Mail;
using WebApi.Domain.ValueObjects.Contracts;
using WebApi.Infrastructure.Exceptions;

namespace WebApi.Domain.ValueObjects;

public sealed class Email : ValueObject
{
    public string Address { get; }

    private Email(string address)
    {
        Address = address;
    }

    public static Email Create(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ValidationAppException("Email address cannot be empty");
        }
        var addressFormated = address.Trim().ToLowerInvariant();
        try
        {
            var mailAddress = new MailAddress(addressFormated);
            return new Email(mailAddress.Address);
        }
        catch (FormatException)
        {
            throw new ValidationAppException("Invalid email format");
        }
    }

    public override string ToString() => Address;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
    }
}
