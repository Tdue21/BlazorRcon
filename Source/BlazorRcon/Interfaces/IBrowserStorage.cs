using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BlazorRcon.Interfaces;

public interface IBrowserStorage
{
    ValueTask<ProtectedBrowserStorageResult<TType>> GetAsync<TType>(string name);
    ValueTask SetAsync<TType>(string name, TType data);
}