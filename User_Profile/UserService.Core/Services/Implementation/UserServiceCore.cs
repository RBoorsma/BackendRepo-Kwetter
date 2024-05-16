using AutoMapper;
using Isopoh.Cryptography.Argon2;
using UserService.Core.Contracts;
using UserService.Core.ViewModel;
using UserService.Core.ViewModel.RequestBody;
using UserService.Core.ViewModel.ResponseBody;
using UserService.Core.Messaging;
using UserService.DAL.Exceptions;
using UserService.DAL.Model;
using UserService.DAL.Repository;
using UserService.Core.Messaging.Handler;

namespace UserService.Core.Services;

public class UserServiceCore(IUserRepository userRepository, IMapper mapper,  IMessageHandler rabbitMq) : IUserService
{
    public async Task Create(RegisterRequestBody model)
    {
        model.Password = Argon2.Hash(model.Password);
        UserModel userModel = new UserModel();
        mapper.Map(model, userModel);
        await userRepository.Create(userModel);
    }

    public async Task<LoginResponseBody?> GetByLogin(LoginRequestBody loginModel)
    {
        UserModel? user = mapper.Map<UserModel>(loginModel);
        if ((user = await userRepository.GetUserByLogin(user)) != null &&
            Argon2.Verify(user.Password, loginModel.Password))
        {
            return mapper.Map<LoginResponseBody>(user);
        }

        return null;
    }

    public Task RollbackOrDeleteCreation(Guid UserID, Guid Correletion)
    {
        throw new NotImplementedException();
    }

    public Task RollbackOrDeleteCreation(Guid UserID)
    {
        throw new NotImplementedException();
    }
}