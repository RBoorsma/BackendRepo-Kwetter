using System.Data.SqlTypes;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using UserProfileService.DAL.Context;
using UserProfileService.DAL.Model;
using System.Transactions;
using Microsoft.EntityFrameworkCore.Storage;
using UserProfileService.DAL.Exceptions;

namespace UserProfileService.DAL.Repository.Implementation;

public class UserProfileRepository(UserProfileDbContext dbContext) : IUserProfileRepository
{
    public async Task<bool> Create(UserProfile userProfile)
    {
        if (await dbContext.UserProfiles.AnyAsync(x => x.Username == userProfile.Username))
        {
            throw new UserAlreadyExistsException();
        }

        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            await dbContext.AddAsync(userProfile);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            return false;
        }
    }

    public async Task<bool> RollBackOrDeleteAsync(Guid guid)
    {
        UserProfile? profile = await dbContext.UserProfiles.FirstOrDefaultAsync(x => x.UserID == guid);
        if (profile == null)
            throw new NoRecordFoundException();

        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            dbContext.Remove(profile);
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception E)
        {
            await transaction.RollbackAsync();
            return false;
        }
    }
}