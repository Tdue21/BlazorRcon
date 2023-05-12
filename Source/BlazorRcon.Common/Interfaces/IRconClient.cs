using BlazorRcon.Common.Models;

namespace BlazorRcon.Common.Interfaces;

public interface IRconClient
{
    Task<string?> ExecuteCommand(RconData data);
}