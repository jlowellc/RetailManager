namespace RMDataManager.Library.Models
{
	public class ProductModel
	{
		// TODO Do this documentation if needed. May not be needed for this model?

		/// <summary>
		/// The documentation for each of these
		/// </summary>
		public int Id { get; set; }

		public string ProductName { get; set; }
		public string Description { get; set; }
		public decimal RetailPrice { get; set; }
		public int QuantityInStock { get; set; }
	}
}