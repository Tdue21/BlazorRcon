namespace BlazorRcon.Common.Models;

public class RconServerData
{
    public string? ServerName { get; set; }

    public RconData Data { get; set; } = new();

    public static RconServerData Default() => new();
}