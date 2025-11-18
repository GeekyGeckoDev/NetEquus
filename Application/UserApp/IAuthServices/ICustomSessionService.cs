namespace Application.UserApp.AuthServices
{
    public interface ICustomSessionService
    {
        Task<string> GetItemAsStringAsync(string key);
        Task SetItemAsStringAsync(string key, string value);
        Task RemoveItemAsync(string key);
    }
}
