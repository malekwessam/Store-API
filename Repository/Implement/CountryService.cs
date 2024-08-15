using Storee.Entities;
using Store.Repository.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.Repository.Implement
{
	public class CountryService:ICountryService
	{
		private readonly ICountryRepository CountryRepository;

		public CountryService(ICountryRepository CountryRepository)
		{
			this.CountryRepository = CountryRepository;
		}
		public Task<Country> CreateCountryAsync(Country Country)
		{
			return CountryRepository.CreateCountryAsync(Country);
		}
		public Task<bool> DeleteCountryAsync(int CountryId)
		{
			return CountryRepository.DeleteCountryAsync(CountryId);
		}
		public Task<Country> GetCountryAsync(int CountryId)
		{
			return CountryRepository.GetCountryAsync(CountryId);
		}

		public Task<List<Country>> GetCountrysAsync()
		{
			return CountryRepository.GetCountrysAsync();
		}
		public Task<Country> UpdateCountryAsync(Country Country)
		{
			return CountryRepository.UpdateCountryAsync(Country);
		}
		public async Task<bool> IsCountryExistAsync(string name)
		{
			var Country = await CountryRepository.GetCountryAsync(name);
			return Country != null;
		}

		public async Task<Country> GetCountryAndProductPriceAsync(int? CountryId)
		{
			return await CountryRepository.GetCountryAndProductPriceAsync(CountryId);
		}
	}
}
