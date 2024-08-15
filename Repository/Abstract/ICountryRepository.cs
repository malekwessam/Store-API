using Storee.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Repository.Abstract
{
	public interface ICountryRepository
	{
		Task<Country> GetCountryAsync(string name);

		Task<Country> GetCountryAsync(int CountryId);
		Task<List<Country>> GetCountrysAsync();
		Task<Country> CreateCountryAsync(Country Country);
		Task<Country> UpdateCountryAsync(Country Country);
		Task<bool> DeleteCountryAsync(int CountryId);
		Task<Country> GetCountryAndProductPriceAsync(int? CountryId);
	}
}
