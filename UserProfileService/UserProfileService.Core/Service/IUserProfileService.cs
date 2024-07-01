using UserProfileService.Core.ViewModel.ResponseBody;


namespace UserProfileService.Core.Service;

public interface IUserProfileService
{
    Task<bool> Create(NewProfileRequestBody body);
    Task RollbackOrDeleteCreation(Guid UserID, Guid Correlation);

}