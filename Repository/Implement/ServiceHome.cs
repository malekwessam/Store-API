using Store.Repository.Abstract;
using Store.Repository.Implement;
using Storee.Entities;
using Storee.Repository.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storee.Repository.Implement
{
	public class ServiceHome : IServiceHome
	{
		private readonly IRepositoryHome homeRepository;

		public ServiceHome(IRepositoryHome homeRepository)
		{
			this.homeRepository = homeRepository;
		}
		public Task<Home> CreateHomeAsync(Home home)
		{
			return homeRepository.CreateHomeAsync(home);
		}

		public Task<bool> DeleteHomeAsync(short homeId)
		{
			return homeRepository.DeleteHomeAsync(homeId);
		}

		public async Task<Home> GetHomeAndProductsAsync(short homeId)
		{
			return await homeRepository.GetHomeAndProductsAsync(homeId);
		}

		public Task<Home> GetHomeAsync(short homeId)
		{
			return homeRepository.GetHomeAsync(homeId);
		}

		public Task<List<Home>> GetHomesAsync()
		{
			return homeRepository.GetHomesAsync();
		}

		public async Task<bool> IsHomeExistAsync(string name)
		{
			var category = await homeRepository.GetHomeAsync(name);
			return category != null;
		}

		public Task<Home> UpdateHomeAsync(Home home)
		{
			return homeRepository.UpdateHomeAsync(home);
		}
	}
}
