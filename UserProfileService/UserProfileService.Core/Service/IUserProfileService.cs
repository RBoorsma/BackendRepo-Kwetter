using UserProfileService.Core.ViewModel.ResponseBody;


namespace UserProfileService.Core.Service;

public interface IUserProfileService
{
    Task Create(NewProfileRequestBody body);
    Task RollbackOrDeleteCreation(Guid UserID, Guid Correlation);
    Task RollbackOrDeleteCreation(Guid UserID);
}