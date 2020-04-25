using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;

using System.Collections.Generic;

namespace RMDataManager.Library.DataAccess
{
	public class ProductData
	{
		public List<ProductModel> GetProducts()
		{
			SqlDataAccess sql = new SqlDataAccess();

			// TODO this could be an error
			List<ProductModel> output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "RMData");

			return output;
		}
	}
}