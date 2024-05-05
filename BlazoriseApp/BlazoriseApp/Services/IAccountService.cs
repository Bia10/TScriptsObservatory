using BlazoriseApp.Models.DTO;

namespace BlazoriseApp.Services;

public interface IAccountService
{
    Task<string> GetUsernameAsync();
    Task<string> GetPasswordAsync();
    Task<List<AccountDTO>> GetLoadedAccountsAsync();
}