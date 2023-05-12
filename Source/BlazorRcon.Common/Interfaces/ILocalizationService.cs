namespace BlazorRcon.Common.Interfaces;

public interface ILocalizationService
{
    string this[string text] { get; }
}