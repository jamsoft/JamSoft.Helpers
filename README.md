![jamsoft-logo](https://github.com/jamsoft/jamsoft.helpers/blob/master/img/logo.png?raw=true)

# JamSoft.Helpers

A collection of general helpers for applications and libraries. The goal is to provide convenience methods and core building blocks. All in a unit tested, cross-platform .NET Standard 2.0 library with minimal dependencies.

![.NET Core](https://github.com/jamsoft/JamSoft.Helpers/workflows/.NET%20Core/badge.svg?branch=master)
[![Coverage Status](https://coveralls.io/repos/github/jamsoft/JamSoft.Helpers/badge.svg?branch=master)](https://coveralls.io/github/jamsoft/JamSoft.Helpers?branch=master)
![Nuget](https://img.shields.io/nuget/v/JamSoft.Helpers)
![GitHub](https://img.shields.io/github/license/jamsoft/JamSoft.Helpers)
[![CodeFactor](https://www.codefactor.io/repository/github/jamsoft/jamsoft.helpers/badge)](https://www.codefactor.io/repository/github/jamsoft/jamsoft.helpers)

## Table of Contents
- [Docs](#Docs)
- [Installation](#Installation)
- [Tests](#Tests)
- [Configuration](#Configuration)
- [Dirty Object Tracking](#Dirty-Object-Tracking)
- [Collections](#Collections)
- [Environment Variables](#Environment)
- [Cryptography](#Cryptography)
- [Graphics](#Graphics)
- [Human Readable UI Values](#UI-Values)
- [Math](#Math)
- [Strings](#Strings)
- [Serialization](#Serialization)
- [Mvvm Pattern](#Mvvm)
- [Observer Pattern](#Observer)
- [Memento Pattern](#Memento)

# Docs

https://jamsoft.github.io/JamSoft.Helpers/

# Installation
### Nuget
```shell
Install-Package JamSoft.Helpers -Version 1.2.4
```
### CLI
```shell
dotnet add package JamSoft.Helpers --version 1.2.4
```
### Package Reference
```xml
<PackageReference Include="JamSoft.Helpers" Version="1.2.4" />
```
### Package Reference
```shell
paket add JamSoft.Helpers --version 1.2.4
```
# Tests

There is a high level of test coverage as shown in the badge, however, at the moment the pipeline executes only on Windows which means some tests cannot be run in this environment.
The library has been fully tested on Windows 10, 11, OSX Catalina, MacOS, and Fedora 31.

The following test classes also show basic example implementations and uses of the provided pattern classes.

- ObserverTests
- MementoTests
- MyTestViewModel
- ATestSettingsClass
- BTestSettingsClass
- PersonViewModel

# Sample Application

A sample AvaloniaUI application is now also included in the Sample directory.

# Configuration

Rather than getting embroiled in the convoluted and sometimes awkward user settings infrastructure provided by .NET (*.settings), sometimes you just want to store some values in a file, yes?

The issues and additional complications around user.config and strong names can sometimes get in the way. Using the `SettingsBase<T>` class can bypass this.

## Set Up

Create a POCO class containing your settings properties, default values and inherit `SettingsBase<T>`, such as:

```csharp
public sealed class MySettings : SettingsBase<MySettings>
{
    public string ASetting { get; set; } = "A Default Value";
}
```
### Loading & Access

Now you can load, save and manage your settings at runtime like:

```csharp
string myDefaultSettingsPath = "C:\Some\location\on\disk";
MySettings.Load(myDefaultSettingsPath);
```

This call will either load the settings from a file called `mysettings.json`, the name is automatically taken from the type name, or if no file exists, the defaults are loaded.

You can also load and save from a provided file name instead of deriving from the type name, such as:

```csharp
string myDefaultSettingsPath = "C:\Some\location\on\disk";
MySettings.Load(myDefaultSettingsPath, "custom-name.json");
```

You can access the values using the instance:

```csharp
var theValue = MySettings.Instance.ASetting;
```
Or
```csharp
MySettings.Instance.ASetting = theValue;
```
### Saving

Saving the settings is a call to the `Save()` method, like:

```csharp
MySettings.Save();
```

This will always save back to the same file the settings were originally loaded from, or if there was no file, the file will be created and the settings saved.

### Reset
You can easily return back to the defaults by calling the `ResetToDefaults()` method, like:
```csharp
MySettings.ResetToDefaults();
```
This will reset all settings to their default values and immediately write them to disk. If you do not want to write them to disk, simply pass a `false` to the method.
```csharp
MySettings.ResetToDefaults(saveToDisk:false);
```
# Dirty Object Tracking
Using the attributes and validators you can track classes with changes, such as view models, in order save new data or update UI state accordingly.

## The Interface
There are a number of ways of implementing something like such as decorators and so forth, but to keep this as pluggable as possible this feature makes use of an interface to implement on your validatable classes.

```csharp
public interface IDirtyMonitoring
{
    /// <summary>
    /// A flag denoting if the object is dirty
    /// </summary>
    bool IsDirty { get; set; }
    
    /// <summary>
    /// The object hash value
    /// </summary>
    string? Hash { get; set; }
}
```
To create instances of validators in 1.3.0 things have changed a bit. It's no longer a state class and is now instantiated from a factory, such as:
```csharp
services.RegisterLazySingleton(() => DirtyValidatorFactory.Create());
```
Then once you have everything registered at runtime this can be resolved for constructor injection.
```csharp
public class MyClass
{
    private IDirtyValidator _dirtyValidator;
    
    public MyClass(IDirtyValidator dirtyValidator) // easier to mock in tests etc
    {
        _dirtyValidator = dirtyValidator;
        ...
    }
    ...
}
```

Or you can simply call the factory directly.
```csharp
public class MyClass
{
    private IDirtyValidator _dirtyValidator;
    
    public MyClass()
    {
        _isDirtyValidator = DirtyValidatorFactory.Create();
        ...
    }
    ...
}
```
Obviously you could also wire this up in your favourite DI container.
## Usage
First implement the interface on your own classes, such as:
```csharp
public class PersonViewModel : IDirtyMonitoring
{
    public string Name { get; set; }
    [IsDirtyMonitoring]
    public string DisplayName { get; set; }
    public bool IsDirty { get; set; }    
    public string? Hash { get; set; }
}
```
In this example only the `DisplayName` property is monitored for changes. After an object has completed being initialised and is in it's "clean" state, validate it.
```csharp
_isDirtyValidator.Validate(p);
```
Now, at any point in time you can validate it again to detect if the class is dirty.
```csharp
p.DisplayName = "Original";
_isDirtyValidator.Validate(p).IsDirty; // false
p.DisplayName = "SomedifferentValue";
_isDirtyValidator.Validate(p).IsDirty; // true
p.DisplayName = "Original";
_isDirtyValidator.Validate(p).IsDirty; // false
```
## Property & Field Tracking

As of v1.2.0 it is also possible to track which properties in a given object instance have changed. In order to track properties, call the `Validate` method and pass `True` as the `trackProperties` parameter.
```csharp
_isDirtyValidator.Validate(p, trackProperties:true);
```
Now that the object, its properties and fields have been validated changes can be reported in more detail. Calls to the `ValidatePropertiesAndFields()` method will return collections of `PropertyInfo` and `FieldInfo` objects, such as:
```csharp
var (props, fields) = _isDirtyValidator.ValidatePropertiesAndFields(p);
```
In the example above the `props` and `fields` collections contain details of the properties and fields that have changed since the previous validation process.

To restart this validation process on the same instance, simply re-validate it and pass `True` again. 
```csharp
_isDirtyValidator.Validate(p, true);
```
## Dirty Monitoring Example Usage

```csharp
public void LoadPeopleFromDataSource()
{
    var vms = Mapper.Map(_dataService.GetPeople());
    foreach(var vm in vms)
    {
        _isDirtyValidator.Validate(p, trackProperties:true);
        ...
    }
}

public void SaveUiState()
{
    foreach(var vm in _people)
    {
        if(_isDirtyValidator.Validate(vm).IsDirty)
        {
            // save logic
            
            var (props, fields) = _isDirtyValidator.ValidatePropertiesAndFields(vm);
            // more granular save logic
        }
    }
}
```
## Managing Hashes

The property containing the hash store is now exposed (since v1.2.5) so you can better manage the resources in use. You can simple set the property to a new empty collection or clear the existing one.

```csharp
_isDirtyValidator.ObjectValueHashStore = new();
```

Or you can perform a complete reset using the provided method with the option of clearing down the cached type data:
```csharp
_isDirtyValidator.Reset(); // clearTypeInfo:false
```
```csharp
// will clear any cached type info
_isDirtyValidator.Reset(clearTypeInfo:true);
```
# Collections
## Shuffle Collections
```csharp
IEnumerable<int> ints = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
IEnumerable<int> shuffledInts = ints.Shuffle();
```
Or you can provide your own instance of `Random`.
```csharp
Random randomiser = new Random();
IEnumerable<int> ints = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
IEnumerable<int> shuffledInts = ints.Shuffle(randomiser);
```

# Environment
There are a handful of helper methods and classes for access environment variables on various platforms.
## Common Variables
```csharp
var path = EnvEx.GetVariable(EnvExVariableNames.Path);
```
## Windows Variables
```csharp
var appData = EnvEx.GetVariable(EnvExWinVariableNames.AppData); // C:\Users\username\AppData\Roaming
```
## OSX Variables
```csharp
var shell = EnvEx.GetVariable(EnvExOsxVariableNames.Shell); // "/bin/bash"
```
## Linux Variables
```csharp
var manPath = EnvEx.GetVariable(EnvExLinuxVariableNames.ManPath); // ":"
```
More variables names are included in the library than are shown above. You can make use of these via helper constants in the following classes:

- EnvExVariableNames (Common)
- EnvExWinVariableNames
- EnvExOsxVariableNames
- EnvExLinuxVariableNames

Since the `EnvEx.GetVariable` method just takes a string, any value can be passed, such as:
```csharp
var envValue = EnvEx.GetVariable("MYVARIABLENAME");
```
On Window you can also pass a target parameter of type `EnvironmentVariableTarget`. The default for this is `Process` as Linux and OSX do not support this parameter. If anything
other than `Process` is passed on a non-Windows platform it will be defaulted to `Process` to prevent exceptions being raised.

# Cryptography
There is a new little class to help digitally sign data with RSA Cryptography. The main class is created via a factory which can be registered in your DI container of choice.
```csharp
public interface IRsaCryptoFactory
{
}

container.Register<IRsaCryptoFactory, RsaCryptoFactory>();
```
You can then use this factory to obtain instances of the service.
```csharp
var crypto = cryptoFactory.Create();
```
There are many overloads of the create method to control how the service is built and how you want it configured.

You should also wrap each use of this within a using statement such as:
```csharp
using(var crypto = cryptoFactory.Create(_privateKey, _publicKey, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1))
{
   // do your stuff.
}
```
Once you have created an instance if you do not provide either of the keys, you can obtain the used keys for storage via the two properties, such as:
```csharp
var crypto = cryptoFactory.Create();
var publicKey = sut.PublicKey;
var privateKey = sut.PrivateKey;
```
# Graphics
## Convert to HEX
```csharp
int red = 121;
int green = 155;
int blue = 56;

var hex = Graphics.Colors.ToHex(red, green, blue);
```
Or you can also use an alpha value.
```csharp
var hex = Graphics.Colors.ToHex(alpha, red, green, blue);
```
You can also pass values as an array
```csharp
var hex = Graphics.Colors.ToHex(new[] { alpha, red, green, blue });
```
## Convert to RGB
```csharp
int red = 255;
int green = 169;
int blue = 104;

var color = Graphics.Colors.ToRgb("#FFA968");
```
## Convert to ARGB
This can be useful for WPF and XAML which supports an alpha value in the HEX.
```csharp
int alpha = 255;
int red = 146;
int green = 145;
int blue = 145;

var c = Graphics.Colors.ToArgb("#FF929191");
```
# UI Values
### Data Sizes
Converts `integer` and `long` values representing data sizes to human readable form
```csharp
int input = 10000000;
input.ToHumanReadable(); returns "9.54 Mb"

long input = 2000000000000000000;
input.ToHumanReadable(); returns "1.73 Eb"
```
### Time
```csharp
double input = 3657;
input.ToTimeDisplayFromSeconds() returns "01:00:57"

double input = 3657.12;
input.ToTimeDisplayFromSeconds(withMs: true) returns "01:00:57:120"

TimeSpan input = new TimeSpan(16, 45, 0);
input.GetTime() returns "16:45"
```

# Math

## Even or Odd
Even number detection
```csharp
int value = 2;
var isMyNumberEven = value.IsEvenNumber();
```
## Percentages
Basic percentage calculations
```csharp
int value = 500;
int total = 2000;

var percent = value.IsWhatPercentageOf(total) // 25
```
# Strings

## Hamming Distance
Calculates the number of edits required to go from one string to another must be equal lengths to start
```csharp
var inputOne = "InputString1";
var inputTwo = "InputString2";

var distance = Distance.GetHammingDistance(inputOne, inputTwo);
inputOne.HammingDistanceTo(inputTwo)
```
## Levenshtein Distance
Calculates the number of edits required to go from one string to another
```csharp
var inputOne = "InputString1";
var inputTwo = "InputString2";

var distance = Distance.GetLevenshteinDistance(inputOne, inputTwo);
inputOne.LevenshteinDistanceTo(inputTwo)
```
## Shorten Strings
This method allows you to shorten strings to a predetermined length and pad with `...` or any pattern you provide.
```csharp
string input = "Thisismylongstringthatneedsshortening";
var result = input.DotShortenString(10, 20); // "Thisism...shortening"

string input = "Thisismylongstringthatneedsshortening";
var result = input.DotShortenString(10, 20, ";;;"); // "Thisism;;;shortening"
```
## Remove All Multi-spaces
```csharp
var input = "  This  has    too  many  spaces   ";
input.RemoveAllMultiSpace(); // " This has too many spaces "
```
```csharp
var input = "  This  has    too  many  spaces   ";
input.RemoveAllMultiSpace(trim:true); // "This has too many spaces"
```
```csharp
var input = "  This  has    too  many  spaces   ";
input.RemoveAllMultiSpace("--"); // "--This--has--too--many--spaces--"
```
```csharp
var input = "  This  has    too  many  spaces   ";
input.RemoveAllMultiSpace(pattern:"--", trim:true) // "This--has--too--many--spaces"
```
## String Compare
```csharp
string input = "string1";
string pattern = "strinG1";

input.IsExactlySameAs(pattern); // false
```
# Serialization

## XML Encoding Formatting
Adds a strict uppercase UTF-8 to XML declarations
```csharp
using (var sw = new UppercaseUtf8StringWriter())
{
    xsSubmit.Serialize(sw, new TestObject { SomeProperty = "SomeValue" });
    xml = sw.ToString();
}
```
# Patterns

## Mvvm
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
## Mvvm - SuperObservableCollection<T>
An observable collection that mutes change notifications when adding a range of objects and allows sorting
```csharp
public class SuperObservableCollection<T> : ObservableCollection<T>
{
    public SuperObservableCollection(IEnumerable<T> coll)
    {
    }

    public void AddRange(IEnumerable<T> list, bool suppressNotifications = true, bool notifiyOnceAllAdded = true)
    {
        ...
    }

    public void Sort(bool suppressNotifications = false)
    {
        ...
    }
	
    public void Sort(IComparer<T> comparer, bool suppressNotifications = false)
    {
        ...
    }
}
```
## Observer
A very basic implementation of the core bits of the observer pattern
```csharp
public interface IObservable
{
    void Attach(IObserver observer);

    void Detach(IObserver observer);

    void Notify();
}
```
```csharp
public interface IObserver
{
    void Update(IObservable observable);
}
```
```csharp
public abstract class ObservableBase : IObservable
{
}
```
## Memento
```csharp
public interface IMemento
{
    object GetState();
}
```
```csharp
public interface IMementoOwner
{
    IMemento Save();

    void Restore(IMemento memento);
}
```
```csharp
public class MementoManager
{
    ...
}
```