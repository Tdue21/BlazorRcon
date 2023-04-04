namespace BlazorRcon.Models;

public class RconData
{
    public RconData() : this("localhost", 25575,"", "help") { }

    public RconData(string? host, int? port, string? password, string? lastCommand)
    {
        Host        = host;
        Port        = port;
        Password    = password;
        LastCommand = lastCommand;
    }

    public string? Host { get; set; }
    public int? Port { get; set; }
    public string? Password { get; set; }
    public string? LastCommand { get; set; }
    public static RconData Default() => new();
}