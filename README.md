# JamSoft.Helpers

A collection of very generalised things applications and libraries may need

# Basic Math Things
```
int value = 2;
var isMyNumberEven = value.IsEvenNumber();
```
## Percentages
```
int value = 500;
int total = 2000;

var percent = value.IsWhatPercentageOf(total) // 25
```
# String Distances

## Hamming Distance
```
var inputOne = "InputString1";
var inputTwo = "InputString2";

var distance = Distance.GetHammingDistance(inputOne, inputTwo);
inputOne.HammingDistanceTo(inputTwo)
```
## Levenshtein Distance
```
var inputOne = "InputString1";
var inputTwo = "InputString2";

var distance = Distance.GetLevenshteinDistance(inputOne, inputTwo);
inputOne.LevenshteinDistanceTo(inputTwo)
```
# Patterns
## Mvvm - ViewModelBase
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