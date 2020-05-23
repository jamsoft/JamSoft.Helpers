# JamSoft.Helpers

A collection of very generalised things applications and libraries may need

# Install Via Nuget
```
Install-Package JamSoft.Helpers
```
# Graphics
## Convert to HEX
```
int red = 121;
int green = 155;
int blue = 56;

var hex = Graphics.Colors.ToHex(red, green, blue);
```
## Convert to RGB
```
int red = 255;
int green = 169;
int blue = 104;

var color = Graphics.Colors.ToRgb("#FFA968");
```
## Convert to ARGB
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
# Serialization
## XML Encoding Formatting
Adds a strict uppercased UTF-8 to XML declarations
```
using (var sw = new UppercaseUtf8StringWriter())
{
    xsSubmit.Serialize(sw, new TestObject { SomeProperty = "SomeValue" });
    xml = sw.ToString();# Patterns
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