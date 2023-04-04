using BlazorRcon.Interfaces;
using BlazorRcon.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BlazorRcon.ViewModels;

public class MainViewModel
{
    private readonly IRconClient _rconClient;
    private readonly ProtectedBrowserStorage _storage;

    public MainViewModel(IRconClient rconClient, ProtectedBrowserStorage storage)
    {
        _rconClient = rconClient ?? throw new ArgumentNullException(nameof(rconClient));
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public async Task<string?> SendCommand(RconData data)
    {
        var result = await _rconClient.ExecuteCommand(data);
        await SaveStateAsync(data);
        return result;
    }

    public async Task<RconData> LoadStateAsync()
    {
        var data = await _storage.GetAsync<RconData>("RconData");
        return data.Success && data.Value != null
                   ? data.Value
                   : RconData.Default();
    }

    private async Task SaveStateAsync(RconData data)
    {
        await _storage.SetAsync("RconData", data);
    }
}