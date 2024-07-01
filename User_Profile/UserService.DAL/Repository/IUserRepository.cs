using UserService.DAL.Model;

namespace UserService.DAL.Repository;

public interface IUserRepository
{
    Task<bool> RollBackOrDeleteUserAsync(Guid userModel);
    Task<bool> Create(UserModel userModel);
    Task<UserModel?> GetUserByLogin(UserModel userModel);
}