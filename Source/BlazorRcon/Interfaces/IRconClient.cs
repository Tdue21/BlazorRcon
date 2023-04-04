using BlazorRcon.Models;

namespace BlazorRcon.Interfaces;

public interface IRconClient
{
    Task<string?> ExecuteCommand(RconData data);
}