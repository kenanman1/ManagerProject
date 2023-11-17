using Services.DTO;

namespace Services;

public enum SortOrder
{
    ASC, DESC
}
public interface IPersonService
{
    Task<PersonResponce> AddPerson(PersonAddRequest request);
    Task<List<PersonResponce>> GetAllPersons();
    Task<PersonResponce?> GetPerson(Guid id);
    Task<List<PersonResponce>> GetFilteredByAny(string filter, string ?obj);
    List<PersonResponce> GetSorted(List<PersonResponce> people, string filter, SortOrder order = SortOrder.ASC);
    Task<PersonResponce> UpdatePerson(PersonUpdateRequest request);
    Task<bool> DeletePerson(Guid id);
    Task<bool> CheckEmailExists(string email);
}
