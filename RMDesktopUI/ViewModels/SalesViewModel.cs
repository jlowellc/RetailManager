using Caliburn.Micro;

using RMDektopUI.Library.Api;
using RMDektopUI.Library.Models;

using System.ComponentModel;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
	public class SalesViewModel : Screen
	{
		private IProductEndpoint _productEndpoint;

		public SalesViewModel(IProductEndpoint productEndpoint) => _productEndpoint = productEndpoint;

		protected override async void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);
			await LoadProducts();
		}

		private async Task LoadProducts()
		{
			var productList = await _productEndpoint.GetAll();
			Products = new BindingList<ProductModel>(productList);
		}

		private BindingList<ProductModel> _products;

		public BindingList<ProductModel> Products
		{
			get => _products;
			set
			{
				_products = value;
				NotifyOfPropertyChange(() => Products);
			}
		}

		private BindingList<ProductModel> _cart;

		public BindingList<ProductModel> Cart
		{
			get => _cart;
			set
			{
				_cart = value;
				NotifyOfPropertyChange(() => Cart);
			}
		}

		private int _itemQuantity;

		public int ItemQuantity
		{
			get => _itemQuantity;
			set
			{
				_itemQuantity = value;
				NotifyOfPropertyChange(() => ItemQuantity);
			}
		}

		public string SubTotal => "$0.00";          // TODO - Replace with calculation

		public string Tax => "$0.00";       // TODO - Replace with calculation

		public string Total => "$0.00";         // TODO - Replace with calculation

		public bool CanAddToCart
		{
			get
			{
				bool output = false;

				// TODO: Make sure something is selected
				// TODO: Make sure there is an item quantity

				return output;
			}
		}

		public void AddToCart()
		{
		}

		public bool CanRemoveFromCart
		{
			get
			{
				bool output = false;

				// TODO: Make sure something is selected

				return output;
			}
		}

		public void RemoveFromCart()
		{
		}

		public bool CanCheckOut
		{
			get
			{
				bool output = false;

				// TODO: Make sure there is something in the cart

				return output;
			}
		}

		public void CheckOut()
		{
		}
	}
}