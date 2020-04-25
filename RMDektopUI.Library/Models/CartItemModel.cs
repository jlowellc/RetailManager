namespace RMDektopUI.Library.Models
{
	public class CartItemModel
	{
		public ProductModel Product { get; set; }
		public int QunatityInCart { get; set; }
		public string DisplayText => $"{ Product.ProductName } ({ QunatityInCart })";
	}
}