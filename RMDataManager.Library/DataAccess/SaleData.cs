﻿using RMDataManager.Library.Internal.DataAccess;
using RMDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataManager.Library.DataAccess
{
	public class SaleData
	{
		public void SaveSale(SaleModel saleInfo, string cashierId)
		{
			// TODO: Make this SOLID/DRY/Better
			// Start filling in the sale detail models we will save to the database
			List<SaleDetailDbModel> details = new List<SaleDetailDbModel>();
			ProductData products = new ProductData();
			var taxRate = ConfigHelper.GetTaxRate() / 100;

			foreach (var item in saleInfo.SaleDetails)
			{
				var detail = new SaleDetailDbModel
				{
					ProductId = item.ProductId,
					Quantity = item.Quantity
				};

				// Get the information about this product
				var productInfo = products.GetProductById(detail.ProductId);

				if (productInfo == null)
				{
					throw new Exception($"The product Id of { detail.ProductId } could not be found in the database.");
				}

				detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);

				if (productInfo.IsTaxable)
				{
					detail.Tax = (detail.PurchasePrice * taxRate);
				}

				details.Add(detail);
			}

			// Create the Sale model
			SaleDbModel sale = new SaleDbModel
			{
				SubTotal = details.Sum(x => x.PurchasePrice),
				Tax = details.Sum(x => x.Tax),
				CashierId = cashierId
			};

			sale.Total = sale.SubTotal + sale.Tax;

			// Save the sale model
			SqlDataAccess sql = new SqlDataAccess();
			sql.SaveData("dbo.spSale_Insert", sale, "RMData");

			// Get the ID from the sale mode
			sale.Id = sql.LoadData<int, dynamic>("spSale_Lookup", new { sale.CashierId, sale.SaleDate }, "RMData")
				.FirstOrDefault();

			// Finsh filling in the sale detail models
			foreach (var item in details)
			{
				item.SaleId = sale.Id;
				// Save the sale detail models
				sql.SaveData("dbo.spSaleDetail_Insert", item, "RMData");
			}
		}

	}
}
