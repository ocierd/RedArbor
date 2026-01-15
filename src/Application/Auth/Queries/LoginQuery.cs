namespace RedArbor.Application.Auth.Queries;
 /// <summary>
 /// Query to login a user.
 /// </summary>
 /// <param name="Username">Username of the user</param>
 /// <param name="Password">Password of the user</param>
public record LoginQuery(string Username, string Password)
: IRequest<TokenDto>;