namespace RedArbor.Application.Common.Interfaces;

public interface IUser
{
    string UserId { get; }

    string UserName { get; }

    IEnumerable<string> Roles { get; }
}
