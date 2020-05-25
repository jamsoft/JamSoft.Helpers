# JamSoft.Helpers

A collection of very generalised helpers for applications and libraries. The idea is to provide convienience methods and building blocks. All in a cross-platform .NET Standard 2.0 library.

# Install Via Nuget
```
Install-Package JamSoft.Helpers
```
# Environment Variables
There are a handful of helper methods for access environment variables on various platforms.
## Common Variables
```
var path = EnvEx.GetVariable(EnvExVariableNames.Path);
```
## Windows Variables
```
var appData = EnvEx.GetVariable(EnvExWinVariableNames.AppData); // C:\Users\username\AppData\Roaming
```
## OSX Variables
```
var shell = EnvEx.GetVariable(EnvExOsxVariableNames.Shell); // "/bin/bash"
```
## Linux Variables
```
var p = EnvEx.GetVariable(EnvExLinuxVariableNames.ManPath); // ":"
```
More variables names are included in the library than are shown above.
# Graphics
## Convert to HEX
```
int red = 121;
int green = 155;
int blue = 56;

var hex = Graphics.Colors.ToHex(red, green, blue);
```
Or you can also use an alpha value.
```
var hex = Graphics.Colors.ToHex(alpha, red, green, blue);
```
You can also pass values as an array
```
var hex = Graphics.Colors.ToHex(new[] { alpha, red, green, blue });
```
## Convert to RGB
```
int red = 255;
int green = 169;
int blue = 104;

var color = Graphics.Colors.ToRgb("#FFA968");
```
## Convert to ARGB
This can be useful for WPF and XAML which supports an alpha value in the HEX.
```
int alpha = 255;
int red = 146;
int green = 145;
int blue = 145;

var c = Graphics.Colors.ToArgb("#FF929191");
```
# Math Things
## Even or Odd
Even number detection
```
int value = 2;
var isMyNumberEven = value.IsEvenNumber();
```
## Percentages
Basic percentage calculations
```
int value = 500;
int total = 2000;

var percent = value.IsWhatPercentageOf(total) // 25
```
# String Distances
## Hamming Distance
Calculates the number of edits required to go from one string to another must be equal lengths to start
```
var inputOne = "InputString1";
var inputTwo = "InputString2";

var distance = Distance.GetHammingDistance(inputOne, inputTwo);
inputOne.HammingDistanceTo(inputTwo)
```
## Levenshtein Distance
Calculates the number of edits required to go from one string to another
```
var inputOne = "InputString1";
var inputTwo = "InputString2";

var distance = Distance.GetLevenshteinDistance(inputOne, inputTwo);
inputOne.LevenshteinDistanceTo(inputTwo)
```
## Shorten Strings
This method allows you to shorten strings to a predetermined length and pad with `...` or any pattern you provide.
```
string input = "Thisismylongstringthatneedsshortening";
var result = input.DotShortenString(10, 20); // "Thisism...shortening"

string input = "Thisismylongstringthatneedsshortening";
var result = input.DotShortenString(10, 20, ";;;"); // "Thisism;;;shortening"
```
## Remove All Multispaces
``
var input = "  This  has    too  many  spaces   ";
input.RemoveAllMultiSpace(); // " This has too many spaces "
``
``
var input = "  This  has    too  many  spaces   ";
input.RemoveAllMultiSpace(trim:true); // "This has too many spaces"
``
``
var input = "  This  has    too  many  spaces   ";
input.RemoveAllMultiSpace("--"); // "--This--has--too--many--spaces--"
``
## String Compare
```
string input = "string1";
string pattern = "strinG1";

input.IsExactlySameAs(pattern); // false
```
## Secure Strings Compare
```
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
```
using (var sw = new UppercaseUtf8StringWriter())
{
    xsSubmit.Serialize(sw, new TestObject { SomeProperty = "SomeValue" });
    xml = sw.ToString();
}
```
# Patterns

## Mvvm - ViewModelBase
A very bare bones view model with property changed updates
```
public abstract class ViewModelBase : INotifyPropertyChanged
{
    ...
	public bool IsEditable ...
	...
	protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
	{
	    ...
	}
}
```
## Mvvm - SuperObservableCollection<T>
An observable collection that mutes change notifications when adding a range of objects and allows sorting
```
public class SuperObservableCollection<T> : ObservableCollection<T>
{
    public SuperObservableCollection(IEnumerable<T> coll)
	{
	}
	
    public void AddRange(IEnumerable<T> list)
	{
	    ...
	}
	
	public void Sort()
	{
	    ...
	}
	
	public void Sort(IComparer<T> comparer)
	{
	    ...
	}
}
```
## Observer
A very basic implementation of the core bits of the observer pattern
```
public interface IObservable
{
    void Attach(IObserver observer);

    void Detach(IObserver observer);

    void Notify();
}
```
```
public interface IObserver
{
    void Update(IObservable observable);
}
```
```
public abstract class ObservableBase : IObservable
{
}
```
## Memento
```
public interface IMemento
{
    object GetState();
}
```
```
public interface IMementoOwner
{
    IMemento Save();

    void Restore(IMemento memento);
}
```
```
public class MementoManager
{
    ...
}
```
# xUnit Tests

There is a high level of test coverage at about 90+% at the moment, The the library has been tested on Windows, OSX and Linux.