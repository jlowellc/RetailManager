using Caliburn.Micro;

using RMDektopUI.Library.Api;
using RMDektopUI.Library.Models;

using System.ComponentModel;
using System.Linq;
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

		private ProductModel _selectedProduct;

		public ProductModel SelectedProduct
		{
			get => _selectedProduct;
			set
			{
				_selectedProduct = value;
				NotifyOfPropertyChange(() => SelectedProduct);
				NotifyOfPropertyChange(() => CanAddToCart);
			}
		}

		private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();

		public BindingList<CartItemModel> Cart
		{
			get => _cart;
			set
			{
				_cart = value;
				NotifyOfPropertyChange(() => Cart);
			}
		}

		private int _itemQuantity = 1;

		public int ItemQuantity
		{
			get => _itemQuantity;
			set
			{
				_itemQuantity = value;
				NotifyOfPropertyChange(() => ItemQuantity);
				NotifyOfPropertyChange(() => CanAddToCart);
			}
		}

		public string SubTotal
		{
			get
			{
				decimal subTotal = 0;

				foreach (var item in Cart)
				{
					subTotal += (item.Product.RetailPrice * item.QunatityInCart);
				}

				return subTotal.ToString("C");          // TODO - Replace with calculation
			}
		}

		public string Tax => "$0.00";       // TODO - Replace with calculation

		public string Total => "$0.00";         // TODO - Replace with calculation

		public bool CanAddToCart
		{
			get
			{
				bool output = false;

				if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
				{
					output = true;
				}

				return output;
			}
		}

		public void AddToCart()
		{
			CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);

			if (existingItem != null)
			{
				existingItem.QunatityInCart += ItemQuantity;

				// HACK: There should be a better way of refreshing the cart display of quantity +/- to item in cart
				Cart.Remove(existingItem);
				Cart.Add(existingItem);
			}
			else
			{
				CartItemModel item = new CartItemModel
				{
					Product = SelectedProduct,
					QunatityInCart = ItemQuantity
				};
				Cart.Add(item);
			}

			SelectedProduct.QuantityInStock -= ItemQuantity;
			ItemQuantity = 1;
			NotifyOfPropertyChange(() => SubTotal);
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
			NotifyOfPropertyChange(() => SubTotal);
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