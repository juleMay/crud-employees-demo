using WebApi.Domain.Primitives;

namespace WebApi.Domain.Entities;

public class Role(Guid id, string name) : Entity<Guid>(id)
{
    public string Name { get; private set; } = name;

    public IReadOnlyCollection<User> Users => _users;
    private readonly List<User> _users = [];

    public void AuthorizeUser(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        _users.Add(user);
    }
}
