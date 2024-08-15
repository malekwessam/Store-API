using Storee.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Storee.Repository.Abstract
{
	public interface IRepositoryHome
	{
		Task<Home> GetHomeAsync(string name);

		Task<Home> GetHomeAsync(short homeId);
		Task<List<Home>> GetHomesAsync();
		Task<Home> CreateHomeAsync(Home home);
		Task<Home> UpdateHomeAsync(Home home);
		Task<bool> DeleteHomeAsync(short homeId);
		Task<Home> GetHomeAndProductsAsync(short homeId);
	}
}
