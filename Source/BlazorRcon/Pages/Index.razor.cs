using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor;
using System.Globalization;
using BlazorRcon.Interfaces;
using BlazorRcon.Shared;
using Flurl;
using Flurl.Http;

namespace BlazorRcon.Pages;

public partial class Index
{
    private string? _host;
    private int? _port;
    private string? _password;
    private string? _command;
    private bool _processing;

    [Inject] private ILocalizationService Localizer { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ProtectedLocalStorage LocalStorage { get; set; } = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var test = CultureInfo.CurrentCulture.ToString();
            await LoadStateAsync();
            StateHasChanged();
        }
    }

    private async Task SendCommand()
    {
        string? result;
        _processing = true;
        try
        {
            result = await "https://conan-rcon.azurewebsites.net/api/conan-rcon"
                               .SetQueryParams(new
                               {
                                   host = _host,
                                   port = _port,
                                   pwd = _password,
                                   command = _command
                               })
                               .GetStringAsync();
            await SaveStateAsync();
        }
        finally
        {
            _processing = false;
        }

        var options = new DialogOptions
        {
            CloseOnEscapeKey = true,
            CloseButton = true,
            Position = DialogPosition.Center,
            FullWidth = true
        };
        var parameters = new DialogParameters { { "Content", result } };
        DialogService.Show<ResultDialog>(Localizer["RconResponse"], parameters, options);
    }

    private async Task LoadStateAsync()
    {
        var data = await LocalStorage.GetAsync<RconData>("RconData");
        if (data.Success && data.Value != null)
        {
            _host = data.Value.Host;
            _port = data.Value.Port;
            _password = data.Value.Password;
            _command = data.Value.LastCommand;
        }
        else
        {
            _host = "127.0.0.1";
            _port = 25575;
            _password = "";
            _command = "help";
        }
    }

    private async Task SaveStateAsync()
    {
        var data = new RconData(_host, _port, _password, _command);
        await LocalStorage.SetAsync("RconData", data);
    }

    public record RconData(string? Host, int? Port, string? Password, string? LastCommand) { }

    public struct Const
    {
        public const string Title = "Conan Exiles - Online RCON Client";
        public const string Clear = "Clear";
        public const string Command = "Command";
        public const string CommandHelp = "Enter RCON command";
        public const string Host = "Host";
        public const string HostHelp = "Enter name or address of host";
        public const string Ok = "Ok";
        public const string Password = "Password";
        public const string PasswordHelp = "Enter RCON password for host";
        public const string Port = "Port";
        public const string PortHelp = "Enter port of host";
        public const string Processing = "Processing...";
        public const string RconResponse = "RCON response";
        public const string Send = "Send";
    }
}