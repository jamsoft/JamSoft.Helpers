using JamSoft.Helpers.Patterns.Mvvm;
using JamSoft.Helpers.Sample.ViewModels;

namespace JamSoft.Helpers.Sample.Models;

public static class DataSource
{
	public static SuperObservableCollection<PersonViewModel> GetPeople()
	{
		return new SuperObservableCollection<PersonViewModel>
		{
			new() { Name = "Jazzy Jeff", AddressLine1="23 Jazz St", AddressLine2 = "Awesomeville", City = "Jazzton", PostZipCode="129322", PhoneNumber="02324327983" },
			new() { Name = "Kermit The Frog", AddressLine1="44 River St", City = "Muppeton", PostZipCode="129322", PhoneNumber="34534534535" },
			new() { Name = "SpongeBob Squarepants", AddressLine1="Pineapple Palace", City = "Bikini Bottom", PostZipCode="123123", PhoneNumber="72521745642" },
			new() { Name = "Tinky Winky", AddressLine1="Tubbytronic Superdome", City = "Jazzton", PostZipCode="129322", PhoneNumber="62565425645" },
			new() { Name = "Jimmy Cricket", AddressLine1="54 Stump St", City = "Wicketville", PostZipCode="129322", PhoneNumber="62525645256" },
			new() { Name = "Evil Edna", AddressLine1="The Evil Woods", City = "Whispton", PostZipCode="452345", PhoneNumber="83667432825" },
			new() { Name = "Danger Mouse", AddressLine1="1 Bond St", City = "London", PostZipCode="SW1 1DM", PhoneNumber="99345636456" },
			new() { Name = "Penfold", AddressLine1="1 Bond St", City = "London", PostZipCode="SW1 1DM", PhoneNumber="25835643654" },
			new() { Name = "Road Runner", AddressLine1="14 Acme St", City = "Toontown", PostZipCode="129322", PhoneNumber="65287769677" }
		};
	}
}