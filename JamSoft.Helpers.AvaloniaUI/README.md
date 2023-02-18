<img align="center" height="50" src="img/logo.png">

# JamSoft.Helpers.AvaloniaUI

A collection of general helpers for AvaloniaUI applications.

![.NET Core](https://github.com/jamsoft/JamSoft.Helpers/workflows/.NET%20Core/badge.svg?branch=master)
[![Coverage Status](https://coveralls.io/repos/github/jamsoft/JamSoft.Helpers/badge.svg?branch=master)](https://coveralls.io/github/jamsoft/JamSoft.Helpers?branch=master)
![Nuget](https://img.shields.io/nuget/v/JamSoft.Helpers)
![GitHub](https://img.shields.io/github/license/jamsoft/JamSoft.Helpers)

# GitHub Pages Site

https://jamsoft.github.io/JamSoft.Helpers/

# Install
### Nuget
```
Install-Package JamSoft.Helpers.AvaloniaUI
```
### CLI
```
dotnet add package JamSoft.Helpers.AvaloniaUI
```
# Patterns

## Mvvm - ViewModelBase
A very bare bones view model with property changed updates
```csharp
public abstract class ViewModelBase : INotifyPropertyChanged
{
    ...
    public bool IsEditable ...
    ...
    public bool IsBusy ...
    ...
    protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
    {
        ...
    }
}
```