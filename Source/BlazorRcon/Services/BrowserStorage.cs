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

    public ValueTask<ProtectedBrowserStorageResult<TType>> GetAsync<TType>(string name) => _storage.GetAsync<TType>(name);

    public ValueTask SetAsync<TType>(string name, TType data) => _storage.SetAsync(name, data);
}