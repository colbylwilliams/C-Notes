using Contacts;

namespace ContextKit
{
	public static class AddressExtensions
	{
		public static string PostalAddress (this ContextPoint point)
		{
			var postalAddress = new CNMutablePostalAddress {
				Street = point.Location.Thoroughfare,
				City = point.Location.Locality,
				State = point.Location.AdministrativeArea,
				PostalCode = point.Location.PostalCode,
				Country = point.Location.Country,
				IsoCountryCode = point.Location.IsoCountryCode
			};

			return CNPostalAddressFormatter.GetStringFrom (postalAddress, CNPostalAddressFormatterStyle.MailingAddress);
		}
	}
}