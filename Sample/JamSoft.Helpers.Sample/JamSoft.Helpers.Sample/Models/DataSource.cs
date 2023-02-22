using JamSoft.Helpers.Patterns.Mvvm;
using JamSoft.Helpers.Sample.ViewModels;

namespace JamSoft.Helpers.Sample.Models;

public static class DataSource
{
	public static SuperObservableCollection<PersonViewModel> GetPeople()
	{
		return new SuperObservableCollection<PersonViewModel>()
		{
			new PersonViewModel() { Name = "Jazzy Jeff", AddressLine1="23 Jazz St", AddressLine2 = "Awesomeville", City = "Jazzton", PostZipCode="129322", PhoneNumber="02324327983" },
			new PersonViewModel() { Name = "Kermit The Frog", AddressLine1="44 River St", City = "Muppeton", PostZipCode="129322", PhoneNumber="34534534535" },
			new PersonViewModel() { Name = "Sponge Bob", AddressLine1="23 Jazz St", City = "Jazzton", PostZipCode="129322", PhoneNumber="72521745642" },
			new PersonViewModel() { Name = "Tinky Winky", AddressLine1="23 Jazz St", City = "Jazzton", PostZipCode="129322", PhoneNumber="62565425645" },
			new PersonViewModel() { Name = "Jimmy Cricket", AddressLine1="23 Jazz St", City = "Jazzton", PostZipCode="129322", PhoneNumber="62525645256" },
			new PersonViewModel() { Name = "Evil Edna", AddressLine1="23 Jazz St", City = "Jazzton", PostZipCode="129322", PhoneNumber="83667432825" },
			new PersonViewModel() { Name = "Danger Mouse", AddressLine1="23 Bond St", City = "London", PostZipCode="SW1", PhoneNumber="99345636456" },
			new PersonViewModel() { Name = "Penfold", AddressLine1="1 Bond St", City = "London", PostZipCode="SW1", PhoneNumber="25835643654" },
			new PersonViewModel() { Name = "Road Runner", AddressLine1="23 Acme St", City = "Toontown", PostZipCode="129322", PhoneNumber="65287769677" }
		};
	}
}