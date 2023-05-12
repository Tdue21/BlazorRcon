using BlazorRcon.Common.Interfaces;

namespace BlazorRcon.Common.Services;

public class LocalizationService : ILocalizationService
{
    public string this[string text] => text;
}