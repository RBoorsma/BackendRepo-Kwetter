using AutoMapper;
using Isopoh.Cryptography.Argon2;
using Kwetter.Library.Messaging.Datatypes;
using UserService.Core.Contracts;
using UserService.Core.ViewModel;
using UserService.Core.ViewModel.RequestBody;
using UserService.Core.ViewModel.ResponseBody;
using UserService.Core.Messaging;
using UserService.DAL.Exceptions;
using UserService.DAL.Model;
using UserService.DAL.Repository;
using UserService.Core.Messaging.Handler;
using UserService.Core.Messaging.Models;

namespace UserService.Core.Services;

public class UserServiceCore(
    IUserRepository userRepository,
    IMapper mapper,
    IUserMessageHandler messageHandler) : IUserService
{
    public async Task<bool> Create(RegisterRequestBody model)
    {
        try
        {
            model.Password = Argon2.Hash(model.Password);
            UserModel userModel = mapper.Map<UserModel>(model);
            return await userRepository.Create(userModel);
            
        }
        catch
        {
            return false;
        }
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