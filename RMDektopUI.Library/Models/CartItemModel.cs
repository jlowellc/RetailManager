using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDektopUI.Library.Models
{
	public class CartItemModel
	{
		public ProductModel Product { get; set; }
		public int QunatityInCart { get; set; }
		public string DisplayText => $"{ Product.ProductName } ({ QunatityInCart })";
	}
}
