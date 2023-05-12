namespace BlazorRcon.Interfaces;

public interface IBrowserStorage
{
    ValueTask<TType?> GetAsync<TType>(string name);
    ValueTask SetAsync<TType>(string name, TType data);
}