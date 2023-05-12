using BlazorRcon.Interfaces;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BlazorRcon.Services;

public class BrowserStorage : IBrowserStorage
{
    private readonly ProtectedBrowserStorage _storage;

    public BrowserStorage(ProtectedBrowserStorage storage)
    {
        _storage = storage ?? throw new ArgumentNullException(nameof(storage));
    }

    public async ValueTask<TType?> GetAsync<TType>(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        var data = await _storage.GetAsync<TType>("RconData", name);
        return data.Success 
                   ? data.Value 
                   : default;
    }

    public async ValueTask SetAsync<TType>(string name, TType data)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (data == null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        await _storage.SetAsync("RconData", name, data);
    }
}