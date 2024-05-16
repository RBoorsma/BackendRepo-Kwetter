using UserService.DAL.Model;

namespace UserService.DAL.Repository;

public interface IUserRepository
{
    Task<bool> Create(UserModel userModel);
    Task<UserModel?> GetUserByLogin(UserModel userModel);
}