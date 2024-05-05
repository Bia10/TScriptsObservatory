using System.Text.Json;
using BlazoriseApp.Models.DTO;

namespace BlazoriseApp.Services;

public class AccountService : IAccountService
{
    private readonly List<AccountDTO> _accounts;
    private readonly IConfiguration _config;

    public AccountService(IConfiguration config)
    {
        _config = config;
        _accounts = [];

        var accountsFilePath = GetAccountsFilePath();
        LoadAccountsFromFileAsync(accountsFilePath).ConfigureAwait(false);
    }

    private string? GetAccountsFilePath()
        => _config["AccountsFilePath"];

    private async Task LoadAccountsFromFileAsync(string? filePath)
    {
        try
        {
            if (!File.Exists(filePath)) return;

            var json = await File.ReadAllTextAsync(filePath).ConfigureAwait(false);
            List<AccountDTO>? accounts = JsonSerializer.Deserialize<List<AccountDTO>>(json);

            if (accounts is not null)
                _accounts.AddRange(accounts);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load accounts from file: {ex.Message}");
        }
    }

    private async Task SaveAccountsToFileAsync(string filePath)
    {
        try
        {
            var json = JsonSerializer.Serialize(_accounts);
            await File.WriteAllTextAsync(filePath, json).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to save accounts to file: {ex.Message}");
        }
    }

    public Task<string> GetUsernameAsync()
    {
        if (_accounts.Count == 0) return Task.FromResult(string.Empty);

        AccountDTO account = _accounts.First();
        return Task.FromResult(account.Username);
    }

    public Task<string> GetPasswordAsync()
    {
        if (_accounts.Count == 0) return Task.FromResult(string.Empty);

        AccountDTO account = _accounts.First();
        return Task.FromResult(account.Password);
    }

    public Task<List<AccountDTO>> GetLoadedAccountsAsync()
        => Task.FromResult(_accounts);
}