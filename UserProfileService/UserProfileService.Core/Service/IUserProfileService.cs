using UserProfileService.Core.ViewModel.ResponseBody;


namespace UserProfileService.Core.Service;

public interface IUserProfileService
{
    Task<bool> Create(NewProfileRequestBody body);
    Task RollbackOrDeleteCreation(Guid UserID, Guid Correlation);
    Task<ProfileResponseBody?> GetProfile(ProfileRequestBody body);
    Task<ProfileResponseBody> LoadTestDemo(ProfileRequestBody body);
    Task<string> Serverless();
    Task<ProfileResponseBody?> GetProfileByUserID(ProfileByUserRequestBody body);

}