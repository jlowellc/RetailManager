using RMDektopUI.Library.Models;
using System.Threading.Tasks;

namespace RMDektopUI.Library.Api
{
	public interface ISaleEndpoint
	{
		Task PostSale(SaleModel sale);
	}
}