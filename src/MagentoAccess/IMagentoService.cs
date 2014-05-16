﻿using System;
using System.Collections.Generic;
using MagentoAccess.Models.GetOrders;
using MagentoAccess.Models.GetProducts;
using MagentoAccess.Models.PutStockItems;

namespace MagentoAccess
{
	public interface IMagentoService
	{
		IEnumerable< Order > GetOrders( DateTime dateFrom, DateTime dateTo );

		//Task<IEnumerable<Order>> GetOrdersAsync(DateTime dateFrom, DateTime dateTo);

		void UpdateProducts(IEnumerable<InventoryItem> products);

		//Task UpdateProductsAsync(IEnumerable<InventoryStatusRequest> products);

		IEnumerable< Product > GetProducts();

		//Task<IEnumerable<Item>> GetProductsAsync();

		//Task<IEnumerable<Item>> GetProductsAsync(DateTime createTimeFrom);

		//Task<IEnumerable<Item>> GetProductsAsync(DateTime createTimeFromStart, DateTime createTimeFromTo);
	}
}