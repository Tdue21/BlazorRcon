using BlazorRcon.Interfaces;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BlazorRcon.Shared;

public partial class ResultDialog
{
#pragma warning disable CS8618
    [Inject] private ILocalizationService Localizer { get; set; }
#pragma warning restore CS8618

    [CascadingParameter] MudDialogInstance MudDialog { get; set; } = null!;
    [Parameter] public string? Content { get; set; }

    private void OkClicked() => MudDialog.Close(DialogResult.Ok(true));
}