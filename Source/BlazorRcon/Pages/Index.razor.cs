using BlazorRcon.Common.Interfaces;
using BlazorRcon.Common.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using BlazorRcon.Shared;
using BlazorRcon.ViewModels;

namespace BlazorRcon.Pages;

public partial class Index
{
    private bool _processing;
    private string? _host;
    private int? _port;
    private string? _pwd;
    private string? _command;

    [Inject] public ILocalizationService Localizer { get; set; } = null!;
    [Inject] public IDialogService DialogService { get; set; } = null!;
    [Inject] public MainViewModel ViewModel { get; set; } = null!;

    private RconData RconData { get; set; } = RconData.Default();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            RconData = await ViewModel.LoadStateAsync();
            _host    = RconData.Host;
            _port    = RconData.Port;
            _pwd     = RconData.Password;
            _command = RconData.LastCommand;

            StateHasChanged();
        }
    }

    private async Task SendCommand()
    {
        _processing = true;
        try
        {
            RconData.Host     = _host;
            RconData.Port     = _port;
            RconData.Password = _pwd;
            RconData.LastCommand = _command;

            var result = await ViewModel.SendCommand(RconData);

            var options = new DialogOptions
                          {
                              CloseOnEscapeKey = true,
                              CloseButton      = true,
                              Position         = DialogPosition.Center,
                              FullWidth        = true
                          };
            var parameters = new DialogParameters { { "Content", result } };

            await DialogService.ShowAsync<ResultDialog>(Localizer[Const.RconResponse], parameters, options);
        }
        finally
        {
            _processing = false;
        }
    }
}