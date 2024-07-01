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

        await using var transaction = await userDbContext.Database.BeginTransactionAsync();

        try
        {
            await userDbContext.AddAsync(userModel);
            await userDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return false;
        }
    }

    public async Task<UserModel?> GetUserByLogin(UserModel userModel) //
    {
        return await userDbContext.User.FirstOrDefaultAsync(x => x.Email == userModel.Email);
    }

    public async Task<bool> RollBackOrDeleteUserAsync(Guid guid)
    {
        UserModel? userModel = await userDbContext.User.FirstOrDefaultAsync(x => x.UserID == guid);
        if (userModel == null)
        {
            throw new NoRecordFoundException();
        }

        await using var transaction = await userDbContext.Database.BeginTransactionAsync();

        try
        {
            userDbContext.Remove(userModel);
            await userDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return false;
        }
    }
}