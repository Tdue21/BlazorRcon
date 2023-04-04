using BlazorRcon.Interfaces;

namespace BlazorRcon.Services;

public class LocalizationService : ILocalizationService
{
    public string this[string text] => text;
}