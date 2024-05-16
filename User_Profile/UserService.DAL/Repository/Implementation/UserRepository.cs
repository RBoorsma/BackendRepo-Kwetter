using System.Data.SqlTypes;
using Microsoft.EntityFrameworkCore;
using UserService.DAL.Context;
using UserService.DAL.Exceptions;
using UserService.DAL.Model;

namespace UserService.DAL.Repository;

public class UserRepository(UserDbContext userDbContext) : IUserRepository
{
    public async Task<bool> Create(UserModel userModel)
    {
        if (await userDbContext.User.AnyAsync(x => x.Email == userModel.Email || x.Username == userModel.Username))
        {
            throw new UserAlreadyExistsException();
        }
        using var transaction = userDbContext.Database.BeginTransactionAsync();
        {
            try
            {
                await userDbContext.AddAsync(userModel);
                await userDbContext.SaveChangesAsync();
                await transaction.Result.CommitAsync();
            }
            catch (Exception e)
            {
                await transaction.Result.RollbackAsync();
                return false;
            }
        }
        return true;
    }

    public async Task<UserModel?> GetUserByLogin(UserModel userModel) //
    {
        return await userDbContext.User.FirstOrDefaultAsync(x => x.Email == userModel.Email);
    }

    public async Task<bool> RollBackOrDeleteUserAsync(UserModel userModel)
    {
        try
        {
            userDbContext.User.Remove(userModel);
            await userDbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        
    }
}