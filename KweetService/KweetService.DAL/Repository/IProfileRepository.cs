using KweetService.DAL.Model;

namespace KweetService.DAL.Repository;

public interface IProfileRepository
{
    Task addProfile(Profile profile);
}