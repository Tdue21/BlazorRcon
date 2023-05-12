using BlazorRcon.Common.Interfaces;
using BlazorRcon.Common.Models;
using BlazorRcon.Interfaces;

namespace BlazorRcon.ViewModels;

public class MainViewModel
{
    private readonly ILogger<MainViewModel> _logger;
    private readonly IBrowserStorage _storage;
    private readonly IRconClient _client;

    public MainViewModel(ILogger<MainViewModel> logger, IBrowserStorage storage, IRconClient client)
    {
        _logger  = logger ?? throw new ArgumentNullException(nameof(logger));
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        _client  = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<string?> SendCommand(RconData data)
    {
        string? result = null;
        try
        {
            result = await _client.ExecuteCommand(data);
            await _storage.SetAsync("RconData", data);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error saving data.");
        }
        return result;
    }

    public async Task<RconData> LoadStateAsync()
    {
        try
        {
            var data = await _storage.GetAsync<RconData>("RconData");
            return data ?? RconData.Default();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error loading data.");
            return RconData.Default();
        }
    }
}