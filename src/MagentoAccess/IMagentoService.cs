﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MagentoAccess.Misc;
using MagentoAccess.Models.CreateOrders;
using MagentoAccess.Models.CreateProducts;
using MagentoAccess.Models.DeleteProducts;
using MagentoAccess.Models.GetMagentoCoreInfo;
using MagentoAccess.Models.GetOrders;
using MagentoAccess.Models.GetProducts;
using MagentoAccess.Models.PutInventory;
using MagentoAccess.Models.GetShipments;
using Netco.Logging;

namespace MagentoAccess
{
	public interface IMagentoService
	{
		Task< IEnumerable< Order > > GetOrdersAsync( DateTime dateFrom, DateTime dateTo, CancellationToken token, Mark mark = null );

		Task UpdateInventoryAsync( IEnumerable< Inventory > products, CancellationToken token, Mark mark = null );

		Task< IEnumerable< Product > > GetProductsAsync( CancellationToken token, IEnumerable< int > scopes = null, bool includeDetails = false, string productType = null, bool excludeProductByType = false, DateTime? updatedFrom = null, IEnumerable< string > skus = null, bool stockItemsOnly = true, Mark mark = null );

		Task< PingSoapInfo > PingSoapAsync( CancellationToken token, Mark mark = null );

		Task UpdateInventoryBySkuAsync( IEnumerable< InventoryBySku > inventory, CancellationToken token, IEnumerable< int > scopes = null );

		Task< IEnumerable< CreateProductModelResult > > CreateProductAsync( IEnumerable< CreateProductModel > models, CancellationToken token );

		Task< IEnumerable< DeleteProductModelResult > > DeleteProductAsync( IEnumerable< DeleteProductModel > models, CancellationToken token );

		Task< IEnumerable< CreateOrderModelResult > > CreateOrderAsync( IEnumerable< CreateOrderModel > models, CancellationToken token );

		Task< IEnumerable< Order > > GetOrdersAsync( IEnumerable< string > orderIds, CancellationToken token );

		Task< Dictionary< string, IEnumerable< Shipment > > > GetOrdersShipmentsAsync( DateTime modifiedFrom, DateTime modifiedTo, CancellationToken token, Mark mark = null );

		Task< IEnumerable< Product > > FillProductsDetailsAsync( IEnumerable< Product > products, CancellationToken token, Mark mark = null );

		Task< IEnumerable< PingSoapInfo > > DetermineMagentoVersionAsync( CancellationToken token, Mark mark = null );

		MagentoService.SaveAccessToken AfterGettingToken { get; set; }

		Func< string > AdditionalLogInfo { get; set; }

		Task< PingSoapInfo > DetermineMagentoVersionAndSetupServiceAsync( CancellationToken token, Mark mark = null );

		Task< bool > InitAsync( bool supressExc = false );

		/// <summary>
		///	This property can be used by the client to monitor the last access library's network activity time.
		/// </summary>
		DateTime LastActivityTime { get; }
	}
}