using TorcLib.Application.DataTransferObjects;
using TorcLib.Domain.Aggregates;
using TorcLib.Domain.Aggregates.Repositories;
using TorcLib.Domain.Exceptions;

namespace TorcLib.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public User Get(UserDto user)
    {
        var logedUser = _repository.Get(user.UserName, user.Password);
        if (logedUser == null) throw new LibraryNotFoundException("User not found, user or password mismatch");

        return logedUser;
    }
}

public interface IUserService
{
    User Get(UserDto user);
}