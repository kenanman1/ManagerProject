namespace Services;

public interface IUsersActivityService
{
    Task UpdateTimesCreated(string userEmail);
    Task UpdateTimesEdited(string userEmail);
    Task UpdateTimesDeleted(string userEmail);
}
