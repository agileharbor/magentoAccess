﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MagentoAccess.MagentoSoapServiceReference;
using MagentoAccess.Misc;
using MagentoAccess.Models.Services.Soap.GetOrders;
using Netco.Logging;
using MagentoAccess.Models.GetShipments;

namespace MagentoAccess.Services.Soap._1_9_2_1_ce
{
	internal partial class MagentoServiceLowLevelSoap_v_1_9_2_1_ce : IMagentoServiceLowLevelSoap
	{
		public virtual async Task< GetOrdersResponse > GetOrdersAsync( DateTime modifiedFrom, DateTime modifiedTo, CancellationToken cancellationToken, Mark mark = null )
		{
			var filters = new filters();
			AddFilter( filters, modifiedFrom.ToSoapParameterString(), "updated_at", "from" );
			AddFilter( filters, modifiedTo.ToSoapParameterString(), "updated_at", "to" );
			if( !string.IsNullOrWhiteSpace( this.Store ) )
				AddFilter( filters, this.Store, "store_id", "in" );

			return await this.GetWithAsync(
				res =>
				{
					//crutch for magento 1.7 
					res.result = res.result.Where( x => Extensions.ToDateTimeOrDefault( x.updated_at ) >= modifiedFrom && Extensions.ToDateTimeOrDefault( x.updated_at ) <= modifiedTo ).ToArray();
					return new GetOrdersResponse( res );
				},
				async ( client, session ) => await client.salesOrderListAsync( session, filters ).ConfigureAwait( false ), 600000, cancellationToken ).ConfigureAwait( false );
		}

		public virtual async Task< GetOrdersResponse > GetOrdersAsync( IEnumerable< string > ordersIds, CancellationToken cancellationToken, string searchField = "increment_id" )
		{
			var ordersIdsAgregated = string.Join( ",", ordersIds );

			var filters = new filters();
			AddFilter( filters, ordersIdsAgregated, searchField, "in" );

			if( !string.IsNullOrWhiteSpace( this.Store ) )
				AddFilter( filters, this.Store, "store_id", "in" );

			return await this.GetWithAsync(
				res => new GetOrdersResponse( res ),
				async ( client, session ) => await client.salesOrderListAsync( session, filters ).ConfigureAwait( false ), 600000, cancellationToken ).ConfigureAwait( false );
		}

		public virtual async Task< OrderInfoResponse > GetOrderAsync( string incrementId, CancellationToken cancellationToken, Mark childMark )
		{
			return await this.GetWithAsync(
				res => new OrderInfoResponse( res ),
				async ( client, session ) => await client.salesOrderInfoAsync( session, incrementId ).ConfigureAwait( false ), 600000, cancellationToken ).ConfigureAwait( false );
		}

		public virtual Task< OrderInfoResponse > GetOrderAsync( Order order, CancellationToken cancellationToken, Mark childMark )
		{
			return this.GetOrderAsync( this.GetOrdersUsesEntityInsteadOfIncrementId ? order.OrderId : order.incrementId, cancellationToken, childMark );
		}

		public Task< Dictionary< string, IEnumerable< Shipment > > > GetOrdersShipmentsAsync( DateTime modifiedFrom, DateTime modifiedTo, CancellationToken token, Mark mark = null )
		{
			return Task.FromResult( new Dictionary< string, IEnumerable< Shipment > >() );
		}
	}
}