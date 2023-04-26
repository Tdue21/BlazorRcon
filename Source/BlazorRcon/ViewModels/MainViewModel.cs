using BlazorRcon.Interfaces;
using BlazorRcon.Models;

namespace BlazorRcon.ViewModels;

public class MainViewModel
{
    private readonly IRconClient _rconClient;
    private readonly IBrowserStorage _storage;

    public MainViewModel(IRconClient rconClient, IBrowserStorage storage)
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
        try
        {
            var data = await _storage.GetAsync<RconData>("RconData");
            return data.Success && data.Value != null
                       ? data.Value
                       : RconData.Default();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return RconData.Default();
        }
    }

    private async Task SaveStateAsync(RconData data)
    {
        try
        {
            await _storage.SetAsync("RconData", data);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}