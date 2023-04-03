namespace BlazorRcon.Interfaces;

public interface ILocalizationService
{
    string this[string text] { get; }
}