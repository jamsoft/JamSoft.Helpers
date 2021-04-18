# JamSoft.Helpers

A collection of general helpers for applications and libraries. The goal is to provide convienience methods and core building blocks. All in a cross-platform .NET Standard 2.0 library.

![.NET Core](https://github.com/jamsoft/JamSoft.Helpers/workflows/.NET%20Core/badge.svg?branch=master)
[![Coverage Status](https://coveralls.io/repos/github/jamsoft/JamSoft.Helpers/badge.svg?branch=master)](https://coveralls.io/github/jamsoft/JamSoft.Helpers?branch=master)
![Nuget](https://img.shields.io/nuget/v/JamSoft.Helpers)
![GitHub](https://img.shields.io/github/license/jamsoft/JamSoft.Helpers)

# GitHub Pages Site

https://jamsoft.github.io/JamSoft.Helpers/

# Install Via Nuget
```
Install-Package JamSoft.Helpers
```
# xUnit Tests

There is a high level of test coverage as shown in the badge above, however, at the moment the pipeline executes on Linux with limited permissions which means some tests cannot be run in this environment.
The library has been fully tested on Windows 10, OSX Catalina and Fedora 31.

The following test classes also show basic example implementations and uses of the provided pattern classes.

- ObserverTests
- MementoTests
- MyTestViewModel

# Environment Variables
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

# RSA Cryptography
There is a new little class to help digitally sign data with RSA Cryptography. The main class is created via a factory which can be registered in your DI container of choice.
```
public interface IRsaCryptoFactory
{
}

container.Register<IRsaCryptoFactory, RsaCryptoFactory>();
```
You can then use this factory to obtain instances of the service.
```
var crypto = cryptoFactory.Create();
```
There are many overloads of the create method to control how the service is built and how you want it configued.

You should also wrap each use of this within a using statement such as:
```
using(var crypto = cryptoFactory.Create(_privateKey, _publicKey, HashAlgorithmName.SHA512, RSASignaturePadding.Pkcs1))
{
   // do your stuff.
}
```
Once you have created an instance if you do not provide either of the keys, you can obtain the used keys for storage via the two properties, such as:
```
var crypto = cryptoFactory.Create();
var publicKey = sut.PublicKey
var privateKey = sut.PrivateKey
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
# Math Things

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
# String Distances

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
## Remove All Multispaces
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
## Secure Strings Compare
```csharp
// #dontdothisinproduction
var input1 = new SecureString();
foreach (char c in "QqNK8f#X4t7lZYomTC#c1rFz9^Xl8rAO!".ToCharArray())
{
    input1.AppendChar(c);
}

// #dontdothisinproduction
var input2 = new SecureString();
foreach (char c in "QqNK8f#X4t7lZYomTC#c1rFz9^Xl8rAO!".ToCharArray())
{
    input2.AppendChar(c);
}

input1.IsExactlySameAs(input2); // true
```

# Serialization

## XML Encoding Formatting
Adds a strict uppercased UTF-8 to XML declarations
```csharp
using (var sw = new UppercaseUtf8StringWriter())
{
    xsSubmit.Serialize(sw, new TestObject { SomeProperty = "SomeValue" });
    xml = sw.ToString();
}
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
## Mvvm - SuperObservableCollection<T>
An observable collection that mutes change notifications when adding a range of objects and allows sorting
```csharp
public class SuperObservableCollection<T> : ObservableCollection<T>
{
    public SuperObservableCollection(IEnumerable<T> coll)
    {
    }

    public void AddRange(IEnumerable<T> list, bool suppressNotifications = true)
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