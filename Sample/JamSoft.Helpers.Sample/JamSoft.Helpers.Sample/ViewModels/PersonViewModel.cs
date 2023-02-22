using JamSoft.Helpers.AvaloniaUI.Patterns.Mvvm;
using JamSoft.Helpers.Ui;
using ReactiveUI;

namespace JamSoft.Helpers.Sample.ViewModels;

public class PersonViewModel : ValidatableAvaloniaViewModelBase
{
    private string? _name;
    private string? _addressLine1;
    private string? _addressLine2;
    private string? _city;
    private string? _postZipCode;
    private string? _phoneNumber;

    [IsDirtyMonitoring]
    public string? Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    
    [IsDirtyMonitoring]
    public string? AddressLine1
    {
        get => _addressLine1;
        set => this.RaiseAndSetIfChanged(ref _addressLine1, value);
    }
    
    [IsDirtyMonitoring]
    public string? AddressLine2
    {
        get => _addressLine2;
        set => this.RaiseAndSetIfChanged(ref _addressLine2, value);
    }
    
    [IsDirtyMonitoring]
    public string? City
    {
        get => _city;
        set => this.RaiseAndSetIfChanged(ref _city, value);
    }
    
    [IsDirtyMonitoring]
    public string? PostZipCode
    {
        get => _postZipCode;
        set => this.RaiseAndSetIfChanged(ref _postZipCode, value);
    }
    
    [IsDirtyMonitoring]
    public string? PhoneNumber
    {
        get => _phoneNumber;
        set => this.RaiseAndSetIfChanged(ref _phoneNumber, value);
    }
}