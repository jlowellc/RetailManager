using RMDektopUI.Library.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDektopUI.Library.Api
{
	public interface IProductEndpoint
	{
		Task<List<ProductModel>> GetAll();
	}
}