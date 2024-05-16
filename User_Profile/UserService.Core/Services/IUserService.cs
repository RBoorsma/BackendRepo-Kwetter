using UserService.Core.ViewModel;
using UserService.Core.ViewModel.RequestBody;
using UserService.Core.ViewModel.ResponseBody;
using UserService.DAL.Model;

namespace UserService.Core.Services;

public interface IUserService
{

    public Task Create(RegisterRequestBody model);
    public Task<LoginResponseBody?> GetByLogin(LoginRequestBody loginModel);
    public Task RollbackOrDeleteCreation(Guid UserID, Guid Correletion);
    public Task RollbackOrDeleteCreation(Guid UserID);
}